<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EXP_ViewRealisationEntry.aspx.cs"
    Inherits="EXP_EXP_ViewRealisationEntry" %>

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
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
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
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/Enable_Disable_Opener.js"></script>
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
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel"><strong>Export Realisation Data Entry View</strong></span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="buttonDefault" ToolTip="Add New Record"
                                    OnClick="btnAdd_Click" TabIndex="5" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                                <input type="hidden" id="hdnUserRole" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                        <tr>
                            <td style="white-space: nowrap; width: 5%; text-align: right">
                                <span class="elementLabel">Branch : </span>
                            </td>
                            <td style="white-space: nowrap; width: 45%; text-align: left">
                                &nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" AutoPostBack="true"
                                    runat="server" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" TabIndex="1">
                                </asp:DropDownList>
                                <input id="txtDocPrFx" type="hidden" runat="server" />
                                <input type="hidden" id="hdnBranchCode" runat="server" />
                                &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;
                                <asp:RadioButton ID="rbtnForeign" runat="server" CssClass="elementLabel" Text="Foreign"
                                    GroupName="FL" AutoPostBack="true" Checked="true" OnCheckedChanged="rbtnForeign_CheckedChanged" />.
                                <asp:RadioButton ID="rbtnLocal" runat="server" CssClass="elementLabel" Text="Local"
                                    GroupName="FL" AutoPostBack="true" OnCheckedChanged="rbtnLocal_CheckedChanged" />
                            </td>
                            <td align="right" style="width: 50%;" valign="top">
                                <span class="elementLabel">Search :</span> &nbsp;<asp:TextBox ID="txtSearch" runat="server"
                                    CssClass="textBox" MaxLength="40" Width="180px"></asp:TextBox>
                                &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="buttonDefault"
                                    ToolTip="Search" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="white-space: nowrap; text-align: right">
                                <span class="elementLabel">Year :</span>
                            </td>
                            <td style="white-space: nowrap; text-align: left">
                                &nbsp;<asp:TextBox ID="txtYear" runat="server" CssClass="textBox" Width="20px" OnTextChanged="txtYear_TextChanged"
                                    AutoPostBack="true" TabIndex="4" MaxLength="2"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0">
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
                                <asp:RadioButton ID="rbtIBD" runat="server" CssClass="elementLabel" Text="Vendor Bill Discounting"
                                    GroupName="TransType" AutoPostBack="true" OnCheckedChanged="rbtIBD_CheckedChanged" Enabled="false" />&nbsp;
                            </td>
                            <td align="left">
                                <asp:RadioButton ID="rbtBEB" runat="server" CssClass="elementLabel" Text="Advance"
                                    GroupName="TransType" AutoPostBack="true" OnCheckedChanged="rbtBEB_CheckedChanged" Enabled="false" />&nbsp;
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
                                <asp:GridView ID="GridViewEXPDocRealised" runat="server" AutoGenerateColumns="False"
                                    Width="100%" AllowPaging="True" OnRowCommand="GridViewEXPDocRealised_RowCommand"
                                    OnRowDataBound="GridViewEXPDocRealised_RowDataBound">
                                    <PagerSettings Visible="false" />
                                    <RowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                        CssClass="gridItem" />
                                    <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Middle" CssClass="gridHeader" />
                                    <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                        CssClass="gridAlternateItem" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Document No" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocumentNo" runat="server" Text='<%# Eval("Document_No") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sr No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SR_NO") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Realised Date" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="7%" ItemStyle-Width="7%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocumentDate" runat="server" Text='<%# Eval("Realised_Date","{0:dd/MM/yyyy}") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer Name" HeaderStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="30%" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustName" runat="server" Text='<%# Eval("CUST_NAME") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Currency" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="5%"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCurr" runat="server" Text='<%# Eval("Currency") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Realised Amount" HeaderStyle-HorizontalAlign="Right"
                                            HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Realised_Amount","{0:0.00}") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Payment Ind" HeaderStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPaymentIndication" runat="server" Text='<%# Eval("Payment_Indication") %>'
                                                    CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Overseas Bank" HeaderStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBenfName" runat="server" Text='<%# Eval("BankName") %>' CssClass="elementLabel"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="5%" HeaderText="Delete"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" Visible="true">
                                            <ItemTemplate>
                                                <asp:Button ID="btnDelete" runat="server" CommandArgument='<%# Eval("Document_No")+";"+Eval("SR_NO") %>'
                                                    CommandName="RemoveRecord" Text="Delete" ToolTip="Delete Record" />
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
