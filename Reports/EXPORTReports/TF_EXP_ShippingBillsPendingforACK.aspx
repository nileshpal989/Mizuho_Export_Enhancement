<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_EXP_ShippingBillsPendingforACK.aspx.cs" Inherits="Reports_EXPORTReports_TF_EXP_ShippingBillsPendingforACK" %>

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
    <script type="text/javascript">
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
    </script>
    <script language="javascript" type="text/javascript">

        function custhelp() {
            var fromDate = document.getElementById('txtToDate');
            if (fromDate.value == '') {
                alert('Select as on Date.');
                fromDate.focus();
                return false;
            }
            
            var adcode = document.getElementById('ddlBranch').value;
            popup = window.open('../../TF_CustomerLookUp1.aspx', 'helpCustId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
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
                document.getElementById('txtCustomerID').value = s;
            }
        }


        function selectCustomer(selectedID) {
            var id = selectedID;
            document.getElementById('hdnCustId').value = id;
            document.getElementById('txtCustomerID').value = selectedID;
            document.getElementById('txtCustomerID').focus();
            document.getElementById('btnCustId').click();
        }

        function validateSave() {

            var Branch = document.getElementById('ddlBranch').value;
            if (Branch == 0) {
                alert('Select Branch Name.');
                Branch.focus();
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

            var from = document.getElementById('txtFromDate').value;
            var to = document.getElementById('txtToDate').value;
            var rptType;
            var rptCode;

            if (document.getElementById('rdbSelectedCustomer').checked == true) {
                var txt = document.getElementById('txtCustomerID');
                if (txt.value == '') {
                    alert('Enter Customer A/c.');
                    txt.focus();
                    return false;
                }
            }
        }
        
    </script>
</head>
<body>
    <form id="form2" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
   
    <%--<script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>--%>
    <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>

    <%--<asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="../Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
            <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
            </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                  
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel"><strong>Shipping Bills Pending for Ack</strong></span>
                            </td>
                            <td align="right" style="width: 50%">
                                &nbsp;
                                <input type="hidden" id="hdnCustId" runat="server" />
                                <asp:Button ID="btnCustId" Style="display: none;" runat="server" OnClick="btnCustId_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100%" valign="top" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                    <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="2">
                                <table cellspacing="0" cellpadding="0" border="0" width="800px" style="line-height: 150%">
                                   <tr>
                                        <td width = "10%" align="right" nowrap>
                                        <span class="mandatoryField">* </span><span class="elementLabel">Branch :</span>
                                        </td>
                                        <td align="left" nowrap >
                                          &nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1"
                                          AutoPostBack="true" Width="100px"
                                          runat="server">
                                                </asp:DropDownList>

                                        </td>
                                     </tr>
                                    <tr>
                                        <td align="right" style="width: 100px">
     
                                            <span class="mandatoryField">*</span><span class="elementLabel">As on Date :</span>
                                        </td>
                                        <td>
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10"
                                                Width="70" TabIndex="3"></asp:TextBox>
                                            <asp:Button ID="btncalendar_ToDate" runat="server" CssClass="btncalendar_enabled" />
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                                runat="server" TargetControlID="txtToDate" InputDirection="RightToLeft" AcceptNegative="Left"
                                                ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtToDate" PopupButtonID="btncalendar_ToDate">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                ValidationGroup="dtVal" ControlToValidate="txtToDate" EmptyValueMessage="Enter Date Value"
                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                            </ajaxToolkit:MaskedEditValidator>
                                        </td>
                                    </tr>
                                
                              
                                                                      <tr>
                                    <td>
                                    </td>
                                     <td>
                                            <asp:RadioButton ID="rdbCustomerWise" runat="server" AutoPostBack="true" 
                                                CssClass="elementLabel" GroupName="Data1"  
                                                OnCheckedChanged="rdbCustomerWise_CheckedChanged" TabIndex="5" 
                                                Text="Customer Wise"/>

                                        </td>
                                    
                                        
                                    </tr>   
                                                                      <tr>
                                    <td>
                                    </td>
                                     <td>
                                            <asp:RadioButton ID="rdbAllCustomerWise" runat="server" AutoPostBack="true" 
                                                CssClass="elementLabel" GroupName="Data1" Visible="false" 
                                                OnCheckedChanged="rdbAllCustomerWise_CheckedChanged" TabIndex="5" 
                                                Text="All Customer Wise"/>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rdbSelectedCustomer" runat="server" AutoPostBack="true" 
                                                CssClass="elementLabel" GroupName="Data1"  Visible="false"
                                                OnCheckedChanged="rdbSelectedCustomer_CheckedChanged" TabIndex="5" 
                                                Text="Selected Customer Wise" />
                                        </td>                                    
                                    </tr>   
                                    </table>

                                    <fieldset ID="Custlist" runat="server" style="width: 800px"  visible="false" >
                                    <legend>Select Customer Name</legend>
                                    <table ID="Table2" runat="server">
                                        <tr>
                                            <td align="right" style="font-weight: bold; color: #000000; font-size: medium;" width="200px">
                                                <span style="color: Red">*</span>Customer A/C No :&nbsp;
                                            </td>
                                            <td align="left" width = "0px" style="font-weight: bold; color: #000000;">
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:TextBox ID="txtCustomerID" runat="server" CssClass="textBox" 
                                                    MaxLength="40"  TabIndex="6" Visible="false" 
                                                    Width="100px" ontextchanged="txtCustomerID_TextChanged1" 
                                                    AutoPostBack="True"></asp:TextBox>
                                                &nbsp;
                                                <asp:Button ID="btnCustList" runat="server" CssClass="btnHelp_enabled" 
                                                    Visible="false" />
                                                &nbsp;
                                                <asp:Label ID="lblCustomerName" runat="server" CssClass="elementLabel" 
                                                    Visible="false" Width="400px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                                <table>
                                    <tr valign="bottom">
                                        <td align="right" style="width: 120px">
                                        </td>
                                        <td align="left" style="width: 700px; padding-top: 10px; padding-bottom: 10px" valign="bottom">
                                            &nbsp;
                                            <asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" Text="Generate"
                                                ToolTip="Generate" TabIndex="7" onclick="btnSave_Click" />
                                            
                                            <asp:Button ID="Button2" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                            <asp:Button ID="Button3" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                        </td>
                                        <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                            <asp:Button ID="Button1" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                    </tr>
                        </tr>
                    </table>
                    <input type="hidden" runat="server" id="hdnFromDate" />
                    <input type="hidden" runat="server" id="hdnToDate" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
</body>
</html>
