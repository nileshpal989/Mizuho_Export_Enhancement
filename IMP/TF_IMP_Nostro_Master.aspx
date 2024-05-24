<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_Nostro_Master.aspx.cs"
    Inherits="IMP_TF_IMP_Nostro_Master" %>

<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Validations.js" language="javascript" type="text/javascript"></script>
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <%--snackbar--%>
    <link href="../Style/SnackBar.css" rel="stylesheet" type="text/css" />
    <script src="../Help_Plugins/MyJquery1.js" type="text/javascript"></script>
    <script src="../Help_Plugins/jquerynew.min.js" language="javascript" type="text/javascript"></script>
    <script src="../Help_Plugins/jquery-ui.js" type="text/javascript"></script>
    <link href="../Help_Plugins/JueryUI.css" rel="stylesheet" type="text/css" />
    <script src="../Help_Plugins/AlertJquery.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function validate_Number(evnt) {

            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            //  alert(charCode);
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }
        function validation() {
            var CustABBR = document.getElementById('txtCustABBR')
            if (CustABBR.value == '') {
                alert('Nostro Bank Id Can Not be Blank.','#txtCustABBR')
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <br />
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
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel"><b>Nostro Bank Master Details<b></span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                    TabIndex="11" OnClick="btnBack_Click" />
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
                                <table cellspacing="0" cellpadding="0" border="0" width="600px" style="line-height: 150%">
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Nostro Bank Id :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            <asp:TextBox ID="txtCustABBR" runat="server" CssClass="textBox" TabIndex="1"
                                                Width="100px" MaxLength="12">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Currency :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="dropdownList" TabIndex="2"
                                                Width="100px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">GL Code :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            <asp:TextBox ID="txtGLcode" runat="server" CssClass="textBox" TabIndex="3" MaxLength="5"
                                                Width="80px"></asp:TextBox>
                                                 <span class="elementLabel"> &nbsp;&nbsp; ( 5 Digit Numeric )</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">A/c No. :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            <asp:TextBox ID="txtACNo" runat="server" CssClass="textBox" TabIndex="4" MaxLength="20"
                                                Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Swift Code :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            <asp:TextBox ID="txtSwiftCode" runat="server" CssClass="textBox" TabIndex="5" MaxLength="20"
                                                Width="120px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">A/c Type :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            <asp:TextBox ID="txtACtype" runat="server" CssClass="textBox" TabIndex="6" MaxLength="6"
                                                Width="60px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Nostro A/c No. :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            <asp:TextBox ID="txtNostroACno" runat="server" CssClass="textBox" TabIndex="7" MaxLength="25"
                                                Width="170px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Bank Name :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            <asp:TextBox ID="txtBankName" runat="server" CssClass="textBox" TabIndex="8" MaxLength="100"
                                                Width="400px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 200px">
                                        </td>
                                        <td align="left" style="width: 400px; padding-top: 10px; padding-bottom: 10px">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault" ToolTip="Save"
                                                TabIndex="9" OnClick="btnSave_Click" />
                                            <asp:Button ID="txtCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                ToolTip="Cancel" TabIndex="10" OnClick="txtCancel_Click" />
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
