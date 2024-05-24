<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMPWH_PaymentFileUpload.aspx.cs"
    Inherits="ImportWareHousing_FIleUpload_TF_IMPWH_PaymentFileUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/Style_new.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../Scripts/jquery.min.js"></script>
    <script type="text/javascript" language="javascript">
        function ValidateProcess() {
            var ORMAmt = document.getElementById('lblOrmAmount').innerText;
            var InvCurr = document.getElementById('hdnInvCurr').value;
            var PayCurr = document.getElementById('hdnPayCurr').value;
            var ExchangeRate = document.getElementById('txtExchangeRate').value;
            var TotalPpayAmt = document.getElementById('lblTotPayAmt').innerText;
            if (TotalPpayAmt > ORMAmt) {
                alert('Total Payment Amount is greater than Balance ORM Amount.');
                return false;
            }
            if (InvCurr != PayCurr) {
                if (ExchangeRate == '') {
                    alert('Exchange Rate can not be blank for cross currency.');
                    document.getElementById('txtExchangeRate').focus();
                    return false;
                }
            }
        }
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        function Cust_Help() {
            var brname = document.getElementById('ddlBranch');

            if (brname.value == "---Select---") {
                alert('Please select branchname');
                brname.focus();
                return false;

            }
            popup = window.open('../HelpForms/TF_IMPWH_ImportCustomer.aspx', 'CustHelp', 'height=500,  width=450,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "CustHelp";
        }
        function selectCustomer(Cust, label) {
            document.getElementById('txtPartyID').value = Cust;
            document.getElementById('lblCustName').innerText = label;
            __doPostBack("txtPartyID", "TextChanged");
        }
        function OTT_Help() {

            if (document.getElementById('txtPartyID').value == '') {
                alert('Customer ID Can not be blank.');
                document.getElementById('txtPartyID').focus();
                return false;
            }
            var e = document.getElementById("ddlBranch");
            var branch = e.options[e.selectedIndex].text;
            //var year = document.getElementById('txtYear').value;
            var iecode = document.getElementById('txtPartyID').value;
            popup = window.open('../../IDPMS/OTT_Help.aspx?branch=' + branch + '&iecode=' + iecode, 'CustList', 'height=500,  width=450,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            return false;
        }
        function selectOtt(acno, date, amt, curr) {
            document.getElementById('txtORMNo').value = acno;
            document.getElementById('lblOrmAmount').innerHTML = amt;
            document.getElementById('lblcurren').innerHTML = curr;
            //__doPostBack("txtORMNo", "TextChanged");
            //javascript: setTimeout('__doPostBack(\'txtORMNo\',\'\')', 0)
        }

        function isNumberKey(evt, obj) {

            var charCode = (evt.which) ? evt.which : event.keyCode
            var value = obj.value;
            var dotcontains = value.indexOf(".") != -1;
            if (dotcontains)
                if (charCode == 46) return false;
            if (charCode == 46) return true;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
        function Calculate() {
            var sum = 0;
            $('#GridViewInvoice tr').not(':first').each(function () {

                sum += parseFloat($(this).find('td:nth-child(9)').find("[type='text']").val());
            });
            $("#lblTotPayAmt").text(sum);
        }

        function Search_Gridview(strKey, strGV) {
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById(strGV);
            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }    
    </script>
    <style type="text/css">
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: transparent;
            z-index: 99;
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
        .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid navy;
            width: 400px;
            height: 40px;
            display: none;
            position: absolute;
            background-color: white;
            z-index: 999;
        }
        
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
        
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=40);
            opacity: 0.4;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            width: 400px;
            border: 3px solid navy;
        }
        .modalPopup .header
        {
            background-color: navy;
            height: 30px;
            color: White;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
        }
        .modalPopup .body
        {
            padding-top: 5px;
            min-height: 100px;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
        }
        .modalPopup .footerAjaxModel
        {
            padding-bottom: 5px;
        }
        .modalPopup .Yes, .modalPopup .No
        {
            margin: 0 1px 0 0;
            padding: 4px 10px;
            width: 100px;
            background: #007DFB;
            color: #FFF;
            text-align: center;
            text-decoration: none;
            border: 0;
            cursor: pointer;
            font-size: 11px;
        }
        .modalPopup .Yes
        {
            background-color: #2FBDF1;
            border: 1px solid #0DA9D0;
        }
        .modalPopup .No
        {
            background-color: #2FBDF1;
            border: 1px solid #0DA9D0;
        }
    </style>
</head>
<body onload="EndRequest();closeWindows();" onunload="closeWindows();">
    <form id="form1" runat="server" autocomplete="off">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
        PopupControlID="pnlPopup" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground"
        CancelControlID="btnNo">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
        <div class="header">
            Confirmation
        </div>
        <div class="body">
            This File Already Uploaded Do you want to Upload It Again?
        </div>
        <div class="footerAjaxModel" align="center">
            <asp:Button ID="btnYes" runat="server" CssClass="Yes" Text="Yes" OnClick="btnYes_Click" />
            <asp:Button ID="btnNo" runat="server" Text="No" CssClass="No" />
        </div>
    </asp:Panel>
    <script language="javascript" type="text/javascript" src="../../Scripts/Enable_Disable_Opener.js"></script>
    <script language="javascript" type="text/javascript" src="../../Scripts/InitEndRequest.js"></script>
    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="../../Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div class="loading" align="center">
        Uploading file. Please wait.
        <br />
        <img src="../../Images/ProgressBar1.gif" alt="" />
    </div>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <Triggers>
                    <%--<asp:PostBackTrigger ControlID="btnUpldCSV" />--%>
                    <asp:PostBackTrigger ControlID="btnUpload" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <input type="hidden" id="hdnCustId" runat="server" />
                            <input type="hidden" id="hdnRecordCount" runat="server" />
                            <input type="hidden" id="hdnStatus" runat="server" />
                            <input type="hidden" id="hdnFilePath" runat="server" />
                            <input type="hidden" id="hdnFileExtension" runat="server" />
                            <input type="hidden" id="hdnFileName" runat="server" />
                            <input type="hidden" id="hdnInvCurr" runat="server" />
                            <input type="hidden" id="hdnPayCurr" runat="server" />
                            <td align="left" style="width: 50%; font-weight: bold" valign="bottom">
                                &nbsp;<span class="pageLabel" style="font-weight: bold">Payment Authorization File Upload</span>
                            </td>
                            <td align="right" style="width: 50%" colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="3">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%;" valign="top" colspan="2">
                                <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 150%">
                                    <tr>
                                        <td width="5%" align="right">
                                            <span class="elementLabel">Branch :</span>
                                        </td>
                                        <td align="left" style="white-space: nowrap" colspan="2">
                                            &nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" AutoPostBack="true"
                                                Width="100px" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="white-space: nowrap">
                                            <span class="elementLabel"><span class="mandatoryField">* </span>Customer A/C No. :</span>
                                        </td>
                                        <td align="left" style="white-space: nowrap" colspan="2">
                                            &nbsp;<asp:TextBox ID="txtPartyID" runat="server" Width="110px" TabIndex="1" CssClass="textBox"
                                                AutoPostBack="true" OnTextChanged="txtPartyID_TextChanged"></asp:TextBox>
                                            <asp:Button ID="btnPartyID" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                            <asp:Label ID="lblCustName" runat="server" CssClass="elementLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <span class="elementLabel"><span class="mandatoryField">* </span>ORM No :</span>
                                        </td>
                                        <td style="white-space: nowrap; text-align: left" colspan="2">
                                            &nbsp;<asp:TextBox ID="txtORMNo" Width="140px" runat="server" CssClass="textBox"
                                                TabIndex="2" AutoPostBack="true" OnTextChanged="txtORMNo_TextChanged"> </asp:TextBox>
                                            <asp:Button ID="btnHelp_DocNo" runat="server" CssClass="btnHelp_enabled" />
                                            &nbsp;
                                            <asp:Label ID="ORMamt" runat="server" CssClass="elementLabel" Text="ORM Amount:"></asp:Label>
                                            <asp:Label ID="lblcurren" runat="server" CssClass="elementLabel"></asp:Label>
                                            &nbsp;
                                            <asp:Label ID="lblOrmAmount" runat="server" CssClass="elementLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 10%">
                                            <span class="elementLabel">Exchange Rate :</span>
                                        </td>
                                        <td style="width: 90%" colspan="2">
                                            &nbsp;<asp:TextBox ID="txtExchangeRate" runat="server" CssClass="textBox" MaxLength="10"
                                                Width="50px" TabIndex="2"></asp:TextBox>
                                            &nbsp;<asp:DropDownList ID="ddlExchangeRateSign" CssClass="dropdownList" Width="100px"
                                                runat="server">
                                                <asp:ListItem Text="/" Value="D"></asp:ListItem>
                                                <asp:ListItem Text="*" Value="M"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td align="right" style="width: 10%">
                                            <span class="mandatoryField">*</span>&nbsp;<span class="elementLabel">Select File :</span>
                                        </td>
                                        <td align="left" style="width: 90%" colspan="2">
                                            &nbsp;<asp:FileUpload ID="fileinhouse" runat="server" ViewStateMode="Enabled" TabIndex="3"
                                                Width="500px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 10%">
                                            <span class="elementLabel">Input File :</span>
                                        </td>
                                        <td align="left" style="width: 90%" colspan="2">
                                            &nbsp;<asp:TextBox ID="txtInputFile" runat="server" CssClass="textBox" MaxLength="100"
                                                Width="250px" TabIndex="2" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 10%">
                                        </td>
                                        <td align="left" style="width: 90%" colspan="2">
                                            <table border="0" style="border-color: red" cellpadding="2" width="100%">
                                                <tr>
                                                    <td width="20%" nowrap="nowrap">
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Button ID="btnUpload" runat="server" Text="Upload Excel File" CssClass="buttonDefault"
                                                                    ToolTip="Upload Excel File" TabIndex="4" OnClick="btnUpload_Click" />
                                                                <asp:Label ID="labelMessage" runat="server" CssClass="elementLabel"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td width="20%" nowrap="nowrap">
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Button ID="btnProcess" CssClass="buttonDefault" Text="Process" runat="server"
                                                                    OnClick="btnProcess_Click" Visible="false" />
                                                                <asp:Label ID="label1" runat="server" CssClass="elementLabel"></asp:Label>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td width="40%" nowrap="nowrap" align="right">
                                                        <asp:Label ID="lblSearch" runat="server" CssClass="elementLabel" Text="Search :"
                                                            Visible="false"></asp:Label>
                                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="textBox" MaxLength="100" Width="150px"
                                                            TabIndex="2" Visible="false" onkeyup="Search_Gridview(this, 'GridViewInvoice')"></asp:TextBox>
                                                    </td>
                                                    <td width="20%" nowrap="nowrap" align="right">
                                                        <asp:Label ID="lblPayAmtTotal" runat="server" CssClass="elementLabel" Text="Payment Amt :"
                                                            Visible="false"></asp:Label>
                                                        <asp:Label ID="lblTotPayAmt" runat="server" CssClass="elementLabel"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="rowGrid" runat="server">
                                        <td align="left" style="width: 94%;" valign="top" colspan="3">
                                            <table cellspacing="0" cellpadding="0" border="0" style="line-height: 100%; width: 100%;">
                                                <tr id="Tr1" runat="server">
                                                    <td align="left" style="width: 100%; padding-left: 1%; padding-right: 1%" valign="top"
                                                        rowspan="1">
                                                        <asp:GridView ID="GridViewInvoice" runat="server" Width="100%" AutoGenerateColumns="False"
                                                            HeaderStyle-Height="10px" RowStyle-Height="10px" CssClass="GridView" OnRowDataBound="GridViewInvoice_RowDataBound"
                                                            AllowPaging="True" PageSize="50">
                                                            <PagerSettings Visible="false" />
                                                            <RowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="top" CssClass="gridItem" />
                                                            <HeaderStyle ForeColor="#1a60a6" VerticalAlign="Top" CssClass="gridHeader" />
                                                            <AlternatingRowStyle Wrap="False" HorizontalAlign="Left" Height="18px" VerticalAlign="Middle"
                                                                CssClass="gridAlternateItem" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Supplier Name" HeaderStyle-HorizontalAlign="Left"
                                                                    HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle"
                                                                    HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSupplierName" runat="server" Text='<%#Eval("Supplier_Name")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="BOE No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                    HeaderStyle-Width="4%" ItemStyle-Width="4%" ItemStyle-VerticalAlign="Middle"
                                                                    HeaderStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBOENo" Text='<%# Eval("BOE_No") %>' runat="server"></asp:Label>
                                                                        <asp:Label ID="lblIECode" Text='<%# Eval("IECode") %>' CssClass="elementLabel" runat="server"
                                                                            Visible="false"></asp:Label>
                                                                        <asp:Label ID="LabelPayment_Term" Text='<%# Eval("Payment_Term") %>' CssClass="elementLabel"
                                                                            runat="server" Visible="false"></asp:Label>
                                                                        <asp:Label ID="LabelPayment_Due_Date" Text='<%# Eval("Payment_Due_Date") %>' CssClass="elementLabel"
                                                                            runat="server" Visible="false"></asp:Label>
                                                                        <asp:Label ID="LabelThird_Party_Name" Text='<%# Eval("Third_Party_Name") %>' CssClass="elementLabel"
                                                                            runat="server" Visible="false"></asp:Label>
                                                                        <asp:Label ID="LabelMaximum_Due_Date" Text='<%# Eval("Maximum_Due_Date") %>' CssClass="elementLabel"
                                                                            runat="server" Visible="false"></asp:Label>
                                                                        <asp:Label ID="LabelDate_Of_Shipping" Text='<%# Eval("Date_Of_Shipping") %>' CssClass="elementLabel"
                                                                            runat="server" Visible="false"></asp:Label>
                                                                        <asp:Label ID="LabelSupplier_Country" Text='<%# Eval("Supplier_Country") %>' CssClass="elementLabel"
                                                                            runat="server" Visible="false"></asp:Label>
                                                                        <asp:Label ID="LabelThird_Party_Country" Text='<%# Eval("Third_Party_Country") %>'
                                                                            CssClass="elementLabel" runat="server" Visible="false"></asp:Label>
                                                                        <asp:Label ID="LabelName_Of_Shipping_Comp" Text='<%# Eval("Name_Of_Shipping_Comp") %>'
                                                                            CssClass="elementLabel" runat="server" Visible="false"></asp:Label>
                                                                        <asp:Label ID="LabelVessel_Air_Carrier_Name" Text='<%# Eval("Vessel_Air_Carrier_Name") %>'
                                                                            CssClass="elementLabel" runat="server" Visible="false"></asp:Label>
                                                                        <asp:Label ID="LabelTransport_Doc_No" Text='<%# Eval("Transport_Doc_No") %>' CssClass="elementLabel"
                                                                            runat="server" Visible="false"></asp:Label>
                                                                        <asp:Label ID="LabelPort_Of_Discharge" Text='<%# Eval("Port_Of_Discharge") %>' CssClass="elementLabel"
                                                                            runat="server" Visible="false"></asp:Label>
                                                                        <asp:Label ID="LabelPort_Of_Loading" Text='<%# Eval("Port_Of_Loading") %>' CssClass="elementLabel"
                                                                            runat="server" Visible="false"></asp:Label>
                                                                        <asp:Label ID="LabelRawMaterial_CapitalGoods" Text='<%# Eval("RawMaterial_CapitalGoods") %>'
                                                                            CssClass="elementLabel" runat="server" Visible="false"></asp:Label>
                                                                        <asp:Label ID="LabelGoods_Description" Text='<%# Eval("Goods_Description") %>' CssClass="elementLabel"
                                                                            runat="server" Visible="false"></asp:Label>
                                                                        <asp:Label ID="LabelCountry_Of_Origin" Text='<%# Eval("Country_Of_Origin") %>' CssClass="elementLabel"
                                                                            runat="server" Visible="false"></asp:Label>
                                                                        <asp:Label ID="LabelAD_Code" Text='<%# Eval("AD_Code") %>' CssClass="elementLabel"
                                                                            runat="server" Visible="false"></asp:Label>
                                                                        <asp:Label ID="LabelFileName" Text='<%# Eval("FileName") %>' CssClass="elementLabel"
                                                                            runat="server" Visible="false"></asp:Label>
                                                                        <asp:Label ID="LabelOut_Standing_Amount" Text='<%# Eval("Out_Standing_Amount") %>'
                                                                            CssClass="elementLabel" runat="server" Visible="false"></asp:Label>
                                                                        <asp:Label ID="LabelExchRate" Text='<%# Eval("ExchRate") %>' CssClass="elementLabel"
                                                                            runat="server" Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="BOE Date" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                                    HeaderStyle-Width="4%" ItemStyle-Width="4%" ItemStyle-VerticalAlign="Middle"
                                                                    HeaderStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBOEDate" runat="server" Text='<%#Eval("BOE_Date")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Remark" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                    HeaderStyle-Width="10%" ItemStyle-Width="10%" ItemStyle-VerticalAlign="Middle"
                                                                    HeaderStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRemark" runat="server" Text='<%#Eval("Remark")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Invoice No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                    HeaderStyle-Width="5%" ItemStyle-Width="5%" ItemStyle-VerticalAlign="Middle"
                                                                    HeaderStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblInvoiceNo" runat="server" Text='<%#Eval("Invoice_No")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Invoice Curr" HeaderStyle-HorizontalAlign="Center"
                                                                    HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="4%"
                                                                    ItemStyle-Width="4%" ItemStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblInvoiceCurr" runat="server" Text='<%#Eval("Invoice_Curr")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Invoice Amt" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                                    HeaderStyle-Width="6%" ItemStyle-Width="6%" ItemStyle-VerticalAlign="Middle"
                                                                    HeaderStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox runat="server" CssClass="textBox AlgRgh" Text='<%# Eval("Amount_To_Be_Remitted","{0:0.00}") %>'
                                                                            ID="lblinvcamt" Width="90%" MaxLength="20" ReadOnly="true" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Payment Curr" HeaderStyle-HorizontalAlign="Center"
                                                                    HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="7%"
                                                                    ItemStyle-Width="7%" ItemStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPaymentCurr" runat="server" Text='<%#Eval("PaymentCurrency")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Payment Amt" HeaderStyle-HorizontalAlign="Right" HeaderStyle-VerticalAlign="Middle"
                                                                    ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="6%" ItemStyle-Width="6%"
                                                                    ItemStyle-VerticalAlign="Middle">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox AutoPostBack="true" runat="server" onkeypress="return isNumberKey(event,this)"
                                                                            onchange="return Calculate();" OnTextChanged="txtPayAmt_textchange" CssClass="textBox AlgRgh"
                                                                            Text='<%# Eval("Payment_Amt","{0:0.00}") %>' ID="txtPayAmt" Width="90%" MaxLength="20" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="rowPager" runat="server">
                                        <td align="center" style="width: 100%;" valign="top" colspan="3" class="gridHeader">
                                            <table cellspacing="0" cellpadding="2" width="100%" border="0" class="gridHeader">
                                                <tbody>
                                                    <tr>
                                                        <td align="left" width="25%">
                                                            &nbsp;Records Per Page :&nbsp;
                                                            <asp:DropDownList ID="ddlrecordperpage" runat="server" CssClass="dropdownList" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlrecordperpage_SelectedIndexChanged">
                                                                <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                                                <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                                                <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="center" valign="top" width="50%">
                                                            <asp:Button ID="btnnavfirst" runat="server" Text="First" ToolTip="First" OnClick="btnnavfirst_Click" />
                                                            <asp:Button ID="btnnavpre" runat="server" Text="Prev" ToolTip="Previous" OnClick="btnnavpre_Click" />
                                                            <asp:Button ID="btnnavnext" runat="server" Text="Next" ToolTip="Next" OnClick="btnnavnext_Click" />
                                                            <asp:Button ID="btnnavlast" runat="server" Text="Last" ToolTip="Last" OnClick="btnnavlast_Click" />
                                                        </td>
                                                        <td align="right" style="width: 25%;">
                                                            &nbsp;<asp:Label ID="lblpageno" runat="server"></asp:Label>
                                                            &nbsp;
                                                            <asp:Label ID="lblrecordno" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="3">
                                <hr />
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
