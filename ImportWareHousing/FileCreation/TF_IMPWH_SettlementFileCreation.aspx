<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMPWH_SettlementFileCreation.aspx.cs" Inherits="ImportWareHousing_FileCreation_TF_IMPWH_SettlementFileCreation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/Style_new.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function CustHelp() {
            popup = window.open('../HelpForms/TF_IMPWH_ImportCustomer.aspx', 'CustHelp', 'height=500,  width=500,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "CustHelp";
            return false;
        }
        function selectCustomer(Cust, label) {
            document.getElementById('txtCustACNo').value = Cust;
            document.getElementById('lblcustname').innerHTML = label;
        }
        function Validate() {

            if (document.getElementById('txtFromDate').value == '') {
                alert('Enter as on date.');
                document.getElementById('txtFromDate').focus();
                return false;
            }

            if (document.getElementById('txtCustACNo').value == '') {
                alert('Enter Cust Account Number.');
                document.getElementById('txtCustACNo').focus();
                return false;
            }

        }
    </script>
    <style type="text/css">
        hr
        {
            display: block;
            margin-top: 0.5em;
            margin-bottom: 0.5em;
            margin-left: auto;
            margin-right: auto;
            border-style: inset;
            border-width: 2px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" unselectable="off">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script src="../../Scripts/InitEndRequest.js" type="text/javascript"></script>
    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
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
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnCreate" />
                    <asp:PostBackTrigger ControlID="lnkDownload" />
                </Triggers>
                <ContentTemplate>
                    <input type="hidden" id="hdnFileName" runat="server" />
                    <table cellspacing="2" cellpadding="2" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                &nbsp;<asp:Label ID="lblPaheHeader" runat="server" CssClass="pageLabel" Font-Bold="true"></asp:Label>
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
                            <td align="left" style="width: 100%;" valign="top">
                                <table cellspacing="0" cellpadding="0" border="0" width="1300px" style="line-height: 150%">
                                    <tr>
                                        <td style="width: 10%" align="right">
                                            <span class="elementLabel">Branch :</span>
                                        </td>
                                        <td align="left" style="width: 700px">
                                            &nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" AutoPostBack="true"
                                                runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 10%">
                                            <span class="mandatoryField">*</span> <span class="elementLabel">As On Date :</span>
                                        </td>
                                        <td align="left" style="width: 700px">
                                            &nbsp;<asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10"
                                                ValidationGroup="dtVal" Width="70" TabIndex="2"></asp:TextBox>
                                            <asp:Button ID="btncalendar_FromDate" runat="server" CssClass="btncalendar_enabled"
                                                TabIndex="-1" />
                                            <ajaxToolkit:MaskedEditExtender ID="mdfdate" Mask="99/99/9999" MaskType="Date" runat="server"
                                                TargetControlID="txtFromDate" InputDirection="RightToLeft" AcceptNegative="Left"
                                                ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="btncalendar_FromDate">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdfdate"
                                                ValidationGroup="dtVal" ControlToValidate="txtFromDate" EmptyValueMessage="Enter Date Value"
                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                            </ajaxToolkit:MaskedEditValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap style="width: 10%">
                                            <span class="mandatoryField">&nbsp; *</span><span class="elementLabel">Customer A/C
                                                :</span>
                                        </td>
                                        <td align="left" nowrap style="width: 50%;">
                                            &nbsp;<asp:TextBox ID="txtCustACNo" runat="server" CssClass="textBox" MaxLength="20"
                                                TabIndex="2" ToolTip="Press F2 for Help" Width="100" AutoPostBack="true" OnTextChanged="txtCustACNo_TextChanged"></asp:TextBox>
                                            <asp:Button ID="btnCustHelp" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                                OnClientClick="return CustHelp();" />
                                            <asp:Label ID="lblcustname" runat="server" CssClass="elementLabel" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table cellspacing="0" cellpadding="0" border="0" width="1300px" style="line-height: 150%">
                                    <tr>
                                        <td style="width: 10%">
                                        </td>
                                        <td align="left" nowrap style="width: 50%;">
                                            &nbsp;<asp:Button ID="btnCreate" runat="server" CssClass="buttonDefault" OnClick="btnCreate_Click"
                                                TabIndex="3" Text="Create File" ToolTip="Create File" />
                                            <asp:Label ID="lblPath" runat="server" CssClass="mandatoryField"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table cellspacing="0" cellpadding="0" border="0" width="1300px" style="line-height: 150%">
                                    <tr>
                                        <td style="width: 10%">
                                        </td>
                                        <td align="left" nowrap style="width: 50%;">
                                            &nbsp;
                                            <asp:Label ID="lblbop6name" runat="server" CssClass="pageLabel" Font-Bold="true"></asp:Label>&nbsp;
                                            <asp:LinkButton ID="lnkDownload" runat="server" Visible="false" Text="Download File"
                                                CssClass="buttonDefault" OnClick="lnkDownload_Click"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top">
                                <hr />
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

