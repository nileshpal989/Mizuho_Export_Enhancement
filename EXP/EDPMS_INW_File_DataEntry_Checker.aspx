<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDPMS_INW_File_DataEntry_Checker.aspx.cs" Inherits="EXP_EDPMS_INW_File_DataEntry_Checker" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />   
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Scripts/jquery-1.8.3.min.js" type="text/javascript""></script>
    <script src="../Help_Plugins/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <link href="../Help_Plugins/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Help_Plugins/MyJquery1.js" type="text/javascript"></script>
    <script type="text/javascript" src="Scripts/TF_EXP_INW_REMMITENCE_CHECKER.js"></script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
        <div>
            <uc1:Menu ID="Menu1" runat="server" />
            <asp:ScriptManager ID="ScriptManagerMain" runat="server" EnablePageMethods="true">
            </asp:ScriptManager>
            <script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>
            <script language="javascript" type="text/javascript" src="../Scripts/Enable_Disable_Opener.js"></script>
            <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
                <ProgressTemplate>
                    <div id="progressBackgroundMain" class="progressBackground">
                        <div id="processMessage" class="progressimageposition">
                            <img src="../Images/ajax-loader.gif" style="border: 0px" alt="" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <br />
                    <table width="100%" cellpadding="1">
                        <tr>
                            <td colspan="2" align="left">
                                <span class="pageLabel"><strong>Inward Remittances Data Entry-Checker </strong></span>
                            </td>
                            <td colspan="2" align="right">
                                 <asp:Label ID="ReccuringLEI" runat="server" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="LEIAmtCheck" runat="server"></asp:Label>
                                <asp:Label ID="LEIverify" runat="server"></asp:Label>
                                <asp:Button runat="server" ID="btnBack" CssClass="buttonDefault" Text="Back" OnClick="btnBack_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="left">
                                <asp:Label ID="lblmessage" runat="server" CssClass="mandatoryField"></asp:Label>
                                <input type="hidden" id="hdnOverseasPartyId" runat="server" />
                                <asp:Button ID="btnOverseasParty" Style="display: none;" runat="server" />
                                <input type="hidden" id="hdnLeiFlag" runat="server" />
                                <input type="hidden" id="hdnLeiSpecialFlag" runat="server" />
                                <input type="hidden" id="hdnBranchCode" runat="server" />
                                <input type="hidden" id="hdnCustabbr" runat="server" />
                                <input type="hidden" id="hdncustlei" runat="server" />
                                <input type="hidden" id="hdncustleiexpiry" runat="server" />
                                <input type="hidden" id="hdnoverseaslei" runat="server" />
                                <input type="hidden" id="hdnoverseasleiexpiry" runat="server" />
                                <input type="hidden" id="hdncustleiexpiryRe" runat="server" />
                                <input type="hidden" id="hdnoverseasleiexpiryRe" runat="server" />
                                <input type="hidden" id="hdnleiExchRate" runat="server" />
                                <input type="hidden" id="hdnbillamtinr" runat="server" />
                                <input type="hidden" id="hdnCustname" runat="server" />
                                <input type="hidden" id="hdnValidateLei" runat="server" />
                                <asp:Label ID="lblLEIEffectDate" runat="server" Text="22/07/2023" Visible="false"></asp:Label>
                                <input type="hidden" id="hdnDocNo" runat="server" />
                                <input type="hidden" id="hdnUserName" runat="server" />
                                <input type="hidden" id="hdnRejectReason" runat="server" />
                                <input type="hidden" id="hdnDocDate" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" width="5%">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Branch ID : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtBranchCode" runat="server" CssClass="textBox" Style="width: 40px" ReadOnly="true"
                                    Enabled="false" TabIndex="1"></asp:TextBox>
                                <asp:Label ID="lblBranchName" runat="server" CssClass="elementLabel"></asp:Label>
                            </td>
                            <td style="text-align: right; width: 5%; white-space: nowrap;">
                               <%-- <span class="mandatoryField">*</span> <span class="elementLabel">Year : </span>--%>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtYear" runat="server" CssClass="textBox" Style="text-align: center" ReadOnly="true"
                                    Width="20px" MaxLength="2" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Document No : </span>
                            </td>
                            <td align="left" width="3%">
                                <asp:TextBox ID="txtDocPref" runat="server" CssClass="textBox" Style="text-transform: uppercase; text-align: center"
                                    MaxLength="3" Width="30px" TabIndex="2" autocomplete="off" ReadOnly="true" />
                                <asp:TextBox ID="txtDocBRCode" runat="server" AutoPostBack="true" CssClass="textBox" ReadOnly="true"
                                    Style="text-align: center" Width="30px" Enabled="true"
                                    MaxLength="3" />
                                <asp:TextBox ID="txtDocNo" runat="server" AutoPostBack="true" CssClass="textBox" ReadOnly="true"
                                    Style="width: 60px; text-transform: uppercase; text-align: center" ToolTip="IRM number" MaxLength="6"
                                    TabIndex="3" />
                                <asp:TextBox ID="txtDocSrNo" runat="server" AutoPostBack="true" CssClass="textBox" ReadOnly="true"
                                    Style="width: 20px; text-transform: uppercase; text-align: center" MaxLength="2" />
                                <asp:Button ID="btnDocNoList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" Visible="false"
                                    OnClientClick="openDocNoList('mouseClick');" />
                            </td>
                            <td style="text-align: right; width: 5%; white-space: nowrap;">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Document Date :
                                </span>
                            </td>
                            <td align="left" nowrap>
                                <asp:TextBox ID="txtDocDate" runat="server" CssClass="textBox" Style="width: 70px" ReadOnly="true"
                                    TabIndex="4" ToolTip="Date on which IRM number is generated"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="mdDocdate" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtDocDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="btncalendar_DocDate" runat="server" CssClass="btncalendar_enabled" ReadOnly="true"
                                    TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtDocDate" PopupButtonID="btncalendar_DocDate" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdDocdate"
                                    ValidationGroup="dtVal" ControlToValidate="txtDocDate" EmptyValueMessage="Enter Date Value"
                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Value Date : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtvalueDate" runat="server" Width="70px" ToolTip="Remittance Date" CssClass="textBox" TabIndex="5"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtvalueDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="Button3" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtvalueDate" PopupButtonID="Button3" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender1"
                                    ValidationGroup="dtVal" ControlToValidate="txtvalueDate" EmptyValueMessage="Enter Date Value"
                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                            </td>
                            <td align="right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Swift Code:</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txt_swiftcode" runat="server" CssClass="textBox" MaxLength="20" ReadOnly="true"
                                    TabIndex="6" AutoPostBack="true"></asp:TextBox>
                                <asp:Button ID="btnHelpSwiftCode" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                    OnClientClick="openSwiftCodeHelp('mouseClick');" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Customer A/C No :
                                </span>
                            </td>
                            <td align="left" colspan="3">
                                <asp:TextBox ID="txtCustAcNo" runat="server" CssClass="textBox" Style="width: 100px"
                                    AutoPostBack="true" MaxLength="15" TabIndex="7"></asp:TextBox>
                                <asp:Button ID="btnHelpCustAcNo" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                    OnClientClick="openCustAcNo('mouseClick');" />
                                &nbsp;
                            <asp:Label ID="lblCustName" runat="server" Text="" CssClass="elementLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Purpose Code :
                                </span>
                            </td>
                            <td align="left" colspan="3">
                                <asp:TextBox ID="txtPurposeCode" runat="server" CssClass="textBox" Style="width: 50px"
                                    AutoPostBack="true" MaxLength="6"
                                    TabIndex="8"></asp:TextBox>
                                <asp:Button ID="Button1" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                    OnClientClick="return openPurposeCode('mouseClick','1');" />
                                &nbsp;<asp:Label ID="lblpurposeCode" runat="server" CssClass="elementLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="mandatoryField">*</span><span class="elementLabel">Remitter ID :</span>
                            </td>
                            <td align="left" colspan="3">
                                <asp:TextBox ID="txtOverseasPartyID" runat="server" AutoPostBack="True" CssClass="textBox"
                                    MaxLength="7" onkeydown="OpenOverseasPartyList(this);" TabIndex="2" Width="70px"
                                    ToolTip="Press F2 for help."></asp:TextBox>
                                <asp:Button ID="btnOverseasPartyList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                <asp:Label ID="lblOverseasPartyDesc" runat="server" CssClass="elementLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Remitter Name :</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtRemitterName" runat="server" Width="280px" CssClass="textBox"
                                    MaxLength="250" TabIndex="9"></asp:TextBox>
                            </td>
                            <td style="text-align: right; white-space: nowrap;">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Remitter Country :</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtRemitterCountry" runat="server" CssClass="textBox" Style="width: 30px"
                                    AutoPostBack="true" MaxLength="2" TabIndex="10" ToolTip="Remitter Country" OnTextChanged="txtOverseasPartyID_TextChanged"></asp:TextBox>
                                <asp:Button ID="Button4" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                    OnClientClick="return openCountryCode('mouseClick','1');" />
                                &nbsp;<asp:Label ID="lblCountryDesc" runat="server" CssClass="elementLabel" Width="200px"></asp:Label>
                                <%--<span id="SpanLei1" runat="server" class="elementLabel" visible="false">Customer LEI :</span>--%>
                                <asp:Label ID="lblLEI_CUST_Remark" runat="server" Visible="false"></asp:Label>
                        </tr>
                        <tr>
                            <td style="text-align: right; white-space: nowrap">
                                <span class="mandatoryField">*</span><span class="elementLabel">Remitter Address :</span>
                            </td>
                            <td colspan="3" align="left">
                                <asp:TextBox ID="txtRemitterAddress" CssClass="textBox" runat="server" MaxLength="100"
                                    Width="500px" TabIndex="11"></asp:TextBox>
                                <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <span id="SpanLei2" runat="server" class="elementLabel" visible="false">Customer LEI Expiry :</span>--%>
                                <asp:Label ID="lblLEIExpiry_CUST_Remark" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="style1">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Remitting Bank :</span>
                            </td>
                            <td align="left" class="style1">
                                <asp:TextBox ID="txtRemitterBank" CssClass="textBox" runat="server" MaxLength="100"
                                    Width="280px" TabIndex="12"></asp:TextBox>
                            </td>
                            <td style="text-align: right; white-space: nowrap;" class="style1">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Bank Country :</span>
                            </td>
                            <td align="left" class="style1">
                                <asp:TextBox ID="txtRemBankCountry" runat="server" CssClass="textBox" Style="width: 30px"
                                    AutoPostBack="true" MaxLength="2" TabIndex="13"></asp:TextBox>
                                <asp:Button ID="Button5" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                    OnClientClick="return openCountryCode('mouseClick','2');" />
                                &nbsp;<asp:Label ID="lblRemBankCountryDesc" runat="server" CssClass="elementLabel"
                                    Width="200px"></asp:Label>
                                <%--&nbsp;<span id="SpanLei3" runat="server" class="elementLabel" visible="false">Applicant LEI :</span>--%>
                                <asp:Label ID="lblLEI_Overseas_Remark" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="white-space: nowrap; text-align: right">
                                <span class="elementLabel">Remitting Bank Address : </span>
                            </td>
                            <td colspan="3" align="left">
                                <asp:TextBox ID="txtRemitterBankAddress" CssClass="textBox" runat="server" MaxLength="100"
                                    Width="500px" TabIndex="14"></asp:TextBox>
                                <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <span id="SpanLei4" runat="server" class="elementLabel" visible="false">Applicant LEI Expiry :</span>--%>
                                <asp:Label ID="lblLEIExpiry_Overseas_Remark" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; white-space: nowrap">
                                <span class="elementLabel">Purpose Of Remittance :</span>
                            </td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtpurposeofRemittance" CssClass="textBox" runat="server" MaxLength="40"
                                    Width="280px" TabIndex="15" ToolTip="Applicable purpose of remittance"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Currency : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="ddlCurrency" runat="server" CssClass="dropdownList" ToolTip="Remittance FC Code" TabIndex="16" Height="19px" Width="25px">
                                </asp:TextBox>
                                &nbsp;
                            <asp:Label ID="lblcurrencyDesc" runat="server" CssClass="elementLabel" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <span class="elementLabel">Amount In FC : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtAmount" runat="server" CssClass="textBoxRight" Style="width: 120px"
                                    MaxLength="20" TabIndex="17" AutoPostBack="true" ToolTip="Remittance FC Amount"></asp:TextBox>
                                <span class="elementLabel">LEI Exch Rate :</span>
                                <asp:Label ID="lbl_Exch_rate" runat="server" CssClass="elementLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <span class="elementLabel">Exchange Rate : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtExchangeRate" runat="server" CssClass="textBoxRight" TabIndex="18"
                                    Style="width: 120px" AutoPostBack="true" ToolTip="Exchange rate for INR conversion"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <span class="elementLabel">Amount In INR : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtAmtInINR" runat="server" CssClass="textBoxRight" MaxLength="20"
                                    TabIndex="19" ToolTip="Equivalent INR code" Style="width: 120px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <span class="elementLabel">FIRC NO : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtFIRCNo" runat="server" CssClass="textBox" MaxLength="15" TabIndex="20"
                                    Style="width: 120px"></asp:TextBox>
                            </td>
                            <td style="text-align: right">
                                <span class="elementLabel">FIRC Date : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtFIRCDate" runat="server" CssClass="textBox" Style="width: 70px"
                                    TabIndex="21"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="meFircDate" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtFIRCDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="Button2" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtFIRCDate" PopupButtonID="Button2" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meFircDate"
                                    ValidationGroup="dtVal" ControlToValidate="txtFIRCDate" EmptyValueMessage="Enter Date Value"
                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                                <span class="elementLabel">FIRC AD Code : </span>
                                <asp:TextBox ID="txtADCode" runat="server" CssClass="textBox" Style="width: 55px"
                                    MaxLength="7" TabIndex="22"></asp:TextBox>
                            </td>
                        </tr>
                          <%-- ------------------- create by Anand15-06-2023---------------------------------------------%>
                        <tr>
                        <td style="text-align: right">
                                <span class="elementLabel">Bank Unique Transaction ID : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtBankUniqueTransactionID" MaxLength="25" Enabled="false" runat="server" CssClass="textBoxRight" TabIndex="18"
                                    Style="width: 170px"  AutoPostBack="true" ToolTip="Technical unique transaction ID for each request."></asp:TextBox>
                            </td>
                            <td style="text-align: right">
                                <span class="elementLabel">IFSC Code : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtIFSCCode" MaxLength="11" Enabled="false" runat="server" CssClass="textBoxRight" TabIndex="18"
                                    Style="width: 120px" AutoPostBack="true" ToolTip="IFSC Code of the bank"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                        <td style="text-align: right">
                                <span class="elementLabel">Remittance AD Code : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtRemittanceADCode" runat="server" Enabled="false" MaxLength="50" CssClass="textBoxRight" TabIndex="18"
                                    Style="width: 120px" AutoPostBack="true" ToolTip="Remittance Bank AD Code"></asp:TextBox>
                            </td>
                            <td style="text-align: right">
                                <span class="elementLabel">IEC Code : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtIECCode" runat="server" Enabled="false" CssClass="textBoxRight" MaxLength="10" TabIndex="18"
                                    Style="width: 120px" AutoPostBack="true" ToolTip="IEC Code"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                        <td style="text-align: right">
                                <span class="elementLabel">Pan Number : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPanNumber" runat="server" Enabled="false" CssClass="textBoxRight" MaxLength="10" TabIndex="18"
                                    Style="width: 120px" AutoPostBack="true" ToolTip="Pan Number"></asp:TextBox>
                            </td>
                            <td style="text-align: right">
                                <span class="elementLabel">Mode of Payment : </span>
                            </td>
                            <td align="left">
                               <%-- <asp:TextBox ID="txtModeofPayment" runat="server" Enabled="false" MaxLength="50" CssClass="textBoxRight" TabIndex="18"
                                    Style="width: 180px" AutoPostBack="true" ToolTip="Mode of payment (e.g.SWIFT or any mechanism,NEFT, RTGS etc through which payment is received)"></asp:TextBox>--%>
                                     <asp:DropDownList ID="ddlModeOfPayment" runat="server" CssClass="dropdownList"
                                     TabIndex="32" Height="19px" Enabled="false" Style="width: 130px" ToolTip="Mode of payment (e.g.SWIFT or any mechanism,NEFT, RTGS etc through which payment is received)">
                                    <asp:ListItem Value=""> </asp:ListItem>  
                                        <asp:ListItem>SWIFT</asp:ListItem> 
                                        <asp:ListItem>RTGS</asp:ListItem>  
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                        <%--<td style="text-align: right">
                                <span class="elementLabel">Factoring flag : </span>
                            </td>
                            <td align="left">
                               
                                      <asp:DropDownList ID="ddlFactoringflag" runat="server" Enabled="false" CssClass="dropdownList"
                                     TabIndex="16" Height="19px">
                                    <asp:ListItem Value=""> </asp:ListItem>  
                                        <asp:ListItem>Y</asp:ListItem>  
                                        <asp:ListItem>N</asp:ListItem>  
            
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: right">
                                <span class="elementLabel">Forfeiting flag : </span>
                            </td>
                            <td align="left">
                                      <asp:DropDownList ID="ddlForfeitingflag" runat="server" Enabled="false" CssClass="dropdownList"
                                     TabIndex="16" Height="19px">
                                    <asp:ListItem Value=""> </asp:ListItem>  
                                        <asp:ListItem>Y</asp:ListItem>  
                                        <asp:ListItem>N</asp:ListItem>  
            
                                </asp:DropDownList>
                            </td>--%>
                             <td style="text-align: right">
                                <span class="elementLabel">Bank Reference Number :</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtBankReferencenumber" runat="server" Enabled="false" CssClass="textBoxRight" MaxLength="20" TabIndex="18"
                                    Style="width: 140px" ToolTip="Bank reference number corresponding to IRM message."></asp:TextBox>
                            </td>
                              <td style="text-align: right">
                               <span class="mandatoryField">*</span><span class="elementLabel">Bank Account Number : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtBankAccountNumber" runat="server" Enabled="false" CssClass="textBoxRight" MaxLength="25" TabIndex="19"
                                    Style="width: 180px" ToolTip="Bank account number"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                        <td style="text-align: right">
                                <span class="mandatoryField">*</span><span class="elementLabel">IRM Status : </span>
                            </td>
                            <td align="left">       
                                    <asp:DropDownList ID="ddlIRMStatus" runat="server" Enabled="false" CssClass="dropdownList"
                                     TabIndex="16" Height="19px" ToolTip="IRM Status(F – Fresh,A -Amended,C – Cancelled)">
                                    <asp:ListItem Value=""> </asp:ListItem>  
                                        <asp:ListItem>Fresh</asp:ListItem> 
                                        <%--<asp:ListItem>Amended</asp:ListItem>  --%>
                                        <asp:ListItem>Cancelled</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <%------------------------------End----------------------------------------------------%>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td align="left" colspan="3">
                                <asp:Button ID="btnLEI" runat="server" Text="Verify LEI" CssClass="buttonDefault"
                                    ToolTip="Verify LEI" TabIndex="132" OnClick="btnLEI_Click" />
                                &nbsp;                            
                            <asp:Button ID="btnPrint" runat="server" CssClass="buttonDefault" Text="Print"
                                OnClick="btnPrint_Click" TabIndex="24" />
                                &nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="buttonDefault" Text="Cancel"
                                OnClick="btnCancel_Click" TabIndex="24" />
                                <asp:Button ID="btnSave" Style="visibility: hidden" runat="server" Text="Save" CssClass="buttonDefault"
                                   ToolTip="Save" OnClick="btnSave_Click" TabIndex="24" />
                            
                            </td>
                        </tr>
                        <tr style="height: 35px">
                            <td align="right">
                                <span class="elementLabel">Approve / Reject :</span>
                            </td>
                            <td align="left" colspan="3" width="90%">
                                <asp:DropDownList ID="ddlApproveReject" runat="server" CssClass="dropdownList" TabIndex="35">
                                    <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Approve"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Reject"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                         <tr>
                            <td align="center" colspan="4">
                                <asp:Label ID="lblChecker_Remark" runat="server" CssClass="mandatoryField" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
     <script language="javascript" type="text/javascript">
         window.onload = function () {
             var txtAmount = document.getElementById('txtAmount');
             var txtExchangeRate = document.getElementById('txtExchangeRate');
             var txtAmtInINR = document.getElementById('txtAmtInINR');

             if (txtAmount.value == '')
                 txtAmount.value = 0;
             txtAmount.value = parseFloat(txtAmount.value).toFixed(2);

             if (txtExchangeRate.value == '')
                 txtExchangeRate.value = 0;
             txtExchangeRate.value = parseFloat(txtExchangeRate.value).toFixed(5);

             if (txtAmtInINR.value == '')
                 txtAmtInINR.value = 0;
             txtAmtInINR.value = parseFloat(txtAmtInINR.value).toFixed(2);
         } //function

         function ExecuteCSharpCodeLodg() {
             var DocPref = document.getElementById('txtDocPref').value;
             var DocBRCode = document.getElementById('txtDocBRCode').value;
             var txtDocNo = document.getElementById('txtDocNo').value;
             var txtDocSrNo = document.getElementById('txtDocSrNo').value;
             var docNo = DocPref + '/' + DocBRCode + '/' + txtDocNo + txtDocSrNo;
             var checker = document.getElementById('hdnUserName').value;

             $.ajax({
                 type: 'POST',
                 url: 'EDPMS_INW_File_DataEntry_Checker.aspx/ExecuteCSharpCodeLodg',
                 data: JSON.stringify({ docNo: docNo, checker: checker }),
                 contentType: 'application/json; charset=utf-8',
                 dataType: 'json',
                 success: function (data) {
                 },
                 error: function (error) {
                 }
             });
         }

     </script>
</body>
</html>
