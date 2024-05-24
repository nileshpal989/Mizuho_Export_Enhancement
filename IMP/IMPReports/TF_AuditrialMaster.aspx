<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_AuditrialMaster.aspx.cs"
    Inherits="Cust_Liability_Report_Cust_Liability_AuditTrail_Master" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen"/> 
    <script src="../../Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        function toDate() {
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0! 
            var yyyy = today.getFullYear();
            if (dd < 10) { dd = '0' + dd }
            if (mm < 10) { mm = '0' + mm }
            if (document.getElementById('txtFromDate').value != "__/__/____") {

                var toDate;
                toDate = dd + '/' + mm + '/' + yyyy;
                document.getElementById('txtToDate').value = toDate;
            }
        }

        function Opencustid(e) {
            var keycode;
            if (window.event) keycode = window.event.keyCode;

            if (keycode == 113 || e == 'mouseClick') {

                open_popup('../../TF_UserLookUp.aspx', 400, 300, 'custid_help');
                return false;
            }
        }
        function selectUser(username, userrole) {
            document.getElementById('txtusername').value = username;
            document.getElementById('lbluserrole').value = userrole;



            javascript: setTimeout('__doPostBack(\'ctl00$ContentPlaceHolder1$TextBox4\',\'\')', 0)
            return true;
                }
        


        //Alert validation
        function Alert(Result, ID) {
            $("#Paragraph").text(Result);
            $("#dialog").dialog({
                title: "Message From LMCC",
                width: 300,
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
    </script>
<script type="text/javascript">
    function Generate() {
        var ddlBranch = document.getElementById('ddlBranch');
        var DropDownMenu = document.getElementById('DropDownMenu');
        var txtfromDate = document.getElementById('txtfromDate');
        var txtToDate = document.getElementById('txtToDate');
        var rdbAlluser = document.getElementById('rdbAlluser');
        var rdbSelecteduser = document.getElementById('rdbSelecteduser');
        var txtusername = document.getElementById('txtusername');

        if (ddlBranch == 0) {
            alert('Select Branch Name.');
            ddlBranch.focus();
            return false;
        }
        if (txtfromDate.value == '') {
            Alert('Select From Date.', '#txtfromDate');
            // txtfromDate.focus();
            return false;
        }
        if (txtToDate.value == '') {
            alert('Select To Date.');
            txtToDate.focus();
            return false;
        }
        if (toDate.value == '') {
            alert('Select To Date.');
            toDate.focus();
            return false;
        }

        if (document.getElementById('rdbSelecteduser').checked == true) {
            if (txtusername.value == '') {
                Alert('Select user name.', '#txtusername')
                //txtusername.focus();
                return false;
            }
        }
        // menu

//        var rdbAllmaster = document.getElementById('rdbAllmaster');
//        var rdbCurrancyMaster = document.getElementById('rdbCurrancyMaster');
//        var rdbSancCountryMaster = document.getElementById('rdbSancCountryMaster');
//        var rdbCommodityMaster = document.getElementById('rdbCommodityMaster');
//        var rdbHoliDateDetail = document.getElementById('rdbHoliDateDetail');
//        var rdbCommissionMaster = document.getElementById('rdbCommissionMaster');
//        var rdbStampDuty = document.getElementById('rdbStampDuty');
//        var rdbOBankChargeMaster = document.getElementById('rdbOBankChargeMaster');
//        var rdbDisChargMaster = document.getElementById('rdbDisChargMaster');
        //type
        var rdbAllTypes = document.getElementById('rdbAllTypes');
        var rdbAdd = document.getElementById('rdbAdd');
        var rdbModify = document.getElementById('rdbModify');
        var rdbDelete = document.getElementById('rdbDelete');


        var Type = "";
        var User = "";
//        var Menu = "";
        if (rdbAlluser.checked == true)
            User = "all";
        else
            User = txtusername.value;
        //menu
//        if (rdbAllmaster.checked == true)
//            Menu = "all"

//        else if (rdbCurrancyMaster.checked == true)
//            Menu = "Currency Master";

//        else if (rdbSancCountryMaster.checked == true)
//            Menu = "Sanctioned Country Master";

//        else if (rdbCommodityMaster.checked == true)
//            Menu = "Commodity Master";

//        else if (rdbHoliDateDetail.checked == true)
//            Menu = "Holiday Dates Master";

//        else if (rdbCommissionMaster.checked == true)
//            Menu = "Commission Master";

//        else if (rdbStampDuty.checked == true)
//            Menu = "Stamp Duty Master";

//        else if (rdbOBankChargeMaster.checked == true)
//            Menu = "Other Bank Charges Master";

//        else if (rdbDisChargMaster.checked == true)
//            Menu = "Discrepency Charges Master";


        if (rdbAllTypes.checked == true)
            Type = "all";
        else if (rdbAdd.checked == true)
            Type = "A";
        else if (rdbDelete.checked == true)
            Type = "D";
        else if (rdbModify.checked == true)
            Type = "M";


        open_popup('Mizho_IMP_MasterReport.aspx?frm=' + txtfromDate.value + '&to=' + txtToDate.value + '&Branch=' + ddlBranch.value + '&User=' + User + '&menu=' + DropDownMenu.value + '&Type=' + Type + ' ', 500, 1000, 'custid_help');

        winname.focus();
        return false;
    }
    </script>
</head>
<body>
    <form id="form2" runat="server" autocomplete="off">
         <%--  alert message--%>
    <div id="dialog" class="AlertJqueryHide">
        <p id="Paragraph">
        </p>
    </div>
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script src="../../Scripts/InitEndRequest.js" type="text/javascript"></script>
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
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                            <br />
                                <span class="pageLabel"><strong>Audit Trail For Masters</strong></span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100%" valign="top">
                                <asp:Label ID="lblmessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top">
                                <table cellspacing="0" cellpadding="0" border="0" width="1300px" style="line-height: 150%">
                                    <tr>
                                        <td width="10%" align="right" nowrap>
                                            <span class="mandatoryField">*</span><span class="elementLabel">Branch :</span>
                                        </td>
                                        <td align="left" nowrap>
                                            &nbsp;
                                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" AutoPostBack="true"
                                                Width="100px" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 150px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">From Date :</span>
                                        </td>
                                        <td align="left" style="width: 800px">
                                            &nbsp;
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
                                            <%-- <asp:Button ID="btnChangeDate" runat="server" OnClick="btnChangeDate_Click" />--%>
                                            &nbsp; <span class="mandatoryField">*</span><span class="elementLabel">To Date :</span>
                                            &nbsp;
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
                                    <tr>
                                        <td align="right">
                                        </td>
                                        <td style="width: 700px; padding-top: 10px">
                                            &nbsp;
                                            <asp:RadioButton ID="rdbAlluser" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data2" TabIndex="4" Text="All Users" OnCheckedChanged="rdbAlluser_CheckedChanged" />
                                            &nbsp;&nbsp;
                                            <asp:RadioButton ID="rdbSelecteduser" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data2" TabIndex="5" Text="Selected Users" OnCheckedChanged="rdbSelecteduser_CheckedChanged" />
                                        </td>
                                    </tr>
                                    <caption>
                                
                                        <tr>
                                                  <td width="10%" align="right" nowrap>
                                            <span class="mandatoryField">*</span><span class="elementLabel">All Masters :</span>
                                        </td>
                                                <td><asp:DropDownList ID="DropDownMenu" CssClass="dropdownList" AutoPostBack="true"
                                                Width="20%" runat="server">
                                                <asp:ListItem Value="All">All Masters</asp:ListItem>
                                                <asp:ListItem>Customer Master</asp:ListItem>
                                                <asp:ListItem>Currency Master</asp:ListItem>
                                                <asp:ListItem>Sanctioned Country Master</asp:ListItem>
                                               <%-- <asp:ListItem>Commodity Master</asp:ListItem>--%>
                                                <asp:ListItem>Holiday Dates Master</asp:ListItem>
                                                <asp:ListItem>Commission Master</asp:ListItem>
                                                <asp:ListItem>Stamp Duty Master</asp:ListItem>
                                                <%--<asp:ListItem>Other Bank Charges Master</asp:ListItem>--%>
                                                <asp:ListItem>Discrepency Charges Master</asp:ListItem>
                                                <asp:ListItem>Local Bank Master</asp:ListItem>
                                            </asp:DropDownList>
                                            </td>
                                                <%--<td>
                                                    &nbsp;
                                                    <asp:RadioButton ID="rdbAllmaster" runat="server" Checked="true" 
                                                        CssClass="elementLabel" GroupName="Data11" TabIndex="6" Text="All Masters" />
                                                        <asp:RadioButton ID="rdbCurrancyMaster" runat="server" CssClass="elementLabel" 
                                                        GroupName="Data11" TabIndex="6" Text="Currency Master" />
                                                        <asp:RadioButton ID="rdbSancCountryMaster" runat="server" CssClass="elementLabel" 
                                                        GroupName="Data11" TabIndex="6" Text="Sanctioned Country Master" />
                                                        <asp:RadioButton ID="rdbCommodityMaster" runat="server" CssClass="elementLabel" 
                                                        GroupName="Data11" TabIndex="6" Text="Commodity Master" />
                                                        <asp:RadioButton ID="rdbHoliDateDetail" runat="server" CssClass="elementLabel" 
                                                        GroupName="Data11" TabIndex="6" Text="Holiday Dates Master" />
                                                         <asp:RadioButton ID="rdbCommissionMaster" runat="server" CssClass="elementLabel" 
                                                        GroupName="Data11" TabIndex="6" Text="Commission Master" />
                                                         <asp:RadioButton ID="rdbStampDuty" runat="server" CssClass="elementLabel" 
                                                        GroupName="Data11" TabIndex="6" Text="Stamp Duty Master" />
                                                         <asp:RadioButton ID="rdbOBankChargeMaster" runat="server" CssClass="elementLabel" 
                                                        GroupName="Data11" TabIndex="6" Text="Other Bank Charges Master" />
                                                         <br />
                                                    &nbsp;
                                                         <asp:RadioButton ID="rdbDisChargMaster" runat="server" CssClass="elementLabel" 
                                                        GroupName="Data11" TabIndex="6" Text="Discrepency Charges Master" />
                                                </td>--%>
                                            </tr>
                                        <caption>
                                        
                                            <tr>
                                                <td align="right">
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:RadioButton ID="rdbAllTypes" runat="server" Checked="true" 
                                                        CssClass="elementLabel" GroupName="Data1" TabIndex="6" Text="All TYPES" />
                                                    &nbsp;&nbsp;<asp:RadioButton ID="rdbAdd" runat="server" CssClass="elementLabel" 
                                                        GroupName="Data1" TabIndex="6" Text="Only ADD" />
                                                    &nbsp;&nbsp;<asp:RadioButton ID="rdbModify" runat="server" CssClass="elementLabel" 
                                                        GroupName="Data1" TabIndex="6" Text="Only MODIFY" />
                                                    &nbsp;&nbsp;<asp:RadioButton ID="rdbDelete" runat="server" CssClass="elementLabel" 
                                                        GroupName="Data1" TabIndex="6" Text="Only DELETE" />
                                                </td>
                                            </tr>
                                            <tr ID="Table1" runat="server">
                                                <td align="right">
                                                    <span class="elementLabel">Select User : </span>
                                                </td>
                                                <td align="left">
                                                    &nbsp;
                                                    <asp:TextBox ID="txtusername" runat="server" AutoPostBack="true" 
                                                        CssClass="textBox" MaxLength="40" 
                                                        TabIndex="6" Width="100px"></asp:TextBox>
                                                    &nbsp;
                                                    <asp:Button ID="btCustList" runat="server" CssClass="btnHelp_enabled" 
                                                        OnClientClick="return Opencustid('mouseClick');" />
                                                       <%-- <asp:TextBox ID="txtUserName" CssClass="textBox"
                                                    runat="server" TabIndex="5"></asp:TextBox><asp:Button ID="btnUserList" CssClass="btnHelp_enabled"
                                                        runat="server" TabIndex="-1" OnTextChanged="txtusername_TextChanged"/>--%>
                                                       
                                                    <asp:Label ID="lbluserrole" runat="server" CssClass="elementLabel"></asp:Label>
                                                </td>
                                            </tr>
                                        </caption>
                                    </caption>
                                </table>
                                <table>
                                    <tr valign="bottom">
                                        <td align="right" style="width: 120px">
                                        </td>
                                        <td align="left" style="width: 700px; padding-top: 10px; padding-bottom: 10px" valign="bottom">
                                            &nbsp;
                                            <asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" Text="Generate"
                                                ToolTip="Genarate" TabIndex="7" />
                                            <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="false" />
                                        </td>
                                    </tr>
                                </table>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
</body>
</html>
