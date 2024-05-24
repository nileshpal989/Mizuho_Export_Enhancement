<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDPMS_E_FIRC_Old_Closure_Upload.aspx.cs"
    Inherits="EDPMS_EDPMS_E_FIRC_Old_Closure_Upload" %>

<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LMCC-Tradefinance System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <link href="../Style/Style_V2.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:Menu ID="Menu1" runat="server" />
        <br />
        <table style="width: 100%">
            <tr>
                <td style="text-align: left" colspan="2">
                    <span class="pageLabel"><strong>Excel File Upload - E FIRC (Old) Closure</strong></span>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 5%; white-space: nowrap">
                    <span class="elementLabel">Branch :</span>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlBranch" runat="server" CssClass="textBox">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; white-space: nowrap">
                    <span class="elementLabel">Select File : </span>
                </td>
                <td style="text-align: left">
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space: nowrap">
                    <span class="elementLabel">Input File : </span>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtInputFile" runat="server" Width="400px" CssClass="textBox" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="text-align: left">
                    <asp:Button ID="btnupload" CssClass="buttonDefault" Text="Upload" runat="server"
                        OnClick="btnupload_Click" />
                    <asp:Button ID="btnValidate" CssClass="buttonDefault" Text="Validate" runat="server"
                        OnClick="btnValidate_Click" />
                    <asp:Button ID="btnProcess" CssClass="buttonDefault" Text="Generate XML" runat="server"
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
                </td>
            </tr>
            <tr style="height: 60px; vertical-align: bottom">
                <td>
                </td>
                <td style="text-align: left">
                    <asp:Label ID="labelMessage" runat="server" CssClass="pageLabel"></asp:Label><br />
                    <asp:Label ID="lblValMsg" runat="server" CssClass="pageLabel"></asp:Label><br />
                    <asp:Label ID="lbltest" runat="server" CssClass="pageLabel"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="text-align: left">
                    <asp:Label CssClass="pageLabel" ID="lblmessage" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
