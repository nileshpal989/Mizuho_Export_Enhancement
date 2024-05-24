<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_OtherBOE_Data_Entry.aspx.cs"
    Inherits="IDPMS_TF_OtherBOE_Data_Entry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="../Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>LMCC TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="../Images/favicon.ico"
        type="image/x-icon" />
    <link id="Link2" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link3" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <link href="../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">


        function PortHelp() {
            open_popup('../HelpForms/Help_GRCustoms_Port_Code.aspx', 400, 400, "DocFile");
            return true;
        }

        function selectPort(Uname, PortDesc) {

            document.getElementById('txtPortCode').value = Uname;
            document.getElementById('lblPrtcde').innerHTML = PortDesc;
            document.getElementById('txtPortCode').focus();
        }


        function validate() {
            var docdate = document.getElementById('txtdocdate').value;
            var BOENo = document.getElementById('txtBOENo').value;
            var BOEDATE = document.getElementById('txtBOEDate').value;
            var BOEPortCode = document.getElementById('txtPortCode').value;

            if (BOENo == '') {

                alert('BOE No cant be blank');
                document.getElementById('txtBOENo').focus();
                return false;
            }

            if (BOEDATE == '') {

                alert('BOE Date cant be blank');
                document.getElementById('txtBOEDate').focus();
                return false;
            }

            if (BOEPortCode == '') {

                alert('BOE PortCode cant be blank');
                document.getElementById('txtPortCode').focus();
                return false;
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
                checkDate(controlID);
            }
        }




        function checkDate(EnteredDate) {

            var EnteredDateval = EnteredDate.value;
            var date = EnteredDateval.substring(0, 2);
            var month = EnteredDateval.substring(3, 5);
            var year = EnteredDateval.substring(6, 10);

            var myDate = new Date(year, month - 1, date);

            var today = new Date();

            if (myDate > today) {
                alert("Entered date is greater than today's date ");
                EnteredDate.focus();
            }
        }



      
      
 


    </script>
    <style type="text/css">
        .style3
        {
            width: 20%;
        }
        .style4
        {
            width: 15%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
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
            <asp:updatepanel id="UpdatePanelMain" runat="server" updatemode="Conditional">
                <%-- <asp:ScriptManager ID="ScriptManagerMain" runat="server">
                 </asp:ScriptManager>--%>
                <contenttemplate>
                    <table cellspacing="0" border="0" width="100%">

                         <tr>
                            <td align="left" valign="bottom" width=10% nowrap>
                                <span class="pageLabel"> <strong>Bill Of Entry - Other AD Data Entry</strong></span>
                              
                            </td>

                            <td nowrap class="style4">
                             <input type="hidden" id="hdnbranch" runat="server" />
                              <input type="hidden" id="hdnyr" runat=server />
                            </td>

                             <td width="10%" nowrap>
                            &nbsp;
                            </td>

                             <td width="10%" nowrap>
                            &nbsp;
                            </td>

                             <td width="10%" nowrap>
                            &nbsp;
                            </td>

                             <td align="right" nowrap>
                            <asp:Label runat="server" ID="lblSupervisormsg" Text="Role : Supervisor - ONLY VIEW THE DATA  " 
                                    style="color:red" ></asp:Label>&nbsp;
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" 
                                     ToolTip="Back" onclick="btnBack_Click" />
                            </td>

                            </tr>

                            <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="6">
                                <hr />
                            </td>
                        </tr>
                        <tr>

                       
                             <td width="15%" align="right" nowrap>
                                <span class="elementLabel">Branch :</span>
                            </td>
                            <td align="left" class="style4">
                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList"
                                    AutoPostBack="true" runat="server" 
                                    TabIndex="1">
                                </asp:DropDownList>
                            </td>
                     
                        </tr>
                      <tr>
                            <td align="right" style="white-space: nowrap" class="style12"  >
                                <span class="elementLabel">Document No :</span>
                            </td>
                            <td align="left" class="style4">
                                <asp:TextBox ID="txtdocno" runat="server" Width="125px" MaxLength="50" AutoPostBack="true"
                                    CssClass="textBox" Enabled="false" OnTextChanged="txtdocno_TextChanged"></asp:TextBox>
                            </td>
                            <td class="style11" align="right" nowrap>
                                <span class="elementLabel">Document Date :</span>
                            </td>
                            <td align="left" style="white-space: nowrap" class="style12">
                                <asp:TextBox ID="txtdocdate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                    onfocus="this.select()" TabIndex="2" Enabled="false"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtdocdate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="btncalendar_DocDate2" runat="server" CssClass="btncalendar_enabled"
                                    TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtdocdate" PopupButtonID="btncalendar_DocDate2" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                   <%--chnges--%>   </tr>

                        <tr>
                             <td align="right">
                         <span class="mandatoryField">*</span> <span class="elementLabel">BOE No:</span>
                        </td>
                        <td align="left" nowrap class="style4">
                        <asp:TextBox runat="server" CssClass="textBox" ID="txtBOENo" Width=100px 
                                MaxLength="7" AutoPostBack="True" 
                                TabIndex="1"  />
                        </td>
                        </tr>
                        <tr>
                          <td align="right">
                          <span class="mandatoryField">*</span><span class="elementLabel">BOE Date:</span>
                        </td>
                         <td align="left" class="style4">
                            <asp:TextBox ID="txtBOEDate" runat="server" CssClass="textBox" 
                                    Width="80px" MaxLength="10" onfocus="this.select()" TabIndex="3" 
                                 ></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="mdRemdate1" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtBOEDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="btncalendar_DocDate1" runat="server" CssClass="btncalendar_enabled"
                                    TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="calendarFromDate2" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtBOEDate" PopupButtonID="btncalendar_DocDate1" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                        </td>
                        </tr>
                        <tr>
                                   <td align="right">
                         <span class="mandatoryField">*</span><span class="elementLabel">PortCode:</span>
                        </td>
                        <td align="left" class="style4" colspan="2">
                            <asp:TextBox runat="server" ID="txtPortCode" CssClass="textBox" Width="100px" 
                                MaxLength="6" TabIndex="4" ontextchanged="txtPortCode_TextChanged" AutoPostBack="true"/>  
                                <asp:Button ID="btnporthelp" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                    Width="16px" />
                                <asp:Label Text="" ID="lblPrtcde" CssClass="elementLabel" runat="server" />

                        </td>
                        </tr>
                        <tr>
                   <td>
                   </td>
                        </tr>
                        <tr>
                        <td align="right">
                       
                        

                        </td>

                        <td class="style4" align="left" colspan="2">
                          <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                            ToolTip="Save" onclick="btnSave_Click" TabIndex="5" />
                      <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                            ToolTip="Cancel" onclick="btnCancel_Click" TabIndex="6"  />
                        
                        </td>

                        <td>
                        
                        </td>
                        </tr>

                            </table>

                            </contenttemplate>
            </asp:updatepanel>
        </center>
    </div>
    </form>
</body>
</html>
