﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_FXHelp.aspx.cs" Inherits="IMP_HelpForms_TF_IMP_FXHelp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" language="javascript">
        var SelectedRow = null;
        var SelectedRowIndex = null;
        var UpperBound = null;
        var LowerBound = null;

        window.onload = function () {

            UpperBound = parseInt('<%= this.GridViewFXList.Rows.Count %>') - 1;
            LowerBound = 0;
            SelectedRowIndex = -1;

            return false;

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

        function SelectRow(CurrentRow, RowIndex) {

            if (SelectedRow != null) {
                SelectedRow.style.backgroundColor = SelectedRow.originalBackgroundColor;
                SelectedRow.style.color = SelectedRow.originalForeColor;
            }

            if (CurrentRow != null) {
                CurrentRow.originalBackgroundColor = CurrentRow.style.backgroundColor;
                CurrentRow.originalForeColor = CurrentRow.style.color;
                CurrentRow.style.backgroundColor = '#DCFC5C';
                CurrentRow.style.color = 'Black';
            }

            SelectedRow = CurrentRow;
            SelectedRowIndex = RowIndex;
            setTimeout("SelectedRow.focus();", 0);
        }

        function DisSelectRow(CurrentRow, RowIndex) {

            if (SelectedRow != null) {
                SelectedRow.style.backgroundColor = SelectedRow.originalBackgroundColor;
                SelectedRow.style.color = SelectedRow.originalForeColor;
            }

            if (CurrentRow != null) {
                CurrentRow.originalBackgroundColor = CurrentRow.style.backgroundColor;
                CurrentRow.originalForeColor = CurrentRow.style.color;
                CurrentRow.style.backgroundColor = CurrentRow.originalBackgroundColor;
                CurrentRow.style.color = 'Black';
            }

        }

        function Search_Gridview(strKey, strGV) {
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById(strGV);
            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }

        function CloseMe() {
            enable_parent();
            window.close();
        }
    </script>
</head>
<body bgcolor="#CDDAE4" topmargin="0" bottommargin="0" rightmargin="0" leftmargin="0"
    onload="disable_parent();EndRequest();" onunload="enable_parent();" onblur="CheckFocus();"
    onfocus="CheckFocus();" style="width: 100%; height: 100%;">
    <form id="form1" runat="server" autocomplete="off">
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
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <table align="left" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 100%" valign="top">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; height: 21px;" valign="top">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 2%;" valign="middle" nowrap>
                                <asp:TextBox ID="txtSearch" runat="server"
                                    CssClass="txtenabled" MaxLength="40" Width="200px" onblur="GiveFocus='FALSE';CheckFocus();"
                                    onclick="Change(this);" onkeyup="Search_Gridview(this, 'GridViewFXList')"></asp:TextBox>
                                &nbsp;
                                <asp:Button ID="btngo" runat="server" ToolTip="Search" CssClass="btnGO" OnClick="btngo_Click" />
                                <asp:Button ID="btnCloseMe" CssClass="btnCloseMe" runat="server" ToolTip="Close Me!"
                                    OnClientClick="CloseMe();" />
                            </td>
                        </tr>
                        <tr id="rowGrid" runat="server">
                            <td align="left" style="width: 100%" valign="top">
                                <asp:GridView ID="GridViewFXList" runat="server" AutoGenerateColumns="False"
                                    Width="100%" AllowPaging="false" OnRowCommand="GridViewFXList_RowCommand"
                                    OnRowDataBound="GridViewFXList_RowDataBound" OnRowCreated="GridViewFXList_RowCreated"
                                    PageSize="15" onblur="GiveFocus='FALSE';CheckFocus();">
                                    <PagerSettings Visible="false" />
                                    <RowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="top" CssClass="gridItem" />
                                    <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
                                    <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                        CssClass="gridAlternateItem" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Gbase Ref No." HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGbaseRefNo" runat="server" Text='<%# Eval("GBASE_REFERENCE_NUMBER") %>' CssClass="elementLabel"></asp:Label>
                                                <asp:Label ID="lblContractDate" runat="server" Text='<%# Eval("CONTRACT_DATE") %>' CssClass="elementLabel" Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>  
                                        <asp:TemplateField HeaderText="Internal Rate" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInternalRate" runat="server" Text='<%# Eval("INTERNAL_RATE") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Exchange Rate" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExchangeRate" runat="server" Text='<%# Eval("EXCHANGE_RATE") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contract Amount" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContractAmount" runat="server" Text='<%# Eval("CONTRACT_AMOUNT") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contract Currency" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContractCurrency" runat="server" Text='<%# Eval("CONTRACT_CURRENCY") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Equivalent Amount" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEquivalentAmount" runat="server" Text='<%# Eval("EQUIVALENT_AMOUNT") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Equivalent Currency" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEquivalentCurrency" runat="server" Text='<%# Eval("EQUIVALENT_CURRENCY") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
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



