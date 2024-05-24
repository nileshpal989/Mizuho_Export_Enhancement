<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IRM_AdustmentClosure.aspx.cs" Inherits="EDPMS_IRM_AdustmentClosure" %>

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

//         function amt() {

//             var adjamt = document.getElementById('txt_adjamt').value;
//             if (adjamt!='') {
//                 adjamt.value = parseFloat(adjamt).toFixed(2).toString("0.00");

//             }
//             else {

//                 alert('Adjustment Amount cant be blank');
//                 document.getElementById('txt_adjamt').focus();
//                 return false;
//             }
         
         
       //  }

         function OpenTTNoList() {

             var irmno = document.getElementById('txt_irmno');
             var year = document.getElementById('hdnyr');
           

             open_popup('IRMNoHelp.aspx?custAcNo=' + irmno.value + '&year=' + year.value, 300, 500, 'TTRefNo');
             return false;

         }


         function saveTTRefDetails(TTRefNo) {

             document.getElementById('txt_irmno').value = TTRefNo;
             __doPostBack("txt_irmno", "TextChanged");
             
         }


         function adj() {


             open_popup('R4AHelp.aspx', 300, 500, 'TTRefNo');
             return false;

         }


         function selectadj(desc) {

             document.getElementById('txt_adjreason').value = desc;
            
         }



         function validate() {

             var irmno = document.getElementById('txt_irmno').value;
             var adcode = document.getElementById('txt_adcode').value;
             var iecode = document.getElementById('txt_iecode').value;
             var curr = document.getElementById('ddlcurr').value;
             var adjamt = document.getElementById('txt_adjamt').value;
             var adjdate = document.getElementById('txt_adjdate').value;
             var adjreason = document.getElementById('txt_adjreason').value;
             var approved = document.getElementById('ddlapproved').value;
             var docno = document.getElementById('txt_docno').value;
             var docdate = document.getElementById('txt_docdate').value;
             var remclose = document.getElementById('ddlremclose').value;

             if (irmno=='') {

                 alert('IRM No cant be blank');
                 document.getElementById('txt_irmno').focus();
                 return false;
             }

             if (adcode == '') {

                 alert('Ad Code cant be blank');
                 document.getElementById('txt_adcode').focus();
                 return false;
             }


             if (iecode == '') {

                 alert('IE Code cant be blank');
                 document.getElementById('txt_iecode').focus();
                 return false;
             }

             if (curr == '' || curr == 'Select') {

                 alert('Select Currency');
                 document.getElementById('ddlcurr').focus();
                 return false;
             }

             if (adjamt == '') {

                 alert('Adjustment Amount cant be blank');
                 document.getElementById('txt_adjamt').focus();
                 return false;
             }

             if (adjdate == '' || adjdate=='__/__/____') {

                 alert('Adjustment Date cant be blank');
                 document.getElementById('txt_adjdate').focus();
                 return false;
             }

             if (adjreason == '') {

                 alert('Adjustment reason cant be blank');
                 document.getElementById('txt_adjreason').focus();
                 return false;
             }

             if (approved == '') {

                 alert('Approved By cant be blank');
                 document.getElementById('ddlapproved').focus();
                 return false;
             }

             if (docno == '') {

                 alert('Document No cant be blank');
                 document.getElementById('txt_docno').focus();
                 return false;
             }

             if (docdate == '' || docdate=='__/__/____') {

                 alert('Document Date cant be blank');
                 document.getElementById('txt_docdate').focus();
                 return false;
             }

             if (remclose=='') {

                 alert('Remittance Indicator cant be blank');
                 document.getElementById('ddlremclose').focus();
                 return false;
             }

            
         
         }


     </script>
</head>
<body>
    <form id="form1" runat="server">
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
            <%-- <asp:ScriptManager ID="ScriptManagerMain" runat="server">
                 </asp:ScriptManager>--%>

                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">

                         <tr>
                            <td align="left" valign="bottom" width=10% nowrap>
                                <span class="pageLabel" >Inward Remittance Closure</span>
                              
                            </td>

                            <td width=10% nowrap>
                             <input type="hidden" id="hdnbranch" runat="server" />
                              <input type="hidden" id="hdnyr" runat=server />
                            </td>

                             <td width=10% nowrap>
                            &nbsp;
                            </td>

                             <td width=10% nowrap>
                            &nbsp;
                            </td>

                             <td width=10% nowrap>
                            &nbsp;
                            </td>

                             <td align="right" nowrap>
                            <asp:Label runat="server" ID="lblSupervisormsg" Text="Role : Supervisor - ONLY VIEW THE DATA  " 
                                    style="color:red" ></asp:Label>&nbsp;
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" 
                                     ToolTip="Back" onclick="btnBack_Click" />
                            </td>

                            </tr>

                            <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="6">
                                <hr />
                            </td>
                        </tr>

                        <tr>
                        <td align=right>
                         <span class="mandatoryField">*</span><span class="elementLabel">IRM No:</span>
                        </td>
                         <td align=left nowrap>
                          <asp:TextBox runat="server" CssClass="textBox" ID="txt_irmno" Width=100px 
                                 MaxLength=50 ontextchanged="txt_irmno_TextChanged" AutoPostBack=true/>
                           <asp:Button ID="btncusthelp" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />   
                        </td>

                        <td align=right>
                          <span class="mandatoryField">*</span><span class="elementLabel">Ad Code:</span>
                        </td>
                        <td align=left>
                        <asp:TextBox runat="server" CssClass="textBox" ID="txt_adcode" Width=100px MaxLength=7 />
                        </td>
                        
                   <td align=right>
                         <span class="mandatoryField">*</span> <span class="elementLabel">IE Code:</span>
                        </td>
                        <td align=left>
                        <asp:TextBox runat="server" CssClass="textBox" ID="txt_iecode" Width=100px MaxLength=10/>
                        </td>

                        </tr>

                        <tr>

                          <td align=right>
                          <span class="mandatoryField">*</span><span class="elementLabel">Currency:</span>
                        </td>
                        <td align=left>
                            <asp:DropDownList runat="server" CssClass="dropdownList" ID="ddlcurr" Width="70px">   
                            </asp:DropDownList>
                        </td>

                         <td align=right nowrap>
                          <span class="mandatoryField">*</span><span class="elementLabel">Adjustment Amount:</span>
                        </td>
                         <td align=left nowrap>
                          <asp:TextBox runat="server" CssClass="textBox" ID="txt_adjamt" Width=100px style="text-align:right" />
                        
                        </td>

                          <td align=right nowrap>
                          <span class="mandatoryField">*</span><span class="elementLabel">Adjustment Date:</span>
                        </td>
                         <td align=left nowrap>
                            <asp:TextBox ID="txt_adjdate" runat="server" CssClass="textBox" 
                                    Width="80px" MaxLength="10" onfocus="this.select()"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="mdRemdate1" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txt_adjdate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="btncalendar_DocDate1" runat="server" CssClass="btncalendar_enabled"
                                    TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="calendarFromDate2" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txt_adjdate" PopupButtonID="btncalendar_DocDate1" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                        
                        </td>

                      

                        </tr>

                        <tr>
                        <td align=right> 
                          <span class="mandatoryField">*</span><span class="elementLabel">Reason For Adjustment:</span>
                        </td>

                        <td align=left colspan=5> 
                         <asp:TextBox runat="server" CssClass="textBox" ID="txt_adjreason" Width=400px MaxLength=200 />
                         <asp:Button ID="btn_reasonhelp" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" /> 
                        </td>
                        </tr>
                        <tr>
                            <td align=right>
                         <span class="elementLabel">Letter No:</span>
                        </td>
                        <td align=left>
                            <asp:TextBox runat="server" ID="txt_letterNo" CssClass="textBox" Width=100px MaxLength=50/>  
                        </td>

                       

                       <td align=right nowrap>
                         <span class="mandatoryField">*</span> <span class="elementLabel">Approved By:</span>
                        </td>
                        <td align=left>
                            <asp:DropDownList runat="server" ID="ddlapproved" CssClass="dropdownList" Width=70px>
                                <asp:ListItem Text="RBI" Value="1" />
                                <asp:ListItem Text="AD BANK" Value="2" />
                                <asp:ListItem Text="OTHERS" Value="3" />
                            </asp:DropDownList>
                        </td>

                        </tr>

                        <tr>
                        <td align=right>
                         <span class="mandatoryField">*</span> <span class="elementLabel">Document No:</span>
                        </td>

                        <td align=left>
                         <asp:TextBox runat="server" ID="txt_docno" CssClass="textBox" Width=100px MaxLength=20/>  
                        </td>

                        <td align=right>
                         <span class="mandatoryField">*</span> <span class="elementLabel">Document Date:</span>
                        </td>

                        <td align=left nowrap>
                        <asp:TextBox ID="txt_docdate" runat="server" CssClass="textBox"  
                                    Width="80px" MaxLength="10" onfocus="this.select()"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="mdRemdate2" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txt_docdate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="btncalendar_DocDate2" runat="server" CssClass="btncalendar_enabled"
                                    TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="calendarFromDate3" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txt_docdate" PopupButtonID="btncalendar_DocDate2" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                        </td>
                        </tr>

                        <tr>
                        <td align=right>
                       <span class="elementLabel">Port Of Receiving:</span>
                        </td>
                        <td align=left>
                          <asp:TextBox runat="server" ID="txt_recport" CssClass="textBox" Width=100px MaxLength=20/>  
                        </td>

                        <td align=right nowrap>
                            <span class="mandatoryField">*</span><span class="elementLabel">Closure Sequence No:</span>
                        </td>

                        <td align=left>
                         <asp:TextBox runat="server" ID="txt_closseqno" CssClass="textBox" Width=100px 
                                MaxLength=10 AutoPostBack=true ontextchanged="txt_closseqno_TextChanged"/>  
                        </td>

                        <td align=right nowrap>
                           <span class="mandatoryField">*</span><span class="elementLabel">Remittance Indicator:</span>
                        </td>

                        <td align=left>
                          <asp:DropDownList runat="server" ID="ddlremclose" CssClass="dropdownList" Width=70px>
                                <asp:ListItem Text="OPEN" Value="1" />
                                <asp:ListItem Text="CLOSE" Value="2" />
                             
                            </asp:DropDownList>
                        </td>

                        </tr>

                        <tr>
                        <td align=right>
                           <span class="elementLabel">Remarks:</span>
                        </td>

                        <td colspan=5 align=left>
                         <asp:TextBox runat="server" CssClass="textBox" ID="txt_remarks" Width=400px MaxLength=200 />
                        </td>
                        </tr>

                        <tr>
                        <td>
                        &nbsp;
                        </td>
                        </tr>

                        <tr>

                        <td>
                        &nbsp;
                        </td>

                        <td>
                        &nbsp;
                        </td>

                        <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                            ToolTip="Save" onclick="btnSave_Click"  />
                        
                        </td>

                        <td>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                            ToolTip="Cancel" onclick="btnCancel_Click"  />
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
