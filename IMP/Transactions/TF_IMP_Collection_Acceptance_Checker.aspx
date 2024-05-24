<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_Collection_Acceptance_Checker.aspx.cs" Inherits="IMP_Transactions_TF_IMP_Collection_Acceptance_Checker" %>

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
    <script src="../Scripts/TF_IMP_Collection_Acceptance_Checker.js" type="text/javascript"></script>
    
</head>
<body>
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
                        <asp:Label ID="label1" runat="server" CssClass="mandatoryField"></asp:Label>
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back" OnClick="btnBack_Click" />
                        </td>
                    </tr>
                    <%--<tr>
                        <td align="left" style="width: 100%" valign="middle" colspan="2">
                        <asp:Label ID="label1" runat="server" CssClass="mandatoryField"></asp:Label>
                            <hr />
                        </td>
                    </tr>--%>
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
                            <input type="hidden" id="hdnBranchCode" runat="server" />
                            <input type="hidden" id="hdnDocNo" runat="server" />
                            <input type="hidden" id="hdnRejectReason" runat="server" />
                            <input type="hidden" id="hdnBranchName" runat="server" />
                            <input type="hidden" id="hdnDocType" runat="server" />
                            <input type="hidden" id="hdnMode" runat="server" />
                            <input type="hidden" id="hdnNegoRemiBankType" runat="server" />
                            <input type="hidden" id="hdnUserName" runat="server" />
                             <%-------Added by Bhupen for LEI on 14102022----------------%>
                            <input type="hidden" id="hdnleino" runat="server" />
                            <input type="hidden" id="hdnleiexpiry" runat="server" />
                            <input type="hidden" id="hdnDraweeleino" runat="server" />
                            <input type="hidden" id="hdnDraweeleiexpiry" runat="server" />
                            <input type="hidden" id="hdnLeiFlag" runat="server" />
                            <input type="hidden" id="hdnleiexpiry1" runat="server" />
                            <input type="hidden" id="hdnDraweeleiexpiry1" runat="server" />
                            <input type="hidden" id="hdnDrawer" runat="server" />
                            <input type="hidden" id="hdnDrawerno" runat="server" />
                            <input type="hidden" id="Hidden1" runat="server" />
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
                        <td width="40%" align="left">
                            <span class="elementLabel">Acceptance Date :</span>
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
                            <asp:Label ID="lblBillAmt" runat="server" CssClass="elementLabel"></asp:Label>&nbsp;&nbsp;
                          <%---------Added by bhupen for lei on 14112022-----------%>
                            <span class="elementLabel">Exch Rate : </span>
                            <asp:Label ID="lbl_Exch_rate" runat="server" CssClass="elementLabel"></asp:Label>
                          <%--------------------------------------------END--------------------------------------%>
                        </td>
                        <td align="right" width="10%">
                        </td>
                        <td align="left" width="20%">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="5">
                            <ajaxToolkit:TabContainer ID="TabContainerMain" runat="server" ActiveTabIndex="1"
                                CssClass="ajax__tab_xp-theme">
                                <ajaxToolkit:TabPanel ID="tbDocumentDetails" runat="server" HeaderText="DOCUMENT DETAILS"
                                    Font-Bold="true" ForeColor="White">
                                    <ContentTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">CUSTOMER :</span>
                                                </td>
                                                <td align="left" colspan="2" width="90%">
                                                    <asp:TextBox ID="txtCustomer_ID" runat="server" AutoPostBack="True" CssClass="textBox"
                                                        TabIndex="3" MaxLength="20" Width="100px"></asp:TextBox>
                                                    <asp:Label ID="lblCustomerDesc" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td align="right" >
                                                    <span class="elementLabel">ACCEPTED DATE :</span>
                                                </td>
                                                <td align="left" >
                                                    <asp:TextBox ID="txt_Accepted_Date" runat="server" CssClass="textBox" MaxLength="10"
                                                        TabIndex="24" ValidationGroup="dtVal" Width="70px"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="ME_Received_date" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txt_Accepted_Date" ErrorTooltipEnabled="True"
                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                        CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btncal_Accepted_Date" runat="server" CssClass="btncalendar_enabled"
                                                        TabIndex="2" />
                                                    <ajaxToolkit:CalendarExtender ID="CE_Received_date" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txt_Accepted_Date" PopupButtonID="btncal_Accepted_Date" Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <ajaxToolkit:MaskedEditValidator ID="MV_Received_date" runat="server" ControlExtender="ME_Received_date"
                                                        ValidationGroup="dtVal" ControlToValidate="txt_Accepted_Date" EmptyValueMessage="Enter Date Value"
                                                        InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                        Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">MATURITY DATE :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_Maturity_Date" runat="server" CssClass="textBox" MaxLength="10" TabIndex="24"
                                                        ValidationGroup="dtVal" Width="70px"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txt_Maturity_Date" ErrorTooltipEnabled="True"
                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                        CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btncal_Maturity_Date" runat="server" CssClass="btncalendar_enabled"
                                                        TabIndex="2" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txt_Maturity_Date" PopupButtonID="btncal_Maturity_Date" Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender4"
                                                        ValidationGroup="dtVal" ControlToValidate="txt_Maturity_Date" EmptyValueMessage="Enter Date Value"
                                                        InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                        Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">ATTN :</span>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <asp:TextBox ID="txtAttn" Width="350px" runat="server" CssClass="textBox" MaxLength="70"
                                                        TabIndex="41" ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">EXPECTED INTEREST :</span>
                                                </td>
                                                <td align="left" width="10%">
                                                    <asp:TextBox ID="txt_Expected_InterestAmt" runat="server" CssClass="textBox" MaxLength="16"
                                                        TabIndex="30" Width="100px" Style="text-align: right"></asp:TextBox>
                                                </td>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">RECEIVED INTEREST :</span>
                                                </td>
                                                <td align="left" width="70%">
                                                    <asp:TextBox ID="txt_Received_InterestAmt" runat="server" CssClass="textBox" MaxLength="16"
                                                        TabIndex="32" Width="100px" Style="text-align: right"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                </td>
                                                <td colspan="3">
                                                    <asp:Button ID="btnDocNext" runat="server" Text="Next >>" CssClass="buttonDefault"
                                                        TabIndex="50" ToolTip="Go to General Operation" OnClick="btnDocNext_Click" />
                                                    <asp:Button ID="btn_Verify" runat="server" Text="LEIVerify" CssClass="buttonDefault"
                                                        ToolTip="Click here to verify LEI Details" Visible="false" TabIndex="107" 
                                                        OnClick="btn_Verify_Click" />
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
                                                        TabIndex="201" OnCheckedChanged="chkGO_Swift_SFMS_OnCheckedChanged" AutoPostBack="true"/>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                        <div id="DivGO_Swift_SFMS" style="width: 50%; float: left; height: 100%">
                                            <table width="98%">
                                                <asp:Panel ID="panalGO_Swift_SFMS" runat="server" visible="false" >
                                                    <tr>
                                                        <td colspan="6">
                                                            <span class="elementLabel"><b>GENERAL OPERATION FOR SWIFT / SFMS CHRG</b></span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
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
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Comment" runat="server" CssClass="textBox" TabIndex="204"
                                                                ></asp:TextBox>
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
                                                        
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Remarks" runat="server" CssClass="textBox" Width="300px" TabIndex="206" MaxLength="30" onkeyup="return Toggel_GO_SWIFT_Remark();"></asp:TextBox>
                                                        </td>
                                                        <td align="right" >
                                                            <span class="elementLabel">MEMO : </span>
                                                        </td>
                                                        <td >
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Memo" runat="server" CssClass="textBox" Width="50px"
                                                                TabIndex="207" ></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
	                                                    <td align="right" >
		                                                    <span class="elementLabel">SCHEME No : </span>
	                                                    </td>
	                                                    <td align="left">
		                                                    <asp:TextBox ID="txt_GO_Swift_SFMS_Scheme_no" runat="server" CssClass="textBox" TabIndex="207"></asp:TextBox>
	                                                    </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="20%">
                                                            <span class="elementLabel">DEBIT / CREDIT : </span>
                                                        </td>
                                                        <td  align="left" width="20%">
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Debit_Code" runat="server" CssClass="textBox"
                                                                TabIndex="208" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right"  width="15%">
                                                            <span class="elementLabel">CCY : </span>
                                                        </td>
                                                        <td width="25%" align="left">
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Debit_Curr" runat="server" CssClass="textBox"
                                                                TabIndex="209" Width="25px"></asp:TextBox>
                                                        </td>
                                                        <td align="right" width="10%">
                                                            <span class="elementLabel">AMOUNT : </span>
                                                        </td>
                                                        <td align="left" width="10%">
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Debit_Amt" runat="server" CssClass="textBox" Width="90px"
                                                                onkeydown="return validate_Number(event);" onkeyup="return DebitCredit_Amt('Swift_SFMS');"
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
                                                                TabIndex="214" Width="25px" MaxLength="3" ></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">EXCH RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Debit_Exch_Rate" runat="server" CssClass="textBox"
                                                                 TabIndex="215" Width="90px" Style="text-align: right"
                                                                MaxLength="16"></asp:TextBox>
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
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Debit_AdPrint" runat="server" CssClass="textBox"
                                                                TabIndex="216" Width="20px" MaxLength="1"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">DETAILS : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Debit_Details" runat="server" CssClass="textBox"
                                                                TabIndex="217"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">ENTITY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Debit_Entity" runat="server" CssClass="textBox"
                                                                 TabIndex="217" Width="90px" Style="text-align: right"
                                                                MaxLength="3"></asp:TextBox>
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
                                                        <td>
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
                                                                Enabled="false"  TabIndex="220" Width="90px"
                                                                Style="text-align: right" MaxLength="16"></asp:TextBox>
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
                                                                MaxLength="3" TabIndex="224" Width="25px" ></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">EXCH RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Credit_Exch_Rate" runat="server" CssClass="textBox"
                                                                 TabIndex="225" Width="90px" Style="text-align: right"
                                                                MaxLength="16"></asp:TextBox>
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
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Credit_AdPrint" runat="server" CssClass="textBox"
                                                                TabIndex="226" Width="20px" MaxLength="1" ></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">DETAILS : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Credit_Details" runat="server" CssClass="textBox"
                                                                TabIndex="227"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">ENTITY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO_Swift_SFMS_Credit_Entity" runat="server" CssClass="textBox"
                                                                 TabIndex="228" Width="90px" Style="text-align: right"
                                                                MaxLength="3"></asp:TextBox>
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
                                            <table width="100%">
                                                <tr>
                                                    <td width="10%">
                                                        
                                                    </td>
                                                    <td width="90%">
                                                        <asp:Button ID="btnGO_Prev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                            ToolTip="Back to Document Details" TabIndex="256" OnClick="btnGO_Prev_Click" />
                                                         <asp:Button ID="btnGO_Next" runat="server" Text="Next >>" CssClass="buttonDefault"
                                                            ToolTip="Back to Document Details" TabIndex="256" OnClick="btnGO_Next_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                               <ajaxToolkit:TabPanel ID="tbSwiftDetails" runat="server" HeaderText="Swift Details"
                                    Font-Bold="true" ForeColor="White">
                                    <ContentTemplate>
                                        <table width="100%" style="padding-top:20px;padding-left:20px;">
                                            <tr>
                                                <td width="50%">
                                                    <span class="elementLabel">[79] Narrative </span>
                                                    <span class="mandatoryField">[MT499/MT999]</span>
                                                </td>
                                                <td width="50%">
                                                    <span class="elementLabel">Sender to Receiver Information </span>
                                                    <span class="mandatoryField">[MT412]</span>
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
                                                                    <asp:TextBox Enabled="false" ID="Narrative4991" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">2.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative4992" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">3.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative4993" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">4.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative4994" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">5.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative4995" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">6.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative4996" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>                                                            
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">7.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative4997" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">8.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative4998" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">9.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative4999" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">10.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative49910" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">11.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative49911" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">12.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative49912" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">13.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative49913" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">14.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative49914" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">15.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative49915" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">16.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative49916" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">17.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative49917" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">18.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative49918" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">19.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative49919" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">20.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative49920" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">21.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative49921" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">22.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative49922" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">23.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative49923" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">24.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative49924" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">25.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative49925" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">26.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative49926" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">27.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative49927" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">28.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative49928" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">29.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative49929" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">30.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative49930" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">31.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative49931" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">32.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative49932" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">33.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative49933" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">34.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative49934" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">35.</span>
                                                                    <asp:TextBox Enabled="false" ID="Narrative49935" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>                                                           
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                                <td width="50%" valign="top">
                                                    <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="panel_AddNarrative"
                                                                CollapsedSize="0" ExpandedSize="200"
                                                                ScrollContents="True" Enabled="True" />
                                                    <asp:Panel ID="panel_AddNarrative" runat="server">
                                                        <table cellspacing="0" width="100%">
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">1.</span>
                                                                    <asp:TextBox Enabled="false" ID="txt_Narrative_1" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">2.</span>
                                                                    <asp:TextBox Enabled="false" ID="txt_Narrative_2" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">3.</span>
                                                                    <asp:TextBox Enabled="false" ID="txt_Narrative_3" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">4.</span>
                                                                    <asp:TextBox Enabled="false" ID="txt_Narrative_4" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span class="elementLabel">5.</span>
                                                                    <asp:TextBox Enabled="false" ID="txt_Narrative_5" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <span class="elementLabel">6.</span>
                                                                    <asp:TextBox Enabled="false" ID="txt_Narrative_6" Width="90%" runat="server" CssClass="textBox" MaxLength="50" TabIndex="46"></asp:TextBox>
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
                                                    <asp:CheckBox ID='rdb_swift_None' Text="None" CssClass="elementLabel" runat="server"
                                                        onchange="return Toggel_Swift_Type('None');" Enabled="false"/>
                                                    <asp:CheckBox ID='rdb_swift_412' Text="MT 412" CssClass="elementLabel" runat="server"
                                                        onchange="return Toggel_Swift_Type('MT412');" Enabled="false"/>
                                                    <asp:CheckBox ID='rdb_swift_499' Text="MT 499" CssClass="elementLabel" runat="server"
                                                        onchange="return Toggel_Swift_Type('MT499');" Enabled="false" />
                                                    <asp:CheckBox ID='rdb_swift_999' Text="MT999" CssClass="elementLabel" runat="server"
                                                        onchange="return Toggel_Swift_Type('MT999');" Enabled="false"/>
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
                                                        ToolTip="Back to Document Details" TabIndex="256" OnClick="btnSwift_Prev_Click"
                                                        OnClientClick="return SaveUpdateData();" />&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btn_Generate_Swift" runat="server" Text="View SWIFT Message" CssClass="buttonDefault"
                                                        Width="150px" ToolTip="View SWIFT Message" TabIndex="256" OnClientClick="ViewSwiftMessage();" />
                                                    <asp:Button ID="btn_Generate_SFMS" runat="server" Text="View SFMS Message" CssClass="buttonDefault"
                                                        Width="150px" ToolTip="View SFMS Message" TabIndex="256" OnClientClick="ViewSFMSMessage();" Visible="false"/>
                                                    <asp:Button ID="btnSave" Style="visibility: hidden" runat="server" Text="Save" CssClass="buttonDefault"
                                                    ToolTip="Save" OnClick="btnSave_Click" TabIndex="107" />&nbsp;&nbsp;&nbsp;
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
