<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_AddEditRelationshipMaster.aspx.cs"
    Inherits="TF_AddEditRelationshipMaster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-TRDFIN System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="Images/favicon.ico" type="image/ico" />
    <link href="Style/Style.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/Validations.js"  language="javascript" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="Scripts/Enable_Disable_Opener.js"></script>
    <link href="Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script type="text/javascript" language="javascript">

        function Myfunction2() {

            Myfunction();
            var x2 = document.getElementById('txtBusinessSourceID1').value;
            var x3 = document.getElementById('txtBusinessSourceID2').value;
            var x4 = document.getElementById('txtBusinessSourceID3').value;
            var x5 = document.getElementById('txtBusinessSourceID4').value;
            var x6 = document.getElementById('txtBusinessSourceID5').value;

            var y;
            var v = document.getElementById('Button2').value;
            
            if (x2 != '') {
                if (x2[0] == v[0]) {
                    return true;
                }
                else {
                    alert('Enter only ' + v[0]);
                    return false;
                }
            }
            if (x3 != '') {
                if (x3[0] == v[0]) {
                    return true;
                }
                else {
                    alert('Enter only ' + v[0]);
                    return false;
                }
            }
            if (x4 != '') {
                if (x4[0] == v[0]) {
                    return true;
                }
                else {
                    alert('Enter only ' + v[0]);
                    return false;
                }
            }
                if (x5 != '') {
                    if (x5[0] == v[0]) {
                        return true;
                    }
                    else {
                        alert('Enter only ' + v[0]);
                        return false;
                    }
                }
                 if (x6 != '') {
                if (x6[0] == v[0]) {
                    return true;
                }
                else {
                    alert('Enter only ' + v[0]);
                    return false;
                }
            }
        }

        function Myfunction() {
            var x = document.getElementById("txtBusinessSourceID1").value;
            var x7 = document.getElementById("txtBusinessSourceID2").value;
            var x8 = document.getElementById("txtBusinessSourceID3").value;
            var x9 = document.getElementById("txtBusinessSourceID4").value;
            var x10 = document.getElementById("txtBusinessSourceID5").value;

            if (x != '') {
                for (i = 1; i < 4; i++) {
                    y = x.charAt(i);
                    if (y == " " || isNaN(y)) {
                        alert("Enter Only Numbers");
                    }
                }
            }
            if (x7 != '') {
                for (i = 1; i < 4; i++) {
                    y = x7.charAt(i);
                    if (y == " " || isNaN(y)) {
                        alert("Enter Only Numbers");
                    }
                }
            }
            if (x8 != '') {
                for (i = 1; i < 4; i++) {
                    y = x8.charAt(i);
                    if (y == " " || isNaN(y)) {
                        alert("Enter Only Numbers");
                    }
                }
            }
            if (x9 != '') {
                for (i = 1; i < 4; i++) {
                    y = x9.charAt(i);
                    if (y == " " || isNaN(y)) {
                        alert("Enter Only Numbers");
                    }
                }
            }
            if (x10 != '') {
                for (i = 1; i < 4; i++) {
                    y = x10.charAt(i);
                    if (y == " " || isNaN(y)) {
                        alert("Enter Only Numbers");
                    }
                }
            }
        }



        function ACofficerhelp1() {


            var Branch = document.getElementById('Button2').value;
            var x = '1';
            popup = window.open('TF_ACofficerHelpL1.aspx?&branch=' + Branch + '&bid=' + x, 'helpACOfficerId1', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "helpACOfficerId1";
            return false;

        }
        function ACofficerhelp2() {


            var Branch = document.getElementById('Button2').value;
            var x = '2';
            popup = window.open('TF_ACofficerHelpL1.aspx?&branch=' + Branch + '&bid=' + x, 'helpACOfficerId2', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "helpACOfficerId2";
            return false;

        }
        function ACofficerhelp3() {


            var Branch = document.getElementById('Button2').value;
            var x = '3';
            popup = window.open('TF_ACofficerHelpL1.aspx?&branch=' + Branch + '&bid=' + x, 'helpACOfficerId3', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "helpACOfficerId3";
            return false;

        }
        function ACofficerhelp4() {


            var Branch = document.getElementById('Button2').value;
            var x = '4';
            popup = window.open('TF_ACofficerHelpL1.aspx?&branch=' + Branch + '&bid=' + x, 'helpACOfficerId4', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "helpACOfficerId4";
            return false;

        }
        function ACofficerhelp5() {


            var Branch = document.getElementById('Button2').value;
            var x = '5';
            popup = window.open('TF_ACofficerHelpL1.aspx?&branch=' + Branch + '&bid=' + x, 'helpACOfficerId5', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
            common = "helpACOfficerId5";
            return false;

        }

        function ACofficerId1(event) {
            var key = event.keyCode;
            if (key == 113 && key != 13) {
                document.getElementById('btnBusSourceList1').click();
            }
        }
        function ACofficerId2(event) {
            var key = event.keyCode;
            if (key == 113 && key != 13) {
                document.getElementById('btnBusSourceList2').click();
            }
        }
        function ACofficerId3(event) {
            var key = event.keyCode;
            if (key == 113 && key != 13) {
                document.getElementById('btnBusSourceList3').click();
            }
        }
        function ACofficerId4(event) {
            var key = event.keyCode;
            if (key == 113 && key != 13) {
                document.getElementById('btnBusSourceList4').click();
            }
        }
        function ACofficerId5(event) {
            var key = event.keyCode;
            if (key == 113 && key != 13) {
                document.getElementById('btnBusSourceList5').click();
            }
        }

        function sss() {
            var s = popup.document.getElementById('txtcell1').value;
            if (common == "helpACOfficerId1") {
                document.getElementById('txtBusinessSourceID1').value = s;
            }
            if (common == "helpACOfficerId2") {
                document.getElementById('txtBusinessSourceID2').value = s;
            }
            if (common == "helpACOfficerId3") {
                document.getElementById('txtBusinessSourceID3').value = s;
            }
            if (common == "helpACOfficerId4") {
                document.getElementById('txtBusinessSourceID4').value = s;
            }
            if (common == "helpACOfficerId5") {
                document.getElementById('txtBusinessSourceID5').value = s;
            }
        }
        function validateSave() {
            var BusSourceId = document.getElementById('txtBusinessSourceID');
            if (trimAll(BusSourceId.value) == '') {

                alert('Enter Business Source ID.');
                BusSourceId.focus();
                return false;

            }

            var BusSourceName = document.getElementById('txtName');
            if (trimAll(BusSourceName.value) == '') {

                alert('Enter Business Source Name.');
                BusSourceName.focus();
                return false;
            }


            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:Menu ID="Menu1" runat="server" />
        <br />
        <br />
        <asp:ScriptManager ID="ScriptManagerMain" runat="server">
        </asp:ScriptManager>
        <script language="javascript" type="text/javascript" src="Scripts/InitEndRequest.js"></script>
        <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
            <ProgressTemplate>
                <div id="progressBackgroundMain" class="progressBackground">
                    <div id="processMessage" class="progressimageposition">
                        <img src="Images/ajax-loader.gif" style="border: 0px" alt="" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
            <ContentTemplate>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom">
                            <span class="pageLabel">Relationship Master Details</span>
                        </td>
                        <td align="right" style="width: 50%">
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                TabIndex="17" OnClick="btnBack_Click" />
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
                            <table cellspacing="0" cellpadding="0" border="0" width="400px" style="line-height: 150%">
                                <table >
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Business Source ID (Level1): </span>
                                        </td>
                                        <td>
                                            <%-- <asp:TextBox ID="txtID" runat="server" CssClass="textBox" MaxLength="1" Width="15px"
                                                onkeydown="" TabIndex="-1"  Enabled="False"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtBusinessSourceID1" runat="server" CssClass="textBox" MaxLength="4"
                                                Width="50px" TabIndex="1" OnTextChanged="txtBusinessSourceID1_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            &nbsp;<asp:Button ID="btnBusSourceList1" runat="server" CssClass="btnHelp_enabled"
                                                TabIndex="-1" />&nbsp;<asp:Label ID="lblName1" runat="server" CssClass="elementLabel" Width="400px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Business Source ID (Level2): </span>
                                        </td>
                                        <td>
                                            <%-- <asp:TextBox ID="txtID2" runat="server" CssClass="textBox" MaxLength="1" Width="15px"
                                                onkeydown="" TabIndex="-1"  Enabled="False"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtBusinessSourceID2" runat="server" CssClass="textBox" MaxLength="4"
                                                Width="50px" TabIndex="2" OnTextChanged="txtBusinessSourceID2_TextChanged" AutoPostBack="true"></asp:TextBox>&nbsp;
                                            <asp:Button ID="btnBusSourceList2" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />&nbsp;<asp:Label ID="lblName2" runat="server" CssClass="elementLabel" Width="400px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Business Source ID (Level3): </span>
                                        </td>
                                        <td>
                                            <%--<asp:TextBox ID="txtID3" runat="server" CssClass="textBox" MaxLength="1" Width="15px"
                                                onkeydown="" TabIndex="-1"  Enabled="False"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtBusinessSourceID3" runat="server" CssClass="textBox" MaxLength="4"
                                                Width="50px" TabIndex="3" OnTextChanged="txtBusinessSourceID3_TextChanged" AutoPostBack="true"></asp:TextBox>&nbsp;
                                            <asp:Button ID="btnBusSourceList3" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />&nbsp;<asp:Label ID="lblName3" runat="server" CssClass="elementLabel" Width="400px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Business Source ID (Level4): </span>
                                        </td>
                                        <td>
                                            <%--  <asp:TextBox ID="txtID4" runat="server" CssClass="textBox" MaxLength="1" Width="15px"
                                                onkeydown="" TabIndex="-1" Enabled="False"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtBusinessSourceID4" runat="server" CssClass="textBox" MaxLength="4"
                                                Width="50px" TabIndex="4" OnTextChanged="txtBusinessSourceID4_TextChanged" AutoPostBack="true"></asp:TextBox>&nbsp;
                                            <asp:Button ID="btnBusSourceList4" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />&nbsp;<asp:Label ID="lblName4" runat="server" CssClass="elementLabel" Width="400px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="elementLabel">Business Source ID (Level5): </span>
                                        </td>
                                        <td>
                                            <%-- <asp:TextBox ID="txtID5" runat="server" CssClass="textBox" MaxLength="1" Width="15px"
                                                onkeydown="" TabIndex="-1"  Enabled="False"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtBusinessSourceID5" runat="server" CssClass="textBox" MaxLength="4"
                                                Width="50px" TabIndex="5" OnTextChanged="txtBusinessSourceID5_TextChanged" AutoPostBack="true"></asp:TextBox>&nbsp;
                                            <asp:Button ID="btnBusSourceList5" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />&nbsp;<asp:Label ID="lblName5" runat="server" CssClass="elementLabel" Width="400px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table >
                                    <tr>
                                        <td width="60px">
                                        </td>
                                        <td align="left" style="width: 220px">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault" ToolTip="Save"
                                                OnClick="btnSave_Click" TabIndex="6" />&nbsp
                                            <asp:Button ID="BtnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                OnClick="BtnCancel_Click" />
                                            <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                            <asp:Button ID="Button1" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                                            <asp:Button ID="Button2" runat="server" ClientIDMode="Static" Style="visibility: hidden"
                                                Text="Button" UseSubmitBehavior="False" />
                                        </td>
                                    </tr>
                                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
