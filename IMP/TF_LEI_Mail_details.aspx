<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_LEI_Mail_details.aspx.cs" Inherits="IMP_Transactions_TF_LEI_Mail_details" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-Tradefinance System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/Style_V2.css" rel="Stylesheet" type="text/css" media="screen" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Scripts/jquery.min.js" language="javascript" type="text/javascript"></script>
    <script src="../Help_Plugins/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <link href="../Help_Plugins/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Help_Plugins/Myjquery2.js" language="javascript" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
       function validateSave() {
           var _To = document.getElementById('txtTo');
           var _subject = document.getElementById('txtsubject');
           var _Servicetime1 = document.getElementById('txtservicetime1');
           var _Servicetime2 = document.getElementById('txtservicetime2');

           if (_To.value == '') {
               alert('Please enter To Email Id.');
               _To.focus();
               return false;
           }
           if (_subject.value == '') {
               alert('Please enter subject.');
               _subject.focus();
               return false;
           }
           if (_Servicetime1.value == '') {
               alert('Please enter Service time.');
               _Servicetime1.focus();
               return false;
           }
           if (_Servicetime2.value == '') {
               alert('Please enter Service time.');
               _Servicetime2.focus();
               return false;
           }
           return true;
       }

    </script>
    <script type="text/javascript">
        function ClearMsg() {
            document.getElementById('lblcapital').style.color = "red";
            document.getElementById('lblletter').style.color = "red";
            document.getElementById('lblnumber').style.color = "red";
            document.getElementById('lblSpecial').style.color = "red";
            document.getElementById('lbllenght').style.color = "red";
            return true;
        }
    </script>
    <style type="text/css">
        .style9
        {
            width: 40%;
        }
        .style10
        {
            width: 10%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" defaultbutton="btnSave">
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
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="conditional">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 100%" valign="bottom">
                                <span class="pageLabel">LEI Email Details</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100%" valign="top">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField" Font-Size="Medium" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top">
                                <table cellspacing="0" cellpadding="0" border="0" width="450px" style="line-height: 150%;
                                    width: 1267px;">
                                    <tr>
                                        <td align="right" style="width: 120px">
                                            <span class="elementLabel">&nbsp;&nbsp; Report Type : &nbsp;</span>
                                        </td>
                                        <td align="left" style="white-space: nowrap">
                                            <asp:DropDownList ID="ddlReporttype" CssClass="dropdownList" TabIndex="1" runat="server">
                                                <%--<asp:ListItem Text="--Select--" Value=""></asp:ListItem>--%>
                                                <asp:ListItem Text="EOD Report" Value="EOD Report"></asp:ListItem>
                                                <asp:ListItem Text="Sight Bill Report" Value="Sight Bill Report"></asp:ListItem>
                                                <%--<asp:ListItem Text="Sight Bill Report" Value="Exp"></asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </td>
                                       </tr>
                                    <tr>
                                        <td align="right" style="width: 120px">
                                            <span class="elementLabel">&nbsp;&nbsp; Module : &nbsp;</span>
                                        </td>
                                        <td align="left" style="white-space: nowrap">
                                            <asp:DropDownList ID="ddlModule" CssClass="dropdownList" TabIndex="1" runat="server">
                                               <%-- <asp:ListItem Text="--Select--" Value=""></asp:ListItem>--%>
                                                <asp:ListItem Text="Import" Value="Import"></asp:ListItem>
                                                <asp:ListItem Text="Export" Value="Export"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 120px">
                                            <span class="elementLabel">TO : &nbsp;</span>
                                        </td>
                                        <td align="left" style="white-space: nowrap">
                                            <asp:TextBox ID="txtTo" runat="server" CssClass="textBox" TextMode="MultiLine" 
                                              MaxLength="200" Width="700px" TabIndex="2"></asp:TextBox>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td align="right" style="width: 120px">
                                            <span class="elementLabel">CC : &nbsp;</span>
                                        </td>
                                        <td align="left" style="white-space: nowrap">
                                            <asp:TextBox ID="txtCC" runat="server" CssClass="textBox" TextMode="MultiLine" 
                                               MaxLength="100" Width="700px" TabIndex="3"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align="right" style="width: 120px">
                                            <span class="elementLabel">BCC : &nbsp;</span>
                                        </td>
                                        <td align="left" style="white-space: nowrap">
                                            <asp:TextBox ID="txtBCC" runat="server" CssClass="textBox" TextMode="MultiLine" 
                                               MaxLength="100" Width="700px" TabIndex="4"></asp:TextBox>
                                        </td>
                                    </tr>
                                    
                                    <tr style="height: 70px">
                                        <td align="right" valign="top">
                                            <span class="elementLabel">Subject : &nbsp;</span>
                                        </td>
                                        <td style="white-space: nowrap">
                                            <asp:TextBox ID="txtsubject" runat="server" CssClass="textBox" MaxLength="100" Width="400px"
                                                TextMode="MultiLine" Height="65px" TabIndex="5"></asp:TextBox>
                                                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtsubject"
                                                    ForeColor="Red" SetFocusOnError="true" Display="Dynamic"
                                                    ErrorMessage="Special characters are not allowed" ID="rfvname"
                                                    ValidationExpression="^[\sa-zA-Z0-9/?:().,'+-]*$">
                                                    <%--"[^0-9a-zA-Z /?:().,'+-]">--%>
                                                </asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 120px">
                                            <span class="elementLabel">Service schedule time&nbsp;</span>
                                        </td>
                                        <td align="left" style="white-space: nowrap">
                                            <asp:TextBox ID="txtservicetime1" runat="server" CssClass="textBox" 
                                              MaxLength="2" Width="15px" TabIndex="6"></asp:TextBox>:
                                            <asp:TextBox ID="txtservicetime2" runat="server" CssClass="textBox" 
                                              MaxLength="2" Width="15px" TabIndex="6"></asp:TextBox>
                                            <asp:Label ID="label1" runat="server" Text="24 Hour Format [13:10]" CssClass="mandatoryField"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style10">
                                        </td>
                                        <td align="left" style="padding-top: 10px; padding-bottom: 10px" class="style9">
                                            &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" Width="120" 
                                                CssClass="buttonDefault" OnClick="btnSave_Click" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="120"
                                                CssClass="buttonDefault" OnClick="btnCancel_Click" />
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
