<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EXP_DocsReceived_ADTransfer_View.aspx.cs" Inherits="Reports_EXPReports_EXP_DocsReceived_ADTransfer_View" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EXPORT Documents Received Report- For AD Transfer</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID=ScriptManagerMain runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatPanelMain" runat="server">
        <ContentTemplate>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="500px" Width="1200px" ShowParameterPrompts="false">
        </rsweb:ReportViewer>
        </rsweb>
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
