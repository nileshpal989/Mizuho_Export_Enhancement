<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptExportOverDueStatement.aspx.cs"
    Inherits="Reports_EXPReports_rptExportOverDueStatement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script language="javascript" type="text/javascript">

        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            //  alert(charCode);
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }

        function OpenDocList() {
            var txtFromDate = document.getElementById('txtFromDate').value;

            if (txtFromDate == '') {
                alert('Enter As On Date.');
                rptFDate.focus();
                return false;
            }

            var txtBillAmt = document.getElementById('txtBill').value;

            if (txtBillAmt == '') {
                alert('Enter Bill Amount');
                txtBillAmt.focus();
                return false;
            }

            var txtDays = document.getElementById('txtDays').value;

            if (txtDays == '') {
                alert('Enter No. Of Days');
                txtDays.focus();
                return false;
            }


            if (document.getElementById('rbtbla').checked == true) {
                var DocType = 'BLA';
            }

            if (document.getElementById('rbtblu').checked == true) {
                var DocType = 'BLU';
            }

            if (document.getElementById('rbtbba').checked == true) {
                var DocType = 'BBA';
            }

            if (document.getElementById('rbtbbu').checked == true) {
                var DocType = 'BBU';
            }

            if (document.getElementById('rbtbca').checked == true) {
                var DocType = 'BCA';
            }

            if (document.getElementById('rbtbcu').checked == true) {
                var DocType = 'BCU';
            }

            if (document.getElementById('rbtIBD').checked == true) {
                var DocType = 'IBD';
            }

//            if (document.getElementById('rbtLBC').checked == true) {
//                var DocType = 'LBC';
//            }
            if (document.getElementById('rbtBEB').checked == true) {
                var DocType = 'EB';
            }
            if (document.getElementById('rdbAll').checked == true) {
                var DocType = 'ALL';
            }

            if (document.getElementById('rdbCustomer').checked == true) {
                var OverSeasBank = 'Cust';
            }

            if (document.getElementById('rdbOverseasBank').checked == true) {
                var OverSeasBank = 'Over';
            }
            if (document.getElementById('rdbBoth').checked == true) {
                var OverSeasBank = 'All';
            }

            if (document.getElementById('rdbLCWise').checked == true) {
                var rptLC = 'LC';
            }

            if (document.getElementById('rdbNonLCWise').checked == true) {
                var rptLC = 'NLC';
            }

            if (document.getElementById('rdbLCAll').checked == true) {
                var rptLC = 'All';
            }


            if (document.getElementById('rdbLoanAdv').checked == true) {
                var rptLoan = 'Y';
            }
            if (document.getElementById('rdbLoanNotAdv').checked == true) {
                var rptLoan = 'N';
            }
            if (document.getElementById('rdbLoanAll').checked == true) {
                var rptLoan = 'All';
            }

            var Branch = document.getElementById('ddlBranch').value;
            var BillAmt = document.getElementById('txtBill').value;
            var Days = document.getElementById('txtDays').value;


            open_popup('HelpExportOverdueStatement.aspx?DocType=' + DocType + '&FromDate=' + txtFromDate + '&Branch=' + Branch + '&BillAmt=' + BillAmt + '&Days=' + Days + '&OverSeasBank=' + OverSeasBank + '&rptLC=' + rptLC + '&rptLoan=' + rptLoan, 400, 400, "DocFile");
        }

        function selectCurr(Curr) {
            document.getElementById('txtCurrency').value = Curr;
        }


        function selectUser(Uname) {

            document.getElementById('txtFDocNo').value = Uname;
            document.getElementById('btnSave').focus();

        }



        function validateSave() {

            var txtFromDate = document.getElementById('txtFromDate').value;


            if (txtFromDate == '') {
                alert('Enter As On Date.');
                rptFDate.focus();
                return false;
            }

            var txtBillAmt = document.getElementById('txtBill').value;

            if (txtBillAmt == '') {
                alert('Enter Bill Amount');
                txtBillAmt.focus();
                return false;
            }

            var txtDays = document.getElementById('txtDays').value;

            if (txtDays == '') {
                alert('Enter No. Of Days');
                txtDays.focus();
                return false;
            }

            if (document.getElementById('rbtbla').checked == true) {
                var DocType = 'BLA';
            }

            if (document.getElementById('rbtblu').checked == true) {
                var DocType = 'BLU';
            }

            if (document.getElementById('rbtbba').checked == true) {
                var DocType = 'BBA';
            }

            if (document.getElementById('rbtbbu').checked == true) {
                var DocType = 'BBU';
            }

            if (document.getElementById('rbtbca').checked == true) {
                var DocType = 'BCA';
            }

            if (document.getElementById('rbtbcu').checked == true) {
                var DocType = 'BCU';
            }

            if (document.getElementById('rbtIBD').checked == true) {
                var DocType = 'IBD';
            }

//            if (document.getElementById('rbtLBC').checked == true) {
//                var DocType = 'LBC';
//            }
            if (document.getElementById('rbtBEB').checked == true) {
                var DocType = 'EB';
            } if (document.getElementById('rdbAll').checked == true) {
                var DocType = 'ALL';
            }

            if (document.getElementById('rdbCustomer').checked == true) {
                var OverSeasBank = 'Cust';
            }

            if (document.getElementById('rdbOverseasBank').checked == true) {
                var OverSeasBank = 'Over';
            }
            if (document.getElementById('rdbBoth').checked == true) {
                var OverSeasBank = 'All';
            }


            var Branch = document.getElementById('ddlBranch').value;
            var BillAmt = document.getElementById('txtBill').value;
            var Days = document.getElementById('txtDays').value;

            if (document.getElementById('rdbAllDocumentNo').checked == true) {
                var CustType = 'All';
                var FCust = '';
            }

            if (document.getElementById('rdbSelectedDocumentNo').checked == true) {
                var CustType = 'Single';
                var FCust = document.getElementById('txtFDocNo').value;
            }

            if (document.getElementById('rdbLoanAdv').checked == true) {
                rptLoan = 'Y';
            }
            else if (document.getElementById('rdbLoanNotAdv').checked == true) {
                rptLoan = 'N';
            }
            else if (document.getElementById('rdbLoanAll').checked == true) {
                rptLoan = 'All';
            }

            if (document.getElementById('rdbLCWise').checked == true) {
                var rptLC = 'LC';
            }

            if (document.getElementById('rdbNonLCWise').checked == true) {
                var rptLC = 'NLC';
            }

            if (document.getElementById('rdbLCAll').checked == true) {
                var rptLC = 'All';
            }

            var winname = window.open('ViewExportReportOverDueStatement.aspx?DocType=' + DocType + '&FromDate=' + txtFromDate + '&Branch=' + Branch + '&BillAmt=' + BillAmt + '&Days=' + Days + '&OverSeasBank=' + OverSeasBank + '&CustType=' + CustType + '&FCust=' + encodeURIComponent(FCust) + '&rptLoan=' + rptLoan + '&rptLC=' + rptLC, '', 'scrollbars=yes,status=0,menubar=0,left=0,top=50,width=1100,height=550');
            winname.focus();

            return false;

        }

    </script>
   
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="../../Scripts/InitEndRequest.js"></script>
    <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="../../Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div style="text-align:left">
        <uc1:Menu ID="Menu1" runat="server" />
        <br />
       
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
        <ContentTemplate>
            <table cellspacing="0" border="0" width="100%">
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="bottom">
                        <span class="pageLabel"><strong>EXPORT BILLS OVERDUE STATEMENT</strong></span>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top">
                        <hr />
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="left" width="10%"  nowrap>
                        <span class="elementLabel">Branch :</span>                   
                        <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" Width="100px"
                            runat="server">
                        </asp:DropDownList>
                    </td>
                    <td align="right"  valign="top">
                        <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                    </td>
                </tr>
            </table>
            <fieldset id="DocTypeList" runat="server" style="width:1000px" visible="true">
                <table cellspacing="0" border="0">
                    <tr>
                        <td height="20px" align="left" valign="middle"  nowrap>
                            <asp:RadioButton ID="rbtbla" runat="server" CssClass="elementLabel" TabIndex="3"
                                Text="Bills Bght with L/C at Sight" Style="font-weight: bold;" GroupName="TransType" />
                        </td>
                        <td height="20px" align="left" valign="middle" nowrap>
                            <asp:RadioButton ID="rbtbba" runat="server" CssClass="elementLabel" Text="Bills Bght W/O L/C at Sight"
                                GroupName="TransType" TabIndex="4" Style="font-weight: bold;" />
                        </td>
                        <td height="20px" align="left" valign="middle" nowrap>
                            <asp:RadioButton ID="rbtblu" runat="server" CssClass="elementLabel" Text="Bills Bght with L/C Usance"
                                GroupName="TransType" TabIndex="5" Style="font-weight: bold;" />
                        </td>
                        <td height="20px" align="left" valign="middle"  nowrap>
                            <asp:RadioButton ID="rbtbbu" runat="server" CssClass="elementLabel" Text="Bills Bght W/O L/C Usance"
                                GroupName="TransType" TabIndex="6" Style="font-weight: bold;" />
                        </td>
                        <td height="20px" align="left" valign="middle"  nowrap>
                            <asp:RadioButton ID="rbtbca" runat="server" CssClass="elementLabel" Text="Bills For Coll. at Sight "
                                GroupName="TransType" TabIndex="7" Style="font-weight: bold;" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20px" align="left" valign="middle"  nowrap>
                            <asp:RadioButton ID="rbtbcu" runat="server" CssClass="elementLabel" Text="Bills For Coll. Usance"
                                GroupName="TransType" TabIndex="8" Style="font-weight: bold;" />
                        </td>
                        <td height="20px" align="left" valign="middle"  nowrap>
                            <asp:RadioButton ID="rbtIBD" runat="server" CssClass="elementLabel" Text="Vendor Bills Dis."
                                GroupName="TransType" TabIndex="9" Style="font-weight: bold;" />
                        </td>
                       <%-- <td height="20px" align="left" valign="middle" width="150px" nowrap>
                            <asp:RadioButton ID="rbtLBC" runat="server" CssClass="elementLabel" Text="Local Bills Coll."
                                GroupName="TransType" TabIndex="10" Style="font-weight: bold;" />
                        </td>--%>
                        <td height="20px" align="left" valign="middle" width="150px" nowrap>
                            <asp:RadioButton ID="rbtBEB" runat="server" CssClass="elementLabel" Text="Advance"
                                GroupName="TransType" TabIndex="10" Style="font-weight: bold;" />
                        </td>
                        <td height="20px" align="left" valign="middle" width="150px" nowrap>
                            <asp:RadioButton ID="rdbAll" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                GroupName="TransType" Text="ALL" Style="forecolor: #000000; font-weight: bold;"
                                TabIndex="11" Checked="True" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <table cellspacing="0" border="0">
                <tr>
                    <td nowrap align="left" width="150px">
                        <span class="mandatoryField">&nbsp;&nbsp;&nbsp; * </span><span class="elementLabel">As On Date : </span>
                    </td>
                    <td nowrap align="left" style="width: 700px">
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                            Width="70" TabIndex="1"></asp:TextBox>&nbsp;
                        <asp:Button ID="btncalendar_FromDate" runat="server" CssClass="btncalendar_enabled"
                            TabIndex="8" Width="20px" Height="16px" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <ajaxToolkit:MaskedEditExtender ID="mdfdate" Mask="99/99/9999" MaskType="Date" runat="server"
                            TargetControlID="txtFromDate" InputDirection="RightToLeft" AcceptNegative="Left"
                            ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                        </ajaxToolkit:MaskedEditExtender>
                        <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                            TargetControlID="txtFromDate" PopupButtonID="btncalendar_FromDate">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdfdate"
                            ValidationGroup="dtVal" ControlToValidate="txtFromDate" EmptyValueMessage="Enter Date Value"
                            InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                        </ajaxToolkit:MaskedEditValidator>
                      
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td height="40px" align="left" valign="middle" nowrap>
                        <asp:RadioButton ID="rdbCustomer" runat="server" AutoPostBack="true" CssClass="elementLabel"
                            GroupName="Data3" Checked="false" Text="Customer" Width="150px" />
                    </td>
                    <td height="40px" align="left" valign="middle" nowrap>
                        <asp:RadioButton ID="rdbOverseasBank" runat="server" AutoPostBack="true" CssClass="elementLabel"
                            GroupName="Data3" Checked="false" Text="OverseasBank" />
                    </td>
                    <td height="40px" align="left" valign="middle" nowrap>
                        <asp:RadioButton ID="rdbBoth" runat="server" AutoPostBack="true" CssClass="elementLabel"
                            GroupName="Data3" Text="Both" Checked="true" />
                    </td>
                </tr>
                <tr>
                    <td height="40px" align="left" valign="middle" nowrap>
                        <asp:RadioButton ID="rdbLoanAdv" runat="server" AutoPostBack="true" CssClass="elementLabel"
                            GroupName="Loan" Text="Loan Advanced" />
                    </td>
                    <td height="40px" align="left" valign="middle" nowrap>
                        <asp:RadioButton ID="rdbLoanNotAdv" runat="server" AutoPostBack="true" CssClass="elementLabel"
                            Text="Loan Not Advanced" GroupName="Loan" />
                    </td>
                    <td height="40px" align="left" valign="middle" nowrap>
                        <asp:RadioButton ID="rdbLoanAll" runat="server" AutoPostBack="true" CssClass="elementLabel"
                            Text="All" GroupName="Loan" Checked="True" />
                    </td>
                  
                </tr>
                <tr>
                    <td height="40px" align="left" valign="middle" nowrap>
                        <asp:RadioButton ID="rdbLCWise" runat="server" AutoPostBack="true" CssClass="elementLabel"
                            GroupName="LC" Text="LC's" />
                    </td>
                    <td height="40px" align="left" valign="middle" nowrap>
                        <asp:RadioButton ID="rdbNonLCWise" runat="server" AutoPostBack="true" CssClass="elementLabel"
                            Text="Non LC's" GroupName="LC" />
                    </td>
                    <td height="40px" align="left" valign="middle" nowrap>
                        <asp:RadioButton ID="rdbLCAll" runat="server" AutoPostBack="true" CssClass="elementLabel"
                            Text="All" GroupName="LC" Checked="True" />
                    </td>
                </tr>
            </table>
            <table id="Table1" runat="server" border="0">
                <tr>
                    <td>
                        <span class="elementLabel">Bill Amount >= &nbsp;</span>
                        <asp:TextBox ID="txtBill" runat="server" CssClass="textBox" MaxLength="40" TabIndex="6"
                            Width="144px" Style="text-align: right">0</asp:TextBox>
                        <span class="elementLabel">&nbsp;&nbsp;&nbsp;&nbsp; No. Of Days >= &nbsp;</span>
                        <asp:TextBox ID="txtDays" runat="server" CssClass="textBox" MaxLength="40" TabIndex="6"
                            Width="73px" Style="text-align: right">0</asp:TextBox>
                    </td>
                </tr>
            </table>
            <table cellpadding="0" border="0">
                <tr>
                    <td height="20px" align="left" valign="middle" width="150px">
                        <asp:RadioButton ID="rdbAllDocumentNo" runat="server" AutoPostBack="true" Checked="true"
                            CssClass="elementLabel" GroupName="Data2" Text="All Customer" OnCheckedChanged="rdbAllDocumentNo_CheckedChanged" />
                    </td>
                    <td height="20px" align="left" valign="middle" width="180px">
                        <asp:RadioButton ID="rdbSelectedDocumentNo" runat="server" AutoPostBack="true" Checked="False"
                            CssClass="elementLabel" GroupName="Data2" Text="Single Customer" OnCheckedChanged="rdbSelectedDocumentNo_CheckedChanged" />
                    </td>
                </tr>
            </table>
            <table id="tblDocNo" runat="server" border="0" visible="false">
                <tr>
                    <td height="20px" align="left" valign="middle" width="150px">
                    </td>
                    <td height="20px" align="left" valign="middle" width="100px">
                        <span class="elementLabel">Customer Id :</span>
                    </td>
                    <td align="left" width="0px" style="font-weight: bold; color: #000000;" class="style4">
                        &nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtFDocNo" runat="server" CssClass="textBox" MaxLength="40" TabIndex="6"
                            Width="186px"></asp:TextBox>
                        &nbsp;
                        <asp:Button ID="btnFDocHelp" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                    </td>
                </tr>
            </table>
            <table border="0">
                <tr valign="bottom">
                    <td align="right" style="width: 120px">
                    </td>
                    <td align="left" style="width: 140px; padding-top: 10px; padding-bottom: 10px" valign="bottom">
                        &nbsp;
                        <asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" Text="Generate"
                            ToolTip="Generate" TabIndex="7" />
                    </td>
                    <td align="left" style="width: 500px; padding-top: 10px; padding-bottom: 10px" valign="bottom">
                        &nbsp;
                        <asp:Button ID="btnCSV" runat="server" CssClass="buttonDefault" Text="Generate CSV File"
                            ToolTip="Generate CSV File" TabIndex="8" Width="110px" OnClick="btnCSV_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
       </div>
    </form>
</body>
</html>
