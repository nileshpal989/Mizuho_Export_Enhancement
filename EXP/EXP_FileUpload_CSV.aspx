<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EXP_FileUpload_CSV.aspx.cs" Inherits="EXP_EXP_FileUpload_CSV" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-Tradefinance System</title>
     <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script language="javascript" type="text/javascript">

        function validateSave() {

            var ddlBranch = document.getElementById('ddlBranch');
            // var ddlBranchValue = ddlBranch.options[ddlBranch.selectedIndex].value;

            if (ddlBranch.value == "0") {
                alert('Select Branch.');
                ddlBranch.focus();
                return false;
            }

            var filelen = document.getElementById('fileinhouse').value.length;
            var strext = document.getElementById('fileinhouse').value.substring(filelen - 4, filelen);

            if (document.getElementById('fileinhouse').value == "") {
                alert('Select csv file to import.');
                try {
                    document.getElementById('fileinhouse').focus();
                    return false;
                }
                catch (err) {
                    return false;
                }
            }
            else {
                strext = strext.toLowerCase();
                //if (strext != '.xls' && strext != 'xlsx') {
                if (strext != '.csv') {
                    alert('Invalid file format.');
                    return false;
                }
                else {
                    return true;
                }
            }

            return true;
        }


        function checkSysDate(controlID) {

            var obj = controlID;

            if (controlID.value != "__/__/____") {

                var day = obj.value.split("/")[0];

                var month = obj.value.split("/")[1];
                var year = obj.value.split("/")[2];


                var dt = new Date(year, month - 1, day);
                var today = new Date();

                if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year) || (dt > today)) {

                    alert("Invalid Date");
                    controlID.focus();
                    return false;
                }
            }
        }
	
   
		
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" defaultbutton="btnupload">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
     <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
		<ProgressTemplate>
			<div id="progressBackgroundMain" class="progressBackground">
				<div id="processMessage" class="progressimageposition">
					<img src="~/Images/ajax-loader.gif" style="border: 0px" alt="" />
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
                    <asp:PostBackTrigger ControlID="btnupload" />
                </Triggers>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <asp:Label ID="pgLabel" runat="server" CssClass="pageLabel"></asp:Label>
                            </td>
                            <td align="right" style="width: 50%">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100%" valign="top" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top" colspan="2">
                                <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 150%">
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Branch :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" runat="server" TabIndex="1">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td align="right" style="width: 150px">
                                            <span class="elementLabel">Select File :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:FileUpload ID="fileinhouse" runat="server" contenteditable="false" Height="17px"
                                                Width="350" TabIndex="2" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Co. ID :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtCoID" CssClass="textBox" runat="server" Width="40px" MaxLength="2"
                                                Text="1" TabIndex="3"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap>
                                            <span class="elementLabel">Upload Date :</span>
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:TextBox ID="txtUploadDate" runat="server" CssClass="textBox" MaxLength="10"
                                                ValidationGroup="dtVal" Width="70px" TabIndex="4"></asp:TextBox>
                                            <ajaxToolkit:MaskedEditExtender ID="mdDocdate" Mask="99/99/9999" MaskType="Date"
                                                runat="server" TargetControlID="txtUploadDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                                CultureTimePlaceholder=":" Enabled="True">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <asp:Button ID="btncalendar_DocDate" runat="server" CssClass="btncalendar_enabled"
                                                TabIndex="-1" />
                                            <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtUploadDate" PopupButtonID="btncalendar_DocDate" Enabled="True">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdDocdate"
                                                ValidationGroup="dtVal" ControlToValidate="txtUploadDate" EmptyValueMessage="Enter Date Value"
                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="Invalid"></ajaxToolkit:MaskedEditValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <table border="1" bordercolor="red" cellpadding="2">
                                                <tr>
                                                     <td width="500px">
                                                        <span class="mandatoryField" style="font-size: medium">Warning : </span><span class="elementLabel"
                                                            style="font-size: small">System Date Format should be <font color="red">dd/MM/yyyy</font></span><br /><span class="elementLabel"
                                                            style="font-size: small">Once the files are uploaded, Document Nos are generated
                                                            by the System and hence cannot be cancelled/undone.</span><br />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="elementLabel" style="font-size: small">Do
                                                            you want to upload NOW ?</span><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Button ID="btnupload" runat="server" Text="YES" OnClick="btnupload_Click" CssClass="buttonDefault"
                                                            TabIndex="6" />
                                                        <asp:Button ID="btnNo" runat="server" Text="NO" CssClass="buttonDefault" 
                                                            TabIndex="5" onclick="btnNo_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <br />
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
