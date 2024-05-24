<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_EBRC_ORM_CheckerView.aspx.cs" Inherits="EDPMS_TF_EBRC_ORM_CheckerView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script src="../Scripts/InitEndRequest.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
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

            if (docNoLen < 6) {
                for (var i = docNoLen; i < 5; i++) {
                    _tempDocNo = 0 + _tempDocNo;
                }
                docNo.value = docYear.value.substring(3) + _tempDocNo;
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

    </script>
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
    </script>
    <style type="text/css">
        .style1 {
            height: 28px;
        }
    </style>

    <script type="text/javascript">
        // Select/Deselect checkboxes based on header checkbox
        function SelectheaderCheckboxes(headerchk) {
            debugger
            var gvcheck = document.getElementById('GridViewInwData');
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
            var gvcheck = document.getElementById('GridViewInwData');
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
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            var grid = document.getElementById('GridViewInwData');
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
                if (confirm("Do you want to approve bulk ORM transaction?")) {
                    confirm_value.value = "Yes";
                }
                else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManagerMain" runat="server">
        </asp:ScriptManager>
        <script src="../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
        <script src="../Scripts/InitEndRequest.js" type="text/javascript"></script>
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

                    <ContentTemplate>
                        <table cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="left" nowrap class="style1">
                                    <span class="pageLabel"><strong>EBRC ORM DataEntry View - Checker</strong></span>
                                </td>
                                <td align="right" class="style1">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="buttonDefault" ToolTip="Add New Record"
                                        AccessKey="A" OnClick="btnAdd_Click" />

                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="1" align="left">
                                    <asp:Label ID="lblmessage1" runat="server" CssClass="mandatoryField"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <span class="elementLabel">Branch : </span>
                                    <asp:DropDownList ID="ddlbranch" runat="server" CssClass="dropdownList"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    <span class="elementLabel">Search :</span> &nbsp;<asp:TextBox ID="txtSearch" runat="server"
                                        CssClass="textBox" MaxLength="40" Width="180px" TabIndex="5"></asp:TextBox>
                                    <asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="buttonDefault"
                                        ToolTip="Search" OnClick="btnSearch_Click" />
                                </td>
                            </tr>
                            </tr>
                        <tr>
                            <td colspan="2">
                                <hr />
                            </td>
                        </tr>

                            <tr>
                                <td align="left" colspan="2">&nbsp;<span class="mandatoryField">*</span><span class="elementLabel"><span class="elementLabel">From Date : </span>
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
                                    &nbsp;&nbsp;
                                            <asp:Button ID="btnsearchrecord" runat="server"
                                                Text="Search Records" CssClass="buttonDefault"
                                                ToolTip="Search Records" OnClick="btnsearchrecord_Click" />

                                </td>
                            </tr>
                            <tr>
                                <%--<td align="left" nowrap width="10%">
                                <span class="elementLabel">Document No :</span>
                                <asp:TextBox ID="txtDocPrFx" runat="server" CssClass="textBox" ReadOnly="True" 
                                    text="IRM" Width="30px"></asp:TextBox>
                                <asp:TextBox ID="txtDocumentNo" runat="server" CssClass="textBox" MaxLength="5" 
                                    TabIndex="3" Width="60px"></asp:TextBox>
                                <asp:TextBox ID="txtYear" runat="server" AutoPostBack="true" CssClass="textBox" 
                                    MaxLength="4" TabIndex="4" Width="50px" 
                                    ontextchanged="txtYear_TextChanged"></asp:TextBox>
                            </td>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:Label ID="lblMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                                </td>
                            </tr>--%>
                                <tr>
                                    <td align="left" style="width: 60%;" valign="top">
                                        <asp:RadioButton ID="rdbAll" runat="server" Text="All" AutoPostBack="true" Checked="true"
                                            GroupName="Status" OnCheckedChanged="rdbAll_CheckedChanged" Font-Bold="true" />
                                        <asp:RadioButton ID="rdbApproved" runat="server" Text="Approved" Font-Bold="true"
                                            AutoPostBack="true" GroupName="Status" OnCheckedChanged="rdbApproved_CheckedChanged"
                                            ForeColor="GreenYellow" />
                                        <asp:RadioButton ID="rdbRejected" runat="server" Text="Rejected" Font-Bold="true"
                                            AutoPostBack="true" GroupName="Status" OnCheckedChanged="rdbRejected_CheckedChanged"
                                            ForeColor="Tomato" />
                                        <asp:RadioButton ID="rdbPending" runat="server" Text="Pending" Font-Bold="true"
                                            AutoPostBack="true" GroupName="Status" OnCheckedChanged="rdbPending_CheckedChanged"
                                            ForeColor="BlueViolet" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:DropDownList ID="ddlOrmstatus" runat="server" CssClass="dropdownList"
                                            AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlOrmstatus_SelectedIndexChanged" Width="124px">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Fresh</asp:ListItem>
                                            <asp:ListItem Value="2">Amended</asp:ListItem>
                                            <asp:ListItem Value="3">Cancelled</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btnapprove" runat="server" CssClass="buttonDefault"
                                            OnClick="btnapprove_Click" Text="Approve" Style="display: none" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <asp:Label ID="lblMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="rowGrid" runat="server">
                                    <td colspan="2">
                                        <asp:GridView ID="GridViewInwData" runat="server" AllowPaging="True"
                                            AllowSorting="True" AutoGenerateColumns="False" EditIndex="1"
                                            ShowHeaderWhenEmpty="True"
                                            Width="100%" OnRowCommand="GridViewInwData_RowCommand"
                                            OnRowDataBound="GridViewInwData_RowDataBound">
                                            <PagerSettings Visible="false" />
                                            <RowStyle CssClass="gridItem" Height="18px" HorizontalAlign="Left"
                                                VerticalAlign="top" Wrap="false" />
                                            <HeaderStyle CssClass="gridHeader" ForeColor="#1a60a6" VerticalAlign="Top" />
                                            <AlternatingRowStyle CssClass="gridAlternateItem" Height="18px"
                                                HorizontalAlign="left" VerticalAlign="Middle" Wrap="false" />
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%"
                                                    HeaderText="Bank Unique Tx Id" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBkUniqueTxId" runat="server" CssClass="elementLabel"
                                                            Text='<%# Eval("BkUniqueTxId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%"
                                                    HeaderText="ORM Issue Date" ItemStyle-HorizontalAlign="center" ItemStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblORMIssueDate" runat="server" CssClass="elementLabel"
                                                            Text='<%# Eval("ORMIssueDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%"
                                                    HeaderText="ORM No" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblORMNo" runat="server" CssClass="elementLabel"
                                                            Text='<%# Eval("ORMNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="5%"
                                                    HeaderText="AD Code" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblADCode" runat="server" CssClass="elementLabel"
                                                            Text='<%# Eval("ADCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="3%"
                                                    HeaderText="ORN FCC" ItemStyle-HorizontalAlign="center" ItemStyle-Width="3%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblORNFCC" runat="server" CssClass="elementLabel"
                                                            Text='<%# Eval("ORNFCC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%"
                                                    HeaderText="ORN FCC Amt" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblORNFCCAmt" runat="server" CssClass="elementLabel"
                                                            Text='<%# string.Format("{0:f}",DataBinder . Eval(Container.DataItem,"ORNFCCAmt")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="8%"
                                                    HeaderText="INR Payable Amt" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblINRPayableAmt" runat="server" CssClass="elementLabel"
                                                            Text='<%# string.Format("{0:f}",DataBinder . Eval(Container.DataItem,"INRPayableAmt")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%"
                                                    HeaderText="Beneficiary Name " ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbBeneficiaryName" runat="server" CssClass="elementLabel"
                                                            Text='<%# Eval("BenefName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="6%"
                                                    HeaderText="Beneficiary Country" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBeneficiaryCountry" runat="server" CssClass="elementLabel"
                                                            Text='<%# Eval("BenefCountry") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="6%"
                                                    HeaderText="Purpose Code" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPurpose_Code" runat="server" CssClass="elementLabel"
                                                            Text='<%# Eval("PurposeCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%"
                                                    HeaderText="Reference IRM" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRefIRM" runat="server" CssClass="elementLabel"
                                                            Text='<%# Eval("RefIRM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="6%"
                                                    HeaderText="ORM Status" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblormstatus" runat="server" CssClass="elementLabel"
                                                            Text='<%# Eval("ORMStatus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="6%"
                                                    HeaderText="API Status" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAPIstatus" runat="server" CssClass="elementLabel"
                                                            Text='<%# Eval("API Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="6%"
                                                    HeaderText="DGFT Status" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDGFTstatus" runat="server" CssClass="elementLabel"
                                                            Text='<%# Eval("DGFT Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="6%"
                                                    HeaderText="Status" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="6%" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstatus" runat="server" CssClass="elementLabel"
                                                            Text='<%# Eval("[Status]") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="6%" 
                                                HeaderText="Delete" ItemStyle-HorizontalAlign="center" ItemStyle-Width="6%">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnDelete" runat="server" Autopostback="true" 
                                                        CommandArgument='<%# Eval("ORMNo") %>' CommandName="RemoveRecord" 
                                                        Text="Delete" ToolTip="Delete Record" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                                <%--<asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="2%" 
                                                 ItemStyle-HorizontalAlign="center" ItemStyle-Width="2%">
                                                 <HeaderTemplate> 
                                                     <asp:CheckBox runat="server" ID="chkall" AutoPostBack="true" 
                                                            onclick="javascript:SelectheaderCheckboxes(this)" 
                                                            oncheckedchanged="chkall_CheckedChanged"/>
                                                 </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" ID="chkselect" AutoPostBack="true" CommandArgument="View profile" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr id="rowPager" runat="server">
                                    <td align="center" class="gridHeader" colspan="2" style="width: 100%"
                                        valign="top">
                                        <table border="0" cellpadding="2" cellspacing="0" class="gridHeader"
                                            width="100%">
                                            <tbody>
                                                <tr>
                                                    <td align="left" style="width: 25%">&nbsp;Records per page:&nbsp;
                                                    <asp:DropDownList ID="ddlrecordperpage" runat="server" AutoPostBack="true"
                                                        CssClass="dropdownList"
                                                        OnSelectedIndexChanged="ddlrecordperpage_SelectedIndexChanged1">
                                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                        <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                                        <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    </td>
                                                    <td align="center" style="width: 50%" valign="top">
                                                        <asp:Button ID="btnnavfirst" runat="server"
                                                            Text="First" ToolTip="First" OnClick="btnnavfirst_Click" />
                                                        <asp:Button ID="btnnavpre" runat="server" Text="Prev"
                                                            ToolTip="Previous" OnClick="btnnavpre_Click" />
                                                        <asp:Button ID="btnnavnext" runat="server"
                                                            Text="Next" ToolTip="Next" OnClick="btnnavnext_Click" />
                                                        <asp:Button ID="btnnavlast" runat="server"
                                                            Text="Last" ToolTip="Last" OnClick="btnnavlast_Click" />
                                                    </td>
                                                    <td align="right" style="width: 25%;">&nbsp;<asp:Label ID="lblpageno" runat="server"></asp:Label>
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

                                        <asp:Label runat="server" ID="lbl"></asp:Label>
                                    </td>
                                </tr>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </center>

        </div>
    </form>
</body>
</html>
