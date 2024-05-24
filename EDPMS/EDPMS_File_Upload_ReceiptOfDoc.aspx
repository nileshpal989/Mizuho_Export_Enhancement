<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDPMS_File_Upload_ReceiptOfDoc.aspx.cs"
    Inherits="EDPMS_EDPMS_File_Upload_ReceiptOfDoc" %>

<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-Tradefinance System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
          <uc1:Menu ID="Menu1" runat="server" />
        <table width="100%">
            <tr>
                <td align="left" colspan="2">
                    <span class="pageLabel">File Upload - Receipt of Document</span>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    <asp:Label CssClass="mandatoryField" ID="lblmessage" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" width="5%" nowrap>
                    <span class="elementLabel">Reference Number :</span>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlRefNo" runat="server" CssClass="textBox">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <span class="elementLabel">Select File : </span>
                </td>
                <td align="left">
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <br>
                </td>
            </tr>
            <%--<tr>
                <td>
                </td> 
                <td align="left">
                    <span class="pageLabel"><strong>Do you want to upload now? </strong></span>
                </td>
            </tr>--%>
            <tr>
                <td>
                </td>
                <td align="left">
                    <asp:Button ID="btnupload" CssClass="buttonDefault" Text="Upload" runat="server"
                        OnClick="btnupload_Click" />
                    <asp:Button ID="btnValidate" CssClass="buttonDefault" Text="Validate" runat="server"
                        OnClick="btnValidate_Click" />
                    <asp:Button ID="btnProcess" CssClass="buttonDefault" Text="Process" runat="server"
                        OnClick="btnProcess_Click" />
                </td>
            </tr>
            <tr height="60px" valign="bottom">
                <td>
                </td>
                <td align="left">
                    <asp:Label ID="labelMessage" runat="server" CssClass="pageLabel"></asp:Label>
                    <asp:Label ID="lbltest" runat="server" CssClass="pageLabel"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
