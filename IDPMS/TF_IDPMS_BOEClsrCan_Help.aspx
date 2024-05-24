<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IDPMS_BOEClsrCan_Help.aspx.cs" Inherits="IDPMS_TF_IDPMS_BOEClsrCan_Help" %>

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
    <link href="../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-3.3.1.min.js" type="text/javascript"></script>
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
        function CloseMe() {
            enable_parent();
            window.close();
        }
    </script>
</head>
<body bgcolor="#CDDAE4" topmargin="0" bottommargin="0" rightmargin="0" leftmargin="0"
    onload="disable_parent();EndRequest();" onunload="enable_parent();" onblur="CheckFocus();"
    onfocus="CheckFocus();" style="width: 100%; height: 100%;">
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
                                        onclick="Change(this);"></asp:TextBox>
                                    &nbsp;
                                <asp:Button ID="btngo" runat="server" ToolTip="Search" CssClass="btnGO" OnClick="btngo_Click" />
                                    <asp:Button ID="btnCloseMe" CssClass="btnCloseMe" runat="server" ToolTip="Close Me!"
                                        OnClientClick="CloseMe();" />
                                </td>
                            </tr>
                            <tr id="rowGrid" runat="server">
                                <td align="left" style="width: 100%" valign="top">
                                    <asp:GridView ID="GridViewDumpList" runat="server" AutoGenerateColumns="False"
                                        Width="100%" AllowPaging="false" OnRowCommand="GridViewDumpList_RowCommand"
                                        OnRowDataBound="GridViewDumpList_RowDataBound" OnRowCreated="GridViewDumpList_RowCreated"
                                        PageSize="15" onblur="GiveFocus='FALSE';CheckFocus();" CssClass="GridView">
                                        <PagerSettings Visible="false" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="BOE No." HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblboeno" runat="server" Text='<%# Eval("billOfEntryNumber") %>' CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                                <ItemStyle HorizontalAlign="Center" Width="20%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BOE Date" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblboedate" runat="server" Text='<%# Eval("billOfEntryDate") %>' CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                                <ItemStyle HorizontalAlign="Center" Width="20%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Port Code" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblprtcode" runat="server" Text='<%# Eval("portOfDischarge") %>' CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                                <ItemStyle HorizontalAlign="Center" Width="20%" />
                                            </asp:TemplateField>

                                            <%--<asp:TemplateField HeaderText="Inv Sr No" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvSr" runat="server" Text='<%# Eval("invoiceSerialNo") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                                        </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Invoice No" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvNo" runat="server" Text='<%# Eval("invoiceNo") %>' CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="25%" />
                                                <ItemStyle HorizontalAlign="Center" Width="25%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Adjusted Amt" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30%" ItemStyle-Width="30%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAdjAmt" runat="server" Text='<%# Eval("adjustedValue","{0:0.00}") %>' CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" Width="30%" />
                                                <ItemStyle HorizontalAlign="Right" Width="30%" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="BOE Adjusted Date" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAdjDate" runat="server" Text='<%# Eval("adjustmentDate") %>' CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                                <ItemStyle HorizontalAlign="Center" Width="20%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Adjustment Ref No" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40%" ItemStyle-Width="40%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAdjRef" runat="server" Text='<%# Eval("adjustmentReferenceNumber") %>' CssClass="elementLabel"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="50%" />
                                                <ItemStyle HorizontalAlign="Center" Width="50%" />
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
