﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMPWH_PaymentHelp.aspx.cs" Inherits="ImportWareHousing_HelpForms_TF_IMPWH_PaymentHelp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>LMCC TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        var SelectedRow = null;
        var SelectedRowIndex = null;
        var UpperBound = null;
        var LowerBound = null;

        window.onload = function () {

            UpperBound = parseInt('<%= this.GridViewDumpList.Rows.Count %>') - 1;
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
        function OnMouseOver(CurrentRow, RowIndex) {

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

        }

        function OnMouseOut(CurrentRow, RowIndex) {

            if (SelectedRow != null) {
                SelectedRow.style.backgroundColor = SelectedRow.originalBackgroundColor;
                SelectedRow.style.color = SelectedRow.originalForeColor;
            }

            if (CurrentRow != null) {
                CurrentRow.originalForeColor = CurrentRow.style.color;
                CurrentRow.style.backgroundColor = "";
                CurrentRow.style.color = 'Black';
            }

        }
        function CloseMe() {
            enable_parent();
            window.close();
        }
    </script>
</head>
<body onload="disable_parent();EndRequest();" onunload="enable_parent();" style="width: 100%;
    height: 100%; margin: 0 0 0 0; background-color: White;">
    <form id="form1" runat="server">
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
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
            <ContentTemplate>
                <table align="left" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" style="width: 100%; height: 21px;" valign="top">
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 2%;" valign="middle" nowrap>
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="textBox" MaxLength="40" onblur="GiveFocus='FALSE';CheckFocus();"
                                onclick="Change(this);"></asp:TextBox>
                            &nbsp;
                            <asp:Button ID="btngo" runat="server" ToolTip="Search" CssClass="btnGO" OnClick="btngo_Click" />
                            <asp:Button ID="btnCloseMe" CssClass="btnCloseMe" runat="server" ToolTip="Close Me!"
                                OnClientClick="CloseMe();" />
                        </td>
                    </tr>
                    <tr id="rowGrid" runat="server">
                        <td align="left" style="width: 100%" valign="top">
                            <asp:GridView ID="GridViewDumpList" runat="server" AutoGenerateColumns="False" Width="100%"
                                AllowPaging="true" OnRowCommand="GridViewDumpList_RowCommand" OnRowDataBound="GridViewDumpList_RowDataBound"
                                OnRowCreated="GridViewDumpList_RowCreated" PageSize="20" onblur="GiveFocus='FALSE';CheckFocus();"
                                CssClass="GridView">
                                <PagerSettings Visible="false" />
                                <Columns>
                                    <asp:TemplateField HeaderText="BOE No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblboeno" runat="server" Text='<%# Eval("BOE_No") %>' CssClass="elementLabel"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblboedate" runat="server" Text='<%# Eval("BOE_Date") %>' CssClass="elementLabel"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Port Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprtcode" runat="server" Text='<%# Eval("Port_Of_Discharge") %>' CssClass="elementLabel"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Currency">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCur" runat="server" Text='<%# Eval("Invoice_Curr") %>' CssClass="elementLabel"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblboeamt" runat="server" Text='<%# Eval("BOEAmt","{0:0.00}") %>'
                                                CssClass="elementLabel"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" Width="20%" />
                                        <ItemStyle HorizontalAlign="Right" Width="20%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Balance Amt">
                                        <ItemTemplate>
                                            <asp:Label ID="lblboebalamt" runat="server" Text='<%# Eval("BOEBalAmt","{0:0.00}") %>'
                                                CssClass="elementLabel"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" Width="20%" />
                                        <ItemStyle HorizontalAlign="Right" Width="20%" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr id="rowPager" runat="server">
                        <td class="gridHeader" style="text-align: center; width: 100%; vertical-align: top">
                            <table cellspacing="0" cellpadding="2" width="100%" border="0" style="width: 100%">
                                <tbody>
                                    <tr>
                                        <td align="left" style="width: 25%; vertical-align: middle">
                                            &nbsp;Records per page:&nbsp;
                                            <br />
                                            <asp:DropDownList ID="ddlrecordperpage" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlrecordperpage_SelectedIndexChanged">
                                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                <asp:ListItem Value="20" Text="20" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                                <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center" style="width: 50%; vertical-align: middle" valign="top">
                                            <asp:Button ID="btnnavfirst" runat="server" Text="First" ToolTip="First" OnClick="btnnavfirst_Click" />
                                            <asp:Button ID="btnnavpre" runat="server" Text="Prev" ToolTip="Previous" OnClick="btnnavpre_Click" />
                                            <asp:Button ID="btnnavnext" runat="server" Text="Next" ToolTip="Next" OnClick="btnnavnext_Click" />
                                            <asp:Button ID="btnnavlast" runat="server" Text="Last" ToolTip="Last" OnClick="btnnavlast_Click" />
                                        </td>
                                        <td align="right" style="width: 25%; vertical-align: middle">
                                            <asp:Label ID="lblpageno" runat="server"></asp:Label>
                                            <br />
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
