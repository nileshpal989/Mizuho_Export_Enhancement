<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_ReversalOfLiability_Maker.aspx.cs" Inherits="IMP_Transactions_TF_IMP_ReversalOfLiability_Maker" %>

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
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <link href="../../Style/Style_V2.css" rel="Stylesheet" type="text/css" media="screen" />
    <link href="../../Style/TAB.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Scripts/TF_IMP_ReversalOfLiability_Maker.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-1.8.3.min.js" type="text/javascript""></script>
    <script id="Save_script" language="javascript" type="text/javascript">
        $(document).ready(function (e) {
            // Configure to save every 5 seconds  
            window.setInterval(SaveUpdateData, 5000); //calling saveDraft function for every 5 seconds
            OnInputKeyPress();
        });

    </script>
    <style type="text/css">
        .textBox {
            text-transform: uppercase;
        }

        .tdpayment1 {
            width: 20%;
        }
    </style>
</head>
<body onload="EndRequest();closeWindows();" onunload="closeWindows();">
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManagerMain" runat="server">
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
                <br />
                <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnBack" />
                    </Triggers>
                    <ContentTemplate>
                        <table cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="left" style="width: 80%" valign="bottom">
                                    <span class="pageLabel"><strong>&nbsp; Reversal Of Liability - Maker</strong></span>
                                    <asp:Label ID="lblRejectReson" runat="server" CssClass="mandatoryField" Visible="false"></asp:Label>
                                </td>
                                <td align="right" style="width: 20%">
                                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                        TabIndex="38" OnClientClick="return OnBackClick();" />
                                    <input type="hidden" runat="server" id="hdnBranchCode" />
                                    <input type="hidden" runat="server" id="hdnBranchName" />
                                    <input type="hidden" runat="server" id="hdnUserName" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100%" valign="middle" colspan="2">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 60%">
                                    <table id="tbl_Acceptance" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td align="right"><span class="elementLabel">Reference No :</span>&nbsp;</td>
                                            <td align="left">
                                                <asp:TextBox ID="txtRefNo" runat="server" CssClass="textBox" MaxLength="14" TabIndex="1" Width="110px"
                                                     Enabled="false"></asp:TextBox>
                                            </td>
                                            <td align="right"><span class="elementLabel">Amount / Currency :</span>&nbsp;</td>
                                            <td align="left">
                                                <asp:TextBox ID="txtAmount" runat="server" CssClass="textBox" TabIndex="2" Style="text-align: right" onkeyDown="return validate_Number(event);" MaxLength="16"></asp:TextBox>
                                                <asp:TextBox ID="txtCurrency" runat="server" CssClass="textBox" MaxLength="3" TabIndex="3" Width="35px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right"><span class="elementLabel">Customer :</span>&nbsp;</td>
                                            <td align="left">
                                                <asp:TextBox ID="txtCustomerAcNo" runat="server" CssClass="textBox" MaxLength="12" TabIndex="4" Width="100px"
                                                    AutoPostBack="true" OnTextChanged="txtCustomerAcNo_TextChanged"></asp:TextBox>
                                                <asp:Label ID="lblCustomerDesc" runat="server" CssClass="elementLabel"></asp:Label>
                                            </td>
                                            <td align="right"><span class="elementLabel">C. Code :</span>&nbsp;</td>
                                            <td align="left">
                                                <asp:TextBox ID="txtCCode" runat="server" CssClass="textBox" MaxLength="16" TabIndex="5" Width="100px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right"><span class="elementLabel">Value Date :</span>&nbsp;</td>
                                            <td align="left">
                                                <asp:TextBox ID="txtValueDate" runat="server" TabIndex="6" CssClass="textBox" MaxLength="10"
                                                    ValidationGroup="dtVal" Width="80px" Enabled="false" ></asp:TextBox>
                                                <ajaxToolkit:MaskedEditExtender ID="ME_DDate" Mask="99/99/9999" MaskType="Date"
                                                    runat="server" TargetControlID="txtValueDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                    CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                    CultureTimePlaceholder=":">
                                                </ajaxToolkit:MaskedEditExtender>
                                                <asp:Button ID="btnValue_Date" runat="server" CssClass="btncalendar_enabled"  Enabled="false"/>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtValueDate" PopupButtonID="btnValue_Date" Enabled="True">
                                                </ajaxToolkit:CalendarExtender>
                                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="ME_DDate"
                                                    ValidationGroup="dtVal" ControlToValidate="txtValueDate" EmptyValueMessage="Enter Date Value"
                                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                    Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                            </td>
                                            <td align="right"><span class="elementLabel">Expiry Date :</span>&nbsp;</td>
                                            <td align="left">
                                                <asp:TextBox ID="txtExpiryDate" runat="server" TabIndex="7" CssClass="textBox" MaxLength="10"
                                                    ValidationGroup="dtVal" Width="80px"></asp:TextBox>
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                                    runat="server" TargetControlID="txtExpiryDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                    CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                    CultureTimePlaceholder=":">
                                                </ajaxToolkit:MaskedEditExtender>
                                                <asp:Button ID="btnExpiry_date" runat="server" CssClass="btncalendar_enabled" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtExpiryDate" PopupButtonID="btnExpiry_date" Enabled="True">
                                                </ajaxToolkit:CalendarExtender>
                                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender1"
                                                    ValidationGroup="dtVal" ControlToValidate="txtExpiryDate" EmptyValueMessage="Enter Date Value"
                                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                    Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right"><span class="elementLabel">Amendment Option :</span>&nbsp;</td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlAmendmentOption" CssClass="dropdownList" runat="server" TabIndex="8">
                                                    <asp:ListItem Text="4 - Cancel" Value="4"></asp:ListItem>
                                                </asp:DropDownList>
                                                <td align="right"><span class="elementLabel">Amount / Currency :</span>&nbsp;</td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtAAmount" runat="server" CssClass="textBox" MaxLength="16" TabIndex="9" Width="90px"></asp:TextBox>
                                                    <asp:TextBox ID="txtACurrency" runat="server" CssClass="textBox" MaxLength="3" TabIndex="10" Width="35px"></asp:TextBox>
                                                </td>
                                        </tr>
                                        <tr>
                                            <td align="right"><span class="elementLabel">Remarks :</span>&nbsp;</td>
                                            <td align="left" colspan="3">
                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="textBox" MaxLength="100" TabIndex="11" Width="250px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right"><span class="elementLabel">Application No :</span>&nbsp;</td>
                                            <td align="left">
                                                <asp:TextBox ID="txtApplicationNo" runat="server" CssClass="textBox" MaxLength="16" TabIndex="12" Width="60px"></asp:TextBox></td>
                                            <td align="right"><span class="elementLabel">Applying Branch :</span>&nbsp;</td>
                                            <td align="left">
                                                <asp:TextBox ID="txtApplyingBranch" runat="server" CssClass="textBox" MaxLength="16" TabIndex="13" Width="150px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right"><span class="elementLabel">Advising Bank :</span>&nbsp;</td>
                                            <td align="left">
                                                <asp:TextBox ID="txtAdvisingBank" runat="server" CssClass="textBox" MaxLength="16" TabIndex="14" Width="150px"></asp:TextBox></td>
                                            <td align="right"><span class="elementLabel">Attention Code :</span>&nbsp;</td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlAttentionCode" runat="server" CssClass="dropdownList" TabIndex="15">
                                                    <asp:ListItem Text="0 - No Attention" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="1 - Attention" Value="1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <table cellspacing="0" border="0" width="100%" style="padding-top: 3%; padding-left: 10%;">
                                        <tr>
                                            <td align="center" colspan="2"><span class="elementLabel">Creditor Details :</span>&nbsp;</td>
                                            <td align="right"><span class="elementLabel">CCY. :</span>&nbsp;</td>
                                            <td align="left">
                                                <span class="elementLabel">Amount :</span>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right"><span class="elementLabel">1</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                            <td align="left"><span class="elementLabel">Amendment Charge (F.C.) :</span>&nbsp;</td>
                                            <td align="right">
                                                <asp:TextBox ID="txtCDCCY1" runat="server" CssClass="textBox" MaxLength="3" TabIndex="16" Width="35px"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtCDAmount1" runat="server" CssClass="textBox" MaxLength="16" TabIndex="17" Width="90px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right"><span class="elementLabel">2</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                            <td align="left"><span class="elementLabel">Amendment Charge (H.C.) :</span>&nbsp;</td>
                                            <td align="right">
                                                <asp:TextBox ID="txtCDCCY2" runat="server" CssClass="textBox" MaxLength="3" TabIndex="18" Width="35px"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtCDAmount2" runat="server" CssClass="textBox" MaxLength="16" TabIndex="19" Width="90px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right"><span class="elementLabel">3</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                            <td align="left"><span class="elementLabel">Cable / Mail Charge :</span>&nbsp;</td>
                                            <td align="right">
                                                <asp:TextBox ID="txtCDCCY3" runat="server" CssClass="textBox" MaxLength="3" TabIndex="20" Width="35px"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtCDAmount3" runat="server" CssClass="textBox" MaxLength="16" TabIndex="21" Width="90px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table cellspacing="0" border="0" width="100%" style="padding-top: 3%; padding-left: 10%;">
                                        <tr>
                                            <td align="center"><span class="elementLabel">Debtor Code</span></td>
                                            <td align="center"><span class="elementLabel">A/C Short Name</span></td>
                                            <td align="center"><span class="elementLabel">Cust Abbr.</span></td>
                                            <td align="center"><span class="elementLabel">A/C Number</span></td>
                                            <td align="center"><span class="elementLabel">CCY.</span></td>
                                            <td align="center"><span class="elementLabel">Amount</span></td>
                                        </tr>
                                        <tr>
                                            <td align="center"><span class="elementLabel">1</span></td>
                                            <td align="center">
                                                <asp:TextBox ID="txtDCACShortName1" runat="server" CssClass="textBox" MaxLength="20" TabIndex="22" Width="90px"></asp:TextBox></td>
                                            <td align="center">
                                                <asp:TextBox ID="txtDCCustAbbr1" runat="server" CssClass="textBox" MaxLength="12" TabIndex="23" Width="90px"></asp:TextBox></td>
                                            <td align="center">
                                                <asp:TextBox ID="txtDCAccountNo1" runat="server" CssClass="textBox" MaxLength="20" TabIndex="24" Width="90px"></asp:TextBox></td>
                                            <td align="center">
                                                <asp:TextBox ID="txtDCCCY1" runat="server" CssClass="textBox" MaxLength="3" TabIndex="25" Width="35px"></asp:TextBox></td>
                                            <td align="center">
                                                <asp:TextBox ID="txtDCAmount1" runat="server" CssClass="textBox" MaxLength="16" TabIndex="26" Width="90px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td align="center"><span class="elementLabel">2</span></td>
                                            <td align="center">
                                                <asp:TextBox ID="txtDCACShortName2" runat="server" CssClass="textBox" MaxLength="20" TabIndex="27" Width="90px"></asp:TextBox></td>
                                            <td align="center">
                                                <asp:TextBox ID="txtDCCustAbbr2" runat="server" CssClass="textBox" MaxLength="12" TabIndex="28" Width="90px"></asp:TextBox></td>
                                            <td align="center">
                                                <asp:TextBox ID="txtDCAccountNo2" runat="server" CssClass="textBox" MaxLength="20" TabIndex="29" Width="90px"></asp:TextBox></td>
                                            <td align="center">
                                                <asp:TextBox ID="txtDCCCY2" runat="server" CssClass="textBox" MaxLength="3" TabIndex="30" Width="35px"></asp:TextBox></td>
                                            <td align="center">
                                                <asp:TextBox ID="txtDCAmount2" runat="server" CssClass="textBox" MaxLength="16" TabIndex="31" Width="90px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td align="center"><span class="elementLabel">3</span></td>
                                            <td align="center">
                                                <asp:TextBox ID="txtDCACShortName3" runat="server" CssClass="textBox" MaxLength="20" TabIndex="32" Width="90px"></asp:TextBox></td>
                                            <td align="center">
                                                <asp:TextBox ID="txtDCCustAbbr3" runat="server" CssClass="textBox" MaxLength="12" TabIndex="33" Width="90px"></asp:TextBox></td>
                                            <td align="center">
                                                <asp:TextBox ID="txtDCAccountNo3" runat="server" CssClass="textBox" MaxLength="20" TabIndex="34" Width="90px"></asp:TextBox></td>
                                            <td align="center">
                                                <asp:TextBox ID="txtDCCCY3" runat="server" CssClass="textBox" MaxLength="3" TabIndex="35" Width="35px"></asp:TextBox></td>
                                            <td align="center">
                                                <asp:TextBox ID="txtDCAmount3" runat="server" CssClass="textBox" MaxLength="16" TabIndex="36" Width="90px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="left" style="width: 40%"></td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ToolTip="Send To Checker" CssClass="buttonDefault" TabIndex="37"
                                        OnClientClick="return SubmitValidation();" />
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
