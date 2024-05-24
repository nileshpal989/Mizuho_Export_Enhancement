<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDPMS_AddEdit_EDPMSData_Realisation.aspx.cs"
    Inherits="EDPMS_EDPMS_AddEdit_EDPMSData_Realisation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script language="javascript" type="text/javascript">
        function openCountryCode(e, hNo) {
            var keycode;
            if (keycode == 113 || e == 'mouseClick') {
                open_popup('../TF_CountryLookUp1.aspx?hNo=' + hNo, 500, 500, 'purposeid');
                return false;
            }
            return true;
        }

        function selectCountry(id, hNo) {
            var txtRemmiterCountry = document.getElementById('txtRemmiterCountry');
            if (hNo == '1') {
                txtRemmiterCountry.value = id;
                __doPostBack('txtRemmiterCountry', '');
                return true;
            }
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
                if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year) || (dt > today)) {

                    alert('Invalid ' + CName);
                    controlID.focus();
                    return false;
                }
            }
        }


        function validateSave() {
            var txtRemmiterCountry = document.getElementById('txtRemmiterCountry');
            var lblCountryDesc = document.getElementById('lblCountryDesc');
            if (lblCountryDesc.innerHTML == "") {
                alert('Invalid Country');
                txtRemmiterCountry.focus();
                return false;
            }

            var ddlExportType = document.getElementById('ddlExportType');
            var ddlExportAgency = document.getElementById('ddlExportAgency');
            var ddlDirectDispatch = document.getElementById('ddlDirectDispatch');
            var ddlCurrency = document.getElementById('ddlCurrency');
            var ddlPortCode = document.getElementById('ddlPortCode');

            if (ddlPortCode.value == "0") {
                alert('Select Port Code');
                ddlPortCode.focus();
                return false;
            }

            if (ddlCurrency.value == "0") {
                alert('Select Currency');
                ddlCurrency.focus();
                return false;
            }

            if (ddlExportType.value == "0") {
                alert('Select Export Type');
                ddlExportType.focus();
                return false;
            }
            if (ddlExportAgency.value == "0") {
                alert('Select Export Type');
                ddlExportAgency.focus();
                return false;
            }
            if (ddlDirectDispatch.value == "0") {
                alert('Select Direct Dispatch');
                ddlDirectDispatch.focus();
                return false;
            }        
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>
    <script src="../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
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
        <center>
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="conditional">
                <ContentTemplate>
                    <table border="0" width="100%" cellpadding="2" cellspacing="2">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel"><strong>Updation Of Error Records - Payment Realisation</strong> </span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100%" valign="top">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top">
                                <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 150%">
                                    <tr>
                                        <td align="right" style="width: 15%">
                                            <span class="elementLabel">Branch :</span>
                                        </td>
                                        <td align="left" style="width: 10%">
                                            <asp:TextBox ID="txtBranch" runat="server" CssClass="textBox" Width="100px" TabIndex="-1"
                                                AutoPostBack="true" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 15%">
                                            <span class="elementLabel">Shipping Bill No :</span>
                                        </td>
                                        <td align="left" style="width: 10%">
                                            <asp:TextBox ID="txtShippingBillNo" runat="server" CssClass="textBox" Width="150px"
                                                TabIndex="-1" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right; width: 15%">
                                            <span class="elementLabel">Shipping Date :</span>
                                        </td>
                                        <td style="text-align: left; width: 15%">
                                            <asp:TextBox ID="txtShippingDate" runat="server" CssClass="textBox" Width="70px"
                                                TabIndex="1"></asp:TextBox>
                                            <asp:Button ID="btncalendar_FromDate" runat="server" CssClass="btncalendar_enabled"
                                                TabIndex="-1" />
                                            <ajaxToolkit:MaskedEditExtender ID="mdfdate" Mask="99/99/9999" MaskType="Date" runat="server"
                                                TargetControlID="txtShippingDate" InputDirection="RightToLeft" AcceptNegative="Left"
                                                ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <ajaxToolkit:CalendarExtender ID="calendarShipDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtShippingDate" PopupButtonID="btncalendar_FromDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                        <td align="right">
                                            <span class="elementLabel">Port Code : </span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlPortCode" CssClass="dropdownList" runat="server"  TabIndex="2">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>      <td align="right">
                                            <span class="elementLabel">Invoice No :</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtInvoiceNo" runat="server"  Width="150px" CssClass="textBox" Enabled="false" MaxLength="25" TabIndex="3"></asp:TextBox>
                                            </td>
                                        <td align="right">
                                            <span class="elementLabel">Invoice Sr. No :</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtInvoiceSrNo" runat="server" CssClass="textBox"  TabIndex="4" MaxLength="25"></asp:TextBox>
                                        </td>
                                  
                                        <td style="text-align: right; width: 10%">
                                            <span class="elementLabel">Invoice Date :</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtInvoiceDate" runat="server" CssClass="textBox" Width="70px"  TabIndex="5"></asp:TextBox>
                                            <asp:Button ID="btnInvoiceDate" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" Mask="99/99/9999" MaskType="Date"
                                                runat="server" TargetControlID="txtInvoiceDate" InputDirection="RightToLeft"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left"
                                                PromptCharacter="_">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtInvoiceDate" PopupButtonID="btnInvoiceDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <span class="elementLabel">AD Bill No :</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtADBillNo" runat="server" CssClass="textBox" Width="150px" MaxLength="18"
                                                TabIndex="6"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right" nowrap>
                                            <span class="elementLabel">Negotiation Date :</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNegotiationDate" runat="server" CssClass="textBox" Width="70px"
                                                TabIndex="7"></asp:TextBox>
                                            <asp:Button ID="Button1" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                                runat="server" TargetControlID="txtNegotiationDate" InputDirection="RightToLeft"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left"
                                                PromptCharacter="_">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtNegotiationDate" PopupButtonID="Button1">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <span class="elementLabel">Export Type :</span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlExportType" runat="server" CssClass="dropdownList" TabIndex="8">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                <asp:ListItem Value="1">Goods</asp:ListItem>
                                                <asp:ListItem Value="2">Softex</asp:ListItem>
                                                <asp:ListItem Value="3">Royalty</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: right">
                                            <span class="elementLabel">Form No :</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFormNo" runat="server" CssClass="textBox"  MaxLength="20"
                                                TabIndex="9"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <span class="elementLabel">IE Code :</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtIECode" runat="server" CssClass="textBox" Width="100px" MaxLength="15"
                                                TabIndex="10"></asp:TextBox>
                                        </td>
                                         <td align="right">
                                            <span class="elementLabel">EBRC No : </span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEBRCNo" runat="server" CssClass="textBox" MaxLength="20"  TabIndex="11"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <span class="elementLabel">Export Agency :</span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlExportAgency" runat="server" CssClass="dropdownList" TabIndex="12">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                <asp:ListItem Value="1">Customs</asp:ListItem>
                                                <asp:ListItem Value="2">SEZ</asp:ListItem>
                                                <asp:ListItem Value="3">STPI</asp:ListItem>
                                                <asp:ListItem Value="4">Status Holder Exporters</asp:ListItem>
                                                <asp:ListItem Value="5">100% EOU</asp:ListItem>
                                                <asp:ListItem Value="6">Warehouse Export</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: right">
                                            <span class="elementLabel">Direct Dispatch :</span>
                                        </td>
                                        <td colspan="3">
                                            <asp:DropDownList ID="ddlDirectDispatch" runat="server" CssClass="dropdownList" TabIndex="13">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                <asp:ListItem Value="1">Dispatched Directly By Exporter</asp:ListItem>
                                                <asp:ListItem Value="2">By Bank </asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Realised Currency :</span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="dropdownList"
                                                TabIndex="14">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: right" nowrap>
                                            <span class="elementLabel">Realised Date :</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRealisedDate" runat="server" CssClass="textBox" Width="70px"  TabIndex="15"></asp:TextBox>
                                            <asp:Button ID="btnRealisedDate" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date"
                                                runat="server" TargetControlID="txtRealisedDate" InputDirection="RightToLeft"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left"
                                                PromptCharacter="_">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtRealisedDate" PopupButtonID="btnRealisedDate">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <span class="elementLabel">Remitter Name :</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRemmiterName" runat="server" CssClass="textBox" Width="250px"
                                                TabIndex="16" MaxLength="40"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right" nowrap>
                                            <span class="elementLabel">Remitter Country :</span>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtRemmiterCountry" runat="server" CssClass="textBox" Width="30px"
                                                TabIndex="17" ontextchanged="txtRemmiterCountry_TextChanged" AutoPostBack="true" MaxLength="2"></asp:TextBox>
                                                 <asp:Button ID="Button4" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                OnClientClick="return openCountryCode('mouseClick','1');" />
                            &nbsp;<asp:Label ID="lblCountryDesc" runat="server" CssClass="elementLabel"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td align="right" style="width: 100px">
                                        </td>
                                        <td align="left" style="width: 400px; padding-top: 10px; padding-bottom: 10px">
                                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                                ToolTip="Save" TabIndex="18" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                ToolTip="Cancel" TabIndex="19" OnClick="btnCancel_Click" />
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
