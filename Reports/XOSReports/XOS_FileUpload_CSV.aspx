<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XOS_FileUpload_CSV.aspx.cs" Inherits="XOS_XOS_FileUpload_CSV" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-Tradefinance System</title>
     <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/style_new.css"  rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function validateSave() {

            var filelen = document.getElementById('fileinhouse').value.length;
            var strext = document.getElementById('fileinhouse').value.substring(filelen - 4, filelen);           
            if (document.getElementById('fileinhouse').value == "") {
                alert('Select csv file to import.');                
                document.getElementById('fileinhouse').focus();
                    return false;
                }
                
            else 
            {
                strext = strext.toLowerCase();
                if (strext != '.csv') {
                    alert('Invalid file format.');
                    return false;
                }                
            }            
//            var winname = window.open('ViewXOScsvoutputdatavalidation.aspx', '', 'scrollbars=yes,status=0,menubar=0,left=0,top=50,width=950,height=550');
//            winname.focus();
//            return false;

            return true;
        }
        


//        function checkSysDate(controlID) {

//            var obj = controlID;

//            if (controlID.value != "__/__/____") {

//                var day = obj.value.split("/")[0];

//                var month = obj.value.split("/")[1];
//                var year = obj.value.split("/")[2];


//                var dt = new Date(year, month - 1, day);
//                var today = new Date();

//                if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year) || (dt > today)) {

//                    alert("Invalid Date");
//                    controlID.focus();
//                    return false;
//                }
//            }
//        }
	
   
		
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
                              <span class="pageLabel">XOS CSV Output Validation</span>
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
                                    <%--<tr>
                                        <td align="right">
                                            <span class="elementLabel">Branch :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" runat="server" TabIndex="1">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
--%>                                    <tr valign="top">
                                        <td align="right" style="width: 140px">
                                            <span class="elementLabel">Select File :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:FileUpload ID="fileinhouse" runat="server" contenteditable="false" Height="17px"
                                                Width="350" TabIndex="2" />
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
                                            <table cellpadding="2">
                                                <tr>
                                                <td>
                                                    <asp:Button ID="btnupload" runat="server" Text="Generate" OnClick="btnupload_Click" CssClass="buttonDefault"
                                                            TabIndex="2" />                                                    
                                                </td>
                                                    <td width="500px">                                                        
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                                            TabIndex="3" onclick="btnCancel_Click" />                                                        
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
