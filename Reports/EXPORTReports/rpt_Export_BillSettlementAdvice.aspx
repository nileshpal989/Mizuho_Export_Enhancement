<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Export_BillSettlementAdvice.aspx.cs" Inherits="Reports_EXPORTReports_rpt_Export_BillSettlementAdvice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ register src="../../Menu/Menu.ascx" tagname="Menu" tagprefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="../../Images/favicon.ico"
        type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="../../Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript" src="../../Scripts/Validations.js"></script>
    <script src="../../Scripts/Enable_Disable_Opener.js" language="javascript" type="text/javascript"></script>
    <script>
        function validateGenerate() {
            var Report = document.getElementById('PageHeader').innerHTML;
            var Branch = document.getElementById('ddlBranch').value;
            if (Branch == 0) {
                alert('Select Branch Name.');
                Branch.focus();
                return false;
            }

            var Todocno = document.getElementById('ddlDocumentno');
            if (Todocno.value == '') {
                alert('Enter To Document No..');
                txtToDocumentNo.focus();
                return false;
            }


            rptFrdocno = document.getElementById('ddlDocumentno').value;

            var winname = window.open('ViewExportBillDiscountAdvice.aspx?rptTodocno=' + rptTodocno + '&Branch=' + Branch + '&Report=' + Report, '', 'scrollbars=yes,status=0,menubar=0,left=0,top=50,width=950,height=550');
            winname.focus();
            return false;
        }
   
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
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
                    <asp:PostBackTrigger ControlID="btnSave" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="10" cellpadding="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%;" valign="top" colspan="2">
                                <span class="pageLabel">EXPORT BILL SETTLEMENT ADVICE </span>
                                <asp:Label ID="PageHeader" CssClass="pageLabel" runat="server" Style="font-weight: bold"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 60px;" colspan="2">
                                <span class="mandatoryField">* </span><span class="elementLabel">Branch :</span>
                                &nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" AutoPostBack="true"
                                    Width="120px" runat="server" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                             <td  style="padding-left: 12px;" colspan="2">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Document Date :
                                </span>
                                <asp:TextBox ID="txtDocDate" runat="server" AutoPostBack="true" CssClass="textBox"
                                    Style="width: 70px" TabIndex="2" OnTextChanged="txtDocDate_TextChanged"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="mdDocdate" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtDocDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="btncalendar_DocDate" runat="server" CssClass="btncalendar_enabled"
                                    TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtDocDate" PopupButtonID="btncalendar_DocDate" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdDocdate"
                                    ValidationGroup="dtVal" ControlToValidate="txtDocDate" EmptyValueMessage="Enter Date Value"
                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                            </td>
                            <td  style="padding-left: 10px;" colspan="2">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Sr No :
                                </span>
                                <asp:TextBox ID="txtSrNo" runat="server" AutoPostBack="true" CssClass="textBox" Text="1"
                                    Style="width: 20px" TabIndex="3" OnTextChanged="txtSrNo_TextChanged"></asp:TextBox></td>
                        </tr>
                        <tr style="height: 30px;" valign="bottom">
                            <td style="text-align: left; width: 5%; white-space: nowrap;">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Bill Type : </span>
                                <asp:RadioButton ID="rbAll" runat="server" TabIndex="4" CssClass="elementLabel" Checked="true"
                                    Text="All" GroupName="TransType" OnCheckedChanged="All_CheckedChanged" AutoPostBack="true" />&nbsp;
                                <asp:RadioButton ID="rbBLA" runat="server" TabIndex="5" CssClass="elementLabel" Text="BLA"
                                    GroupName="TransType" OnCheckedChanged="rbBLA_CheckedChanged" AutoPostBack="true" />&nbsp;
                                <asp:RadioButton ID="rbBLU" runat="server" TabIndex="6" CssClass="elementLabel" Text="BLU"
                                    GroupName="TransType" OnCheckedChanged="rbBLU_CheckedChanged" AutoPostBack="true" />&nbsp;
                                <asp:RadioButton ID="rbBBA" runat="server" TabIndex="7" CssClass="elementLabel" Text="BBA"
                                    GroupName="TransType" OnCheckedChanged="rbBBA_CheckedChanged" AutoPostBack="true" />&nbsp;
                                <asp:RadioButton ID="rbBBU" runat="server" TabIndex="8" CssClass="elementLabel" Text="BBU"
                                    GroupName="TransType" AutoPostBack="true" OnCheckedChanged="rbBBU_CheckedChanged" />&nbsp;
                                <asp:RadioButton ID="rbBCA" runat="server" TabIndex="9" CssClass="elementLabel" Text="BCA"
                                    GroupName="TransType" AutoPostBack="true" OnCheckedChanged="rbBCA_CheckedChanged" />&nbsp;
                                <asp:RadioButton ID="rbBCU" runat="server" TabIndex="10" CssClass="elementLabel" Text="BCU"
                                    GroupName="TransType" AutoPostBack="true" OnCheckedChanged="rbBCU_CheckedChanged" />&nbsp;
                            </td>
                        </tr>
                        <br />
                        <tr>
                            <td align="left" nowrap>
                                <span class="mandatoryField">*</span> <span class="elementLabel" runat="server">Document
                                    No: </span>&nbsp;<asp:DropDownList ID="ddlDocumentno" CssClass="dropdownList" TabIndex="11"
                                        AutoPostBack="true" Width="150px" runat="server">
                                    </asp:DropDownList>
                            </td>
                        </tr>
                        <tr valign="bottom">
                            <td align="left" style="width: 700px; padding-top: 10px; padding-left: 80px" valign="bottom">
                                &nbsp;
                                <asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" Text="Generate"
                                    Width="135px" ToolTip="Generate" TabIndex="12" OnClick="btnSave_Click" />
                            </td>
                        </tr>
                    </table>
                    </td>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
</body>
</html>
