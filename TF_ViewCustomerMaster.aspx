<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_ViewCustomerMaster.aspx.cs"
    Inherits="TF_ViewCustomerMaster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="Style/SnackBar.css" rel="Stylesheet" type="text/css" media="screen" />
    <link href="Style/Style_V2.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="Help_Plugins/MyJquery1.js" type="text/javascript"></script>
    <%--snackbar jquery--%>
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

        function Alert(Result) {
            MyAlert(Result);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <div id="snackbar">
        <div id="snackbarbody" style="padding-top: -500px;">
        </div>
    </div>
    <script language="javascript" type="text/javascript" src="Scripts/InitEndRequest.js"></script>
    <script language="javascript" type="text/javascript" src="Scripts/Enable_Disable_Opener.js"></script>
    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div>
        <uc1:Menu ID="Menu1" runat="server" />
        <br />
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnAdd" />
            </Triggers>
            <ContentTemplate>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom" colspan="2">
                            <span class="pageLabel"><strong>Customer Master View</strong> </span>
                        </td>
                        <td align="right" style="width: 50%">
                            <input type="hidden" id="hdnUserRole" runat="server" />
                            <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="buttonDefault" ToolTip="Add New Record"
                                OnClick="btnAdd_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%" valign="top" colspan="3">
                            <hr />
                        </td>
                    </tr>
                    <tr align="left">
                        <td style="width: 50%" align="left">
                            <span class="elementLabel">Branch :</span>
                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" AutoPostBack="true" runat="server"
                                TabIndex="1" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td align="right" style="width: 100%;" valign="top">
                            <span class="elementLabel">Search </span>&nbsp;<asp:TextBox ID="txtSearch" runat="server"
                                CssClass="textBox" MaxLength="40" Width="180px"></asp:TextBox>
                            &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="buttonDefault"
                                ToolTip="Search" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%; height: 21px;" valign="top" colspan="3">
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                        </td>
                    </tr>
                    <tr id="rowGrid" runat="server">
                        <td align="left" style="width: 100%" valign="top" colspan="3">
                            <asp:GridView ID="GridViewCustomerList" runat="server" AutoGenerateColumns="False"
                                Width="100%" AllowPaging="True" OnRowCommand="GridViewCustomerList_RowCommand"
                                OnRowDataBound="GridViewCustomerList_RowDataBound">
                                <PagerSettings Visible="false" />
                                <RowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                    CssClass="gridItem" />
                                <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Middle" CssClass="gridHeader" />
                                <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                    CssClass="gridAlternateItem" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Customer A/C No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcustomerACNo" runat="server" CssClass="elementLabel" Text='<%# Eval("CUST_ACCOUNT_NO") %>'></asp:Label>
                                            <asp:Label ID="lblLinked" runat="server" Text='<%# Eval("numberOfLinkedRecords") %>'
                                                Visible="False"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="8%" />
                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cust Abbr">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCust_Abbr" runat="server" CssClass="elementLabel" Text='<%# Eval("Cust_Abbr") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="8%" />
                                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcustomername" runat="server" CssClass="elementLabel" ToolTip='<%# Eval("CUST_NAME") %>'
                                                Text='<%# (Eval("CUST_NAME").ToString().Length > 35) ? (Eval("CUST_NAME").ToString().Substring(0, 35) + "...") : Eval("CUST_NAME")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Address">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" CssClass="elementLabel" ToolTip='<%# Eval("CUST_ADDRESS") %>'
                                                Text='<%# (Eval("CUST_ADDRESS").ToString().Length > 60) ? (Eval("CUST_ADDRESS").ToString().Substring(0, 60) + "...") : Eval("CUST_ADDRESS")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="30%" />
                                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="City">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcity" runat="server" CssClass="elementLabel" Text='<%# Eval("City") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Country">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcountryid" runat="server" CssClass="elementLabel" Text='<%# Eval("CUST_COUNTRY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="IE Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lbliecode" runat="server" CssClass="elementLabel" Text='<%# Eval("CUST_IE_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="08%" />
                                        <ItemStyle HorizontalAlign="Center" Width="08%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Emails">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSDate1" runat="server" CssClass="elementLabel" Text='<%# Eval("S_Date1") %>'></asp:Label>
                                            <asp:Label ID="lblSDate2" runat="server" CssClass="elementLabel" Text='<%# Eval("S_Date2") %>'></asp:Label>
                                            <asp:Label ID="lblSDate3" runat="server" CssClass="elementLabel" Text='<%# Eval("S_Date3") %>'></asp:Label>
                                            <asp:Label ID="lblSDate4" runat="server" CssClass="elementLabel" Text='<%# Eval("S_Date4") %>'></asp:Label>
                                            <asp:Label ID="lblSDate5" runat="server" CssClass="elementLabel" Text='<%# Eval("S_Date5") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="6%" />
                                        <ItemStyle HorizontalAlign="Center" Width="6%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="IWC">
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="RowCheckBox1" AutoPostBack="true" OnCheckedChanged="RowCheckBox1_CheckedChanged"
                                                Enabled="false"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="IMP">
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkIMP" AutoPostBack="true" OnCheckedChanged="chkIMP_CheckedChanged"
                                                Enabled="false"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:Button ID="btnDelete" runat="server" CommandArgument='<%# Eval("CUST_ACCOUNT_NO")+";"+
                                            Eval("Cust_Abbr")+";"+Eval("CUST_NAME")+";"+Eval("CUST_ADDRESS")+";"+Eval("City")+";"+
                                            Eval("CUST_COUNTRY")+";"+Eval("CUST_IE_CODE")+";"+Eval("S_Date1")+";"+Eval("S_Date2")+";"+
                                            Eval("S_Date3")+";"+Eval("S_Date4")%>' CommandName="RemoveRecord" Text="Delete" ToolTip="Delete Record" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr id="rowPager" runat="server">
                        <td align="center" style="width: 100%" valign="top" colspan="3" class="gridHeader">
                            <table cellspacing="0" cellpadding="3" width="100%" border="0">
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
    </div>
    </form>
</body>
</html>
