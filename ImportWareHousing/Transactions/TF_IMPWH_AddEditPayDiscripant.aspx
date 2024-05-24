<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMPWH_AddEditPayDiscripant.aspx.cs"
    Inherits="ImportWareHousing_Transactions_TF_IMPWH_AddEditPayDiscripant" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript">
        function validateSave() {
            var type = document.getElementById('txtauthosign');
            if (type.value == '') {
                alert('Enter Authorised Signatory.');
                type.focus();
                return false;
            }
            // return true;
        }
        function checkEmail(textbox) {
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (textbox.value.trim() == "") {
                return false;
            }
            if (!filter.test(textbox.value)) {
                alert('Please provide a valid email address');
                textbox.focus();
                return false;
            }
        }
        //        function ConfirmDelete() {
        //            var ans = confirm('Do You want to delete this Record?');
        //            if (ans) {
        //                return true;
        //            }
        //            else
        //                return false;
        //        }
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="../../Scripts/InitEndRequest.js"></script>
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
        <uc2:Menu ID="Menu1" runat="server" />
        <br />
        <br />
        <center>
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="conditional">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel"><b>GDP Details<b></span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100%" valign="top" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="2">
                                <table border="0" cellspacing="0" width="50%">
                                    <tr>
                                        <td align="right" width="10%" nowrap>
                                            <span class="elementLabel">Branch :</span>
                                        </td>
                                        <td align="left" nowrap width="10%">
                                            &nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right" width="10%" nowrap>
                                            <span class="elementLabel">Port Code :</span>
                                        </td>
                                        <td align="left" nowrap width="10%">
                                            &nbsp;<asp:TextBox ID="txtPortCode" runat="server" CssClass="textBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="10%" nowrap>
                                            <span class="elementLabel"><span class="mandatoryField">* </span>Import Agency :</span>
                                        </td>
                                        <td align="left" nowrap width="10%">
                                            &nbsp;<asp:DropDownList ID="ddImpAgency" runat="server" CssClass="dropdownList" TabIndex="4"
                                            Width="150px">
                                            <asp:ListItem Value="1">Customs</asp:ListItem>
                                            <asp:ListItem Value="2">SEZ</asp:ListItem>
                                        </asp:DropDownList>
                                        </td>
                                        <td align="right" width="10%" nowrap>
                                            <span class="elementLabel">Bill Of Entry No. :</span>
                                        </td>
                                        <td align="left" nowrap width="10%">
                                            &nbsp;<asp:TextBox ID="TextBox1" runat="server" CssClass="textBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="10%" nowrap>
                                            <span class="elementLabel">Bill Of Entry Date :</span>
                                        </td>
                                        <td align="left" nowrap width="10%">
                                            &nbsp;<asp:TextBox ID="txtbilldate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                            Width="70" TabIndex="8"></asp:TextBox>
                                        </td>
                                        <td align="right" width="10%" nowrap>
                                            <span class="elementLabel">Customer A/C :</span>
                                        </td>
                                        <td align="left" nowrap width="10%">
                                            &nbsp;<asp:TextBox ID="TextBox2" runat="server" CssClass="textBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="10%" nowrap>
                                            <span class="elementLabel">Invoice No :</span>
                                        </td>
                                        <td align="left" nowrap width="10%">
                                            &nbsp;<asp:TextBox ID="TextBox7" runat="server" CssClass="textBox"></asp:TextBox>
                                        </td>
                                        <td align="right" width="10%" nowrap>
                                            <span class="elementLabel">Term Invoice :</span>
                                        </td>
                                        <td align="left" nowrap width="10%">
                                            &nbsp;<asp:TextBox ID="TextBox3" runat="server" CssClass="textBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="10%" nowrap>
                                            <span class="elementLabel">Invoice Currency :</span>
                                        </td>
                                        <td align="left" nowrap width="10%">
                                            &nbsp;<asp:TextBox ID="TextBox8" runat="server" CssClass="textBox"></asp:TextBox>
                                        </td>
                                        <td align="right" width="10%" nowrap>
                                            <span class="elementLabel">Invoice Amount:</span>
                                        </td>
                                        <td align="left" nowrap width="10%">
                                            &nbsp;<asp:TextBox ID="TextBox4" runat="server" CssClass="textBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="10%" nowrap>
                                            <span class="elementLabel">Supplier Name :</span>
                                        </td>
                                        <td align="left" nowrap width="10%">
                                            &nbsp;<asp:TextBox ID="TextBox9" runat="server" CssClass="textBox"></asp:TextBox>
                                        </td>
                                        <td align="right" width="10%" nowrap>
                                            <span class="elementLabel">Supplier Country :</span>
                                        </td>
                                        <td align="left" nowrap width="10%">
                                            &nbsp;<asp:TextBox ID="TextBox5" runat="server" CssClass="textBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" width="10%" nowrap>
                                            <span class="elementLabel">Seller Name :</span>
                                        </td>
                                        <td align="left" nowrap width="10%">
                                            &nbsp;<asp:TextBox ID="TextBox10" runat="server" CssClass="textBox"></asp:TextBox>
                                        </td>
                                        <td align="right" width="10%" nowrap>
                                            <span class="elementLabel">Seller Country :</span>
                                        </td>
                                        <td align="left" nowrap width="10%">
                                            &nbsp;<asp:TextBox ID="TextBox6" runat="server" CssClass="textBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td align="right" style="width: 30px">
                                        </td>
                                        <td align="left" style="width: 400px; padding-top: 10px; padding-bottom: 10px">
                                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                                ToolTip="Save" TabIndex="4" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                ToolTip="Cancel" TabIndex="5" />
                                        </td>
                                    </tr>
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
