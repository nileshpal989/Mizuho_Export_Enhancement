<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_OtherBankCharges.aspx.cs"
    Inherits="TF_OtherBankCharges" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-Trade Finance System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />

      <script src="../Help_Plugins/MyJquery1.js" type="text/javascript"></script>
    <script src="../Help_Plugins/jquerynew.min.js" language="javascript" type="text/javascript"></script>
    <script src="../Help_Plugins/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <link href="../Help_Plugins/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Help_Plugins/AlertJquery.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            //alert(charCode);
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;

        }

        function validateSave() {

            var id = document.getElementById('txtid');
            var desc = document.getElementById('txtdesc');
            var amt = document.getElementById('txtAmount');



            if (id.value == '') {

//                alert('Enter ID.');
                //                custacno.focus();
                VAlert('Enter ID.', '#txtid');
                return false;
            }

            if (desc.value == '') {
//                alert('Enter description');
//                txtCommissionID.focus();
                VAlert('Enter description.', '#txtdesc');
                return false;
            }
            if (amt.value == '') {
//                alert('Enter Amount');
                //                txtCommissionDesc.focus();
                VAlert('Enter Amount.', '#txtAmount');
                return false;
            }
            
            return true;
        }


    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            // Configure to save every 5 seconds  
            window.setInterval(saveDraft, 5000); //calling saveDraft function for every 5 seconds  
        });

        // ajax method  
        function saveDraft() {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "TF_OtherBankCharges.aspx/AutoSave",
                data: "{'firstname':'" + document.getElementById('txtid').value + "','middlename':'" + document.getElementById('txtdesc').value + "','lastname':'" + document.getElementById('txtAmount').value + "'}",

                success: function (response) {

                }

            });
        }  
    </script>
</head>
<body>
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
                            <input type="hidden" runat="server" id="hdnotherbankdescription" />
                            <input type="hidden" runat="server" id="hdnAmount" />
                                <span class="pageLabel"><strong>Other Bank Charges Master</strong></span>
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
                                            <span class="elementLabel">ID :</span>
                                        </td>
                                        <td align="left" nowrap width="20%">
                                            <asp:TextBox ID="txtid" runat="server" CssClass="textBox" Height="14px" onfocus="this.select()"
                                                Style="text-align: right;" Width="40px" TabIndex="1" 
                                                ontextchanged="txtid_TextChanged" AutoPostBack="true" MaxLength="10"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap width="10%">
                                            <span class="elementLabel">Description :</span>
                                        </td>
                                        <td align="left" nowrap width="20%">
                                            <asp:TextBox ID="txtdesc" runat="server" CssClass="textBox" Height="14px" onfocus="this.select()"
                                                Width="250px" TabIndex="1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap width="10%">
                                            <span class="elementLabel">Amount :</span>
                                        </td>
                                        <td align="left" nowrap width="20%">
                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="textBox" Height="14px" onfocus="this.select()"
                                                Style="text-align: right;" Width="80px" TabIndex="1"></asp:TextBox>
                                        </td>
                                    </tr>
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
