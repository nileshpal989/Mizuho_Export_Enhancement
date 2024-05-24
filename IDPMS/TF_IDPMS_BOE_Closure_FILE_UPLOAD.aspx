<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IDPMS_BOE_Closure_FILE_UPLOAD.aspx.cs"
    Inherits="TF_IDPMS_BOE_Closure_FILE_UPLOAD" %>

<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-Tradefinance System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style_V2.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="../Scripts/Enable_Disable_Opener.js"></script>
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
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
        </center>
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnupload" />
            </Triggers>
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td align="left" colspan="2">
                            <span class="pageLabel"><strong>Excel File Upload - BOE Closure</strong></span>
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
                            <span class="elementLabel">Select File : </span>
                        </td>
                        <td align="left">
                            <asp:FileUpload ID="FileUpload1" runat="server" Width="400px" />
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
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left" valign="middle">
                            <asp:Label ID="lblUploadMsg" runat="server" CssClass="pageLabel"></asp:Label>
                            <br />
                            <asp:Label ID="lblValMsg" runat="server" CssClass="pageLabel"></asp:Label>
                            <br />
                            <asp:Label ID="lblProcessMsg" runat="server" CssClass="pageLabel"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
