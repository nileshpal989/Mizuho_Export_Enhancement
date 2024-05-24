<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EXP_RemittanceAdvance_CSV.aspx.cs"
    Inherits="EXP_EXP_RemittanceAdvance_CSV" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"> </script>
    <script type="text/javascript">

        function openDocNo(e, hNo) {
            var keycode;
            var txtDate = document.getElementById('txtDate');
            // var ddlBranch = document.getElementById('ddlBranch');
            if (keycode == 113 || e == 'mouseClick') {
                open_popup('../EXP/EXP_DocNo_Help.aspx?Document_No=' + txtDate.value, 500, 500, 'DocNo');
                return false;
            }
            return true;
        }

        function selectDoc(id) {
            var txtDocno = document.getElementById('txtDocno');
            txtDocno.value = id;
            __doPostBack('txtDocno');
            document.getElementById('DC').style.display = 'block';
            return true;
        }

        function toggledisplay() {
            var rbtAllDocNo = document.getElementById('rbtAllDocNo');
            var rbtSelectedDocNo = document.getElementById('rbtSelectedDocNo');
            var DC = document.getElementById('DC');

            if (rbtAllDocNo.checked == true) {
                DC.style.display = 'none';
            } else
                DC.style.display = 'block';
            return true;
        }

        function validate_save() {
            var txtDate = document.getElementById('txtDate');
            var txtDocno = document.getElementById('txtDocno');
            var rbtSelectedDocNo = document.getElementById('rbtSelectedDocNo');

            if (txtDate.value == '') {
                alert('Enter Date');
                txtDate.focus();
                return false;
            }
            if (rbtSelectedDocNo.checked == true) {
                if (txtDocno.value == '') {
                    alert('Enter Document No');
                    txtDocno.focus();
                    return false;
                }
            }
        }
     
    </script>
</head>
<body>
    <form id="form2" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/Enable_Disable_Opener.js"></script>
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
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <table cellspacing="2" cellpadding="2" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom" colspan="2">
                                <span class="pageLabel"><strong>Export Document against Advance Remittance CSV File</strong></span>
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
                            <td align="right" width="5%">
                                <span class="elementLabel">Branch :</span>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="dropdownList" TabIndex="1" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="5%">
                                <span class="elementLabel">For Date : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtDate" runat="server" CssClass="textBox" Width="70px"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                    runat="server" TargetControlID="txtDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="Button1" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtDate" PopupButtonID="Button1" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="left">
                                <asp:RadioButton ID="rbtAllDocNo" runat="server" CssClass="elementLabel" GroupName="Docno"
                                    Text="All Document No" Checked="true" />
                                <asp:RadioButton ID="rbtSelectedDocNo" runat="server" CssClass="elementLabel" GroupName="Docno"
                                    Text="Selected Document No" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="left">
                                <div id="DC" runat="server" style="display: none">
                                    <span class="elementLabel">Document No : </span>
                                    <asp:TextBox ID="txtDocno" runat="server" CssClass="textBox" OnTextChanged="txtDocno_TextChanged"></asp:TextBox>
                                    <asp:Button ID="btnHelpDocNo" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                        OnClientClick="openDocNo('mouseClick');" />
                                    &nbsp;
                                    <asp:Label ID="lblDocNo" runat="server" CssClass="elementLabel" Width="400px"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="left">
                                <asp:Button ID="btnGenerate" runat="server" CssClass="buttonDefault" Text="Generate"
                                    OnClick="btnGenerate_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
    <script type="text/javascript" language="javascript">
        window.onload = function () {
            toggledisplay();
        }
    </script>
</body>
</html>
