<%@ Page Language="C#" AutoEventWireup="true" CodeFile="View_IRM_AdustmentClosure.aspx.cs" Inherits="EDPMS_View_IRM_AdustmentClosure" %>
<%@ Register Src="../Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script type="text/javascript" language=javascript>
    
    
         function checkYear() {

             var d = new Date();
             var docYear = document.getElementById('txtYear');
             var docYearLen = docYear.value.length;

             if (docYearLen > 3) {

                 if (parseFloat(docYear.value) > 1990 && parseFloat(docYear.value) < 2050) {
                     return false;
                 }

                 else
                     docYear.value = d.getFullYear();
             }

             else
                 docYear.value = d.getFullYear();
         }

         function validate() {
             var docYear = document.getElementById('txtYear');
            // var docNo = document.getElementById('txtDocumentNo');
             var branchCode = document.getElementById('ddlBranch');


             if (branchCode.value == '0') {

                 alert('Enter Branch Code');
                 branchCode.focus();
                 return false;
             }

             if (docYear.value == '') {
                 alert('Enter Year');
                 docYear.focus();
                 return false;
             }

         }


         function validate_Number(evnt) {
             var charCode = (evnt.which) ? evnt.which : event.keyCode;
             // alert(charCode);
             if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                 return false;
             else
                 return true;
         }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
      <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <div>
    <center>
      <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
              
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel">IRM Adjustment Closure View</span>
                            </td>

                              <td align="right" style="width: 50%">
                                <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="buttonDefault" ToolTip="Add New Record"
                                    OnClick="btnAdd_Click" TabIndex="3" />

                                  <input type="hidden"  runat="server" id="hdnUserRole" />
                            </td>

                            </tr>

                              <tr>
                            <td colspan=2>
                              <hr />
                            </td>
                            </tr>

                              <tr align="right">
                            <td width="15%" align="left" nowrap>
                                <span class="elementLabel">Branch :</span><asp:DropDownList ID="ddlBranch" CssClass="dropdownList"
                                    AutoPostBack="true" runat="server" 
                                    TabIndex="1">
                                </asp:DropDownList>
                            </td>
                            <td align="right" style="width: 100%;" valign="top">
                                <span class="elementLabel">Search :</span> &nbsp;<asp:TextBox ID="txtSearch" runat="server"
                                    CssClass="textBox" MaxLength="40" Width="180px" TabIndex="5"></asp:TextBox>
                                &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="buttonDefault"
                                    ToolTip="Search" OnClick="btnSearch_Click" TabIndex="6" />
                            </td>
                        </tr>

                        <tr>
                            <td width="10%" nowrap align="left">
                               &nbsp;&nbsp; <span class="elementLabel">Year:</span>

                               <%-- <asp:TextBox ID="txtDocPrFx" runat="server" CssClass="textBox" Width="20px" ReadOnly="True"></asp:TextBox>
                                <asp:TextBox ID="txtDocumentNo" runat="server" CssClass="textBox" Width="60px" TabIndex="3"
                                    MaxLength="5"></asp:TextBox>
                                <asp:TextBox ID="txtBranchCode" runat="server" CssClass="textBox" Width="60px" TabIndex="3"
                                    MaxLength="5"></asp:TextBox>--%>

                                <asp:TextBox ID="txtYear" runat="server" CssClass="textBox" Width="50px" OnTextChanged="txtYear_TextChanged"
                                    AutoPostBack="true" TabIndex="4" MaxLength="4"></asp:TextBox>
                            </td>
                        </tr>

                             <tr>
                            <td align="left" style="width: 100%; height: 21px;" valign="top" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>

                            <tr>
                            <td colspan=2>
                            
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">


                        <tr id="rowGrid" runat="server">
                       <%-- <td>
                        &nbsp;
                        </td>--%>
                       
                       <td>
                     
     <asp:GridView ID="GridViewinwardclosure" runat="server" AutoGenerateColumns="false"
         Width="100%" GridLines="Both" AllowPaging="true" PageSize="20" OnRowDataBound="GridViewinwardclosure_RowDataBound"
         OnRowCommand="GridViewinwardclosure_RowCommand">
         <PagerSettings Visible="false" />
         <RowStyle Wrap="false" HorizontalAlign="Left" Height="18px" VerticalAlign="Top" CssClass="gridItem" />
         <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
         <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
             CssClass="gridAlternateItem" />
         <Columns>

         

             <asp:TemplateField HeaderText="IRM No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                 HeaderStyle-Width="15%" ItemStyle-Width="15%">
                 <ItemTemplate>
                     <asp:Label ID="lblirmno" runat="server" Text='<%# Eval("IRMNO") %>' CssClass="elementLabel"></asp:Label>
                 </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Left" Width="15%" />
                 <ItemStyle HorizontalAlign="Left" Width="15%" />
             </asp:TemplateField>

               <asp:TemplateField HeaderText="IE Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                 HeaderStyle-Width="15%" ItemStyle-Width="15%">
                 <ItemTemplate>
                     <asp:Label ID="lbliecode" runat="server" Text='<%# Eval("IECode") %>' CssClass="elementLabel"></asp:Label>
                 </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Left" Width="15%" />
                 <ItemStyle HorizontalAlign="Left" Width="15%" />
             </asp:TemplateField>

               <asp:TemplateField HeaderText="Document No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                 HeaderStyle-Width="15%" ItemStyle-Width="15%">
                 <ItemTemplate>
                     <asp:Label ID="lbldocno" runat="server" Text='<%# Eval("DocumentNo") %>' CssClass="elementLabel"></asp:Label>
                 </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Left" Width="15%" />
                 <ItemStyle HorizontalAlign="Left" Width="15%" />
             </asp:TemplateField>



               <asp:TemplateField HeaderText="Doc Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                 HeaderStyle-Width="10%" ItemStyle-Width="10%">
                 <ItemTemplate>
                     <asp:Label ID="lbldocdate" runat="server" Text='<%# Eval("DocDate","{0:dd/MM/yyyy}") %>'
                         CssClass="elementLabel"></asp:Label>
                 </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Center" Width="10%" />
                 <ItemStyle HorizontalAlign="Center" Width="10%" />
             </asp:TemplateField>
             
             

             <asp:TemplateField HeaderText="Adjustment Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                 HeaderStyle-Width="10%" ItemStyle-Width="10%">
                 <ItemTemplate>
                     <asp:Label ID="lbladjdate" runat="server" Text='<%# Eval("AdjsutmentDate","{0:dd/MM/yyyy}") %>' CssClass="elementLabel"></asp:Label>
                 </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Center" Width="10%" />
                 <ItemStyle HorizontalAlign="Center" Width="10%" />
             </asp:TemplateField>
             

             <asp:TemplateField HeaderText="Adjustment Amount" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                 HeaderStyle-Width="10%" ItemStyle-Width="10%">
                 <ItemTemplate>
                     <asp:Label ID="lbladjamt" runat="server" Text='<%# Eval("AmountAdjusted","{0:#0.00}") %>' CssClass="elementLabel"></asp:Label>
                 </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Center" Width="10%"/>
                 <ItemStyle HorizontalAlign="right" Width="10%"/>
             </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" HeaderText="Delete"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Button ID="btnDelete" runat="server" CommandArgument='<%# Eval("IRMNO") %>'
                                                    CommandName="RemoveRecord" Text="Delete" ToolTip="Delete Record" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                        </asp:TemplateField>


         </Columns>
     </asp:GridView>

                                                    
                       </td>
                        </tr>

                        <tr id="rowPager" runat="server">
                            <td align="center" style="width: 100%" valign="top" class="gridHeader">
                                <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td align="left" width="25%">
                                                &nbsp;Records Per Page :&nbsp;
                                                <asp:DropDownList ID="ddlrecordperpage" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlrecordperpage_SelectedIndexChanged">
                                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                    <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                    <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                                    <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" valign="top" width="50%">
                                                <asp:Button ID="btnnavfirst" runat="server" Text="First" ToolTip="First" OnClick="btnnavfirst_Click" />
                                                <asp:Button ID="btnnavpre" runat="server" Text="Prev" ToolTip="Previous" OnClick="btnnavpre_Click" />
                                                <asp:Button ID="btnnavnext" runat="server" Text="Next" ToolTip="Next" OnClick="btnnavnext_Click" />
                                                <asp:Button ID="btnnavlast" runat="server" Text="Last" ToolTip="Last" OnClick="btnnavlast_Click" />
                                            </td>
                                            <td align="right" style="width: 25%;">
                                                &nbsp;<asp:Label ID="lblpageno" runat="server"></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="lblrecordno" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>

                        <tr>
                        <td>
                        &nbsp;
                        </td>
                        </tr>

                        
                    </table>


                    </ContentTemplate>
                    </asp:UpdatePanel>

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
