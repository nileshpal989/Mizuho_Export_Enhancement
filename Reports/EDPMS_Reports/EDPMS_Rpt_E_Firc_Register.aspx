<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDPMS_Rpt_E_Firc_Register.aspx.cs" Inherits="Reports_EDPMS_Reports_EDPMS_Rpt_E_Firc_Register" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
      <%-- <script src="../../E_FIRC_Cust_Help.aspx" type="text/javascript"></script>--%>
     
    <script type="text/javascript" language="javascript">



        function custhelp() {
            popup = window.open('../../E_FIRC_Cust_Help.aspx', 'helpCustId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "helpCustId"
            return false;

        }
        function CustId(event) {

            var key = event.keyCode;
            if (key == 113 && key != 13) {
                document.getElementById('btnCustList').click();
            }
        }
        function sss() {
            var s = popup.document.getElementById('txtcell1').value;
            if (common == "helpCustId") {
                document.getElementById('txtcustID').value = s;
            }
        }




        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0! 
        var yyyy = today.getFullYear();
        if (dd < 10) { dd = '0' + dd }
        if (mm < 10) { mm = '0' + mm }
        function toDate() {

            if (document.getElementById('txtFromDate').value != "__/__/____") {

                var toDate;
                toDate = dd + '/' + mm + '/' + yyyy;
                document.getElementById('txtToDate').value = toDate;
            }
        }

        function validateSave() {

            var ddlBranch = document.getElementById('ddlBranch');
            if (ddlBranch.value == "---Select---") {
                alert('Select Branch Name.');
                ddlBranch.focus();
                return false;
            }

            var fromDate = document.getElementById('txtFromDate');
            if (fromDate.value == '') {
                alert('Select From Date.');
                fromDate.focus();
                return false;
            }
            var toDate = document.getElementById('txtToDate');
            if (toDate.value == '') {
                alert('Select To Date.');
                toDate.focus();
                return false;
            }

           
            var rdballr = document.getElementById('rdball1')
            var rdbselectedcust1 = document.getElementById('Rdbselectedcust')
            var custwise = document.getElementById('rdbCust')
               var rdbdoc = document.getElementById('RdbDocument')


               if(rdbdoc.checked==true){
               custid1=" ";
               }



            if (custwise.checked == true) {

                if (rdballr.checked == true) {

                    custid1 = " ";

                }

                else if (rdbselectedcust1.checked == true) {

                    custid1 = document.getElementById('txtcustID');
                    if (custid1.value == '') {
                        alert('Select customer ID.');
                        custid1.focus();
                        return false;

                    }
                    custid1 = document.getElementById('txtcustID').value
                }

            }
            if (ddlBranch.value != 0 && fromDate.value != '' && toDate.value != '') {

                var winname = window.open('View_EDPMS_Rpt_E_Firc_Register.aspx?frm=' + fromDate.value + '&to=' + toDate.value + '&custid=' + custid1 + '&branch=' + ddlBranch.value ,'', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1100,height=550');
            }

        }
        
    </script>
</head>
<body>
    <form id="form2" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
   <%-- <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script src="../../Scripts/InitEndRequest.js" type="text/javascript"></script>--%>
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
         <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom" colspan="2">
                                <span class="pageLabel"><strong>EDPMS E FIRC Register</strong> </span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                            </td>
                        </tr>
                      
                        <tr>
                            <td align="right" width="5%">
                                <span class="elementLabel">Branch :</span>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="dropdownList" TabIndex="1" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="elementLabel">From Date :</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                    Width="70px" TabIndex="2"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtFromDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="Button1" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtFromDate" PopupButtonID="Button1" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                                &nbsp;&nbsp; <span class="elementLabel">To Date :</span>
                                <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                    Width="70px" TabIndex="3"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtToDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="Button2" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtToDate" PopupButtonID="Button2" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                       
                            </td>
                            <td align="left" nowrap>
                                  <asp:RadioButton ID="rdbCust" runat="server" Text="IE Code Wise" 
                                            CssClass="elementLabel" Checked="true" AutoPostBack="true" 
                                      oncheckedchanged="rdbCust_CheckedChanged" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                             <asp:RadioButton ID="RdbDocument" runat="server" Text="Document Wise" AutoPostBack="true" 
                                            CssClass="elementLabel"  oncheckedchanged="RdbDocument_CheckedChanged" />
                            </td>
                            </tr>
                            <tr>
                            <td>
                                        <td align="left" nowrap>
                                                    <asp:Panel ID="Cust" runat="server" Visible=true>
                                        
                                        <asp:RadioButton ID="rdball1" runat=server Text="All IE Code" CssClass=elementLabel  Checked=true GroupName="data"
                                                            AutoPostBack="true" oncheckedchanged="rdball1_CheckedChanged"/>

                                                        &nbsp;<asp:RadioButton ID="Rdbselectedcust" runat=server Text="Selected IE Code" AutoPostBack="true" GroupName="Data"
                                                            CssClass=elementLabel oncheckedchanged="Rdbselectedcust_CheckedChanged"/>
                                                            </asp:Panel></td>
                                        </td>
                            </tr>
                            <tr ID="CustID" runat="server" visible="false">
                                <td>
                                </td>
                                <td align="left">
                                    <span class="elementLabel">IE Code : </span>
                                    <asp:TextBox ID="txtcustID" runat="server" CssClass="textBox" AutoPostBack="true" MaxLength="25"
                                        ontextchanged="txtcustID_TextChanged"></asp:TextBox>
                                    <asp:Button ID="btnCustList" runat="server" CssClass="btnHelp_enabled" 
                                        Visible="false" />
                                    &nbsp;
                                    <asp:Label ID="lblCustomerName" runat="server" CssClass="elementLabel" 
                                        Visible="false"></asp:Label>
                                </td>
                                </tr>
                            <tr>
                                <td>
                                </td>
                                <td align="left" height="40px">
                                    <asp:Button ID="btnGenerate" runat="server" CssClass="buttonDefault" 
                                        TabIndex="4" Text="Generate" ToolTip="Generate" />
                                          <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="false" />
                                </td>
                                <caption>
                                    &nbsp;</caption>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
</body>
</html>

