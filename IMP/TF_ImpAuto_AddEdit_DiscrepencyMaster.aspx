<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_ImpAuto_AddEdit_DiscrepencyMaster.aspx.cs"
    Inherits="TF_ImpAuto_AddEdit_DiscrepencyMaster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-Trade Finance System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />

      <script src="../Help_Plugins/MyJquery1.js" type="text/javascript"></script> <%--snackbar--%>
     <script src="../Help_Plugins/jquerynew.min.js" language="javascript" type="text/javascript"></script>
    <script src="../Help_Plugins/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <link href="../Help_Plugins/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Help_Plugins/AlertJquery.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="~/Scripts/Validations.js"></script>
    <script language="javascript" type="text/javascript">

        function decimal() {
            var txtAmount = document.getElementById('txtAmount');
            if (txtAmount.value == '') {

                txtAmount.value = "0";
                return false;
            }
            txtAmount.value = parseFloat(txtAmount.value).toFixed(2);
        } 
       
        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            //alert(charCode);
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;

        }

        function validateSave() {

            var id = document.getElementById('txtCurr');
//            var inr = document.getElementById('txtINR');
            var amt = document.getElementById('txtAmount');



            if (id.value == '') {

//                alert('Enter ID.');
                //                custacno.focus();
                VAlert('Enter ID.', '#txtCurr');
                return false;

            }
            if (amt.value == '') {
//                alert('Enter Amount');
                //                txtCommissionDesc.focus();
                VAlert('Enter Amount.', '#txtAmount');
                return false;
            }
//            if (inr.value == '') {
//                //                alert('Enter description');
//                //                txtCommissionID.focus();
//                VAlert('Enter INR.', '#txtINR');
//                return false;
//            }

            return true;
        }

        function curhelp3() {

            popup = window.open('../TF_CurrencyLookUp2.aspx', 'helpCurId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "helpCurId2";
            return false;

        }


        function sss() {
            var s = popup.document.getElementById('txtcell1').value;
            //            var s1 = popup.document.getElementById('txtcell2').value;
            if (common == "helpCurId2") {
                document.getElementById('txtCurr').value = s;
                //                document.getElementById('lblCurrDesc').innerHTML = s1;

                javascript: setTimeout('__doPostBack(\'txtCurr\',\'\')', 0);
            }

        }


    </script>
</head>
<body onload="decimal()">
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
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
     <%--  alert message--%>
    <div id="dialog" class="AlertJqueryHide">
        <p id="Paragraph">
        </p>
    </div>
    <div>
        <uc2:Menu ID="Menu1" runat="server" />
        <br />
        <br />
        <center>
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="conditional">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                            <input type="hidden" runat="server" id="hdnAmount" />
                           <%-- <input type="hidden" runat="server" id="hdnINR" />--%>
                                <span class="pageLabel"> <strong>Discrepency Charges Master</strong> </span>
                            </td>
                        </tr> 
                        <tr>
                            <td align="left" style="width: 100%" valign="top">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100%" valign="top">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top">
                                <table border="0" cellspacing="0" width="30%">
                                    <tr>
                                        <td align="right" nowrap width="10%">
                                            <span class="elementLabel">Currency :</span>
                                        </td>
                                        <td align="left" nowrap width="20%">
                                            <asp:TextBox ID="txtCurr" runat="server" CssClass="textBox" Height="14px" onfocus="this.select()"
                                                Style="text-align: right;" Width="40px" TabIndex="1" OnTextChanged="txtCurr_TextChanged"
                                                AutoPostBack="true"></asp:TextBox>
                                            <asp:Button ID="btnCur" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                            <asp:Label ID="lblCurrDesc" runat="server" CssClass="elementLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap width="10%">
                                            <span class="elementLabel">FC Amount :</span>
                                        </td>
                                        <td align="left" nowrap width="20%">
                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="textBox" Height="14px" onfocus="this.select()"
                                                Style="text-align: right;" Width="85px" TabIndex="1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td align="right" nowrap width="10%">
                                            <span class="elementLabel">INR :</span>
                                        </td>
                                        <td align="left" nowrap width="20%">
                                            <asp:TextBox ID="txtINR" runat="server" CssClass="textBox" Height="14px" onfocus="this.select()"
                                                Style="text-align: right;" Width="85px" TabIndex="1"></asp:TextBox>
                                        </td>
                                    </tr>--%>
                                    <%--<tr>
                                        <td align="right" nowrap width="10%">
                                            <span class="elementLabel">Bank Certificate Fees :</span>
                                        </td>
                                        <td align="left" nowrap width="20%">
                                            <asp:TextBox ID="txtbankCertificate" runat="server" CssClass="textBox" Height="14px"
                                                onfocus="this.select()" Style="text-align: right;" Width="60px" TabIndex="1"></asp:TextBox>
                                        </td>
                                    </tr>--%>
                                    <%--<tr>
                                        <td align="right" nowrap>
                                            <span class="elementLabel">Negotiation Fees :</span>
                                        </td>
                                        <td align="left" nowrap>
                                            <asp:TextBox ID="txtNegotiation" runat="server" CssClass="textBox" Height="14px"
                                                onfocus="this.select()" Style="text-align: right;" Width="60px" TabIndex="2"></asp:TextBox>
                                        </td>
                                    </tr>--%>
                                    <%--<tr>
                                        <td align="right" nowrap>
                                            <span class="elementLabel">Postage/Courier Charges :</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCourier" runat="server" CssClass="textBox" Height="14px" onfocus="this.select()"
                                                Style="text-align: right;" Width="60px" TabIndex="3"></asp:TextBox>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" nowrap>
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault" ToolTip="Save"
                                                TabIndex="4" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                ToolTip="Cancel" TabIndex="5" OnClick="btnCancel_Click" />

                                                 <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
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
