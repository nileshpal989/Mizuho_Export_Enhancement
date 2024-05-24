<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDPMS_AddEdit_Bill_Details.aspx.cs"
    Inherits="EDPMS_EDPMS_AddEdit_Bill_Details" %>

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

        function Party_Help() {
            open_popup('Help_OverseasPartyCode.aspx', 400, 400, "DocFile");
            return true;
        }

        function SelectOverSeas(Uname) {
            document.getElementById('txtPartyID').value = Uname;
            document.getElementById('txtPartyID').focus();
        }


        function PortHelp() {
            open_popup('../XOS/Help_GRCustoms_Port_Code.aspx', 400, 400, "DocFile");
            return true;
        }

        function selectPort(Uname) {
            document.getElementById('ddlPortCodeGrid').value = Uname;
            document.getElementById('ddlPortCodeGrid').focus();
        }

        function ValidateSave() {
            if (document.getElementById('txtShipBillAmt').value != '') {
                alert('Please ensure click Add Button before Save Record.');
                return false;
            }
            return true;
        }

        function ValidateAdd() {
            if (document.getElementById('txtShippingBillNo').value == '') {
                alert('Shipping Bill No. Can not be blank.');
                return false;
            }
            if (document.getElementById('txtShippingBillDate').value == '') {
                alert('Shipping Bill Date Can not be blank.');
                return false;
            }
            if (document.getElementById('txtShipBillAmt').value == '') {
                alert('Shipping Bill Amt. Can not be blank.');
                return false;
            }
            if (document.getElementById('txtInvoiceNo').value == '') {
                alert('Invoice No. Can not be blank.');
                document.getElementById('txtInvoiceNo').focus();
                return false;
            }
            if (document.getElementById('txtInvoiceDate').value == '') {
                alert('Invoice Date Can not be blank.');
                document.getElementById('txtInvoiceDate').focus();
                return false;
            }
            if (document.getElementById('txtInvoiceAmt').value == '') {
                alert('Invoice Amt. Can not be blank.');
                document.getElementById('txtInvoiceAmt').focus();
                return false;
            }

            if (document.getElementById('ddlPortCodeGrid').value == '0') {
                alert('Please Select Port Code.');
                document.getElementById('ddlPortCodeGrid').focus();
                return false;
            }

            return true;
        }


        function HelpShippNo() {

            if (document.getElementById('txtDocumentNo').value == '') {
                alert('Document Number Can not be blank');
                return false;
            }

            var DocNo = document.getElementById('txtDocumentNo').value;
            open_popup('Help_Shipp_No.aspx?DocNo=' + DocNo, 400, 400, "DocFile");
            return true;
        }

        function selectShippNo(ShippNo, ShipAmt, ShippDate) {
            document.getElementById('txtShippingBillNo').value = ShippNo;
            document.getElementById('txtShippingBillDate').value = ShippDate;
            document.getElementById('txtShipBillAmt').value = ShipAmt;
        }

        function HelpDocNo() {
            var Branch = document.getElementById('hdnBranch').value;
            var Year = document.getElementById('hdnYear').value;

            open_popup('Help_Doc_No.aspx?Branch=' + Branch + '&Year=' + Year, 400, 400, "DocFile");


            return true;
        }
        function selectDocNo(DocNo, BillAmt, Curr) {
            document.getElementById('txtDocumentNo').value = DocNo;
            document.getElementById('txtBillAmount').value = BillAmt;
            document.getElementById('lblCurrency').innerHTML = Curr;

            __doPostBack('txtDocumentNo', '');

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
                            <asp:Button ID="btnGridValues" Style="display: none;" runat="server" OnClick="btnGridValues_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom">
                            <span class="pageLabel">Updation of EDPMS Bill Detalis</span>
                        </td>
                        <td align="right" style="width: 50%">
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                TabIndex="20" OnClick="btnBack_Click" />
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
                                        <asp:TextBox ID="txtDocumentNo" Width="140px" runat="server" CssClass="textBox" Enabled="false"
                                            TabIndex="-1" OnTextChanged="txtDocumentNo_TextChanged" AutoPostBack="true"> </asp:TextBox>&nbsp;
                                        <asp:Button ID="btnHelp_DocNo" runat="server" CssClass="btnHelp_enabled" Visible="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" nowrap>
                                        <span class="elementLabel">Bill Amount :</span>
                                    </td>
                                    <td align="left" nowrap>
                                        <asp:TextBox ID="txtBillAmount" runat="server" CssClass="textBox" Style="text-align: right"
                                            TabIndex="-1" Enabled="false" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:Label
                                                ID="lblCurrency" runat="server" CssClass="elementLabel" Style="font-weight: bold;"
                                                Width="50px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                <td align="right" nowrap>
                                        <span class="elementLabel">Party ID :</span>
                                    </td>
                                    <td align="left" nowrap>
                                        <asp:TextBox ID="txtPartyID" runat="server" Width="80px" TabIndex="1" CssClass="textBox"></asp:TextBox>
                                        <asp:Button ID="btnPartyID" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <center>
                                <table cellspacing="0" border="1" width="80%">
                                    <tr>
                                        <td colspan="6" align="right">
                                            <span class="mandatoryField">Please ensure to click on <b style="color: #007DFB;">Add</b>
                                                button before you Save the Bill record.</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="70px">
                                            <span class="elementLabel">Ship. Bill No</span>
                                        </td>
                                        <td nowrap align="center">
                                            <span class="elementLabel">Ship. Bill Date</span>
                                        </td>
                                        <td nowrap>
                                            <span class="elementLabel">Ship. Bill Amt</span>
                                        </td>
                                        <td nowrap>
                                            <span class="elementLabel">Type Of Export</span>
                                        </td>
                                        <td width="5%">
                                            <span class="elementLabel">Export Agency</span>
                                        </td>
                                        <td width="5%">
                                            <span class="elementLabel">Dispatch Indicator</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" width="5%">
                                            <asp:TextBox ID="txtShippingBillNo" runat="server" Width="60px" TabIndex="-1" MaxLength="30"
                                                CssClass="textBox" Enabled="false"></asp:TextBox>
                                            <asp:Button ID="Help_btnBillNo" runat="server" CssClass="btnHelp_enabled" Visible="false" />
                                        </td>
                                        <td align="center" width="5%">
                                            <asp:TextBox ID="txtShippingBillDate" runat="server" Width="70px" TabIndex="-1" CssClass="textBox"
                                                Enabled="false"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender ID="mdShippingDate" Mask="99/99/9999" MaskType="Date"
                                                runat="server" TargetControlID="txtShippingBillDate" ErrorTooltipEnabled="True"
                                                CultureName="en-GB" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                CultureDateFormat="DMY" CultureDatePlaceholder="/" CultureDecimalPlaceholder="."
                                                CultureThousandsPlaceholder="," CultureTimePlaceholder=":" Enabled="True">
                                            </ajaxToolkit:MaskedEditExtender>
                                        </td>
                                        <td align="center" width="5%">
                                            <asp:TextBox ID="txtShipBillAmt" runat="server" Width="80px" TabIndex="-1" CssClass="textBox"
                                                Style="text-align: right" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td align="center" style="margin-left: 40px">
                                            <asp:DropDownList ID="ddlTypeOfExport" runat="server" CssClass="dropdownList" TabIndex="1"
                                                Width="80px">
                                                <asp:ListItem Value="Goods">Goods</asp:ListItem>
                                                <asp:ListItem Value="SOFTEX">Softex</asp:ListItem>
                                                <asp:ListItem Value="Royalty">Royalty</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" nowrap style="margin-left: 40px">
                                            <asp:DropDownList ID="ddlExportAgency" runat="server" CssClass="dropdownList" TabIndex="2"
                                                Width="150px">
                                                <asp:ListItem Value="Customs">Customs</asp:ListItem>
                                                <asp:ListItem Value="SEZ">SEZ</asp:ListItem>
                                                <asp:ListItem Value="STPI">STPI</asp:ListItem>
                                                <asp:ListItem Value="Status holder exporters">Status holder exporters</asp:ListItem>
                                                <asp:ListItem Value="100% EOU">100% EOU</asp:ListItem>
                                                <asp:ListItem Value="Warehouse export">Warehouse export</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" style="margin-left: 40px">
                                            <asp:DropDownList ID="ddlDispachInd" runat="server" CssClass="dropdownList" TabIndex="3"
                                                Width="200px">
                                                <asp:ListItem Value="Dispatched directly by exporter">Dispatched directly by exporter</asp:ListItem>
                                                <asp:ListItem Value="By Bank">By Bank</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap>
                                            <span class="elementLabel">Invoice Sr. No.</span>
                                        </td>
                                        <td align="center" nowrap>
                                            <span class="elementLabel">Invoice No.</span>
                                        </td>
                                        <td width="5%">
                                            <span class="elementLabel">Invoice Date</span>
                                        </td>
                                        <td width="5%">
                                            <span class="elementLabel">Invoice Amt.</span>
                                        </td>
                                        <td nowrap>
                                            <span class="elementLabel">Freight Amt.</span>
                                        </td>
                                        <td width="5%">
                                            <span class="elementLabel">Insurance Amt.</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap>
                                            <asp:TextBox ID="txtInvoiceSrNo" runat="server" Width="80px" TabIndex="4" MaxLength="50"
                                                CssClass="textBox"></asp:TextBox>
                                        </td>
                                        <td width="5%">
                                            <asp:TextBox ID="txtInvoiceNo" runat="server" Width="120px" TabIndex="5" MaxLength="30"
                                                CssClass="textBox"></asp:TextBox>
                                        </td>
                                        <td width="5%">
                                            <asp:TextBox ID="txtInvoiceDate" runat="server" Width="70px" TabIndex="5" MaxLength="10"
                                                CssClass="textBox"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                                runat="server" TargetControlID="txtInvoiceDate" InputDirection="RightToLeft"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left"
                                                PromptCharacter="_">
                                            </ajaxToolkit:MaskedEditExtender>
                                        </td>
                                        <td align="center" nowrap>
                                            <asp:TextBox ID="txtInvoiceAmt" runat="server" Width="60px" TabIndex="6" CssClass="textBox"
                                                Style="text-align: right"></asp:TextBox>
                                        </td>
                                        <td align="center" nowrap>
                                            <asp:TextBox ID="txtFreight" runat="server" Width="60px" TabIndex="6" CssClass="textBox"
                                                Style="text-align: right"></asp:TextBox>
                                        </td>
                                        <td align="center" nowrap>
                                            <asp:TextBox ID="txtInsuranceAmt" runat="server" Width="80px" TabIndex="7" CssClass="textBox"
                                                Style="text-align: right"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="5%">
                                            <span class="elementLabel">Port Code.</span>
                                        </td>
                                        <td width="5%">
                                            <span class="elementLabel">Discount Amt.</span>
                                        </td>
                                        <td width="5%">
                                            <span class="elementLabel">Packing Charges</span>
                                        </td>
                                        <td width="5%">
                                            <span class="elementLabel">Deduction Charges</span>
                                        </td>
                                        <td width="5%">
                                            <span class="elementLabel">Commission</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" nowrap>
                                            <asp:DropDownList ID="ddlPortCodeGrid" runat="server" Width="80px" TabIndex="8" CssClass="dropdownList">
                                            </asp:DropDownList>
                                            <asp:Button ID="btnPortCode" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                        </td>
                                        <td align="center" nowrap>
                                            <asp:TextBox ID="txtDiscount" runat="server" Width="80px" TabIndex="9" CssClass="textBox"
                                                Style="text-align: right"></asp:TextBox>
                                        </td>
                                        <td align="center" nowrap>
                                            <asp:TextBox ID="txtPackingCharges" runat="server" Width="80px" TabIndex="10" CssClass="textBox"
                                                Style="text-align: right"></asp:TextBox>
                                        </td>
                                        <td align="center" nowrap>
                                            <asp:TextBox ID="txtDeductionCharges" runat="server" Width="80px" TabIndex="11" CssClass="textBox"
                                                Style="text-align: right"></asp:TextBox>
                                        </td>
                                        <td align="center" nowrap>
                                            <asp:TextBox ID="txtcommission" runat="server" Width="80px" TabIndex="12" CssClass="textBox"
                                                Style="text-align: right"></asp:TextBox>
                                        </td>
                                        <td align="left">
                                            <asp:Button ID="btnAddGRPPCustoms" runat="server" Text="Add" CssClass="buttonDefault"
                                                TabIndex="13" OnClick="btnAddGRPPCustoms_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GridViewEDPMS_Bill_Details" runat="server" AutoGenerateColumns="false"
                                                Width="100%" GridLines="Both" AllowPaging="true" PageSize="20" OnRowDataBound="GridViewEDPMS_Bill_Details_RowDataBound">
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
                                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                        <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Shipping Bill No." HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblShippingBillNo" runat="server" Text='<%# Eval("ShippingBillNo") %>'
                                                                CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Shipping Bill Date" HeaderStyle-HorizontalAlign="Left"
                                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblShipping_Bill_Date" runat="server" Text='<%# Eval("Shipping_Bill_Date","{0:dd/MM/yyyy}") %>'
                                                                CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Shipping Bill Amt." HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblShipBillAmt" runat="server" Text='<%# Eval("ShipBillAmt","{0:0.00}") %>'
                                                                CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type of Export" HeaderStyle-HorizontalAlign="Right"
                                                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTypeofExport" runat="server" Text='<%# Eval("TypeofExport") %>'
                                                                CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                        <ItemStyle HorizontalAlign="left" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Export Agency" HeaderStyle-HorizontalAlign="Right"
                                                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblExportAgency" runat="server" Text='<%# Eval("ExportAgency") %>'
                                                                CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dispatch Indicator">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDispatchInd" runat="server" Text='<%# Eval("DispatchInd") %>' CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                        <ItemStyle HorizontalAlign="left" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Invoice Sr No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblInvoiceSrNo" runat="server" Text='<%# Eval("InvoiceSrNo") %>' CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                        <ItemStyle HorizontalAlign="left" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Invoice No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblInvoiceNo" runat="server" Text='<%# Eval("InvoiceNo") %>' CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Invoice Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblInvoice_Date" runat="server" Text='<%# Eval("Invoice_Date","{0:dd/MM/yyyy}") %>'
                                                                CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="InvoiceAmt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblInvoiceAmt" runat="server" Text='<%# Eval("Invoice_Amt","{0:0.00}") %>'
                                                                CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="FreightAmt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFreightAmt" runat="server" Text='<%# Eval("FreightAmt","{0:0.00}") %>'
                                                                CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Insurance Amt.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblInsuranceAmt" runat="server" Text='<%# Eval("InsuranceAmt","{0:0.00}") %>'
                                                                CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Port Code.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPortCode" runat="server" Text='<%# Eval("PortCode") %>' CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Discount Amt.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDiscountAmt" runat="server" Text='<%# Eval("DiscountAmt","{0:0.00}") %>'
                                                                CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Packing Charges">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPackingCharges" runat="server" Text='<%# Eval("PackingCharges","{0:0.00}") %>'
                                                                CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Deduction Charges">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeductionCharges" runat="server" Text='<%# Eval("DeductionCharges","{0:0.00}") %>'
                                                                CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Commission">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCommission" runat="server" Text='<%# Eval("Commission","{0:0.00}") %>'
                                                                CssClass="elementLabel"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnDelete" runat="server" CommandArgument='<%# Eval("SrNo") %>' CommandName="RemoveRecord"
                                                                CssClass="deleteButton" OnClick="LinkButtonClick" Text="Delete" ToolTip="Delete Record" /></ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                    </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table cellspacing="0" cellpadding="0" border="0" width="400px" style="line-height: 150%">
                                    <tr>
                                        <td align="right" style="width: 110px">
                                        </td>
                                        <td align="left" style="width: 290px; padding-top: 10px; padding-bottom: 10px">
                                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault"
                                                ToolTip="Save" TabIndex="13" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                ToolTip="Cancel" TabIndex="14" OnClick="btnCancel_Click" />
                                            <%--<asp:Button ID="btnUpdate" runat="server" TabIndex="-1" style="display:none;" OnClick="update" />--%>
                                            <input type="hidden" id="hdnBranch" runat="server" />
                                            <input type="hidden" id="hdnYear" runat="server" />
                                            <input type="hidden" id="hdnInvoiceAmt" runat="server" />
                                        </td>
                                    </tr>
                                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
