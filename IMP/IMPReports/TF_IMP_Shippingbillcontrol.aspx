<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_Shippingbillcontrol.aspx.cs" Inherits="IMP_IMPReports_TF_IMP_Shippingbillcontrol" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script src="../../Scripts/InitEndRequest.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>
    <link href="../../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/style_new.css" rel="stylesheet" type="text/css" />

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

                open_popup('../TF_UserLookUp.aspx', 400, 300, 'custid_help');
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
            var txtfromDate = document.getElementById('txtfromDate');
             var txtToDate = document.getElementById('txtToDate');

            if (txtfromDate.value == '') {
                VAlert('Select From Date.', '#txtfromDate');
                txtfromDate.focus();
                return false;
            }
            if (txtToDate.value == '') {
                VAlert('Select To Date.', '#txtToDate');
                txtToDate.focus();
                return false;
            }



            var winname = open_popup('TF_IMP_View_Shippingbillcontrol.aspx?frm=' + txtfromDate.value + '&to=' + txtToDate.value, '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1400,height=600');

            winname.focus();
            return false;
        }
    </script>


</head>
<body>
  <form id="form1" runat="server" >
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
     
     <%--  alert message--%>
    <div id="dialog" class="AlertJqueryHide">
        <p id="Paragraph">
        </p>
    </div>
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
          <uc1:menu ID="Menu1" runat="server" />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                            <br />
                                <span class="pageLabel"><strong>Import Documents Control Sheet</strong></span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100%" valign="top">
                                <asp:Label ID="lblmessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top">
                                <table cellspacing="0" cellpadding="0" border="0" width="1300px" style="line-height: 150%">
                                    <tr>
                                        <td align="right" style="width: 150px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">From Lodgement Date :</span>
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
                                             <%--<asp:Button ID="btnChangeDate" runat="server" OnClick="btnChangeDate_Click" />--%>
                                            &nbsp; <span class="mandatoryField">*</span><span class="elementLabel">To Lodgement Date :</span>
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
                                   <%-- <tr>
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
                                        <td align="right" class="style1">
                                        </td>
                                        <td style="padding-top: 10px" class="style2">
                                            &nbsp;
                                            <asp:RadioButton ID="rdbAlluser" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data1" TabIndex="4" Text="All Users" OnCheckedChanged="rdbAlluser_CheckedChanged" />
                                            &nbsp;&nbsp;
                                            <asp:RadioButton ID="rdbSelecteduser" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data1" TabIndex="5" Text="Selected Users" OnCheckedChanged="rdbSelecteduser_CheckedChanged" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                        </td>
                                        <td style="width: 700px; padding-top: 10px">
                                            &nbsp;
                                            <asp:RadioButton ID="rdbAllDocType" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data2" TabIndex="4" Text="All DocType"/>
                                                    <asp:RadioButton ID="rdbICA" runat="server" CssClass="elementLabel" 
                                                        GroupName="Data2" TabIndex="6" Text="ICA" />
                                                        <asp:RadioButton ID="rdbICU" runat="server" CssClass="elementLabel" 
                                                        GroupName="Data2" TabIndex="6" Text="ICU" />--%>
                                        <%--    <asp:RadioButton ID="rdbselectDocType" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data2" TabIndex="5" Text="Selected DocType" 
                                                oncheckedchanged="selectDocType_CheckedChanged" />--%>
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td align="right">
                                        </td>
                                        <td style="width: 700px; padding-top: 10px">
                                            &nbsp;
                                            <asp:RadioButton ID="rdbAllDocno" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data3" TabIndex="4" Text="All Docno" 
                                                oncheckedchanged="AllDocno_CheckedChanged"/>
                                            &nbsp;&nbsp;
                                            <asp:RadioButton ID="rdbselectedDocno" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data3" TabIndex="5" Text="Selected DocNo" 
                                                oncheckedchanged="selectedDocno_CheckedChanged" />
                                        </td>
                                    </tr>--%>
                                    <caption>
                                
                                        
                                        <caption>
                                            <%--<tr ID="Table1" runat="server">
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
                                            </tr>--%>
                                       <%--     <tr ID="Table2" runat="server">
                                                <td align="right">
                                                    <span class="elementLabel">Select DocType : </span>
                                                </td>
                                                <td align="left">
                                                     <asp:TextBox ID="txtdoctype" runat="server" AutoPostBack="true" 
                                                        CssClass="textBox" MaxLength="40" 
                                                        TabIndex="6" Width="100px"></asp:TextBox>
                                                    &nbsp;
                                                       <asp:Button ID="Button1" runat="server" CssClass="btnHelp_enabled" 
                                                        OnClientClick="return Opencustid('mouseClick');" />
                                                        <asp:Label ID="Label1" runat="server" CssClass="elementLabel"></asp:Label>
                                                    
                                                </td>
                                            </tr>
                                            <tr ID="Table3" runat="server">
                                                <td align="right">
                                                    <span class="elementLabel">Select DocNO : </span>
                                                </td>
                                                <td align="left">
                                                    &nbsp;
                                                     <asp:TextBox ID="txtdocno" runat="server" AutoPostBack="true" 
                                                        CssClass="textBox" MaxLength="40" 
                                                        TabIndex="6" Width="100px"></asp:TextBox>
                                                    &nbsp;
                                                     <asp:Button ID="Button2" runat="server" CssClass="btnHelp_enabled" 
                                                        OnClientClick="return Opencustid('mouseClick');" />
                                                        <asp:Label ID="Label2" runat="server" CssClass="elementLabel"></asp:Label>
                                                         &nbsp;
                                                    
                                                </td>
                                            </tr>--%>
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
                                </td>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
</body>
</html>
