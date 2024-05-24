<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_RMA_Master.aspx.cs"
    Inherits="IMP_TF_IMP_RMA_Master" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Help_Plugins/jquerynew.min.js" language="javascript" type="text/javascript"></script>
    <script src="../Help_Plugins/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <link href="../Help_Plugins/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Help_Plugins/AlertJquery.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function Alert(Result, ID) {
            $("#Paragraph").text(Result);
            $("#dialog").dialog({
                title: "Message From LMCC",
                width: 350,
                modal: true,
                closeOnEscape: true,
                dialogClass: "AlertJqueryDisplay",
                hide: { effect: "close", duration: 400 },
                buttons: [
                    {
                        text: "Ok",
                        icon: "ui-icon-heart",
                        click: function () {
                            $(this).dialog("close");
                            $(ID).focus();
                        }
                    }
                  ]
            });
            $('.ui-dialog :button').blur();
            return false;
        }
        // Validate report
        function Valiadat(Result) {
            $("#Paragraph").text(Result);
            $("#dialog").dialog({
                title: "Message From LMCC",
                width: 570,
                modal: true,
                closeOnEscape: true,
                dialogClass: "AlertJqueryDisplay",
                hide: { effect: "close", duration: 400 },
                buttons: [
                    {
                        text: "Ok",
                        icon: "ui-icon-heart",
                        click: function () {
                            $(this).dialog("close");

                            // popup = window.open('EDPMS_rpt_AD_TransferData_validation.aspx?mode=A', '_blank', 'height=600,  width=900,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100');
                            popup = window.open('RET_CSV_Validation.aspx', '_blank', 'height=600,  width=1000,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100');
                            $("").focus();
                        }
                    }
                  ]
            });
            $('.ui-dialog :button').blur();
            return false;
        }
   
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--  alert message--%>
    <div id="dialog" class="AlertJqueryHide">
        <p id="Paragraph">
        </p>
    </div>
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
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnUpload" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <input type="hidden" id="hdnCustId" runat="server" />
                            <td align="left" style="width: 50%; font-weight: bold" valign="bottom">
                                <span class="pageLabel" style="font-weight: bold;padding-left:1%;">RMA Master File Upload</span>
                            </td>
                            <td align="right" style="width: 50%">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%;" valign="top" colspan="2">
                                <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 150%">
                                    <tr>
                                        <td colspan="2" nowrap align="center">
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td align="right" style="width: 10%">
                                            <span class="elementLabel">Select File :</span>
                                        </td>
                                        <td align="left" style="width: 90%">
                                            &nbsp;<asp:FileUpload ID="fileinhouse" runat="server" ViewStateMode="Enabled" TabIndex="3"
                                                Width="500px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 10%">
                                            <span class="elementLabel">Input File :</span>
                                        </td>
                                        <td align="left" style="width: 90%">
                                            &nbsp;<asp:TextBox ID="txtInputFile" runat="server" CssClass="textBox" MaxLength="10"
                                                Width="413px" TabIndex="2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 10%">
                                        </td>
                                        <td align="left" style="width: 90%">
                                            <table border="0" style="border-color: red" cellpadding="2">
                                                <tr>
                                                    <td width="130px" nowrap>
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Button ID="btnUpload" runat="server" Text="Upload RMA CSV File" CssClass="buttonDefault"
                                                                    ToolTip="Upload RMA CSV File" TabIndex="4" OnClick="btnUpload_Click" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Label runat="server" CssClass="elementLabel" ID="labelMessage" Style="font-weight: bold;"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr align="right">
                                        <td align="right" style="width: 100%;" valign="top" colspan="2">
                                            <asp:Label runat="server" CssClass="elementLabel" ID="lblSearch" Text="Search :"></asp:Label>
                                            &nbsp;<asp:TextBox ID="txtSearch" runat="server" CssClass="textBox" MaxLength="40"
                                                Width="180px"></asp:TextBox>
                                            &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="buttonDefault"
                                                OnClick="btnSearch_Click" ToolTip="Search" />
                                        </td>
                                    </tr>
                                    <tr id="rowGrid" runat="server">
                                        <td align="left" style="width: 100%" valign="top" colspan="2">
                                            <asp:GridView ID="GridViewRMA" runat="server" AutoGenerateColumns="False" Width="50%"
                                                AllowPaging="True" CssClass="GridView">
                                                <PagerSettings Visible="false" />
                                                <RowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                                    CssClass="gridItem" />
                                                <HeaderStyle VerticalAlign="Middle" CssClass="gridHeader" />
                                                <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                                    CssClass="gridAlternateItem" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Own BIC" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCurrencyID" runat="server" Text='<%#Eval("OwnBIC")%>' CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Correspondent" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="45%" ItemStyle-Width="45%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Correspondent")%>' CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="R. Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGBaseCode" runat="server" Text='<%#Eval("RStatus")%>' CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="R. Restr." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("RRestr")%>' CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="S. Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("SStatus")%>' CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="S. Restr." HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("SRestr")%>' CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Correspondent Name" HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("CorrespondentName")%>' CssClass="elementLabel"></asp:Label>
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
                                <br />
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
