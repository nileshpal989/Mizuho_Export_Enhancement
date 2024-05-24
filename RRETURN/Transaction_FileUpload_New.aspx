<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Transaction_FileUpload_New.aspx.cs"
    Inherits="RRETURN_Transaction_FileUpload_New" uiCulture="en" culture="en-gb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Help_Plugins/jquerynew.min.js" language="javascript" type="text/javascript"></script>
    <script src="../Help_Plugins/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <link href="../Help_Plugins/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Help_Plugins/AlertJquery.js" type="text/javascript"></script> 
    <script type="text/javascript" language="javascript">
        //        function validate() {
        //            var uploadcontrol = document.getElementById('FileUpload1').value;
        //            //Regular Expression for fileupload control.
        //            var reg = /^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.xls|.XLS)$/;
        //            if (uploadcontrol.length > 0) {
        //                //Checks with the control value.
        //                if (reg.test(uploadcontrol)) {
        //                    return true;
        //                }
        //                else {
        //                    //If the condition not satisfied shows error message.
        //                    alert("Only xlsx files are allowed!");
        //                    return false;
        //                }
        //            }
        //            if (document.getElementById('FileUpload1').value == "") {
        //                alert('Select xls file to import.');
        //                try {
        //                    document.getElementById('FileUpload1').focus();
        //                    return false;
        //                }
        //                catch (err) {
        //                    return false;
        //                }
        //            }
        //        } //End of function validate.
        //        function changeBranchDesc() {
        //            var ddlBranch = document.getElementById('ddlBranch');
        //            var lbl_adcode = document.getElementById('lbl_adcode');
        //            if (ddlBranch.value != "0")
        //                lbl_adcode.innerHTML = ddlBranch.value;
        //            else
        //                lbl_adcode.innerHTML = "";
        //            ddlBranch.focus();
        //            return true;
        //        }
        //        function confirmGeneration1() {
        //            var btnConfirmval = document.getElementById('btnConfirm');
        //            btnConfirmval.click();
        //        }
        //        function confirmGeneration() {
        //            var btnConfirm = document.getElementById('btnConfirm');
        //            var ans = confirm('Transaction file created, Do you want to Recreate?');
        //            if (ans) {
        //                btnConfirm.click();
        //            }
        //        }
        //Alert validation textbox
        function Alert(Result, ID) {
            $("#Paragraph").text(Result);
            $("#dialog").dialog({
                title: "Message From LMCC",
                width: 350,
                modal: true,
                closeOnEscape: true,
                dialogClass: "AlertJqueryDisplay",
                hide: { effect: "close", duration: 400 },
                buttons: [
                    {
                        text: "Ok",
                        icon: "ui-icon-heart",
                        click: function () {
                            $(this).dialog("close");
                            $(ID).focus();
                        }
                    }
                  ]
            });
            $('.ui-dialog :button').blur();
            return false;
        }
        // Validate report
        function Valiadat(Result) {
            $("#Paragraph").text(Result);
            $("#dialog").dialog({
                title: "Message From LMCC",
                width: 570,
                modal: true,
                closeOnEscape: true,
                dialogClass: "AlertJqueryDisplay",
                hide: { effect: "close", duration: 400 },
                buttons: [
                    {
                        text: "Ok",
                        icon: "ui-icon-heart",
                        click: function () {
                            $(this).dialog("close");

                            // popup = window.open('EDPMS_rpt_AD_TransferData_validation.aspx?mode=A', '_blank', 'height=600,  width=900,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100');
                            popup = window.open('RET_CSV_Validation.aspx', '_blank', 'height=600,  width=1000,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100');
                            $("").focus();
                        }
                    }
                  ]
            });
            $('.ui-dialog :button').blur();
            return false;
        }
   
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
       <%--  alert message--%>
    <div id="dialog" class="AlertJqueryHide">
        <p id="Paragraph">
        </p>
    </div>
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
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <Triggers>
                    <%--<asp:PostBackTrigger ControlID="btnUpldCSV" />--%>
                    <asp:PostBackTrigger ControlID="btnUpload" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <input type="hidden" id="hdnCustId" runat="server" />
                            <td align="left" style="width: 50%; font-weight: bold" valign="bottom">
                                <span class="pageLabel" style="font-weight: bold">Excel Input Data File Upload at Branch</span>
                            </td>
                            <td align="right" style="width: 50%">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%;" valign="top" colspan="2">
                                <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 150%">
                                    <tr>
                                        <td colspan="2" nowrap align="center">
                                            <%--<asp:Label ID="labelMessage1" runat="server" CssClass="mandatoryField"></asp:Label>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Branch :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" runat="server" AutoPostBack="true"
                                                TabIndex="1" Width="100px">
                                            </asp:DropDownList>
                                            &nbsp;&nbsp;
                                            <%--<asp:Label Text="" ID="lbl_adcode" runat="server" CssClass="elementLabel" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">From FortNight Date :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10"
                                                ValidationGroup="dtVal" Width="70" TabIndex="2" AutoPostBack="true"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender ID="mdDocdate" Mask="99/99/9999" MaskType="Date"
                                                runat="server" TargetControlID="txtFromDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                CultureTimePlaceholder=":" Enabled="True">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <asp:Button ID="btncalendar_FromDate" runat="server" CssClass="btncalendar_enabled"
                                                TabIndex="-1" />
                                            <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="btncalendar_FromDate" Enabled="True">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdDocdate"
                                                ValidationGroup="dtVal" ControlToValidate="txtFromDate" EmptyValueMessage="Enter Date Value"
                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="Invalid"></ajaxToolkit:MaskedEditValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">To FortNight Date :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10"
                                                Width="70" TabIndex="2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td align="right" style="width: 150px">
                                            <span class="elementLabel">Select File :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:FileUpload ID="fileinhouse" runat="server" ViewStateMode="Enabled" TabIndex="3"
                                                Width="500px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Input File :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:TextBox ID="txtInputFile" runat="server" CssClass="textBox" MaxLength="10"
                                                Width="413px" TabIndex="2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <table border="0" style="border-color: red" cellpadding="2">
                                                <tr>
                                                    <td width="130px" nowrap>
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Button ID="btnUpload" runat="server" Text="Upload Excel File" CssClass="buttonDefault"
                                                                    ToolTip="Upload Excel File" TabIndex="4" OnClick="btnUpload_Click" />
                                                                <asp:Button ID="btnValidate" CssClass="buttonDefault" Text="Validate" runat="server"
                                                                    OnClick="btnValidate_Click" />
                                                                <asp:Button ID="btnProcess" CssClass="buttonDefault" Text="Process" runat="server"
                                                                    OnClick="btnProcess_Click" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblHint" CssClass="mandatoryField" Font-Size="Small" Font-Bold="true"
                                                            runat="server" />&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <span Class="mandatoryField">*</span>
                                                        <asp:Label ID="lbldateformathint" Font-Bold="true" Font-Size="Medium" Text="Excel file all date column should be formated in UK 'dd/mm/yyyy' format." 
                                                        CssClass="mandatoryField" runat="server" />&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" valign="top">
                                                        <span Class="mandatoryField">*</span>
                                                        <asp:Label ID="Label1" Font-Bold="true" Font-Size="Medium"  Text=" 1." CssClass="mandatoryField" runat="server" />&nbsp;
                                                        <asp:Label ID="Label6" Font-Bold="true" Font-Size="Medium"   Text=" First Upload Excel File." CssClass="mandatoryField"
                                                            runat="server" />
                                                        &nbsp; &nbsp;<asp:Label ID="Label4" Text=" 2." Font-Size="Medium"  Font-Bold="true" CssClass="mandatoryField" runat="server" />&nbsp;
                                                        <asp:Label ID="Label7"  Font-Bold="true" Font-Size="Medium"   Text=" Validate For Error Records. " CssClass="mandatoryField"
                                                            runat="server" />
                                                        &nbsp; &nbsp;<asp:Label ID="Label5" Font-Bold="true" Font-Size="Medium"   Text=" 3. " CssClass="mandatoryField" runat="server" />&nbsp;
                                                        <asp:Label ID="Label8" Text=" Process. " Font-Bold="true" Font-Size="Medium"   CssClass="mandatoryField" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Label runat="server" CssClass="elementLabel" ID="labelMessage" Style="font-weight: bold;"></asp:Label>
                                                                &nbsp;<br />
                                                                <asp:Label runat="server" CssClass="elementLabel" ID="label2" Style="font-weight: bold;"></asp:Label>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                                                                <asp:Label runat="server" CssClass="elementLabel" ID="label3" Style="font-weight: bold;"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <br />
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
