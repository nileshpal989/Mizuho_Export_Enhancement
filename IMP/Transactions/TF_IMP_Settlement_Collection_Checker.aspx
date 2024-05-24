<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_Settlement_Collection_Checker.aspx.cs"
    Inherits="IMP_Transactions_TF_IMP_Settlement_Collection_Checker" %>

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
    <script src="../Scripts/TF_IMP_Settlement_Collection_Checker.js" type="text/javascript"></script>
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
                        <td align="left" style="width: 50%" valign="bottom">
                            <span class="pageLabel"><strong>Import Bill Settlement Collection - Checker</strong></span>
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
                        <td align="left" style="width: 100%;" valign="top" colspan="2">
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            <%-------------------------  hidden fields  --------------------------------%>
                            <input type="hidden" id="hdnDocNo" runat="server" />
                            <input type="hidden" id="hdnBranchName" runat="server" />
                            <input type="hidden" id="hdnDocType" runat="server" />
                            <input type="hidden" id="hdnDoc_Scrutiny" runat="server" />
                            <input type="hidden" id="hdnflcIlcType" runat="server" />
                            <input type="hidden" id="hdnMode" runat="server" />
                            <input type="hidden" id="hdnNegoRemiBankType" runat="server" />
                            <input type="hidden" id="hdnRejectReason" runat="server" />
                            <input type="hidden" id="Settlement_For_Cust_AccNo" runat="server" />
                            <input type="hidden" id="Settlement_For_Cust_Abbr" runat="server" />
                            <input type="hidden" id="Settlement_For_Cust_AccCode" runat="server" />
                            <input type="hidden" id="Settl_For_Bank_Abbr" runat="server" />
                            <input type="hidden" id="Settl_ForBank_AccNo" runat="server" />
                            <input type="hidden" id="Settl_ForBank_AccCode" runat="server" />
                            <input type="hidden" id="hdnLedgerStatusCode" runat="server" />
                            <%-------Added by Bhupen for LEI on 05122022----------------%>
                            <input type="hidden" id="hdnUserName" runat="server" />
                            <input type="hidden" id="hdnleino" runat="server" />
                            <input type="hidden" id="hdnCustAbbr" runat="server" />
                            <input type="hidden" id="hdnleiexpiry" runat="server" />
                            <input type="hidden" id="hdnDraweeleino" runat="server" />
                            <input type="hidden" id="hdnDraweeleiexpiry" runat="server" />
                            <input type="hidden" id="hdnLeiFlag" runat="server" />
                            <input type="hidden" id="hdnleiexpiry1" runat="server" />
                            <input type="hidden" id="hdnDraweeleiexpiry1" runat="server" />
                            <input type="hidden" id="hdnDrawer" runat="server" />
                            <input type="hidden" id="hdnDrawerno" runat="server" />
                            <input type="hidden" id="hdnBranchCode" runat="server" />
                            <input type="hidden" id="hdnDuedate_Collection" runat="server" />
                            <input type="hidden" id="hdncustleiflag" runat="server" />
                            <input type="hidden" id="hdnDrawercountry" runat="server" />
                        </td>
                    </tr>
                </table>
                <table id="tbl_Settlement_Collection" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="right" width="10%">
                            <span class="elementLabel">Trans. Ref. No :</span>
                        </td>
                        <td width="30%" align="left">
                            <asp:TextBox ID="txtDocNo" Width="100px" runat="server" CssClass="textBox" TabIndex="1"
                                Enabled="false"></asp:TextBox>
                            <asp:Label ID="lblScrutiny" runat="server" CssClass="elementLabel"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblForeign_Local" runat="server" CssClass="elementLabel"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblCollection_Lodgment_UnderLC" runat="server" CssClass="elementLabel"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblSight_Usance" runat="server" CssClass="elementLabel"></asp:Label>
                        </td>
                        <td width="40%" align="left">
                            <span class="elementLabel">Val.Date :</span>
                            <asp:TextBox ID="txtValueDate" runat="server" TabIndex="2" CssClass="textBox" MaxLength="10"
                                ValidationGroup="dtVal" Width="70px" Enabled="false"></asp:TextBox>
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
                            <%---------Added by bhupen for lei on 02122022-----------%>
                            <span class="elementLabel">Exch Rate : </span>
                            <asp:Label ID="lbl_Exch_rate" runat="server" CssClass="elementLabel"></asp:Label>
                        </td>
                        <td align="right" width="10%">
                        </td>
                        <td align="left" width="20%">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="5">
                            <ajaxToolkit:TabContainer ID="TabContainerMain" runat="server" ActiveTabIndex="0"
                                CssClass="ajax__tab_xp-theme">
                                <ajaxToolkit:TabPanel ID="tbDocumentDetailsCollection" runat="server" HeaderText="SETTLEMENT(ICU,ICA,IBA)"
                                    Font-Bold="true" ForeColor="White" Enabled="false">
                                    <ContentTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">CUSTOMER :</span>
                                                </td>
                                                <td align="left" width="90%">
                                                    <asp:TextBox ID="txt_Collection_Customer_ID" runat="server" CssClass="textBox" TabIndex="3"
                                                        MaxLength="40" Width="100px" Enabled="false"></asp:TextBox>
                                                    <asp:Label ID="lbl_Collection_Customer_Name" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">SETTLEMENT FOR CUST :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_Collection_Settlement_for_Cust" runat="server" CssClass="textBox"
                                                        MaxLength="2" TabIndex="7" Width="30px" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">SETTLEMENT FOR BANK :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_Collection_Settlement_For_Bank" runat="server" CssClass="textBox"
                                                        MaxLength="2" Width="30px" TabIndex="8" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">ATTN :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_Collection_Attn" runat="server" CssClass="textBox" TabIndex="9"
                                                        MaxLength="70" Width="300px" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                </td>
                                                <td align="left">
                                                    <asp:Button ID="btn_Collection_Next" runat="server" CssClass="buttonDefault" TabIndex="10"
                                                        Text="Next >>" ToolTip="Go to IMPORT ACCOUNTING" OnClientClick="return OnDocNextClick(2);" />
                                                    <asp:Button ID="btn_Verify_Collection" runat="server" Text="LEIVerify" CssClass="buttonDefault"
                                                        ToolTip="Click here to verify LEI Details" Visible="false" TabIndex="10" 
                                                        OnClick="btn_Verify_Collection_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel ID="tbDocumentDetailsUnderLC" runat="server" HeaderText="DOCUMENT DETAILS"
                                    Font-Bold="true" ForeColor="White" Enabled="false">
                                    <ContentTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">CUSTOMER :</span>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:TextBox ID="txt_UnderLC_Customer_ID" runat="server" CssClass="textBox" TabIndex="3"
                                                        MaxLength="40" Width="100px" Enabled="false"></asp:TextBox>
                                                    <asp:Label ID="lbl_UnderLC_Customer_Name" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">COMMENT CODE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_UnderLC_Comment_Code" runat="server" CssClass="textBox" MaxLength="2"
                                                        TabIndex="5" Width="30px" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">MATURITY :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_UnderLC_Maturity" runat="server" CssClass="textBox" MaxLength="10"
                                                        Width="70px" TabIndex="6" Enabled="false"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender8" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txt_UnderLC_Maturity" ErrorTooltipEnabled="True"
                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                        CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">SETTLEMENT FOR CUST :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_UnderLC_Settlement_for_Cust" runat="server" CssClass="textBox"
                                                        MaxLength="2" TabIndex="7" Width="30px" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">SETTLEMENT FOR BANK :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_UnderLC_Settlement_For_Bank" runat="server" CssClass="textBox"
                                                        MaxLength="2" Width="30px" TabIndex="8" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">INTEREST FROM :</span>
                                                </td>
                                                <td align="left" width="10%">
                                                    <asp:TextBox ID="txt_UnderLC_Interest_From" runat="server" CssClass="textBox" MaxLength="10"
                                                        TabIndex="9" ValidationGroup="dtVal" Width="70px" OnTextChanged="Get_Acceptance_Get_Date_Diff"
                                                        Enabled="false"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txt_UnderLC_Interest_From" ErrorTooltipEnabled="True"
                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                        CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btncal_Interest_From_Date" runat="server" CssClass="btncalendar_enabled"
                                                        Enabled="false" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txt_UnderLC_Interest_From" PopupButtonID="btncal_Interest_From_Date"
                                                        Enabled="false">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <ajaxToolkit:MaskedEditValidator ID="MV_Received_date" runat="server" ControlExtender="MaskedEditExtender4"
                                                        ValidationGroup="dtVal" ControlToValidate="txt_UnderLC_Interest_From" EmptyValueMessage="Enter Date Value"
                                                        InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                        Enabled="false"></ajaxToolkit:MaskedEditValidator>
                                                </td>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">DISCOUNT :</span>
                                                </td>
                                                <td align="left" width="70%">
                                                    <asp:TextBox ID="txt_UnderLC_Discount" runat="server" CssClass="textBox" MaxLength="1"
                                                        TabIndex="10" Width="30px" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">INTEREST TO :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_UnderLC_Interest_To" runat="server" CssClass="textBox" MaxLength="10"
                                                        TabIndex="11" ValidationGroup="dtVal" Width="70px" OnTextChanged="Get_Acceptance_Get_Date_Diff"
                                                        Enabled="false"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txt_UnderLC_Interest_To" ErrorTooltipEnabled="True"
                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                        CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btncal_Interest_To_Date" runat="server" CssClass="btncalendar_enabled"
                                                        Enabled="False" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txt_UnderLC_Interest_To" PopupButtonID="btncal_Interest_To_Date"
                                                        Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender5"
                                                        ValidationGroup="dtVal" ControlToValidate="txt_UnderLC_Interest_To" EmptyValueMessage="Enter Date Value"
                                                        InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                        Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">NOS OF DAYS :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_UnderLC_No_Of_Days" runat="server" CssClass="textBox" MaxLength="4"
                                                        Width="30px" TabIndex="12" Style="text-align: right" onkeyDown="return validate_Number(event);"
                                                        Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">RATE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_UnderLC_Rate" runat="server" CssClass="textBox" MaxLength="16"
                                                        TabIndex="13" Width="100px" Style="text-align: right" onkeyDown="return validate_Number(event);"
                                                        Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">AMOUNT :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_UnderLC_Amount" runat="server" CssClass="textBox" MaxLength="16"
                                                        TabIndex="14" Width="100px" Style="text-align: right" onkeyDown="return validate_Number(event);"
                                                        Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">OVERDUE :</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">RATE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_UnderLC_Overdue_Interestrate" runat="server" CssClass="textBox"
                                                        MaxLength="10" TabIndex="15" Width="70px" Style="text-align: right" onkeyDown="return validate_Number(event);"
                                                        Enabled="false"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">NOS OF DAYS :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_UnderLC_Oveduenoofdays" runat="server" CssClass="textBox" MaxLength="4"
                                                        Width="30px" TabIndex="16" Style="text-align: right" onkeyDown="return validate_Number(event);"
                                                        Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">AMOUNT :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_UnderLC_Overdueamount" runat="server" CssClass="textBox" MaxLength="16"
                                                        TabIndex="17" Width="100px" Style="text-align: right" onkeyDown="return validate_Number(event);"
                                                        Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">ATTN :</span>
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txt_UnderLC_Attn" runat="server" CssClass="textBox" TabIndex="18"
                                                        MaxLength="70" Width="190px" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                </td>
                                                <td>
                                                </td>
                                                <td colspan="3">
                                                    <asp:Button ID="btn_UnderLC_Next" runat="server" CssClass="buttonDefault" TabIndex="19"
                                                        Text="Next >>" ToolTip="Go to IMPORT ACCOUNTING" OnClientClick="return OnDocNextClick(2);" />
                                                    <asp:Button ID="btn_Verify_UnderLC" runat="server" Text="LEIVerify" CssClass="buttonDefault"
                                                        ToolTip="Click here to verify LEI Details" Visible="false" TabIndex="18" 
                                                        OnClick="btn_Verify_UnderLC_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel ID="tbDocumentAccounting" runat="server" HeaderText="IMPORT ACCOUNTING"
                                    Font-Bold="true" ForeColor="White">
                                    <ContentTemplate>
                                        <ajaxToolkit:TabContainer ID="TabSubContainerACC" runat="server" ActiveTabIndex="0"
                                            CssClass="ajax__subtab_xp-theme">
                                            <ajaxToolkit:TabPanel ID="TabPanelACC1" runat="server" HeaderText="IMPORT ACCOUNTING I"
                                                Font-Bold="true" ForeColor="White">
                                                <ContentTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" width="50%">
                                                                <asp:CheckBox Enabled="false" ID="chk_IMPACC1Flag" Text="IMPORT ACCOUNTING I" runat="server"
                                                                    CssClass="elementLabel" OnCheckedChanged="chk_IMPACC1Flag_OnCheckedChanged" />
                                                            </td>
                                                            <td align="center" width="50%">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%">
                                                        <asp:Panel ID="PanelIMPACC1" runat="server" Visible="false">
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td align="left">
                                                                    <span class="elementLabel">1-DISC </span>
                                                                </td>
                                                                <td colspan="3">
                                                                    <span class="elementLabel">FC Ref No:</span>
                                                                    <asp:TextBox ID="txt_IMPACC1_FCRefNo" runat="server" CssClass="textBox" MaxLength="14"
                                                                        TabIndex="20" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <span class="elementLabel">AMOUNT :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_DiscAmt" runat="server" onkeydown="return validate_Number(event);"
                                                                        CssClass="textBox" TabIndex="20" MaxLength="16" Width="100px" Style="text-align: right"
                                                                        Enabled="false"></asp:TextBox>
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
                                                                    <asp:TextBox ID="txt_IMPACC1_Princ_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="20" onkeydown="return validate_Commission_MATU_Code(event);" Width="40px"
                                                                        Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Princ_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="21" onkeydown="return validate_Commission_LUMP_Code(event);" Width="40px"
                                                                        Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Princ_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="22" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Princ_Ex_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                        TabIndex="23" Width="40px" onkeyup="return DR_Curr_Toggel();" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Princ_Ex_rate" runat="server" CssClass="textBox" MaxLength="16"
                                                                        Style="text-align: right" TabIndex="24" onkeydown="return validate_Number(event);"
                                                                        onchange="return DR_Amt_Calculation();" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Princ_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" TabIndex="25" onkeydown="return validate_Number(event);"
                                                                        Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">INTEREST :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Interest_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                                        onkeydown="return validate_Commission_MATU_Code(event);" TabIndex="26" Width="40px"
                                                                        Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Interest_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                                        onkeydown="return validate_Commission_LUMP_Code(event);" TabIndex="27" Width="40px"
                                                                        Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Interest_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="28" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Interest_Ex_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="29" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Interest_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                        TabIndex="30" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Interest_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                        TabIndex="31" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">COMMISSION :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Commission_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                                        onkeydown="return validate_Commission_MATU_Code(event);" TabIndex="32" Width="40px"
                                                                        Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Commission_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                                        onkeydown="return validate_Commission_LUMP_Code(event);" TabIndex="33" Width="40px"
                                                                        Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Commission_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="34" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Commission_Ex_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="35" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Commission_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                        TabIndex="36" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Commission_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                        TabIndex="37" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">THEIR COMMISSION :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Their_Commission_matu" runat="server" CssClass="textBox"
                                                                        MaxLength="1" onkeydown="return validate_Commission_MATU_Code(event);" TabIndex="38"
                                                                        Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Their_Commission_lump" runat="server" CssClass="textBox"
                                                                        MaxLength="1" onkeydown="return validate_Commission_LUMP_Code(event);" TabIndex="39"
                                                                        Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Their_Commission_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="40" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Their_Commission_Ex_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="41" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Their_Commission_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                        TabIndex="42" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC1_Their_Commission_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                        TabIndex="43" Width="100px" Enabled="false"></asp:TextBox>
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
                                                                            <td align="left" width="10%">
                                                                                <span class="elementLabel"><b>/CR/</b> CODE :</span> &nbsp;<asp:Button ID="btn_IMPACC1_CR_Code_help"
                                                                                    runat="server" ToolTip="Press for AcCode list." CssClass="btnHelp_enabled" Enabled="false" />
                                                                            </td>
                                                                            <td align="left" width="25%">
                                                                                <span class="elementLabel">A/C SHORT NAME</span>
                                                                            </td>
                                                                            <td align="center" width="10%">
                                                                                <span class="elementLabel">CUST ABBR</span>
                                                                            </td>
                                                                            <td align="center" width="10%">
                                                                                <span class="elementLabel">A/C NUMBER</span>
                                                                            </td>
                                                                            <td align="center" width="10%">
                                                                                <span class="elementLabel">CCY</span>
                                                                            </td>
                                                                            <td align="left" width="20%">
                                                                                <span class="elementLabel">AMOUNT</span>
                                                                            </td>
                                                                            <td align="left" width="5%">
                                                                                <span class="elementLabel">PAYER</span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Code" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="44" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_AC_Short_Name" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="44" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Cust_abbr" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="45" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="46" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Acceptance_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="47" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Acceptance_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" onkeydown="return validate_Number(event);" Style="text-align: right"
                                                                                    TabIndex="48" Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Acceptance_payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="49" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">INTEREST</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Interest_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="50" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Interest_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="51"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Interest_payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="52" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">ACCEPTANCE COMMISSION</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Accept_Commission_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="53" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Accept_Commission_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" onkeydown="return validate_Number(event);" Style="text-align: right"
                                                                                    TabIndex="54" Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Accept_Commission_Payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="55" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">PAYMENT/HANDLING COMMISSION</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Pay_Handle_Commission_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="56" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Pay_Handle_Commission_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" onkeydown="return validate_Number(event);" Style="text-align: right"
                                                                                    TabIndex="57" Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Pay_Handle_Commission_Payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="58" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">OTHERS</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Others_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                                    TabIndex="59" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Others_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="60"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Others_Payer" runat="server" CssClass="textBox" MaxLength="1"
                                                                                    TabIndex="61" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">THEIR COMMISSION</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Their_Commission_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="62" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Their_Commission_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" onkeydown="return validate_Number(event);" Style="text-align: right"
                                                                                    TabIndex="63" Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_CR_Their_Commission_Payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="64" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel"><b>/DR/</b> CODE:</span> &nbsp;<asp:Button ID="btn_IMPACC1_DR_Code_help"
                                                                                    runat="server" ToolTip="Press for AcCode list." CssClass="btnHelp_enabled" Enabled="false" />
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
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Code" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="65" Width="90px" onblur="return Toggel_DR_Code();" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_AC_Short_Name" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="65" Width="150px" onblur="return Toggel_DR_Cust_Name();"
                                                                                    Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cust_abbr" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="66" Width="90px" onblur="return Toggel_DR_Cust_Abbr();" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="67" Width="150px" onblur="return Toggel_DR_Cust_Acc();" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                                    TabIndex="68" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="69"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="70" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Code2" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="71" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_AC_Short_Name2" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="71" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cust_abbr2" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="71" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cust_Acc2" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="71" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_Curr2" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="71" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_amt2" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="72"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_payer2" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="73" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Code3" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="74" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_AC_Short_Name3" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="74" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cust_abbr3" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="74" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cust_Acc3" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="74" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_Curr3" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="74" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_amt3" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="74"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_payer3" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="74" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Code4" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="75" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_AC_Short_Name4" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="75" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cust_abbr4" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="75" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cust_Acc4" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="75" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_Curr4" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="75" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_amt4" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="75"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_payer4" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="75" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Code5" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="76" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_AC_Short_Name5" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="76" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cust_abbr5" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="76" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cust_Acc5" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="76" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_Curr5" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="76" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_amt5" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="76"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_payer5" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="76" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Code6" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="77" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_AC_Short_Name6" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="77" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cust_abbr6" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="77" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cust_Acc6" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="77" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_Curr6" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="77" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_amt6" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="77"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC1_DR_Cur_Acc_payer6" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="77" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </asp:Panel>
                                                    </table>
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="10%">
                                                            </td>
                                                            <td align="left" width="90%">
                                                                <asp:Button ID="btnImpAcc1_Prev" runat="server" CssClass="buttonDefault" TabIndex="77"
                                                                    Text="<< Prev" ToolTip="Back to Document Details" OnClientClick="return ImportAccountingPrevClick();" />
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btnImpAcc1_Next" runat="server" CssClass="buttonDefault" TabIndex="78"
                                                                    Text="Next >>" ToolTip="Go to IMPORT ACCOUNTING II" OnClientClick="return ImportAccountingNextClick(1);" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                            <ajaxToolkit:TabPanel ID="TabPanelACC2" runat="server" HeaderText="IMPORT ACCOUNTING II"
                                                Font-Bold="true" ForeColor="White">
                                                <ContentTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" width="50%">
                                                                <asp:CheckBox Enabled="false" ID="chk_IMPACC2Flag" Text="IMPORT ACCOUNTING II" runat="server"
                                                                    CssClass="elementLabel" OnCheckedChanged="chk_IMPACC2Flag_OnCheckedChanged" />
                                                            </td>
                                                            <td align="center" width="50%">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%">
                                                        <asp:Panel ID="PanelIMPACC2" runat="server" Visible="false">
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td align="left">
                                                                    <span class="elementLabel">1-DISC </span>
                                                                </td>
                                                                <td colspan="3">
                                                                    <span class="elementLabel">FC Ref No:</span>
                                                                    <asp:TextBox ID="txt_IMPACC2_FCRefNo" runat="server" CssClass="textBox" MaxLength="14"
                                                                        TabIndex="20" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <span class="elementLabel">AMOUNT :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_DiscAmt" runat="server" onchange="return DR_Amt_Calculation();"
                                                                        onkeydown="return validate_Number(event);" CssClass="textBox" TabIndex="20" MaxLength="16"
                                                                        Width="100px" Style="text-align: right" Enabled="false"></asp:TextBox>
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
                                                                    <asp:TextBox ID="txt_IMPACC2_Princ_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="20" onkeydown="return validate_Commission_MATU_Code(event);" Width="40px"
                                                                        Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Princ_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="21" onkeydown="return validate_Commission_LUMP_Code(event);" Width="40px"
                                                                        Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Princ_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="22" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Princ_Ex_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                        TabIndex="23" Width="40px" onkeyup="return DR_Curr_Toggel();" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Princ_Ex_rate" runat="server" CssClass="textBox" MaxLength="16"
                                                                        Style="text-align: right" TabIndex="24" onkeydown="return validate_Number(event);"
                                                                        onchange="return DR_Amt_Calculation();" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Princ_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" TabIndex="25" onkeydown="return validate_Number(event);"
                                                                        Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">INTEREST :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Interest_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                                        onkeydown="return validate_Commission_MATU_Code(event);" TabIndex="26" Width="40px"
                                                                        Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Interest_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                                        onkeydown="return validate_Commission_LUMP_Code(event);" TabIndex="27" Width="40px"
                                                                        Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Interest_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="28" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Interest_Ex_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="29" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Interest_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                        TabIndex="30" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Interest_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                        TabIndex="31" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">COMMISSION :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Commission_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                                        onkeydown="return validate_Commission_MATU_Code(event);" TabIndex="32" Width="40px"
                                                                        Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Commission_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                                        onkeydown="return validate_Commission_LUMP_Code(event);" TabIndex="33" Width="40px"
                                                                        Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Commission_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="34" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Commission_Ex_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="35" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Commission_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                        TabIndex="36" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Commission_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                        TabIndex="37" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">THEIR COMMISSION :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Their_Commission_matu" runat="server" CssClass="textBox"
                                                                        MaxLength="1" onkeydown="return validate_Commission_MATU_Code(event);" TabIndex="38"
                                                                        Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Their_Commission_lump" runat="server" CssClass="textBox"
                                                                        MaxLength="1" onkeydown="return validate_Commission_LUMP_Code(event);" TabIndex="39"
                                                                        Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Their_Commission_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="40" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Their_Commission_Ex_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="41" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Their_Commission_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                        TabIndex="42" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC2_Their_Commission_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                        TabIndex="43" Width="100px" Enabled="false"></asp:TextBox>
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
                                                                            <td align="left" width="10%">
                                                                                <span class="elementLabel"><b>/CR/</b> CODE :</span> &nbsp;<asp:Button ID="btn_IMPACC2_CR_Code_help"
                                                                                    runat="server" ToolTip="Press for AcCode list." CssClass="btnHelp_enabled" Enabled="false" />
                                                                            </td>
                                                                            <td align="left" width="25%">
                                                                                <span class="elementLabel">A/C SHORT NAME</span>
                                                                            </td>
                                                                            <td align="center" width="10%">
                                                                                <span class="elementLabel">CUST ABBR</span>
                                                                            </td>
                                                                            <td align="center" width="10%">
                                                                                <span class="elementLabel">A/C NUMBER</span>
                                                                            </td>
                                                                            <td align="center" width="10%">
                                                                                <span class="elementLabel">CCY</span>
                                                                            </td>
                                                                            <td align="left" width="20%">
                                                                                <span class="elementLabel">AMOUNT</span>
                                                                            </td>
                                                                            <td align="left" width="5%">
                                                                                <span class="elementLabel">PAYER</span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Code" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="44" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_AC_Short_Name" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="44" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Cust_abbr" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="45" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="46" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Acceptance_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="47" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Acceptance_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" onkeydown="return validate_Number(event);" Style="text-align: right"
                                                                                    TabIndex="48" Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Acceptance_payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="49" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">INTEREST</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Interest_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="50" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Interest_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="51"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Interest_payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="52" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">ACCEPTANCE COMMISSION</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Accept_Commission_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="53" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Accept_Commission_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" onkeydown="return validate_Number(event);" Style="text-align: right"
                                                                                    TabIndex="54" Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Accept_Commission_Payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="55" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">PAYMENT/HANDLING COMMISSION</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Pay_Handle_Commission_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="56" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Pay_Handle_Commission_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" onkeydown="return validate_Number(event);" Style="text-align: right"
                                                                                    TabIndex="57" Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Pay_Handle_Commission_Payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="58" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">OTHERS</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Others_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                                    TabIndex="59" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Others_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="60"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Others_Payer" runat="server" CssClass="textBox" MaxLength="1"
                                                                                    TabIndex="61" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">THEIR COMMISSION</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Their_Commission_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="62" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Their_Commission_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" onkeydown="return validate_Number(event);" Style="text-align: right"
                                                                                    TabIndex="63" Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_CR_Their_Commission_Payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="64" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel"><b>/DR/</b> CODE:</span> &nbsp;<asp:Button ID="btn_IMPACC2_DR_Code_help"
                                                                                    runat="server" ToolTip="Press for AcCode list." CssClass="btnHelp_enabled" Enabled="false" />
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
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Code" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="65" Width="90px" onblur="return Toggel_DR_Code();" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_AC_Short_Name" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="65" Width="150px" onblur="return Toggel_DR_Cust_Name();"
                                                                                    Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cust_abbr" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="66" Width="90px" onblur="return Toggel_DR_Cust_Abbr();" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="67" Width="150px" onblur="return Toggel_DR_Cust_Acc();" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                                    TabIndex="68" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="69"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="70" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Code2" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="71" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_AC_Short_Name2" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="71" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cust_abbr2" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="71" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cust_Acc2" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="71" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_Curr2" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="71" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_amt2" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="72"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_payer2" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="73" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Code3" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="74" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_AC_Short_Name3" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="74" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cust_abbr3" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="74" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cust_Acc3" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="74" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_Curr3" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="74" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_amt3" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="75"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_payer3" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="76" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Code4" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="75" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_AC_Short_Name4" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="75" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cust_abbr4" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="75" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cust_Acc4" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="75" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_Curr4" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="75" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_amt4" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="75"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_payer4" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="75" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Code5" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="76" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_AC_Short_Name5" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="76" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cust_abbr5" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="76" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cust_Acc5" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="76" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_Curr5" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="76" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_amt5" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="76"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_payer5" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="76" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Code6" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="77" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_AC_Short_Name6" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="77" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cust_abbr6" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="77" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cust_Acc6" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="77" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_Curr6" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="77" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_amt6" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="77"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC2_DR_Cur_Acc_payer6" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="77" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </asp:Panel>
                                                    </table>
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="10%">
                                                            </td>
                                                            <td align="left" width="90%">
                                                                <asp:Button ID="btnImpAcc2_Prev" runat="server" CssClass="buttonDefault" TabIndex="77"
                                                                    Text="<< Prev" ToolTip="Back to IMPORT ACCOUNTING I" OnClientClick="return ImportAccountingNextClick(0);" />
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btnImpAcc2_Next" runat="server" CssClass="buttonDefault" TabIndex="78"
                                                                    Text="Next >>" ToolTip="Go to IMPORT ACCOUNTING III" OnClientClick="return ImportAccountingNextClick(2);" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                            <ajaxToolkit:TabPanel ID="TabPanelACC3" runat="server" HeaderText="IMPORT ACCOUNTING III"
                                                Font-Bold="true" ForeColor="White">
                                                <ContentTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" width="50%">
                                                                <asp:CheckBox Enabled="false" ID="chk_IMPACC3Flag" Text="IMPORT ACCOUNTING III" runat="server"
                                                                    CssClass="elementLabel" OnCheckedChanged="chk_IMPACC3Flag_OnCheckedChanged" />
                                                            </td>
                                                            <td align="center" width="50%">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%">
                                                        <asp:Panel ID="PanelIMPACC3" runat="server" Visible="false">
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td align="left">
                                                                    <span class="elementLabel">1-DISC </span>
                                                                </td>
                                                                <td colspan="3">
                                                                    <span class="elementLabel">FC Ref No:</span>
                                                                    <asp:TextBox ID="txt_IMPACC3_FCRefNo" runat="server" CssClass="textBox" MaxLength="14"
                                                                        TabIndex="20" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <span class="elementLabel">AMOUNT :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_DiscAmt" runat="server" onchange="return DR_Amt_Calculation();"
                                                                        onkeydown="return validate_Number(event);" CssClass="textBox" TabIndex="20" MaxLength="16"
                                                                        Width="100px" Style="text-align: right" Enabled="false"></asp:TextBox>
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
                                                                    <asp:TextBox ID="txt_IMPACC3_Princ_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="20" onkeydown="return validate_Commission_MATU_Code(event);" Width="40px"
                                                                        Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Princ_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="21" onkeydown="return validate_Commission_LUMP_Code(event);" Width="40px"
                                                                        Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Princ_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="22" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Princ_Ex_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                        TabIndex="23" Width="40px" onkeyup="return DR_Curr_Toggel();" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Princ_Ex_rate" runat="server" CssClass="textBox" MaxLength="16"
                                                                        Style="text-align: right" TabIndex="24" onkeydown="return validate_Number(event);"
                                                                        onchange="return DR_Amt_Calculation();" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Princ_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" TabIndex="25" onkeydown="return validate_Number(event);"
                                                                        Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">INTEREST :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Interest_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                                        onkeydown="return validate_Commission_MATU_Code(event);" TabIndex="26" Width="40px"
                                                                        Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Interest_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                                        onkeydown="return validate_Commission_LUMP_Code(event);" TabIndex="27" Width="40px"
                                                                        Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Interest_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="28" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Interest_Ex_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="29" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Interest_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                        TabIndex="30" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Interest_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                        TabIndex="31" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">COMMISSION :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Commission_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                                        onkeydown="return validate_Commission_MATU_Code(event);" TabIndex="32" Width="40px"
                                                                        Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Commission_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                                        onkeydown="return validate_Commission_LUMP_Code(event);" TabIndex="33" Width="40px"
                                                                        Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Commission_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="34" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Commission_Ex_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="35" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Commission_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                        TabIndex="36" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Commission_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                        TabIndex="37" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">THEIR COMMISSION :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Their_Commission_matu" runat="server" CssClass="textBox"
                                                                        MaxLength="1" onkeydown="return validate_Commission_MATU_Code(event);" TabIndex="38"
                                                                        Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Their_Commission_lump" runat="server" CssClass="textBox"
                                                                        MaxLength="1" onkeydown="return validate_Commission_LUMP_Code(event);" TabIndex="39"
                                                                        Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Their_Commission_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="40" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Their_Commission_Ex_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="41" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Their_Commission_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                        TabIndex="42" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC3_Their_Commission_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                        TabIndex="43" Width="100px" Enabled="false"></asp:TextBox>
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
                                                                            <td align="left" width="10%">
                                                                                <span class="elementLabel"><b>/CR/</b> CODE :</span> &nbsp;<asp:Button ID="btn_IMPACC3_CR_Code_help"
                                                                                    runat="server" ToolTip="Press for AcCode list." CssClass="btnHelp_enabled" Enabled="false" />
                                                                            </td>
                                                                            <td align="left" width="25%">
                                                                                <span class="elementLabel">A/C SHORT NAME</span>
                                                                            </td>
                                                                            <td align="center" width="10%">
                                                                                <span class="elementLabel">CUST ABBR</span>
                                                                            </td>
                                                                            <td align="center" width="10%">
                                                                                <span class="elementLabel">A/C NUMBER</span>
                                                                            </td>
                                                                            <td align="center" width="10%">
                                                                                <span class="elementLabel">CCY</span>
                                                                            </td>
                                                                            <td align="left" width="20%">
                                                                                <span class="elementLabel">AMOUNT</span>
                                                                            </td>
                                                                            <td align="left" width="5%">
                                                                                <span class="elementLabel">PAYER</span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Code" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="44" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_AC_Short_Name" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="44" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Cust_abbr" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="45" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="46" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Acceptance_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="47" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Acceptance_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" onkeydown="return validate_Number(event);" Style="text-align: right"
                                                                                    TabIndex="48" Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Acceptance_payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="49" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">INTEREST</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Interest_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="50" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Interest_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="51"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Interest_payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="52" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">ACCEPTANCE COMMISSION</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Accept_Commission_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="53" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Accept_Commission_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" onkeydown="return validate_Number(event);" Style="text-align: right"
                                                                                    TabIndex="54" Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Accept_Commission_Payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="55" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">PAYMENT/HANDLING COMMISSION</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Pay_Handle_Commission_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="56" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Pay_Handle_Commission_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" onkeydown="return validate_Number(event);" Style="text-align: right"
                                                                                    TabIndex="57" Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Pay_Handle_Commission_Payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="58" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">OTHERS</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Others_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                                    TabIndex="59" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Others_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="60"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Others_Payer" runat="server" CssClass="textBox" MaxLength="1"
                                                                                    TabIndex="61" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">THEIR COMMISSION</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Their_Commission_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="62" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Their_Commission_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" onkeydown="return validate_Number(event);" Style="text-align: right"
                                                                                    TabIndex="63" Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_CR_Their_Commission_Payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="64" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel"><b>/DR/</b> CODE:</span> &nbsp;<asp:Button ID="btn_IMPACC3_DR_Code_help"
                                                                                    runat="server" ToolTip="Press for AcCode list." CssClass="btnHelp_enabled" Enabled="false" />
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
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Code" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="65" Width="90px" onblur="return Toggel_DR_Code();" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_AC_Short_Name" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="65" Width="150px" onblur="return Toggel_DR_Cust_Name();"
                                                                                    Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cust_abbr" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="66" Width="90px" onblur="return Toggel_DR_Cust_Abbr();" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="67" Width="150px" onblur="return Toggel_DR_Cust_Acc();" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                                    TabIndex="68" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="69"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="70" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Code2" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="71" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_AC_Short_Name2" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="71" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cust_abbr2" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="71" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cust_Acc2" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="71" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_Curr2" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="71" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_amt2" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="71"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_payer2" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="71" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Code3" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="71" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_AC_Short_Name3" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="71" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cust_abbr3" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="71" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cust_Acc3" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="71" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_Curr3" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="74" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_amt3" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="75"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_payer3" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="75" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Code4" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="75" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_AC_Short_Name4" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="75" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cust_abbr4" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="75" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cust_Acc4" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="75" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_Curr4" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="75" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_amt4" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="75"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_payer4" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="75" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Code5" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="76" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_AC_Short_Name5" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="76" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cust_abbr5" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="76" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cust_Acc5" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="76" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_Curr5" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="76" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_amt5" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="76"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_payer5" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="76" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Code6" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="77" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_AC_Short_Name6" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="77" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cust_abbr6" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="77" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cust_Acc6" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="77" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_Curr6" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="77" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_amt6" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="77"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC3_DR_Cur_Acc_payer6" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="77" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </asp:Panel>
                                                    </table>
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="10%">
                                                            </td>
                                                            <td align="left" width="90%">
                                                                <asp:Button ID="btnImpAcc3_Prev" runat="server" CssClass="buttonDefault" TabIndex="77"
                                                                    Text="<< Prev" ToolTip="Back to IMPORT ACCOUNTING II" OnClientClick="return ImportAccountingNextClick(1);" />
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btnImpAcc3_Next" runat="server" CssClass="buttonDefault" TabIndex="78"
                                                                    Text="Next >>" ToolTip="Go to IMPORT ACCOUNTING IV" OnClientClick="return ImportAccountingNextClick(3);" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                            <ajaxToolkit:TabPanel ID="TabPanelACC4" runat="server" HeaderText="IMPORT ACCOUNTING IV"
                                                Font-Bold="true" ForeColor="White">
                                                <ContentTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" width="50%">
                                                                <asp:CheckBox Enabled="false" ID="chk_IMPACC4Flag" Text="IMPORT ACCOUNTING IV" runat="server"
                                                                    CssClass="elementLabel" OnCheckedChanged="chk_IMPACC4Flag_OnCheckedChanged" />
                                                            </td>
                                                            <td align="center" width="50%">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%">
                                                        <asp:Panel ID="PanelIMPACC4" runat="server" Visible="false">
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td align="left">
                                                                    <span class="elementLabel">1-DISC </span>
                                                                </td>
                                                                <td colspan="3">
                                                                    <span class="elementLabel">FC Ref No:</span>
                                                                    <asp:TextBox ID="txt_IMPACC4_FCRefNo" runat="server" CssClass="textBox" MaxLength="14"
                                                                        TabIndex="20" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <span class="elementLabel">AMOUNT :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_DiscAmt" runat="server" onchange="return DR_Amt_Calculation();"
                                                                        onkeydown="return validate_Number(event);" CssClass="textBox" TabIndex="20" MaxLength="16"
                                                                        Width="100px" Style="text-align: right" Enabled="false"></asp:TextBox>
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
                                                                    <asp:TextBox ID="txt_IMPACC4_Princ_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="20" onkeydown="return validate_Commission_MATU_Code(event);" Width="40px"
                                                                        Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Princ_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="21" onkeydown="return validate_Commission_LUMP_Code(event);" Width="40px"
                                                                        Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Princ_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="22" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Princ_Ex_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                        TabIndex="23" Width="40px" onkeyup="return DR_Curr_Toggel();" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Princ_Ex_rate" runat="server" CssClass="textBox" MaxLength="16"
                                                                        Style="text-align: right" TabIndex="24" onkeydown="return validate_Number(event);"
                                                                        onchange="return DR_Amt_Calculation();" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Princ_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" TabIndex="25" onkeydown="return validate_Number(event);"
                                                                        Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">INTEREST :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Interest_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                                        onkeydown="return validate_Commission_MATU_Code(event);" TabIndex="26" Width="40px"
                                                                        Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Interest_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                                        onkeydown="return validate_Commission_LUMP_Code(event);" TabIndex="27" Width="40px"
                                                                        Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Interest_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="28" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Interest_Ex_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="29" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Interest_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                        TabIndex="30" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Interest_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                        TabIndex="31" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">COMMISSION :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Commission_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                                        onkeydown="return validate_Commission_MATU_Code(event);" TabIndex="32" Width="40px"
                                                                        Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Commission_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                                        onkeydown="return validate_Commission_LUMP_Code(event);" TabIndex="33" Width="40px"
                                                                        Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Commission_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="34" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Commission_Ex_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="35" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Commission_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                        TabIndex="36" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Commission_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                        TabIndex="37" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">THEIR COMMISSION :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Their_Commission_matu" runat="server" CssClass="textBox"
                                                                        MaxLength="1" onkeydown="return validate_Commission_MATU_Code(event);" TabIndex="38"
                                                                        Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Their_Commission_lump" runat="server" CssClass="textBox"
                                                                        MaxLength="1" onkeydown="return validate_Commission_LUMP_Code(event);" TabIndex="39"
                                                                        Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Their_Commission_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="40" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Their_Commission_Ex_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="41" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Their_Commission_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                        TabIndex="42" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC4_Their_Commission_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                        TabIndex="43" Width="100px" Enabled="false"></asp:TextBox>
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
                                                                            <td align="left" width="10%">
                                                                                <span class="elementLabel"><b>/CR/</b> CODE :</span> &nbsp;<asp:Button ID="btn_IMPACC4_CR_Code_help"
                                                                                    runat="server" ToolTip="Press for AcCode list." CssClass="btnHelp_enabled" Enabled="false" />
                                                                            </td>
                                                                            <td align="left" width="25%">
                                                                                <span class="elementLabel">A/C SHORT NAME</span>
                                                                            </td>
                                                                            <td align="center" width="10%">
                                                                                <span class="elementLabel">CUST ABBR</span>
                                                                            </td>
                                                                            <td align="center" width="10%">
                                                                                <span class="elementLabel">A/C NUMBER</span>
                                                                            </td>
                                                                            <td align="center" width="10%">
                                                                                <span class="elementLabel">CCY</span>
                                                                            </td>
                                                                            <td align="left" width="20%">
                                                                                <span class="elementLabel">AMOUNT</span>
                                                                            </td>
                                                                            <td align="left" width="5%">
                                                                                <span class="elementLabel">PAYER</span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Code" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="44" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_AC_Short_Name" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="44" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Cust_abbr" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="45" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="46" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Acceptance_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="47" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Acceptance_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" onkeydown="return validate_Number(event);" Style="text-align: right"
                                                                                    TabIndex="48" Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Acceptance_payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="49" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">INTEREST</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Interest_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="50" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Interest_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="51"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Interest_payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="52" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">ACCEPTANCE COMMISSION</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Accept_Commission_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="53" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Accept_Commission_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" onkeydown="return validate_Number(event);" Style="text-align: right"
                                                                                    TabIndex="54" Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Accept_Commission_Payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="55" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">PAYMENT/HANDLING COMMISSION</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Pay_Handle_Commission_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="56" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Pay_Handle_Commission_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" onkeydown="return validate_Number(event);" Style="text-align: right"
                                                                                    TabIndex="57" Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Pay_Handle_Commission_Payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="58" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">OTHERS</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Others_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                                    TabIndex="59" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Others_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="60"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Others_Payer" runat="server" CssClass="textBox" MaxLength="1"
                                                                                    TabIndex="61" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">THEIR COMMISSION</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Their_Commission_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="62" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Their_Commission_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" onkeydown="return validate_Number(event);" Style="text-align: right"
                                                                                    TabIndex="63" Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_CR_Their_Commission_Payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="64" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel"><b>/DR/</b> CODE:</span> &nbsp;<asp:Button ID="btn_IMPACC4_DR_Code_help"
                                                                                    runat="server" ToolTip="Press for AcCode list." CssClass="btnHelp_enabled" Enabled="false" />
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
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Code" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="65" Width="90px" onblur="return Toggel_DR_Code();" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_AC_Short_Name" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="65" Width="150px" onblur="return Toggel_DR_Cust_Name();"
                                                                                    Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cust_abbr" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="66" Width="90px" onblur="return Toggel_DR_Cust_Abbr();" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="67" Width="150px" onblur="return Toggel_DR_Cust_Acc();" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                                    TabIndex="68" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="69"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="70" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Code2" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="71" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_AC_Short_Name2" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="71" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cust_abbr2" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="71" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cust_Acc2" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="71" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_Curr2" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="71" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_amt2" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="72"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_payer2" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="73" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Code3" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="74" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_AC_Short_Name3" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="74" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cust_abbr3" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="74" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cust_Acc3" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="74" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_Curr3" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="74" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_amt3" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="75"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_payer3" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="75" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Code4" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="75" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_AC_Short_Name4" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="75" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cust_abbr4" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="75" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cust_Acc4" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="75" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_Curr4" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="75" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_amt4" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="75"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_payer4" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="75" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Code5" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="76" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_AC_Short_Name5" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="76" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cust_abbr5" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="76" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cust_Acc5" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="76" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_Curr5" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="76" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_amt5" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="76"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_payer5" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="76" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Code6" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="77" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_AC_Short_Name6" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="77" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cust_abbr6" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="77" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cust_Acc6" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="77" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_Curr6" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="77" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_amt6" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="77"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC4_DR_Cur_Acc_payer6" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="77" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </asp:Panel>
                                                    </table>
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="10%">
                                                            </td>
                                                            <td align="left" width="90%">
                                                                <asp:Button ID="btnImpAcc4_Prev" runat="server" CssClass="buttonDefault" TabIndex="77"
                                                                    Text="<< Prev" ToolTip="Back to IMPORT ACCOUNTING III" OnClientClick="return ImportAccountingNextClick(2);" />
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btnImpAcc4_Next" runat="server" CssClass="buttonDefault" TabIndex="78"
                                                                    Text="Next >>" ToolTip="Go to IMPORT ACCOUNTING V" OnClientClick="return ImportAccountingNextClick(4);" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                            <ajaxToolkit:TabPanel ID="TabPanelACC5" runat="server" HeaderText="IMPORT ACCOUNTING V"
                                                Font-Bold="true" ForeColor="White">
                                                <ContentTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" width="50%">
                                                                <asp:CheckBox Enabled="false" ID="chk_IMPACC5Flag" Text="IMPORT ACCOUNTING V" runat="server"
                                                                    CssClass="elementLabel" OnCheckedChanged="chk_IMPACC5Flag_OnCheckedChanged" />
                                                            </td>
                                                            <td align="center" width="50%">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%">
                                                        <asp:Panel ID="PanelIMPACC5" runat="server" Visible="false">
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td align="left">
                                                                    <span class="elementLabel">1-DISC </span>
                                                                </td>
                                                                <td colspan="3">
                                                                    <span class="elementLabel">FC Ref No:</span>
                                                                    <asp:TextBox ID="txt_IMPACC5_FCRefNo" runat="server" CssClass="textBox" MaxLength="14"
                                                                        TabIndex="20" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <span class="elementLabel">AMOUNT :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_DiscAmt" runat="server" onchange="return DR_Amt_Calculation();"
                                                                        onkeydown="return validate_Number(event);" CssClass="textBox" TabIndex="20" MaxLength="16"
                                                                        Width="100px" Style="text-align: right" Enabled="false"></asp:TextBox>
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
                                                                    <asp:TextBox ID="txt_IMPACC5_Princ_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="20" onkeydown="return validate_Commission_MATU_Code(event);" Width="40px"
                                                                        Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Princ_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="21" onkeydown="return validate_Commission_LUMP_Code(event);" Width="40px"
                                                                        Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Princ_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="22" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Princ_Ex_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                        TabIndex="23" Width="40px" onkeyup="return DR_Curr_Toggel();" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Princ_Ex_rate" runat="server" CssClass="textBox" MaxLength="16"
                                                                        Style="text-align: right" TabIndex="24" onkeydown="return validate_Number(event);"
                                                                        onchange="return DR_Amt_Calculation();" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Princ_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" TabIndex="25" onkeydown="return validate_Number(event);"
                                                                        Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">INTEREST :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Interest_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                                        onkeydown="return validate_Commission_MATU_Code(event);" TabIndex="26" Width="40px"
                                                                        Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Interest_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                                        onkeydown="return validate_Commission_LUMP_Code(event);" TabIndex="27" Width="40px"
                                                                        Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Interest_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="28" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Interest_Ex_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="29" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Interest_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                        TabIndex="30" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Interest_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                        TabIndex="31" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">COMMISSION :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Commission_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                                        onkeydown="return validate_Commission_MATU_Code(event);" TabIndex="32" Width="40px"
                                                                        Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Commission_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                                        onkeydown="return validate_Commission_LUMP_Code(event);" TabIndex="33" Width="40px"
                                                                        Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Commission_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="34" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Commission_Ex_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="35" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Commission_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                        TabIndex="36" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Commission_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                        TabIndex="37" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">THEIR COMMISSION :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Their_Commission_matu" runat="server" CssClass="textBox"
                                                                        MaxLength="1" onkeydown="return validate_Commission_MATU_Code(event);" TabIndex="38"
                                                                        Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Their_Commission_lump" runat="server" CssClass="textBox"
                                                                        MaxLength="1" onkeydown="return validate_Commission_LUMP_Code(event);" TabIndex="39"
                                                                        Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Their_Commission_Contract_no" runat="server" CssClass="textBox"
                                                                        MaxLength="9" TabIndex="40" Width="90px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Their_Commission_Ex_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="41" Width="40px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Their_Commission_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                        TabIndex="42" Width="100px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IMPACC5_Their_Commission_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                                        MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                        TabIndex="43" Width="100px" Enabled="false"></asp:TextBox>
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
                                                                            <td align="left" width="10%">
                                                                                <span class="elementLabel"><b>/CR/</b> CODE :</span> &nbsp;<asp:Button ID="btn_IMPACC5_CR_Code_help"
                                                                                    runat="server" ToolTip="Press for AcCode list." CssClass="btnHelp_enabled" Enabled="false" />
                                                                            </td>
                                                                            <td align="left" width="25%">
                                                                                <span class="elementLabel">A/C SHORT NAME</span>
                                                                            </td>
                                                                            <td align="center" width="10%">
                                                                                <span class="elementLabel">CUST ABBR</span>
                                                                            </td>
                                                                            <td align="center" width="10%">
                                                                                <span class="elementLabel">A/C NUMBER</span>
                                                                            </td>
                                                                            <td align="center" width="10%">
                                                                                <span class="elementLabel">CCY</span>
                                                                            </td>
                                                                            <td align="left" width="20%">
                                                                                <span class="elementLabel">AMOUNT</span>
                                                                            </td>
                                                                            <td align="left" width="5%">
                                                                                <span class="elementLabel">PAYER</span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Code" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="44" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_AC_Short_Name" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="44" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Cust_abbr" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="45" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="46" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Acceptance_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="47" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Acceptance_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" onkeydown="return validate_Number(event);" Style="text-align: right"
                                                                                    TabIndex="48" Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Acceptance_payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="49" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">INTEREST</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Interest_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="50" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Interest_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="51"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Interest_payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="52" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">ACCEPTANCE COMMISSION</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Accept_Commission_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="53" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Accept_Commission_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" onkeydown="return validate_Number(event);" Style="text-align: right"
                                                                                    TabIndex="54" Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Accept_Commission_Payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="55" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">PAYMENT/HANDLING COMMISSION</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Pay_Handle_Commission_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="56" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Pay_Handle_Commission_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" onkeydown="return validate_Number(event);" Style="text-align: right"
                                                                                    TabIndex="57" Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Pay_Handle_Commission_Payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="58" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">OTHERS</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Others_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                                    TabIndex="59" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Others_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="60"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Others_Payer" runat="server" CssClass="textBox" MaxLength="1"
                                                                                    TabIndex="61" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <span class="elementLabel">THEIR COMMISSION</span>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Their_Commission_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="62" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Their_Commission_amt" runat="server" CssClass="textBox"
                                                                                    MaxLength="16" onkeydown="return validate_Number(event);" Style="text-align: right"
                                                                                    TabIndex="63" Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_CR_Their_Commission_Payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="64" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel"><b>/DR/</b> CODE:</span> &nbsp;<asp:Button ID="btn_IMPACC5_DR_Code_help"
                                                                                    runat="server" ToolTip="Press for AcCode list." CssClass="btnHelp_enabled" Enabled="false" />
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
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Code" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="65" Width="90px" onblur="return Toggel_DR_Code();" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_AC_Short_Name" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="65" Width="150px" onblur="return Toggel_DR_Cust_Name();"
                                                                                    Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cust_abbr" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="66" Width="90px" onblur="return Toggel_DR_Cust_Abbr();" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="67" Width="150px" onblur="return Toggel_DR_Cust_Acc();" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                                    TabIndex="68" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="69"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_payer" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="70" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Code2" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="71" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_AC_Short_Name2" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="71" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cust_abbr2" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="71" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cust_Acc2" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="71" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_Curr2" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="71" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_amt2" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="72"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_payer2" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="73" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Code3" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="74" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_AC_Short_Name3" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="74" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cust_abbr3" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="74" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cust_Acc3" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="74" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_Curr3" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="74" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_amt3" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="75"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_payer3" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="76" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Code4" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="75" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_AC_Short_Name4" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="75" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cust_abbr4" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="75" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cust_Acc4" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="75" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_Curr4" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="75" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_amt4" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="75"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_payer4" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="75" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Code5" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="76" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_AC_Short_Name5" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="76" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cust_abbr5" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="76" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cust_Acc5" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="76" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_Curr5" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="76" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_amt5" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="76"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_payer5" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="76" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Code6" runat="server" CssClass="textBox" MaxLength="5"
                                                                                    TabIndex="77" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_AC_Short_Name6" runat="server" CssClass="textBox"
                                                                                    MaxLength="20" TabIndex="77" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cust_abbr6" runat="server" CssClass="textBox" MaxLength="12"
                                                                                    TabIndex="77" Width="90px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cust_Acc6" runat="server" CssClass="textBox" MaxLength="20"
                                                                                    TabIndex="77" Width="150px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_Curr6" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="77" Width="40px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_amt6" runat="server" CssClass="textBox" MaxLength="16"
                                                                                    onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="77"
                                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_IMPACC5_DR_Cur_Acc_payer6" runat="server" CssClass="textBox"
                                                                                    MaxLength="1" TabIndex="77" Width="50px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </asp:Panel>
                                                    </table>
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="10%">
                                                            </td>
                                                            <td align="left" width="90%">
                                                                <asp:Button ID="btnImpAcc5_Prev" runat="server" CssClass="buttonDefault" TabIndex="77"
                                                                    Text="<< Prev" ToolTip="Back to IMPORT ACCOUNTING IV" OnClientClick="return ImportAccountingNextClick(3);" />
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btnImpAcc5_Next" runat="server" CssClass="buttonDefault" TabIndex="78"
                                                                    Text="Next >>" ToolTip="Go to GENERAL OPERATION 1" OnClientClick="return GeneralOperationNextClick(0);" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                        </ajaxToolkit:TabContainer>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel ID="tbDocumentGO" runat="server" HeaderText="GENERAL OPERATION"
                                    Font-Bold="true" ForeColor="White">
                                    <ContentTemplate>
                                        <ajaxToolkit:TabContainer ID="TabSubContainerGO" runat="server" ActiveTabIndex="0"
                                            CssClass="ajax__subtab_xp-theme">
                                            <ajaxToolkit:TabPanel ID="TabPanelGO1" runat="server" HeaderText="GENERAL OPERATION I"
                                                Font-Bold="true" ForeColor="White">
                                                <ContentTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" width="50%">
                                                                <asp:CheckBox Enabled="false" ID="chk_GO1Flag" Text="GENERAL OPERATION I" runat="server"
                                                                    CssClass="elementLabel" OnCheckedChanged="chk_GO1Flag_OnCheckedChanged" />
                                                            </td>
                                                            <td align="center" width="50%">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="left" width="50%">
                                                                <table width="100%">
                                                                    <asp:Panel ID="Panel_GO1Left" runat="server" Visible="false">
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">COMMENT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Comment" runat="server" CssClass="textBox" TabIndex="81"
                                                                                    MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">Section No:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO1_Left_SectionNo" runat="server" CssClass="textBox" TabIndex="82"
                                                                                    MaxLength="2" Width="20px" Enabled="false"></asp:TextBox>
                                                                                <span class="elementLabel">Remarks:</span>
                                                                                <asp:TextBox ID="txt_GO1_Left_Remarks" runat="server" CssClass="textBox" Width="300px"
                                                                                    MaxLength="30" TabIndex="82" onblur="return Toggel_GO1_Left_Remarks();" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">MEMO:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Memo" runat="server" CssClass="textBox" Width="50px"
                                                                                    TabIndex="82" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">SCHEME No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Scheme_no" runat="server" CssClass="textBox" TabIndex="83"
                                                                                    MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" width="10%">
                                                                                <span class="elementLabel">DEBIT / CREDIT:</span>
                                                                            </td>
                                                                            <td align="left" width="24%">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Code" runat="server" CssClass="textBox" TabIndex="84"
                                                                                    Width="20px" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right" width="6%">
                                                                                <span class="elementLabel">CCY:</span>
                                                                            </td>
                                                                            <td align="left" width="34%">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Curr" runat="server" CssClass="textBox" TabIndex="84"
                                                                                    Width="25px" MaxLength="3" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right" width="6%">
                                                                                <span class="elementLabel">AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left" width="20%">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Amt" runat="server" CssClass="textBox" Width="90px"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="84" Style="text-align: right"
                                                                                    MaxLength="16" onblur="return Toggel_Debit_Amt();" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CUSTOMER:</span>
                                                                            </td>
                                                                            <td align="left" colspan="4">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Cust" runat="server" CssClass="textBox" TabIndex="85"
                                                                                    MaxLength="12" Width="100px" Enabled="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Cust_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="85" MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C CODE:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                                    TabIndex="86" MaxLength="5" Width="50px" Enabled="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="86" MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                                    TabIndex="86" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" align="right">
                                                                                <span class="elementLabel">EXCH RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Exch_Rate" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="87" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">EXCH CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Exch_CCY" runat="server" CssClass="textBox" MaxLength="3"
                                                                                    TabIndex="87" Width="25px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">FUND:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_FUND" runat="server" CssClass="textBox" TabIndex="88"
                                                                                    Width="20px" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CHECK No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Check_No" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    Width="50px" TabIndex="88" MaxLength="6" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AVAILABLE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Available" runat="server" CssClass="textBox"
                                                                                    TabIndex="88" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ADVICE PRINT:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_AdPrint" runat="server" CssClass="textBox" TabIndex="89"
                                                                                    Width="20px" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                                <span class="elementLabel">DETAILS:</span>
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Details" runat="server" CssClass="textBox" TabIndex="89"
                                                                                    MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ENTITY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="89" Width="90px" Style="text-align: right" MaxLength="3" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DIVISION:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Division" runat="server" CssClass="textBox" TabIndex="90"
                                                                                    MaxLength="2" Width="20px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Inter_Amount" onkeydown="return validate_Number(event);"
                                                                                    runat="server" Width="100px" MaxLength="16" Style="text-align: right" CssClass="textBox"
                                                                                    TabIndex="90" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Inter_Rate" runat="server" CssClass="textBox"
                                                                                    TabIndex="90" Width="100px" MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                                    Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <br />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DEBIT / CREDIT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Code" runat="server" CssClass="textBox" TabIndex="91"
                                                                                    MaxLength="1" Width="20px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Curr" runat="server" CssClass="textBox" TabIndex="91"
                                                                                    MaxLength="3" Width="25px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Amt" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="91" Width="90px" Style="text-align: right" MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CUSTOMER:</span>
                                                                            </td>
                                                                            <td align="left" colspan="4">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Cust" runat="server" CssClass="textBox" TabIndex="92"
                                                                                    MaxLength="12" Width="100px" Enabled="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Cust_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="92" MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C CODE:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                                    TabIndex="93" MaxLength="5" Width="50px" Enabled="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="93" MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                                    TabIndex="93" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" align="right">
                                                                                <span class="elementLabel">EXCH RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Exch_Rate" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="94" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">EXCH CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Exch_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="94" Width="25px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">FUND:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_FUND" runat="server" CssClass="textBox" TabIndex="95"
                                                                                    MaxLength="1" Width="20px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CHECK No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Check_No" runat="server" CssClass="textBox"
                                                                                    TabIndex="95" onkeydown="return validate_Number(event);" Width="50px" MaxLength="6"
                                                                                    Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AVAILABLE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Available" runat="server" CssClass="textBox"
                                                                                    TabIndex="95" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ADVICE PRINT:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_AdPrint" runat="server" CssClass="textBox" TabIndex="96"
                                                                                    Width="20px" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                                <span class="elementLabel">DETAILS:</span>
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Details" runat="server" CssClass="textBox" TabIndex="96"
                                                                                    Width="300px" MaxLength="30" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ENTITY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="96" Width="90px" Style="text-align: right" MaxLength="3" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DIVISION:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Division" runat="server" CssClass="textBox"
                                                                                    TabIndex="97" Width="20px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Inter_Amount" onkeydown="return validate_Number(event);"
                                                                                    runat="server" CssClass="textBox" TabIndex="97" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Inter_Rate" onkeydown="return validate_Number(event);"
                                                                                    runat="server" CssClass="textBox" TabIndex="97" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </asp:Panel>
                                                                </table>
                                                            </td>
                                                            <td align="left" width="50%">
                                                                <table width="100%">
                                                                    <asp:Panel ID="Panel_GO1Right" runat="server" Visible="false">
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">COMMENT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Comment" runat="server" CssClass="textBox" TabIndex="101"
                                                                                    MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">Section No:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO1_Right_SectionNo" runat="server" CssClass="textBox" TabIndex="102"
                                                                                    MaxLength="2" Width="20px" Enabled="false"></asp:TextBox>
                                                                                <span class="elementLabel">Remarks:</span>
                                                                                <asp:TextBox ID="txt_GO1_Right_Remarks" runat="server" CssClass="textBox" Width="300px"
                                                                                    MaxLength="30" TabIndex="102" onblur="return Toggel_GO1_Right_Remarks();" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">MEMO:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Memo" runat="server" CssClass="textBox" Width="50px"
                                                                                    TabIndex="102" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">SCHEME No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Scheme_no" runat="server" CssClass="textBox" TabIndex="103"
                                                                                    MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" width="10%">
                                                                                <span class="elementLabel">DEBIT / CREDIT:</span>
                                                                            </td>
                                                                            <td align="left" width="24%">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Code" runat="server" CssClass="textBox" TabIndex="104"
                                                                                    Width="20px" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right" width="6%">
                                                                                <span class="elementLabel">CCY:</span>
                                                                            </td>
                                                                            <td align="left" width="34%">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Curr" runat="server" CssClass="textBox" TabIndex="104"
                                                                                    Width="25px" MaxLength="3" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right" width="6%">
                                                                                <span class="elementLabel">AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left" width="20%">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Amt" runat="server" CssClass="textBox" Width="90px"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="104" Style="text-align: right"
                                                                                    MaxLength="16" onblur="return Toggel_Debit_Amt();" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CUSTOMER:</span>
                                                                            </td>
                                                                            <td align="left" colspan="4">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Cust" runat="server" CssClass="textBox" TabIndex="105"
                                                                                    MaxLength="12" Width="100px" Enabled="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Cust_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="105" MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C CODE:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                                    TabIndex="105" MaxLength="5" Width="50px" Enabled="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="105" MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                                    TabIndex="105" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" align="right">
                                                                                <span class="elementLabel">EXCH RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Exch_Rate" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="106" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">EXCH CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Exch_CCY" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="106" Width="25px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">FUND:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_FUND" runat="server" CssClass="textBox" TabIndex="107"
                                                                                    Width="20px" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CHECK No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Check_No" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="107" MaxLength="6" Width="50px"
                                                                                    Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AVAILABLE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Available" runat="server" CssClass="textBox"
                                                                                    TabIndex="107" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ADVICE PRINT:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_AdPrint" runat="server" CssClass="textBox" TabIndex="108"
                                                                                    Width="20px" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                                <span class="elementLabel">DETAILS:</span>
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Details" runat="server" CssClass="textBox" TabIndex="108"
                                                                                    MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ENTITY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="108" Width="90px" Style="text-align: right" MaxLength="3" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DIVISION:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Division" runat="server" CssClass="textBox"
                                                                                    TabIndex="109" MaxLength="2" Width="20px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Inter_Amount" onkeydown="return validate_Number(event);"
                                                                                    runat="server" Width="100px" MaxLength="16" Style="text-align: right" CssClass="textBox"
                                                                                    TabIndex="109" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Inter_Rate" runat="server" CssClass="textBox"
                                                                                    TabIndex="109" Width="100px" MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                                    Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <br />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DEBIT / CREDIT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Code" runat="server" CssClass="textBox" TabIndex="110"
                                                                                    MaxLength="1" Width="20px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Curr" runat="server" CssClass="textBox" TabIndex="110"
                                                                                    MaxLength="3" Width="25px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Amt" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="110" Width="90px" Style="text-align: right" MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CUSTOMER:</span>
                                                                            </td>
                                                                            <td align="left" colspan="4">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Cust" runat="server" CssClass="textBox" TabIndex="111"
                                                                                    MaxLength="12" Width="100px" Enabled="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Cust_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="111" MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C CODE:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                                    TabIndex="112" MaxLength="5" Width="50px" Enabled="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="112" MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                                    TabIndex="112" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" align="right">
                                                                                <span class="elementLabel">EXCH RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Exch_Rate" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="113" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">EXCH CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Exch_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="113" Width="25px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">FUND:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_FUND" runat="server" CssClass="textBox" TabIndex="114"
                                                                                    MaxLength="1" Width="20px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CHECK No: </span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Check_No" runat="server" CssClass="textBox"
                                                                                    TabIndex="114" onkeydown="return validate_Number(event);" Width="50px" MaxLength="6"
                                                                                    Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AVAILABLE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Available" runat="server" CssClass="textBox"
                                                                                    TabIndex="114" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ADVICE PRINT:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_AdPrint" runat="server" CssClass="textBox"
                                                                                    TabIndex="115" Width="20px" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                                <span class="elementLabel">DETAILS:</span>
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Details" runat="server" CssClass="textBox"
                                                                                    TabIndex="115" Width="300px" MaxLength="30" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ENTITY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="115" Width="90px" Style="text-align: right" MaxLength="3" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DIVISION:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Division" runat="server" CssClass="textBox"
                                                                                    TabIndex="116" Width="20px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Inter_Amount" onkeydown="return validate_Number(event);"
                                                                                    runat="server" CssClass="textBox" TabIndex="116" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Inter_Rate" onkeydown="return validate_Number(event);"
                                                                                    runat="server" CssClass="textBox" TabIndex="116" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </asp:Panel>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="10%">
                                                            </td>
                                                            <td align="left" width="90%">
                                                                <asp:Button ID="btn_GO1_Prev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                                    ToolTip="Back to IMPORT ACCOUNTING V" TabIndex="117" OnClientClick="return ImportAccountingNextClick(4);" />&nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btn_GO1_Next" runat="server" Text="Next >>" CssClass="buttonDefault"
                                                                    ToolTip="Go to GENERAL OPERATION 2" TabIndex="117" OnClientClick="return GeneralOperationNextClick(1);" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                            <ajaxToolkit:TabPanel ID="TabPanelGO2" runat="server" HeaderText="GENERAL OPERATION II"
                                                Font-Bold="true" ForeColor="White">
                                                <ContentTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" width="50%">
                                                                <asp:CheckBox Enabled="false" ID="chk_GO2Flag" Text="GENERAL OPERATION II" runat="server"
                                                                    CssClass="elementLabel" OnCheckedChanged="chk_GO2Flag_OnCheckedChanged" />
                                                            </td>
                                                            <td align="center" width="50%">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="left" width="50%">
                                                                <table width="100%">
                                                                    <asp:Panel ID="Panel_GO2Left" runat="server" Visible="false">
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">COMMENT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Comment" runat="server" CssClass="textBox" TabIndex="81"
                                                                                    MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">Section No:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO2_Left_SectionNo" runat="server" CssClass="textBox" TabIndex="82"
                                                                                    MaxLength="2" Width="20px" Enabled="false"></asp:TextBox>
                                                                                <span class="elementLabel">Remarks:</span>
                                                                                <asp:TextBox ID="txt_GO2_Left_Remarks" runat="server" CssClass="textBox" Width="300px"
                                                                                    MaxLength="30" TabIndex="82" onblur="return Toggel_GO2_Left_Remarks();" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">MEMO:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Memo" runat="server" CssClass="textBox" Width="50px"
                                                                                    TabIndex="82" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">SCHEME No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Scheme_no" runat="server" CssClass="textBox" TabIndex="83"
                                                                                    MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" width="10%">
                                                                                <span class="elementLabel">DEBIT / CREDIT:</span>
                                                                            </td>
                                                                            <td align="left" width="24%">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Code" runat="server" CssClass="textBox" TabIndex="84"
                                                                                    Width="20px" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right" width="6%">
                                                                                <span class="elementLabel">CCY:</span>
                                                                            </td>
                                                                            <td align="left" width="34%">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Curr" runat="server" CssClass="textBox" TabIndex="84"
                                                                                    Width="25px" MaxLength="3" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right" width="6%">
                                                                                <span class="elementLabel">AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left" width="20%">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Amt" runat="server" CssClass="textBox" Width="90px"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="84" Style="text-align: right"
                                                                                    MaxLength="16" onblur="return Toggel_Debit_Amt();" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CUSTOMER:</span>
                                                                            </td>
                                                                            <td align="left" colspan="4">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Cust" runat="server" CssClass="textBox" TabIndex="85"
                                                                                    MaxLength="12" Width="100px" Enabled="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Cust_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="85" MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C CODE:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                                    TabIndex="86" MaxLength="5" Width="50px" Enabled="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="86" MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                                    TabIndex="86" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" align="right">
                                                                                <span class="elementLabel">EXCH RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Exch_Rate" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="87" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">EXCH CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Exch_CCY" runat="server" CssClass="textBox" MaxLength="3"
                                                                                    TabIndex="87" Width="25px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">FUND:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_FUND" runat="server" CssClass="textBox" TabIndex="88"
                                                                                    Width="20px" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CHECK No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Check_No" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    Width="50px" TabIndex="88" MaxLength="6" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AVAILABLE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Available" runat="server" CssClass="textBox"
                                                                                    TabIndex="88" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ADVICE PRINT:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_AdPrint" runat="server" CssClass="textBox" TabIndex="89"
                                                                                    Width="20px" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                                <span class="elementLabel">DETAILS:</span>
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Details" runat="server" CssClass="textBox" TabIndex="89"
                                                                                    MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ENTITY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="89" Width="90px" Style="text-align: right" MaxLength="3" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DIVISION:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Division" runat="server" CssClass="textBox" TabIndex="90"
                                                                                    MaxLength="2" Width="20px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Inter_Amount" onkeydown="return validate_Number(event);"
                                                                                    runat="server" Width="100px" MaxLength="16" Style="text-align: right" CssClass="textBox"
                                                                                    TabIndex="90" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Inter_Rate" runat="server" CssClass="textBox"
                                                                                    TabIndex="90" Width="100px" MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                                    Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <br />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DEBIT / CREDIT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Code" runat="server" CssClass="textBox" TabIndex="91"
                                                                                    MaxLength="1" Width="20px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Curr" runat="server" CssClass="textBox" TabIndex="91"
                                                                                    MaxLength="3" Width="25px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Amt" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="91" Width="90px" Style="text-align: right" MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CUSTOMER:</span>
                                                                            </td>
                                                                            <td align="left" colspan="4">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Cust" runat="server" CssClass="textBox" TabIndex="92"
                                                                                    MaxLength="12" Width="100px" Enabled="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Cust_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="92" MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C CODE:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                                    TabIndex="93" MaxLength="5" Width="50px" Enabled="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="93" MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                                    TabIndex="93" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" align="right">
                                                                                <span class="elementLabel">EXCH RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Exch_Rate" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="94" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">EXCH CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Exch_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="94" Width="25px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">FUND:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_FUND" runat="server" CssClass="textBox" TabIndex="95"
                                                                                    MaxLength="1" Width="20px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CHECK No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Check_No" runat="server" CssClass="textBox"
                                                                                    TabIndex="95" onkeydown="return validate_Number(event);" Width="50px" MaxLength="6"
                                                                                    Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AVAILABLE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Available" runat="server" CssClass="textBox"
                                                                                    TabIndex="95" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ADVICE PRINT:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_AdPrint" runat="server" CssClass="textBox" TabIndex="96"
                                                                                    Width="20px" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                                <span class="elementLabel">DETAILS:</span>
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Details" runat="server" CssClass="textBox" TabIndex="96"
                                                                                    Width="300px" MaxLength="30" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ENTITY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="96" Width="90px" Style="text-align: right" MaxLength="3" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DIVISION:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Division" runat="server" CssClass="textBox"
                                                                                    TabIndex="97" Width="20px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Inter_Amount" onkeydown="return validate_Number(event);"
                                                                                    runat="server" CssClass="textBox" TabIndex="97" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Inter_Rate" onkeydown="return validate_Number(event);"
                                                                                    runat="server" CssClass="textBox" TabIndex="97" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </asp:Panel>
                                                                </table>
                                                            </td>
                                                            <td align="left" width="50%">
                                                                <table width="100%">
                                                                    <asp:Panel ID="Panel_GO2Right" runat="server" Visible="false">
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">COMMENT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Comment" runat="server" CssClass="textBox" TabIndex="101"
                                                                                    MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">Section No:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO2_Right_SectionNo" runat="server" CssClass="textBox" TabIndex="102"
                                                                                    MaxLength="2" Width="20px" Enabled="false"></asp:TextBox>
                                                                                <span class="elementLabel">Remarks:</span>
                                                                                <asp:TextBox ID="txt_GO2_Right_Remarks" runat="server" CssClass="textBox" Width="300px"
                                                                                    MaxLength="30" TabIndex="102" onblur="return Toggel_GO2_Right_Remarks();" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">MEMO:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Memo" runat="server" CssClass="textBox" Width="50px"
                                                                                    TabIndex="102" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">SCHEME No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Scheme_no" runat="server" CssClass="textBox" TabIndex="103"
                                                                                    MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" width="10%">
                                                                                <span class="elementLabel">DEBIT / CREDIT:</span>
                                                                            </td>
                                                                            <td align="left" width="24%">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Code" runat="server" CssClass="textBox" TabIndex="104"
                                                                                    Width="20px" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right" width="6%">
                                                                                <span class="elementLabel">CCY:</span>
                                                                            </td>
                                                                            <td align="left" width="34%">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Curr" runat="server" CssClass="textBox" TabIndex="104"
                                                                                    Width="25px" MaxLength="3" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right" width="6%">
                                                                                <span class="elementLabel">AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left" width="20%">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Amt" runat="server" CssClass="textBox" Width="90px"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="104" Style="text-align: right"
                                                                                    MaxLength="16" onblur="return Toggel_Debit_Amt();" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CUSTOMER:</span>
                                                                            </td>
                                                                            <td align="left" colspan="4">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Cust" runat="server" CssClass="textBox" TabIndex="105"
                                                                                    MaxLength="12" Width="100px" Enabled="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Cust_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="105" MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C CODE:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                                    TabIndex="105" MaxLength="5" Width="50px" Enabled="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="105" MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                                    TabIndex="105" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" align="right">
                                                                                <span class="elementLabel">EXCH RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Exch_Rate" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="106" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">EXCH CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Exch_CCY" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="106" Width="25px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">FUND:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_FUND" runat="server" CssClass="textBox" TabIndex="107"
                                                                                    Width="20px" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CHECK No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Check_No" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="107" MaxLength="6" Width="50px"
                                                                                    Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AVAILABLE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Available" runat="server" CssClass="textBox"
                                                                                    TabIndex="107" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ADVICE PRINT:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_AdPrint" runat="server" CssClass="textBox" TabIndex="108"
                                                                                    Width="20px" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                                <span class="elementLabel">DETAILS:</span>
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Details" runat="server" CssClass="textBox" TabIndex="108"
                                                                                    MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ENTITY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="108" Width="90px" Style="text-align: right" MaxLength="3" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DIVISION:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Division" runat="server" CssClass="textBox"
                                                                                    TabIndex="109" MaxLength="2" Width="20px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Inter_Amount" onkeydown="return validate_Number(event);"
                                                                                    runat="server" Width="100px" MaxLength="16" Style="text-align: right" CssClass="textBox"
                                                                                    TabIndex="109" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Inter_Rate" runat="server" CssClass="textBox"
                                                                                    TabIndex="109" Width="100px" MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                                    Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <br />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DEBIT / CREDIT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Code" runat="server" CssClass="textBox" TabIndex="110"
                                                                                    MaxLength="1" Width="20px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Curr" runat="server" CssClass="textBox" TabIndex="110"
                                                                                    MaxLength="3" Width="25px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Amt" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="110" Width="90px" Style="text-align: right" MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CUSTOMER:</span>
                                                                            </td>
                                                                            <td align="left" colspan="4">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Cust" runat="server" CssClass="textBox" TabIndex="111"
                                                                                    MaxLength="12" Width="100px" Enabled="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Cust_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="111" MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C CODE:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                                    TabIndex="112" MaxLength="5" Width="50px" Enabled="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="112" MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                                    TabIndex="112" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" align="right">
                                                                                <span class="elementLabel">EXCH RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Exch_Rate" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="113" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">EXCH CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Exch_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="113" Width="25px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">FUND:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_FUND" runat="server" CssClass="textBox" TabIndex="114"
                                                                                    MaxLength="1" Width="20px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CHECK No: </span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Check_No" runat="server" CssClass="textBox"
                                                                                    TabIndex="114" onkeydown="return validate_Number(event);" Width="50px" MaxLength="6"
                                                                                    Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AVAILABLE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Available" runat="server" CssClass="textBox"
                                                                                    TabIndex="114" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ADVICE PRINT:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_AdPrint" runat="server" CssClass="textBox"
                                                                                    TabIndex="115" Width="20px" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                                <span class="elementLabel">DETAILS:</span>
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Details" runat="server" CssClass="textBox"
                                                                                    TabIndex="115" Width="300px" MaxLength="30" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ENTITY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="115" Width="90px" Style="text-align: right" MaxLength="3" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DIVISION:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Division" runat="server" CssClass="textBox"
                                                                                    TabIndex="116" Width="20px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Inter_Amount" onkeydown="return validate_Number(event);"
                                                                                    runat="server" CssClass="textBox" TabIndex="116" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Inter_Rate" onkeydown="return validate_Number(event);"
                                                                                    runat="server" CssClass="textBox" TabIndex="116" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </asp:Panel>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="10%">
                                                            </td>
                                                            <td align="left" width="90%">
                                                                <asp:Button ID="btn_GO2_Prev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                                    ToolTip="Back to GENERAL OPERATION I" TabIndex="117" OnClientClick="return GeneralOperationNextClick(0);" />&nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btn_GO2_Next" runat="server" Text="Next >>" CssClass="buttonDefault"
                                                                    ToolTip="Go to GENERAL OPERATION III" TabIndex="117" OnClientClick="return GeneralOperationNextClick(2);" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                            <ajaxToolkit:TabPanel ID="TabPanelGO3" runat="server" HeaderText="GENERAL OPERATION III"
                                                Font-Bold="true" ForeColor="White">
                                                <ContentTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" width="50%">
                                                                <asp:CheckBox Enabled="false" ID="chk_GO3Flag" Text="GENERAL OPERATION III" runat="server"
                                                                    CssClass="elementLabel" OnCheckedChanged="chk_GO3Flag_OnCheckedChanged" />
                                                            </td>
                                                            <td align="center" width="50%">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="left" width="50%">
                                                                <table width="100%">
                                                                    <asp:Panel ID="Panel_GO3Left" runat="server" Visible="false">
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">COMMENT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Left_Comment" runat="server" CssClass="textBox" TabIndex="81"
                                                                                    MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">Section No:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO3_Left_SectionNo" runat="server" CssClass="textBox" TabIndex="82"
                                                                                    MaxLength="2" Width="20px" Enabled="false"></asp:TextBox>
                                                                                <span class="elementLabel">Remarks:</span>
                                                                                <asp:TextBox ID="txt_GO3_Left_Remarks" runat="server" CssClass="textBox" Width="300px"
                                                                                    MaxLength="30" TabIndex="82" onblur="return Toggel_GO3_Left_Remarks();" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">MEMO:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Left_Memo" runat="server" CssClass="textBox" Width="50px"
                                                                                    TabIndex="82" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">SCHEME No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Left_Scheme_no" runat="server" CssClass="textBox" TabIndex="83"
                                                                                    MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" width="10%">
                                                                                <span class="elementLabel">DEBIT / CREDIT:</span>
                                                                            </td>
                                                                            <td align="left" width="24%">
                                                                                <asp:TextBox ID="txt_GO3_Left_Debit_Code" runat="server" CssClass="textBox" TabIndex="84"
                                                                                    Width="20px" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right" width="6%">
                                                                                <span class="elementLabel">CCY:</span>
                                                                            </td>
                                                                            <td align="left" width="34%">
                                                                                <asp:TextBox ID="txt_GO3_Left_Debit_Curr" runat="server" CssClass="textBox" TabIndex="84"
                                                                                    Width="25px" MaxLength="3" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right" width="6%">
                                                                                <span class="elementLabel">AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left" width="20%">
                                                                                <asp:TextBox ID="txt_GO3_Left_Debit_Amt" runat="server" CssClass="textBox" Width="90px"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="84" Style="text-align: right"
                                                                                    MaxLength="16" onblur="return Toggel_Debit_Amt();" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CUSTOMER:</span>
                                                                            </td>
                                                                            <td align="left" colspan="4">
                                                                                <asp:TextBox ID="txt_GO3_Left_Debit_Cust" runat="server" CssClass="textBox" TabIndex="85"
                                                                                    MaxLength="12" Width="100px" Enabled="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO3_Left_Debit_Cust_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="85" MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C CODE:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO3_Left_Debit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                                    TabIndex="86" MaxLength="5" Width="50px" Enabled="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO3_Left_Debit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="86" MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Left_Debit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                                    TabIndex="86" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" align="right">
                                                                                <span class="elementLabel">EXCH RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Left_Debit_Exch_Rate" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="87" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">EXCH CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Left_Debit_Exch_CCY" runat="server" CssClass="textBox" MaxLength="3"
                                                                                    TabIndex="87" Width="25px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">FUND:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Left_Debit_FUND" runat="server" CssClass="textBox" TabIndex="88"
                                                                                    Width="20px" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CHECK No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Left_Debit_Check_No" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    Width="50px" TabIndex="88" MaxLength="6" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AVAILABLE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Left_Debit_Available" runat="server" CssClass="textBox"
                                                                                    TabIndex="88" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ADVICE PRINT:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO3_Left_Debit_AdPrint" runat="server" CssClass="textBox" TabIndex="89"
                                                                                    Width="20px" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                                <span class="elementLabel">DETAILS:</span>
                                                                                <asp:TextBox ID="txt_GO3_Left_Debit_Details" runat="server" CssClass="textBox" TabIndex="89"
                                                                                    MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ENTITY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Left_Debit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="89" Width="90px" Style="text-align: right" MaxLength="3" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DIVISION:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Left_Debit_Division" runat="server" CssClass="textBox" TabIndex="90"
                                                                                    MaxLength="2" Width="20px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Left_Debit_Inter_Amount" onkeydown="return validate_Number(event);"
                                                                                    runat="server" Width="100px" MaxLength="16" Style="text-align: right" CssClass="textBox"
                                                                                    TabIndex="90" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Left_Debit_Inter_Rate" runat="server" CssClass="textBox"
                                                                                    TabIndex="90" Width="100px" MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                                    Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <br />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DEBIT / CREDIT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Left_Credit_Code" runat="server" CssClass="textBox" TabIndex="91"
                                                                                    MaxLength="1" Width="20px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Left_Credit_Curr" runat="server" CssClass="textBox" TabIndex="91"
                                                                                    MaxLength="3" Width="25px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Left_Credit_Amt" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="91" Width="90px" Style="text-align: right" MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CUSTOMER:</span>
                                                                            </td>
                                                                            <td align="left" colspan="4">
                                                                                <asp:TextBox ID="txt_GO3_Left_Credit_Cust" runat="server" CssClass="textBox" TabIndex="92"
                                                                                    MaxLength="12" Width="100px" Enabled="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO3_Left_Credit_Cust_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="92" MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C CODE:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO3_Left_Credit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                                    TabIndex="93" MaxLength="5" Width="50px" Enabled="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO3_Left_Credit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="93" MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Left_Credit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                                    TabIndex="93" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" align="right">
                                                                                <span class="elementLabel">EXCH RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Left_Credit_Exch_Rate" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="94" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">EXCH CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Left_Credit_Exch_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="94" Width="25px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">FUND:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Left_Credit_FUND" runat="server" CssClass="textBox" TabIndex="95"
                                                                                    MaxLength="1" Width="20px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CHECK No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Left_Credit_Check_No" runat="server" CssClass="textBox"
                                                                                    TabIndex="95" onkeydown="return validate_Number(event);" Width="50px" MaxLength="6"
                                                                                    Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AVAILABLE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Left_Credit_Available" runat="server" CssClass="textBox"
                                                                                    TabIndex="95" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ADVICE PRINT:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO3_Left_Credit_AdPrint" runat="server" CssClass="textBox" TabIndex="96"
                                                                                    Width="20px" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                                <span class="elementLabel">DETAILS:</span>
                                                                                <asp:TextBox ID="txt_GO3_Left_Credit_Details" runat="server" CssClass="textBox" TabIndex="96"
                                                                                    Width="300px" MaxLength="30" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ENTITY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Left_Credit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="96" Width="90px" Style="text-align: right" MaxLength="3" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DIVISION:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Left_Credit_Division" runat="server" CssClass="textBox"
                                                                                    TabIndex="97" Width="20px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Left_Credit_Inter_Amount" onkeydown="return validate_Number(event);"
                                                                                    runat="server" CssClass="textBox" TabIndex="97" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Left_Credit_Inter_Rate" onkeydown="return validate_Number(event);"
                                                                                    runat="server" CssClass="textBox" TabIndex="97" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </asp:Panel>
                                                                </table>
                                                            </td>
                                                            <td align="left" width="50%">
                                                                <table width="100%">
                                                                    <asp:Panel ID="Panel_GO3Right" runat="server" Visible="false">
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">COMMENT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Right_Comment" runat="server" CssClass="textBox" TabIndex="101"
                                                                                    MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">Section No:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO3_Right_SectionNo" runat="server" CssClass="textBox" TabIndex="102"
                                                                                    MaxLength="2" Width="20px" Enabled="false"></asp:TextBox>
                                                                                <span class="elementLabel">Remarks:</span>
                                                                                <asp:TextBox ID="txt_GO3_Right_Remarks" runat="server" CssClass="textBox" Width="300px"
                                                                                    MaxLength="30" TabIndex="102" onblur="return Toggel_GO3_Right_Remarks();" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">MEMO:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Right_Memo" runat="server" CssClass="textBox" Width="50px"
                                                                                    TabIndex="102" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">SCHEME No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Right_Scheme_no" runat="server" CssClass="textBox" TabIndex="103"
                                                                                    MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" width="10%">
                                                                                <span class="elementLabel">DEBIT / CREDIT:</span>
                                                                            </td>
                                                                            <td align="left" width="24%">
                                                                                <asp:TextBox ID="txt_GO3_Right_Debit_Code" runat="server" CssClass="textBox" TabIndex="104"
                                                                                    Width="20px" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right" width="6%">
                                                                                <span class="elementLabel">CCY:</span>
                                                                            </td>
                                                                            <td align="left" width="34%">
                                                                                <asp:TextBox ID="txt_GO3_Right_Debit_Curr" runat="server" CssClass="textBox" TabIndex="104"
                                                                                    Width="25px" MaxLength="3" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right" width="6%">
                                                                                <span class="elementLabel">AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left" width="20%">
                                                                                <asp:TextBox ID="txt_GO3_Right_Debit_Amt" runat="server" CssClass="textBox" Width="90px"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="104" Style="text-align: right"
                                                                                    MaxLength="16" onblur="return Toggel_Debit_Amt();" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CUSTOMER:</span>
                                                                            </td>
                                                                            <td align="left" colspan="4">
                                                                                <asp:TextBox ID="txt_GO3_Right_Debit_Cust" runat="server" CssClass="textBox" TabIndex="105"
                                                                                    MaxLength="12" Width="100px" Enabled="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO3_Right_Debit_Cust_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="105" MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C CODE:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO3_Right_Debit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                                    TabIndex="105" MaxLength="5" Width="50px" Enabled="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO3_Right_Debit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="105" MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Right_Debit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                                    TabIndex="105" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" align="right">
                                                                                <span class="elementLabel">EXCH RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Right_Debit_Exch_Rate" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="106" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">EXCH CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Right_Debit_Exch_CCY" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="106" Width="25px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">FUND:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Right_Debit_FUND" runat="server" CssClass="textBox" TabIndex="107"
                                                                                    Width="20px" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CHECK No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Right_Debit_Check_No" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="107" MaxLength="6" Width="50px"
                                                                                    Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AVAILABLE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Right_Debit_Available" runat="server" CssClass="textBox"
                                                                                    TabIndex="107" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ADVICE PRINT:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO3_Right_Debit_AdPrint" runat="server" CssClass="textBox" TabIndex="108"
                                                                                    Width="20px" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                                <span class="elementLabel">DETAILS:</span>
                                                                                <asp:TextBox ID="txt_GO3_Right_Debit_Details" runat="server" CssClass="textBox" TabIndex="108"
                                                                                    MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ENTITY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Right_Debit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="108" Width="90px" Style="text-align: right" MaxLength="3" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DIVISION:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Right_Debit_Division" runat="server" CssClass="textBox"
                                                                                    TabIndex="109" MaxLength="2" Width="20px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Right_Debit_Inter_Amount" onkeydown="return validate_Number(event);"
                                                                                    runat="server" Width="100px" MaxLength="16" Style="text-align: right" CssClass="textBox"
                                                                                    TabIndex="109" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Right_Debit_Inter_Rate" runat="server" CssClass="textBox"
                                                                                    TabIndex="109" Width="100px" MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"
                                                                                    Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <br />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DEBIT / CREDIT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Right_Credit_Code" runat="server" CssClass="textBox" TabIndex="110"
                                                                                    MaxLength="1" Width="20px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Right_Credit_Curr" runat="server" CssClass="textBox" TabIndex="110"
                                                                                    MaxLength="3" Width="25px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Right_Credit_Amt" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="110" Width="90px" Style="text-align: right" MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CUSTOMER:</span>
                                                                            </td>
                                                                            <td align="left" colspan="4">
                                                                                <asp:TextBox ID="txt_GO3_Right_Credit_Cust" runat="server" CssClass="textBox" TabIndex="111"
                                                                                    MaxLength="12" Width="100px" Enabled="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO3_Right_Credit_Cust_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="111" MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C CODE:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO3_Right_Credit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                                    TabIndex="112" MaxLength="5" Width="50px" Enabled="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO3_Right_Credit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="112" MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Right_Credit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                                    TabIndex="112" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" align="right">
                                                                                <span class="elementLabel">EXCH RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Right_Credit_Exch_Rate" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="113" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">EXCH CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Right_Credit_Exch_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="113" Width="25px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">FUND:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Right_Credit_FUND" runat="server" CssClass="textBox" TabIndex="114"
                                                                                    MaxLength="1" Width="20px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CHECK No: </span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Right_Credit_Check_No" runat="server" CssClass="textBox"
                                                                                    TabIndex="114" onkeydown="return validate_Number(event);" Width="50px" MaxLength="6"
                                                                                    Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AVAILABLE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Right_Credit_Available" runat="server" CssClass="textBox"
                                                                                    TabIndex="114" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ADVICE PRINT:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO3_Right_Credit_AdPrint" runat="server" CssClass="textBox"
                                                                                    TabIndex="115" Width="20px" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                                <span class="elementLabel">DETAILS:</span>
                                                                                <asp:TextBox ID="txt_GO3_Right_Credit_Details" runat="server" CssClass="textBox"
                                                                                    TabIndex="115" Width="300px" MaxLength="30" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ENTITY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Right_Credit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="115" Width="90px" Style="text-align: right" MaxLength="3" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DIVISION:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Right_Credit_Division" runat="server" CssClass="textBox"
                                                                                    TabIndex="116" Width="20px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Right_Credit_Inter_Amount" onkeydown="return validate_Number(event);"
                                                                                    runat="server" CssClass="textBox" TabIndex="116" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO3_Right_Credit_Inter_Rate" onkeydown="return validate_Number(event);"
                                                                                    runat="server" CssClass="textBox" TabIndex="116" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </asp:Panel>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="10%">
                                                            </td>
                                                            <td align="left" width="90%">
                                                                <asp:Button ID="btn_GO3_Prev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                                    ToolTip="Back to GENERAL OPERATION II" TabIndex="117" OnClientClick="return GeneralOperationNextClick(1);" />&nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btn_GO3_Next" runat="server" Text="Next >>" CssClass="buttonDefault"
                                                                    ToolTip="Go to GENERAL OPERATION SWIFT" TabIndex="117" OnClientClick="return GO3_NextClick();" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                            <ajaxToolkit:TabPanel ID="tbDocumentGOAccChange" runat="server" HeaderText="GENERAL OPERATION FOR INTER-OFFICE"
                                                Font-Bold="true" ForeColor="White" Enabled="false">
                                                <ContentTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center" width="50%">
                                                                <asp:CheckBox Enabled="false" ID="chk_GOAcccChangeFlag" Text="GENERAL OPERATION FOR INTER-OFFICE"
                                                                    CssClass="elementLabel" OnCheckedChanged="chk_GOAcccChangeFlag_Flag_OnCheckedChanged"
                                                                    runat="server" />
                                                            </td>
                                                            <td width="50%">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="left" width="50%">
                                                                <table width="100%">
                                                                    <asp:Panel ID="panal_GOAccChange" runat="server" Visible="false">
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">REFERENCE NO:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Ref_No" Width="100px" runat="server" TabIndex="222"
                                                                                    CssClass="textBox" MaxLength="14" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">COMMENT:</span>
                                                                            </td>
                                                                            <td colspan="3" align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Comment" runat="server" CssClass="textBox" TabIndex="223"
                                                                                    Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">Section No:</span>
                                                                            </td>
                                                                            <td colspan="3" align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_SectionNo" runat="server" CssClass="textBox" TabIndex="224"
                                                                                    MaxLength="2" Width="20px" Enabled="false"></asp:TextBox>
                                                                                <span class="elementLabel">Remarks:</span>
                                                                                <asp:TextBox ID="txt_GOAccChange_Remarks" runat="server" Width="300px" CssClass="textBox"
                                                                                    MaxLength="30" TabIndex="225" onblur="return Toggel_GOAccChange_Remarks();" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">MEMO:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Memo" runat="server" CssClass="textBox" Width="50px"
                                                                                    TabIndex="226" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">SCHEME No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Scheme_no" runat="server" CssClass="textBox" TabIndex="227"
                                                                                    MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DEBIT / CREDIT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Code" runat="server" CssClass="textBox" TabIndex="228"
                                                                                    MaxLength='1' Width="20px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Curr" runat="server" CssClass="textBox" TabIndex="229"
                                                                                    MaxLength='3' Width="25px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Amt" runat="server" CssClass="textBox" Width="100px"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="230" Style="text-align: right"
                                                                                    MaxLength="16" onblur="return Toggel_txt_GOAccChange_Debit_Amt();" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CUSTOMER:</span>
                                                                            </td>
                                                                            <td colspan="4" align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Cust" runat="server" CssClass="textBox" TabIndex="231"
                                                                                    MaxLength='12' Width="100px" Enabled="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Cust_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="231" MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C CODE:</span>
                                                                            </td>
                                                                            <td colspan="3" align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                                    TabIndex="232" MaxLength='5' Width="50px" Enabled="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                                                    MaxLength="40" TabIndex="232" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                                    TabIndex="233" MaxLength='20' Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" align="right">
                                                                                <span class="elementLabel">EXCH RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Exch_Rate" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="234" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">EXCH CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Exch_CCY" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="235" Width="25px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">FUND:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_FUND" runat="server" CssClass="textBox" TabIndex="236"
                                                                                    MaxLength="1" Width="20px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CHECK No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Check_No" runat="server" CssClass="textBox"
                                                                                    TabIndex="237" onkeydown="return validate_Number(event);" MaxLength="6" Width="50px"
                                                                                    Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AVAILABLE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Available" runat="server" CssClass="textBox"
                                                                                    TabIndex="238" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ADVICE PRINT:</span>
                                                                            </td>
                                                                            <td colspan="3" align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_AdPrint" runat="server" CssClass="textBox"
                                                                                    TabIndex="239" Width="20px" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                                <span class="elementLabel">DETAILS:</span>
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Details" runat="server" CssClass="textBox"
                                                                                    TabIndex="240" Width="300px" MaxLength="30" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ENTITY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Entity" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="241" Width="90px" Style="text-align: right"
                                                                                    MaxLength="3" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DIVISION:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Division" runat="server" CssClass="textBox"
                                                                                    TabIndex="242" MaxLength="2" Width="20px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Inter_Amount" onkeydown="return validate_Number(event);"
                                                                                    MaxLength="16" Width="100px" Style="text-align: right" runat="server" CssClass="textBox"
                                                                                    TabIndex="243" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Debit_Inter_Rate" onkeydown="return validate_Number(event);"
                                                                                    MaxLength="16" runat="server" CssClass="textBox" TabIndex="244" Width="100px"
                                                                                    Style="text-align: right" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <br />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DEBIT / CREDIT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Code" runat="server" CssClass="textBox" TabIndex="245"
                                                                                    MaxLength='1' Width="20px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Curr" runat="server" CssClass="textBox" TabIndex="246"
                                                                                    Width="25px" MaxLength="3" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Amt" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="247" Width="100px" Style="text-align: right" MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CUSTOMER:</span>
                                                                            </td>
                                                                            <td colspan="4" align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Cust" runat="server" CssClass="textBox" TabIndex="248"
                                                                                    MaxLength='12' Width="100px" Enabled="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Cust_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="248" MaxLength="40" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C CODE:</span>
                                                                            </td>
                                                                            <td colspan="3" align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                                    TabIndex="249" MaxLength='5' Width="50px" Enabled="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                                                    MaxLength="40" TabIndex="249" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                                    TabIndex="250" MaxLength='20' Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" align="right">
                                                                                <span class="elementLabel">EXCH RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Exch_Rate" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="251" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">EXCH CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Exch_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="252" Width="25px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">FUND:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_FUND" runat="server" CssClass="textBox" TabIndex="253"
                                                                                    MaxLength="1" Width="20px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CHECK No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Check_No" runat="server" CssClass="textBox"
                                                                                    TabIndex="254" onkeydown="return validate_Number(event);" MaxLength="6" Width="50px"
                                                                                    Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AVAILABLE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Available" runat="server" CssClass="textBox"
                                                                                    TabIndex="255" MaxLength="20" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ADVICE PRINT:</span>
                                                                            </td>
                                                                            <td colspan="3" align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_AdPrint" runat="server" CssClass="textBox"
                                                                                    TabIndex="256" Width="20px" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                                <span class="elementLabel">DETAILS:</span>
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Details" runat="server" CssClass="textBox"
                                                                                    TabIndex="257" MaxLength="30" Width="300px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ENTITY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Entity" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="258" Width="90px" Style="text-align: right"
                                                                                    MaxLength="3" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DIVISION:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Division" runat="server" CssClass="textBox"
                                                                                    TabIndex="259" MaxLength="2" Width="20px" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Inter_Amount" runat="server" CssClass="textBox"
                                                                                    TabIndex="260" onkeydown="return validate_Number(event);" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GOAccChange_Credit_Inter_Rate" runat="server" CssClass="textBox"
                                                                                    TabIndex="261" onkeydown="return validate_Number(event);" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </asp:Panel>
                                                                </table>
                                                            </td>
                                                            <td align="left" width="50%">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="10%">
                                                            </td>
                                                            <td align="left" width="90%">
                                                                <asp:Button ID="btnGOAccChange_Prev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                                    ToolTip="Back to GENERAL OPERATION 3" TabIndex="262" OnClientClick="return GeneralOperationNextClick(2);" />
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btnGOAccChange_Next" runat="server" Text="Next >>" CssClass="buttonDefault"
                                                                    ToolTip="Go to GENERAL OPERATION SWIFT" TabIndex="117" OnClientClick="return SwiftNextClick(0);" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                        </ajaxToolkit:TabContainer>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel ID="tbSwift" runat="server" HeaderText="SWIFT" Font-Bold="true"
                                    ForeColor="White">
                                    <ContentTemplate>
                                        <ajaxToolkit:TabContainer ID="TabContainerSwift" runat="server" ActiveTabIndex="0"
                                            CssClass="ajax__tab_xp-theme">
                                            <ajaxToolkit:TabPanel ID="tbSwift103" runat="server" HeaderText="MT 103" Font-Bold="true"
                                                ForeColor="White">
                                                <ContentTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">Receiver:</span>
                                                            </td>
                                                            <td align="left" colspan="3">
                                                                <asp:TextBox ID="txt_MT103Receiver" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="100px" MaxLength="16" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[13C] Time Indication:</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtTimeIndication" runat="server" CssClass="textBox" TabIndex="238" Enabled="false"
                                                                    Width="100px" MaxLength="18"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <span class="elementLabel">[23B] Bank Operation Code:</span>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlBankOperationCode" runat="server" CssClass="dropdownList" Enabled="false">
                                                                    <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                                                    <asp:ListItem Text="CRED" Value="CRED"></asp:ListItem>
                                                                    <asp:ListItem Text="CRTS" Value="CRTS"></asp:ListItem>
                                                                    <asp:ListItem Text="SPAY" Value="SPAY"></asp:ListItem>
                                                                    <asp:ListItem Text="SPRI" Value="SPRI"></asp:ListItem>
                                                                    <asp:ListItem Text="SSTD" Value="SSTD"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[23E] Instruction Code : </span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtInstructionCode" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <span class="elementLabel">[26T] Transaction Type Code : </span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtTransactionTypeCode" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="30px" MaxLength="3" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[32A] Value Date / Currency Code / Amount : </span>
                                                            </td>
                                                            <td align="left" colspan="3">
                                                                <span class="elementLabel">Value Date : </span>
                                                                <asp:TextBox ID="txt103Date" runat="server" CssClass="textBox" TabIndex="238" Width="70px"
                                                                    MaxLength="10" Enabled="false"></asp:TextBox>
                                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date"
                                                                    runat="server" TargetControlID="txt103Date" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                    CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                    CultureTimePlaceholder=":">
                                                                </ajaxToolkit:MaskedEditExtender>
                                                                <span class="elementLabel">Currency Code : </span>
                                                                <asp:TextBox ID="txt103Currency" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="30px" MaxLength="3" Enabled="false"></asp:TextBox>
                                                                <span class="elementLabel">Amount : </span>
                                                                <asp:TextBox ID="txt103Amount" runat="server" CssClass="textBox" TabIndex="238" Width="150px"
                                                                    MaxLength="15" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[33B] Currency/Instructed Amount : </span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtCurrency" runat="server" CssClass="textBox" TabIndex="238" Width="30px"
                                                                    MaxLength="3" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txtInstructedAmount" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="150px" MaxLength="15" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <span class="elementLabel">[36] Exchange Rate : </span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtExchangeRate" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="120px" MaxLength="12" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[50A] Ordering Customer : </span>
                                                                <asp:DropDownList ID="ddlOrderingCustomer" runat="server" CssClass="dropdownList"
                                                                    Enabled="false">
                                                                    <asp:ListItem Text="50A" Value="A"></asp:ListItem>
                                                                    <asp:ListItem Text="50F" Value="F"></asp:ListItem>
                                                                    <asp:ListItem Text="50K" Value="K"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblOrderingCustomer_Acc" runat="server" CssClass="elementLabel" Text="A / C :"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtOrderingCustomer_Acc" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblOrderingCustomer_SwiftCode" runat="server" CssClass="elementLabel"
                                                                    Text="SwiftCode :"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtOrderingCustomer_SwiftCode" CssClass="textBox"
                                                                    MaxLength="11" Width="100px" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtOrderingCustomer_Name" CssClass="textBox" Visible="false"
                                                                    MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblOrderingCustomer_Addr1" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address1" Enabled="false"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtOrderingCustomer_Addr1" CssClass="textBox" Visible="false"
                                                                    MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblOrderingCustomer_Addr2" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address2" Enabled="false"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtOrderingCustomer_Addr2" CssClass="textBox" Visible="false"
                                                                    MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblOrderingCustomer_Addr3" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address3" Enabled="false"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtOrderingCustomer_Addr3" CssClass="textBox" Visible="false"
                                                                    MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[51A] Sending Institution : </span>
                                                            </td>
                                                            <td align="left" colspan="3">
                                                                <asp:TextBox ID="txtSendingInstitutionAccountNumber" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txtSendingInstitutionSwiftCode" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="11" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[52A] Ordering Institution : </span>
                                                                <asp:DropDownList ID="ddlOrderingInstitution" runat="server" CssClass="dropdownList"
                                                                    Enabled="false">
                                                                    <asp:ListItem Text="52A" Value="A"></asp:ListItem>
                                                                    <asp:ListItem Text="52D" Value="D"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span class="elementLabel">A / C :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtOrderingInstitutionAccountNumber" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <span class="elementLabel">[53A] Sender's Correspondent : </span>
                                                                <asp:DropDownList ID="ddlSendersCorrespondent" runat="server" CssClass="dropdownList"
                                                                    Enabled="false">
                                                                    <asp:ListItem Text="53A" Value="A"></asp:ListItem>
                                                                    <asp:ListItem Text="53B" Value="B"></asp:ListItem>
                                                                    <asp:ListItem Text="53D" Value="D"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span class="elementLabel">A / C :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtSendersCorrespondentAccountNumber" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblOrderingInstitutionSwiftCode" runat="server" CssClass="elementLabel"
                                                                    Text="SwiftCode :"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtOrderingInstitutionSwiftCode" CssClass="textBox"
                                                                    MaxLength="11" Width="100px" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtOrderingInstitutionName" CssClass="textBox" Visible="false"
                                                                    MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblSendersCorrespondentSwiftCode" runat="server" CssClass="elementLabel"
                                                                    Text="SwiftCode :"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtSendersCorrespondentSwiftCode" CssClass="textBox"
                                                                    MaxLength="11" Width="100px" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtSendersCorrespondentName" CssClass="textBox" Visible="false"
                                                                    MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtSendersCorrespondentLocation" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblOrderingInstitutionAddress1" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address1"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtOrderingInstitutionAddress1" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblSendersCorrespondentAddress1" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address1"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtSendersCorrespondentAddress1" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblOrderingInstitutionAddress2" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address2"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtOrderingInstitutionAddress2" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblSendersCorrespondentAddress2" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address2"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtSendersCorrespondentAddress2" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblOrderingInstitutionAddress3" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address3"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtOrderingInstitutionAddress3" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblSendersCorrespondentAddress3" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address3"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtSendersCorrespondentAddress3" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[54A] Receiver's Correspondent : </span>
                                                                <asp:DropDownList ID="ddlReceiversCorrespondent" runat="server" CssClass="dropdownList"
                                                                    Enabled="false">
                                                                    <asp:ListItem Text="54A" Value="A"></asp:ListItem>
                                                                    <asp:ListItem Text="54B" Value="B"></asp:ListItem>
                                                                    <asp:ListItem Text="54D" Value="D"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span class="elementLabel">A / C :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtReceiversCorrespondentAccountNumber" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <span class="elementLabel">[55A] Third Reimbursement Institution : </span>
                                                                <asp:DropDownList ID="ddlThirdReimbursementInstitution" runat="server" CssClass="dropdownList"
                                                                    Enabled="false">
                                                                    <asp:ListItem Text="55A" Value="A"></asp:ListItem>
                                                                    <asp:ListItem Text="55B" Value="B"></asp:ListItem>
                                                                    <asp:ListItem Text="55D" Value="D"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span class="elementLabel">A / C :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtThirdReimbursementInstitutionAccountNumber" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblReceiversCorrespondentSwiftCode" runat="server" CssClass="elementLabel"
                                                                    Text="SwiftCode :"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtReceiversCorrespondentSwiftCode" CssClass="textBox"
                                                                    MaxLength="11" Width="100px" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtReceiversCorrespondentName" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtReceiversCorrespondentLocation" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblThirdReimbursementInstitutionSwiftCode" runat="server" CssClass="elementLabel"
                                                                    Text="SwiftCode :"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtThirdReimbursementInstitutionSwiftCode" CssClass="textBox"
                                                                    MaxLength="11" Width="100px" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtThirdReimbursementInstitutionName" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtThirdReimbursementInstitutionLocation" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblReceiversCorrespondentAddress1" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address1"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtReceiversCorrespondentAddress1" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblThirdReimbursementInstitutionAddress1" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address1"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtThirdReimbursementInstitutionAddress1" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblReceiversCorrespondentAddress2" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address2"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtReceiversCorrespondentAddress2" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblThirdReimbursementInstitutionAddress2" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address2"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtThirdReimbursementInstitutionAddress2" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblReceiversCorrespondentAddress3" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address3"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtReceiversCorrespondentAddress3" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblThirdReimbursementInstitutionAddress3" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address3"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtThirdReimbursementInstitutionAddress3" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[56] Intermediary Institution : </span>
                                                                <asp:DropDownList ID="ddlIntermediary103" runat="server" CssClass="dropdownList"
                                                                    Enabled="false">
                                                                    <asp:ListItem Text="56A" Value="A"></asp:ListItem>
                                                                    <asp:ListItem Text="56C" Value="C"></asp:ListItem>
                                                                    <asp:ListItem Text="56D" Value="D"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span class="elementLabel">A / C :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtIntermediary103AccountNumber" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <span class="elementLabel">[57] Account With Institution : </span>
                                                                <asp:DropDownList ID="ddlAccountWithInstitution103" runat="server" CssClass="dropdownList"
                                                                    Enabled="false">
                                                                    <asp:ListItem Text="57A" Value="A"></asp:ListItem>
                                                                    <asp:ListItem Text="57B" Value="B"></asp:ListItem>
                                                                    <asp:ListItem Text="57C" Value="C"></asp:ListItem>
                                                                    <asp:ListItem Text="57D" Value="D"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span class="elementLabel">A / C :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtAccountWithInstitution103AccountNumber" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblIntermediary103SwiftCode" runat="server" CssClass="elementLabel"
                                                                    Text="SwiftCode :"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtIntermediary103SwiftCode" CssClass="textBox" MaxLength="11"
                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtIntermediary103Name" CssClass="textBox" Visible="false"
                                                                    MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblAccountWithInstitution103SwiftCode" runat="server" CssClass="elementLabel"
                                                                    Text="SwiftCode :"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtAccountWithInstitution103SwiftCode" CssClass="textBox"
                                                                    MaxLength="11" Width="100px" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtAccountWithInstitution103Name" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtAccountWithInstitution103Location" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblIntermediary103Address1" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address1"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtIntermediary103Address1" CssClass="textBox" Visible="false"
                                                                    MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblAccountWithInstitution103Address1" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address1"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtAccountWithInstitution103Address1" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblIntermediary103Address2" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address2"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtIntermediary103Address2" CssClass="textBox" Visible="false"
                                                                    MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblAccountWithInstitution103Address2" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address2"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtAccountWithInstitution103Address2" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblIntermediary103Address3" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address3"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtIntermediary103Address3" CssClass="textBox" Visible="false"
                                                                    MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblAccountWithInstitution103Address3" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address3"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtAccountWithInstitution103Address3" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[59A] Beneficiary Customer A/C : </span>
                                                                <asp:DropDownList ID="ddlBeneficiaryCustomer" runat="server" CssClass="dropdownList"
                                                                    Enabled="false">
                                                                    <asp:ListItem Text="59A" Value="A"></asp:ListItem>
                                                                    <asp:ListItem Text="59F" Value="F"></asp:ListItem>
                                                                    <asp:ListItem Text="No letter" Value="N"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span class="elementLabel">A / C :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtBeneficiaryCustomerAccountNumber" runat="server" CssClass="textBox"
                                                                    Enabled="false" TabIndex="238" Width="200px" MaxLength="35"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <span class="elementLabel">[71A] Details Of Charges : </span>
                                                                <span class="mandatoryField">Details of Charges must be one of BEN, OUR or SHA.</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtDetailsOfCharges" runat="server" CssClass="textBox" TabIndex="238" Enabled="false"
                                                                    Width="30px" MaxLength="3"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblBeneficiaryCustomerSwiftCode" runat="server" CssClass="elementLabel"
                                                                    Text="SwiftCode :"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtBeneficiaryCustomerSwiftCode" CssClass="textBox"
                                                                    MaxLength="11" Width="100px" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtBeneficiaryCustomerName" CssClass="textBox" Visible="false"
                                                                    MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblBeneficiaryCustomerAddr1" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address1"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtBeneficiaryCustomerAddr1" CssClass="textBox" Visible="false"
                                                                    MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblBeneficiaryCustomerAddr2" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address2"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtBeneficiaryCustomerAddr2" CssClass="textBox" Visible="false"
                                                                    MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblBeneficiaryCustomerAddr3" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address3"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtBeneficiaryCustomerAddr3" CssClass="textBox" Visible="false"
                                                                    MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[70] Remittance Information : </span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtRemittanceInformation1" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="300px" MaxLength="35"  Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtRemittanceInformation2" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="300px" MaxLength="35"  Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtRemittanceInformation3" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="300px" MaxLength="35"  Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtRemittanceInformation4" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="300px" MaxLength="35"  Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[71F] Sender's Charges : </span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtSenderCharges" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="30px" MaxLength="3" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txtSenderCharges2" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="150px" MaxLength="15" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <span class="elementLabel">[71G] Receiver's Charges : </span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtReceiverCharges" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="30px" MaxLength="3" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txtReceiverCharges2" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="150px" MaxLength="15" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[72] Sender to Receiver Information : </span>
                                                            </td>
                                                            <td align="left" colspan="3">
                                                                <asp:TextBox ID="txtSendertoReceiverInformation" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txtSendertoReceiverInformation2" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txtSendertoReceiverInformation3" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                            </td>
                                                            <td align="left" colspan="3">
                                                                <asp:TextBox ID="txtSendertoReceiverInformation4" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txtSendertoReceiverInformation5" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txtSendertoReceiverInformation6" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[77B] Regulatory Reporting : </span>
                                                            </td>
                                                            <td align="left" colspan="3">
                                                                <asp:TextBox ID="txtRegulatoryReporting" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txtRegulatoryReporting2" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txtRegulatoryReporting3" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                            <ajaxToolkit:TabPanel ID="tbSwift202" runat="server" HeaderText="MT 202" Font-Bold="true"
                                                ForeColor="White">
                                                <ContentTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[32a] Amount : </span>
                                                            </td>
                                                            <td align="left" colspan="3">
                                                                <asp:TextBox ID="txt202Amount" runat="server" CssClass="textBox" TabIndex="238" Width="150px"
                                                                    MaxLength="15" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[52] Ordering Institution : </span>
                                                                <asp:DropDownList ID="ddlOrderingInstitution202" runat="server" CssClass="dropdownList"
                                                                    Enabled="false">
                                                                    <asp:ListItem Text="52A" Value="A"></asp:ListItem>
                                                                    <asp:ListItem Text="52D" Value="D"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span class="elementLabel">A / C :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtOrderingInstitution202AccountNumber" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <span class="elementLabel">[53] Sender's Correspondent : </span>
                                                                <asp:DropDownList ID="ddlSendersCorrespondent202" runat="server" CssClass="dropdownList"
                                                                    Enabled="false">
                                                                    <asp:ListItem Text="53A" Value="A"></asp:ListItem>
                                                                    <asp:ListItem Text="53B" Value="B"></asp:ListItem>
                                                                    <asp:ListItem Text="53D" Value="D"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span class="elementLabel">A / C :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtSendersCorrespondent202AccountNumber" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblOrderingInstitution202SwiftCode" runat="server" CssClass="elementLabel"
                                                                    Text="SwiftCode :"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtOrderingInstitution202SwiftCode" CssClass="textBox"
                                                                    MaxLength="11" Width="100px" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtOrderingInstitution202Name" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblSendersCorrespondent202SwiftCode" runat="server" CssClass="elementLabel"
                                                                    Text="SwiftCode :"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtSendersCorrespondent202SwiftCode" CssClass="textBox"
                                                                    MaxLength="11" Width="100px" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtSendersCorrespondent202Name" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtSendersCorrespondent202Location" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblOrderingInstitution202Address1" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address 1"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtOrderingInstitution202Address1" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblSendersCorrespondent202Address1" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address 1"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtSendersCorrespondent202Address1" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblOrderingInstitution202Address2" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address 2"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtOrderingInstitution202Address2" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblSendersCorrespondent202Address2" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address 2"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtSendersCorrespondent202Address2" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblOrderingInstitution202Address3" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address 3"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtOrderingInstitution202Address3" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblSendersCorrespondent202Address3" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address 3"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtSendersCorrespondent202Address3" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[54] Receiver's Correspondent : </span>
                                                                <asp:DropDownList ID="ddlReceiversCorrespondent202" runat="server" CssClass="dropdownList"
                                                                    Enabled="false">
                                                                    <asp:ListItem Text="54A" Value="A"></asp:ListItem>
                                                                    <asp:ListItem Text="54B" Value="B"></asp:ListItem>
                                                                    <asp:ListItem Text="54D" Value="D"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span class="elementLabel">A / C :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtReceiversCorrespondent202AccountNumber" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <span class="elementLabel">[56] Intermediary : </span>
                                                                <asp:DropDownList ID="ddlIntermediary202" runat="server" CssClass="dropdownList"
                                                                    Enabled="false">
                                                                    <asp:ListItem Text="56A" Value="A"></asp:ListItem>
                                                                    <asp:ListItem Text="56D" Value="D"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span class="elementLabel">A / C :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtIntermediary202AccountNumber" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblReceiversCorrespondent202SwiftCode" runat="server" CssClass="elementLabel"
                                                                    Text="SwiftCode :"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtReceiversCorrespondent202SwiftCode" CssClass="textBox"
                                                                    MaxLength="11" Width="100px" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtReceiversCorrespondent202Name" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtReceiversCorrespondent202Location" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblIntermediary202SwiftCode" runat="server" CssClass="elementLabel"
                                                                    Text="SwiftCode :"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtIntermediary202SwiftCode" CssClass="textBox" MaxLength="11"
                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtIntermediary202Name" CssClass="textBox" Visible="false"
                                                                    MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblReceiversCorrespondent202Address1" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address 1"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtReceiversCorrespondent202Address1" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblIntermediary202Address1" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address 1"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtIntermediary202Address1" CssClass="textBox" Visible="false"
                                                                    MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblReceiversCorrespondent202Address2" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address 2"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtReceiversCorrespondent202Address2" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblIntermediary202Address2" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address 2"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtIntermediary202Address2" CssClass="textBox" Visible="false"
                                                                    MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblReceiversCorrespondent202Address3" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address 3"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtReceiversCorrespondent202Address3" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblIntermediary202Address3" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address 3"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtIntermediary202Address3" CssClass="textBox" Visible="false"
                                                                    MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[57] Account With Institution : </span>
                                                                <asp:DropDownList ID="ddlAccountWithInstitution202" runat="server" CssClass="dropdownList"
                                                                    Enabled="false">
                                                                    <asp:ListItem Text="57A" Value="A"></asp:ListItem>
                                                                    <asp:ListItem Text="57B" Value="B"></asp:ListItem>
                                                                    <asp:ListItem Text="57D" Value="D"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span class="elementLabel">A / C :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtAccountWithInstitution202AccountNumber" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <span class="elementLabel">[58] Beneficiary Institution : </span>
                                                                <asp:DropDownList ID="ddlBeneficiaryInstitution202" runat="server" CssClass="dropdownList"
                                                                    Enabled="false">
                                                                    <asp:ListItem Text="58A" Value="A"></asp:ListItem>
                                                                    <asp:ListItem Text="58D" Value="D"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span class="elementLabel">A / C :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtBeneficiaryInstitution202AccountNumber" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblAccountWithInstitution202SwiftCode" runat="server" CssClass="elementLabel"
                                                                    Text="SwiftCode :"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtAccountWithInstitution202SwiftCode" CssClass="textBox"
                                                                    MaxLength="11" Width="100px" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtAccountWithInstitution202Name" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtAccountWithInstitution202Location" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblBeneficiaryInstitution202SwiftCode" runat="server" CssClass="elementLabel"
                                                                    Text="SwiftCode :"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtBeneficiaryInstitution202SwiftCode" CssClass="textBox"
                                                                    MaxLength="11" Width="100px" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txtBeneficiaryInstitution202Name" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblAccountWithInstitution202Address1" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address 1"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtAccountWithInstitution202Address1" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblBeneficiaryInstitution202Address1" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address 1"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtBeneficiaryInstitution202Address1" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblAccountWithInstitution202Address2" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address 2"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtAccountWithInstitution202Address2" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblBeneficiaryInstitution202Address2" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address 2"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtBeneficiaryInstitution202Address2" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblAccountWithInstitution202Address3" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address 3"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtAccountWithInstitution202Address3" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblBeneficiaryInstitution202Address3" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address 3"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txtBeneficiaryInstitution202Address3" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[72] Sender To Receiver Information 1 : </span>
                                                            </td>
                                                            <td align="left" colspan="3">
                                                                <asp:TextBox runat="server" ID="txtSenderToReceiverInformation2021" CssClass="textBox"
                                                                    MaxLength="35" Width="300px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">2 : </span>
                                                            </td>
                                                            <td align="left" colspan="3">
                                                                <asp:TextBox runat="server" ID="txtSenderToReceiverInformation2022" CssClass="textBox"
                                                                    MaxLength="35" Width="300px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">3 : </span>
                                                            </td>
                                                            <td align="left" colspan="3">
                                                                <asp:TextBox runat="server" ID="txtSenderToReceiverInformation2023" CssClass="textBox"
                                                                    MaxLength="35" Width="300px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">4 : </span>
                                                            </td>
                                                            <td align="left" colspan="3">
                                                                <asp:TextBox runat="server" ID="txtSenderToReceiverInformation2024" CssClass="textBox"
                                                                    MaxLength="35" Width="300px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">5 : </span>
                                                            </td>
                                                            <td align="left" colspan="3">
                                                                <asp:TextBox runat="server" ID="txtSenderToReceiverInformation2025" CssClass="textBox"
                                                                    MaxLength="35" Width="300px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">6 : </span>
                                                            </td>
                                                            <td align="left" colspan="3">
                                                                <asp:TextBox runat="server" ID="txtSenderToReceiverInformation2026" CssClass="textBox"
                                                                    MaxLength="35" Width="300px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                            <ajaxToolkit:TabPanel ID="tbSwift200" runat="server" HeaderText="MT 200" Font-Bold="true"
                                                ForeColor="White">
                                                <ContentTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">Bic Code. : </span>
                                                            </td>
                                                            <td align="left" colspan="3">
                                                                <asp:TextBox ID="txt200BicCode" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="100px" MaxLength="16" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[20] Transaction Ref No. : </span>
                                                            </td>
                                                            <td align="left" colspan="3">
                                                                <asp:TextBox ID="txt200TransactionRefNO" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="160px" MaxLength="16" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[32A] Value Date / Currency Code / Amount : </span>
                                                            </td>
                                                            <td align="left" colspan="3">
                                                                <span class="elementLabel">Value Date : </span>
                                                                <asp:TextBox ID="txt200Date" runat="server" CssClass="textBox" TabIndex="238" Width="70px"
                                                                    MaxLength="10" Enabled="false"></asp:TextBox>
                                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                                                    runat="server" TargetControlID="txt200Date" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                    CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                    CultureTimePlaceholder=":">
                                                                </ajaxToolkit:MaskedEditExtender>
                                                                <span class="elementLabel">Currency Code : </span>
                                                                <asp:TextBox ID="txt200Currency" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="30px" MaxLength="3" Enabled="false"></asp:TextBox>
                                                                <span class="elementLabel">Amount : </span>
                                                                <asp:TextBox ID="txt200Amount" runat="server" CssClass="textBox" TabIndex="238" Width="150px"
                                                                    MaxLength="15" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[53B] Sender's Correspondent : </span>
                                                            </td>
                                                            <td align="left" colspan="3">
                                                                <asp:TextBox ID="txt200SenderCorreCode" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txt200SenderCorreLocation" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[56A] Intermediary : </span>
                                                                <asp:DropDownList ID="ddl200Intermediary" runat="server" CssClass="dropdownList"
                                                                    Enabled="false">
                                                                    <asp:ListItem Text="56A" Value="A"></asp:ListItem>
                                                                    <asp:ListItem Text="56D" Value="D"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span class="elementLabel">A / C :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt200IntermediaryAccountNumber" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lbl200IntermediarySwiftCode" runat="server" CssClass="elementLabel"
                                                                    Text="SwiftCode :"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txt200IntermediarySwiftCode" CssClass="textBox" MaxLength="11"
                                                                    Width="100px" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txt200IntermediaryName" CssClass="textBox" Visible="false"
                                                                    MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lbl200IntermediaryAddress1" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address1"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txt200IntermediaryAddress1" CssClass="textBox" Visible="false"
                                                                    MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lbl200IntermediaryAddress2" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address2"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txt200IntermediaryAddress2" CssClass="textBox" Visible="false"
                                                                    MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lbl200IntermediaryAddress3" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address3"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txt200IntermediaryAddress3" CssClass="textBox" Visible="false"
                                                                    MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[57A] Account With Institution : </span>
                                                                <asp:DropDownList ID="ddl200AccWithInstitution" runat="server" CssClass="dropdownList"
                                                                    Enabled="false">
                                                                    <asp:ListItem Text="57A" Value="A"></asp:ListItem>
                                                                    <asp:ListItem Text="57B" Value="B"></asp:ListItem>
                                                                    <asp:ListItem Text="57D" Value="D"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span class="elementLabel">A / C :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt200AccWithInstitutionAccountNumber" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lbl200AccWithInstitutionSwiftCode" runat="server" CssClass="elementLabel"
                                                                    Text="SwiftCode :"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txt200AccWithInstitutionSwiftCode" CssClass="textBox"
                                                                    MaxLength="11" Width="100px" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txt200AccWithInstitutionLocation" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox runat="server" ID="txt200AccWithInstitutionName" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lbl200AccWithInstitutionAddress1" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address1"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txt200AccWithInstitutionAddress1" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lbl200AccWithInstitutionAddress2" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address2"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txt200AccWithInstitutionAddress2" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lbl200AccWithInstitutionAddress3" runat="server" CssClass="elementLabel"
                                                                    Visible="false" Text="Address3"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox runat="server" ID="txt200AccWithInstitutionAddress3" CssClass="textBox"
                                                                    Visible="false" MaxLength="35" Width="250px" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[72] Sender to Receiver Information : </span>
                                                            </td>
                                                            <td align="left" colspan="3">
                                                                <asp:TextBox ID="txt200SendertoReceiverInformation1" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txt200SendertoReceiverInformation2" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txt200SendertoReceiverInformation3" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                            </td>
                                                            <td align="left" colspan="3">
                                                                <asp:TextBox ID="txt200SendertoReceiverInformation4" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txt200SendertoReceiverInformation5" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                                <asp:TextBox ID="txt200SendertoReceiverInformation6" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                            <ajaxToolkit:TabPanel ID="tbSwiftR42" runat="server" HeaderText="R 42" Font-Bold="true"
                                                ForeColor="White">
                                                <ContentTemplate>
                                                    <table width="50%">
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[2020] Transaction Ref No : </span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTransactionRefNoR42" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="110px" MaxLength="14" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[2006] Related Reference : </span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtRelatedReferenceR42" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="125px" MaxLength="16" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[4488] Value Date/Currency/Amount : </span>
                                                            </td>
                                                            <td>
                                                                <span class="elementLabel">Date</span><asp:TextBox ID="txtValueDateR42" runat="server"
                                                                    CssClass="textBox" TabIndex="238" Width="80px" MaxLength="10" Enabled="false"></asp:TextBox>
                                                                <span class="elementLabel">Currency</span><asp:TextBox ID="txtCureencyR42" runat="server"
                                                                    CssClass="textBox" TabIndex="238" Width="25px" MaxLength="3" Enabled="false"></asp:TextBox>
                                                                <span class="elementLabel">Amount</span><asp:TextBox ID="txtAmountR42" runat="server"
                                                                    CssClass="textBox" TabIndex="238" Width="110px" MaxLength="16" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[5517] Ordering Institution IFSC : </span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtOrderingInstitutionIFSCR42" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="90px" MaxLength="11" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[6521] Beneficiary Institution IFSC : </span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtBeneficiaryInstitutionIFSCR42" runat="server" CssClass="textBox"
                                                                    TabIndex="238" Width="90px" MaxLength="11" Enabled="false"></asp:TextBox>
                                                                <span class="mandatoryField">To update Receiver information modify this texbox.
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[7495] Sender to Receiver Info : </span>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">Code Word </span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCodeWordR42" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="25px" MaxLength="3" Text="TRF" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">Additional Information </span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtAdditionalInformationR42" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="500px" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">More Info </span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMoreInfo1R42" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="500px" MaxLength="50" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMoreInfo2R42" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="500px" MaxLength="50" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMoreInfo3R42" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="500px" MaxLength="50" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMoreInfo4R42" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="500px" MaxLength="50" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMoreInfo5R42" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="300px" MaxLength="35" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                            <ajaxToolkit:TabPanel ID="tbSwiftMT754" runat="server" HeaderText="MT 754" Font-Bold="true"
                                                ForeColor="White">
                                                <ContentTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel" style="color: Red; font-weight: bold;">[20] Sender's Reference:</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_754_SenRef" runat="server" MaxLength="16" CssClass="textBox"
                                                                    TabIndex="1" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <span class="elementLabel" style="color: Red; font-weight: bold;">[21] Related Reference:</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_754_RelRef" runat="server" CssClass="textBox" MaxLength="16"
                                                                    TabIndex="2" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel" style="color: Red; font-weight: bold;">[32A] Principal Amount
                                                                    Paid: </span>
                                                                <asp:DropDownList ID="ddlPrinAmtPaidAccNego_754" runat="server" CssClass="dropdownList"
                                                                    Enabled="false">
                                                                    <asp:ListItem Text="32A" Value="A"></asp:ListItem>
                                                                    <asp:ListItem Text="32B" Value="B"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label runat="server" ID="lbl_rinAmtPaidAccNegoDate_754" CssClass="elementLabel"
                                                                    Text="Date:"></asp:Label>
                                                                <asp:TextBox ID="txtPrinAmtPaidAccNegoDate_754" runat="server" CssClass="textBox"
                                                                    Width="60px" MaxLength="10" TabIndex="2" Enabled="false"></asp:TextBox>
                                                                <span class="elementLabel">CCY:</span>
                                                                <asp:TextBox ID="txtPrinAmtPaidAccNegoCurr_754" runat="server" CssClass="textBox"
                                                                    MaxLength="3" Width="35px" TabIndex="2" Enabled="false"></asp:TextBox>
                                                                <span class="elementLabel">Amt:</span>
                                                                <asp:TextBox ID="txtPrinAmtPaidAccNegoAmt_754" runat="server" CssClass="textBox"
                                                                    MaxLength="15" Width="115px" TabIndex="2" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <span class="elementLabel">[33B] Additional Amounts : </span>
                                                            </td>
                                                            <td>
                                                                <span class="elementLabel">CCY:</span>
                                                                <asp:TextBox ID="txt_754_AddAmtClamd_Ccy" runat="server" CssClass="textBox" Width="35px"
                                                                    MaxLength="3" TabIndex="2" Enabled="false"></asp:TextBox>
                                                                <span class="elementLabel">Amt:</span>
                                                                <asp:TextBox ID="txt_754_AddAmtClamd_Amt" runat="server" CssClass="textBox" MaxLength="15"
                                                                    Width="95px" TabIndex="2" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">[71D] Charges Deducted : </span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_754_ChargesDeduct1" runat="server" CssClass="textBox" Width="330px"
                                                                    MaxLength="35" TabIndex="3" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <span class="elementLabel">[73A] Charges Added: </span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_754_ChargesAdded1" runat="server" CssClass="textBox" Width="330px"
                                                                    MaxLength="35" TabIndex="4" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <tr>
                                                                <td align="right">
                                                                    <span></span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_754_ChargesDeduct2" runat="server" CssClass="textBox" Width="330px"
                                                                        MaxLength="35" TabIndex="3" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <span class="elementLabel"></span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_754_ChargesAdded2" runat="server" CssClass="textBox" Width="330px"
                                                                        MaxLength="35" TabIndex="4" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span></span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_754_ChargesDeduct3" runat="server" CssClass="textBox" Width="330px"
                                                                        MaxLength="35" TabIndex="3" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <span></span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_754_ChargesAdded3" runat="server" CssClass="textBox" Width="330px"
                                                                        MaxLength="35" TabIndex="4" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span></span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_754_ChargesDeduct4" runat="server" CssClass="textBox" Width="330px"
                                                                        MaxLength="35" TabIndex="3" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <span></span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_754_ChargesAdded4" runat="server" CssClass="textBox" Width="330px"
                                                                        MaxLength="35" TabIndex="4" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span></span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_754_ChargesDeduct5" runat="server" CssClass="textBox" Width="330px"
                                                                        MaxLength="35" TabIndex="3" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <span></span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_754_ChargesAdded5" runat="server" CssClass="textBox" Width="330px"
                                                                        MaxLength="35" TabIndex="4" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span></span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_754_ChargesDeduct6" runat="server" CssClass="textBox" Width="330px"
                                                                        MaxLength="35" TabIndex="3" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <span></span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_754_ChargesAdded6" runat="server" CssClass="textBox" Width="330px"
                                                                        MaxLength="35" TabIndex="4" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">[34A] Total Amount Claimed : </span>
                                                                    <asp:DropDownList ID="ddlTotalAmtclamd_754" runat="server" CssClass="dropdownList"
                                                                        Enabled="false" TabIndex="5">
                                                                        <asp:ListItem Text="34A" Value="A"></asp:ListItem>
                                                                        <asp:ListItem Text="34B" Value="B"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:Label runat="server" ID="lbl_754_TotalAmtClmd_Date" CssClass="elementLabel"
                                                                        Text="Date:"></asp:Label>
                                                                    <asp:TextBox ID="txt_754_TotalAmtClmd_Date" runat="server" CssClass="textBox" Width="60px"
                                                                        MaxLength="10" TabIndex="5" Enabled="false"></asp:TextBox>
                                                                    <span class="elementLabel">CCY:</span>
                                                                    <asp:TextBox ID="txt_754_TotalAmtClmd_Ccy" runat="server" CssClass="textBox" MaxLength="3"
                                                                        Width="35px" TabIndex="5" Enabled="false"></asp:TextBox>
                                                                    <span class="elementLabel">Amt:</span>
                                                                    <asp:TextBox ID="txt_754_TotalAmtClmd_Amt" runat="server" CssClass="textBox" MaxLength="15"
                                                                        Width="115px" TabIndex="5" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">[53A] Reimbursing Bank : </span>
                                                                    <asp:DropDownList ID="ddlReimbursingbank_754" runat="server" CssClass="dropdownList"
                                                                        TabIndex="6" Enabled="false">
                                                                        <asp:ListItem Text="53A" Value="A"></asp:ListItem>
                                                                        <asp:ListItem Text="53B" Value="B"></asp:ListItem>
                                                                        <asp:ListItem Text="53D" Value="D"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <span class="elementLabel">Party Identifier : </span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtReimbursingBankAccountnumber_754" runat="server" CssClass="textBox"
                                                                        TabIndex="6" Width="20px" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                    <asp:TextBox ID="txtReimbursingBankpartyidentifier_754" runat="server" CssClass="textBox"
                                                                        TabIndex="6" Width="330px" MaxLength="34" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblReimbursingBankIdentifier_754" runat="server" CssClass="elementLabel"
                                                                        Text="Identifier Code :"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox runat="server" ID="txtReimbursingBankIdentifiercode_754" CssClass="textBox"
                                                                        MaxLength="11" TabIndex="6" Width="100px" Enabled="false"></asp:TextBox>
                                                                    <asp:TextBox runat="server" ID="txtReimbursingBankLocation_754" CssClass="textBox"
                                                                        Visible="false" MaxLength="35" TabIndex="6" Width="330px" Enabled="false"></asp:TextBox>
                                                                    <asp:TextBox runat="server" ID="txtReimbursingBankName_754" CssClass="textBox" Visible="false"
                                                                        MaxLength="35" TabIndex="6" Width="330px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblReimbursingBankAddress1_754" runat="server" CssClass="elementLabel"
                                                                        Visible="false" Text="Address1"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox runat="server" ID="txtReimbursingBankAddress1_754" CssClass="textBox"
                                                                        Visible="false" TabIndex="6" MaxLength="35" Width="330px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblReimbursingBankAddress2_754" runat="server" CssClass="elementLabel"
                                                                        Visible="false" Text="Address2"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox runat="server" ID="txtReimbursingBankAddress2_754" CssClass="textBox"
                                                                        Visible="false" TabIndex="6" MaxLength="35" Width="330px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblReimbursingBankAddress3_754" runat="server" CssClass="elementLabel"
                                                                        Visible="false" Text="Address3"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox runat="server" ID="txtReimbursingBankAddress3_754" CssClass="textBox"
                                                                        Visible="false" TabIndex="6" MaxLength="35" Width="330px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">[57A] Account With Bank : </span>
                                                                    <asp:DropDownList ID="ddlAccountwithbank_754" runat="server" CssClass="dropdownList"
                                                                        TabIndex="9" Enabled="false">
                                                                        <asp:ListItem Text="57A" Value="A"></asp:ListItem>
                                                                        <asp:ListItem Text="57B" Value="B"></asp:ListItem>
                                                                        <asp:ListItem Text="57D" Value="D"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <span class="elementLabel">Party Identifier : </span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtAccountwithBankAccountnumber_754" runat="server" CssClass="textBox"
                                                                        TabIndex="9" Width="20px" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                    <asp:TextBox ID="txtAccountwithBankpartyidentifier_754" runat="server" CssClass="textBox"
                                                                        TabIndex="9" Width="330px" MaxLength="34" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblAccountwithBankIdentifier_754" runat="server" CssClass="elementLabel"
                                                                        Text="Identifier Code :"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox runat="server" ID="txtAccountwithBankIdentifiercode_754" CssClass="textBox"
                                                                        MaxLength="11" TabIndex="9" Width="100px" Enabled="false"></asp:TextBox>
                                                                    <asp:TextBox runat="server" ID="txtAccountwithBankLocation_754" CssClass="textBox"
                                                                        Visible="false" MaxLength="35" TabIndex="9" Width="330px" Enabled="false"></asp:TextBox>
                                                                    <asp:TextBox runat="server" ID="txtAccountwithBankName_754" CssClass="textBox" Visible="false"
                                                                        MaxLength="35" TabIndex="9" Width="330px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblAccountwithBankAddress1_754" runat="server" CssClass="elementLabel"
                                                                        Visible="false" Text="Address1"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox runat="server" ID="txtAccountwithBankAddress1_754" CssClass="textBox"
                                                                        Visible="false" TabIndex="9" MaxLength="35" Width="330px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblAccountwithBankAddress2_754" runat="server" CssClass="elementLabel"
                                                                        Visible="false" Text="Address2"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox runat="server" ID="txtAccountwithBankAddress2_754" CssClass="textBox"
                                                                        Visible="false" TabIndex="9" MaxLength="35" Width="330px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblAccountwithBankAddress3_754" runat="server" CssClass="elementLabel"
                                                                        Visible="false" Text="Address3"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox runat="server" ID="txtAccountwithBankAddress3_754" CssClass="textBox"
                                                                        Visible="false" TabIndex="9" MaxLength="35" Width="330px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">[58A] Beneficiary Bank : </span>
                                                                    <asp:DropDownList ID="ddlBeneficiarybank_754" runat="server" CssClass="dropdownList"
                                                                        TabIndex="10" Enabled="false">
                                                                        <asp:ListItem Text="58A" Value="A"></asp:ListItem>
                                                                        <asp:ListItem Text="58D" Value="D"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <span class="elementLabel">Party Identifier : </span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtBeneficiaryBankAccountnumber_754" runat="server" CssClass="textBox"
                                                                        TabIndex="10" Width="20px" MaxLength="1" Enabled="false"></asp:TextBox>
                                                                    <asp:TextBox ID="txtBeneficiarypartyidentifire" runat="server" CssClass="textBox"
                                                                        TabIndex="10" Width="330px" MaxLength="34" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <span class="elementLabel">[72Z] Sender to Receiver Information : </span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_754_SenRecInfo1" runat="server" CssClass="textBox" Width="330px"
                                                                        MaxLength="35" TabIndex="11" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblBeneficiaryBankIdentifier_754" runat="server" CssClass="elementLabel"
                                                                        Text="Identifier Code :"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox runat="server" ID="txtBeneficiaryBankIdentifiercode_754" CssClass="textBox"
                                                                        MaxLength="11" TabIndex="10" Width="100px" Enabled="false"></asp:TextBox>
                                                                    <asp:TextBox runat="server" ID="txtBeneficiaryBankName_754" CssClass="textBox" Visible="false"
                                                                        MaxLength="35" TabIndex="10" Width="330px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <span></span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_754_SenRecInfo2" runat="server" CssClass="textBox" Width="330px"
                                                                        MaxLength="35" TabIndex="11" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblBeneficiaryBankAddress1_754" runat="server" CssClass="elementLabel"
                                                                        Visible="false" Text="Address1"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox runat="server" ID="txtBeneficiaryBankAddress1_754" CssClass="textBox"
                                                                        Visible="false" TabIndex="10" MaxLength="35" Width="330px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <span></span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_754_SenRecInfo3" runat="server" CssClass="textBox" Width="330px"
                                                                        MaxLength="35" TabIndex="11" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblBeneficiaryBankAddress2_754" runat="server" CssClass="elementLabel"
                                                                        Visible="false" Text="Address2"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox runat="server" ID="txtBeneficiaryBankAddress2_754" CssClass="textBox"
                                                                        Visible="false" TabIndex="10" MaxLength="35" Width="330px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <span></span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_754_SenRecInfo4" runat="server" CssClass="textBox" Width="330px"
                                                                        MaxLength="35" TabIndex="11" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblBeneficiaryBankAddress3_754" runat="server" CssClass="elementLabel"
                                                                        Visible="false" Text="Address3"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox runat="server" ID="txtBeneficiaryBankAddress3_754" CssClass="textBox"
                                                                        Visible="false" TabIndex="10" MaxLength="35" Width="330px" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <span></span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_754_SenRecInfo5" runat="server" CssClass="textBox" Width="330px"
                                                                        MaxLength="35" TabIndex="11" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                </td>
                                                                <td align="left">
                                                                </td>
                                                                <td align="right">
                                                                    <span></span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_754_SenRecInfo6" runat="server" CssClass="textBox" Width="330px"
                                                                        MaxLength="35" TabIndex="11" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                    </table>
                                                    <table width="50%">
                                                        <tr>
                                                            <td width="15%" align="center">
                                                                <span class="elementLabel">[77A] Narrative : </span>
                                                            </td>
                                                            <td width="85%" valign="top">
                                                                <ajaxToolkit:CollapsiblePanelExtender ID="CPE_Narrative_754" runat="server" TargetControlID="panel_AddNarrative_754"
                                                                    CollapsedSize="0" ExpandedSize="200" ScrollContents="True" Enabled="True" />
                                                                <asp:Panel ID="panel_AddNarrative_754" runat="server">
                                                                    <table cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">1.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_1" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">2.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_2" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">3.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_3" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">4.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_4" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">5.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_5" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">6.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_6" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">7.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_7" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">8.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_8" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">9.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_9" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">10.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_10" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">11.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_11" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">12.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_12" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">13.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_13" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">14.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_14" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">15.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_15" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">16.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_16" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">17.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_17" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">18.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_18" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">19.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_19" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">20.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_20" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">21.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_21" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">22.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_22" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">23.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_23" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">24.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_24" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">25.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_25" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">26.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_26" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">27.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_27" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">28.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_28" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">29.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_29" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">30.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_30" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">31.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_31" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">32.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_32" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">33.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_33" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">34.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_34" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">35.</span>
                                                                                <asp:TextBox ID="txt_Narrative_754_35" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50" TabIndex="46" Enabled="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                        </ajaxToolkit:TabContainer>
                                        <table width="100%">
                                            <tr>
                                                <td width="10%">
                                                </td>
                                                <td width="90%">
                                                    <span class="elementLabel">SELECT FILE TYPE :</span>
                                                    <asp:CheckBox ID='rdb_swift_None' Text="None" CssClass="elementLabel" runat="server"
                                                        onchange="return Toggel_Swift_Type('None');" Enabled="false" />
                                                    <asp:CheckBox ID='rdb_swift_103' Text="MT 103" CssClass="elementLabel" runat="server"
                                                        onchange="return Toggel_Swift_Type('MT103');" Enabled="false" />
                                                    <asp:CheckBox ID='rdb_swift_202' Text="MT 202" CssClass="elementLabel" runat="server"
                                                        onchange="return Toggel_Swift_Type('MT202');" Enabled="false" />
                                                    <asp:CheckBox ID='rdb_swift_200' Text="MT 200" CssClass="elementLabel" runat="server"
                                                        onchange="return Toggel_Swift_Type('MT200');" Enabled="false" />
                                                    <asp:CheckBox ID='rdb_swift_R42' Text="R42" CssClass="elementLabel" runat="server"
                                                        onchange="return Toggel_Swift_Type('R42');" Enabled="false" />
                                                    <asp:CheckBox ID='rdb_swift_754' Text="MT 754" CssClass="elementLabel" runat="server"
                                                        onchange="return Toggel_Swift_Type('MT754');" Enabled="false" />
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
                                                    <asp:Button ID="btn_Swift_Prev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                        ToolTip="Back to General Operation II" TabIndex="256" OnClientClick="return SwiftPrevClick();" />
                                                    <asp:Button ID="btn_Generate_Swift" runat="server" Text="View SWIFT Message" CssClass="buttonDefault"
                                                        Width="150px" ToolTip="View SWIFT Message" TabIndex="256" OnClientClick="ViewSwiftMessage();" />
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
                                ToolTip="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp; <span class="elementLabel">
                                    Approve / Reject :</span>
                            <asp:DropDownList ID="ddlApproveReject" runat="server" CssClass="dropdownList" TabIndex="35" Enabled="false">
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
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
