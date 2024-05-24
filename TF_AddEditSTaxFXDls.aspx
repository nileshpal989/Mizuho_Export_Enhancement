<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_AddEditSTaxFXDls.aspx.cs"
    Inherits="TF_AddEditSTaxFXDls" %>

<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico"
        type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Validations.js"  language="javascript" type="text/javascript"></script>
    <link href="Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script language="javascript" type="text/javascript">
       
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" defaultbutton="btnSave">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="conditional">
                <Triggers>
                    <%--<asp:PostBackTrigger ControlID="btnBack" />--%>
                   
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel">Service Tax On FX Dls Master Details</span>
                            </td>
                            <td align="right" style="width: 50%">
                               <%-- <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" OnClick="btnBack_Click"
                                    ToolTip="Back" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100%" valign="top" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="2">
                                <table cellspacing="0" cellpadding="0" border="0" width="600px" style="line-height: 150%">
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Effective Date :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            &nbsp;
                                            <asp:TextBox ID="txtEffDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                                Width="70px" TabIndex="1" AutoPostBack="True" 
                                                ontextchanged="txtEffDate_TextChanged"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender ID="mdDocdate" Mask="99/99/9999" MaskType="Date"
                                                runat="server" TargetControlID="txtEffDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                CultureTimePlaceholder=":" Enabled="True">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <asp:Button ID="btncalendar_DocDate" runat="server" CssClass="btncalendar_enabled"
                                                TabIndex="-1" />
                                            <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtEffDate" PopupButtonID="btncalendar_DocDate" Enabled="True">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdDocdate"
                                                ValidationGroup="dtVal" ControlToValidate="txtEffDate" EmptyValueMessage="Enter Date Value"
                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="MaskedEditValidator3"></ajaxToolkit:MaskedEditValidator>
                                        </td>
                                    </tr>
                                </table>
                                <table cellspacing="0" cellpadding="0" border="0" width="600px" style="line-height: 150%">
                                    <tr>
                                        <td align="right" nowrap>
                                            <span class="mandatoryField">*</span><span class="elementLabel">Sr No. :</span>
                                        </td>
                                        <td align="left" nowrap>
                                            <span class="mandatoryField">*</span><span class="elementLabel">Invoice Value From :</span>
                                        </td>
                                        <td align="left" nowrap>
                                            <span class="mandatoryField">*</span><span class="elementLabel">Invoice Value To :</span>
                                        </td>
                                        <td align="left" nowrap>
                                            <span class="mandatoryField">*</span><span class="elementLabel">Rate :</span>
                                        </td>
                                        <td align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">1 :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtSlabFrom1" runat="server" CssClass="textBox" 
                                                Width="100px" TabIndex="1"></asp:TextBox>
                                        </td>
                                         <td align="left">
                                            &nbsp;<asp:TextBox ID="txtSlabTo1" runat="server" CssClass="textBox"  
                                                 Width="100px" TabIndex="2"></asp:TextBox>
                                        </td>
                                         <td align="left">
                                            &nbsp;<asp:TextBox ID="txtRate1" runat="server" CssClass="textBox"  Width="50px" 
                                                 TabIndex="3"></asp:TextBox>
                                        </td>
                                         <td align="left" nowrap>
                                         <span class="elementLabel">Min Amt :</span>
                                            &nbsp;<asp:TextBox ID="txtMinAmt" runat="server" CssClass="textBox"  
                                                 Width="100px" TabIndex="4"></asp:TextBox>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td align="right">
                                            <span class="elementLabel">2 :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtSlabFrom2" runat="server" CssClass="textBox" 
                                                Width="100px" TabIndex="5"></asp:TextBox>
                                        </td>
                                         <td align="left">
                                            &nbsp;<asp:TextBox ID="txtSlabTo2" runat="server" CssClass="textBox"  
                                                 Width="100px" TabIndex="6"></asp:TextBox>
                                        </td>
                                         <td align="left">
                                            &nbsp;<asp:TextBox ID="txtRate2" runat="server" CssClass="textBox"  Width="50px" 
                                                 TabIndex="7"></asp:TextBox>
                                        </td>
                                         <td align="left" nowrap>
                                        
                                        </td>
                                    </tr>
                                       <tr>
                                        <td align="right">
                                            <span class="elementLabel">3 :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtSlabFrom3" runat="server" CssClass="textBox" 
                                                Width="100px" TabIndex="8"></asp:TextBox>
                                        </td>
                                         <td align="left">
                                            &nbsp;<asp:TextBox ID="txtSlabTo3" runat="server" CssClass="textBox"  
                                                 Width="100px" TabIndex="9"></asp:TextBox>
                                        </td>
                                         <td align="left">
                                            &nbsp;<asp:TextBox ID="txtRate3" runat="server" CssClass="textBox"  Width="50px" 
                                                 TabIndex="10"></asp:TextBox>
                                        </td>
                                         <td align="left" nowrap>
                                         <span class="elementLabel">Max Amt :</span>
                                            &nbsp;<asp:TextBox ID="txtMaxAmt" runat="server" CssClass="textBox"  
                                                 Width="100px" TabIndex="11"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                           
                                        </td>
                                        <td align="right">
                                            <span class="elementLabel">Edu Cess :</span>
                                        </td>
                                         <td align="left">
                                            &nbsp;<asp:TextBox ID="txtEduCess" runat="server" CssClass="textBox"  
                                                 Width="100px" TabIndex="12"></asp:TextBox>
                                        </td>
                                         <td align="left">
                                            <span class="elementLabel">S.Edu. Cess :</span>
                                        </td>
                                         <td align="left" nowrap>
                                            &nbsp;<asp:TextBox ID="txtSeduCess" runat="server" CssClass="textBox"  
                                                 Width="100px" TabIndex="13"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                    <td></td>
                                                <td align="right">
                                                   <span class="mandatoryField">*</span><span class="elementLabel">SBCess (%) :</span>
                                                    <td align="left">
                                                        &nbsp
                                                    <asp:TextBox ID="txtsbcess" runat="server" CssClass="textBox" TabIndex="13" Width="40px"
                                                        Style="text-align: right"></asp:TextBox>
                                                    <%--<span class=elementLabel>SBCess Amt :</span>--%>
                                                <%--<td align="left">--%>
                                                   <%-- <asp:TextBox ID="txtSBcesssamt" runat="server" CssClass="textBox" TabIndex="59" Width="100px"
                                                        Style="text-align: right"></asp:TextBox>--%>
                                                </td>

                                                 <td align="right">
                                                   <span class="mandatoryField">*</span><span class="elementLabel">KKCess (%) :</span>
                                                    <td align="left">
                                                        &nbsp
                                                    <asp:TextBox ID="txtkkcess" runat="server" CssClass="textBox" TabIndex="13" Width="40px"
                                                        Style="text-align: right"></asp:TextBox>
                                                    <%--<span class=elementLabel>SBCess Amt :</span>--%>
                                                <%--<td align="left">--%>
                                                   <%-- <asp:TextBox ID="txtSBcesssamt" runat="server" CssClass="textBox" TabIndex="59" Width="100px"
                                                        Style="text-align: right"></asp:TextBox>--%>
                                                </td>



                                            </tr>

                                </table>
                                <table cellspacing="0" cellpadding="0" border="0" width="600px" style="line-height: 150%">
                                    <tr>
                                        <td align="right" width="150px">
                                        </td>
                                        <td align="left" style="width: 400px; padding-top: 10px; padding-bottom: 10px">
                                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                                OnClick="btnSave_Click" ToolTip="Save" TabIndex="14" />
                                        </td>
                                    </tr>
                                </table>
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
