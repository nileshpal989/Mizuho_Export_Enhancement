<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/IDPMS/IDPMS_EXTN_UPLOAD.aspx.cs"
    Inherits="IDPMS_IDPMS_EXTN_UPLOAD" %>

<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>LMCC TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:Menu ID="Menu1" runat="server" />
        <table width="100%">
            <tr>
                <td align="left" colspan="2">
                    <br />
                    <span class="pageLabel"><strong>Excel File Upload - Payment Extension</strong></span>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            <tr>
                <td align="right" width="5%" nowrap>
                    <span class="elementLabel">Branch :</span>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlBranch" runat="server" CssClass="textBox">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" nowrap>
                    <span class="elementLabel">Select File : </span>
                </td>
                <td align="left">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="400px" />
                </td>
            </tr>
            <tr>
                <td>
                    <span class="elementLabel">Input File : </span>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtFileName" runat="server" Width="400px" Enabled="false" CssClass="txtdisabled"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="left">
                    <asp:Button ID="btnupload" CssClass="buttonDefault" Text="Upload" runat="server"
                        OnClick="btnupload_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnValidate" CssClass="buttonDefault" Text="Validate" runat="server"
                        OnClick="btnValidate_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnProcess" CssClass="buttonDefault" Text="Process" runat="server"
                        OnClick="btnProcess_Click" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="left">
                    <br />
                    <asp:Label ID="lblInstMsg" Font-Bold="true" runat="server" CssClass="pageLabel" ForeColor="Red"
                        Text="1.First Upload Excel File &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.Validate For Error Records &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.Process"></asp:Label>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="left" valign="middle">
                    <asp:Label ID="lblupload" runat="server" CssClass="pageLabel"></asp:Label>
                    <br />
                    <asp:Label ID="lblvalidation" runat="server" CssClass="pageLabel"></asp:Label>
                    <br />
                    <asp:Label ID="lblprocess" runat="server" CssClass="pageLabel"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
