<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EXP_ViewDelinkingEntry.aspx.cs"
    Inherits="EXP_EXP_ViewDelinkingEntry" %>

<%@ Register Src="../Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
     <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
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
        function checkYear() {

            var d = new Date();
            var docYear = document.getElementById('txtYear');
            var docYearLen = docYear.value.length;

            if (docYearLen > 3) {

                if (parseFloat(docYear.value) > 2000 && parseFloat(docYear.value) < 2050) {
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
            var branchCode = document.getElementById('txtBranchCode');

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
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <br />
            <asp:ScriptManager ID="ScriptManagerMain" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <%--                <Triggers>
                    <asp:PostBackTrigger ControlID="btnAdd" />
                </Triggers>--%>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom" colspan="2">
                                <span class="pageLabel">Export Delinking Entry View</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr align="right">
                            <td width="15%" align="left" nowrap>
                                <span class="elementLabel">Branch :</span><asp:DropDownList ID="ddlBranch" CssClass="dropdownList"
                                    AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                                    TabIndex="1">
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;&nbsp;<span class="elementLabel">Year :</span>
                                <asp:TextBox ID="txtYear" runat="server" CssClass="textBox" Width="50px" OnTextChanged="txtYear_TextChanged"
                                    AutoPostBack="true" TabIndex="4" MaxLength="4"></asp:TextBox>
                            </td>
                            <td align="right" style="width: 100%;" valign="top">
                                <span class="elementLabel">Search :</span> &nbsp;<asp:TextBox ID="txtSearch" runat="server"
                                    CssClass="textBox" MaxLength="40" Width="180px"></asp:TextBox>
                                &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="buttonDefault"
                                    ToolTip="Search" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="35%" nowrap>
                                <asp:RadioButton ID="rdbNego" Text="Negotiation" CssClass="elementLabel" runat="server"
                                    GroupName="DocType" TabIndex="23" Checked="true" AutoPostBack="true" OnCheckedChanged="rdbNego_CheckedChanged" /><asp:RadioButton
                                        ID="rdbPur" Text="Purchase" CssClass="elementLabel" runat="server" GroupName="DocType"
                                        TabIndex="23" AutoPostBack="true" OnCheckedChanged="rdbPur_CheckedChanged" /><asp:RadioButton
                                            ID="rdbDis" Text="Discount" CssClass="elementLabel" runat="server" GroupName="DocType"
                                            TabIndex="23" AutoPostBack="true" OnCheckedChanged="rdbDis_CheckedChanged" />
                                <asp:RadioButton ID="rdbEBR" Text="EBR" CssClass="elementLabel" runat="server" GroupName="DocType"
                                    TabIndex="23" AutoPostBack="true" OnCheckedChanged="rdbEBR_CheckedChanged" />
                                <asp:RadioButton ID="rdbColl" Text="Collection" CssClass="elementLabel" runat="server"
                                    GroupName="DocType" TabIndex="23" AutoPostBack="true" OnCheckedChanged="rdbColl_CheckedChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; height: 21px;" valign="top" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr id="rowGrid" runat="server">
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <asp:GridView ID="GridViewDelinking" runat="server" AutoGenerateColumns="false" Width="100%"
                                    GridLines="Both" AllowPaging="true" OnRowDataBound="GridViewDelinking_RowDataBound">
                                    <PagerSettings Visible="false" />
                                    <RowStyle Wrap="false" HorizontalAlign="Left" Height="18px" VerticalAlign="Top" CssClass="gridItem" />
                                    <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
                                    <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                        CssClass="gridAlternateItem" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Document No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="blDocNo" runat="server" Text='<%# Eval("DocNo") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rec Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRecDate" runat="server" Text='<%# Eval("RecDt") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBillType" runat="server" Text='<%# Eval("BillType") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer Name" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCountryName" runat="server" Text='<%# Eval("CustName") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cur" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCur" runat="server" Text='<%# Eval("Cur") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill Amt" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBillAmt" runat="server" Text='<%# Eval("BillAmt") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="right" Width="10%" />
                                            <ItemStyle HorizontalAlign="Right" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nego Amt" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNegoAmt" runat="server" Text='<%# Eval("NegoAmt") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                            <ItemStyle HorizontalAlign="Right" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delinked Date" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDelinkedDate" runat="server" Text='<%# Eval("DelinkedDt") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                            <ItemStyle HorizontalAlign="Right" Width="10%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr id="rowPager" runat="server">
                            <td align="center" style="width: 100%" valign="top" colspan="2" class="gridHeader">
                                <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td align="left" width="25%">
                                                &nbsp;Records Per Page :&nbsp;
                                                <asp:DropDownList ID="ddlrecordpage" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlrecordpage_SelectedIndexChanged">
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
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
</body>
</html>
