<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_EBRC_ORM_Maker.aspx.cs" Inherits="EBR_TF_EBRC_ORM_Maker" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"> </script>
    <script language="javascript" type="text/javascript" src="../Scripts/Validations.js"></script>
    <script language="javascript" type="text/javascript">
        function openPurposeCode(e, hNo) {
            var keycode;
            if (keycode == 113 || e == 'mouseClick') {
                open_popup('../TF_PurposeCodeLookUp2.aspx?hNo=' + hNo, 500, 500, 'purposeid');
                return false;
            }
            return true;
        }

        function selectPurpose(id, hNo) {
            var purposeid = document.getElementById('txtPurposeCode');
            if (hNo == '1') {
                purposeid.value = id;
                __doPostBack('purposeid', '');
                return true;
            }
        }
        function openCustAcNo(e, hNo) {
            var keycode;
            var txtBranchCode = document.getElementById('txtBranchCode');
            if (keycode == 113 || e == 'mouseClick') {
                open_popup('../TF_CustomerLookUp3.aspx?branchCode=' + txtBranchCode.value, 500, 500, 'CustAcNo');
                return false;
            }
            return true;
        }

        function selectCustomer(id) {
            var txtCustAcNo = document.getElementById('txtCustAcNo');
            txtCustAcNo.value = id;
            __doPostBack('txtCustAcNo');
            return true;
        }

        function openCountryCode(e, hNo) {
            var keycode;
            if (keycode == 113 || e == 'mouseClick') {
                open_popup('../TF_CountryLookUp1.aspx?hNo=' + hNo, 500, 500, 'purposeid');
                return false;
            }
            return true;
        }

        function selectCountry(id, hNo) {
            var txtRemitterCountry = document.getElementById('txtBeneficiaryCountry');
            var txtRemBankCountry = document.getElementById('txtRemBankCountry');

            if (hNo == '1') {
                txtRemitterCountry.value = id;
                __doPostBack('txtRemitterCountry', '');
                return true;
            }
            if (hNo == '2') {
                txtRemBankCountry.value = id;
                __doPostBack('txtRemBankCountry', '');
                return true;
            }
        }

        function ComputeAmtInINR() {
            var txtAmount = document.getElementById('txtAmount');
            var txtExchangeRate = document.getElementById('txtExchangeRate');
            var txtAmtInINR = document.getElementById('txtAmtInINR');
            if (txtExchangeRate.value == '')
                txtExchangeRate.value = 0;

            document.getElementById('txtExchangeRate').value = parseFloat(txtExchangeRate.value).toFixed(10);
            var inramt = parseFloat((txtAmount.value) * (txtExchangeRate.value)).toFixed(0);
            if (isNaN(inramt) == false)
                txtAmtInINR.value = parseFloat(inramt).toFixed(2);
            else
                txtAmtInINR.value = 0;
        }

        function isValidDate(controlID, CName) {
            var obj = controlID;
            if (controlID.value != "__/__/____") {
                var day = obj.value.split("/")[0];
                var month = obj.value.split("/")[1];
                var year = obj.value.split("/")[2];
                var today = new Date();
                if (day == "__") {
                    day = today.getDay();
                }
                if (month == "__") {
                    month = today.getMonth() + 1;
                }
                if (year == "____") {
                    year = today.getFullYear();
                }
                var dt = new Date(year, month - 1, day);
                if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year) || (dt > today)) {

                    alert('Invalid ' + CName);
                    controlID.focus();
                    return false;
                }
            }
        }
        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }

        function validateSave() {
            var txtdocSrNo = document.getElementById('txtdocSrNo');
            var txtDocDate = document.getElementById('txtDocDate');
            var txtCustAcNo = document.getElementById('txtCustAcNo');
            var txtPurposeCode = document.getElementById('txtPurposeCode');
            var txtAmount = document.getElementById('txtAmount');
            var txtExchangeRate = document.getElementById('txtExchangeRate');
            var txtAmtInINR = document.getElementById('txtAmtInINR');
            var txtFIRCNo = document.getElementById('txtFIRCNo');
            var txtvalueDate = document.getElementById('txtvalueDate');
            var ddlCurrency = document.getElementById('ddlCurrency');
            if (ddlCurrency.value == '0') {
                alert('Select Currency.');
                ddlCurrency.focus();
                return false;
            }
            if (txtdocSrNo.value == '') {
                alert('Enter Document No.');
                txtdocSrNo.focus();
                return false;
            }
            if (txtDocDate.value == '') {
                alert('Enter Document Date.');
                txtDocDate.focus();
                return false;
            }
            if (txtCustAcNo.value == '') {
                alert('Enter Customer A/C No.');
                txtCustAcNo.focus();
                return false;
            }
            if (txtPurposeCode.value == '') {
                alert('Enter Purpose Code.');
                txtPurposeCode.focus();
                return false;
            }
            // if (txtAmount.value != '' || txtAmount.value != 0) {
            //  alert('Invalid Amount In FC');
            // txtAmount.focus();
            //return false;

            //  if (txtExchangeRate.value == '') {
            //      alert('Enter Exchange Rate.');
            //      txtExchangeRate.focus();
            //       return false;
            //    }
            // }
            if (txtvalueDate.value == '') {
                alert('Enter Value Date.');
                txtvalueDate.focus();
                return false;
            }
            //            if (txtAmtInINR.value == '' || txtAmtInINR.value == 0) {
            //                alert('Invalid Amount In INR');
            //                txtAmtInINR.focus();
            //                return false;
            //            }
        }
        function chkFIRCADCode() {
            var txtADCode = document.getElementById('txtADCode');
            if (txtADCode.value.length < 7) {
                alert('FIRC AD Code Should Be 7 Digit.');
                txtADCode.focus();
                return false;
            }
        }

        function validateAmt() {
            var ormfcamt = document.getElementById('txORNFCCAmount').value;
            if (ormfcamt == '') {
                document.getElementById('txORNFCCAmount').value = '';
                return false;
            }
            else {
                var f = (Math.round(ormfcamt * 100) / 100);
                document.getElementById('txORNFCCAmount').value = formatAmt(f);
            }

            var INRpaymentAMT = document.getElementById('txt_inrpayableamt').value;
            if (INRpaymentAMT == '') {
                document.getElementById('txt_inrpayableamt').value = '';
                return false;
            }
            else {
                var f = (Math.round(INRpaymentAMT * 100) / 100);
                document.getElementById('txt_inrpayableamt').value = formatAmt(f);
            }

            var EXCRATE = document.getElementById('txt_exchagerate').value;
            if (EXCRATE == '') {
                document.getElementById('txt_exchagerate').value = '';
                return false;
            }
            else {
                var f = (Math.round(EXCRATE * 100) / 100);
                document.getElementById('txt_exchagerate').value = formatAmt(f);
            }
        }
    </script>
    <style type="text/css">
        .style2
        {
            width: 10%;
        }
        .style3
        {
            width: 14%;
        }
    </style>
</head>
<body  onload="EndRequest();setfocus();" onunload="closeWindows();">
    <form id="form1" runat="server" autocomplete="off">
    <div>
        <uc1:Menu ID="Menu1" runat="server" />
        <asp:ScriptManager ID="ScriptManagerMain" runat="server">
        </asp:ScriptManager>
        <script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>
        <script language="javascript" type="text/javascript" src="../Scripts/Enable_Disable_Opener.js"></script>
        <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
            <ProgressTemplate>
                <div id="progressBackgroundMain" class="progressBackground">
                    <div id="processMessage" class="progressimageposition">
                        <img src="../Images/ajax-loader.gif" style="border: 0px" alt="" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
            <ContentTemplate>
                <table width="100%" cellpadding="1">
                    <tr>
                        <td colspan="4" align="left">
                            <span class="pageLabel"><strong>EBRC ORM DataEntry View - Maker </strong></span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="left">
                            <asp:Label ID="lblmessage" runat="server" CssClass="mandatoryField"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="style3">
                            <span class="mandatoryField">*</span> <span class="elementLabel">Branch ID : </span>
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="txtBranchCode" runat="server" CssClass="textBox" Style="width: 40px"
                                Enabled="false" TabIndex="1" Text="01"></asp:TextBox>
                            
                        </td>
                    </tr>
                    
                    <tr>
                        <td style="text-align: right" class="style3">
                            <span class="mandatoryField">*</span> <span class="elementLabel">ORM No : </span>
                        </td>
                        <td align="left" width="3%">
                            <asp:TextBox ID="txtORMNo" runat="server" CssClass="textBox" 
                                AutoPostBack="true" Width="200px"></asp:TextBox>

                          <%--  <asp:TextBox ID="txtdocSrNo" Style="width: 45px" runat="server" CssClass="textBox"
                                MaxLength="6" OnTextChanged="txtdocSrNo_TextChanged" AutoPostBack="true" TabIndex="2"> </asp:TextBox>--%>
                        </td>
                        <td style="text-align: right; width: 5%;white-space:nowrap;">
                            <span class="mandatoryField">*</span> <span class="elementLabel">Payment Date :
                            </span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtPaymentDate" runat="server" CssClass="textBox" Style="width: 70px"
                                TabIndex="3"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="mdPaymentdate" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtPaymentDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="btncalendar_PaymentDate" runat="server" CssClass="btncalendar_enabled"
                                TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtPaymentDate" PopupButtonID="btncalendar_PaymentDate" Enabled="True">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdPaymentdate"
                                ValidationGroup="dtVal" ControlToValidate="txtPaymentDate" EmptyValueMessage="Enter Date Value"
                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="style3">
                            <span class="mandatoryField">*</span> <span class="elementLabel">Purpose Code :
                            </span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPurposeCode" runat="server" CssClass="textBox" Style="width: 50px"
                                AutoPostBack="true" MaxLength="6"
                                TabIndex="6" ></asp:TextBox>
                            <asp:Button ID="btnpurposecode" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" OnClientClick="return openPurposeCode('mouseClick','1');"/>
                            &nbsp;<asp:Label ID="lblpurposeCode" runat="server" CssClass="elementLabel"></asp:Label>
                        </td>
                        
                    </tr>
                     <tr>
                        <td style="text-align: right" class="style3">
                          <span class="mandatoryField">*</span>
                            <span class="elementLabel">Beneficiary Name :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtBeneficiaryName" runat="server" Width="280px" CssClass="textBox"
                                MaxLength="50" TabIndex="7"></asp:TextBox>
                        </td>
                        <td style="text-align: right;white-space:nowrap;" >
                          <span class="mandatoryField">*</span>
                            <span class="elementLabel">Beneficiary Country :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtBeneficiaryCountry" runat="server" CssClass="textBox" Style="width: 30px"
                                AutoPostBack="true" MaxLength="2" TabIndex="8" 
                                ></asp:TextBox>
                            <asp:Button ID="btncountrylist" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" OnClientClick="return openCountryCode('mouseClick','1');"/>
                            &nbsp;<asp:Label ID="lblCountryDesc" runat="server" CssClass="elementLabel"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="style3">
                            <span class="elementLabel">IEC Code. : </span>
                        </td>
                        <td align="left">

                        <asp:TextBox ID="txt_ieccode" runat="server" CssClass="textBox" 
                                AutoPostBack="true" Width="150px" Enabled="true" MaxLength="10"></asp:TextBox>
                        </td>

                        <td style="text-align: right;white-space:nowrap;" >
                          <span class="mandatoryField">*</span> <span class="elementLabel">AD Code :</span>
                        </td>
                        <td align="left">
                          <asp:TextBox ID="txadcode" runat="server" CssClass="textBox" 
                                MaxLength="7" Width="200px"  TabIndex="6"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="style3">
                            <span class="mandatoryField">*</span> <span class="elementLabel">ORN FC : </span>
                        </td>
                        <td align="left">

                        <asp:TextBox ID="txt_ORNfc" runat="server" CssClass="textBox" 
                                AutoPostBack="true" Width="150px" Enabled="true"></asp:TextBox>
                        </td>

                        <td style="text-align: right;white-space:nowrap;" >
                          <span class="mandatoryField">*</span> <span class="elementLabel">ORN FCC Amount :</span>
                        </td>
                        <td align="left">
                          <asp:TextBox ID="txORNFCCAmount" runat="server" CssClass="textBoxRight" 
                                MaxLength="50" Width="200px"  TabIndex="6"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td style="text-align: right" class="style3">
                            <span class="elementLabel">Exchange Rate : </span>
                        </td>
                        <td align="left">

                        <asp:TextBox ID="txt_exchagerate" runat="server"  CssClass="textBoxRight" 
                                AutoPostBack="true" Width="150px" Enabled="true"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td style="text-align: right" class="style3">
                            <span class="mandatoryField">*</span> <span class="elementLabel">Bank Transction Id : </span>
                        </td>
                        <td align="left" width="3%">
                            <asp:TextBox ID="txtbktxid" runat="server" CssClass="textBox" 
                                AutoPostBack="true" Width="200px" Enabled="false" 
                                ontextchanged="txtbktxid_TextChanged"></asp:TextBox>

                          <%--  <asp:TextBox ID="txtdocSrNo" Style="width: 45px" runat="server" CssClass="textBox"
                                MaxLength="6" OnTextChanged="txtdocSrNo_TextChanged" AutoPostBack="true" TabIndex="2"> </asp:TextBox>--%>
                        </td>
                        <td style="text-align: right; width: 5%;white-space:nowrap;">
                            <span class="mandatoryField">*</span> <span class="elementLabel">ORM Issue Date :
                            </span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtormissueDate" runat="server" CssClass="textBox" Style="width: 70px"
                                TabIndex="3"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="mdormissuedate" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtormissueDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="btncalendar_ormissueDate" runat="server" CssClass="btncalendar_enabled"
                                TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="calendarFromDate1" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtormissueDate" PopupButtonID="btncalendar_ormissueDate" Enabled="True">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="mdormissuedate"
                                ValidationGroup="dtVal" ControlToValidate="txtormissueDate" EmptyValueMessage="Enter Date Value"
                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="style3">
                            <span class="elementLabel">INR Payable Amount : </span>
                        </td>
                        <td align="left">

                        <asp:TextBox ID="txt_inrpayableamt" runat="server"  CssClass="textBoxRight" 
                                AutoPostBack="true" Width="150px" Enabled="true"></asp:TextBox>
                        </td>

                        <td style="text-align: right;white-space:nowrap;" >
                          <span class="mandatoryField">*</span> <span class="elementLabel">IFSC Code :</span>
                        </td>
                        <td align="left">
                          <asp:TextBox ID="txtifsccode" runat="server" CssClass="textBox" 
                                MaxLength="11" Width="150px"  TabIndex="6"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="style3">
                            <span class="mandatoryField">*</span> <span class="elementLabel">Pan Number : </span>
                        </td>
                        <td align="left">

                        <asp:TextBox ID="txt_panno" runat="server" CssClass="textBox" 
                                AutoPostBack="true" Width="150px" Enabled="true" MaxLength="10"></asp:TextBox>
                        </td>

                        <td style="text-align: right;white-space:nowrap;" >
                          <span class="elementLabel">Mode Of Payment :</span>
                        </td>
                        <td align="left">
                          <asp:TextBox ID="txtmodeofpayment" runat="server" CssClass="textBox" 
                                MaxLength="50" Width="150px"  TabIndex="6"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="style3">
                            <span class="elementLabel">Reference IRM : </span>
                        </td>
                        <td align="left">

                        <asp:TextBox ID="txtrefIRM" runat="server" CssClass="textBox" 
                                AutoPostBack="true" Width="200px" Enabled="true"></asp:TextBox>
                        </td>

                        <%--<td style="text-align: right;white-space:nowrap;" >
                          <span class="mandatoryField">*</span> <span class="elementLabel">Bank Account Number :</span>
                        </td>
                        <td align="left">
                          <asp:TextBox ID="txtbkacno" runat="server" CssClass="textBox" 
                                MaxLength="50" Width="200px"  TabIndex="6"></asp:TextBox>
                        </td>--%>
                    </tr>
                    <tr>
                        <%--<td style="text-align: right" class="style3">
                            <span class="mandatoryField">*</span> <span class="elementLabel">Approve / Reject : </span>
                        </td>
                        <td align="left">

                            <asp:DropDownList runat="server" ID="ddlstatus">
                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Approve" Value="A"></asp:ListItem>
                            <asp:ListItem Text="Reject" Value="R"></asp:ListItem>
                            </asp:DropDownList>
                        </td>--%>

                        <td style="text-align: right;white-space:nowrap;" >
                          <span class="mandatoryField">*</span> <span class="elementLabel">Orm Status :</span>
                        </td>
                        <td align="left">
                           <asp:DropDownList runat="server" ID="ddlOrmstatus" CssClass="dropdownList" AutoPostBack="true">
                             <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="F" Value="F"></asp:ListItem>
                            <asp:ListItem Text="A" Value="A"></asp:ListItem>
                            <asp:ListItem Text="C" Value="C"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    

                    
                    <tr>
                        <td class="style3">
                        </td>
                        <td align="left" colspan="3">
                            <asp:Button ID="btnAdd" runat="server" CssClass="buttonDefault" Text="Sent To Checker"
                                TabIndex="18" onclick="btnAdd_Click"  />
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="buttonDefault" Text="Cancel"
                               TabIndex="20" onclick="btnCancel_Click" />
                            <br />
                            <br />
                            <br />
                           
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                       <%-- <td align="right"><span class="elementLabel" style="color:red">Remarks :- </span></td>--%>
                        <td style="width :20%; text-align :right" >
                            <span class="elementLabel" style="color:red">Remarks :- </span>
                            <asp:Label ID="lblremark" runat="server" style="color:red" ></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
    <script language="javascript" type="text/javascript">
        window.onload = function () {
            toogleDisplayHelp();

            var txtvaluetxORNFCCAmount = document.getElementById('txORNFCCAmount');
            if (txtvaluetxORNFCCAmount.value == '')
                txtvaluetxORNFCCAmount.value = 0;
            else
                document.getElementById('txORNFCCAmount').value = parseFloat(txtvaluetxORNFCCAmount.value).toFixed(2);

            var txtvaluetxt_inrpayableamt = document.getElementById('txt_inrpayableamt');
            if (txtvaluetxt_inrpayableamt.value == '')
                txtvaluetxt_inrpayableamt.value = 0;
            else
                document.getElementById('txt_inrpayableamt').value = parseFloat(txtvaluetxt_inrpayableamt.value).toFixed(2);

            var txtvaluetxt_exchagerate = document.getElementById('txt_exchagerate');
            if (txtvaluetxt_exchagerate.value == '')
                txtvaluetxt_exchagerate.value = 0;
            else
                document.getElementById('txt_exchagerate').value = parseFloat(txtvaluetxt_exchagerate.value).toFixed(2);
            //suchitra end
        }
    </script>
</body>
</html>
