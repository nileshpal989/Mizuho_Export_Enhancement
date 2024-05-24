<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_CustomerMaster_CSV_file_Genaration.aspx.cs"
    Inherits="TF_CustomerMaster_CSV_file_Genaration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <link href="Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function validateSave() {
            var Branch = document.getElementById('ddlBranch').value;
            if (Branch == 0) {
                alert('Select Branch Name.');
                Branch.focus();
                return false;
            }

        }
    
    </script>
</head>
<body>
    <form id="form2" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script src="Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script src="Scripts/InitEndRequest.js" type="text/javascript"></script>
    <%--<asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <div>
        <uc1:Menu ID="Menu1" runat="server" />
        <br />
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
            <ContentTemplate>
                <table cellspacing="2" cellpadding="2" border="0" width="100%">
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom">
                            <span class="pageLabel">Customer Master CSV File Creation</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top">
                            <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                <tr>
                                    <td width="10%" align="right" nowrap>
                                        <span class="mandatoryField">* </span><span class="elementLabel">Branch :</span>
                                    </td>
                                    <td style="white-space: nowrap; text-align: left">
                                        &nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" AutoPostBack="true"
                                            Width="100px" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="white-space: nowrap; text-align: right">
                                        <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField" Style="text-align: right"></asp:Label>
                                    </td>
                                </tr>
                                <tr valign="bottom">
                                    <td align="right" style="width: 120px">
                                    </td>
                                    <td align="left" style="width: 700px; padding-top: 10px; padding-bottom: 10px" valign="bottom">
                                        &nbsp;
                                        <asp:Button ID="btnCreate" runat="server" CssClass="buttonDefault" Text="Generate"
                                            ToolTip="Generate" TabIndex="6" OnClick="btnCreate_Click" />
                                        <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                            Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                        <asp:Button ID="Button1" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                            Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                    </td>
                                </tr>
                            </table>
                            <input type="hidden" runat="server" id="hdnFromDate" />
                            <input type="hidden" runat="server" id="hdnToDate" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
