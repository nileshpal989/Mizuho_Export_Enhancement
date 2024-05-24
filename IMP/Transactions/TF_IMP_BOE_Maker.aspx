<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_BOE_Maker.aspx.cs"
    Inherits="IMP_Transactions_TF_IMP_BOE_Maker" EnableEventValidation="false" %>

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
    <link href="../../Style/TAB.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <link href="../../Style/Style_V2.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../../Scripts/jquery-1.8.3.min.js" type="text/javascript""></script>
    <script src="../../Help_Plugins/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <link href="../../Help_Plugins/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../../Help_Plugins/MyJquery1.js" type="text/javascript"></script>
    <script src="../Scripts/TF_IMP_BOE_Maker.js" type="text/javascript"></script>
    <script id="Save_script" language="javascript" type="text/javascript">
        $(document).ready(function (e) {
            // Configure to save every 5 seconds
            window.setInterval(SaveUpdateData, 5000); //calling saveDraft function for every 5 seconds
            window.setInterval(SaveUpdateLCData, 5000);
            OnInputKeyPress();
        });
    </script>
    <style type="text/css">
        .textBox
        {
            text-transform: uppercase;
        }
        
        .tdpayment1
        {
            width: 20%;
        }
        
        .tdpayment1
        {
            width: 80%;
        }
        
        .riskcustmercss
        {
            visibility: hidden;
        }
    </style>
</head>
<body onload="EndRequest();closeWindows();" onunload="closeWindows();">
    <form id="form1" runat="server" autocomplete="off" unselectable="off">
    <div style="width: 100%">
        <div id="dialog" class="AlertJqueryHide">
            <p id="Paragraph">
            </p>
        </div>
    </div>
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
            </br>
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <Triggers>
                    <%-- <asp:PostBackTrigger ControlID="btnBack" />--%>
                    <asp:PostBackTrigger ControlID="TabContainerMain$tbAmlDetails$lnkDownload" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel"><strong>Imports Bill Lodgment - Maker</strong></span>
                            </td>
                            <td align="right" style="width: 50%" valign="bottom">
                            <%--<asp:Label ID="label1" runat="server" CssClass="mandatoryField"></asp:Label>--%>
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                    OnClientClick="return OnBackClick();" />
                            </td>
                        </tr>
                        <tr>
                        <td align="right" style="width: 100%" valign="bottom" colspan="2">
                            <asp:Label ID="label1" runat="server" CssClass="mandatoryField"></asp:Label>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 100%" valign="bottom" colspan="2">
                        <asp:Label ID="ReccuringLEI" runat="server" CssClass="mandatoryField" Visible="false"></asp:Label>
                            <hr />
                        </td>
                    </tr>
                        <tr>
                            <td align="left" style="width: 100%;" valign="top" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                                <%-------------------------  hidden fields  --------------------------------%>
                                <input type="hidden" id="hdnBranchCode" runat="server" />
                                <input type="hidden" id="hdnBranchName" runat="server" />
                                <input type="hidden" id="hdnDocType" runat="server" />
                                <input type="hidden" id="hdnCustAbbr" runat="server" />
                                <input type="hidden" id="hdnUserName" runat="server" />
                                <input type="hidden" id="hdnStamp_Duty_Per_Thousand" runat="server" />
                                <input type="hidden" id="hdnMT999LC" runat="server" />
                                <input type="hidden" id="hdnNegoRemiSwiftCode" runat="server" />
                                <input type="hidden" id="hdnDocumentScrutiny" runat="server" />
                                <input type="hidden" id="hdnLCREF1" runat="server" />
                                <input type="hidden" id="hdnLCREF2" runat="server" />
                                <input type="hidden" id="hdnLCREF3" runat="server" />
                                <input type="hidden" id="hdnLCCurrency" runat="server" />
                                <input type="hidden" id="hdnLCAppNo" runat="server" />
                                <input type="hidden" id="hdnLCCountry" runat="server" />
                                <input type="hidden" id="hdnLCCommCode" runat="server" />
                                <input type="hidden" id="hdnExpiryDate" runat="server" />
                                <input type="hidden" id="hdnLCRiskCust" runat="server" />
                                <input type="hidden" id="hdnLCSettlementAccNo" runat="server" />
                                <input type="hidden" id="hdnLCSpCustAbbr" runat="server" />
                                <input type="hidden" id="hdnBRONo" runat="server" />
                                <input type="hidden" id="hdnSGDocNo" runat="server" />
                                <%-------Added by Bhupen for LEI on 10102022----------------%>
                                <input type="hidden" id="hdnleino" runat="server" />
                                <input type="hidden" id="hdnleiexpiry" runat="server" />
                                <input type="hidden" id="hdnDraweeleino" runat="server" />
                                <input type="hidden" id="hdnDraweeleiexpiry" runat="server" />
                                <input type="hidden" id="hdnLeiFlag" runat="server" />
                                <input type="hidden" id="hdnleiexpiry1" runat="server" />
                                <input type="hidden" id="hdnDraweeleiexpiry1" runat="server" />
                                <input type="hidden" id="Hidden1" runat="server" />
                                <input type="hidden" id="hdncustacno" runat="server" />
                                <input type="hidden" id="hdncustdesc" runat="server" />
                                <input type="hidden" id="hdncustleiflag" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="right" width="10%">
                                <span class="elementLabel">Trans. Ref. No :</span>
                            </td>
                            <td width="25%" align="left">
                                <asp:TextBox ID="txtDocNo" Width="100px" runat="server" CssClass="textBox" Enabled="false"
                                    TabIndex="1"></asp:TextBox>
                                &nbsp;
                                <asp:Label ID="lblForeign_Local" runat="server" CssClass="elementLabel"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblCollection_Lodgment_UnderLC" runat="server" CssClass="elementLabel"></asp:Label>&nbsp;&nbsp;
                                <asp:Label ID="lblSight_Usance" runat="server" CssClass="elementLabel"></asp:Label>
                            </td>
                            <td align="left" width="30%">
                                <span class="elementLabel">Received Date :</span>
                                <asp:TextBox ID="txtDateReceived" runat="server" CssClass="textBox" MaxLength="10"
                                    ValidationGroup="dtVal" Width="70px" TabIndex="2" onchange="return OnReceivedChange();"
                                    OnTextChanged="txtDateReceived_TextChanged"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="ME_Received_date" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtDateReceived" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="btncal_Received_date" runat="server" CssClass="btncalendar_enabled"
                                    TabIndex="2" />
                                <ajaxToolkit:CalendarExtender ID="CE_Received_date" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtDateReceived" PopupButtonID="btncal_Received_date" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MV_Received_date" runat="server" ControlExtender="ME_Received_date"
                                    ValidationGroup="dtVal" ControlToValidate="txtDateReceived" EmptyValueMessage="Enter Date Value"
                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                    Enabled="false">
                                </ajaxToolkit:MaskedEditValidator>
                                &nbsp; <span class="elementLabel">&nbsp; Lodgment Date :</span>
                                <asp:TextBox ID="txtLogdmentDate" runat="server" CssClass="textBox" MaxLength="10"
                                    Enabled="false" Width="70px" TabIndex="3"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="ME_Logdment_Date" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtLogdmentDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/">
                                </ajaxToolkit:MaskedEditExtender>
                            </td>
                            <td align="left" width="35%">
                                <asp:CheckBox ID="chkLodgCumAcc" runat="server" Text="Same Day Lodg. And Accept."
                                    CssClass="elementLabel" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="4">
                                <ajaxToolkit:TabContainer ID="TabContainerMain" runat="server" ActiveTabIndex="0"
                                    CssClass="ajax__tab_xp-theme">
                                    <ajaxToolkit:TabPanel ID="tbDocumentDetails" runat="server" HeaderText="DOCUMENT DETAILS"
                                        Font-Bold="true" ForeColor="White">
                                        <ContentTemplate>
                                            <table cellpadding="0" cellspacing="2" border="0" width="100%">
                                                <tr>
                                                    <td align="right" width="10%">
                                                        <span class="elementLabel">Customer :</span>
                                                    </td>
                                                    <td align="left" width="30%">
                                                        <asp:TextBox ID="txtCustomer_ID" runat="server" CssClass="textBox" MaxLength="20"
                                                            onchange="return FillCustomerDetails();" OnTextChanged="txtCustomer_ID_TextChanged"
                                                            TabIndex="4" Width="120px" ReadOnly="true"></asp:TextBox>
                                                            <input type="hidden" id="hdncustnamelei" runat="server" />
                                                        <asp:Button ID="btnCustomerList" runat="server" ToolTip="Press for Customers list."
                                                            CssClass="btnHelp_enabled" />
                                                        <asp:Label ID="lblCustomerDesc" runat="server" CssClass="elementLabel"></asp:Label>
                                                    </td>
                                                    <asp:Panel ID="panal_LC_No" runat="server">
                                                        <td align="right" width="10%">
                                                            <span class="elementLabel">LC No :</span>
                                                        </td>
                                                        <td align="left" width="70%" colspan="3">
                                                            <asp:TextBox ID="txt_LC_No" runat="server" CssClass="textBox" MaxLength="30" TabIndex="5"
                                                                Width="100px" ReadOnly="true" onchange="return FillLCDetails();" OnTextChanged="txt_LC_No_TextChanged"></asp:TextBox>
                                                            <input type="hidden" id="hdnCustLCNo" runat="server" />
                                                            <asp:Button ID="btnLC_Help" runat="server" ToolTip="Press for Customers list." CssClass="btnHelp_enabled" />
                                                            <asp:Label ID="lblLCDesc" runat="server" CssClass="elementLabel"></asp:Label>
                                                            <asp:Label ID="lblBRONo" runat="server" CssClass="mandatoryField"></asp:Label>
                                                            <asp:Label ID="lblBRODate" runat="server" CssClass="mandatoryField"></asp:Label>
                                                            <asp:Label ID="lblBROAmount" runat="server" CssClass="mandatoryField"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="lblSGDocNo" runat="server" CssClass="mandatoryField"></asp:Label>
                                                            <asp:Label ID="lblSGValDate" runat="server" CssClass="mandatoryField"></asp:Label>
                                                            <asp:Label ID="lblSGAmount" runat="server" CssClass="mandatoryField"></asp:Label>
                                                        </td>
                                                    </asp:Panel>
                                                </tr>
                                                <tr runat="server" id="rowRiskCust">
                                                    <td align="right" width="10%">
                                                        <span class="elementLabel">Special Case :</span>
                                                    </td>
                                                    <td align="left" width="30%">
                                                        <asp:CheckBox ID="chkSpecialCase" runat="server" onchange="return OnSpecialCasesChange();"
                                                            CssClass="checkboxes" />
                                                        <asp:Label ID="lblRiskCust" Text="Risk Customer :" runat="server" CssClass="elementLabel riskcustmercss"></asp:Label>
                                                        <asp:TextBox ID="txtRiskCust" runat="server" CssClass="textBox riskcustmercss" MaxLength="20"
                                                            TabIndex="4" Width="120px"></asp:TextBox>
                                                        <asp:Label ID="lblSettelementAcNo" Text="Settelement A/C No :" runat="server" CssClass="elementLabel riskcustmercss"></asp:Label>
                                                        <asp:TextBox ID="txtSettelementAcNo" runat="server" CssClass="textBox riskcustmercss"
                                                            MaxLength="20" TabIndex="4" Width="120px"></asp:TextBox>
                                                    </td>
                                                    <td align="right" width="10%">
                                                        <asp:Label ID="lblRiskCustAbbr" Text="Customer ABBR 2 :" runat="server" CssClass="elementLabel riskcustmercss"></asp:Label>
                                                    </td>
                                                    <td align="left" width="10%" colspan="3">
                                                        <asp:TextBox ID="txtRiskCustAbbr" runat="server" CssClass="textBox riskcustmercss"
                                                            MaxLength="20" TabIndex="4" Width="120px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" width="10%">
                                                        <span class="elementLabel">Currency :</span>
                                                    </td>
                                                    <td align="left" width="30%">
                                                        <asp:DropDownList ID="ddl_Doc_Currency" runat="server" CssClass="dropdownList" TabIndex="6"
                                                            onchange="return OnCurrencyChange();" Width="70px" OnSelectedIndexChanged="ddl_Doc_Currency_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="lbl_Doc_Currency" runat="server" CssClass="elementLabel" Width="174px"></asp:Label>
                                                        &nbsp; <span class="elementLabel">Lodg. Amt :</span>
                                                        <asp:TextBox ID="txtBillAmount" runat="server" CssClass="textBox" MaxLength="16"
                                                            TabIndex="7" Width="120px" Style="text-align: right" AutoPostBack="true"
                                                            OnTextChanged="txtBillAmount_TextChanged"></asp:TextBox>
                                                        <asp:Label ID="lbl_Exch_rate" runat="server" CssClass="elementLabel"></asp:Label>
                                                    </td>
                                                    <td align="right" width="10%">
                                                        <asp:Label ID="lbl_AcceptanceDate" runat="server" CssClass="elementLabel"></asp:Label>
                                                    </td>
                                                    <td align="left" width="10%">
                                                        <asp:TextBox ID="txt_AcceptanceDate" runat="server" CssClass="textBox" MaxLength="10"
                                                            ValidationGroup="dtVal" Width="70px" TabIndex="8" onchange="return Toggel_Instruction_Date();"></asp:TextBox>
                                                        <ajaxToolkit:MaskedEditExtender ID="ME_Accept_Date" Mask="99/99/9999" MaskType="Date"
                                                            runat="server" TargetControlID="txt_AcceptanceDate" ErrorTooltipEnabled="True"
                                                            CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                            CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                            CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                        </ajaxToolkit:MaskedEditExtender>
                                                        <asp:Button ID="btncal_Accept_Date" runat="server" CssClass="btncalendar_enabled" />
                                                        <ajaxToolkit:CalendarExtender ID="CE_Accept_Date" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txt_AcceptanceDate" PopupButtonID="btncal_Accept_Date" Enabled="True">
                                                        </ajaxToolkit:CalendarExtender>
                                                        <ajaxToolkit:MaskedEditValidator ID="MV_Accept_Date" runat="server" ControlExtender="ME_Accept_Date"
                                                            ValidationGroup="dtVal" ControlToValidate="txt_AcceptanceDate" EmptyValueMessage="Enter Date Value"
                                                            InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                            Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                                    </td>
                                                    <asp:Panel ID="panelDP_DA" runat="server" Visible="false">
                                                        <td align="right" width="10%">
                                                            &nbsp;&nbsp;&nbsp; <span class="elementLabel">Type :</span>
                                                        </td>
                                                        <td align="left" width="30%">
                                                            <asp:RadioButton ID="rbtDP" Text="D/P" CssClass="elementLabel" runat="server" GroupName="TypeG"
                                                                TabIndex="9" Enabled="False" />
                                                            <asp:RadioButton ID="rbtDA" Text="D/A" CssClass="elementLabel" runat="server" GroupName="TypeG"
                                                                TabIndex="9" Enabled="False" />
                                                        </td>
                                                    </asp:Panel>
                                                </tr>
                                                <tr>
                                                    <td align="right" width="10%">
                                                        <span class="elementLabel">Tenor :</span>
                                                    </td>
                                                    <td width="35%" align="left">
                                                        <asp:DropDownList ID="ddlTenor" runat="server" Width='70px' CssClass="dropdownList"
                                                            onchange="return OnTenorChange();" TabIndex="10" OnSelectedIndexChanged="ddlTenor_SelectedIndexChanged">
                                                            <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="SIGHT"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="DEF Pmt"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="MIX pmt"></asp:ListItem>
                                                            <asp:ListItem Value="4" Text="Others"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtTenor" Width="30px" runat="server" CssClass="textBox" TabIndex="11"
                                                            MaxLength="3" onchange="return OnTenorTextChange();" OnTextChanged="txtTenor_TextChanged"></asp:TextBox>
                                                        <span class="elementLabel">Days From</span>
                                                        <asp:DropDownList ID="ddlTenor_Days_From" runat="server" Width="70px" CssClass="dropdownList"
                                                            TabIndex="12" onchange="return OnDaysFromChange();" OnSelectedIndexChanged="ddlTenor_Days_From_SelectedIndexChanged">
                                                            <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                            <asp:ListItem Value="SHIPMENT DATE" Text="SHIPMENT DATE"></asp:ListItem>
                                                            <asp:ListItem Value="INVOICE DATE" Text="INVOICE DATE"></asp:ListItem>
                                                            <asp:ListItem Value="BOEXCHANGE DATE" Text="BOEXCHANGE DATE"></asp:ListItem>
                                                            <asp:ListItem Value="OTHERS/BLANK" Text="OTHERS/BLANK"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtTenor_Description" runat="server" CssClass="textBox" MaxLength="31"
                                                            TabIndex="13" Width="190px" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td align="right" width="5%">
                                                        <span class="elementLabel">BOExchange Date :</span>
                                                    </td>
                                                    <td align="left" width="10%">
                                                        <asp:TextBox ID="txtBOExchange" runat="server" CssClass="textBox" MaxLength="10"
                                                            ValidationGroup="dtVal" Width="70px" TabIndex="14" onchange="return OnBoeExchangeDateChange();"
                                                            OnTextChanged="txtBOExchange_TextChanged"></asp:TextBox>
                                                        <ajaxToolkit:MaskedEditExtender ID="ME_BOExDate" Mask="99/99/9999" MaskType="Date"
                                                            runat="server" TargetControlID="txtBOExchange" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                            CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                            CultureTimePlaceholder=":">
                                                        </ajaxToolkit:MaskedEditExtender>
                                                        <asp:Button ID="btnCal_BOExDate" runat="server" CssClass="btncalendar_enabled" />
                                                        <ajaxToolkit:CalendarExtender ID="CE_BOExDate" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtBOExchange" PopupButtonID="btnCal_BOExDate" Enabled="True">
                                                        </ajaxToolkit:CalendarExtender>
                                                        <ajaxToolkit:MaskedEditValidator ID="MV_BOExDate" runat="server" ControlExtender="ME_DueDate"
                                                            ValidationGroup="dtVal" ControlToValidate="txtBOExchange" EmptyValueMessage="Enter Date Value"
                                                            InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                            Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                                    </td>
                                                    <td align="right" width="10%">
                                                        <span class="elementLabel">Due Date :</span>
                                                    </td>
                                                    <td align="left" width="30%">
                                                        <asp:TextBox ID="txtDueDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                                            Width="70px" TabIndex="15" onchange="return OnDueDateChange();"></asp:TextBox>
                                                        <ajaxToolkit:MaskedEditExtender ID="ME_DueDate" Mask="99/99/9999" MaskType="Date"
                                                            runat="server" TargetControlID="txtDueDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                            CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                            CultureTimePlaceholder=":">
                                                        </ajaxToolkit:MaskedEditExtender>
                                                        <asp:Button ID="btnCal_DueDate" runat="server" CssClass="btncalendar_enabled" />
                                                        <ajaxToolkit:CalendarExtender ID="CE_DueDate" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtDueDate" PopupButtonID="btnCal_DueDate" Enabled="True">
                                                        </ajaxToolkit:CalendarExtender>
                                                        <ajaxToolkit:MaskedEditValidator ID="MV_DueDate" runat="server" ControlExtender="ME_DueDate"
                                                            ValidationGroup="dtVal" ControlToValidate="txtDueDate" EmptyValueMessage="Enter Date Value"
                                                            InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                            Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" width="10%">
                                                        <span class="elementLabel">Nego / Remit Bank :</span>
                                                    </td>
                                                    <td align="left" width="30%">
                                                        <asp:DropDownList ID="ddl_Nego_Remit_Bank" runat="server" Width='70px' CssClass="dropdownList"
                                                            TabIndex="16" onchange="return OnNegoRemiTypeChange();" OnSelectedIndexChanged="ddl_Nego_Remit_Bank_SelectedIndexChanged">
                                                            <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                            <asp:ListItem Value="FOREIGN" Text="FOREIGN"></asp:ListItem>
                                                            <asp:ListItem Value="LOCAL" Text="LOCAL"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:Label ID="lbl_Nego_Remit_Local_Foreign" Text="Bank Code :" runat="server" CssClass="elementLabel"></asp:Label>
                                                        <asp:TextBox ID="txtNego_Remit_Bank" runat="server" CssClass="textBox" MaxLength="12"
                                                            TabIndex="17" Width="100px" ReadOnly="true" onchange="return FillNegoRemiBank();"
                                                            OnTextChanged="txtNego_Remit_Bank_TextChanged"></asp:TextBox>
                                                        <asp:Button ID="btn_Nego_Remit_Bank" runat="server" CssClass="btnHelp_enabled" />
                                                        <asp:Label ID="lbl_Nego_RemitSwift_IFSC" runat="server" CssClass="elementLabel" Font-Underline="true"></asp:Label>
                                                        <asp:Label ID="lbl_Nego_Remit_Bank" runat="server" CssClass="elementLabel" Font-Underline="true"></asp:Label>
                                                        <asp:Label ID="lbl_Nego_Remit_Bank_Addr" runat="server" CssClass="elementLabel" Font-Underline="true"></asp:Label>
                                                        <input type="hidden" id="hdnnego_remittype" runat="server" />
                                                    </td>
                                                    <td align="right" width="10%">
                                                        <span class="elementLabel">Their Ref.No :</span>
                                                    </td>
                                                    <td align="left" width="10%">
                                                        <asp:TextBox ID="txt_Their_Ref_no" runat="server" CssClass="textBox" MaxLength="30"
                                                            TabIndex="18" Width="160px" onchange="return OnTheirRefNoChange();"></asp:TextBox>
                                                    </td>
                                                    <td align="right" width="10%">
                                                        <span class="elementLabel">Nego Date :</span>
                                                    </td>
                                                    <td align="left" width="30%">
                                                        <asp:TextBox ID="txtNego_Date" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                                            TabIndex="19" Width="70px" onchange="return OnNegoDateChange();"></asp:TextBox>
                                                        <ajaxToolkit:MaskedEditExtender ID="ME_Nego_Date" Mask="99/99/9999" MaskType="Date"
                                                            runat="server" TargetControlID="txtNego_Date" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                            CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                            CultureTimePlaceholder=":">
                                                        </ajaxToolkit:MaskedEditExtender>
                                                        <asp:Button ID="btnCal_Nego_Date" runat="server" CssClass="btncalendar_enabled" />
                                                        <ajaxToolkit:CalendarExtender ID="CE_Nego_Date" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtNego_Date" PopupButtonID="btnCal_Nego_Date" Enabled="True">
                                                        </ajaxToolkit:CalendarExtender>
                                                        <ajaxToolkit:MaskedEditValidator ID="MV_Nego_Date" runat="server" ControlExtender="ME_Nego_Date"
                                                            ValidationGroup="dtVal" ControlToValidate="txtNego_Date" EmptyValueMessage="Enter Date Value"
                                                            InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                            Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" width="10%">
                                                        <span class="elementLabel">A/C with Institution :</span>
                                                    </td>
                                                    <td align="left" width="30%">
                                                        <asp:TextBox ID="txtAcwithInstitution" runat="server" CssClass="textBox" MaxLength="12"
                                                            TabIndex="20" ReadOnly="true" Width="100px" onchange="return FillAccountWithInstitution();"
                                                            OnTextChanged="txtAcwithInstitution_TextChanged"></asp:TextBox>
                                                        <asp:Button ID="btnAcwithInstitution" runat="server" ToolTip="Press for list." CssClass="btnHelp_enabled" />
                                                        <asp:Label ID="lblAcWithInstiBankDesc" runat="server" CssClass="elementLabel"></asp:Label>
                                                    </td>
                                                    <td align="right" width="60%" colspan="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" width="10%">
                                                        <span class="elementLabel">Reimbursing Bank :</span>
                                                    </td>
                                                    <td width="30%" align="left">
                                                        <asp:TextBox ID="txtReimbursingbank" Width="100px" runat="server" CssClass="textBox"
                                                            TabIndex="21" ReadOnly="true" MaxLength="12" onchange="return FillReimbursingBank();"
                                                            OnTextChanged="txtReimbursingbank_TextChanged"></asp:TextBox>
                                                        <asp:Button ID="btn_Reimbursingbank" runat="server" CssClass="btnHelp_enabled" />
                                                        <asp:Label ID="lbl_Reimbursingbank" runat="server" CssClass="elementLabel"></asp:Label>
                                                    </td>
                                                    <td align="right" width="10%">
                                                        <span class="elementLabel">Invoice No :</span>
                                                    </td>
                                                    <td align="left" width="50%" colspan="3">
                                                        <asp:TextBox ID="txt_Inv_No" runat="server" CssClass="textBox" MaxLength="100" TabIndex="22"
                                                            Width="300px" onchange="return OnInvoiceNoChange();"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" width="10%">
                                                        <span class="elementLabel">Drawer :</span>
                                                    </td>
                                                    <td align="left" width="30%">
                                                        <asp:DropDownList ID="ddlDrawer" runat="server" Width='500px' CssClass="dropdownList"
                                                            TabIndex="23" onchange="return OnDrawerChange();">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="right" width="10%">
                                                        <span class="elementLabel">Invoice Date :</span>
                                                    </td>
                                                    <td align="left" width="50%" colspan="3">
                                                        <asp:TextBox ID="txt_Inv_Date" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                                            Width="70px" TabIndex="24" onchange="return OnInvoiceDateChange();" OnTextChanged="txt_Inv_Date_TextChanged"></asp:TextBox>
                                                        <ajaxToolkit:MaskedEditExtender ID="ME_Inv_Date" Mask="99/99/9999" MaskType="Date"
                                                            runat="server" TargetControlID="txt_Inv_date" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                            CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                            CultureTimePlaceholder=":">
                                                        </ajaxToolkit:MaskedEditExtender>
                                                        <asp:Button ID="btnCal_Inv_Date" runat="server" CssClass="btncalendar_enabled" />
                                                        <ajaxToolkit:CalendarExtender ID="CE_Inv_Date" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="Txt_Inv_date" PopupButtonID="btnCal_Inv_Date" Enabled="True">
                                                        </ajaxToolkit:CalendarExtender>
                                                        <ajaxToolkit:MaskedEditValidator ID="MV_Inv_Date" runat="server" ControlExtender="ME_Inv_Date"
                                                            ValidationGroup="dtVal" ControlToValidate="txt_Inv_date" EmptyValueMessage="Enter Date Value"
                                                            InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                            Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" width="10%">
                                                        <span class="elementLabel">Commodity Code :</span>
                                                    </td>
                                                    <td align="left" width="30%">
                                                        <asp:DropDownList ID="ddl_Commodity" runat="server" CssClass="dropdownList" MaxLength="3"
                                                            TabIndex="25" Width="70px" onchange="return OnCommodityCodeChange();" OnSelectedIndexChanged="ddl_Commodity_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="lblCommodityDesc" runat="server" CssClass="elementLabel"></asp:Label>&nbsp;
                                                        <asp:TextBox ID="txtCommodityDesc" runat="server" CssClass="textBox" MaxLength="100"
                                                            TabIndex="26" Width="250px" onchange="return OnCommodityDescChange();"></asp:TextBox>
                                                    </td>
                                                    <td align="right" width="10%">
                                                        <span class="elementLabel">Country of Origin :</span>
                                                    </td>
                                                    <td align="left" width="50%" colspan="3">
                                                        <asp:DropDownList ID="ddlCountryOfOrigin" runat="server" CssClass="dropdownList"
                                                            MaxLength="2" TabIndex="28" Width="70px" onchange="return OnCountryOfOriginChange();"
                                                            OnSelectedIndexChanged="ddlCountryOfOrigin_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="lblCountryOfOriginDesc" runat="server" CssClass="elementLabel"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" width="10%">
                                                        <span class="elementLabel">Country Code :</span>
                                                    </td>
                                                    <td align="left" width="30%">
                                                        <asp:DropDownList ID="ddlCountryCode" runat="server" CssClass="dropdownList" MaxLength="2"
                                                            TabIndex="28" Width="70px" onchange="return OnCountryCodeChange();" OnSelectedIndexChanged="ddlCountryCode_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="lblCountryDesc" runat="server" CssClass="elementLabel"></asp:Label>
                                                    </td>
                                                    <td align="right" width="60%" colspan="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="100%" colspan="6">
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
                                                                <td align="right" width="14.3%">
                                                                    <span class="elementLabel">Shipment Date :</span>
                                                                </td>
                                                                <td align="left" width="45.7%">
                                                                    <asp:TextBox ID="txtShippingDate" runat="server" CssClass="textBox" MaxLength="10"
                                                                        ValidationGroup="dtVal" Width="70px" TabIndex="29" onchange="return OnShippmentDateChange();"
                                                                        OnTextChanged="txtShippingDate_TextChanged"></asp:TextBox>
                                                                    <ajaxToolkit:MaskedEditExtender ID="ME_Ship_Date" Mask="99/99/9999" MaskType="Date"
                                                                        runat="server" TargetControlID="txtShippingDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                        CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                        CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                        CultureTimePlaceholder=":">
                                                                    </ajaxToolkit:MaskedEditExtender>
                                                                    <asp:Button ID="btnCal_Ship_Date" runat="server" CssClass="btncalendar_enabled" />
                                                                    <ajaxToolkit:CalendarExtender ID="CE_Ship_Date" runat="server" Format="dd/MM/yyyy"
                                                                        TargetControlID="txtShippingDate" PopupButtonID="btnCal_Ship_Date" Enabled="True">
                                                                    </ajaxToolkit:CalendarExtender>
                                                                    <ajaxToolkit:MaskedEditValidator ID="MV_Ship_Date" runat="server" ControlExtender="ME_Ship_Date"
                                                                        ValidationGroup="dtVal" ControlToValidate="txtShippingDate" EmptyValueMessage="Enter Date Value"
                                                                        InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                                        Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">FIRST :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtDocFirst1" runat="server" CssClass="textBox" MaxLength="30" TabIndex="30"
                                                                        Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    </span><asp:TextBox ID="txtDocFirst2" runat="server" CssClass="textBox" MaxLength="30"
                                                                        TabIndex="31" Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtDocFirst3" runat="server" CssClass="textBox" MaxLength="30" TabIndex="32"
                                                                        Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <span class="elementLabel">Vessel Name :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtVesselName" runat="server" CssClass="textBox" MaxLength="30"
                                                                        TabIndex="33" Width="300px" onchange="return FillAMLVesselName();"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">SECOND :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtDocSecond1" runat="server" CssClass="textBox" MaxLength="30"
                                                                        TabIndex="34" Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtDocSecond2" runat="server" CssClass="textBox" MaxLength="30"
                                                                        TabIndex="35" Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtDocSecond3" runat="server" CssClass="textBox" MaxLength="30"
                                                                        TabIndex="36" Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <span class="elementLabel">From :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtFromPort" runat="server" CssClass="textBox" MaxLength="30" TabIndex="37"
                                                                        Width="100px" onchange="return FillAMLFromPort();"></asp:TextBox>
                                                                    &nbsp;&nbsp; <span class="elementLabel">To :</span>
                                                                    <asp:TextBox ID="txtToPort" runat="server" CssClass="textBox" MaxLength="30" TabIndex="38"
                                                                        Width="100px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" width="100%" colspan="6">
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="right" width="10%">
                                                                    <span class="elementLabel" style="font-weight: bold">Commission / Charges :</span>
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    <span class="elementLabel" style="font-weight: bold">CURRENCY</span>
                                                                </td>
                                                                <td align="left" width="10%">
                                                                    &nbsp;&nbsp;<span class="elementLabel" style="font-weight: bold">AMOUNT</span>
                                                                </td>
                                                                <td align="left" width="35%">
                                                                    <asp:Button ID="btn_DiscrepancyList" runat="server" Text="Discrepancy" CssClass="buttonDefault"
                                                                        Height="20px" Width="150px" Enabled="false"></asp:Button>
                                                                    <asp:CheckBox ID="cb_Protest" Text="Protest Flag" CssClass="elementLabel" runat="server"
                                                                        onclick="return validate_ProtestFlag('Protest');" />
                                                                    <asp:RadioButton ID="rdb_MT499" Text="MT499" CssClass="elementLabel" runat="server"
                                                                        GroupName="Discrepancy_Type" />
                                                                    <asp:RadioButton ID="rdb_MT734" Text="MT734" CssClass="elementLabel" runat="server"
                                                                        GroupName="Discrepancy_Type" onclick="return validate_ProtestFlag('MT734');" />
                                                                    <asp:RadioButton ID="rdb_MT799" Text="MT799" CssClass="elementLabel" runat="server"
                                                                        GroupName="Discrepancy_Type" onclick="return validate_ProtestFlag('MT799');" />
                                                                    <asp:RadioButton ID="rdb_MT999" Text="MT999" CssClass="elementLabel" runat="server"
                                                                        GroupName="Discrepancy_Type" />
                                                                    <asp:RadioButton ID="rbd_None" Text="None" CssClass="elementLabel" runat="server"
                                                                        GroupName="Discrepancy_Type" onclick="return validate_ProtestFlag('NONE');" />
                                                                </td>
                                                                <td width="35%">
                                                                    <asp:Button ID="btn_NarrativeList" runat="server" Text="Narrative" CssClass="buttonDefault"
                                                                        Height="20px" Width="150px" Enabled="false"></asp:Button>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">Interest :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:DropDownList ID="ddl_Interest_Currency" runat="server" CssClass="dropdownList"
                                                                        Enabled="False" TabIndex="39">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_Interest_Amount" runat="server" CssClass="textBox" MaxLength="12"
                                                                        TabIndex="40" Width="150px" Style="text-align: right"></asp:TextBox>
                                                                </td>
                                                                <td valign="top" rowspan='5'>
                                                                    <ajaxToolkit:CollapsiblePanelExtender ID="CPE_Discrepancy" runat="server" TargetControlID="panel_AddDiscrepancy"
                                                                        CollapsedSize="0" ExpandedSize="150" ExpandControlID="btn_DiscrepancyList" CollapseControlID="btn_DiscrepancyList"
                                                                        ScrollContents="True" Enabled="True" />
                                                                    <div id="divDisc" style="display: none;" runat="server">
                                                                        <asp:Panel ID="panel_AddDiscrepancy" runat="server">
                                                                            <table cellspacing="0" width="100%">
                                                                                <tr>
                                                                                    <td align="left">
                                                                                        <span class="elementLabel">1.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_1" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <span class="elementLabel">2.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_2" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left">
                                                                                        <span class="elementLabel">3.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_3" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <span class="elementLabel">4.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_4" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <span class="elementLabel">5.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_5" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left">
                                                                                        <span class="elementLabel">6.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_6" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <span class="elementLabel">7.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_7" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left">
                                                                                        <span class="elementLabel">8.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_8" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <span class="elementLabel">9.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_9" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <span class="elementLabel">10.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_10" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left">
                                                                                        <span class="elementLabel">11.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_11" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <span class="elementLabel">12.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_12" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left">
                                                                                        <span class="elementLabel">13.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_13" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <span class="elementLabel">14.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_14" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <span class="elementLabel">15.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_15" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left">
                                                                                        <span class="elementLabel">16.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_16" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <span class="elementLabel">17.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_17" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left">
                                                                                        <span class="elementLabel">18.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_18" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <span class="elementLabel">19.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_19" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <span class="elementLabel">20.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_20" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left">
                                                                                        <span class="elementLabel">21.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_21" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <span class="elementLabel">22.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_22" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left">
                                                                                        <span class="elementLabel">23.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_23" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <span class="elementLabel">24.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_24" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <span class="elementLabel">25.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_25" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left">
                                                                                        <span class="elementLabel">26.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_26" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <span class="elementLabel">27.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_27" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left">
                                                                                        <span class="elementLabel">28.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_28" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <span class="elementLabel">29.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_29" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <span class="elementLabel">30.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_30" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left">
                                                                                        <span class="elementLabel">31.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_31" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <span class="elementLabel">32.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_32" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left">
                                                                                        <span class="elementLabel">33.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_33" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <span class="elementLabel">34.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_34" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <span class="elementLabel">35.</span>
                                                                                        <asp:TextBox ID="txt_Discrepancy_35" Width="90%" runat="server" CssClass="textBox"
                                                                                            MaxLength="50" TabIndex="45"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </div>
                                                                </td>
                                                                <td width="40%" valign="top" rowspan='5'>
                                                                    <ajaxToolkit:CollapsiblePanelExtender ID="CPE_Narrative" runat="server" TargetControlID="panel_AddNarrative"
                                                                        CollapsedSize="0" ExpandedSize="150" ExpandControlID="btn_NarrativeList" CollapseControlID="btn_NarrativeList"
                                                                        ScrollContents="True" Enabled="True" />
                                                                    <asp:Panel ID="panel_AddNarrative" runat="server">
                                                                        <table cellspacing="0" width="100%">
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <span class="elementLabel">1.</span>
                                                                                    <asp:TextBox ID="txt_Narrative_1" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                                                        TabIndex="46"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <span class="elementLabel">2.</span>
                                                                                    <asp:TextBox ID="txt_Narrative_2" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                                                        TabIndex="46"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <span class="elementLabel">3.</span>
                                                                                    <asp:TextBox ID="txt_Narrative_3" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                                                        TabIndex="46"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <span class="elementLabel">4.</span>
                                                                                    <asp:TextBox ID="txt_Narrative_4" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                                                        TabIndex="46"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <span class="elementLabel">5.</span>
                                                                                    <asp:TextBox ID="txt_Narrative_5" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                                                        TabIndex="46"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <span class="elementLabel">6.</span>
                                                                                    <asp:TextBox ID="txt_Narrative_6" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                                                        TabIndex="46"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <span class="elementLabel">7.</span>
                                                                                    <asp:TextBox ID="txt_Narrative_7" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                                                        TabIndex="46"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <span class="elementLabel">8.</span>
                                                                                    <asp:TextBox ID="txt_Narrative_8" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                                                        TabIndex="46"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <span class="elementLabel">9.</span>
                                                                                    <asp:TextBox ID="txt_Narrative_9" Width="90%" runat="server" CssClass="textBox" MaxLength="50"
                                                                                        TabIndex="46"></asp:TextBox>
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
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">Our Commission :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:DropDownList ID="ddl_Comission_Currency" runat="server" CssClass="dropdownList"
                                                                        Enabled="False">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtComissionAmount" runat="server" CssClass="textBox" MaxLength="12"
                                                                        TabIndex="41" Width="150px" Style="text-align: right"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">Other Commission :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:DropDownList ID="ddl_Other_Currency" runat="server" CssClass="dropdownList"
                                                                        Enabled="False">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtOther_amount" runat="server" CssClass="textBox" MaxLength="12"
                                                                        TabIndex="42" Width="150px" Style="text-align: right"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">Their Commission :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:DropDownList ID="ddl_Their_Commission_Currency" runat="server" CssClass="dropdownList"
                                                                        Enabled="False">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtTheirCommission_Amount" runat="server" CssClass="textBox" TabIndex="43"
                                                                        MaxLength="12" Width="150px" Style="text-align: right"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">Total Bill Amt :</span>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_Total_Bill_Amt" runat="server" CssClass="textBox" MaxLength="12"
                                                                        Width="150px" Style="text-align: right" Enabled="False"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <asp:Panel ID="panal_Stamp_Duty_Charges" runat="server" Visible="false">
                                                                    <td align="right">
                                                                        <span class="elementLabel">Stamp Duty Charges :</span>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="ddl_Stamp_Duty_Charges_Curr" runat="server" CssClass="dropdownList"
                                                                            Enabled="False">
                                                                            <asp:ListItem Value="INR" Text="INR"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <span class="elementLabel">Exch. Rate:</span>
                                                                        <asp:TextBox ID="txt_Stamp_Duty_Charges_ExRate" runat="server" CssClass="textBox"
                                                                            TabIndex="44" MaxLength="8" Width="50px" Style="text-align: right"></asp:TextBox>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txt_Stamp_Duty_Charges_Amount" runat="server" CssClass="textBox"
                                                                            MaxLength="12" Width="150px" Style="text-align: right" Enabled="False"></asp:TextBox>
                                                                    </td>
                                                                </asp:Panel>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" colspan="2">
                                                                    <asp:Panel ID="panal_Scrutiny" runat="server">
                                                                        <span class="elementLabel">Documents Scrutiny :</span>
                                                                        <asp:DropDownList ID="ddl_Doc_Scrutiny" runat="server" CssClass="dropdownList" onchange="return OnDocumentScrutinyChange();">
                                                                            <asp:ListItem Value="1" Text="Clean"></asp:ListItem>
                                                                            <asp:ListItem Value="2" Text="Discrepant"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </asp:Panel>
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:Button ID="btnNext" runat="server" Text="Next >>" CssClass="buttonDefault" ToolTip="Go to Instructions"
                                                                        OnClientClick="return OnDocNextClickNew(1);" />&nbsp;&nbsp;&nbsp;
                                                                    <asp:Button ID="btn_Swift_Create" runat="server" Text="View Swift Massage" CssClass="buttonDefault"
                                                                        Width="150px" OnClientClick="return ViewSwiftMessage();"></asp:Button>&nbsp;&nbsp;&nbsp;
                                                                    <asp:Button ID="btn_SFMS_create" runat="server" Text="View SFMS Massage" CssClass="buttonDefault"
                                                                        Width="150px" OnClientClick="return ViewSFMSMessage();"></asp:Button>
                                                                    <asp:Button ID="btn_Verify" runat="server" Text="LEIVerify" CssClass="buttonDefault"
                                                                        ToolTip="Click here to verify LEI Details" Visible="false" TabIndex="107" 
                                                                        OnClientClick="return LeiVerify();" OnClick="btn_Verify_Click" />
                                                                    <asp:Label ID="lblSwift_SFMS" runat="server" CssClass="elementLabel"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel ID="tbSwift" runat="server" HeaderText="SWIFT" Font-Bold="true"
                                        ForeColor="White">
                                        <ContentTemplate>
                                            <table cellpadding="0" cellspacing="2" border="0" width="100%">
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">[73] Charges Claimed :</span>
                                                    </td>
                                                    <td align="left">
                                                        <span class="elementLabel">1 :</span>&nbsp;
                                                        <asp:TextBox ID="txtChargesClaimed7341" Width="350px" runat="server" CssClass="textBox"
                                                            MaxLength="35" TabIndex="47"></asp:TextBox>
                                                        <span class="elementLabel">2 :</span>&nbsp;
                                                        <asp:TextBox ID="txtChargesClaimed7342" Width="350px" runat="server" CssClass="textBox"
                                                            MaxLength="35" TabIndex="47"></asp:TextBox>
                                                        <span class="elementLabel">3 :</span>&nbsp;
                                                        <asp:TextBox ID="txtChargesClaimed7343" Width="350px" runat="server" CssClass="textBox"
                                                            MaxLength="35" TabIndex="47"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                    </td>
                                                    <td align="left">
                                                        <span class="elementLabel">4 :</span>&nbsp;
                                                        <asp:TextBox ID="txtChargesClaimed7344" Width="350px" runat="server" CssClass="textBox"
                                                            MaxLength="35" TabIndex="47"></asp:TextBox>
                                                        <span class="elementLabel">5 :</span>&nbsp;
                                                        <asp:TextBox ID="txtChargesClaimed7345" Width="350px" runat="server" CssClass="textBox"
                                                            MaxLength="35" TabIndex="47"></asp:TextBox>
                                                        <span class="elementLabel">6 :</span>&nbsp;
                                                        <asp:TextBox ID="txtChargesClaimed7346" Width="350px" runat="server" CssClass="textBox"
                                                            MaxLength="35" TabIndex="47"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">[33A] Total Amount Claimed </span>
                                                        <asp:DropDownList ID="ddlTotalAmountClaimed734" runat="server" CssClass="dropdownList"
                                                            onchange="return TotalAmountClaimedChange();">
                                                            <asp:ListItem Text="33A" Value="A"></asp:ListItem>
                                                            <asp:ListItem Text="33B" Value="B"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        &nbsp;
                                                    </td>
                                                    <td align="left">
                                                        &nbsp;<asp:Label ID="lblDate734" runat="server" Text="Date :" CssClass="elementLabel"></asp:Label>
                                                        <asp:TextBox ID="txtDate734" Width="70px" runat="server" CssClass="textBox" MaxLength="10"
                                                            TabIndex="47" ValidationGroup="dtVal" onkeydown="return allowBackSpace('734',event);"></asp:TextBox>
                                                        <asp:Button ID="btncalendar_Date734" runat="server" CssClass="btncalendar_enabled"
                                                            TabIndex="-1" />
                                                        <ajaxToolkit:MaskedEditExtender ID="md743date" Mask="99/99/9999" MaskType="Date"
                                                            runat="server" TargetControlID="txtDate734" InputDirection="RightToLeft" AcceptNegative="Right"
                                                            ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Right" PromptCharacter="_">
                                                        </ajaxToolkit:MaskedEditExtender>
                                                        <ajaxToolkit:CalendarExtender ID="calendar734Date" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtDate734" PopupButtonID="btncalendar_Date734">
                                                        </ajaxToolkit:CalendarExtender>
                                                        <span class="elementLabel">Currency :</span>
                                                        <asp:TextBox ID="txtCurrency734" Width="30px" runat="server" CssClass="textBox" MaxLength="3"
                                                            TabIndex="47" onchange="return SwiftsAmountvalidate('734');"></asp:TextBox>
                                                        <span class="elementLabel">Amount :</span>
                                                        <asp:TextBox ID="txtAmount734" Width="130px" runat="server" CssClass="textBox" MaxLength="15"
                                                            TabIndex="47" onchange="return SwiftsAmountvalidate('734');"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">[57A] Account With Bank : </span>
                                                        <asp:DropDownList ID="ddlAccountWithBank734" runat="server" CssClass="dropdownList"
                                                            onchange="return AccountWithBankChange();">
                                                            <asp:ListItem Text="57A" Value="A"></asp:ListItem>
                                                            <asp:ListItem Text="57B" Value="B"></asp:ListItem>
                                                            <asp:ListItem Text="57D" Value="D"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <span class="elementLabel">A / C :</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtAccountWithBank734PartyIdentifier" runat="server" CssClass="textBox"
                                                            TabIndex="47" Width="10px" MaxLength="1"></asp:TextBox>
                                                        <asp:TextBox ID="txtAccountWithBank734AccountNo" runat="server" CssClass="textBox"
                                                            TabIndex="47" Width="300px" MaxLength="35"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblAccountWithBank734SwiftCode" runat="server" CssClass="elementLabel"
                                                            Text="Swift/IFSC Code :"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox runat="server" ID="txtAccountWithBank734SwiftCode" CssClass="textBox"
                                                            TabIndex="47" MaxLength="11" Width="100px"></asp:TextBox>
                                                        <asp:TextBox runat="server" ID="txtAccountWithBank734Location" CssClass="textBox"
                                                            Style="display: none;" MaxLength="35" Width="250px" TabIndex="47"></asp:TextBox>
                                                        <asp:TextBox runat="server" ID="txtAccountWithBank734Name" CssClass="textBox" Style="display: none;"
                                                            MaxLength="35" Width="250px" TabIndex="47"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblAccountWithBank734Address1" runat="server" CssClass="elementLabel"
                                                            Style="display: none;" Text="Address1 :"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox runat="server" ID="txtAccountWithBank734Address1" CssClass="textBox"
                                                            Style="display: none;" MaxLength="35" Width="250px" TabIndex="47"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblAccountWithBank734Address2" runat="server" CssClass="elementLabel"
                                                            Style="display: none;" Text="Address2 :"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox runat="server" ID="txtAccountWithBank734Address2" CssClass="textBox"
                                                            Style="display: none;" MaxLength="35" Width="250px" TabIndex="47"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblAccountWithBank734Address3" runat="server" CssClass="elementLabel"
                                                            Style="display: none;" Text="Address3 :"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox runat="server" ID="txtAccountWithBank734Address3" CssClass="textBox"
                                                            Style="display: none;" MaxLength="35" Width="250px" TabIndex="47"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">[72] Sender to Receiver Information :</span>
                                                    </td>
                                                    <td align="left">
                                                        <span class="elementLabel">1 :</span>&nbsp;
                                                        <asp:TextBox ID="txtSendertoReceiverInformation7341" Width="350px" runat="server"
                                                            CssClass="textBox" MaxLength="35" TabIndex="47"></asp:TextBox>
                                                        <span class="elementLabel">2 :</span>&nbsp;
                                                        <asp:TextBox ID="txtSendertoReceiverInformation7342" Width="350px" runat="server"
                                                            CssClass="textBox" MaxLength="35" TabIndex="47"></asp:TextBox>
                                                        <span class="elementLabel">3 :</span>&nbsp;
                                                        <asp:TextBox ID="txtSendertoReceiverInformation7343" Width="350px" runat="server"
                                                            CssClass="textBox" MaxLength="35" TabIndex="47"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                    </td>
                                                    <td align="left">
                                                        <span class="elementLabel">4 :</span>&nbsp;
                                                        <asp:TextBox ID="txtSendertoReceiverInformation7344" Width="350px" runat="server"
                                                            CssClass="textBox" MaxLength="35" TabIndex="47"></asp:TextBox>
                                                        <span class="elementLabel">5 :</span>&nbsp;
                                                        <asp:TextBox ID="txtSendertoReceiverInformation7345" Width="350px" runat="server"
                                                            CssClass="textBox" MaxLength="35" TabIndex="47"></asp:TextBox>
                                                        <span class="elementLabel">6 :</span>&nbsp;
                                                        <asp:TextBox ID="txtSendertoReceiverInformation7346" Width="350px" runat="server"
                                                            CssClass="textBox" MaxLength="35" TabIndex="47"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">[77B] Disposal Of Document:</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="ddl_DisposalOfDoc" runat="server" CssClass="dropdownList" Width="80px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr><td><br /></td></tr>
                                                
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Button ID="btnSwiftNext" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                            ToolTip="Back to Document Details" TabIndex="106" OnClientClick="return OnSwiftPrevClickNew(0);" />&nbsp;&nbsp;&nbsp;
                                                        <asp:Button ID="btnSwiftPrev" runat="server" Text=">> Next" CssClass="buttonDefault"
                                                            ToolTip="Go to Import Accounting" TabIndex="106" OnClientClick="return OnSwiftNextClickNew(2);" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel ID="tbDocumentInstructions" runat="server" HeaderText="INSTRUCTIONS"
                                        Font-Bold="true" ForeColor="Lime">
                                        <ContentTemplate>
                                            <div style="width: 50%; float: left; height: 100%">
                                                <table cellpadding="0" cellspacing="2" border="0" width="100%">
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:RadioButton ID="rdb_SP_Instr_None" Text="None" CssClass="elementLabel" runat="server"
                                                                onchange="return OnNoneChange();" GroupName="SP_Instruction" OnCheckedChanged="rdb_SP_Instr_None_CheckedChanged" />
                                                            <asp:RadioButton ID="rdb_SP_Instr_Other" Text="Others" CssClass="elementLabel" runat="server"
                                                                GroupName="SP_Instruction" onchange="return OnOthersChange();" OnCheckedChanged="rdb_SP_Instr_Other_CheckedChanged" />
                                                            <asp:RadioButton ID="rdb_SP_Instr_Annexure" Text="As per Annexure" CssClass="elementLabel"
                                                                runat="server" GroupName="SP_Instruction" onchange="return OnAsPerAnnexureChange();"
                                                                OnCheckedChanged="rdb_SP_Instr_Annexure_CheckedChanged" />
                                                            <asp:RadioButton ID="rdb_SP_Instr_On_Sight" Text="Bene. to be paid on sight" CssClass="elementLabel"
                                                                runat="server" GroupName="SP_Instruction1" onchange="return OnBenetobepaidonsightChange();"
                                                                OnCheckedChanged="rdb_SP_Instr_On_Sight_CheckedChanged" />
                                                            <asp:RadioButton ID="rdb_SP_Instr_On_Date" Text="Bene. to be paid on Dated" CssClass="elementLabel"
                                                                runat="server" GroupName="SP_Instruction1" onchange="return OnBenetobepaidonDatedChange();"
                                                                OnCheckedChanged="rdb_SP_Instr_On_Date_CheckedChanged" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="20%">
                                                            <span class="elementLabel" style="font-weight: bold">Special Instructions :</span>
                                                        </td>
                                                        <td align="left" width="80%">
                                                            <span class="elementLabel">1.</span>
                                                            <asp:TextBox ID="txt_SP_Instructions1" Width="400px" runat="server" CssClass="textBox"
                                                                MaxLength="60" TabIndex="47"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <span class="elementLabel">2.</span>
                                                            <asp:TextBox ID="txt_SP_Instructions2" Width="400px" runat="server" CssClass="textBox"
                                                                MaxLength="60" TabIndex="48"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td align="left">
                                                            <span class="elementLabel">3.</span>
                                                            <asp:TextBox ID="txt_SP_Instructions3" Width="400px" runat="server" CssClass="textBox"
                                                                MaxLength="60" TabIndex="49"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <span class="elementLabel">4.</span>
                                                            <asp:TextBox ID="txt_SP_Instructions4" Width="400px" runat="server" CssClass="textBox"
                                                                MaxLength="60" TabIndex="50"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <span class="elementLabel">5.</span>
                                                            <asp:TextBox ID="txt_SP_Instructions5" Width="400px" runat="server" CssClass="textBox"
                                                                MaxLength="60" TabIndex="51"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="font-weight: bold">Instructions :</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="txt_Instructions1" runat="server" CssClass="elementLabel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="txt_Instructions2" runat="server" CssClass="elementLabel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="txt_Instructions3" runat="server" CssClass="elementLabel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="txt_Instructions4" runat="server" CssClass="elementLabel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="txt_Instructions5" runat="server" CssClass="elementLabel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="font-weight: bold">Own LC Discounting :</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:RadioButton ID="rdb_ownLCDiscount_Yes" Text="Yes" CssClass="elementLabel" runat="server"
                                                                GroupName="ownLCDiscount" TabIndex="54" />
                                                            <asp:RadioButton ID="rdb_ownLCDiscount_No" Text="No" CssClass="elementLabel" runat="server"
                                                                GroupName="ownLCDiscount" TabIndex="54" Checked="True" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Button ID="btn_Instr_Prev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                                ToolTip="Back to Document Details" TabIndex="106" OnClientClick="return OnInstPrevClickNew(1);" />&nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btn_Instr_Next" runat="server" Text=">> Next" CssClass="buttonDefault"
                                                                ToolTip="Go to Import Accounting" TabIndex="106" OnClientClick="return OnInstNextClickNew(3);" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div style="width: 40%; float: left; height: 100%">
                                                <asp:Panel ID="PanelPaymentSwiftDetail" runat="server">
                                                    <table width="100%">
                                                        <tr>
                                                            <td class="tdpayment1">
                                                            </td>
                                                            <td class="tdpayment2">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <span class="elementLabel" style="font-weight: bold">Payment Swift Details :</span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" colspan="2">
                                                                <asp:DropDownList ID="ddlPaymentSwift56" runat="server" CssClass="dropdownList" Width="60px"
                                                                    onchange="return OnPaymentSwift56Change();" OnSelectedIndexChanged="ddlPaymentSwift56_SelectedIndexChanged"
                                                                    TabIndex="55">
                                                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                                    <asp:ListItem Value="56A" Text="56A"></asp:ListItem>
                                                                    <asp:ListItem Value="56D" Text="56D"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span class="elementLabel">Intermediary</span>
                                                            </td>
                                                        </tr>
                                                        <tr id="panel56ACC_No" runat="server" style="display: none;">
                                                            <td align="right" class="tdpayment1">
                                                                <span class="elementLabel">56 A/C No :</span>
                                                            </td>
                                                            <td align="left" class="tdpayment2">
                                                                <asp:TextBox ID="txt_PaymentSwift56ACC_No" Width="180px" runat="server" CssClass="textBox"
                                                                    MaxLength="35" TabIndex="56"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" style="display: none;" id="panel56A">
                                                            <td align="right" class="tdpayment1">
                                                                <span class="elementLabel">56A Swift :</span>
                                                            </td>
                                                            <td align="left" class="tdpayment2">
                                                                <asp:TextBox ID="txt_PaymentSwift56A" Width="100px" runat="server" CssClass="textBox"
                                                                    MaxLength="11" TabIndex="57"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" style="display: none;" id="panel56DName">
                                                            <td align="right" class="tdpayment1">
                                                                <span class="elementLabel">56D Name :</span>
                                                            </td>
                                                            <td align="left" class="tdpayment2">
                                                                <asp:TextBox ID="txt_PaymentSwift56D_name" Width="350px" runat="server" CssClass="textBox"
                                                                    MaxLength="35" TabIndex="58"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" style="display: none;" id="panel56DAddress">
                                                            <td align="right" class="tdpayment1">
                                                                <span class="elementLabel">56D Address :</span>
                                                            </td>
                                                            <td align="left" class="tdpayment2">
                                                                <asp:TextBox ID="txt_PaymentSwift56D_Address" Width="350px" runat="server" CssClass="textBox"
                                                                    MaxLength="105" TabIndex="59"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" colspan="2">
                                                                <asp:DropDownList ID="ddlPaymentSwift57" runat="server" CssClass="dropdownList" Width="60px"
                                                                    onchange="return OnPaymentSwift57Change();" OnSelectedIndexChanged="ddlPaymentSwift57_SelectedIndexChanged"
                                                                    TabIndex="60">
                                                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                                    <asp:ListItem Value="57A" Text="57A"></asp:ListItem>
                                                                    <asp:ListItem Value="57D" Text="57D"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span class="elementLabel">A/c with Institution</span>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" style="display: none;" id="panel57ACC_No">
                                                            <td align="right" class="tdpayment1">
                                                                <span class="elementLabel">57 A/C No :</span>
                                                            </td>
                                                            <td align="left" class="tdpayment2">
                                                                <asp:TextBox ID="txt_PaymentSwift57ACC_No" Width="180px" runat="server" CssClass="textBox"
                                                                    MaxLength="35" TabIndex="61"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" style="display: none;" id="panel57A">
                                                            <td align="right" class="tdpayment1">
                                                                <span class="elementLabel">57A Swift :</span>
                                                            </td>
                                                            <td align="left" class="tdpayment2">
                                                                <asp:TextBox ID="txt_PaymentSwift57A" Width="100px" runat="server" CssClass="textBox"
                                                                    MaxLength="11" TabIndex="62"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" style="display: none;" id="panel57DName">
                                                            <td align="right" class="tdpayment1">
                                                                <span class="elementLabel">57D Name :</span>
                                                            </td>
                                                            <td align="left" class="tdpayment2">
                                                                <asp:TextBox ID="txt_PaymentSwift57D_name" Width="350px" runat="server" CssClass="textBox"
                                                                    MaxLength="35" TabIndex="63"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" style="display: none;" id="panel57DAddress">
                                                            <td align="right" class="tdpayment1">
                                                                <span class="elementLabel">57D Address :</span>
                                                            </td>
                                                            <td align="left" class="tdpayment2">
                                                                <asp:TextBox ID="txt_PaymentSwift57D_Address" Width="350px" runat="server" CssClass="textBox"
                                                                    MaxLength="105" TabIndex="64"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" colspan="2">
                                                                <asp:DropDownList ID="ddlPaymentSwift58" runat="server" CssClass="dropdownList" Width="60px"
                                                                    TabIndex="65" onchange="return OnPaymentSwift58Change();" OnSelectedIndexChanged="ddlPaymentSwift58_SelectedIndexChanged">
                                                                    <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                                    <asp:ListItem Value="58A" Text="58A"></asp:ListItem>
                                                                    <asp:ListItem Value="58D" Text="58D"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span class="elementLabel">Beneficiary Institution</span>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" style="display: none;" id="panel58ACC_No">
                                                            <td align="right" class="tdpayment1">
                                                                <span class="elementLabel">58 A/C No :</span>
                                                            </td>
                                                            <td align="left" class="tdpayment2">
                                                                <asp:TextBox ID="txt_PaymentSwift58ACC_No" Width="180px" runat="server" CssClass="textBox"
                                                                    MaxLength="35" TabIndex="66"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" style="display: none;" id="panel58A">
                                                            <td align="right" class="tdpayment1">
                                                                <span class="elementLabel">58A Swift :</span>
                                                            </td>
                                                            <td align="left" class="tdpayment2">
                                                                <asp:TextBox ID="txt_PaymentSwift58A" Width="100px" runat="server" CssClass="textBox"
                                                                    MaxLength="11" TabIndex="67"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" style="display: none;" id="panel58DName">
                                                            <td align="right" class="tdpayment1">
                                                                <span class="elementLabel">58D Name :</span>
                                                            </td>
                                                            <td align="left" class="tdpayment2">
                                                                <asp:TextBox ID="txt_PaymentSwift58D_name" Width="350px" runat="server" CssClass="textBox"
                                                                    MaxLength="35" TabIndex="68"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" style="display: none;" id="panel58Address">
                                                            <td align="right" class="tdpayment1">
                                                                <span class="elementLabel">58D Address 1 :</span>
                                                            </td>
                                                            <td align="left" class="tdpayment2">
                                                                <asp:TextBox ID="txt_PaymentSwift58D_Address" Width="350px" runat="server" CssClass="textBox"
                                                                    MaxLength="35" TabIndex="69"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" style="display: none;" id="panel58Address2">
                                                            <td align="right" class="tdpayment1">
                                                                <span class="elementLabel">58D Address 2 :</span>
                                                            </td>
                                                            <td align="left" class="tdpayment2">
                                                                <asp:TextBox ID="txt_PaymentSwift58D_Address2" Width="350px" runat="server" CssClass="textBox"
                                                                    MaxLength="35" TabIndex="69"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" style="display: none;" id="panel58Address3">
                                                            <td align="right" class="tdpayment1">
                                                                <span class="elementLabel">58D Address 3 :</span>
                                                            </td>
                                                            <td align="left" class="tdpayment2">
                                                                <asp:TextBox ID="txt_PaymentSwift58D_Address3" Width="350px" runat="server" CssClass="textBox"
                                                                    MaxLength="35" TabIndex="69"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" style="display: none;" id="panel58Address4">
                                                            <td align="right" class="tdpayment1">
                                                            </td>
                                                            <td align="left" class="tdpayment2">
                                                                <asp:TextBox ID="txt_PaymentSwift58D_Address4" Width="350px" runat="server" CssClass="textBox"
                                                                    MaxLength="35" TabIndex="69" Style="visibility: hidden"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel ID="tbAmlDetails" runat="server" HeaderText="AML" Font-Bold="true"
                                        ForeColor="Lime">
                                        <ContentTemplate>
                                            <div style="width: 50%; float: left; height: 100%">
                                                <table cellpadding="0" cellspacing="2" border="0" width="100%">
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">Drawee :</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtAMLDrawee" Width="350px" runat="server" CssClass="textBox" MaxLength="50"
                                                                TabIndex="70"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">Drawer :</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtAMLDrawer" Width="350px" runat="server" CssClass="textBox" MaxLength="50"
                                                                TabIndex="71"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">Nego / Remit Bank :</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtAMLNagoRemiBank" Width="350px" runat="server" CssClass="textBox"
                                                                MaxLength="50" TabIndex="72"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">Commodity :</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtAMLCommodity" Width="350px" runat="server" CssClass="textBox"
                                                                MaxLength="50" TabIndex="73"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">Vessel :</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtAMLVessel" Width="350px" runat="server" CssClass="textBox" MaxLength="50"
                                                                TabIndex="74"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">From Port :</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtAMLFromPort" Width="350px" runat="server" CssClass="textBox"
                                                                MaxLength="50" TabIndex="75"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">Country :</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtAMLCountry" Width="350px" runat="server" CssClass="textBox" MaxLength="50"
                                                                TabIndex="75"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">Country of Origin :</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtCountryOfOrigin" Width="350px" runat="server" CssClass="textBox"
                                                                MaxLength="50" TabIndex="75"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server"
                                                                TargetControlID="panel_AML" CollapsedSize="0" ExpandedSize="150" ScrollContents="True"
                                                                Enabled="True" />
                                                            <asp:Panel ID="panel_AML" runat="server">
                                                                <div style="width: 72.3%; float: left; height: 100%">
                                                                    <table cellpadding="0" cellspacing="2" border="0" width="100%">
                                                                        <tr>
                                                                            <td align="right" style="width: 50%">
                                                                                <span class="elementLabel">1.</span>
                                                                            </td>
                                                                            <td align="left" style="width: 50%">
                                                                                <asp:TextBox ID="txtAML1" Width="350px" runat="server" CssClass="textBox" TabIndex="76"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 50%">
                                                                                <span class="elementLabel">2.</span>
                                                                            </td>
                                                                            <td align="left" style="width: 50%">
                                                                                <asp:TextBox ID="txtAML2" Width="350px" runat="server" CssClass="textBox" TabIndex="76"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 50%">
                                                                                <span class="elementLabel">3.</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtAML3" Width="350px" runat="server" CssClass="textBox" TabIndex="76"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 50%">
                                                                                <span class="elementLabel">4.</span>
                                                                            </td>
                                                                            <td align="left" style="width: 50%">
                                                                                <asp:TextBox ID="txtAML4" Width="350px" runat="server" CssClass="textBox" TabIndex="76"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 50%">
                                                                                <span class="elementLabel">5.</span>
                                                                            </td>
                                                                            <td align="left" style="width: 50%">
                                                                                <asp:TextBox ID="txtAML5" Width="350px" runat="server" CssClass="textBox" TabIndex="76"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 50%">
                                                                                <span class="elementLabel">6.</span>
                                                                            </td>
                                                                            <td align="left" style="width: 50%">
                                                                                <asp:TextBox ID="txtAML6" Width="350px" runat="server" CssClass="textBox" TabIndex="76"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 50%">
                                                                                <span class="elementLabel">7.</span>
                                                                            </td>
                                                                            <td align="left" style="width: 50%">
                                                                                <asp:TextBox ID="txtAML7" Width="350px" runat="server" CssClass="textBox" TabIndex="76"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 50%">
                                                                                <span class="elementLabel">8.</span>
                                                                            </td>
                                                                            <td align="left" style="width: 50%">
                                                                                <asp:TextBox ID="txtAML8" Width="350px" runat="server" CssClass="textBox" TabIndex="76"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 50%">
                                                                                <span class="elementLabel">9.</span>
                                                                            </td>
                                                                            <td align="left" style="width: 50%">
                                                                                <asp:TextBox ID="txtAML9" Width="350px" runat="server" CssClass="textBox" TabIndex="76"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 50%">
                                                                                <span class="elementLabel">10.</span>
                                                                            </td>
                                                                            <td align="left" style="width: 50%">
                                                                                <asp:TextBox ID="txtAML10" Width="350px" runat="server" CssClass="textBox" TabIndex="76"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 50%">
                                                                                <span class="elementLabel">11.</span>
                                                                            </td>
                                                                            <td align="left" style="width: 50%">
                                                                                <asp:TextBox ID="txtAML11" Width="350px" runat="server" CssClass="textBox" TabIndex="76"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 50%">
                                                                                <span class="elementLabel">12.</span>
                                                                            </td>
                                                                            <td align="left" style="width: 50%">
                                                                                <asp:TextBox ID="txtAML12" Width="350px" runat="server" CssClass="textBox" TabIndex="76"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 50%">
                                                                                <span class="elementLabel">13.</span>
                                                                            </td>
                                                                            <td align="left" style="width: 50%">
                                                                                <asp:TextBox ID="txtAML13" Width="350px" runat="server" CssClass="textBox" TabIndex="76"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 50%">
                                                                                <span class="elementLabel">14.</span>
                                                                            </td>
                                                                            <td align="left" style="width: 50%">
                                                                                <asp:TextBox ID="txtAML14" Width="350px" runat="server" CssClass="textBox" TabIndex="76"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 50%">
                                                                                <span class="elementLabel">15.</span>
                                                                            </td>
                                                                            <td align="left" style="width: 50%">
                                                                                <asp:TextBox ID="txtAML15" Width="350px" runat="server" CssClass="textBox" TabIndex="76"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 50%">
                                                                                <span class="elementLabel">16.</span>
                                                                            </td>
                                                                            <td align="left" style="width: 50%">
                                                                                <asp:TextBox ID="txtAML16" Width="350px" runat="server" CssClass="textBox" TabIndex="76"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 50%">
                                                                                <span class="elementLabel">17.</span>
                                                                            </td>
                                                                            <td align="left" style="width: 50%">
                                                                                <asp:TextBox ID="txtAML17" Width="350px" runat="server" CssClass="textBox" TabIndex="76"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 50%">
                                                                                <span class="elementLabel">18.</span>
                                                                            </td>
                                                                            <td align="left" style="width: 50%">
                                                                                <asp:TextBox ID="txtAML18" Width="350px" runat="server" CssClass="textBox" TabIndex="76"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 50%">
                                                                                <span class="elementLabel">19.</span>
                                                                            </td>
                                                                            <td align="left" style="width: 50%">
                                                                                <asp:TextBox ID="txtAML19" Width="350px" runat="server" CssClass="textBox" TabIndex="76"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 50%">
                                                                                <span class="elementLabel">20.</span>
                                                                            </td>
                                                                            <td align="left" style="width: 50%">
                                                                                <asp:TextBox ID="txtAML20" Width="350px" runat="server" CssClass="textBox" TabIndex="76"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 50%">
                                                                                <span class="elementLabel">21.</span>
                                                                            </td>
                                                                            <td align="left" style="width: 50%">
                                                                                <asp:TextBox ID="txtAML21" Width="350px" runat="server" CssClass="textBox" TabIndex="76"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 50%">
                                                                                <span class="elementLabel">22.</span>
                                                                            </td>
                                                                            <td align="left" style="width: 50%">
                                                                                <asp:TextBox ID="txtAML22" Width="350px" runat="server" CssClass="textBox" TabIndex="76"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 50%">
                                                                                <span class="elementLabel">23.</span>
                                                                            </td>
                                                                            <td align="left" style="width: 50%">
                                                                                <asp:TextBox ID="txtAML23" Width="350px" runat="server" CssClass="textBox" TabIndex="76"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 50%">
                                                                                <span class="elementLabel">24.</span>
                                                                            </td>
                                                                            <td align="left" style="width: 50%">
                                                                                <asp:TextBox ID="txtAML24" Width="350px" runat="server" CssClass="textBox" TabIndex="76"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 50%">
                                                                                <span class="elementLabel">25.</span>
                                                                            </td>
                                                                            <td align="left" style="width: 50%">
                                                                                <asp:TextBox ID="txtAML25" Width="350px" runat="server" CssClass="textBox" TabIndex="76"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 50%">
                                                                                <span class="elementLabel">26.</span>
                                                                            </td>
                                                                            <td align="left" style="width: 50%">
                                                                                <asp:TextBox ID="txtAML26" Width="350px" runat="server" CssClass="textBox" TabIndex="76"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 50%">
                                                                                <span class="elementLabel">27.</span>
                                                                            </td>
                                                                            <td align="left" style="width: 50%">
                                                                                <asp:TextBox ID="txtAML27" Width="350px" runat="server" CssClass="textBox" TabIndex="76"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 50%">
                                                                                <span class="elementLabel">28.</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtAML28" Width="350px" runat="server" CssClass="textBox" TabIndex="76"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 50%">
                                                                                <span class="elementLabel">29.</span>
                                                                            </td>
                                                                            <td align="left" style="width: 50%">
                                                                                <asp:TextBox ID="txtAML29" Width="350px" runat="server" CssClass="textBox" TabIndex="76"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" style="width: 50%">
                                                                                <span class="elementLabel">30.</span>
                                                                            </td>
                                                                            <td align="left" style="width: 50%">
                                                                                <asp:TextBox ID="txtAML30" Width="350px" runat="server" CssClass="textBox" TabIndex="76"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%" colspan="2">
                                                            <asp:Button ID="btnDocAccPrev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                                ToolTip="Back to Document Details" TabIndex="106" OnClientClick="return OnAMLPrevClickNew(2);" />
                                                            &nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btnAMLReport" runat="server" Text="View AML Report" CssClass="buttonDefault"
                                                                Width="150px" OnClientClick="return ViewAMLReport();" Visible="false"></asp:Button>
                                                            <asp:LinkButton ID="lnkDownload" runat="server" Text="Download AML Report" CssClass="buttonDefault"
                                                                OnClick="lnkDownload_Click"></asp:LinkButton>
                                                            &nbsp;&nbsp;&nbsp;
                                                           <%-- <asp:Button ID="btn_Verify" runat="server" Text="LEIVerify" CssClass="buttonDefault"
                                                                ToolTip="Click here to verify LEI Details" TabIndex="107" 
                                                                OnClientClick="return LeiVerify();" OnClick="btn_Verify_Click" />--%>
                                                            &nbsp;&nbsp;&nbsp;
                                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="buttonDefault"
                                                                ToolTip="Submit to Checker" OnClientClick="return SubmitConfirm();" OnClick="btnSubmit_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                </ajaxToolkit:TabContainer>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4">
                                <asp:Label ID="lblChecker_Remark" runat="server" CssClass="mandatoryField" Font-Bold="true"></asp:Label>
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
                                <asp:Label ID="Labeltest" runat="server" CssClass="mandatoryField" Font-Bold="true" ForeColor="Green" Visible="false"></asp:Label>
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
