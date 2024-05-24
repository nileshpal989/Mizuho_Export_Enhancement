<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_AddEditProfitLieu_Master.aspx.cs" Inherits="TF_AddEditProfitLieu_Master" %>

<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
      <link href="Style/style_new.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript" type="text/javascript">
        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            //alert(charCode);
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;

        }
        function Rate() {
            var Rate = document.getElementById('txtRate');
            if (Rate.value == '') {
                Rate.value = parseFloat(0).toFixed(6);
            }
            else {
                Rate.value = parseFloat(Rate.value).toFixed(6);
            }
        }


        function custhelp(adcode) {

            //  var custacno = document.getElementById('txt_custacno');
            open_popup('TF_CustomerLookUp2.aspx?adcode=' + adcode, 450, 550, 'Custlist');

            return false;

        }

        function selectCustomer(a) {

            var custid = a;
            document.getElementById('txt_custacno').value = custid;
            document.getElementById('txtCommissionID').focus();

        }

        function validateSave() {

            var custacno = document.getElementById('txt_custacno');
            var txtCommissionID = document.getElementById('txtCommissionID');
            var txtCommissionDesc = document.getElementById('txtDescription');
            //  var txtbillAmtFrom = document.getElementById('txtBillAmtFrom');
            //var txtbillAmtTo = document.getElementById('txtBillAmtTO');
            var txtRate = document.getElementById('txtRate');
            var txtMiniRs = document.getElementById('txtMinRS');
            var txtMaxRs = document.getElementById('txt_maxrs');
            var txtflat = document.getElementById('txt_flat');

            if (custacno.value == '') {

                alert('Enter Cust A/C No.');
                custacno.focus();
                return false;

            }

            if (txtCommissionID.value == '') {
                alert('Enter Commission ID.');
                txtCommissionID.focus();
                return false;
            }


            if (txtCommissionDesc.value == '') {
                alert('Enter Commission Description.');
                txtCommissionDesc.focus();
                return false;
            }


            if (txtRate.value != '' && txtRate.value != '0.000000') {

                if (txtMiniRs.value == '') {

                    alert('Enter minimum Amt..');
                    txtMiniRs.focus();
                    return false;
                }

                if (txtMaxRs.value == '') {
                    alert('Enter maximum Amt.');
                    txtMaxRs.focus();
                    return false;

                }


            }




            if (txtRate.value == '' || txtRate.value == '0.000000') {

                if (txtflat.value == '' || txtflat.value == '0.00') {

                    alert('Enter Flat value');
                    txtflat.focus();
                    return false;

                }
            }

            //            if (txtMiniRs.value == '') {
            //                alert('Enter Mini Rs.');
            //                txtMiniRs.focus();
            //                return false;
            //            }

            //            if (txtMaxRs.value == '') {

            //                alert('Enter Max Rs.');
            //                txtMaxRs.focus();
            //                return false;

            //            }

            return true;
        }



    </script>
    <style type="text/css">
        .style1
        {
            height: 21px;
        }
        .style2
        {
            height: 21px;
            width: 12%;
        }
        .style3
        {
            width: 12%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" defaultbutton="btnSave">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
     <script language="javascript" type="text/javascript" src="Scripts/Enable_Disable_Opener.js"></script>
    <script language="javascript" type="text/javascript" src="Scripts/InitEndRequest.js"></script>

     <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
              
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel">Profit In Lieu Master Details</span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" OnClick="btnBack_Click"
                                    ToolTip="Back" TabIndex="9"/>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" Font-Bold="true" Font-Size="Small" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="2">
                                <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 150%">
                                <tr>
                                <td align=right class="style2">
                                <span class="mandatoryField">*</span><span class="elementLabel">Customer A/C No :</span>
                                </td>
                                <td align=left nowrap class="style1">
                                 &nbsp;<asp:TextBox ID="txt_custacno" runat="server" CssClass="textBox" MaxLength="12"
                                                Width="100px" TabIndex="1" ></asp:TextBox>
                                                <asp:Button ID="btncustacno" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                   
                                </td>
                                </tr>
                                    <tr>

                                   
                                        <td align="right" class="style3">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Commission ID:</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtCommissionID" runat="server" CssClass="textBox" MaxLength="2"
                                                Width="40" TabIndex="1" AutoPostBack=true 
                                                ontextchanged="txtCommissionID_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style3">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Description :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtDescription" runat="server" CssClass="textBox" MaxLength="60"
                                                Width="400px" TabIndex="2"></asp:TextBox>
                                        </td>
                                    </tr>
                                  <%--  <tr>
                                        <td align="right" style="width: 10%">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Bill.Amt From :</span>
                                        </td>
                                        <td align="left" >
                                            &nbsp;<asp:TextBox ID="txtBillAmtFrom" runat="server" CssClass="textBoxRight" MaxLength="10"
                                                Width="100px" TabIndex="3"></asp:TextBox>
                                        </td>
                                    </tr>--%>
                                    <%--<tr>
                                        <td align="right" style="width: 10%">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Bill.Amt To :</span>
                                        </td>
                                        <td align="left" >
                                            &nbsp;<asp:TextBox ID="txtBillAmtTO" runat="server" CssClass="textBoxRight" MaxLength="10"
                                                Width="100px" TabIndex="4"></asp:TextBox>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td align="right" class="style3">
                                           <span class="elementLabel">Rate :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtRate" runat="server" CssClass="textBoxRight" MaxLength="10"
                                                Width="80px" TabIndex="5" ></asp:TextBox>
                                                  &nbsp;<span class="elementLabel">%</span>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td align="right" class="style3">
                                          <span class="elementLabel">Minimum Rs. :</span>
                                        </td>
                                        <td align="left" >
                                            &nbsp;<asp:TextBox ID="txtMinRS" runat="server" CssClass="textBoxRight" MaxLength="10"
                                                Width="80px" TabIndex="6" ></asp:TextBox>
                                        </td>

                                    </tr>

                                    <tr>
                                    <td align=right class="style3">
                                 <span class="elementLabel">Maximum Rs. :</span>
                                    </td>

                                    <td>
                                        &nbsp;<asp:TextBox runat="server" ID="txt_maxrs" CssClass="textBoxRight" Width=80px TabIndex="6" style="text-align:right"/>
                                    </td>

                                    </tr>

                                    <tr>
                                    <td align=right class="style3">
                                      <span class="elementLabel">Flat Amount Rs. :</span>
                                    </td>
                                    <td align=left>
                                    &nbsp;<asp:TextBox runat="server" ID="txt_flat" CssClass="textBoxRight" Width=80px TabIndex="6" style="text-align:right"/>
                                    </td>
                                    </tr>



                                    <tr>
                                        <td align="right" class="style3">
                                        </td>
                                        <td align="left" style="width: 290px; padding-top: 10px; padding-bottom: 10px">
                                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                                OnClick="btnSave_Click" ToolTip="Save" TabIndex="7"/>
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                OnClick="btnCancel_Click" ToolTip="Cancel" TabIndex="8"/>
                                        </td>
                                    </tr>
                                </table>
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