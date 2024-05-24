<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_BROPRINTING.aspx.cs" Inherits="IMP_IMPReports_TF_IMP_BROPRINTING" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Src="~/Menu/Menu.ascx" TagName="menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../../Style/Style_V2.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script language="javascript" type="text/javascript">

        
        function OpenBRONoHelp() {
            var date = document.getElementById('txtdate').value;
            var Branch = document.getElementById('ddlBranch').value;
           // var brotype = '';
            var rpt = window.open('../HelpForms/Brohelpform1.aspx?Branch=' + Branch + '&Date=' + date, 'helpbroId', 'height=320,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            rpt.focus();
            common = "helpbroId";
            return false;
        }

        function selectBroNo(Brono) {
            document.getElementById('txtbroNo').value = Brono;
        }
        //help.....................................



        function generateReport() {

            var Branchcode = document.getElementById('ddlBranch');
            var txtbroNo = document.getElementById('txtbroNo');
            
            var Type = "";

            if (document.getElementById('rdbAllbro').checked == true) {
                Type = "All";
            }

             if (document.getElementById('rbdselectedbro').checked == true) {
                Type = txtbroNo.value;
            }
            if (document.getElementById('rbdselectedbro').checked == true) {
                if (txtbroNo.value == '') {
                    alert('Select BRO no.');
                    txtbroNo.focus();
                    return false;
                }
            }
           
            var txtdate = document.getElementById('txtdate');


            if (txtdate.value == "") {
                alert('Please select  Date')
                txtdate.focus();
                return false;
            }
            var winame = window.open('TF_IMP_ViewBroReport.aspx?Branchcode=' + Branchcode.value + '&Date=' + txtdate.value + '&Type=' + Type, '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1300,height=700');
            winame.focus();
            return false;

        }
    </script>
     <style type="text/css">
        hr
        {
            display: block;
            margin-top: 0.5em;
            margin-bottom: 0.5em;
            margin-left: auto;
            margin-right: auto;
            border-style: inset;
            border-width: 2px;
        }
         .style2
         {
             width: 168px;
             height: 24px;
         }
         .style3
         {
             height: 24px;
         }
         .style6
         {
             width: 168px;
         }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script src="../../Scripts/InitEndRequest.js" type="text/javascript"></script>
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
            <uc1:menu ID="Menu1" runat="server" />
            <br />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="pageLabel"><strong>BANK RELEASE ORDER LETTER</strong></span>
                                <%--<asp:Label ID="PageHeader" CssClass="pageLabel" runat="server"></asp:Label>--%>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 50%" valign="top">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100%" valign="top">
                                <asp:Label ID="lblmessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top">
                                <table cellspacing="0" cellpadding="0" border="0" width="1300px" style="line-height: 150%">
                                    <tr>
                                        <td align="right">
                                            <span class="mandatoryField">*</span><span class="elementLabel">Branch :</span>
                                        </td>
                                        <td align="left" > &nbsp;
                                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" AutoPostBack="true"
                                                Width="100px" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style6">
                                            <span class="mandatoryField">&nbsp; *</span><span class="elementLabel">
                                           Bro Value Date :</span>
                                        </td>
                                        <td align="Left" style="width: 800px"> &nbsp;
                                            <asp:TextBox ID="txtdate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtval"
                                                Width="70" TabIndex="2"></asp:TextBox>
                                            <asp:Button ID="btncalendar_FromDate" runat="server" CssClass="btncalendar_enabled"
                                                TabIndex="-1" />
                                            <ajaxToolkit:MaskedEditExtender ID="mdfdate" Mask="99/99/9999" MaskType="Date" runat="server"
                                                TargetControlID="txtdate" InputDirection="RightToLeft" AcceptNegative="Left"
                                                ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtdate" PopupButtonID="btncalendar_FromDate">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdfdate"
                                                ValidationGroup="dtVal" ControlToValidate="txtdate" EmptyValueMessage="Enter Date Value"
                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                            </ajaxToolkit:MaskedEditValidator> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            &nbsp;
                                        </td>
                                        <td align="left" nowrap>
                                            <asp:RadioButton ID="rdbAllbro" runat="server" CssClass="elementLabel" GroupName="rd1"
                                            TabIndex="3" Text="All BRO's" Checked="true" AutoPostBack="true"/>
                                          
                                          <asp:RadioButton ID="rbdselectedbro" runat="server" CssClass="elementLabel" GroupName="rd1"
                                                TabIndex="4" AutoPostBack="true" Text="Selected BRO's"/>
                                            <br />&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                           <asp:TextBox ID="txtbroNo" runat="server" CssClass="textBox" MaxLength="50" ValidationGroup="dtval"
                                                Width="170" TabIndex="2" Visible="false"></asp:TextBox>
                                            <asp:Button ID="btnbro" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"  Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr valign="bottom">
                                        <td align="right" style="width: 120px">
                                        </td>
                                        <td align="left" style="width: 700px; padding-top: 10px; padding-bottom: 10px" valign="bottom">
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                            <asp:Button ID="Generate" runat="server" CssClass="buttonDefault" Text="Generate"
                                                ToolTip="Genarate" TabIndex="4"  />
                                        </td>
                                    </tr>
                                </table>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
</body>
</html>
