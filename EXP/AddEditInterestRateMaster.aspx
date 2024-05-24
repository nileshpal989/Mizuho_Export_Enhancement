<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddEditInterestRateMaster.aspx.cs"
    Inherits="EXPORT_AddEditInterestRateMaster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRADE FINANCE System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script language="javascript" type="text/javascript">

        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            //alert(charCode);
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;

        }
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
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
        <uc2:Menu ID="Menu1" runat="server" />
        <br />
        <br />
        <center>
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="conditional">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel">Interest Rate Master</span>
                            </td>
                            <%-- <td align="right" style="width: 50%">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                    OnClick="btnBack_Click" />
                            </td>--%>
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
                                <table border="0" cellspacing="0" width="43%">
                                    <tr>
                                        <td width="6%">
                                            &nbsp;
                                        </td>
                                        <td align="center" nowrap width="5%">
                                            <span class="elementLabel">Max. Days</span>
                                        </td>
                                        <td align="center" nowrap width="5%">
                                            <span class="elementLabel">Out of Days</span>
                                        </td>
                                        <td align="center" nowrap width="8%">
                                            <span class="elementLabel">Range Days</span>
                                        </td>
                                        <td align="center" nowrap width="5%">
                                            <span class="elementLabel">Int. Rate</span>
                                        </td>
                                        <td align="center" nowrap width="5%">
                                            <span class="elementLabel">Overdue Int</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap>
                                            <span class="elementLabel">Sight Bills :</span>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txtSightMaxDays" runat="server" Height="14px" Width="35px" Style="text-align: right;"
                                                CssClass="textBox" onfocus="this.select()" TabIndex="1"></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txtSightOutDays" runat="server" Height="14px" Width="35px" Style="text-align: right;"
                                                CssClass="textBox" onfocus="this.select()" TabIndex="2"></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txtSightDayRange1" runat="server" Height="14px" Width="35px" Style="text-align: right;"
                                                CssClass="textBox" onfocus="this.select()" TabIndex="3"></asp:TextBox>&nbsp;<span
                                                    class="elementLabel">To</span>
                                            <asp:TextBox ID="txtSightDayRange2" runat="server" Height="14px" Width="35px" Style="text-align: right;"
                                                CssClass="textBox" onfocus="this.select()" TabIndex="4"></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txtSightInterestRate" runat="server" Height="14px" Width="35px"
                                                Style="text-align: right;" CssClass="textBox" onfocus="this.select()" TabIndex="5"></asp:TextBox>&nbsp;<span
                                                    class="elementLabel">%</span>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txtSightOverdueInterest" runat="server" Height="14px" Width="35px"
                                                Style="text-align: right;" CssClass="textBox" onfocus="this.select()" TabIndex="6"></asp:TextBox>&nbsp;<span
                                                    class="elementLabel">%</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Usance Bills :</span>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txtUsanceMaxDays" runat="server" Height="14px" Width="35px" Style="text-align: right;"
                                                CssClass="textBox" onfocus="this.select()" TabIndex="7"></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txtUsanceOutDays" runat="server" Height="14px" Width="35px" Style="text-align: right;"
                                                CssClass="textBox" onfocus="this.select()" TabIndex="8"></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txtUsanceDayRange1" runat="server" Height="14px" Width="35px" Style="text-align: right;"
                                                CssClass="textBox" onfocus="this.select()" TabIndex="9"></asp:TextBox>&nbsp;<span
                                                    class="elementLabel">To</span>
                                            <asp:TextBox ID="txtUsanceDayRange2" runat="server" Height="14px" Width="35px" Style="text-align: right;"
                                                CssClass="textBox" onfocus="this.select()" TabIndex="10"></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txtUsanceInterestRate1" runat="server" Height="14px" Width="35px"
                                                Style="text-align: right;" CssClass="textBox" onfocus="this.select()" TabIndex="11">&nbsp;</asp:TextBox>&nbsp;<span
                                                    class="elementLabel">%</span>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="TxtUsanceOverdueInterest1" runat="server" Height="14px" Width="35px"
                                                Style="text-align: right;" CssClass="textBox" onfocus="this.select()" TabIndex="12">&nbsp;</asp:TextBox>&nbsp;<span
                                                    class="elementLabel">%</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            &nbsp;
                                        </td>
                                        <td align="center">
                                            &nbsp;
                                        </td>
                                        <td align="center">
                                            &nbsp;
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txtUsanceDayRange3" runat="server" Height="14px" Width="35px" Style="text-align: right;"
                                                CssClass="textBox" onfocus="this.select()" TabIndex="13"></asp:TextBox>&nbsp;<span
                                                    class="elementLabel">To</span>
                                            <asp:TextBox ID="txtUsanceDayRange4" runat="server" Height="14px" Width="35px" Style="text-align: right;"
                                                CssClass="textBox" onfocus="this.select()" TabIndex="14"></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txtUsanceInterestRate2" runat="server" Height="14px" Width="35px"
                                                Style="text-align: right;" CssClass="textBox" onfocus="this.select()" TabIndex="15"></asp:TextBox>&nbsp;<span
                                                    class="elementLabel">%</span>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="TxtUsanceOverdueInterest2" runat="server" Height="14px" Width="35px"
                                                Style="text-align: right;" CssClass="textBox" onfocus="this.select()" TabIndex="16"></asp:TextBox>&nbsp;<span
                                                    class="elementLabel">%</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">E.B.R. Bills :</span>
                                        </td>
                                        <td align="center">
                                            &nbsp;
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txtEBROutDays" runat="server" Height="14px" Width="35px" Style="text-align: right;"
                                                CssClass="textBox" onfocus="this.select()" TabIndex="17"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            <span class="elementLabel">LIBOR +&nbsp;&nbsp;&nbsp;</span>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txtLiborInterestRate" runat="server" Height="14px" Width="35px"
                                                Style="text-align: right;" CssClass="textBox" onfocus="this.select()" TabIndex="18"></asp:TextBox>&nbsp;<span
                                                    class="elementLabel">%</span>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txtLiborOverdueInterest" runat="server" Height="14px" Width="35px"
                                                Style="text-align: right;" CssClass="textBox" onfocus="this.select()" TabIndex="19"></asp:TextBox>&nbsp;<span
                                                    class="elementLabel">%</span>
                                        </td>
                                    </tr>
                                </table>
                                <table border="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault" ToolTip="Save"
                                                TabIndex="20" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                ToolTip="Cancel" TabIndex="21" OnClick="btnCancel_Click" />
                                        </td>
                                    </tr>
                                </table>
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
