<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_BRO.aspx.cs" Inherits="IMP_Transactions_TF_IMP_BRO" %>

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
    <script src="../../Scripts/jquery-1.8.3.min.js" type="text/javascript""></script>
    <script src="../../Help_Plugins/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <link href="../../Help_Plugins/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../../Help_Plugins/MyJquery1.js" type="text/javascript"></script>
    <script src="../Scripts/TF_IMP_BRO_MAKER.js" type="text/javascript"></script>
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
      <div style="width: 100%">
            <div id="Div1" class="AlertJqueryHide">
                <p id="P1">
                </p>
            </div>
        </div>
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
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel"><strong>&nbsp; Bank Release Order (BRO)- Maker</strong></span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                    TabIndex="34"  OnClientClick="return OnBackClick();" />
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
                                <%-----------------Hidden Fields--------------------------%>
                                 <input type="hidden" id="hdnBranchCode" runat="server" />
                                 <input type="hidden" id="hdnBranchName" runat="server" />
                                 <input type="hidden" id="hdnUserName" runat="server" />
                            </td>
                        </tr>
                        </table>
                        <table id="tbl_Acceptance" cellspacing="0" border="0" width="100%">
                        <tr>
                          <td width="40%" align="center" valign="top" style="padding-right:300px">
                            <span class="elementLabel">Val.Date :</span>
                            <asp:TextBox ID="txtValueDate" runat="server" TabIndex="1" CssClass="textBox" MaxLength="10"
                                ValidationGroup="dtVal" Width="85px"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="ME_Value_Date" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtValueDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                CultureTimePlaceholder=":">
                            </ajaxToolkit:MaskedEditExtender>
                        </td>
                        </tr>
                        <tr>
                         <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="5">
                                <ajaxToolkit:TabContainer ID="TabContainerMain" runat="server" ActiveTabIndex="0"
                                    CssClass="ajax__tab_xp-theme">
                                     <ajaxToolkit:TabPanel ID="tbBRODetails" runat="server" HeaderText="BRO Document"
                                        Font-Bold="true" ForeColor="White">
                                   <ContentTemplate>
                                     <table width="100%">
                                     <tr>
                                        <td align="right"><span class="elementLabel">Branch :</span>&nbsp; </td>
                                        <td align="left" style="width: 290px">
                                            <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true" CssClass="dropdownList" Width="120px" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged1">
                                         </asp:DropDownList>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="right" class="auto-style1"><span class="elementLabel">Delivery Order Serial No :</span>&nbsp; </td>
                                        <td align="left" style="width: 150px">
                                            <asp:TextBox ID="txtdosrno" runat="server" ReadOnly="true" CssClass="textBox" MaxLength="3" TabIndex="1" Width="35px" Height="12px" Text="BRO"></asp:TextBox>
                                            &nbsp;<asp:TextBox ID="txtdosrno1" runat="server" ReadOnly="true" CssClass="textBox" MaxLength="3" TabIndex="2" Width="30px" Height="12px" ></asp:TextBox>
                                            &nbsp;<asp:TextBox ID="txtdosrno2" runat="server" ReadOnly="true" CssClass="textBox" MaxLength="6" TabIndex="3" Width="60px" Height="12px" ></asp:TextBox>
                                            <br />
                                        </td>
                                        <td align="left" class="auto-style1"><span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Date :</span>
                                        
                                            <asp:TextBox ID="txtdate" runat="server" CssClass="textBox" MaxLength="10" TabIndex="4" Width="85px" Height="12px" ValidationGroup="dtVal"></asp:TextBox>
                                             <ajaxToolkit:MaskedEditExtender ID="ME_DDate" Mask="99/99/9999" MaskType="Date"
                                            runat="server" TargetControlID="txtdate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                            CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                            CultureTimePlaceholder=":">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <asp:Button ID="btnCal_Date" runat="server" CssClass="btncalendar_enabled" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtdate" PopupButtonID="btnCal_Date" Enabled="True">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="ME_DDate"
                                            ValidationGroup="dtVal" ControlToValidate="txtdate" EmptyValueMessage="Enter Date Value"
                                            InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                            Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="right" style="width: 200px" ><span class="elementLabel">Shipping Co Name :</span>&nbsp;</td>
                                        <td align="left" style="width: 115px">
                                            <asp:TextBox ID="txtshipname" runat="server" CssClass="textBox" MaxLength="50" TabIndex="5" Width="410px" Height="12"></asp:TextBox>
                                            <br />
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="right" style="width: 200px"  valign="top"><span class="elementLabel">
                                            Shipping Co Address :</span>&nbsp; </td>
                                        <td align="left" style="width: 115px">
                                            <asp:TextBox ID="txtshipcoadd" runat="server" CssClass="textBox" Height="12" MaxLength="35" TabIndex="6" Width="410px"></asp:TextBox>
                                        </td>
                                   </tr>
                                   <tr>
                                   <td align="right" style="width: 200px"> <span></span> </td>
                                   <td align="left" style="width: 115px">
                                   <asp:TextBox ID="txtshipcoadd1" runat="server" CssClass="textBox" Height="12" MaxLength="35" TabIndex="6" Width="410px"></asp:TextBox>
                                   </td>
                                   </tr>

                                   <tr>
                                   <td align="right" style="width: 200px"> <span></span> </td>
                                   <td align="left" style="width: 115px">
                                   <asp:TextBox ID="txtshipcoadd2" runat="server" CssClass="textBox" Height="12" MaxLength="35" TabIndex="6" Width="410px"></asp:TextBox>
                                   </td>
                                   </tr>

                                   <tr>
                                   <td align="right" style="width: 200px"> <span></span> </td>
                                   <td align="left" style="width: 115px">
                                   <asp:TextBox ID="txtshipcoadd3" runat="server" CssClass="textBox" Height="12" MaxLength="35" TabIndex="6" Width="410px"></asp:TextBox>
                                   </td>
                                   </tr>
                                     <tr>
                                        <td align="right" style="width: 200px" ><span class="elementLabel">Applicant :</span>&nbsp;</td>
                                        <td align="left" style="width: 115px">
                                            <asp:TextBox ID="txtApplid" runat="server" CssClass="textBox" MaxLength="15" TabIndex="7" Width="100px" Height="12" 
                                                 AutoPostBack="true" OnTextChanged="txtApplid_TextChanged"></asp:TextBox>
                                            <asp:Button ID="btnApplName" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                            <asp:Label ID="lblApplName" runat="server" Width="280px" CssClass="elementLabel"></asp:Label>
                                            <br />
                                        </td>
                                        <td align="left" class="auto-style1" valign="top"><span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; LC Ref No :</span>
                                           <asp:TextBox ID="txtlcrefno" runat="server" CssClass="textBox" MaxLength="14" TabIndex="9" Width="200px" Height="12px"></asp:TextBox>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="right" style="width: 200px"  valign="top"><span class="elementLabel">
                                            Applicant Address :</span>&nbsp;</td>
                                        <td align="left" style="width: 290px">
                                            <asp:TextBox ID="txtApplAdd" runat="server" CssClass="textBox" Height="55px" MaxLength="100" TabIndex="8" TextMode="MultiLine" Width="410px"></asp:TextBox>
                                            <br />
                                        </td>
                                        <td align="left" class="auto-style1" valign="middle">
                                               <span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; City:</span>
                                               <asp:TextBox ID="txtApplCity" runat="server" CssClass="textBox" MaxLength="20" TabIndex="10" Width="150px" Height="12px"></asp:TextBox>
                                               <br />
                                               <br />
                                               <span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Pincode :</span>
                                               <asp:TextBox ID="txtApplPincode" runat="server" CssClass="textBox" MaxLength="6" TabIndex="10" Width="70px" Height="12px"></asp:TextBox>
                                         </td>
                                       </tr>
                                   
                                     <tr>
                                        <td align="right" style="width: 200px"><span class="elementLabel">Beneficiary Name :</span>&nbsp;</td>
                                        <td align="left" style="width: 115px">
                                            <asp:TextBox ID="txtbenefname" runat="server" CssClass="textBox" MaxLength="50" TabIndex="10" Width="410px" Height="12px"></asp:TextBox>
                                           
                                        </td>
                                          <td align="left" class="auto-style1"><span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Country :</span>
                                         <asp:TextBox ID="txtcountry" runat="server" CssClass="textBox" MaxLength="3" TabIndex="11"
                                              Width="50px" Height="12px" onkeyup="return Uppercase('txtcountry')" AutoPostBack="true" OnTextChanged="txtcountry_TextChanged"></asp:TextBox>
                                          <asp:Button ID="btncountry" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                          <asp:Label ID="lblcountryname" runat="server" Width="200px" CssClass="elementLabel"></asp:Label>
                                        </td>
                                    </tr>
                                     <tr>
                                         <td align="right" style="width: 200px"><span class="elementLabel">Airways Bill No 1 :</span>&nbsp; </td>
                                        <td align="left" style="width: 150px">
                                            <asp:TextBox ID="txtbill1" runat="server" CssClass="textBox" MaxLength="30" TabIndex="12" Width="300px" Height="12px" Text="MAWB "></asp:TextBox>
                                            </td>
                                            <td>
                                            <span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Date :</span>
                                            <asp:TextBox ID="txtbilldate" runat="server" CssClass="textBox" MaxLength="50" TabIndex="13" Width="75px" Height="12px" ValidationGroup="dtVal"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender ID="ME_airwaysDate" Mask="99/99/9999" MaskType="Date"
                                            runat="server" TargetControlID="txtbilldate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                            CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                            CultureTimePlaceholder=":">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <asp:Button ID="btnCal_airwaysDate" runat="server" CssClass="btncalendar_enabled" />
                                            <ajaxToolkit:CalendarExtender ID="CE_airwaysDate" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtbilldate" PopupButtonID="btnCal_airwaysDate" Enabled="True">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="MV_airwaysDate" runat="server" ControlExtender="ME_airwaysDate"
                                            ValidationGroup="dtVal" ControlToValidate="txtbilldate" EmptyValueMessage="Enter Date Value"
                                            InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                            Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                            <br />
                                       </td>
                                    </tr>
                                     <tr>
                                        <td align="right" style="width: 200px"><span class="elementLabel">Airways Bill No 2 :</span>&nbsp; </td>
                                        <td align="left" style="width: 150px">
                                            <asp:TextBox ID="txtbill2" runat="server" CssClass="textBox" MaxLength="30" TabIndex="14" Width="300px" Height="12px" Text="HAWB "></asp:TextBox>
                                            </td>
                                            <td>
                                            <span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Date :</span>
                                            <asp:TextBox ID="txtbilldate2" runat="server" CssClass="textBox" MaxLength="50" TabIndex="15" Width="75px" Height="12px" ValidationGroup="dtVal"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" Mask="99/99/9999" MaskType="Date"
                                            runat="server" TargetControlID="txtbilldate2" ErrorTooltipEnabled="True" CultureName="en-GB"
                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                            CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                            CultureTimePlaceholder=":">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <asp:Button ID="btnCal_airwaysDate1" runat="server" CssClass="btncalendar_enabled" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtbilldate2" PopupButtonID="btnCal_airwaysDate1" Enabled="True">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender3"
                                            ValidationGroup="dtVal" ControlToValidate="txtbilldate2" EmptyValueMessage="Enter Date Value"
                                            InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                            Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                            <br />
                                       </td>  
                                    </tr>
                                     <tr>
                                        <td align="right" style="width: 200px"> <span class="elementLabel">
                                            Airlines Company Name :</span>&nbsp; </td>
                                        <td align="left" style="width: 115px">
                                           
                                            <asp:TextBox ID="txtAircompname" runat="server" CssClass="textBox" MaxLength="50" TabIndex="16" Width="410px" Height="12px"></asp:TextBox>
                                           
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="right" style="width: 200px"><span class="elementLabel">
                                            Flight No 1 :</span>&nbsp;</td>
                                        <td align="left" style="width: 50px">
                                            
                                            <asp:TextBox ID="txtflightno1" runat="server" CssClass="textBox" MaxLength="10" TabIndex="17" Width="120px" Height="12px"></asp:TextBox>
                                           
                                        <%--</td> <td align="left" class="auto-style1">--%>
                                       <span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Date :</span> 
                                            <asp:TextBox ID="txtairlinedate1" runat="server" CssClass="textBox" MaxLength="50" TabIndex="18" Width="75px" Height="12px" ValidationGroup="dtVal"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                            runat="server" TargetControlID="txtairlinedate1" ErrorTooltipEnabled="True" CultureName="en-GB"
                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                            CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                            CultureTimePlaceholder=":">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <asp:Button ID="btnCal_airlineDate1" runat="server" CssClass="btncalendar_enabled" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtairlinedate1" PopupButtonID="btnCal_airlineDate1" Enabled="True">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender1"
                                            ValidationGroup="dtVal" ControlToValidate="txtairlinedate1" EmptyValueMessage="Enter Date Value"
                                            InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                            Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="right" style="width: 200px"><span class="elementLabel">
                                            Flight No 2 :</span>&nbsp;</td>
                                        <td align="left" style="width: 50px">
                                            
                                            <asp:TextBox ID="txtflightno2" runat="server" CssClass="textBox" MaxLength="10" TabIndex="19" Width="120px" Height="12px"></asp:TextBox>
                                           
                                    <%--    </td><td align="left" class="auto-style1">--%>
                                        <span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Date :</span> 
                                            <asp:TextBox ID="txtairlinedate2" runat="server" CssClass="textBox" MaxLength="50" TabIndex="20" Width="75px" Height="12px" ValidationGroup="dtVal"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date"
                                            runat="server" TargetControlID="txtairlinedate2" ErrorTooltipEnabled="True" CultureName="en-GB"
                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                            CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                            CultureTimePlaceholder=":">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <asp:Button ID="btnCal_airlineDate2" runat="server" CssClass="btncalendar_enabled" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtairlinedate2" PopupButtonID="btnCal_airlineDate2" Enabled="True">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender1"
                                            ValidationGroup="dtVal" ControlToValidate="txtairlinedate2" EmptyValueMessage="Enter Date Value"
                                            InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                            Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="right" style="width: 200px"><span class="elementLabel">Shipper Name :</span>&nbsp; </td>
                                        <td align="left" style="width: 115px">
                                            <asp:TextBox ID="txtshipper" runat="server" CssClass="textBox" MaxLength="50" TabIndex="21" Width="410px" Height="12px"></asp:TextBox>
                                            <br />
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="right" style="width: 200px"><span class="elementLabel">Supplier Name :</span>&nbsp; </td>
                                        <td align="left" style="width: 115px">
                                            <asp:TextBox ID="txtsupplier" runat="server" CssClass="textBox" MaxLength="50" TabIndex="22" Width="410px" Height="12px"></asp:TextBox>
                                            <br />
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="right" style="width: 200px"><span class="elementLabel">Consignee Name :</span>&nbsp;</td>
                                        <td align="left" style="width: 115px">
                                            <asp:TextBox ID="txtconsignee" runat="server" CssClass="textBox" MaxLength="50" TabIndex="23" 
                                            Width="190px" Text="MIZUHO BANK LTD"  Height="12px" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="right" style="width: 200px"><span class="elementLabel">Notify Party :</span>&nbsp; </td>
                                        <td align="left" style="width: 115px">
                                            <asp:TextBox ID="txtnotifyname" runat="server" CssClass="textBox" MaxLength="50" TabIndex="24" Width="410px" Height="11px"></asp:TextBox>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="right" style="width: 200px"><span class="elementLabel">Description Of Goods :</span>&nbsp;</td>
                                        <td align="left" style="width: 115px">
                                            <asp:TextBox ID="txtdescofgoods" runat="server" CssClass="textBox" MaxLength="50" TabIndex="25" Width="410px" Height="12px"></asp:TextBox>
                                           
                                        </td>
                                   
                                          <td align="left" class="auto-style1"><span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Quantity :</span>
                                         <asp:TextBox ID="txtquantity" runat="server" CssClass="textBox" MaxLength="15" TabIndex="26" Width="70px" Height="12px"></asp:TextBox>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="right" style="width: 200px"><span class="elementLabel">Shipping Marks :</span>&nbsp;</td>
                                        <td align="left" style="width: 115px">
                                            <asp:TextBox ID="txtshipmarks" runat="server" CssClass="textBox" MaxLength="50" TabIndex="27" Width="410px" Height="12px"></asp:TextBox>
                                          
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="right" style="width: 200px"><span class="elementLabel">Currency :</span>&nbsp; </td>
                                        <td align="left" style="width: 200px">
                                            <asp:TextBox ID="txtcurrency" runat="server" CssClass="textBox" MaxLength="3"  
                                            TabIndex="28" Width="50px" Height="12px"></asp:TextBox>
                                             <asp:Button ID="btncurrency" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                            <span class="elementLabel">Bill Amt :</span>
                                             <asp:TextBox ID="txtbillamt" runat="server" Style="text-align: right"  CssClass="textBox" MaxLength="15" 
                                             onchange="return ccyformat();" TabIndex="29" Width="100px" Height="12px"></asp:TextBox>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="right" style="width: 200px"><span class="elementLabel">Goods imported under Import Licence No. <br /> 
                                            Dated or Goods imported under OGL <br /> Para Chapter of Exim Policy 2015-2020 :</span>&nbsp; </td>
                                        <td align="left" style="width: 115px"><asp:TextBox ID="txtgoodspolicy" TextMode="MultiLine"  runat="server" CssClass="textBox" Text="Foreign Trade Policy 2015-2020" MaxLength="100" TabIndex="30" Height="55px" Width="410px"></asp:TextBox>
                                            <br />
                                        </td>
                                       <td align="left" class="auto-style1" valign="top"><span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            Our Reference No :</span>
                                            <asp:TextBox ID="txtref" runat="server" Enabled="false" CssClass="textBox" MaxLength="14" Height="12px" TabIndex="31" Width="150px"></asp:TextBox>
                                            <br />
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="right" style="width: 200px"><span class="elementLabel"></span></td>
                                     <td align="left" style="width: 115px">
                                             &nbsp;&nbsp;
                                            <asp:CheckBox runat="server" ID="Chk_Ahm" Text="AHM" TabIndex="32"
                                                CssClass="elementLabel" Checked="false"></asp:CheckBox>
                                       </td>
                                    </tr>
                                    <tr>
                                    <td align="right" style="width: 200px"><span class="elementLabel"></span></td>
                                     <td align="left" style="width: 115px">
                                    <asp:Button ID="btnGO1_Next" runat="server" Text="Next >>" CssClass="buttonDefault"
                                    ToolTip="Go to GENERAL OPERATION" TabIndex="33" OnClientClick="return OnDocNextClick(1);" />
                                    </td>
                                    </tr>
                                    </table>
                                    </ContentTemplate>
                                   </ajaxToolkit:TabPanel>
                                     <ajaxToolkit:TabPanel ID="tbBROGO1" runat="server" HeaderText="GENERAL OPERATION "
                                        Font-Bold="true" ForeColor="White">
                                        <ContentTemplate>
                                            <table width="80%">
                                                <tr>
                                                    <td width="15%"></td>
                                                    <td colspan="4">
                                                     <asp:CheckBox runat="server" ID="Chk_GenOprtn" Text="General Operations" TabIndex="34"
                                                      CssClass="elementLabel"  OnCheckedChanged="Chk_GenOprtn_OnCheckedChanged" AutoPostBack="true">
                                                      </asp:CheckBox>
                                                    </td>
                                                </tr>
                                                <asp:Panel ID="PanelGO_Bill_Handling" runat="server" Visible="false">
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">COMMENT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Comment" MaxLength="20" runat="server" CssClass="textBox" TabIndex="35"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="20%">
                                                            <span class="elementLabel">Section No : </span>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="txt_GO1_SectionNo" MaxLength="2" runat="server" CssClass="textBox" TabIndex="36" Text="05"
                                                                Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right" width="10%">
                                                            <span class="elementLabel">Remarks : </span>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="txt_GO1_Remarks" Width="300px" MaxLength="30" runat="server" CssClass="textBox" TabIndex="37" onkeyup="return Toggel_GO1_Remarks();"></asp:TextBox>
                                                        </td>
                                                        <td align="right" width="15%">
                                                            <span class="elementLabel">MEMO : </span>
                                                        </td>
                                                        <td width="10%">
                                                            <asp:TextBox ID="txt_GO1_Memo" MaxLength="20" runat="server" CssClass="textBox" Width="50px" TabIndex="38"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="15%">
                                                            <span class="elementLabel">SCHEME No : </span>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="txt_GO1_Scheme_no" MaxLength="20" runat="server" CssClass="textBox" TabIndex="39"></asp:TextBox>
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
                                                            <asp:TextBox ID="txt_GO1_Debit_Code" runat="server" CssClass="textBox" TabIndex="40"
                                                               Text="D" Width="20px"  MaxLength="1"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Curr" runat="server" CssClass="textBox" TabIndex="41"
                                                                Width="30px"   MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Amt" runat="server" CssClass="textBox" Width="90px" onkeydown="return validate_Number(event);"
                                                             onchange="return OnGOCurrencyChange();"  TabIndex="42" Style="text-align: right" MaxLength="16" onkeyup="return Toggel_Debit_Amt1();"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">CUSTOMER : </span>
                                                        </td>
                                                        <td colspan="4">
                                                            <asp:TextBox ID="txt_GO1_Debit_Cust" runat="server" CssClass="textBox" TabIndex="43"  MaxLength="15"
                                                                 Width="180px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C CODE : </span>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txt_GO1_Debit_Cust_AcCode" runat="server" CssClass="textBox" TabIndex="44"  MaxLength="5"
                                                                Width="90px"></asp:TextBox>
                                                         
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C No : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Cust_AccNo" runat="server" CssClass="textBox" TabIndex="45"  MaxLength="15"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" align="right">
                                                            <span class="elementLabel">EXCH RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Exch_Rate" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                               Text="0" TabIndex="46" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">EXCH CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Exch_CCY" runat="server" CssClass="textBox" MaxLength="3"
                                                                TabIndex="47" Width="25px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">FUND : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_FUND" runat="server" CssClass="textBox" TabIndex="48"
                                                               MaxLength="1" Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CHECK No. : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Check_No" runat="server" Text="0" MaxLength="20" CssClass="textBox" TabIndex="49"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AVAILABLE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Available" runat="server" CssClass="textBox" TabIndex="50"
                                                               MaxLength="20" Width="90px" Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">ADVICE PRINT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_AdPrint" runat="server" CssClass="textBox" TabIndex="51"
                                                              Text="Y"  Width="20px" MaxLength="1"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">DETAILS : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Details" Width="300px" runat="server" CssClass="textBox" MaxLength="30" TabIndex="52"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">ENTITY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                             TabIndex="53" Width="90px" Style="text-align: right" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">DIVISION : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Division" runat="server" CssClass="textBox" TabIndex="54"
                                                             Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Inter_Amount" onkeydown="return validate_Number(event);" Text="0" runat="server" CssClass="textBox" TabIndex="55"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Inter_Rate"  runat="server" CssClass="textBox" TabIndex="56"
                                                              Text="0"  Width="90px" Style="text-align: right" onkeydown="return validate_Number(event);"></asp:TextBox>
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
                                                            <asp:TextBox ID="txt_GO1_Credit_Code" runat="server" CssClass="textBox" TabIndex="57"  MaxLength="1"
                                                              Text="C"  Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_Curr" runat="server" CssClass="textBox" TabIndex="58"  MaxLength="3"
                                                                Width="30px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                                <asp:TextBox ID="txt_GO1_Credit_Amt" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                TabIndex="59" Width="90px" Style="text-align: right" MaxLength="16" ReadOnly="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">CUSTOMER : </span>
                                                        </td>
                                                        <td colspan="4">
                                                            <asp:TextBox ID="txt_GO1_Credit_Cust" runat="server" CssClass="textBox" TabIndex="60"  MaxLength="15"
                                                                Width="180px"></asp:TextBox>
                                                       
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C CODE : </span>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txt_GO1_Credit_Cust_AcCode" runat="server" CssClass="textBox" TabIndex="61"  MaxLength="5"
                                                                Width="90px"></asp:TextBox>
                                                          
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C No : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_Cust_AccNo" runat="server" CssClass="textBox" TabIndex="62"  MaxLength="20"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" align="right">
                                                            <span class="elementLabel">EXCH RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_Exch_Rate" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                              Text="0" TabIndex="63" Width="90px" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">EXCH CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_Exch_Curr" runat="server" CssClass="textBox" MaxLength="3"
                                                                TabIndex="64" Width="25px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">FUND : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_FUND" runat="server" CssClass="textBox" TabIndex="65"
                                                              MaxLength="1"  Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CHECK No. : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_Check_No" runat="server" Text="0" CssClass="textBox" MaxLength="20" TabIndex="66"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AVAILABLE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_Available" runat="server" CssClass="textBox" TabIndex="67"
                                                              MaxLength="20"  Width="90px" Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">ADVICE PRINT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_AdPrint" runat="server" CssClass="textBox" TabIndex="68"
                                                                Width="20px" MaxLength="1"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">DETAILS : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_Details" Width="300px" runat="server" CssClass="textBox" MaxLength="30" TabIndex="69"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">ENTITY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                              TabIndex="70" Width="90px" Style="text-align: right" MaxLength="3" ></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">DIVISION : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_Division" runat="server" CssClass="textBox" TabIndex="71"
                                                               Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_Inter_Amount" Text="0" onkeydown="return validate_Number(event);" runat="server" CssClass="textBox" TabIndex="72"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_Inter_Rate" onkeydown="return validate_Number(event);" runat="server" CssClass="textBox" TabIndex="73"
                                                              Text="0"  Width="90px" Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                                <tr>
                                                   <td align="left"> 
                                                   <td >
                                                   <asp:Button ID="btnGO1_Prev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                   ToolTip="Back to IMPORT ACCOUNTING" Width="60px" TabIndex="74" OnClientClick="return OnDocNextClick(0);" />
                                                   <asp:Button ID="btnSave" runat="server" OnClientClick="return SubmitValidation();"
                                                   CssClass="buttonDefault" TabIndex="75" Text="Send To Checker" Width="120px" ToolTip="Send To Checker" OnClick="btnSave_Click" />
                                                   </td>
                                                   </td>
                                               </tr>
                                            </table>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                </ajaxToolkit:TabContainer>
                             </td>
                           </tr>
                            <tr> 
                                 <td align="center" colspan="4">
                                 <asp:Label ID="lblChecker_Remark" runat="server" CssClass="mandatoryField" Font-Bold="true"></asp:Label>
                                 </td>
                                      <%--  <td align="right" style="width: 200px"></td>
                                        <td align="left" style="width: 290px; padding-top: 10px; padding-bottom: 10px">&nbsp;--%>
                                             <%--<asp:Button ID="Button1" runat="server"
                                             CssClass="buttonDefault" TabIndex="31" Text="Save" ToolTip="Save" OnClick="btnSave_Click" />--%>
                                            <%--  <asp:Button ID="btnCancel" runat="server" CssClass="buttonDefault" TabIndex="32" Text="Cancel" ToolTip="Cancel" OnClick="btnCancel_Click" />
                                           <%--  <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                            <asp:Button ID="Button2" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />--%>
                                            
                                         <%--  <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                            <asp:Button ID="Button3" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />--%>
                                          <%--  <asp:Button ID="btnsendtochecker" runat="server" CssClass="buttonDefault" Width="120" TabIndex="33" Text="SEND TO CHECKER" ToolTip="SEND TO CHECKER" OnClick="btnsendtochecker_Click" />--%>
                                     <%-- </td>--%>
                               </tr>
                        </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
</body>
</html>
