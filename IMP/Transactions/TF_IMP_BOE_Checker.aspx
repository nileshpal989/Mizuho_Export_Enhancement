<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_BOE_Checker.aspx.cs"
    Inherits="IMP_Transactions_TF_IMP_BOE_Checker" %>

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
    <%--<link href="../../Style/Style.css" rel="stylesheet" type="text/css" media="screen" />--%>
    <link href="../../Style/Style_V2.css" rel="Stylesheet" type="text/css" media="screen" />
    <link href="../../Style/TAB.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../../Scripts/jquery-1.8.3.min.js" type="text/javascript""></script>
    <script src="../../Help_Plugins/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <link href="../../Help_Plugins/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../../Help_Plugins/MyJquery1.js" type="text/javascript"></script>
    <script src="../Scripts/TF_IMP_BOE_Checker.js" type="text/javascript"></script>
    <style type="text/css">
        .riskcustmercss
        {
            visibility: hidden;
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
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom">
                            <span class="pageLabel"><strong>Imports Bill Lodgment - Checker</strong></span>
                        </td>
                          <td align="right" style="width: 50%" valign="bottom">
                          <%--<asp:Label ID="label1" runat="server" CssClass="mandatoryField"></asp:Label>--%>
                          <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                             OnClick="btnBack_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 100%" valign="bottom" colspan="2">
                        <asp:Label ID="label1" runat="server" CssClass="mandatoryField"></asp:Label>
                        <%--<asp:Label ID="ReccuringLEI" runat="server" CssClass="mandatoryField" Visible="false"></asp:Label>--%>
                            <hr />
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
                        <td align="left" style="width: 100%;" valign="top">
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            <%-------------------------  hidden fields  --------------------------------%>
                            <input type="hidden" id="hdnBranchName" runat="server" />
                            <input type="hidden" id="hdnDocType" runat="server" />
                            <input type="hidden" id="hdnDocNo" runat="server" />
                            <input type="hidden" id="hdnRejectReason" runat="server" />
                            <input type="hidden" id="hdnMT999LC" runat="server" />
                            <input type="hidden" id="hdnCustAbbr" runat="server" />
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
                            <input type="hidden" id="hdnDocumentScrutiny" runat="server" />
                            <input type="hidden" id="hdnBRONo" runat="server" />
                            <input type="hidden" id="hdnSGDocNo" runat="server" />
                            <input type="hidden" id="hdnUserName" runat="server" />
                            <input type="hidden" id="hdnNegoRemiSwiftCode" runat="server" />
                            <%-------Added by Bhupen for LEI on 01112022----------------%>
                            <input type="hidden" id="hdnBranchCode" runat="server" />
                            <input type="hidden" id="hdnleino" runat="server" />
                            <input type="hidden" id="hdnleiexpiry" runat="server" />
                            <input type="hidden" id="hdnDraweeleino" runat="server" />
                            <input type="hidden" id="hdnDraweeleiexpiry" runat="server" />
                            <input type="hidden" id="hdnLeiFlag" runat="server" />
                            <input type="hidden" id="hdnleiexpiry1" runat="server" />
                            <input type="hidden" id="hdnDraweeleiexpiry1" runat="server" />
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
                            <asp:TextBox ID="txtDocNo" Width="100px" runat="server" CssClass="textBox" ReadOnly="True"
                                TabIndex="1"></asp:TextBox>
                            &nbsp;
                            <asp:Label ID="lblForeign_Local" runat="server" CssClass="elementLabel"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblCollection_Lodgment_UnderLC" runat="server" CssClass="elementLabel"></asp:Label>&nbsp;&nbsp;
                            <asp:Label ID="lblSight_Usance" runat="server" CssClass="elementLabel"></asp:Label>
                        </td>
                        <td align="right" width="30%">
                            <span class="elementLabel">Received Date :</span>
                            <asp:TextBox ID="txtDateReceived" runat="server" CssClass="textBox" MaxLength="10"
                                ValidationGroup="dtVal" Width="70px" TabIndex="2"></asp:TextBox>
                            &nbsp; <span class="elementLabel">&nbsp; Lodgment Date :</span>
                            <asp:TextBox ID="txtLogdmentDate" runat="server" CssClass="textBox" MaxLength="10"
                                Enabled="false" Width="70px" TabIndex="3"></asp:TextBox>
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
                                                        OnTextChanged="txtCustomer_ID_TextChanged" TabIndex="4" Width="90px"></asp:TextBox>
                                                    <asp:Label ID="lblCustomerDesc" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                                <asp:Panel ID="panal_LC_No" runat="server">
                                                    <td align="right" width="10%">
                                                        <span class="elementLabel">LC No :</span>
                                                    </td>
                                                    <td align="left" width="70%" colspan="3">
                                                        <asp:TextBox ID="txt_LC_No" runat="server" CssClass="textBox" MaxLength="30" TabIndex="5"
                                                            Width="100px"></asp:TextBox>
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
                                            <tr runat="server" id="rowRiskCust" visible="false">
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">Special Case :</span>
                                                </td>
                                                <td align="left" width="30%">
                                                    <asp:CheckBox ID="chkSpecialCase" runat="server" onchange="return OnSpecialCasesChange();"
                                                        Style="margin-top: 500px;" />
                                                    <asp:Label ID="lblRiskCust" Text="Risk Customer :" runat="server" CssClass="elementLabel riskcustmercss"></asp:Label>
                                                    <asp:TextBox ID="txtRiskCust" runat="server" CssClass="textBox riskcustmercss" MaxLength="20"
                                                        TabIndex="4" Width="120px"></asp:TextBox>
                                                    &nbsp;&nbsp;
                                                    <asp:Label ID="lblSettelementAcNo" Text="Settelement A/C No :" runat="server" CssClass="elementLabel riskcustmercss"></asp:Label>
                                                    <asp:TextBox ID="txtSettelementAcNo" runat="server" CssClass="textBox riskcustmercss"
                                                        MaxLength="20" TabIndex="4" Width="100px"></asp:TextBox>
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
                                                    <asp:DropDownList ID="ddl_Doc_Currency" runat="server" CssClass="dropdownList" TabIndex="7"
                                                        onchange="return OnCurrencyChange();" OnSelectedIndexChanged="ddl_Doc_Currency_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lbl_Doc_Currency" runat="server" CssClass="elementLabel" Width="174px"></asp:Label>
                                                    &nbsp; <span class="elementLabel">Lodg. Amt :</span>
                                                    <asp:TextBox ID="txtBillAmount" runat="server" CssClass="textBox" MaxLength="12"
                                                        TabIndex="6" Width="120px" Style="text-align: right"></asp:TextBox>
                                                    <asp:Label ID="lbl_Exch_rate" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                                <td align="right" width="10%">
                                                    <asp:Label ID="lbl_AcceptanceDate" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                                <td align="left" width="10%">
                                                    <asp:TextBox ID="txt_AcceptanceDate" runat="server" CssClass="textBox" MaxLength="10"
                                                        ValidationGroup="dtVal" Width="70px" TabIndex="8"></asp:TextBox>
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
                                                        TabIndex="10">
                                                        <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="SIGHT"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="DEF Pmt"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="MIX pmt"></asp:ListItem>
                                                        <asp:ListItem Value="4" Text="Others"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtTenor" Width="30px" runat="server" CssClass="textBox" TabIndex="10"
                                                        MaxLength="3"></asp:TextBox>
                                                    <span class="elementLabel">Days From</span>
                                                    <asp:DropDownList ID="ddlTenor_Days_From" runat="server" Width="70px" CssClass="dropdownList"
                                                        TabIndex="10" Enabled="false">
                                                        <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                        <asp:ListItem Value="SHIPMENT DATE" Text="SHIPMENT DATE"></asp:ListItem>
                                                        <asp:ListItem Value="INVOICE DATE" Text="INVOICE DATE"></asp:ListItem>
                                                        <asp:ListItem Value="BOEXCHANGE DATE" Text="BOEXCHANGE DATE"></asp:ListItem>
                                                        <asp:ListItem Value="OTHERS/BLANK" Text="OTHERS/BLANK"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtTenor_Description" runat="server" CssClass="textBox" MaxLength="31"
                                                        TabIndex="11" Width="190px"></asp:TextBox>
                                                </td>
                                                <td align="right" width="5%">
                                                    <span class="elementLabel">BOExchange Date :</span>
                                                </td>
                                                <td align="left" width="10%">
                                                    <asp:TextBox ID="txtBOExchange" runat="server" CssClass="textBox" MaxLength="10"
                                                        ValidationGroup="dtVal" Width="70px" TabIndex="12"></asp:TextBox>
                                                </td>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">Due Date :</span>
                                                </td>
                                                <td align="left" width="30%">
                                                    <asp:TextBox ID="txtDueDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                                        Width="70px" TabIndex="13"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">Nego / Remit Bank :</span>
                                                </td>
                                                <td align="left" width="30%">
                                                    <asp:DropDownList ID="ddl_Nego_Remit_Bank" runat="server" Width='70px' CssClass="dropdownList"
                                                        TabIndex="14" AutoPostBack="True" OnSelectedIndexChanged="ddl_Nego_Remit_Bank_SelectedIndexChanged">
                                                        <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                        <asp:ListItem Value="FOREIGN" Text="FOREIGN"></asp:ListItem>
                                                        <asp:ListItem Value="LOCAL" Text="LOCAL"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lbl_Nego_Remit_Local_Foreign" Text="Bank Code :" runat="server" CssClass="elementLabel"></asp:Label>
                                                    <asp:TextBox ID="txtNego_Remit_Bank" runat="server" CssClass="textBox" MaxLength="12"
                                                        TabIndex="14" Width="100px" OnTextChanged="txtNego_Remit_Bank_TextChanged"></asp:TextBox>
                                                    <asp:Label ID="lbl_Nego_Remit_Bank" runat="server" CssClass="elementLabel" Font-Underline="true"></asp:Label>
                                                    <asp:Label ID="lbl_Nego_RemitSwift_IFSC" runat="server" CssClass="elementLabel" Font-Underline="true"></asp:Label>
                                                    <asp:Label ID="lbl_Nego_Remit_Bank_Addr" runat="server" CssClass="elementLabel" Font-Underline="true"></asp:Label>
                                                </td>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">Their Ref.No :</span>
                                                </td>
                                                <td align="left" width="10%">
                                                    <asp:TextBox ID="txt_Their_Ref_no" runat="server" CssClass="textBox" MaxLength="30"
                                                        TabIndex="15" Width="160px"></asp:TextBox>
                                                </td>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">Nego Date :</span>
                                                </td>
                                                <td align="left" width="30%">
                                                    <asp:TextBox ID="txtNego_Date" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                                        TabIndex="16" Width="70px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">A/C with Institution :</span>
                                                </td>
                                                <td align="left" width="30%">
                                                    <asp:TextBox ID="txtAcwithInstitution" AutoPostBack="True" runat="server" CssClass="textBox"
                                                        MaxLength="12" TabIndex="17" Width="100px" OnTextChanged="txtAcwithInstitution_TextChanged"></asp:TextBox>
                                                    <asp:Label ID="lblAcWithInstiBankDesc" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                                <td align="right" width="10%">
                                                </td>
                                                <td align="left" width="50%" colspan="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">Reimbursing Bank :</span>
                                                </td>
                                                <td width="30%" align="left">
                                                    <asp:TextBox ID="txtReimbursingbank" Width="100px" runat="server" CssClass="textBox"
                                                        TabIndex="19" MaxLength="12" AutoPostBack="True" OnTextChanged="txtReimbursingbank_TextChanged"></asp:TextBox>
                                                    <asp:Label ID="lbl_Reimbursingbank" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">Invoice No :</span>
                                                </td>
                                                <td align="left" width="50%" colspan="3">
                                                    <asp:TextBox ID="txt_Inv_No" runat="server" CssClass="textBox" MaxLength="100" TabIndex="21"
                                                        Width="300px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">Drawer :</span>
                                                </td>
                                                <td align="left" width="30%">
                                                    <asp:DropDownList ID="ddlDrawer" runat="server" Width='500px' CssClass="dropdownList"
                                                        TabIndex="20">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">Invoice Date :</span>
                                                </td>
                                                <td align="left" width="50%" colspan="3">
                                                    <asp:TextBox ID="txt_Inv_Date" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                                        Width="70px" TabIndex="22"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">Commodity Code :</span>
                                                </td>
                                                <td align="left" width="30%">
                                                    <asp:DropDownList ID="ddl_Commodity" runat="server" CssClass="dropdownList" MaxLength="3"
                                                        TabIndex="23" Width="60px" AutoPostBack="True" OnSelectedIndexChanged="ddl_Commodity_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblCommodityDesc" runat="server" CssClass="elementLabel"></asp:Label>&nbsp;
                                                    <asp:TextBox ID="txtCommodityDesc" runat="server" CssClass="textBox" MaxLength="100"
                                                        TabIndex="24" Width="250px"></asp:TextBox>
                                                </td>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">Country of Origin :</span>
                                                </td>
                                                <td align="left" width="50%" colspan="3">
                                                    <asp:TextBox ID="txt_CountryOfOrigin" runat="server" CssClass="textBox" MaxLength="100"
                                                        Width="25px" TabIndex="25"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">Country Code :</span>
                                                </td>
                                                <td align="left" width="30%">
                                                    <asp:DropDownList ID="ddlCountryCode" runat="server" CssClass="dropdownList" MaxLength="2"
                                                        TabIndex="26" Width="60px" AutoPostBack="True" OnSelectedIndexChanged="ddlCountryCode_SelectedIndexChanged">
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
                                                            <td align="right" width="10%">
                                                                <span class="elementLabel">Shipment Date :</span>
                                                            </td>
                                                            <td align="left" width="50%">
                                                                <asp:TextBox ID="txtShippingDate" runat="server" CssClass="textBox" MaxLength="10"
                                                                    ValidationGroup="dtVal" Width="70px" TabIndex="27"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">FIRST :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtDocFirst1" runat="server" CssClass="textBox" MaxLength="30" TabIndex="28"
                                                                    Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                </span><asp:TextBox ID="txtDocFirst2" runat="server" CssClass="textBox" MaxLength="30"
                                                                    TabIndex="29" Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtDocFirst3" runat="server" CssClass="textBox" MaxLength="30" TabIndex="30"
                                                                    Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <span class="elementLabel">Vessel Name :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtVesselName" runat="server" CssClass="textBox" MaxLength="30"
                                                                    TabIndex="34" Width="300px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">SECOND :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtDocSecond1" runat="server" CssClass="textBox" MaxLength="30"
                                                                    TabIndex="31" Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtDocSecond2" runat="server" CssClass="textBox" MaxLength="30"
                                                                    TabIndex="32" Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtDocSecond3" runat="server" CssClass="textBox" MaxLength="30"
                                                                    TabIndex="33" Width="100px"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <span class="elementLabel">From :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtFromPort" runat="server" CssClass="textBox" MaxLength="30" TabIndex="35"
                                                                    Width="100px"></asp:TextBox>
                                                                &nbsp;&nbsp; <span class="elementLabel">To :</span>
                                                                <asp:TextBox ID="txtToPort" runat="server" CssClass="textBox" MaxLength="30" TabIndex="36"
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
                                                                <span class="elementLabel" style="font-weight: bold">Comission / Charges :</span>
                                                            </td>
                                                            <td align="left" width="10%">
                                                                <span class="elementLabel" style="font-weight: bold">CURRENCY</span>
                                                            </td>
                                                            <td align="left" width="10%">
                                                                &nbsp;&nbsp;<span class="elementLabel" style="font-weight: bold">AMOUNT</span>
                                                            </td>
                                                            <td align="left" width="35%">
                                                                <asp:Button ID="btn_DiscrepancyList" runat="server" Text="Discrepancy" CssClass="buttonDefault"
                                                                    Height="20px" Width="150px"></asp:Button>
                                                                <asp:CheckBox ID="cb_Protest" Text="Protest Flag" CssClass="elementLabel" runat="server" />
                                                                <asp:RadioButton ID="rdb_MT499" Text="MT499" CssClass="elementLabel" runat="server"
                                                                    GroupName="Discrepancy_Type" />
                                                                <asp:RadioButton ID="rdb_MT734" Text="MT734" CssClass="elementLabel" runat="server"
                                                                    GroupName="Discrepancy_Type" />
                                                                <asp:RadioButton ID="rdb_MT799" Text="MT799" CssClass="elementLabel" runat="server"
                                                                    GroupName="Discrepancy_Type" />
                                                                <asp:RadioButton ID="rdb_MT999" Text="MT999" CssClass="elementLabel" runat="server"
                                                                    GroupName="Discrepancy_Type" />
                                                                <asp:RadioButton ID="rbd_None" Text="None" CssClass="elementLabel" runat="server"
                                                                    GroupName="Discrepancy_Type" />
                                                            </td>
                                                            <td width="35%">
                                                                <asp:Button ID="btn_NarrativeList" runat="server" Text="Narrative" CssClass="buttonDefault"
                                                                    Height="20px" Width="150px"></asp:Button>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">Interest :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="ddl_Interest_Currency" runat="server" CssClass="dropdownList"
                                                                    Enabled="False" TabIndex="37">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_Interest_Amount" runat="server" CssClass="textBox" MaxLength="12"
                                                                    TabIndex="38" Width="150px" Style="text-align: right"></asp:TextBox>
                                                            </td>
                                                            <td valign="top" rowspan='5'>
                                                                <ajaxToolkit:CollapsiblePanelExtender ID="CPE_Discrepancy" runat="server" TargetControlID="panel_AddDiscrepancy"
                                                                    CollapsedSize="0" ExpandedSize="150" ExpandControlID="btn_DiscrepancyList" CollapseControlID="btn_DiscrepancyList"
                                                                    ScrollContents="True" Enabled="True" />
                                                                <asp:Panel ID="panel_AddDiscrepancy" runat="server">
                                                                    <table cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">1.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_1" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">2.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_2" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">3.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_3" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">4.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_4" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">5.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_5" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">6.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_6" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">7.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_7" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">8.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_8" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">9.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_9" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">10.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_10" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">11.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_11" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">12.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_12" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">13.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_13" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">14.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_14" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">15.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_15" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">16.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_16" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">17.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_17" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">18.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_18" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">19.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_19" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">20.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_20" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">21.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_21" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">22.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_22" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">23.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_23" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">24.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_24" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">25.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_25" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">26.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_26" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">27.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_27" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">28.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_28" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">29.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_29" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">30.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_30" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">31.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_31" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">32.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_32" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">33.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_33" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">34.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_34" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">35.</span>
                                                                                <asp:TextBox ID="txt_Discrepancy_35" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
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
                                                                                <asp:TextBox ID="txt_Narrative_1" Width="90%" runat="server" CssClass="textBox" MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">2.</span>
                                                                                <asp:TextBox ID="txt_Narrative_2" Width="90%" runat="server" CssClass="textBox" MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">3.</span>
                                                                                <asp:TextBox ID="txt_Narrative_3" Width="90%" runat="server" CssClass="textBox" MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">4.</span>
                                                                                <asp:TextBox ID="txt_Narrative_4" Width="90%" runat="server" CssClass="textBox" MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">5.</span>
                                                                                <asp:TextBox ID="txt_Narrative_5" Width="90%" runat="server" CssClass="textBox" MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">6.</span>
                                                                                <asp:TextBox ID="txt_Narrative_6" Width="90%" runat="server" CssClass="textBox" MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">7.</span>
                                                                                <asp:TextBox ID="txt_Narrative_7" Width="90%" runat="server" CssClass="textBox" MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">8.</span>
                                                                                <asp:TextBox ID="txt_Narrative_8" Width="90%" runat="server" CssClass="textBox" MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">9.</span>
                                                                                <asp:TextBox ID="txt_Narrative_9" Width="90%" runat="server" CssClass="textBox" MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">10.</span>
                                                                                <asp:TextBox ID="txt_Narrative_10" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">11.</span>
                                                                                <asp:TextBox ID="txt_Narrative_11" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">12.</span>
                                                                                <asp:TextBox ID="txt_Narrative_12" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">13.</span>
                                                                                <asp:TextBox ID="txt_Narrative_13" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">14.</span>
                                                                                <asp:TextBox ID="txt_Narrative_14" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">15.</span>
                                                                                <asp:TextBox ID="txt_Narrative_15" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">16.</span>
                                                                                <asp:TextBox ID="txt_Narrative_16" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">17.</span>
                                                                                <asp:TextBox ID="txt_Narrative_17" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">18.</span>
                                                                                <asp:TextBox ID="txt_Narrative_18" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">19.</span>
                                                                                <asp:TextBox ID="txt_Narrative_19" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">20.</span>
                                                                                <asp:TextBox ID="txt_Narrative_20" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">21.</span>
                                                                                <asp:TextBox ID="txt_Narrative_21" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">22.</span>
                                                                                <asp:TextBox ID="txt_Narrative_22" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">23.</span>
                                                                                <asp:TextBox ID="txt_Narrative_23" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">24.</span>
                                                                                <asp:TextBox ID="txt_Narrative_24" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">25.</span>
                                                                                <asp:TextBox ID="txt_Narrative_25" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">26.</span>
                                                                                <asp:TextBox ID="txt_Narrative_26" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">27.</span>
                                                                                <asp:TextBox ID="txt_Narrative_27" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">28.</span>
                                                                                <asp:TextBox ID="txt_Narrative_28" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">29.</span>
                                                                                <asp:TextBox ID="txt_Narrative_29" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">30.</span>
                                                                                <asp:TextBox ID="txt_Narrative_30" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">31.</span>
                                                                                <asp:TextBox ID="txt_Narrative_31" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">32.</span>
                                                                                <asp:TextBox ID="txt_Narrative_32" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left">
                                                                                <span class="elementLabel">33.</span>
                                                                                <asp:TextBox ID="txt_Narrative_33" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">34.</span>
                                                                                <asp:TextBox ID="txt_Narrative_34" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="elementLabel">35.</span>
                                                                                <asp:TextBox ID="txt_Narrative_35" Width="90%" runat="server" CssClass="textBox"
                                                                                    MaxLength="50"></asp:TextBox>
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
                                                                    Enabled="False" TabIndex="39">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtComissionAmount" runat="server" CssClass="textBox" MaxLength="12"
                                                                    TabIndex="40" Width="150px" Style="text-align: right"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <span class="elementLabel">Other Commission :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="ddl_Other_Currency" runat="server" CssClass="dropdownList"
                                                                    Enabled="False" TabIndex="41">
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
                                                                    Enabled="False" TabIndex="45">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtTheirCommission_Amount" runat="server" CssClass="textBox" TabIndex="46"
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
                                                                    TabIndex="47" Width="150px" Style="text-align: right" Enabled="False"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <asp:Panel ID="panal_Stamp_Duty_Charges" runat="server" Visible="false">
                                                                <td align="right">
                                                                    <span class="elementLabel">Stamp Duty Charges :</span>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:DropDownList ID="ddl_Stamp_Duty_Charges_Curr" runat="server" CssClass="dropdownList"
                                                                        Enabled="False" TabIndex="45">
                                                                        <asp:ListItem Value="INR" Text="INR"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <span class="elementLabel">Exch. Rate:</span>
                                                                    <asp:TextBox ID="txt_Stamp_Duty_Charges_ExRate" runat="server" CssClass="textBox"
                                                                        TabIndex="46" MaxLength="5" Width="50px" Style="text-align: right"></asp:TextBox>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txt_Stamp_Duty_Charges_Amount" runat="server" CssClass="textBox"
                                                                        TabIndex="46" MaxLength="12" Width="150px" Style="text-align: right" Enabled="False"></asp:TextBox>
                                                                </td>
                                                            </asp:Panel>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2">
                                                                <asp:Panel ID="panal_Scrutiny" runat="server">
                                                                    <span class="elementLabel">Documents Scrutiny :</span>
                                                                    <asp:DropDownList ID="ddl_Doc_Scrutiny" runat="server" CssClass="dropdownList" Enabled="False"
                                                                        TabIndex="48">
                                                                        <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                                        <asp:ListItem Value="1" Text="Clean"></asp:ListItem>
                                                                        <asp:ListItem Value="2" Text="Discrepant"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </asp:Panel>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:Button ID="btnNext" runat="server" Text="Next >>" CssClass="buttonDefault" ToolTip="Go to Instructions"
                                                                    OnClick="btnDocNext_Click" OnClientClick="return OnDocNextClickNew();" />&nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btn_Swift_Create" runat="server" Text="View Swift Massage" CssClass="buttonDefault"
                                                                    Width="150px" OnClientClick="ViewSwiftMessage();"></asp:Button>&nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btn_SFMS_create" runat="server" Text="View SFMS Massage" CssClass="buttonDefault"
                                                                    Width="150px" OnClientClick="ViewSFMSMessage();"></asp:Button>
                                                                    <asp:Button ID="btn_Verify" runat="server" Text="LEIVerify" CssClass="buttonDefault"
                                                                    ToolTip="Click here to verify LEI Details" Visible="false" TabIndex="107" OnClick="btn_Verify_Click"/>
                                                                <%-- OnClientClick="return LeiVerify();"  />--%>
                                                                <asp:Label ID="lblSwift_SFMS" runat="server" CssClass="elementLabel"></asp:Label>
                                                                <asp:Label ID="lblFolderLink" runat="server" CssClass="elementLabel"></asp:Label>
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
                                                        Enabled="false"></asp:TextBox>
                                                    <span class="elementLabel">2 :</span>&nbsp;
                                                    <asp:TextBox ID="txtChargesClaimed7342" Width="350px" runat="server" CssClass="textBox"
                                                        Enabled="false"></asp:TextBox>
                                                    <span class="elementLabel">3 :</span>&nbsp;
                                                    <asp:TextBox ID="txtChargesClaimed7343" Width="350px" runat="server" CssClass="textBox"
                                                        Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                </td>
                                                <td align="left">
                                                    <span class="elementLabel">4 :</span>&nbsp;
                                                    <asp:TextBox ID="txtChargesClaimed7344" Width="350px" runat="server" CssClass="textBox"
                                                        Enabled="false"></asp:TextBox>
                                                    <span class="elementLabel">5 :</span>&nbsp;
                                                    <asp:TextBox ID="txtChargesClaimed7345" Width="350px" runat="server" CssClass="textBox"
                                                        Enabled="false"></asp:TextBox>
                                                    <span class="elementLabel">6 :</span>&nbsp;
                                                    <asp:TextBox ID="txtChargesClaimed7346" Width="350px" runat="server" CssClass="textBox"
                                                        Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">[33A] Total Amount Claimed </span>
                                                    <asp:DropDownList ID="ddlTotalAmountClaimed734" runat="server" CssClass="dropdownList"
                                                        Enabled="false">
                                                        <asp:ListItem Text="33A" Value="A"></asp:ListItem>
                                                        <asp:ListItem Text="33B" Value="B"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    &nbsp;
                                                </td>
                                                <td align="left">
                                                    &nbsp;<asp:Label ID="lblDate734" runat="server" Text="Date :" CssClass="elementLabel"></asp:Label>
                                                    <asp:TextBox ID="txtDate734" Width="70px" runat="server" CssClass="textBox" Enabled="false"></asp:TextBox>
                                                    <span class="elementLabel">Currency :</span>
                                                    <asp:TextBox ID="txtCurrency734" Width="30px" runat="server" CssClass="textBox" Enabled="false"></asp:TextBox>
                                                    <span class="elementLabel">Amount :</span>
                                                    <asp:TextBox ID="txtAmount734" Width="130px" runat="server" CssClass="textBox" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">[57A] Account With Bank : </span>
                                                    <asp:DropDownList ID="ddlAccountWithBank734" runat="server" CssClass="dropdownList"
                                                        Enabled="false">
                                                        <asp:ListItem Text="57A" Value="A"></asp:ListItem>
                                                        <asp:ListItem Text="57B" Value="B"></asp:ListItem>
                                                        <asp:ListItem Text="57D" Value="D"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <span class="elementLabel">A / C :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtAccountWithBank734PartyIdentifier" runat="server" CssClass="textBox"
                                                        TabIndex="47" Width="10px" MaxLength="1" Enabled="false"></asp:TextBox><asp:TextBox
                                                            ID="txtAccountWithBank734AccountNo" runat="server" CssClass="textBox" Width="300px"
                                                            Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblAccountWithBank734SwiftCode" runat="server" CssClass="elementLabel"
                                                        Text="Swift/IFSC Code :"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox runat="server" ID="txtAccountWithBank734SwiftCode" CssClass="textBox"
                                                        Enabled="false" Width="100px"></asp:TextBox>
                                                    <asp:TextBox runat="server" ID="txtAccountWithBank734Location" CssClass="textBox"
                                                        Style="display: none;" Enabled="false" Width="250px"></asp:TextBox>
                                                    <asp:TextBox runat="server" ID="txtAccountWithBank734Name" CssClass="textBox" Style="display: none;"
                                                        Enabled="false" Width="250px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblAccountWithBank734Address1" runat="server" CssClass="elementLabel"
                                                        Style="display: none;" Text="Address1 :"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox runat="server" ID="txtAccountWithBank734Address1" CssClass="textBox"
                                                        Style="display: none;" Enabled="false" Width="250px" TabIndex="47"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblAccountWithBank734Address2" runat="server" CssClass="elementLabel"
                                                        Style="display: none;" Text="Address2 :"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox runat="server" ID="txtAccountWithBank734Address2" CssClass="textBox"
                                                        Style="display: none;" Enabled="false" Width="250px" TabIndex="47"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblAccountWithBank734Address3" runat="server" CssClass="elementLabel"
                                                        Style="display: none;" Text="Address3 :"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox runat="server" ID="txtAccountWithBank734Address3" CssClass="textBox"
                                                        Style="display: none;" Enabled="false" Width="250px" TabIndex="47"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">[72] Sender to Receiver Information :</span>
                                                </td>
                                                <td align="left">
                                                    <span class="elementLabel">1 :</span>&nbsp;
                                                    <asp:TextBox ID="txtSendertoReceiverInformation7341" Width="350px" runat="server"
                                                        CssClass="textBox" Enabled="false"></asp:TextBox>
                                                    <span class="elementLabel">2 :</span>&nbsp;
                                                    <asp:TextBox ID="txtSendertoReceiverInformation7342" Width="350px" runat="server"
                                                        CssClass="textBox" Enabled="false"></asp:TextBox>
                                                    <span class="elementLabel">3 :</span>&nbsp;
                                                    <asp:TextBox ID="txtSendertoReceiverInformation7343" Width="350px" runat="server"
                                                        CssClass="textBox" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                </td>
                                                <td align="left">
                                                    <span class="elementLabel">4 :</span>&nbsp;
                                                    <asp:TextBox ID="txtSendertoReceiverInformation7344" Width="350px" runat="server"
                                                        CssClass="textBox" Enabled="false"></asp:TextBox>
                                                    <span class="elementLabel">5 :</span>&nbsp;
                                                    <asp:TextBox ID="txtSendertoReceiverInformation7345" Width="350px" runat="server"
                                                        CssClass="textBox" Enabled="false"></asp:TextBox>
                                                    <span class="elementLabel">6 :</span>&nbsp;
                                                    <asp:TextBox ID="txtSendertoReceiverInformation7346" Width="350px" runat="server"
                                                        CssClass="textBox" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">[77B] Disposal Of Document:</span>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddl_DisposalOfDoc" runat="server" CssClass="dropdownList" Width="80px"
                                                        Enabled="false">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr><td><br /></td></tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td align="left">
                                                    <asp:Button ID="btnSwiftPrev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                        ToolTip="Back to Document Details" TabIndex="106" OnClick="btnSwiftPrev_Click"
                                                        OnClientClick="return OnSwiftPrevClickNew();" />&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnSwiftNext" runat="server" Text=">> Next" CssClass="buttonDefault"
                                                        ToolTip="Go to Import Accounting" TabIndex="106" OnClick="btnSwiftNext_Click"
                                                        OnClientClick="return OnSwiftNextClickNew();" />
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
                                                            GroupName="SP_Instruction" />
                                                        <asp:RadioButton ID="rdb_SP_Instr_Other" Text="Others" CssClass="elementLabel" runat="server"
                                                            GroupName="SP_Instruction" TabIndex="100" />
                                                        <asp:RadioButton ID="rdb_SP_Instr_Annexure" Text="As per Annexure" CssClass="elementLabel"
                                                            runat="server" GroupName="SP_Instruction" TabIndex="100" />
                                                        <asp:RadioButton ID="rdb_SP_Instr_On_Sight" Text="Bene. to paid on sight" CssClass="elementLabel"
                                                            runat="server" GroupName="SP_Instruction1" TabIndex="100" />
                                                        <asp:RadioButton ID="rdb_SP_Instr_On_Date" Text="Bene. to paid on Dated" CssClass="elementLabel"
                                                            runat="server" GroupName="SP_Instruction1" TabIndex="100" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <br />
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                <td align="right" valign="top" width="10%">
                                                    <span class="elementLabel">Special Instructions :</span>
                                                </td>
                                                <td width="90%" align="left">
                                                    <asp:TextBox ID="txt_SP_Instructions1" Width="400px" Height="50px" runat="server"
                                                        CssClass="textBox" MaxLength="80" TabIndex="101"></asp:TextBox>
                                                </td>
                                            </tr>--%>
                                                <tr>
                                                    <td align="right" width="20%">
                                                        <span class="elementLabel">Special Instructions :</span>
                                                    </td>
                                                    <td align="left" width="80%">
                                                        <span class="elementLabel">1.</span>
                                                        <asp:TextBox ID="txt_SP_Instructions1" Width="400px" runat="server" CssClass="textBox"
                                                            MaxLength="60"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <span class="elementLabel">2.</span>
                                                        <asp:TextBox ID="txt_SP_Instructions2" Width="400px" runat="server" CssClass="textBox"
                                                            MaxLength="60"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td align="left">
                                                        <span class="elementLabel">3.</span>
                                                        <asp:TextBox ID="txt_SP_Instructions3" Width="400px" runat="server" CssClass="textBox"
                                                            MaxLength="60"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <span class="elementLabel">4.</span>
                                                        <asp:TextBox ID="txt_SP_Instructions4" Width="400px" runat="server" CssClass="textBox"
                                                            MaxLength="60"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <span class="elementLabel">5.</span>
                                                        <asp:TextBox ID="txt_SP_Instructions5" Width="400px" runat="server" CssClass="textBox"
                                                            MaxLength="60"></asp:TextBox>
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
                                                    <td>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel" style="font-weight: bold">Own LC Discounting :</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:RadioButton ID="rdb_ownLCDiscount_Yes" Text="Yes" CssClass="elementLabel" runat="server"
                                                            GroupName="ownLCDiscount" TabIndex="100" />
                                                        <asp:RadioButton ID="rdb_ownLCDiscount_No" Text="No" Checked="true" CssClass="elementLabel"
                                                            runat="server" GroupName="ownLCDiscount" TabIndex="100" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <br />
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                <td colspan="2">
                                                    <br />
                                                </td>
                                            </tr>--%>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Button ID="btn_Instr_Prev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                            ToolTip="Back to Document Details" TabIndex="106" OnClick="btn_Instr_Prev_Click"
                                                            OnClientClick="return OnInstPrevClickNew();" />&nbsp;&nbsp;&nbsp;
                                                        <asp:Button ID="btn_Instr_Next" runat="server" Text=">> Next" CssClass="buttonDefault"
                                                            ToolTip="Go to Import Accounting" TabIndex="106" OnClick="btn_Instr_Next_Click"
                                                            OnClientClick="return OnInstNextClickNew();" />
                                                        <asp:Button ID="btnSave" Style="visibility: hidden" runat="server" Text="Save" CssClass="buttonDefault"
                                                            ToolTip="Save" OnClick="btnSave_Click" TabIndex="107" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="width: 40%; float: left; height: 100%">
                                            <asp:Panel ID="PanelPaymentSwiftDetail" runat="server">
                                                <table width="100%">
                                                    <tr>
                                                        <td width="15%">
                                                        </td>
                                                        <td width="85%">
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
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlPaymentSwift56_SelectedIndexChanged">
                                                                <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                                <asp:ListItem Value="56A" Text="56A"></asp:ListItem>
                                                                <asp:ListItem Value="56D" Text="56D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Intermediary</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <asp:Panel ID="panel56ACC_No" runat="server" Visible="False">
                                                            <td align="right">
                                                                <span class="elementLabel">56 A/C No :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_PaymentSwift56ACC_No" Width="180px" runat="server" CssClass="textBox"
                                                                    MaxLength="35"></asp:TextBox>
                                                            </td>
                                                        </asp:Panel>
                                                    </tr>
                                                    <tr>
                                                        <asp:Panel ID="panel56A" runat="server" Visible="False">
                                                            <td align="right">
                                                                <span class="elementLabel">56A Swift :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_PaymentSwift56A" Width="100px" runat="server" CssClass="textBox"
                                                                    MaxLength="11"></asp:TextBox>
                                                            </td>
                                                        </asp:Panel>
                                                    </tr>
                                                    <tr>
                                                        <asp:Panel ID="panel56DName" runat="server" Visible="False">
                                                            <td align="right">
                                                                <span class="elementLabel">56D Name :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_PaymentSwift56D_name" Width="350px" runat="server" CssClass="textBox"
                                                                    MaxLength="35"></asp:TextBox>
                                                            </td>
                                                        </asp:Panel>
                                                    </tr>
                                                    <tr>
                                                        <asp:Panel ID="panel56DAddress" runat="server" Visible="False">
                                                            <td align="right">
                                                                <span class="elementLabel">56D Address :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_PaymentSwift56D_Address" Width="350px" runat="server" CssClass="textBox"
                                                                    MaxLength="100"></asp:TextBox>
                                                            </td>
                                                        </asp:Panel>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <asp:DropDownList ID="ddlPaymentSwift57" runat="server" CssClass="dropdownList" Width="60px"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlPaymentSwift57_SelectedIndexChanged">
                                                                <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                                <asp:ListItem Value="57A" Text="57A"></asp:ListItem>
                                                                <asp:ListItem Value="57D" Text="57D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">A/c with Institution</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <asp:Panel ID="panel57ACC_No" runat="server" Visible="False">
                                                            <td align="right">
                                                                <span class="elementLabel">57 A/C No :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_PaymentSwift57ACC_No" Width="180px" runat="server" CssClass="textBox"
                                                                    MaxLength="35"></asp:TextBox>
                                                            </td>
                                                        </asp:Panel>
                                                    </tr>
                                                    <tr>
                                                        <asp:Panel ID="panel57A" runat="server" Visible="False">
                                                            <td align="right">
                                                                <span class="elementLabel">57A Swift :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_PaymentSwift57A" Width="100px" runat="server" CssClass="textBox"
                                                                    MaxLength="11"></asp:TextBox>
                                                            </td>
                                                        </asp:Panel>
                                                    </tr>
                                                    <tr>
                                                        <asp:Panel ID="panel57DName" runat="server" Visible="False">
                                                            <td align="right">
                                                                <span class="elementLabel">57D Name :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_PaymentSwift57D_name" Width="350px" runat="server" CssClass="textBox"
                                                                    MaxLength="80"></asp:TextBox>
                                                            </td>
                                                        </asp:Panel>
                                                    </tr>
                                                    <tr>
                                                        <asp:Panel ID="panel57DAddress" runat="server" Visible="False">
                                                            <td align="right">
                                                                <span class="elementLabel">57D Address :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_PaymentSwift57D_Address" Width="350px" runat="server" CssClass="textBox"
                                                                    MaxLength="100"></asp:TextBox>
                                                            </td>
                                                        </asp:Panel>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <asp:DropDownList ID="ddlPaymentSwift58" runat="server" CssClass="dropdownList" Width="60px"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlPaymentSwift58_SelectedIndexChanged">
                                                                <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                                <asp:ListItem Value="58A" Text="58A"></asp:ListItem>
                                                                <asp:ListItem Value="58D" Text="58D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Beneficiary Institution</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <asp:Panel ID="panel58ACC_No" runat="server" Visible="False">
                                                            <td align="right">
                                                                <span class="elementLabel">58 A/C No :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_PaymentSwift58ACC_No" Width="180px" runat="server" CssClass="textBox"
                                                                    MaxLength="35"></asp:TextBox>
                                                            </td>
                                                        </asp:Panel>
                                                    </tr>
                                                    <tr>
                                                        <asp:Panel ID="panel58A" runat="server" Visible="False">
                                                            <td align="right">
                                                                <span class="elementLabel">58A Swift :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_PaymentSwift58A" Width="100px" runat="server" CssClass="textBox"
                                                                    MaxLength="11"></asp:TextBox>
                                                            </td>
                                                        </asp:Panel>
                                                    </tr>
                                                    <tr>
                                                        <asp:Panel ID="panel58DName" runat="server" Visible="False">
                                                            <td align="right">
                                                                <span class="elementLabel">58D Name :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_PaymentSwift58D_name" Width="350px" runat="server" CssClass="textBox"
                                                                    MaxLength="80"></asp:TextBox>
                                                            </td>
                                                        </asp:Panel>
                                                    </tr>
                                                    <tr>
                                                        <asp:Panel ID="panel58Address" runat="server" Visible="False">
                                                            <td align="right">
                                                                <span class="elementLabel">58D Address 1 :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_PaymentSwift58D_Address" Width="350px" runat="server" CssClass="textBox"
                                                                    MaxLength="100"></asp:TextBox>
                                                            </td>
                                                        </asp:Panel>
                                                    </tr>
                                                    <tr>
                                                        <asp:Panel ID="panel58Address2" runat="server" Visible="False">
                                                            <td align="right">
                                                                <span class="elementLabel">58D Address 2 :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_PaymentSwift58D_Address2" Width="350px" runat="server" CssClass="textBox"
                                                                    MaxLength="100"></asp:TextBox>
                                                            </td>
                                                        </asp:Panel>
                                                    </tr>
                                                    <tr>
                                                        <asp:Panel ID="panel58Address3" runat="server" Visible="False">
                                                            <td align="right">
                                                                <span class="elementLabel">58D Address 3 :</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_PaymentSwift58D_Address3" Width="350px" runat="server" CssClass="textBox"
                                                                    MaxLength="100"></asp:TextBox>
                                                            </td>
                                                        </asp:Panel>
                                                    </tr>
                                                    <tr>
                                                        <asp:Panel ID="panel58Address4" runat="server" Visible="False">
                                                            <td align="right">
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txt_PaymentSwift58D_Address4" Width="350px" runat="server" CssClass="textBox"
                                                                    MaxLength="100" Style="visibility: hidden"></asp:TextBox>
                                                            </td>
                                                        </asp:Panel>
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
                                                        <asp:TextBox ID="txtAMLDrawee" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                            TabIndex="70"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">Drawer :</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtAMLDrawer" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                            TabIndex="71"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">Nego / Remit Bank :</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtAMLNagoRemiBank" Width="450px" runat="server" CssClass="textBox"
                                                            MaxLength="50" TabIndex="72"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                    <td align="right">
                                                        <span class="elementLabel">Swift Code :</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtAMLSwiftCode" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                            TabIndex="72"></asp:TextBox>
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">Commodity :</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtAMLCommodity" Width="450px" runat="server" CssClass="textBox"
                                                            MaxLength="50" TabIndex="73"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">Vessel :</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtAMLVessel" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                            TabIndex="74"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">From Port :</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtAMLFromPort" Width="450px" runat="server" CssClass="textBox"
                                                            MaxLength="50" TabIndex="75"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">Country :</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtAMLCountry" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                            TabIndex="75"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="elementLabel">Country of Origin :</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtCountryOfOrigin" Width="450px" runat="server" CssClass="textBox"
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
                                                                            <asp:TextBox ID="txtAML1" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                                                TabIndex="76"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 50%">
                                                                            <span class="elementLabel">2.</span>
                                                                        </td>
                                                                        <td align="left" style="width: 50%">
                                                                            <asp:TextBox ID="txtAML2" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                                                TabIndex="76"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 50%">
                                                                            <span class="elementLabel">3.</span>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtAML3" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                                                TabIndex="76"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 50%">
                                                                            <span class="elementLabel">4.</span>
                                                                        </td>
                                                                        <td align="left" style="width: 50%">
                                                                            <asp:TextBox ID="txtAML4" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                                                TabIndex="76"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 50%">
                                                                            <span class="elementLabel">5.</span>
                                                                        </td>
                                                                        <td align="left" style="width: 50%">
                                                                            <asp:TextBox ID="txtAML5" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                                                TabIndex="76"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 50%">
                                                                            <span class="elementLabel">6.</span>
                                                                        </td>
                                                                        <td align="left" style="width: 50%">
                                                                            <asp:TextBox ID="txtAML6" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                                                TabIndex="76"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 50%">
                                                                            <span class="elementLabel">7.</span>
                                                                        </td>
                                                                        <td align="left" style="width: 50%">
                                                                            <asp:TextBox ID="txtAML7" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                                                TabIndex="76"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 50%">
                                                                            <span class="elementLabel">8.</span>
                                                                        </td>
                                                                        <td align="left" style="width: 50%">
                                                                            <asp:TextBox ID="txtAML8" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                                                TabIndex="76"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 50%">
                                                                            <span class="elementLabel">9.</span>
                                                                        </td>
                                                                        <td align="left" style="width: 50%">
                                                                            <asp:TextBox ID="txtAML9" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                                                TabIndex="76"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 50%">
                                                                            <span class="elementLabel">10.</span>
                                                                        </td>
                                                                        <td align="left" style="width: 50%">
                                                                            <asp:TextBox ID="txtAML10" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                                                TabIndex="76"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 50%">
                                                                            <span class="elementLabel">11.</span>
                                                                        </td>
                                                                        <td align="left" style="width: 50%">
                                                                            <asp:TextBox ID="txtAML11" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                                                TabIndex="76"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 50%">
                                                                            <span class="elementLabel">12.</span>
                                                                        </td>
                                                                        <td align="left" style="width: 50%">
                                                                            <asp:TextBox ID="txtAML12" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                                                TabIndex="76"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 50%">
                                                                            <span class="elementLabel">13.</span>
                                                                        </td>
                                                                        <td align="left" style="width: 50%">
                                                                            <asp:TextBox ID="txtAML13" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                                                TabIndex="76"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 50%">
                                                                            <span class="elementLabel">14.</span>
                                                                        </td>
                                                                        <td align="left" style="width: 50%">
                                                                            <asp:TextBox ID="txtAML14" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                                                TabIndex="76"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 50%">
                                                                            <span class="elementLabel">15.</span>
                                                                        </td>
                                                                        <td align="left" style="width: 50%">
                                                                            <asp:TextBox ID="txtAML15" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                                                TabIndex="76"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 50%">
                                                                            <span class="elementLabel">16.</span>
                                                                        </td>
                                                                        <td align="left" style="width: 50%">
                                                                            <asp:TextBox ID="txtAML16" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                                                TabIndex="76"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 50%">
                                                                            <span class="elementLabel">17.</span>
                                                                        </td>
                                                                        <td align="left" style="width: 50%">
                                                                            <asp:TextBox ID="txtAML17" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                                                TabIndex="76"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 50%">
                                                                            <span class="elementLabel">18.</span>
                                                                        </td>
                                                                        <td align="left" style="width: 50%">
                                                                            <asp:TextBox ID="txtAML18" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                                                TabIndex="76"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 50%">
                                                                            <span class="elementLabel">19.</span>
                                                                        </td>
                                                                        <td align="left" style="width: 50%">
                                                                            <asp:TextBox ID="txtAML19" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                                                TabIndex="76"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 50%">
                                                                            <span class="elementLabel">20.</span>
                                                                        </td>
                                                                        <td align="left" style="width: 50%">
                                                                            <asp:TextBox ID="txtAML20" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                                                TabIndex="76"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 50%">
                                                                            <span class="elementLabel">21.</span>
                                                                        </td>
                                                                        <td align="left" style="width: 50%">
                                                                            <asp:TextBox ID="txtAML21" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                                                TabIndex="76"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 50%">
                                                                            <span class="elementLabel">22.</span>
                                                                        </td>
                                                                        <td align="left" style="width: 50%">
                                                                            <asp:TextBox ID="txtAML22" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                                                TabIndex="76"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 50%">
                                                                            <span class="elementLabel">23.</span>
                                                                        </td>
                                                                        <td align="left" style="width: 50%">
                                                                            <asp:TextBox ID="txtAML23" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                                                TabIndex="76"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 50%">
                                                                            <span class="elementLabel">24.</span>
                                                                        </td>
                                                                        <td align="left" style="width: 50%">
                                                                            <asp:TextBox ID="txtAML24" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                                                TabIndex="76"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 50%">
                                                                            <span class="elementLabel">25.</span>
                                                                        </td>
                                                                        <td align="left" style="width: 50%">
                                                                            <asp:TextBox ID="txtAML25" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                                                TabIndex="76"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 50%">
                                                                            <span class="elementLabel">26.</span>
                                                                        </td>
                                                                        <td align="left" style="width: 50%">
                                                                            <asp:TextBox ID="txtAML26" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                                                TabIndex="76"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 50%">
                                                                            <span class="elementLabel">27.</span>
                                                                        </td>
                                                                        <td align="left" style="width: 50%">
                                                                            <asp:TextBox ID="txtAML27" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                                                TabIndex="76"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 50%">
                                                                            <span class="elementLabel">28.</span>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtAML28" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                                                TabIndex="76"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 50%">
                                                                            <span class="elementLabel">29.</span>
                                                                        </td>
                                                                        <td align="left" style="width: 50%">
                                                                            <asp:TextBox ID="txtAML29" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                                                TabIndex="76"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" style="width: 50%">
                                                                            <span class="elementLabel">30.</span>
                                                                        </td>
                                                                        <td align="left" style="width: 50%">
                                                                            <asp:TextBox ID="txtAML30" Width="450px" runat="server" CssClass="textBox" MaxLength="50"
                                                                                TabIndex="76"></asp:TextBox>
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
                                                            ToolTip="Back to Document Details" TabIndex="106" OnClick="btnDocAccPrev_Click"
                                                            OnClientClick="return OnAMLPrevClickNew();" />&nbsp;&nbsp;&nbsp;
                                                        <asp:Button ID="btnAMLReport" runat="server" Text="View AML Report" CssClass="buttonDefault"
                                                            Width="150px" OnClientClick="return ViewAMLReport();"></asp:Button>
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
                                                        <%--<asp:TextBox ID="txtRejectReason" runat="server" CssClass="textBox" MaxLength="30"
                                                        TabIndex="35" Width="300px"></asp:TextBox>--%>
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
                                        </div>
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
