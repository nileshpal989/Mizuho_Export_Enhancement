<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EXP_AddEditAcceptedDate.aspx.cs"
    Inherits="EXP_EXP_AddEditAcceptedDate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="../Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
     <title>LMCC-Trade Finance System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="../Images/favicon.ico"
        type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="../Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script language="javascript" type="text/javascript">
        function ValidateSave() {
            var txtAcceptedDueDate = document.getElementById('txtAcceptedDueDate');
            if (txtAcceptedDueDate.value == "") {
                var ans = confirm('Accepted Due Date is blank. Do you want to proceed?');
                if (ans) {
                    return true;
                }
                else {
                    txtAcceptedDueDate.focus();
                    return false;
                }

            }
            return true;
        }
        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            //  alert(charCode);
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }
    </script>
    <script language="javascript" type="text/javascript">

        function checkSysDate(controlID) {

            var obj = controlID;

            if (controlID.value != "__/__/____") {

                var day = obj.value.split("/")[0];

                var month = obj.value.split("/")[1];
                var year = obj.value.split("/")[2];


                var dt = new Date(year, month - 1, day);

                var dateRcvd = document.getElementById('txtDateRcvd');
                var drday = dateRcvd.value.split("/")[0];

                var drMonth = dateRcvd.value.split("/")[1];
                var drYear = dateRcvd.value.split("/")[2];

                var dt1 = new Date(drYear, drMonth - 1, drday);

                if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year) || (dt < dt1)) {

                    alert("Invalid Date");
                    controlID.focus();
                    return false;
                }
            }
        }
    
    </script>
</head>
<body onload="EndRequest();closeWindows();" onunload="closeWindows();">
    <form id="form1" runat="server" autocomplete="off" unselectable="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="../Scripts/Enable_Disable_Opener.js"></script>
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
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
        </center>
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom">
                            <span class="pageLabel">Updation of Accepted Due Date/Agency Commn.</span>
                        </td>
                        <td align="right" style="width: 50%">
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                OnClick="btnBack_Click" TabIndex="7" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%" valign="top" colspan="2">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%" valign="top" colspan="2">
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table cellpadding="0" cellspacing="2" border="0" width="100%">
                                <tr>
                                    <td align="right">
                                        <span class="elementLabel">Doc Type :</span>
                                    </td>
                                    <td align="left" nowrap colspan="1">
                                        <asp:TextBox ID="txtDocType" Width="20px" runat="server" CssClass="textBox" ReadOnly="True"
                                            TabIndex="-1"></asp:TextBox>&nbsp;<asp:Label ID="lblDocumentType" runat="server"
                                                CssClass="elementLabel" Width="150px"></asp:Label>
                                    </td>
                                    <td width="5%" align="right">
                                        <span class="elementLabel">Doc No :</span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtDocumentNo" Width="140px" runat="server" CssClass="textBox" ReadOnly="True"
                                            TabIndex="-1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" nowrap width="20%">
                                        <span class="elementLabel">Customer A/c No. :</span>
                                    </td>
                                    <td nowrap width="10%" align="left">
                                        <asp:TextBox ID="txtCustAcNo" runat="server" CssClass="txtdisabled" MaxLength="6"
                                            TabIndex="-1" Width="70px"></asp:TextBox><asp:Label ID="lblCustomerDesc" runat="server"
                                                CssClass="elementLabel" Width="150px"></asp:Label>
                                    </td>
                                    <td align="right" nowrap>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span class="elementLabel">Date Received :</span>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtDateRcvd" runat="server" CssClass="txtdisabled" MaxLength="10"
                                            ValidationGroup="dtVal" Width="70px" TabIndex="-1"></asp:TextBox>
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                            runat="server" TargetControlID="txtDateRcvd" ErrorTooltipEnabled="True" CultureName="en-GB"
                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                            CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                            CultureTimePlaceholder=":" Enabled="True">
                                        </ajaxToolkit:MaskedEditExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" nowrap>
                                        <span class="elementLabel">Overseas Party ID :</span>
                                    </td>
                                    <td nowrap align="left" colspan="3">
                                        <asp:TextBox ID="txtOverseasPartyID" runat="server" CssClass="txtdisabled" MaxLength="5"
                                            TabIndex="-1" Width="70px"></asp:TextBox><asp:Label ID="lblOverseasPartyDesc" runat="server"
                                                CssClass="elementLabel" Width="200px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="elementLabel">Bill Amount :</span>
                                    </td>
                                    <td nowrap colspan="2" align="left">
                                        <asp:TextBox ID="txtBillAmount" runat="server" CssClass="txtdisabled" Style="text-align: right"
                                            Width="100px" TabIndex="-1" MaxLength="20"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Label
                                                ID="lblCurrency" runat="server" CssClass="elementLabel" Style="font-weight: bold;"
                                                Width="50px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" nowrap>
                                        <span class="elementLabel">Due Date :</span>
                                    </td>
                                    <td align="left" nowrap>
                                        <asp:TextBox ID="txtDueDate" runat="server" CssClass="txtdisabled" MaxLength="10"
                                            ValidationGroup="dtVal" Width="70px" TabIndex="-1"></asp:TextBox>
                                        <ajaxToolkit:MaskedEditExtender ID="mdDoGaurantee" Mask="99/99/9999" MaskType="Date"
                                            runat="server" TargetControlID="txtDueDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                            CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                            CultureTimePlaceholder=":" Enabled="True">
                                        </ajaxToolkit:MaskedEditExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" nowrap width="10%">
                                        <span class="elementLabel">Accepted Due Date :</span>
                                    </td>
                                    <td align="left" nowrap>
                                        <asp:TextBox ID="txtAcceptedDueDate" runat="server" CssClass="textBox" MaxLength="10"
                                            ValidationGroup="dtVal" Width="70px" TabIndex="1"></asp:TextBox>
                                        <ajaxToolkit:MaskedEditExtender ID="mdAccpDuedate" Mask="99/99/9999" MaskType="Date"
                                            runat="server" TargetControlID="txtAcceptedDueDate" ErrorTooltipEnabled="True"
                                            CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                            CultureDateFormat="DMY" CultureDatePlaceholder="/" CultureDecimalPlaceholder="."
                                            CultureThousandsPlaceholder="," CultureTimePlaceholder=":" Enabled="True">
                                        </ajaxToolkit:MaskedEditExtender>
                                        <asp:Button ID="btnCalAccpDueDate" runat="server" CssClass="btncalendar_enabled"
                                            TabIndex="-1" />
                                        <ajaxToolkit:CalendarExtender ID="calAccpDueDate" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtAcceptedDueDate" PopupButtonID="btnCalAccpDueDate" Enabled="True">
                                        </ajaxToolkit:CalendarExtender>
                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="mdAccpDuedate"
                                            ValidationGroup="dtVal" ControlToValidate="txtAcceptedDueDate" EmptyValueMessage="Enter Date Value"
                                            InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" nowrap width="10%">
                                        <span class="elementLabel">Agency Commission Amount :</span>
                                    </td>
                                    <td align="left" nowrap>
                                        <asp:TextBox ID="txtAgencyCommAmt" runat="server" CssClass="textBox" MaxLength="20"
                                            Width="70px" TabIndex="2" Style="text-align: right"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" nowrap width="10%">
                                        <span class="elementLabel">Agency Commission paid Date :</span>
                                    </td>
                                    <td align="left" nowrap>
                                        <asp:TextBox ID="txtACdate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                            Width="70px" TabIndex="3"></asp:TextBox>
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date"
                                            runat="server" TargetControlID="txtACdate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                            CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                            CultureTimePlaceholder=":" Enabled="True">
                                        </ajaxToolkit:MaskedEditExtender>
                                        <asp:Button ID="Button1" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtACdate" PopupButtonID="Button1" Enabled="True">
                                        </ajaxToolkit:CalendarExtender>
                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2"
                                            ValidationGroup="dtVal" ControlToValidate="txtACdate" EmptyValueMessage="Enter Date Value"
                                            InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" nowrap width="10%">
                                        <span class="elementLabel">Agency Commission Remarks :</span>
                                    </td>
                                    <td align="left" nowrap colspan="3">
                                        <asp:TextBox ID="txtACremarks" runat="server" CssClass="textBox" MaxLength="100"
                                            Width="350px" TabIndex="4"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
                <table cellspacing="0" cellpadding="0" border="0" width="400px" style="line-height: 150%">
                    <tr>
                        <td align="right" style="width: 110px">
                        </td>
                        <td align="left" style="width: 290px; padding-top: 10px; padding-bottom: 10px">
                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                ToolTip="Save" OnClick="btnSave_Click" TabIndex="5" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                ToolTip="Cancel" OnClick="btnCancel_Click" TabIndex="6" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
    <script language="javascript" type="text/javascript">
        window.onload = function () {

            var txtBillAmount = document.getElementById('txtBillAmount');
            if (txtBillAmount.value == '')
                txtBillAmount.value = 0;
            txtBillAmount.value = parseFloat(txtBillAmount.value).toFixed(2);

            var txtAgencyCommAmt = document.getElementById('txtAgencyCommAmt');
            if (txtAgencyCommAmt.value == '')
                txtAgencyCommAmt.value = 0;
            txtAgencyCommAmt.value = parseFloat(txtAgencyCommAmt.value).toFixed(2);

        }
    </script>
</body>
</html>
