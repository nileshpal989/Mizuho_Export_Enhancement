<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_LC_DESCOUNTING_DISCREPANT_ACC_IBD_Maker.aspx.cs"
    Inherits="IMP_Transactions_TF_IMP_LC_DESCOUNTING_DISCREPANT_ACC_IBD_Maker" %>

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
    <script src="../Scripts/TF_IMP_LC_DESCOUNTING_DISCREPANT_ACC_IBD_Maker.js" type="text/javascript"></script>
    <script id="Save_script" language="javascript" type="text/javascript">
        $(document).ready(function () {
            //           Configure to save every 5 seconds  
            window.setInterval(SaveUpdateData, 5000); //calling saveDraft function for every 5 seconds  
        });
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
                            <span class="pageLabel"><strong>Own LC Bill Discounting (IBD,ACC)- Maker</strong></span>
                        </td>
                        <td align="right" style="width: 50%" valign="bottom">
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                OnClientClick="return SaveUpdateData();" OnClick="btnBack_Click" />
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
                            <input type="hidden" id="hdnUserName" runat="server" />
                            <input type="hidden" id="hdnBranchName" runat="server" />
                            <input type="hidden" id="hdnDocType" runat="server" />
                            <input type="hidden" id="hdnMode" runat="server" />
                            <input type="hidden" id="hdnNegoRemiBankType" runat="server" />
                            <input type="hidden" id="hdnDiscrepancyChrg" runat="server" />
                            <input type="hidden" id="hdnTaxChrg" runat="server" />
                            <input type="hidden" id="hdnIBDExtnFlag" runat="server" />
                        </td>
                    </tr>
                </table>
                <table id="tbl_LCDiscount" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="right" width="10%">
                            <span class="elementLabel">IBD. Ref. No :</span>
                        </td>
                        <td width="30%" align="left">
                            <asp:TextBox ID="txtIBDDocNo" Width="110px" runat="server" CssClass="textBox" TabIndex="1"
                                OnTextChanged="txtIBDDocNo_CheckedChanged" AutoPostBack="true"></asp:TextBox>
                            <asp:Button ID="btn_IBDDocNo_Help" runat="server" ToolTip="Press for IBD Ref No list."
                                CssClass="btnHelp_enabled" OnClientClick="return OpenIBDDocNo_help('mouseClick');" />
                            &nbsp;
                            <asp:Label ID="lblForeign_Local" runat="server" CssClass="elementLabel"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblLCDescount_Lodgment_UnderLC" runat="server" CssClass="elementLabel"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblSight_Usance" runat="server" CssClass="elementLabel"></asp:Label>
                        </td>
                        <td width="40%" align="left">
                            <span class="elementLabel">Val.Date :</span>
                            <asp:TextBox ID="txtValueDate" runat="server" TabIndex="2" CssClass="textBox" MaxLength="10"
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
                        <td align="right" width="30%">
                            <span class="elementLabel">Trans. Ref. No :</span>
                            <asp:TextBox Enabled="false" ID="txtDocNo" Width="110px" runat="server" CssClass="textBox"
                                TabIndex="3"></asp:TextBox>
                            <asp:Panel ID="PanelIBDExtn" runat="server" Visible="false">
                                <span class="elementLabel">IBD.Extn.No:</span>
                                <asp:TextBox Enabled="false" ID="txtIBDExtnNo" Width="110px" runat="server" CssClass="textBox"
                                    TabIndex="3"></asp:TextBox>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="5">
                            <ajaxToolkit:TabContainer ID="TabContainerMain" runat="server" ActiveTabIndex="3"
                                CssClass="ajax__tab_xp-theme">
                                <ajaxToolkit:TabPanel ID="tbDocumentDetails" runat="server" HeaderText="DOCUMENT DETAILS"
                                    Font-Bold="true" ForeColor="White">
                                    <ContentTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2">
                                                    <span class="elementLabel">
                                                        <asp:RadioButton ID="rdbIDOA" runat="server" GroupName="A1" TabIndex="4" Text="INTEREST+DISCREPANCY ON APPLICANT"
                                                            AutoPostBack="True" OnCheckedChanged="rdbIDOA_CheckedChanged"></asp:RadioButton>
                                                    </span>
                                                </td>
                                                <td colspan="2">
                                                    <span class="elementLabel">
                                                        <asp:RadioButton ID="rdbIDOB" runat="server" GroupName="A1" TabIndex="4" Text="INTEREST+DISCREPANCY ON BENEFICIARY"
                                                            AutoPostBack="True" OnCheckedChanged="rdbIDOB_CheckedChanged"></asp:RadioButton>
                                                    </span>
                                                </td>
                                                <td colspan="2">
                                                    <span class="elementLabel">
                                                        <asp:RadioButton ID="rdbIOBDOA" runat="server" GroupName="A1" TabIndex="4" Text="INTEREST ON BENE+DISCREPANCY ON APP"
                                                            AutoPostBack="True" OnCheckedChanged="rdbIOBDOA_CheckedChanged"></asp:RadioButton>
                                                    </span>
                                                </td>
                                                <td colspan="2">
                                                    <span class="elementLabel">
                                                        <asp:RadioButton ID="rdbIOADOB" runat="server" GroupName="A1" TabIndex="4" Text="INTEREST ON APPLICANT+DISCREPANCY ON BENEFICIARY"
                                                            AutoPostBack="True" OnCheckedChanged="rdbIOADOB_CheckedChanged"></asp:RadioButton>
                                                    </span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 100%" valign="middle" colspan="8">
                                                    <hr />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">CUSTOMER :</span>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:TextBox ID="txtCustName" runat="server" AutoPostBack="True" CssClass="textBox"
                                                        TabIndex="4" MaxLength="40" Width="350px"></asp:TextBox>
                                                    <%--<asp:Label ID="lblCustomerDesc" runat="server" CssClass="elementLabel"></asp:Label>--%>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">HO APL :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtHO_Apl" runat="server" CssClass="textBox" MaxLength="9" TabIndex="9"
                                                        Width="100px"></asp:TextBox>
                                                </td>
                                                <td align="left" colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">IBD/ACC KIND :</span>
                                                </td>
                                                <td align="left" width="10%">
                                                    <asp:TextBox ID="txtIBD_ACC_kind" runat="server" CssClass="textBox" MaxLength="1"
                                                        TabIndex="10" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">Value Date1 :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtvalueDate1" runat="server" AutoPostBack="True" CssClass="textBox"
                                                        MaxLength="10" OnTextChanged="Get_LCDescounting_Get_Date_Diff" TabIndex="104"
                                                        ValidationGroup="dtVal" Width="80px"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder="AM;PM"
                                                        CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY" CultureDatePlaceholder="/"
                                                        CultureDecimalPlaceholder="." CultureName="en-GB" CultureThousandsPlaceholder=","
                                                        CultureTimePlaceholder=":" Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txtvalueDate1">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btnCal_txtvalueDate1" runat="server" CssClass="btncalendar_enabled" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtendertxtvalueDate1" runat="server" Enabled="True"
                                                        Format="dd/MM/yyyy" PopupButtonID="btnCal_txtvalueDate1" TargetControlID="txtvalueDate1">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">COMMENT CODE :</span>
                                                </td>
                                                <td align="left" width="10%">
                                                    <asp:TextBox ID="txtCommentCode" runat="server" CssClass="textBox" MaxLength="2"
                                                        TabIndex="11" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">AUTO STTL :</span>
                                                </td>
                                                <td align="left" width="30%">
                                                    <asp:TextBox ID="txtAutoSettlement" runat="server" CssClass="textBox" MaxLength="1"
                                                        TabIndex="12" Width="40px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">DRAFT AMT :</span>
                                                </td>
                                                <td align="left" width="10%">
                                                    <asp:TextBox ID="txtDraftAmt" runat="server" CssClass="textBox" MaxLength="16" Width="120px"
                                                        TabIndex="13" onkeydown="return validate_Number(event);" Style="text-align: right"
                                                        onchange="return Toggel_Bill_Amt();"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">CONTRACT NO :</span>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:TextBox ID="txtContractNo" runat="server" CssClass="textBox" MaxLength="9" TabIndex="15"
                                                        Width="90px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">EXCH RATE :</span>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:TextBox ID="txtExchRate" runat="server" CssClass="textBox" MaxLength="11" TabIndex="16"
                                                        Width="120px" onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">IBD AMT :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtIBDAmt" runat="server" CssClass="textBox" MaxLength="16" Width="120px"
                                                        TabIndex="17" onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">COUNTRY RISK :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtCountryRisk" runat="server" CssClass="textBox" MaxLength="2"
                                                        TabIndex="18" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">RISK CUST :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtRiskCust" runat="server" CssClass="textBox" MaxLength="12" TabIndex="19"
                                                        Width="140px" onkeyup="return CustAbbr();"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">GRADE CODE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtGradeCode" runat="server" CssClass="textBox" MaxLength="2" TabIndex="20"
                                                        Width="40px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">APPL. NO :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtApplNo" runat="server" CssClass="textBox" MaxLength="7" TabIndex="21"
                                                        Width="90px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">APPL BR :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtApplBR" runat="server" CssClass="textBox" MaxLength="3" TabIndex="22"
                                                        Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">PURPOSE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtPurpose" runat="server" CssClass="textBox" MaxLength="7" TabIndex="23"
                                                        Width="25px"></asp:TextBox>
                                                </td>
                                                <td colspan="2">
                                                    <span class="elementLabel">PURPOSE CODE:</span>
                                                    <asp:DropDownList ID="ddl_PurposeCode" runat="server" CssClass="dropdownList" MaxLength="3"
                                                        TabIndex="24" Width="60px" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lbl_PurposeCodeDesc" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">SETTL FOR CUST :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtsettlCodeForCust" runat="server" CssClass="textBox" TabIndex="25"
                                                        MaxLength="7" Width="50px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">ABBR :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtsettlforCust_Abbr" runat="server" CssClass="textBox" MaxLength="12"
                                                        TabIndex="26" Width="140px" onkeyup="return DebitCredit_For_IMP_ACC();"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">A/C CODE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtsettlforCust_AccCode" runat="server" CssClass="textBox" MaxLength="5"
                                                        TabIndex="27" Width="50px" onkeyup="return DebitCredit_For_IMP_ACC();"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">A/C NO :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtsettlforCust_AccNo" runat="server" CssClass="textBox" MaxLength="20"
                                                        TabIndex="28" Width="160px" onkeyup="return DebitCredit_For_IMP_ACC();"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">INTEREST FROM :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtInterest_From" runat="server" AutoPostBack="True" CssClass="textBox"
                                                        MaxLength="10" TabIndex="29" ValidationGroup="dtVal" Width="80px"></asp:TextBox>
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
                                                <td align="right">
                                                    <span class="elementLabel">INTEREST TO :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtInterest_To" OnTextChanged="Get_LCDescounting_Get_Date_Diff"
                                                        runat="server" AutoPostBack="True" CssClass="textBox" MaxLength="10" TabIndex="30"
                                                        ValidationGroup="dtVal" Width="80px"></asp:TextBox>
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
                                                    <asp:TextBox ID="txt_No_Of_Days" runat="server" CssClass="textBox" MaxLength="3"
                                                        TabIndex="31" Width="50px" onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">RATE(%) :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_INT_Rate" runat="server" CssClass="textBox" MaxLength="10" TabIndex="32"
                                                        Width="70px" onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">BASE RATE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtBaseRate" runat="server" CssClass="textBox" MaxLength="2" TabIndex="33"
                                                        Width="70px" onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">SPREAD(%) :</span>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:TextBox ID="txtSpread" runat="server" CssClass="textBox" MaxLength="9" TabIndex="34"
                                                        Width="70px" onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">INT. AMT :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtInterestAmt" runat="server" AutoPostBack="True" CssClass="textBox"
                                                        MaxLength="16" TabIndex="35" Width="120px" onchange="return ONINTERESTAMT();"
                                                        onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">FUND TYPE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtFundType" runat="server" CssClass="textBox" MaxLength="1" TabIndex="36"
                                                        Width="20px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">INTERNAL RATE :</span>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:TextBox ID="txtInternalRate" runat="server" CssClass="textBox" MaxLength="10"
                                                        onkeyDown="return validate_Number(event);" Style="text-align: right" TabIndex="37"
                                                        Width="70px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">SETTL FOR BANK :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtSettl_CodeForBank" runat="server" CssClass="textBox" MaxLength="12"
                                                        TabIndex="38" Width="50px"></asp:TextBox>
                                                    <asp:Button ID="btnCR_Code_help" runat="server" ToolTip="Press for AcCode list."
                                                        CssClass="btnHelp_enabled" OnClientClick="return OpenSettl_CodeForBank_help('mouseClick');" />
                                                    <asp:Label ID="lblSettl_ForBank" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">ABBR :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtSettl_ForBank_Abbr" runat="server" CssClass="textBox" MaxLength="12"
                                                        TabIndex="39" Width="140px" onkeyup="return DebitCredit_For_IMP_ACC();"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">A/C CODE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtSettl_ForBank_AccCode" runat="server" CssClass="textBox" MaxLength="5"
                                                        TabIndex="41" Width="50px" onkeyup="return DebitCredit_For_IMP_ACC();"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">A/C NO :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtSettl_ForBank_AccNo" runat="server" CssClass="textBox" MaxLength="20"
                                                        TabIndex="38" Width="160px" onkeyup="return DebitCredit_For_IMP_ACC();"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">NEGO. BANK TYPE :</span>
                                                </td>
                                                <td align="left" colspan="7">
                                                    <asp:TextBox ID="txtNego_Remit_Bank_Type" runat="server" CssClass="textBox" MaxLength="12"
                                                        TabIndex="42" Width="100px"></asp:TextBox>
                                                    &nbsp;&nbsp;<span class="elementLabel">NEGOTIATING BANK :</span>
                                                    <asp:TextBox ID="txtNego_Remit_Bank" runat="server" CssClass="textBox" MaxLength="12"
                                                        TabIndex="43" Width="100px"></asp:TextBox>
                                                    <asp:Label ID="lbl_Nego_Remit_Bank" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">ACC. WITH BANK :</span>
                                                </td>
                                                <td align="left" colspan="7">
                                                    <asp:TextBox ID="txtAcwithInstitution" runat="server" CssClass="textBox" MaxLength="12"
                                                        TabIndex="44" Width="100px"></asp:TextBox>
                                                    <asp:Label ID="lblAcWithInstiBankDesc" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">REIMBURSING BANK :</span>
                                                </td>
                                                <td align="left" colspan="7">
                                                    <asp:TextBox ID="txtReimbursingbank" runat="server" CssClass="textBox" MaxLength="12"
                                                        TabIndex="45" Width="100px"></asp:TextBox>
                                                    <asp:Label ID="lbl_Reimbursingbank" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">REM(EUC) :</span>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:TextBox ID="txtREM_EUC" runat="server" CssClass="textBox" MaxLength="20" TabIndex="46"
                                                        Width="200px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">ATTN :</span>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:TextBox ID="txtAttn" runat="server" CssClass="textBox" MaxLength="70" TabIndex="47"
                                                        Width="350px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                </td>
                                                <td colspan="6">
                                                    <asp:Button ID="btnDocNext" runat="server" CssClass="buttonDefault" OnClientClick="return OnDocNextClick(1);"
                                                        TabIndex="48" Text="Next &gt;&gt;" ToolTip="Go to Instructions" />
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
                                                    <asp:TextBox ID="txtDrawer" runat="server" CssClass="textBox" MaxLength="70" TabIndex="46"
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
                                                <td align="left" colspan="2">
                                                    <asp:DropDownList ID="ddlTenor" runat="server" CssClass="dropdownList" TabIndex="47">
                                                        <asp:ListItem Text="-Select-"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="SIGHT"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="DEF Pmt"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="MIX pmt"></asp:ListItem>
                                                        <asp:ListItem Value="4" Text="Others"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtTenor" Width="45px" runat="server" CssClass="textBox" TabIndex="48"
                                                        MaxLength="3"></asp:TextBox>
                                                    <span class="elementLabel">DAYS FROM</span>&nbsp;&nbsp;
                                                    <asp:DropDownList ID="ddlTenor_Days_From" runat="server" CssClass="dropdownList"
                                                        TabIndex="49">
                                                        <asp:ListItem Text="-Select-"></asp:ListItem>
                                                        <asp:ListItem Value="SHIPMENT DATE" Text="SHIPMENT DATE"></asp:ListItem>
                                                        <asp:ListItem Value="INVOICE DATE" Text="INVOICE DATE"></asp:ListItem>
                                                        <asp:ListItem Value="BOEXCHANGE DATE" Text="BOEXCHANGE DATE"></asp:ListItem>
                                                        <asp:ListItem Value="OTHERS/BLANK" Text="OTHERS/BLANK"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtTenor_Description" runat="server" CssClass="textBox" MaxLength="31"
                                                        TabIndex="50" Width="230px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">ARRIVAL DATE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDateArrival" runat="server" CssClass="textBox" MaxLength="10"
                                                        ValidationGroup="dtVal" Width="70px" TabIndex="51"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="ME_Arrival_date" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtDateArrival" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                        CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                        CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                        CultureTimePlaceholder=":">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btncal_Arrival_date" runat="server" CssClass="btncalendar_enabled"
                                                        TabIndex="52" />
                                                    <ajaxToolkit:CalendarExtender ID="CE_Arrival_date" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtDateArrival" PopupButtonID="btncal_Arrival_date" Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <ajaxToolkit:MaskedEditValidator ID="MV_Arrival_date" runat="server" ControlExtender="ME_Arrival_date"
                                                        ValidationGroup="dtVal" ControlToValidate="txtDateArrival" EmptyValueMessage="Enter Date Value"
                                                        InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                        Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                                </td>
                                                <td align="left">
                                                    <span class="elementLabel">THEIR REF.NO :</span>
                                                    <asp:TextBox ID="txt_Their_Ref_no" runat="server" CssClass="textBox" MaxLength="20"
                                                        TabIndex="53" Width="160px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">COMMODITY CODE :</span>
                                                </td>
                                                <td align="left" colspan="2">
                                                    <asp:TextBox ID="txtCommodity" runat="server" CssClass="textBox" TabIndex="54" Width="50px"
                                                        AutoPostBack="True"></asp:TextBox>
                                                    <asp:Label ID="lblCommodityDesc" runat="server" CssClass="elementLabel"></asp:Label>
                                                    &nbsp;
                                                    <asp:TextBox ID="txtCommodityDesc" runat="server" CssClass="textBox" MaxLength="100"
                                                        TabIndex="55" Width="250px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">SHIPPMENT DATE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtShippingDate" runat="server" CssClass="textBox" MaxLength="10"
                                                        ValidationGroup="dtVal" Width="70px" TabIndex="56"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="ME_Ship_Date" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtShippingDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                        CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                        CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                        CultureTimePlaceholder=":">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btnCal_Ship_Date" runat="server" CssClass="btncalendar_enabled" TabIndex="57" />
                                                    <ajaxToolkit:CalendarExtender ID="CE_Ship_Date" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtShippingDate" PopupButtonID="btnCal_Ship_Date" Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <ajaxToolkit:MaskedEditValidator ID="MV_Ship_Date" runat="server" ControlExtender="ME_Ship_Date"
                                                        ValidationGroup="dtVal" ControlToValidate="txtShippingDate" EmptyValueMessage="Enter Date Value"
                                                        InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                        Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                                </td>
                                                <td align="left">
                                                    <span class="elementLabel">VESSEL NAME :</span>
                                                    <asp:TextBox ID="txtVesselName" runat="server" CssClass="textBox" MaxLength="30"
                                                        TabIndex="58" Width="300px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">FROM :</span>
                                                </td>
                                                <td align="left" colspan="2">
                                                    <asp:TextBox ID="txtFromPort" runat="server" CssClass="textBox" MaxLength="30" TabIndex="59"
                                                        Width="100px"></asp:TextBox>
                                                    &nbsp;&nbsp;&nbsp;<span class="elementLabel">To :</span>
                                                    <asp:TextBox ID="txtToPort" runat="server" CssClass="textBox" MaxLength="30" TabIndex="60"
                                                        Width="100px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" width="100%" colspan="3">
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
                                                            <td align="left" width="70%">
                                                                &nbsp;&nbsp;<span class="elementLabel" style="font-weight: bold">AWB</span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">FIRST :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtDocFirst1" runat="server" CssClass="textBox" MaxLength="3" TabIndex="61"
                                                                    Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                </span>
                                                                <asp:TextBox ID="txtDocFirst2" runat="server" CssClass="textBox" MaxLength="3" TabIndex="62"
                                                                    Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtDocFirst3" runat="server" CssClass="textBox" MaxLength="3" TabIndex="63"
                                                                    Width="100px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">SECOND :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtDocSecond1" runat="server" CssClass="textBox" MaxLength="3" TabIndex="64"
                                                                    Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtDocSecond2" runat="server" CssClass="textBox" MaxLength="3" TabIndex="64"
                                                                    Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtDocSecond3" runat="server" CssClass="textBox" MaxLength="3" TabIndex="64"
                                                                    Width="100px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="3">
                                                </td>
                                                <td>
                                                    <asp:Button ID="btn_DiscrepancyList" runat="server" Text="DISCREPANCIES" CssClass="buttonDefault"
                                                        Height="20px" Width="150px" Enabled="False"></asp:Button>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel"><b>INST CODE :</b></span>
                                                </td>
                                                <td align="left" width="10%">
                                                    <asp:TextBox ID="txt_INST_Code" runat="server" MaxLength='2' CssClass="textBox" TabIndex="65"
                                                        Width="50px" onkeydown="return validate_Number(event);"></asp:TextBox>
                                                    <span class="elementLabel">: </span>
                                                </td>
                                                <td align="left" width="30%">
                                                    <asp:Label ID="lbl_Instructions1" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                                <td align="left" width="50%">
                                                    <%--    <asp:RadioButton ID="rdb_SP_Instr_Other" Text="Others" CssClass="elementLabel" runat="server"
                                                        GroupName="SP_Instruction" />
                                                    <asp:RadioButton ID="rdb_SP_Instr_Annexure" Text="As per Annexure" CssClass="elementLabel"
                                                        runat="server" GroupName="SP_Instruction" />
                                                    <asp:RadioButton ID="rdb_SP_Instr_On_Sight" Text="Bene. to be paid on sight" CssClass="elementLabel"
                                                        runat="server" GroupName="SP_Instruction1" />
                                                    <asp:RadioButton ID="rdb_SP_Instr_On_Date" Text="Bene. to be paid on Dated" CssClass="elementLabel"
                                                        runat="server" GroupName="SP_Instruction1" />--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                </td>
                                                <td align="left">
                                                    <%-- <asp:Label ID="lbl_Instructions2" runat="server" CssClass="elementLabel"></asp:Label>--%>
                                                </td>
                                                <td align="left">
                                                    <span class="elementLabel">1.</span>
                                                    <asp:TextBox ID="txt_SP_Instructions1" Width="400px" runat="server" CssClass="textBox"
                                                        MaxLength="50" TabIndex="66"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                </td>
                                                <td align="left">
                                                    <%--<asp:Label ID="lbl_Instructions3" runat="server" CssClass="elementLabel"></asp:Label>--%>
                                                </td>
                                                <td align="left">
                                                    <span class="elementLabel">2.</span>
                                                    <asp:TextBox ID="txt_SP_Instructions2" Width="400px" runat="server" CssClass="textBox"
                                                        MaxLength="50" TabIndex="67"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                </td>
                                                <td align="left">
                                                    <%-- <asp:Label ID="lbl_Instructions4" runat="server" CssClass="elementLabel"></asp:Label>--%>
                                                </td>
                                                <td align="left">
                                                    <span class="elementLabel">3.</span>
                                                    <asp:TextBox ID="txt_SP_Instructions3" Width="400px" runat="server" CssClass="textBox"
                                                        MaxLength="50" TabIndex="68"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                </td>
                                                <td align="left">
                                                    <span class="elementLabel">4.</span>
                                                    <asp:TextBox ID="txt_SP_Instructions4" Width="400px" runat="server" CssClass="textBox"
                                                        MaxLength="50" TabIndex="69"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                </td>
                                                <td align="left">
                                                    <asp:Button ID="btn_Instr_Prev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                        ToolTip="Back to Document Details" TabIndex="71" OnClientClick="return OnDocNextClick(0);" />&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btn_Instr_Next" runat="server" Text=">> Next" CssClass="buttonDefault"
                                                        ToolTip="Go to Import Accounting" TabIndex="71" OnClientClick="return OnDocNextClick(2);" />
                                                </td>
                                                <td align="left">
                                                    <span class="elementLabel">5.</span>
                                                    <asp:TextBox ID="txt_SP_Instructions5" Width="400px" runat="server" CssClass="textBox"
                                                        MaxLength="50" TabIndex="70"></asp:TextBox>
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
                                                </td>
                                                <td align="right" colspan="2">
                                                    <span class="elementLabel">AMOUNT :</span><asp:TextBox ID="txt_DiscAmt" runat="server"
                                                        CssClass="textBox" TabIndex="72" MaxLength="16" Width="100px" onkeydown="return validate_Number(event);"
                                                        Style="text-align: right"></asp:TextBox>
                                                </td>
                                                <td align="left" colspan="2">
                                                    <span class="elementLabel">EXCH RATE :</span><asp:TextBox ID="txt_IMP_ACC_ExchRate"
                                                        runat="server" CssClass="textBox" TabIndex="73" MaxLength="9" Width="70px" Style="text-align: right"
                                                        onkeydown="return validate_Number(event);"></asp:TextBox>
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
                                                    <asp:TextBox ID="txtPrinc_matu" runat="server" CssClass="textBox" MaxLength="1" TabIndex="74"
                                                        Width="40px" onkeydown="return validate_Commission_MATU_Code(event);"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtPrinc_lump" runat="server" CssClass="textBox" MaxLength="1" TabIndex="75"
                                                        Width="40px" onkeydown="return validate_Commission_LUMP_Code(event);"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtprinc_Contract_no" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="76" Width="90px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_Princ_Ex_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                        TabIndex="77" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtprinc_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="78" onkeydown="return validate_Number(event);" Style="text-align: right"
                                                        Width="70px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtprinc_Intnl_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="79" onkeydown="return validate_Number(event);" Style="text-align: right"
                                                        Width="70px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">INTEREST :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtInterest_matu" runat="server" AutoPostBack="true" CssClass="textBox"
                                                        MaxLength="1" onkeydown="return validate_Commission_MATU_Code(event);" OnTextChanged="txtInterest_matu_TextChanged"
                                                        TabIndex="80" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtInterest_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                        onkeydown="return validate_Commission_LUMP_Code(event);" TabIndex="81" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtInterest_Contract_no" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="82" Width="90px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_interest_Ex_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                        TabIndex="83" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtInterest_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                        onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="84"
                                                        Width="70px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtInterest_Intnl_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                        onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="85"
                                                        Width="70px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">COMMISSION :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtCommission_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                        TabIndex="86" Width="40px" onkeydown="return validate_Commission_MATU_Code(event);"
                                                        onkeyup="return Commission_Toggel();"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtCommission_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                        TabIndex="87" Width="40px" onkeydown="return validate_Commission_LUMP_Code(event);"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtCommission_Contract_no" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="88" Width="90px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_Commission_Ex_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                        TabIndex="89" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtCommission_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="89" onkeydown="return validate_Number(event);" Style="text-align: right"
                                                        Width="70px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtCommission_Intnl_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="90" onkeydown="return validate_Number(event);" Style="text-align: right"
                                                        Width="70px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">THEIR COMMISSION :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtTheir_Commission_matu" runat="server" CssClass="textBox" MaxLength="1"
                                                        TabIndex="91" Width="40px" onkeydown="return validate_Commission_MATU_Code(event);"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtTheir_Commission_lump" runat="server" CssClass="textBox" MaxLength="1"
                                                        TabIndex="92" Width="40px" onkeydown="return validate_Commission_LUMP_Code(event);"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtTheir_Commission_Contract_no" runat="server" CssClass="textBox"
                                                        TabIndex="93" MaxLength="9" Width="90px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_Their_Commission_Ex_Curr" runat="server" CssClass="textBox"
                                                        TabIndex="94" MaxLength="3" Width="40px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtTheir_Commission_Ex_rate" runat="server" CssClass="textBox" MaxLength="9"
                                                        TabIndex="95" onkeydown="return validate_Number(event);" Style="text-align: right"
                                                        Width="70px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtTheir_Commission_Intnl_Ex_rate" runat="server" CssClass="textBox"
                                                        TabIndex="96" onkeydown="return validate_Number(event);" Style="text-align: right"
                                                        MaxLength="9" Width="70px"></asp:TextBox>
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
                                                            <td width="15%" align="left">
                                                                <span class="elementLabel"><b>/CR/</b> CODE :</span>
                                                                <asp:Button ID="btn_CR_Code_help" runat="server" ToolTip="Press for AcCode list."
                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenCR_Code_help('mouseClick','IMPACC3','Credit');" />
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
                                                                    TabIndex="97" Width="90px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_CR_AC_ShortName" runat="server" CssClass="textBox" MaxLength="20"
                                                                    TabIndex="98" Width="160px"></asp:TextBox>
                                                                <%-- <span class="elementLabel">RSV DEPO TO RBI</span>--%>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_CR_Cust_abbr" runat="server" CssClass="textBox" MaxLength="12"
                                                                    TabIndex="99" Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_CR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="20"
                                                                    TabIndex="100" Width="160px"></asp:TextBox>
                                                            </td>
                                                            <td align="center">
                                                                <asp:TextBox ID="txt_CR_Acceptance_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                    TabIndex="101" Width="40px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_CR_Acceptance_amt" runat="server" CssClass="textBox" MaxLength="9"
                                                                    onkeydown="return validate_Number(event);" TabIndex="102" Width="90px" Style="text-align: right"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_CR_Acceptance_payer" runat="server" CssClass="textBox" MaxLength="1"
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
                                                                <asp:TextBox ID="txt_CR_Interest_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                    TabIndex="104" Width="40px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_CR_Interest_amt" runat="server" CssClass="textBox" MaxLength="9"
                                                                    onkeydown="return validate_Number(event);" TabIndex="105" Width="90px" Style="text-align: right"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_CR_Interest_payer" runat="server" CssClass="textBox" MaxLength="1"
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
                                                                    <asp:TextBox ID="txt_CR_Accept_Commission_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="107" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_CR_Accept_Commission_amt" runat="server" CssClass="textBox"
                                                                        MaxLength="9" onkeydown="return validate_Number(event);" onkeyup="return Toggel_IMP_ACC_Coll_Comm();"
                                                                        Style="text-align: right" TabIndex="108" Width="90px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_CR_Accept_Commission_Payer" runat="server" CssClass="textBox"
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
                                                                    <asp:TextBox ID="txt_CR_Pay_Handle_Commission_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="110" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_CR_Pay_Handle_Commission_amt" runat="server" CssClass="textBox"
                                                                        MaxLength="9" onkeydown="return validate_Number(event);" Style="text-align: right"
                                                                        TabIndex="111" Width="90px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_CR_Pay_Handle_Commission_Payer" runat="server" CssClass="textBox"
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
                                                                    <asp:TextBox ID="txt_CR_Others_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                        TabIndex="113" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_CR_Others_amt" runat="server" CssClass="textBox" MaxLength="9"
                                                                        onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="114"
                                                                        Width="90px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_CR_Others_Payer" runat="server" CssClass="textBox" MaxLength="1"
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
                                                                    <asp:TextBox ID="txt_CR_Their_Commission_Curr" runat="server" CssClass="textBox"
                                                                        MaxLength="3" TabIndex="116" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_CR_Their_Commission_amt" runat="server" CssClass="textBox" MaxLength="9"
                                                                        onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="117"
                                                                        Width="90px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_CR_Their_Commission_Payer" runat="server" CssClass="textBox"
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
                                                                    <asp:TextBox ID="txt_IBD_DR_Code" runat="server" CssClass="textBox" MaxLength="5"
                                                                        TabIndex="119" Width="90px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <%--<asp:TextBox ID="txt_IBD_DR_AC_ShortName" runat="server" CssClass="textBox" 
                                                                        MaxLength="20" TabIndex="119" Width="90px"></asp:TextBox>--%>
                                                                    <span class="elementLabel">I.B.D.</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IBD_DR_Cust_abbr" runat="server" CssClass="textBox" MaxLength="12"
                                                                        TabIndex="120" Width="130px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IBD_DR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="20"
                                                                        TabIndex="121" Width="160px"></asp:TextBox>
                                                                </td>
                                                                <td align="center">
                                                                    <asp:TextBox ID="txt_IBD_DR_Cur_Acc_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                        TabIndex="122" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IBD_DR_Cur_Acc_amt" runat="server" CssClass="textBox" MaxLength="9"
                                                                        onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="123"
                                                                        Width="90px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_IBD_DR_Cur_Acc_payer" runat="server" CssClass="textBox" MaxLength="1"
                                                                        onkeydown="return validate_Commission_PAYER_Code(event);" TabIndex="124" Width="50px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <asp:Panel ID="panal_DRdetails" runat="server" Visible="false">
                                                                    <td align="right">
                                                                        <asp:TextBox ID="txt_DR_Code" runat="server" CssClass="textBox" MaxLength="5" TabIndex="125"
                                                                            Width="90px"></asp:TextBox>
                                                                        <asp:Button ID="btn_DR_Code_help" runat="server" ToolTip="Press for AcCode list."
                                                                            CssClass="btnHelp_enabled" OnClientClick="return OpenCR_Code_help('mouseClick','IMPACC3','Debit');" />
                                                                    </td>
                                                                    <td align="left">
                                                                        <span class="elementLabel">CURRENT ACCOUNT</span>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txt_DR_Cust_abbr" runat="server" CssClass="textBox" MaxLength="12"
                                                                            TabIndex="126" Width="100px"></asp:TextBox>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txt_DR_Cust_Acc" runat="server" CssClass="textBox" MaxLength="20"
                                                                            TabIndex="127" Width="160px"></asp:TextBox>
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:TextBox ID="txt_DR_Cur_Acc_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                            TabIndex="128" Width="40px"></asp:TextBox>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txt_DR_Cur_Acc_amt" runat="server" CssClass="textBox" MaxLength="9"
                                                                            onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="129"
                                                                            Width="90px"></asp:TextBox>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txt_DR_Cur_Acc_payer" runat="server" CssClass="textBox" MaxLength="1"
                                                                            onkeydown="return validate_Commission_PAYER_Code(event);" TabIndex="130" Width="50px"></asp:TextBox>
                                                                    </td>
                                                                </asp:Panel>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                </td>
                                                                <td align="left" colspan="3">
                                                                </td>
                                                                <td align="center">
                                                                    <asp:TextBox ID="txt_DR_Cur_Acc_Curr2" runat="server" CssClass="textBox" MaxLength="3"
                                                                        TabIndex="131" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_DR_Cur_Acc_amt2" runat="server" CssClass="textBox" MaxLength="9"
                                                                        onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="132"
                                                                        Width="90px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_DR_Cur_Acc_payer2" runat="server" CssClass="textBox" MaxLength="1"
                                                                        onkeydown="return validate_Commission_PAYER_Code(event);" TabIndex="133" Width="50px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                </td>
                                                                <td colspan="3">
                                                                </td>
                                                                <td align="center">
                                                                    <asp:TextBox ID="txt_DR_Cur_Acc_Curr3" runat="server" CssClass="textBox" MaxLength="3"
                                                                        TabIndex="134" Width="40px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_DR_Cur_Acc_amt3" runat="server" CssClass="textBox" MaxLength="9"
                                                                        onkeydown="return validate_Number(event);" Style="text-align: right" TabIndex="135"
                                                                        Width="90px"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_DR_Cur_Acc_payer3" runat="server" CssClass="textBox" MaxLength="1"
                                                                        onkeydown="return validate_Commission_PAYER_Code(event);" TabIndex="136" Width="50px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </caption>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="7">
                                                    <asp:Button ID="btnDocAccPrev" runat="server" CssClass="buttonDefault" OnClientClick="return OnDocNextClick(1);"
                                                        TabIndex="159" Text="&lt;&lt; Prev" ToolTip="Back to Document Details" />
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnImpAccNext" runat="server" CssClass="buttonDefault" OnClientClick="return OnDocNextClick(3);"
                                                        TabIndex="137" Text="Next &gt;&gt;" ToolTip="Next To GENERAL OPERATION" />
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
                                                    <asp:CheckBox ID="chk_GO1_Flag" Text="GENERAL OPERATION I" runat="server" CssClass="elementLabel"
                                                        AutoPostBack="True" OnCheckedChanged="chk_GO1_Flag_CheckedChanged" />
                                                </td>
                                            </tr>
                                            <asp:Panel ID="Panel_GO1" runat="server" Visible="False">
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">COMMENT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO1_Comment" runat="server" CssClass="textBox" MaxLength="20"
                                                            TabIndex="80"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" width="20%">
                                                        <span class="elementLabel">Section No : </span>
                                                    </td>
                                                    <td width="20%">
                                                        <asp:TextBox ID="txt_GO1_SectionNo" runat="server" MaxLength="2" CssClass="textBox"
                                                            TabIndex="81" Width="30px"></asp:TextBox>
                                                    </td>
                                                    <td align="right" width="15%">
                                                        <span class="elementLabel">Remarks : </span>
                                                    </td>
                                                    <td width="20%">
                                                        <asp:TextBox ID="txt_GO1_Remarks" runat="server" MaxLength="30" CssClass="textBox"
                                                            Width="260px" TabIndex="82" onkeyup="return CustRem();"></asp:TextBox>
                                                    </td>
                                                    <td align="right" width="15%">
                                                        <span class="elementLabel">MEMO : </span>
                                                    </td>
                                                    <td width="10%">
                                                        <asp:TextBox ID="txt_GO1_Memo" runat="server" MaxLength="20" CssClass="textBox" Width="50px"
                                                            TabIndex="83"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" width="15%">
                                                        <span class="elementLabel">SCHEME No : </span>
                                                    </td>
                                                    <td width="20%">
                                                        <asp:TextBox ID="txt_GO1_Scheme_no" runat="server" MaxLength="20" CssClass="textBox"
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
                                                        <asp:DropDownList ID="txt_GO1_Debit_Code" runat="server" Width="70px" CssClass="dropdownList"
                                                            TabIndex="84" onchange="return TogggleDebitCreditCode('1','1');">
                                                            <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                            <asp:ListItem Value="D" Text="D"></asp:ListItem>
                                                            <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        &nbsp;
                                                        <asp:Button ID="btn_GO1_Debit_AccCode_help" runat="server" ToolTip="Press for AcCode list."
                                                            CssClass="btnHelp_enabled" OnClientClick="return OpenCR_Code_help('mouseClick','IMPACC1','Debit');" />
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">CCY : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO1_Debit_Curr" runat="server" MaxLength="3" CssClass="textBox"
                                                            TabIndex="86" Width="45px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">AMOUNT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO1_Debit_Amt" runat="server" CssClass="textBox" Width="90px"
                                                            TabIndex="87" Style="text-align: right" MaxLength="16" onkeydown="return validate_Number(event);"
                                                            onkeyup="return Toggle_GO_Amt('GO1');"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">CUSTOMER : </span>
                                                    </td>
                                                    <td colspan="4">
                                                        <asp:TextBox ID="txt_GO1_Debit_Cust" runat="server" CssClass="textBox" MaxLength="20"
                                                            TabIndex="88" Width="90px"></asp:TextBox>
                                                        <%--  <asp:TextBox ID="txt_GO1_Debit_Cust_Name" runat="server" CssClass="textBox" MaxLength="40" TabIndex="88"
                                                        Width="150px" Visible="false"></asp:TextBox>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">A/C CODE : </span>
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txt_GO1_Debit_Cust_AcCode" MaxLength="20" runat="server" CssClass="textBox"
                                                            TabIndex="89" Width="90px"></asp:TextBox>
                                                        <%-- <asp:TextBox ID="txt_GO1_Debit_Cust_AcCode_Name" MaxLength="40" runat="server" CssClass="textBox"
                                                        TabIndex="89" Width="150px" Visible="false"></asp:TextBox>--%>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">A/C No : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO1_Debit_Cust_AccNo" runat="server" MaxLength="20" CssClass="textBox"
                                                            TabIndex="90"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" align="right">
                                                        <span class="elementLabel">EXCH RATE : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO1_Debit_Exch_Rate" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                            TabIndex="91" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">EXCH CCY : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO1_Debit_Exch_CCY" runat="server" CssClass="textBox" MaxLength="3"
                                                            TabIndex="92" Width="35px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">FUND : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO1_Debit_FUND" runat="server" MaxLength="1" CssClass="textBox"
                                                            TabIndex="93" Width="30px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">CHECK No. : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO1_Debit_Check_No" runat="server" CssClass="textBox" TabIndex="94"
                                                            onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">AVAILABLE : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO1_Debit_Available" MaxLength="20" runat="server" CssClass="textBox"
                                                            TabIndex="95" Width="90px" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">ADVICE PRINT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO1_Debit_AdPrint" runat="server" CssClass="textBox" TabIndex="96"
                                                            Width="30px" MaxLength="1"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">DETAILS : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO1_Debit_Details" runat="server" MaxLength="30" CssClass="textBox"
                                                            Width="260px" TabIndex="97"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">ENTITY : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO1_Debit_Entity" runat="server" CssClass="textBox" TabIndex="98"
                                                            Width="90px" MaxLength="3" onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">DIVISION : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO1_Debit_Division" runat="server" CssClass="textBox" TabIndex="99"
                                                            Width="30px" onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">INTER-AMOUNT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO1_Debit_Inter_Amount" runat="server" CssClass="textBox" TabIndex="100"
                                                            onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">INTER-RATE : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO1_Debit_Inter_Rate" runat="server" CssClass="textBox" TabIndex="101"
                                                            Width="90px" onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
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
                                                        <asp:DropDownList ID="txt_GO1_Credit_Code" runat="server" Width="70px" CssClass="dropdownList"
                                                            TabIndex="84" onchange="return TogggleDebitCreditCode('1','2');">
                                                            <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                            <asp:ListItem Value="D" Text="D"></asp:ListItem>
                                                            <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        &nbsp;
                                                        <asp:Button ID="btn_GO1_Credit_AccCode_help" runat="server" ToolTip="Press for AcCode list."
                                                            CssClass="btnHelp_enabled" OnClientClick="return OpenCR_Code_help('mouseClick','IMPACC1','Credit');" />
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">CCY : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO1_Credit_Curr" runat="server" CssClass="textBox" TabIndex="103"
                                                            Width="45px" MaxLength="3"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">AMOUNT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO1_Credit_Amt" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                            TabIndex="104" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">CUSTOMER : </span>
                                                    </td>
                                                    <td colspan="4">
                                                        <asp:TextBox ID="txt_GO1_Credit_Cust" runat="server" CssClass="textBox" MaxLength="20"
                                                            TabIndex="105" Width="90px"></asp:TextBox>
                                                        <%-- <asp:TextBox ID="txt_GO1_Credit_Cust_Name" MaxLength="40" runat="server" CssClass="textBox" TabIndex="105"
                                                        Width="150px"></asp:TextBox>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">A/C CODE : </span>
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txt_GO1_Credit_Cust_AcCode" runat="server" CssClass="textBox" TabIndex="106"
                                                            Width="90px"></asp:TextBox>
                                                        <%-- <asp:TextBox ID="txt_GO1_Credit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                        TabIndex="106" Width="150px"></asp:TextBox>--%>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">A/C No : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO1_Credit_Cust_AccNo" MaxLength="20" runat="server" CssClass="textBox"
                                                            TabIndex="107"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" align="right">
                                                        <span class="elementLabel">EXCH RATE : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO1_Credit_Exch_Rate" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                            TabIndex="108" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">EXCH CCY : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO1_Credit_Exch_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                            TabIndex="109" Width="35px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">FUND : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO1_Credit_FUND" MaxLength="1" runat="server" CssClass="textBox"
                                                            TabIndex="110" Width="30px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">CHECK No. : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO1_Credit_Check_No" runat="server" CssClass="textBox" TabIndex="111"
                                                            onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">AVAILABLE : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO1_Credit_Available" runat="server" MaxLength="20" CssClass="textBox"
                                                            TabIndex="112" Width="90px" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">ADVICE PRINT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO1_Credit_AdPrint" runat="server" CssClass="textBox" TabIndex="113"
                                                            Width="30px" MaxLength="1"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">DETAILS : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO1_Credit_Details" runat="server" MaxLength="30" CssClass="textBox"
                                                            Width="260px" TabIndex="114"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">ENTITY : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO1_Credit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                            TabIndex="115" Width="90px" Style="text-align: right" MaxLength="3"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">DIVISION : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO1_Credit_Division" runat="server" CssClass="textBox" TabIndex="116"
                                                            Width="30px" onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">INTER-AMOUNT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO1_Credit_Inter_Amount" runat="server" CssClass="textBox" TabIndex="117"
                                                            onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">INTER-RATE : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO1_Credit_Inter_Rate" runat="server" CssClass="textBox" TabIndex="118"
                                                            Width="90px" onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                            <tr>
                                                <td align="left" colspan="6">
                                                    <asp:Button ID="btnGO1_Prev" runat="server" CssClass="buttonDefault" OnClientClick="return OnDocNextClick(2);"
                                                        TabIndex="119" Text="&lt;&lt; Prev" ToolTip="Back to IMPORT ACCOUNTING" />
                                                    &nbsp;&nbsp;
                                                    <asp:Button ID="btnGO1_Next" runat="server" CssClass="buttonDefault" OnClientClick="return OnDocNextClick(4);"
                                                        TabIndex="119" Text="Next &gt;&gt;" ToolTip="Next to GENRAL OPERATION II" />
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
                                                    <asp:CheckBox ID="chk_GO2_Flag" Text="GENERAL OPERATION II" runat="server" CssClass="elementLabel"
                                                        AutoPostBack="True" OnCheckedChanged="chk_GO2_Flag_CheckedChanged" />
                                                </td>
                                            </tr>
                                            <asp:Panel ID="Panel_GO2" runat="server" Visible="False">
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">COMMENT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO2_Comment" runat="server" CssClass="textBox" MaxLength="20"
                                                            TabIndex="80"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" width="20%">
                                                        <span class="elementLabel">Section No : </span>
                                                    </td>
                                                    <td width="20%">
                                                        <asp:TextBox ID="txt_GO2_SectionNo" runat="server" MaxLength="2" CssClass="textBox"
                                                            TabIndex="81" Width="30px"></asp:TextBox>
                                                    </td>
                                                    <td align="right" width="15%">
                                                        <span class="elementLabel">Remarks : </span>
                                                    </td>
                                                    <td width="20%">
                                                        <asp:TextBox ID="txt_GO2_Remarks" runat="server" MaxLength="30" CssClass="textBox"
                                                            Width="260px" TabIndex="82" onkeyup="return CustRem();"></asp:TextBox>
                                                    </td>
                                                    <td align="right" width="15%">
                                                        <span class="elementLabel">MEMO : </span>
                                                    </td>
                                                    <td width="10%">
                                                        <asp:TextBox ID="txt_GO2_Memo" runat="server" MaxLength="20" CssClass="textBox" Width="50px"
                                                            TabIndex="83"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" width="15%">
                                                        <span class="elementLabel">SCHEME No : </span>
                                                    </td>
                                                    <td width="20%">
                                                        <asp:TextBox ID="txt_GO2_Scheme_no" runat="server" MaxLength="20" CssClass="textBox"
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
                                                        <asp:DropDownList ID="txt_GO2_Debit_Code" runat="server" Width="70px" CssClass="dropdownList"
                                                            TabIndex="84" onchange="return TogggleDebitCreditCode('2','1');">
                                                            <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                            <asp:ListItem Value="D" Text="D"></asp:ListItem>
                                                            <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        &nbsp;
                                                        <asp:Button ID="btn_GO2_Debit_AccCode_help" runat="server" ToolTip="Press for AcCode list."
                                                            CssClass="btnHelp_enabled" OnClientClick="return OpenCR_Code_help('mouseClick','IMPACC2','Debit');" />
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">CCY : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO2_Debit_Curr" runat="server" MaxLength="3" CssClass="textBox"
                                                            TabIndex="86" Width="45px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">AMOUNT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO2_Debit_Amt" runat="server" CssClass="textBox" Width="90px"
                                                            onkeydown="return validate_Number(event);" TabIndex="87" Style="text-align: right"
                                                            MaxLength="16" onkeyup="return Toggle_GO_Amt('GO2');"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">CUSTOMER : </span>
                                                    </td>
                                                    <td colspan="4">
                                                        <asp:TextBox ID="txt_GO2_Debit_Cust" runat="server" CssClass="textBox" MaxLength="20"
                                                            TabIndex="88" Width="90px"></asp:TextBox>
                                                        <%--  <asp:TextBox ID="txt_GO2_Debit_Cust_Name" runat="server" CssClass="textBox" MaxLength="40" TabIndex="88"
                                                        Width="150px" Visible="false"></asp:TextBox>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">A/C CODE : </span>
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txt_GO2_Debit_Cust_AcCode" MaxLength="20" runat="server" CssClass="textBox"
                                                            TabIndex="89" Width="90px"></asp:TextBox>
                                                        <%--<asp:TextBox ID="txt_GO2_Debit_Cust_AcCode_Name" MaxLength="40" runat="server" CssClass="textBox"
                                                        TabIndex="89" Width="150px" Visible="false"></asp:TextBox>--%>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">A/C No : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO2_Debit_Cust_AccNo" runat="server" MaxLength="20" CssClass="textBox"
                                                            TabIndex="90"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" align="right">
                                                        <span class="elementLabel">EXCH RATE : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO2_Debit_Exch_Rate" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                            TabIndex="91" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">EXCH CCY : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO2_Debit_Exch_CCY" runat="server" CssClass="textBox" MaxLength="3"
                                                            TabIndex="92" Width="35px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">FUND : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO2_Debit_FUND" runat="server" MaxLength="1" CssClass="textBox"
                                                            TabIndex="93" Width="30px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">CHECK No. : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO2_Debit_Check_No" runat="server" CssClass="textBox" TabIndex="94"
                                                            onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">AVAILABLE : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO2_Debit_Available" MaxLength="20" runat="server" CssClass="textBox"
                                                            TabIndex="95" Width="90px" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">ADVICE PRINT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO2_Debit_AdPrint" runat="server" CssClass="textBox" TabIndex="96"
                                                            Width="30px" MaxLength="1"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">DETAILS : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO2_Debit_Details" runat="server" MaxLength="30" CssClass="textBox"
                                                            Width="260px" TabIndex="97"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">ENTITY : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO2_Debit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                            TabIndex="98" Width="90px" Style="text-align: right" MaxLength="3"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">DIVISION : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO2_Debit_Division" runat="server" CssClass="textBox" TabIndex="99"
                                                            Width="30px" onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">INTER-AMOUNT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO2_Debit_Inter_Amount" runat="server" CssClass="textBox" TabIndex="100"
                                                            onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">INTER-RATE : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO2_Debit_Inter_Rate" runat="server" CssClass="textBox" TabIndex="101"
                                                            Width="90px" onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
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
                                                        <asp:DropDownList ID="txt_GO2_Credit_Code" runat="server" Width="70px" CssClass="dropdownList"
                                                            TabIndex="84" onchange="return TogggleDebitCreditCode('2','2');">
                                                            <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                            <asp:ListItem Value="D" Text="D"></asp:ListItem>
                                                            <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        &nbsp;
                                                        <asp:Button ID="btn_GO2_Credit_Code_help" runat="server" ToolTip="Press for AcCode list."
                                                            CssClass="btnHelp_enabled" OnClientClick="return OpenCR_Code_help('mouseClick','IMPACC2','Credit');" />
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">CCY : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO2_Credit_Curr" runat="server" CssClass="textBox" TabIndex="103"
                                                            Width="45px" MaxLength="3"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">AMOUNT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO2_Credit_Amt" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                            TabIndex="104" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">CUSTOMER : </span>
                                                    </td>
                                                    <td colspan="4">
                                                        <asp:TextBox ID="txt_GO2_Credit_Cust" runat="server" CssClass="textBox" MaxLength="20"
                                                            TabIndex="105" Width="90px"></asp:TextBox>
                                                        <%--<asp:TextBox ID="txt_GO2_Credit_Cust_Name" MaxLength="40" runat="server" CssClass="textBox" TabIndex="105"
                                                        Width="150px"></asp:TextBox>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">A/C CODE : </span>
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txt_GO2_Credit_Cust_AcCode" runat="server" CssClass="textBox" TabIndex="106"
                                                            Width="90px"></asp:TextBox>
                                                        <%-- <asp:TextBox ID="txt_GO2_Credit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                        TabIndex="106" Width="150px"></asp:TextBox>--%>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">A/C No : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO2_Credit_Cust_AccNo" MaxLength="20" runat="server" CssClass="textBox"
                                                            TabIndex="107"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" align="right">
                                                        <span class="elementLabel">EXCH RATE : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO2_Credit_Exch_Rate" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                            TabIndex="108" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">EXCH CCY : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO2_Credit_Exch_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                            TabIndex="109" Width="45px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">FUND : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO2_Credit_FUND" MaxLength="1" runat="server" CssClass="textBox"
                                                            TabIndex="110" Width="30px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">CHECK No. : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO2_Credit_Check_No" runat="server" CssClass="textBox" TabIndex="111"
                                                            onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">AVAILABLE : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO2_Credit_Available" runat="server" MaxLength="20" CssClass="textBox"
                                                            TabIndex="112" Width="90px" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">ADVICE PRINT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO2_Credit_AdPrint" runat="server" CssClass="textBox" TabIndex="113"
                                                            Width="30px" MaxLength="1"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">DETAILS : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO2_Credit_Details" runat="server" MaxLength="30" CssClass="textBox"
                                                            Width="260px" TabIndex="114"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">ENTITY : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO2_Credit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                            TabIndex="115" Width="90px" Style="text-align: right" MaxLength="3"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">DIVISION : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO2_Credit_Division" runat="server" CssClass="textBox" TabIndex="116"
                                                            Width="30px" onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">INTER-AMOUNT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO2_Credit_Inter_Amount" runat="server" CssClass="textBox" TabIndex="117"
                                                            onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">INTER-RATE : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GO2_Credit_Inter_Rate" runat="server" CssClass="textBox" TabIndex="118"
                                                            Width="90px" onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                            <tr>
                                                <td>
                                                    <td align="left" colspan="6">
                                                        <asp:Button ID="btnGO2_Prev" runat="server" CssClass="buttonDefault" OnClientClick="return OnDocNextClick(3);"
                                                            TabIndex="120" Text="&lt;&lt; Prev" ToolTip="Back to IMPORT ACCOUNTING" />
                                                        &nbsp;&nbsp;
                                                        <asp:Button ID="btnGO2_Next" runat="server" CssClass="buttonDefault" OnClientClick="return OnDocNextClick(5);"
                                                            TabIndex="121" Text="Next &gt;&gt;" ToolTip="Next to GENRAL OPERATION II" />
                                                    </td>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel ID="tbDocumentGOBranch" runat="server" HeaderText="GENERAL OPERATION BR"
                                    Font-Bold="true" ForeColor="White" Visible="false">
                                    <ContentTemplate>
                                        <table width="80%">
                                            <tr>
                                                <td align="left">
                                                    <asp:CheckBox ID="cb_GOBranch_Bill_Handling_Flag" Text="GENERAL OPERATION BR" runat="server"
                                                        CssClass="elementLabel" AutoPostBack="True" OnCheckedChanged="cb_GOBranch_Bill_Handling_Flag_CheckedChanged" />
                                                </td>
                                            </tr>
                                            <asp:Panel ID="PanelGOBR_Bill_Handling" runat="server" Visible="False">
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">REFERENCE NO:</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txt_GOBR_Ref_No" Width="150px" runat="server" TabIndex="80"
                                                            CssClass="textBox" MaxLength="14"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">COMMENT : </span>
                                                    </td>
                                                    <td align="left" colspan="3">
                                                        <asp:TextBox ID="txt_GOBR_Comment" runat="server" CssClass="textBox" MaxLength="20"
                                                            TabIndex="80"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" width="20%">
                                                        <span class="elementLabel">Section No : </span>
                                                    </td>
                                                    <td width="20%">
                                                        <asp:TextBox ID="txt_GOBR_SectionNo" runat="server" MaxLength="2" CssClass="textBox"
                                                            TabIndex="81" Width="30px"></asp:TextBox>
                                                    </td>
                                                    <td align="right" width="15%">
                                                        <span class="elementLabel">Remarks : </span>
                                                    </td>
                                                    <td width="20%">
                                                        <asp:TextBox ID="txt_GOBR_Remarks" runat="server" MaxLength="30" CssClass="textBox"
                                                            Width="260px" TabIndex="82" onkeyup="return CustRem();"></asp:TextBox>
                                                    </td>
                                                    <td align="right" width="15%">
                                                        <span class="elementLabel">MEMO : </span>
                                                    </td>
                                                    <td width="10%">
                                                        <asp:TextBox ID="txt_GOBR_Memo" runat="server" MaxLength="20" CssClass="textBox"
                                                            Width="50px" TabIndex="83"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" width="15%">
                                                        <span class="elementLabel">SCHEME No : </span>
                                                    </td>
                                                    <td width="20%">
                                                        <asp:TextBox ID="txt_GOBR_Scheme_no" runat="server" MaxLength="20" CssClass="textBox"
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
                                                        <asp:DropDownList ID="txt_GOBR_Debit_Code" runat="server" Width="70px" CssClass="dropdownList"
                                                            TabIndex="84" onchange="return TogggleDebitCreditCode('BR','1');">
                                                            <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                            <asp:ListItem Value="D" Text="D"></asp:ListItem>
                                                            <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        &nbsp;
                                                        <asp:Button ID="btn_GOBR_Debit_AccCode_help" runat="server" ToolTip="Press for AcCode list."
                                                            CssClass="btnHelp_enabled" OnClientClick="return OpenCR_Code_help('mouseClick','GO4','Debit');" />
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">CCY : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GOBR_Debit_Curr" runat="server" MaxLength="3" CssClass="textBox"
                                                            TabIndex="86" Width="45px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">AMOUNT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GOBR_Debit_Amt" runat="server" CssClass="textBox" Width="90px"
                                                            onkeydown="return validate_Number(event);" TabIndex="87" Style="text-align: right"
                                                            MaxLength="16" onkeyup="return Toggle_GO_Amt('BR');"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">CUSTOMER : </span>
                                                    </td>
                                                    <td colspan="4">
                                                        <asp:TextBox ID="txt_GOBR_Debit_Cust" runat="server" CssClass="textBox" MaxLength="20"
                                                            TabIndex="88" Width="90px"></asp:TextBox>
                                                        <%--  <asp:TextBox ID="txt_GOBR_Debit_Cust_Name" runat="server" CssClass="textBox" MaxLength="40" TabIndex="88"
                                                        Width="150px" Visible="false"></asp:TextBox>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">A/C CODE : </span>
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txt_GOBR_Debit_Cust_AcCode" MaxLength="20" runat="server" CssClass="textBox"
                                                            TabIndex="89" Width="90px"></asp:TextBox>
                                                        <%-- <asp:TextBox ID="txt_GOBR_Debit_Cust_AcCode_Name" MaxLength="40" runat="server" CssClass="textBox"
                                                        TabIndex="89" Width="150px" Visible="false"></asp:TextBox>--%>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">A/C No : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GOBR_Debit_Cust_AccNo" runat="server" MaxLength="20" CssClass="textBox"
                                                            TabIndex="90"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" align="right">
                                                        <span class="elementLabel">EXCH RATE : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GOBR_Debit_Exch_Rate" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                            TabIndex="91" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">EXCH CCY : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GOBR_Debit_Exch_CCY" runat="server" CssClass="textBox" MaxLength="3"
                                                            TabIndex="92" Width="45px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">FUND : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GOBR_Debit_FUND" runat="server" MaxLength="1" CssClass="textBox"
                                                            TabIndex="93" Width="30px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">CHECK No. : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GOBR_Debit_Check_No" runat="server" CssClass="textBox" TabIndex="94"
                                                            onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">AVAILABLE : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GOBR_Debit_Available" MaxLength="20" runat="server" CssClass="textBox"
                                                            TabIndex="95" Width="90px" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">ADVICE PRINT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GOBR_Debit_AdPrint" runat="server" CssClass="textBox" TabIndex="96"
                                                            Width="30px" MaxLength="1"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">DETAILS : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GOBR_Debit_Details" runat="server" MaxLength="30" CssClass="textBox"
                                                            Width="260px" TabIndex="97"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">ENTITY : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GOBR_Debit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                            TabIndex="98" Width="90px" Style="text-align: right" MaxLength="3"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">DIVISION : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GOBR_Debit_Division" runat="server" CssClass="textBox" TabIndex="99"
                                                            Width="30px" onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">INTER-AMOUNT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GOBR_Debit_Inter_Amount" runat="server" CssClass="textBox" TabIndex="100"
                                                            onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">INTER-RATE : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GOBR_Debit_Inter_Rate" runat="server" CssClass="textBox" TabIndex="101"
                                                            Width="90px" onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
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
                                                        <asp:DropDownList ID="txt_GOBR_Credit_Code" runat="server" Width="70px" CssClass="dropdownList"
                                                            TabIndex="84" onchange="return TogggleDebitCreditCode('BR','2');">
                                                            <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                            <asp:ListItem Value="D" Text="D"></asp:ListItem>
                                                            <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        &nbsp;
                                                        <asp:Button ID="btn_GOBR_Credit_AccCode_help" runat="server" ToolTip="Press for AcCode list."
                                                            CssClass="btnHelp_enabled" OnClientClick="return OpenCR_Code_help('mouseClick','GO4','Credit');" />
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">CCY : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GOBR_Credit_Curr" runat="server" CssClass="textBox" TabIndex="103"
                                                            Width="45px" MaxLength="3"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">AMOUNT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GOBR_Credit_Amt" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                            TabIndex="104" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">CUSTOMER : </span>
                                                    </td>
                                                    <td colspan="4">
                                                        <asp:TextBox ID="txt_GOBR_Credit_Cust" runat="server" CssClass="textBox" MaxLength="20"
                                                            TabIndex="105" Width="90px"></asp:TextBox>
                                                        <%--  <asp:TextBox ID="txt_GOBR_Credit_Cust_Name" MaxLength="40" runat="server" CssClass="textBox" TabIndex="105"
                                                        Width="150px"></asp:TextBox>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">A/C CODE : </span>
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox ID="txt_GOBR_Credit_Cust_AcCode" runat="server" CssClass="textBox" TabIndex="106"
                                                            Width="90px"></asp:TextBox>
                                                        <%-- <asp:TextBox ID="txt_GOBR_Credit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                        TabIndex="106" Width="150px"></asp:TextBox>--%>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">A/C No : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GOBR_Credit_Cust_AccNo" MaxLength="20" runat="server" CssClass="textBox"
                                                            TabIndex="107"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" align="right">
                                                        <span class="elementLabel">EXCH RATE : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GOBR_Credit_Exch_Rate" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                            TabIndex="108" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">EXCH CCY : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GOBR_Credit_Exch_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                            TabIndex="109" Width="35px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">FUND : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GOBR_Credit_FUND" MaxLength="1" runat="server" CssClass="textBox"
                                                            TabIndex="110" Width="30px"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">CHECK No. : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GOBR_Credit_Check_No" runat="server" CssClass="textBox" TabIndex="111"
                                                            onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">AVAILABLE : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GOBR_Credit_Available" runat="server" MaxLength="20" CssClass="textBox"
                                                            TabIndex="112" Width="90px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">ADVICE PRINT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GOBR_Credit_AdPrint" runat="server" CssClass="textBox" TabIndex="113"
                                                            Width="30px" MaxLength="1"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">DETAILS : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GOBR_Credit_Details" runat="server" MaxLength="30" CssClass="textBox"
                                                            Width="260px" TabIndex="114"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">ENTITY : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GOBR_Credit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                            TabIndex="115" Width="90px" Style="text-align: right" MaxLength="3"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">DIVISION : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GOBR_Credit_Division" runat="server" CssClass="textBox" TabIndex="116"
                                                            Width="30px" onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">INTER-AMOUNT : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GOBR_Credit_Inter_Amount" runat="server" CssClass="textBox"
                                                            TabIndex="117" onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                    <td align="right">
                                                        <span class="elementLabel">INTER-RATE : </span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_GOBR_Credit_Inter_Rate" runat="server" CssClass="textBox" TabIndex="118"
                                                            Width="90px" onkeydown="return validate_Number(event);" Style="text-align: right"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </asp:Panel>
                                            <tr>
                                                <td align="left">
                                                    <asp:Button ID="btnGOBR_Prev" runat="server" CssClass="buttonDefault" OnClientClick="return OnDocNextClick(3);"
                                                        TabIndex="119" Text="&lt;&lt; Prev" ToolTip="Back to IMPORT ACCOUNTING" />
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnGOBR_Next" runat="server" CssClass="buttonDefault" OnClientClick="return OnDocNextClick(6);"
                                                        TabIndex="119" Text="Next &gt;&gt;" ToolTip="Next To GENRAL OPERATION I" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel ID="tblR42format" runat="server" HeaderText="R42 FORMAT FOR IBD"
                                    Font-Bold="true" ForeColor="White">
                                    <ContentTemplate>
                                        <table width="50%">
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">[2020] Transaction Ref No : </span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_R42_tansactionRefNO" runat="server" CssClass="textBox" MaxLength="20"
                                                        TabIndex="301" Width="110px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">[2006] Related Reference : </span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_R42_RelatedRef" runat="server" CssClass="textBox" MaxLength="20"
                                                        TabIndex="302" Width="200px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">[4488] Value Date/Currency/Amount : </span>
                                                </td>
                                                <td>
                                                    <span class="elementLabel">Date</span><asp:TextBox ID="txt_R42_ValueDate_4488" runat="server"
                                                        CssClass="textBox" MaxLength="10" TabIndex="303" Width="80px"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="ME_R42Value_Date" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txt_R42_ValueDate_4488" ErrorTooltipEnabled="True"
                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                        CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <span class="elementLabel">Currency</span><asp:TextBox ID="txt_R42_Curr_4488" runat="server"
                                                        CssClass="textBox" MaxLength="3" TabIndex="304" Width="35px"></asp:TextBox>
                                                    <span class="elementLabel">Amount</span><asp:TextBox ID="txt_R42_Amt_4488" runat="server"
                                                        CssClass="textBox" MaxLength="20" TabIndex="305" Width="100px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">[5517] Ordering Institution IFSC : </span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_R42_Orderingins_IFSC_5517" runat="server" CssClass="textBox"
                                                        MaxLength="11" TabIndex="306" Width="100px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">[6521] Beneficiary Institution IFSC : </span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_R42_Benificiary_IFSC_6521" runat="server" CssClass="textBox"
                                                        MaxLength="11" TabIndex="307" Width="100px"></asp:TextBox>
                                                    <span class="mandatoryField">To update Receiver information modify this texbox.</span>
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
                                                    <asp:TextBox ID="txt_R42_CodeWord_7495" runat="server" CssClass="textBox" MaxLength="20"
                                                        TabIndex="308" Width="100px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">Additional Information </span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_R42_AddInfo_7495" runat="server" CssClass="textBox" MaxLength="40"
                                                        TabIndex="309" Width="400px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">More Info </span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_R42_MoreInfo_7495" runat="server" CssClass="textBox" MaxLength="35"
                                                        TabIndex="310" Width="500px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_R42_MoreInfo2_7495" runat="server" CssClass="textBox" TabIndex="238"
                                                        Width="500px" MaxLength="35"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_R42_MoreInfo3_7495" runat="server" CssClass="textBox" TabIndex="238"
                                                        Width="500px" MaxLength="35"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_R42_MoreInfo4_7495" runat="server" CssClass="textBox" TabIndex="238"
                                                        Width="500px" MaxLength="35"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt_R42_MoreInfo5_7495" runat="server" CssClass="textBox" TabIndex="238"
                                                        Width="500px" MaxLength="35"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                    <asp:Button ID="btn_Generate_Swift" runat="server" Text="View R42 Message" CssClass="buttonDefault"
                                                        Width="150px" ToolTip="View R42 Message" TabIndex="256" OnClientClick="ViewSwiftMessage();" />
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
                                Width="150px" ToolTip="Submit to Checker" TabIndex="400" OnClientClick="return SubmitCheck();"
                                OnClick="btnSubmit_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" align="center">
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
