<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_AuditrailFileUpload.aspx.cs" Inherits="IMP_IMPReports_TF_AuditrailFileUpload" %>

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
 
        var rdbAllTypes = document.getElementById('rdbAllTypes');
        var rdbRmaMaster = document.getElementById('rdbRmaMaster');
        var rdbHolidayMaster = document.getElementById('rdbHolidayMaster');
        var rdbDelete = document.getElementById('rdbDelete');


        var Type = "";
        var User = "";
      
        if (rdbAlluser.checked == true)
            User = "all";
        else
            User = txtusername.value;
      

        if (rdbAllTypes.checked == true)
            Type = "all";
        else if (rdbRmaMaster.checked == true)
            Type = "RMA Master";
        else if (rdbHolidayMaster.checked == true)
            Type = "Holiday Master";

     open_popup('Mizho_IMP_FileUploadReport.aspx?frm=' + txtfromDate.value + '&to=' + txtToDate.value +  '&User=' + User + '&Type=' + Type + ' ', 500, 1000, 'custid_help');

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
                                <span class="pageLabel"><strong>Audit Trail For File Upload</strong></span>
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
                                
                                       
                                        <caption>
                                        
                                            <tr>
                                                <td align="right">
                                                </td>
                                                <td>
                                                    &nbsp;
                                                    <asp:RadioButton ID="rdbAllTypes" runat="server" Checked="true" 
                                                        CssClass="elementLabel" GroupName="Data1" TabIndex="6" Text="All TYPES" />
                                                    &nbsp;&nbsp;<asp:RadioButton ID="rdbRmaMaster" runat="server" CssClass="elementLabel" 
                                                        GroupName="Data1" TabIndex="6" Text="RMA Master" />
                                                    &nbsp;&nbsp;<asp:RadioButton ID="rdbHolidayMaster" runat="server" CssClass="elementLabel" 
                                                        GroupName="Data1" TabIndex="6" Text="Holiday Master" />
                                                  
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