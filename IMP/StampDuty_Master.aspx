<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StampDuty_Master.aspx.cs"
    Inherits="IMP_StampDuty_Master" %>

<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <link href="../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <script src="../Help_Plugins/jquerynew.min.js" language="javascript" type="text/javascript"></script>
    <script src="../Help_Plugins/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <link href="../Help_Plugins/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Help_Plugins/AlertJquery.js" type="text/javascript"></script>
      <script src="../Help_Plugins/MyJquery1.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function validate_Number(evnt) {

            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            //  alert(charCode);
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }
        function validateSave() {

            var txtEffectiveDate = document.getElementById('txtEffectiveDate');
            var txtStampID = document.getElementById('txtStampID');
            var txtDesc = document.getElementById('txtDesc');
            var txtTenor = document.getElementById('txtTenor');
            var txtRates = document.getElementById('txtRates');

            if (txtEffectiveDate.value == "" || txtEffectiveDate == "__/__/____") {
//                alert("Enter effective date.");
                //                txtEffectiveDate.focus();
                VAlert('Enter effective date.', '#txtEffectiveDate');
                return false;
            }
            if (txtStampID.value == "") {
//                alert("Enter stamp id.");
                //                txtStampID.focus();
                VAlert('Enter stamp id.', '#txtStampID');
                return false;
            }
            if (txtDesc.value == '') {

//                alert('Enter description.');
//                txtDesc.focus();
                VAlert('Enter description.', '#txtDesc');
                return false;
            }
            if (txtTenor.value == '') {
//                alert('Enter Tenor.');
//                txtTenor.focus();
                VAlert('Enter Tenor.', '#txtTenor');
                return false;
            }
            if (txtRates.value == '') {
//                alert('Enter rates.');
//                txtRates.focus();
                VAlert('Enter rates.', '#txtRates');
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
     <div id="dialog" class="AlertJqueryHide">
        <p id="Paragraph">
        </p>
    </div>
    <div>
        <uc1:Menu ID="Menu1" runat="server" />
        <br />
        <br />
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom">
                        <input type="hidden" runat="server" id="hdnstampdutydescription" />
                         <input type="hidden" runat="server" id="hdntenior" />
                          <input type="hidden" runat="server" id="hdnrate" />
                            <span class="pageLabel"><strong>Stamp Duty Charges</strong></span>
                        </td>
                        <td align="right" style="width: 50%">
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                OnClick="btnBack_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 100%" valign="top" colspan="2">
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="2">
                            <table cellpadding="0" cellspacing="0" width="600px" style="line-height: 150%">
                                <tr>
                                    <td align="right" width="10%" nowrap>
                                        <span class="elementLabel">Effective Date :</span>
                                    </td>
                                    <td align="left" width="40px">
                                        &nbsp;<asp:TextBox ID="txtEffectiveDate" runat="server" CssClass="textBox" MaxLength="12"
                                            Width="70px" AutoPostBack="true" TabIndex="1" ontextchanged="txtEffectiveDate_TextChanged"></asp:TextBox>
                                        <ajaxToolkit:MaskedEditExtender ID="mdDocdate" runat="server" CultureAMPMPlaceholder="AM;PM"
                                            CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY" CultureDatePlaceholder="/"
                                            CultureDecimalPlaceholder="." CultureName="en-GB" CultureThousandsPlaceholder=","
                                            CultureTimePlaceholder=":" Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999"
                                            MaskType="Date" TargetControlID="txtEffectiveDate">
                                        </ajaxToolkit:MaskedEditExtender>
                                        <asp:Button ID="btncalendar_DocDate" runat="server" CssClass="btncalendar_enabled"
                                            TabIndex="-1" />
                                        <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Enabled="True"
                                            Format="dd/MM/yyyy" PopupButtonID="btncalendar_DocDate" TargetControlID="txtEffectiveDate">
                                        </ajaxToolkit:CalendarExtender>
                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdDocdate"
                                            ControlToValidate="txtEffectiveDate" EmptyValueBlurredText="*" EmptyValueMessage="Enter Date Value"
                                            ErrorMessage="Invalid" InvalidValueBlurredMessage="Date is invalid" ValidationGroup="dtVal"></ajaxToolkit:MaskedEditValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="10%" nowrap>
                                        <span class="elementLabel">ID :</span>
                                    </td>
                                    <td align="left" width="40%">
                                        &nbsp;<asp:TextBox ID="txtStampID" runat="server" CssClass="textBox" Style="width: 40px"
                                            TabIndex="2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="elementLabel">Description :</span>
                                    </td>
                                    <td align="left" width="40px">
                                        &nbsp;<asp:TextBox ID="txtDesc" runat="server" CssClass="textBox" Style="width: 96%"
                                            TabIndex="3"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="elementLabel">Tenor :</span>
                                    </td>
                                    <td align="left" width="40px">
                                        &nbsp;<asp:TextBox ID="txtTenor" runat="server" CssClass="textBox" Style="width: 40px"
                                            MaxLength="3" TabIndex="4"></asp:TextBox>
                                        <span class="elementLabel">Days</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="elementLabel">Upto Tenor :</span>
                                    </td>
                                    <td align="left" width="40px">
                                        &nbsp;<asp:TextBox ID="txtUptoTenor" runat="server" CssClass="textBox" Style="width: 40px"
                                            MaxLength="3" TabIndex="4"></asp:TextBox>
                                        <span class="elementLabel">Days</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="elementLabel">Rates :</span>
                                    </td>
                                    <td align="left" width="40px">
                                        &nbsp;<asp:TextBox ID="txtRates" runat="server" CssClass="textBox" Style="width: 40px"
                                            TabIndex="5"></asp:TextBox>
                                        <span class="elementLabel">%</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 200px">
                                    </td>
                                    <td align="left" style="width: 400px; padding-top: 10px; padding-bottom: 10px">
                                        &nbsp<asp:Button ID="btnSave" Text="Save" runat="server" ToolTip="Save" CssClass="buttonDefault"
                                            TabIndex="6" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="buttonDefault"
                                            TabIndex="7" />
                                        <%--<asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                    Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
