<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Impw_CustomerMandatoryFieldMaster.aspx.cs" Inherits="ImportWareHousing_Masters_Impw_CustomerMandatoryFieldMaster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
   
    <script type="text/javascript" language="javascript">
        function CustHelp() {

            var brname = document.getElementById('ddlBranch');

            if (brname.value == "---Select---") {
                alert('Please select branchname');
                brname.focus();
                return false;

            }
            popup = window.open('../HelpForms/TF_IMPWH_ImportCustomer.aspx', 'CustHelp', 'height=500,  width=450,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "CustHelp";
        }
        function selectCustomer(Cust, label) {
            document.getElementById('txtcustname').value = Cust;
            document.getElementById('lblcustname').innerText = label;
            javascript: setTimeout('__doPostBack(\'txtcustname\',\'\')', 0)
        }

        
    
    </script>
    <style type="text/css">
        hr
        {
            display: block;
            margin-top: 0.5em;
            margin-bottom: 0.5em;
            margin-left: auto;
            margin-right: auto;
            border-style: inset;
            border-width: 2px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script src="../../Scripts/InitEndRequest.js" type="text/javascript"></script>
    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="../../Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div>
        <center>
            <uc2:Menu ID="Menu1" runat="server" />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                &nbsp;<span class="pageLabel"><strong>Customer Mandatory Master </strong>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 50%" valign="top">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100%" valign="top">
                                <asp:Label ID="lblmessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td align="left" style="width: 100%;" valign="top">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%;" valign="top">
                                <table cellspacing="0" cellpadding="0" border="0" width="1300px" style="line-height: 150%">
                                    <tr>
                                        <td width="12%" align="right" nowrap>
                                            <span class="mandatoryField">*</span><span class="elementLabel">Branch &nbsp;</span>
                                        </td>
                                        <td align="left" nowrap>
                                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" AutoPostBack="true"
                                                Width="100px" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                     <tr>
                                       <td width="12%" align="right" nowrap>
                                            <span class="mandatoryField">&nbsp; *</span><span class="elementLabel">Customer Id
                                                &nbsp;</span>
                                        </td>
                                        <td  width="3%" align="left" nowrap>
                                            <asp:TextBox ID="txtcustname" runat="server" Width="100" TabIndex="2" MaxLength="20"
                                                CssClass="textBox" ToolTip="Press F2 for Help" 
                                                ontextchanged="txtcustname_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </td>
                                        <td width="3%" nowrap>
                                            <asp:Image ID="btnCustHelp" runat="server" ImageUrl="~/Style/images/help1.png" />
                                        </td>
                                        <td>
                                             <asp:Label ID="lblcustname" CssClass="elementLabel" runat="server" Text=""></asp:Label>
                                               <asp:Label ID="impcust" CssClass="elementLabel" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                 
                                </table>
                                <br />
                                <asp:GridView ID="GridViewMenuList" runat="server" AutoGenerateColumns="False" PageSize="5"
                                        Width="40%">
                                        <AlternatingRowStyle CssClass="gridAlternateItem" Height="18px" HorizontalAlign="Left"
                                            VerticalAlign="Middle" Wrap="False" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr No.">
                                                <ItemTemplate>
                                                    <span class="elementLabel">
                                                        <%# ((GridViewRow)Container).RowIndex + 1%></span>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Coloum Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMenu" runat="server" CssClass="elementLabel" Text='<%# Eval("MenuName") %>'></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Allow/Deny" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%"
                                                ItemStyle-Width="10%">
                                                <HeaderTemplate>
                                                    <asp:CheckBox runat="server" ID="HeaderChkAllow" AutoPostBack="true" ToolTip="Select All"
                                                        Text="Yes/No"  TabIndex="2" OnCheckedChanged="HeaderChkAllow_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" ID="RowChkAllow" AutoPostBack="true" OnCheckedChanged="RowChkAllow_CheckedChanged"
                                                        TabIndex="3" />
                                                    <asp:Label ID="lblAccess" runat="server" CssClass="elementLabel" ForeColor="RED"
                                                        Text="No"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                                            </asp:TemplateField>
                                           <%-- <asp:TemplateField HeaderText="Module">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblModule" runat="server" CssClass="elementLabel" Text='<%# Eval("Module") %>'></asp:Label></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                                            </asp:TemplateField>--%>
                                        </Columns>
                                        <HeaderStyle CssClass="gridHeader" ForeColor="#1A60A6" VerticalAlign="Top" />
                                        <RowStyle CssClass="gridItem" Height="18px" HorizontalAlign="Left" VerticalAlign="Top"
                                            Wrap="False" />
                                    </asp:GridView>
                                
                                <table>
                                  
                                </table>
                                <br />
                            </td>
                        </tr>
                         <tr>
                               
                               
                                <td width="15%" align="left" nowrap>
                                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault" ToolTip="Save"
                                       TabIndex="4" onclick="btnSave_Click" />&nbsp;&nbsp;&nbsp;
                                         <asp:Button ID="btncancel" runat="server" Text="Cancel" 
                                        CssClass="buttonDefault" ToolTip="Save"
                                       TabIndex="4" onclick="btncancel_Click"  />
                                </td>
                            </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top">
                                <hr />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
</body>
</html>
