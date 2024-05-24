<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_Settlement_Maker_View.aspx.cs" Inherits="IMP_Transactions_TF_IMP_Settlement_Maker_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <%--<link href="../../Style/Style.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../Style/Style_V2.css" rel="Stylesheet" type="text/css" media="screen" />
    <link href="../../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
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
        function validate() {
            var txtDocPrFx = document.getElementById('txtDocPrFx');
            var branchCode = document.getElementById('txtBranchCode');
            var docYear = document.getElementById('txtYear');
            var docNo = document.getElementById('txtDocumentNo');
            var rdb_LC = document.getElementById('rdb_LC');
            var ddl_Doc_Scrutiny = document.getElementById('ddl_Doc_Scrutiny');

            if (txtDocPrFx.value == '') {
                alert('Select Document Type.');
                docNo.focus();
                return false;
            }
            if (branchCode.value == '') {
                alert('Select Branch.');
                branchCode.focus();
                return false;
            }
            if (docYear.value == '') {
                alert('Enter Year.');
                docYear.focus();
                return false;
            }
            if (docNo.value == '') {
                alert('Enter Document No.');
                docNo.focus();
                return false;
            }
            if (rdb_LC.checked == true) {
                if (ddl_Doc_Scrutiny.value == '0') {
                    alert('Select Document Scrutiny.');
                    ddl_Doc_Scrutiny.focus();
                    return false;
                }
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
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" valign="bottom" colspan="2">
                                <span class="pageLabel"><strong>Import Bill Settlement View - Maker</strong></span>
                            </td>
                            <td align="right">
                                
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" colspan="3">
                                <hr />
                            </td>
                        </tr>
                        <tr align="right">
                            <td width="15%" align="center">
                                <span class="elementLabel">Branch :</span>
                                <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" AutoPostBack="true" runat="server"
                                    OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td width="20%" align="left">
                                <asp:RadioButton ID="rdb_Foreign" Text="Foreign" CssClass="elementLabel" runat="server"
                                    GroupName="FL" Checked="True" TabIndex="1" AutoPostBack="true" OnCheckedChanged="rdb_Foreign_CheckedChanged" />
                                <asp:RadioButton ID="rdb_Local" Text="Local" CssClass="elementLabel" runat="server"
                                    GroupName="FL" TabIndex="1" AutoPostBack="true" OnCheckedChanged="rdb_Local_CheckedChanged" />
                            </td>
                            <td align="right" width="75%" valign="top">
                                <span class="elementLabel">Search :</span> &nbsp;<asp:TextBox ID="txtSearch" runat="server"
                                    CssClass="textBox" MaxLength="40" Width="180px" TabIndex="6"></asp:TextBox>
                                &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="buttonDefault"
                                    ToolTip="Search" TabIndex="6" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                            </td>
                            <td align="left">
                                <asp:RadioButton ID="rdb_C" Text="Collection" CssClass="elementLabel" runat="server"
                                    GroupName="CL" TabIndex="2" AutoPostBack="true" OnCheckedChanged="rdb_C_CheckedChanged"
                                    Checked="true" />
                                <asp:RadioButton ID="rdb_LC" Text="Lodgment Under LC" CssClass="elementLabel" runat="server"
                                    GroupName="CL" TabIndex="2" AutoPostBack="true" OnCheckedChanged="rdb_LC_CheckedChanged" />
                            </td>
                            <td align="left">
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                            </td>
                            <td align="left">
                                <asp:RadioButton ID="rdb_sight" Text="Sight" CssClass="elementLabel" runat="server"
                                    GroupName="SU" TabIndex="3" AutoPostBack="true" Checked="true" OnCheckedChanged="rdb_sight_CheckedChanged" />
                                <asp:RadioButton ID="rdb_usance" Text="Usance" CssClass="elementLabel" runat="server"
                                    GroupName="SU" TabIndex="3" AutoPostBack="true" OnCheckedChanged="rdb_usance_CheckedChanged" />
                            </td>
                        </tr>
                       
                        <tr>
                            <td align="left" style="width: 100%;" valign="top" colspan="3">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="0" border="0" width="100%">
                        <tr id="rowGrid" runat="server">
                            <td align="left" style="width: 100%" valign="top">
                                <asp:GridView ID="GridViewImpEntryList" runat="server" AutoGenerateColumns="False"
                                    Width="100%" AllowPaging="True" OnRowCommand="GridViewImpEntryList_RowCommand"
                                    OnRowDataBound="GridViewImpEntryList_RowDataBound" CssClass="GridView">
                                    <PagerSettings Visible="false" />
                                    <RowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="top" CssClass="gridItem" />
                                    <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
                                    <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                        CssClass="gridAlternateItem" />
                                   <Columns>
                                        <asp:TemplateField HeaderText="Branch" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBranchName" runat="server" Text='<%# Eval("BranchName") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Doc_Scrutiny" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDoc_Scrutiny" runat="server" Text='<%# Eval("Document_Scrutiny_Disc") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Trans. Ref. No" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="7%" ItemStyle-Width="7%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocumentNo" runat="server" Text='<%# Eval("Document_No") %>' CssClass="elementLabel"></asp:Label>
                                                <asp:HiddenField ID="lblDocType" runat="server" Value='<%# Eval("Document_Type") %>' />
                                                <asp:HiddenField ID="lblForeignLocal" runat="server" Value='<%# Eval("Document_FLC_ILC") %>' />
                                                <asp:HiddenField ID="lblDocument_Scrutiny" runat="server" Value='<%# Eval("Document_Scrutiny") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Lodg. Date" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="7%" ItemStyle-Width="7%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLodgDate" runat="server" Text='<%# Eval("Lodgment_Date","{0:dd/MM/yyyy}") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received Date" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="7%" ItemStyle-Width="7%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocDate" runat="server" Text='<%# Eval("Received_Date","{0:dd/MM/yyyy}") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer Name" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="21%" ItemStyle-Width="21%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustName" runat="server" Text='<%# Eval("CUST_NAME") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Currency" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBillCurr" runat="server" Text='<%# Eval("Bill_Currency") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill Amount" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="7%" ItemStyle-Width="7%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBillAmt" runat="server" Text='<%# Eval("Bill_Amount") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Due Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="6%" ItemStyle-Width="6%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaturityDate" runat="server" Text='<%# Eval("Maturity_Date","{0:dd/MM/yyyy}") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GBase Status" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="7%" ItemStyle-Width="7%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGBase_Status" runat="server" Text='<%# Eval("GBase_Status") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Swift Status" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="7%" ItemStyle-Width="7%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSwift_Status" runat="server" Text='<%# Eval("Swift_Status") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SFMS Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="7%" ItemStyle-Width="7%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSFMS_Status" runat="server" Text='<%# Eval("SFMS_Status") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                       <tr id="rowPager" runat="server">
                            <td align="center" style="width: 100%" valign="top" class="gridHeader">
                                <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td align="left" style="width: 25%">
                                                &nbsp;Records per page:&nbsp;
                                                <asp:DropDownList ID="ddlrecordperpage" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlrecordperpage_SelectedIndexChanged">
                                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                    <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                    <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                                    <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" style="width: 50%" valign="top">
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
