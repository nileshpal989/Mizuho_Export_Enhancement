<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_AddEditServiceTaxMaster.aspx.cs"
    Inherits="TF_AddEditServiceTaxMaster" %>

<%@ Register Src="Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico"
        type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    
    <link href="Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script language="javascript" type="text/javascript">
        function onlyNumber(e) {

            var key = e.keyCode;
            var keychar = String.fromCharCode(key);
            var reg = new RegExp("[0-9.]");
            if (key != 13) {
                return reg.test(keychar);
            }
        }
           

            function validateSave() {

                //var srno = document.getElementById('txtSrNo').value;
                var edate = document.getElementById('txtEffectiveDate').value;
                var stax = document.getElementById('txtServiceTax').value;
                var sbcess = document.getElementById('txt_sbcess').value;
                var kkcess = document.getElementById('txt_kkcess').value;

                var edutax = document.getElementById('txtEduCessTax').value;
                var secedutax = document.getElementById('txtSecEduCessTax').value;

//                if (srno == '') {
//                    alert('Enter Serial Number');
//                    document.getElementById('txtSrNo').focus();
//                    return false;
//                }

                if (edate == '' || edate=='__/__/____') {
                    alert('Enter Effective Date');
                    document.getElementById('txtEffectiveDate').focus();
                    return false;
                }
                if (stax == '') {
                    alert('Enter Service Tax');
                    document.getElementById('txtServiceTax').focus();
                    return false;
                }

                if (sbcess == '') {
                    alert('Enter SBCess Tax');
                  document.getElementById('txt_sbcess').focus();
                    return false;
                }

                if (kkcess == '') {
                    alert('Enter KKCess Tax');
                    document.getElementById('txt_kkcess').focus();
                    return false;
                }

                if (edutax == '') {
                    alert('Enter Edu Cess Tax');
                    document.getElementById('txtEduCessTax').focus();
                    return false;
                }
                if (secedutax == '') {
                    alert('Enter Sec Edu Cess Tax ');
                    document.getElementById('txtSecEduCessTax').focus();
                    return false;
                }
            }
        
        
       
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
                    <asp:PostBackTrigger ControlID="btnBack" />
                    <asp:PostBackTrigger ControlID="btnCancel" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel">GST Rate Master Details</span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                    OnClick="btnBack_Click" />
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
                                    <%--<tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">SrNo :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            &nbsp;<asp:TextBox ID="txtSrNo" runat="server" CssClass="textBox" MaxLength="3"
                                                Width="30px" TabIndex="1"></asp:TextBox>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Effective Date :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            &nbsp;<asp:TextBox ID="txtEffectiveDate" runat="server" CssClass="textBox" MaxLength="12"
                                                Width="70px" TabIndex="2"></asp:TextBox>&nbsp;
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
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">GST Rate(%) :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            &nbsp;<asp:TextBox ID="txtServiceTax" runat="server" CssClass="textBox" MaxLength="12"
                                                Width="70px" TabIndex="3"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                    <td align=right>
                                    <span class="mandatoryField">*</span><span class="elementLabel">SBCess (%) :</span>
                                    </td>
                                    <td>
                                    &nbsp;<asp:TextBox ID="txt_sbcess" runat="server" CssClass="textBox" MaxLength="12" Width="70px" TabIndex="4"></asp:TextBox>
                                    </td>
                                    </tr>

                                      <tr>
                                    <td align=right>
                                    <span class="mandatoryField">*</span><span class="elementLabel">KKCess (%) :</span>
                                    </td>
                                    <td>
                                    &nbsp;<asp:TextBox ID="txt_kkcess" runat="server" CssClass="textBox" MaxLength="12" Width="70px" TabIndex="4"></asp:TextBox>
                                    </td>
                                    </tr>

                                    <tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Edu Cess Tax(%) :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            &nbsp;<asp:TextBox ID="txtEduCessTax" runat="server" CssClass="textBox" MaxLength="12"
                                                Width="70px" TabIndex="4"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Sec Edu Cess Tax(%) :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            &nbsp;<asp:TextBox ID="txtSecEduCessTax" runat="server" CssClass="textBox" AutoPostBack="true" MaxLength="12"
                                                Width="70px" TabIndex="5" ontextchanged="txtSecEduCessTax_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Total Tax(%) :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            &nbsp;<asp:TextBox ID="txtTotalTax" runat="server" CssClass="textBox" MaxLength="12"
                                                Width="70px" TabIndex="6"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 400px; padding-top: 10px; padding-bottom: 10px">
                                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                                ToolTip="Save" OnClick="btnSave_Click" TabIndex="7" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                ToolTip="Cancel" OnClick="btnCancel_Click" TabIndex="8" />
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
