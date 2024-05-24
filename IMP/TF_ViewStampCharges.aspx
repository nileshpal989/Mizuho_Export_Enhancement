<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_ViewStampCharges.aspx.cs" Inherits="IMP_TF_ViewStampCharges" %>

<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE SYSTEM</title>
     <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon"/>
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico"/>
    <link href="../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />

    <script src="../Help_Plugins/jquerynew.min.js" language="javascript" type="text/javascript"></script>
    <script src="../Help_Plugins/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <link href="../Help_Plugins/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Help_Plugins/AlertJquery.js" type="text/javascript"></script>
      <script src="../Help_Plugins/MyJquery1.js" type="text/javascript"></script> <%--snackbar--%>
    <link href="../Style/SnackBar.css" rel="stylesheet" type="text/css" /> <%--snackbar jquery--%>

    
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
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
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
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                 <Triggers>
                 <asp:PostBackTrigger ControlID="btnAdd" />
                        <asp:PostBackTrigger ControlID="btnDeleteConfirm" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align=left width="50%">
                                <span class="pageLabel"><strong>Stamp Duty Charges View</strong></span>
                            </td>
                            <td align="right" style="width: 50%">
                               <input type="hidden" id="hdid" runat="server" />
                               <input type="hidden" id="hdndate" runat="server" />
                                <input type="hidden" id="hdndescription" runat="server" />
                                 <input type="hidden" id="hdntenior" runat="server" />
                                  <input type="hidden" id="hdnrate" runat="server" />
                            <asp:Button ID="btnDeleteConfirm" Style="display: none;" runat="server" onclick="btnDeleteConfirm_Click" />
                                <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="buttonDefault" ToolTip="Add New Record"
                                onclick="btnAdd_Click"/>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr align="right">
                            <td align="right" style="width: 100%;" valign="top" colspan="2">
                                <span class="elementLabel">Search :</span> &nbsp;<asp:TextBox ID="txtSearch" runat="server"
                                    CssClass="textBox" MaxLength="40" Width="180px"></asp:TextBox>
                                &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="buttonDefault"
                                    ToolTip="Search"/>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; height: 21px;" valign="top" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr id="rowGrid" runat="server">
                            <td align="left" style="width: 80%" valign="top" colspan="2">
                                <asp:GridView ID="GridViewStampDutyChargesList" runat="server" 
                                    AutoGenerateColumns="False" Width="60%" AllowPaging="True" OnRowCommand="GridViewStampDutyChargesList_RowCommand"
                                    OnRowDataBound="GridViewStampDutyChargesList_RowDataBound" CssClass="GridView">
                                    <PagerSettings Visible="false" />
                               <%--     <RowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="top" CssClass="gridItem" />
                                    <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
                                    <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                        CssClass="gridAlternateItem" />--%>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Effective Date" 
                                            HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                            HeaderStyle-Width="2%" ItemStyle-Width="2%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEffectiveDate" runat="server" Text='<%# Eval("Effective_Date") %>' 
                                                    CssClass="elementLabel"></asp:Label>
                                                <asp:Label ID="lblLinked" runat="server" Text='<%# Eval("numberOfLinkedRecords") %>'
                                                    Visible="False"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ID" 
                                            HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="2%" ItemStyle-Width="2%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>' 
                                                    CssClass="elementLabel"></asp:Label>
                                                    
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Description" 
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' 
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Tenor Days" 
                                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="2%" ItemStyle-Width="2%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTenorDays" runat="server" Text='<%# Eval("TenorDays") %>' 
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Upto Tenor Days" 
                                            HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="2%" ItemStyle-Width="2%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUptoTenorDays" runat="server" Text='<%# Eval("UptoTenorDays") %>' 
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Rate(%)" 
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="2%" ItemStyle-Width="2%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRates" runat="server" Text='<%# Eval("Rates") %>' 
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="2%" ItemStyle-Width="2%">
                                            <ItemTemplate>
                                                <asp:Button ID="btnDelete" runat="server" ToolTip="Delete Record" CommandName="RemoveRecord"
                                                    CommandArgument='<%# Eval("ID")+";"+Eval("Effective_Date")+";"+ Eval("Description")+";"+Eval("TenorDays")
                                                    +";"+Eval("Rates") %>' Text="Delete" />
                                            </ItemTemplate>
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
