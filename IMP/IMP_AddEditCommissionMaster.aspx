<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IMP_AddEditCommissionMaster.aspx.cs" Inherits="IMP_IMP_AddEditCommissionMaster" %>

<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico"
        type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script language="javascript" type="text/javascript">
        
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
     <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>
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
        <uc1:Menu ID="Menu1" runat="server" />
        <br />
        <br />
        <center>
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="conditional">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width:50%" valign="bottom">
                                <span class="pageLabel">Commission Master Details</span>
                            </td>
                            <td align=right style="width:50%">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width:100%" valign="top" colspan="2">
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
                                <table cellspacing="0" cellpadding="0" border="0" width="600px" style="line-height:150%">
                                    <tr>
                                        <td align="right" style="width:200px">
                                            <span class="elementLabel">Type :</span>
                                        </td>
                                        <td align="left" style="width:400px">
                                            &nbsp;<asp:TextBox ID="txtType" runat="server" CssClass="textBox" MaxLength="10" Width="50px"
                                                TabIndex="1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width:200px">
                                            <span class="elementLabel">Name :</span>
                                        </td>
                                         <td align="left" style="width: 400px">
                                            &nbsp;<asp:TextBox ID="txtDescription" runat="server" CssClass="textBox" MaxLength="50"
                                                Width="150px" TabIndex="1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width:200px">
                                            <span class="elementLabel">Rate (%) :</span>
                                        </td>
                                         <td align="left" style="width: 400px">
                                            &nbsp;<asp:TextBox ID="txtRate" runat="server" CssClass="textBox" MaxLength="3"
                                                Width="50px" TabIndex="2"></asp:TextBox>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td align="right" style="width: 200px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Minimum Amount :</span>
                                        </td>
                                        <td align="left" style="width: 400px">
                                            &nbsp;<asp:TextBox ID="txtMinAmt" runat="server" CssClass="textBox" MaxLength="5" Width="40px"
                                                TabIndex="3"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                 <br />
                                <table cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td align="right" style="width: 70px">
                                        </td>
                                        <td align="left" style="width: 400px; padding-top: 10px; padding-bottom: 10px">
                                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                                ToolTip="Save" TabIndex="4" onclick="btnSave_Click"/>
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                ToolTip="Cancel" TabIndex="5"/>
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
