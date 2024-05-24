<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_ForAcceptance.aspx.cs" Inherits="IMP_IMPReports_TF_IMP_ForAcceptance" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
   <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>LMCC-TRADE FINANCE SYSTEM</title>
     <link href="../../Style/style_new.css" rel="stylesheet" type="text/css" />
     <link href="../../Style/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style2
        {
            width: 277px;
        }
        .style3
        {
            width: 142px;
        }
        .style4
        {
            width: 86px;
        }
        .style5
        {
            width: 138px;
        }
        
        
          .divchart {
  position: absolute;
  left: 0px;
  top: 70px;
  z-index: -1;
 
      }
      
      .chartdiv
      {
          background-color:Red;
          }
        .style6
        {
            width: 239px;
        }
    </style>
</head>
<body>
     <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
     <div>
    
     
     <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="../../Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    </div>

        <table width="100%" align="center">
  <tr>
  <td>
  <table width="100%" style="background-color:#ECE7E4">
  <tr>
   <td align="right" class="style3">
  From Date
  </td>
   <td align="left" class="style2">
     <asp:TextBox ID="txtfromdate" runat="server" MaxLength="10"></asp:TextBox>


     <uc1:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtfromdate" Enabled="True">
                            </uc1:CalendarExtender>
   </td>
   <td align="right" class="style4">
   To Date
   </td>
   <td align ="left" class="style5">
       <asp:TextBox ID="txttodate" runat="server" MaxLength="10"></asp:TextBox>
       <uc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txttodate" Enabled="True" PopupPosition="BottomLeft">
                            </uc1:CalendarExtender>
   </td>
   <td align="left" class="style6" >
   Status  <asp:DropDownList ID="DropDownList3" runat="server" Width="165px">
                     </asp:DropDownList>
   </td>
   <td align ="left" class="style2">
      
   </td>
   </tr>
   <tr>
    <td align="right" class="style3">
   Lodgment Type
   </td>
   <td align="left" class="style2">
     <asp:DropDownList ID="DropDownList1" runat="server">
                     </asp:DropDownList>
   </td>
   <td align="right" class="style4">
    Branch</td>
   <td align="left" class="style5">
    <asp:DropDownList ID="DropDownList2" runat="server" Width="165px">
                     </asp:DropDownList>
   </td>
   <td align="left" colspan="2">
   &nbsp;<asp:Button ID="Search" runat="server" Text="Search" onclick="Search_Click" CssClass="buttonDefault" />
   </td>
  </tr>
  </table>
  </td>
  </tr>
    <tr>
    <td>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="85%" Width="100%"
                            ShowParameterPrompts="false" CssClass="divchart">
            </rsweb:ReportViewer>
        </ContentTemplate>
    </asp:UpdatePanel>
    </td>
    </tr>
    </table>
    </form>
</body>
</html>
