<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Inward_Remittance_outstanding_statement.aspx.cs"
    Inherits="Reports_EXPORTReports_rpt_Inward_Remittance_outstanding_statement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LMCC - Trade Finance System</title>
    <link href="../../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function custhelp() {
            var adcode = document.getElementById('ddlBranch').value;
            popup = window.open('../../TF_CustomerLookUp2.aspx?adcode=' + adcode, 'helpCustId', 'height=520,  width=620,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "helpCustId"
            return false;

        }
        function selectCustomer(selectedID) {
            var id = selectedID;
            document.getElementById('txtCustomerID').value = selectedID;
            __doPostBack('txtCustomerID', '');
        }

        function validateSave() {

            var Branch = document.getElementById('ddlBranch');
            var AsOnDate = document.getElementById('txtAsOnDate');

            var Cust_AccNo;

            if (Branch.value == 0) {
                alert('Select Branch Name.');
                Branch.focus();
                return false;
            }

            if (AsOnDate.value == '') {
                alert('Select AsOn Date.');
                AsOnDate.focus();
                return false;
            }
            if (document.getElementById('rdbAllCustomer').checked == true) {
                Cust_AccNo = 'All';
            }
            else if (document.getElementById('txtCustomerID').value == '') {
                alert('Select Customer A/C no.');
                Cust_AccNo.focus();
                return false;
            }
            else {
                Cust_AccNo = document.getElementById('txtCustomerID').value;
            }

            var winname = window.open('rpt_Inward_Remmitance_Outstanding_statement_View.aspx?AsOnDate=' + AsOnDate.value + '&Cust_AccNo=' + Cust_AccNo + '&Branch=' + Branch.value + '&Report=IRM_Outstanding_Statement', 'IRM Outstanding Statement', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1200px,height=800px');
            winname.focus();
            return false;
        }
    </script>
</head>
<body>
    <form id="formIRM_Outstanding" runat="server" autocomplete="off">
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
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td align="left" colspan="2">
                                <span class="pageLabel"><strong>Inward Remittance Outstanding Report</strong></span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                <hr />
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td align="left" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="10%" align="right">
                                <span class="mandatoryField">* </span><span class="elementLabel">Branch :</span>
                            </td>
                            <td align="left">
                                &nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" AutoPostBack="true"
                                    Width="100px" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="mandatoryField">*</span><span class="elementLabel">As On IRM RECD Date:</span>
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtAsOnDate" runat="server" CssClass="textBox" MaxLength="10"
                                    ValidationGroup="dtVal" Width="70" TabIndex="2"></asp:TextBox>
                                <asp:Button ID="btncalendar_FromDate" runat="server" CssClass="btncalendar_enabled"
                                    TabIndex="-1" />
                                <ajaxToolkit:MaskedEditExtender ID="mdfdate" Mask="99/99/9999" MaskType="Date" runat="server"
                                    TargetControlID="txtAsOnDate" InputDirection="RightToLeft" AcceptNegative="Left"
                                    ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                </ajaxToolkit:MaskedEditExtender>
                                <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtAsOnDate" PopupButtonID="btncalendar_FromDate">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdfdate"
                                    ValidationGroup="dtVal" ControlToValidate="txtAsOnDate" EmptyValueMessage="Enter Date Value"
                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                </ajaxToolkit:MaskedEditValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdbAllCustomer" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                    GroupName="Customer" OnCheckedChanged="rdbCustomer_CheckedChanged" TabIndex="5"
                                    Text="All Customers" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdbSelectedCustomer" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                    GroupName="Customer" OnCheckedChanged="rdbCustomer_CheckedChanged" TabIndex="5"
                                    Text="Selected Customer" />
                            </td>
                        </tr>
                        <tr id="tr_Customer" runat="server">
                            <td align="right">
                                <span class="mandatoryField">*</span><span class="elementLabel">Customer A/C No:</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtCustomerID" runat="server" CssClass="textBox" MaxLength="40"
                                    TabIndex="6" AutoPostBack="True" OnTextChanged="txtCustomerID_TextChanged"></asp:TextBox>
                                &nbsp;
                                <asp:Button ID="btnCustList" runat="server" TabIndex="6" CssClass="btnHelp_enabled"
                                    OnClientClick="return custhelp();" />
                                &nbsp;
                                <asp:Label ID="lblCustomerName" runat="server" CssClass="elementLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr valign="bottom">
                            <td align="right">
                            </td>
                            <td align="left">
                                <asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" Text="Generate"
                                    ToolTip="Generate" TabIndex="7" OnClientClick="return validateSave();" />
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
