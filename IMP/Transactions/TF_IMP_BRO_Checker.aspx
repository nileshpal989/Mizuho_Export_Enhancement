<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_BRO_Checker.aspx.cs" Inherits="IMP_Transactions_TF_IMP_BRO_Checker" %>

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
    <script src="../Scripts/TF_IMP_BRO_CHECKER.js" type="text/javascript"></script>
    
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
                                <span class="pageLabel"><strong>&nbsp; Bank Release Order (BRO)- Checker</strong></span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                    TabIndex="34"  OnClientClick="return OnBackClick();" />
                               </tr>
                         <tr>
                        <td align="left" style="width: 100%" valign="top" colspan="2">
                            <hr />
                        </td>
                        </tr>
                        <tr>
                         <td align="left" style="width: 100%;" valign="top" colspan="2">
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            <%-------------------------  hidden fields  --------------------------------%>
                            <input type="hidden" id="hdnRejectReason" runat="server" />
                        </td>
                        </tr>
                        </table>
                        <table id="tbl_Acceptance" cellspacing="0" border="0" width="100%">
                        <tr>
                          <td width="40%" align="center" valign="top" style="padding-right:300px">
                            <span class="elementLabel">Val.Date :</span>
                            <asp:TextBox ID="txtValueDate" runat="server" TabIndex="2" CssClass="textBox" MaxLength="10"
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
                         <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" 
                                colspan="2">
                                <ajaxToolkit:TabContainer ID="TabContainerMain" runat="server" ActiveTabIndex="0"
                                    CssClass="ajax__tab_xp-theme">
                                     <ajaxToolkit:TabPanel ID="tbBRODetails" runat="server" HeaderText="BRO Document"
                                        Font-Bold="true" ForeColor="White">
                                   <ContentTemplate>
                                     <table width="100%">
                                     <tr>
                                        <td align="right"><span class="elementLabel">Branch :</span>&nbsp; </td>
                                        <td align="left" style="width: 290px">
                                            <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true" CssClass="dropdownList" Width="120px">
                                         </asp:DropDownList>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="right" class="auto-style1"><span class="elementLabel">Delivery Order Serial No :</span>&nbsp; </td>
                                        <td align="left" style="width: 150px">
                                            <asp:TextBox ID="txtdosrno" runat="server" ReadOnly="true" CssClass="textBox" MaxLength="50" TabIndex="1" Width="35px" Height="12px" Text="BRO"></asp:TextBox>
                                            &nbsp;<asp:TextBox ID="txtdosrno1" runat="server" ReadOnly="true" CssClass="textBox" MaxLength="50" TabIndex="2" Width="30px" Height="12px" ></asp:TextBox>
                                             &nbsp;<asp:TextBox ID="txtdosrno2" runat="server" ReadOnly="true" CssClass="textBox" MaxLength="50" TabIndex="3" Width="60px" Height="12px" Text="190001" ></asp:TextBox>
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
                                            <asp:TextBox ID="txtApplid" runat="server" CssClass="textBox" MaxLength="50" TabIndex="6" Width="100px" Height="12" 
                                                 AutoPostBack="true"></asp:TextBox>
                                            <asp:Button ID="btnApplName" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" Visible="false" />
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
                                            <asp:TextBox ID="txtApplAdd" runat="server" CssClass="textBox" Height="55px" MaxLength="50" TabIndex="7" TextMode="MultiLine" Width="410px"></asp:TextBox>
                                            <br />
                                        </td>
                                         <td align="left" class="auto-style1" valign="middle">
                                               <span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; City:</span>
                                               <asp:TextBox ID="txtApplCity" runat="server" CssClass="textBox" MaxLength="15" TabIndex="10" Width="150px" Height="12px"></asp:TextBox>
                                               <br />
                                               <br />
                                               <span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Pincode :</span>
                                               <asp:TextBox ID="txtApplPincode" runat="server" CssClass="textBox" MaxLength="6" TabIndex="10" Width="70px" Height="12px"></asp:TextBox>
                                         </td>
                                    </tr>
                                     <tr>
                                        <td align="right" style="width: 200px"><span class="elementLabel">Beneficiary Name :</span>&nbsp;</td>
                                        <td align="left" style="width: 115px">
                                            <asp:TextBox ID="txtbenefname" runat="server" CssClass="textBox" MaxLength="50" TabIndex="9" Width="410px" Height="12px"></asp:TextBox>
                                           
                                        </td>
                                          <td align="left" class="auto-style1"><span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Country :</span>
                                         <asp:TextBox ID="txtcountry" runat="server" CssClass="textBox" MaxLength="3" TabIndex="10"
                                              Width="50px" Height="12px" onkeyup="return Uppercase('txtcountry')" AutoPostBack="true"></asp:TextBox>
                                          <asp:Button ID="btncountry" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" Visible="false" />
                                          <asp:Label ID="lblcountryname" runat="server" Width="200px" CssClass="elementLabel"></asp:Label>
                                        </td>
                                    </tr>
                                     <tr>
                                         <td align="right" style="width: 200px"><span class="elementLabel">Airways Bill No1 :</span>&nbsp; </td>
                                        <td align="left" style="width: 150px">
                                            <asp:TextBox ID="txtbill1" runat="server" CssClass="textBox" MaxLength="30" TabIndex="11" Width="300px" Height="12px"></asp:TextBox>
                                            </td>
                                            <td>
                                            <span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Date :</span>
                                            <asp:TextBox ID="txtbilldate" runat="server" CssClass="textBox" MaxLength="50" TabIndex="12" Width="75px" Height="12px" ValidationGroup="dtVal"></asp:TextBox>
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
                                        <td align="right" style="width: 200px"><span class="elementLabel">Airways Bill No2 :</span>&nbsp; </td>
                                        <td align="left" style="width: 150px">
                                            <asp:TextBox ID="txtbill2" runat="server" CssClass="textBox" MaxLength="30" TabIndex="13" Width="300px" Height="12px"></asp:TextBox>
                                            </td>
                                            <td>
                                            <span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Date :</span>
                                            <asp:TextBox ID="txtbilldate2" runat="server" CssClass="textBox" MaxLength="50" TabIndex="14" Width="75px" Height="12px" ValidationGroup="dtVal"></asp:TextBox>
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
                                           
                                            <asp:TextBox ID="txtAircompname" runat="server" CssClass="textBox" MaxLength="50" TabIndex="15" Width="410px" Height="12px"></asp:TextBox>
                                           
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="right" style="width: 200px"><span class="elementLabel">
                                            Flight No 1 :</span>&nbsp;</td>
                                        <td align="left" style="width: 50px">
                                            
                                            <asp:TextBox ID="txtflightno1" runat="server" CssClass="textBox" MaxLength="10" TabIndex="16" Width="120px" Height="12px"></asp:TextBox>
                                           
                                        <%--</td> <td align="left" class="auto-style1">--%>
                                       <span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Date :</span> 
                                            <asp:TextBox ID="txtairlinedate1" runat="server" CssClass="textBox" MaxLength="50" TabIndex="17" Width="75px" Height="12px" ValidationGroup="dtVal"></asp:TextBox>
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
                                            
                                            <asp:TextBox ID="txtflightno2" runat="server" CssClass="textBox" MaxLength="10" TabIndex="18" Width="120px" Height="12px"></asp:TextBox>
                                           
                                    <%--    </td><td align="left" class="auto-style1">--%>
                                        <span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Date :</span> 
                                            <asp:TextBox ID="txtairlinedate2" runat="server" CssClass="textBox" MaxLength="50" TabIndex="19" Width="75px" Height="12px" ValidationGroup="dtVal"></asp:TextBox>
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
                                            <asp:TextBox ID="txtshipper" runat="server" CssClass="textBox" MaxLength="50" TabIndex="20" Width="410px" Height="12px"></asp:TextBox>
                                            <br />
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="right" style="width: 200px"><span class="elementLabel">Supplier Name :</span>&nbsp; </td>
                                        <td align="left" style="width: 115px">
                                            <asp:TextBox ID="txtsupplier" runat="server" CssClass="textBox" MaxLength="50" TabIndex="21" Width="410px" Height="12px"></asp:TextBox>
                                            <br />
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="right" style="width: 200px"><span class="elementLabel">Consignee Name :</span>&nbsp;</td>
                                        <td align="left" style="width: 115px">
                                            <asp:TextBox ID="txtconsignee" runat="server" CssClass="textBox" MaxLength="50" TabIndex="22" Width="410px" Height="12px"></asp:TextBox>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="right" style="width: 200px"><span class="elementLabel">Notify Party :</span>&nbsp; </td>
                                        <td align="left" style="width: 115px">
                                            <asp:TextBox ID="txtnotifyname" runat="server" CssClass="textBox" MaxLength="50" TabIndex="23" Width="410px" Height="11px"></asp:TextBox>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="right" style="width: 200px"><span class="elementLabel">Description Of Goods :</span>&nbsp;</td>
                                        <td align="left" style="width: 115px">
                                            <asp:TextBox ID="txtdescofgoods" runat="server" CssClass="textBox" MaxLength="50" TabIndex="24" Width="410px" Height="12px"></asp:TextBox>
                                           
                                        </td>
                                   
                                          <td align="left" class="auto-style1"><span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Quantity :</span>
                                         <asp:TextBox ID="txtquantity" runat="server" CssClass="textBox" MaxLength="50" TabIndex="25" Width="70px" Height="12px"></asp:TextBox>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="right" style="width: 200px"><span class="elementLabel">Shipping Marks :</span>&nbsp;</td>
                                        <td align="left" style="width: 115px">
                                            <asp:TextBox ID="txtshipmarks" runat="server" CssClass="textBox" MaxLength="50" TabIndex="26" Width="410px" Height="12px"></asp:TextBox>
                                          
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="right" style="width: 200px"><span class="elementLabel">Currency :</span>&nbsp; </td>
                                        <td align="left" style="width: 200px">
                                            <asp:TextBox ID="txtcurrency" runat="server" CssClass="textBox" MaxLength="50" TabIndex="27" Width="50px" Height="12px"></asp:TextBox>
                                             <asp:Button ID="btncurrency" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" Visible="false" />
                                            <span class="elementLabel">Bill Amt :</span>
                                             <asp:TextBox ID="txtbillamt" runat="server" CssClass="textBox" MaxLength="50" TabIndex="28" Width="100px" Height="12px"></asp:TextBox>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="right" style="width: 200px"><span class="elementLabel">Goods imported under Import Licence No. <br /> 
                                            Dated or Goods imported under OGL <br /> Para Chapter of Exim Policy 2015-2020 :</span>&nbsp; </td>
                                        <td align="left" style="width: 115px"><asp:TextBox ID="txtgoodspolicy" TextMode="MultiLine" runat="server" CssClass="textBox" Text="Foreign Trade Policy 2015-2020" MaxLength="100" TabIndex="29" Height="55px" Width="410px"></asp:TextBox>
                                            <br />
                                        </td>
                                       <td align="left" class="auto-style1" valign="top"><span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            Our Reference No :</span>
                                            <asp:TextBox ID="txtref" runat="server" CssClass="textBox" MaxLength="50" Height="12px" TabIndex="30" Width="150px"></asp:TextBox>
                                           
                                            <br />
                                        </td>
                                    </tr>
                                     <tr>
                                        <td align="right" style="width: 200px"><span class="elementLabel"></span></td>
                                     <td align="left" style="width: 115px">
                                             &nbsp;&nbsp;
                                            <asp:CheckBox runat="server" ID="Chk_Ahm" Text="AHM" TabIndex="15"
                                                CssClass="elementLabel" Checked="false"></asp:CheckBox>
                                       </td>
                                    </tr>
                                    <tr>
                                    <td align="right" style="width: 200px"><span class="elementLabel"></span></td>
                                     <td align="left" style="width: 115px">
                                    <asp:Button ID="btnGO1_Next" runat="server" Text="Next >>" CssClass="buttonDefault"
                                    ToolTip="Go to GENERAL OPERATION" TabIndex="120" OnClientClick="return OnDocNextClick(1);" />
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
                                                     <asp:CheckBox runat="server" ID="Chk_GenOprtn" Text="General Operations" TabIndex="14"
                                                      CssClass="elementLabel"  AutoPostBack="true">
                                                      </asp:CheckBox>
                                                    </td>
                                                </tr>
                                                <asp:Panel ID="PanelGO_Bill_Handling" runat="server" Visible="false">
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">COMMENT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Comment" runat="server" CssClass="textBox" TabIndex="80"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="20%">
                                                            <span class="elementLabel">Section No : </span>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="txt_GO1_SectionNo" runat="server" CssClass="textBox" TabIndex="81"
                                                                Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right" width="10%">
                                                            <span class="elementLabel">Remarks : </span>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="txt_GO1_Remarks" Width="300px" runat="server" CssClass="textBox" TabIndex="82" onblur="return Toggel_GO1_Remarks();"></asp:TextBox>
                                                        </td>
                                                        <td align="right" width="15%">
                                                            <span class="elementLabel">MEMO : </span>
                                                        </td>
                                                        <td width="10%">
                                                            <asp:TextBox ID="txt_GO1_Memo" runat="server" CssClass="textBox" Width="50px" TabIndex="83"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="15%">
                                                            <span class="elementLabel">SCHEME No : </span>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="txt_GO1_Scheme_no" runat="server" CssClass="textBox" TabIndex="84"></asp:TextBox>
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
                                                            <asp:TextBox ID="txt_GO1_Debit_Code" runat="server" CssClass="textBox" TabIndex="85"
                                                                Width="20px"  MaxLength="1"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Curr" runat="server" CssClass="textBox" TabIndex="86"
                                                                Width="30px"  MaxLength="3"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Amt" runat="server" CssClass="textBox" Width="90px" 
                                                                TabIndex="87" Style="text-align: right" MaxLength="16"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">CUSTOMER : </span>
                                                        </td>
                                                        <td colspan="4">
                                                            <asp:TextBox ID="txt_GO1_Debit_Cust" runat="server" CssClass="textBox" TabIndex="88"  MaxLength="12"
                                                                Width="180px"></asp:TextBox>
                                                          
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C CODE : </span>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txt_GO1_Debit_Cust_AcCode" runat="server" CssClass="textBox" TabIndex="89"  MaxLength="5"
                                                                Width="90px"></asp:TextBox>
                                                       
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C No : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Cust_AccNo" runat="server" CssClass="textBox" TabIndex="90"  MaxLength="12"></asp:TextBox>
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
                                                                TabIndex="92" Width="25px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">FUND : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_FUND" runat="server" CssClass="textBox" TabIndex="93"
                                                                Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CHECK No. : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Check_No" runat="server" CssClass="textBox" TabIndex="94"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AVAILABLE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Available" runat="server" CssClass="textBox" TabIndex="95"
                                                                Width="90px" Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">ADVICE PRINT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_AdPrint" runat="server" CssClass="textBox" TabIndex="96"
                                                                Width="20px" MaxLength="1"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">DETAILS : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Details"  Width="300px" runat="server" CssClass="textBox" TabIndex="97"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">ENTITY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Entity" runat="server" CssClass="textBox" onkeydown="return validate_Number(event);"
                                                                TabIndex="98" Width="90px" Style="text-align: right" MaxLength="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">DIVISION : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Division" runat="server" CssClass="textBox" TabIndex="99"
                                                                Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Inter_Amount" onkeydown="return validate_Number(event);" runat="server" CssClass="textBox" TabIndex="100"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Debit_Inter_Rate"  runat="server" CssClass="textBox" TabIndex="101"
                                                                Width="90px" Style="text-align: right" onkeydown="return validate_Number(event);"></asp:TextBox>
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
                                                            <asp:TextBox ID="txt_GO1_Credit_Code" runat="server" CssClass="textBox" TabIndex="102"  MaxLength="1"
                                                                Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CCY : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_Curr" runat="server" CssClass="textBox" TabIndex="103"  MaxLength="3"
                                                                Width="30px"></asp:TextBox>
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
                                                            <asp:TextBox ID="txt_GO1_Credit_Cust" runat="server" CssClass="textBox" TabIndex="105"  MaxLength="12"
                                                                Width="180px"></asp:TextBox>
                                                         
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C CODE : </span>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txt_GO1_Credit_Cust_AcCode" runat="server" CssClass="textBox" TabIndex="106"  MaxLength="5"
                                                                Width="90px"></asp:TextBox>
                                                        
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">A/C No : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_Cust_AccNo" runat="server" CssClass="textBox" TabIndex="107"  MaxLength="20"></asp:TextBox>
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
                                                                TabIndex="109" Width="25px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">FUND : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_FUND" runat="server" CssClass="textBox" TabIndex="110"
                                                                Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">CHECK No. : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_Check_No" runat="server" CssClass="textBox" TabIndex="111"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">AVAILABLE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_Available" runat="server" CssClass="textBox" TabIndex="112"
                                                                Width="90px" Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">ADVICE PRINT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_AdPrint" runat="server" CssClass="textBox" TabIndex="113"
                                                                Width="20px" MaxLength="1"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">DETAILS : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_Details"  Width="300px" runat="server" CssClass="textBox" TabIndex="114"></asp:TextBox>
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
                                                                Width="20px"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-AMOUNT : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_Inter_Amount" onkeydown="return validate_Number(event);" runat="server" CssClass="textBox" TabIndex="117"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <span class="elementLabel">INTER-RATE : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_GO1_Credit_Inter_Rate" onkeydown="return validate_Number(event);" runat="server" CssClass="textBox" TabIndex="118"
                                                                Width="90px" Style="text-align: right"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                                <tr>
                                                   <td align="left"> 
                                                   <td >
                                                   <asp:Button ID="btnGO1_Prev" runat="server" Text="<< Prev" CssClass="buttonDefault"
                                                   ToolTip="Back to IMPORT ACCOUNTING" Width="70px" TabIndex="119" OnClientClick="return OnDocNextClick(0);" />
                                                   <asp:Button ID="btnSave" Style="visibility: hidden" runat="server" Text="Save" CssClass="buttonDefault"
                                                   ToolTip="Save" OnClick="btnSave_Click" TabIndex="107" />
                                                   </td>
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
                                            </table>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                </ajaxToolkit:TabContainer>
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
