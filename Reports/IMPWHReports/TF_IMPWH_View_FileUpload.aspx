<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMPWH_View_FileUpload.aspx.cs" Inherits="Reports_IMPWHReports_TF_IMPWH_View_FileUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
   <form id="form1" runat="server" autocomplete="off">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">     
        <ContentTemplate>

           <%-- <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>--%>
          <rsweb:ReportViewer ID="ReportViewer1" runat="server"  Height="500px" 
            ShowParameterPrompts="False" Width="100%" ShowPrintButton="true">
          </rsweb:ReportViewer>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>