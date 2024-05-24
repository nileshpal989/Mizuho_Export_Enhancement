<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_EBRC_ORM_FileUpload.aspx.cs" Inherits="EBR_TF_EBRC_ORM_FileUpload" %>

<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LMCC-EBRC SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script src="../Help_Plugins/jquerynew.min.js" language="javascript" type="text/javascript"></script>
    <link href="../Help_Plugins/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Help_Plugins/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <script src="../Help_Plugins/MyJquery1.js" language="javascript" type="text/javascript"></script>
    <script src="../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script src="../Scripts/TF_EBRC_ORM.js" type="text/javascript"></script>
    <link href="../Style/TF_EBRC_ORM.css" rel="Stylesheet" type="text/css" media="screen" />
</head>
<body>
    <form id="formrdata" runat="server">
        <asp:ScriptManager ID="ScriptManagerMain" runat="server">
        </asp:ScriptManager>
        <div class="loading" align="center">
            Please wait while the File is uploading..<br />
            <br />
            <img src="../Images/ProgressBar1.gif" alt="" />
        </div>
        <div class="loadingValidate" align="center">
            Please wait while the File is validating..<br />
            <br />
            <img src="../Images/ProgressBar1.gif" alt="" />
        </div>
        <div class="loadingProcess" align="center">
            Please wait while the File is processing..<br />
            <br />
            <img src="../Images/ProgressBar1.gif" alt="" />
        </div>
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
                <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">

                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnupload" />
                        <asp:PostBackTrigger ControlID="btnExport" />
                         <asp:PostBackTrigger ControlID="btnrecordnotuploaded" />
                        <asp:PostBackTrigger ControlID="btnValidate" />
                        <asp:PostBackTrigger ControlID="btnProcess" />
                    </Triggers>
                    <ContentTemplate>
                        <table cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="left" style="width: 50%" valign="bottom">
                                    <asp:Label ID="pgLabel" runat="server" CssClass="pageLabel" Style="font-weight: bold">ORM File Upload For DGFT</asp:Label>
                                </td>
                                <td align="right" style="width: 50%">&nbsp;
                                </td>
                            </tr>
                            <input type="hidden" id="hdnhiide" runat="server" />
                            <tr>
                                <td align="left" style="width: 100%" valign="top" colspan="2">
                                    <hr />
                                    <input type="hidden" id="hdnbranchcode" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="2">
                                    <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 150%">
                                        <tr>
                                            <td align="right">
                                                <span class="elementLabel">Branch :</span>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" AutoPostBack="true" runat="server"
                                                    OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" TabIndex="1">
                                                    <asp:ListItem>Mumbai</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span class="elementLabel">Select File : </span>
                                            </td>
                                            <td align="left">
                                                <asp:FileUpload ID="FileUpload1" runat="server" Width="500" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <span class="elementLabel">Input File :</span>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtInputFile" runat="server" CssClass="textBox" MaxLength="10" Width="413px"
                                                    TabIndex="2"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td align="left">
                                                <asp:Button ID="btnupload" CssClass="buttonDefault" Text="Upload" runat="server"
                                                    OnClick="btnupload_Click" />
                                                <asp:Button ID="btnValidate" CssClass="buttonDefault" Text="Validate" runat="server"
                                                    OnClick="btnValidate_Click" />
                                                <asp:Button ID="btnProcess" CssClass="buttonDefault" Text="Process" runat="server"
                                                    OnClick="btnProcess_Click" />
                                                <asp:Button ID="btnExport" runat="server" CssClass="buttonDefault"
                                                    Text="Export" Style="display: none;" OnClick="btnExport_Click" />
                                                <asp:Button ID="btnrecordnotuploaded" runat="server" CssClass="buttonDefault"
                                                    Text="Export" Style="display: none;" OnClick="btnrecordnotuploaded_Click" />

                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td align="left">
                                                <asp:Label ID="lblHint" runat="server" CssClass="pageLabel"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr height="60px" valign="bottom">
                                            <td></td>
                                            <td align="left">
                                                <asp:Label ID="labelMessage" runat="server" CssClass="pageLabel"></asp:Label>
                                                <asp:Label ID="lbltest" runat="server" CssClass="pageLabel"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </center>
        </div>
        <asp:Label runat="server" ID="lblLog" Visible="false"></asp:Label>
    </form>
</body>
</html>
