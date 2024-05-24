<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_AddEditSundryAccountMaster.aspx.cs" Inherits="IMP_TF_IMP_AddEditSundryAccountMaster" %>

<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register src="../Menu/Menu.ascx" tagname="Menu" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico"
        type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script src="../Scripts/Validations.js"  language="javascript" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
      
        function validateSave() {
            var txtcode = document.getElementById('txtcode');
          
            if (txtcode.value=="") 
            {
                alert('Plese Enter Code.');
                txtcode.focus();
                return false;
            }
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
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
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnBack" />
                    <asp:PostBackTrigger ControlID="btnCancel" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel">Sundry Account Master Details</span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" OnClick="btnBack_Click"
                                    ToolTip="Back" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="sundryAc_Rdbtn" runat="server"   text="Sundry Account" 
                                GroupName="Account" oncheckedchanged="sundryAc_Rdbtn_CheckedChanged" AutoPostBack=True></asp:RadioButton>
                            &nbsp;&nbsp;
                            <asp:RadioButton ID="InteroffAc_Rdbtn" runat="server" GroupName="Account" 
                                text="Inter Office Account" 
                                oncheckedchanged="InteroffAc_Rdbtn_CheckedChanged" AutoPostBack=True />
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            &nbsp;</td>

                            <td align="right" style="width: 100%" valign="top" colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="2">
                                <table cellspacing="0" cellpadding="0" border="0" width="400px" style="line-height: 150%">
                                 <tr>
                                        <td align="right" style="width: 110px">
                                           <span class="elementLabel">SrNo. :</span>
                                        </td>
                                        <td align="left" style="width: 290px">
                                            &nbsp;<asp:TextBox ID="txtSrno" runat="server" CssClass="textBox" MaxLength="5"
                                                Width="60"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 110px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Code :</span>
                                        </td>
                                        <td align="left" style="width: 290px">
                                            &nbsp;<asp:TextBox ID="txtcode" runat="server" CssClass="textBox" MaxLength="5" onkeypress="return isNumberKey(event)"
                                                Width="60"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 110px">
                                            <span class="elementLabel">Description :</span>
                                        </td>
                                        <td align="left" style="width: 290px">
                                            &nbsp;<asp:TextBox ID="txtdescription" style="text-transform:uppercase" runat="server" CssClass="textBox" MaxLength="50"
                                                Width="200"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 110px">
                                           <span class="elementLabel">Branch :</span>
                                        </td>
                                        <td align="left" style="width: 290px">
                                            &nbsp;<asp:TextBox ID="txtBranch" style="text-transform:uppercase" runat="server" CssClass="textBox" MaxLength="2"
                                                Width="60"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 110px">
                                            <span class="elementLabel">CCY :</span>
                                        </td>
                                        <td align="left" style="width: 290px">
                                            &nbsp;<asp:TextBox ID="txtCcy" style="text-transform:uppercase" runat="server" CssClass="textBox" MaxLength="3"
                                                Width="60"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 110px">
                                            <span class="elementLabel">Cust Abb :</span>
                                        </td>
                                        <td align="left" style="width: 290px">
                                            &nbsp;<asp:TextBox ID="txtCustabb" style="text-transform:uppercase" runat="server" CssClass="textBox" MaxLength="20"
                                                Width="200"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 110px">
                                            <span class="elementLabel">Account No :</span>
                                        </td>
                                        <td align="left" style="width: 290px">
                                            &nbsp;<asp:TextBox ID="txtCustAcNo" style="text-transform:uppercase" runat="server" CssClass="textBox" MaxLength="20"
                                                Width="200"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 110px">
                                        </td>
                                        <td align="left" style="width: 290px; padding-top: 10px; padding-bottom: 10px">
                                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                                OnClick="btnSave_Click" ToolTip="Save" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                OnClick="btnCancel_Click" ToolTip="Cancel" />
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
