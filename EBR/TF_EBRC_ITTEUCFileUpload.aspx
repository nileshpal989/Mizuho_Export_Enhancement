<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_EBRC_ITTEUCFileUpload.aspx.cs" Inherits="EBR_TF_EBRC_ITTEUCFileUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-Tradefinance System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <link href="../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script src="../Help_Plugins/jquerynew.min.js" language="javascript" type="text/javascript"></script>
    <link href="../Help_Plugins/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Help_Plugins/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <script src="../Help_Plugins/MyJquery1.js" language="javascript" type="text/javascript"></script>
    <script src="../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script src="../Scripts/TF_EBRC_ITT.js" type="text/javascript"></script>
    <link href="../Style/TF_EBRC_ITT.css" rel="Stylesheet" type="text/css" media="screen" />
</head>
<body>

    <form id="form1" runat="server">
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
        <div>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
                <Triggers>
                        <asp:PostBackTrigger ControlID="btnupload" />                        
                        <asp:PostBackTrigger ControlID="btnValidate" />
                        <asp:PostBackTrigger ControlID="btnProcess" />
                    </Triggers>
                </asp:UpdatePanel>
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left" colspan="2">
                            <span class="pageLabel"><strong>EBRC File Upload - ITT EUC</strong></span>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left" colspan="2">
                            <asp:Label CssClass="mandatoryField" ID="lblmessage" runat="server"></asp:Label>
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
                        <td></td>
                        <td style="text-align: left">
                            <asp:Button ID="btnupload" CssClass="buttonDefault" Text="Upload" runat="server"
                                OnClick="btnupload_Click" />
                            <asp:Button ID="btnValidate" CssClass="buttonDefault" Text="Validate" runat="server"
                                OnClick="btnValidate_Click" />
                            <asp:Button ID="btnProcess" CssClass="buttonDefault" Text="Process" runat="server"
                                OnClick="btnProcess_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td align="left">
                            <br />
                            <asp:Label ID="lblInstMsg" Font-Bold="true" runat="server" CssClass="pageLabel" ForeColor="Red"
                                Text="1.First Upload Excel File &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.Validate For Error Records &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.Process"></asp:Label>
                            <br />
                        </td>
                    </tr>
                    <tr style="height: 60px; vertical-align: bottom">
                        <td></td>
                        <td style="text-align: left">
                            <asp:Label ID="LBLCOUNT" runat="server" CssClass="pageLabel"></asp:Label><br />
                            <asp:Label ID="labelMessage" runat="server" CssClass="pageLabel"></asp:Label><br />
                            <asp:Label ID="lblValMsg" runat="server" CssClass="pageLabel"></asp:Label><br />
                            <asp:Label ID="lbltest" runat="server" CssClass="pageLabel"></asp:Label>
                        </td>
                    </tr>
                    
                </table>
             <asp:Button ID="btnExport" runat="server" CssClass="buttonDefault"
                                                    Text="Export" Style="display: none;" OnClick="btnExport_Click" />
            <asp:Button ID="btnrecordnotuploaded" runat="server" CssClass="buttonDefault"
                                                    Text="Export" Style="display: none;" OnClick="btnrecordnotuploaded_Click" />
        </div>
    </form>
</body>
</html>
