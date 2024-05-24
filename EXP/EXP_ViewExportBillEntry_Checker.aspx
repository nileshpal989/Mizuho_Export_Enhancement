<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EXP_ViewExportBillEntry_Checker.aspx.cs" Inherits="EXP_EXP_ViewExportBillEntry_Checker" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script src="../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
   <%-- <script src="../Scripts/InitEndRequest.js" type="text/javascript"></script>--%>
    <script type="text/javascript" language="javascript">
        function validateSearch() {
            var _txtvalue = document.getElementById('txtSearch').value;
            _txtvalue = _txtvalue.replace(/'&lt;'/, "");
            _txtvalue = _txtvalue.replace(/'&gt;'/, "");
            if (_txtvalue.indexOf('<!') != -1 || _txtvalue.indexOf('>!') != -1 || _txtvalue.indexOf('!') != -1 || _txtvalue.indexOf('<') != -1 || _txtvalue.indexOf('>') != -1 || _txtvalue.indexOf('|') != -1) {
                alert('!, |, <, and > are not allowed.');
                document.getElementById('txtSearch').value = _txtvalue;
                return false;
            }
            else
                return true;
        }

        function submitForm(event) {
            if (event.keyCode == '13') {
                if (validateSearch() == true)
                    __doPostBack('btnSearch', '');
                else
                    return false;
            }
        }
        function checkDocNo() {
            var docNo = document.getElementById('txtDocumentNo');
            var _tempDocNo;
            var docYear = document.getElementById('txtYear');
            var docNoLen = docNo.value.length;

            if (docNoLen == 0) {
                docNoLen = 1;
                docNo.value = 1;
            }
            if (docNo.value == 0) {
                docNoLen = 1;
                docNo.value = 1;
            }

            _tempDocNo = docNo.value;

            if (docNoLen < 4) {
                for (var i = docNoLen; i < 4; i++) {
                    _tempDocNo = 0 + _tempDocNo;
                }
                docNo.value = _tempDocNo;
            }

        }


        function checkYear() {

            var d = new Date();
            var docYear = document.getElementById('txtYear');
            var docYearLen = docYear.value.length;

            if (docYearLen > 3) {

                if (parseFloat(docYear.value) > 1990 && parseFloat(docYear.value) < 2050) {
                    return false;
                }
                else
                    docYear.value = d.getFullYear();
            }

            else
                docYear.value = d.getFullYear();
        }

        function validate() {
            var docYear = document.getElementById('txtYear');
            var docNo = document.getElementById('txtDocumentNo');
            var branchCode = document.getElementById('txtBranchCode');

            if (docNo.value == '') {
                alert('Enter Document No.');
                docNo.focus();
                return false;
            }
            if (branchCode.value == '') {
                alert('Enter Branch Code');
                branchCode.focus();
                return false;
            }
            if (docYear.value == '') {
                alert('Enter Year');
                docYear.focus();
                return false;
            }
        }
        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            // alert(charCode);
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }
    </script>
    <%------------------------Anand06-09-2023-------------------------------------------%>
    <script type="text/javascript">
        // Select/Deselect checkboxes based on header checkbox
        function SelectheaderCheckboxes(headerchk) {
            debugger
            var gvcheck = document.getElementById('GridViewEXPbillEntry');
            var i;
            //Condition to check header checkbox selected or not if that is true checked all checkboxes
            if (headerchk.checked) {
                for (i = 0; i < gvcheck.rows.length; i++) {
                    var inputs = gvcheck.rows[i].getElementsByTagName('input');
                    inputs[0].checked = true;
                }
            }
            //if condition fails uncheck all checkboxes in gridview
            else {
                for (i = 0; i < gvcheck.rows.length; i++) {
                    var inputs = gvcheck.rows[i].getElementsByTagName('input');
                    inputs[0].checked = false;
                }
            }
        }
        //function to check header checkbox based on child checkboxes condition
        function Selectchildcheckboxes(header) {
            var ck = header;
            var count = 0;
            var gvcheck = document.getElementById('GridViewEXPbillEntry');
            var headerchk = document.getElementById(header);
            var rowcount = gvcheck.rows.length;
            //By using this for loop we will count how many checkboxes has checked
            for (i = 1; i < gvcheck.rows.length; i++) {
                var inputs = gvcheck.rows[i].getElementsByTagName('input');
                if (inputs[0].checked) {
                    count++;
                }
            }
            //Condition to check all the checkboxes selected or not
            if (count == rowcount - 1) {
                headerchk.checked = true;
            }
            else {
                headerchk.checked = false;
            }
        }
</script>
<script type = "text/javascript">
    function Confirm() {
        var confirm_value = document.createElement("INPUT");
        var grid = document.getElementById('GridViewEXPbillEntry');
        var checkBoxes = grid.getElementsByTagName("INPUT");
        confirm_value.type = "hidden";
        confirm_value.name = "confirm_value";
        var rowCount = 0;
        var rowCount1 = 0;
        for (var i = 0; i < checkBoxes.length; i++) {
            if (checkBoxes[i].checked == false) {
                rowCount++;
            }
            else {
                rowCount1++;
            }
        }
        if (rowCount1 != 0) {
            if (confirm("Do you want to bulk approve Digi-Export transaction?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    }
    </script>

    <script type = "text/javascript">
        function Confirm1() {
            var confirm_value = document.createElement("INPUT");
            var grid = document.getElementById('GridViewEXPbillEntry');
            var checkBoxes = grid.getElementsByTagName("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            var rowCount = 0;
            var rowCount1 = 0;
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].checked == false) {
                    rowCount++;
                }
                else {
                    rowCount1++;
                }
            }
            if (rowCount1 != 0) {
                if (confirm("Do you want to bulk Delete Digi-Export transaction?")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
            }
        }
    </script>

    <%-----------------------------------End--------------------------------------%>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/Enable_Disable_Opener.js"></script>
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
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnAdd" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" valign="bottom">
                                <span class="pageLabel"><strong>Export Bill Entry Checker</strong></span>
                            </td>
                            <td align="right">
                                <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="buttonDefault" ToolTip="Add New Record"
                                    OnClick="btnAdd_Click" TabIndex="3" style="visibility:hidden" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" colspan="2">
                                <hr />
                                <input type="hidden" id="hdnBranchCode" runat="server" />
                                <input type="hidden" id="hdnUserRole" runat="server" />
                            </td>
                        </tr>
                          </table>
                         <table cellspacing="0" cellpadding="0" border="0" width="100%">
                        <tr>
                            <td style="white-space: nowrap; width: 5%; text-align: right">
                                <span class="elementLabel">Branch : </span>
                            </td>
                            <td style="white-space: nowrap; width: 45%; text-align: left">
                                &nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" AutoPostBack="true"
                                    runat="server" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" TabIndex="1">
                                </asp:DropDownList>
                                <input id="Hidden1" type="hidden" runat="server" />
                                <input type="hidden" id="Hidden2" runat="server" />
                                 &nbsp; &nbsp;&nbsp; &nbsp;
                                <span class="elementLabel">Year :</span>
                                &nbsp;<asp:TextBox ID="txtYear" runat="server" CssClass="textBox" Width="20px" OnTextChanged="txtYear_TextChanged"
                                    AutoPostBack="true" TabIndex="2" MaxLength="2"></asp:TextBox>
                                
                                &nbsp; &nbsp;
                                <asp:RadioButton ID="rbtnForeign" runat="server" CssClass="elementLabel" Text="Foreign"
                                    GroupName="FL" AutoPostBack="true" Checked="true" OnCheckedChanged="rbtnForeign_CheckedChanged" />
                                <asp:RadioButton ID="rbtnLocal" runat="server" CssClass="elementLabel" Text="Local"
                                    GroupName="FL" AutoPostBack="true" OnCheckedChanged="rbtnLocal_CheckedChanged" />
                                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                <span class="elementLabel">Document Date : </span>                                
                                <asp:TextBox ID="txtDocDate" runat="server" CssClass="textBox" AutoPostBack="true" Style="width: 70px"
                                    TabIndex="3" OnTextChanged="txtDocDate_TextChanged" ></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="mdDocdate" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtDocDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="btncalendar_DocDate" runat="server" CssClass="btncalendar_enabled"
                                    TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtDocDate" PopupButtonID="btncalendar_DocDate" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdDocdate"
                                    ValidationGroup="dtVal" ControlToValidate="txtDocDate" EmptyValueMessage="Enter Date Value"
                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                            <%--</td>
                            <td align="right" style="width: 50%;" valign="top">--%> &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;
                                 
                                 <asp:RadioButton ID="rdbPending" runat="server" Text="Pending" Font-Bold="true" Checked="true"
                                    AutoPostBack="true" GroupName="Status" OnCheckedChanged="rdbPending_CheckedChanged"
                                    ForeColor="MediumOrchid" /> &nbsp;
                                <asp:RadioButton ID="rdbRejected" runat="server" Text="Rejected" Font-Bold="true"
                                    AutoPostBack="true" GroupName="Status" OnCheckedChanged="rdbRejected_CheckedChanged"
                                    ForeColor="Tomato" /> &nbsp;
                                <asp:RadioButton ID="rdbApproved" runat="server" Text="Approved" Font-Bold="true"
                                    AutoPostBack="true" GroupName="Status" OnCheckedChanged="rdbApproved_CheckedChanged"
                                    ForeColor="GreenYellow" /> &nbsp;
                                <asp:RadioButton ID="rdbAll" runat="server" Text="All" AutoPostBack="true" 
                                    GroupName="Status" OnCheckedChanged="rdbAll_CheckedChanged" Font-Bold="true" /> 
                            </td>
                            <td align="right" style="width: 50%;" valign="top">
                                <span class="elementLabel" style="margin-left:25px;">Search :</span> &nbsp;<asp:TextBox ID="txtSearch" runat="server"
                                    CssClass="textBox" MaxLength="40" Width="180px" TabIndex="4"></asp:TextBox>
                                &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="buttonDefault"
                                    ToolTip="Search" OnClick="btnSearch_Click" TabIndex="5" />
                            </td>
                        </tr>
                        </table>
                        <table width="100%" border="0">
                        <tr style="height: 30px;" valign="bottom">
                            <td width="20%" nowrap align="left">
                                <asp:RadioButton ID="rbtbla" runat="server" CssClass="elementLabel"
                                    Text="Bills Bought with L/C at Sight" GroupName="TransType" OnCheckedChanged="rbtbla_CheckedChanged"
                                    AutoPostBack="true" />&nbsp;
                            </td>
                            <td width="20%" nowrap align="left">
                                <asp:RadioButton ID="rbtbba" runat="server" CssClass="elementLabel" Text="Bills Bought without L/C at Sight"
                                    GroupName="TransType" OnCheckedChanged="rbtbba_CheckedChanged" AutoPostBack="true" />&nbsp;
                            </td>
                            <td width="20%" nowrap align="left">
                                <asp:RadioButton ID="rbtbca" runat="server" CssClass="elementLabel" Text="Bills For Collection at Sight "
                                    GroupName="TransType" OnCheckedChanged="rbtbcs_CheckedChanged" AutoPostBack="true" />&nbsp;
                            </td>
                            <td width="20%" nowrap align="left">
                                <asp:RadioButton ID="rbtIBD" runat="server" CssClass="elementLabel" Text="Vendor Bills Financing"
                                    GroupName="TransType" AutoPostBack="true" OnCheckedChanged="rbtIBD_CheckedChanged" Enabled="false" />&nbsp;
                            </td>
                            <td align="left">
                                <asp:RadioButton ID="rbtBEB" runat="server" CssClass="elementLabel" Text="Advance"
                                    GroupName="TransType" AutoPostBack="true" OnCheckedChanged="rbtBEB_CheckedChanged" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="20%" nowrap align="left">
                                <asp:RadioButton ID="rbtblu" runat="server" CssClass="elementLabel" Text="Bills Bought with L/C Usance"
                                    GroupName="TransType" OnCheckedChanged="rbtblu_CheckedChanged" AutoPostBack="true" />&nbsp;
                            </td>
                            <td width="20%" nowrap align="left">
                                <asp:RadioButton ID="rbtbbu" runat="server" CssClass="elementLabel" Text="Bills Bought without L/C Usance"
                                    GroupName="TransType" OnCheckedChanged="rbtbbu_CheckedChanged" AutoPostBack="true" />&nbsp;
                            </td>
                            <td width="20%" nowrap align="left">
                                <asp:RadioButton ID="rbtbcu" runat="server" CssClass="elementLabel" Text="Bills For Collection Usance"
                                    GroupName="TransType" OnCheckedChanged="rbtbcu_CheckedChanged" AutoPostBack="true" />&nbsp;
                            </td>
                            <td width="20%" nowrap align="left">
                                <asp:RadioButton ID="rbAll" runat="server" CssClass="elementLabel" Text="All" Checked="true"
                                    GroupName="TransType" OnCheckedChanged="rbAll_CheckedChanged" AutoPostBack="true" />&nbsp;
                            </td>
                            <td width="20%" nowrap align="left">
                                <asp:RadioButton ID="rbDigitExport" runat="server" CssClass="elementLabel" Text="Digi-Export"
                                    GroupName="TransType" OnCheckedChanged="rbDigitExport_CheckedChanged" AutoPostBack="true" />&nbsp;
                            </td>
                            
                            <td width="20%" nowrap align="left">
                            </td>
                            <td>
                            </td>
                        </tr>
                       
                        <tr>
                            <td nowrap align="left" colspan="2">
                                <%--<span class="elementLabel">Document No :</span>--%>
                                <asp:TextBox ID="txtDocPrFx" runat="server" CssClass="textBox" Style="width: 30px;"
                                    ReadOnly="True" visible="false"></asp:TextBox>
                                <asp:TextBox ID="txtBranchCode" runat="server" CssClass="textBox" Style="width: 30px;"
                                    TabIndex="3" MaxLength="5" visible="false"></asp:TextBox>
                                <%--<asp:TextBox ID="txtYear" runat="server" CssClass="textBox" Style="width: 20px;"
                                    visible="false" TabIndex="4" MaxLength="2"></asp:TextBox>--%>
                                <asp:TextBox ID="txtDocumentNo" runat="server" CssClass="textBox" Style="width: 40px;"
                                    visible="false" TabIndex="3" MaxLength="5"></asp:TextBox>
                            </td>
                            <td colspan="3" align="right">
                                <asp:Button ID="btnapprove" runat="server"  Visible="false" CssClass="buttonDefault" 
                                        onclick="btnapprove_Click" Text="Bulk Approve"/>
                                <asp:Button ID="btndelete" runat="server" Visible="false" CssClass="buttonDefault" 
                                        onclick="btndelete_Click" Text="Bulk Delete"/>
                            </td>
                        </tr>
                        <tr>
                        
                            <td align="left" valign="top" colspan="5">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                            </tr>
                        <tr id="rowGrid" runat="server">
                            <td align="left" valign="top" colspan="5">
                                <asp:GridView ID="GridViewEXPbillEntry" runat="server" AutoGenerateColumns="false"
                                    Width="100%" GridLines="Both" AllowPaging="true" OnRowCommand="GridViewEXPbillEntry_RowCommand"
                                    OnRowDataBound="GridViewEXPbillEntry_RowDataBound">
                                    <PagerSettings Visible="false" />
                                    <RowStyle Wrap="false" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                        CssClass="gridItem" />
                                    <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Middle" CssClass="gridHeader" />
                                    <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                        CssClass="gridAlternateItem" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Document No" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocumentNo" runat="server" Text='<%# Eval("Document_No") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer Name" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="35%" ItemStyle-Width="35%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustomer" runat="server" Text='<%# Eval("CUST_NAME") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date Received" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDateReceived" runat="server" Text='<%# Eval("Date_Received","{00:dd/MM/yyyy}") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date Negotiated" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDateNegotiated" runat="server" Text='<%# Eval("Date_Negotiated","{00:dd/MM/yyyy}") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Curr" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCurrency" runat="server" Text='<%# Eval("Currency") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill Amount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBillAmount" runat="server" Text='<%# Eval("Bill_Amount","{0:0.00}") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Disptach Indicator" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDispByDefaltuValue" runat="server" Text='<%# Eval("DispByDefaltuValue") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("Status") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Maker Name" HeaderStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAddedby" runat="server" Text='<%# Eval("Addedby") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approver Name" HeaderStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCheckby" runat="server" Text='<%# Eval("Checkby") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="2%" 
                                                 ItemStyle-HorizontalAlign="center" ItemStyle-Width="2%">
                                                 <HeaderTemplate> 
                                                     <asp:CheckBox runat="server" ID="chkall" AutoPostBack="true"  Enabled="false"
                                                            onclick="javascript:SelectheaderCheckboxes(this)" 
                                                            oncheckedchanged="chkall_CheckedChanged"/>
                                                 </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" ID="chkselect"  AutoPostBack="true" Enabled="false" CommandArgument="View profile" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr id="rowPager" runat="server">
                            <td align="center" valign="top" colspan="5" class="gridHeader">
                                <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td align="left" width="25%">
                                                &nbsp;Records Per Page :&nbsp;
                                                <asp:DropDownList ID="ddlrecordperpage" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlrecordperpage_SelectedIndexChanged">
                                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                    <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                    <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                                    <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" valign="top" width="50%">
                                                <asp:Button ID="btnnavfirst" runat="server" Text="First" ToolTip="First" OnClick="btnnavfirst_Click" />
                                                <asp:Button ID="btnnavpre" runat="server" Text="Prev" ToolTip="Previous" OnClick="btnnavpre_Click" />
                                                <asp:Button ID="btnnavnext" runat="server" Text="Next" ToolTip="Next" OnClick="btnnavnext_Click" />
                                                <asp:Button ID="btnnavlast" runat="server" Text="Last" ToolTip="Last" OnClick="btnnavlast_Click" />
                                            </td>
                                            <td align="right" style="width: 25%;">
                                                &nbsp;<asp:Label ID="lblpageno" runat="server"></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="lblrecordno" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                        <td> 
                        <asp:Label ID=lblcount runat="server"></asp:Label>
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
