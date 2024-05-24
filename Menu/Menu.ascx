<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu.ascx.cs" Inherits="Menu_Menu" %>
<!DOCTYPE HTML>
<html>
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=11" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <title>LMCC Trade Finance System</title>
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <script src="../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script type="text/javascript">
		var _timeLeft, _countDownTimer, _popupTimer;
		function startSession() {

			_timeLeft = '<%= Session.Timeout %>';
			//          alert(_timeLeft);
			updateCountDown();
		}

		function updateCountDown() {
			if (_timeLeft > 2) {
				_timeLeft--;
				_countDownTimer = window.setTimeout(updateCountDown, 60000);

			} else {
				showPopup();
			}
		}
		function stopTimers() {
			window.clearTimeout(_popupTimer);
			window.clearTimeout(_countDownTimer);
			document.getElementById("Menu1_CountDownHolder").innerText = "";
		}
		function updateCountDownTimer() {

			var min = Math.floor(_timeLeft / 60);

			var sec = _timeLeft % 60;
			if (sec < 10)
				sec = "0" + sec;

			document.getElementById("Menu1_CountDownHolder").innerText = "This session will expire in " + min + ":" + sec + " mins";

			if (_timeLeft > 0) {
				_timeLeft--;
				_countDownTimer = window.setTimeout(updateCountDownTimer, 1000);
				this.focus();
			} else {
				var loginID = document.getElementById("Menu1_hdnloginid").value;
				// document.location = '../TF_Login.aspx?sessionout=yes&sessionid=' + loginID;
				document.location = <%= QuotedTimeOutUrl %>;

			}
		}
		function showPopup() {

			_timeLeft = 120;

			updateCountDownTimer();

		}
		function sendKeepAlive() {
			stopTimers();
		}

		function checkenter(evnt) {
			var charCode = (evnt.which) ? evnt.which : event.keyCode;
			//alert(charCode);
			if (charCode == 13)
			{
				return false;
			}
			else
				return true;
		}

    </script>
    <script type="text/javascript" language="JavaScript">
		function CallHouseKeeping() {
		 <%= Page.ClientScript.GetPostBackEventReference(this.btnHousekeeping, "") %>
		}
		function checkenter(evnt) {
			var charCode = (evnt.which) ? evnt.which : event.keyCode;
			//alert(charCode);
			if (charCode == 13)
			{
				return false;
			}
			else
				return true;
		}
    </script>
</head>
<body onload="startSession();" onclick="stopTimers();startSession();" onkeyup="stopTimers();startSession();"
    onkeypress="return checkenter(event);">
    <div id="body">
        <div class="container3-header">
            <div class="logo">
            </div>
            <div class="wrapper">
                <div class="header-info">
                    <asp:Label ID="lblUserName" runat="server" CssClass="elementLabel" />
                    <asp:Label ID="lblRole" runat="server" CssClass="elementLabel" />
                    <asp:Label ID="lblTime" runat="server" CssClass="elementLabel" />
                    <input type="hidden" id="hdnloginid" runat="server" />
                    <input type="hidden" id="hdnModuleID" runat="server" />
                </div>
                <div class="module-info">
                    <asp:Label ID="lblModuleName" runat="server" />
                </div>
            </div>
        </div>
        <asp:Label ID="CountDownHolder" runat="server" Font-Bold="true" Font-Size="Large"
            ForeColor="#ff0000" ToolTip="Click to Renew your Session" CssClass="mandatoryField" />
        <nav>
			<ul class="menu">
				<li><a href="~/TF_ModuleSelection.aspx?type=0" id="hHome" runat="server">Home</a>
					<ul>
						<li><a href="~/TF_ModuleSelection.aspx?type=1" runat="server">EXPORT</a> </li>
						<li><a href="~/TF_ModuleSelection.aspx?type=2" runat="server">EBRC</a> </li>
						<li><a href="~/TF_ModuleSelection.aspx?type=3" runat="server">XOS</a> </li>
						<li><a href="~/TF_ModuleSelection.aspx?type=4" runat="server">EDPMS</a> </li>
						<li><a href="~/TF_ModuleSelection.aspx?type=5" runat="server">IDPMS</a> </li>
						<li><a href="~/TF_ModuleSelection.aspx?type=6" runat="server">R-RETURN</a> </li>
						<li><a href="~/TF_ModuleSelection.aspx?type=7" runat="server">IMPORT WAREHOUSING</a></li>
						<li><a href="~/TF_ModuleSelection.aspx?type=8" runat="server">IMPORT</a> </li>
					</ul>
				</li>
				<li><a href="#">Masters</a>
					<ul>
						<%--All Masters Order By Alphabets--%>
						<li>
							<a href="../TF_ViewBusinessSourceMaster.aspx" id="mnuAccountOfficerMaster" runat="server" disabled="disabled" visible="false">Account Officer Master</a>
						</li>
						<li>
							<a href="~/EXP/EXP_ViewAutorisedSignatory.aspx" id="mnuAuthorisedSignatory" disabled="disabled" runat="server" visible="false">Authorised Signatory</a>
						</li>
						<li>
							<a href="~/RRETURN/Ret_ViewAuthSignatory.aspx" id="mnuRet_AuthorisedSignatory" runat="server" disabled="disabled" visible="false">Authorised Signatory</a>
						</li>
						<li>
							<a href="~/EXP/BankChargesMaster.aspx" id="mnuBankChargesMaster" runat="server" disabled="disabled" visible="false">Bank Charges Master</a>
						</li>
						<li>
							<a href="~/INW_ViewOverseasBank.aspx" id="mnuOverseasBankMaster" runat="server" disabled="disabled" visible="false">Bank Master</a>
						</li>
						<li>
							<a href="~/TF_ViewBeneficaryMaster.aspx" id="mnuBeneficiaryMaster" disabled="disabled" runat="server" visible="false">Beneficiary Master</a>
						</li>
						<li>
							<a href="~/IMP/TF_IMP_Opn_Cl_BalanceMaster_View.aspx" id="mnuClosingBalMaster" runat="server" disabled="disabled" visible="false">Closing Balance Master</a>
						</li>
						<li>
							<a href="~/IMP/TF_View_ImpAuto_CommissionMaster.aspx" id="mnuCommisionMaster" runat="server" disabled="disabled" visible="false">Commission Master For Imports</a>
						</li>
						<li>
							<a href="~/TF_View_Commission_Master.aspx" id="mnu_CommissionMaster" runat="server" disabled="disabled" visible="false">Commission Master</a>
						</li>
						<li>
							<a href="~/TF_ViewCommodityMaster.aspx" id="mnuCommodityMaster" runat="server" disabled="disabled" visible="false">Commodity Master</a>
						</li>
						<li>
							<a href="~/TF_ViewCountryMaster.aspx" id="mnuCountryMaster" disabled="disabled" runat="server" visible="false">Country Master</a>
						</li>
						<li>
							<a href="~/TF_View_Courier_Charges_Master.aspx" id="mnu_Courier_Charges" runat="server" disabled="disabled" visible="false">Courier Charges Masters</a>
						</li>
						<li>
							<a href="~/PC/PC_AddEditCurrencyRate.aspx" id="mnuCurrencyRate" disabled="disabled" runat="server" visible="false">Cross Currency Rate Master</a>
						</li>
						<li>
							<a href="../TF_ViewCurrencyMaster.aspx" id="mnuCurrencyMaster" runat="server" disabled="disabled" visible="false">Currency Master</a>
						</li>
						<li>
							<a href="~/EXP/EXP_ViewCurrencyCardRate.aspx" id="mnuCurrencyCardRate" disabled="disabled" runat="server" visible="false">Currency Card Rate Master</a>
						</li>
						<li>
							<a href="../TF_ViewCurrencyMaster.aspx" id="A7" runat="server" disabled="disabled" visible="false">Currency Master</a>
						</li>
						<li>
							<a href="../TF_ViewCustomerMaster.aspx" id="mnuCustomerMaster" runat="server" disabled="disabled" visible="false">Customer Master</a>
						</li>
						<li>
							<a href="~/ImportWareHousing/Masters/Impw_CustomerMandatoryFieldMaster.aspx" id="mnu_CustMandad" runat="server" disabled="disabled" visible="false">Customer Mandatory Master</a>
						</li>
						<li>
							<a href="~/IMP/TF_ImpAuto_View_DiscrepencyMaster.aspx" id="mnu_Discrepency" runat="server" disabled="disabled" visible="false">Discrepency Charges Master</a>
						</li>
						<li>
							<a href="~/IMP/TF_ImpAuto_ViewDrawermaster.aspx" id="mnu_Imp_DrawerMaster" runat="server" disabled="disabled" visible="false">Drawer Master</a>
						</li>
						<li>
							<a href="~/EDPMS_View_Error_Code_Master.aspx" id="mnu_ErrorCode" runat="server" disabled="disabled" visible="false">Error Code Master</a>
						</li>
						<li>
							<a href="~/Masters/TF_GBaseCommodityMaster.aspx" id="mnuGbaseCommMaster" runat="server" disabled="disabled" visible="false">GBase Commodity Master</a>
						</li>
						<li>
							<a href="~/IMP/TF_IMP_GLCode_Master_View.aspx" id="mnuGLCodeMaster" runat="server" disabled="disabled" visible="false">GL Code Master</a>
						</li>
						<li>
							<a href="../TF_ViewServiceTaxMaster.aspx" id="mnuServiceTaxMaster" runat="server" disabled="disabled" visible="false">GST Rate Master</a>
						</li>
						<li>
							<a href="~/IMP/HoildayMaster.aspx" id="mnuHolidayMaster" runat="server" disabled="disabled" visible="false">Holiday Master For Imports</a>
						</li>
						<li>
							<a href="~/TF_SpecialDates.aspx" id="mnuSpecialDates" runat="server" disabled="disabled" visible="false">Holiday Dates Master</a>
						</li>
						<li>
							<a href="~/EXP/AddEditInterestRateMaster.aspx" id="mnuInterestRateMaster" disabled="disabled" runat="server" visible="false">Interest Rate Master</a>
						</li>
						<%--<li>
							<a href="~/IMP/TF_ImpAuto_ViewLocalMaster.aspx"  id="mnuLocalBankMaster" runat="server" disabled="disabled" visible="false">Local Bank master</a>
						</li>--%>
						<li>
							<a href="~/IMP/TF_IMP_NostroMaster_View.aspx" id="mnu_IMP_NOSTRO_Master_View" runat="server" disabled="disabled" visible="false">Nostro Bank Master</a>
						</li>
						<%--<li>
							<a href="~/IMP/TF_ImpAuto_View_OtherBankCharges.aspx"  id="mnu_OtherBank" runat="server" disabled="disabled" visible="false">Other Bank Charges For Imports</a>
						</li>--%>

						<li>
							<a href="~/TF_ViewOverseasPartyMaster.aspx" id="mnuOveseasPartyMaster" disabled="disabled" runat="server" visible="false">Overseas Party Master</a>
						</li>
						<li>
							<a href="../TF_ViewPortMaster.aspx" id="mnuPortCodeMaster" runat="server" disabled="disabled" visible="false">Port Code Master</a>
						</li>
						<li>
							<a href="~/TF_View_ProfitinLeo_Master.aspx" id="mnu_profliue" runat="server" disabled="disabled" visible="false">Profit in Lieu Master</a>
						</li>
						<li>
							<a href="../TF_ViewPurposeCode.aspx" id="A8" runat="server" disabled="disabled" visible="false">Purpose Code Master</a>
						</li>
						<li>
							<a href="../TF_ViewPurposeCode.aspx" id="mnuPurposeCodeMaster" runat="server" disabled="disabled" visible="false">Purpose Code Master</a>
						</li>
						<li>
							<a href="~/TF_View_ReasonForAdjustmentMaster.aspx" id="mnu_adjirm" runat="server" disabled="disabled" visible="false">Reason for adjustment master</a>
						</li>
						<li>
							<a href="~/TF_ViewReceivingBankMaster.aspx" id="mnu_RecvBankMaster" runat="server" disabled="disabled" visible="false">Receiving Bank Master</a>
						</li>
						<li>
							<a href="~/Masters/EXP_View_ReimbursingBank.aspx" id="mnuReimbursingBankMaster" runat="server" disabled="disabled" visible="false">Reimbursing Bank Master</a>
						</li>
						<li>
							<a href="../TF_ViewRelationshipMaster.aspx" id="mnuRelationshipMaster" runat="server" disabled="disabled" visible="false">Relationship Master</a>
						</li>
						<li>
							<a href="~/IMP/TF_IMP_RMA_Master.aspx" id="mnu_RMAMaster" runat="server" disabled="disabled" visible="false">RMA Master</a>
						</li>
						<li>
							<a href="~/EXP/EXP_ViewSanctionedCountry.aspx" id="mnuEXP_SanctionedCountry" disabled="disabled" runat="server" visible="false">Sanctioned Country Master</a>
						</li>
						<li>
							<a href="~/IMP/TF_IMP_SundryAccountMaster.aspx" id="mnuSundryAccMaster" disabled="disabled" runat="server" visible="false">Sundry Account Master</a>
						</li>
						<li>
							<a href="../TF_ViewSectorMaster.aspx" id="mnuSectorMaster" runat="server" disabled="disabled" visible="false">Sector Master</a>
						</li>
						<li>
							<a href="../TF_AddEditSTaxFXDls.aspx" id="mnuServiceTaxFXDLS" runat="server" disabled="disabled" visible="false">Service Tax Master on FXDLS</a>
						</li>
						<li>
							<a href="~/IMP/TF_ViewStampCharges.aspx" id="mnuStampDutyMaster" runat="server" disabled="disabled" visible="false">Stamp Duty Master</a>
						</li>
						<li>
							<a href="~/ImportWareHousing/Masters/Impw_ViewSupplierMaster.aspx" id="mnuSupplierrMaster" runat="server" disabled="disabled" visible="false">Supplier Master</a>
						</li>
						<li>
							<a href="~/ImportWareHousing/Masters/Impw_ViewSupplierMaster_CP.aspx" id="mnuSupplierrMaster_CP" runat="server" disabled="disabled" visible="false">Supplier Master Paging</a>
						</li>
						<li>
							<a href="~/RRETURN/TF_ViewVastroBankMaster.aspx" id="mnuVastroBankMaster" runat="server" disabled="disabled" visible="false">Vostro Bank Master</a>
						</li>
                        <li>
                            <a href="~/EXP/VIEW_LEI_Threshold_Master.aspx" id="mnuLEIThresholdMaster" runat="server" disabled="disabled" visible="false">LEI Threshold Master</a>
                        </li>
                        <li><%--ADDED BY NILESH----04/05/2023--%>
							<a href="~/Masters/EXP_ConsigneePartyMasterView.aspx" id="mnuConsigneePartyMaster" disabled="disabled" runat="server" visible="false">Consignee Party Master</a>
						</li>
                        <li><%--ADDED BY NILESH----05/05/2023--%>
							<a href="~/EXP/TF_EXP_SundryAccountMaster.aspx" id="mnuexpsundrymaster" disabled="disabled" runat="server" visible="false">Sundry Account Master</a>
						</li>
					</ul>
				</li>
				<li><a href="#">Transactions</a>
					<ul>
					<%--Export module Start Transaction--%> 
					 <li>
						<a href="~/EXP/EXP_ViewExportBillEntry.aspx" id="mnu_EXPbillOfEntry" runat="server"
							disabled="disabled" visible="false">Export Bill Data Entry-Maker</a>
					 </li>
                     <%--  -------------------------------------------added by Bhupen 15-03-2023----------------------------------%>
						<li>
						<a href="~/EXP/EXP_ViewExportBillEntry_Checker.aspx" id="mnu_EXPbillOfEntryChecker" runat="server"
							disabled="disabled" visible="false">Export Bill Data Entry-Checker</a>
					 </li>
                    <%-- ------------------------------------------------end----------------------------------------------------------------%>
                    <%--------------------------------------------------------added by Anand 31-07-2023--------------------------%>
                    <li>
						<a href="~/EXP/EXP_ViewApprovedLodgemetForROD_Maker.aspx" id="mnu_EXPApprovedLodgemetForRODMaker" runat="server"
							disabled="disabled" visible="false">Approved Lodgement For ROD-Maker</a>
					 </li>
                     <li>
						<a href="~/EXP/EXP_ViewApprovedLodgemetForROD_Checker.aspx" id="mnu_EXPApprovedLodgemetForRODChecker" runat="server"
							disabled="disabled" visible="false">Approved Lodgement For ROD-Checker</a>
					 </li>
                    <%----------------------------------------------------------------------------End-------------------------------------%>
					  <li>
						<a href="~/EXP/EXP_ViewRealisationEntry.aspx" id="mnu_RealisationDataEntry" runat="server"
							disabled="disabled" visible="false">Export Realisation Data Entry-old</a> </li>
                             <%----------------------------------------added by Nilesh--------------------------------%>
                      <li>
                      <li>
						<a href="~/EXP/EXP_ViewRealisationEntry_Maker.aspx" id="mnu_RealisationDataEntryMaker" runat="server"
							disabled="disabled" visible="false">Export Realisation Data Entry-Maker</a> </li>
                             <%----------------------------------------added by Nilesh--------------------------------%>
                      <li> 
                        <a href="~/EXP/EXP_ViewRealisationEntry_Checker.aspx" id="mnu_RealisationDataEntrychecker" runat="server"
                              disabled="disabled" visible="false">Export Realisation Data Entry-Checker</a>
                      </li>
                      <%--------------------------------------------------------added by Anand 31-07-2023--------------------------%>
                     <li>
					    	<a href="~/EXP/EXP_ViewRealisationEntry_PRN_Maker.aspx" id="mnu_EXPApprovedRealisationForPRNMaker" runat="server"
					    		disabled="disabled" visible="false">Approved Realisation For PRN-Maker</a>
					     </li>
                      <li>
					    	<a href="~/EXP/EXP_ViewRealisationEntry_PRN_Checker.aspx" id="mnu_EXPApprovedRealisationForPRNChecker" runat="server"
					    		disabled="disabled" visible="false">Approved Realisation For PRN-Checker</a>
					     </li>
                    <%----------------------------------------------------------------------------End-------------------------------------%>
					   <li>
					   <a href="~/EXP/EXP_ViewUpdatingAcceptedDueDate.aspx" id="mnu_EXPdueDate" runat="server"
							disabled="disabled" visible="false">Updation of Accepted Due Date/Agency Commn</a>
						</li>
						<li> <a href="~/EXP/EDPMS_INW_File_DataEntryView.aspx" id="mnuInwDataEntry" runat="server"
							disabled="disabled" visible="false">Inward Remittances Data Entry-Maker</a>
						</li>
						 <%----------------------------------------added by Nilesh--------------------------------%>
						<li> <a href="~/EXP/EDPMS_INW_File_DataEntry_CheckerVIEW.aspx" id="mnuInwDataEntrychecker" runat="server"
							disabled="disabled" visible="false">Inward Remittances Data Entry-Checker</a>
						</li>
						 <li>
						 <a href="~/EXP/EXP_Merchantize_Trade_Document.aspx" id="mnuEXP_MerchantTrade"
						  runat="server" disabled="disabled" visible="false">Import linking to Merchanting Trade</a> 
						  </li>
                          <li>
						<a href="~/EXP/Expswift_Makerview.aspx" id="mnu_EXP_swift" runat="server"
						 disabled="disabled" visible="false">Export Swift Message Maker</a>
						 </li>
						<li>
						<a href="~/EXP/Expswift_CheckerView.aspx" id="mnu_EXP_swiftchecker" runat="server"
						 disabled="disabled" visible="false">Export Swift Message Checker</a>
						 </li>
                         <%--Export module End Transaction--%> 
						<li><a href="~/XOS/XOS_AddEditExtensionData.aspx" id="mnu_XOSExtension" runat="server"
							disabled="disabled" visible="false">Export Bill Due Date Extension Data Entry</a>
						</li>
						<li><a href="~/XOS/XOS_ViewWriteOffEntry.aspx" id="mnu_XOSWriteOff" runat="server"
							disabled="disabled" visible="false">Export Bill WriteOff Data Entry</a> </li>
						<li><a href="~/XOS/Exp_ViewGrDetails.aspx" id="mnu_UpdateGR" runat="server" disabled="disabled"
							visible="false">Updation Of GR Details For XOS</a> </li>
						<%--EBRC Module Transaction--%> 
						<li><a href="~/EBR/TF_EBRC_ViewExRdateEntrylist.aspx" id="mnu_EXPRealisation" runat="server"
							disabled="disabled" visible="false">EBRC Data Entry</a> </li>
						<li><a href="~/EBR/EBRC_CancellationDataEntry.aspx" id="mnu_EBRCancellationDataEntry"
							runat="server" disabled="disabled" visible="false">E-BRC Cancellation Data Entry</a>
						</li>
						<li><a href="~/EBR/TF_EBRC_ITTEUC_Cheker.aspx" id="mnu_EBRCITTEUCCheker"
							runat="server" disabled="disabled" visible="false">E-BRC ITT EUC Checker</a>
						</li>
						<%--EBRC Module End Transaction--%> 
						<%--EDPMS Transaction --%>
						<li>
						<a href="~/EDPMS/EXP_ViewPaymentExtension.aspx" id="mnu_payextn" runat="server"
							disabled="disabled" visible="false">Payment Extension Data Entry</a>
						  </li>
						<li>
						<a href="~/EDPMS/EDPMS_ViewEDPMSData.aspx" id="mnuEDPMSData" runat="server" disabled="disabled"
							visible="false">Updation of Error Records</a> 
						 </li>
						<li>
						<a href="~/EDPMS/EDPMS_View_Bill_Details.aspx" id="mnuEDPMS_BillDetails" runat="server"
							disabled="disabled" visible="false">Updation of EDPMS Bill Details</a>
						  </li>
						<li><a href="~/EDPMS/EDPMS_ViewEDPMSDataUpdation.aspx" id="mnu_EDPMSDataUpdation"
							runat="server" disabled="disabled" visible="false">Updation Of EDPMS Data</a>
						</li>
						<li><a href="~/EDPMS/View_IRM_AdustmentClosure.aspx" id="mnu_adj" runat="server"
							disabled="disabled" visible="false">Inward Remittance Adjustment/Closure</a>
						</li>
						<li><a href="~/EDPMS/EDPMS_View_E_FIRCIssue.aspx" id="mnu_EDPMS_EFirc" runat="server"
							visible="false">EDPMS E FIRC</a> </li>
						<%--NILESH CHANGES 23082023--%>
						<li><a href="~/EDPMS/TF_EBRC_ORM_MakerView.aspx" id="mnu_EBRORMMaker" runat="server" disabled="disabled" visible="false">E-BRC ORM Maker</a></li>
                        <li><a href="~/EDPMS/TF_EBRC_ORM_CheckerView.aspx" id="mnu_EBRORMChecker" runat="server" disabled="disabled" visible="false">E-BRC ORM Checker</a></li>
							<%--EDPMS end transaction--%>
							<%--IDPMS Transactions --%>
						<li><a href="~/IDPMS/IDPMS_BOEAddEdit.aspx?mode=Add" id="mnu_AddEditBOE" runat="server"
							disabled="disabled" visible="false">Bill of Entry-Payment Settlement Data Entry</a>
						</li>
						<li><a href="~/IDPMS/IDPMS_ManualBOE_View.aspx" id="mnuAddEditManualBoe" runat="server"
						   disabled="disabled" visible="false">Bill of Entry-Manual Port Data Entry</a>
							</li>
						<li><a href="~/IDPMS/View_TF_OtherBOE_Data_Entry.aspx" id="mnu_OtherAdBOE" runat="server"
						   disabled="disabled" visible="false">Bill Of Entry-Other AD Data Entry</a> 
						 </li>
						<li><a href="~/IDPMS/TF_AddEditPaymentExtn.aspx" id="mnu_AddEditPEX" runat="server"
						   disabled="disabled" visible="false">(Payment Extension Data Entry)</a> </li>
						<li><a href="~/IDPMS/View_Outward_Remittance_Closure.aspx" id="mnuORM_Closure" runat="server"
						   disabled="disabled" visible="false">ORM Adjustment Closure</a> </li>
						<li><a href="~/IDPMS/TF_IDPMS_BOEClosure_View.aspx" id="mnu_BOEClosure" runat="server"
						   disabled="disabled" visible="false">Bill Of Entry Closure</a> </li>
						<li><a href="~/IDPMS/IDPMS_BOE_Setlement_view.aspx" id="mnu_BOE_Sett_View" runat="server"
							disabled="disabled" visible="false">Bill Of Entry - Settlement Deletion Entry</a>
						</li>
						<li><a href="~/IDPMS/IDPMS_BOE_Setlement_Cancel_view.aspx" id="mnuBOECancel" disabled="disabled"
							visible="false" runat="server">IDPMS XML (Payment Settlement Cancellation) File Generation</a>
						</li>
							 <%--IDPMS end Transactions --%>
						<%--R RETURN--%>
						<li><a href="~/RRETURN/RET_ViewReturnData.aspx" id="mnu_RReturn" runat="server" disabled="disabled"
							visible="false">R Return Data Entry</a> </li>
						<li><a href="~/RRETURN/RET_AddEditNostroOpCloBalanceDataEntry.aspx" id="mnu_Nostro"
							runat="server" disabled="disabled" visible="false">Nostro Opening/Closing balance Data Entry</a> </li>
						<li><a href="~/RRETURN/RET_AddEditVostroOpCloBalanceDataEntry.aspx" id="mnu_Vostro"
							runat="server" disabled="disabled" visible="false">Vostro Opening/Closing balance Data Entry</a> </li>

						<%--Import Automation start Transactions--%>
						<li>
							<a href="~/IMP/Transactions/TF_IMP_BOE_Maker_View.aspx" id="mnu_IMP_BOE_Maker_View" runat="server" disabled="disabled" visible="false">Imports Bill Lodgment - Maker</a>
						</li>
						<li>
							<a href="~/IMP/Transactions/TF_IMP_BOE_Checker_View.aspx" id="mnu_IMP_BOE_Checker_View" runat="server" disabled="disabled" visible="false">Imports Bill Lodgment - Checker</a>
						</li>
						<li>
							<a href="~/IMP/Transactions/TF_IMP_BOOKING_OF_IBD_AND_ACC_Maker_View.aspx" id="mnu_IMP_Booking_Of_IBD_ACC_Maker" runat="server" disabled="disabled" visible="false">Import Bill Acceptance Of B/C - Maker</a>
						</li>
						<li>
							<a href="~/IMP/Transactions/TF_IMP_BOOKING_OF_IBD_AND_ACC_Checker_View.aspx" id="mnu_IMP_Booking_Of_IBD_ACC_Checker" runat="server" disabled="disabled" visible="false">Import Bill Acceptance Of B/C - Checker</a>
						</li>
						<li>
							<a href="~/IMP/Transactions/TF_IMP_Settlement_Maker_View.aspx" id="mnu_IMP_Settlement_Maker" runat="server" disabled="disabled" visible="false">Imports Bill Settlement - Maker</a>
						</li>
						<li>
							<a href="~/IMP/Transactions/TF_IMP_Settlement_Checker_View.aspx" id="mnu_IMP_Settlement_Checker" runat="server" disabled="disabled" visible="false">Imports Bill Settlement - Checker</a>
						</li>
						<li>
							<a href="~/IMP/Transactions/TF_IMP_LC_DESCOUNTING_ACC_IBD_Maker_View.aspx" id="mnu_IMP_LC_DESCOUNTING_ACC_IBD_Maker" runat="server" disabled="disabled" visible="false">Own LC Bill Discounting (IBD,ACC) - Maker</a>
						</li>
						<li>
							<a href="~/IMP/Transactions/TF_IMP_LC_DESCOUNTING_ACC_IBD_Checker_View.aspx" id="mnu_IMP_LC_DESCOUNTING_ACC_IBD_Checker" runat="server" disabled="disabled" visible="false">Own LC Bill Discounting (IBD,ACC) - Checker</a>
						</li>
						<li>
							<a href="~/IMP/Transactions/TF_IMP_LC_DESCOUNTING_Settlement_ACC_IBD_Maker_View.aspx" id="mnu_LC_DESCOUNTING_Settlement_Maker" runat="server" disabled="disabled" visible="false">Own LC Bill Discounting Settlement View (IBD,ACC)-Maker</a>
						</li>
						<li>
							<a href="~/IMP/Transactions/TF_IMP_LC_DESCOUNTING_Settlement_ACC_IBD_Checker_View.aspx" id="mnu_LC_DESCOUNTING_Settlement_Checker" runat="server" disabled="disabled" visible="false">Own LC Bill Discounting Settlement View (IBD,ACC)-Checker</a>
						</li>
						<li>
							<a href="~/IMP/Transactions/TF_IMP_ViewBRO.aspx" id="mnu_BankReleaseOrder" runat="server" disabled="disabled" visible="false">Bank Release Order (BRO) - Maker</a>
						</li>
						<li>
							<a href="~/IMP/Transactions/TF_IMP_BRO_Checker_View.aspx" id="mnu_BankReleaseOrder_checker_view" runat="server" disabled="disabled" visible="false">Bank Release Order (BRO) - Checker</a>
						</li>
						<li>
							<a href="~/IMP/Transactions/TF_IMP_ReversalofGO_Maker_View.aspx" id="mnu_BROGO_Maker_View" runat="server" disabled="disabled" visible="false">BRO Margin Reversal - Maker</a>
						</li>
						<li>
							<a href="~/IMP/Transactions/TF_IMP_ReversalofGO_Checker_View.aspx" id="mnu_BROGO_Checker_View" runat="server" disabled="disabled" visible="false">BRO Margin Reversal - Checker</a>
						</li>
						<li>
							<a href="~/IMP/Transactions/TF_IMP_InquiryOfLedgerFile_Maker.aspx" id="mnu_TF_IMP_InquiryOfLedgerFile_Maker" runat="server" disabled="disabled" visible="false">Inquiry of Ledger File - Maker</a>
						</li>
						<li>
							<a href="~/IMP/Transactions/TF_IMP_InquiryOfLedgerFile_Checker_View.aspx" id="mnu_TF_IMP_InquiryOfLedgerFile_Checker" runat="server" disabled="disabled" visible="false">Inquiry of Ledger File - Checker</a>
						</li>
						<li>
							<a href="~/IMP/Transactions/TF_IMP_Ledger_Modification_ICA_ICU_IBA_Maker_View.aspx" id="mnu_TF_IMP_Ledger_Modification_ICA_ICU_IBA_Maker" runat="server" disabled="disabled" visible="false">Ledger Modification View (ICA,ICU,IBA) - Maker</a>
						</li>
						<li>
							<a href="~/IMP/Transactions/TF_IMP_Ledger_Modification_ICA_ICU_IBA_Checker_View.aspx" id="mnu_TF_IMP_Ledger_Modification_ICA_ICU_IBA_Checker" runat="server" disabled="disabled" visible="false">Ledger Modification View (ICA,ICU,IBA) - Checker</a>
						</li>

						<li>
							<a href="~/IMP/Transactions/TF_IMP_Shipping_Guarantee_Maker_View.aspx" id="mnu_Shipping_Guarantee_Maker" runat="server" disabled="disabled" visible="false">Booking liability of Shipping Guarantee - Maker</a>
						</li>
						<li>
							<a href="~/IMP/Transactions/TF_IMP_Shipping_Guarantee_Checker_View.aspx" id="mnu_Shipping_Guarantee_Checker" runat="server" disabled="disabled" visible="false">Booking liability of Shipping Guarantee - Checker</a>
						</li>
						<li>
							<a href="~/IMP/Transactions/TF_IMP_ReversalOfLiability_Maker_View.aspx" id="mnu_TF_IMP_ReversalOfLiability_Maker_View" runat="server" disabled="disabled" visible="false">Reversal Of Liability - Maker</a>
						</li>
						<li>
							<a href="~/IMP/Transactions/TF_IMP_ReversalOfLiability_Checker_View.aspx" id="mnu_TF_IMP_ReversalOfLiability_Checker_View" runat="server" disabled="disabled" visible="false">Reversal Of Liability - Checker</a>
						</li>
                        <li>
							<a href="~/IMP/Transactions/TF_IMP_SwiftFreeFormat_View.aspx?ID=Swift Free Format Message - Maker" id="mnu_IMP_FreeFormatSwift_Maker" runat="server">Swift Free Format Message - Maker</a>
						</li>
                        <li>
							<a href="~/IMP/Transactions/TF_IMP_SwiftFreeFormat_View.aspx?ID=Swift Free Format Message - Checker" id="mnu_IMP_FreeFormatSwift_Checker" runat="server">Swift Free Format Message - Checker</a>
						</li>
						 <%--Import Automation End Transactions--%>
					</ul>
				</li>
				<li><a href="#">File Creation</a>
					<ul>
						<li><a href="~/Reports/EXPORTReports/EXP_CreateSWIFT_MT730_DETAIL.aspx" id="mFC_LCAckMT730"
							runat="server" disabled="disabled" visible="false">LC Acknowledge Message (MT730)</a>
						</li>
						<li><a href="~/Reports/EXPReports/EXP_CreateSWIFT__REIMBURSEMENT_STATEMENT_DETAIL.aspx"
							id="mFC_IMP_ReimbursementStatement" runat="server" disabled="disabled" visible="false">Reimbursement Statement (MT 999)</a> </li>
						<li><a href="~/Reports/EXPReports/EXP_CreateSWIFT__MT742_REIMBURSEMENT_STATEMENT_DETAIL.aspx"
							id="mFC_IMP_MT742_Reimbursement" runat="server" disabled="disabled" visible="false">Reimbursement Statement (MT 742)</a> </li>
						<li><a href="~/Reports/EXPReports/EXP_CreateSWIFT_MT999_FATE_ENQUIRY_DETAIL.aspx"
							id="mFC_IMP_MT999_Fate_Enquiry" runat="server" disabled="disabled" visible="false">Fate Enquiry (MT 999)</a> </li>
						<li><a href="~/Reports/EXPReports/EXP_CreateSWIFT_MT420_FATE_ENQUIRY_DETAIL.aspx"
							id="mFC_IMP_MT420_Fate_Enquiry" runat="server" disabled="disabled" visible="false">Fate Enquiry (MT 420)</a> </li>
						<li><a href="~/Reports/EXPReports/EXP_CreateSWIFT_FATE_ENQUIRY_UNDER_LC_DETAIL.aspx"
							id="mFC_IMP_Fate_Enquiry_Under_LC" runat="server" disabled="disabled" visible="false">Fate Enquiry Under LC</a> </li>
						<li><a href="~/Reports/EXPReports/EXP_CreateSWIFT__MT799_REIMBURSEMENT_DETAIL.aspx"
							id="mFC_EXP_MT799_Reimbursement" runat="server" disabled="disabled" visible="false">Swift Message MT 799</a> </li>
						<li><a href="~/Reports/EXPReports/EXP_CreateSWIFT__MT754_REIMBURSEMENT_STATEMENT_DETAIL.aspx"
							id="mFC_EXP_MT754_Reimbursement" runat="server" disabled="disabled" visible="false">Advice Of Negotiation (MT 754)</a> </li>
						<li><a href="~/Reports/EXPReports/TF_Export_Realisation_ExcelFile_Creation.aspx"
							id="mnuExpRealisationFile" runat="server" disabled="disabled" visible="false">Export
							Realisation CSV File (General)</a> </li>
						<li><a href="~/Reports/EXPReports/TF_EXPORT_REALISATIONON SOMECUSTOMER.aspx" id="mnuExpMbillFileforsome"
							runat="server" disabled="disabled" visible="false">Export Realisation / MBills CSV
							for Sun Pharma</a> </li>
						<li><a href="~/Reports/EXPReports/TF_EXPORT_MBILL_ENTRY_EXCELFILE_CREATION.aspx"
							id="mnuExpMbillFile" runat="server" disabled="disabled" visible="false">Export MBills
							CSV File</a> </li>
							<%-- Export module File creation --%>
						<li><a href="~/EXP/Problem_Exp_Bill_mgnt_circulation_xlx_genarate.aspx" id="MnuprblmExpBill"
							runat="server" disabled="disabled" visible="false">Problem Export Bill Control Excel File Generation</a> 
						</li>
						 <li>
						 <a href="~/EXP/EXP_Bill_Receipt_FileCreation.aspx" id="mnu_Export_Bill_Receipt_CSVFileCreation"
							runat="server" disabled="disabled" visible="false">Export Bill Receipt CSV File Creation</a>
						 </li>
						<li><a href="~/EXP/EXP_Bill_Realisation_CSV.aspx" id="mnuRealisationCSV" runat="server"
							disabled="disabled" visible="false">Export Bill Realisation CSV File Creation</a>
						</li>
						<li><a href="~/EXP/EXP_Manual_GR_CSV.aspx" id="mnuManualGRCSV" runat="server" disabled="disabled"
							visible="false">Export Manual GR CSV File Creation</a> </li>
						<li><a href="~/EXP/EXP_RemittanceAdvance_CSV.aspx" id="mnuRemittanceAdvance" runat="server"
							disabled="disabled" visible="false">Export Document against Advance Remittance CSV File</a>
						</li>
						<li><a href="~/EXP/rptGenerateRReturnData.aspx" id="generateReturnData" runat="server"
							disabled="disabled" visible="false">RReturn Data CSV File Creation</a> </li>
						<li><a href="~/EXP/rptGenerateGBaseData.aspx" id="generateGBaseData" runat="server"
							disabled="disabled" visible="false">Export GBase Data Excel File Creation</a>
						</li>

						<%-- Export module File creation end --%>
					  
						<li><a href="~/EXP/TF_EXPORT_EBRC_File_Creation.aspx" id="mnuExpebrcfile" runat="server"
							disabled="disabled" visible="false">EBRC File Creation (Old)</a> </li>

						<li><a href="~/Reports/XOSReports/Export_Report_XOS.aspx" id="mnuEXPXO" runat="server"
							disabled="disabled" visible="false">XOS CSV File</a> </li>
						<li><a href="~/Reports/XOSReports/Export_Report_XOS_Nil_Statement.aspx" id="mnu_NilXOS"
							runat="server" disabled="disabled" visible="false">XOS NIL CSV File</a> </li>
							<%-- Ebrc module File creation --%>
						<li><a href="~/EBR/Ebrc_Generate_TradeData.aspx?PageHeader=Generate Realised Data From Trade Finance"
							id="mnu_Ebrc_Generate_TradeData" runat="server" disabled="disabled" visible="false">Generate EBRC Data</a> </li>
						<li><a href="~/EBR/Ebrc_GenerateXML.aspx?PageHeader=Generate XML File" id="mnu_Ebrc_Generate_XML"
							runat="server" disabled="disabled" visible="false">E-BRC XML File Generation</a>
						</li>
						 <%-- Ebrc module File creation end --%>
						  <%--EDPMS File Creation--%>
						<li><a href="~/EDPMS/EDPMS_GenerateXMLFile_INWRemittance.aspx" id="mnu_inwxml" runat="server"
							disabled="disabled" visible="false">EDPMS XML (IRM) File Generation</a></li>
						<li><a href="~/EDPMS/TF_EDPMS_FIRC_Created.aspx" id="mnu_E_Firc" runat="server" visible="false">EDPMS XML (E FIRC) File Generation</a></li>
						<li><a href="~/EDPMS/EDPMS_GenerateXMLFile_IRMAdjstmntClsr.aspx" id="mnu_closurexml"
							runat="server" disabled="disabled" visible="false">EDPMS XML (IRM CLOSURE) File Generation</a> </li>
						<li><a href="~/EDPMS/EDPMS_Generate_XMLFile_Receipt_Document.aspx?PageHeader=EDPMS XML (Receipt) File Generation"
							id="mnu_EDPMS_XML_Receipt" runat="server" disabled="disabled" visible="false">EDPMS XML (Receipt) File Generation</a> </li>
						<li><a href="~/EDPMS/EDPMS_XMLgeneration_Realized.aspx?PageHeader=EDPMS XML (Realization) File Generation"
							id="mnu_EDPMS_XML_Realization" runat="server" disabled="disabled" visible="false">EDPMS XML (Realization) File Generation</a> </li>
						<li><a href="~/EDPMS/EDPMS_Generate_XMLFile_Receipt_Document.aspx?PageHeader=EDPMS XML (AD Transfer) File Generation"
							id="mnu_EDPMS_AD_Transfer" runat="server" disabled="disabled" visible="false">EDPMS XML (AD Transfer) File Generation</a> </li>
						<li><a href="~/EDPMS/EDPMS_Payment_Extension_Register_XML_Generation.aspx" id="mnu_pmtextfilecrn"
							runat="server" disabled="disabled" visible="false">EDPMS XML (Payment Extension) File Generation</a></li>

						<li><a href="~/EDPMS/EDPMS_E_FIRC_Closure_XML_Generation.aspx" id="mnuE_Firc_Closure_Xml"
							runat="server" disabled="disabled" visible="false">EDPMS XML (E FIRC Closure) File Generation</a> </li>
						<li><a href="~/EDPMS/EDPMS_File_Creation.aspx" id="mnuDataTransfer" runat="server"
							disabled="disabled" visible="false">EDPMS Data Generation From Export Data</a>
						</li>
						<li><a href="~/EDPMS/EDPMS_RODFileCreationfromAdTransfer.aspx" id="mnu_roddataadtrans"
							runat="server" disabled="disabled" visible="false">Generate ROD Data from AD Transfer</a>
						</li>
						<li><a href="~/EDPMS/EXP_EDPMSAckwAndRealized_CSV.aspx" id="mnu_AckReal" runat="server"
							disabled="disabled" visible="false">EDPMS Acknowledgement And Realized Data File Generation</a> </li>
					   <%--EDPMS END File Creation --%>
						<%--IDPMS File Creation --%>
						<li><a href="~/IDPMS/TF_IDPMS_Out_Remittance_FileCreation.aspx" id="mnu_ORMXMLCreation"
							runat="server" disabled="disabled" visible="false">IDPMS XML (Outward Remittance) File Generation</a>
						</li>
						<li><a href="~/IDPMS/TF_IDPMS_Manual_Bill_File_Created_.aspx" id="mnu_ManFile" runat="server"
							disabled="disabled" visible="false">IDPMS Manual File Creation</a></li>

						<li><a href="~/IDPMS/BOE_CSV_File_Creation.aspx" id="mnu_otherADcsvcre" runat="server"
							disabled="disabled" visible="false">Other AD BOE CSV File Creation</a></li>

						<li><a href="~/IDPMS/TF_IDPMS_Payment_Settlement_XML_Generation.aspx" id="mnu_PaySetCre"
							disabled="disabled" visible="false" runat="server">IDPMS XML (Payment Settlement) File Generation</a></li>

						<li><a href="~/IDPMS/IDPMS_BOE_PaymentExtension_XML_Generation.aspx" id="mnu_PayExt"
						   disabled="disabled" visible="false" runat="server">IDPMS XML (BOE Payment Extention) File Generation</a></li>
						
						<li><a href="~/IDPMS/IDPMS_ORM_Clousure_XML_Generation.aspx" id="mnuORM_XMl" runat="server"
						   disabled="disabled" visible="false">IDPMS XML (Outward Remittance Closure) File Generation</a> </li>

						<li><a href="~/IDPMS/TF_IDPMS_BOE_Closure_XML_Generation.aspx" id="mnuBOEClosure"
						  disabled="disabled"  runat="server" visible="false">IDPMS XML (BOE Closure) File Generation</a></li>
 
						<li><a href="~/TF_CustomerMaster_CSV_file_Genaration.aspx" id="mnu_CustomerMaster_CSV_file"
							runat="server" disabled="disabled" visible="false">Customer Master CSV File Creation</a>
						</li>
						<%--IDPMS END File Creation --%>
						<!--------------------------------------------------RREURN file creation-------------------------------------------------------------------------->
						<li><a href="~/RRETURN/Ret_TxtFileCReation.aspx?PageHeader=RBI Text File [QE,BOP6]"
							id="mnu_ret_txtFileCreation" runat="server" disabled="disabled" visible="false">RBI Text File [QE,BOP6]</a> </li>
						<li><a href="~/RRETURN/Ret_DataCSV.aspx?PageHeader=Data File [CSV] For Checking"
							id="mnu_ret_DataCsvforCheck" runat="server" disabled="disabled" visible="false">File Extraction For HO</a> </li>
						<li><a href="~/RRETURN/RET_RBITextFileAtHeadOffice.aspx?PageHeader=RBI Text File Creation At Head Office"
							id="mnuRET_RBITextFileAtHeadOffice" runat="server" disabled="disabled" visible="false">RBI Text File Creation At Head Office</a> </li>
						<li><a href="~/RRETURN/Ret_CBTR_CSV_File_GENERATE.aspx" id="mnu_Ret_CBTR_CSV_File_GENERATE"
							runat="server" visible="false" disabled="disabled">CSV File For CBTR</a> </li>
						<!--------------------------------------------------ImportWarehousing file creation-------------------------------------------------------------------------->
						<li><a href="~/ImportWareHousing/FileCreation/TF_IMPWH_GDPExcelFileCreation.aspx?PageHeader=Good To Pay File Creation&Type=GDP" id="mnu_GDPExcelFileCreation"
							runat="server" disabled="disabled" visible="false">Good To Pay File Creation</a>
						</li>
						<li><a href="~/ImportWareHousing/FileCreation/TF_IMPWH_GDPExcelFileCreation.aspx?PageHeader=Payment Authorization File Creation&Type=Payment" id="mnu_PaymentFileCreation"
							runat="server" disabled="disabled" visible="false">Payment File Creation</a>
						</li>
						<li><a href="~/ImportWareHousing/FileCreation/TF_IMPWH_SettlementFileCreation.aspx?PageHeader=Settlement File Creation" id="mnu_SettlementFileCreation"
							runat="server" disabled="disabled" visible="false">Settlement File Creation</a>
						</li>
						<!--------------------------------------------------IMPORT Auto file creation-------------------------------------------------------------------------->
						<%--<li>
							<a href="~/IMP/FileCreation/TF_IMP_GBaseFileCreation.aspx" id="mnu_IMP_AUTO_GBaseFileCreation" runat="server"  visible="false" disabled="disabled">Import GBase File Creation</a>
						</li>--%>
 <li><a href="~/IMP/FileCreation/CustomerDataDump.aspx" id="mnu_IMP_CustDumpdata" runat="server" disabled="disabled" visible="false">Customer Master In Excel Format</a></li>
                          <li><a href="~/IMP/FileCreation/BankDataDump.aspx" id="mnu_imp_bankDumpData" runat="server" disabled="disabled" visible="false">Bank Master In Excel Format</a></li>
					</ul>
				</li>

				<li><a href="#">Excel Reports</a>
					<ul>
					 <!--------------------------------------------------IMPORT Auto Excel reports-------------------------------------------------------------------------->
						<li><a href="~/IMP/FileCreation/TF_Excel_Lodgmentmakers.aspx" id="mnu_lodgmentmaker" runat="server" visible="false" disabled="disabled">Lodgment Report (All Fields)</a></li>
						<li><a href="~/IMP/FileCreation/TF_IMP_AcceptanceApproveFileCreationExcel.aspx" id="mnu_AcceptanceApproveFile_XL" runat="server" visible="false" disabled="disabled">Acceptance Report (All Fields)</a></li>
						<li><a href="~/IMP/FileCreation/TF_IMP_SettlementFileCreation.aspx" id="mnu_SettlementFileCreation_XL" runat="server" visible="false" disabled="disabled">Settlement Report (All Fields)</a></li>
						<li><a href="~/IMP/IMPReports/TF_IMP_ImportBillLodgement_ExcelReport.aspx" id="mnuLodgmentExcelrpt" runat="server" disabled="disabled" visible="false">Import Bill Lodgment (Approved and Selected Fields)</a></li>
						<li><a href="~/IMP/IMPReports/TF_IMP_ImportBillAcceptance_ExcelReport.aspx" id="mnuAcceptanceExcelrpt" runat="server" disabled="disabled" visible="false">Import Bill Acceptance (Approved and Selected Fields)</a></li>
						<li><a href="~/IMP/IMPReports/TF_IMP_Shipping_Guarantee_Register.aspx" id="mnuSG_Register" runat="server" disabled="" visible="false">Import SG Register (Approved and Selected Fields)</a></li>
						<li><a href="~/IMP/IMPReports/TF_IMP_Bill_Report.aspx" id="mnuImpBill_Register" runat="server" disabled="" visible="false">Import Bill Register</a></li>
					<!--------------------------------------------------IMPORT Auto Excel reports-------------------------------------------------------------------------->
					</ul>
				</li>
				<li><a href="#">Reports</a>
					<ul>
					<%--Export Reports --%>
						<li>
						<a href="~/Reports/EXPORTReports/rpt_Inward_Remittances_Register.aspx" id="mRep_InwRemReg"
							runat="server" disabled="disabled" visible="false">Inward Remittance Register</a>
						</li>
						<li>
						<a href="~/Reports/EXPORTReports/rpt_Inward_Remittance_outstanding_statement.aspx" id="mnu_rptIRM_Outstanding"
							runat="server" disabled="disabled" visible="false">Inward Remittance Outstanding Statement</a>
						</li>
						<%-----------------------------------------------------------------Anand 23-08-2023-------------------------------------------%>
                        <li>
						<a href="~/Reports/EXPORTReports/rpt_Inward_Remittance_CheckList.aspx" id="mnu_rptIRM_CheckList"
							runat="server" disabled="disabled" visible="false">Inward Remittance CheckList</a>
						</li> 
						 <%-------------------------------------------Anad 18-09-2023-----------------------------%>
                        <li>
						<a href="~/Reports/EXPORTReports/rpt_Export_DiscountAdvice.aspx" id="mnu_EXP_DiscountAdvice"
							runat="server" disabled="disabled" visible="false">EXPORT BILL DISCOUNT/NEGOTIATION ADVICE</a>
						</li><%--END ----%>
						<li><a href="~/Reports/EXPReports/rptExportBillRegister.aspx" id="mnuEXPBillReg"
							runat="server" disabled="disabled" visible="false">Export Bill Register</a>
						</li>
						<li><a href="~/Reports/EXPReports/rptExportBillRegister_Unaccepted.aspx" id="mnuEXPBillReg_Unaccepted"
							runat="server" disabled="disabled" visible="false">Export Bill Register (Unaccepted)</a>
						</li>
						<li>
						<a href="~/Reports/EXPReports/rptExportBillintimation.aspx?PageHeader=Export Bill lodgement Intimation"
							id="mnuEXPBillIntimetion" runat="server" disabled="disabled" visible="false">Export	Bill Lodgement Intimation</a>
						 </li>
						<li><a href="~/Reports/EXPReports/rptExportBillDocument.aspx?PageHeader=Export Bill lodgement"
							id="mnuEXPBillDocument" runat="server" disabled="disabled" visible="false">Export Bill Lodgement Advice</a> 
						 </li>
						<li><a href="~/Reports/EXPReports/rptExportBillDocument.aspx?PageHeader=Export Bill Document Realisation"
							id="mnuEXPBillRealisedDocument" runat="server" disabled="disabled" visible="false">Export Bill Document Realisation Register</a>
						</li>
						<li><a href="~/Reports/EXPReports/rptExportOverDueStatement.aspx" id="mnuExportOverDueStatement"
							runat="server" disabled="disabled" visible="false">Export Bills OverDue Statement</a>
						</li>
						<li><a href="~/Reports/EXPReports/Export_Report_BillOutStanding.aspx" id="mnu_EXBillOut"
							runat="server" disabled="disabled" visible="false">Export Bill Outstanding</a>
						</li>
						<li><a href="~/Reports/EXPReports/rptExportBillRealisationReport1.aspx?PageHeader=Export Realisation Report"
							id="mnuEXPRealisationReport" runat="server" disabled="disabled" visible="false">Export Realisation Report</a>
						</li>
						<li><a href="~/Reports/EXPReports/rptExportBillRegister_Delinking.aspx?PageHeader=Export Documents Realised Report"
							id="mnuListofExpDocRealised" runat="server" disabled="disabled" visible="false">Export Documents Realised Report</a> 
						</li>
						<li><a href="~/Reports/EXPReports/rptExportBillRegister_Delinking.aspx?PageHeader=Export Documents Despatched Report"
							id="mnuListofExpDocDispatched" runat="server" disabled="disabled" visible="false">Export Documents Despatched Report</a>
						</li>
						<li><a href="~/Reports/EXPReports/rpt_Advance_Exp_Bill_Register.aspx" id="mnuadvanceexp"
							runat="server" disabled="disabled" visible="false">Export Bill Covering Letter</a>
						</li>
						<li><a href="~/Reports/EXPReports/TF_Export_Nonsubmission_Export_Doc_followup.aspx"
							id="mnunonsubmissionsdoc" runat="server" disabled="disabled" visible="false">Non Submission of Export Documents - Follow up</a>
						</li>
						<li><a href="~/Reports/EXPReports/rpt_Exp_Bill_Caution_Advice.aspx" id="mnuCautionAdviceEXPBill"
							runat="server" disabled="disabled" visible="false">Export Bill Caution Advice</a>
						</li>
						 <li><a href="~/Reports/EXPReports/EXP_DocsReceived_ADTransfer.aspx" id="mnuADTransfer"
							runat="server" disabled="disabled" visible="false">Export Documents Received -For AD Transfer</a>
						 </li>

						<li>
						<a href="~/Reports/EDPMS_Reports/EDPMS_DueDate_Summary.aspx" id="mnuBilldueDate"
							runat="server" disabled="disabled" visible="false">Bill Due Dates Falling on Holidays</a>
						</li>
						<li><a href="~/Reports/EXPReports/EXP_rptMerchantTradeRegister.aspx" id="mnuEXP_MerchantTradeReport"
							runat="server" disabled="disabled" visible="false">Merchanting Trade Bills Report</a>
						</li>
						<li><a href="~/Reports/EDPMS_Reports/EDPMS_rpt_AdvanceRemittanceReceived.aspx" id="mnuAdvanceRemittance"
							runat="server" disabled="disabled" visible="false">Export Document against Advance Remittance</a>
						</li>
						<li><a href="~/Reports/EXPReports/rptSDFStatement.aspx" id="mnuEXP_SDFstatement"
							runat="server" disabled="disabled" visible="false">Export Bill SDF Statement</a>
						</li>
						<li><a href="~/Reports/EXPReports/rptExportBillRealisationReport1.aspx?PageHeader=Export Bills Due"
							id="mnuexportBillsDue" runat="server" disabled="disabled" visible="false">Export Bills Due</a>
						</li>
						<li><a href="~/Reports/EXPReports/rptExportDataStatusReport.aspx" id="mnuexportDataStatus"
							runat="server" disabled="disabled" visible="false">Export Data Status</a>
						</li>
                        <li><a href="~/EXP/rptLEI_EOD_reports.aspx" id="mnuLEIEODReport" runat="server"
                             disabled="disabled" visible="false" >LEI EOD Reports</a>
                        </li>
						<li>
						<a href="~/Reports/EXPORTReports/rpt_Export_BillSettlementAdvice.aspx" id="mnu_EXP_BillSettlementAdvice"
							runat="server" disabled="disabled" visible="false">EXPORT BILL SETTLEMENT ADVICE</a>
						</li>
						 <%-----------------------------------------------------------------Anand 30-11-2023-------------------------------------------%>
                        <li>
						<a href="~/Reports/EXPORTReports/rpt_Inward_Remittance_CreatedReport.aspx" id="mnu_rptIRM_CreatedReport"
							runat="server" disabled="disabled" visible="false">IRM Created Report</a>
						</li>
						<li>
						<a href="~/Reports/EXPORTReports/rpt_Inward_Remittance_UtilizationReport.aspx" id="mnu_rptIRM_UtilizationReport"
							runat="server" disabled="disabled" visible="false">IRM Utilization Report</a>
						</li>
						<li>
						<a href="~/Reports/EXPORTReports/rpt_Inward_Remittance_OutSatandingReport.aspx" id="mnu_rptIRM_OutSatandingReport"
							runat="server" disabled="disabled" visible="false">IRM O/S Report</a>
						</li>
						 <li> <a href="~/Reports/EXPORTReports/rpt_Export_Pending_ROD.aspx" id="mnu_rpt_PendingROD"
							runat="server" disabled="disabled" visible="false">Export Pending ROD Report</a>
                        </li>
						<li><a href="~/Reports/EXPORTReports/Export_Pending_PRN_Recodrs.aspx" id="mnu_EXP_PendingPRN" runat="server" 
							visible="false">EXPORT Pending PRN Records</a></li>
						<li><a href="~/Reports/EXPORTReports/TF_Export_Report_BillDetails.aspx" id="mnu_rptEXP_ExcelOS_Reports"
							runat="server" disabled="disabled" visible="false">Export Bill O/S Reports-Excel</a></li>
						<li><a href="~/Reports/EXPORTReports/TF_Export_Report_BillDetails270.aspx" id="mnu_rptEXP_ExcelOS_Reports_270"
							runat="server" disabled="disabled" visible="false">Export Bill O/S Reports More Than 270 Days-Excel</a></li>
						<li><a href="~/Reports/EXPORTReports/TF_EXP_ExportBillReport.aspx" id="mnu_rptExportBillReport"
							runat="server" disabled="disabled" visible="false">Export Bill Receipt</a></li>
						<li><a href="~/Reports/EXPORTReports/TF_EXP_ShippingBillsPendingforACK.aspx" id="mnu_rptShippingBillsPendingforACK"
							runat="server" disabled="disabled" visible="false">Shipping Bills Pending for AcK</a></li>
						<li><a href="~/Reports/EXPORTReports/TF_EXP_BILL_Realisation_Report.aspx" id="mnu_EXPRealisation_Excel"
							runat="server" disabled="disabled" visible="false">Export Realisation Excel Report</a></li>
						<%--<li><a href="~/Reports/EXPORTReports/TF_EXP_DIGI_Settlement_Report.aspx" id="mnu_EXP_DigiSettlement"
							runat="server"  visible="true">Export Digi Settlement Report</a></li>--%>
						<li><a href="~/Reports/EXPORTReports/TF_EXP_ExportDigiLodgementReport.aspx" id="mnu_EXP_DigiSettlement"
							runat="server"  visible="true">Export Digi Lodgement/Settlement Report</a></li>						
						<li><a href="~/Reports/EXPORTReports/Export_Pending_Authorization_Transaction_Report.aspx" id="mnu_EXP_pndngatrztn"
							runat="server" disabled="disabled" visible="true" >Export Pending Authorization Transaction Report</a></li>
                        <%-----------------------------------------------------------------END-------------------------------------------%>
							<%--Export Reports end--%>

						<li><a href="~/Reports/XOSReports/Export_Report_XOSValid.aspx" id="mnuExpXOXDATAVaild"
							runat="server" disabled="disabled" visible="false">Input Data Validation For XOS</a>
						</li>
						<li><a href="~/Reports/XOSReports/XOS_rptEBWRegister.aspx?PageHeader=EBW Register"
							id="mnu_EBWRegister" runat="server" disabled="disabled" visible="false">EBW Register</a>
						</li>
						<li><a href="~/Reports/XOSReports/rptETXRegister.aspx" id="mnuETXRegister" runat="server"
							disabled="disabled" visible="false">ETX Register</a> </li>
						<li><a href="~/Reports/XOSReports/XOS_FileUpload_CSV.aspx" id="mnuXOS_Output_Upload"
							runat="server" disabled="disabled" visible="false">XOS CSV Output Validation</a>
						</li>
						<li><a href="~/Reports/XOSReports/XOS_Report.aspx" id="mnu_XOSReport" runat="server"
							disabled="disabled" visible="false">XOS Statement</a> </li>
						<li><a href="~/Reports/XOSReports/XOS_Report_Party_Wise.aspx" id="mnu_XOSReport_Party_Wise"
							runat="server" disabled="disabled" visible="false">XOS Statement Exporter Wise -
							Summary</a> </li>
						<li><a href="~/Reports/XOSReports/XOS_Report_Type_Wise.aspx" id="mnu_XOSReport_Type_Wise"
							runat="server" disabled="disabled" visible="false">XOS Statement Exporter Type Wise
							- Summary</a> </li>
						<li><a href="~/Reports/XOSReports/XOSReport_Summary.aspx" id="mnuXOS_Summary" runat="server"
							disabled="disabled" visible="false">XOS Reconciliation Summary</a> </li>
						<%--EBRReports-------%>
						  <li><a href="~/Reports/EBRReports/rptEBR_Reports.aspx?PageHeader=Data Check List"
							id="mnu_DataCheckList" runat="server" disabled="disabled" visible="false">Data Check List</a>
						  </li>
						<li><a href="~/Reports/EBRReports/rptEBR_DataValidationReports.aspx?PageHeader=Data Validation"
							id="mnu_DataValidation" runat="server" disabled="disabled" visible="false">Data Validation</a>
						 </li>
						<li><a href="~/Reports/EBRReports/rptEBR_Reports.aspx?PageHeader=List of E-BRC Certificate Generated"
							id="mnu_EBRCertGenerated" runat="server" disabled="disabled" visible="false">List of E-BRC Certificate Generated</a>
						 </li>
						<li><a href="~/Reports/EBRReports/rptEBR_Reports.aspx?PageHeader=E-BRC Certificate to be Generated"
							id="mnu_EBRCert_TobeGenerate" runat="server" disabled="disabled" visible="false">E-BRC Certificate to be Generated</a>
						  </li>
						<li><a href="~/Reports/EBRReports/rptEBR_Reports.aspx?PageHeader=List of BRC's Cancelled"
							id="mnu_BRC_Cancelled" runat="server" disabled="disabled" visible="false">List of BRCs Cancelled</a>
						  </li>
						<li><a href="~/Reports/EBRReports/rptEBR_Reports.aspx?PageHeader=Bills Fully Realised - But No GR Details"
							id="mnu_WithoutGRDetails" runat="server" disabled="disabled" visible="false">Bills Fully Realised - But No GR Details</a>
						 </li>
						<li><a href="~/Reports/EBRReports/rptEBRC_Intimation.aspx?PageHeader=EBRC Intimation"
							id="mnuebrcintimation" runat="server" disabled="disabled" visible="false">EBRC Intimation Letter</a>
						</li>
						<%--EDPMS_Reports-------%>
					  <li><a href="~/Reports/EXPORTReports/rpt_Inward_Remittance_Utilization.aspx" id="mnu_InwRem_Utilization"
							runat="server" disabled="disabled" visible="false">Inward Remittance Utilization</a>
						</li>
						  <li><a href="~/Reports/EXPReports/EXP_billpendings_ACK_in _EDPMS.aspx" id="mnuExpbillPendingsAckEDPMS"
							runat="server" disabled="disabled" visible="false">Export Bill Pendings for Acknowledgement in EDPMS</a> </li>

						<li><a href="~/Reports/EDPMS_Reports/EDPMS_Datachecklist_Docreceived.aspx" id="mnu_EDPMS_Datachecklist_DocReceipt"
							runat="server" disabled="disabled" visible="false">Data Checklist - Receipt of Document</a>
						</li>
						<li><a href="~/Reports/EDPMS_Reports/EPDMS_DATACHECK_LIST.aspx" id="mnu_EDPMS_Datachecklist_Realized"
							runat="server" disabled="disabled" visible="false">Data Checklist - Payment Realized</a>
						</li>
						<li><a href="~/Reports/EDPMS_Reports/EDPMS_XMLFile_PendingGenerated.aspx" id="mnu_EDPMS_XML_FilePending"
							runat="server" disabled="disabled" visible="false">XML Files Pending/Generated list</a>
						</li>
						<li><a href="~/Reports/EDPMS_Reports/EDPMS_Receipt_Doc_Ack.aspx" id="mnu_Receipt_AckReport"
							runat="server" disabled="disabled" visible="false">Receipt of Document (Acknowledgement)</a>
						</li>
						<li><a href="~/Reports/EDPMS_Reports/EDPMS_Realization_Doc_Ack .aspx" id="mnu_Realization_AckReport"
							runat="server" disabled="disabled" visible="false">Payment Realized of Document (Acknowledgement)</a> </li>

						<li><a href="~/Reports/EDPMS_Reports/EDPMS_Rpt_E_Firc_Register.aspx" id="Mnu_EDPMS_E_FIRC"
							runat="server" disabled="disabled" visible="false">EDPMS E FIRC Register</a>
						</li>
						<%--EDPMS END Reports --%>
						<%--IDPMS  Reports --%>
						<li><a href="~/IDPMS/IDPMS_Datachecklist_OutwardRemitance.aspx" id="mnu_ORMChecklist"
						   disabled="disabled" runat="server" visible="false">ORM Data Check List</a></li>

						<li><a href="~/IDPMS/IDPMS_BOEDataChecklist.aspx" id="mnu_BOEDataCheck" runat="server"
							disabled="disabled" visible="false">Bill Of Entry - Settlement Report</a> </li>

						<li><a href="~/Reports/IDPMSReports/Mizuho_OutwardRemittance_Ack.aspx" id="mnu_ORMAckReport"
						   disabled="disabled" runat="server" visible="false">ORM (RBI-Acknowledgement)</a> </li>

						<li><a href="~/IDPMS/Mizhuo_ManualBOE_Ack.aspx" id="mnuManualBOEAck" runat="server"
							disabled="disabled" visible="false">Bill Of Entry (RBI-Acknowledgement)</a>
						</li>

						<li><a href="~/Reports/IDPMSReports/BOE_Balance.aspx" id="mnuBOEOSRPT" runat="server"
							disabled="disabled" visible="false">Bill Of Entry - O/S Report</a> </li>

						<li><a href="~/Reports/IDPMSReports/ORM_Balance.aspx" id="mnurptORMOS" runat="server"
							disabled="disabled" visible="false">Outward Remitance O/S Report</a> </li>

					  <li><a href="~/IDPMS/BOE_Pending_PayExt_Report.aspx" id="mnuBOEPenPayExt" runat="server"
							disabled="disabled" visible="false">BOE Pending for Payment Extension</a> </li>

						<li><a href="~/Reports/IDPMSReports/TF_BOE_ACK_LETTER.aspx" id="mnuBOEack" runat="server"
						   disabled="disabled" visible="false">BOE ACKNOWLEDGEMENT LETTER</a></li>

						<li><a href="~/Reports/IDPMSReports/Non_Submission_BOE_Letter.aspx" id="mnuTracerLetter"
						   disabled="disabled" runat="server" visible="false">NON-SUBMISSION OF BOE LETTER</a> </li>

						<li><a href="~/IDPMS/BOE_Pending_PayExt_Done_Report.aspx" id="mnuPayExtDone" runat="server"
							disabled="disabled" visible="false">BOE Payment Extension Done Report</a> </li>

						<li><a href="~/IDPMS/IDPMS_DUMP_AsCustomsData.aspx" id="mnuDmpCust" runat="server"
							disabled="disabled" visible="false">BOE Data As Per DUMP Report</a> </li>

						<li><a href="~/IDPMS/IDPMS_ORM_Closure.aspx" id="mnuOrmClosure" runat="server" disabled="disabled"
							visible="false">ORM Closure Report</a> </li>

						<li><a href="~/IDPMS/IDPMS_BOE_CLOSURE.aspx" id="mnuBoeClosureRep" runat="server"
							disabled="disabled" visible="false">BOE Closure Report</a> </li>

						<li><a href="~/IDPMS/BOE_OtherAD_DataChecklist.aspx" id="mnuOtherAD" runat="server"
							disabled="disabled" visible="false">Bill Of Entry-Other AD Data Checklist</a>
						</li>

						<li><a href="~/IDPMS/TF_IDPMS_ORMACkUpldRpt.aspx" id="mnu_rpt_ORM_Ack_Upload" runat="server"
						   disabled="disabled" visible="false">List Of ORMs RBI Ack. Uploaded to LMCC Server</a>
                        </li>

						<li><a href="~/IDPMS/TF_IDPMS_ORMPendForACKRpt.aspx" id="mnu_rpt_ORM_Ack_NotUpload"
						   disabled="disabled" runat="server" visible="false">List Of ORMs For Which RBI Ack. Not Uploaded to LMCC Server</a>
                        </li>
							<%--IDPMS END Reports --%>
				        <li><a href="~/Reports/EXPReports/rptExportTransStatus.aspx" id="mnuEXPTransStatus"
							runat="server" visible="false">Trade Finance Transactions Data Status</a> </li>
						<!--R-Return reports-->
                        <li><a href="~/Reports/RRETURNReports/rptRRETURN_Datachecklist.aspx?PageHeader=R Return Data CheckList"
							id="mnu_RReturnDataCheckList" runat="server" disabled="disabled" visible="false">Data CheckList</a> </li>
						<li><a href="~/Reports/RRETURNReports/rptRRETURN_Purpose_Control_Totals.aspx?PageHeader=Data Validation"
							id="mnu_RReturnDataValidation" runat="server" disabled="disabled" visible="false">Data Validation</a> </li>
						<li><a href="~/Reports/RRETURNReports/rpt_RRETURN_DataStatistics.aspx?PageHeader=Data Statistics"
							id="mnu_RRETURN_DataStatistics" runat="server" disabled="disabled" visible="false">Data Statistics</a> </li>
						<li><a href="~/Reports/RRETURNReports/rptRRETURN_Purpose_Control_Totals.aspx?PageHeader=R RETURN Cover Page Total"
							id="mnu_RReturnCoverPage" runat="server" disabled="disabled" visible="false">R RETURN Cover Page Total</a> </li>
						<li><a href="~/Reports/RRETURNReports/rptRRETURN_NostroReport.aspx?PageHeader=RRETURN Report to RBI(Nostro)"
							id="mnu_NostroReport" runat="server" disabled="disabled" visible="false">R RETURN Report to RBI(Nostro)</a> </li>
						<li><a href="~/Reports/RRETURNReports/rptRRETURN_VostroReport.aspx?PageHeader=R RETURN Report to RBI(Vostro)"
							id="mnu_RReturnVostroReport" runat="server" disabled="disabled" visible="false">R RETURN Report to RBI(Vostro)</a> </li>
						<li><a href="~/Reports/RRETURNReports/rptRRETURN_Purpose_Control_Totals.aspx?PageHeader=Purpose Code Wise Control Totals"
							id="mnu_RReturnPurposeTotals" runat="server" disabled="disabled" visible="false">Purpose Code Wise Control Totals</a> </li>
						<li><a href="~/Reports/RRETURNReports/rptRRETURN_Schedule.aspx?PageHeader=Schedule 1"
							id="mnu_Schedule1" runat="server" disabled="disabled" visible="false">Schedule 1</a></li>
						<li><a href="~/Reports/RRETURNReports/rptRRETURN_Schedule.aspx?PageHeader=Schedule 2"
							id="mnu_Schedule2" runat="server" disabled="disabled" visible="false">Schedule 2</a></li>
						<li><a href="~/Reports/RRETURNReports/rptRRETURN_Schedule.aspx?PageHeader=Schedule 3/4/5/6"
							id="mnu_Schedule3" runat="server" disabled="disabled" visible="false">Schedule 3/4/5/6</a></li>
						<li><a href="~/Reports/RRETURNReports/rptRRETURN_Schedule.aspx?PageHeader=Supplementary Statement of Purchases"
							id="mnu_NonExport" runat="server" disabled="disabled" visible="false">Supplementary Statement of Purchases</a> </li>
						<li><a href="~/Reports/RRETURNReports/rptRRETURN_NostroReport.aspx?PageHeader=Consolidated R RETURN Report to RBI(Nostro)"
							id="mnu_ConsolRReturnNostroReport" runat="server" disabled="disabled" visible="false">Consolidated R RETURN Report to RBI(Nostro)</a> </li>
						<li><a href="~/Reports/RRETURNReports/rptRRETURN_VostroReport.aspx?PageHeader=Consolidated R RETURN Report to RBI(Vostro)"
							id="mnu_ConsolRreturnVostroReport" runat="server" disabled="disabled" visible="false">Consolidated R RETURN Report to RBI(Vostro)</a> </li>
						<!--------------------------------------------------ImportWarehousing Reports-------------------------------------------------------------------------->
						<li><a href="~/Reports/IMPWHReports/ImportWearh_BillOfEntryOutstanding_Dump.aspx"
							id="mnu_BillOfEntryOutstanding" runat="server" disabled="disabled" visible="false">Bill Of Entry Outstanding Report</a> </li>
						<li><a href="~/Reports/IMPWHReports/IMPWH_GoodToPayAnd_Desc_Report.aspx"
							id="mnu_GoodToPayDiscripancy" runat="server" disabled="disabled" visible="false">Good To Pay & Discripancy Report</a> </li>
						<li><a href="~/Reports/IMPWHReports/TF_IMPWH_UpdationStatusAuidtTrail_Report.aspx"
							id="mnu_GDUpdateAuditTrail" runat="server" disabled="disabled" visible="false">Status Updation Report</a> </li>
						<li><a href="~/Reports/IMPWHReports/TF_IMPWH_ImportBillPaid_Report.aspx"
							id="mnu_ImportBillsPaidReport" runat="server" disabled="disabled" visible="false">Import Bills Paid Report</a> </li>
						<li><a href="~/Reports/IMPWHReports/TF_IMPWH_OverDueImportBillReport.aspx"
							id="mnu_OverdueImportPaidReport" runat="server" disabled="disabled" visible="false">Overdue Import Bills Report</a> </li>
						<!--IMPORT reports-->

						<li>
							<a href="~/IMP/IMPReports/TF_IMP_BillLodg_Interface.aspx" id="mnuLodgment_report_interface" runat="server" disabled="disabled" visible="false">Import Bill Lodgment Checklist</a>
						</li>
						<li>
							<a href="~/IMP/IMPReports/TF_IMP_Acceptance_Bills_LodgReport.aspx" id="mnuAcceptance_Lodg_CheckList" runat="server" disabled="disabled" visible="false">Import Acceptance Bill Checklist</a>
						</li>
						<%-- <li>
							<a href="~/IMP/IMPReports/ImportBillLodgmentReport.aspx" id="mnuMaker_Lodg_CheckList" runat="server" disabled="disabled" visible="false">Import Bill Lodgment Checklist [Maker]</a>
						</li> 
						<li>
							<a href="~/IMP/IMPReports/ImportBillLodgmentCheckerReport.aspx" id="mnuCheker_Lodg_CheckList" runat="server" disabled="disabled" visible="false">Import Bill Lodgement [Approved or Rejected]</a>
						</li> --%>
						<li>
							<a href="~/IMP/IMPReports/TF_IMP_UnsettledDoc.aspx" id="mnu_imp_UnsettledDoc" runat="server" disabled="disabled" visible="false">Unaccepted and Unsettled Documents</a>
						</li>
						<li>
							<a href="~/IMP/IMPReports/TF_IMP_BROPRINTING.aspx" id="mun_imp_broreport" runat="server" disabled="disabled" visible="false">Bank Release Order Letter</a>
						</li>
						<li>
							<a href="~/IMP/IMPReports/BROREGISTERINEXCELFORMAT.aspx" id="mnu_imp_broexcel" runat="server" disabled="disabled" visible="false">BRO Register in Excel Format</a>
						</li>
						<li>
							<a href="~/IMP/IMPReports/ImportTransactionSummary.aspx" id="mnu_Trans_Summary" runat="server" disabled="disabled" visible="false">Import Transactions Summary</a>
						</li>
						<li>
							<a href="~/IMP/IMPReports/TF_IMP_Cash_equivalent_VouchersBalance.aspx" id="mnu_IMP_VouchersBalance" runat="server" disabled="disabled" visible="false">Notes/Cash-equivalent Vouchers Balance</a>
						</li>
						<li>
							<a href="~/IMP/IMPReports/TF_IMP_ExpoImpoDocControlSheetReport.aspx" id="mnu_IMPControlSheetReport" runat="server" disabled="disabled" visible="false">Export Import Document Control Sheet Report</a>
						</li>
						<li>
							<a href="~/IMP/IMPReports/TF_IMP_RecofOutgoingMalisandCourier.aspx" id="mnu_IMPOutgoingMalisandCourier" runat="server" disabled="disabled" visible="false">Record of Outgoing Registered Mails and Courier Services</a>
						</li>

						<li>
							<a href="~/IMP/IMPReports/TF_IMP_AllSwiftFile.aspx" id="mnu_IMP_SwiftGenratedList" runat="server" disabled="disabled" visible="false">Generated swift details</a>
						</li>
					</ul>
				</li>
				<li><a href="#">File Upload</a>
					<ul>
						<li><a href="~/EXP/EXP_FileUpload_CSV.aspx" id="mFU_EXPexcel" runat="server" disabled="disabled"
							visible="false">Excel File Upload - Thomson press</a> </li>
							<%--Export File upload--%>
						<li><a href="~/EDPMS/EDPMS_Upload_INW_CSV_FILE.aspx" id="mnuEDPMS_INW_File_Upload"
							runat="server" disabled="disabled" visible="false">INWARD REMITTANCES FILE UPLOAD</a></li>
						<li><a href="~/EDPMS/EDPMS_OUTSTANDING_BILLS_FILE_UPLOAD.aspx" id="mnuESPMD_OUTSTANDING_File_Upload"
							runat="server" disabled="disabled" visible="false">DATA UPLOAD OF O/S BILLS</a></li>
						<%--Export File upload end--%>
							<%--EDPMS file Upload --%>
							<li><a href="~/EDPMS/EDPMS_File_Upload_ReceiptOfDoc.aspx" id="mFu_EDPMS_Receipt"
							runat="server" disabled="disabled" visible="false">File Upload - Receipt of Document</a>
						</li>
						<li><a href="~/EDPMS/EDPMS_File_Upload.aspx" id="mFu_PaymentRealization" runat="server"
							disabled="disabled" visible="false">File Upload - Payment Realisation</a> </li>
						<li><a href="~/EDPMS/EDPMS_XML_File_Upload_AD_Transfer_Ack.aspx" id="mnu_adtransack"
							runat="server" disabled="disabled" visible="false">XML File Upload - AD Transfer(Acknowledgement)</a>
						</li>
						<li><a href="~/EDPMS/EDPMS_XML_File_Upload_ReciptDoc.aspx" id="mnu_Reciept_Ack" runat="server"
							disabled="disabled" visible="false">XML File Upload - Receipt of Document(Acknowledgement)</a>
						</li>
						<li><a href="~/EDPMS/EDPMS_XML_Fileupload_Paymant_Ack.aspx" id="mnu_Realization_Ack"
							runat="server" disabled="disabled" visible="false">XML File Upload - Payment Realized Document(Acknowledgement)</a> </li>
						<li><a href="~/EDPMS/EDPMS_Dump_Upload.aspx" id="mnu_dump" runat="server" disabled="disabled"
							visible="false">XML File upload-EDPMS DUMP</a> </li>
						<li><a href="~/EDPMS/PaymentExtnAck.aspx" id="mnu_pmtextnack" runat="server" disabled="disabled"
							visible="false">XML File upload-Payment Extension Acknowledgement</a> </li>
						<%--EFIRC Closure File Upload EDPMS--%>
						<li><a href="~/EDPMS/EDPMS_E_FIRC_Closure_Upload.aspx" id="mnu_EFIRC_Closure_File_Upload" runat="server" disabled="disabled"
							visible="false">Excel File Upload - E FIRC Closure</a> </li>
						<li><a href="~/EDPMS/EDPMS_E_FIRC_Old_Closure_Upload.aspx" id="mnu_EFIRC_Old_Closure_File_Upload" runat="server" disabled="disabled"
							visible="false">Excel File Upload - E FIRC (Old) Closure</a> </li>
						<%--ADDED BY NILESH--%>
						<li><a href="~/EDPMS/TF_EBRC_ORM_FileUpload.aspx" id="mnu_ebrcORMFileUpload" runat="server"
							visible="false" disabled="disabled">E-BRC ORM File Upload For DGFT</a> </li>
						<li>
							<a href="~/EDPMS/TF_EBRC_ORM_APISTATUS_Fileupload.aspx" id="mnuapistatusorm" runat="server" disabled="disabled"
							visible="false">DGFT Status File Upload For ORM</a>
						</li>
							<%--EDPMS File Upload End --%>
						<%--File Upload IDPMS-----------------------------------------------%>

						<li><a href="~/IDPMS/IDPMS_OTT_FILE_UPLOAD.aspx" id="mnu_ORMFileupload" runat="server"
						   disabled="disabled" visible="false">Excel File Upload - Outward Remittance</a> </li>

						<li><a href="~/IDPMS/TF_IDPMS_Dump_Upload.aspx" id="mnu_idpms_dump" runat="server"
							disabled="disabled" visible="false">XML File upload - IDPMS DUMP</a> </li>

						<%--New File Upload IDPMS--%>
						<li><a href="~/IDPMS/TF_IDPMS_Settlement_FILE_UPLOAD.aspx" id="mnu_BOE_settlement_Upl"
							runat="server" disabled="disabled" visible="false">Excel File Upload - BOE Settlement</a>
						</li>

						<li><a href="~/IDPMS/IDPMS_Ohter_AD_Data_File_Upload.aspx" id="mnu_Other_AD_Upl"
							runat="server" disabled="disabled" visible="false">Excel File Upload - Other AD</a>
						</li>

						<li><a href="~/IDPMS/TF_IDPMS_ManualFileUpload.aspx" id="mnu_Manual_BOE_Upl" runat="server"
							disabled="disabled" visible="false">Excel File Upload - Manual BOE</a> </li>

						<li><a href="~/IDPMS/IDPMS_EXTN_UPLOAD.aspx" id="mnu_Pay_Ext_Upl" runat="server"
							disabled="disabled" visible="false">Excel File Upload - Payment Extension</a>
						</li>

						<li><a href="~/IDPMS/IDPMS_Out_Rem_Acknolgment.aspx" id="mnu_ORMAck" runat="server" disabled="disabled"
							visible="false">XML File Upload - ORM (Acknowledgement)</a> </li>

						<li><a href="~/IDPMS/TF_IDPMS_Payment_Settlement_RBI_Ack.aspx" id="mnu_PaySet" runat="server" disabled="disabled"
							visible="false">XML File Upload - Payment Settlement (RBI-Acknowledgement)</a></li>

						<li><a href="~/IDPMS/IDPMS_ORM_Closure_Upload.aspx" id="mnu_ORMClosureUpload" runat="server" disabled="disabled"
							visible="false">Excel File Upload - ORM Closure</a> </li>

						<li><a href="~/IDPMS/TF_IDPMS_BOE_Closure_FILE_UPLOAD.aspx" id="mnu_BOEClosureUpload" runat="server" disabled="disabled"
							visible="false">Excel File Upload - BOE Closure</a> </li>

						<!--RRETURN file upload-->
						<li><a href="~/RRETURN/Transaction_FileUpload_New.aspx" id="mFU_RET_CSV" runat="server"
							disabled="disabled" visible="false">Excel Input Data File Upload at Branch</a>
						</li>
						<li><a href="~/RRETURN/Ret_Consolidate_CSV_Data.aspx" id="mnu_ConsolidateCSV" runat="server"
							visible="false" disabled="disabled">Consolidate Branch CSV File At Head Office</a>
						</li>
						<li><a href="~/RRETURN/Transaction_FileUpload.aspx" id="mnu_transfileupload" runat="server"
							visible="false" disabled="disabled">Transaction File Upload</a> </li>

						<!--------------------------------------------Import Warehousing file upload---------------------------------------------------->
						<li><a href="~/ImportWareHousing/FIleUpload/TF_IMPWH_GDPFileUpload.aspx" id="mnu_GDPExcelFileUpload" runat="server"
							visible="false" disabled="disabled">Good To Pay Authorization File Upload</a> </li>
						<li><a href="~/ImportWareHousing/FIleUpload/TF_IMPWH_PaymentFileUpload.aspx" id="mnu_PaymentFileUpload" runat="server"
							visible="false" disabled="disabled">Payment Authorization File Upload</a> </li>
						<li><a href="~/TestEmailPage.aspx?mode=Add" id="mnutestemailpage" runat="server"
							visible="false" disabled="disabled">Mail Sending</a> </li>
						<!--------------------------------------------Import Automation file upload---------------------------------------------------->
						<li><a href="~/IMP/FileUpload/TF_IMP_HolidayMasterFileUpload.aspx" id="mnu_HolidayFileUpload" runat="server"
							visible="false" disabled="disabled">Holiday Master File Upload</a> </li>
						<!--------------------------------------------Ebrc File Upload ---------------------------------------------------->
						<li><a href="~/EBR/TF_EBRC_ITTEUCFileUpload.aspx" id="mnu_ITTEUCFileUpload" runat="server"
							visible="false" disabled="disabled">ITT EUC File Upload</a> </li>
						<li>
							<a href="~/EBR/TF_EBRC_ITTEUC_APISTATUSUPLOAD.aspx" id="mnuapistatusIRMITT" runat="server" disabled="disabled"
							visible="false">DGFT Status File Upload For IRM/ITT</a>
						</li>
					</ul>
				</li>
				<li><a href="#">File Format</a>
					<ul>
					   <%--Export module --%>
						<li><a id="mnu_InwFileFormat" href="~/FileFormat.aspx" runat="server" disabled="disabled" visible="false">Inward File Format</a></li>
						<%--Export module end --%>
						<!--IDPMS File Format-->
						<li><a href="~/File_Format/ORM_File_Format.xlsx" id="mnu_ORMFORMAT" runat="server"
							disabled="disabled" visible="false">ORM File Upload Format</a></li>

						<li><a href="~/File_Format/Payment_Settlemnt_File_Format.xlsx" id="mnu_BOE_settlement_Upl_Format"
							runat="server" disabled="disabled" visible="false">BOE Settlement File Upload Format</a>
						</li>

						<li><a href="~/File_Format/Other_AD_File_Format.xlsx" id="mnu_Other_AD_Upl_Format"
							runat="server" disabled="disabled" visible="false">Other AD File Upload Format</a>
						</li>

						<li><a href="~/File_Format/Manul_BOE_File_Format.xlsx" id="mnu_Manual_BOE_Upl_Format"
							runat="server" disabled="disabled" visible="false">Manual BOE File Upload Format</a>
						</li>

						<li><a href="~/File_Format/Payment_Extension_File_Format.xlsx" id="mnu_Pay_Ext_Upl_Format"
							runat="server" disabled="disabled" visible="false">Payment Extension File Upload Format</a>
						</li>
						
						<li><a href="~/File_Format/ORM_Closure_Format.xlsx" id="mnu_ORM_Clr_Format"
							runat="server" disabled="disabled" visible="false">ORM Closure File Upload Format</a>
						</li>
						
						<li><a href="~/File_Format/BOE_Closure_Format.xlsx" id="mnu_BOE_Clr_Format" runat="server"
							visible="false" disabled="disabled">BOE Closure File Upload Format</a>
						</li>
						<%--EDPMS File format --%>
						<li><a href="~/File_Format/E_FIRC_closure.xlsx" id="mnu_E_FIRC_Clr_Format" runat="server"
							visible="false" disabled="disabled">E FIRC Closure File Upload Format</a>
						</li>
						<!--RRETURN-->
						<li><a href="~/File_Format/RReturnUploadFormat.xlsx" id="mnu_uploadformt" runat="server"
							visible="false" disabled="disabled">Excel Input Data File Upload Format</a>
						</li>
						<!--IMPORT-->
						<li><a href="~/File_Format/HolidayMasterFormat.xlsx" id="mnu_HolidayFileFormat" runat="server"
							visible="false" disabled="disabled">Holiday Master File Upload Format</a>
						</li>
						<li><a href="~/File_Format/RMA_Format.csv" id="mnu_RMAFileFormat" runat="server"
							visible="false" disabled="disabled">RMA Master File Upload Format</a>
						</li>
					</ul>
				</li>
				<li><a href="#">Audit Trail</a>
					<ul>
                        <li><a href="../Reports/EXPReports/EXP_Swiftaudittrail.aspx"
							id="mnuExpswift" runat="server" disabled="disabled" visible="false">Swift Audit Trail</a>
                        </li>
                        <li><a href="../Reports/EXPReports/EXP_Transactionaudittrail.aspx"
							id="mnuTransactionaudittrail" runat="server" disabled="disabled" visible="false">Export Transaction Audit Trail</a>
                        </li>
						<li><a href="~/Reports/TF_AuditTrail.aspx?PageHeader=Audit Trail&ModuleType=FWD"
							id="mnuFwdAuditTrail" runat="server" disabled="disabled" visible="false">Audit Trail</a>
						</li>
						<li><a href="~/Reports/TF_AuditTrail.aspx?PageHeader=Audit Trail&ModuleType=PCFC"
							id="mnuPCFCAuditTrail" runat="server" disabled="disabled" visible="false">Audit
							Trail</a> </li>
						<li><a href="~/Reports/TF_AuditTrail.aspx?PageHeader=Audit Trail&ModuleType=INW"
							id="mnuINWAuditTrail" runat="server" disabled="disabled" visible="false">Audit Trail</a>
						</li>
						<li><a href="~/Reports/TF_AuditTrail.aspx?PageHeader=Audit Trail&ModuleType=OUT"
							id="mnuOUTAuditTrail" runat="server" disabled="disabled" visible="false">Audit Trail</a>
						</li>
						<li><a href="~/Reports/TF_AuditTrail.aspx?PageHeader=Audit Trail&ModuleType=IMP"
							id="mnuIMPAuditTrail" runat="server" disabled="disabled" visible="false">Audit Trail</a>
						</li>
						<%--Export Module audit --%>
						<li><a href="~/Reports/TF_AuditTrail.aspx?PageHeader=Audit Trail&ModuleType=EXP"
							id="mnuEXPAuditTrail" runat="server" disabled="disabled" visible="false">Audit Trail</a>
						</li>
						 <%--Export Module audit End --%>
						 <%--EDPMS Audit --%>
						<li><a href="~/Reports/TF_AuditTrail.aspx?PageHeader=Audit Trail&ModuleType=EDPMS"
							id="mnuEDPMSAuditTrail" runat="server" disabled="disabled" visible="false">Audit Trail</a>
						 </li>
							<%--EDPMS Audit End --%>
							<%--IDPMS Audit  --%>
						<li><a href="~/Reports/TF_AuditTrail.aspx?PageHeader=Audit Trail&ModuleType=IDPMS"
							id="mnuIDPMSAuditTrail" runat="server" disabled="disabled" visible="false">Audit Trail</a> </li>
							<%--IDPMS Audit  End --%>
						<li><a href="~/Reports/IMPWHReports/TF_IMPWH_FileCreationAudittRAIL.aspx"
							id="mnuFileCreationAuditTriail" runat="server" disabled="disabled" visible="false">File Creation Audit Trail</a>
						</li>
						<li><a href="~/Reports/IMPWHReports/TF_IMPWH_FileUpload_AuditTrail.aspx"
							id="mnuFileUploadAuditTriail" runat="server" disabled="disabled" visible="false">File Upload Audit
							Trail</a> </li>
						<li><a href="~/Reports/IMPWHReports/TF_IMPWH_Master_AuditTrail.aspx"
							id="mnuMasterAuditTriail" runat="server" disabled="disabled" visible="false">Masters Audit
							Trail</a> </li>

						<!--IMPORT-->
						<li>
							<a href="../IMP/IMPReports/TF_AuditrialMaster.aspx"
								id="mnu_impMasterAuditTriail" runat="server" disabled="disabled" visible="false">Audit trail For Master</a>
						</li>
						<li>
							<a href="~/IMP/IMPReports/TF_AuditrailFileUpload.aspx"
								id="mnu_impUploadMasterAuditTriail" runat="server" disabled="disabled" visible="false">File Upload Audit trail Master</a>
						</li>
						<li>
							<a href="~/IMP/IMPReports/TF_IMP_Audit_trial_Transaction.aspx" id="mnu_impuploadTransactionAudittrial" runat="server" visible="false">Audit trail For Transaction</a>
						</li>
					</ul>
				</li>
				<li><a href="#">Admin</a>
					<ul>
						<li><a id="mHk_HouseKeeping" runat="server" onclick="CallHouseKeeping()">House Keeping</a> </li>
						<li><a id="mnu_Expuserlist" runat="server" href="~/TF_ExpUserList.aspx" disabled="disabled" visible="false">Download User List</a></li>
						 <li><a href="~/View_UserDetails_Admin.aspx?SecurityHeader=User Activity Dashboard" id="mnu_UserActivityDetails" runat="server" disabled="disabled" visible="false">User Activity Dashboard</a></li> 
						 <%-- <li><a id="mnu_UserActivityDashBoard" runat="server" href="~/UserActivityDashBoard.aspx" disabled="disabled" visible="false">User Activity DashBoard</a></li> --%>
						<%--Edpms module admin --%>
						<li><a id="mnu_DeleteEdpmsRecords" href="~/TF_DeleteErrorRecords.aspx" runat="server"
							disabled="disabled" visible="false">Deletion of EDPMS Error Records</a> </li>
							<%--Edpms module admin --%>
						<li><a id="mHK_AccessControl" runat="server" disabled="disabled" href="~/TF_AccessControl.aspx" visible="false">Access Control</a> </li>
                        <%--RReturn module admin --%>
                        <li><a id="mnu_Error" runat="server" disabled="disabled" href="~/TF_RET_ErrorLog_TxtFileCreation.aspx" visible="false">Error Log</a> </li>
						<%-- import module admin --%>
						<li><a id="mnu_trans_Log" href="~/IMP/TF_imp_Transactions_LogDetails.aspx" runat="server"  disabled="disabled" visible="false">Transactions Log Details</a> </li>
						<li><a id="mnu_ManualLCfLagUPDate" runat="server" disabled="disabled" href="~/IMP/TF_IMP_ManualLCFlagUpdation.aspx" visible="false">Manual LC Flag Updation</a> </li>
						<li><a id="mnu_IMP_RollBackLodgement" runat="server" disabled="disabled" href="~/IMP/TF_IMP_RollBackLodgement.aspx" visible="false">Logdement Rollback</a> </li>
						<li><a id="mnu_ImpReportEntity" runat="server" disabled="disabled" href="~/IMP/TF_IMP_Report_Entities.aspx" visible="false">I-6 Report Entities</a> </li>
                        <li><a id="mnu_LEIEmailDetails" href="~/IMP/TF_ViewLEIEmaildetails.aspx" runat="server"  disabled="disabled" visible="false">LEI Email Details</a> </li>
						<li><a href="~/EXP/TF_EXPGBASE_LOG_Reports.aspx" id="mnuTFEXPGBASELOG" runat="server" disabled="disabled" visible="false">Gbase Log</a> </li>
                        <%-- import module admin end--%>
					</ul>
				</li>
				<li><a href="~/TF_Log_Out.aspx" id="LogOut" runat="server">Logout</a> </li>
			</ul>
		</nav>
    </div>
    <asp:Button ID="btnHousekeeping" Style="display: none;" runat="server" OnClick="btnHousekeeping_Click" />
</body>
</html>
