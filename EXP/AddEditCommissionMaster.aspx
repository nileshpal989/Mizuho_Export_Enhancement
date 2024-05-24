<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddEditCommissionMaster.aspx.cs"
    Inherits="EXP_AddEditCommissionMaster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>LMCC-Trade Finance System</title>
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
                                <span class="pageLabel">Export Commission Master</span>
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
                                <table border="0" cellspacing="0" width="36%">
                                    <tr>
                                        <td align="Center" width="20%" nowrap>
                                            <span class="elementLabel">Bill Amt. From&nbsp;&nbsp;&nbsp;&nbsp;</span> <span class="elementLabel">
                                                &nbsp;&nbsp;&nbsp;&nbsp;Bill Amt. To</span>
                                        </td>
                                        <td align="center" width="8%" nowrap>
                                            <span class="elementLabel">Rate</span>
                                        </td>
                                        <td align="center" width="8%" nowrap>
                                            <span class="elementLabel">Minimum Rs.</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" nowrap>
                                            <asp:TextBox ID="txtRange1" runat="server" Height="14px" Width="100px" CssClass="textBox"
                                                onfocus="this.select()" Style="text-align: right;" TabIndex="1"></asp:TextBox>&nbsp;
                                            <asp:TextBox ID="txtRange2" runat="server" Height="14px" Width="100px" CssClass="textBox"
                                                onfocus="this.select()" Style="text-align: right;" TabIndex="2"></asp:TextBox>
                                        </td>
                                        <td align="center" nowrap>
                                            <asp:TextBox ID="txtRate1" runat="server" Height="14px" Width="60px" CssClass="textBox"
                                                onfocus="this.select()" Style="text-align: right;" TabIndex="3"></asp:TextBox>
                                            &nbsp;<span class="elementLabel">%</span>
                                        </td>
                                        <td align="center" nowrap>
                                            <asp:TextBox ID="txtMinAmt1" runat="server" Height="14px" Width="65px" CssClass="textBox"
                                                onfocus="this.select()" Style="text-align: right;" TabIndex="4"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" nowrap>
                                            <asp:TextBox ID="txtRange3" runat="server" Height="14px" Width="100px" CssClass="textBox"
                                                onfocus="this.select()" Style="text-align: right;" TabIndex="5"></asp:TextBox>&nbsp;
                                            <asp:TextBox ID="txtRange4" runat="server" Height="14px" Width="100px" CssClass="textBox"
                                                onfocus="this.select()" Style="text-align: right;" TabIndex="6"></asp:TextBox>
                                        </td>
                                        <td align="center" nowrap>
                                            <asp:TextBox ID="txtRate2" runat="server" Height="14px" Width="60px" CssClass="textBox"
                                                onfocus="this.select()" Style="text-align: right;" TabIndex="7"></asp:TextBox>
                                            &nbsp;<span class="elementLabel">%</span>
                                        </td>
                                        <td align="center" nowrap>
                                            <asp:TextBox ID="txtMinAmt2" runat="server" Height="14px" Width="65px" CssClass="textBox"
                                                onfocus="this.select()" Style="text-align: right;" TabIndex="8"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" nowrap>
                                            <asp:TextBox ID="txtRange5" runat="server" Height="14px" Width="100px" CssClass="textBox"
                                                onfocus="this.select()" Style="text-align: right;" TabIndex="9"></asp:TextBox>&nbsp;
                                            <asp:TextBox ID="txtRange6" runat="server" Height="14px" Width="100px" CssClass="textBox"
                                                onfocus="this.select()" Style="text-align: right;" TabIndex="10"></asp:TextBox>
                                        </td>
                                        <td align="center" nowrap>
                                            <asp:TextBox ID="txtRate3" runat="server" Height="14px" Width="60px" CssClass="textBox"
                                                onfocus="this.select()" Style="text-align: right;" TabIndex="11"></asp:TextBox>
                                             &nbsp;<span class="elementLabel">%</span>
                                        </td>
                                        <td align="center" nowrap>
                                            <asp:TextBox ID="txtMinAmt3" runat="server" Height="14px" Width="65px" CssClass="textBox"
                                                onfocus="this.select()" Style="text-align: right;" TabIndex="12"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" nowrap>
                                            <asp:TextBox ID="txtRange7" runat="server" Height="14px" Width="100px" CssClass="textBox"
                                                onfocus="this.select()" Style="text-align: right;" TabIndex="13"></asp:TextBox>&nbsp;
                                            <asp:TextBox ID="txtRange8" runat="server" Height="14px" Width="100px" CssClass="textBox"
                                                onfocus="this.select()" Style="text-align: right;" TabIndex="14"></asp:TextBox>
                                        </td>
                                        <td align="center" nowrap>
                                            <asp:TextBox ID="txtRate4" runat="server" Height="14px" Width="60px" CssClass="textBox"
                                                onfocus="this.select()" Style="text-align: right;" TabIndex="15"></asp:TextBox>
                                             &nbsp;<span class="elementLabel">%</span>
                                        </td>
                                        <td align="center" nowrap>
                                            <asp:TextBox ID="txtMinAmt4" runat="server" Height="14px" Width="65px" CssClass="textBox"
                                                onfocus="this.select()" Style="text-align: right;" TabIndex="16"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" nowrap>
                                            <asp:TextBox ID="txtRange9" runat="server" Height="14px" Width="100px" CssClass="textBox"
                                                onfocus="this.select()" Style="text-align: right;" TabIndex="17"></asp:TextBox>&nbsp;
                                            <asp:TextBox ID="txtRange10" runat="server" Height="14px" Width="100px" CssClass="textBox"
                                                onfocus="this.select()" Style="text-align: right;" TabIndex="18"></asp:TextBox>
                                        </td>
                                        <td align="center" nowrap>
                                            <asp:TextBox ID="txtRate5" runat="server" Height="14px" Width="60px" CssClass="textBox"
                                                onfocus="this.select()" Style="text-align: right;" TabIndex="19"></asp:TextBox>
                                             &nbsp;<span class="elementLabel">%</span>
                                        </td>
                                        <td align="center" nowrap>
                                            <asp:TextBox ID="txtMinAmt5" runat="server" Height="14px" Width="65px" CssClass="textBox"
                                                onfocus="this.select()" Style="text-align: right;" TabIndex="20"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" nowrap>
                                            <asp:TextBox ID="txtRange11" runat="server" Height="14px" Width="100px" CssClass="textBox"
                                                onfocus="this.select()" Style="text-align: right;" TabIndex="21"></asp:TextBox>&nbsp;
                                            <asp:TextBox ID="txtRange12" runat="server" Height="14px" Width="100px" CssClass="textBox"
                                                onfocus="this.select()" Style="text-align: right;" TabIndex="22"></asp:TextBox>
                                        </td>
                                        <td align="center" nowrap>
                                            <asp:TextBox ID="txtRate6" runat="server" Height="14px" Width="60px" CssClass="textBox"
                                                onfocus="this.select()" Style="text-align: right;" TabIndex="23"></asp:TextBox>
                                             &nbsp;<span class="elementLabel">%</span>
                                        </td>
                                        <td align="center" nowrap>
                                            <asp:TextBox ID="txtMinAmt6" runat="server" Height="14px" Width="65px" CssClass="textBox"
                                                onfocus="this.select()" Style="text-align: right;" TabIndex="24"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap>
                                            <span class="elementLabel">Commission in Lieu of Exchange :</span>
                                        </td>
                                        <td align="center" nowrap>
                                            <asp:TextBox ID="txtCommissionRate" runat="server" Height="14px" Width="60px" CssClass="textBox"
                                                onfocus="this.select()" Style="text-align: right;" TabIndex="25"></asp:TextBox>
                                             &nbsp;<span class="elementLabel">%</span>
                                        </td>
                                        <td align="center" nowrap>
                                            <asp:TextBox ID="txtCommissionMinAmt" runat="server" Height="14px" Width="65px" CssClass="textBox"
                                                onfocus="this.select()" Style="text-align: right;" TabIndex="26"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault" ToolTip="Save"
                                                TabIndex="27" onclick="btnSave_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                ToolTip="Cancel" TabIndex="28" onclick="btnCancel_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
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
