<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HoildayMaster.aspx.cs" Inherits="IMP_HoildayMaster" %>

<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Help_Plugins/jquerynew.min.js" language="javascript" type="text/javascript"></script>
    <script src="../Help_Plugins/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <link href="../Help_Plugins/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Help_Plugins/AlertJquery.js" type="text/javascript"></script>
    <script src="../Help_Plugins/MyJquery1.js" type="text/javascript"></script>
    <%--snackbar--%>
    <link href="../Style/SnackBar.css" rel="stylesheet" type="text/css" />
    <%--snackbar jquery--%>
    <script src="../Help_Plugins/jquery-ui.js" type="text/javascript"></script>
    <link href="../Help_Plugins/JueryUI.css" rel="stylesheet" type="text/css" />
    <script src="../Help_Plugins/AlertJquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        //        snackbar
        function Alert(Result) {
            MyAlert(Result);
        }

        function confirmDelete() {
            MyConfirm('Do you want to delete this record?', '#btnDeleteConfirm');
        }
        //        function Alert(result) {
        //            VAlert(result, '#sfggfh');
        //        }
    </script>
    <script language="javascript" type="text/javascript">
        function curhelp() {

            popup = window.open('../TF_CurrencyLookUp2.aspx', 'helpCurId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "helpCurId2";
            return false;

        }

        function sss() {
            var s = popup.document.getElementById('txtcell1').value;
            //            var s1 = popup.document.getElementById('txtcell2').value;
            if (common == "helpCurId2") {
                document.getElementById('txtCurrCode').value = s;
                //                document.getElementById('lblCurrDesc').innerHTML = s1;

                javascript: setTimeout('__doPostBack(\'txtCurrCode\',\'\')', 0);
            }

        }

        function validateSave() {
            var txtCurrCode = document.getElementById('txtCurrCode').value;
            var ddlBranch = document.getElementById('ddlBranch').value;
            var txtHolidayDate = document.getElementById('txtHolidayDate').value;
            var txtToolTip = document.getElementById('txtToolTip').value;
            if (txtCurrCode == '') {
                //                alert('Enter Currency.');
                // document.getElementById('txtCurrCode').focus();
                VAlert('Enter Currency.', '#txtCurrCode');
                return false;
            }
            if (ddlBranch == '' && txtCurrCode == 'INR') {
                //                alert('select branch when Currency is INR.');
                //                document.getElementById('ddlBranch').focus();
                VAlert('select branch when Currency is INR.', '#ddlBranch');
                return false;
            }
            if (txtHolidayDate == '' || txtHolidayDate == '__/__/____') {
                //                alert('Enter Holiday Date.');
                //                document.getElementById('txtHolidayDate').focus();
                VAlert('Enter Holiday Date.', '#txtHolidayDate');
                return false;
            }
            if (txtToolTip == '') {
                //                alert('Enter Description.');
                //                document.getElementById('txtToolTip').focus();
                VAlert('Enter Description.', '#txtToolTip');
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <br />
            <%--  alert message--%>
            <div id="dialog" class="AlertJqueryHide">
                <p id="Paragraph">
                </p>
            </div>
            <%--   snackbar--%>
            <div id="snackbar">
                <div id="snackbarbody" style="padding-top: -500px;">
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnDeleteConfirm" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <input type="hidden" runat="server" id="hdnholidaymaster" />
                                <span class="pageLabel"><strong>Holiday Dates Details</strong></span>
                            </td>
                            <td align="right" style="width: 50%">
                                <input type="hidden" id="hdnholidayidDate" runat="server" />
                                <input type="hidden" id="hdnholidaydescription" runat="server" />
                                <input type="hidden" id="hdnADCode" runat="server" />
                                <input type="hidden" id="hdnCurrency" runat="server" />
                                <asp:Button ID="btnDeleteConfirm" Style="display: none;" runat="server" OnClick="btnDeleteConfirm_Click" />                                
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="2">
                                <table cellspacing="0" cellpadding="0" width="700px" style="line-height: 150%">
                                    <tr>
                                        <td align="right" width="10%" nowrap>
                                            <span class="mandatoryField">*</span><span class="elementLabel">Currency :</span>
                                        </td>
                                        <td align="left" width="40%">
                                            <%--<asp:DropDownList ID="ddlCurrency" CssClass="dropdownList" AutoPostBack="true" 
                                        runat="server" style="width:90px"
                                     TabIndex="1" onselectedindexchanged="ddlCurrency_SelectedIndexChanged">
                                </asp:DropDownList>--%>
                                            <asp:TextBox ID="txtCurrCode" runat="server" CssClass="textBox" Style="text-transform: uppercase;
                                                width: 40px" AutoPostBack="true" OnTextChanged="txtCurrCode_TextChanged" MaxLength="3"></asp:TextBox>
                                            <asp:Button ID="btnCur" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                            <asp:Label ID="lblCurrDesc" runat="server" CssClass="elementLabel"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;<asp:Label ID="lblMsg" runat="server" CssClass="mandatoryField"
                                                Text="[In Case of INR, Dates to be Entered Branchwise]" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="right">
                                        <td width="10%" align="right" nowrap>
                                            <span class="elementLabel">Branch :</span>
                                        </td>
                                        <td align="left" width="40%">
                                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" AutoPostBack="true" runat="server"
                                                TabIndex="2" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Holiday Date :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            <asp:TextBox ID="txtHolidayDate" runat="server" CssClass="textBox" MaxLength="10"
                                                ValidationGroup="dtVal" Width="70px" TabIndex="1" OnTextChanged="txtHolidayDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender ID="mdDocdate" Mask="99/99/9999" MaskType="Date"
                                                runat="server" TargetControlID="txtHolidayDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                CultureTimePlaceholder=":" Enabled="True">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <asp:Button ID="btncalendar_DocDate" runat="server" CssClass="btncalendar_enabled" />
                                            <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtHolidayDate" PopupButtonID="btncalendar_DocDate" Enabled="True">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdDocdate"
                                                ValidationGroup="dtVal" ControlToValidate="txtHolidayDate" EmptyValueMessage="Enter Date Value"
                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="MaskedEditValidator3"></ajaxToolkit:MaskedEditValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Description :</span>
                                        </td>
                                        <td align="left" style="width: 600px">
                                            <asp:TextBox ID="txtToolTip" runat="server" Width="250px" CssClass="textBox" AutoPostBack="true"
                                                TabIndex="2" OnTextChanged="txtToolTip_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 400px; padding-top: 10px; padding-bottom: 10px">
                                            &nbsp<asp:Button ID="btnSave" Text="Save" runat="server" ToolTip="Save" CssClass="buttonDefault"
                                                TabIndex="3" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="buttonDefault"
                                                TabIndex="4" OnClick="btnCancel_Click" />
                                            <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                        </td>
                                    </tr>
                                    <tr id="rowGrid" runat="server">
                                        <td align="left" style="width: 100%" valign="top" colspan="2">
                                            <asp:GridView ID="GridViewSpecialDates" runat="server" AutoGenerateColumns="False"
                                                OnSelectedIndexChanged="GridViewSpecialDates_SelectedIndexChanged" Width="100%"
                                                GridLines="Both" AllowPaging="true" OnRowCommand="GridViewSpecialDates_RowCommand"
                                                OnRowDataBound="GridViewSpecialDates_RowDataBound" CssClass="GridView">
                                                <PagerSettings Visible="false" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="12%" ItemStyle-Width="12%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSpecialDates" runat="server" Text='<%#Eval("SpecialDate")%>' CssClass="elementLabel"></asp:Label>
                                                            <asp:Label ID="lblCurr" runat="server" Text='<%#Eval("Currency")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblADCode" runat="server" Text='<%#Eval("AdCode")%>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="80%" ItemStyle-Width="80%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblToolTip" runat="server" Text='<%#Eval("toolTip")%>' CssClass="elementLabel"
                                                                CommandArgument='<%#Eval("SpecialDate")+";"+Eval("toolTip")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnEdit" runat="server" ToolTip="Edit Record" CommandName="EditRecord"
                                                                CommandArgument='<%#Eval("SpecialDate")+";"+Eval("toolTip")+";"+Eval("Currency")+";"+Eval("AdCode")%>' Text="Edit" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnDelete" runat="server" ToolTip="Delete Record" CommandName="RemoveRecord"
                                                                CommandArgument='<%#Eval("SpecialDate")+";"+Eval("toolTip")+";"+Eval("Currency")+";"+Eval("AdCode")%>' Text="Delete" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr id="rowPager" runat="server" visible="false">
                                        <td align="center" style="width: 100%" valign="top" colspan="2" class="gridHeader">
                                            <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td align="left" style="width: 25%">
                                                            &nbsp;Records per page:&nbsp;
                                                            <asp:DropDownList ID="ddlrecordperpage" runat="server" CssClass="dropdownList" AutoPostBack="true" OnSelectedIndexChanged="ddlrecordperpage_SelectedIndexChanged">
                                                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                                <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                                <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                                                <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="center" style="width: 40%" valign="top">
                                                            <asp:Button ID="btnnavfirst" runat="server" Text="First" ToolTip="First" OnClick="btnnavfirst_Click" />
                                                            <asp:Button ID="btnnavpre" runat="server" Text="Prev" ToolTip="Prev" OnClick="btnnavpre_Click" />
                                                            <asp:Button ID="btnnavnext" runat="server" Text="Next" ToolTip="Next" OnClick="btnnavnext_Click" />
                                                            <asp:Button ID="btnnavlast" runat="server" Text="Last" ToolTip="Last" OnClick="btnnavlast_Click" />
                                                        </td>
                                                        <td align="right" style="width: 35%;">
                                                            &nbsp;<asp:Label ID="lblpageno" runat="server"></asp:Label>
                                                            &nbsp;
                                                            <asp:Label ID="lblrecordno" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>                                    
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblMessage" runat="server" CssClass="mandatoryField"></asp:Label>
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
