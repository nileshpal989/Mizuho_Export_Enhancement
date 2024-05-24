<%@ Page Language="C#" AutoEventWireup="true" CodeFile="View_EXP_billpendings_ACK_in _EDPMS.aspx.cs" Inherits="Reports_EXPReports_View_EXP_billpendings_ACK_in__EDPMS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <div>
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     
        <ContentTemplate>
          <rsweb:ReportViewer ID="ReportViewer1" runat="server"  Height="500px" 
            ShowParameterPrompts="False" Width="100%" ShowPrintButton="true">
     </rsweb:ReportViewer>
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>