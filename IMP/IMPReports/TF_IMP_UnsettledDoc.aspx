<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_UnsettledDoc.aspx.cs" Inherits="IMP_IMPReports_TF_IMP_UnsettledDoc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Src="~/Menu/Menu.ascx" TagName="menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript">

        function OpenCustomerCodeList(e) {
            var keycode;
            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {
                open_popup('../HelpForms/TF_IMP_CustomerHelp1.aspx', 400, 400, 'CustomerCodeList');
                return false;
            }
        }

        function selectCustomer(selectedID, name) {
            var txtCustomer_ID = document.getElementById('txtCustomer_ID');
            document.getElementById('lblCustomerDesc').innerText = name;
            txtCustomer_ID.value = selectedID;
            //            __doPostBack('txtCustomer_ID', '');
            txtCustomer_ID.focus();
        }

        //help.....................................



        function generateReport() {

            var Branchcode = document.getElementById('ddlBranch');
           // var CustAC="";
           // var txtCustomer_ID = document.getElementById('txtCustomer_ID');

////            if (document.getElementById('rdbAllCustomer').checked == true)
////             {
////                 CustAC = "All";
////             }

////           else if (document.getElementById('rdbSelectedCustomer').checked == true) {
////               if (txtCustomer_ID.value == '')
////                 {
////                    alert('Select Customer Ac no.');
////                    txtCustomer_ID.focus();
////                    return false;
////                }
////                else
////                 {
////                     CustAC = txtCustomer_ID.value;
////                 }
////             }

             var Type = "";

             if (document.getElementById('rdbunaccepted').checked == true)
              {
                  Type = "A";
              }
              else if (document.getElementById('rdbUnsettled').checked == true)
             {
                 Type = "S";
             }

            var doctype = "";
            var txtdate = document.getElementById('txtdate');

            if (document.getElementById('rbdall').checked == true) {

                doctype = 'All';

            }
           else if (document.getElementById('rbdCollection').checked == true) {

                doctype = 'ICA';

            }
            else if (document.getElementById('rbdLodgment').checked == true) {


                doctype = 'IBA';

            }
            else if (document.getElementById('rdbcollctionslight').checked == true) {


                doctype = 'ICU';

            }
            else if (document.getElementById('rbdloanusane').checked == true) {


                doctype = 'ACC';

            }

            if (txtdate.value == "")
             {
                alert('Please select  Date')
                txtdate.focus();
                return false;
            }
            var winame = window.open('TF_IMP_ViewUnsettledDoc.aspx?Branchcode=' + Branchcode.value + '&Documentype=' + doctype + '&Date=' + txtdate.value + '&Type=' + Type, '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1400,height=600');
                winame.focus();
                return false;

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
         .style2
         {
             width: 168px;
             height: 24px;
         }
         .style3
         {
             height: 24px;
         }
         .style6
         {
             width: 168px;
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
            <uc1:menu ID="Menu1" runat="server" />
            <br />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel"><strong>Unaccepted and Unsettled Import Document</strong></span>
                                <%--<asp:Label ID="PageHeader" CssClass="pageLabel" runat="server"></asp:Label>--%>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 50%" valign="top">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100%" valign="top">
                                <asp:Label ID="lblmessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top">
                                <table cellspacing="0" cellpadding="0" border="0" width="1300px" style="line-height: 150%">
                                    <tr>
                                        <td align="Center" class="style6">
                                            <span class="mandatoryField">&nbsp; *</span><span class="elementLabel">
                                            As ON Maturity Date :</span>
                                        </td>
                                        <td align="Left" style="width: 800px">
                                            <asp:TextBox ID="txtdate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtval"
                                                Width="70" TabIndex="2"></asp:TextBox>
                                            <asp:Button ID="btncalendar_FromDate" runat="server" CssClass="btncalendar_enabled"
                                                TabIndex="-1" />
                                            <ajaxToolkit:MaskedEditExtender ID="mdfdate" Mask="99/99/9999" MaskType="Date" runat="server"
                                                TargetControlID="txtdate" InputDirection="RightToLeft" AcceptNegative="Left"
                                                ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtdate" PopupButtonID="btncalendar_FromDate">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdfdate"
                                                ValidationGroup="dtVal" ControlToValidate="txtdate" EmptyValueMessage="Enter Date Value"
                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                            </ajaxToolkit:MaskedEditValidator> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" nowrap class="style2">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Branch :</span>
                                        </td>
                                        <td align="left" nowrap class="style3">
                                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" AutoPostBack="true"
                                                Width="100px" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;
                                        </td>
                                        <td align="left" nowrap>
                        <asp:RadioButton ID="rdbunaccepted" runat="server" CssClass="elementLabel" GroupName="rd1"
                                            TabIndex="4" Text="UNACCEPTED" Checked="true" AutoPostBack="true"/><asp:RadioButton
                                                ID="rdbUnsettled" runat="server" CssClass="elementLabel" GroupName="rd1"
                                                TabIndex="4" AutoPostBack="true" Text="UNSETTLED" Visible="false"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="style6">
                                            <span class="mandatoryField">&nbsp; *</span><span class="elementLabel">
                                            Document Type:</span>
                                        </td>
                                        <td align="left" nowrap>
                                        <asp:RadioButton ID="rbdall" runat="server" 
                                                  CssClass="elementLabel" GroupName="rdbType" Checked="True" 
                                                Text="ALL" AutoPostBack="True" />
                                            &nbsp; <asp:RadioButton ID="rbdCollection" runat="server" GroupName="rdbType"
                                                CssClass="elementLabel" Text="ICA" AutoPostBack="True"/>
                                            &nbsp;<asp:RadioButton ID="rbdLodgment" runat="server" GroupName="rdbType" CssClass="elementLabel"
                                                Text="IBA" AutoPostBack="True"/>
                                                 &nbsp;<asp:RadioButton ID="rdbcollctionslight" runat="server" 
                                                GroupName="rdbType" CssClass="elementLabel"
                                                Text="ICU" AutoPostBack="True" />
                                                 &nbsp;<asp:RadioButton ID="rbdloanusane" runat="server" 
                                                GroupName="rdbType" CssClass="elementLabel"
                                                Text="ACC" AutoPostBack="True" />                                                                          
                                        </td>
                                    </tr>
                                   <%-- <tr>
                                        <td class="style6">
                                            &nbsp;
                                        </td>
                                        <td align="left" nowrap>
                        <asp:RadioButton ID="rdbAllCustomer" runat="server" CssClass="elementLabel" GroupName="Data1"
                                            TabIndex="4" Text="All Customers" Checked="true" AutoPostBack="true" 
                                                oncheckedchanged="rdbAllCustomer_CheckedChanged"/><asp:RadioButton
                                                ID="rdbSelectedCustomer" runat="server" CssClass="elementLabel" GroupName="Data1"
                                                TabIndex="4" AutoPostBack="true" Text="Selected Customer" 
                                                oncheckedchanged="rdbSelectedCustomer_CheckedChanged" />
                                        </td>
                                    </tr>
                                   <tr>
                                          <div id="divUser"  runat="server" visible="false">
                                        <td align="left" colspan="2" nowrap>
                                         <span class="mandatoryField">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *</span><span class="elementLabel">Customer Ac no :</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCustomer_ID" runat="server" AutoPostBack="True" CssClass="textBox"
                        MaxLength="6" TabIndex="4" Width="90px"></asp:TextBox> 
                                            &nbsp;<asp:Button ID="btnCustomerList" runat="server" ToolTip="Press for Customers list." CssClass="btnHelp_enabled" TabIndex="4" />
                          <asp:Label ID="lblCustomerDesc" runat="server" CssClass="elementLabel"></asp:Label>
                                        </td>
                                        </div>
                                    </tr>--%>
                                </table>
                                <table id="selectedcust" visible="false" runat="server" cellspacing="0" border="0"
                                    width="100%">
                                </table>
                                <table>
                                    <tr valign="bottom">
                                        <td align="right" style="width: 120px">
                                        </td>
                                        <td align="left" style="width: 700px; padding-top: 10px; padding-bottom: 10px" valign="bottom">
                                            &nbsp;
                                            <asp:Button ID="Generate" runat="server" CssClass="buttonDefault" Text="Generate"
                                                ToolTip="Genarate" TabIndex="4"  />
                                        </td>
                                    </tr>
                                </table>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
</body>
</html>
