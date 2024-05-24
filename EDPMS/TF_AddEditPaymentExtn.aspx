<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_AddEditPaymentExtn.aspx.cs" Inherits="EXP_TF_AddEditPaymentExtn"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="../Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link id="Link1" runat="server" rel="shortcut icon" href="../Images/favicon.ico"
        type="image/x-icon" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link id="Link2" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link3" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script type="text/javascript" language=javascript>

        function calc(a, b, c) {

//            var letterno = document.getElementById('a').value;
//            var letterdate = document.getElementById('b').value;
            //            var extndate = document.getElementById('c').value;

            var letterno = document.getElementById(a).value;
            var lettrdate = document.getElementById(b).value;
            var extndate = document.getElementById(c).value;

            if (letterno == '') {

                alert('Letter No cant be blank.');
                document.getElementById(a).focus();
                //document.getElementById(a.value).focus();
                return false;

            }

//            if (lettrdate == '__/__/____' || lettrdate == '') {

//                alert('Letter Date cant be blank.');
//                document.getElementById(b).focus();
//                return false;

//            }

//            if (extndate == '__/__/____' || extndate == '') {

//                alert('Extension Date cant be blank.');
//                document.getElementById(c).focus();
//                return false;

//            }


        }


        function Ldate(a, b, c) {
         
            var lettrdate = document.getElementById(b).value;

            if (lettrdate == '__/__/____' || lettrdate == '') {

                alert('Letter Date cant be blank.');
                document.getElementById(b).focus();
                return false;

            }

        }

        function Edate(a) {

            var extndate = document.getElementById(a).value;

            if (extndate != '__/__/____' || extndate != '') {

               // alert('Extension Date cant be blank.');

                extndate = "__/__/____";
                document.getElementById(a).focus();
                //return false;

            }

        }


        function opencust(e) {

         var keycode;
            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {


                var branch = document.getElementById('txt_branch').value;
                var year = document.getElementById('txt_year').value;

                open_popup('../TF_CustomerLookUpPE.aspx?branch=' + branch + '&year=' + year, 450, 550, 'DocNoList');
                return false;

            }

       
        }


        function selectCustomer(acno,name) {

            document.getElementById('txt_custacno').value = acno;
            //document.getElementById('lbl_custname').innerHTML = name;
            // document.getElementById('txt_custacno').focus();
            __doPostBack("txt_custacno", "TextChanged");
        }
    
    </script>
</head>
<body onload="EndRequest();closeWindows();" onunload="closeWindows();">
    <form id="form1" runat="server" >

   <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>

    <script language="javascript" type="text/javascript" src="../Scripts/Enable_Disable_Opener.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>

    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="../Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
             

            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
           <%--  <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>--%>

                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">

                         <tr>
                            <td align="left" valign="bottom" width=10% colspan=2 nowrap>
                                <span class="pageLabel">Payment Extension Entry Details</span>
                            </td>

                            <td width=10%>
                            &nbsp;
                            </td>
                            <td width=10%>
                            &nbsp;
                            </td>

                            <td width=10%> 
                            &nbsp;
                            </td>
                            <td align="right" width=60% nowrap>
                            <asp:Label runat="server" ID="lblSupervisormsg" Text="Role : Supervisor - ONLY VIEW THE DATA  " 
                                    style="color:red" ></asp:Label>&nbsp;
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                    OnClick="btnBack_Click"  TabIndex=8/>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="6">
                                <hr />
                            </td>
                        </tr>

                        <tr>
                        <td nowrap align=right>
                            <span class="elementLabel">Branch:</span>    
                        </td>
                        <td align=left>
                            <asp:TextBox runat="server" CssClass="textBox" ID="txt_branch" Width=70px MaxLength=15 Enabled=false/>
                        </td>
                        
                       
                        </tr>

                        <tr>
                         <td align=right nowrap>
                            <span class="elementLabel">Year:</span>
                        </td>

                        <td align=left nowrap>
                            <asp:TextBox runat="server" ID="txt_year" Width=50px CssClass="textBox" MaxLength=4 Enabled=false/>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        </tr>
                        <tr>
                        <td nowrap align=right>
                         <span class="elementLabel">Customer AC No:</span>
                        </td>

                        <td nowrap align=left colspan=5>
                            <asp:TextBox runat="server" CssClass="textBox" Width=100px  TabIndex=1  
                                ID="txt_custacno"  onkeydown="return opencust(113);" MaxLength="20" 
                                ontextchanged="txt_custacno_TextChanged" AutoPostBack=true/>
                            <asp:Button ID="btncusthelp" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" OnClientClick="return opencust('mouseClick');"/>    
                            <asp:Label Text="" runat="server" CssClass="elementLabel" ID="lbl_custname"/>                   
                        </td>

                        </tr>

                        <tr>
                        <td>
                        &nbsp;
                        </td>
                        </tr>
                          <tr>
                        <td>
                            <asp:Label Text="" ID="lblmessage" runat="server" CssClass="mandatoryField"/>
                        </td>
                        </tr>

                        <tr>
                       <%-- <td>
                        &nbsp;
                        </td>--%>
                       
                       <td colspan=6>
                     
     <asp:GridView ID="GridViewpaymentextn" runat="server" AutoGenerateColumns="false"  
         Width="100%" GridLines="Both" AllowPaging="true" PageSize="20" OnRowDataBound="GridViewpaymentextn_RowDataBound"
         OnRowCommand="GridViewpaymentextn_RowCommand">
         <PagerSettings Visible="false" />
         <RowStyle Wrap="false" HorizontalAlign="Left" Height="18px" VerticalAlign="Top" CssClass="gridItem" />
         <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
         <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
             CssClass="gridAlternateItem" />
         <Columns>

             <asp:TemplateField HeaderText="Document No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                 HeaderStyle-Width="15%" ItemStyle-Width="15%">
                 <ItemTemplate>
                     <asp:Label ID="lbldocno" runat="server" Text='<%# Eval("Document_No") %>' CssClass="elementLabel"></asp:Label>
                 </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Left" Width="15%" />
                 <ItemStyle HorizontalAlign="Left" Width="15%" />
             </asp:TemplateField>

               <asp:TemplateField HeaderText="Doc. Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                 HeaderStyle-Width="10%" ItemStyle-Width="10%">
                 <ItemTemplate>
                     <asp:Label ID="lbldocdate" runat="server" Text='<%# Eval("docdate","{0:dd/MM/yyyy}") %>'
                         CssClass="elementLabel"></asp:Label>
                 </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Center" Width="10%" />
                 <ItemStyle HorizontalAlign="Center" Width="10%" />
             </asp:TemplateField>
             
             <asp:TemplateField HeaderText="Shipping Bill No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                 HeaderStyle-Width="10%" ItemStyle-Width="10%">
                 <ItemTemplate>
                     <asp:Label ID="lblshipbillno" runat="server" Text='<%# Eval("Shipping_Bill_No") %>' CssClass="elementLabel"></asp:Label>
                 </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Left" Width="18%" />
                 <ItemStyle HorizontalAlign="Left" Width="18%" />
             </asp:TemplateField>

             <asp:TemplateField HeaderText="Ship Bill Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                 HeaderStyle-Width="10%" ItemStyle-Width="10%">
                 <ItemTemplate>
                     <asp:Label ID="lblshipbilldate" runat="server" Text='<%# Eval("Shipping_Bill_Date","{0:dd/MM/yyyy}") %>' CssClass="elementLabel"></asp:Label>
                 </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Center" Width="10%" />
                 <ItemStyle HorizontalAlign="Center" Width="10%" />
             </asp:TemplateField>


             <asp:TemplateField HeaderText="AD Bank" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left"
                 HeaderStyle-Width="3%" ItemStyle-Width="3%">
                 <ItemTemplate>
                     <%--<asp:Label ID="lblDispInd" runat="server" Text='<%# Eval("") %>' CssClass="elementLabel"></asp:Label>--%>
                    <%-- <asp:TextBox runat="server" CssClass="textBox" Text='<%# Eval("ADBank") %>'  ID="txt_adbank" Width=30px TabIndex="2"/>--%>
                     <asp:DropDownList runat="server" ID="ddl_adbank" CssClass="dropdownList">
                         <asp:ListItem Text="AD Bank" Value="1" />
                         <asp:ListItem Text="RBI Extn" Value="2" />
                         <asp:ListItem Text="No Extn" Value="0" />
                     </asp:DropDownList>
                 </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Center" Width="8%" />
                 <ItemStyle HorizontalAlign="Center" Width="8%" />
             </asp:TemplateField>


               <asp:TemplateField HeaderText="Letter No." HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left"
                 HeaderStyle-Width="8%" ItemStyle-Width="8%">
                 <ItemTemplate>
                     <%--<asp:Label ID="lblDispInd" runat="server" Text='<%# Eval("") %>' CssClass="elementLabel"></asp:Label>--%>
                     <asp:TextBox runat="server" CssClass="textBox" Text='<%# Eval("LetterNo") %>'  ID="txt_letterno" Width=135px MaxLength=20/>
                 </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Left" Width="15%" />
                 <ItemStyle HorizontalAlign="Left" Width="15%" />
             </asp:TemplateField>

             

              <asp:TemplateField HeaderText="Letter Date." HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left"
                 HeaderStyle-Width="5%" ItemStyle-Width="5%">
                 <ItemTemplate>
                     <%--<asp:Label ID="lblDispInd" runat="server" Text='<%# Eval("") %>' CssClass="elementLabel"></asp:Label>--%>
                   <%--  <asp:TextBox runat="server" CssClass="textBox" Text='<%# Eval("LetterDate","{0:dd/MM/yyyy}") %>'  ID="txt_letterdate" Width=90px TabIndex="4"/>--%>

                      <asp:TextBox ID="txt_letterdate" runat="server" CssClass="textBox"  Text='<%# Eval("LetterDate","{0:dd/MM/yyyy}") %>'
                                    Width="90px"  MaxLength="10" onfocus="this.select()"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="mdRemdate" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txt_letterdate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="btncalendar_DocDate" runat="server" CssClass="btncalendar_enabled"
                                    TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="calendarFromDate1" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txt_letterdate" PopupButtonID="btncalendar_DocDate" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                               <%-- <ajaxToolkit:MaskedEditValidator ID="Mev1" runat="server" ControlExtender="mdRemdate"
                                    ValidationGroup="dtVal" ControlToValidate="txtDateRealised" EmptyValueMessage="Enter Date Value"
                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="mev"></ajaxToolkit:MaskedEditValidator>--%>
                 </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Center" Width="12%" />
                 <ItemStyle HorizontalAlign="Center" Width="12%" />
             </asp:TemplateField>


              <asp:TemplateField HeaderText="Extension Date" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left"
                 HeaderStyle-Width="15%" ItemStyle-Width="15%">
                 <ItemTemplate>
                     <%--<asp:Label ID="lblDispInd" runat="server" Text='<%# Eval("") %>' CssClass="elementLabel"></asp:Label>--%>
                     <%--<asp:TextBox runat="server" CssClass="textBox" Text='<%# Eval("ExtensionDate","{0:dd/MM/yyyy}") %>'  ID="txt_extndate" Width=100px TabIndex="5"/>--%>

                      <asp:TextBox ID="txt_extndate" runat="server" CssClass="textBox"  Text='<%# Eval("ExtensionDate","{0:dd/MM/yyyy}") %>'
                                    Width="90px" MaxLength="10" onfocus="this.select()"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="mdRemdate1" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txt_extndate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="btncalendar_DocDate1" runat="server" CssClass="btncalendar_enabled"
                                    TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="calendarFromDate2" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txt_extndate" PopupButtonID="btncalendar_DocDate1" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                                
                 </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Center" Width="15%" />
                 <ItemStyle HorizontalAlign="Center" Width="15%" />
             </asp:TemplateField>

           <%--  <asp:TemplateField HeaderText="Save" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="10%" ItemStyle-Width="10%">
             
             <ItemTemplate>
             
                  <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                            ToolTip="Save" OnClick="btnSave_Click"" />
             </ItemTemplate>

             <HeaderStyle HorizontalAlign="Center" Width="10%"/>
             <ItemStyle HorizontalAlign="Center" Width="10%" />
             </asp:TemplateField>--%>


               <asp:TemplateField HeaderText="Add" HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="Left"
                 HeaderStyle-Width="8%" ItemStyle-Width="8%">
                 <ItemTemplate>
                     <%--<asp:Label ID="lblDispInd" runat="server" Text='<%# Eval("") %>' CssClass="elementLabel"></asp:Label>--%>
                   <%-- <asp:CheckBox runat="server" ID="RowChkAllow1" AutoPostBack="true" OnCheckedChanged="RowChkAllow1_CheckedChanged" />--%>
                    <asp:Button ID="btnadd" runat="server" Text="Add" CssClass="buttonDefault"  CommandArgument='<%# Eval("Document_No")+","+ Eval("docdate")+","+Eval("Shipping_Bill_No")+","+Eval("Shipping_Bill_Date","{0:dd/MM/yyyy}")+","+Eval("LetterNo")+","+Eval("LetterDate")+","+Eval("ExtensionDate","{0:dd/MM/yyyy}")%>'
                            ToolTip="Add"/>
                 </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Center" Width="5%" />
                 <ItemStyle HorizontalAlign="Center" Width="5%" />
             </asp:TemplateField>
             

         </Columns>
     </asp:GridView>

                                                    
                       </td>
                        </tr>

                        <tr id="rowPager" runat="server" width=100% visible=false class="gridHeader">
                           
                                            <td align="left" colspan=4>
                                                &nbsp;<asp:Label Text="Records Per Page :" runat="server" CssClass="elementLabel"/>&nbsp;
                                                <asp:DropDownList ID="ddlrecordperpage" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlrecordperpage_SelectedIndexChanged">
                                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                    <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                    <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                                    <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left" valign="top" width="50%">
                                                <asp:Button ID="btnnavfirst" runat="server" Text="First" ToolTip="First" OnClick="btnnavfirst_Click" CssClass="elementLabel" />
                                                <asp:Button ID="btnnavpre" runat="server" Text="Prev" ToolTip="Previous" OnClick="btnnavpre_Click" CssClass="elementLabel" />
                                                <asp:Button ID="btnnavnext" runat="server" Text="Next" ToolTip="Next" OnClick="btnnavnext_Click" CssClass="elementLabel"/>
                                                <asp:Button ID="btnnavlast" runat="server" Text="Last" ToolTip="Last" OnClick="btnnavlast_Click" CssClass="elementLabel"/>
                                            </td>
                                            <td align="right" width= "25%">
                                                <asp:Label ID="lblpageno" CssClass="elementLabel" runat="server" Visible=true></asp:Label>
                                                &nbsp;
                                                <asp:Label ID="lblrecordno" runat="server" CssClass="elementLabel"></asp:Label>
                                            </td>
                                       
                        </tr>

                         

                      
                    </table>

                    

                    <table border="0" cellpadding="0" cellspacing="0">
                       
                      <tr>
                        <td>
                        &nbsp;
                        </td>
                        </tr>

                        <tr>
                        <td>
                         <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                            ToolTip="Save" OnClick="btnSave_Click" TabIndex="6" Visible=false/>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                            ToolTip="Cancel" OnClick="btnCancel_Click" TabIndex="7" Visible=false />    
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
