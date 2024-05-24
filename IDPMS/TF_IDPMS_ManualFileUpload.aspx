<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IDPMS_ManualFileUpload.aspx.cs"
    Inherits="IDPMS_TF_IDPMS_ManualFileUpload" %>

<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-Tradefinance System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script type="text/javascript">
        function showFileName(input) {
            var file = $("#FileUpload1").val();
            var file = document.getElementById('FileUpload1').value;
            document.getElementById("txtInputFile").value = file;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:Menu ID="Menu1" runat="server" />
        <table width="100%">
            <tr>
                <td align="left" colspan="2">
                    <br />
                    <span class="pageLabel"><strong>Excel File Upload - Manual BOE</strong></span>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            <tr>
                <td align="right" width="5%" style="white-space: nowrap">
                    <span class="elementLabel">Branch :</span>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlBranch" runat="server" CssClass="textBox">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space: nowrap">
                    <span class="elementLabel">Select File :</span>
                </td>
                <td align="left">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="30%" />
                </td>
            </tr>
            <tr>
                <td align="right" style="white-space: nowrap">
                    <span class="elementLabel">Input File :</span>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtInputFile" runat="server" Width="30%" Enabled="false" CssClass="txtdisabled"></asp:TextBox>
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
                        OnClick="btnupload_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnValidate" CssClass="buttonDefault" Text="Validate" runat="server"
                        OnClick="btnValidate_Click" />&nbsp;&nbsp;
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
                    <asp:Label ID="labelMessage" runat="server" CssClass="pageLabel"></asp:Label>
                    <br />
                    <asp:Label ID="labelMessage1" runat="server" CssClass="pageLabel"></asp:Label>
                    <br />
                    <asp:Label ID="labelMessage2" runat="server" CssClass="pageLabel"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
