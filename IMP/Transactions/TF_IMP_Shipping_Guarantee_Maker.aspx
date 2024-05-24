<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_Shipping_Guarantee_Maker.aspx.cs"
    Inherits="IMP_Transactions_TF_IMP_Shipping_Guarantee_Maker" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <script src="../Scripts/TF_IMP_Shipping_Guarantee_Maker.js" type="text/javascript"></script>
    <script id="Save_script" language="javascript" type="text/javascript">
        $(document).ready(function () {
            // Configure to save every 5 seconds  
            window.setInterval(SaveUpdateData, 5000); //calling saveDraft function for every 5 seconds  
        });
        $(document).ready(function (e) {
            $("input").keypress(function (e) {
                var k;
                document.all ? k = e.keyCode : k = e.which;
                return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || k == 44 || k == 45 || k == 46 || k == 47 || (k >= 48 && k <= 57));
            });
        });
    </script>
    <style type="text/css">
        .textBox
        {
            text-transform: uppercase;
        }
        .panel
        {
            border: 1px solid #49A3FF;
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
                            <span class="pageLabel"><strong>Booking liability of Shipping Guarantee - Maker</strong></span>
                        </td>
                        <td align="right" style="width: 50%" valign="bottom">
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                OnClientClick="return OnBackClick();" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%;" valign="top" colspan="2">
                            <br />
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            <%-------------------------  hidden fields  --------------------------------%>
                            <input type="hidden" id="hdnMode" runat="server" />
                            <input type="hidden" id="hdnUserName" runat="server" />
                            <input type="hidden" id="hdnBranchCode" runat="server" />
                            <input type="hidden" id="hdnBranchName" runat="server" />
                            <input type="hidden" id="hdnCustAbbr" runat="server" />
                            <input type="hidden" id="hdnCustAccountNo" runat="server" />
                        </td>
                    </tr>
                </table>
                <table id="tbl_Ship_Guarantee" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="right" width="10%">
                            <span class="elementLabel">Trans. Ref. No :</span>
                        </td>
                        <td width="30%" align="left">
                            <asp:TextBox ID="txtDocNo" Width="100px" runat="server" CssClass="textBox" TabIndex="1"
                                Enabled="false"></asp:TextBox>
                            <span class="elementLabel">LC No :</span>
                            <asp:TextBox ID="txt_LC_No" Width="100px" runat="server" CssClass="textBox" TabIndex="1"
                                MaxLength="14" onkeyup="return Toggel_LC_RefNo();"></asp:TextBox>
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
                            <asp:DropDownList ID="ddlDoc_Curr" runat="server" CssClass="dropdownList" TabIndex="3" onchange="return ccyformat1();"
                                Width="70px">
                            </asp:DropDownList>
                            &nbsp;&nbsp; <span class="elementLabel">Bill Amt : </span>
                            <asp:TextBox ID="txt_Bill_Amt" runat="server" TabIndex="4" CssClass="textBox" Width="100px"
                                Style="text-align: right" onkeyDown="return validate_Number(event);" onchange="return ccyformat1();"
                                 MaxLength="16"></asp:TextBox>
                        </td>
                        <td align="right" width="10%">
                        </td>
                        <td align="left" width="20%">
                        </td>
                    </tr>
                </table>
                <table id="tbl_Ship_Panal" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top">
                            <ajaxToolkit:TabContainer ID="TabContainerMain" runat="server" ActiveTabIndex="0"
                                CssClass="ajax__tab_xp-theme">
                                <ajaxToolkit:TabPanel ID="tbSGRegister" runat="server" HeaderText="SG Register"
                                    Font-Bold="true" ForeColor="White">
                                    <ContentTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td align="right" style="width: 200px">
                                                    <span class="elementLabel">Beneficiary Name :</span>&nbsp;
                                                </td>
                                                <td align="left" style="width: 115px">
                                                    <asp:TextBox ID="txtbenename" runat="server" CssClass="textBox" MaxLength="50" TabIndex="5"
                                                        Width="410px" Height="12"></asp:TextBox>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 200px" valign="top">
                                                    <span class="elementLabel">Beneficiary Address :</span>&nbsp;
                                                </td>
                                                <td align="left" style="width: 115px">
                                                    <asp:TextBox ID="txtbeneadd1" runat="server" CssClass="textBox" Height="12" MaxLength="35"
                                                        TabIndex="6" Width="410px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 200px">
                                                    <span></span>
                                                </td>
                                                <td align="left" style="width: 115px">
                                                    <asp:TextBox ID="txtbeneadd2" runat="server" CssClass="textBox" Height="12" MaxLength="35"
                                                        TabIndex="6" Width="410px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 200px">
                                                    <span></span>
                                                </td>
                                                <td align="left" style="width: 115px">
                                                    <asp:TextBox ID="txtbeneadd3" runat="server" CssClass="textBox" Height="12" MaxLength="35"
                                                        TabIndex="6" Width="410px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 200px">
                                                    <span></span>
                                                </td>
                                                <td align="left" style="width: 115px">
                                                    <asp:TextBox ID="txtbeneadd4" runat="server" CssClass="textBox" Height="12" MaxLength="35"
                                                        TabIndex="6" Width="410px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                               <td align="right" style="width: 200px" ><span class="elementLabel">Applicant :</span>&nbsp;</td>
                                               <td align="left" style="width: 115px">
                                            <asp:TextBox ID="txtApplid" runat="server" CssClass="textBox" MaxLength="15" TabIndex="7" Width="100px" Height="12" 
                                             AutoPostBack="true" OnTextChanged="txtApplid_TextChanged"></asp:TextBox>
                                             <%--<asp:Button ID="btnApplName" runat="server" ToolTip="Press for Customers list."
                                                        CssClass="btnHelp_enabled" OnClientClick="return OpenCustomerCodeList('mouseClick');" />--%>
                                            <asp:Button ID="btnApplName" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" OnClientClick="return CustomerHelp();"/>
                                            <asp:Label ID="lblApplName" runat="server" Width="280px" CssClass="elementLabel"></asp:Label>
                                            <br />
                                        </td>
                                                <td align="left" class="auto-style1" valign="top">
                                                    <span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; LC Ref No :</span>
                                                    <asp:TextBox ID="txtlcrefno" runat="server" CssClass="textBox" MaxLength="14" TabIndex="9"
                                                        Width="200px" Height="12px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 200px" valign="top">
                                                    <span class="elementLabel">Applicant Address :</span>&nbsp;
                                                </td>
                                                <td align="left" style="width: 290px">
                                                    <asp:TextBox ID="txtApplAdd" runat="server" CssClass="textBox" Height="55px" MaxLength="100"
                                                        TabIndex="8" TextMode="MultiLine" Width="410px"></asp:TextBox>
                                                    <br />
                                                </td>
                                                <td align="left" class="auto-style1" valign="middle">
                                                    <span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; City:</span>
                                                    <asp:TextBox ID="txtApplCity" runat="server" CssClass="textBox" MaxLength="20" TabIndex="10"
                                                        Width="150px" Height="12px"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Pincode :</span>
                                                    <asp:TextBox ID="txtApplPincode" runat="server" CssClass="textBox" MaxLength="6"
                                                        TabIndex="10" Width="70px" Height="12px" onkeyDown="return validate_Number(event);"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 200px">
                                                    <span class="elementLabel">Shipping issued in favour of:</span>&nbsp;
                                                </td>
                                                <td align="left" style="width: 115px">
                                                    <asp:TextBox ID="txtShippingissued" runat="server" CssClass="textBox" MaxLength="50" TabIndex="10"
                                                        Width="410px" Height="12px"></asp:TextBox>
                                                </td>
                                                    <td align="left" class="auto-style1"><span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Country :</span>
                                                    <asp:TextBox ID="txtcountry" runat="server" CssClass="textBox" MaxLength="3" TabIndex="11"
                                                    Width="50px" Height="12px" onkeyup="return Uppercase('txtcountry')" AutoPostBack="true" OnTextChanged="txtcountry_TextChanged"></asp:TextBox>
                                                    <asp:Button ID="btncountry" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" OnClientClick="return Countryhelp();" />
                                                    <asp:Label ID="lblcountryname" runat="server" Width="200px" CssClass="elementLabel"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 200px">
                                                    <span class="elementLabel">Shipping Company Name :</span>&nbsp;
                                                </td>
                                                <td align="left" style="width: 115px">
                                                    <asp:TextBox ID="txtShipcompname" runat="server" CssClass="textBox" MaxLength="50"
                                                        TabIndex="16" Width="410px" Height="12px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 200px">
                                                    <span class="elementLabel">Vessel name 1 :</span>&nbsp;
                                                </td>
                                                <td align="left" style="width: 50px">
                                                    <asp:TextBox ID="txtVesselname1" runat="server" CssClass="textBox" MaxLength="10" TabIndex="17"
                                                        Width="120px" Height="12px"></asp:TextBox>
                                                    <%--</td> <td align="left" class="auto-style1">--%>
                                                    <span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Date :</span>
                                                    <asp:TextBox ID="txtVesseldate1" runat="server" CssClass="textBox" MaxLength="50"
                                                        TabIndex="18" Width="75px" Height="12px" ValidationGroup="dtVal"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MExairlinedate1" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtVesseldate1" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                        CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                        CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                        CultureTimePlaceholder=":">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btnCal_airlineDate1" runat="server" CssClass="btncalendar_enabled" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtVesseldate1" PopupButtonID="btnCal_airlineDate1" Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MExairlinedate1"
                                                        ValidationGroup="dtVal" ControlToValidate="txtVesseldate1" EmptyValueMessage="Enter Date Value"
                                                        InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                        Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 200px">
                                                    <span class="elementLabel">Vessel name 2 :</span>&nbsp;
                                                </td>
                                                <td align="left" style="width: 50px">
                                                    <asp:TextBox ID="txtVesselname2" runat="server" CssClass="textBox" MaxLength="10" TabIndex="19"
                                                        Width="120px" Height="12px"></asp:TextBox>
                                                    <%--    </td><td align="left" class="auto-style1">--%>
                                                    <span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Date :</span>
                                                    <asp:TextBox ID="txtVesseldate2" runat="server" CssClass="textBox" MaxLength="50"
                                                        TabIndex="20" Width="75px" Height="12px" ValidationGroup="dtVal"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MExairlinedate2" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txtVesseldate2" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                        CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                        CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                        CultureTimePlaceholder=":">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btnCal_airlineDate2" runat="server" CssClass="btncalendar_enabled" />
                                                    <ajaxToolkit:CalendarExtender ID="CEx_airlineDate2" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtVesseldate2" PopupButtonID="btnCal_airlineDate2" Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MExairlinedate2"
                                                        ValidationGroup="dtVal" ControlToValidate="txtVesseldate2" EmptyValueMessage="Enter Date Value"
                                                        InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                        Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 200px">
                                                    <span class="elementLabel">Shipper Name :</span>&nbsp;
                                                </td>
                                                <td align="left" style="width: 115px">
                                                    <asp:TextBox ID="txtshipper" runat="server" CssClass="textBox" MaxLength="50" TabIndex="21"
                                                        Width="410px" Height="12px"></asp:TextBox>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 200px">
                                                    <span class="elementLabel">Supplier Name :</span>&nbsp;
                                                </td>
                                                <td align="left" style="width: 115px">
                                                    <asp:TextBox ID="txtsupplier" runat="server" CssClass="textBox" MaxLength="50" TabIndex="22"
                                                        Width="410px" Height="12px"></asp:TextBox>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 200px">
                                                    <span class="elementLabel">Consignee Name :</span>&nbsp;
                                                </td>
                                                <td align="left" style="width: 115px">
                                                    <asp:TextBox ID="txtconsignee" runat="server" CssClass="textBox" MaxLength="50" TabIndex="23"
                                                        Width="190px" Text="MIZUHO BANK LTD" Height="12px" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 200px">
                                                    <span class="elementLabel">Notify Party :</span>&nbsp;
                                                </td>
                                                <td align="left" style="width: 115px">
                                                    <asp:TextBox ID="txtnotifyname" runat="server" CssClass="textBox" MaxLength="50"
                                                        TabIndex="24" Width="410px" Height="11px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 200px">
                                                    <span class="elementLabel">Description Of Goods :</span>&nbsp;
                                                </td>
                                                <td align="left" style="width: 115px">
                                                    <asp:TextBox ID="txtdescofgoods" runat="server" CssClass="textBox" MaxLength="50"
                                                        TabIndex="25" Width="410px" Height="12px"></asp:TextBox>
                                                </td>
                                                <td align="left" class="auto-style1">
                                                    <span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Quantity :</span>
                                                    <asp:TextBox ID="txtquantity" runat="server" CssClass="textBox" MaxLength="15" TabIndex="26"
                                                        Width="70px" Height="12px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 200px">
                                                    <span class="elementLabel">Shipping Marks :</span>&nbsp;
                                                </td>
                                                <td align="left" style="width: 115px">
                                                    <asp:TextBox ID="txtshipmarks" runat="server" CssClass="textBox" MaxLength="50" TabIndex="27"
                                                        Width="410px" Height="12px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 200px"><span class="elementLabel">Currency :</span>&nbsp; </td>
                                                <td align="left" style="width: 200px">
                                                <asp:TextBox ID="txtcurrency" runat="server" CssClass="textBox" MaxLength="3"  
                                                TabIndex="28" Width="50px" Height="12px" onchange="return ccyformat();"></asp:TextBox>
                                                <asp:Button ID="btncurrency" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" OnClientClick="return CurrencyHelp();"/>
                                                    <span class="elementLabel">Bill Amt :</span>
                                                    <asp:TextBox ID="txtbillamt" runat="server" Style="text-align: right" CssClass="textBox"
                                                        MaxLength="15" onkeyDown="return validate_Number(event);" onchange="return ccyformat();" TabIndex="28" Width="100px" Height="12px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 200px">
                                                    <span class="elementLabel">Commercial Invoice No :</span>&nbsp;
                                                </td>
                                                <td align="left" style="width: 200px">
                                                    <asp:TextBox ID="txt_Com_InvNo" runat="server" CssClass="textBox" MaxLength="50" TabIndex="28"
                                                        Width="300px" Height="12px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 200px">
                                                    <span class="elementLabel">Vessel Name :</span>&nbsp;
                                                </td>
                                                <td align="left" style="width: 200px">
                                                    <asp:TextBox ID="txtVesselname3" runat="server" CssClass="textBox" MaxLength="50" TabIndex="28"
                                                        Width="300px" Height="12px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 200px">
                                                    <span class="elementLabel">Bill No :</span>&nbsp;
                                                </td>
                                                <td align="left" style="width: 200px">
                                                    <asp:TextBox ID="txt_Bill_No" runat="server" CssClass="textBox" MaxLength="50" TabIndex="28"
                                                        Width="300px" Height="12px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 200px">
                                                    <span class="elementLabel">Remarks :</span>&nbsp;
                                                </td>
                                                <td align="left" style="width: 200px">
                                                    <asp:TextBox ID="txt_Remarks_Reg" runat="server" CssClass="textBox" MaxLength="50" TabIndex="28"
                                                        Width="300px" Height="12px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 200px">
                                                    <span class="elementLabel">Goods imported under Import Licence No.
                                                        <br />
                                                        Dated or Goods imported under OGL
                                                        <br />
                                                        Para Chapter of Exim Policy 2015-2020 :</span>&nbsp;
                                                </td>
                                                <td align="left" style="width: 115px">
                                                    <asp:TextBox ID="txtgoodspolicy" TextMode="MultiLine" runat="server" CssClass="textBox"
                                                        Text="Foreign Trade Policy 2015-2020" MaxLength="100" TabIndex="30" Height="55px"
                                                        Width="410px"></asp:TextBox>
                                                    <br />
                                                </td>
                                                <td align="left" class="auto-style1" valign="top">
                                                    <span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Our Reference No :</span>
                                                    <asp:TextBox ID="txtOurref" runat="server" Enabled="false" CssClass="textBox" MaxLength="14"
                                                        Height="12px" TabIndex="31" Width="150px"></asp:TextBox>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 200px">
                                                    <span class="elementLabel"></span>
                                                </td>
                                                <td align="left" style="width: 115px">
                                                    &nbsp;&nbsp;
                                                    <asp:CheckBox runat="server" ID="Chk_Ahm" Text="AHM" TabIndex="32" CssClass="elementLabel"
                                                        Checked="false"></asp:CheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 200px">
                                                    <span class="elementLabel"></span>
                                                </td>
                                                <td align="left" style="width: 115px">
                                                    <asp:Button ID="btnGO1_Next" runat="server" Text="Next >>" CssClass="buttonDefault"
                                                        ToolTip="Go to document details" TabIndex="33" OnClientClick="return OnDocNextClick(1);" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel ID="tbDocumentDetails" runat="server" HeaderText="DOCUMENT DETAILS"
                                    Font-Bold="true" ForeColor="White">
                                    <ContentTemplate>
                                        <table id="tbl_Ship_Doc" width="100%">
                                            <tr>
                                                <td align="right" width="20%">
                                                    <span class="elementLabel">Customer : </span>
                                                </td>
                                                <td align="left" width="20%">
                                                    <asp:TextBox ID="txt_Doc_Customer_ID" runat="server" CssClass="textBox" TabIndex="5"
                                                        onchange="return FillCustomerDetails();" MaxLength="20" Width="100px" Enabled="false"></asp:TextBox>
                                                    <asp:Button ID="btnCustomerList" runat="server" ToolTip="Press for Customers list."
                                                        CssClass="btnHelp_enabled" OnClientClick="return OpenCustomerCodeList('mouseClick');" />
                                                    <asp:Label ID="lblCustomerDesc" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">C. Code : </span>
                                                </td>
                                                <td align="left" width="50%">
                                                    <asp:TextBox ID="txt_CCode" runat="server" CssClass="textBox" TabIndex="6" MaxLength="12"
                                                        Width="100px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">Issuing Date :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_Issuing_Date" runat="server" TabIndex="7" CssClass="textBox"
                                                        MaxLength="10" ValidationGroup="dtVal" Width="70px" Enabled="false"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txt_Issuing_Date" ErrorTooltipEnabled="True"
                                                        CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                        CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                        CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btncal_Issuing_Date" runat="server" CssClass="btncalendar_enabled" />
                                                    <ajaxToolkit:CalendarExtender ID="CE_Accept_Date" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txt_Issuing_Date" PopupButtonID="btncal_Issuing_Date" Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">Expiry Date :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_Expiry_Date" runat="server" TabIndex="8" CssClass="textBox"
                                                        MaxLength="10" Width="70px" Enabled="false"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date"
                                                        runat="server" TargetControlID="txt_Expiry_Date" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                        CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                        CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                        CultureTimePlaceholder=":">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <asp:Button ID="btncal_Expiry_Date" runat="server" CssClass="btncalendar_enabled" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txt_Expiry_Date" PopupButtonID="btncal_Expiry_Date" Enabled="True">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">Revolving Information Option :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddl_revInfoOpt" CssClass="dropdownList" runat="server" TabIndex="8">
                                                        <asp:ListItem Text="1 - Non Revolving" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="2 - Monthly" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="3 - Instantly" Value="3"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="right">
                                                </td>
                                                <td align="left">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">Appl. No. Branch :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_ApplNoBranch" runat="server" TabIndex="10" CssClass="textBox"
                                                        MaxLength="7" Width="70px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">Advising Bank :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_AdvisingBank" runat="server" TabIndex="11" CssClass="textBox"
                                                        MaxLength="12" Width="100px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">Country Code :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlCountryCode" runat="server" CssClass="dropdownList" TabIndex="12"
                                                        Width="70px">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblCountryDesc" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">Commodity :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddl_Commodity" runat="server" CssClass="dropdownList" TabIndex="13"
                                                        Width="70px" OnSelectedIndexChanged="ddl_Commodity_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblCommodityDesc" runat="server" CssClass="elementLabel"></asp:Label>&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">Risk Country :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_RiskCountry" runat="server" TabIndex="15" CssClass="textBox"
                                                        MaxLength="2" Width="20px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">Risk Customer:</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_RiskCust" runat="server" TabIndex="16" CssClass="textBox" MaxLength="20"
                                                        Width="100px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">Grade Code :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_GradeCode" runat="server" TabIndex="17" CssClass="textBox" MaxLength="2"
                                                        Width="20px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">HO APL :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_HoApl" runat="server" TabIndex="18" CssClass="textBox" MaxLength="9"
                                                        Width="20px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">Remarks :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_Remarks" runat="server" TabIndex="18" CssClass="textBox" MaxLength="100"
                                                        Width="300px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">REM(EUC):</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_RemEUC" runat="server" TabIndex="18" CssClass="textBox" MaxLength="20"
                                                        Width="100px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <table id="tbl_Ship_Comm" width="100%">
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel"><b>COMMISSION DETAILS</b></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="20%">
                                                    <span class="elementLabel">Rate :</span>
                                                </td>
                                                <td align="left" width="10%">
                                                    <asp:TextBox ID="txt_Comm_Rate" runat="server" TabIndex="18" CssClass="textBox" Width="100px"
                                                        Style="text-align: right" onkeyDown="return validate_Number(event);" MaxLength="16"></asp:TextBox>
                                                    <span class="elementLabel">%</span>
                                                </td>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">Calc. Code:</span>
                                                </td>
                                                <td align="left" width="10%">
                                                    <asp:TextBox ID="txt_Comm_CalCode" runat="server" TabIndex="18" CssClass="textBox"
                                                        Width="100px" Style="text-align: right" onkeyDown="return validate_Number(event);"
                                                        MaxLength="16"></asp:TextBox>
                                                </td>
                                                <td align="right" width="10%">
                                                    <span class="elementLabel">Interval:</span>
                                                </td>
                                                <td align="left" width="40%">
                                                    <asp:TextBox ID="txt_Comm_Interval" runat="server" TabIndex="18" CssClass="textBox"
                                                        Width="100px" Style="text-align: right" onkeyDown="return validate_Number(event);"
                                                        MaxLength="16"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">Advance :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_Comm_Advance" runat="server" TabIndex="18" CssClass="textBox"
                                                        MaxLength="1" Width="20px"></asp:TextBox>
                                                    <span class="elementLabel">(Y/N)</span>
                                                </td>
                                                <td align="right">
                                                </td>
                                                <td align="left">
                                                </td>
                                                <td align="right">
                                                    <span class="elementLabel">End Inclu :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_Comm_EndInclu" runat="server" TabIndex="18" CssClass="textBox"
                                                        MaxLength="1" Width="20px"></asp:TextBox>
                                                    <span class="elementLabel">(Y/N)</span>
                                                </td>
                                            </tr>
                                        </table>
                                        <table id="tbl_ShipDebitCreditDetails" width="100%">
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel"><b>CREDITOR DETAILS</b></span>
                                                </td>
                                                <td align="left" colspan="3">
                                                </td>
                                                <td align="left">
                                                    <span class="elementLabel"><b>CCY</b></span>
                                                </td>
                                                <td align="left">
                                                    <span class="elementLabel"><b>AMOUNT</b></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">1 &nbsp;&nbsp;&nbsp;&nbsp;</span>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <span class="elementLabel">Opening / Confirming Charges :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_CreditOpenChrg_Curr" runat="server" TabIndex="18" CssClass="textBox"
                                                        MaxLength="3" Width="25px" onchange="return OnChange_CreditOpenChrg_Curr();"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_CreditOpenChrg" runat="server" TabIndex="18" CssClass="textBox"
                                                        Width="100px" Style="text-align: right" onkeyDown="return validate_Number(event);"
                                                        MaxLength="16" onchange="return OnChange_CreditOpenChrg();"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">2 &nbsp;&nbsp;&nbsp;&nbsp;</span>
                                                </td>
                                                <td align="left" colspan="3">
                                                    <span class="elementLabel">Cable / Mail Charges :</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_CreditMailChrgCurr" runat="server" TabIndex="18" CssClass="textBox"
                                                        MaxLength="3" onchange="return ccyformat2();" Width="25px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_CreditMailChrg" runat="server" TabIndex="18" CssClass="textBox"
                                                        Width="100px" Style="text-align: right" onkeyDown="return validate_Number(event);" 
                                                        onchange="return ccyformat2();" MaxLength="16"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">EXCH.Inf. CCY</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_CreditExchInfCurr" runat="server" TabIndex="18" CssClass="textBox"
                                                        MaxLength="3" Width="25px"></asp:TextBox>
                                                </td>
                                                <td align="left" colspan="2">
                                                    <span class="elementLabel">EX. Rate</span>
                                                    <asp:TextBox ID="txt_CreditExchRate" runat="server" TabIndex="18" CssClass="textBox"
                                                        Width="100px" Style="text-align: right" onkeyDown="return validate_Number(event);"
                                                        MaxLength="16"></asp:TextBox>
                                                </td>
                                                <td align="left" colspan="2">
                                                    <span class="elementLabel">ILT EX. Rate</span>
                                                    <asp:TextBox ID="txt_CreditILTExchRate" runat="server" TabIndex="18" CssClass="textBox"
                                                        Width="100px" Style="text-align: right" onkeyDown="return validate_Number(event);"
                                                        MaxLength="16"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="20%">
                                                    <span class="elementLabel"><b>DEBTOR CODE &nbsp;&nbsp;</b></span>
                                                </td>
                                                <td align="left" width="10%">
                                                    <span class="elementLabel"><b>A/C SHORT NAME</b></span>
                                                </td>
                                                <td align="left" width="10%">
                                                    <span class="elementLabel"><b>CUST. ABBR.</b></span>
                                                </td>
                                                <td align="left" width="10%">
                                                    <span class="elementLabel"><b>A/C NUMBER</b></span>
                                                </td>
                                                <td align="left" width="10%">
                                                    <span class="elementLabel"><b>CCY</b></span>
                                                </td>
                                                <td align="left" width="40%">
                                                    <span class="elementLabel"><b>AMOUNT</b></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">1 &nbsp;&nbsp;&nbsp;&nbsp;</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_DebitAcShortName" runat="server" TabIndex="18" CssClass="textBox"
                                                        MaxLength="40" Width="100px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_DebitCustAbbr" runat="server" TabIndex="18" CssClass="textBox"
                                                        MaxLength="12" Width="100px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_DebitAcNo" runat="server" TabIndex="18" CssClass="textBox" MaxLength="20"
                                                        Width="100px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_DebitCurr" runat="server" TabIndex="18" CssClass="textBox" MaxLength="3"
                                                        Width="25px" onchange="return ccyformat4();"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_DebitAmt" runat="server" TabIndex="18" CssClass="textBox" Width="100px"
                                                        Style="text-align: right" onkeyDown="return validate_Number(event);"
                                                        onchange="return ccyformat4();" MaxLength="16"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="elementLabel">2 &nbsp;&nbsp;&nbsp;&nbsp;</span>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_DebitAcShortName2" runat="server" TabIndex="18" CssClass="textBox"
                                                        MaxLength="40" Width="100px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_DebitCustAbbr2" runat="server" TabIndex="18" CssClass="textBox"
                                                        MaxLength="12" Width="100px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_DebitAcNo2" runat="server" TabIndex="18" CssClass="textBox"
                                                        MaxLength="20" Width="100px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_DebitCurr2" runat="server" TabIndex="18" CssClass="textBox"
                                                        MaxLength="3" Width="25px" onchange="return ccyformat5();" ></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txt_DebitAmt2" runat="server" TabIndex="18" CssClass="textBox" Width="100px"
                                                        Style="text-align: right" onkeyDown="return validate_Number(event);" 
                                                        onchange="return ccyformat5();" MaxLength="16"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="20%">
                                                </td>
                                                <td width="80%" align="left">
                                                <asp:Button ID="btnGO1_previous" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                                    ToolTip="Back to Document Details" TabIndex="117" OnClientClick="return OnDocNextClick(0);" />&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnDocNext" runat="server" Text="Next>>" CssClass="buttonDefault"
                                                        ToolTip="GO to General Operation" TabIndex="106" OnClientClick="return OnDocNextClick(2);" />
                                                </td>
                                            </tr>
                                        </table>
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
                                                                <asp:CheckBox ID="chk_GO1Flag" Text="GENERAL OPERATION I" runat="server" CssClass="elementLabel"
                                                                    OnCheckedChanged="chk_GO1Flag_OnCheckedChanged" AutoPostBack="true" />
                                                            </td>
                                                            <td align="center" width="50%">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="left" width="50%" style="border: 1px solid #49A3FF">
                                                                <table width="100%">
                                                                    <asp:Panel ID="Panel_GO1Left" runat="server" Visible="false">
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">COMMENT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Comment" runat="server" CssClass="textBox" TabIndex="81"
                                                                                    MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">Section No:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO1_Left_SectionNo" runat="server" CssClass="textBox" TabIndex="82"
                                                                                    MaxLength="2" Width="20px"></asp:TextBox>
                                                                                <span class="elementLabel">Remarks:</span>
                                                                                <asp:TextBox ID="txt_GO1_Left_Remarks" runat="server" CssClass="textBox" Width="300px"
                                                                                    MaxLength="30" TabIndex="82" onblur="return Toggel_GO1_Left_Remarks();"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">MEMO:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Memo" runat="server" CssClass="textBox" Width="50px"
                                                                                    TabIndex="82" MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">SCHEME No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Scheme_no" runat="server" CssClass="textBox" TabIndex="83"
                                                                                    MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" width="10%">
                                                                                <span class="elementLabel">DEBIT / CREDIT:</span>
                                                                            </td>
                                                                            <td align="left" width="24%">
                                                                                <asp:DropDownList ID="txt_GO1_Left_Debit_Code" runat="server" Width="70px" CssClass="dropdownList"
                                                                                    TabIndex="84" onchange="return TogggleDebitCreditCode('1','1');">
                                                                                    <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                                                    <asp:ListItem Value="D" Text="D"></asp:ListItem>
                                                                                    <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                &nbsp;
                                                                                <asp:Button ID="btn_GO1_Left_Debit_AccCode_help" runat="server" ToolTip="Press for GL Code list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenGO_help('mouseClick','GO1','Debit1');" />
                                                                                <asp:Button ID="btn_GO1_CR_Code_Left1_help" runat="server" ToolTip="Press for Sundry list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenCR_Code_help('mouseClick','GO1Left1','Credit');" />
                                                                                <asp:Button ID="btn_GO1_CR_Code_Left1_Custhelp" runat="server" ToolTip="Press for Cust Acc list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenSG_Cust_help('mouseClick','GO1Left1','Credit');" />
                                                                            </td>
                                                                            <td align="right" width="6%">
                                                                                <span class="elementLabel">CCY:</span>
                                                                            </td>
                                                                            <td align="left" width="34%">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Curr" runat="server" CssClass="textBox" TabIndex="84"
                                                                                    Width="25px" MaxLength="3" onchange="return txt_GO1_Left_Debit_Curr_Change();"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right" width="6%">
                                                                                <span class="elementLabel">AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left" width="20%">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Amt" runat="server" CssClass="textBox" Width="90px"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="84" Style="text-align: right"
                                                                                    MaxLength="16" onblur="return Toggel_Debit_Amt();" onchange="return GO1_Amt_Calculation();"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CUSTOMER:</span>
                                                                            </td>
                                                                            <td align="left" colspan="4">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Cust" runat="server" CssClass="textBox" TabIndex="85"
                                                                                    MaxLength="12" Width="100px"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Cust_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="85" MaxLength="40" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C CODE:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                                    TabIndex="86" MaxLength="5" Width="50px"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="86" MaxLength="40" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                                    TabIndex="86" MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" align="right">
                                                                                <span class="elementLabel">EXCH RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Exch_Rate" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="87" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" onchange="return txt_GO1_Left_Debit_Exch_Rate_Change();"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">EXCH CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Exch_CCY" runat="server" CssClass="textBox" MaxLength="3"
                                                                                    TabIndex="87" Width="25px" onchange="return txt_GO1_Left_Debit_Exch_CCY_Change();"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">FUND:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_FUND" runat="server" CssClass="textBox" TabIndex="88"
                                                                                    Width="20px" MaxLength="1"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CHECK No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Check_No" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    Width="50px" TabIndex="88" MaxLength="6"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AVAILABLE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Available" runat="server" CssClass="textBox"
                                                                                    TabIndex="88" MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ADVICE PRINT:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_AdPrint" runat="server" CssClass="textBox" TabIndex="89"
                                                                                    Width="20px" MaxLength="1"></asp:TextBox>
                                                                                <span class="elementLabel">DETAILS:</span>
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Details" runat="server" CssClass="textBox" TabIndex="89"
                                                                                    MaxLength="40" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ENTITY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="89" Width="90px" Style="text-align: right" MaxLength="3"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DIVISION:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Division" runat="server" CssClass="textBox" TabIndex="90"
                                                                                    MaxLength="2" Width="20px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Inter_Amount" onkeydown="return validate_Number(event);"
                                                                                    runat="server" Width="100px" MaxLength="16" Style="text-align: right" CssClass="textBox"
                                                                                    TabIndex="90"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Debit_Inter_Rate" runat="server" CssClass="textBox"
                                                                                    TabIndex="90" Width="100px" MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"></asp:TextBox>
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
                                                                                <asp:DropDownList ID="txt_GO1_Left_Credit_Code" runat="server" Width="70px" CssClass="dropdownList"
                                                                                    TabIndex="91" onchange="return TogggleDebitCreditCode('1','2');">
                                                                                    <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                                                    <asp:ListItem Value="D" Text="D"></asp:ListItem>
                                                                                    <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                &nbsp;
                                                                                <asp:Button ID="btn_GO1_Left_Credit_AccCode_help" runat="server" ToolTip="Press for GL Code list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenGO_help('mouseClick','GO1','Debit2');" />
                                                                                <asp:Button ID="btn_GO1_CR_Code_Left2_help" runat="server" ToolTip="Press for Sundry list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenCR_Code_help('mouseClick','GO1Left2','Credit');" />
                                                                                <asp:Button ID="btn_GO1_CR_Code_Left2_Custhelp" runat="server" ToolTip="Press for Cust Acc list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenSG_Cust_help('mouseClick','GO1Left2','Credit');" />
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Curr" runat="server" CssClass="textBox" TabIndex="91"
                                                                                    MaxLength="3" Width="25px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Amt" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="91" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CUSTOMER:</span>
                                                                            </td>
                                                                            <td align="left" colspan="4">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Cust" runat="server" CssClass="textBox" TabIndex="92"
                                                                                    MaxLength="12" Width="100px"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Cust_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="92" MaxLength="40" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C CODE:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                                    TabIndex="93" MaxLength="5" Width="50px"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="93" MaxLength="40" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                                    TabIndex="93" MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" align="right">
                                                                                <span class="elementLabel">EXCH RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Exch_Rate" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="94" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">EXCH CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Exch_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="94" Width="25px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">FUND:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_FUND" runat="server" CssClass="textBox" TabIndex="95"
                                                                                    MaxLength="1" Width="20px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CHECK No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Check_No" runat="server" CssClass="textBox"
                                                                                    TabIndex="95" onkeydown="return validate_Number(event);" Width="50px" MaxLength="6"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AVAILABLE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Available" runat="server" CssClass="textBox"
                                                                                    TabIndex="95" MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ADVICE PRINT:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_AdPrint" runat="server" CssClass="textBox" TabIndex="96"
                                                                                    Width="20px" MaxLength="1"></asp:TextBox>
                                                                                <span class="elementLabel">DETAILS:</span>
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Details" runat="server" CssClass="textBox" TabIndex="96"
                                                                                    Width="300px" MaxLength="30"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ENTITY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="96" Width="90px" Style="text-align: right" MaxLength="3"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DIVISION:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Division" runat="server" CssClass="textBox"
                                                                                    TabIndex="97" Width="20px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Inter_Amount" onkeydown="return validate_Number(event);"
                                                                                    runat="server" CssClass="textBox" TabIndex="97" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Left_Credit_Inter_Rate" onkeydown="return validate_Number(event);"
                                                                                    runat="server" CssClass="textBox" TabIndex="97" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </asp:Panel>
                                                                </table>
                                                            </td>
                                                            <td align="left" width="50%" style="border: 1px solid #49A3FF">
                                                                <table width="100%">
                                                                    <asp:Panel ID="Panel_GO1Right" runat="server" Visible="false">
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">COMMENT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Comment" runat="server" CssClass="textBox" TabIndex="101"
                                                                                    MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">Section No:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO1_Right_SectionNo" runat="server" CssClass="textBox" TabIndex="102"
                                                                                    MaxLength="2" Width="20px"></asp:TextBox>
                                                                                <span class="elementLabel">Remarks:</span>
                                                                                <asp:TextBox ID="txt_GO1_Right_Remarks" runat="server" CssClass="textBox" Width="300px"
                                                                                    MaxLength="30" TabIndex="102"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">MEMO:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Memo" runat="server" CssClass="textBox" Width="50px"
                                                                                    TabIndex="102" MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">SCHEME No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Scheme_no" runat="server" CssClass="textBox" TabIndex="103"
                                                                                    MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" width="10%">
                                                                                <span class="elementLabel">DEBIT / CREDIT:</span>
                                                                            </td>
                                                                            <td align="left" width="24%">
                                                                                <asp:DropDownList ID="txt_GO1_Right_Debit_Code" runat="server" Width="70px" CssClass="dropdownList"
                                                                                    TabIndex="104" onchange="return TogggleDebitCreditCode('1','3');">
                                                                                    <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                                                    <asp:ListItem Value="D" Text="D"></asp:ListItem>
                                                                                    <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                &nbsp;
                                                                                <asp:Button ID="btn_GO1_Right_Debit_AccCode_help" runat="server" ToolTip="Press for GL Code list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenGO_help('mouseClick','GO1','Debit3');" />
                                                                                <asp:Button ID="btn_GO1_CR_Code_Right1_help" runat="server" ToolTip="Press for Sundry list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenCR_Code_help('mouseClick','GO1Right1','Credit');" />
                                                                                <asp:Button ID="btn_GO1_CR_Code_Right1_Custhelp" runat="server" ToolTip="Press for Cust Acc list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenSG_Cust_help('mouseClick','GO1Right1','Credit');" />
                                                                            </td>
                                                                            <td align="right" width="6%">
                                                                                <span class="elementLabel">CCY:</span>
                                                                            </td>
                                                                            <td align="left" width="34%">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Curr" runat="server" CssClass="textBox" TabIndex="104"
                                                                                    Width="25px" MaxLength="3" onchange="return txt_GO1_Right_Debit_Curr_Change();"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right" width="6%">
                                                                                <span class="elementLabel">AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left" width="20%">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Amt" runat="server" CssClass="textBox" Width="90px"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="104" Style="text-align: right"
                                                                                    MaxLength="16" onblur="return Toggel_Debit_Amt();" onchange="return GO1_Right_Amt_Calculation();"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CUSTOMER:</span>
                                                                            </td>
                                                                            <td align="left" colspan="4">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Cust" runat="server" CssClass="textBox" TabIndex="105"
                                                                                    MaxLength="12" Width="100px"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Cust_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="105" MaxLength="40" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C CODE:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                                    TabIndex="105" MaxLength="5" Width="50px"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="105" MaxLength="40" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                                    TabIndex="105" MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" align="right">
                                                                                <span class="elementLabel">EXCH RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Exch_Rate" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="106" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" onchange="return txt_GO1_Right_Debit_Exch_Rate_Change();"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">EXCH CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Exch_CCY" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="106" Width="25px" onchange="return txt_GO1_Right_Debit_Exch_CCY_Change();"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">FUND:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_FUND" runat="server" CssClass="textBox" TabIndex="107"
                                                                                    Width="20px" MaxLength="1"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CHECK No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Check_No" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="107" MaxLength="6" Width="50px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AVAILABLE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Available" runat="server" CssClass="textBox"
                                                                                    TabIndex="107" MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ADVICE PRINT:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_AdPrint" runat="server" CssClass="textBox" TabIndex="108"
                                                                                    Width="20px" MaxLength="1"></asp:TextBox>
                                                                                <span class="elementLabel">DETAILS:</span>
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Details" runat="server" CssClass="textBox" TabIndex="108"
                                                                                    MaxLength="40" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ENTITY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="108" Width="90px" Style="text-align: right" MaxLength="3"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DIVISION:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Division" runat="server" CssClass="textBox"
                                                                                    TabIndex="109" MaxLength="2" Width="20px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Inter_Amount" onkeydown="return validate_Number(event);"
                                                                                    runat="server" Width="100px" MaxLength="16" Style="text-align: right" CssClass="textBox"
                                                                                    TabIndex="109"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Debit_Inter_Rate" runat="server" CssClass="textBox"
                                                                                    TabIndex="109" Width="100px" MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"></asp:TextBox>
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
                                                                                <asp:DropDownList ID="txt_GO1_Right_Credit_Code" runat="server" Width="70px" CssClass="dropdownList"
                                                                                    TabIndex="110" onchange="return TogggleDebitCreditCode('1','4');">
                                                                                    <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                                                    <asp:ListItem Value="D" Text="D"></asp:ListItem>
                                                                                    <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                &nbsp;
                                                                                <asp:Button ID="btn_GO1_Right_Credit_AccCode_Help" runat="server" ToolTip="Press for GL Code list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenGO_help('mouseClick','GO1','Debit4');" />
                                                                                <asp:Button ID="btn_GO1_CR_Code_Right2_help" runat="server" ToolTip="Press for Sundry list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenCR_Code_help('mouseClick','GO1Right2','Credit');" />
                                                                                <asp:Button ID="btn_GO1_CR_Code_Right2_Custhelp" runat="server" ToolTip="Press for Cust Acc list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenSG_Cust_help('mouseClick','GO1Right2','Credit');" />
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Curr" runat="server" CssClass="textBox" TabIndex="110"
                                                                                    MaxLength="3" Width="25px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Amt" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="110" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CUSTOMER:</span>
                                                                            </td>
                                                                            <td align="left" colspan="4">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Cust" runat="server" CssClass="textBox" TabIndex="111"
                                                                                    MaxLength="12" Width="100px"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Cust_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="111" MaxLength="40" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C CODE:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                                    TabIndex="112" MaxLength="5" Width="50px"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="112" MaxLength="40" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                                    TabIndex="112" MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" align="right">
                                                                                <span class="elementLabel">EXCH RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Exch_Rate" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="113" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">EXCH CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Exch_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="113" Width="25px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">FUND:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_FUND" runat="server" CssClass="textBox" TabIndex="114"
                                                                                    MaxLength="1" Width="20px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CHECK No: </span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Check_No" runat="server" CssClass="textBox"
                                                                                    TabIndex="114" onkeydown="return validate_Number(event);" Width="50px" MaxLength="6"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AVAILABLE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Available" runat="server" CssClass="textBox"
                                                                                    TabIndex="114" MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ADVICE PRINT:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_AdPrint" runat="server" CssClass="textBox"
                                                                                    TabIndex="115" Width="20px" MaxLength="1"></asp:TextBox>
                                                                                <span class="elementLabel">DETAILS:</span>
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Details" runat="server" CssClass="textBox"
                                                                                    TabIndex="115" Width="300px" MaxLength="30"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ENTITY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="115" Width="90px" Style="text-align: right" MaxLength="3"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DIVISION:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Division" runat="server" CssClass="textBox"
                                                                                    TabIndex="116" Width="20px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Inter_Amount" onkeydown="return validate_Number(event);"
                                                                                    runat="server" CssClass="textBox" TabIndex="116" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO1_Right_Credit_Inter_Rate" onkeydown="return validate_Number(event);"
                                                                                    runat="server" CssClass="textBox" TabIndex="116" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16"></asp:TextBox>
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
                                                                    ToolTip="Back to Document Details" TabIndex="117" OnClientClick="return OnDocNextClick(1);" />&nbsp;&nbsp;&nbsp;
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
                                                                <asp:CheckBox ID="chk_GO2Flag" Text="GENERAL OPERATION II" runat="server" CssClass="elementLabel"
                                                                    OnCheckedChanged="chk_GO2Flag_OnCheckedChanged" AutoPostBack="true" />
                                                            </td>
                                                            <td align="center" width="50%">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="left" width="50%" style="border: 1px solid #49A3FF">
                                                                <table width="100%">
                                                                    <asp:Panel ID="Panel_GO2Left" runat="server" Visible="false">
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">COMMENT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Comment" runat="server" CssClass="textBox" TabIndex="81"
                                                                                    MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">Section No:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO2_Left_SectionNo" runat="server" CssClass="textBox" TabIndex="82"
                                                                                    MaxLength="2" Width="20px"></asp:TextBox>
                                                                                <span class="elementLabel">Remarks:</span>
                                                                                <asp:TextBox ID="txt_GO2_Left_Remarks" runat="server" CssClass="textBox" Width="300px"
                                                                                    MaxLength="30" TabIndex="82"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">MEMO:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Memo" runat="server" CssClass="textBox" Width="50px"
                                                                                    TabIndex="82" MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">SCHEME No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Scheme_no" runat="server" CssClass="textBox" TabIndex="83"
                                                                                    MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" width="10%">
                                                                                <span class="elementLabel">DEBIT / CREDIT:</span>
                                                                            </td>
                                                                            <td align="left" width="24%">
                                                                                <asp:DropDownList ID="txt_GO2_Left_Debit_Code" runat="server" Width="70px" CssClass="dropdownList"
                                                                                    TabIndex="84" onchange="return TogggleDebitCreditCode('2','1');">
                                                                                    <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                                                    <asp:ListItem Value="D" Text="D"></asp:ListItem>
                                                                                    <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                &nbsp;
                                                                                <asp:Button ID="btn_GO2_Left_Debit_AccCode_Help" runat="server" ToolTip="Press for GL Code list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenGO_help('mouseClick','GO2','Debit1');" />
                                                                                <asp:Button ID="btn_GO2_CR_Code_Left1_help" runat="server" ToolTip="Press for Sundry list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenCR_Code_help('mouseClick','GO2Left1','Credit');" />
                                                                                <asp:Button ID="btn_GO2_CR_Code_Left1_Custhelp" runat="server" ToolTip="Press for Cust Acc list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenSG_Cust_help('mouseClick','GO2Left1','Credit');" />
                                                                            </td>
                                                                            <td align="right" width="6%">
                                                                                <span class="elementLabel">CCY:</span>
                                                                            </td>
                                                                            <td align="left" width="34%">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Curr" runat="server" CssClass="textBox" TabIndex="84"
                                                                                    Width="25px" MaxLength="3" onchange="return txt_GO2_Left_Debit_Curr_Change();"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right" width="6%">
                                                                                <span class="elementLabel">AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left" width="20%">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Amt" runat="server" CssClass="textBox" Width="90px"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="84" Style="text-align: right"
                                                                                    MaxLength="16" onblur="return Toggel_Debit_Amt();" onchange="return GO2_Amt_Calculation();"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CUSTOMER:</span>
                                                                            </td>
                                                                            <td align="left" colspan="4">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Cust" runat="server" CssClass="textBox" TabIndex="85"
                                                                                    MaxLength="12" Width="100px"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Cust_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="85" MaxLength="40" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C CODE:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                                    TabIndex="86" MaxLength="5" Width="50px"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="86" MaxLength="40" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                                    TabIndex="86" MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" align="right">
                                                                                <span class="elementLabel">EXCH RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Exch_Rate" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="87" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" onchange="return txt_GO2_Left_Debit_Exch_Rate_Change();"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">EXCH CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Exch_CCY" runat="server" CssClass="textBox" MaxLength="3"
                                                                                    TabIndex="87" Width="25px" onchange="return txt_GO2_Left_Debit_Exch_CCY_Change();"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">FUND:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_FUND" runat="server" CssClass="textBox" TabIndex="88"
                                                                                    Width="20px" MaxLength="1"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CHECK No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Check_No" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    Width="50px" TabIndex="88" MaxLength="6"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AVAILABLE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Available" runat="server" CssClass="textBox"
                                                                                    TabIndex="88" MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ADVICE PRINT:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_AdPrint" runat="server" CssClass="textBox" TabIndex="89"
                                                                                    Width="20px" MaxLength="1"></asp:TextBox>
                                                                                <span class="elementLabel">DETAILS:</span>
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Details" runat="server" CssClass="textBox" TabIndex="89"
                                                                                    MaxLength="40" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ENTITY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="89" Width="90px" Style="text-align: right" MaxLength="3"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DIVISION:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Division" runat="server" CssClass="textBox" TabIndex="90"
                                                                                    MaxLength="2" Width="20px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Inter_Amount" onkeydown="return validate_Number(event);"
                                                                                    runat="server" Width="100px" MaxLength="16" Style="text-align: right" CssClass="textBox"
                                                                                    TabIndex="90"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Debit_Inter_Rate" runat="server" CssClass="textBox"
                                                                                    TabIndex="90" Width="100px" MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"></asp:TextBox>
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
                                                                                <asp:DropDownList ID="txt_GO2_Left_Credit_Code" runat="server" Width="70px" CssClass="dropdownList"
                                                                                    TabIndex="91" onchange="return TogggleDebitCreditCode('2','2');">
                                                                                    <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                                                    <asp:ListItem Value="D" Text="D"></asp:ListItem>
                                                                                    <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                &nbsp;
                                                                                <asp:Button ID="btn_GO2_Left_Credit_AccCode_help" runat="server" ToolTip="Press for GL Code list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenGO_help('mouseClick','GO2','Debit2');" />
                                                                                <asp:Button ID="btn_GO2_CR_Code_Left2_help" runat="server" ToolTip="Press for Sundry list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenCR_Code_help('mouseClick','GO2Left2','Credit');" />
                                                                                <asp:Button ID="btn_GO2_CR_Code_Left2_Custhelp" runat="server" ToolTip="Press for Cust Acc list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenSG_Cust_help('mouseClick','GO2Left2','Credit');" />
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Curr" runat="server" CssClass="textBox" TabIndex="91"
                                                                                    MaxLength="3" Width="25px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Amt" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="91" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CUSTOMER:</span>
                                                                            </td>
                                                                            <td align="left" colspan="4">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Cust" runat="server" CssClass="textBox" TabIndex="92"
                                                                                    MaxLength="12" Width="100px"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Cust_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="92" MaxLength="40" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C CODE:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                                    TabIndex="93" MaxLength="5" Width="50px"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="93" MaxLength="40" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                                    TabIndex="93" MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" align="right">
                                                                                <span class="elementLabel">EXCH RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Exch_Rate" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="94" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">EXCH CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Exch_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="94" Width="25px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">FUND:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_FUND" runat="server" CssClass="textBox" TabIndex="95"
                                                                                    MaxLength="1" Width="20px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CHECK No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Check_No" runat="server" CssClass="textBox"
                                                                                    TabIndex="95" onkeydown="return validate_Number(event);" Width="50px" MaxLength="6"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AVAILABLE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Available" runat="server" CssClass="textBox"
                                                                                    TabIndex="95" MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ADVICE PRINT:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_AdPrint" runat="server" CssClass="textBox" TabIndex="96"
                                                                                    Width="20px" MaxLength="1"></asp:TextBox>
                                                                                <span class="elementLabel">DETAILS:</span>
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Details" runat="server" CssClass="textBox" TabIndex="96"
                                                                                    Width="300px" MaxLength="30"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ENTITY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="96" Width="90px" Style="text-align: right" MaxLength="3"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DIVISION:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Division" runat="server" CssClass="textBox"
                                                                                    TabIndex="97" Width="20px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Inter_Amount" onkeydown="return validate_Number(event);"
                                                                                    runat="server" CssClass="textBox" TabIndex="97" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Left_Credit_Inter_Rate" onkeydown="return validate_Number(event);"
                                                                                    runat="server" CssClass="textBox" TabIndex="97" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </asp:Panel>
                                                                </table>
                                                            </td>
                                                            <td align="left" width="50%" style="border: 1px solid #49A3FF">
                                                                <table width="100%">
                                                                    <asp:Panel ID="Panel_GO2Right" runat="server" Visible="false">
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">COMMENT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Comment" runat="server" CssClass="textBox" TabIndex="101"
                                                                                    MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">Section No:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO2_Right_SectionNo" runat="server" CssClass="textBox" TabIndex="102"
                                                                                    MaxLength="2" Width="20px"></asp:TextBox>
                                                                                <span class="elementLabel">Remarks:</span>
                                                                                <asp:TextBox ID="txt_GO2_Right_Remarks" runat="server" CssClass="textBox" Width="300px"
                                                                                    MaxLength="30" TabIndex="102"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">MEMO:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Memo" runat="server" CssClass="textBox" Width="50px"
                                                                                    TabIndex="102" MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">SCHEME No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Scheme_no" runat="server" CssClass="textBox" TabIndex="103"
                                                                                    MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right" width="10%">
                                                                                <span class="elementLabel">DEBIT / CREDIT:</span>
                                                                            </td>
                                                                            <td align="left" width="24%">
                                                                                <asp:DropDownList ID="txt_GO2_Right_Debit_Code" runat="server" Width="70px" CssClass="dropdownList"
                                                                                    TabIndex="104" onchange="return TogggleDebitCreditCode('2','3');">
                                                                                    <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                                                    <asp:ListItem Value="D" Text="D"></asp:ListItem>
                                                                                    <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                &nbsp;
                                                                                <asp:Button ID="btn_GO2_Right_Debit_AccCode_help" runat="server" ToolTip="Press for GL Code list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenGO_help('mouseClick','GO2','Debit3');" />
                                                                                <asp:Button ID="btn_GO2_CR_Code_Right1_help" runat="server" ToolTip="Press for Sundry list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenCR_Code_help('mouseClick','GO2Right1','Credit');" />
                                                                                <asp:Button ID="btn_GO2_CR_Code_Right1_Custhelp" runat="server" ToolTip="Press for Cust Acc list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenSG_Cust_help('mouseClick','GO2Right1','Credit');" />
                                                                            </td>
                                                                            <td align="right" width="6%">
                                                                                <span class="elementLabel">CCY:</span>
                                                                            </td>
                                                                            <td align="left" width="34%">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Curr" runat="server" CssClass="textBox" TabIndex="104"
                                                                                    Width="25px" MaxLength="3" onchange="return txt_GO2_Right_Debit_Curr_Change();"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right" width="6%">
                                                                                <span class="elementLabel">AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left" width="20%">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Amt" runat="server" CssClass="textBox" Width="90px"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="104" Style="text-align: right"
                                                                                    MaxLength="16" onblur="return Toggel_Debit_Amt();" onchange="return GO2_Right_Amt_Calculation();"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CUSTOMER:</span>
                                                                            </td>
                                                                            <td align="left" colspan="4">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Cust" runat="server" CssClass="textBox" TabIndex="105"
                                                                                    MaxLength="12" Width="100px"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Cust_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="105" MaxLength="40" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C CODE:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                                    TabIndex="105" MaxLength="5" Width="50px"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="105" MaxLength="40" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                                    TabIndex="105" MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" align="right">
                                                                                <span class="elementLabel">EXCH RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Exch_Rate" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="106" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16" onchange="return txt_GO2_Right_Debit_Exch_Rate_Change();"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">EXCH CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Exch_CCY" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="106" Width="25px" onchange="return txt_GO2_Right_Debit_Exch_CCY_Change();"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">FUND:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_FUND" runat="server" CssClass="textBox" TabIndex="107"
                                                                                    Width="20px" MaxLength="1"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CHECK No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Check_No" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="107" MaxLength="6" Width="50px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AVAILABLE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Available" runat="server" CssClass="textBox"
                                                                                    TabIndex="107" MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ADVICE PRINT:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_AdPrint" runat="server" CssClass="textBox" TabIndex="108"
                                                                                    Width="20px" MaxLength="1"></asp:TextBox>
                                                                                <span class="elementLabel">DETAILS:</span>
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Details" runat="server" CssClass="textBox" TabIndex="108"
                                                                                    MaxLength="40" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ENTITY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="108" Width="90px" Style="text-align: right" MaxLength="3"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DIVISION:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Division" runat="server" CssClass="textBox"
                                                                                    TabIndex="109" MaxLength="2" Width="20px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Inter_Amount" onkeydown="return validate_Number(event);"
                                                                                    runat="server" Width="100px" MaxLength="16" Style="text-align: right" CssClass="textBox"
                                                                                    TabIndex="109"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Debit_Inter_Rate" runat="server" CssClass="textBox"
                                                                                    TabIndex="109" Width="100px" MaxLength="16" Style="text-align: right" onkeydown="return validate_Number(event);"></asp:TextBox>
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
                                                                                <asp:DropDownList ID="txt_GO2_Right_Credit_Code" runat="server" Width="70px" CssClass="dropdownList"
                                                                                    TabIndex="110" onchange="return TogggleDebitCreditCode('2','4');">
                                                                                    <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                                                                                    <asp:ListItem Value="D" Text="D"></asp:ListItem>
                                                                                    <asp:ListItem Value="C" Text="C"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                &nbsp;
                                                                                <asp:Button ID="btn_GO2_Right_Credit_AccCode_Help" runat="server" ToolTip="Press for GL Code list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenGO_help('mouseClick','GO2','Debit4');" />
                                                                                <asp:Button ID="btn_GO2_CR_Code_Right2_help" runat="server" ToolTip="Press for Sundry list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenCR_Code_help('mouseClick','GO2Right2','Credit');" />
                                                                                <asp:Button ID="btn_GO2_CR_Code_Right2_Custhelp" runat="server" ToolTip="Press for Cust Acc list."
                                                                                    CssClass="btnHelp_enabled" OnClientClick="return OpenSG_Cust_help('mouseClick','GO2Right2','Credit');" />
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Curr" runat="server" CssClass="textBox" TabIndex="110"
                                                                                    MaxLength="3" Width="25px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Amt" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="110" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CUSTOMER:</span>
                                                                            </td>
                                                                            <td align="left" colspan="4">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Cust" runat="server" CssClass="textBox" TabIndex="111"
                                                                                    MaxLength="12" Width="100px"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Cust_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="111" MaxLength="40" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C CODE:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Cust_AcCode" runat="server" CssClass="textBox"
                                                                                    TabIndex="112" MaxLength="5" Width="50px"></asp:TextBox>
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Cust_AcCode_Name" runat="server" CssClass="textBox"
                                                                                    TabIndex="112" MaxLength="40" Width="300px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">A/C No:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Cust_AccNo" runat="server" CssClass="textBox"
                                                                                    TabIndex="112" MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3" align="right">
                                                                                <span class="elementLabel">EXCH RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Exch_Rate" runat="server" CssClass="textBox"
                                                                                    onkeydown="return validate_Number(event);" TabIndex="113" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">EXCH CCY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Exch_Curr" runat="server" CssClass="textBox"
                                                                                    MaxLength="3" TabIndex="113" Width="25px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">FUND:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_FUND" runat="server" CssClass="textBox" TabIndex="114"
                                                                                    MaxLength="1" Width="20px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">CHECK No: </span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Check_No" runat="server" CssClass="textBox"
                                                                                    TabIndex="114" onkeydown="return validate_Number(event);" Width="50px" MaxLength="6"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">AVAILABLE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Available" runat="server" CssClass="textBox"
                                                                                    TabIndex="114" MaxLength="20"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ADVICE PRINT:</span>
                                                                            </td>
                                                                            <td align="left" colspan="3">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_AdPrint" runat="server" CssClass="textBox"
                                                                                    TabIndex="115" Width="20px" MaxLength="1"></asp:TextBox>
                                                                                <span class="elementLabel">DETAILS:</span>
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Details" runat="server" CssClass="textBox"
                                                                                    TabIndex="115" Width="300px" MaxLength="30"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">ENTITY:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                                    TabIndex="115" Width="90px" Style="text-align: right" MaxLength="3"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">
                                                                                <span class="elementLabel">DIVISION:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Division" runat="server" CssClass="textBox"
                                                                                    TabIndex="116" Width="20px"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-AMOUNT:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Inter_Amount" onkeydown="return validate_Number(event);"
                                                                                    runat="server" CssClass="textBox" TabIndex="116" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16"></asp:TextBox>
                                                                            </td>
                                                                            <td align="right">
                                                                                <span class="elementLabel">INTER-RATE:</span>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txt_GO2_Right_Credit_Inter_Rate" onkeydown="return validate_Number(event);"
                                                                                    runat="server" CssClass="textBox" TabIndex="116" Width="100px" Style="text-align: right"
                                                                                    MaxLength="16"></asp:TextBox>
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
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </ajaxToolkit:TabPanel>
                                        </ajaxToolkit:TabContainer>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                            </ajaxToolkit:TabContainer>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td align="center" width="100%">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit to Checker" CssClass="buttonDefault"
                                Width="150px" ToolTip="Submit to Checker" TabIndex="256" OnClick="btnSubmit_Click"
                                OnClientClick="return SubmitCheck();" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
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
