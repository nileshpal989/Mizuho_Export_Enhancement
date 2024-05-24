<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_LC_DESCOUNTING_Settlement_ACC_IBD_Checker.aspx.cs" Inherits="IMP_Transactions_TF_IMP_LC_DESCOUNTING_Settlement_ACC_IBD_Checker" %>

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
    <script src="../Scripts/TF_IMP_IBD_Settlement_Checker.js" type="text/javascript"></script>
    <script id="Save_script" language="javascript" type="text/javascript">
        
        $(document).ready(function (e) {
            $("input").keypress(function (e) {
                var k;
                document.all ? k = e.keyCode : k = e.which;
                return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || k == 46 || k == 45 || k == 44 || (k >= 48 && k <= 57));
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
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="conditional">
            <ContentTemplate>
                <table id="tbl_Header" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom">
                            <span class="pageLabel"><strong>Own LC Bill Discounting Settlement- Checker</strong></span>
                        </td>
                        <td align="right" style="width: 50%" valign="bottom">
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                OnClientClick="return OnBackClick();" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%" valign="middle" colspan="2">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%;" valign="top" colspan="2">
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            <%-------------------------  hidden fields  --------------------------------%>
                            <input type="hidden" id="hdnDocNo" runat="server" />
                            <input type="hidden" id="hdnUserName" runat="server" />
                            <input type="hidden" id="hdnBranchName" runat="server" />
                            <input type="hidden" id="hdnDocType" runat="server" />
                            <input type="hidden" id="hdnMode" runat="server" />
                            <input type="hidden" id="hdnNegoRemiBankType" runat="server" />
                            <input type="hidden" id="hdnRejectReason" runat="server" />
                            <input type="hidden" id="hdnDocScrutiny" runat="server" />
                            <input type="hidden" id="hdnIBDInterest_MATU" runat="server" />
                        </td>
                    </tr>
                </table>
                <table id="tbl_LCDiscount" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="right" width="10%">
                            <span class="elementLabel">IBD. Ref. No :</span>
                        </td>
                        <td width="30%" align="left">
                            <asp:TextBox Enabled="false" ID="txtIBDDocNo" Width="110px" runat="server" CssClass="textBox" TabIndex="1"></asp:TextBox>
                            
                            &nbsp;
                            <asp:Label ID="lblForeign_Local" runat="server" CssClass="elementLabel"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblLCDescount_Lodgment_UnderLC" runat="server" CssClass="elementLabel"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblSight_Usance" runat="server" CssClass="elementLabel"></asp:Label>
                        </td>
                        <td width="40%" align="left">
                            <span class="elementLabel">Val.Date :</span>
                            <asp:TextBox Enabled="false" ID="txtValueDate" runat="server" TabIndex="2" CssClass="textBox" MaxLength="10"
                                ValidationGroup="dtVal" Width="80px"></asp:TextBox>
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
                        <td align="right" width="10%">
                            <span class="elementLabel">Trans. Ref. No :</span>
                        </td>
                        <td align="left" width="20%">
                            <asp:TextBox Enabled="false" ID="txtDocNo" Width="110px" runat="server" CssClass="textBox" TabIndex="3"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="5">
                            <ajaxToolkit:TabContainer ID="TabContainerMain" runat="server" ActiveTabIndex="3"
                                CssClass="ajax__tab_xp-theme">
                                <ajaxToolkit:TabPanel ID="tbDocumentDetails" runat="server" HeaderText="ACC DOCUMENT DETAILS"
                                    Font-Bold="true" ForeColor="White">
                                    <ContentTemplate>
                                        <table width="100%">
                                        <tr>
                                                <td align="left">
                                                    <asp:CheckBox Enabled="false" ID="chk_AccDocDetails" Text="ACC DOCUMENT DETAILS" runat="server" CssClass="elementLabel"/>
                                                </td>
                                            </tr>
                                            <asp:Panel ID="Panel_AccDocDetails" runat="server">
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">CUSTOMER NAME:</span>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:TextBox Enabled="false" ID="txtCustName" runat="server"  CssClass="textBox"
                                                        TabIndex="1" MaxLength="40" Width="350px"></asp:TextBox>
                                                </td>
                                                <td align="left" colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">COMMENT CODE :</span>
                                                </td>
                                                <td align="left" width="10%">
                                                    <asp:TextBox Enabled="false" ID="txtCommentCode" runat="server" CssClass="textBox" MaxLength="2"
                                                        TabIndex="4" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="right" width="10%">
                                                </td>
                                                <td align="left" width="10%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">MATURITY :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtMaturityDate" runat="server"  CssClass="textBox"
                                                        MaxLength="10"  TabIndex="5" ValidationGroup="dtVal"
                                                        Width="80px"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" CultureAMPMPlaceholder="AM;PM"
                                                        CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY" CultureDatePlaceholder="/"
                                                        CultureDecimalPlaceholder="." CultureName="en-GB" CultureThousandsPlaceholder=","
                                                        CultureTimePlaceholder=":" Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txtMaturityDate">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btnCal_txtMaturityDate" runat="server" CssClass="btncalendar_enabled" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtendertxtMaturityDate" runat="server"
                                                        Enabled="True" Format="dd/MM/yyyy" PopupButtonID="btnCal_txtMaturityDate" TargetControlID="txtMaturityDate">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td align="right" width="10%">
                                                </td>
                                                <td align="left" width="10">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">SETTL FOR CUST :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtsettlCodeForCust" runat="server" CssClass="textBox" TabIndex="7"
                                                        MaxLength="7" Width="50px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">SETTL FOR BANK :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtSettl_CodeForBank" runat="server" CssClass="textBox" MaxLength="12"
                                                        TabIndex="17" Width="50px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">INTEREST FROM :</span>
                                                    <td align="left">
                                                        <asp:TextBox Enabled="false" ID="txtInterest_From" runat="server" CssClass="textBox"
                                                            MaxLength="10"  TabIndex="8" ValidationGroup="dtVal"
                                                            Width="80px"></asp:TextBox>
                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder="AM;PM"
                                                            CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY" CultureDatePlaceholder="/"
                                                            CultureDecimalPlaceholder="." CultureName="en-GB" CultureThousandsPlaceholder=","
                                                            CultureTimePlaceholder=":" Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999"
                                                            MaskType="Date" TargetControlID="txtInterest_From">
                                                        </ajaxToolkit:MaskedEditExtender>
                                                        <asp:Button ID="btnCal_txtInterest_From" runat="server" CssClass="btncalendar_enabled" />
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtendertxtInterest_From" runat="server"
                                                            Enabled="True" Format="dd/MM/yyyy" PopupButtonID="btnCal_txtInterest_From" TargetControlID="txtInterest_From">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                    <td align="right" width="10%">
                                                        <span class="elementLabel">DISCOUNT :</span>
                                                    </td>
                                                    <td align="left" width="10%">
                                                        <asp:TextBox Enabled="false" ID="txtDiscount" runat="server" CssClass="textBox" MaxLength="16" Width="120px"
                                                            TabIndex="6" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">INTEREST TO :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtInterest_To" runat="server" CssClass="textBox"
                                                        MaxLength="10" TabIndex="9" ValidationGroup="dtVal"
                                                        Width="80px"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder="AM;PM"
                                                        CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY" CultureDatePlaceholder="/"
                                                        CultureDecimalPlaceholder="." CultureName="en-GB" CultureThousandsPlaceholder=","
                                                        CultureTimePlaceholder=":" Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txtInterest_To">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btnCal_txtInterest_To" runat="server" CssClass="btncalendar_enabled" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtendertxtInterest_To" runat="server"
                                                        Enabled="True" Format="dd/MM/yyyy" PopupButtonID="btnCal_txtInterest_To" TargetControlID="txtInterest_To">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">NO. OF DAYS :</span>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:TextBox Enabled="false" ID="txt_No_Of_Days" runat="server" CssClass="textBox" MaxLength="3"
                                                        TabIndex="10" Width="50px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">RATE(%) :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_INT_Rate" runat="server" CssClass="textBox" MaxLength="10" Style="text-align: right"
                                                        TabIndex="11" Width="70px"></asp:TextBox>
                                                </td>
                                                <%-- <td align="right">
                                                            <span class="elementLabel">BASE RATE :</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox Enabled="false" ID="txtBaseRate" runat="server" CssClass="textBox" MaxLength="2" 
                                                                Style="text-align: right" TabIndex="12" Width="70px"></asp:TextBox>
                                                        </td>--%>
                                                <%--<td align="right">
                                                            <span class="elementLabel">SPREAD(%) :</span>
                                                        </td>
                                                        <td align="left" colspan="3">
                                                            <asp:TextBox Enabled="false" ID="txtSpread" runat="server" CssClass="textBox" MaxLength="9" 
                                                                Style="text-align: right" TabIndex="13" Width="70px"></asp:TextBox>
                                                        </td>--%>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">AMOUNT :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtInterestAmt" runat="server" CssClass="textBox"
                                                        MaxLength="16" Style="text-align: right" TabIndex="14" Width="100px" ></asp:TextBox>
                                                </td>
                                               
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">OVERDUE </span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">INTEREST RATE(%) :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtOverinterestRate" runat="server" CssClass="textBox" MaxLength="10"
                                                        Style="text-align: right" TabIndex="28" Width="70px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">NO. OF DAYS :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtOverNoOfDays" runat="server" CssClass="textBox" MaxLength="3"
                                                        TabIndex="27" Width="50px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">AMOUNT :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtOverAmount" runat="server" CssClass="textBox"
                                                        MaxLength="16" Style="text-align: right" TabIndex="14" Width="100px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                
                                                <td align="right">
                                                    <span class="elementLabel">ATTN :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtAttn" runat="server" CssClass="textBox" MaxLength="70" TabIndex="18"
                                                        Width="350px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            </asp:Panel>
                                            <tr>
                                                <td align="center" colspan="2">
                                                </td>
                                                <td colspan="6">
                                                    <asp:Button ID="btnDocNext" runat="server" CssClass="buttonDefault" OnClientClick="return OnDocNextClick(1);"
                                                        TabIndex="19" Text="Next &gt;&gt;" ToolTip="Go to IMPORT ACCOUNTING" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel ID="tbDocumentAccounting" runat="server" HeaderText="ACC IMPORT ACCOUNTING"
                                    Font-Bold="true" ForeColor="White">
                                    <ContentTemplate>
                                        <table width="80%">
                                        <tr>
                                                <td align="left">
                                                    <asp:CheckBox Enabled="false" ID="chk_AccImpAccounting" Text="ACC IMPORT ACCOUNTING" runat="server" CssClass="elementLabel" />
                                                </td>
                                            </tr>
                                            <asp:Panel ID="Panel_AccImpAccounting" runat="server" >
                                            <tr>
                                                <td>
                                                </td>
                                                <td align="left" colspan="2">
                                                    <span class="elementLabel">1-DISC </span>
                                                </td>
                                                <td align="right" colspan="2">
                                                    <span class="elementLabel">AMOUNT :</span><asp:TextBox Enabled="false" ID="txt_DiscAmt" runat="server"
                                                        CssClass="textBox" TabIndex="72" MaxLength="16" Width="100px" Style="text-align: right"></asp:TextBox>
                                                </td>
                                                <td align="left" colspan="2">
                                                    <span class="elementLabel">EXCH RATE :</span><asp:TextBox Enabled="false" ID="txt_IMP_ACC_ExchRate"
                                                        runat="server" CssClass="textBox" TabIndex="73" MaxLength="9" Width="70px" Style="text-align: right"
                                                        ></asp:TextBox>
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
                                                    <asp:TextBox Enabled="false" ID="txtPrinc_matu" runat="server" CssClass="textBox" MaxLength="1" TabIndex="74"
                                                        Width="40px" ></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtPrinc_lump" runat="server" CssClass="textBox" MaxLength="1" TabIndex="75"
                                                        Width="40px" ></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtprinc_Contract_no" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="76" Width="90px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_Princ_Ex_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                        TabIndex="77" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtprinc_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="78"  Width="70px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtprinc_Intnl_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="79" Width="70px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">INTEREST :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtInterest_matu" runat="server"  CssClass="textBox"
                                                        MaxLength="1" TabIndex="80" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtInterest_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                         TabIndex="81" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtInterest_Contract_no" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="82" Width="90px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_interest_Ex_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                        TabIndex="83" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtInterest_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                         TabIndex="84" Width="70px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtInterest_Intnl_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="85" Width="70px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">COMMISSION :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtCommission_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                        TabIndex="86" Width="40px" ></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtCommission_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                        TabIndex="87" Width="40px" ></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtCommission_Contract_no" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="88" Width="90px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_Commission_Ex_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                        TabIndex="89" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtCommission_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="89" Width="70px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtCommission_Intnl_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="90"  Width="70px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">THEIR COMMISSION :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtTheir_Commission_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                        TabIndex="91" Width="40px" ></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtTheir_Commission_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                        TabIndex="92" Width="40px" ></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtTheir_Commission_Contract_no" runat="server" CssClass="textBox"
                                                        TabIndex="93" MaxLength="9" Width="90px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_Their_Commission_Ex_Curr" runat="server" CssClass="textBox"
                                                        TabIndex="94" MaxLength="3" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtTheir_Commission_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="95"  Width="70px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txtTheir_Commission_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                        TabIndex="96"  MaxLength="9" Width="70px"></asp:TextBox>
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
                                                                <asp:TextBox Enabled="false" ID="txt_CR_Code" runat="server" CssClass="textBox" MaxLength="5" 
                                                                    TabIndex="97" Width="90px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox Enabled="false" ID="txt_CR_AC_ShortName" runat="server" CssClass="textBox" MaxLength="20"
                                                                    TabIndex="98" Width="160px"></asp:TextBox>
                                                               
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox Enabled="false" ID="txt_CR_Cust_abbr" runat="server" CssClass="textBox" MaxLength="12"
                                                                    TabIndex="99" Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox Enabled="false" ID="txt_CR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="20"
                                                                    TabIndex="100" Width="90px"></asp:TextBox>
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox Enabled="false" ID="txt_CR_Acceptance_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                    TabIndex="101" Width="40px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox Enabled="false" ID="txt_CR_Acceptance_amt" runat="server" CssClass="textBox" MaxLength="9"
                                                                     TabIndex="102" Width="90px" Style="text-align: right"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox Enabled="false" ID="txt_CR_Acceptance_payer" runat="server" CssClass="textBox" MaxLength="1"
                                                                    TabIndex="103" Width="50px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                            </td>
                                                            <td align="left" colspan="3">
                                                                <span class="elementLabel">INTEREST</span>
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox Enabled="false" ID="txt_CR_Interest_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                    TabIndex="104" Width="40px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox Enabled="false" ID="txt_CR_Interest_amt" runat="server" CssClass="textBox" MaxLength="9"
                                                                     TabIndex="105" Width="90px" Style="text-align: right"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox Enabled="false" ID="txt_CR_Interest_payer" runat="server" CssClass="textBox" MaxLength="1"
                                                                    TabIndex="106" Width="50px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <caption>
                                                            <tr>
                                                                <td align="right">
                                                                </td>
                                                                <td align="left" colspan="3">
                                                                    <span class="elementLabel">ACCEPTANCE COMMISSION</span>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:TextBox Enabled="false" ID="txt_CR_Accept_Commission_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="107" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_CR_Accept_Commission_amt" runat="server" CssClass="textBox"
                                                                        MaxLength="9"  
                                                                        Style="text-align: right" TabIndex="108" Width="90px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_CR_Accept_Commission_Payer" runat="server" CssClass="textBox"
                                                                        MaxLength="1" TabIndex="109" Width="50px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                </td>
                                                                <td align="left" colspan="3">
                                                                    <span class="elementLabel">PAYMENT/HANDLING COMMISSION</span>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:TextBox Enabled="false" ID="txt_CR_Pay_Handle_Commission_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="110" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_CR_Pay_Handle_Commission_amt" runat="server" CssClass="textBox"
                                                                        MaxLength="9"  Style="text-align: right"
                                                                        TabIndex="111" Width="90px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_CR_Pay_Handle_Commission_Payer" runat="server" CssClass="textBox"
                                                                        MaxLength="1" TabIndex="112" Width="50px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                </td>
                                                                <td align="left" colspan="3">
                                                                    <span class="elementLabel">OTHERS</span>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:TextBox Enabled="false" ID="txt_CR_Others_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                        TabIndex="113" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_CR_Others_amt" runat="server" CssClass="textBox" MaxLength="9"
                                                                         Style="text-align: right" TabIndex="114"
                                                                        Width="90px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_CR_Others_Payer" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="115" Width="50px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                </td>
                                                                <td align="left" colspan="3">
                                                                    <span class="elementLabel">THEIR COMMISSION</span>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:TextBox Enabled="false" ID="txt_CR_Their_Commission_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="116" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_CR_Their_Commission_amt" runat="server" CssClass="textBox" MaxLength="9"
                                                                         Style="text-align: right" TabIndex="117"
                                                                        Width="90px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_CR_Their_Commission_Payer" runat="server" CssClass="textBox"
                                                                        MaxLength="1" TabIndex="118" Width="50px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel"><b>/DR/</b> CODE:</span>
                                                                </td>
                                                                <td align="left" colspan="3">
                                                                </td>
                                                                <td align="center">
                                                                </td>
                                                                <td align="left">
                                                                </td>
                                                                <td align="left">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:TextBox Enabled="false" ID="txt_DR_Code" runat="server" CssClass="textBox" MaxLength="5" TabIndex="125"
                                                                        Width="90px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <span class="elementLabel">CURRENT ACCOUNT</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_DR_Cust_abbr" runat="server" CssClass="textBox" 
                                                                        MaxLength="12" TabIndex="126" Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_DR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="20"
                                                                        TabIndex="127" Width="90px"></asp:TextBox>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:TextBox Enabled="false" ID="txt_DR_Cur_Acc_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                        TabIndex="128" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_DR_Cur_Acc_amt" runat="server" CssClass="textBox" MaxLength="9"
                                                                         Style="text-align: right" TabIndex="129"
                                                                        Width="90px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_DR_Cur_Acc_payer" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="130" Width="50px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                </td>
                                                                <td align="left" colspan="3">
                                                                </td>
                                                                <td align="center">
                                                                    <asp:TextBox Enabled="false" ID="txt_DR_Cur_Acc_Curr2" runat="server" CssClass="textBox" MaxLength="3"
                                                                        TabIndex="131" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_DR_Cur_Acc_amt2" runat="server" CssClass="textBox" MaxLength="9"
                                                                         Style="text-align: right" TabIndex="132"
                                                                        Width="90px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_DR_Cur_Acc_payer2" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="133" Width="50px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                </td>
                                                                <td colspan="3">
                                                                </td>
                                                                <td align="center">
                                                                    <asp:TextBox Enabled="false" ID="txt_DR_Cur_Acc_Curr3" runat="server" CssClass="textBox" MaxLength="3"
                                                                        TabIndex="134" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_DR_Cur_Acc_amt3" runat="server" CssClass="textBox" MaxLength="9"
                                                                         Style="text-align: right" TabIndex="135"
                                                                        Width="90px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_DR_Cur_Acc_payer3" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="136" Width="50px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </caption>
                                                    </table>
                                                </td>
                                            </tr>
                                            </asp:Panel>
                                            <tr>
                                                <td align="left" colspan="7">
                                                    <asp:Button ID="btnDocAccPrev1" runat="server" CssClass="buttonDefault" OnClientClick="return OnDocNextClick(0);"
                                                        TabIndex="159" Text="&lt;&lt; Prev" ToolTip="Back to Document Details" />
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnImpAccNext1" runat="server" CssClass="buttonDefault" OnClientClick="return OnDocNextClick(2);"
                                                        TabIndex="137" Text="Next &gt;&gt;" ToolTip="Next To GENERAL OPERATION I" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel ID="tbDocumentGO1" runat="server" HeaderText="GENERAL OPERATION I"
                                    Font-Bold="true" ForeColor="White">
                                    <ContentTemplate>
                                        <table width="80%">
                                            <tr>
                                                <td align="left">
                                                    <asp:CheckBox Enabled="false" ID="chk_GO1_Flag" Text="GENERAL OPERATION I" runat="server" CssClass="elementLabel"
                                                         />
                                                </td>
                                            </tr>
                                            <asp:Panel ID="Panel_GO1" runat="server" Visible="False">
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">COMMENT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Comment" runat="server" CssClass="textBox" MaxLength="20"
                                                            TabIndex="80"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" width="20%">
                                                        <span class="elementLabel">Section No : </span>
                                                    </td>
                                                    <td width="20%">
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_SectionNo" runat="server" MaxLength="2" CssClass="textBox"
                                                            TabIndex="81" Width="30px"></asp:TextBox>
                                                    </td>
                                                    <td align="right" width="15%">
                                                        <span class="elementLabel">Remarks : </span>
                                                    </td>
                                                    <td width="20%">
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Remarks" runat="server" MaxLength="30" CssClass="textBox"
                                                            Width="260px" TabIndex="82" ></asp:TextBox>
                                                    </td>
                                                    <td align="right" width="15%">
                                                        <span class="elementLabel">MEMO : </span>
                                                    </td>
                                                    <td width="10%">
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Memo" runat="server" MaxLength="20" CssClass="textBox" Width="50px"
                                                            TabIndex="83"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" width="15%">
                                                        <span class="elementLabel">SCHEME No : </span>
                                                    </td>
                                                    <td width="20%">
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Scheme_no" runat="server" MaxLength="20" CssClass="textBox"
                                                            TabIndex="84"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">DEBIT / CREDIT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList Enabled="false" ID="txt_GO1_Debit_Code" runat="server" Width="70px" CssClass="dropdownList"
                                                            TabIndex="84" onchange="return TogggleDebitCreditCode('1','1');">
                                                            <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                            <asp:ListItem Value="D" Text="D"></asp:ListItem>
                                                            <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">CCY : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Debit_Curr" runat="server" MaxLength="3" CssClass="textBox"
                                                            TabIndex="86" Width="45px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">AMOUNT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Debit_Amt" runat="server" CssClass="textBox" Width="90px"
                                                             TabIndex="87" Style="text-align: right" MaxLength="16" ></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">CUSTOMER : </span>
                                                    </td>
                                                    <td colspan="4">
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Debit_Cust" runat="server" CssClass="textBox" MaxLength="20"
                                                            TabIndex="88" Width="90px"></asp:TextBox>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Debit_Cust_Name" runat="server" CssClass="textBox" MaxLength="40"
                                                            TabIndex="88" Width="150px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">A/C CODE : </span>
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Debit_Cust_AcCode" MaxLength="20" runat="server" CssClass="textBox"
                                                            TabIndex="89" Width="90px"></asp:TextBox>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Debit_Cust_AcCode_Name" MaxLength="40" runat="server" CssClass="textBox"
                                                            TabIndex="89" Width="150px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">A/C No : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Debit_Cust_AccNo" runat="server" MaxLength="20" CssClass="textBox"
                                                            TabIndex="90"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" align="right">
                                                        <span class="elementLabel">EXCH RATE : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Debit_Exch_Rate" runat="server" CssClass="textBox" 
                                                            TabIndex="91" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">EXCH CCY : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Debit_Exch_CCY" runat="server" CssClass="textBox" MaxLength="3"
                                                            TabIndex="92" Width="35px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">FUND : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Debit_FUND" runat="server" MaxLength="1" CssClass="textBox"
                                                            TabIndex="93" Width="30px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">CHECK No. : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Debit_Check_No" runat="server" CssClass="textBox" TabIndex="94"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">AVAILABLE : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Debit_Available" MaxLength="20" runat="server" CssClass="textBox"
                                                            TabIndex="95" Width="90px" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">ADVICE PRINT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Debit_AdPrint" runat="server" CssClass="textBox" TabIndex="96"
                                                            Width="30px" MaxLength="1"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">DETAILS : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Debit_Details" runat="server" MaxLength="30" CssClass="textBox"
                                                            Width="260px" TabIndex="97"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">ENTITY : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Debit_Entity" runat="server" CssClass="textBox" 
                                                            TabIndex="98" Width="90px" Style="text-align: right" MaxLength="3"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">DIVISION : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Debit_Division" runat="server" CssClass="textBox" TabIndex="99"
                                                            Width="30px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">INTER-AMOUNT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Debit_Inter_Amount" runat="server" CssClass="textBox" TabIndex="100"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">INTER-RATE : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Debit_Inter_Rate" runat="server" CssClass="textBox" TabIndex="101"
                                                            Width="90px" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">DEBIT / CREDIT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList Enabled="false" ID="txt_GO1_Credit_Code" runat="server" Width="70px" CssClass="dropdownList"
                                                            TabIndex="84" onchange="return TogggleDebitCreditCode('1','2');">
                                                            <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                            <asp:ListItem Value="D" Text="D"></asp:ListItem>
                                                            <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                                        </asp:DropDownList>
                                                       </td>
                                                    <td align="right">
                                                        <span class="elementLabel">CCY : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Credit_Curr" runat="server" CssClass="textBox" TabIndex="103"
                                                            Width="45px" MaxLength="3"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">AMOUNT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Credit_Amt" runat="server" CssClass="textBox" 
                                                            TabIndex="104" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">CUSTOMER : </span>
                                                    </td>
                                                    <td colspan="4">
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Credit_Cust" runat="server" CssClass="textBox" MaxLength="20"
                                                            TabIndex="105" Width="90px"></asp:TextBox>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Credit_Cust_Name" MaxLength="40" runat="server" CssClass="textBox"
                                                            TabIndex="105" Width="150px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">A/C CODE : </span>
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Credit_Cust_AcCode" runat="server" CssClass="textBox" TabIndex="106"
                                                            Width="90px"></asp:TextBox>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Credit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                            TabIndex="106" Width="150px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">A/C No : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Credit_Cust_AccNo" MaxLength="20" runat="server" CssClass="textBox"
                                                            TabIndex="107"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" align="right">
                                                        <span class="elementLabel">EXCH RATE : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Credit_Exch_Rate" runat="server" CssClass="textBox" 
                                                            TabIndex="108" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">EXCH CCY : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Credit_Exch_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                            TabIndex="109" Width="35px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">FUND : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Credit_FUND" MaxLength="1" runat="server" CssClass="textBox"
                                                            TabIndex="110" Width="30px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">CHECK No. : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Credit_Check_No" runat="server" CssClass="textBox" TabIndex="111"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">AVAILABLE : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Credit_Available" runat="server" MaxLength="20" CssClass="textBox"
                                                            TabIndex="112" Width="90px" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">ADVICE PRINT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Credit_AdPrint" runat="server" CssClass="textBox" TabIndex="113"
                                                            Width="30px" MaxLength="1"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">DETAILS : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Credit_Details" runat="server" MaxLength="30" CssClass="textBox"
                                                            Width="260px" TabIndex="114"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">ENTITY : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Credit_Entity" runat="server" CssClass="textBox" 
                                                            TabIndex="115" Width="90px" Style="text-align: right" MaxLength="3"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">DIVISION : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Credit_Division" runat="server" CssClass="textBox" TabIndex="116"
                                                            Width="30px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">INTER-AMOUNT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Credit_Inter_Amount" runat="server" CssClass="textBox" TabIndex="117"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">INTER-RATE : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO1_Credit_Inter_Rate" runat="server" CssClass="textBox" TabIndex="118"
                                                            Width="90px" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                            <tr>
                                                <td>
                                                    <td align="left">
                                                        <asp:Button ID="btnGO1_Prev" runat="server" CssClass="buttonDefault" OnClientClick="return OnDocNextClick(1);"
                                                            TabIndex="119" Text="&lt;&lt; Prev" ToolTip="Back to IMPORT ACCOUNTING" />
                                                            <asp:Button ID="Button5" runat="server" CssClass="buttonDefault" OnClientClick="return OnDocNextClick(3);"
                                                            TabIndex="119" Text="Next >>" ToolTip="IBD DOCUMENT DETAILS" />
                                                    </td>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel ID="tbDocumentGO2" runat="server" HeaderText="GENERAL OPERATION II"
                                    Font-Bold="true" ForeColor="White">
                                    <ContentTemplate>
                                        <table width="80%">
                                            <tr>
                                                <td align="left">
                                                    <asp:CheckBox Enabled="false" ID="chk_GO2_Flag" Text="GENERAL OPERATION II" runat="server" CssClass="elementLabel"
                                                        AutoPostBack="True" />
                                                </td>
                                            </tr>
                                            <asp:Panel ID="Panel_GO2" runat="server" Visible="False">
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">COMMENT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Comment" runat="server" CssClass="textBox" MaxLength="20"
                                                            TabIndex="80"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" width="20%">
                                                        <span class="elementLabel">Section No : </span>
                                                    </td>
                                                    <td width="20%">
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_SectionNo" runat="server" MaxLength="2" CssClass="textBox"
                                                            TabIndex="81" Width="30px"></asp:TextBox>
                                                    </td>
                                                    <td align="right" width="15%">
                                                        <span class="elementLabel">Remarks : </span>
                                                    </td>
                                                    <td width="20%">
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Remarks" runat="server" MaxLength="30" CssClass="textBox"
                                                            Width="260px" TabIndex="82"></asp:TextBox>
                                                    </td>
                                                    <td align="right" width="15%">
                                                        <span class="elementLabel">MEMO : </span>
                                                    </td>
                                                    <td width="10%">
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Memo" runat="server" MaxLength="20" CssClass="textBox" Width="50px"
                                                            TabIndex="83"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" width="15%">
                                                        <span class="elementLabel">SCHEME No : </span>
                                                    </td>
                                                    <td width="20%">
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Scheme_no" runat="server" MaxLength="20" CssClass="textBox"
                                                            TabIndex="84"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">DEBIT / CREDIT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList Enabled="false" ID="txt_GO2_Debit_Code" runat="server" Width="70px" CssClass="dropdownList"
                                                            TabIndex="84">
                                                            <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                            <asp:ListItem Value="D" Text="D"></asp:ListItem>
                                                            <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">CCY : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO2_Debit_Curr" Enabled="false" runat="server" MaxLength="3" CssClass="textBox"
                                                            TabIndex="86" Width="45px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">AMOUNT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Debit_Amt" runat="server" CssClass="textBox" Width="90px"
                                                            TabIndex="87" Style="text-align: right"
                                                            MaxLength="16"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">CUSTOMER : </span>
                                                    </td>
                                                    <td colspan="4">
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Debit_Cust" runat="server" CssClass="textBox" MaxLength="20"
                                                            TabIndex="88" Width="90px"></asp:TextBox>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Debit_Cust_Name" runat="server" CssClass="textBox" MaxLength="40"
                                                            TabIndex="88" Width="150px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">A/C CODE : </span>
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Debit_Cust_AcCode" MaxLength="20" runat="server" CssClass="textBox"
                                                            TabIndex="89" Width="90px"></asp:TextBox>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Debit_Cust_AcCode_Name" MaxLength="40" runat="server" CssClass="textBox"
                                                            TabIndex="89" Width="150px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">A/C No : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Debit_Cust_AccNo" runat="server" MaxLength="20" CssClass="textBox"
                                                            TabIndex="90"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" align="right">
                                                        <span class="elementLabel">EXCH RATE : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Debit_Exch_Rate" runat="server" CssClass="textBox"
                                                            TabIndex="91" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">EXCH CCY : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Debit_Exch_CCY" runat="server" CssClass="textBox" MaxLength="3"
                                                            TabIndex="92" Width="35px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">FUND : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Debit_FUND" runat="server" MaxLength="1" CssClass="textBox"
                                                            TabIndex="93" Width="30px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">CHECK No. : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Debit_Check_No" runat="server" CssClass="textBox" TabIndex="94"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">AVAILABLE : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Debit_Available" MaxLength="20" runat="server" CssClass="textBox"
                                                            TabIndex="95" Width="90px" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">ADVICE PRINT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Debit_AdPrint" runat="server" CssClass="textBox" TabIndex="96"
                                                            Width="30px" MaxLength="1"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">DETAILS : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Debit_Details" runat="server" MaxLength="30" CssClass="textBox"
                                                            Width="260px" TabIndex="97"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">ENTITY : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Debit_Entity" runat="server" CssClass="textBox"
                                                            TabIndex="98" Width="90px" Style="text-align: right" MaxLength="3"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">DIVISION : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Debit_Division" runat="server" CssClass="textBox" TabIndex="99"
                                                            Width="30px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">INTER-AMOUNT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Debit_Inter_Amount" runat="server" CssClass="textBox" TabIndex="100"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">INTER-RATE : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Debit_Inter_Rate" runat="server" CssClass="textBox" TabIndex="101"
                                                            Width="90px" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">DEBIT / CREDIT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList Enabled="false" ID="txt_GO2_Credit_Code" runat="server" Width="70px" CssClass="dropdownList"
                                                            TabIndex="84">
                                                            <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                            <asp:ListItem Value="D" Text="D"></asp:ListItem>
                                                            <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">CCY : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Credit_Curr" runat="server" CssClass="textBox" TabIndex="103"
                                                            Width="45px" MaxLength="3"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">AMOUNT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Credit_Amt" runat="server" CssClass="textBox"
                                                            TabIndex="104" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">CUSTOMER : </span>
                                                    </td>
                                                    <td colspan="4">
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Credit_Cust" runat="server" CssClass="textBox" MaxLength="20"
                                                            TabIndex="105" Width="90px"></asp:TextBox>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Credit_Cust_Name" MaxLength="40" runat="server" CssClass="textBox"
                                                            TabIndex="105" Width="150px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">A/C CODE : </span>
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Credit_Cust_AcCode" runat="server" CssClass="textBox" TabIndex="106"
                                                            Width="90px"></asp:TextBox>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Credit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                            TabIndex="106" Width="150px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">A/C No : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Credit_Cust_AccNo" MaxLength="20" runat="server" CssClass="textBox"
                                                            TabIndex="107"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" align="right">
                                                        <span class="elementLabel">EXCH RATE : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Credit_Exch_Rate" runat="server" CssClass="textBox"
                                                            TabIndex="108" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">EXCH CCY : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Credit_Exch_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                            TabIndex="109" Width="35px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">FUND : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Credit_FUND" MaxLength="1" runat="server" CssClass="textBox"
                                                            TabIndex="110" Width="30px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">CHECK No. : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Credit_Check_No" runat="server" CssClass="textBox" TabIndex="111"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">AVAILABLE : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Credit_Available" runat="server" MaxLength="20" CssClass="textBox"
                                                            TabIndex="112" Width="90px" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">ADVICE PRINT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Credit_AdPrint" runat="server" CssClass="textBox" TabIndex="113"
                                                            Width="30px" MaxLength="1"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">DETAILS : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Credit_Details" runat="server" MaxLength="30" CssClass="textBox"
                                                            Width="260px" TabIndex="114"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">ENTITY : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Credit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                            TabIndex="115" Width="90px" Style="text-align: right" MaxLength="3"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">DIVISION : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Credit_Division" runat="server" CssClass="textBox" TabIndex="116"
                                                            Width="30px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">INTER-AMOUNT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Credit_Inter_Amount" runat="server" CssClass="textBox" TabIndex="117"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">INTER-RATE : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Enabled="false" ID="txt_GO2_Credit_Inter_Rate" runat="server" CssClass="textBox" TabIndex="118"
                                                            Width="90px" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                            <tr>
                                                <td>
                                                    <td align="left">
                                                        <asp:Button ID="btnGO2_Prev" runat="server" CssClass="buttonDefault" OnClientClick="return OnDocNextClick(2);"
                                                            TabIndex="119" Text="&lt;&lt; Prev" ToolTip="Back GENERAL OPERATION I" />
                                                             <asp:Button ID="btnGO2_Next" runat="server" CssClass="buttonDefault" OnClientClick="return OnDocNextClick(4);"
                                                            TabIndex="119" Text="Next >>" ToolTip="Go to IBD DOCUMENT DETAILS" />
                                                    </td>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel ID="tbIBDDocumentDetails" runat="server" HeaderText="IBD DOCUMENT DETAILS"
                                    Font-Bold="true" ForeColor="White">
                                    <ContentTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">CUSTOMER NAME:</span>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:TextBox Enabled="false" ID="txt_IBD_CustName" runat="server"  CssClass="textBox"
                                                        TabIndex="1" MaxLength="40" Width="350px"></asp:TextBox>
                                                </td>
                                                <td align="left" colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">COMMENT CODE :</span>
                                                </td>
                                                <td align="left" width="10%">
                                                    <asp:TextBox Enabled="false" ID="txt_IBD_CommentCode" runat="server" CssClass="textBox" MaxLength="2"
                                                        TabIndex="4" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="right" width="10%">
                                                </td>
                                                <td align="left" width="10%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">MATURITY :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBD_MaturityDate" runat="server" CssClass="textBox"
                                                        MaxLength="10" TabIndex="5" ValidationGroup="dtVal"
                                                        Width="80px"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder="AM;PM"
                                                        CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY" CultureDatePlaceholder="/"
                                                        CultureDecimalPlaceholder="." CultureName="en-GB" CultureThousandsPlaceholder=","
                                                        CultureTimePlaceholder=":" Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txt_IBD_MaturityDate">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btnCal_txt_IBD_MaturityDate" runat="server" CssClass="btncalendar_enabled" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtendertxt_IBD_MaturityDate" runat="server"
                                                        Enabled="True" Format="dd/MM/yyyy" PopupButtonID="btnCal_txt_IBD_MaturityDate"
                                                        TargetControlID="txt_IBD_MaturityDate">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td align="right" width="10%">
                                                </td>
                                                <td align="left" width="10">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">SETTL FOR CUST :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBD_settlCodeForCust" runat="server" CssClass="textBox" TabIndex="7"
                                                        MaxLength="7" Width="50px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">SETTL FOR BANK :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBD_Settl_CodeForBank" runat="server" CssClass="textBox" MaxLength="12"
                                                        TabIndex="17" Width="50px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">INTEREST FROM :</span>
                                                    <td align="left">
                                                        <asp:TextBox Enabled="false" ID="txt_IBD_Interest_From" runat="server"  CssClass="textBox"
                                                            MaxLength="10"  TabIndex="8" ValidationGroup="dtVal"
                                                            Width="80px"></asp:TextBox>
                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" CultureAMPMPlaceholder="AM;PM"
                                                            CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY" CultureDatePlaceholder="/"
                                                            CultureDecimalPlaceholder="." CultureName="en-GB" CultureThousandsPlaceholder=","
                                                            CultureTimePlaceholder=":" Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999"
                                                            MaskType="Date" TargetControlID="txt_IBD_Interest_From">
                                                        </ajaxToolkit:MaskedEditExtender>
                                                        <asp:Button ID="btnCal_txt_IBD_Interest_From" runat="server" CssClass="btncalendar_enabled" />
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtendertxt_IBD_Interest_From" runat="server"
                                                            Enabled="True" Format="dd/MM/yyyy" PopupButtonID="btnCal_txt_IBD_Interest_From"
                                                            TargetControlID="txt_IBD_Interest_From">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                    <td align="right" width="10%">
                                                        <span class="elementLabel">DISCOUNT :</span>
                                                    </td>
                                                    <td align="left" width="10%">
                                                        <asp:TextBox Enabled="false" ID="txt_IBD_Discount" runat="server" CssClass="textBox" MaxLength="16"
                                                            Width="120px" TabIndex="6" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">INTEREST TO :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBD_Interest_To" runat="server"  CssClass="textBox"
                                                        MaxLength="10" TabIndex="9" ValidationGroup="dtVal"
                                                        Width="80px"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" CultureAMPMPlaceholder="AM;PM"
                                                        CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY" CultureDatePlaceholder="/"
                                                        CultureDecimalPlaceholder="." CultureName="en-GB" CultureThousandsPlaceholder=","
                                                        CultureTimePlaceholder=":" Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txt_IBD_Interest_To">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btnCal_txt_IBD_Interest_To" runat="server" CssClass="btncalendar_enabled" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtendertxt_IBD_Interest_To" runat="server"
                                                        Enabled="True" Format="dd/MM/yyyy" PopupButtonID="btnCal_txt_IBD_Interest_To"
                                                        TargetControlID="txt_IBD_Interest_To">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">NO. OF DAYS :</span>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:TextBox Enabled="false" ID="txt_IBD__No_Of_Days" runat="server" CssClass="textBox" MaxLength="3"
                                                        TabIndex="10" Width="50px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">RATE(%) :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBD__INT_Rate" runat="server" CssClass="textBox" MaxLength="10"
                                                        Style="text-align: right" TabIndex="11" Width="70px"></asp:TextBox>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">AMOUNT :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBD_InterestAmt"  runat="server" CssClass="textBox"
                                                        MaxLength="16" Style="text-align: right" TabIndex="14" Width="100px" ></asp:TextBox>
                                                </td>
                                               
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">OVERDUE </span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">INTEREST RATE(%) :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBD_OverinterestRate" runat="server" CssClass="textBox" MaxLength="10"
                                                        Style="text-align: right" TabIndex="28" Width="70px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">NO. OF DAYS :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBD_OverNoOfDays" runat="server" CssClass="textBox" MaxLength="3"
                                                        TabIndex="27" Width="50px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">AMOUNT :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBD_OverAmount"  runat="server" CssClass="textBox"
                                                        MaxLength="16" Style="text-align: right" TabIndex="14" Width="100px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                
                                                <td align="right">
                                                    <span class="elementLabel">ATTN :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBD_Attn" runat="server" CssClass="textBox" MaxLength="70" TabIndex="18"
                                                        Width="350px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                </td>
                                                <td colspan="6">
                                                <asp:Button ID="Button4" runat="server" CssClass="buttonDefault" OnClientClick="return OnDocNextClick(3);"
                                                        TabIndex="19" Text="<< PREV" ToolTip="Go to GENERAL OPERATION I" />
                                                        <asp:Button ID="Button1" runat="server" CssClass="buttonDefault" OnClientClick="return OnDocNextClick(5);"
                                                        TabIndex="19" Text="Next >>" ToolTip="Go to IMPORT ACCOUNTING IBD" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel ID="tbIBDDocumentAccounting" runat="server" HeaderText="IBD IMPORT ACCOUNTING"
                                    Font-Bold="true" ForeColor="White">
                                    <ContentTemplate>
                                        <table width="80%">
                                            <tr>
                                                <td>
                                                </td>
                                                <td align="left" colspan="2">
                                                    <span class="elementLabel">1-DISC </span>
                                                </td>
                                                <td align="right" colspan="2">
                                                    <span class="elementLabel">AMOUNT :</span><asp:TextBox Enabled="false" ID="txt_IBD_DiscAmt" runat="server"
                                                        CssClass="textBox" TabIndex="72" MaxLength="16" Width="100px" Style="text-align: right"></asp:TextBox>
                                                </td>
                                                <td align="left" colspan="2">
                                                    <span class="elementLabel">EXCH RATE :</span><asp:TextBox Enabled="false" ID="txt_IBD_IMP_ACC_ExchRate"
                                                        runat="server" CssClass="textBox" TabIndex="73" MaxLength="9" Width="70px" Style="text-align: right"
                                                        ></asp:TextBox>
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
                                                    <asp:TextBox Enabled="false" ID="txt_IBDPrinc_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                        TabIndex="74" Width="40px" ></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBDPrinc_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                        TabIndex="75" Width="40px" ></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBDprinc_Contract_no" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="76" Width="90px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBD_Princ_Ex_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                        TabIndex="77" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBDprinc_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="78"  Width="70px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBDprinc_Intnl_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="79"  Width="70px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">INTEREST :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBDInterest_matu" runat="server"  CssClass="textBox"
                                                        MaxLength="1" TabIndex="80" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBDInterest_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                         TabIndex="81" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBDInterest_Contract_no" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="82" Width="90px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBD_interest_Ex_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                        TabIndex="83" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBDInterest_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                         TabIndex="84" Width="70px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBDInterest_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                        MaxLength="9"  TabIndex="85" Width="70px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">COMMISSION :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBDCommission_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                        TabIndex="86" Width="40px" 
                                                        ></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBDCommission_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                        TabIndex="87" Width="40px" ></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBDCommission_Contract_no" runat="server" CssClass="textBox"
                                                        MaxLength="9" TabIndex="88" Width="90px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBD_Commission_Ex_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                        TabIndex="89" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBDCommission_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="89"  Width="70px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBDCommission_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                        MaxLength="9" TabIndex="90"  Width="70px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">THEIR COMMISSION :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBDTheir_Commission_matu" runat="server" CssClass="textBox"
                                                        MaxLength="1" TabIndex="91" Width="40px" ></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBDTheir_Commission_lump" runat="server" CssClass="textBox"
                                                        MaxLength="1" TabIndex="92" Width="40px" ></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBDTheir_Commission_Contract_no" runat="server" CssClass="textBox"
                                                        TabIndex="93" MaxLength="9" Width="90px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBD_Their_Commission_Ex_Curr" runat="server" CssClass="textBox"
                                                        TabIndex="94" MaxLength="3" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBDTheir_Commission_Ex_rate" runat="server" CssClass="textBox"
                                                        MaxLength="9" TabIndex="95"  Width="70px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox Enabled="false" ID="txt_IBDTheir_Commission_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                        TabIndex="96"  MaxLength="9" Width="70px"></asp:TextBox>
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
                                                                <asp:TextBox Enabled="false" ID="txt_IBD_CR_Code" runat="server" CssClass="textBox" MaxLength="5"
                                                                     TabIndex="97" Width="90px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox Enabled="false" ID="txt_IBD_CR_AC_ShortName" runat="server" CssClass="textBox" MaxLength="20"
                                                                    TabIndex="98" Width="160px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox Enabled="false" ID="txt_IBD_CR_Cust_abbr" runat="server" CssClass="textBox" MaxLength="12"
                                                                    TabIndex="99" Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox Enabled="false" ID="txt_IBD_CR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="20"
                                                                    TabIndex="100" Width="90px"></asp:TextBox>
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox Enabled="false" ID="txt_IBD_CR_Acceptance_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                    TabIndex="101" Width="40px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox Enabled="false" ID="txt_IBD_CR_Acceptance_amt" runat="server" CssClass="textBox" MaxLength="9"
                                                                     TabIndex="102" Width="90px" Style="text-align: right"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox Enabled="false" ID="txt_IBD_CR_Acceptance_payer" runat="server" CssClass="textBox" MaxLength="1"
                                                                    TabIndex="103" Width="50px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                            </td>
                                                            <td align="left" colspan="3">
                                                                <span class="elementLabel">INTEREST</span>
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox Enabled="false" ID="txt_IBD_CR_Interest_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                    TabIndex="104" Width="40px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox Enabled="false" ID="txt_IBD_CR_Interest_amt" runat="server" CssClass="textBox" MaxLength="9"
                                                                     TabIndex="105" Width="90px" Style="text-align: right"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox Enabled="false" ID="txt_IBD_CR_Interest_payer" runat="server" CssClass="textBox" MaxLength="1"
                                                                    TabIndex="106" Width="50px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <caption>
                                                            <tr>
                                                                <td align="right">
                                                                </td>
                                                                <td align="left" colspan="3">
                                                                    <span class="elementLabel">ACCEPTANCE COMMISSION</span>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:TextBox Enabled="false" ID="txt_IBD_CR_Accept_Commission_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="107" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_IBD_CR_Accept_Commission_amt" runat="server" CssClass="textBox"
                                                                        MaxLength="9"  
                                                                        Style="text-align: right" TabIndex="108" Width="90px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_IBD_CR_Accept_Commission_Payer" runat="server" CssClass="textBox"
                                                                        MaxLength="1" TabIndex="109" Width="50px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                </td>
                                                                <td align="left" colspan="3">
                                                                    <span class="elementLabel">PAYMENT/HANDLING COMMISSION</span>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:TextBox Enabled="false" ID="txt_IBD_CR_Pay_Handle_Commission_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="110" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_IBD_CR_Pay_Handle_Commission_amt" runat="server" CssClass="textBox"
                                                                        MaxLength="9"  Style="text-align: right"
                                                                        TabIndex="111" Width="90px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_IBD_CR_Pay_Handle_Commission_Payer" runat="server" CssClass="textBox"
                                                                        MaxLength="1" TabIndex="112" Width="50px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                </td>
                                                                <td align="left" colspan="3">
                                                                    <span class="elementLabel">OTHERS</span>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:TextBox Enabled="false" ID="txt_IBD_CR_Others_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                        TabIndex="113" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_IBD_CR_Others_amt" runat="server" CssClass="textBox" MaxLength="9"
                                                                         Style="text-align: right" TabIndex="114"
                                                                        Width="90px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_IBD_CR_Others_Payer" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="115" Width="50px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                </td>
                                                                <td align="left" colspan="3">
                                                                    <span class="elementLabel">THEIR COMMISSION</span>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:TextBox Enabled="false" ID="txt_IBD_CR_Their_Commission_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="116" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_IBD_CR_Their_Commission_amt" runat="server" CssClass="textBox"
                                                                        MaxLength="9"  Style="text-align: right"
                                                                        TabIndex="117" Width="90px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_IBD_CR_Their_Commission_Payer" runat="server" CssClass="textBox"
                                                                        MaxLength="1" TabIndex="118" Width="50px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel"><b>/DR/</b> CODE:</span>
                                                                </td>
                                                                <td align="left" colspan="3">
                                                                </td>
                                                                <td align="center">
                                                                </td>
                                                                <td align="left">
                                                                </td>
                                                                <td align="left">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:TextBox Enabled="false" ID="txt_IBD_IBD_DR_Code" runat="server" CssClass="textBox" MaxLength="5"
                                                                        TabIndex="119" Width="90px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <%--<asp:TextBox Enabled="false" ID="txt_IBD_IBD_DR_AC_ShortName" runat="server" CssClass="textBox" 
                                                                        MaxLength="20" TabIndex="119" Width="90px"></asp:TextBox>--%>
                                                                    <span class="elementLabel">CURRENT ACCOUNT</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_IBD_IBD_DR_Cust_abbr" runat="server" CssClass="textBox" MaxLength="12"
                                                                        TabIndex="120" Width="100px" ></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_IBD_IBD_DR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="20"
                                                                        TabIndex="121" Width="90px"></asp:TextBox>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:TextBox Enabled="false" ID="txt_IBD_IBD_DR_Cur_Acc_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                        TabIndex="122" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_IBD_IBD_DR_Cur_Acc_amt" runat="server" CssClass="textBox" MaxLength="9"
                                                                         Style="text-align: right" TabIndex="123"
                                                                        Width="90px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_IBD_IBD_DR_Cur_Acc_payer" runat="server" CssClass="textBox"
                                                                        TabIndex="124" Width="50px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <asp:Panel ID="panal_IBD_DRdetails" runat="server" Visible="false">
                                                                    <td align="right">
                                                                        <asp:TextBox Enabled="false" ID="txt_IBD_DR_Code" runat="server" CssClass="textBox" MaxLength="5" TabIndex="125"
                                                                            Width="90px"></asp:TextBox>
                                                                    </td>
                                                                    <td align="left">
                                                                        <span class="elementLabel">CURRENT ACCOUNT</span>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox Enabled="false" ID="txt_IBD_DR_Cust_abbr" runat="server" CssClass="textBox" 
                                                                            MaxLength="12" TabIndex="126" Width="100px"></asp:TextBox>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox Enabled="false" ID="txt_IBD_DR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="20"
                                                                            TabIndex="127" Width="90px"></asp:TextBox>
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:TextBox Enabled="false" ID="txt_IBD_DR_Cur_Acc_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                            TabIndex="128" Width="40px"></asp:TextBox>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox Enabled="false" ID="txt_IBD_DR_Cur_Acc_amt" runat="server" CssClass="textBox" MaxLength="9"
                                                                             Style="text-align: right" TabIndex="129"
                                                                            Width="90px"></asp:TextBox>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox Enabled="false" ID="txt_IBD_DR_Cur_Acc_payer" runat="server" CssClass="textBox" MaxLength="1"
                                                                            TabIndex="130" Width="50px"></asp:TextBox>
                                                                    </td>
                                                                </asp:Panel>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                </td>
                                                                <td align="left" colspan="3">
                                                                </td>
                                                                <td align="center">
                                                                    <asp:TextBox Enabled="false" ID="txt_IBD_DR_Cur_Acc_Curr2" runat="server" CssClass="textBox" MaxLength="3"
                                                                        TabIndex="131" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_IBD_DR_Cur_Acc_amt2" runat="server" CssClass="textBox" MaxLength="9"
                                                                         Style="text-align: right" TabIndex="132"
                                                                        Width="90px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_IBD_DR_Cur_Acc_payer2" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="133" Width="50px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                </td>
                                                                <td colspan="3">
                                                                </td>
                                                                <td align="center">
                                                                    <asp:TextBox Enabled="false" ID="txt_IBD_DR_Cur_Acc_Curr3" runat="server" CssClass="textBox" MaxLength="3"
                                                                        TabIndex="134" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_IBD_DR_Cur_Acc_amt3" runat="server" CssClass="textBox" MaxLength="9"
                                                                         Style="text-align: right" TabIndex="135"
                                                                        Width="90px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox Enabled="false" ID="txt_IBD_DR_Cur_Acc_payer3" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="136" Width="50px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </caption>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" >
                                                    <asp:Button ID="Button2" runat="server" CssClass="buttonDefault" OnClientClick="return OnDocNextClick(4);"
                                                        TabIndex="159" Text="&lt;&lt; Prev" ToolTip="Back to Document Details" />
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:CheckBox Enabled="false" ID="chk_IBDExtn_Flag" Text="IBD Extn Flag" runat="server" CssClass="elementLabel"/>
                                                </td>
                                                <td align="left" colspan="6">
                                                  
                                                        <span class="elementLabel">Approve / Reject :</span>
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
                        <td align="center" colspan="5">
                        <asp:Button ID="btnSave" Style="visibility: hidden" runat="server" Text="Save" CssClass="buttonDefault"
                                                   ToolTip="Save" OnClick="btnSave_Click" TabIndex="107" />
                            <asp:Label ID="lblChecker_Remark" runat="server" CssClass="mandatoryField" Font-Bold="true"></asp:Label>

                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
