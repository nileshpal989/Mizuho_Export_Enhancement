<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDPMS_E_FIRC_Issuse.aspx.cs" Inherits="EDPMS_EDPMS_E_FIRC_Issuse" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC- Trade  System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script language="javascript" type="text/javascript">
        function OpenDocNoList(e) {
            var keycode;
            var txtDocNo;
            var Branch = document.getElementById('ddlBranch').value;
            var Year = document.getElementById('txtYear').value;

            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {
                txtDocNo = document.getElementById('txtDocumentNo').value;
                open_popup('EDPMS_IINW_Help.aspx?txtDocNo=' + txtDocNo + '&Branch=' + Branch + '&Year=' + Year, 400, 750, 'EBRCNoList');
                return false;
            }

        }
        function selectDocNo(selectedID) {

            var id = selectedID;
            document.getElementById('hdnDocNo').value = id;
            document.getElementById('btnDocNumber').click();
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
                if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year)) {
                    alert('Invalid ' + CName);
                    controlID.focus();
                    return false;
                }
            }
        }
        function balance() {
            var firamount = document.getElementById('txtfircamt');
            var balanceamt = document.getElementById('txtbalance');
            var totalamt = document.getElementById('txttotalamt');
            var hdnbalanceamt = document.getElementById('hdnBalanceAmount');
            var fircAmt = document.getElementById('hdnFircAmt');
            if (fircAmt.value == '')
                fircAmt.value = 0;
                fircAmt.value = parseFloat(fircAmt.value).toFixed(2);

                if (firamount.value == '')
                    firamount.value = 0;
                firamount.value = parseFloat(firamount.value).toFixed(2);


                if (totalamt.value == '')
                    totalamt.value = 0;
                totalamt.value = parseFloat(totalamt.value).toFixed(2);




                if (firamount.value > 0) {

                    var _fircAmt = 0;
                    var _firamount = 0
                    var _totalamt = 0;

                    if (firamount.value != '') {
                        _firamount = parseFloat(firamount.value);
                    }
                    if (_fircAmt.value != '') {
                        _fircAmt = parseFloat(fircAmt.value);
                    }
                    if (totalamt.value != '') {
                        _totalamt = parseFloat(totalamt.value);
                    }

                    if (balanceamt.value<=0) {
                        alert('Balance Amount is Zero');
                        if (_fircAmt.value != '') {
                            _fircAmt = 0;
                        }

                    }
                    else {
                        var balce = (_totalamt - parseFloat(_firamount + _fircAmt));
                        balanceamt.value = balce.toFixed(2);
                        if (balanceamt.value < 0) {
                            alert('Balance Amount Can not Be Less than Zero.');
                            firamount.focus();
                        }
                   }
            }

        }

        function validation() {
           
            var DocumentNo = document.getElementById('txtDocumentNo');
            var iecode = document.getElementById('txtiecode');
            var iename = document.getElementById('txtiename');
            var fircissuedate = document.getElementById('txtfirc_issuedate');
            var fircamount = document.getElementById('txtfircamt');
            var ok = true;
            var missingfileds = false;
            var stringfields = "";

          

            if (DocumentNo.value =='') {
                missingfileds = true;
                stringfields+="Enter Document No.\n";
               
            }
            if (iecode.value == '') {
                missingfileds=true;
                stringfields += "Enter IE Code. \n";

            }
            if (iecode.value.length < 10) {
                missingfileds = true;
                stringfields += "IE Code Should Not Less Than 10 digits. \n";

            }
            if (iename.value == '') {
                missingfileds = true;
                stringfields += "Enter IE Name. \n";

            }
            
            if(fircamount.value=='' || fircamount.value<=0)
            {
                missingfileds = true;
                stringfields += "Enter firc Amount.\n";
            }
            if (missingfileds) {
                alert(stringfields);
                return false;
            }
            return true;
        
        }




       
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
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
    <div align="left">
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
        </center>
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom">
                            <span class="pageLabel">E-FIRC Data Entry</span>
                        </td>
                        <td align="right" style="width: 50%" nowrap>
                         <asp:Label runat="server" ID="lblSupervisormsg" Text="Role : Supervisor - ONLY VIEW THE DATA  " 
                                    style="color:red" Visible="false" ></asp:Label>&nbsp;

                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                TabIndex="62" onclick="btnBack_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%" valign="top" colspan="2">
                            <input type="hidden" id="hdnCustomerCode" runat="server" />
                             <input type="hidden" id="hdnBalanceAmount" runat="server" />
                              <input type="hidden" id="hdnFircAmt" runat="server" />
                            <asp:Button ID="btnCustomerCode" Style="display: none;" runat="server" />
                            <input type="hidden" id="hdnDocNo" runat="server" />
                            <asp:Button ID="btnDocNumber" Style="display: none;" runat="server" OnClick="btnDocNumber_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%" valign="top" colspan="2">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%;" valign="top" colspan="2">
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table cellspacing="0" border="0" width="45%">
                    <tr align="left">
                        <td rowspan="14" width="40%">
                        &nbsp;
                        </td>
                        <td width="20%" align="right" nowrap>
                            <span class="elementLabel">Branch :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" AutoPostBack="true" runat="server"
                                TabIndex="1" Width="100px" >
                            </asp:DropDownList>
                        </td>
                    </tr>
                   
                    <tr>
                    <td width="20%" align="right" nowrap>
                            <span class="elementLabel">Year :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtYear" runat="server" MaxLength="4" Width="50px" CssClass="textBox" TabIndex="3"
                               ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" align="right" nowrap>
                            <span class="elementLabel">Document No :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtDocumentNo" runat="server" MaxLength="20" TabIndex="1" Width="150px"
                                CssClass="textBox" AutoPostBack="true" 
                                onkeydown="OpenDocNoList(this);" ReadOnly="true" ></asp:TextBox>
                            <asp:Button ID="btnDocNo" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                            <asp:Label ID="lblDocNo" runat="server" CssClass="elementLabel" Width="200px"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td width="20%" align="right" nowrap>
                            <span class="elementLabel">IE Code :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtiecode" runat="server" MaxLength="10" TabIndex="2" Width="100px"  CssClass="textBox"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td width="20%" align="right" nowrap>
                            <span class="elementLabel">IE Name :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtiename" runat="server" MaxLength="100" TabIndex="3" Width="300px"  CssClass="textBox"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td width="20%" align="right" nowrap>
                            <span class="elementLabel">Sr No :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtSrNo" runat="server" MaxLength="6" Width="50px" CssClass="textBox"
                                ReadOnly="True" TabIndex="4"></asp:TextBox>
                        </td>
                    </tr>
                    <tr >
                        <td width="20%" align="right" nowrap>
                            <span class="elementLabel">FIRC No :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtfircno" runat="server" MaxLength="20" Width="150px" CssClass="textBox"
                                ReadOnly="True" TabIndex="5"></asp:TextBox>
                        </td>
                    </tr>
                    <tr >
                        <td width="20%" align="right" nowrap >
                            <span class="elementLabel">FIRC Issue Date :</span>
                        </td>
                        <td align="left" nowrap >
                            <asp:TextBox ID="txtfirc_issuedate" runat="server" CssClass="textBox" MaxLength="10" TabIndex="6"
                                ValidationGroup="dtVal" Width="70px" ></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender
                                                                ID="mdValueDate" runat="server" CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£"
                                                                CultureDateFormat="DMY" CultureDatePlaceholder="/" CultureDecimalPlaceholder="."
                                                                CultureName="en-GB" CultureThousandsPlaceholder="," CultureTimePlaceholder=":"
                                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtfirc_issuedate">
                                                            </ajaxToolkit:MaskedEditExtender>
                                                        <asp:Button ID="btncalendar_ValueDate" runat="server" CssClass="btncalendar_enabled"
                                                            TabIndex="-1" />
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                                            Format="dd/MM/yyyy" PopupButtonID="btncalendar_ValueDate" TargetControlID="txtfirc_issuedate">
                                                        </ajaxToolkit:CalendarExtender>
                                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="mdValueDate"
                                                            ControlToValidate="txtfirc_issuedate" EmptyValueBlurredText="*" EmptyValueMessage="Enter Date Value"
                                                            ErrorMessage="MaskedEditValidator3" InvalidValueBlurredMessage="Date is invalid"
                                                            ValidationGroup="dtVal" Enabled="false"></ajaxToolkit:MaskedEditValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" align="right" nowrap >
                            <span class="elementLabel">FIRC Amount :</span>
                        </td>
                        <td align="left" nowrap >
                            <asp:TextBox ID="txtfircamt" runat="server" CssClass="textBox"  
                                Style="text-align: right" TabIndex="7" ontextchanged="txtfircamt_TextChanged" AutoPostBack="true"
                               ></asp:TextBox>
                        </td>
                    </tr>
                    <tr >
                        <td width="20%" align="right" nowrap>
                            <span class="elementLabel">Balance Amount :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtbalance" runat="server" CssClass="textBox" MaxLength="20" Style="text-align: right"  Enabled="false"
                                ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" align="right" nowrap>
                            <span class="elementLabel">TT Amount :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txttotalamt" runat="server" CssClass="textBox" MaxLength="20" ReadOnly="false" Style="text-align: right"  Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                  
                  
                   
                  
                </table>
                <br />
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td width="15%">
                        </td>
                        <td align="left">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault" ToolTip="Save"
                                TabIndex="8" onclick="btnSave_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                ToolTip="Cancel" TabIndex="9" onclick="btnCancel_Click"  />
                            <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
 
</body>
</html>
