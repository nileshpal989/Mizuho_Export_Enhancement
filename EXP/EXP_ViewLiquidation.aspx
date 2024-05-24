<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EXP_ViewLiquidation.aspx.cs" Inherits="EXP_EXP_ViewLiquidation" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="../Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
  
    <script type="text/javascript" language="javascript">

        function OpenDocNoList(e) {

            var keycode;
            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {


                var branch = document.getElementById('hdnBranchCode');

                if (branch.value == '') {
                    alert('Select Branch.');
                  //  branch.focus();
                    return false;
                }

                open_popup('EXP_TTdocumentNoLookUp.aspx?branch=' + branch.value + '&custAcNo=', 450, 550, 'DocNoList');
                return false;
            }
        }

        function selectDocNo(selectedID) {
            var id = selectedID;
            document.getElementById('txtDocumentNo').value = id;
            __doPostBack("txtDocumentNo", "TextChanged");

        }

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

        function validateAdd() {
            var docNo = document.getElementById('txtDocumentNo').value;
            if (docNo.length < 18) {
                alert('Invalid Document No.');
                document.getElementById('txtDocumentNo').focus();
                return false;
            }
            return true;
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
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnAdd" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel">Export Entry Liquidation View</span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="buttonDefault" ToolTip="Add New Record"
                                    OnClick="btnAdd_Click" TabIndex="4" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                                <input type="hidden" id="hdnBranchCode" runat="server" />
                            </td>
                        </tr>
                        <tr align="right">
                            <td align="right" style="width: 100%;" valign="top" colspan="2">
                                <span class="elementLabel">Search :</span> &nbsp;<asp:TextBox ID="txtSearch" runat="server"
                                    CssClass="textBox" MaxLength="40" Width="180px" TabIndex="5"></asp:TextBox>
                                &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="buttonDefault"
                                    ToolTip="Search" OnClick="btnSearch_Click" TabIndex="6"/>
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                        <tr>
                            <td width="10%" align="right" nowrap>
                                <span class="elementLabel">Branch :</span>
                            </td>
                            <td width="10%" align="left" nowrap>
                                <asp:DropDownList ID="ddlBranch" CssClass="dropdownList"  runat="server"
                                    TabIndex="1" AutoPostBack="true" 
                                    onselectedindexchanged="ddlBranch_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td width="10%" align="right" nowrap>
                               
                            </td>
                            <td  align="left" nowrap>
                               
                            </td>
                            </tr>
                            <tr>
                            <td width="10%" nowrap>
                                <span class="elementLabel">Document No :</span>
                            </td>
                            <td align="left" nowrap>
                                <%--<asp:DropDownList ID="ddlDocumentNo" CssClass="dropdownList" AutoPostBack="true"
                                    runat="server" 
                                    TabIndex="3" Width="200px" 
                                    onselectedindexchanged="ddlDocumentNo_SelectedIndexChanged">
                                </asp:DropDownList>--%>
                                <asp:TextBox ID="txtDocumentNo" runat="server" CssClass="textBox" AutoPostBack="true"
                                TabIndex="3" Width="200px" ontextchanged="txtDocumentNo_TextChanged" onkeydown="OpenDocNoList(this)" MaxLength="19">
                                </asp:TextBox>
                                <asp:Button ID="btnDocNoList" runat="server" CssClass="btnHelp_enabled"
                                                            TabIndex="-1" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; height: 21px;" valign="top" colspan="4">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="0" border="0" width="100%">
                        <tr id="rowGrid" runat="server">
                            <td align="left" style="width: 100%" valign="top">
                                <asp:GridView ID="GridViewLiquidationList" runat="server" AutoGenerateColumns="False"
                                    Width="100%" AllowPaging="True" OnRowCommand="GridViewLiquidationList_RowCommand"
                                    OnRowDataBound="GridViewLiquidationList_RowDataBound">
                                    <PagerSettings Visible="false" />
                                    <RowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="top" CssClass="gridItem" />
                                    <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
                                    <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                        CssClass="gridAlternateItem" />
                                    <Columns>
                                       
                                        <asp:TemplateField HeaderText="Document Date" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocumentDate" runat="server" Text='<%# Eval("Doc_Date","{0:dd/MM/yyyy}") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                                      <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SrNo") %>'
                                                    CssClass="elementLabel" Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cust Name">
                                            <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustName" runat="server" Text='<%# Eval("CUST_NAME") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Curr">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCurr" runat="server" Text='<%# Eval("PC_Currency") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PCFC A/C No">
                                            <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblPCacNo" runat="server" Text='<%# Eval("PC_ACno") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sub A/C No">
                                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblsubAcNo" runat="server" Text='<%# Eval("SubAcNo") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Liquidated Amt">
                                            <HeaderStyle HorizontalAlign="Right" Width="20%" />
                                            <ItemStyle HorizontalAlign="Right" Width="20%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblLiquiAmt" runat="server" Text='<%# Eval("PC_LiquiAmt") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%" HeaderText="Delete"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Button ID="btnDelete" runat="server" CommandArgument='<%# Eval("Document_No") %>'
                                                    CommandName="RemoveRecord" Text="Delete" ToolTip="Delete Record" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                        </asp:TemplateField>--%>
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
