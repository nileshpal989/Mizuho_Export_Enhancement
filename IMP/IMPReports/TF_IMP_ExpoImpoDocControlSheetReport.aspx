<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_IMP_ExpoImpoDocControlSheetReport.aspx.cs" Inherits="IMP_IMPReports_ExpoImpoDocControlSheetReport" %>

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
    	function generateReport() {

    		var Branchcode = document.getElementById('ddlBranch');
    		var fromDate = document.getElementById('txtFromDate');
    		if (fromDate.value == '') {
    			alert('Select From Date.', '#fromDate');
    			// fromDate.focus();
    			return false;
    		}
    		var toDate = document.getElementById('txtToDate');
    		if (toDate.value == '') {
    			alert('Select To Date.', '#toDate');
    			// toDate.focus();
    			return false;
    		}
    		var winame = window.open('TF_IMP_ViewExpoImpoDocControlSheetReport.aspx?Branchcode=' + Branchcode.value + '&fromDate=' + fromDate.value + '&toDate=' + toDate.value, '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1200,height=600');
    		winame.focus();
    		return false;
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
                            <td align="left" colspan="2" style="width: 50%" valign="bottom">
                                <span class="pageLabel"><strong>Export Import Document Control Sheet Report</strong></span>
                                <%--<asp:Label ID="PageHeader" CssClass="pageLabel" runat="server"></asp:Label>--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left" style="width: 50%" valign="top">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2" style="width: 100%" valign="top">
                                <asp:Label ID="lblmessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                              <tr>
                                        <td align="right" width="10%">
											<span class="elementLabel">From Date :</span>
										</td>
										<td align="left">
											<asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10" 
												TabIndex="2" ValidationGroup="dtVal" Width="70px"></asp:TextBox>
											<ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
												CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" 
												CultureDateFormat="DMY" CultureDatePlaceholder="/" 
												CultureDecimalPlaceholder="." CultureName="en-GB" 
												CultureThousandsPlaceholder="," CultureTimePlaceholder=":" Enabled="True" 
												ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" 
												TargetControlID="txtFromDate">
											</ajaxToolkit:MaskedEditExtender>
											<asp:Button ID="Button1" runat="server" CssClass="btncalendar_enabled" 
												TabIndex="-1" />
											<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
												Enabled="True" Format="dd/MM/yyyy" PopupButtonID="Button1" 
												TargetControlID="txtFromDate">
											</ajaxToolkit:CalendarExtender>
											&nbsp;&nbsp; <span class="elementLabel">To Date :</span>
											<asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10" 
												TabIndex="3" ValidationGroup="dtVal" Width="70px"></asp:TextBox>
											<ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" 
												CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" 
												CultureDateFormat="DMY" CultureDatePlaceholder="/" 
												CultureDecimalPlaceholder="." CultureName="en-GB" 
												CultureThousandsPlaceholder="," CultureTimePlaceholder=":" Enabled="True" 
												ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" 
												TargetControlID="txtToDate">
											</ajaxToolkit:MaskedEditExtender>
											<asp:Button ID="Button2" runat="server" CssClass="btncalendar_enabled" 
												TabIndex="-1" />
											<ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" 
												Enabled="True" Format="dd/MM/yyyy" PopupButtonID="Button2" 
												TargetControlID="txtToDate">
											</ajaxToolkit:CalendarExtender>
										</td>
                                    </tr>
                               </td>
                                <tr>
							 <td align="right" width="10%">
										<span class="mandatoryField">*</span><span class="elementLabel">Branch :</span>
                               </td>
                             <td align="left">
                                        <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true" 
											CssClass="dropdownList" TabIndex="1" Width="100px">
										</asp:DropDownList>
									</td>
						</tr>
                       <tr>
                       <td colspan="2" align ="left">
								&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								<asp:Button ID="Generate" runat="server" CssClass="buttonDefault" TabIndex="4" 
									Text="Generate" ToolTip="Genarate" />
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
