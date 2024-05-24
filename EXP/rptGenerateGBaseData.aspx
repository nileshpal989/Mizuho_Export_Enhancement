<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptGenerateGBaseData.aspx.cs"
    Inherits="EXP_rptGenerateGBaseData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script type="text/javascript">
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0! 
        var yyyy = today.getFullYear();
        if (dd < 10) { dd = '0' + dd }
        if (mm < 10) { mm = '0' + mm }
        function toDate() {
            if (document.getElementById('txtFromDate').value != "__/__/____") {
                var toDate;
                toDate = dd + '/' + mm + '/' + yyyy;
                document.getElementById('txtToDate').value = toDate;
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
                if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year)) {
                    alert('Invalid ' + CName);
                    controlID.focus();
                    return false;
                }
            }
        }

        function openCustAcNo(e, hNo) {
            var keycode;
            var ddlBranch = document.getElementById('ddlBranch');
            if (keycode == 113 || e == 'mouseClick') {
                open_popup('../TF_CustomerLookUp3.aspx?branchCode=' + ddlBranch.value, 500, 500, 'CustAcNo');
                return false;
            }
            return true;
        }

        function selectCustomer(id) {
            var txtCustomer = document.getElementById('txtCustomer');
            txtCustomer.value = id;
            __doPostBack('txtCustomer');
            return true;
        }


        function validateSave() {
            var ddlBranch = document.getElementById('ddlBranch');
            if (ddlBranch.value == 0) {
                alert('Select Branch Name.');
                ddlBranch.focus();
                return false;
            }
            var fromDate = document.getElementById('txtFromDate');
            if (fromDate.value == '') {
                alert('Select From Date.');
                fromDate.focus();
                return false;
            }
            var toDate = document.getElementById('txtToDate');
            if (toDate.value == '') {
                alert('Select To Date.');
                toDate.focus();
                return false;
            }

        }       
    </script>
</head>
<body>
    <form id="form2" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/Enable_Disable_Opener.js"></script>
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
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <table cellspacing="2" cellpadding="2" border="0" width="100%">
                        <input type="hidden" id="hdnGBASE" runat="server" />
                        <input type="hidden" id="hdnGOGABSE" runat="server" />
                        <input type="hidden" id="hdnIOGBASE" runat="server" />
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom" colspan="2">
                                <span class="pageLabel"><strong>Export GBase Data Excel File Creation</strong></span>
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
                            <td align="right" width="5%">
                                <span class="elementLabel">Branch :</span>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="dropdownList" TabIndex="1" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">From Date :</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                    Width="70px" TabIndex="2"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtFromDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="Button1" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtFromDate" PopupButtonID="Button1" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                                &nbsp;&nbsp; <span class="elementLabel">To Date :</span>
                                <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                    Width="70px" TabIndex="3"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtToDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="Button2" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtToDate" PopupButtonID="Button2" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td width="10%">
                            </td>
                            <td align="left" height="40px">
                                <asp:Button ID="btnCreate" runat="server" CssClass="buttonDefault" Text="Generate"
                                    ToolTip="Generate" TabIndex="7" OnClick="btnCreate_Click" />
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="2" cellpadding="2" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom" colspan="2">
                                <span class="pageLabel"><strong>Export GBase Data Excel File Creation For Settlement</strong></span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100%" valign="top" colspan="2">
                                <asp:Label ID="labelmsgsettlement" runat="server" CssClass="mandatoryField"></asp:Label>
                                <asp:Label ID="lblgabsenorecord" runat="server" CssClass="mandatoryField"></asp:Label>
                                <asp:Label ID="lblgogabsenorecord" runat="server" CssClass="mandatoryField"></asp:Label>
                                <asp:Label ID="lblIOgabsenorecord" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="5%">
                                <span class="elementLabel">Branch :</span>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlbranchsettlement" runat="server" CssClass="dropdownList" TabIndex="1" Enabled="true"/>
                            </td>
                        </tr>
                        <tr>
                            <input type="hidden" id="hdnauthorisedcode" runat="server" />
                            <td align="right" nowrap>
                                <span class="elementLabel">From Date :</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtfromdatesettlement" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                    Width="70px" TabIndex="2"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtfromdatesettlement" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="btnfromdatesettlement" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtfromdatesettlement" PopupButtonID="btnfromdatesettlement" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                                &nbsp;&nbsp; <span class="elementLabel">To Date :</span>
                                <asp:TextBox ID="txttodatesettlement" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                    Width="70px" TabIndex="3"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txttodatesettlement" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="btntodatesettlement" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txttodatesettlement" PopupButtonID="btntodatesettlement" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                    </table>
                     <table width="100%">
                        <tr>
                            <td width="10%">
                            </td>
                            <td align="left" height="40px">
                                <asp:Button ID="btngenretesettlement" runat="server" CssClass="buttonDefault" Text="Generate"
                                    ToolTip="Generate" TabIndex="7" OnClick="btnCreatesettlement_Click" />
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
