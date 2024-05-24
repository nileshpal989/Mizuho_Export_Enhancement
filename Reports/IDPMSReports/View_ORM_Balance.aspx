<%@ Page Language="C#" AutoEventWireup="true" CodeFile="View_ORM_Balance.aspx.cs" Inherits="Reports_EDPMS_Reports_View_ORM_Balance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<script runat="server">

  
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="formrdata" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="580px" ShowParameterPrompts="False"
                Width="100%" Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)"
                WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                <LocalReport ReportPath="Reports\EDPMS_Reports\ORM_Balance_Report.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource  Name="DataSet1" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

