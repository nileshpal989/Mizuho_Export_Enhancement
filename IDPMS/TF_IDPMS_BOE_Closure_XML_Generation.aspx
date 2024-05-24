<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IDPMS_BOE_Closure_XML_Generation.aspx.cs"
    Inherits="IDPMS_TF_IDPMS_BOE_Closure_XML_Generation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>LMCC TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <link href="../Style/Style_V2.css" rel="stylesheet" type="text/css" />
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

        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }
        function countsrno() {
            var srno = document.getElementById('txtsrno');
            var srnolen = srno.value.length;
            for (var i = srnolen; i < 2; i++) {

                srno.value = 0 + srno.value;
            }
        }
    
    </script>
    <script language="javascript" type="text/javascript">

        function Generate() {
            var ddlBranch = document.getElementById('ddlBranch').value;
            var txtfromDate = document.getElementById('txtfromDate');
            var txtToDate = document.getElementById('txtToDate');

            var Report = document.getElementById("PageHeader").innerHTML;
            var txtsrno = document.getElementById('txtsrno');
            if (ddlBranch == 0) {
                alert('Select Branch Name');
                ddlBranch.focus();
                return false;
            }
            if (txtfromDate.value == '') {
                alert('Select From Date.');
                txtfromDate.focus();
                return false;
            }
            if (txtToDate.value == '') {
                alert('Select To Date.');
                txtToDate.focus();
                return false;
            }


        }

    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" atomicselection="false">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script src="../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script src="../Scripts/InitEndRequest.js" type="text/javascript"></script>
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
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
            <ContentTemplate>
                <table cellspacing="5" border="0" width="100%">
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom">
                            <asp:Label ID="PageHeader" CssClass="pageLabel" runat="server"><strong>IDPMS XML File Generation-BOE Closure</strong></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 100%" valign="top">
                            <asp:Label ID="lblmessage" runat="server" CssClass="mandatoryField"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top">
                            <table cellspacing="1" cellpadding="0" border="0" width="100%" style="line-height: 150%">
                                <tr>
                                    <td style="width: 12%; text-align: right; white-space: nowrap">
                                        <span class="mandatoryField">*</span><span class="elementLabel">Branch :</span>
                                    </td>
                                    <td style="text-align: left; white-space: nowrap">
                                        <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" AutoPostBack="true"
                                            Width="100px" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; white-space: nowrap">
                                        <span class="mandatoryField">*</span><span class="elementLabel">From Document Date :</span>
                                    </td>
                                    <td style="width: 12%; text-align: left; white-space: nowrap">
                                        <asp:TextBox ID="txtfromDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtval"
                                            Width="70" TabIndex="2"></asp:TextBox>
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
                                    <td style="text-align: left; white-space: nowrap">
                                        <span class="mandatoryField">*</span><span class="elementLabel">To Document Date :</span>
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10" Width="70"
                                            TabIndex="3"></asp:TextBox>
                                        <asp:Button ID="btncalendar_ToDate" runat="server" CssClass="btncalendar_enabled" />
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                            runat="server" TargetControlID="txtToDate" InputDirection="RightToLeft" AcceptNegative="Left"
                                            ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                        </ajaxToolkit:MaskedEditExtender>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtToDate" PopupButtonID="btncalendar_ToDate">
                                        </ajaxToolkit:CalendarExtender>
                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                            ValidationGroup="dtVal" ControlToValidate="txtToDate" EmptyValueMessage="Enter Date Value"
                                            InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                        </ajaxToolkit:MaskedEditValidator>
                                    </td>
                                </tr>
                                <tr style="padding: 5px 5px 5px 5px;" valign="bottom">
                                    <td>
                                    </td>
                                    <td style="text-align: left; white-space: nowrap; padding: 5px 0px 5px 0px" colspan="2">
                                        <asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" Text="Genarate XML File"
                                            ToolTip="Genarate XML File" TabIndex="9" Width="120px" OnClick="btnSave_Click" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnSaveCan" runat="server" CssClass="buttonDefault" Text="Genarate Cancel XML File"
                                            ToolTip="Genarate XML File" TabIndex="9" Width="145px" OnClick="btnSaveCan_Click" />
                                        <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                            Style="visibility: hidden" Text="Button" UseSubmitBehavior="false" />
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
