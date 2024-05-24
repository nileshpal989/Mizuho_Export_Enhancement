<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddEdit_Outward_Remittance_Closure.aspx.cs"
    Inherits="IDPMS_AddEdit_Outward_Remittance_Closure" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="../Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>LMCC TRADE FINANCE SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link3" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <link href="../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function OpenTTNoList() {

            var irmno = document.getElementById('txt_irmno');
            var year = document.getElementById('hdnyr');
            var iecode = document.getElementById('txt_iecode').value;
            var br = document.getElementById('ddlBranch').value;

            open_popup('ORM_Closure_Help.aspx?Branch=' + br + '&IEcode=' + iecode, 300, 500, 'ORM Closure Help');
            return false;

        }

        function selectOrm(ormno, amount) {
            document.getElementById('txt_irmno').value = ormno;
            javascript: setTimeout('__doPostBack(\'txt_irmno\',\'\')', 0)
        }


        function OTT_Help() {
            var iecode = document.getElementById('txt_iecode').value;
            var branch = document.getElementById('ddlBranch').value;
            open_popup('../IDPMS/OTT_Help_New.aspx?iecode=' + iecode + '&branch=' + branch, 450, 550, 'CustList');
            return false;
        }
        function selectOtt(acno, date, amt) {

            document.getElementById('txt_irmno').value = acno;
            document.getElementById('lblOrmAmount').innerHTML = amt;
            document.getElementById('txt_adjamt').value = amt;
            __doPostBack("txtORMNo", "TextChanged");
        }
        function saveTTRefDetails(TTRefNo) {

            document.getElementById('txt_irmno').value = TTRefNo;
            __doPostBack("txt_irmno", "TextChanged");

        }


        function adj() {

            open_popup('R4AHelp.aspx', 300, 500, 'TTRefNo');
            return false;

        }


        function selectadj(desc) {

            document.getElementById('txt_adjreason').value = desc;

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

            var irmno = document.getElementById('txt_irmno').value;
            if (irmno == '') {

                alert('IRM No cant be blank');
                document.getElementById('txt_irmno').focus();
                return false;
            }

            var curr = document.getElementById('ddlcurr').value;
            if (curr == '' || curr == 'Select') {

                alert('Select Currency');
                document.getElementById('ddlcurr').focus();
                return false;
            }

            var adjamt = document.getElementById('txt_adjamt').value;
            if (adjamt == '') {

                alert('Adjustment Amount cant be blank');
                document.getElementById('txt_adjamt').focus();
                return false;
            }

            var adjdate = document.getElementById('txt_adjdate').value;
            if (adjdate == '' || adjdate == '__/__/____') {

                alert('Adjustment Date cant be blank');
                document.getElementById('txt_adjdate').focus();
                return false;
            }

            var approved = document.getElementById('ddlapproved').value;
            if (approved == '') {

                alert('Approved By cant be blank');
                document.getElementById('ddlapproved').focus();
                return false;
            }

            var rbi = document.getElementById('ddlapproved').value;
            var letterno = document.getElementById('txt_letterNo').value;
            var letterdate = document.getElementById('txtletterdate').value;
            if (rbi == '1') {
                if (letterno == '') {
                    alert('letter No Cannot be Blank');
                    document.getElementById('txt_letterNo').focus();
                    return false;
                }
                if (letterdate == '') {
                    alert('Letter Date Cannot be Blank');
                    document.getElementById('txtletterdate').focus();
                    return false;
                }
            }

            var swiftcode = document.getElementById('txtswift').value;
            if (swiftcode == '') {
                alert('Swift Code Cannot Be Blank');
                document.getElementById('txtswift').focus();
                return false;
            }

            var Adjusind = document.getElementById('DdladjustInd').value;
            var docno = document.getElementById('txt_docno').value;
            var docdate = document.getElementById('txt_docdate').value;
            if (Adjusind == '2') {
                if (docno == '') {
                    alert('Document No Cannot be Blank');
                    document.getElementById('txt_docno').focus();
                    return false;

                }
                if (docdate == '') {
                    alert('Document date Cannot be blank');
                    document.getElementById('txt_docdate').focus();
                    return false;
                }
            }


            var remclose = document.getElementById('ddlremclose').value;
            if (remclose == '') {

                alert('Remittance Indicator cant be blank');
                document.getElementById('ddlremclose').focus();
                return false;
            }

            var r = confirm('Are you sure you want to close this ORM');
            if (r == true) {
            }
            else {
                return false;
            }
        }
        function Cust_Help() {
            //            var e = document.getElementById("ddlBranch");
            //            var branch = e.options[e.selectedIndex].text;
            //            var year = document.getElementById('txtYear').value;
            open_popup('IDPMS_CUST_IECode_help.aspx', 450, 550, 'CustList');
            return false;
        }

        function selectCustomer(acno, name) {

            document.getElementById('txt_iecode').value = acno;
            document.getElementById('lblCustName1').innerText = name;
            // document.getElementById('txt_custacno').focus();
            //__doPostBack("txtPartyID", "TextChanged");
            javascript: setTimeout('__doPostBack(\'txt_iecode\',\'\')', 0)

        }

        function HelpDocNo1() {
            //            var Branch = document.getElementById('ddlBranch').value;
            //            var Year = document.getElementById('hdnYear').value;
            //                        var custid = document.getElementById('txt_irmno').value;
            var iecode = document.getElementById('txt_iecode').value;

            popup = window.open('Help_ORMNoCancel.aspx', 'dochelp', 'height=500,  width=600,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');


            return true;
        }
        function selectAccount1(desc) {

            document.getElementById('txtDocNo').value = desc;
            //            document.getElementById('lblOrmAmount').innerHTML = balamt;
            javascript: setTimeout('__doPostBack(\'txtDocNo\',\'\')', 0)
            //            __doPostBack('txtDocNo', '');
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
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" valign="bottom" width="10%" style="white-space: nowrap">
                                <span class="pageLabel"><strong>Outward Remittance Closure</strong></span>
                            </td>
                            <td width="10%" style="white-space: nowrap">
                                <input type="hidden" id="hdnbranch" runat="server" />
                                <input type="hidden" id="hdnyr" runat="server" />
                            </td>
                            <td width="10%" style="white-space: nowrap">
                                &nbsp;
                            </td>
                            <td width="10%" style="white-space: nowrap">
                                &nbsp;
                            </td>
                            <td align="right" style="white-space: nowrap">
                                <asp:Label runat="server" ID="lblSupervisormsg" Text="Role : Supervisor - ONLY VIEW THE DATA"
                                    Style="color: red;"></asp:Label>
                                &nbsp;
                            </td>
                            <td align="right" style="width: 50%">
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
                            <td width="15%" align="right" style="white-space: nowrap">
                                <span class="elementLabel"><span class="mandatoryField">*</span> Branch :</span>
                            </td>
                            <td align="left" style="white-space: nowrap">
                                <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" AutoPostBack="true" runat="server"
                                    TabIndex="1">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="white-space: nowrap">
                                <span class="mandatoryField">*</span><span class="elementLabel">Closure/Cancellation:</span>
                            </td>
                            <td align="left">
                                <asp:DropDownList runat="server" ID="ddlremclose" CssClass="dropdownList" Width="70px"
                                    OnSelectedIndexChanged="ddlremclose_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="New" Value="1" />
                                    <asp:ListItem Text="Cancel" Value="3" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Document No.:</span>
                            </td>
                            <td align="left">
                                <asp:TextBox runat="server" CssClass="textBox" ID="txtDocNo" Width="100px" MaxLength="10"
                                    Enabled="False" AutoPostBack="true" OnTextChanged="txtDocNo_TextChanged" />
                                <asp:Button ID="btnormCanHelp" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                            </td>
                            <td align="right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">IE Code:</span>
                            </td>
                            <td align="left" style="white-space: nowrap" colspan="3">
                                <asp:TextBox runat="server" CssClass="textBox" ID="txt_iecode" Width="100px" MaxLength="10"
                                    AutoPostBack="True" OnTextChanged="txt_iecode_TextChanged" />
                                <asp:Button ID="btnhelp" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                <asp:Label ID="lblCustName1" runat="server" CssClass="elementLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="mandatoryField">*</span><span class="elementLabel">ORM No:</span>
                            </td>
                            <td align="left" style="white-space: nowrap">
                                <asp:TextBox runat="server" CssClass="textBox" ID="txt_irmno" Width="100px" MaxLength="50"
                                    OnTextChanged="txt_irmno_TextChanged" AutoPostBack="True" />
                                <asp:Button ID="btncusthelp" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                                <asp:Label runat="server" ID="lblOrmAmountLBL" Text=" Balance ORM Amount: " CssClass="elementLabel"></asp:Label>
                                <asp:Label runat="server" ID="lblOrmAmount" CssClass="elementLabel"></asp:Label>
                                <asp:Label runat="server" ID="lblcurr" CssClass="elementLabel"></asp:Label>
                            </td>
                            <%--<td align="right">
                              <span class="mandatoryField">*</span><span class="elementLabel">Ad Code:</span>
                              </td>--%>
                            <%--<td align="left">
                              <asp:TextBox runat="server" CssClass="textBox" ID="txt_adcode" Width=100px MaxLength=7 />
                              </td>--%>
                            <%--     <td align="right">
                              <span class="mandatoryField">*</span> <span class="elementLabel">IE Code:</span>
                              </td>
                              <td align="left">
                              <asp:TextBox runat="server" CssClass="textBox" ID="txt_iecode" Width=100px MaxLength=10/>
                              </td>--%>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="mandatoryField">*</span><span class="elementLabel">Currency:</span>
                            </td>
                            <td align="left">
                                <asp:DropDownList runat="server" CssClass="dropdownList" ID="ddlcurr" Width="70px">
                                </asp:DropDownList>
                            </td>
                            <td align="right" style="white-space: nowrap">
                                <span class="mandatoryField">*</span><span class="elementLabel">Adjustment Amount:</span>
                            </td>
                            <td align="left" style="white-space: nowrap">
                                <asp:TextBox runat="server" CssClass="textBox" ID="txt_adjamt" Width="100px" Style="text-align: right" />
                            </td>
                            <td align="right" style="white-space: nowrap">
                                <span class="mandatoryField">*</span><span class="elementLabel">Adjustment Date:</span>
                            </td>
                            <td align="left" style="white-space: nowrap">
                                <asp:TextBox ID="txt_adjdate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"></asp:TextBox>
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
                            <td align="right">
                                <span class="mandatoryField">*</span><span class="elementLabel">Reason For Adjustment:</span>
                            </td>
                            <td align="left" colspan="3">
                                <asp:DropDownList runat="server" ID="DdladjustInd" CssClass="dropdownList" Width="450px">
                                    <asp:ListItem Text="Refund of full import proceeds (import not taken place)" Value="1" />
                                    <asp:ListItem Text="Import document (BE not in System)" Value="2" />
                                    <asp:ListItem Text="Refund of untilised import payments due to quality issue/sort shipment"
                                        Value="3" />
                                    <asp:ListItem Text="Others" Value="4" />
                                    <asp:ListItem Text="BOE Prior To date 01/04/2016" Value="5" />
                                    <asp:ListItem Text="BOE Waiver" Value="6" />
                                    <asp:ListItem Text="Import through Courier" Value="7" />
                                </asp:DropDownList>
                            </td>
                            <td align="right" style="white-space: nowrap">
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
                                <span class="elementLabel"><span class="mandatoryField">*</span> Letter No:</span>
                            </td>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txt_letterNo" CssClass="textBox" Width="100px" MaxLength="50" />
                            </td>
                            <td align="right" style="white-space: nowrap">
                                <span class="mandatoryField">*</span><span class="elementLabel">Letter Date:</span>
                            </td>
                            <td align="left" style="white-space: nowrap">
                                <asp:TextBox ID="txtletterdate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                    ValidationGroup="DateValid"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtletterdate" InputDirection="RightToLeft" AcceptNegative="Left"
                                    ErrorTooltipEnabled="True" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="btncalendar_LetDate1" runat="server" CssClass="btncalendar_enabled"
                                    TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtletterdate" PopupButtonID="btncalendar_LetDate1" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                    ValidationGroup="DateValid" ControlToValidate="txtletterdate" EmptyValueMessage="Enter Date"
                                    InvalidValueBlurredMessage="Invalid date" EmptyValueBlurredText="*">
                                </ajaxToolkit:MaskedEditValidator>
                            </td>
                            <td align="right">
                                <span class="mandatoryField">*</span><span class="elementLabel">Swift Code :</span>
                            </td>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txtswift" CssClass="textBox" Width="100px" MaxLength="15" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">BOE No:</span>
                            </td>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txt_docno" CssClass="textBox" Width="100px" MaxLength="20" />
                            </td>
                            <td align="right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">BOE Date:</span>
                            </td>
                            <td align="left" style="white-space: nowrap">
                                <asp:TextBox ID="txt_docdate" runat="server" CssClass="textBox" Width="80px" MaxLength="10"
                                    ValidationGroup="BOEDateValid"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="mdRemdate2" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txt_docdate" InputDirection="RightToLeft" AcceptNegative="Left"
                                    ErrorTooltipEnabled="True" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="btncalendar_DocDate2" runat="server" CssClass="btncalendar_enabled"
                                    TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="calendarFromDate3" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txt_docdate" PopupButtonID="btncalendar_DocDate2" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="mdRemdate2"
                                    ValidationGroup="BOEDateValid" ControlToValidate="txt_docdate" EmptyValueMessage="Enter Date"
                                    InvalidValueBlurredMessage="Invalid date" EmptyValueBlurredText="*">
                                </ajaxToolkit:MaskedEditValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="elementLabel">Port Of descharge:</span>
                            </td>
                            <td align="left">
                                <asp:TextBox runat="server" ID="txt_recport" CssClass="textBox" Width="100px" MaxLength="20" />
                            </td>
                            <%--<td align="right" style="white-space:nowrap">
                              <span class="mandatoryField">*</span><span class="elementLabel">adjust Sequence No:</span>
                              </td>
                              
                              <td align="left">
                              <asp:TextBox runat="server" ID="txt_closseqno" CssClass="textBox" Width=100px 
                                  MaxLength=10 AutoPostBack=true ontextchanged="txt_closseqno_TextChanged"/>  
                              </td>--%>
                            <%--<td align="right" style="white-space:nowrap">
                              <span class="mandatoryField">*</span><span class="elementLabel">Remittance Indicator:</span>
                           </td>
                           <td align="left">
                              <asp:DropDownList runat="server" ID="ddlremclose" CssClass="dropdownList" Width=70px>
                                 <asp:ListItem Text="New" Value="1" />
                                 <asp:ListItem Text="Cancel" Value="3" />
                              </asp:DropDownList>
                           </td>--%>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="elementLabel">Remarks:</span>
                            </td>
                            <td colspan="5" align="left">
                                <asp:TextBox runat="server" CssClass="textBox" ID="txt_remarks" Width="450px" MaxLength="200" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault" ToolTip="Save"
                                    OnClick="btnSave_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                    ToolTip="Cancel" OnClick="btnCancel_Click" />
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
