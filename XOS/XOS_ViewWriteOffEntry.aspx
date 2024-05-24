<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XOS_ViewWriteOffEntry.aspx.cs"
    Inherits="XOS_XOS_ViewWriteOffEntry" %>

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
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script language="javascript" type="text/javascript">

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
            //            var docNo = document.getElementById('txtDocumentNo');
            var branchCode = document.getElementById('hdnBranchCode');

            //            if (docNo.value == '') {
            //                alert('Enter Document No.');
            //                docNo.focus();
            //                return false;
            //            }

            if (branchCode.value == '') {
                alert('Enter Branch Code');
                //branchCode.focus();
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
    <style type="text/css">
        .style1
        {
            height: 19px;
        }
    </style>
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
                <%--<Triggers>
                    <asp:PostBackTrigger ControlID="btnAdd" />
                </Triggers>--%>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel">XOS Write Off Data Entry View</span>
                            </td>
                            
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                                <td align="left" style="width: 100%" valign="top" colspan="2">
                                    <hr />
                                    <input type="hidden" id="hdnBranchCode" runat="server" />
                                    <input type="hidden" id="hdnUserRole" runat="server" />
                                    <input type="hidden" id="hdnBillDocumentType" runat="server" />
                                </td>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" nowrap width="10%">
                                <span class="elementLabel">Branch :</span>
                                <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" AutoPostBack="true" runat="server"
                                    OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" TabIndex="1">
                                </asp:DropDownList>
                                <%--<input id="txtDocPrFx" type="hidden" runat="server" />
                                <input type="hidden" id="hdnBranchCode" runat="server" />--%>
                            </td>
                            <td align="right" style="width: 100%;" valign="top" colspan="2">
                                <span class="elementLabel">Search :</span> &nbsp;<asp:TextBox ID="txtSearch" runat="server"
                                    CssClass="textBox" MaxLength="40" Width="180px"></asp:TextBox>
                                &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="buttonDefault"
                                    ToolTip="Search" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                        <tr>
                            <td width="20%" nowrap align="left">
                                <asp:RadioButton ID="rbtbla" runat="server" CssClass="elementLabel" Checked="true"
                                    Text="Bills Bought with L/C at Sight" GroupName="TransType" OnCheckedChanged="rbtbla_CheckedChanged"
                                    AutoPostBack="true" />&nbsp;
                            </td>
                            <td width="20%" nowrap align="left">
                                <asp:RadioButton ID="rbtbba" runat="server" CssClass="elementLabel" Text="Bills Bought without L/C at Sight"
                                    GroupName="TransType" OnCheckedChanged="rbtbba_CheckedChanged" AutoPostBack="true" />&nbsp;
                            </td>
                            <td width="20%" nowrap align="left">
                                <asp:RadioButton ID="rbtbca" runat="server" CssClass="elementLabel" Text="Bills For Collection at Sight "
                                    GroupName="TransType" OnCheckedChanged="rbtbcs_CheckedChanged" AutoPostBack="true" />&nbsp;
                            </td>
                            <td width="20%" nowrap align="left">
                                <%--<asp:RadioButton ID="rbtIBD" runat="server" CssClass="elementLabel" Text="Local Bills Discounting"
                                    GroupName="TransType" AutoPostBack="true" OnCheckedChanged="rbtIBD_CheckedChanged" />&nbsp;--%>
                            </td>
                            <td align="left">
                                <%--<asp:RadioButton ID="rbtBEB" runat="server" CssClass="elementLabel" Text="Advance"
                                    GroupName="TransType" AutoPostBack="true" OnCheckedChanged="rbtBEB_CheckedChanged" />&nbsp;--%>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%" nowrap align="left">
                                <asp:RadioButton ID="rbtblu" runat="server" CssClass="elementLabel" Text="Bills Bought with L/C Usance"
                                    GroupName="TransType" OnCheckedChanged="rbtblu_CheckedChanged" AutoPostBack="true" />&nbsp;
                            </td>
                            <td width="20%" nowrap align="left">
                                <asp:RadioButton ID="rbtbbu" runat="server" CssClass="elementLabel" Text="Bills Bought without L/C Usance"
                                    GroupName="TransType" OnCheckedChanged="rbtbbu_CheckedChanged" AutoPostBack="true" />&nbsp;
                            </td>
                            <td width="20%" nowrap align="left">
                                <asp:RadioButton ID="rbtbcu" runat="server" CssClass="elementLabel" Text="Bills For Collection Usance"
                                    GroupName="TransType" OnCheckedChanged="rbtbcu_CheckedChanged" AutoPostBack="true" />&nbsp;
                            </td>
                            <td width="20%" nowrap align="left">
                                <%--<asp:RadioButton ID="rbtLBC" runat="server" CssClass="elementLabel" Text="Local Bills Collection"
                                    GroupName="TransType" AutoPostBack="true" OnCheckedChanged="rbtLBC_CheckedChanged" />&nbsp;--%>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td width="10%" nowrap align="left">
                                <span class="elementLabel">Document No :</span>
                                <asp:TextBox ID="txtDocPrFx" runat="server" CssClass="textBox" Width="30px" Text="EBW"
                                    ReadOnly="True"></asp:TextBox>
                                <asp:TextBox ID="txtBranchCode" runat="server" CssClass="textBox" Width="30px" TabIndex="3"
                                    MaxLength="5" Enabled="false" style="font-weight:bold;"></asp:TextBox>
                                <asp:TextBox ID="txtYear" runat="server" CssClass="textBox" Width="20px" OnTextChanged="txtYear_TextChanged"
                                    AutoPostBack="true" TabIndex="4" MaxLength="2"></asp:TextBox>
                                <asp:TextBox ID="txtDocumentNo" runat="server" CssClass="textBox" Width="40px" TabIndex="5"
                                    MaxLength="5" Enabled="false" style="font-weight:bold;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; height: 21px;" valign="top" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="0" border="0" width="100%">
                        <tr id="rowGrid" runat="server">
                            <td align="left" style="width: 100%" valign="top">
                                <asp:GridView ID="GridViewXOSWriteOff" runat="server" AutoGenerateColumns="False"
                                    Width="100%" AllowPaging="True" OnRowDataBound="GridViewXOSWriteOff_RowDataBound">
                                    <PagerSettings Visible="false" />
                                    <RowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="top" CssClass="gridItem" />
                                    <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
                                    <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                        CssClass="gridAlternateItem" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Bill No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="7%" ItemStyle-Width="7%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBillNo" runat="server" Text='<%# Eval("BillNo") %>' CssClass="elementLabel"></asp:Label>
                                                <%--<asp:Label ID="lblCancelFees" runat="server" Text='<%# Eval("Cancellation_Fees") %>'
                                                    Visible="False"></asp:Label>--%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDateRecieved" runat="server" Text='<%# Eval("Date_Negotiated","{0:dd/MM/yyyy}") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer Name">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemStyle HorizontalAlign="left" Width="10%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustName" runat="server" Text='<%# Eval("CUST_NAME") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Currency">
                                            <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                            <ItemStyle HorizontalAlign="Center" Width="3%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCurr" runat="server" Text='<%# Eval("Currency") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill Amount">
                                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("ActBillAmt","{0:0.00}") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bal Amount">
                                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblBalAmount" runat="server" Text='<%# Eval("Balance","{0:0.00}") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Due Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDueDate" runat="server" Text='<%# Eval("Due_Date","{0:dd/MM/yyyy}") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Document No" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="7%" ItemStyle-Width="7%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocumentNo" runat="server" Text='<%# Eval("DocumentNo") %>' CssClass="elementLabel"></asp:Label>
                                                <%--<asp:Label ID="lblCancelFees" runat="server" Text='<%# Eval("Cancellation_Fees") %>'
                                                    Visible="False"></asp:Label>--%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="W/O Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWriteOffDate" runat="server" Text='<%# Eval("WriteOffDate","{0:dd/MM/yyyy}") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Write Off Number">
                                            <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                            <ItemStyle HorizontalAlign="Center" Width="7%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblWriteOffNo" runat="server" Text='<%# Eval("WriteOffNo") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Write Off Amount">
                                            <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                            <ItemStyle HorizontalAlign="Right" Width="7%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblWriteOffAmount" runat="server" Text='<%# Eval("WrittenOffAmount","{0:0.00}") %>'
                                                    CssClass="elementLabel"></asp:Label>
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
