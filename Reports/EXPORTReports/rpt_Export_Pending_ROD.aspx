<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Export_Pending_ROD.aspx.cs" Inherits="Reports_EXPORTReports_rpt_Pending_ROD" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script type="text/javascript" src="../../Scripts/Enable_Disable_Opener.js"></script>
    <script type="text/javascript">
    </script>
    <script language="javascript" type="text/javascript">
        function Custhelp() {
            var fromDate = document.getElementById('txtFromDate');
            if (fromDate.value == '') {
                alert('Select As On Date.');
                fromDate.focus();
                return false;
            }
            popup = window.open('../../TF_CustomerLookUp1.aspx', 'helpCustId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "helpCustId"
            return false;
        }

        function CustId(event) {
            var key = event.keyCode;
            if (key == 113 && key != 13) {
                document.getElementById('btnCustList').click();
            }
        }

        
        function Countryhelp() {
            popup = window.open('../../TF_CurrencyLookUp2.aspx', 'helpCountryId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "helpCountryId"
            return false;

        }

        function CountryId(event) {
            var key = event.keyCode;
            if (key == 113 && key != 13) {
                document.getElementById('btnCountryList').click();
            }
        }
        function sss() {
            var s = popup.document.getElementById('txtcell1').value;
            if (common == "helpCustId") {
                document.getElementById('txtCustomer').value = s;
            }
            if (common == "helpCountryId") {
                document.getElementById('txtCountry').value = s;
            }
        }

    </script>
</head>
<body>
 <form id="form2" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
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
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                  <Triggers>
                    <asp:PostBackTrigger ControlID="btnGenerate" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel"><strong>Pending ROD Report</strong></span>
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
                                <table cellspacing="0" cellpadding="0" border="0" width="800px" style="line-height: 150%">
                                    <tr>
                                        <td width="12.5%" align="right" nowrap>
                                            <span class="mandatoryField">* </span><span class="elementLabel">Branch :</span>
                                        </td>
                                        <td align="left" nowrap>
                                            &nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                                                AutoPostBack="true" Width="100px" runat="server">
                                            </asp:DropDownList>
                                            <%--<asp:Button ID="btnPurCodeList" runat="server" CssClass="btnHelp_enabled" 
                                                    TabIndex="-1" />--%>
                                        </td>
                                    </tr>
                                    </table>
                               
                                <table cellspacing="0" cellpadding="0" border="0" width="800px" style="line-height: 150%">
                                    <tr>
                                        <td align="right" style="width: 100px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">From DOC Date :</span>
                                        </td>
                                        <td align="left" style="width: 700px">
                                            &nbsp;<asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10"
                                                ValidationGroup="dtVal" Width="70" TabIndex="2"></asp:TextBox>
                                            <asp:Button ID="btncalendar_FromDate" runat="server" CssClass="btncalendar_enabled"
                                                TabIndex="-1" />
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
                                            <%-- <asp:Button ID="btnChangeDate" runat="server" OnClick="btnChangeDate_Click" />--%>
                                            &nbsp; <span class="mandatoryField">*</span><span class="elementLabel">To DOC Date :</span>
                                            &nbsp;
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10" Width="70"
                                                TabIndex="3"></asp:TextBox>
                                            <asp:Button ID="btncalendar_ToDate" runat="server" CssClass="btncalendar_enabled" />
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                                runat="server" TargetControlID="txtToDate" InputDirection="RightToLeft" AcceptNegative="Left"
                                                ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtToDate" PopupButtonID="btncalendar_ToDate">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                ValidationGroup="dtVal" ControlToValidate="txtToDate" EmptyValueMessage="Enter Date Value"
                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                            </ajaxToolkit:MaskedEditValidator>
                                        </td>
                                    </tr> 
                                    </table>
                            
                    <table cellspacing="0" border="0">
                        <tr>
                            <td height="40px" align="left" valign="middle" width="150px">
                                <asp:RadioButton ID="rdbDocumnetwise" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                    GroupName="Data" OnCheckedChanged="rdbDocumnetwise_CheckedChanged" Text="Document wise"
                                    Style="forecolor: #000000; font-weight: bold;" TabIndex="24" Checked="True" />
                            </td>
                            <td height="20px" valign="middle" width="150px">
                                <asp:RadioButton ID="rdbCustomerwise" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                    Text="Customer wise" GroupName="Data" OnCheckedChanged="rdbCustomerwise_CheckedChanged"
                                    Style="forecolor: #000000; font-weight: bold;" TabIndex="25" />
                            </td>
                          
                            
                            <td height="40px" valign="middle" width="170px">
                                <asp:RadioButton ID="rdbCountrywise" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                    Text="Currency wise" GroupName="Data" OnCheckedChanged="rdbCountrywise_CheckedChanged"
                                    Style="forecolor: #000000; font-weight: bold;" TabIndex="34" />
                            </td>
                        </tr>
                    </table>
                    <fieldset id="CustList" runat="server" style="width: 900px" visible="false">
                        <legend><span class="elementLabel">Select Customer</span></legend>
                        <table id="Table1" runat="server">
                            <tr>
                                <td align="right" style="font-weight: bold; color: #000000; font-size: medium;" width="200px">
                                    <span style="color: Red">*</span><span class="elementLabel">Customer A/c No. :</span>&nbsp;
                                </td>
                                <td align="left" width="0px" style="font-weight: bold; color: #000000;">
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtCustomer" runat="server" CssClass="textBox" MaxLength="40" TabIndex="37"
                                        Visible="false" Width="100px" OnTextChanged="txtCustomer_TextChanged" AutoPostBack="True"></asp:TextBox>
                                    &nbsp;
                                    <asp:Button ID="BtnCustList" runat="server" CssClass="btnHelp_enabled" Visible="false" />
                                    &nbsp;
                                    <asp:Label ID="lblCustomerName" runat="server" CssClass="elementLabel" Visible="false"
                                        Width="400px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                
                    <fieldset id="CountryList" runat="server" style="width: 900px" visible="false">
                        <legend><span class="elementLabel">Select Currency</span></legend>
                        <table id="Table4" runat="server">
                            <tr>
                                <td align="right" style="font-weight: bold; color: #000000; font-size: medium;" width="200px">
                                    <span style="color: Red">*</span><span class="elementLabel">Currency Id. :&nbsp;</span>
                                </td>
                                <td align="left" width="0px" style="font-weight: bold; color: #000000;">
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtCountry" runat="server" CssClass="textBox" MaxLength="40" TabIndex="40"
                                        Visible="false" Width="100px" AutoPostBack="True" OnTextChanged="txtCountry_TextChanged"></asp:TextBox>
                                    &nbsp;
                                    <asp:Button ID="btnCountryList" runat="server" CssClass="btnHelp_enabled" Visible="false" />
                                    &nbsp;
                                    <asp:Label ID="lblCountyName" runat="server" CssClass="elementLabel" Visible="false"
                                        Width="400px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <table>
                        <tr valign="top">
                               <td align="left" style="width: 700px; padding-top: 10px; padding-bottom: 10px" valign="top">
                                &nbsp;  &nbsp;   &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp;  &nbsp;  &nbsp; &nbsp;  &nbsp;
                                <asp:Button ID="btnGenerate" runat="server" CssClass="buttonDefault" Text="Download"
                                    ToolTip="Download" TabIndex="41" onclick="btnGenerate_Click"/>
                               
                                <td>
                                    &nbsp;
                                </td>
                                <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                    Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                <asp:Button ID="Button1" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                    Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                            </td>
                        </tr>
                    </table>
                    
                    <input type="hidden" runat="server" id="hdnFromDate" />
                    <input type="hidden" runat="server" id="hdnToDate" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
    <p>
        &nbsp;</p>
</body>
</html>
