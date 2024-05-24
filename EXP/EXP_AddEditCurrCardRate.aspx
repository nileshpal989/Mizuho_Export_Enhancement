<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EXP_AddEditCurrCardRate.aspx.cs" Inherits="EXP_EXP_AddEditCurrCardRate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
     <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script language="javascript" type="text/javascript">
        function validateSave() {
            var txtDate = document.getElementById('txtDate');
            if (txtDate.value == "") {
                alert('Enter Date');
                txtDate.focus();
                return false;
            }
            return true;
        }

        function FormatRates() {
            var USDSrate = document.getElementById('txtUSD_SRate');
            var USDPrate = document.getElementById('txtUSD_PRate');
            if (USDSrate.value == '')
                USDSrate.value = 0;
            if (USDPrate.value == '')
                USDPrate.value = 0;

                USDSrate.value = parseFloat(USDSrate.value).toFixed(4);
                USDPrate.value = parseFloat(USDPrate.value).toFixed(4);
            
            var GBPSrate = document.getElementById('txtGBP_SRate');
            var GBPPrate = document.getElementById('txtGBP_PRate');

            if (GBPPrate.value == '')
                GBPPrate.value = 0;

            if (GBPSrate.value == '')
                GBPSrate.value = 0;

                GBPSrate.value = parseFloat(GBPSrate.value).toFixed(4);
                GBPPrate.value = parseFloat(GBPPrate.value).toFixed(4);
           
            var EURSrate = document.getElementById('txtEur_SRate');
            var EURPrate = document.getElementById('txtEur_PRate');

            if (EURSrate.value == '')
                EURSrate.value = 0;

            if (EURPrate.value == '')
                EURPrate.value = 0;

              EURSrate.value = parseFloat(EURSrate.value).toFixed(4);
              EURPrate.value = parseFloat(EURPrate.value).toFixed(4);
            
            var JPYSrate = document.getElementById('txtJPY_SRate');
            var JPYPrate = document.getElementById('txtJPY_PRate');

            if (JPYSrate.value == '')
                JPYSrate.value = 0;
            if (JPYPrate.value == '')
                JPYPrate.value = 0;

                JPYSrate.value = parseFloat(JPYSrate.value).toFixed(4);
                JPYPrate.value = parseFloat(JPYPrate.value).toFixed(4);
          
            var CHFSrate = document.getElementById('txtCHF_SRate');
            var CHFPrate = document.getElementById('txtCHF_PRate');

            if (CHFSrate.value == '')
                CHFSrate.value = 0;
            if (CHFPrate.value == '')
                CHFPrate.value = 0;

                CHFSrate.value = parseFloat(CHFSrate.value).toFixed(4);
                CHFPrate.value = parseFloat(CHFPrate.value).toFixed(4);
           
            var AUDSrate = document.getElementById('txtAUD_SRate');
            var AUDPrate = document.getElementById('txtAUD_PRate');

            if (AUDSrate.value == '')
                AUDSrate.value = 0;
            if (AUDPrate.value == '')
                AUDPrate.value = 0;

                AUDSrate.value = parseFloat(AUDSrate.value).toFixed(4);
                AUDPrate.value = parseFloat(AUDPrate.value).toFixed(4);
           
            var CADSrate = document.getElementById('txtCAD_SRate');
            var CADPrate = document.getElementById('txtCAD_PRate');

            if (CADSrate.value == '')
                CADSrate.value = 0;
            if (CADPrate.value == '')
                CADPrate.value = 0;

                CADSrate.value = parseFloat(CADSrate.value).toFixed(4);
                CADPrate.value = parseFloat(CADPrate.value).toFixed(4);
            
            var SGDSrate = document.getElementById('txtSGD_SRate');
            var SGDPrate = document.getElementById('txtSGD_PRate');

            if (SGDSrate.value == '')
                SGDSrate.value = 0;
            if (SGDPrate.value == '')
                SGDPrate.value = 0;

                SGDSrate.value = parseFloat(SGDSrate.value).toFixed(4);
                SGDPrate.value = parseFloat(SGDPrate.value).toFixed(4);
           
            var SEKSrate = document.getElementById('txtSEK_SRate');
            var SEKPrate = document.getElementById('txtSEK_PRate');

            if (SEKSrate.value == '')
                SEKSrate.value = 0;
            if (SEKPrate.value == '')
                SEKPrate.value = 0;
                
                SEKSrate.value = parseFloat(SEKSrate.value).toFixed(4);
                SEKPrate.value = parseFloat(SEKPrate.value).toFixed(4);
           
            var HKDSrate = document.getElementById('txtHKD_SRate');
            var HKDPrate = document.getElementById('txtHKD_PRate');

            if (HKDSrate.value == '')
                HKDSrate.value = 0;
            if (HKDPrate.value == '')
                HKDPrate.value = 0;

                HKDSrate.value = parseFloat(HKDSrate.value).toFixed(4);
                HKDPrate.value = parseFloat(HKDPrate.value).toFixed(4);
            
            var SARSrate = document.getElementById('txtSAR_SRate');
            var SARPrate = document.getElementById('txtSAR_PRate');

            if (SARSrate.value == '')
                SARSrate.value = 0;
            if (SARPrate.value == '')
                SARPrate.value = 0;

                SARSrate.value = parseFloat(SARSrate.value).toFixed(4);
                SARPrate.value = parseFloat(SARPrate.value).toFixed(4);

            var DEMSrate = document.getElementById('txtDEM_SRate');
            var DEMPrate = document.getElementById('txtDEM_PRate');

                if (DEMSrate.value == '')
                    DEMSrate.value = 0;
                if (DEMPrate.value == '')
                    DEMPrate.value = 0;

                DEMSrate.value = parseFloat(DEMSrate.value).toFixed(4);
                DEMPrate.value = parseFloat(DEMPrate.value).toFixed(4);
           
            return true;
        }

        function isValidDate(controlID, CName) {
            var obj = controlID;
            if (controlID.value != "__/__/____") {

                var day = obj.value.split("/")[0];
                var month = obj.value.split("/")[1];
                var year = obj.value.split("/")[2];
                var today = new Date();

                if (day == "__") {
                    day = today.getDay();
                }
                if (month == "__") {
                    month = today.getMonth() + 1;
                }
                if (year == "____") {
                    year = today.getFullYear();
                }
                var dt = new Date(year, month - 1, day);

                //              if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year) || (dt > today)) {

                if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year)) {

                    alert('Invalid ' + CName);
                    controlID.focus();
                    return false;
                }
            }
        }
    

    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>
    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="../Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div>
        <uc2:Menu ID="Menu1" runat="server" />
        <br />
        <br />
        <center>
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="conditional">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnBack" />
                    <asp:PostBackTrigger ControlID="btnCancel" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel">Currency Rate Master Details</span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                    OnClick="btnBack_Click" TabIndex="26" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                                <input type="hidden" id="hdnMODE_DEMcurr" runat="server" />
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
                                            <span class="mandatoryField">*</span><span class="elementLabel">Date :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            &nbsp;<asp:TextBox ID="txtDate" runat="server" CssClass="textBox" MaxLength="10"
                                                ValidationGroup="dtVal" Width="70px" TabIndex="1"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender ID="mdDocdate" Mask="99/99/9999" MaskType="Date"
                                                runat="server" TargetControlID="txtDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                CultureTimePlaceholder=":" Enabled="True">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <asp:Button ID="btncalendar_DocDate" runat="server" CssClass="btncalendar_enabled"
                                                TabIndex="-1" />
                                            <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtDate" PopupButtonID="btncalendar_DocDate" Enabled="True">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdDocdate"
                                                ValidationGroup="dtVal" ControlToValidate="txtDate" EmptyValueMessage="Enter Date Value"
                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="Date is invalid"></ajaxToolkit:MaskedEditValidator>
                                        </td>
                                    </tr>
                                </table>
                                <table cellspacing="0" cellpadding="1" border="0" width="800px" style="line-height: 150%">
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 50px">
                                            <span class="elementLabel">Currency </span>
                                        </td>
                                        <td align="center" style="width: 150px"><span class="elementLabel">Purchase Rate</span>
                                        </td>
                                        <td align="left" >
                                            <span class="elementLabel">  Sale Rate</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 50px">
                                            <asp:TextBox ID="txtCurrUSD" runat="server" CssClass="textBox" Font-Bold="true" MaxLength="3" Width="40px"
                                                Text="USD" Enabled="false" ></asp:TextBox>
                                        </td>
                                        <td align="center" style="width: 100px">
                                            <asp:TextBox ID="txtUSD_PRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                TabIndex="2" style="text-align:right" onfocus="this.select();"></asp:TextBox>
                                        </td>
                                      <td align="left" >
                                      
                                            <asp:TextBox ID="txtUSD_SRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                   style="text-align:right" onfocus="this.select();" TabIndex="3"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 50px">
                                            <asp:TextBox ID="txtCurrGBP" runat="server" CssClass="textBox" MaxLength="3" Width="40px"
                                                Text="GBP" Enabled="false" Font-Bold="true"></asp:TextBox>
                                        </td>
                                        <td align="center" style="width: 100px">
                                            <asp:TextBox ID="txtGBP_PRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                TabIndex="4" style="text-align:right" onfocus="this.select();"></asp:TextBox>
                                        </td>
                                        <td align="left" >
                                            <asp:TextBox ID="txtGBP_SRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                style="text-align:right" onfocus="this.select();" TabIndex="5"> </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 50px">
                                            <asp:TextBox ID="txtCurrEur" runat="server" CssClass="textBox" MaxLength="3" Width="40px"
                                                Text="EUR" Enabled="false" Font-Bold="true"></asp:TextBox>
                                        </td>
                                        <td align="center" style="width: 100px">
                                            <asp:TextBox ID="txtEur_PRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                TabIndex="6" style="text-align:right" onfocus="this.select();"></asp:TextBox>
                                        </td>
                                        <td align="left" >
                                            <asp:TextBox ID="txtEur_SRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                 style="text-align:right" onfocus="this.select();" TabIndex="7"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 50px">
                                            <asp:TextBox ID="txtCurrJPY" runat="server" CssClass="textBox" MaxLength="3" Width="40px"
                                                Text="JPY" Enabled="false" Font-Bold="true"></asp:TextBox>
                                        </td>
                                        <td align="center" style="width: 100px">
                                            <asp:TextBox ID="txtJPY_PRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                TabIndex="8" style="text-align:right" onfocus="this.select();"></asp:TextBox>
                                        </td>
                                        <td align="left" >
                                            <asp:TextBox ID="txtJPY_SRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                 style="text-align:right" onfocus="this.select();" TabIndex="9"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 50px">
                                            <asp:TextBox ID="txtCurrCHF" runat="server" CssClass="textBox" MaxLength="3" Width="40px"
                                                Text="CHF" Enabled="false" Font-Bold="true"></asp:TextBox>
                                        </td>
                                        <td align="center" style="width: 100px">
                                            <asp:TextBox ID="txtCHF_PRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                TabIndex="10" style="text-align:right" onfocus="this.select();"></asp:TextBox>
                                        </td>
                                      <td align="left" >
                                            <asp:TextBox ID="txtCHF_SRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                 style="text-align:right" onfocus="this.select();" TabIndex="11"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 50px">
                                            <asp:TextBox ID="txtCurrAUD" runat="server" CssClass="textBox" MaxLength="3" Width="40px"
                                                Text="AUD" Enabled="false" Font-Bold="true"></asp:TextBox>
                                        </td>
                                        <td align="center" style="width: 100px">
                                            <asp:TextBox ID="txtAUD_PRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                TabIndex="12" style="text-align:right" onfocus="this.select();"></asp:TextBox>
                                        </td>
                                       <td align="left" >
                                            <asp:TextBox ID="txtAUD_SRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                 style="text-align:right" onfocus="this.select();" TabIndex="13"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 50px">
                                            <asp:TextBox ID="txtCurrCAD" runat="server" CssClass="textBox" MaxLength="3" Width="40px"
                                                Text="CAD" Enabled="false" Font-Bold="true"></asp:TextBox>
                                        </td>
                                        <td align="center" style="width: 100px">
                                            <asp:TextBox ID="txtCAD_PRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                TabIndex="14" style="text-align:right" onfocus="this.select();"></asp:TextBox>
                                        </td>
                                      <td align="left" >
                                            <asp:TextBox ID="txtCAD_SRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                 style="text-align:right" onfocus="this.select();" TabIndex="15"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width:50px">
                                            <asp:TextBox ID="txtCurrSGD" runat="server" CssClass="textBox" MaxLength="3" Width="40px"
                                                Text="SGD" Enabled="false" Font-Bold="true"></asp:TextBox>
                                        </td>
                                        <td align="center" style="width: 100px">
                                            <asp:TextBox ID="txtSGD_PRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                TabIndex="16" style="text-align:right" onfocus="this.select();"></asp:TextBox>
                                        </td>
                                     <td align="left" >
                                            <asp:TextBox ID="txtSGD_SRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                  style="text-align:right" onfocus="this.select();" TabIndex="17"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 50px">
                                            <asp:TextBox ID="txtCurrSEK" runat="server" CssClass="textBox" MaxLength="3" Width="40px"
                                                Text="SEK" Enabled="false" Font-Bold="true"></asp:TextBox>
                                        </td>
                                        <td align="center" style="width: 100px">
                                            <asp:TextBox ID="txtSEK_PRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                TabIndex="18" style="text-align:right" onfocus="this.select();"></asp:TextBox>
                                        </td>
                                        <td align="left" >
                                            <asp:TextBox ID="txtSEK_SRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                  style="text-align:right" onfocus="this.select();" TabIndex="19"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 50px">
                                            <asp:TextBox ID="txtCurrHKD" runat="server" CssClass="textBox" MaxLength="3" Width="40px"
                                                Text="HKD" Enabled="false" Font-Bold="true"></asp:TextBox>
                                        </td>
                                        <td align="center" style="width: 100px">
                                            <asp:TextBox ID="txtHKD_PRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                TabIndex="20" style="text-align:right" onfocus="this.select();"></asp:TextBox>
                                        </td>
                                        <td align="left" >
                                            <asp:TextBox ID="txtHKD_SRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                 style="text-align:right" onfocus="this.select();" TabIndex="21"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 50px">
                                            <asp:TextBox ID="txtCurrSAR" runat="server" CssClass="textBox" MaxLength="3" Width="40px"
                                                Text="SAR" Enabled="false" Font-Bold="true"></asp:TextBox>
                                        </td>
                                        <td align="center" style="width:100px">
                                            <asp:TextBox ID="txtSAR_PRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                TabIndex="22" style="text-align:right" onfocus="this.select();"></asp:TextBox>
                                        </td>
                                       <td align="left" >
                                            <asp:TextBox ID="txtSAR_SRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                style="text-align:right" onfocus="this.select();" TabIndex="23"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 50px">
                                            <asp:TextBox ID="txtCurrDEM" runat="server" CssClass="textBox" MaxLength="3" Width="40px"
                                                Text="DEM" Enabled="false" Font-Bold="true"></asp:TextBox>
                                        </td>
                                        <td align="center" style="width:100px">
                                            <asp:TextBox ID="txtDEM_PRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                TabIndex="24" style="text-align:right" onfocus="this.select();"></asp:TextBox>
                                        </td>
                                       <td align="left" >
                                            <asp:TextBox ID="txtDEM_SRate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                                style="text-align:right" onfocus="this.select();" TabIndex="25"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td align="right" style="width: 120px">
                                        </td>
                                        <td align="left" style="width: 400px; padding-top: 10px; padding-bottom: 10px">
                                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                                ToolTip="Save" TabIndex="24" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                ToolTip="Cancel" TabIndex="25" OnClick="btnCancel_Click" />
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
