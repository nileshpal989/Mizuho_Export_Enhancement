<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentExtnAck.aspx.cs" Inherits="EDPMS_PaymentExtnAck" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
       <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>

        <script type="text/javascript" language="javascript">
            function validate() {
                var uploadcontrol = document.getElementById('<%=FileUpload1.ClientID%>').value;
                //Regular Expression for fileupload control.
                var reg = /^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.xml|.XML)$/;
                if (uploadcontrol.length > 0) {
                    //Checks with the control value.
                    if (reg.test(uploadcontrol)) {
                        return true;
                    }
                    else {
                        //If the condition not satisfied shows error message.
                        alert("Only XML files are allowed!");
                        return false;
                    }
                }
            } //End of function validate.
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
          <%--<uc1:Menu ID="Menu1" runat="server" />--%>
          <uc1:Menu ID="Menu1" runat="server"/>

        <table width="100%">
            <tr>
                <td align="left" colspan="2">
                    <span class="pageLabel">XML File Upload - Payment Extension Acknowledgement</span>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    <asp:Label CssClass="mandatoryField" ID="lblmessage" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" width="8%" nowrap>
                    <span class="elementLabel">Branch :</span>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlRefNo" runat="server" CssClass="textBox">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <span class="elementLabel">Select File : </span>
                </td>
                <td align="left">
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <br>
                </td>
            </tr>
         
            <tr>
                <td>
                </td>
                <td align="left">
                    <asp:Button ID="btnupload" CssClass="buttonDefault" Text="Upload" runat="server"
                        OnClick="btnupload_Click" OnClientClick="return validate();"  />
                 
                </td>
            </tr>
            <tr height="60px" valign="bottom">
                <td>
                </td>
                <td align="left">
                 <asp:Label ID="labelMessage" runat="server" CssClass="pageLabel"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
