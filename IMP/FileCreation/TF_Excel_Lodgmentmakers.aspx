<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_Excel_Lodgmentmakers.aspx.cs" Inherits="Lodgmentmakers" %>
<%@ Register src="../../Menu/Menu.ascx" tagname="Menu" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style_V2.css" rel="Stylesheet" type="text/css" media="screen" />
    <link href="../../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../../Scripts/jquery.min.js" language="javascript" type="text/javascript"></script>
    <script src="../../Help_Plugins/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <link href="../../Help_Plugins/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../../Help_Plugins/Myjquery2.js" language="javascript" type="text/javascript"></script>
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
                if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year)) {
                    alert('Invalid ' + CName);
                    controlID.focus();
                    return false;
                }
            }
        }

        //help.................................................

        function OpenCustomerCodeList(e) {
            var keycode;
            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {
                open_popup('../HelpForms/TF_IMP_CustomerHelp1.aspx', 400, 400, 'CustomerCodeList');
                return false;
            }
        }

        function selectCustomer(selectedID,name) {
            var txtCustomer_ID = document.getElementById('txtCustomer_ID');
         document.getElementById('lblCustomerDesc').innerText=name;
            txtCustomer_ID.value = selectedID;
//            __doPostBack('txtCustomer_ID', '');
            txtCustomer_ID.focus();
        }

        //help.....................................


        function validateSave() {

            var txtCustomer_ID = document.getElementById('txtCustomer_ID');
            if (document.getElementById('rdbSelectedCustomer').checked == true)
            {
                if (txtCustomer_ID.value == '') 
              {
              	VAlert('Select Customer Ac no.', '#txtCustomer_ID');
               // txtCustomer_ID.focus();
                return false;
              }
            }

            var fromDate = document.getElementById('txtFromDate');
            if (fromDate.value == '') {
            	VAlert('Select From Date.', '#fromDate');
               // fromDate.focus();
                return false;
            }
            var toDate = document.getElementById('txtToDate');
            if (toDate.value == '') {
            	VAlert('Select To Date.', '#toDate');
               // toDate.focus();
                return false;
               }
               MyConfirm('Do you want to download this report?', '#btndownload');

        }       
    </script>
  <script src="../../Scripts/jquery-1.8.3.min.js"type="text/javascript""></script>
    <script src="../../Help_Plugins/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <link href="../../Help_Plugins/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../../Help_Plugins/MyJquery1.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/InitEndRequest.js"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/Enable_Disable_Opener.js"></script>
</head>
<body>
    <form id="form2" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
  <div id="dialog" class="AlertJqueryHide">
            <p id="Paragraph">
            </p>
        </div>
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
                  <asp:PostBackTrigger ControlID="btndownload" />
                   </Triggers>
                <ContentTemplate>
                    <table cellspacing="2" cellpadding="2" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom" colspan="2">
                                <span class="pageLabel"><strong>Lodgment Report (All Fields)</strong></span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                          <td align="right" valign="top">
                            </td>
                            <td align="right" valign="top">
                                 <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap class="style1">
                                <span class="elementLabel">From Lodgement Date :</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                    Width="70px" TabIndex="2"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtFromDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="Button1" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtFromDate" PopupButtonID="Button1" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                                &nbsp;&nbsp; <span class="elementLabel">To Lodgement Date :</span>
                                <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                    Width="70px" TabIndex="3"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtToDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="Button2" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtToDate" PopupButtonID="Button2" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="elementLabel">Branch :</span>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="dropdownList" TabIndex="1" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span class="elementLabel">Status :</span>
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="dropdownList" 
									TabIndex="2" >
                            		<asp:ListItem Value="A">Approved</asp:ListItem>
									<asp:ListItem Value="R">Rejected</asp:ListItem>
									<asp:ListItem Value="C">Pending to be Approved</asp:ListItem>
								</asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                             <td align="right"></td>
                         <td align="left" style="width: 50%" valign="top">
                        <asp:RadioButton ID="rdbAllCustomer" runat="server" CssClass="elementLabel" GroupName="Data1"
                                            TabIndex="4" Text="All Customers" Checked="true" AutoPostBack="true" OnCheckedChanged="rdbAllCustomer_CheckedChanged" /><asp:RadioButton
                                                ID="rdbSelectedCustomer" runat="server" CssClass="elementLabel" GroupName="Data1"
                                                TabIndex="4" AutoPostBack="true" Text="Selected Customer" OnCheckedChanged="rdbSelectedCustomer_CheckedChanged" />
                        </td>
                        </tr>
                        <tr>
                      <td align="right"></td>
                      <td align="left">
                    <div id="divUser"  runat="server" visible="false">
                        <span class="elementLabel">Customer Ac :</span>
                     <asp:TextBox ID="txtCustomer_ID" runat="server" AutoPostBack="True" CssClass="textBox"
                        MaxLength="6" TabIndex="4" Width="90px"></asp:TextBox> 
                         &nbsp;<asp:Button ID="btnCustomerList" runat="server" ToolTip="Press for Customers list." CssClass="btnHelp_enabled" TabIndex="4" />
                          <asp:Label ID="lblCustomerDesc" runat="server" CssClass="elementLabel"></asp:Label>
                 </div>
                   </td>
                    </tr>
                    <tr>
                    <td align="right"  width="7%">
                                <span class="elementLabel">Document Type :</span>
                            </td>
                    <td  align="left">
                     <asp:RadioButton ID="rdbAll" runat="server" CssClass="elementLabel" GroupName="Data2"
                                            Text="All" Checked="true" />&nbsp;&nbsp;
                                      <asp:RadioButton ID="rdbIBA" runat="server" CssClass="elementLabel" GroupName="Data2"  Text="IBA"/>&nbsp;&nbsp;
                                       <asp:RadioButton ID="rdbICA" runat="server" CssClass="elementLabel" GroupName="Data2" Text="ICA" />&nbsp;&nbsp;
                                            <asp:RadioButton ID="rdbICU" runat="server" CssClass="elementLabel" GroupName="Data2" Text="ICU" />&nbsp;&nbsp;
                                             <asp:RadioButton ID="rdbACC" runat="server" CssClass="elementLabel" GroupName="Data2" Text="ACC" />
                    </td>
                    </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td width="10%">
                            </td>
                            <td align="left" height="40px">
                                  <asp:Button ID="btnCreate" runat="server" CssClass="buttonDefault" Text="Generate"
                                    ToolTip="Generate" TabIndex="7" OnClientClick="return validateSave();" />
                              <asp:Button ID="btndownload" Style="display: none;" runat="server" 
                                 OnClick="btndownload_Click" />
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



