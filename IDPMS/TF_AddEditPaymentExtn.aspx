<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TF_AddEditPaymentExtn.aspx.cs"
	Inherits="IDPMS_TF_AddEditPaymentExtn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Duration="1" Location="None" VaryByParam="none" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
	Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<meta http-equiv="X-UA-Compatible" content="IE=edge">
	<title>LMCC TRADE FINANCE SYSTEM</title>
	<link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
	<link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
	<link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
	<link href="../Style/Style_V2.css" rel="stylesheet" type="text/css" />
	<style type="text/css">
		td > table
		{
			border: 0px solid #ddd;
		}
	</style>
	<script type="text/javascript" language="javascript">
		function calc(a, b, c) {

			//            var letterno = document.getElementById('a').value;
			//            var letterdate = document.getElementById('b').value;
			//            var extndate = document.getElementById('c').value;

			var letterno = document.getElementById(a).value;
			var lettrdate = document.getElementById(b).value;
			var extndate = document.getElementById(c).value;

			if (letterno == '') {

				alert('Letter No cant be blank.');
				document.getElementById(a).focus();
				//document.getElementById(a.value).focus();
				return false;

			}

			//            if (lettrdate == '__/__/____' || lettrdate == '') {

			//                alert('Letter Date cant be blank.');
			//                document.getElementById(b).focus();
			//                return false;

			//            }

			//            if (extndate == '__/__/____' || extndate == '') {

			//                alert('Extension Date cant be blank.');
			//                document.getElementById(c).focus();
			//                return false;

			//            }


		}


		function Ldate(a, b, c) {

			var lettrdate = document.getElementById(b).value;

			if (lettrdate == '__/__/____' || lettrdate == '') {

				alert('Letter Date cant be blank.');
				document.getElementById(b).focus();
				return false;

			}

		}

		function Edate(a) {

			var extndate = document.getElementById(a).value;

			if (extndate != '__/__/____' || extndate != '') {

				// alert('Extension Date cant be blank.');

				extndate = "__/__/____";
				document.getElementById(a).focus();
				//return false;

			}

		}


		function opencust(e) {

			var keycode;
			if (window.event) keycode = window.event.keyCode;
			if (keycode == 113 || e == 'mouseClick') {


				var branch = document.getElementById('ddlBranch').value;
				//                var year = document.getElementById('txt_year').value;

				open_popup('../TF_CustomerLookUpPE.aspx?branch=' + branch, 450, 550, 'DocNoList');
				return false;

			}


		}

		function selectCustomer(acno, name) {

			document.getElementById('txt_custacno').value = acno;
			//document.getElementById('lbl_custname').innerHTML = name;
			// document.getElementById('txt_custacno').focus();
			__doPostBack("txt_custacno", "TextChanged");
		}
			
	</script>
</head>
<body onload="EndRequest();closeWindows();" onunload="closeWindows();">
	<form id="form1" runat="server">
	<asp:ScriptManager ID="ScriptManagerMain" runat="server">
	</asp:ScriptManager>
	<script language="javascript" type="text/javascript" src="../Scripts/Enable_Disable_Opener.js"></script>
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
		<center>
			<uc1:Menu ID="Menu1" runat="server" />
			<br />
			<asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
				<ContentTemplate>
					<table border="0" width="100%">
						<tr>
							<td style="text-align: left; white-space: nowrap; vertical-align: bottom">
								<span class="pageLabel"><strong>Payment Extension Data Entry</strong></span>
							</td>
							<td style="text-align: right; white-space: nowrap; vertical-align: bottom">
								<asp:Label runat="server" ID="lblSupervisormsg" Text="Role : Supervisor - ONLY VIEW THE DATA  "
									Style="color: red"></asp:Label>
							</td>
						</tr>
					</table>
					<table border="0" width="100%">
						<tr>
							<td align="left" style="width: 100%" valign="top" colspan="3">
								<hr />
							</td>
						</tr>
						<tr>
							<td style="text-align: right; white-space: nowrap;">
								<span class="elementLabel">Branch:</span>
							</td>
							<td style="text-align: left; white-space: nowrap; vertical-align: bottom;" colspan="2">
								<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" AutoPostBack="true" runat="server"
									TabIndex="1">
								</asp:DropDownList>
							</td>
						</tr>
						<tr>
							<td style="text-align: right; white-space: nowrap; width: 2%">
								<span class="elementLabel">Customer AC No:</span>
							</td>
							<td style="text-align: left; white-space: nowrap; width: 68%">
								<asp:TextBox runat="server" CssClass="textBox" Width="100px" TabIndex="1" ID="txt_custacno"
									onkeydown="return opencust(113);" MaxLength="20" OnTextChanged="txt_custacno_TextChanged"
									AutoPostBack="true" />
								<asp:Button ID="btncusthelp" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
									OnClientClick="return opencust('mouseClick');" />
								<asp:Label Text="" runat="server" CssClass="elementLabel" ID="lbl_custname" />
							</td>
							<td style="text-align: right; white-space: nowrap; width: 30%">
								<span class="elementLabel">Search :</span>
								<asp:TextBox ID="txtSearch" runat="server" CssClass="textBox" MaxLength="40" Width="180px"
									TabIndex="5"></asp:TextBox>
								<asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="buttonDefault" ToolTip="Search"
									OnClick="btnSearch_Click" />
							</td>
						</tr>
						<tr>
							<td colspan="3">
								<asp:Label Text="" ID="lblmessage" runat="server" CssClass="mandatoryField" />
							</td>
						</tr>
						<tr>
							<td colspan="3">
								<asp:GridView ID="GridViewpaymentextn" runat="server" AutoGenerateColumns="false"
									Width="80%" AllowPaging="true" PageSize="20" OnRowDataBound="GridViewpaymentextn_RowDataBound"
									OnRowCommand="GridViewpaymentextn_RowCommand" CssClass="GridView">
									<PagerSettings Visible="false" />
									<Columns>
										<asp:TemplateField HeaderText="Port Code">
											<ItemTemplate>
												<asp:Label ID="lblprtcd" runat="server" Text='<%# Eval("PortCode") %>' CssClass="elementLabel"></asp:Label>
											</ItemTemplate>
											<HeaderStyle HorizontalAlign="Center" Width="10%" />
											<ItemStyle HorizontalAlign="Center" Width="10%" />
										</asp:TemplateField>
										<asp:TemplateField HeaderText="BOE No">
											<ItemTemplate>
												<asp:Label ID="lblbillno" runat="server" Text='<%# Eval("Bill_Entry_No") %>' CssClass="elementLabel"></asp:Label>
											</ItemTemplate>
											<HeaderStyle HorizontalAlign="Center" Width="10%" />
											<ItemStyle HorizontalAlign="Center" Width="10%" />
										</asp:TemplateField>
										<asp:TemplateField HeaderText="BOE Date">
											<ItemTemplate>
												<asp:Label ID="lblbilldate" runat="server" Text='<%# Eval("Bill_Entry_Date","{0:dd/MM/yyyy}") %>'
													CssClass="elementLabel"></asp:Label>
											</ItemTemplate>
											<HeaderStyle HorizontalAlign="Center" Width="10%" />
											<ItemStyle HorizontalAlign="Center" Width="10%" />
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Extension Date">
											<ItemTemplate>
												<asp:TextBox ID="txt_extndate" runat="server" CssClass="textBox" Text='<%# Eval("ExtensionDate","{0:dd/MM/yyyy}") %>'
													Width="90px" MaxLength="10" onfocus="this.select()"></asp:TextBox>
												<ajaxToolkit:MaskedEditExtender ID="mdRemdate1" Mask="99/99/9999" MaskType="Date"
													runat="server" TargetControlID="txt_extndate" ErrorTooltipEnabled="True" CultureName="en-GB"
													CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
													CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
													CultureTimePlaceholder=":" Enabled="True">
												</ajaxToolkit:MaskedEditExtender>
												<asp:Button ID="btncalendar_DocDate1" runat="server" CssClass="btncalendar_enabled"
													TabIndex="-1" />
												<ajaxToolkit:CalendarExtender ID="calendarFromDate2" runat="server" Format="dd/MM/yyyy"
													TargetControlID="txt_extndate" PopupButtonID="btncalendar_DocDate1" Enabled="True">
												</ajaxToolkit:CalendarExtender>
											</ItemTemplate>
											<HeaderStyle HorizontalAlign="Center" Width="15%" />
											<ItemStyle HorizontalAlign="Center" Width="15%" />
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Extension By">
											<ItemTemplate>
												<asp:DropDownList runat="server" ID="ddl_extby" CssClass="dropdownList">
													<asp:ListItem Text="AD Bank" Value="2" />
													<asp:ListItem Text="RBI" Value="1" />
													<asp:ListItem Text="Others" Value="0" />
												</asp:DropDownList>
											</ItemTemplate>
											<HeaderStyle HorizontalAlign="Center" Width="10%" />
											<ItemStyle HorizontalAlign="Center" Width="10%" />
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Letter No.">
											<ItemTemplate>
												<asp:TextBox runat="server" CssClass="textBox" Text='<%# Eval("LetterNo") %>' ID="txt_letterno"
													Width="135px" MaxLength="20" />
											</ItemTemplate>
											<HeaderStyle HorizontalAlign="Center" Width="15%" />
											<ItemStyle HorizontalAlign="Center" Width="15%" />
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Letter Date.">
											<ItemTemplate>
												<asp:TextBox ID="txt_letterdate" runat="server" CssClass="textBox" Text='<%# Eval("LetterDate","{0:dd/MM/yyyy}") %>'
													Width="90px" MaxLength="10" onfocus="this.select()"></asp:TextBox>
												<ajaxToolkit:MaskedEditExtender ID="mdRemdate" Mask="99/99/9999" MaskType="Date"
													runat="server" TargetControlID="txt_letterdate" ErrorTooltipEnabled="True" CultureName="en-GB"
													CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
													CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
													CultureTimePlaceholder=":" Enabled="True">
												</ajaxToolkit:MaskedEditExtender>
												<asp:Button ID="btncalendar_DocDate" runat="server" CssClass="btncalendar_enabled"
													TabIndex="-1" />
												<ajaxToolkit:CalendarExtender ID="calendarFromDate1" runat="server" Format="dd/MM/yyyy"
													TargetControlID="txt_letterdate" PopupButtonID="btncalendar_DocDate" Enabled="True">
												</ajaxToolkit:CalendarExtender>
											</ItemTemplate>
											<HeaderStyle HorizontalAlign="Center" Width="15%" />
											<ItemStyle HorizontalAlign="Center" Width="15%" />
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Add">
											<ItemTemplate>
												<asp:Button ID="btnadd" runat="server" Text="Add" CssClass="buttonDefault" CommandArgument='<%# Eval("PortCode")+","+Eval("Bill_Entry_No")+","+Eval("Bill_Entry_Date","{0:dd/MM/yyyy}")+","+Eval("LetterNo")+","+Eval("LetterDate")+","+Eval("ExtensionDate","{0:dd/MM/yyyy}")%>'
													ToolTip="Add" Style="width: 100%" />
											</ItemTemplate>
											<HeaderStyle HorizontalAlign="Center" Width="10%" />
											<ItemStyle HorizontalAlign="Center" Width="10%" />
										</asp:TemplateField>
									</Columns>
								</asp:GridView>
							</td>
						</tr>
					</table>
					<table border="0" width="100%">
						<tr id="rowPager" runat="server" width="100%" visible="false" class="gridHeader">
							<td align="left">
								&nbsp;
								<asp:Label ID="Label1" Text="Records Per Page :" runat="server" />
								<asp:DropDownList ID="ddlrecordperpage" runat="server" CssClass="dropdownList" AutoPostBack="true"
									OnSelectedIndexChanged="ddlrecordperpage_SelectedIndexChanged">
									<asp:ListItem Value="10" Text="10"></asp:ListItem>
									<asp:ListItem Value="20" Text="20"></asp:ListItem>
									<asp:ListItem Value="30" Text="30"></asp:ListItem>
									<asp:ListItem Value="40" Text="40"></asp:ListItem>
									<asp:ListItem Value="50" Text="50"></asp:ListItem>
								</asp:DropDownList>
							</td>
							<td align="center" width="50%">
								<asp:Button ID="btnnavfirst" runat="server" Text="First" ToolTip="First" OnClick="btnnavfirst_Click"
									CssClass="elementLabel" />
								<asp:Button ID="btnnavpre" runat="server" Text="Prev" ToolTip="Previous" OnClick="btnnavpre_Click"
									CssClass="elementLabel" />
								<asp:Button ID="btnnavnext" runat="server" Text="Next" ToolTip="Next" OnClick="btnnavnext_Click"
									CssClass="elementLabel" />
								<asp:Button ID="btnnavlast" runat="server" Text="Last" ToolTip="Last" OnClick="btnnavlast_Click"
									CssClass="elementLabel" />
							</td>
							<td align="right" width="25%">
								<asp:Label ID="lblpageno" runat="server" Visible="true"></asp:Label>
								&nbsp;
								<asp:Label ID="lblrecordno" runat="server"></asp:Label>
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
