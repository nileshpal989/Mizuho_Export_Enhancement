<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IDPMS_BOEClosure.aspx.cs"
    Inherits="IDPMS_IDPMS_BOEClosure" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link3" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <link href="../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function Cust_Help() {
            //            var e = document.getElementById("ddlBranch");
            //            var branch = e.options[e.selectedIndex].text;
            //            var year = document.getElementById('txtYear').value;
            open_popup('IDPMS_CUST_IECode_help.aspx', 450, 550, 'CustList');
            return false;
        }

        function selectCustomer(acno, name) {

            document.getElementById('txt_iecode').value = acno;
            document.getElementById('lblcust').innerText = name;
            // document.getElementById('txt_custacno').focus();
            //__doPostBack("txtPartyID", "TextChanged");
            javascript: setTimeout('__doPostBack(\'txt_iecode\',\'\')', 0)

        }

        function OpenTTNoList() {

            var custid = document.getElementById('txt_iecode');
            var irmno = document.getElementById('txt_boeno');
            //var year = document.getElementById('hdnyr');


            open_popup('BOENoHelp.aspx?custid=' + custid.value + '&boeno=' + irmno.value, 300, 500, 'TTRefNo');
            return false;

        }

        function saveTTRefDetails(TTRefNo) {

            document.getElementById('txt_boeno').value = TTRefNo;
            __doPostBack("txt_boeno", "TextChanged");

        }

        function Dump_Help() {
            var iecode = document.getElementById('txt_iecode').value;

            var e = document.getElementById("ddlBranch");
            var branch = e.options[e.selectedIndex].value;

            open_popup('../HelpForms/Dump_Help.aspx?iecode=' + iecode + '&branch=' + branch, 500, 550, 'CustList');
            return false;
        }
        function selectDump(acno) {

            document.getElementById('txt_boeno').value = acno;
            __doPostBack("txt_boeno", "TextChanged");
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

        function validate() {

            var iecode = document.getElementById('txt_iecode').value;
            if (iecode == '') {

                alert('IE Code cant be blank');
                document.getElementById('txt_iecode').focus();
                return false;
            }

            var boeno = document.getElementById('txt_boeno').value;
            if (boeno == '') {

                alert('BOE No cant be blank');
                document.getElementById('txt_boeno').focus();
                return false;
            }

            var prtcod = document.getElementById('txtprtcd').value;
            if (prtcod == '') {

                alert('Port Code cant be blank');
                document.getElementById('txtprtcd').focus();
                return false;
            }

            var billdate = document.getElementById('txtbilldate').value;
            if (billdate == '') {

                alert('Bill Date cant be blank');
                document.getElementById('txtbilldate').focus();
                return false;
            }

            var approved = document.getElementById('ddlapproved').value;
            if (approved == '') {

                alert('Approved By cant be blank');
                document.getElementById('ddlapproved').focus();
                return false;
            }

            //var adjamt = document.getElementById('txt_adjamt').value;
            var adjdate = document.getElementById('txt_adjdate').value;
            if (adjdate == '' || adjdate == '__/__/____') {

                alert('Closure Date cant be blank');
                document.getElementById('txt_adjdate').focus();
                return false;
            }

            var adjreason = document.getElementById('ddlreasadj').value;
            var docno = document.getElementById('txt_docno').value;
            var docdate = document.getElementById('txt_docdate').value;
            var docprt = document.getElementById('txtdocprt').value;

            if (adjreason == '') {

                alert('Adjustment reason cant be blank');
                document.getElementById('ddlreasadj').focus();
                return false;
            }
            if (adjreason == '4' || adjreason == '5' || adjreason == '6') {
                if (docno == '') {

                    alert('Shipping Bill No. cant be blank When Reason For Closure is Re-import or Re-export or Set-off/Net off');
                    document.getElementById('txt_docno').focus();
                    return false;
                }
            }
            if (adjreason == '4' || adjreason == '5' || adjreason == '6') {
                if (docdate == '' || docdate == '__/__/____') {

                    alert('Shipping Bill Date cant be blank When Reason For Closure is Re-import or Re-export or Set-off/Net off');
                    document.getElementById('txt_docdate').focus();
                    return false;
                }
            }
            if (adjreason == '4' || adjreason == '5' || adjreason == '6') {
                if (docprt == '') {

                    alert('Shipping Bill Port cant be blank When Reason For Closure is Re-import or Re-export or Set-off/Net off');
                    document.getElementById('txtdocprt').focus();
                    return false;
                }
            }

            var rbi = document.getElementById('ddlapproved').value;
            var letterno = document.getElementById('txt_letterNo').value;
            var letterdate = document.getElementById('txt_letterDate').value;
            if (rbi == '1') {
                if (letterno == '') {
                    alert('letter No Cannot be Blank');
                    document.getElementById('txt_letterNo').focus();
                    return false;
                }
                if (letterdate == '') {
                    alert('Letter Date Cannot be Blank');
                    document.getElementById('txt_letterDate').focus();
                    return false;
                }
            }

            var boeclose = document.getElementById('ddlboeclose').value;

            if (boeclose == '') {

                alert('Close Of Bill Indicator cant be blank');
                document.getElementById('ddlboeclose').focus();
                return false;
            }



        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
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
        <uc1:Menu ID="Menu1" runat="server" />
        <br />
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" valign="bottom" width="10%" nowrap>
                            <span class="pageLabel"><strong>Bill Of Entry Closure Data Entry</strong> </span>
                        </td>
                        <td width="10%" nowrap>
                            <input type="hidden" id="hdnbranch" runat="server" />
                            <input type="hidden" id="hdnyr" runat="server" />
                        </td>
                        <td width="10%" nowrap>
                            &nbsp;
                        </td>
                        <td width="10%" nowrap>
                            &nbsp;
                        </td>
                        <td width="10%" nowrap>
                            &nbsp;
                        </td>
                        <td align="right" nowrap>
                            <asp:Label runat="server" ID="lblSupervisormsg" Text="Role : Supervisor - ONLY VIEW THE DATA  "
                                Style="color: red"></asp:Label>
                            &nbsp;
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                TabIndex="20" OnClick="btnBack_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%" valign="top" colspan="6">
                            <hr />
                        </td>
                    </tr>
                    <tr align="right">
                        <td width="15%" align="right" nowrap>
                            <span class="elementLabel">Branch :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" AutoPostBack="true" runat="server"
                                TabIndex="1">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="mandatoryField">*</span> <span class="elementLabel">IE Code:</span>
                        </td>
                        <td align="left" nowrap colspan="1">
                            <asp:TextBox runat="server" CssClass="textBox" ID="txt_iecode" Width="100px" MaxLength="10"
                                OnTextChanged="txt_iecode_TextChanged" />
                            <asp:Button ID="btncusthelp" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                            <asp:Label ID="lblcust" CssClass="elementLabel" runat="server" Style="text-overflow: ellipsis;
                                overflow: hidden; width: 20px; overflow-y: hidden; height: 37px"></asp:Label>
                        </td>
                        <td align="right">
                            <span class="mandatoryField">*</span><span class="elementLabel">BOE No:</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox runat="server" CssClass="textBox" ID="txt_boeno" Width="100px" MaxLength="50"
                                AutoPostBack="true" OnTextChanged="txt_boeno_TextChanged" Enabled="false" />
                            <asp:Button ID="btnboehelp" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                            <asp:Label Text="BOE Balance Amt :" CssClass="elementLabel" runat="server" />
                            <asp:Label Text="" ID="lblBalAmt" CssClass="elementLabel" runat="server" />
                            <asp:Label Text="" ID="lblBOECur" CssClass="elementLabel" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="mandatoryField">*</span><span class="elementLabel">Port Code:</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtprtcd" runat="server" CssClass="textBox" Width="100px" Style="text-align: left"
                                Enabled="false"></asp:TextBox>
                        </td>
                        <td align="right" nowrap>
                            <span class="mandatoryField">*</span><span class="elementLabel">Bill Date:</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtbilldate" runat="server" CssClass="textBox" Width="100px" Style="text-align: left"
                                Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="mandatoryField">*</span><span class="elementLabel">Reason For Closure:</span>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="ddlreasadj" runat="server" CssClass="dropdownList" Width="200px">
                                <asp:ListItem Text="Destroy of goods" Value="1" />
                                <asp:ListItem Text="Sort Shipment" Value="2" />
                                <asp:ListItem Text="Quality Issue" Value="3" />
                                <asp:ListItem Text="Re-import" Value="4" />
                                <asp:ListItem Text="Re-export" Value="5" />
                                <asp:ListItem Text="Set-off/Net off" Value="6" />
                                <asp:ListItem Text="Others" Value="7" />
                                <asp:ListItem Text="BOE PRIOR TO '10/10/2016'" Value="8" />
                            </asp:DropDownList>
                        </td>
                        <td align="right" nowrap>
                            <span class="mandatoryField">*</span><span class="elementLabel">Closure Date:</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txt_adjdate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                onfocus="this.select()"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="mdRemdate1" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txt_adjdate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="btncalendar_DocDate1" runat="server" CssClass="btncalendar_enabled"
                                TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="calendarFromDate2" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txt_adjdate" PopupButtonID="btncalendar_DocDate1" Enabled="True">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="mandatoryField">*</span> <span class="elementLabel">Approved By:</span>
                        </td>
                        <td align="left">
                            <asp:DropDownList runat="server" ID="ddlapproved" CssClass="dropdownList" Width="70px">
                                <asp:ListItem Text="RBI" Value="1" />
                                <asp:ListItem Text="AD BANK" Value="2" />
                                <asp:ListItem Text="OTHERS" Value="3" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="elementLabel">Letter No:</span>
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txt_letterNo" CssClass="textBox" Width="100px" MaxLength="50" />
                        </td>
                        <td align="right">
                            <span class="elementLabel">Letter Date:</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txt_letterDate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                onfocus="this.select()"> </asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txt_letterDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="btncalendar_DocDate3" runat="server" CssClass="btncalendar_enabled"
                                TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txt_letterDate" PopupButtonID="btncalendar_DocDate3" Enabled="True">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="mandatoryField">*</span> <span class="elementLabel">Shipping Bill No:</span>
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txt_docno" CssClass="textBox" Width="100px" MaxLength="20" />
                        </td>
                        <td align="right">
                            <span class="mandatoryField">*</span> <span class="elementLabel">Shipping Bill Date:</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txt_docdate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                onfocus="this.select()"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="mdRemdate2" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txt_docdate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="btncalendar_DocDate2" runat="server" CssClass="btncalendar_enabled"
                                TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="calendarFromDate3" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txt_docdate" PopupButtonID="btncalendar_DocDate2" Enabled="True">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="elementLabel">Shipping Port:</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtdocprt" runat="server" CssClass="textBox" Width="100px" MaxLength="50" />
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="mandatoryField">*</span><span class="elementLabel">Close Of Bill Indicator:</span>
                        </td>
                        <td align="left">
                            <asp:DropDownList runat="server" ID="ddlboeclose" CssClass="dropdownList" Width="70px">
                                <asp:ListItem Text="OPEN" Value="1" />
                                <asp:ListItem Text="CLOSE" Selected="true" Value="2" />
                            </asp:DropDownList>
                        </td>
                        <td align="right" nowrap>
                            <span class="elementLabel">Closure Reference No:</span>
                        </td>
                        <td align="left">
                            <asp:TextBox runat="server" ID="txtadjrefno" CssClass="textBox" Width="200px" MaxLength="20"
                                Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="elementLabel">Remarks:</span>
                        </td>
                        <td colspan="5" align="left">
                            <asp:TextBox runat="server" CssClass="textBox" ID="txt_remarks" Width="400px" MaxLength="200" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left" colspan="5">
                            <table width="50%">
                                <tr>
                                    <td colspan="2">
                                        <asp:GridView ID="GridViewInvoice" runat="server" AutoGenerateColumns="false" GridLines="Both"
                                            AllowPaging="true" PageSize="20" CssClass="GridView">
                                            <PagerSettings Visible="false" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblinsrno" runat="server" Text='<%# Eval("InvoiceSerialNo") %>' CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="center" Width="2%" />
                                                    <ItemStyle HorizontalAlign="center" Width="2%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Inv No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="7%" ItemStyle-Width="7%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblinvoiceno" runat="server" Text='<%# Eval("InvoiceNo") %>' CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="center" Width="4%" />
                                                    <ItemStyle HorizontalAlign="center" Width="4%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bal Inv Amt" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                    HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBalInvAmt" runat="server" Text='<%# Eval("InvoiceAmt","{0:0.00}") %>'
                                                            CssClass="elementLabel"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="center" Width="4%" />
                                                    <ItemStyle HorizontalAlign="Right" Width="4%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Closure Value" HeaderStyle-HorizontalAlign="Right"
                                                    ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" align="right" OnTextChanged="txt_adrefno_textchange"
                                                            AutoPostBack="true" CssClass="textBox AlgRgh" Text='<%# Eval("InvoiceAmt","{0:0.00}") %>'
                                                            ID="txt_adrefno" Width="150px" MaxLength="20" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" Width="4%" />
                                                    <ItemStyle HorizontalAlign="Right" Width="4%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="All" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="4%"
                                                    ItemStyle-Width="4%">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox runat="server" ID="HeaderChkAllow" AutoPostBack="true" ToolTip="Select All"
                                                            Text="All" OnCheckedChanged="HeaderChkAllow_CheckedChanged" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox runat="server" ID="RowChkAllow" AutoPostBack="true" OnCheckedChanged="RowChkAllow_CheckedChanged" />
                                                        <asp:Label ID="lblSel" runat="server" CssClass="elementLabel" ForeColor="RED" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="3%" />
                                                    <ItemStyle HorizontalAlign="Center" Width="3%" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblBalTot" runat="server" CssClass="pageLabel" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblTot" runat="server" CssClass="pageLabel" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: right">
                            <asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" OnClick="btnSave_Click"
                                Text="Save" ToolTip="Save" />
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="buttonDefault" OnClick="btnCancel_Click"
                                Text="Cancel" ToolTip="Cancel" />
                        </td>
                        <td style="white-space: nowrap; text-align: left">
                            <a runat="server" id="lblLink" target="_blank" font-size="15px" style="color:Red"></a>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
