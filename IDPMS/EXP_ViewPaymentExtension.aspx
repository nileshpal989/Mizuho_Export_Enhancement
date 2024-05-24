<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EXP_ViewPaymentExtension.aspx.cs" Inherits="IDPMS_EXP_ViewPaymentExtension" %>

<%@ Register Src="../Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">

     <script type="text/javascript" language="javascript">
         function validateSearch() {
             var _txtvalue = document.getElementById('txtSearch').value;
             _txtvalue = _txtvalue.replace(/'&lt;'/, "");
             _txtvalue = _txtvalue.replace(/'&gt;'/, "");
             if (_txtvalue.indexOf('<!') != -1 || _txtvalue.indexOf('>!') != -1 || _txtvalue.indexOf('!') != -1 || _txtvalue.indexOf('<') != -1 || _txtvalue.indexOf('>') != -1 || _txtvalue.indexOf('|') != -1) {
                 alert('!, |, <, and > are not allowed.');
                 document.getElementById('txtSearch').value = _txtvalue;
                 return false;
             }
             else
                 return true;
         }

         function submitForm(event) {
             if (event.keyCode == '13') {
                 if (validateSearch() == true)
                     __doPostBack('btnSearch', '');
                 else
                     return false;
             }
         }

         //         function checkDocNo() {
         //             var docNo = document.getElementById('txtDocumentNo');
         //             var _tempDocNo;
         //             var docYear = document.getElementById('txtYear');
         //             var docNoLen = docNo.value.length;

         //             if (docNoLen == 0) {
         //                 docNoLen = 1;
         //                 docNo.value = 1;
         //             }
         //             if (docNo.value == 0) {
         //                 docNoLen = 1;
         //                 docNo.value = 1;
         //             }

         //             _tempDocNo = docNo.value;

         //             if (docNoLen < 6) {
         //                 for (var i = docNoLen; i < 5; i++) {
         //                     _tempDocNo = 0 + _tempDocNo;
         //                 }
         //                 docNo.value = docYear.value.substring(3) + _tempDocNo;
         //             }

         //         }


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
                                <span class="pageLabel"><strong>Payment Extension View</strong></span>
                            </td>
                            
                              <td align="right" style="width: 50%">
                                <%--<asp:Button runat="server" Text="Add New" CssClass="buttonDefault" 
                                      ToolTip="Add New Record" TabIndex="3" ID="btnAdd" onclick="btnAdd_Click1" ></asp:Button>--%>
                                      <asp:Button ID="btnAdd" runat="server" Text="Add New" CssClass="buttonDefault" ToolTip="Add New Record"
                                    OnClick="btnAdd_Click" TabIndex="3" />
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
                                    AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
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
                       
                       <td align="left">
                     
     <asp:GridView ID="GridViewpaymentextn" runat="server" AutoGenerateColumns="false"
         Width="60%" GridLines="Both" AllowPaging="true" PageSize="20" OnRowDataBound="GridViewpaymentextn_RowDataBound">
         <PagerSettings Visible="false" />
         <RowStyle Wrap="false" HorizontalAlign="Left" Height="18px" VerticalAlign="Top" CssClass="gridItem" />
         <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
         <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
             CssClass="gridAlternateItem" />
         <Columns>

            <asp:TemplateField HeaderText="cust IECode" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                 HeaderStyle-Width="6%" ItemStyle-Width="6%" Visible="false">
                 <ItemTemplate>
                     <asp:Label ID="lblcustacno" runat="server" Text='<%# Eval("IECode") %>' CssClass="elementLabel"></asp:Label>
                 </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Left" Width="5%" />
                 <ItemStyle HorizontalAlign="Left" Width="5%" />
             </asp:TemplateField>

             <asp:TemplateField HeaderText="Port Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                 HeaderStyle-Width="1%" ItemStyle-Width="1%">
                 <ItemTemplate>
                     <asp:Label ID="lblprtcd" runat="server" Text='<%# Eval("portOfDischarge") %>' CssClass="elementLabel"></asp:Label>
                 </ItemTemplate>
                 <HeaderStyle HorizontalAlign="center" Width="2%" />
                 <ItemStyle HorizontalAlign="center" Width="2%" />
             </asp:TemplateField>

           
             
             <asp:TemplateField HeaderText="Bill Of Entry No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                 HeaderStyle-Width="3%" ItemStyle-Width="3%">
                 <ItemTemplate>
                     <asp:Label ID="lblbillno" runat="server" Text='<%# Eval("billOfEntryNumber") %>' CssClass="elementLabel"></asp:Label>
                 </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Left" Width="3%"/>
                 <ItemStyle HorizontalAlign="Left" Width="3%"/>
             </asp:TemplateField>

             <asp:TemplateField HeaderText="Bill Of Entry Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                 HeaderStyle-Width="5%" ItemStyle-Width="5%">
                 <ItemTemplate>
                     <asp:Label ID="lblbilldate" runat="server" Text='<%# Eval("billOfEntryDate","{0:dd/MM/yyyy}") %>' CssClass="elementLabel"></asp:Label>
                 </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Center" Width="3%" />
                 <ItemStyle HorizontalAlign="Center" Width="3%" />
             </asp:TemplateField>
             
                <asp:TemplateField HeaderText="Extension Date" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left"
                 HeaderStyle-Width="2%" ItemStyle-Width="2%">
                 <ItemTemplate>
                     <asp:Label ID="lblextndate" runat="server" Text='<%# Eval("extensionDate","{0:dd/MM/yyyy}") %>' CssClass="elementLabel"></asp:Label>
                     <%--<asp:TextBox runat="server" CssClass="textBox" Text='<%# Eval("ExtensionDate","{0:dd/MM/yyyy}") %>'  ID="txt_extndate" Width=100px TabIndex="5"/>--%>
       
                 </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Center" Width="5%" />
                 <ItemStyle HorizontalAlign="Center" Width="5%" />
             </asp:TemplateField>
             


               <asp:TemplateField HeaderText="Letter No." HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left"
                 HeaderStyle-Width="3%" ItemStyle-Width="3%">
                 <ItemTemplate>
                     <asp:Label ID="lblletterno" runat="server" Text='<%# Eval("letterNumber") %>' CssClass="elementLabel"></asp:Label>
                     <%--<asp:TextBox runat="server" CssClass="textBox" Text='<%# Eval("LetterNo") %>'  ID="txt_letterno" Width=135px TabIndex="3"/>--%>
                 </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Left" Width="3%" />
                 <ItemStyle HorizontalAlign="Left" Width="3%" />
             </asp:TemplateField>


              <asp:TemplateField HeaderText="Letter Date." HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left"
                 HeaderStyle-Width="4%" ItemStyle-Width="4%">
                 <ItemTemplate>
                     <asp:Label ID="lblletterdate" runat="server" Text='<%# Eval("letterDate","{0:dd/MM/yyyy}") %>' CssClass="elementLabel"></asp:Label>
                   <%--  <asp:TextBox runat="server" CssClass="textBox" Text='<%# Eval("LetterDate","{0:dd/MM/yyyy}") %>'  ID="txt_letterdate" Width=90px TabIndex="4"/>--%>

                    
                               <%-- <ajaxToolkit:MaskedEditValidator ID="Mev1" runat="server" ControlExtender="mdRemdate"
                                    ValidationGroup="dtVal" ControlToValidate="txtDateRealised" EmptyValueMessage="Enter Date Value"
                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="mev"></ajaxToolkit:MaskedEditValidator>--%>
                 </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Center" Width="4%" />
                 <ItemStyle HorizontalAlign="Center" Width="4%" />
             </asp:TemplateField>


             

         </Columns>
     </asp:GridView>

                                                    
                       </td>
                        </tr>

                        <tr id="rowPager" runat="server">
                            <td align="center" style="width: 50%" valign="top" class="gridHeader">
                                <table cellspacing="0" cellpadding="2" width="100%" border="0">
                                    <tbody>
                                        <tr>
                                            <td align="left" width="10%">
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
                                            <td align="left" valign="top" width="10%">
                                                <asp:Button ID="btnnavfirst" runat="server" Text="First" ToolTip="First" OnClick="btnnavfirst_Click" />
                                                <asp:Button ID="btnnavpre" runat="server" Text="Prev" ToolTip="Previous" OnClick="btnnavpre_Click" />
                                                <asp:Button ID="btnnavnext" runat="server" Text="Next" ToolTip="Next" OnClick="btnnavnext_Click" />
                                                <asp:Button ID="btnnavlast" runat="server" Text="Last" ToolTip="Last" OnClick="btnnavlast_Click" />
                                            </td>
                                            <td align="right" style="width: 10%;">
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
