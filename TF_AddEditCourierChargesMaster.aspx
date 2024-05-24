<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_AddEditCourierChargesMaster.aspx.cs"
    Inherits="TF_AddEditCourierChargesMaster" %>

<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="Style/style_new.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            //alert(charCode);
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;

        }
            function validateSave() {
            var txtCourierID = document.getElementById('txtCourierchrgID');
            var txtCourierDesc = document.getElementById('txtDescription');
            var txtAmt = document.getElementById('txtAmount');

            if (txtCourierID.value == '') {
                alert('Enter Courier Charges ID.');
                txtCourierID.focus();
                return false;
            }
            if (txtCourierDesc.value == '') {
                alert('Enter Courier Description.');
                txtCourierDesc.focus();
                return false;
            }
            if (txtAmt.value == '') {
                alert('Enter Amount.');
                txtAmt.focus();
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" defaultbutton="btnSave">
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
                                <span class="pageLabel"><strong>Courier Charges Master Details</strong></span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" OnClick="btnBack_Click"
                                    ToolTip="Back" TabIndex="9" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" Font-Bold="true" Font-Size="Small" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="2">
                                <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 150%">
                                    <tr>
                                        <td align="right" style="width: 10%" nowrap>
                                            <span class="mandatoryField">*</span><span class="elementLabel">Courier Charges ID :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtCourierchrgID" runat="server" CssClass="textBox" MaxLength="2"
                                                Width="40" TabIndex="1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 10%" nowrap>
                                            <span class="mandatoryField">*</span><span class="elementLabel">Description :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtDescription" runat="server" CssClass="textBox" MaxLength="60"
                                                Width="400px" TabIndex="2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 10%">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Amount :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtAmount" runat="server" CssClass="textBoxRight" MaxLength="10"
                                                Width="100px" TabIndex="3"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align="right">
                                        </td>
                                        <td align="left" style="width: 290px; padding-top: 10px; padding-bottom: 10px">
                                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                                OnClick="btnSave_Click" ToolTip="Save" TabIndex="7" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                OnClick="btnCancel_Click" ToolTip="Cancel" TabIndex="8" />
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
