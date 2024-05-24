<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Exp_SwiftChecker.aspx.cs"
    Inherits="EXP_Exp_SwiftChecker" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=11" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <title>LMCC-TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <%--<link href="../Style/TAB.css" rel="stylesheet" type="text/css" />--%>
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Scripts/jquery-1.8.3.min.js" type="text/javascript""></script>
    <script src="../Help_Plugins/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <link href="../Help_Plugins/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Help_Plugins/MyJquery1.js" type="text/javascript"></script>
    
    <script language="javascript" type="text/javascript">
        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }

        function DialogAlert()
        {
    var divO = { id: "divO", css: { "width": "100%" } };
    var divA = {id: "dialog",};
    var para = {id: "Paragraph"};
    var table1 = { id: "tblDialog", css: { "width": "100%" } };
    var Para1 = $("<p>", para);
    var div1 = $("<div>", divO);
    var dialog = $("<div>", divA);
    var tablea = $("<table>", table1);
    var tr1 = "<tr><td width='20%' align='right'><span id='lblReason' class='elementLabel' style='font-weight:bold'>Reason: </span></td><td width='80%'><textarea id='txtRejectReason' cols='40' rows='4' maxlength='200'></textarea></td></tr><tr><td colspan='2' align='left'><span class='mandatoryField' id='spnDialog'>Max 200 char</span></td><tr>";

    $("body").append(div1);
    $("#divO").append(dialog);
    $("#dialog").append(Para1);
    $("#dialog").append(tablea);
    $("#tblDialog").append(tr1);

    var _ddlApproveReject = $("#ddlApproveReject");
    if (_ddlApproveReject.val() == '0') {
        alert('Select Reject or Approve.');
        _ddlApproveReject.focus();
        return false;
    }
    if (_ddlApproveReject.val() == '1') {
        $("#txtRejectReason").hide();
        $("#spnDialog").hide();
        $("#Paragraph").text("Do you want to approve transaction?");
        $("#lblReason").hide();
        $("#dialog").dialog({
            title: "Confirm",
            width: 400,
            modal: true,
            closeOnEscape: true,
            dialogClass: "AlertJqueryDisplay",
            hide: { effect: "explode", duration: 400 },
            buttons: [
            {
                text: "Yes", //"✔"
                icon: "ui-icon-heart",
                click: function () {
                    $(this).dialog("close");
                    document.getElementById('btnSave').click();
                }
            },
            {
                text: "No", //"✖"
                icon: "ui-icon-heart",
                click: function () {
                    $(this).dialog("close");
                    $("#ddlApproveReject").val('-Select-')
                    return false;
                }
            }
            ]
        });
        $('.ui-dialog :button').blur();
    }
    if (_ddlApproveReject.val() == '2') {
        $("#txtRejectReason").val($("#hdnRejectReason").val());
        $("#txtRejectReason").show();
        $("#spnDialog").show();
        $("#Paragraph").text("Are you sure you want to reject transaction?");
        $("#lblReason").show();
        $("#dialog").dialog({
            title: "Confirm",
            width: 500,
            modal: true,
            closeOnEscape: true,
            dialogClass: "AlertJqueryDisplay",
            hide: { effect: "explode", duration: 400 },
            buttons: [
            {
                text: "Yes", //"✔"
                icon: "ui-icon-heart",
                click: function () {
                    if ($("#txtRejectReason").val().trim() != '') {
                        $(this).dialog("close");
                        $("#hdnRejectReason").val($("#txtRejectReason").val());
                        $("#txtRejectReason").val('');
                        document.getElementById('btnSave').click();
                    }
                    else {
                        alert("Reject reason can not be blank.");
                        $("#txtRejectReason").focus();
                        return false;
                    }
                }
            },
            {
                text: "No", //"✖"
                icon: "ui-icon-heart",
                click: function () {
                    $(this).dialog("close");
                    $("#txtRejectReason").remove();
                    $("#ddlApproveReject").val('-Select-')
                    return false;
                }
            }
            ]
        });
        $('.ui-dialog :button').blur();
    }
    return true;
}
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="../../Scripts/Enable_Disable_Opener.js"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/InitEndRequest.js"></script>
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
        <uc1:Menu ID="Menu1" runat="server" />
        <br />
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnBack" />
            </Triggers>
            <ContentTemplate>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" valign="bottom">
                            <span class="pageLabel"><strong>Swift Message Checker </strong></span>
                        </td>
                        <td align="right" style="width: 50%">
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                TabIndex="34" OnClick="btnBack_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="labelMessage1" runat="server" CssClass="mandatoryField"></asp:Label>
                            <br />
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="left">
                            <span class="elementLabel" style="font-size: small">Branch :</span>
                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" AutoPostBack="true" Width="100px"
                                runat="server">
                                <%--<asp:ListItem Text="----Select----" Value="0" />
                                <asp:ListItem Text="Mumbai" Value="1" />
                                <asp:ListItem Text="New Delhi" Value="2" />
                                <asp:ListItem Text="Chennai" Value="3" />
                                <asp:ListItem Text="Bangalore" Value="4" />--%>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <div>
                        <tr align="left">
                            <td align="left">
                                <span class="elementLabel" style="font-size: medium">Swift Types :</span>
                                <asp:DropDownList ID="ddlSwiftTypes" Height="22px" Width="100px" CssClass="dropdownList"
                                    AutoPostBack="true" runat="server">
                                    <asp:ListItem Text="----Select----" Value="" />
                                    <asp:ListItem Text="MT 742" Value="8" />
                                    <asp:ListItem Text="MT 754" Value="11" />
                                    <asp:ListItem Text="MT 799" Value="9" />
                                    <asp:ListItem Text="MT 420" Value="5" />
                                    <asp:ListItem Text="MT 499" Value="6" />
                                    <asp:ListItem Text="MT 199" Value="3" />
                                    <asp:ListItem Text="MT 999" Value="10" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%;" valign="top">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                                <input type="hidden" id="hdnRejectReason" runat="server" />
                                <br />
                            </td>
                        </tr>
                    </div>
                    <table>
                        <tr>
                            <td align="left" style="width: 0%; border: 1px solid #49A3FF">
                                <ajaxToolkit:TabContainer ID="TabContainerMain" Visible="false" runat="server" ActiveTabIndex="0"
                                    CssClass="ajax__tab_xp-theme">
                                    <ajaxToolkit:TabPanel ID="TabSwifts" runat="server" Visible="false" HeaderText=""
                                        Font-Bold="true" ForeColor="White">
                                        <ContentTemplate>
                                            <table>
                                                <asp:Panel ID="Panel_742" runat="server" Visible="false">
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <span style="font-size: large">MT : 742 - Reimbursement Claim </span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">Receiver : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReceiver742" Width="" runat="server" MaxLength="11" CssClass="textBox"
                                                                TabIndex="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[20] Claiming Bank's
                                                                Reference : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_742_ClaimBankRef" Width="" runat="server" MaxLength="16" CssClass="textBox"
                                                                TabIndex="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[21] Documentary Credit
                                                                Number : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_742_DocumCreditNo" runat="server" CssClass="textBox" MaxLength="16"
                                                                TabIndex="2"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[31C] Date Of Issue : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_742_Dateofissue" runat="server" CssClass="textBox" Width="75px"
                                                                MaxLength="6" TabIndex="3"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txt_742_Dateofissue" ErrorTooltipEnabled="True"
                                                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                                CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="Dateofissue742" runat="server" CssClass="btncalendar_enabled" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txt_742_Dateofissue" PopupButtonID="Dateofissue742" Enabled="True">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender1"
                                                                ValidationGroup="dtVal" ControlToValidate="txt_742_Dateofissue" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                                Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[52A] Issuing Bank
                                                                : Party Identifier : </span>
                                                            <asp:DropDownList ID="ddl_Issuingbank_742" runat="server" CssClass="dropdownList"
                                                                OnSelectedIndexChanged="ddl_Issuingbank_742_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="52A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="52D" Value="D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">[52A] Issuing Bank : Party Identifier : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtIssuingBankAccountnumber_742" runat="server" CssClass="textBox"
                                                                TabIndex="4" Width="20px" MaxLength="1"></asp:TextBox>
                                                            <asp:TextBox ID="txtIssuingBankAccountnumber1_742" runat="server" CssClass="textBox"
                                                                TabIndex="4" Width="330px" MaxLength="34"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblIssuingBankIdentifier_742" runat="server" CssClass="elementLabel"
                                                                Text="Identifier Code :"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtIssuingBankIdentifiercode_742" CssClass="textBox"
                                                                MaxLength="11" TabIndex="4" Width="100px"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtIssuingBankName_742" CssClass="textBox" Visible="false"
                                                                MaxLength="35" TabIndex="4" Width="330px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblIssuingBankAddress1_742" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address1"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtIssuingBankAddress1_742" CssClass="textBox" Visible="false"
                                                                TabIndex="4" MaxLength="35" Width="330px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblIssuingBankAddress2_742" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address2"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtIssuingBankAddress2_742" CssClass="textBox" Visible="false"
                                                                TabIndex="4" MaxLength="35" Width="330px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblIssuingBankAddress3_742" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address3"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtIssuingBankAddress3_742" CssClass="textBox" Visible="false"
                                                                TabIndex="4" MaxLength="35" Width="330px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[32B] Principal Amount
                                                                Claimed : </span>
                                                        </td>
                                                        <td>
                                                            <span class="elementLabel">Currency : </span>
                                                            <%--<asp:TextBox ID="txt_742_PrinAmtClmd_Ccy" runat="server" CssClass="textBox"  Width="35px" MaxLength="3" TabIndex="5"></asp:TextBox>--%>
                                                            <asp:DropDownList ID="ddl_742_PrinAmtClmd_Ccy" runat="server" CssClass="dropdownList"
                                                                TabIndex="5" Width="70px" OnSelectedIndexChanged="ddl_742_PrinAmtClmd_Ccy_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Amount : </span>
                                                            <asp:TextBox ID="txt_742_PrinAmtClmd_Amt" runat="server" CssClass="textBox" MaxLength="15"
                                                                Width="95px" TabIndex="5"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[33B] Additional Amount Claimed as Allowed for in Excess
                                                                of Principal Amount : </span>
                                                        </td>
                                                        <td>
                                                            <span class="elementLabel">Currency : </span>
                                                            <%--<asp:TextBox ID="txt_742_AddAmtClamd_Ccy" runat="server" CssClass="textBox"  Width="35px" MaxLength="3" TabIndex="6"></asp:TextBox>--%>
                                                            <asp:DropDownList ID="ddl_742_AddAmtClamd_Ccy" runat="server" CssClass="dropdownList"
                                                                TabIndex="5" Width="70px" OnSelectedIndexChanged="ddl_742_AddAmtClamd_Ccy_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Amount : </span>
                                                            <asp:TextBox ID="txt_742_AddAmtClamd_Amt" runat="server" CssClass="textBox" MaxLength="15"
                                                                Width="95px" TabIndex="6"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[71D] Charges : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_742_Charges1" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="7"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_742_Charges2" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="7"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_742_Charges3" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="7"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_742_Charges4" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="7"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_742_Charges5" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="7"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_742_Charges6" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="7"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[34A] Total Amount
                                                                Claimed : </span>
                                                            <asp:DropDownList ID="ddlTotalAmtclamd_742" runat="server" CssClass="dropdownList"
                                                                OnSelectedIndexChanged="ddlTotalAmtclamd_742_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="34A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="34B" Value="B"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lbl_742_TotalAmtClmd_Date" Text="Date :" runat="server" class="elementLabel"></asp:Label>
                                                            <asp:TextBox ID="txt_742_TotalAmtClmd_Date" runat="server" CssClass="textBox" Width="60px"
                                                                MaxLength="6" TabIndex="8"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txt_742_TotalAmtClmd_Date" ErrorTooltipEnabled="True"
                                                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                                CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="TotalAmtClmd742_Date" runat="server" CssClass="btncalendar_enabled" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txt_742_TotalAmtClmd_Date" PopupButtonID="TotalAmtClmd742_Date"
                                                                Enabled="True">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender2"
                                                                ValidationGroup="dtVal" ControlToValidate="txt_742_TotalAmtClmd_Date" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                                Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                                            <span class="elementLabel">Currency : </span>
                                                            <%--<asp:TextBox ID="txt_742_TotalAmtClmd_Ccy" runat="server" CssClass="textBox" MaxLength="3" Width="35px" TabIndex="8"></asp:TextBox>--%>
                                                            <asp:DropDownList ID="ddl_742_TotalAmtClmd_Ccy" runat="server" CssClass="dropdownList"
                                                                TabIndex="5" Width="70px" OnSelectedIndexChanged="ddl_742_TotalAmtClmd_Ccy_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Amount : </span>
                                                            <asp:TextBox ID="txt_742_TotalAmtClmd_Amt" runat="server" CssClass="textBox" MaxLength="15"
                                                                Width="115px" TabIndex="8"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[57A] Account With Bank : </span>
                                                            <asp:DropDownList ID="ddlAccountwithbank_742" runat="server" CssClass="dropdownList"
                                                                OnSelectedIndexChanged="ddlAccountwithbank_742_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="57A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="57B" Value="B"></asp:ListItem>
                                                                <asp:ListItem Text="57D" Value="D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Party Identifier : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtAccountwithBankAccountnumber_742" runat="server" CssClass="textBox"
                                                                TabIndex="9" Width="20px" MaxLength="1"></asp:TextBox>
                                                            <asp:TextBox ID="txtAccountwithBankAccountnumber1_742" runat="server" CssClass="textBox"
                                                                TabIndex="9" Width="330px" MaxLength="34"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountwithBankIdentifier_742" runat="server" CssClass="elementLabel"
                                                                Text="Identifier Code :"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountwithBankIdentifiercode_742" CssClass="textBox"
                                                                MaxLength="11" TabIndex="9" Width="100px"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtAccountwithBankLocation_742" CssClass="textBox"
                                                                Visible="false" MaxLength="35" TabIndex="9" Width="330px"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtAccountwithBankName_742" CssClass="textBox" Visible="false"
                                                                MaxLength="35" TabIndex="9" Width="330px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountwithBankAddress1_742" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address1"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountwithBankAddress1_742" CssClass="textBox"
                                                                Visible="false" TabIndex="9" MaxLength="35" Width="330px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountwithBankAddress2_742" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address2"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountwithBankAddress2_742" CssClass="textBox"
                                                                Visible="false" TabIndex="9" MaxLength="35" Width="330px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountwithBankAddress3_742" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address3"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountwithBankAddress3_742" CssClass="textBox"
                                                                Visible="false" TabIndex="9" MaxLength="35" Width="330px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[58A] Beneficiary Bank : </span>
                                                            <asp:DropDownList ID="ddlBeneficiarybank_742" runat="server" CssClass="dropdownList"
                                                                OnSelectedIndexChanged="ddlBeneficiarybank_742_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="58A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="58D" Value="D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Party Identifier : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtBeneficiaryBankAccountnumber_742" runat="server" CssClass="textBox"
                                                                TabIndex="10" Width="20px" MaxLength="1"></asp:TextBox>
                                                            <asp:TextBox ID="txtBeneficiaryBankAccountnumber1_742" runat="server" CssClass="textBox"
                                                                TabIndex="10" Width="330px" MaxLength="34"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblBeneficiaryBankIdentifier_742" runat="server" CssClass="elementLabel"
                                                                Text="Identifier Code :"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtBeneficiaryBankIdentifiercode_742" CssClass="textBox"
                                                                MaxLength="11" TabIndex="10" Width="100px"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtBeneficiaryBankName_742" CssClass="textBox" Visible="false"
                                                                MaxLength="35" TabIndex="10" Width="330px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblBeneficiaryBankAddress1_742" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address1"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtBeneficiaryBankAddress1_742" CssClass="textBox"
                                                                Visible="false" TabIndex="10" MaxLength="35" Width="330px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblBeneficiaryBankAddress2_742" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address2"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtBeneficiaryBankAddress2_742" CssClass="textBox"
                                                                Visible="false" TabIndex="10" MaxLength="35" Width="330px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblBeneficiaryBankAddress3_742" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address3"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtBeneficiaryBankAddress3_742" CssClass="textBox"
                                                                Visible="false" TabIndex="10" MaxLength="35" Width="330px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[72Z] Sender to Receiver Information : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_742_SenRecInfo1" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_742_SenRecInfo2" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_742_SenRecInfo3" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_742_SenRecInfo4" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_742_SenRecInfo5" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_742_SenRecInfo6" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </table>
                                            <table>
                                                <asp:Panel ID="Panel_754" runat="server" Visible="false">
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <span style="font-size: large">MT : 754 - Advice of Payment/Acceptance/Negotiation </span>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">Receiver : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReceiver754" Width="" runat="server" MaxLength="11" CssClass="textBox"
                                                                TabIndex="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[20] Sender's Reference
                                                                : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_SenRef" Width="" runat="server" MaxLength="16" CssClass="textBox"
                                                                TabIndex="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[21] Related Reference
                                                                : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_RelRef" runat="server" CssClass="textBox" MaxLength="16"
                                                                TabIndex="2"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[32A] Principal Amount
                                                                Paid/Accepted/Negotiated : </span>
                                                            <asp:DropDownList ID="ddlPrinAmtPaidAccNego_754" runat="server" CssClass="dropdownList"
                                                                OnSelectedIndexChanged="ddlPrinAmtPaidAccNego_754_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="32A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="32B" Value="B"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblPrinAmtPaidAccNegoDate_754" Text="Date :" runat="server" class="elementLabel"></asp:Label>
                                                            <asp:TextBox ID="txtPrinAmtPaidAccNegoDate_754" runat="server" CssClass="textBox"
                                                                Width="60px" MaxLength="6" TabIndex="2"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txtPrinAmtPaidAccNegoDate_754" ErrorTooltipEnabled="True"
                                                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                                CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="BtnPrinAmtPaidAccNegoDate_754" runat="server" CssClass="btncalendar_enabled" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtPrinAmtPaidAccNegoDate_754" PopupButtonID="BtnPrinAmtPaidAccNegoDate_754"
                                                                Enabled="True">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator5" runat="server" ControlExtender="MaskedEditExtender3"
                                                                ValidationGroup="dtVal" ControlToValidate="txtPrinAmtPaidAccNegoDate_754" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                                Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                                            <span class="elementLabel">Currency : </span>
                                                            <%--<asp:TextBox ID="txtPrinAmtPaidAccNegoCurr_754" runat="server" CssClass="textBox" MaxLength="3" Width="35px" TabIndex="2"></asp:TextBox>--%>
                                                            <asp:DropDownList ID="ddl_PrinAmtPaidAccNegoCurr_754" runat="server" CssClass="dropdownList"
                                                                TabIndex="2" Width="70px" OnSelectedIndexChanged="ddl_PrinAmtPaidAccNegoCurr_754_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Amount : </span>
                                                            <asp:TextBox ID="txtPrinAmtPaidAccNegoAmt_754" runat="server" CssClass="textBox"
                                                                MaxLength="15" Width="115px" TabIndex="2"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[33B] Additional Amounts : </span>
                                                        </td>
                                                        <td>
                                                            <span class="elementLabel">Currency : </span>
                                                            <%--<asp:TextBox ID="txt_754_AddAmtClamd_Ccy" runat="server" CssClass="textBox"  Width="35px" MaxLength="3" TabIndex="2"></asp:TextBox>--%>
                                                            <asp:DropDownList ID="ddl_754_AddAmtClamd_Ccy" runat="server" CssClass="dropdownList"
                                                                TabIndex="2" Width="70px" OnSelectedIndexChanged="ddl_754_AddAmtClamd_Ccy_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Amount : </span>
                                                            <asp:TextBox ID="txt_754_AddAmtClamd_Amt" runat="server" CssClass="textBox" MaxLength="15"
                                                                Width="95px" TabIndex="2"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[71D] Charges Deducted : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_ChargesDeduct1" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_ChargesDeduct2" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_ChargesDeduct3" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_ChargesDeduct4" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_ChargesDeduct5" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_ChargesDeduct6" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[73A] Charges Added: </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_ChargesAdded1" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_ChargesAdded2" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_ChargesAdded3" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_ChargesAdded4" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_ChargesAdded5" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_ChargesAdded6" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[34A] Total Amount Claimed : </span>
                                                            <asp:DropDownList ID="ddlTotalAmtclamd_754" runat="server" CssClass="dropdownList"
                                                                OnSelectedIndexChanged="ddlTotalAmtclamd_754_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="34A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="34B" Value="B"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lbl_754_TotalAmtClmd_Date" Text="Date :" runat="server" class="elementLabel"></asp:Label>
                                                            <asp:TextBox ID="txt_754_TotalAmtClmd_Date" runat="server" CssClass="textBox" Width="60px"
                                                                MaxLength="6" TabIndex="8"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txt_754_TotalAmtClmd_Date" ErrorTooltipEnabled="True"
                                                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                                CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="TotalAmtClmd754_Date" runat="server" CssClass="btncalendar_enabled" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txt_754_TotalAmtClmd_Date" PopupButtonID="TotalAmtClmd754_Date"
                                                                Enabled="True">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator6" runat="server" ControlExtender="MaskedEditExtender4"
                                                                ValidationGroup="dtVal" ControlToValidate="txt_754_TotalAmtClmd_Date" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                                Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                                            <span class="elementLabel">Currency : </span>
                                                            <%--<asp:TextBox ID="txt_754_TotalAmtClmd_Ccy" runat="server" CssClass="textBox" MaxLength="3" Width="35px" TabIndex="8"></asp:TextBox>--%>
                                                            <asp:DropDownList ID="ddl_754_TotalAmtClmd_Ccy" runat="server" CssClass="dropdownList"
                                                                TabIndex="2" Width="70px" OnSelectedIndexChanged="ddl_754_TotalAmtClmd_Ccy_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Amount : </span>
                                                            <asp:TextBox ID="txt_754_TotalAmtClmd_Amt" runat="server" CssClass="textBox" MaxLength="15"
                                                                Width="115px" TabIndex="8"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[53A] Reimbursing Bank : </span>
                                                            <asp:DropDownList ID="ddlReimbursingbank_754" runat="server" CssClass="dropdownList"
                                                                OnSelectedIndexChanged="ddlReimbursingbank_754_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="53A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="53B" Value="B"></asp:ListItem>
                                                                <asp:ListItem Text="53D" Value="D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Party Identifier : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtReimbursingBankAccountnumber_754" runat="server" CssClass="textBox"
                                                                TabIndex="9" Width="20px" MaxLength="1"></asp:TextBox>
                                                            <asp:TextBox ID="txtReimbursingBankAccountnumber1_754" runat="server" CssClass="textBox"
                                                                TabIndex="9" Width="330px" MaxLength="34"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblReimbursingBankIdentifier_754" runat="server" CssClass="elementLabel"
                                                                Text="Identifier Code :"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtReimbursingBankIdentifiercode_754" CssClass="textBox"
                                                                MaxLength="11" TabIndex="9" Width="100px"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtReimbursingBankLocation_754" CssClass="textBox"
                                                                Visible="false" MaxLength="35" TabIndex="9" Width="330px"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtReimbursingBankName_754" CssClass="textBox" Visible="false"
                                                                MaxLength="35" TabIndex="9" Width="330px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblReimbursingBankAddress1_754" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address1"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtReimbursingBankAddress1_754" CssClass="textBox"
                                                                Visible="false" TabIndex="9" MaxLength="35" Width="330px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblReimbursingBankAddress2_754" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address2"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtReimbursingBankAddress2_754" CssClass="textBox"
                                                                Visible="false" TabIndex="9" MaxLength="35" Width="330px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblReimbursingBankAddress3_754" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address3"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtReimbursingBankAddress3_754" CssClass="textBox"
                                                                Visible="false" TabIndex="9" MaxLength="35" Width="330px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[57A] Account With Bank : </span>
                                                            <asp:DropDownList ID="ddlAccountwithbank_754" runat="server" CssClass="dropdownList"
                                                                OnSelectedIndexChanged="ddlAccountwithbank_754_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="57A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="57B" Value="B"></asp:ListItem>
                                                                <asp:ListItem Text="57D" Value="D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Party Identifier : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtAccountwithBankAccountnumber_754" runat="server" CssClass="textBox"
                                                                TabIndex="9" Width="20px" MaxLength="1"></asp:TextBox>
                                                            <asp:TextBox ID="txtAccountwithBankAccountnumber1_754" runat="server" CssClass="textBox"
                                                                TabIndex="9" Width="330px" MaxLength="34"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountwithBankIdentifier_754" runat="server" CssClass="elementLabel"
                                                                Text="Identifier Code :"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountwithBankIdentifiercode_754" CssClass="textBox"
                                                                MaxLength="11" TabIndex="9" Width="100px"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtAccountwithBankLocation_754" CssClass="textBox"
                                                                Visible="false" MaxLength="35" TabIndex="9" Width="330px"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtAccountwithBankName_754" CssClass="textBox" Visible="false"
                                                                MaxLength="35" TabIndex="9" Width="330px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountwithBankAddress1_754" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address1"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountwithBankAddress1_754" CssClass="textBox"
                                                                Visible="false" TabIndex="9" MaxLength="35" Width="330px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountwithBankAddress2_754" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address2"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountwithBankAddress2_754" CssClass="textBox"
                                                                Visible="false" TabIndex="9" MaxLength="35" Width="330px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblAccountwithBankAddress3_754" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address3"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtAccountwithBankAddress3_754" CssClass="textBox"
                                                                Visible="false" TabIndex="9" MaxLength="35" Width="330px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[58A] Beneficiary Bank : </span>
                                                            <asp:DropDownList ID="ddlBeneficiarybank_754" runat="server" CssClass="dropdownList"
                                                                OnSelectedIndexChanged="ddlBeneficiarybank_754_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="58A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="58D" Value="D"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Party Identifier : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtBeneficiaryBankAccountnumber_754" runat="server" CssClass="textBox"
                                                                TabIndex="10" Width="20px" MaxLength="1"></asp:TextBox>
                                                            <asp:TextBox ID="txtBeneficiaryBankAccountnumber1_754" runat="server" CssClass="textBox"
                                                                TabIndex="10" Width="330px" MaxLength="34"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblBeneficiaryBankIdentifier_754" runat="server" CssClass="elementLabel"
                                                                Text="Identifier Code :"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtBeneficiaryBankIdentifiercode_754" CssClass="textBox"
                                                                MaxLength="11" TabIndex="10" Width="100px"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="txtBeneficiaryBankName_754" CssClass="textBox" Visible="false"
                                                                MaxLength="35" TabIndex="10" Width="330px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblBeneficiaryBankAddress1_754" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address1"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtBeneficiaryBankAddress1_754" CssClass="textBox"
                                                                Visible="false" TabIndex="10" MaxLength="35" Width="330px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblBeneficiaryBankAddress2_754" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address2"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtBeneficiaryBankAddress2_754" CssClass="textBox"
                                                                Visible="false" TabIndex="10" MaxLength="35" Width="330px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblBeneficiaryBankAddress3_754" runat="server" CssClass="elementLabel"
                                                                Visible="false" Text="Address3"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox runat="server" ID="txtBeneficiaryBankAddress3_754" CssClass="textBox"
                                                                Visible="false" TabIndex="10" MaxLength="35" Width="330px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[72Z] Sender to Receiver Information : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_SenRecInfo1" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_SenRecInfo2" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_SenRecInfo3" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_SenRecInfo4" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_SenRecInfo5" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_SenRecInfo6" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[77] Narrative : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr1" runat="server" CssClass="textBox" Width="420px" MaxLength="35"
                                                                TabIndex="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr2" runat="server" CssClass="textBox" Width="420px" MaxLength="35"
                                                                TabIndex="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr3" runat="server" CssClass="textBox" Width="420px" MaxLength="35"
                                                                TabIndex="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr4" runat="server" CssClass="textBox" Width="420px" MaxLength="35"
                                                                TabIndex="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr5" runat="server" CssClass="textBox" Width="420px" MaxLength="35"
                                                                TabIndex="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr6" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr7" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr8" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr9" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr10" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="12"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr11" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="13"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr12" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="14"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr13" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="15"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr14" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="16"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr15" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="17"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr16" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="18"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr17" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="19"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr18" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="20"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr19" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="21"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_754_Narr20" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="22"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </table>
                                            <table>
                                                <asp:Panel ID="Panel_799" runat="server" Visible="false">
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <span style="font-size: large">MT : 799 - Free Format Message  (For LC documents) </span>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">Receiver : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReceiver799" Width="" runat="server" MaxLength="11" CssClass="textBox"
                                                                TabIndex="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[20] Transaction Reference
                                                                Number : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_transRefNo" Width="" runat="server" MaxLength="16" CssClass="textBox"
                                                                TabIndex="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[21] Related Reference : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_RelRef" runat="server" CssClass="textBox" MaxLength="16"
                                                                TabIndex="2"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[79] Narrative :
                                                            </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr1" runat="server" CssClass="textBox" Width="420px" MaxLength="35"
                                                                TabIndex="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr2" runat="server" CssClass="textBox" Width="420px" MaxLength="35"
                                                                TabIndex="4"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr3" runat="server" CssClass="textBox" Width="420px" MaxLength="35"
                                                                TabIndex="5"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr4" runat="server" CssClass="textBox" Width="420px" MaxLength="35"
                                                                TabIndex="6"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr5" runat="server" CssClass="textBox" Width="420px" MaxLength="35"
                                                                TabIndex="7"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr6" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="8"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr7" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="9"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr8" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="10"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr9" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr10" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="12"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr11" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="13"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr12" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="14"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr13" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="15"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr14" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="16"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr15" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="17"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr16" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="18"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr17" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="19"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr18" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="20"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr19" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="21"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr20" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="22"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr21" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="23"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr22" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="24"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr23" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="25"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr24" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="26"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr25" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="27"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr26" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="28"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr27" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="29"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr28" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="30"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr29" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="31"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr30" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="32"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr31" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="33"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr32" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="34"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr33" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="35"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr34" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="36"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_799_Narr35" runat="server" CssClass="textBox" MaxLength="35"
                                                                Width="420px" TabIndex="37"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </table>
                                            <table>
                                                <asp:Panel ID="Panel_499" runat="server" Visible="false">
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <span style="font-size: large">MT : 499 - Free Format Message (For non LC documents) </span>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;"> Receiver :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReceiver499" runat="server" MaxLength="16" CssClass="textBox"
                                                                TabIndex="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[20] Transaction Reference Number :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_transRefNo" Width="" runat="server" MaxLength="16" CssClass="textBox"
                                                                TabIndex="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[21] Related Reference : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_RelRef" runat="server" CssClass="textBox" MaxLength="16"
                                                                TabIndex="2"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[79] Narrative : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr1" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr2" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="4"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr3" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="5"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr4" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="6"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr5" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="7"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr6" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="8"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr7" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="9"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr8" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="10"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr9" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr10" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="12"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr11" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="13"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr12" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="14"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr13" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="15"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr14" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="16"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr15" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="17"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr16" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="18"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr17" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="19"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr18" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="20"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr19" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="21"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr20" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="22"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr21" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="23"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr22" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="24"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr23" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="25"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr24" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="26"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr25" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="27"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr26" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="28"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr27" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="29"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr28" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="30"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr29" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="31"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr30" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="32"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr31" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="33"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr32" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="34"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr33" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="35"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr34" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="36"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_499_Narr35" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="37"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </table>
                                            <table>
                                                <asp:Panel ID="Panel_420" runat="server" Visible="false">
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <span style="font-size: large">MT : 420 - Tracer (Enquires about documents sent for collection) </span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;"> Receiver :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReceiver420" runat="server" MaxLength="16" CssClass="textBox"
                                                                TabIndex="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[20] Sending Bank's
                                                                TRN :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_420_SendingBankTRN" runat="server" MaxLength="16" CssClass="textBox"
                                                                TabIndex="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[21] Related Reference
                                                                : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_420_RelRef" runat="server" CssClass="textBox" MaxLength="16"
                                                                TabIndex="2"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[32A] Amount Traced
                                                                : </span>
                                                            <asp:DropDownList ID="ddlAmountTraced_420" runat="server" CssClass="dropdownList"
                                                                OnSelectedIndexChanged="ddlAmountTraced_420_TextChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="32A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="32B" Value="B"></asp:ListItem>
                                                                <asp:ListItem Text="32K" Value="K"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblAmountTracedDayMonth_420" Text="(Day/Month) :" runat="server" class="elementLabel"
                                                                Visible="false"></asp:Label>
                                                            <%--<asp:TextBox ID="txtAmountTracedDayMonth_420" runat="server" CssClass="textBox" TabIndex="5"
                                                                Width="18px" Visible="false" MaxLength="1"></asp:TextBox>--%>
                                                            <asp:DropDownList ID="ddlAmountTracedDayMonth_420" Height="22px" Width="35px" 
                                                              CssClass="dropdownList" AutoPostBack="true" runat="server" Visible="false" TabIndex="3">
                                                                <asp:ListItem Text="-Select-" Value="" />
                                                                <asp:ListItem Text="D" Value="D" />
                                                                <asp:ListItem Text="M" Value="M" /> 
                                                                </asp:DropDownList>
                                                            <asp:Label ID="lblAmountTracedNoofDaysMonth_420" Text="(No of Days/Months) :" runat="server"
                                                                class="elementLabel" Visible="false"></asp:Label>
                                                            <asp:TextBox ID="txtAmountTracedNoofDaysMonth_420" runat="server" CssClass="textBox"
                                                                TabIndex="5" Width="50px" Visible="false" MaxLength="3"></asp:TextBox>
                                                            <asp:Label ID="lblAmountTracedDate_420" Text="Date :" runat="server" class="elementLabel"></asp:Label>
                                                            <asp:TextBox ID="txtAmountTracedDate_420" runat="server" CssClass="textBox" TabIndex="5"
                                                                Width="70px"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="AmountTracedDate420" Mask="99/99/9999" MaskType="Date"
                                                                runat="server" TargetControlID="txtAmountTracedDate_420" ErrorTooltipEnabled="True"
                                                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                                CultureDateFormat="DMY" CultureDatePlaceholder="/" Enabled="True" CultureDecimalPlaceholder="."
                                                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="btnCal420_Date" runat="server" CssClass="btncalendar_enabled" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtAmountTracedDate_420" PopupButtonID="btnCal420_Date" Enabled="True">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="AmountTracedDate420"
                                                                ValidationGroup="dtVal" ControlToValidate="txtAmountTracedDate_420" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                                Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                                            <asp:Label ID="lblAmountTracedCode_420" Text="Code :" runat="server" class="elementLabel"
                                                                Visible="false"></asp:Label>
                                                            <%--<asp:TextBox ID="txtAmountTracedCode_420" runat="server" CssClass="textBox" TabIndex="5"
                                                                Width="35px" Visible="false" MaxLength="2"></asp:TextBox>--%>
                                                                <asp:DropDownList ID="ddlAmountTracedCode_420" Height="22px" Width="300px" 
                                                                   CssClass="dropdownList" AutoPostBack="true" runat="server" Visible="false" TabIndex="3">
                                                                    <asp:ListItem Text="--Select--" Value="" />
                                                                    <asp:ListItem Text="BE-After the date of the bill of exchange" Value="BE" />
                                                                    <asp:ListItem Text="CC-After customs clearance of goods" Value="CC" /> 
                                                                    <asp:ListItem Text="FD-After goods pass food and drug administration" Value="FD" />
                                                                    <asp:ListItem Text="FP-First presentation" Value="FP" />
                                                                    <asp:ListItem Text="GA-After arrival of goods" Value="GA" />
                                                                    <asp:ListItem Text="ID-After invoice date" Value="ID" />
                                                                    <asp:ListItem Text="ST-After sight" Value="ST" />
                                                                    <asp:ListItem Text="TD-After date of transport documents" Value="TD" />
                                                                    <asp:ListItem Text="XX-See field 72 for specification" Value="XX" />
                                                                </asp:DropDownList>
                                                            <span class="elementLabel">Currency : </span>
                                                            <asp:DropDownList ID="ddl_AmountTracedCurrency_420" runat="server" CssClass="dropdownList"
                                                                TabIndex="5" Width="70px" OnSelectedIndexChanged="ddl_AmountTracedCurrency_420_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <span class="elementLabel">Amount : </span>
                                                            <asp:TextBox ID="txtAmountTracedAmount_420" runat="server" CssClass="textBox" TabIndex="5"
                                                                Width="150px" MaxLength="15"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[30] Date of Collection Instruction : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_420_DateofCollnInstruction" runat="server" CssClass="textBox"
                                                                Width="80px" TabIndex="2"></asp:TextBox>
                                                            <ajaxToolkit:MaskedEditExtender ID="DateofCollnInstruction420" Mask="99/99/9999"
                                                                MaskType="Date" runat="server" TargetControlID="txt_420_DateofCollnInstruction"
                                                                ErrorTooltipEnabled="True" CultureName="en-GB" CultureAMPMPlaceholder="AM;PM"
                                                                CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY" CultureDatePlaceholder="/"
                                                                Enabled="True" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                                CultureTimePlaceholder=":">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                            <asp:Button ID="btnCal_Date" runat="server" CssClass="btncalendar_enabled" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txt_420_DateofCollnInstruction" PopupButtonID="btnCal_Date"
                                                                Enabled="True">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="DateofCollnInstruction420"
                                                                ValidationGroup="dtVal" ControlToValidate="txt_420_DateofCollnInstruction" EmptyValueMessage="Enter Date Value"
                                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                                                Enabled="False"></ajaxToolkit:MaskedEditValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[59] Drawee </span><span class="elementLabel">Account :
                                                            </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_420_DraweeAccount" runat="server" CssClass="textBox" MaxLength="34"
                                                                Width="330px" TabIndex="18"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">Name : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_420_DraweeName" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="19"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">Address 1 : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_420_DraweeAdd1" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="20"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">Address 2 : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_420_DraweeAdd2" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="21"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">Address 3 : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txt_420_DraweeAdd3" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="22"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[72] Sender to Receiver Information : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_420_SenToRecinfo1" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="23"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_420_SenToRecinfo2" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="24"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_420_SenToRecinfo3" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="25"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_420_SenToRecinfo4" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="26"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_420_SenToRecinfo5" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="27"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_420_SenToRecinfo6" runat="server" CssClass="textBox" Width="330px"
                                                                MaxLength="35" TabIndex="28"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </table>
                                            <table>
                                                <asp:Panel ID="Panel_199" runat="server" Visible="false">
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <span style="font-size: large">MT : 199 - Free Format Message </span>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;"> Receiver :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReceiver199" runat="server" MaxLength="16" CssClass="textBox"
                                                                TabIndex="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[20] Transaction Reference Number :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_transRefNo" Width="" runat="server" MaxLength="16" CssClass="textBox"
                                                                TabIndex="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[21] Related Reference : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_RelRef" runat="server" CssClass="textBox" MaxLength="16"
                                                                TabIndex="2"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[79] Narrative : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr1" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr2" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="4"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr3" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="5"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr4" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="6"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr5" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="7"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr6" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="8"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr7" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="9"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr8" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="10"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr9" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr10" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="12"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr11" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="13"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr12" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="14"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr13" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="15"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr14" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="16"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr15" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="17"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr16" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="18"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr17" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="19"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr18" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="20"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr19" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="21"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr20" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="22"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr21" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="23"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr22" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="24"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr23" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="25"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr24" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="26"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr25" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="27"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr26" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="28"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr27" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="29"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr28" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="30"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr29" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="31"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr30" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="32"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr31" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="33"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr32" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="34"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr33" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="35"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr34" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="36"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_199_Narr35" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="37"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </table>
                                            <table>
                                                <asp:Panel ID="Panel_299" runat="server" Visible="false">
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <span style="font-size: large">MT : 299 - Free Format Message </span>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;"> Receiver :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReceiver299" runat="server" MaxLength="16" CssClass="textBox"
                                                                TabIndex="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[20] Transaction Reference Number :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_transRefNo" Width="" runat="server" MaxLength="16" CssClass="textBox"
                                                                TabIndex="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[21] Related Reference : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_RelRef" runat="server" CssClass="textBox" MaxLength="16"
                                                                TabIndex="2"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[79] Narrative : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr1" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr2" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="4"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr3" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="5"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr4" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="6"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr5" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="7"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr6" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="8"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr7" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="9"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr8" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="10"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr9" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr10" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="12"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr11" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="13"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr12" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="14"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr13" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="15"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr14" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="16"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr15" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="17"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr16" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="18"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr17" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="19"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr18" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="20"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr19" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="21"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr20" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="22"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr21" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="23"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr22" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="24"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr23" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="25"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr24" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="26"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr25" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="27"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr26" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="28"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr27" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="29"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr28" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="30"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr29" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="31"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr30" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="32"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr31" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="33"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr32" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="34"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr33" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="35"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr34" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="36"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_299_Narr35" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="37"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </table>
                                            <table>
                                                <asp:Panel ID="Panel_999" runat="server" Visible="false">
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            <span style="font-size: large">MT : 999 - Free Format Message </span>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;"> Receiver :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReceiver999" runat="server" MaxLength="16" CssClass="textBox"
                                                                TabIndex="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[20] Transaction Reference Number :</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_transRefNo" Width="" runat="server" MaxLength="16" CssClass="textBox"
                                                                TabIndex="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel">[21] Related Reference : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_RelRef" runat="server" CssClass="textBox" MaxLength="16"
                                                                TabIndex="2"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="elementLabel" style="color: Red; font-weight: bold;">[79] Narrative : </span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr1" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr2" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="4"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr3" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="5"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr4" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="6"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr5" runat="server" CssClass="textBox" Width="420px" MaxLength="50"
                                                                TabIndex="7"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr6" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="8"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr7" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="9"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr8" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="10"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr9" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="11"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr10" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="12"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr11" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="13"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr12" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="14"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr13" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="15"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr14" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="16"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr15" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="17"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr16" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="18"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr17" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="19"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr18" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="20"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr19" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="21"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr20" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="22"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr21" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="23"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr22" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="24"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr23" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="25"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr24" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="26"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr25" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="27"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr26" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="28"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr27" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="29"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr28" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="30"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr29" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="31"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr30" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="32"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr31" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="33"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr32" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="34"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr33" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="35"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr34" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="36"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span></span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_999_Narr35" runat="server" CssClass="textBox" MaxLength="50"
                                                                Width="420px" TabIndex="37"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </asp:Panel>
                                            </table>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                </ajaxToolkit:TabContainer>
                            </td>
                        </tr>
                    </table>
                </table>
                <tr style="height: 35px">
                    <td align="left" colspan="2">
                        <asp:Button ID="btn_View_Swift" runat="server" Text="View Swift Message" CssClass="buttonDefault"
                            Width="150px" ToolTip="View SWIFT Message" Height="32" TabIndex="256" OnClick="btn_View_Swift_Click" />
                    </td>
                   <td align="right">
                        <span class="elementLabel">Approve / Reject :</span>
                    </td>
                    <td align="left" colspan="3" width="90%">
                        <asp:DropDownList ID="ddlApproveReject" runat="server" CssClass="dropdownList" TabIndex="256">
                            <asp:ListItem Value="" Text="-Select-"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Approve"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Reject"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="btnSave" Style="visibility: hidden" runat="server" Text="Save" CssClass="buttonDefault"
                            ToolTip="Save" OnClick="btnSave_Click" TabIndex="256" />
                    </td>
                </tr>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
