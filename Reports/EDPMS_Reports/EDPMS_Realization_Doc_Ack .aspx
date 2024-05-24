<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDPMS_Realization_Doc_Ack .aspx.cs" Inherits="Reports_EDPMS_Reports_EDPMS_Realization_Doc_Ack_" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../../Menu/Menu.ascx" TagName="menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/style_new.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">

        function generateReport() {

            var brname = document.getElementById('ddlBranch');
            var rdball = document.getElementById('rdball');
            var rdbsucessful = document.getElementById('rdbsuccessful');
            var rdbonlyerror = document.getElementById('rdberror');
            var ErrorCode = "";
            var modtype = "";
            if (brname.value == "---Select---") {

                alert('Please select branchname');
                brname.focus();
                return false;

            }
            if (rdball.checked == true) {

                ErrorCode = 'All';

            }

            else if (rdbsucessful.checked == true) {

                ErrorCode = 'SUCCESS';

            }
            else if (rdbonlyerror.checked == true) {
                ErrorCode = 'Error';

            }

            var winame = window.open('View_Realization_Doc_Ack.aspx?branchname=' + brname.value + '&ErrorCode=' + ErrorCode, '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1200,height=600');
            winame.focus();
            return false;
            //return true;
        }
    
    </script>
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
                        <td align=left><span class=pageLabel>Payment Realized Of Document (Acknowledgement)</span></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 50%" valign="top">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top">
                                <table cellspacing="0" cellpadding="0" border="0" width="100%" style="line-height: 150%">
                                    <tr>
                                        <td width="10%" align="right" nowrap>
                                            <span class="mandatoryField">*</span><span class="elementLabel">Branch :</span>
                                        </td>
                                        <td align="left" nowrap>
                                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" AutoPostBack="true"
                                                Width="100px" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                    <td>
                                    &nbsp;
                                    </td>
                                    <td align="left" nowrap>
                                    <asp:RadioButton ID="rdball" runat="server" Text="All" CssClass="elementLabel" Checked="true" GroupName="ErrorCode" />
                                    <asp:RadioButton ID="rdbsuccessful" runat="server" CssClass="elementLabel" Text="Successful" GroupName="ErrorCode" />
                                     <asp:RadioButton ID="rdberror" runat="server"  CssClass="elementLabel" Text="Only Error" GroupName="ErrorCode" />                                   
                                    </td>
                                    </tr>

                                </table>
                               
                                <table width="100%">
                                    <tr valign="bottom">
                                        <td align="right" style="width: 10%">
                                        </td>
                                        <td align="left" style="width: 700px; padding-top: 10px; padding-bottom: 10px" valign="bottom">
                                            &nbsp;
                                            <asp:Button ID="Generate" runat="server" CssClass="buttonDefault" Text="Generate"
                                                ToolTip="Genarate" TabIndex="4" />
                                            <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="false" />
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
