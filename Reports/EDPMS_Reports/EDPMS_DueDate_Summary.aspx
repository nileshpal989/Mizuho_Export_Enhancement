<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDPMS_DueDate_Summary.aspx.cs"
    Inherits="Reports_EDPMS_Reports_EDPMS_DueDate_Summary" %>
<%@ Register Src="../../Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="../../Images/favicon.ico"
        type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="../../Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style.css" rel="Stylesheet" type="text/css" />
    <link href="../../Style/style_new.css" rel="Stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function isValidDate(controlID, CName) {
            var obj = controlID;
            if (controlID != "__/__/____") {
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
                if ((dt.getDate() != day) || (dt.getMonth != month - 1) || (dt.getFullYear() != year)) {
                    alert("Invalid" + CName);
                    controlID.focus();
                    return false;
                }
            }
        }

        function validatSave() {
            var txtAsOnDate = document.getElementById('txtAsOnDate');
            if (txtAsOnDate.value == '') {
                alert("Enter As On Date.");
                txtAsOnDate.focus();
                return false;
            }
            var ddlBranch = document.getElementById('ddlBranch');

//            var winame = window.open('EDPMS_DueDate_Summary_View.aspx?branch=' + ddlBranch.value + '&AsOnDate=' + txtAsOnDate.value, '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1200,height=600');


            var winname = window.open('EDPMS_DueDate_Summary_View.aspx?branch=' + ddlBranch.value + '&AsOnDate=' + txtAsOnDate.value, '', 'scrollbars=yes,left=0,top=50,maximizeButton=yes,menubar=0,width=1200,height=500');
            winname.focus();
            return false;
        }      
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <div>
        <uc1:Menu ID="Menu1" runat="server" />
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td align="left" colspan="2" style="height: 30px">
                            <span class="pageLabel"><strong>Bill Due Dates Falling on Holidays</strong></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="5%" nowrap style="height: 30px">
                            <span class="elementLabel">Branch : </span>
                        </td>
                        <td align="left">                        
                        <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" runat="server" Enabled="false">
                        </asp:DropDownList>
                           </td>
                    </tr>
                    <tr>
                        <td nowrap align="right" style="height: 30px">
                            <span class="elementLabel">As On Date : </span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtAsOnDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                Width="70px" TabIndex="4"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtAsOnDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="Button1" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtAsOnDate" PopupButtonID="Button1" Enabled="True">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left" style="height: 60px">
                            <asp:Button ID="btnGenerate" runat="server" CssClass="buttonDefault" Text="Generate" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
