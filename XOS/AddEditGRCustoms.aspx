<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddEditGRCustoms.aspx.cs"
    Inherits="EXP_AddEditGRCustoms" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="../Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <title>LMCC-TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="../Images/favicon.ico"
        type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="../Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function AlertGRcurrency() {
            var curr = $get("lblCurrency");
            var currValue = curr.innerHTML;
            var currGR = $get("ddlCurrencyGRPP");
            var currGRValue = currGR.options[currGR.selectedIndex].value;

            if (currGRValue != currValue) {
                alert("Invalid Currency");
            }
            return true;
        }

        function FillFormType_SDF() {

            var FormType = $get("ddlFormType");
            var FormTypeValue = FormType.options[FormType.selectedIndex].value;

            if (FormTypeValue == 'SDF' && document.getElementById("txtGRPPCustomsNo").value == '') {
                document.getElementById("txtGRPPCustomsNo").value = '00000';
            }
            return true;
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

                //              if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year) || (dt > today)) {

                if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year)) {

                    alert('Invalid ' + CName);
                    controlID.focus();
                    return false;
                }
            }
        }

        function OpenOverseasPartyList(e) {
            var keycode;

            if (window.event) keycode = window.event.keyCode;

            if (keycode == 113 || e == 'mouseClick') {

                var txtOverseasPartyID = document.getElementById("txtOverseasPartyID");
                open_popup('../EXP/TF_OverseasPartyLookUp.aspx?bankID=' + txtOverseasPartyID.value, 450, 500, 'OverseasPartyList');
                return false;
            }
        }

        function selectOverseasParty(selectedID) {
            var id = selectedID;
            document.getElementById('hdnOverseasPartyId').value = id;
            document.getElementById('btnOverseasParty').click();
        }

        function ValidateSave() {

            var hdnGRtotalAmount = document.getElementById("hdnGRtotalAmount");
            var hdnBillType = document.getElementById("hdnBillType");
            var txtAmountGRPP = document.getElementById("txtAmountGRPP");
            var txtDueDate = document.getElementById("txtDueDate");

            var txtCountryCode = document.getElementById("txtCountryCode");

            if (txtAmountGRPP.value == "")
                txtAmountGRPP.value = "0";

            var txtBillAmount = $get("txtBillAmount");
            if (txtBillAmount.value == '')
                txtBillAmount.value = 0;

            if (parseFloat(txtAmountGRPP.value) > 0) {
                alert('Click Add button to save GR Details and then try to Save the bill record.');
                // currValue.focus();
                return false;
            }

            if (hdnBillType.value != 'M') {
                if (txtDueDate.value == '') {
                    alert('Enter Duedate.');
                    txtDueDate.focus();
                    return false;
                }

                if (txtCountryCode.value == '') {
                    alert('Select Country.');
                    txtCountryCode.focus();
                    return false;
                }
            }


            txtBillAmount.value = parseFloat(txtBillAmount.value).toFixed(2);


            if (parseFloat(txtBillAmount.value) != parseFloat(hdnGRtotalAmount.value)) {
                if (parseFloat(txtBillAmount.value) > parseFloat(hdnGRtotalAmount.value)) {
                    answer = confirm('GR Amount is Less than Bill Amount by ' + (parseFloat(hdnGRtotalAmount.value) - parseFloat(txtBillAmount.value)))
                    if (!answer)

                        return false;
                }
                if (parseFloat(txtBillAmount.value) < parseFloat(hdnGRtotalAmount.value)) {
                    answer = confirm('GR Amount is Greater than Bill Amount by ' + (parseFloat(hdnGRtotalAmount.value) - parseFloat(txtBillAmount.value)))
                    if (!answer)

                        return false;
                }
            }

            return true;
        }

        function gridClicked(sr) {
            var id = sr;

            document.getElementById('hdnGridValues').value = id;
            document.getElementById('btnGridValues').click();
        }

        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            //  alert(charCode);
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }

        function Commodityelp() {
            open_popup('Help_GRCustoms_Commodity.aspx', 400, 400, "DocFile");
            return true;
        }

        function OpenCountryList(hNo) {

            open_popup('Help_GRCustoms_Country.aspx?hNo=' + hNo, 450, 450, 'CountryList');
            return false;
        }


        function selectCountry(selectedID, country) {
            var id = selectedID;

            document.getElementById('txtCountryCode').value = id;
            document.getElementById('lblCountryCode').innerText = country;

        }

        function PortHelp() {
            open_popup('Help_GRCustoms_Port_Code.aspx', 400, 400, "DocFile");
            return true;
        }


        function selectPort(Uname) {
            document.getElementById('ddlPortCode').value = Uname;
            document.getElementById('ddlPortCode').focus();
        }


        function selectCommodity(Uname, Commodity) {
            document.getElementById('txtCommodityID').value = Uname;
            document.getElementById('lblCommodity').innerHTML = Commodity;
            document.getElementById('txtCommodityID').focus();
        }

    </script>
    <script language="javascript" type="text/javascript">

        function checkSysDate(controlID) {

            var obj = controlID;

            if (controlID.value != "__/__/____") {

                var day = obj.value.split("/")[0];

                var month = obj.value.split("/")[1];
                var year = obj.value.split("/")[2];

                var dt = new Date(year, month - 1, day);

                var dateRcvd = document.getElementById('txtDateRcvd');
                var drday = dateRcvd.value.split("/")[0];

                var drMonth = dateRcvd.value.split("/")[1];
                var drYear = dateRcvd.value.split("/")[2];

                var dt1 = new Date(drYear, drMonth - 1, drday);

                if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year) || (dt < dt1)) {

                    alert("Invalid Date");
                    controlID.focus();
                    return false;
                }
            }
        }

        function chkShippingBillNo() {

            var txtShippingBillNo = $get("txtShippingBillNo");
            txtShippingBillNo.value = txtShippingBillNo.value.replace(/ /g, "");
            var ddlPortCode = $get("ddlPortCode");
            var txtShippingBillDt = $get("txtShippingBillDate");
            var txtInvoiceNo = $get("txtInvoiceNum");

            var i = 0;
            var Grid = $get("GridViewGRPPCustomsDetails");

            //            if (txtShippingBillNo.value == "") {
            //                alert('Please Enter Shipping Bill No.');
            //                txtShippingBillNo.focus();
            //                return false;
            //            }

            var txtExchRate = document.getElementById('txtExchRateGR');

            if (txtExchRate.value == 0) {
                alert('Please Enter Exch. Rate');
                txtExchRate.focus();
                return false;

            }

            var txtAmt = document.getElementById('txtAmountGRPP');

            if (txtAmt.value == 0) {
                alert('Please Enter Amount');
                txtAmt.focus();
                return false;
            }

            if (ddlPortCode.value == "0") {
                alert("Please Select Port Code.");
                ddlPortCode.focus();
                return false;
            }

            if (txtShippingBillNo.value == "") {
                alert('Please Enter Unique Shipping Bill No.');
                txtShippingBillNo.focus();
                return false;
            }
            else {

                for (i = 1; i < Grid.rows.length; i++) {

                    if ((txtShippingBillNo.value.toString().trim() == Grid.rows[i].cells[9].innerText.toString().trim()) && txtInvoiceNo.value == Grid.rows[i].cells[11].innerText.toString().trim()) {
                        alert("Invoice no. " + txtInvoiceNo.value + " is already exists in Sr No." + Grid.rows[i].cells[0].innerText.toString().trim());
                        txtInvoiceNo.focus();
                        return false;
                    }
                    if ((txtShippingBillNo.value.toString().trim() == Grid.rows[i].cells[9].innerText.toString().trim()) && (txtShippingBillDt.value != Grid.rows[i].cells[10].innerText.toString().trim())) {
                        alert("Shipping Bill Date should be same for same Shipping Bill Number.");
                        txtShippingBillDt.focus();
                        return false;

                    }

                }
            }

            return true;
        }

        function Calculate() {
            var txtAmountGRPP = document.getElementById('txtAmountGRPP');
            if (txtAmountGRPP.value == '')
                txtAmountGRPP.value = 0;
            txtAmountGRPP.value = parseFloat(txtAmountGRPP.value).toFixed(2);

            var txtGRppExchRate = document.getElementById('txtExchRateGR');
            if (txtGRppExchRate.value == '')
                txtGRppExchRate.value = 0;
            txtGRppExchRate.value = parseFloat(txtGRppExchRate.value).toFixed(10);

            var txtAmountGRPP_In_INR = document.getElementById('txtAmountinINRGR');
            if (txtAmountGRPP_In_INR.value == '')
                txtAmountGRPP_In_INR.value = 0;
            txtAmountGRPP_In_INR.value = parseFloat(txtAmountGRPP_In_INR.value).toFixed(2);

            txtAmountGRPP_In_INR.value = parseFloat(txtAmountGRPP.value) * parseFloat(txtGRppExchRate.value);
            txtAmountGRPP_In_INR.value = parseFloat(txtAmountGRPP_In_INR.value).toFixed(2);
            return true;
        }
    </script>
</head>
<body onload="EndRequest();closeWindows();" onunload="closeWindows();">
    <form id="form1" runat="server" autocomplete="off" unselectable="off">
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
        </center>
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                            <input type="hidden" id="hdnGridValues" runat="server" />
                            <input type="hidden" id="hdnGRtotalAmount" runat="server" />
                            <input type="hidden" id="hdnBillType" runat="server" />
                            <input type="hidden" id="hdnOverseasPartyId" runat="server" />
                            <asp:Button ID="btnGridValues" Style="display: none;" runat="server" OnClick="btnGridValues_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom">
                            <span class="pageLabel">Updation of GR Details For XOS</span>
                        </td>
                        <td align="right" style="width: 50%">
                            <asp:Label runat="server" ID="lblSupervisormsg" Text="Role : Supervisor - ONLY VIEW THE DATA  "
                                Style="color: red"></asp:Label>&nbsp;
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                TabIndex="25" OnClick="btnBack_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%" valign="top" colspan="2">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%" valign="top" colspan="2">
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table cellpadding="0" cellspacing="2" border="0" width="100%">
                                <tr>
                                    <td width="5%" align="right">
                                        <span class="elementLabel">Doc No :</span>
                                    </td>
                                    <td align="left" nowrap>
                                        <asp:TextBox ID="txtDocumentNo" Width="140px" runat="server" CssClass="textBox" ReadOnly="True"
                                            TabIndex="-1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" nowrap>
                                        <span class="elementLabel">Bill Amount :</span>
                                    </td>
                                    <td align="left" nowrap>
                                        <asp:TextBox ID="txtBillAmount" runat="server" CssClass="txtdisabled" Style="text-align: right"
                                            Width="100px" TabIndex="-1" MaxLength="20" ReadOnly="true"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Label
                                                ID="lblCurrency" runat="server" CssClass="elementLabel" Style="font-weight: bold;"
                                                Width="50px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="10%" align="right">
                                        <span class="elementLabel">Commodity ID :</span>
                                    </td>
                                    <td align="LEFT" nowrap>
                                        <asp:TextBox ID="txtCommodityID" runat="server" Width="84px" TabIndex="1" MaxLength="3"
                                            CssClass="textBox" OnTextChanged="txtCommodityID_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <asp:Button ID="btnHelpCommodCode" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                        <asp:Label ID="lblCommodity" runat="server" CssClass="elementLabel"> </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="10%" align="right">
                                        <span class="elementLabel">Country Code :</span>
                                    </td>
                                    <td align="LEFT" nowrap>
                                        <asp:TextBox ID="txtCountryCode" runat="server" Width="20px" TabIndex="1" MaxLength="2"
                                            CssClass="textBox" OnTextChanged="txtCountryCode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <asp:Button ID="btnHelpCountryCode" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                        <asp:Label ID="lblCountryCode" runat="server" CssClass="elementLabel"> </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="10%" align="right">
                                        <span class="elementLabel">AWB/BL Date :</span>
                                    </td>
                                    <td align="left" nowrap>
                                        <asp:TextBox ID="txtAsOnDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                            Width="70px" TabIndex="2"></asp:TextBox>
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date"
                                            runat="server" TargetControlID="txtAsOnDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                            CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                            CultureTimePlaceholder=":" Enabled="True">
                                        </ajaxToolkit:MaskedEditExtender>
                                        <asp:Button ID="Button1" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtAsOnDate" PopupButtonID="Button1" Enabled="True">
                                        </ajaxToolkit:CalendarExtender>
                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender2"
                                            ValidationGroup="dtVal" ControlToValidate="txtAsOnDate" EmptyValueMessage="Enter Date Value"
                                            InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="10%" align="right">
                                        <span class="elementLabel">Due Date :</span>
                                    </td>
                                    <td align="left" nowrap>
                                        <asp:TextBox ID="txtDueDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                                            Width="70px" TabIndex="2"></asp:TextBox>
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" Mask="99/99/9999" MaskType="Date"
                                            runat="server" TargetControlID="txtDueDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                            CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                            CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                            CultureTimePlaceholder=":" Enabled="True">
                                        </ajaxToolkit:MaskedEditExtender>
                                        <asp:Button ID="Button2" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtDueDate" PopupButtonID="Button2" Enabled="True">
                                        </ajaxToolkit:CalendarExtender>
                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender3"
                                            ValidationGroup="dtVal" ControlToValidate="txtDueDate" EmptyValueMessage="Enter Date Value"
                                            InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" nowrap>
                                        <span class="mandatoryField">*</span><span class="elementLabel">Overseas Party ID :</span>
                                    </td>
                                    <td align="left" nowrap>
                                        <asp:TextBox ID="txtOverseasPartyID" runat="server" AutoPostBack="True" CssClass="textBox"
                                            MaxLength="5" onkeydown="OpenOverseasPartyList(this);" TabIndex="2" Width="70px"
                                            ToolTip="Press F2 for help." OnTextChanged="txtOverseasPartyID_TextChanged"></asp:TextBox><asp:Button
                                                ID="btnOverseasPartyList" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" /><asp:Label
                                                    ID="lblOverseasPartyDesc" runat="server" CssClass="elementLabel" Width="200px"></asp:Label>
                                        <asp:Button ID="btnOverseasParty" Style="display: none;" runat="server" OnClick="btnOverseasParty_Click" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <%--<center>
                                <table cellspacing="0" border="1" width="70%">
                                    <tr>
                                        <td colspan="6" align="right">
                                            <span class="mandatoryField">GR/PP/Customs Details</span>
                                        </td>
                                        <td colspan="5" align="right">
                                            <span class="mandatoryField">Please ensure to click on <b style="color: #007DFB;">Add</b>
                                                button before you Save the Bill record.</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="10%">
                                            <span class="elementLabel">Form Type</span>
                                        </td>
                                        <td width="15%" nowrap>
                                            <span class="elementLabel">GR/PP/Customs No.</span>
                                        </td>
                                        <td width="5%" nowrap>
                                            <span class="elementLabel">Currency</span>
                                        </td>
                                        <td width="15%" nowrap>
                                            <span class="elementLabel">Amount</span>
                                        </td>
                                        <td width="15%" nowrap>
                                            <span class="elementLabel">Exch. Rate</span>
                                        </td>
                                        <td width="15%" nowrap>
                                            <span class="elementLabel">Amount In INR</span>
                                        </td>
                                        <td width="15%" nowrap>
                                            <span class="elementLabel">Ship. Bill No</span>
                                        </td>
                                        <td width="10%" nowrap>
                                            <span class="elementLabel">Ship. Bill Date</span>
                                        </td>
                                        <td width="10%">
                                            <span class="elementLabel">Commission</span>
                                        </td>
                                        <td width="10%">
                                            <span class="elementLabel">Port Code</span>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="margin-left: 40px">
                                            <asp:DropDownList ID="ddlFormTypeGrid" runat="server" CssClass="dropdownList" TabIndex="3"
                                                Width="100px">
                                                <asp:ListItem Value="GR">GR</asp:ListItem>
                                                <asp:ListItem Value="SDF">SDF</asp:ListItem>
                                                <asp:ListItem Value="SOFTEX">SOFTEX</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtGRPPCustomsNo" runat="server" Width="140px" TabIndex="4" MaxLength="30"
                                                CssClass="textBox"></asp:TextBox>
                                        </td>
                                        <td id="Td1" align="left" runat="server">
                                            <asp:DropDownList ID="ddlCurrencyGRPP" runat="server" TabIndex="5" Width="100px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtAmountGRPP" runat="server" Width="100px" TabIndex="6" MaxLength="20"
                                                CssClass="textBox" onfocus="this.select();" Style="text-align: right"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtGRppExchRate" runat="server" Width="100px" TabIndex="7" MaxLength="20"
                                                CssClass="textBox" onfocus="this.select();" Style="text-align: right"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtAmountGRPP_In_INR" runat="server" Width="100px" TabIndex="8"
                                                MaxLength="20" CssClass="textBox" onfocus="this.select();" Style="text-align: right"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtShippingBillNo" runat="server" Width="100px" TabIndex="9" MaxLength="30"
                                                CssClass="textBox"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtShippingBillDate" runat="server" Width="80px" TabIndex="10" CssClass="textBox"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender ID="mdShippingDate" Mask="99/99/9999" MaskType="Date"
                                                runat="server" TargetControlID="txtShippingBillDate" ErrorTooltipEnabled="True"
                                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                CultureDateFormat="DMY" CultureDatePlaceholder="/" CultureDecimalPlaceholder="."
                                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":" Enabled="True">
                                            </ajaxToolkit:MaskedEditExtender>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtCommissionGRPP" runat="server" Width="100px" TabIndex="11" MaxLength="20"
                                                CssClass="textBox" Style="text-align: right"></asp:TextBox>
                                        </td>
                                        <td align="left" nowrap>
                                            <asp:DropDownList ID="ddlPortCode" runat="server" Width="100px" TabIndex="12">
                                            </asp:DropDownList>
                                            <asp:Button ID="btnPortCode" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnAddGRPPCustoms" runat="server" Text="Add" CssClass="buttonDefault"
                                                TabIndex="13" OnClick="btnAddGRPPCustoms_Click" />
                                        </td>
                                    </tr>
                                    <table>
                                        <tr>
                                            <asp:GridView ID="GridViewGRPPCustomsDetails" runat="server" AutoGenerateColumns="false"
                                                Width="90%" GridLines="Both" AllowPaging="true" PageSize="20" OnRowDataBound="GridViewGRPPCustomsDetails_RowDataBound">
                                                <PagerSettings Visible="false" />
                                                <RowStyle Wrap="false" HorizontalAlign="Left" Height="18px" VerticalAlign="Top" CssClass="gridItem" />
                                                <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
                                                <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                                    CssClass="gridAlternateItem" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SrNo") %>' CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                                        <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Form Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFormType" runat="server" Text='<%# Eval("FormType") %>' CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="GR/PP/Customs No." HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGR" runat="server" Text='<%# Eval("GR") %>' CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Currency" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="10%" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCurrency" runat="server" Text='<%# Eval("GRCurrency") %>' CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount","{0:0.00}") %>'
                                                                CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Exch. Rate" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblExchRate" runat="server" Text='<%# Eval("ExchRate","{0:0.00}") %>'
                                                                CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount In INR" HeaderStyle-HorizontalAlign="Right"
                                                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmountInInr" runat="server" Text='<%# Eval("AmtinINR","{0:0.00}") %>'
                                                                CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ship. Bill No." HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblShippingBillNo" runat="server" Text='<%# Eval("Shipping_Bill_No") %>'
                                                                CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ship. Bill Date" HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblShippingBillDate" runat="server" Text='<%# Eval("Shipping_Bill_Date","{0:dd/MM/yyyy}") %>'
                                                                CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Commission" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCommission" runat="server" Text='<%# Eval("Commission","{0:0.00}") %>'
                                                                CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Port Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPortCode" runat="server" Text='<%# Eval("PortCode") %>' CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnDelete" runat="server" CommandArgument='<%# Eval("SrNo") %>' CommandName="RemoveRecord"
                                                                CssClass="deleteButton" OnClick="LinkButtonClick" Text="Delete" ToolTip="Delete Record" /></ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                    </table>
                            </center>--%>
                            <center>
                                <table cellspacing="0" border="1" width="75%">
                                    <tr>
                                        <td colspan="6">
                                            <span class="elementLabel">GR/PP/Customs Details</span>
                                        </td>
                                        <td colspan="5" align="right">
                                            <span class="mandatoryField">Please ensure to click on <b style="color: #007DFB;">Add</b>
                                                button before you Save the Bill record.</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="10%" nowrap>
                                            <span class="elementLabel">Type</span>
                                        </td>
                                        <td width="10%" nowrap>
                                            <span class="elementLabel">Export Agency</span>
                                        </td>
                                        <td width="10%" nowrap>
                                            <span class="elementLabel">Dispatch Ind.</span>
                                        </td>
                                        <td width="10%" nowrap>
                                            <span class="elementLabel">GR/PP/Cust.No.</span>
                                        </td>
                                        <td width="5%" nowrap>
                                            <span class="elementLabel">Currency</span>
                                        </td>
                                        <td width="10%" nowrap>
                                            <span class="elementLabel">Amount</span>
                                        </td>
                                       <%-- <td width="12%" nowrap>
                                            <span class="elementLabel">Exch Rate</span>
                                        </td>--%>
                                        <td width="10%" nowrap>
                                            <span class="elementLabel">Amt in INR</span>
                                        </td>
                                        <td width="10%" nowrap>
                                            <span class="elementLabel">Ship. Bill No</span>
                                        </td>
                                        <td width="10%" nowrap>
                                            <span class="elementLabel">Ship. Date</span>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlFormType" runat="server" CssClass="dropdownList" TabIndex="3"
                                                Width="70px">
                                                <asp:ListItem Text="GOODS" Value="GOODS"></asp:ListItem>
                                                <asp:ListItem Text="SOFTEX" Value="SOFTEX"></asp:ListItem>
                                                <asp:ListItem Text="ROYALTY" Value="ROYALTY"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlExportAgency" runat="server" CssClass="dropdownList" TabIndex="4"
                                                Width="84px">
                                                <asp:ListItem Value="Customs">Customs</asp:ListItem>
                                                <asp:ListItem Value="SEZ">SEZ</asp:ListItem>
                                                <asp:ListItem Value="STPI">STPI</asp:ListItem>
                                                <asp:ListItem Value="Status holder exporters">Status holder exporters</asp:ListItem>
                                                <asp:ListItem Value="100% EOU">100% EOU</asp:ListItem>
                                                <asp:ListItem Value="Warehouse export">Warehouse export</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" style="margin-left: 40px">
                                            <asp:DropDownList ID="ddlDispachInd" runat="server" CssClass="dropdownList" TabIndex="5"
                                                Width="100px">
                                                <asp:ListItem Value="Dispatched directly by exporter">By Exporter</asp:ListItem>
                                                <asp:ListItem Value="By Bank">By Bank</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtGRPPCustomsNo" runat="server" Width="70px" TabIndex="6" MaxLength="30"
                                                CssClass="textBox"></asp:TextBox>
                                        </td>
                                        <td id="Td1" align="left" runat="server">
                                            <asp:DropDownList ID="ddlCurrencyGRPP" runat="server" CssClass="dropdownList" TabIndex="7">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtAmountGRPP" runat="server" Width="85px" TabIndex="8" MaxLength="20"
                                                onfocus="this.select()" CssClass="textBox" Style="text-align: right"></asp:TextBox>
                                        </td>
                                        <%--<td align="left">
                                            <asp:TextBox ID="txtExchRateGR" runat="server" Width="90px" TabIndex="9" MaxLength="20"
                                                onfocus="this.select()" CssClass="textBox" Style="text-align: right"></asp:TextBox>
                                        </td>--%>
                                        <td align="left">
                                            <asp:TextBox ID="txtAmountinINRGR" runat="server" Width="90px" TabIndex="10" MaxLength="20"
                                                onfocus="this.select()" CssClass="textBox" Style="text-align: right"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtShippingBillNo" runat="server" Width="70px" TabIndex="11" MaxLength="30"
                                                CssClass="textBox"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtShippingBillDate" runat="server" Width="70px" TabIndex="12" CssClass="textBox"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender ID="mdShippingDate" Mask="99/99/9999" MaskType="Date"
                                                runat="server" TargetControlID="txtShippingBillDate" ErrorTooltipEnabled="True"
                                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                CultureDateFormat="DMY" CultureDatePlaceholder="/" CultureDecimalPlaceholder="."
                                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":" Enabled="True">
                                            </ajaxToolkit:MaskedEditExtender>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="10%" nowrap>
                                            <span class="elementLabel">Invoice No.</span>
                                        </td>
                                        <td width="10%" nowrap>
                                            <span class="elementLabel">Invoice Date</span>
                                        </td>
                                        <td width="10%" nowrap>
                                            <span class="elementLabel">Invoice Amt</span>
                                        </td>
                                        <td width="10%" nowrap>
                                            <span class="elementLabel">Freight Amt</span>
                                        </td>
                                        <td width="10%" nowrap>
                                            <span class="elementLabel">Insurance Amt</span>
                                        </td>
                                        <td width="10%">
                                            <span class="elementLabel">Discount Amt</span>
                                        </td>
                                        <td width="10%">
                                            <span class="elementLabel">Comm. Amt.</span>
                                        </td>
                                        <td width="10%">
                                            <span class="elementLabel">Oth. Ded. Chrgs</span>
                                        </td>
                                        <td width="10%">
                                            <span class="elementLabel">Packing Chrgs</span>
                                        </td>
                                        <td width="10%">
                                            <span class="elementLabel">Port Code</span>
                                        </td>
                                        <td>
                                        </td>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtInvoiceNum" runat="server" Width="75px" TabIndex="13" MaxLength="30"
                                                    CssClass="textBox"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtInvoiceDt" runat="server" Width="70px" TabIndex="14" MaxLength="10"
                                                    CssClass="textBox"></asp:TextBox>
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender11" Mask="99/99/9999" MaskType="Date"
                                                    runat="server" TargetControlID="txtInvoiceDt" InputDirection="RightToLeft" AcceptNegative="Left"
                                                    ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                                </ajaxToolkit:MaskedEditExtender>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtInvoiceAmt" runat="server" Width="85px" TabIndex="15" MaxLength="20"
                                                    onfocus="this.select()" CssClass="textBox" Style="text-align: right"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtFreight" runat="server" CssClass="textBox" Width="80px" TabIndex="16"
                                                    onfocus="this.select()" Style="text-align: right"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtInsurance" runat="server" CssClass="textBox" Width="80px" TabIndex="17"
                                                    onfocus="this.select()" Style="text-align: right"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtDiscount" runat="server" CssClass="textBox" Width="80px" TabIndex="18"
                                                    onfocus="this.select()" Style="text-align: right"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtCommissionGRPP" runat="server" Width="80px" TabIndex="19" MaxLength="20"
                                                    CssClass="textBox" onfocus="this.select()" Style="text-align: right"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtOthDeduction" runat="server" CssClass="textBox" Width="80px"
                                                    TabIndex="20" onfocus="this.select()" Style="text-align: right"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtPacking" runat="server" CssClass="textBox" Width="80px" TabIndex="20"
                                                    onfocus="this.select()" Style="text-align: right"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlPortCode" runat="server" CssClass="dropdownList" TabIndex="21"
                                                    Width="70px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnAddGRPPCustoms" runat="server" Text="Add" CssClass="buttonDefault"
                                                    TabIndex="22" OnClick="btnAddGRPPCustoms_Click" />
                                            </td>
                                        </tr>
                                    <tr>
                                        <asp:GridView ID="GridViewGRPPCustomsDetails" runat="server" AutoGenerateColumns="false"
                                            Width="90%" GridLines="Both" AllowPaging="true" PageSize="20" OnRowDataBound="GridViewGRPPCustomsDetails_RowDataBound">
                                            <PagerSettings Visible="false" />
                                            <RowStyle Wrap="false" HorizontalAlign="Left" Height="18px" VerticalAlign="Top" CssClass="gridItem" />
                                            <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
                                            <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                                CssClass="gridAlternateItem" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SrNo") %>' CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                                    <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFormType" runat="server" Text='<%# Eval("FormType") %>' CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Exp. Agency" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblExpAgency" runat="server" Text='<%# Eval("ExportAgency") %>' CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Disp. Ind." HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="8%" ItemStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDispInd" runat="server" Text='<%# Eval("DispInd") %>' CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="8%" />
                                                    <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cust. No." HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGR" runat="server" Text='<%# Eval("GR") %>' CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Curr." HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCurrency" runat="server" Text='<%# Eval("GRCurrency") %>' CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                                    <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ship.Amt." HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                    HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount","{0:0.00}") %>'
                                                            CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                    <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Exch Rate" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                    HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblExchRate" runat="server" Text='<%# Eval("ExchRate","{0:0.00}") %>'
                                                            CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                    <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Amt in INR" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                    HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmountinINR" runat="server" Text='<%# Eval("AmtinINR","{0:0.00}") %>'
                                                            CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                    <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Shipp. Bill No." HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblShippingBillNo" runat="server" Text='<%# Eval("Shipping_Bill_No") %>'
                                                            CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ship. Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblShippingBillDate" runat="server" Text='<%# Eval("Shipping_Bill_Date","{0:dd/MM/yyyy}") %>'
                                                            CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Invoice No." HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInvoiceNo" runat="server" Text='<%# Eval("InvoiceNo") %>' CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Invoice Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInvoiceDate" runat="server" Text='<%# Eval("InvoiceDate","{0:dd/MM/yyyy}") %>'
                                                            CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Inv.Amt" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                    HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInvoiceAmt" runat="server" Text='<%# Eval("InvoiceAmt","{0:0.00}") %>'
                                                            CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                    <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Frgt.Amt" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                    HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFreightAmt" runat="server" Text='<%# Eval("FreightAmount","{0:0.00}") %>'
                                                            CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                    <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Insur.Amt" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                    HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInsuranceAmt" runat="server" Text='<%# Eval("InsuranceAmount","{0:0.00}") %>'
                                                            CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                    <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dis.Amt" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                    HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDiscountAmt" runat="server" Text='<%# Eval("DiscountAmt","{0:0.00}") %>'
                                                            CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                    <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Comm." HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right"
                                                    HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCommission" runat="server" Text='<%# Eval("Commission","{0:0.00}") %>'
                                                            CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                    <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Oth Ded Charges" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOthDeduction" runat="server" Text='<%# Eval("OtherDeductionAmt","{0:0.00}") %>'
                                                            CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                    <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pack. Chrgs" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right"
                                                    HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPacking" runat="server" Text='<%# Eval("PackingCharges","{0:0.00}") %>'
                                                            CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                    <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Port Code" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPortCode" runat="server" Text='<%# Eval("PortCode") %>' CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnDelete" runat="server" CommandArgument='<%# Eval("SrNo") %>' CommandName="RemoveRecord"
                                                            CssClass="deleteButton" OnClick="LinkButtonClick" Text="Delete" ToolTip="Delete Record" /></ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </tr>
                                </table>
                            </center>
                            <br />
                            <table cellspacing="0" cellpadding="0" border="0" width="400px" style="line-height: 150%">
                                <tr>
                                    <td align="right" style="width: 110px">
                                    </td>
                                    <td align="left" style="width: 290px; padding-top: 10px; padding-bottom: 10px">
                                        &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                            ToolTip="Save" TabIndex="23" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                            ToolTip="Cancel" TabIndex="24" OnClick="btnCancel_Click" />
                                    </td>
                                </tr>
                            </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
    <script language="javascript" type="text/javascript">
        window.onload = function () {

            var txtAmountGRPP = document.getElementById('txtAmountGRPP');
            if (txtAmountGRPP.value == '')
                txtAmountGRPP.value = 0;
            txtAmountGRPP.value = parseFloat(txtAmountGRPP.value).toFixed(2);

            var txtGRppExchRate = document.getElementById('txtExchRateGR');
            if (txtGRppExchRate.value == '')
                txtGRppExchRate.value = 0;
            txtGRppExchRate.value = parseFloat(txtGRppExchRate.value).toFixed(10);

            var txtAmountGRPP_In_INR = document.getElementById('txtAmountinINRGR');
            if (txtAmountGRPP_In_INR.value == '')
                txtAmountGRPP_In_INR.value = 0;
            txtAmountGRPP_In_INR.value = parseFloat(txtAmountGRPP_In_INR.value).toFixed(2);
        }
    </script>
</body>
</html>
