<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptExportBRCRegister.aspx.cs" Inherits="Reports_EXPReports_rptExportBRCRegister" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script language="javascript" type="text/javascript">

        function OpenDocList() {
            var txtFromDate = document.getElementById('txtFromDate').value;
            var txtToDate = document.getElementById('txtToDate').value;

            if (txtFromDate == '') {
                alert('Enter From Date.');
                rptFDate.focus();
                return false;
            }

            if (txtToDate == '') {
                alert('Enter To Date.');
                txtToDate.focus();
                return false;
            }

            if (document.getElementById('rdbNegotiation').checked == true) {
                var DocType = 'N';
            }

            if (document.getElementById('rdbDiscount').checked == true) {
                var DocType = 'D';
            }

            if (document.getElementById('rdbCollection').checked == true) {
                var DocType = 'C';
            }

            if (document.getElementById('rdbAdv').checked == true) {
                var DocType = 'M';
            }

            if (document.getElementById('rdbPurchase').checked == true) {
                var DocType = 'P';
            }

            if (document.getElementById('rdbEBR').checked == true) {
                var DocType = 'E';
            }

            if (document.getElementById('rdbAll').checked == true) {
                var DocType = 'ALL';
            }

            var txtType = "";
            var txtDocNo = "";
            var Branch = document.getElementById('ddlBranch').value;
            var pc = 1;
            open_popup('HelpExportBRCDetails.aspx?DocType=' + DocType + '&FromDate=' + txtFromDate + '&ToDate=' + txtToDate + '&Branch=' + Branch + '&DocNo=' + txtDocNo + '&pc=' + pc, 400, 400, "DocFile");
        }

        function OpenDocList1() {
            var txtFromDate = document.getElementById('txtFromDate').value;
            var txtToDate = document.getElementById('txtToDate').value;
            
            if (txtFromDate == '') {
                alert('Enter From Date.');
                rptFDate.focus();
                return false;
            }

            if (txtToDate == '') {
                alert('Enter To Date.');
                txtToDate.focus();
                return false;
            }
            
            if (document.getElementById('rdbNegotiation').checked == true) {
                var DocType = 'N';
            }

            if (document.getElementById('rdbDiscount').checked == true) {
                var DocType = 'D';
            }

            if (document.getElementById('rdbCollection').checked == true) {
                var DocType = 'C';
            }

            if (document.getElementById('rdbAdv').checked == true) {
                var DocType = 'M';
            }

            if (document.getElementById('rdbPurchase').checked == true) {
                var DocType = 'P';
            }

            if (document.getElementById('rdbEBR').checked == true) {
                var DocType = 'E';
            }

            if (document.getElementById('rdbAll').checked == true) {
                var DocType = 'ALL';
            }


             var Branch = document.getElementById('ddlBranch').value;
             var txtDocNo = document.getElementById('txtFDocNo').value;

             if (txtDocNo == '') {
                 alert('Enter From Customer');
                 txtDocNo.focus();
                 return false;

             }
            
            var pc = 2;

            open_popup('HelpExportBRCDetails.aspx?DocType=' + DocType + '&FromDate=' + txtFromDate + '&ToDate=' + txtToDate + '&Branch=' + Branch  + '&DocNo=' + txtDocNo + '&pc=' + pc, 400, 400, "DocFile");
        }

        

        function selectCurr(Curr) {
            document.getElementById('txtCurrency').value = Curr;
        }


        function selectUser(Uname) {

            document.getElementById('txtFDocNo').value = Uname;
            document.getElementById('txtTDocNo').focus();

        }

        function selectUser1(Uname) {
            var DocNo = document.getElementById('txtFDocNo');
            if (DocNo.value == '') {
                alert('Enter From Document Number');
                return false;
            }

            document.getElementById('txtTDocNo').value = Uname;
            document.getElementById('btnSave').focus();

        }

        function validateSave() {

            var Branch = document.getElementById('ddlBranch').value;

            var txtFromDate = document.getElementById('txtFromDate').value;
            var txtToDate = document.getElementById('txtToDate').value;

            if (txtFromDate == '') {
                alert('Enter From Date.');
                rptFDate.focus();
                return false;
            }

            if (txtToDate == '') {
                alert('Enter To Date.');
                txtToDate.focus();
                return false;
            }

            if (document.getElementById('rdbNegotiation').checked == true) {
                var DocType = 'N';
            }
            
            if (document.getElementById('rdbDiscount').checked == true) {
                var DocType = 'D';
            }
            
            if (document.getElementById('rdbCollection').checked == true) {
                var DocType = 'C';
            }
            
            if (document.getElementById('rdbAdv').checked == true) {
                var DocType = 'M';
            }
            
            if (document.getElementById('rdbPurchase').checked == true) {
                var DocType = 'P';
            }
            
            if (document.getElementById('rdbEBR').checked == true) {
                var DocType = 'E';
            }
            
            if (document.getElementById('rdbAll').checked == true) {
                var DocType = 'ALL';
            }
           

            if (document.getElementById('rdbAllDocumentNo').checked == true) {
                
                var rptFdoc = "";
                var rptTdoc = "";
                var rptCustType = "All";
            }
            else {
                
                var txtFdoc = document.getElementById('txtFDocNo');
                var txtTdoc = document.getElementById('txtTDocNo');
                //alert(txtTdoc.value);
                if (txtFdoc.value == '') {
                    alert('Enter From Document Number .');
                    txtFdoc.focus();
                    return false;
                }

                if (txtTdoc.value == '') {
                    alert('Enter To Document Number .');
                    txtTdoc.focus();
                    return false;
                }

                var rptCustType = "Single";
                var rptFdoc = document.getElementById('txtFDocNo').value;
                var rptTdoc = document.getElementById('txtTDocNo').value;

            }

            var winname = window.open('ViewExportReportBRCRegister.aspx?Branch=' + Branch + '&DocType=' + DocType + '&from=' + txtFromDate + '&To=' + txtToDate + '&CustType=' + rptCustType + '&FCust=' + rptFdoc + '&TCust=' + rptTdoc , '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=950,height=500');

            winname.focus();
            return false;
        }

    </script>
    <style type="text/css">
        .style1
        {
            width: 50%;
        }
        .style3
        {
            width: 148px;
            height: 25px;
        }
        .style4
        {
            height: 25px;
        }
    </style>
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
    <div>
        <uc1:Menu ID="Menu1" runat="server" />

        <br />
        <br />
    </div>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
        <ContentTemplate>
            <table cellspacing="0" border="0" width="100%">
                <tr>
                <td></td></tr>
                <tr>
                    <td align="left" valign="bottom" class="style1">
                        <span class="pageLabel"> BRC Register</span>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 100%" valign="top">
                        <hr />
                    </td>
                </tr>
            </table>
            <table cellspacing="0" border="0" >
                <tr>
                    <td   align = "left" width = "150px">
                        <span class="elementLabel">Branch :</span>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" Width="100px"
                            runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                </table>
                
                <fieldset id="DocTypeList" runat="server" style="width: 800px" visible="true">
                <table cellspacing="0" border = "0">
                <tr>

                    <td height="20px" align="left" valign="middle" width="150px">
                        <asp:RadioButton ID="rdbNegotiation" runat="server" AutoPostBack="true" CssClass="elementLabel"
                            GroupName="Data1" Checked="false" Text="Negotiation"  />
                        </td>
                        <td height="20px" align="left" valign="middle" width="150px">
                        <asp:RadioButton ID="rdbDiscount" runat="server" AutoPostBack="true" CssClass="elementLabel"
                            GroupName="Data1" Checked="false" Text="Discount"  />
                    </td>
                    <td height="20px" align="left" valign="middle" width="150px">
                        <asp:RadioButton ID="rdbCollection" runat="server" AutoPostBack="true" CssClass="elementLabel"
                            GroupName="Data1" Checked="false" Text="Collection"  />
                        </td>
                        <td height="20px" align="left" valign="middle" width="150px">
                        <asp:RadioButton ID="rdbAdv" runat="server" AutoPostBack="true" CssClass="elementLabel"
                            GroupName="Data1" Checked="false" Text="Advance Payments"  />
                    
                </tr>
                <tr>
                    <td height="20px" align="left" valign="middle" width="150px">
                        <asp:RadioButton ID="rdbPurchase" runat="server" AutoPostBack="true" CssClass="elementLabel"
                            GroupName="Data1" Checked="false" Text="Purchase"  />
                       </td>
                       <td height="20px" align="left" valign="middle" width="150px">
                        <asp:RadioButton ID="rdbEBR" runat="server" AutoPostBack="true" CssClass="elementLabel"
                            GroupName="Data1" Checked="false" Text="EBR"  />
                       
                    </td>
                    
                        <td height="20px" align="left" valign="middle" width="150px">
                        <asp:RadioButton ID="rdbAll" runat="server" AutoPostBack="true" CssClass="elementLabel"
                            GroupName="Data1" Checked="true" Text="ALL"  />
                    </td>
                </tr>
            </table>

            </fieldset>

            <table cellspacing="0" border="0" >
                <tr>
                    <td nowrap align = "left" width = "150px">
                        <span class="mandatoryField"> * </span>
                        <span class="elementLabel">From  Date : </span>
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
                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender1"
                                                 ControlToValidate="txtFromDate" EmptyValueMessage="Enter Date Value"
                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                            </ajaxToolkit:MaskedEditValidator>


                        <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                            TargetControlID="txtFromDate" PopupButtonID="btncalendar_FromDate">
                        </ajaxToolkit:CalendarExtender>&nbsp;
                        <span class="mandatoryField">* </span>
                        <span class="elementLabel">To Date : </span>
                    
                        <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtVal"
                            Width="70" TabIndex="1"></asp:TextBox>&nbsp;
                        <asp:Button ID="btncalendar_ToDate" runat="server" CssClass="btncalendar_enabled"
                            TabIndex="8" Width="20px" Height="16px" />


                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                 ControlToValidate="txtToDate" EmptyValueMessage="Enter Date Value"
                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                            </ajaxToolkit:MaskedEditValidator>
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                            runat="server" TargetControlID="txtToDate" InputDirection="RightToLeft" AcceptNegative="Left"
                            ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                        </ajaxToolkit:MaskedEditExtender>
                        
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                            TargetControlID="txtToDate" PopupButtonID="btncalendar_ToDate">
                        </ajaxToolkit:CalendarExtender>
                        </td>
                    
                </tr>
                </table>

            <table  cellpadding = "0" border="0">
                <tr>
                    <td height="20px" align="left" valign="middle" width="150px">
                    
                        <asp:RadioButton ID="rdbAllDocumentNo" runat="server" AutoPostBack="true" Checked="true"
                            CssClass="elementLabel" GroupName="Data2" Text="All Customer" 
                            oncheckedchanged="rdbAllDocumentNo_CheckedChanged"  />
                    </td>
                    <td height="20px" align="left" valign="middle" width="180px">
                    
                        <asp:RadioButton ID="rdbSelectedDocumentNo" runat="server" AutoPostBack="true" Checked="False"
                            CssClass="elementLabel" GroupName="Data2" Text="Selected Customer" 
                            oncheckedchanged="rdbSelectedDocumentNo_CheckedChanged"  />
                    </td>
                </tr>
            </table>
            <table id="tblDocNo" runat="server" border="0" visible = "false">
                <tr>
                    <td height="20px" align="left" valign="middle" width="150px">
                    
                    </td>
                    <td height="20px" align="left" valign="middle" width="150px">
                    
                        <span class="elementLabel">From Customer :</span>
                    </td>
                    <td align="left" width="0px" style="font-weight: bold; color: #000000;" class="style4">
                        &nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtFDocNo" runat="server" CssClass="textBox" MaxLength="40" TabIndex="6"
                            Width="186px"></asp:TextBox>
                        &nbsp;
                        <asp:Button ID="btnFDocHelp" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td height="20px" align="left" valign="middle" width="150px">
                    
                        <span class="elementLabel">To Customer :</span>&nbsp;
                    </td>
                    <td align="left" width="0px" style="font-weight: bold; color: #000000;" class="style4">
                        &nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtTDocNo" runat="server" CssClass="textBox" MaxLength="40" TabIndex="6"
                            Width="186px"></asp:TextBox>
                        &nbsp;
                        <asp:Button ID="btnTDocHelp" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                    </td>
                </tr>
            </table>
            
            
            <table border="0">
                <tr valign="bottom">
                    <td align="right" style="width: 120px">
                    </td>
                    <td align="left" style="width: 700px; padding-top: 10px; padding-bottom: 10px" valign="bottom">
                        &nbsp;
                        <asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" Text="Generate"
                            ToolTip="Generate" TabIndex="7" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
