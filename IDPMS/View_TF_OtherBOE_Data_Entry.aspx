<%@ Page Language="C#" AutoEventWireup="true" CodeFile="View_TF_OtherBOE_Data_Entry.aspx.cs"
    Inherits="IDPMS_View_TF_OtherBOE_Data_Entry" %>

<%@ Register Src="../Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>LMCC TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <link href="../Style/Style_V2.css" rel="Stylesheet" type="text/css" media="screen">
    <script type="text/javascript" language="javascript">


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
            // var docNo = document.getElementById('txtDocumentNo');
            var branchCode = document.getElementById('ddlBranch');


            if (branchCode.value == '0') {

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
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel"><strong>BOE Other AD View</strong></span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="buttonDefault" ToolTip="Add New Record"
                                    OnClick="btnAdd_Click" TabIndex="3" />
                                <input type="hidden" runat="server" id="hdnUserRole" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr align="right">
                            <td width="15%" align="left" nowrap>
                                <span class="elementLabel">Branch :</span><asp:DropDownList ID="ddlBranch1" CssClass="dropdownList"
                                    AutoPostBack="true" runat="server" TabIndex="1" OnSelectedIndexChanged="ddlBranch1_SelectedIndexChanged1">
                                </asp:DropDownList>
                            </td>
                            <td align="right" style="width: 100%;" valign="top">
                                <span class="elementLabel">Search :</span> &nbsp;<asp:TextBox ID="txtSearch" runat="server"
                                    CssClass="textBox" MaxLength="40" Width="180px" TabIndex="5"></asp:TextBox>
                                &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="buttonDefault"
                                    ToolTip="Search" OnClick="btnSearch_Click" TabIndex="6" />
                            </td>
                        </tr>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; height: 21px;" valign="top" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellspacing="0" border="0" width="100%">
                                            <tr id="rowGrid" runat="server">
                                                <td align="left">
                                                    <asp:GridView ID="GridViewinwardclosure" runat="server" AutoGenerateColumns="false"
                                                        Width="40%" GridLines="Both" AllowPaging="true" PageSize="20" OnRowDataBound="GridViewinwardclosure_RowDataBound"
                                                        OnRowCommand="GridViewinwardclosure_RowCommand" CssClass="GridView">
                                                        <PagerSettings Visible="false" />
                                                        
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Doc No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDoc" runat="server" Text='<%# Eval("Doc_No") %>' CssClass="elementLabel"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="BOE No." HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBOE" runat="server" Text='<%# Eval("BOENo") %>' CssClass="elementLabel"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="BOE Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBOEDate" runat="server" Text='<%# Eval("BOEDate") %>' CssClass="elementLabel"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PortCode" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPortCode" runat="server" Text='<%# Eval("BOEPortCode") %>' CssClass="elementLabel"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="1%" />
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="5%" HeaderText="Delete"
                                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnDelete" runat="server" CommandArgument='<%# Eval("Doc_No")+";"+Eval("BOENo")+";"+Eval("BOEDate")+";"+Eval("BOEPortCode") %>'
                                                                        CommandName="RemoveRecord" Text="Delete" ToolTip="Delete Record" />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="2%" />
                                                                <ItemStyle HorizontalAlign="Center" Width="2%" />
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
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
