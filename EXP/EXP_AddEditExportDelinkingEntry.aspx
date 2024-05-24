<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EXP_AddEditExportDelinkingEntry.aspx.cs"
    Inherits="EXP_EXP_AddEditExportDelinkingEntry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script type="text/javascript" language="javascript">

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

        function validate_Number(evnt) {

            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            // alert(charCode);
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }

        function Exrate() {

            var lc = document.getElementById('txtDelinkedExRt');
            if (lc.value == '') {
                lc.value = 0;

            }
            lc.value = parseFloat(lc.value).toFixed(10);
            

        }
    </script>
    <style type="text/css">
        .style2
        {
            width: 587px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
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
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="conditional">
            <ContentTemplate>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom">
                            <span class="pageLabel">Export Document Delinking</span>
                        </td>
                        <td align="right" style="width: 50%">
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                OnClick="btnBack_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%;" valign="top">
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="right" width="15%" nowrap>
                            <span class="pageLabel">Document No :</span>
                        </td>
                        <td align="left" nowrap width="30%">
                            <asp:TextBox ID="txtDocNo" runat="server" AutoPostBack="True" CssClass="textBox"
                                MaxLength="20" TabIndex="-1" Width="150px" OnTextChanged="txtDocNo_TextChanged"
                                Enabled="false"></asp:TextBox>
                        </td>
                        <td align="left" nowrap width="10%">
                            <asp:RadioButton ID="rdbSight" Text="Sight" CssClass="elementLabel" runat="server"
                                GroupName="SU" Checked="True" TabIndex="1" Visible="false" />
                            <asp:RadioButton ID="rdbUsance" Text="Usance" CssClass="elementLabel" runat="server"
                                GroupName="SU" TabIndex="1" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="elementLabel">Customer A/c No :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtCustAcNo" runat="server" CssClass="textBox" Width="70px" 
                                Enabled="false" ontextchanged="txtCustAcNo_TextChanged"></asp:TextBox>
                            <asp:Label ID="lblCustname" runat="server" CssClass="elementLabel"></asp:Label>
                        </td>
                        <td align="right" nowrap>
                            <span class="elementLabel">Date Received :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtDateRecieved" runat="server" CssClass="textBox" Width="70px"
                                Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="elementLabel">Overseas Party Id :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtOverseasParty" runat="server" CssClass="textBox" Width="70px"
                                Enabled="false" ontextchanged="txtOverseasParty_TextChanged"></asp:TextBox>
                            <asp:Label ID="lblOverseasParty" runat="server" CssClass="elementLabel"></asp:Label>
                        </td>
                        <td align="right" nowrap>
                            <span class="elementLabel">Date Negotiated :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtNegoDt" runat="server" CssClass="textBox" Width="70px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="elementLabel">Overseas Bank Id :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtOverseasBank" runat="server" CssClass="textBox" Width="70px"
                                Enabled="false" ontextchanged="txtOverseasBank_TextChanged"></asp:TextBox>
                            <asp:Label ID="lblOverseasBank" runat="server" CssClass="elementLabel"></asp:Label>
                        </td>
                        <td align="right" nowrap>
                            <span class="elementLabel">Due Date :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtDueDt" runat="server" CssClass="textBox" Width="70px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="elementLabel">Cur :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtCur" runat="server" CssClass="textBox" Width="70px" Enabled="false"></asp:TextBox>
                            &nbsp;&nbsp; <span class="elementLabel">Other Cur :</span>
                            <asp:TextBox ID="txtotherCur" runat="server" CssClass="textBox" Width="70px" Enabled="false"></asp:TextBox>
                        </td>
                        <td align="right" nowrap>
                            <span class="elementLabel">Exchage Rate :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtExRt" runat="server" CssClass="textBox" Width="70px" Enabled="false" Style="text-align: right;" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="elementLabel">Nego. Amt :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtNegoAmt" runat="server" CssClass="textBox" Width="100px" Enabled="false"  Style="text-align: right;"></asp:TextBox>
                        </td>
                        <td align="right" nowrap>
                            <span class="elementLabel">Nego. Amt in Rs :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtNegoAmtRs" runat="server" CssClass="textBox" Width="100px" Enabled="false" Style="text-align: right;" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="elementLabel">Bill Amt :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtBillAmt" runat="server" CssClass="textBox" Width="100px" Enabled="false" Style="text-align: right;" ></asp:TextBox>
                        </td>
                        <td align="right" nowrap>
                            <span class="elementLabel">Bill Amt in Rs :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtBillAmtRs" runat="server" CssClass="textBox" Width="100px" Enabled="false" Style="text-align: right;" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="elementLabel">Date Delinked :</span>
                        </td>
                        <td width="10%"  align="left" nowrap>
                            <asp:TextBox ID="txtDateDelinked" runat="server" CssClass="textBox" MaxLength="10"
                                ValidationGroup="dtVal" Width="70px" TabIndex="1" 
                                style="background-color:Yellow;" AutoPostBack = "true" ontextchanged="txtDateDelinked_TextChanged" ></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="mdDateDelinked" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtDateDelinked" ErrorTooltipEnabled="True" CultureName="en-GB"
                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="btnDelinkedDt" runat="server" Visible="false" CssClass="btncalendar_enabled"
                                TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="calDraftDoc" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtDateDelinked" PopupButtonID="btnDelinkedDt" Enabled="false">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="mdDateDelinked"
                                ValidationGroup="dtVal" ControlToValidate="txtDateDelinked" EmptyValueMessage="Enter Date Value"
                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                        </td>
                        <td align="right" nowrap>
                            <span class="elementLabel">Delinked Exch Rate :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtDelinkedExRt" runat="server" CssClass="textBox" 
                                Width="100px" Enabled="true"
                                MaxLength="9" TabIndex="2" ontextchanged="txtDelinkedExRt_TextChanged" AutoPostBack = "true"  Style ="text-align: right; background-color: yellow;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="elementLabel">Interest Rate :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtIntRate" runat="server" CssClass="textBox" Width="100px" Enabled="false"></asp:TextBox>
                        </td>
                        <td align="right" nowrap>
                            <span class="elementLabel">For :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtDays" runat="server" CssClass="textBox" Width="100px" Enabled="false"></asp:TextBox>
                            <span class="elementLabel">Days</span>
                        </td>
                    </tr>
                </table>
                <table cellspacing="0" border="0" width="50%">
                    <tr>
                        <td width="30%" nowrap>
                        </td>
                        <td align="right" nowrap width="30%">
                            <span class="elementLabel">Amt In </span>
                            <asp:Label ID="lblCur" runat="server" CssClass="elementLabel"></asp:Label>
                        </td>
                        <td align="right">
                            <span class="elementLabel">Amt In Rs.</span>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap align="right">
                            <span class="elementLabel">Negotiated Amount :</span>
                        </td>
                        <td align="right" nowrap>
                            <asp:TextBox ID="txtNegoAmtInt" runat="server" CssClass="textBox" Width="150px" Enabled="false"></asp:TextBox>
                        </td>
                        <td align="right" nowrap>
                            <asp:TextBox ID="txtNegoAmtRsInt" runat="server" CssClass="textBox" Width="150px"
                                Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap align="right">
                            <span class="elementLabel">Overdue Interest :</span>
                        </td>
                        <td align="right" nowrap>
                            <asp:TextBox ID="txtODInt" runat="server" CssClass="textBox" Width="150px" Enabled="false"></asp:TextBox>
                        </td>
                        <td align="right" nowrap>
                            <asp:TextBox ID="txtODIntRs" runat="server" CssClass="textBox" Width="150px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap align="right">
                            <span class="elementLabel">Interest Already Charged :</span>
                        </td>
                        <td align="right" nowrap>
                            <asp:TextBox ID="txtIAC" runat="server" CssClass="textBox" Width="150px" Enabled="false"></asp:TextBox>
                        </td>
                        <td align="right" nowrap>
                            <asp:TextBox ID="txtIACRs" runat="server" CssClass="textBox" Width="150px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap align="right">
                            <span class="elementLabel">Balance Interest :</span>
                        </td>
                        <td align="right" nowrap>
                        </td>
                        <td align="right" nowrap>
                            <asp:TextBox ID="txtBalInt" runat="server" CssClass="textBox" Width="150px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault" ToolTip="Save"
                                OnClick="btnSave_Click" TabIndex="3" />
                        </td>
                        <td>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault" ToolTip="Save"
                                OnClick="btnCancel_Click" TabIndex="4" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
