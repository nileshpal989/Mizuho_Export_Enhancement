<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDPMS_XMLFile_PendingGenerated.aspx.cs"
    Inherits="Reports_EDPMS_Reports_EDPMS_XMLFile_PendingGenerated" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../../Menu/Menu.ascx" TagName="menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/style_new.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function validateSave() {
            var ddlBranch = document.getElementById('ddlBranch');
            if (ddlBranch.value == 0) {
                alert('Select Branch Name.');
                ddlBranch.focus();
                return false;
            }
            var txtDate = document.getElementById('txtDate');
            if (txtDate.value == '') {
                alert('Enter Date');
                txtDate.focus();
                return false;
            }

            var rbtreceipt = document.getElementById('rbtreceipt');
            var rbtpayment = document.getElementById('rbtpayment');
            var doctype = "";

            if (rbtreceipt.checked == true) {
                doctype = 'DocBill';
            }
            else if (rbtpayment.checked == true) {
                doctype = 'Realized';
            }

            var rbtPending = document.getElementById('rbtPending');
            var rbtGenerated = document.getElementById('rbtGenerated');
            var status = "";
            if (rbtPending.checked == true) {
                status = 'P';

            } else if (rbtGenerated.checked == true) {
                status = 'G';

            }
            window.open('View_EDPMS_XMLFile_PendingGenerated.aspx?branchname=' + ddlBranch.value + '&date=' + txtDate.value + '&doctype=' + doctype + '&status=' + status, '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1080,height=600');
            //            winame.focus();
            return false;

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
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
            <uc1:menu ID="Menu1" runat="server" />
            <br />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" colspan="2">
                                <span class="pageLabel">XML File List</span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td width="5%" nowrap>
                                <span class="elementLabel">Branch : </span>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" runat="server" Width="10%">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="elementLabel">Date : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtDate" CssClass="textBox" runat="server" Width="70px"></asp:TextBox>
                                <asp:Button ID="btncalendar_FromDate" runat="server" CssClass="btncalendar_enabled"
                                    TabIndex="-1" />
                                <ajaxToolkit:MaskedEditExtender ID="mdfdate" Mask="99/99/9999" MaskType="Date" runat="server"
                                    TargetControlID="txtDate" InputDirection="RightToLeft" AcceptNegative="Left"
                                    ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                </ajaxToolkit:MaskedEditExtender>
                                <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtDate" PopupButtonID="btncalendar_FromDate">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdfdate"
                                    ValidationGroup="dtVal" ControlToValidate="txtDate" EmptyValueMessage="Enter Date Value"
                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                </ajaxToolkit:MaskedEditValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="left">
                                <asp:RadioButton ID="rbtreceipt" runat="server" CssClass="elementLabel" Checked="true"
                                    Text="Receipt of Document" GroupName="Doctype" />
                                <asp:RadioButton ID="rbtpayment" runat="server" CssClass="elementLabel" Text="Payment Realized"
                                    GroupName="Doctype" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="left">
                                <asp:RadioButton ID="rbtPending" runat="server" CssClass="elementLabel" Checked="true"
                                    GroupName="XML" Text="XML File Pending to be Generated" />
                                <asp:RadioButton ID="rbtGenerated" runat="server" CssClass="elementLabel" GroupName="XML"
                                    Text="XML File already Generated" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="left">
                                <asp:Button ID="btnGenerate" runat="server" CssClass="buttonDefault" Text="Generate" />
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
