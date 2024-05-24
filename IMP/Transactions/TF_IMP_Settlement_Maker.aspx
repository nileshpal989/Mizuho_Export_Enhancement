<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_Settlement_Maker.aspx.cs"
    Inherits="IMP_Transactions_TF_IMP_Settlement_Maker" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LMCC-TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style_V2.css" rel="Stylesheet" type="text/css" media="screen" />
    <link href="../../Style/TAB.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../../Scripts/jquery-1.8.3.min.js" type="text/javascript""></script>
    <script src="../Scripts/TF_IMP_Settlement_Maker.js" type="text/javascript"></script>
    <script id="Save_script" language="javascript" type="text/javascript">
        $(document).ready(function () {
            // Configure to save every 5 seconds  
            window.setInterval(SaveUpdateData, 5000); //calling saveDraft function for every 5 seconds  
        });
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
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table id="tbl_Header" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel"><strong>Import Bill Settlement - Maker</strong></span>
                            </td>
                            <td align="right" style="width: 50%" valign="bottom">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back" OnClientClick="return OnBackClick();" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%;" valign="top" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                                <%-------------------------  hidden fields  --------------------------------%>
                                <input type="hidden" id="hdnUserName" runat="server" />
                                <input type="hidden" id="hdnBranchName" runat="server" />
                                <input type="hidden" id="hdnDocType" runat="server" />
                                <input type="hidden" id="hdnDoc_Scrutiny" runat="server" />
                                <input type="hidden" id="hdnflcIlcType" runat="server" />
                                <input type="hidden" id="hdnMode" runat="server" />
                                <input type="hidden" id="hdnNegoRemiBankType" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <table id="tbl_Settlement" cellspacing="0" border="0" width="100%">
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
                            <td width="40%" align="left">
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
                            </td>
                            <td align="right" width="10%"></td>
                            <td align="left" width="20%"></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="5">
                                <ajaxToolkit:TabContainer ID="TabContainerMain" runat="server" ActiveTabIndex="2"
                                    CssClass="ajax__tab_xp-theme">
                                    <ajaxToolkit:TabPanel ID="tbDocumentDetails" runat="server" HeaderText="DOCUMENT DETAILS"
                                        Font-Bold="true" ForeColor="White">
                                        <ContentTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">CUSTOMER :</span>
                                                    </td>
                                                    <td align="left" colspan="3">
                                                        <asp:TextBox ID="txt_Doc_Customer_ID" runat="server" CssClass="textBox" TabIndex="3"
                                                            MaxLength="40" Width="100px" Enabled="false"></asp:TextBox>
                                                        <asp:Label ID="lblCustomerDesc" runat="server" CssClass="elementLabel"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" width="10%">
                                                        <span class="elementLabel">VALUE DATE :</span>
                                                    </td>
                                                    <td align="left" width="10%">
                                                        <asp:TextBox ID="txt_Doc_Value_Date" runat="server" TabIndex="4" CssClass="textBox"
                                                            MaxLength="10" ValidationGroup="dtVal" Width="70px"></asp:TextBox>
                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" Mask="99/99/9999" MaskType="Date"
                                                            runat="server" TargetControlID="txt_Doc_Value_Date" ErrorTooltipEnabled="True"
                                                            CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                            CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                            CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                        </ajaxToolkit:MaskedEditExtender>
                                                    </td>
                                                    <td align="right" width="10%">
                                                        <span class="elementLabel">COMMENT CODE :</span>
                                                    </td>
                                                    <td align="left" width="10%">
                                                        <asp:TextBox ID="txt_Doc_Comment_Code" runat="server" CssClass="textBox" MaxLength="2"
                                                            TabIndex="5" Width="30px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">MATURITY :</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txt_Doc_Maturity" runat="server" CssClass="textBox" MaxLength="10"
                                                            Width="70px" TabIndex="6"></asp:TextBox>
                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender8" Mask="99/99/9999" MaskType="Date"
                                                            runat="server" TargetControlID="txt_Doc_Maturity" ErrorTooltipEnabled="True"
                                                            CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                            CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                            CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                        </ajaxToolkit:MaskedEditExtender>
                                                    </td>
                                                    <td align="left" colspan="2"></td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">SETTLEMENT FOR CUST :</span>
                                                    </td>
                                                    <td align="left" colspan="3">
                                                        <asp:TextBox ID="txt_Doc_Settlement_for_Cust" runat="server" CssClass="textBox" MaxLength="2"
                                                            TabIndex="7" Width="30px"></asp:TextBox>
                                                    </td>
                                                    <td align="right"></td>
                                                    <td align="left"></td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">SETTLEMENT FOR BANK :</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txt_Doc_Settlement_For_Bank" runat="server" CssClass="textBox" MaxLength="2"
                                                            Width="30px" TabIndex="8"></asp:TextBox>
                                                    </td>
                                                    <td align="right"></td>
                                                    <td align="left"></td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">INTEREST FROM :</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txt_Doc_Interest_From" runat="server" CssClass="textBox" MaxLength="10" AutoPostBack="true"
                                                            TabIndex="9" ValidationGroup="dtVal" Width="70px" OnTextChanged="Get_Acceptance_Get_Date_Diff"></asp:TextBox>
                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" Mask="99/99/9999" MaskType="Date"
                                                            runat="server" TargetControlID="txt_Doc_Interest_From" ErrorTooltipEnabled="True"
                                                            CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                            CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                            CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                        </ajaxToolkit:MaskedEditExtender>
                                                        <asp:Button ID="btncal_Interest_From_Date" runat="server" CssClass="btncalendar_enabled" />
                                                      
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txt_Doc_Interest_From" PopupButtonID="btncal_Interest_From_Date" Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                                                      <ajaxToolkit:MaskedEditValidator ID="MV_Received_date" runat="server" ControlExtender="MaskedEditExtender4"
                                                        ValidationGroup="dtVal" ControlToValidate="txt_Doc_Interest_From" EmptyValueMessage="Enter Date Value"
                                                        InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                        Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">DISCOUNT :</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txt_Doc_Discount" runat="server" CssClass="textBox" MaxLength="1"
                                                            TabIndex="10" Width="30px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">INTEREST TO :</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txt_Doc_Interest_To" runat="server" CssClass="textBox" MaxLength="10" AutoPostBack="true"
                                                            TabIndex="11" ValidationGroup="dtVal" Width="70px" OnTextChanged="Get_Acceptance_Get_Date_Diff"></asp:TextBox>
                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" Mask="99/99/9999" MaskType="Date"
                                                            runat="server" TargetControlID="txt_Doc_Interest_To" ErrorTooltipEnabled="True"
                                                            CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                            CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                            CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                        </ajaxToolkit:MaskedEditExtender>
                                                        <asp:Button ID="btncal_Interest_To_Date" runat="server" CssClass="btncalendar_enabled" />
                                                      
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txt_Doc_Interest_To" PopupButtonID="btncal_Interest_To_Date" Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                                                      <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender5"
                                                        ValidationGroup="dtVal" ControlToValidate="txt_Doc_Interest_To" EmptyValueMessage="Enter Date Value"
                                                        InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                        Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">NOS OF DAYS :</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txt_Doc_No_Of_Days" runat="server" CssClass="textBox" MaxLength="4"
                                                            Width="30px" TabIndex="12" Style="text-align: right"  onkeyDown="return validate_Number(event);"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">RATE :</span>
                                                    </td>
                                                    <td align="left" colspan="3">
                                                        <asp:TextBox ID="txt_Doc_Rate" runat="server" CssClass="textBox" MaxLength="10" TabIndex="13"
                                                            Width="70px" Style="text-align: right" onkeyDown="return validate_Number(event);"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">AMOUNT :</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txt_Doc_Amount" runat="server" CssClass="textBox" MaxLength="16"
                                                            TabIndex="14" Width="100px" Style="text-align: right" onkeyDown="return validate_Number(event);"></asp:TextBox>
                                                    </td>
                                                    <td align="right"></td>
                                                    <td align="left"></td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">OVERDUE :</span>
                                                    </td>
                                                    <td colspan="3"></td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">RATE :</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txt_Doc_Overdue_Interestrate" runat="server" CssClass="textBox"
                                                            MaxLength="10" TabIndex="15" Width="70px" Style="text-align: right"  onkeyDown="return validate_Number(event);"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">NOS OF DAYS :</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txt_Doc_Oveduenoofdays" runat="server" CssClass="textBox" MaxLength="4"
                                                            Width="30px" TabIndex="16" Style="text-align: right"  onkeyDown="return validate_Number(event);"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">AMOUNT :</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txt_Doc_Overdueamount" runat="server" CssClass="textBox" MaxLength="16"
                                                            TabIndex="17" Width="100px" Style="text-align: right"  onkeyDown="return validate_Number(event);"></asp:TextBox>
                                                    </td>
                                                    <td align="right"></td>
                                                    <td align="left"></td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">ATTN :</span>
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txt_Doc_Attn" runat="server" CssClass="textBox" TabIndex="18" MaxLength="70"
                                                            Width="190px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center"></td>
                                                    <td></td>
                                                    <td colspan="3">
                                                        <asp:Button ID="btnDocNext" runat="server" CssClass="buttonDefault" TabIndex="19"
                                                            Text="Next >>" ToolTip="Go to IMPORT ACCOUNTING" OnClientClick="return OnDocNextClick(1);" />
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
                                                    <td></td>
                                                    <td align="left" colspan="2">
                                                        <span class="elementLabel">1-DISC </span>
                                                    </td>
                                                    <td align="right" colspan="2">
                                                        <span class="elementLabel">AMOUNT :</span><asp:TextBox ID="txt_DiscAmt" runat="server" onchange="return DR_Amt_Calculation();"
                                                            CssClass="textBox" TabIndex="20" MaxLength="16" Width="100px" Style="text-align: right"  onkeyDown="return validate_Number(event);"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" width="20%"></td>
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
                                                    <td align="left" width="20%"></td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">PRINCIPAL :</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtPrinc_matu" runat="server" CssClass="textBox" MaxLength="1" TabIndex="20"
                                                            Width="40px"></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtPrinc_lump" runat="server" CssClass="textBox" MaxLength="1" TabIndex="21"
                                                            Width="40px"></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtprinc_Contract_no" runat="server" CssClass="textBox" MaxLength="9"
                                                            TabIndex="22" Width="90px" ></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txt_Princ_Ex_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                            TabIndex="23" Width="40px" onkeyup="return DR_Curr_Toggel();"></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtprinc_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                            TabIndex="24" onkeydown="return validate_Number(event);" onchange="return DR_Amt_Calculation();" Width="70px"></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtprinc_Intnl_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                            TabIndex="25" onkeydown="return validate_Number(event);" Width="70px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">INTEREST :</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtInterest_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                            TabIndex="26" Width="40px" onkeydown="return validate_Commission_Code(event);"></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtInterest_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                            TabIndex="27" Width="40px" onkeydown="return validate_Commission_Code(event);"></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtInterest_Contract_no" runat="server" CssClass="textBox" MaxLength="9"
                                                            TabIndex="28" Width="90px" ></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txt_interest_Ex_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                            TabIndex="29" Width="40px" ></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtInterest_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                            onkeydown="return validate_Number(event);" TabIndex="30" Width="70px"></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtInterest_Intnl_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                            onkeydown="return validate_Number(event);" TabIndex="31" Width="70px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">COMMISSION :</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtCommission_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                            TabIndex="32" Width="40px" onkeydown="return validate_Commission_Code(event);"
                                                            onkeyup="return Commission_Toggel();"></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtCommission_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                            TabIndex="33" Width="40px" onkeydown="return validate_Commission_Code(event);"></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtCommission_Contract_no" runat="server" CssClass="textBox" MaxLength="9"
                                                            TabIndex="34" Width="90px" ></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txt_Commission_Ex_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                            TabIndex="35" Width="40px" ></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtCommission_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                            TabIndex="36" onkeydown="return validate_Number(event);" Width="70px"></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtCommission_Intnl_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                            TabIndex="37" onkeydown="return validate_Number(event);" Width="70px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">THEIR COMMISSION :</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtTheir_Commission_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                            TabIndex="38" Width="40px" onkeydown="return validate_Commission_Code(event);"></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtTheir_Commission_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                            TabIndex="39" Width="40px" onkeydown="return validate_Commission_Code(event);"></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtTheir_Commission_Contract_no" runat="server" CssClass="textBox"
                                                            TabIndex="40" MaxLength="9" Width="90px" ></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txt_Their_Commission_Ex_Curr" runat="server" CssClass="textBox"
                                                            TabIndex="41" MaxLength="3" Width="40px" ></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtTheir_Commission_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                            TabIndex="42" onkeydown="return validate_Number(event);" Width="70px"></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtTheir_Commission_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                            TabIndex="43" onkeydown="return validate_Number(event);" MaxLength="9" Width="70px"></asp:TextBox>
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
                                                                    &nbsp;<asp:Button ID="btnCR_Code_help" runat="server" ToolTip="Press for AcCode list." CssClass="btnHelp_enabled" OnClientClick="return OpenCR_Code_help('mouseClick');" />
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
                                                                    <asp:TextBox ID="txt_CR_Code" runat="server" CssClass="textBox" MaxLength="5" onkeydown="return validate_Number(event);"
                                                                        TabIndex="44" Width="90px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <%-- <span class="elementLabel">ACCEPTANCES</span>--%>
                                                                    <asp:TextBox ID="txt_CR_AC_Short_Name" runat="server" CssClass="textBox" MaxLength="20"
                                                                        TabIndex="44" Width="150px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_CR_Cust_abbr" runat="server" CssClass="textBox" MaxLength="12"
                                                                        TabIndex="45" Width="90px" ></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_CR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="20"
                                                                        TabIndex="46" Width="150px" ></asp:TextBox>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:TextBox ID="txt_CR_Acceptance_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                        TabIndex="47" Width="40px" ></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_CR_Acceptance_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                        onkeydown="return validate_Number(event);" TabIndex="48" Width="100px" Style="text-align: right"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_CR_Acceptance_payer" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="49" Width="50px" ></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right"></td>
                                                                <td align="left" colspan="3">
                                                                    <span class="elementLabel">INTEREST</span>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:TextBox ID="txt_CR_Interest_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                        TabIndex="50" Width="40px" ></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_CR_Interest_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                        onkeydown="return validate_Number(event);" TabIndex="51" Width="100px" Style="text-align: right"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_CR_Interest_payer" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="52" Width="50px" ></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right"></td>
                                                                <td align="left" colspan="3">
                                                                    <span class="elementLabel">ACCEPTANCE COMMISSION</span>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:TextBox ID="txt_CR_Accept_Commission_Curr" runat="server" CssClass="textBox"
                                                                        TabIndex="53" MaxLength="3" Width="40px" ></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_CR_Accept_Commission_amt" runat="server" CssClass="textBox"
                                                                        onkeydown="return validate_Number(event);" TabIndex="54" MaxLength="16" Width="100px"
                                                                        Style="text-align: right"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_CR_Accept_Commission_Payer" runat="server" CssClass="textBox"
                                                                        TabIndex="55" MaxLength="1" Width="50px" ></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right"></td>
                                                                <td align="left" colspan="3">
                                                                    <span class="elementLabel">PAYMENT/HANDLING COMMISSION</span>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:TextBox ID="txt_CR_Pay_Handle_Commission_Curr" runat="server" CssClass="textBox"
                                                                        TabIndex="56" MaxLength="3" Width="40px" ></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_CR_Pay_Handle_Commission_amt" runat="server" CssClass="textBox"
                                                                        onkeydown="return validate_Number(event);" TabIndex="57" MaxLength="16" Width="100px"
                                                                        Style="text-align: right"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_CR_Pay_Handle_Commission_Payer" runat="server" CssClass="textBox"
                                                                        TabIndex="58" MaxLength="1" Width="50px" ></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right"></td>
                                                                <td align="left" colspan="3">
                                                                    <span class="elementLabel">OTHERS</span>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:TextBox ID="txt_CR_Others_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                        TabIndex="59" Width="40px" ></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_CR_Others_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                        onkeydown="return validate_Number(event);" TabIndex="60" Width="100px" Style="text-align: right"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_CR_Others_Payer" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="61" Width="50px" ></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right"></td>
                                                                <td align="left" colspan="3">
                                                                    <span class="elementLabel">THEIR COMMISSION</span>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:TextBox ID="txt_CR_Their_Commission_Curr" runat="server" CssClass="textBox"
                                                                        TabIndex="62" MaxLength="3" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_CR_Their_Commission_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                        onkeydown="return validate_Number(event);" TabIndex="63" Width="100px" Style="text-align: right"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_CR_Their_Commission_Payer" runat="server" CssClass="textBox"
                                                                        TabIndex="64" MaxLength="1" Width="50px" ></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel"><b>/DR/</b> CODE:</span>
                                                                    &nbsp;<asp:Button ID="btnDR_Code_help" runat="server" ToolTip="Press for AcCode list." CssClass="btnHelp_enabled" OnClientClick="return OpenDR_Code_help('mouseClick');" />
                                                                </td>
                                                                <td align="left" colspan="3"></td>
                                                                <td align="center">
                                                                </td>
                                                                <td align="left"></td>
                                                                <td align="left"></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:TextBox ID="txt_DR_Code" runat="server" CssClass="textBox" MaxLength="5" TabIndex="65" onblur="return Toggel_DR_Code();"
                                                                        Width="90px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_DR_AC_Short_Name" runat="server" CssClass="textBox" MaxLength="20" onblur="return Toggel_DR_Cust_Name();"
                                                                        TabIndex="65" Width="150px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_DR_Cust_abbr" runat="server" CssClass="textBox" MaxLength="12" onblur="return Toggel_DR_Cust_Abbr();"
                                                                        TabIndex="66" Width="90px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_DR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="20" onblur="return Toggel_DR_Cust_Acc();"
                                                                        TabIndex="67" Width="150px"></asp:TextBox>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:TextBox ID="txt_DR_Cur_Acc_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                        TabIndex="68" Width="40px" ></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_DR_Cur_Acc_amt" runat="server" CssClass="textBox" MaxLength="16"
                                                                        onkeydown="return validate_Number(event);" TabIndex="69" Width="100px" Style="text-align: right"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_DR_Cur_Acc_payer" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="70" Width="50px" ></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right"></td>
                                                                <td align="left" colspan="3"></td>
                                                                <td align="center">
                                                                    <asp:TextBox ID="txt_DR_Cur_Acc_Curr2" runat="server" CssClass="textBox" MaxLength="3"
                                                                        TabIndex="71" Width="40px" ></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_DR_Cur_Acc_amt2" runat="server" CssClass="textBox" MaxLength="16"
                                                                        onkeydown="return validate_Number(event);" TabIndex="72" Width="100px" Style="text-align: right"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_DR_Cur_Acc_payer2" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="73" Width="50px" ></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right"></td>
                                                                <td align="left" colspan="3"></td>
                                                                <td align="center">
                                                                    <asp:TextBox ID="txt_DR_Cur_Acc_Curr3" runat="server" CssClass="textBox" MaxLength="3"
                                                                        TabIndex="74" Width="40px" ></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_DR_Cur_Acc_amt3" runat="server" CssClass="textBox" MaxLength="16"
                                                                        TabIndex="75" onkeydown="return validate_Number(event);" Width="100px" Style="text-align: right"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_DR_Cur_Acc_payer3" runat="server" CssClass="textBox" MaxLength="1"
                                                                        TabIndex="76" Width="50px" ></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td ></td>
                                                                <td align="left"  colspan="5">
                                                                    <asp:Button ID="btnImpAc_Prev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                                        ToolTip="Back to SETTLEMENT(IBD,ACC)" TabIndex="77" OnClientClick="return OnDocNextClick(0);" />
                                                                <asp:Button ID="btnImpAc_Next" runat="server" Text="Next>>" CssClass="buttonDefault"
                                                                    ToolTip="Go to GENERAL OPERATION 1" TabIndex="78" OnClientClick="return OnDocNextClick(2);" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                            </table>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel ID="tbDocumentGO_Bill_Handling" runat="server" HeaderText="GENERAL OPERATION I"
                                        Font-Bold="true" ForeColor="White">
                                        <ContentTemplate>
                                            <table width="80%">
                                                <tr>
                                                    <td width="20%"></td>
                                                    <td colspan="3">
                                                        <asp:CheckBox ID="cb_GO_Bill_Handling_Flag" Text="GENERAL OPERATION I" runat="server"
                                                            CssClass="elementLabel" OnCheckedChanged="cb_GO_Bill_Handling_Flag_OnCheckedChanged" AutoPostBack="true" />
                                                    </td>
                                                </tr>
                                                <asp:Panel ID="PanelGO_Bill_Handling" runat="server" Visible="false">
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">COMMENT : </span>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txt_Bill_Handling_Comment" runat="server" CssClass="textBox"  MaxLength="20" TabIndex="80"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="20%">
                                                            <span class="elementLabel">Section No : </span>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="txt_Bill_Handling_SectionNo" runat="server" CssClass="textBox" TabIndex="81"  MaxLength="2"
                                                                Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right" width="15%">
                                                            <span class="elementLabel">Remarks : </span>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="txt_Bill_Handling_Remarks" runat="server"  MaxLength="30" CssClass="textBox" TabIndex="82" Width="300px" onblur="return Toggel_GO1_Remarks();"></asp:TextBox>
                                                        </td>
                                                        <td align="right" width="15%">
                                                            <span class="elementLabel">MEMO : </span>
                                                        </td>
                                                        <td width="10%">
                                                            <asp:TextBox ID="txt_Bill_Handling_Memo" runat="server" CssClass="textBox" Width="50px"  MaxLength="20"
                                                                TabIndex="83"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="15%">
                                                            <span class="elementLabel">SCHEME No : </span>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="txt_Bill_Handling_Scheme_no" runat="server"  MaxLength="20" CssClass="textBox" TabIndex="84"></asp:TextBox>
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
                                                            <asp:TextBox ID="txt_Bill_Handling_Debit_Code" runat="server" CssClass="textBox"  MaxLength="1"
                                                                TabIndex="85" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Bill_Handling_Debit_Curr" runat="server" CssClass="textBox"  MaxLength="3"
                                                                TabIndex="86" Width="25px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Bill_Handling_Debit_Amt" runat="server" CssClass="textBox" Width="100px" onblur="return Toggel_Debit_Amt();"
                                                              onkeydown="return validate_Number(event);"  TabIndex="87" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">CUSTOMER : </span>
                                                        </td>
                                                        <td colspan="4">
                                                            <asp:TextBox ID="txt_Bill_Handling_Debit_Cust" runat="server" CssClass="textBox"  MaxLength="12"
                                                                TabIndex="88" Width="90px"></asp:TextBox>
                                                            <asp:TextBox ID="txt_Bill_Handling_Debit_Cust_Name" runat="server" CssClass="textBox"  MaxLength="40"
                                                                TabIndex="88" Width="300px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C CODE : </span>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txt_Bill_Handling_Debit_Cust_AcCode" runat="server" CssClass="textBox"  MaxLength="5"
                                                                TabIndex="89" Width="90px"></asp:TextBox>
                                                            <asp:TextBox ID="txt_Bill_Handling_Debit_Cust_AcCode_Name" runat="server" CssClass="textBox"  MaxLength="40"
                                                                TabIndex="89" Width="300px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C No : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Bill_Handling_Debit_Cust_AccNo" runat="server" CssClass="textBox"  MaxLength="20"
                                                                TabIndex="90"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" align="right">
                                                            <span class="elementLabel">EXCH RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Bill_Handling_Debit_Exch_Rate" runat="server" CssClass="textBox"
                                                                onkeydown="return validate_Number(event);" TabIndex="91" Width="100px" Style="text-align: right"
                                                                MaxLength="16"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">EXCH CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Bill_Handling_Debit_Exch_CCY" runat="server" CssClass="textBox"
                                                                MaxLength="3" TabIndex="92" Width="25px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">FUND : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Bill_Handling_Debit_FUND" runat="server" CssClass="textBox"  MaxLength="1"
                                                                TabIndex="93" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CHECK No. : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Bill_Handling_Debit_Check_No" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                TabIndex="94" MaxLength="6"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AVAILABLE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Bill_Handling_Debit_Available" runat="server" CssClass="textBox"
                                                                TabIndex="95" Width="100px" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">ADVICE PRINT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Bill_Handling_Debit_AdPrint" runat="server" CssClass="textBox"
                                                                TabIndex="96" Width="20px" MaxLength="1"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">DETAILS : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Bill_Handling_Debit_Details" runat="server" CssClass="textBox" Width="300px" MaxLength="30"
                                                                TabIndex="97"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">ENTITY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Bill_Handling_Debit_Entity" runat="server" CssClass="textBox"
                                                                onkeydown="return validate_Number(event);" TabIndex="98" Width="90px" Style="text-align: right"
                                                                MaxLength="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">DIVISION : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Bill_Handling_Debit_Division" runat="server" CssClass="textBox" MaxLength="2"
                                                                TabIndex="99" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Bill_Handling_Debit_Inter_Amount" onkeydown="return validate_Number(event);" runat="server" CssClass="textBox" MaxLength="16" Style="text-align: right"
                                                                 Width="100px" TabIndex="100"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Bill_Handling_Debit_Inter_Rate" runat="server" CssClass="textBox" MaxLength="16"
                                                                TabIndex="101" Width="100px" Style="text-align: right" onkeydown="return validate_Number(event);"></asp:TextBox>
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
                                                            <asp:TextBox ID="txt_Bill_Handling_Credit_Code" runat="server" CssClass="textBox"
                                                                TabIndex="102" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Bill_Handling_Credit_Curr" runat="server" CssClass="textBox"
                                                                TabIndex="103" Width="25px" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Bill_Handling_Credit_Amt" runat="server" CssClass="textBox"
                                                                onkeydown="return validate_Number(event);" TabIndex="104" Width="100px"
                                                                Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">CUSTOMER : </span>
                                                        </td>
                                                        <td colspan="4">
                                                            <asp:TextBox ID="txt_Bill_Handling_Credit_Cust" runat="server" CssClass="textBox" MaxLength="12"
                                                                TabIndex="105" Width="90px"></asp:TextBox>
                                                            <asp:TextBox ID="txt_Bill_Handling_Credit_Cust_Name" runat="server" CssClass="textBox" MaxLength="40"
                                                                TabIndex="105" Width="300px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C CODE : </span>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txt_Bill_Handling_Credit_Cust_AcCode" runat="server" CssClass="textBox" MaxLength="5"
                                                                TabIndex="106" Width="90px"></asp:TextBox>
                                                            <asp:TextBox ID="txt_Bill_Handling_Credit_Cust_AcCode_Name" runat="server" CssClass="textBox" MaxLength="40"
                                                                TabIndex="106" Width="300px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C No : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Bill_Handling_Credit_Cust_AccNo" runat="server" CssClass="textBox" MaxLength="20"
                                                                TabIndex="107"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" align="right">
                                                            <span class="elementLabel">EXCH RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Bill_Handling_Credit_Exch_Rate" runat="server" CssClass="textBox"
                                                                onkeydown="return validate_Number(event);" TabIndex="108" Width="100px" Style="text-align: right"
                                                                MaxLength="16"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">EXCH CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Bill_Handling_Credit_Exch_Curr" runat="server" CssClass="textBox"
                                                                MaxLength="3" TabIndex="109" Width="25px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">FUND : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Bill_Handling_Credit_FUND" runat="server" CssClass="textBox" MaxLength="1"
                                                                TabIndex="110" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CHECK No. : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Bill_Handling_Credit_Check_No" runat="server" CssClass="textBox" MaxLength="6" onkeydown="return validate_Number(event);" 
                                                                TabIndex="111"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AVAILABLE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Bill_Handling_Credit_Available" runat="server" CssClass="textBox" MaxLength="20"
                                                                TabIndex="112" Width="100px" ></asp:TextBox> 
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">ADVICE PRINT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Bill_Handling_Credit_AdPrint" runat="server" CssClass="textBox"
                                                                TabIndex="113" Width="20px" MaxLength="1"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">DETAILS : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Bill_Handling_Credit_Details" runat="server" CssClass="textBox" Width="300px" MaxLength="30"
                                                                TabIndex="114"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">ENTITY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Bill_Handling_Credit_Entity" runat="server" CssClass="textBox"
                                                                onkeydown="return validate_Number(event);" TabIndex="115" Width="90px" Style="text-align: right"
                                                                MaxLength="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">DIVISION : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Bill_Handling_Credit_Division" runat="server" CssClass="textBox" MaxLength="2"
                                                                onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="116" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Bill_Handling_Credit_Inter_Amount" runat="server" CssClass="textBox" Width="100px"
                                                               onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="117"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Bill_Handling_Credit_Inter_Rate" runat="server" CssClass="textBox"
                                                               onkeydown="return validate_Number(event);"  TabIndex="118" Width="100px" Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                                <tr>
                                                    <td></td>
                                                    <td align="left" colspan="5">
                                                        <asp:Button ID="btnBill_Handling_Prev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                            ToolTip="Back to IMPORT ACCOUNTING" TabIndex="119" OnClientClick="return OnDocNextClick(1);" />
                                                    <asp:Button ID="btnBill_Handling_Next" runat="server" Text="Next>>" CssClass="buttonDefault"
                                                        ToolTip="Go to GENERAL OPERATION 2" TabIndex="220" OnClientClick="return OnDocNextClick(3);" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel ID="tbDocumentGO_Sundry" runat="server" HeaderText="GENERAL OPERATION II"
                                        Font-Bold="true" ForeColor="White">
                                        <ContentTemplate>
                                            <table width="80%">
                                                <tr>
                                                    <td width="20%"></td>
                                                    <td colspan="3">
                                                        <asp:CheckBox ID="cb_GO_Sundry_Flag" Text="GENERAL OPERATION II"
                                                            CssClass="elementLabel" OnCheckedChanged="cb_GO_Sundry_Flag_OnCheckedChanged" runat="server" AutoPostBack="true" />
                                                    </td>
                                                </tr>
                                                <asp:Panel ID="panalGO_Sundry" runat="server" Visible="false">
                                                    <tr>

                                                        <td align="right">
                                                            <span class="elementLabel">COMMENT : </span>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txt_Sundry_Comment" runat="server" CssClass="textBox" TabIndex="223"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="20%">
                                                            <span class="elementLabel">Section No : </span>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="txt_Sundry_SectionNo" runat="server" CssClass="textBox" TabIndex="224"
                                                                Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right" width="15%">
                                                            <span class="elementLabel">Remarks : </span>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="txt_Sundry_Remarks" onkeyup="return Toggel_GO_Sundry_Remarks();"  runat="server" CssClass="textBox" TabIndex="225" Width="300px"></asp:TextBox>
                                                        </td>
                                                        <td align="right" width="15%">
                                                            <span class="elementLabel">MEMO : </span>
                                                        </td>
                                                        <td width="10%">
                                                            <asp:TextBox ID="txt_Sundry_Memo" runat="server" CssClass="textBox" Width="50px"
                                                                TabIndex="226"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="15%">
                                                            <span class="elementLabel">SCHEME No : </span>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="txt_Sundry_Scheme_no" runat="server" CssClass="textBox" TabIndex="227"></asp:TextBox>
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
                                                            <asp:TextBox ID="txt_Sundry_Debit_Code" runat="server" CssClass="textBox" TabIndex="228"
                                                                Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sundry_Debit_Curr" runat="server" CssClass="textBox" TabIndex="229"
                                                                Width="25px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sundry_Debit_Amt" runat="server" CssClass="textBox" Width="90px"
                                                                TabIndex="230" Style="text-align: right" MaxLength="16" onblur="return Toggel_GO_Sundry_Debit_Amt();"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">CUSTOMER : </span>
                                                        </td>
                                                        <td colspan="4">
                                                            <asp:TextBox ID="txt_Sundry_Debit_Cust" runat="server" CssClass="textBox" TabIndex="231"
                                                                Width="90px"></asp:TextBox>
                                                            <asp:TextBox ID="txt_Sundry_Debit_Cust_Name" runat="server" CssClass="textBox" TabIndex="231"
                                                                Width="150px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C CODE : </span>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txt_Sundry_Debit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                TabIndex="232" Width="90px"></asp:TextBox>
                                                            <asp:TextBox ID="txt_Sundry_Debit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                                TabIndex="232" Width="150px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C No : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sundry_Debit_Cust_AccNo" runat="server" CssClass="textBox" TabIndex="233"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" align="right">
                                                            <span class="elementLabel">EXCH RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sundry_Debit_Exch_Rate" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                TabIndex="234" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">EXCH CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sundry_Debit_Exch_CCY" runat="server" CssClass="textBox" MaxLength="3"
                                                                TabIndex="235" Width="25px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">FUND : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sundry_Debit_FUND" runat="server" CssClass="textBox" TabIndex="236"
                                                                Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CHECK No. : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sundry_Debit_Check_No" runat="server" CssClass="textBox" TabIndex="237"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AVAILABLE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sundry_Debit_Available" runat="server" CssClass="textBox" TabIndex="238"
                                                                Width="90px" Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">ADVICE PRINT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sundry_Debit_AdPrint" runat="server" CssClass="textBox" TabIndex="239"
                                                                Width="20px" MaxLength="1"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">DETAILS : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sundry_Debit_Details" runat="server" CssClass="textBox" Width="300px" TabIndex="240"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">ENTITY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sundry_Debit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                TabIndex="241" Width="90px" Style="text-align: right" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">DIVISION : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sundry_Debit_Division" runat="server" CssClass="textBox" TabIndex="242"
                                                                Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sundry_Debit_Inter_Amount" runat="server" CssClass="textBox"
                                                                TabIndex="243"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sundry_Debit_Inter_Rate" runat="server" CssClass="textBox" TabIndex="244"
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
                                                            <asp:TextBox ID="txt_Sundry_Credit_Code" runat="server" CssClass="textBox" TabIndex="245"
                                                                Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sundry_Credit_Curr" runat="server" CssClass="textBox" TabIndex="246"
                                                                Width="25px" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sundry_Credit_Amt" runat="server" CssClass="textBox"
                                                                onkeydown="return validate_Number(event);" TabIndex="247" Width="90px" Style="text-align: right"
                                                                MaxLength="16"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">CUSTOMER : </span>
                                                        </td>
                                                        <td colspan="4">
                                                            <asp:TextBox ID="txt_Sundry_Credit_Cust" runat="server" CssClass="textBox" TabIndex="248"
                                                                Width="90px"></asp:TextBox>
                                                            <asp:TextBox ID="txt_Sundry_Credit_Cust_Name" runat="server" CssClass="textBox" TabIndex="248"
                                                                Width="150px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C CODE : </span>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txt_Sundry_Credit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                TabIndex="249" Width="90px"></asp:TextBox>
                                                            <asp:TextBox ID="txt_Sundry_Credit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                                TabIndex="249" Width="150px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C No : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sundry_Credit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                TabIndex="250"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" align="right">
                                                            <span class="elementLabel">EXCH RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sundry_Credit_Exch_Rate" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                TabIndex="251" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">EXCH CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sundry_Credit_Exch_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                TabIndex="252" Width="25px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">FUND : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sundry_Credit_FUND" runat="server" CssClass="textBox" TabIndex="253"
                                                                Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CHECK No. : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sundry_Credit_Check_No" runat="server" CssClass="textBox" TabIndex="254"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AVAILABLE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sundry_Credit_Available" runat="server" CssClass="textBox" TabIndex="255"
                                                                Width="90px" Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">ADVICE PRINT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sundry_Credit_AdPrint" runat="server" CssClass="textBox" TabIndex="256"
                                                                Width="20px" MaxLength="1"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">DETAILS : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sundry_Credit_Details" runat="server" Width="300px" CssClass="textBox" TabIndex="257"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">ENTITY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sundry_Credit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                TabIndex="258" Width="90px" Style="text-align: right" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">DIVISION : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sundry_Credit_Division" runat="server" CssClass="textBox" TabIndex="259"
                                                                Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sundry_Credit_Inter_Amount" runat="server" CssClass="textBox"
                                                                TabIndex="260"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Sundry_Credit_Inter_Rate" runat="server" CssClass="textBox"
                                                                TabIndex="261" Width="90px" Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                                <tr>
                                                    <td></td>
                                                    <td align="left" colspan="5">
                                                        <asp:Button ID="btnSundry_Prev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                            ToolTip="Back to GENERAL OPERATION 1" TabIndex="262" OnClientClick="return OnDocNextClick(2);" />
                                                    <asp:Button ID="btnSundry_Next" runat="server" Text="Next>>" CssClass="buttonDefault"
                                                        ToolTip="Go to GENERAL OPERATION 3" TabIndex="263" OnClientClick="return OnDocNextClick(4);" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel ID="tbDocumentGO_Comm_Recieved" runat="server" HeaderText="GENERAL OPERATION III"
                                        Font-Bold="true" ForeColor="White">
                                        <ContentTemplate>
                                            <table width="80%">
                                                <tr>
                                                    <td width="20%"></td>
                                                    <td colspan="3">
                                                        <asp:CheckBox ID="cb_GO_Comm_Recieved_Flag" Text="GENERAL OPERATION III"
                                                            CssClass="elementLabel" runat="server" OnCheckedChanged="cb_GO_Comm_Recieved_Flag_OnCheckedChanged" AutoPostBack="true" />
                                                    </td>
                                                </tr>
                                                <asp:Panel ID="PanelGO_Comm_Recieved" runat="server" Visible="false">
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">COMMENT : </span>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txt_Comm_Recieved_Comment" MaxLength="20" runat="server" CssClass="textBox" TabIndex="266"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="20%">
                                                            <span class="elementLabel">Section No : </span>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="txt_Comm_Recieved_SectionNo" runat="server" CssClass="textBox" TabIndex="267"  MaxLength="2"
                                                                Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right" width="15%">
                                                            <span class="elementLabel">Remarks : </span>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="txt_Comm_Recieved_Remarks"  MaxLength="30" runat="server" CssClass="textBox" Width="300px" TabIndex="268"></asp:TextBox>
                                                        </td>
                                                        <td align="right" width="15%">
                                                            <span class="elementLabel">MEMO : </span>
                                                        </td>
                                                        <td width="10%">
                                                            <asp:TextBox ID="txt_Comm_Recieved_Memo" runat="server" CssClass="textBox" Width="50px"  MaxLength="20"
                                                                TabIndex="269"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="15%">
                                                            <span class="elementLabel">SCHEME No : </span>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="txt_Comm_Recieved_Scheme_no"  MaxLength="20" runat="server" CssClass="textBox" TabIndex="270"></asp:TextBox>
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
                                                            <asp:TextBox ID="txt_Comm_Recieved_Debit_Code" runat="server" CssClass="textBox"  MaxLength="1"
                                                                TabIndex="271" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Debit_Curr" runat="server" CssClass="textBox"  MaxLength="3"
                                                                TabIndex="272" Width="25px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Debit_Amt" runat="server" CssClass="textBox" Width="100px" onkeyDown="return validate_Number(event);"
                                                                TabIndex="273" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">CUSTOMER : </span>
                                                        </td>
                                                        <td colspan="4">
                                                            <asp:TextBox ID="txt_Comm_Recieved_Debit_Cust" runat="server" CssClass="textBox" MaxLength="12"
                                                                TabIndex="274" Width="90px"></asp:TextBox>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Debit_Cust_Name" runat="server" CssClass="textBox" MaxLength="40"
                                                                TabIndex="274" Width="300px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C CODE : </span>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txt_Comm_Recieved_Debit_Cust_AcCode" runat="server" CssClass="textBox" MaxLength="5"
                                                                TabIndex="275" Width="90px"></asp:TextBox>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Debit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                                TabIndex="275" Width="300px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C No : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Debit_Cust_AccNo" runat="server" CssClass="textBox" MaxLength="20"
                                                                TabIndex="276"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" align="right">
                                                            <span class="elementLabel">EXCH RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Debit_Exch_Rate" runat="server" CssClass="textBox"
                                                                onkeydown="return validate_Number(event);" TabIndex="277" Width="90px" Style="text-align: right"
                                                                MaxLength="16"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">EXCH CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Debit_Exch_CCY" runat="server" CssClass="textBox"
                                                                MaxLength="3" TabIndex="278" Width="25px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">FUND : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Debit_FUND" runat="server" CssClass="textBox" MaxLength="1"
                                                                TabIndex="279" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CHECK No. : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Debit_Check_No" runat="server" CssClass="textBox" MaxLength="6" onkeyDown="return validate_Number(event);"
                                                                TabIndex="280"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AVAILABLE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Debit_Available" runat="server" CssClass="textBox" MaxLength="20"
                                                                TabIndex="281" Width="100px" Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">ADVICE PRINT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Debit_AdPrint" runat="server" CssClass="textBox"
                                                                TabIndex="282" Width="20px" MaxLength="1"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">DETAILS : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Debit_Details" runat="server" CssClass="textBox" Width="300px" MaxLength="30"
                                                                TabIndex="283"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">ENTITY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Debit_Entity" runat="server" CssClass="textBox"
                                                                onkeydown="return validate_Number(event);" TabIndex="284" Width="90px" Style="text-align: right"
                                                                MaxLength="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">DIVISION : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Debit_Division" runat="server" CssClass="textBox" MaxLength="2" onkeydown="return validate_Number(event);"
                                                                TabIndex="285" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Debit_Inter_Amount" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                              MaxLength="16" Width="100px" Style="text-align: right" TabIndex="286"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Debit_Inter_Rate" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                              MaxLength="16" TabIndex="287" Width="100px" Style="text-align: right"></asp:TextBox>
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
                                                            <asp:TextBox ID="txt_Comm_Recieved_Credit_Code" runat="server" CssClass="textBox" MaxLength="1"
                                                                TabIndex="288" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Credit_Curr" runat="server" CssClass="textBox"
                                                                TabIndex="289" Width="25px" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Credit_Amt" runat="server" CssClass="textBox"
                                                                onkeydown="return validate_Number(event);" TabIndex="290" Width="100px"
                                                                Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">CUSTOMER : </span>
                                                        </td>
                                                        <td colspan="4">
                                                            <asp:TextBox ID="txt_Comm_Recieved_Credit_Cust" runat="server" CssClass="textBox" MaxLength="12"
                                                                TabIndex="291" Width="90px"></asp:TextBox>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Credit_Cust_Name" runat="server" CssClass="textBox" MaxLength="40"
                                                                TabIndex="291" Width="300px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C CODE : </span>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txt_Comm_Recieved_Credit_Cust_AcCode" runat="server" CssClass="textBox" MaxLength="5"
                                                                TabIndex="292" Width="90px"></asp:TextBox>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Credit_Cust_AcCode_Name" runat="server" CssClass="textBox" MaxLength="40"
                                                                TabIndex="292" Width="300px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C No : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Credit_Cust_AccNo" runat="server" CssClass="textBox" MaxLength="20"
                                                                TabIndex="293" ></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" align="right">
                                                            <span class="elementLabel">EXCH RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Credit_Exch_Rate" runat="server" CssClass="textBox"
                                                                onkeydown="return validate_Number(event);" TabIndex="294" Width="100px" Style="text-align: right"
                                                                MaxLength="16"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">EXCH CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Credit_Exch_Curr" runat="server" CssClass="textBox"
                                                                MaxLength="3" TabIndex="295" Width="25px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">FUND : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Credit_FUND" runat="server" CssClass="textBox" MaxLength="1"
                                                                TabIndex="296" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CHECK No. : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Credit_Check_No" runat="server" CssClass="textBox" MaxLength="6"
                                                                TabIndex="297"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AVAILABLE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Credit_Available" runat="server" CssClass="textBox" MaxLength="20"
                                                                TabIndex="298" Width="100px" Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">ADVICE PRINT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Credit_AdPrint" runat="server" CssClass="textBox"
                                                                TabIndex="299" Width="20px" MaxLength="1"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">DETAILS : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Credit_Details" runat="server" CssClass="textBox" Width="300px" MaxLength="30"
                                                                TabIndex="300"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">ENTITY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Credit_Entity" runat="server" CssClass="textBox"
                                                                onkeydown="return validate_Number(event);" TabIndex="301" Width="90px" Style="text-align: right"
                                                                MaxLength="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">DIVISION : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Credit_Division" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                MaxLength="2" TabIndex="302" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Credit_Inter_Amount" runat="server" CssClass="textBox"
                                                                TabIndex="303"  MaxLength="16"  Width="100px" Style="text-align: right" onkeydown="return validate_Number(event);"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Comm_Recieved_Credit_Inter_Rate" runat="server" CssClass="textBox"
                                                                TabIndex="304"  MaxLength="16"  Width="100px" Style="text-align: right" onkeydown="return validate_Number(event);"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                                <tr>
                                                    <td></td>
                                                    <td align="left" colspan="5">
                                                        <asp:Button ID="btnComm_Recieved_Prev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                            ToolTip="Back to GENERAL OPERATION 2" TabIndex="262" OnClientClick="return OnDocNextClick(3);" />
                                                    <asp:Button ID="btnComm_Recieved_Next" runat="server" Text="Next>>" CssClass="buttonDefault"
                                                        ToolTip="Go to GENERAL OPERATION 4" TabIndex="263" OnClientClick="return OnDocNextClick(5);" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel ID="tbDocumentGo_Acc_Change" runat="server" HeaderText="GENERAL OPERATION IV"
                                        Font-Bold="true" ForeColor="White">
                                        <ContentTemplate>
                                            <table width="80%">
                                                <tr>
                                                    <td width="20%"></td>
                                                    <td colspan="3">
                                                        <asp:CheckBox ID="cb_GO_Acc_Change_Flag" Text="GENERAL OPERATION IV"
                                                            CssClass="elementLabel" runat="server" OnCheckedChanged="cb_GO_Acc_Change_Flag_OnCheckedChanged" AutoPostBack="true" />
                                                    </td>
                                                </tr>
                                                <asp:Panel ID="PanelGO_Acc_Change" runat="server" Visible="false">
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">COMMENT : </span>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txt_Acc_Change_Comment" runat="server" CssClass="textBox" TabIndex="308" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="20%">
                                                            <span class="elementLabel">Section No : </span>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="txt_Acc_Change_SectionNo" runat="server" CssClass="textBox" TabIndex="309"  MaxLength="2"
                                                                Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right" width="15%">
                                                            <span class="elementLabel">Remarks : </span>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="txt_Acc_Change_Remarks" runat="server"  MaxLength="30" CssClass="textBox" Width="300px" TabIndex="310"></asp:TextBox>
                                                        </td>
                                                        <td align="right" width="15%">
                                                            <span class="elementLabel">MEMO : </span>
                                                        </td>
                                                        <td width="10%">
                                                            <asp:TextBox ID="txt_Acc_Change_Memo" runat="server" CssClass="textBox" Width="50px"  MaxLength="20"
                                                                TabIndex="311"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="15%">
                                                            <span class="elementLabel">SCHEME No : </span>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="txt_Acc_Change_Scheme_no" runat="server" CssClass="textBox" TabIndex="312"  MaxLength="20"></asp:TextBox>
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
                                                            <asp:TextBox ID="txt_Acc_Change_Debit_Code" runat="server" CssClass="textBox" TabIndex="313"  MaxLength="1"
                                                                Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Acc_Change_Debit_Curr" runat="server" CssClass="textBox" TabIndex="314"  MaxLength="3"
                                                                Width="25px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Acc_Change_Debit_Amt" runat="server" CssClass="textBox" Width="100px"
                                                                TabIndex="315" Style="text-align: right" onkeyDown="return validate_Number(event);" MaxLength="16"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">CUSTOMER : </span>
                                                        </td>
                                                        <td colspan="4">
                                                            <asp:TextBox ID="txt_Acc_Change_Debit_Cust" runat="server" CssClass="textBox" TabIndex="316" MaxLength="12"
                                                                Width="90px"></asp:TextBox>
                                                            <asp:TextBox ID="txt_Acc_Change_Debit_Cust_Name" runat="server" CssClass="textBox" MaxLength="40"
                                                                TabIndex="316" Width="300px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C CODE : </span>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txt_Acc_Change_Debit_Cust_AcCode" runat="server" CssClass="textBox" MaxLength="5"
                                                                TabIndex="317" Width="90px"></asp:TextBox>
                                                            <asp:TextBox ID="txt_Acc_Change_Debit_Cust_AcCode_Name" runat="server" CssClass="textBox" MaxLength="40"
                                                                TabIndex="317" Width="300px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C No : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Acc_Change_Debit_Cust_AccNo" runat="server" CssClass="textBox" MaxLength="20"
                                                                TabIndex="318"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" align="right">
                                                            <span class="elementLabel">EXCH RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Acc_Change_Debit_Exch_Rate" runat="server" CssClass="textBox"
                                                                onkeydown="return validate_Number(event);" TabIndex="319" Width="100px" Style="text-align: right"
                                                                MaxLength="16"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">EXCH CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Acc_Change_Debit_Exch_CCY" runat="server" CssClass="textBox"
                                                                MaxLength="3" TabIndex="320" Width="25px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">FUND : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Acc_Change_Debit_FUND" runat="server" CssClass="textBox" TabIndex="321" MaxLength="1"
                                                                Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CHECK No. : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Acc_Change_Debit_Check_No" runat="server" CssClass="textBox" MaxLength="6"
                                                            onkeyDown="return validate_Number(event);" TabIndex="322"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AVAILABLE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Acc_Change_Debit_Available" runat="server" CssClass="textBox"
                                                                TabIndex="323" Width="100px" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">ADVICE PRINT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Acc_Change_Debit_AdPrint" runat="server" CssClass="textBox"
                                                                TabIndex="324" Width="20px" MaxLength="1"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">DETAILS : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Acc_Change_Debit_Details" runat="server" CssClass="textBox" Width="300px" MaxLength="30"
                                                                TabIndex="325"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">ENTITY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Acc_Change_Debit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                TabIndex="326" Width="90px" Style="text-align: right" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">DIVISION : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Acc_Change_Debit_Division" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                TabIndex="327" Width="20px" MaxLength="2"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Acc_Change_Debit_Inter_Amount" runat="server" CssClass="textBox"
                                                                TabIndex="328" Width="100px" Style="text-align: right" MaxLength="16" onkeydown="return validate_Number(event);"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Acc_Change_Debit_Inter_Rate" runat="server" CssClass="textBox"
                                                                TabIndex="329" Width="100px" Style="text-align: right" MaxLength="16" onkeydown="return validate_Number(event);"></asp:TextBox>
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
                                                            <asp:TextBox ID="txt_Acc_Change_Credit_Code" runat="server" CssClass="textBox" TabIndex="330" MaxLength="1"
                                                                Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Acc_Change_Credit_Curr" runat="server" CssClass="textBox" TabIndex="331"
                                                                Width="25px" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Acc_Change_Credit_Amt" runat="server" CssClass="textBox"
                                                                onkeydown="return validate_Number(event);" TabIndex="332" Width="100px" Style="text-align: right"
                                                                MaxLength="16"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">CUSTOMER : </span>
                                                        </td>
                                                        <td colspan="4">
                                                            <asp:TextBox ID="txt_Acc_Change_Credit_Cust" runat="server" CssClass="textBox" TabIndex="333"
                                                                Width="90px" MaxLength="12"></asp:TextBox>
                                                            <asp:TextBox ID="txt_Acc_Change_Credit_Cust_Name" runat="server" CssClass="textBox"
                                                                TabIndex="333" Width="300px" MaxLength="40"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C CODE : </span>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txt_Acc_Change_Credit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                TabIndex="334" Width="90px" MaxLength="5"></asp:TextBox>
                                                            <asp:TextBox ID="txt_Acc_Change_Credit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                                TabIndex="334" Width="300px" MaxLength="40"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C No : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Acc_Change_Credit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                TabIndex="335" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" align="right">
                                                            <span class="elementLabel">EXCH RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Acc_Change_Credit_Exch_Rate" runat="server" CssClass="textBox"
                                                                onkeydown="return validate_Number(event);" TabIndex="336" Width="100px" Style="text-align: right"
                                                                MaxLength="16"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">EXCH CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Acc_Change_Credit_Exch_Curr" runat="server" CssClass="textBox"
                                                                MaxLength="3" TabIndex="337" Width="25px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">FUND : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Acc_Change_Credit_FUND" runat="server" CssClass="textBox" TabIndex="338" MaxLength="1"
                                                                Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CHECK No. : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Acc_Change_Credit_Check_No" runat="server" CssClass="textBox" MaxLength="6"
                                                               onkeydown="return validate_Number(event);" TabIndex="339"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AVAILABLE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Acc_Change_Credit_Available" runat="server" CssClass="textBox"  MaxLength="20"
                                                                TabIndex="340" Width="90px" ></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">ADVICE PRINT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Acc_Change_Credit_AdPrint" runat="server" CssClass="textBox"
                                                                TabIndex="341" Width="20px" MaxLength="1"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">DETAILS : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Acc_Change_Credit_Details" runat="server" CssClass="textBox" Width="300px" MaxLength="30"
                                                                TabIndex="342"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">ENTITY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Acc_Change_Credit_Entity" runat="server" CssClass="textBox"
                                                                onkeydown="return validate_Number(event);" TabIndex="343" Width="90px" Style="text-align: right"
                                                                MaxLength="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">DIVISION : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Acc_Change_Credit_Division" runat="server" CssClass="textBox" MaxLength="2" onkeydown="return validate_Number(event);"
                                                                TabIndex="344" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Acc_Change_Credit_Inter_Amount" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                               Width="100px" Style="text-align: right" MaxLength="16" TabIndex="345"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Acc_Change_Credit_Inter_Rate" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                TabIndex="346" Width="100px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                                <tr>
                                                    <td></td>
                                                    <td align="left" colspan="5">
                                                        <asp:Button ID="btnAcc_Change_Prev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                            ToolTip="Back To GENERAL OPERATION 3" TabIndex="347" OnClientClick="return OnDocNextClick(4);" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel ID="tbSwift" runat="server" HeaderText="SWIFT"
                                        Font-Bold="true" ForeColor="White">
                                        <ContentTemplate>
                                            <ajaxToolkit:TabContainer ID="TabContainerSwift" runat="server" ActiveTabIndex="0"
                                                CssClass="ajax__tab_xp-theme">
                                                <ajaxToolkit:TabPanel ID="tbSwift200" runat="server" HeaderText="MT 200"
                                                    Font-Bold="true" ForeColor="White">
                                                    <ContentTemplate>
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">[20] Transaction Ref No. : </span>
                                                                </td>
                                                                <td align="left" colspan="3">
                                                                    <asp:TextBox ID="txt200TransactionRefNO" runat="server" CssClass="textBox" TabIndex="238"
                                                                        Width="160px" MaxLength="16"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">[32A] Value Date / Currency Code / Amount : </span>
                                                                </td>
                                                                <td align="left" colspan="3">
                                                                    <span class="elementLabel">Value Date : </span>
                                                                    <asp:TextBox ID="txt200Date" runat="server" CssClass="textBox" TabIndex="238"
                                                                        Width="70px" MaxLength="10"></asp:TextBox>
                                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                                                        runat="server" TargetControlID="txt200Date" ErrorTooltipEnabled="True"
                                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                                        CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                                    </ajaxToolkit:MaskedEditExtender>
                                                                    <span class="elementLabel">Currency Code : </span>
                                                                    <asp:TextBox ID="txt200Currency" runat="server" CssClass="textBox" TabIndex="238"
                                                                        Width="30px" MaxLength="3"></asp:TextBox>
                                                                    <span class="elementLabel">Amount : </span>
                                                                    <asp:TextBox ID="txt200Amount" runat="server" CssClass="textBox" TabIndex="238"
                                                                        Width="150px" MaxLength="15"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">[53B] Sender's Correspondent : </span>
                                                                </td>
                                                                <td align="left" colspan="3">
                                                                    <asp:TextBox ID="txt200SenderCorreCode" runat="server" CssClass="textBox" TabIndex="238"
                                                                        Width="300px" MaxLength="35"></asp:TextBox>
                                                                    <asp:TextBox ID="txt200SenderCorreLocation" runat="server" CssClass="textBox" TabIndex="238"
                                                                        Width="300px" MaxLength="35"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">[56A] Intermediary : </span>
                                                                    <asp:DropDownList ID="ddl200Intermediary" runat="server" CssClass="dropdownList"
                                                                        OnSelectedIndexChanged="ddl200Intermediary_TextChanged" AutoPostBack="true">
                                                                        <asp:ListItem Text="56A" Value="A"></asp:ListItem>
                                                                        <asp:ListItem Text="56D" Value="D"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <span class="elementLabel">A / C :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt200IntermediaryAccountNumber" runat="server" CssClass="textBox" TabIndex="238"
                                                                        Width="300px" MaxLength="35"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lbl200IntermediarySwiftCode" runat="server" CssClass="elementLabel" Text="SwiftCode :"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox runat="server" ID="txt200IntermediarySwiftCode" CssClass="textBox" MaxLength="11" Width="100px"></asp:TextBox>
                                                                    <asp:TextBox runat="server" ID="txt200IntermediaryName" CssClass="textBox" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lbl200IntermediaryAddress1" runat="server" CssClass="elementLabel" Visible="false" Text="Address1"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox runat="server" ID="txt200IntermediaryAddress1" CssClass="textBox" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lbl200IntermediaryAddress2" runat="server" CssClass="elementLabel" Visible="false" Text="Address2"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox runat="server" ID="txt200IntermediaryAddress2" CssClass="textBox" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lbl200IntermediaryAddress3" runat="server" CssClass="elementLabel" Visible="false" Text="Address3"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox runat="server" ID="txt200IntermediaryAddress3" CssClass="textBox" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">[57A] Account With Institution : </span>
                                                                    <asp:DropDownList ID="ddl200AccWithInstitution" runat="server" CssClass="dropdownList"
                                                                        OnSelectedIndexChanged="ddl200AccWithInstitution_TextChanged" AutoPostBack="true">
                                                                        <asp:ListItem Text="57A" Value="A"></asp:ListItem>
                                                                        <asp:ListItem Text="57B" Value="B"></asp:ListItem>
                                                                        <asp:ListItem Text="57D" Value="D"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <span class="elementLabel">A / C :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt200AccWithInstitutionAccountNumber" runat="server" CssClass="textBox" TabIndex="238"
                                                                        Width="300px" MaxLength="35"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lbl200AccWithInstitutionSwiftCode" runat="server" CssClass="elementLabel" Text="SwiftCode :"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox runat="server" ID="txt200AccWithInstitutionSwiftCode" CssClass="textBox" MaxLength="11" Width="100px"></asp:TextBox>
                                                                    <asp:TextBox runat="server" ID="txt200AccWithInstitutionLocation" CssClass="textBox" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                                    <asp:TextBox runat="server" ID="txt200AccWithInstitutionName" CssClass="textBox" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lbl200AccWithInstitutionAddress1" runat="server" CssClass="elementLabel" Visible="false" Text="Address1"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox runat="server" ID="txt200AccWithInstitutionAddress1" CssClass="textBox" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lbl200AccWithInstitutionAddress2" runat="server" CssClass="elementLabel" Visible="false" Text="Address2"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox runat="server" ID="txt200AccWithInstitutionAddress2" CssClass="textBox" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lbl200AccWithInstitutionAddress3" runat="server" CssClass="elementLabel" Visible="false" Text="Address3"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox runat="server" ID="txt200AccWithInstitutionAddress3" CssClass="textBox" Visible="false" MaxLength="35" Width="250px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span class="elementLabel">[72] Sender to Receiver Information : </span>
                                                                </td>
                                                                <td align="left" colspan="3">
                                                                    <asp:TextBox ID="txt200SendertoReceiverInformation1" runat="server" CssClass="textBox" TabIndex="238"
                                                                        Width="300px" MaxLength="35"></asp:TextBox>
                                                                    <asp:TextBox ID="txt200SendertoReceiverInformation2" runat="server" CssClass="textBox" TabIndex="238"
                                                                        Width="300px" MaxLength="35"></asp:TextBox>
                                                                    <asp:TextBox ID="txt200SendertoReceiverInformation3" runat="server" CssClass="textBox" TabIndex="238"
                                                                        Width="300px" MaxLength="35"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right"></td>
                                                                <td align="left" colspan="3">
                                                                    <asp:TextBox ID="txt200SendertoReceiverInformation4" runat="server" CssClass="textBox" TabIndex="238"
                                                                        Width="300px" MaxLength="35"></asp:TextBox>
                                                                    <asp:TextBox ID="txt200SendertoReceiverInformation5" runat="server" CssClass="textBox" TabIndex="238"
                                                                        Width="300px" MaxLength="35"></asp:TextBox>
                                                                    <asp:TextBox ID="txt200SendertoReceiverInformation6" runat="server" CssClass="textBox" TabIndex="238"
                                                                        Width="300px" MaxLength="35"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel ID="tbSwiftR42" runat="server" HeaderText="R 42"
                                                    Font-Bold="true" ForeColor="White">
                                                    <ContentTemplate>
                                                        <table width="50%">
                                                            <tr>
                                                                <td align="right"><span class="elementLabel">[2020] Transaction Ref No : </span></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtTransactionRefNoR42" runat="server" CssClass="textBox" TabIndex="238"
                                                                        Width="110px" MaxLength="14"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right"><span class="elementLabel">[2006] Related Reference : </span></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtRelatedReferenceR42" runat="server" CssClass="textBox" TabIndex="238"
                                                                        Width="125px" MaxLength="16"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right"><span class="elementLabel">[4488] Value Date/Currency/Amount : </span></td>
                                                                <td><span class="elementLabel">Date</span><asp:TextBox ID="txtValueDateR42" runat="server" CssClass="textBox" TabIndex="238"
                                                                    Width="80px" MaxLength="10"></asp:TextBox>
                                                                    <ajaxToolkit:MaskedEditExtender ID="ME_Received_date" Mask="99/99/9999" MaskType="Date"
                                                                        runat="server" TargetControlID="txtValueDateR42" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                                        CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                                        CultureDatePlaceholder="/" Enabled="True">
                                                                    </ajaxToolkit:MaskedEditExtender>
                                                                    <asp:Button ID="btncal_Received_date" runat="server" CssClass="btncalendar_enabled"
                                                                        TabIndex="2" />
                                                                    <ajaxToolkit:CalendarExtender ID="CE_Received_date" runat="server" Format="dd/MM/yyyy"
                                                                        TargetControlID="txtValueDateR42" PopupButtonID="btncal_Received_date" Enabled="True">
                                                                    </ajaxToolkit:CalendarExtender>
                                                                    <span class="elementLabel">Currency</span><asp:TextBox ID="txtCureencyR42" runat="server" CssClass="textBox" TabIndex="238"
                                                                        Width="25px" MaxLength="3"></asp:TextBox>
                                                                    <span class="elementLabel">Amount</span><asp:TextBox ID="txtAmountR42" runat="server" CssClass="textBox" TabIndex="238"
                                                                        Width="110px" MaxLength="16"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right"><span class="elementLabel">[5517] Ordering Institution IFSC : </span></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtOrderingInstitutionIFSCR42" runat="server" CssClass="textBox" TabIndex="238"
                                                                        Width="90px" MaxLength="11"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right"><span class="elementLabel">[6521] Beneficiary Institution IFSC : </span></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtBeneficiaryInstitutionIFSCR42" runat="server" CssClass="textBox" TabIndex="238"
                                                                        Width="90px" MaxLength="11"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right"><span class="elementLabel">[7495] Sender to Receiver Info : </span></td>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right"><span class="elementLabel">Code Word </span></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCodeWordR42" runat="server" CssClass="textBox" TabIndex="238"
                                                                        Width="25px" MaxLength="3" Text="TRF"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right"><span class="elementLabel">Additional Information </span></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAdditionalInformationR42" runat="server" CssClass="textBox" TabIndex="238"
                                                                        Width="500px" TextMode="MultiLine"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right"><span class="elementLabel">More Info </span></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtMoreInfo1R42" runat="server" CssClass="textBox" TabIndex="238"
                                                                        Width="500px" MaxLength="50"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtMoreInfo2R42" runat="server" CssClass="textBox" TabIndex="238"
                                                                        Width="500px" MaxLength="50"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtMoreInfo3R42" runat="server" CssClass="textBox" TabIndex="238"
                                                                        Width="500px" MaxLength="50"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtMoreInfo4R42" runat="server" CssClass="textBox" TabIndex="238"
                                                                        Width="500px" MaxLength="50"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtMoreInfo5R42" runat="server" CssClass="textBox" TabIndex="238"
                                                                        Width="500px" MaxLength="50"></asp:TextBox></td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                            </ajaxToolkit:TabContainer>
                                            <table width="100%">
                                                <tr>
                                                    <td width="10%"></td>
                                                    <td width="90%">
                                                        <span class="elementLabel">SELECT FILE TYPE :</span>
                                                        <asp:CheckBox ID='rdb_swift_None' Text="None" CssClass="elementLabel" runat="server"
                                                            onchange="return Toggel_Swift_Type('None');" Checked="true" />
                                                        <asp:CheckBox ID='rdb_swift_200' Text="MT 200" CssClass="elementLabel" runat="server"
                                                            onchange="return Toggel_Swift_Type('MT200');" />
                                                        <asp:CheckBox ID='rdb_swift_R42' Text="R42" CssClass="elementLabel" runat="server"
                                                            onchange="return Toggel_Swift_Type('R42');" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td align="left">
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
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit to Checker" CssClass="buttonDefault"
                                    Width="150px" ToolTip="Submit to Checker" TabIndex="256" OnClick="btnSubmit_Click" OnClientClick="return SubmitCheck();" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="5">
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
