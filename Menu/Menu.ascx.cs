using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Menu_Menu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {

            Response.Redirect("TF_Login.aspx?sessionout=yes&sessionid=", true);
        }
        if (!IsPostBack)
        {
            DateTime nowDate = System.DateTime.Now;
            if (Session["LoggedUserId"] != null)
            {
                lblUserName.Text = "Welcome, " + Session["userName"].ToString().Trim();
                lblRole.Text = "| Role: " + Session["userRole"].ToString().Trim();
                lblTime.Text = nowDate.ToLongDateString();
                hdnloginid.Value = Session["LoggedUserId"].ToString();
                if (Session["ModuleID"] != null)
                {
                    hdnModuleID.Value = Session["ModuleID"].ToString();

                }
            }
        }
        #region STP Module
        if (hdnModuleID.Value == "STP")
        {
            lblModuleName.Text = "Swift STP Module";
        }
        #endregion

        #region Import Module
        if (hdnModuleID.Value == "IMP")
        {
            lblModuleName.Text = "Import Module";



            //--------------Masters------------------//
            mnuClosingBalMaster.Disabled = false;
            mnuClosingBalMaster.Visible = true;

            mnuCustomerMaster.Disabled = false;
            mnuCustomerMaster.Visible = true;

            mnuServiceTaxMaster.Disabled = false;
            mnuServiceTaxMaster.Visible = true;

            mnuPurposeCodeMaster.Disabled = false;
            mnuPurposeCodeMaster.Visible = true;

            mnuPortCodeMaster.Disabled = false;
            mnuPortCodeMaster.Visible = true;

            mnuCountryMaster.Disabled = false;
            mnuCountryMaster.Visible = true;

            mnuGbaseCommMaster.Disabled = false;
            mnuGbaseCommMaster.Visible = true;

            mnuGLCodeMaster.Visible = true;
            mnuGLCodeMaster.Disabled = false;

            mnuOverseasBankMaster.Disabled = false;
            mnuOverseasBankMaster.Visible = true;

            mnuReimbursingBankMaster.Disabled = false;
            mnuReimbursingBankMaster.Visible = true;

            mnu_IMP_NOSTRO_Master_View.Disabled = false;
            mnu_IMP_NOSTRO_Master_View.Visible = true;

            mnuHolidayMaster.Disabled = false;
            mnuHolidayMaster.Visible = true;

            mnuCommisionMaster.Disabled = false;
            mnuCommisionMaster.Visible = true;

            mnuCurrencyMaster.Disabled = false;
            mnuCurrencyMaster.Visible = true;

            mnuEXP_SanctionedCountry.Disabled = false;
            mnuEXP_SanctionedCountry.Visible = true;

            mnuStampDutyMaster.Disabled = false;
            mnuStampDutyMaster.Visible = true;

            mnu_Discrepency.Disabled = false;
            mnu_Discrepency.Visible = true;

            mnu_Imp_DrawerMaster.Disabled = false;
            mnu_Imp_DrawerMaster.Visible = true;

            mnu_RMAMaster.Disabled = false;
            mnu_RMAMaster.Visible = true;

            mnuSundryAccMaster.Disabled = false;
            mnuSundryAccMaster.Visible = true;

            mnuLEIEODReport.Disabled = false;
            mnuLEIEODReport.Visible = true;

            mnuLEIThresholdMaster.Disabled = false;
            mnuLEIThresholdMaster.Visible = true;

            /*  transaction

            mnu_IMP_BOE_Maker_View.Disabled = true;
            mnu_IMP_BOE_Maker_View.Visible = true;

            mnu_IMP_BOE_Checker_View.Disabled = true;
            mnu_IMP_BOE_Checker_View.Visible = true;

            mnu_IMP_Booking_Of_IBD_ACC_Maker.Disabled = true;
            mnu_IMP_Booking_Of_IBD_ACC_Maker.Visible = true;

            mnu_IMP_Booking_Of_IBD_ACC_Checker.Disabled = true;
            mnu_IMP_Booking_Of_IBD_ACC_Checker.Visible = true;

            mnu_IMP_Settlement_Maker.Disabled = true;
            mnu_IMP_Settlement_Maker.Visible = true;

            mnu_IMP_Settlement_Checker.Disabled = true;
            mnu_IMP_Settlement_Checker.Visible = true;

            mnu_IMP_LC_DESCOUNTING_ACC_IBD_Maker.Visible = true;
            mnu_IMP_LC_DESCOUNTING_ACC_IBD_Maker.Disabled = true;

            mnu_IMP_LC_DESCOUNTING_ACC_IBD_Checker.Visible = true;
            mnu_IMP_LC_DESCOUNTING_ACC_IBD_Checker.Disabled = true;

            mnu_BankReleaseOrder.Disabled = true;
            mnu_BankReleaseOrder.Visible = true;

            mnu_BankReleaseOrder_checker_view.Disabled = true;
            mnu_BankReleaseOrder_checker_view.Visible = true;

            mnu_BROGO_Maker_View.Disabled = true;
            mnu_BROGO_Maker_View.Visible = true;

            mnu_BROGO_Checker_View.Disabled = true;
            mnu_BROGO_Checker_View.Visible = true;

            mnu_LC_DESCOUNTING_Settlement_Maker.Disabled = true;
            mnu_LC_DESCOUNTING_Settlement_Maker.Visible = true;

            mnu_LC_DESCOUNTING_Settlement_Checker.Disabled = true;
            mnu_LC_DESCOUNTING_Settlement_Checker.Visible = true;

            //// report
            mnuMaker_Lodg_CheckList.Disabled = true;
            mnuMaker_Lodg_CheckList.Visible = true;

            mnuCheker_Lodg_CheckList.Disabled = true;
            mnuCheker_Lodg_CheckList.Visible = true;

            mun_imp_broreport.Disabled = true;
            mun_imp_broreport.Visible = true;

            mnu_imp_broexcel.Disabled = true;
            mnu_imp_broexcel.Visible = true;

            mnu_imp_UnsettledDoc.Disabled = true;
            mnu_imp_UnsettledDoc.Visible = true;

            mnu_lodgmentmaker.Disabled = true;
            mnu_lodgmentmaker.Visible = true;

            mnu_AcceptanceApproveFile_XL.Disabled = true;
            mnu_AcceptanceApproveFile_XL.Visible = true;

            mnu_SettlementFileCreation_XL.Disabled = true;
            mnu_SettlementFileCreation_XL.Visible = true;

            mnu_Trans_Summary.Disabled = true;
            mnu_Trans_Summary.Visible = true;

            mnuAcceptance_Lodg_CheckList.Disabled = true;
            mnuAcceptance_Lodg_CheckList.Visible = true;

            mnuLodgment_report_interface.Disabled = true;
            mnuLodgment_report_interface.Visible = true;

            mnuLodgmentExcelrpt.Disabled = true;
            mnuLodgmentExcelrpt.Visible = true;

            mnuAcceptanceExcelrpt.Disabled = true;
            mnuAcceptanceExcelrpt.Visible = true;
            //// File Upload
            mnu_HolidayFileUpload.Disabled = true;
            mnu_HolidayFileUpload.Visible = true;

            //// File Format
            mnu_HolidayFileFormat.Disabled = true;
            mnu_HolidayFileFormat.Visible = true;

            mnu_RMAFileFormat.Disabled = true;
            mnu_RMAFileFormat.Visible = true;

            //// vinay audit traill
            mnu_impMasterAuditTriail.Disabled = true;
            mnu_impMasterAuditTriail.Visible = true;
            */

            
        }
        #endregion

        #region Export Module
        if (hdnModuleID.Value == "EXP")
        {
            lblModuleName.Text = "Export Module";

            //--------------Masters--------------//
            mnuCustomerMaster.Disabled = false;
            mnuCustomerMaster.Visible = true;
            mnuPortCodeMaster.Disabled = false;
            mnuPortCodeMaster.Visible = true;
            mnuSpecialDates.Disabled = false;
            mnuSpecialDates.Visible = true;
            mnuOveseasPartyMaster.Disabled = false;
            mnuOveseasPartyMaster.Visible = true;
            mnuOverseasBankMaster.Disabled = false;
            mnuOverseasBankMaster.Visible = true;

            mnuReimbursingBankMaster.Disabled = false;
            mnuReimbursingBankMaster.Visible = true;


            mnu_CommissionMaster.Disabled = false;
            mnu_CommissionMaster.Visible = true;
            mnu_Courier_Charges.Disabled = false;
            mnu_Courier_Charges.Visible = true;
            mnuCurrencyMaster.Disabled = false;
            mnuCurrencyMaster.Visible = true;
            mnuPurposeCodeMaster.Disabled = false;
            mnuPurposeCodeMaster.Visible = true;

            mnu_RecvBankMaster.Visible = true;
            mnu_RecvBankMaster.Disabled = false;

            mnuServiceTaxMaster.Visible = true;
            mnuServiceTaxMaster.Disabled = false;

            mnuServiceTaxFXDLS.Visible = true;
            mnuServiceTaxFXDLS.Disabled = false;

            mnuEXP_SanctionedCountry.Visible = true;
            mnuEXP_SanctionedCountry.Disabled = false;

            mnuCommodityMaster.Visible = true;
            mnuCommodityMaster.Disabled = false;

            mnuCountryMaster.Visible = true;
            mnuCountryMaster.Disabled = false;

            mnu_profliue.Visible = true;
            mnu_profliue.Disabled = false;

            mnu_adjirm.Visible = true;
            mnu_adjirm.Disabled = false;

            mnuGbaseCommMaster.Disabled = false;
            mnuGbaseCommMaster.Visible = true;

            mnuExpswift.Disabled = false;
            mnuExpswift.Visible = true;

            mnuTransactionaudittrail.Disabled = false;// Anand 09-01-2024
            mnuTransactionaudittrail.Visible = true;// Anand 09-01-2024

            mnuLEIEODReport.Disabled = false;
            mnuLEIEODReport.Visible = true;

            mnuLEIThresholdMaster.Disabled = false;
            mnuLEIThresholdMaster.Visible = true;

            mnuConsigneePartyMaster.Visible = true;
            mnuConsigneePartyMaster.Disabled = false;

            mnuexpsundrymaster.Visible = true; // ADDED BY NILESH 05-08-2023
            mnuexpsundrymaster.Disabled = false;

            mnuTFEXPGBASELOG.Disabled = false;
            mnuTFEXPGBASELOG.Visible = true;
            //--------------Transction------------//
            //mnu_EXPbillOfEntry.Visible = true;
            //mnu_EXPbillOfEntry.Disabled = false;

            //mnu_RealisationDataEntry.Visible = true;
            //mnu_RealisationDataEntry.Disabled = false;

            //mnu_EXPdueDate.Visible = true;
            //mnu_EXPdueDate.Disabled = false;

            //mnuInwDataEntry.Visible = true;
            //mnuInwDataEntry.Disabled = false;

            //mnuEXP_MerchantTrade.Visible = true;
            //mnuEXP_MerchantTrade.Disabled = false;

            //mnu_EXP_swift.Visible = true;
            //mnu_EXP_swift.Disabled = false;

            ////-------------FILE CREATION-----------//

            //MnuprblmExpBill.Visible = true;
            //MnuprblmExpBill.Disabled = false;

            //mnu_Export_Bill_Receipt_CSVFileCreation.Visible = true;
            //mnu_Export_Bill_Receipt_CSVFileCreation.Disabled = false;

            //mnuRealisationCSV.Visible = true;
            //mnuRealisationCSV.Disabled = false;

            //mnuManualGRCSV.Visible = true;
            //mnuManualGRCSV.Disabled = false;

            //mnuRemittanceAdvance.Visible = true;
            //mnuRemittanceAdvance.Disabled = false;

            //generateReturnData.Visible = true;
            //generateReturnData.Disabled = false;

            //generateGBaseData.Visible = true;
            //generateGBaseData.Disabled = false;

            ////-------------FILE Upload-----------//
            //mnuEDPMS_INW_File_Upload.Visible = true;
            //mnuEDPMS_INW_File_Upload.Disabled = false;

            //mnuESPMD_OUTSTANDING_File_Upload.Visible = true;
            //mnuESPMD_OUTSTANDING_File_Upload.Disabled = false;

            ////File Format
            //mnu_InwFileFormat.Visible = true;
            //mnu_InwFileFormat.Disabled = false;

            ////-------------Reports-----------------//

            //mRep_InwRemReg.Visible = true;
            //mRep_InwRemReg.Disabled = false;

            //mnuEXPBillReg.Visible = true;
            //mnuEXPBillReg.Disabled = false;

            //mnuEXPBillReg_Unaccepted.Visible = true;
            //mnuEXPBillReg_Unaccepted.Disabled = false;

            //mnuEXPBillIntimetion.Visible = true;
            //mnuEXPBillIntimetion.Disabled = false;

            //mnuEXPBillDocument.Visible = true;
            //mnuEXPBillDocument.Disabled = false;

            //mnuEXPBillRealisedDocument.Visible = true;
            //mnuEXPBillRealisedDocument.Disabled = false;

            //mnuExportOverDueStatement.Visible = true;
            //mnuExportOverDueStatement.Disabled = false;

            //mnu_EXBillOut.Visible = true;
            //mnu_EXBillOut.Disabled = false;

            //mnuEXPRealisationReport.Visible = true;
            //mnuEXPRealisationReport.Disabled = false;

            //mnuListofExpDocRealised.Visible = true;
            //mnuListofExpDocRealised.Disabled = false;

            //mnuListofExpDocDispatched.Visible = true;
            //mnuListofExpDocDispatched.Disabled = false;

            //mnuadvanceexp.Visible = true;
            //mnuadvanceexp.Disabled = false;

            //mnunonsubmissionsdoc.Visible = true;
            //mnunonsubmissionsdoc.Disabled = false;

            //mnuCautionAdviceEXPBill.Visible = true;
            //mnuCautionAdviceEXPBill.Disabled = false;

            //mnuADTransfer.Visible = true;
            //mnuADTransfer.Disabled = false;

            //mnuBilldueDate.Visible = true;
            //mnuBilldueDate.Disabled = false;

            //mnuEXP_MerchantTradeReport.Visible = true;
            //mnuEXP_MerchantTradeReport.Disabled = false;

            //mnuAdvanceRemittance.Visible = true;
            //mnuAdvanceRemittance.Disabled = false;

            //mnuEXP_SDFstatement.Visible = true;
            //mnuEXP_SDFstatement.Disabled = false;

            //mnuexportBillsDue.Visible = true;
            //mnuexportBillsDue.Disabled = false;

            //mnuexportDataStatus.Visible = true;
            //mnuexportDataStatus.Disabled = false;

        }
        #endregion

        #region XOS Module

        if (hdnModuleID.Value == "XOS")
        {
            lblModuleName.Text = "XOS Module";
            // Masters //
            mnuCommodityMaster.Visible = true;
            mnuCommodityMaster.Disabled = false;

            mnuCurrencyCardRate.Visible = true;
            mnuCurrencyCardRate.Disabled = false;

            mnuCustomerMaster.Visible = true;
            mnuCustomerMaster.Disabled = false;

            mnuCurrencyRate.Visible = true;
            mnuCurrencyRate.Disabled = false;

            // Transaction //
            mnu_XOSExtension.Visible = true;
            mnu_XOSExtension.Disabled = false;
            mnu_XOSWriteOff.Visible = true;
            mnu_XOSWriteOff.Disabled = false;
            mnu_UpdateGR.Visible = true;
            mnu_UpdateGR.Disabled = false;

            // File creation

            mnuEXPXO.Visible = true;
            mnuEXPXO.Disabled = false;
            mnu_NilXOS.Visible = true;
            mnu_NilXOS.Disabled = false;

            // Reports //


            mnuExpXOXDATAVaild.Visible = true;
            mnuExpXOXDATAVaild.Disabled = false;
            mnu_EBWRegister.Visible = true;
            mnu_EBWRegister.Disabled = false;

            mnuETXRegister.Visible = true;
            mnuETXRegister.Disabled = false;
            mnu_XOSReport.Visible = true;
            mnu_XOSReport.Disabled = false;
            mnuXOS_Output_Upload.Visible = true;
            mnuXOS_Output_Upload.Disabled = false;
            mnuXOS_Summary.Visible = true;
            mnuXOS_Summary.Disabled = false;
            mnu_XOSReport_Party_Wise.Visible = true;
            mnu_XOSReport_Party_Wise.Disabled = false;

            mnu_XOSReport_Type_Wise.Visible = true;
            mnu_XOSReport_Type_Wise.Disabled = false;


        }

        #endregion

        #region EBRC Module
        if (hdnModuleID.Value == "EBR")
        {
            lblModuleName.Text = "EBRC Module";
            // Masters //

            mnuCustomerMaster.Visible = true;
            mnuCustomerMaster.Disabled = false;
            mnuPortCodeMaster.Visible = true;
            mnuPortCodeMaster.Disabled = false;

            //// Transaction //

            //mnu_EBRCancellationDataEntry.Visible = true;
            //mnu_EBRCancellationDataEntry.Disabled = false;

            //mnu_EXPRealisation.Visible = true;
            //mnu_EXPRealisation.Disabled = false;
            //// File creation

            //mnu_Ebrc_Generate_TradeData.Visible = true;
            //mnu_Ebrc_Generate_TradeData.Disabled = false;
            //mnu_Ebrc_Generate_XML.Visible = true;
            //mnu_Ebrc_Generate_XML.Disabled = false;

            //// Reports //
            //mnu_DataCheckList.Visible = true;
            //mnu_DataCheckList.Disabled = false;

            //mnu_DataValidation.Visible = true;
            //mnu_DataValidation.Disabled = false;

            //mnu_EBRCertGenerated.Visible = true;
            //mnu_EBRCertGenerated.Disabled = false;

            //mnu_EBRCert_TobeGenerate.Visible = true;
            //mnu_EBRCert_TobeGenerate.Disabled = false;

            //mnu_BRC_Cancelled.Visible = true;
            //mnu_BRC_Cancelled.Disabled = false;

            //mnu_WithoutGRDetails.Visible = true;
            //mnu_WithoutGRDetails.Disabled = false;

            //mnuebrcintimation.Visible = true;
            //mnuebrcintimation.Disabled = false;



        }
        #endregion

        #region EDPMS Module
        if (hdnModuleID.Value == "EDPMS")
        {
            lblModuleName.Text = "EDPMS Module";

            //----Masters-----------------------------------------------------------------
            mnu_ErrorCode.Visible = true;
            mnu_ErrorCode.Disabled = false;

            //------Admin Acess to all users----------------------------------
            mnu_DeleteEdpmsRecords.Visible = true;
            mnu_DeleteEdpmsRecords.Disabled = false;



            ////----Trasaction-----------------
            // ADDED BY NILESH
            mnu_EBRORMMaker.Visible = true;
            mnu_EBRORMChecker.Visible = true;
            mnu_EBRORMMaker.Disabled = false;
            mnu_EBRORMChecker.Disabled = false;

            mnu_ebrcORMFileUpload.Visible = true;
            mnu_ebrcORMFileUpload.Disabled = false;
            //mnu_payextn.Visible = true;
            //mnu_payextn.Disabled = false;


            //mnuEDPMSData.Visible = true;
            //mnuEDPMSData.Disabled = false;

            //mnuEDPMS_BillDetails.Visible = true;
            //mnuEDPMS_BillDetails.Disabled = false;

            //mnu_EDPMSDataUpdation.Visible = true;
            //mnu_EDPMSDataUpdation.Disabled = false;

            //mnu_adj.Visible = true;
            //mnu_adj.Disabled = false;

            //mnu_EDPMS_EFirc.Visible = true;
            //mnu_E_Firc.Visible = true;

            ////----File Creation-----------------

            //mnu_inwxml.Visible = true;
            //mnu_inwxml.Disabled = false;


            //mnu_closurexml.Visible = true;
            //mnu_closurexml.Disabled = false;

            //mnu_EDPMS_XML_Receipt.Disabled = false;
            //mnu_EDPMS_XML_Receipt.Visible = true;

            //mnu_EDPMS_XML_Realization.Disabled = false;
            //mnu_EDPMS_XML_Realization.Visible = true;

            //mnu_EDPMS_AD_Transfer.Visible = true;
            //mnu_EDPMS_AD_Transfer.Disabled = false;

            //mnu_pmtextfilecrn.Visible = true;
            //mnu_pmtextfilecrn.Disabled = false;

            //mnuE_Firc_Closure_Xml.Visible = true;
            //mnuE_Firc_Closure_Xml.Disabled = false;


            //mnuDataTransfer.Visible = true;
            //mnuDataTransfer.Disabled = false;

            //mnu_roddataadtrans.Visible = true;
            //mnu_roddataadtrans.Disabled = false;


            //mnu_AckReal.Visible = true;
            //mnu_AckReal.Disabled = false;

            ////---Reports------------------------------

            //mnu_InwRem_Utilization.Visible = true;
            //mnu_InwRem_Utilization.Disabled = false;

            //mnuExpbillPendingsAckEDPMS.Visible = true;
            //mnuExpbillPendingsAckEDPMS.Disabled = false;

            //mnu_EDPMS_Datachecklist_DocReceipt.Visible = true;
            //mnu_EDPMS_Datachecklist_DocReceipt.Disabled = false;

            //mnu_EDPMS_Datachecklist_Realized.Visible = true;
            //mnu_EDPMS_Datachecklist_Realized.Disabled = false;


            //mnu_EDPMS_XML_FilePending.Visible = true;
            //mnu_EDPMS_XML_FilePending.Disabled = false;

            //mnu_Receipt_AckReport.Visible = true;
            //mnu_Receipt_AckReport.Disabled = false;

            //mnu_Realization_AckReport.Visible = true;
            //mnu_Realization_AckReport.Disabled = false;

            //Mnu_EDPMS_E_FIRC.Visible = true;
            //Mnu_EDPMS_E_FIRC.Disabled = false;

            ////-------File upload-------------------------

            //mFu_EDPMS_Receipt.Visible = true;
            //mFu_EDPMS_Receipt.Disabled = false;

            //mFu_PaymentRealization.Visible = true;
            //mFu_PaymentRealization.Disabled = false;

            //mnu_adtransack.Visible = true;
            //mnu_adtransack.Disabled = false;

            //mnu_Reciept_Ack.Visible = true;
            //mnu_Reciept_Ack.Disabled = false;

            //mnu_Realization_Ack.Visible = true;
            //mnu_Realization_Ack.Disabled = false;

            //mnu_dump.Visible = true;
            //mnu_dump.Disabled = false;

            //mnu_pmtextnack.Visible = true;
            //mnu_pmtextnack.Disabled = false;


            //mnu_EFIRC_Closure_File_Upload.Visible = true;
            //mnu_EFIRC_Closure_File_Upload.Disabled = false;

            //mnu_EFIRC_Old_Closure_File_Upload.Visible = true;
            //mnu_EFIRC_Old_Closure_File_Upload.Disabled = false;

            ////------File Format----------
            //mnu_E_FIRC_Clr_Format.Visible = true;
            //mnu_E_FIRC_Clr_Format.Disabled = false;

        }
        #endregion

        #region IDPMS Module
        if (hdnModuleID.Value == "IDPMS")
        {
            lblModuleName.Text = "IDPMS Module";

            //--------------Masters--------------//


            mnuCustomerMaster.Disabled = false;
            mnuCustomerMaster.Visible = true;

            mnuPortCodeMaster.Disabled = false;
            mnuPortCodeMaster.Visible = true;

            mnuSpecialDates.Disabled = false;
            mnuSpecialDates.Visible = true;

            mnuOveseasPartyMaster.Disabled = false;
            mnuOveseasPartyMaster.Visible = true;

            mnuOverseasBankMaster.Disabled = false;
            mnuOverseasBankMaster.Visible = true;

            mnuCurrencyMaster.Disabled = false;
            mnuCurrencyMaster.Visible = true;

            mnuPurposeCodeMaster.Disabled = false;
            mnuPurposeCodeMaster.Visible = true;


            ////------------Transactions------------------------

            //       mnu_AddEditBOE.Visible = true;
            //       mnu_AddEditBOE.Disabled = false;

            //       mnuAddEditManualBoe.Visible = true;
            //       mnuAddEditManualBoe.Disabled = false;

            //       mnu_OtherAdBOE.Visible = true;
            //       mnu_OtherAdBOE.Disabled = false;

            //       mnu_AddEditPEX.Visible = true;
            //       mnu_AddEditPEX.Disabled = false;

            //       mnuORM_Closure.Visible = true;
            //       mnuORM_Closure.Disabled = false;

            //       mnu_BOEClosure.Visible = true;
            //       mnu_BOEClosure.Disabled = false;

            //       mnu_BOE_Sett_View.Visible = true;
            //       mnu_BOE_Sett_View.Disabled = false;

            //       mnuBOECancel.Visible = true;
            //       mnuBOECancel.Disabled = false;


            //       //-------------FILE CREATION-----------//

            //       mnu_ORMXMLCreation.Visible = true;
            //       mnu_ORMXMLCreation.Disabled = false;

            //       mnu_ManFile.Visible = true;
            //       mnu_ManFile.Disabled = false;

            //       mnu_otherADcsvcre.Visible = true;
            //       mnu_otherADcsvcre.Disabled = false;

            //       mnu_PaySetCre.Visible = true;
            //       mnu_PaySetCre.Disabled = false;

            //       mnu_PayExt.Visible = true;
            //       mnu_PayExt.Disabled = false;

            //       mnuORM_XMl.Visible = true;
            //       mnuORM_XMl.Disabled = false;

            //       mnuBOEClosure.Visible = true;
            //       mnuBOEClosure.Disabled = false;

            //       mnu_CustomerMaster_CSV_file.Visible = true;
            //       mnu_CustomerMaster_CSV_file.Disabled = false;


            //       //-------------Reports-----------------//

            //       mnu_ORMChecklist.Visible = true;
            //       mnu_ORMChecklist.Disabled = false;

            //       mnu_BOEDataCheck.Visible = true;
            //       mnu_BOEDataCheck.Disabled = false;

            //       mnu_ORMAckReport.Visible = true;
            //       mnu_ORMAckReport.Disabled = false;

            //       mnuManualBOEAck.Visible = true;
            //       mnuManualBOEAck.Disabled = false;

            //       mnuBOEOSRPT.Visible = true;
            //       mnuBOEOSRPT.Disabled = false;

            //       mnurptORMOS.Visible = true;
            //       mnurptORMOS.Disabled = false;

            //       mnuBOEPenPayExt.Visible = true;
            //       mnuBOEPenPayExt.Disabled = false;

            //       mnuBOEack.Visible = true;
            //       mnuBOEack.Disabled = false;

            //       mnuTracerLetter.Visible = true;
            //       mnuTracerLetter.Disabled = false;

            //       mnuPayExtDone.Visible = true;
            //       mnuPayExtDone.Disabled = false;

            //       mnuDmpCust.Visible = true;
            //       mnuDmpCust.Disabled = false;

            //       mnuOrmClosure.Visible = true;
            //       mnuOrmClosure.Disabled = false;

            //       mnuBoeClosureRep.Visible = true;
            //       mnuBoeClosureRep.Disabled = false;


            //       mnuOtherAD.Visible = true;
            //       mnuOtherAD.Disabled = false;

            //       mnu_rpt_ORM_Ack_Upload.Visible = true;
            //       mnu_rpt_ORM_Ack_Upload.Disabled = false;

            //       mnu_rpt_ORM_Ack_NotUpload.Visible = true;
            //       mnu_rpt_ORM_Ack_NotUpload.Disabled = false;

            //       //-------------FILE Upload-----------//

            //       mnu_ORMFileupload.Visible = true;
            //       mnu_ORMFileupload.Disabled = false;

            //       mnu_idpms_dump.Visible = true;
            //       mnu_idpms_dump.Disabled = false;

            //       mnu_BOE_settlement_Upl.Visible = true;
            //       mnu_BOE_settlement_Upl.Disabled = false;

            //       mnu_Other_AD_Upl.Visible = true;
            //       mnu_Other_AD_Upl.Disabled = false;

            //       mnu_Manual_BOE_Upl.Visible = true;
            //       mnu_Manual_BOE_Upl.Disabled = false;

            //       mnu_Pay_Ext_Upl.Visible = true;
            //       mnu_Pay_Ext_Upl.Disabled = false;

            //       mnu_ORMAck.Visible = true;
            //       mnu_ORMAck.Disabled = false;

            //       mnu_PaySet.Visible = true;
            //       mnu_PaySet.Disabled = false;

            //       mnu_ORMClosureUpload.Visible = true;
            //       mnu_ORMClosureUpload.Disabled = false;

            //       mnu_BOEClosureUpload.Visible = true;
            //       mnu_BOEClosureUpload.Disabled = false;

            //       //--------File Format------------------------------------------

            //       mnu_ORMFORMAT.Visible = true;
            //       mnu_ORMFORMAT.Disabled = false;

            //       mnu_BOE_settlement_Upl_Format.Visible = true;
            //       mnu_BOE_settlement_Upl_Format.Disabled = false;

            //       mnu_Other_AD_Upl_Format.Visible = true;
            //       mnu_Other_AD_Upl_Format.Disabled = false;

            //       mnu_Manual_BOE_Upl_Format.Visible = true;
            //       mnu_Manual_BOE_Upl_Format.Disabled = false;

            //       mnu_Pay_Ext_Upl_Format.Visible = true;
            //       mnu_Pay_Ext_Upl_Format.Disabled = false;

            //       mnu_ORM_Clr_Format.Visible = true;
            //       mnu_ORM_Clr_Format.Disabled = false;

            //       mnu_BOE_Clr_Format.Visible = true;
            //       mnu_BOE_Clr_Format.Disabled = false;

        }
        #endregion

        #region RReturn Module
        if (hdnModuleID.Value == "R-Return")
        {
            lblModuleName.Text = "R Return Module";
            //---------------Admin--------------------------
            //Bhupen
            if (Session["userRole"].ToString().Trim() == "Admin")
            {
                mnu_Error.Visible = true;
                mnu_Error.Disabled = false;
            }
            else
            {
                mnu_Error.Visible = false;
                mnu_Error.Disabled = true;
            }

            //---------------- Master ---------------------- 

            mnuPortCodeMaster.Disabled = false;
            mnuPortCodeMaster.Visible = true;

            mnuCurrencyMaster.Disabled = false;
            mnuCurrencyMaster.Visible = true;

            mnuPurposeCodeMaster.Disabled = false;
            mnuPurposeCodeMaster.Visible = true;

            mnuCountryMaster.Disabled = false;
            mnuCountryMaster.Visible = true;

            mnuVastroBankMaster.Disabled = false;
            mnuVastroBankMaster.Visible = true;

            mnuRet_AuthorisedSignatory.Disabled = false;
            mnuRet_AuthorisedSignatory.Visible = true;

            
        }
        #endregion

        #region RReturn Module
        if (hdnModuleID.Value == "ImportWareHousing")
        {
            lblModuleName.Text = "Digital Import Payment System";

            //---------------- Master ---------------------- 
            mnuCustomerMaster.Disabled = false;
            mnuCustomerMaster.Visible = true;

            mnuCurrencyMaster.Disabled = false;
            mnuCurrencyMaster.Visible = true;

            //mnuSupplierrMaster.Visible = true;
            //mnuSupplierrMaster.Disabled = false;

            //------------ Transactions-----------//
            //mnu_Impwh_Settlement.Visible = true;
            //mnu_Impwh_Settlement.Disabled = false;

            mnu_CustMandad.Visible = true;
            mnu_CustMandad.Disabled = false;

            //------File Creation-----------//
            mnu_GDPExcelFileCreation.Visible = true;
            mnu_GDPExcelFileCreation.Disabled = false;

            mnu_PaymentFileCreation.Visible = true;
            mnu_PaymentFileCreation.Disabled = false;

            mnu_SettlementFileCreation.Visible = true;
            mnu_SettlementFileCreation.Disabled = false;


            //----------Reports----------------//
            mnu_GoodToPayDiscripancy.Visible = true;
            mnu_GoodToPayDiscripancy.Disabled = false;

            mnu_BillOfEntryOutstanding.Visible = true;
            mnu_BillOfEntryOutstanding.Disabled = false;

            //mnu_GDUpdateAuditTrail.Visible = true;
            //mnu_GDUpdateAuditTrail.Disabled = false;

            mnu_ImportBillsPaidReport.Visible = true;
            mnu_ImportBillsPaidReport.Disabled = false;

            mnu_OverdueImportPaidReport.Visible = true;
            mnu_OverdueImportPaidReport.Disabled = false;

            //----------FIle Upload------------//
            mnu_GDPExcelFileUpload.Visible = true;
            mnu_GDPExcelFileUpload.Disabled = false;

            mnu_PaymentFileUpload.Visible = true;
            mnu_PaymentFileUpload.Disabled = false;

            //mnutestemailpage.Visible = true;
            //mnutestemailpage.Disabled = false;

            //---------Audit Trail---------------//
            mnuFileCreationAuditTriail.Visible = true;
            mnuFileCreationAuditTriail.Disabled = false;

            mnuFileUploadAuditTriail.Visible = true;
            mnuFileUploadAuditTriail.Disabled = false;

            mnuMasterAuditTriail.Visible = true;
            mnuMasterAuditTriail.Disabled = false;
        }
        #endregion

        #region Supervisor Role

        if (Session["userRole"] != null && Session["userRole"].ToString().Trim() == "Supervisor")
        {

            if (hdnModuleID.Value == "EXP")
            {
                mnuEXPAuditTrail.Visible = true;
                mnuEXPAuditTrail.Disabled = false;
            }

            if (hdnModuleID.Value == "EDPMS")
            {
                mnuEDPMSAuditTrail.Visible = true;
                mnuEDPMSAuditTrail.Disabled = false;
            }

            if (hdnModuleID.Value == "EBR")
            {
                mnu_Ebrc_Generate_TradeData.Visible = true;
                mnu_Ebrc_Generate_TradeData.Disabled = false;
            }

            if (hdnModuleID.Value == "IDPMS")
            {
                mnuIDPMSAuditTrail.Visible = true;
                mnuIDPMSAuditTrail.Disabled = false;
            }
            if (hdnModuleID.Value == "IMP")
            {
                mnu_ImpReportEntity.Visible = true;
                mnu_ImpReportEntity.Disabled = false;

                mnu_ManualLCfLagUPDate.Disabled = false;
                mnu_ManualLCfLagUPDate.Visible = true;
                mnu_trans_Log.Disabled = false;
                mnu_trans_Log.Visible = true;
                mnu_LEIEmailDetails.Disabled = false;
                mnu_LEIEmailDetails.Visible = true;
                mnu_IMP_RollBackLodgement.Disabled = false;
                mnu_IMP_RollBackLodgement.Visible = true;

            }
            mnu_UserActivityDetails.Visible = true;
            mnu_UserActivityDetails.Disabled = false;

        }
        #endregion

        #region Admin Role

        if (Session["userRole"] != null && Session["userRole"].ToString().Trim() == "Admin")
        {
            mHK_AccessControl.Visible = true;
            mHK_AccessControl.Disabled = false;

            mnu_Expuserlist.Visible = true;
            mnu_Expuserlist.Disabled = false;

            mnu_UserActivityDetails.Visible = true;
            mnu_UserActivityDetails.Disabled = false;

            //mnu_UserActivityDashBoard.Visible = true;
            // mnu_UserActivityDashBoard.Disabled = false;
            if (hdnModuleID.Value == "IMP")
            {

                mnu_ManualLCfLagUPDate.Disabled = false;
                mnu_ManualLCfLagUPDate.Visible = true;
                mnu_trans_Log.Disabled = false;
                mnu_trans_Log.Visible = true;
                mnu_LEIEmailDetails.Disabled = false;
                mnu_LEIEmailDetails.Visible = true;
                mnu_IMP_RollBackLodgement.Disabled = false;
                mnu_IMP_RollBackLodgement.Visible = true;
            }
        }

        mHk_HouseKeeping.Visible = true;
        mHk_HouseKeeping.Disabled = false;
        
        #endregion

        TF_DATA objData = new TF_DATA();
        string _query = "TF_GetAccessedMenuList";
        string _user = "";
        if (Session["userName"] != null)
            _user = Session["userName"].ToString().Trim();

        SqlParameter pUserName = new SqlParameter("@userName", SqlDbType.VarChar);
        pUserName.Value = _user;
        DataTable dt = objData.getData(_query, pUserName);
        if (dt.Rows.Count > 0)
        {
            string menuName = "";
            string moduleName = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                menuName = dt.Rows[i]["MenuName"].ToString();
                moduleName = dt.Rows[i]["Module"].ToString();

                #region Imports Module

                //----------------IMPORT Auto--------------//
                if (hdnModuleID.Value == "IMP")
                {
                    // ==================== Masters =================//
                    /**
                    if (menuName == mnuCustomerMaster.InnerHtml)
                    {
                        mnuCustomerMaster.Visible = true;
                        mnuCustomerMaster.Disabled = false;
                    }
                    if (menuName == mnuServiceTaxMaster.InnerHtml)
                    {
                        mnuServiceTaxMaster.Visible = true;
                        mnuServiceTaxMaster.Disabled = false;
                    }
                    if (menuName == mnuPurposeCodeMaster.InnerHtml)
                    {
                        mnuPurposeCodeMaster.Visible = true;
                        mnuPurposeCodeMaster.Disabled = false;
                    }
                    if (menuName == mnuPortCodeMaster.InnerHtml)
                    {
                        mnuPortCodeMaster.Visible = true;
                        mnuPortCodeMaster.Disabled = false;
                    }
                    if (menuName == mnuCountryMaster.InnerHtml)
                    {
                        mnuCountryMaster.Visible = true;
                        mnuCountryMaster.Disabled = false;
                    }
                    if (menuName == mnuGbaseCommMaster.InnerHtml)
                    {
                        mnuGbaseCommMaster.Visible = true;
                        mnuGbaseCommMaster.Disabled = false;
                    }
                    if (menuName == mnuGLCodeMaster.InnerHtml)
                    {
                        mnuGLCodeMaster.Visible = true;
                        mnuGLCodeMaster.Disabled = false;
                    }
                    if (menuName == mnuOverseasBankMaster.InnerHtml)
                    {
                        mnuOverseasBankMaster.Visible = true;
                        mnuOverseasBankMaster.Disabled = false;
                    }
                    if (menuName == mnuReimbursingBankMaster.InnerHtml)
                    {
                        mnuReimbursingBankMaster.Visible = true;
                        mnuReimbursingBankMaster.Disabled = false;
                    }
                    if (menuName == mnuHolidayMaster.InnerHtml)
                    {
                        mnuHolidayMaster.Visible = true;
                        mnuHolidayMaster.Disabled = false;
                    }
                    if (menuName == mnuCommisionMaster.InnerHtml)
                    {
                        mnuCommisionMaster.Visible = true;
                        mnuCommisionMaster.Disabled = false;
                    }
                    if (menuName == mnuCurrencyMaster.InnerHtml)
                    {
                        mnuCurrencyMaster.Visible = true;
                        mnuCurrencyMaster.Disabled = false;
                    }
                    if (menuName == mnuEXP_SanctionedCountry.InnerHtml)
                    {
                        mnuEXP_SanctionedCountry.Visible = true;
                        mnuEXP_SanctionedCountry.Disabled = false;
                    }
                    if (menuName == mnuStampDutyMaster.InnerHtml)
                    {
                        mnuStampDutyMaster.Visible = true;
                        mnuStampDutyMaster.Disabled = false;
                    }

                    if (menuName == mnu_Discrepency.InnerHtml)
                    {
                        mnu_Discrepency.Visible = true;
                        mnu_Discrepency.Disabled = false;
                    }
                    if (menuName == mnu_Imp_DrawerMaster.InnerHtml)
                    {
                        mnu_Imp_DrawerMaster.Visible = true;
                        mnu_Imp_DrawerMaster.Disabled = false;
                    }
                    if (menuName == mnu_IMP_NOSTRO_Master_View.InnerHtml)
                    {
                        mnu_IMP_NOSTRO_Master_View.Visible = true;
                        mnu_IMP_NOSTRO_Master_View.Disabled = false;
                    }
                    if (menuName == mnu_RMAMaster.InnerHtml)
                    {
                        mnu_RMAMaster.Visible = true;
                        mnu_RMAMaster.Disabled = false;
                    }
                    if (menuName == mnuSundryAccMaster.InnerHtml)
                    {
                        mnuSundryAccMaster.Visible = true;
                        mnuSundryAccMaster.Disabled = false;
                    }
                     
                    **/

                    //------------Transction-------------------//
                    if (menuName == mnu_IMP_BOE_Maker_View.InnerHtml)
                    {
                        mnu_IMP_BOE_Maker_View.Visible = true;
                        mnu_IMP_BOE_Maker_View.Disabled = false;
                    }
                    if (menuName == mnu_IMP_BOE_Checker_View.InnerHtml)
                    {
                        mnu_IMP_BOE_Checker_View.Visible = true;
                        mnu_IMP_BOE_Checker_View.Disabled = false;
                    }

                    if (menuName == mnu_IMP_Booking_Of_IBD_ACC_Maker.InnerHtml)
                    {
                        mnu_IMP_Booking_Of_IBD_ACC_Maker.Visible = true;
                        mnu_IMP_Booking_Of_IBD_ACC_Maker.Disabled = false;
                    }
                    if (menuName == mnu_IMP_Booking_Of_IBD_ACC_Checker.InnerHtml)
                    {
                        mnu_IMP_Booking_Of_IBD_ACC_Checker.Visible = true;
                        mnu_IMP_Booking_Of_IBD_ACC_Checker.Disabled = false;
                    }

                    if (menuName == mnu_IMP_Settlement_Maker.InnerHtml)
                    {
                        mnu_IMP_Settlement_Maker.Visible = true;
                        mnu_IMP_Settlement_Maker.Disabled = false;
                    }
                    if (menuName == mnu_IMP_Settlement_Checker.InnerHtml)
                    {
                        mnu_IMP_Settlement_Checker.Visible = true;
                        mnu_IMP_Settlement_Checker.Disabled = false;
                    }
                    if (menuName == mnu_IMP_LC_DESCOUNTING_ACC_IBD_Maker.InnerHtml)
                    {
                        mnu_IMP_LC_DESCOUNTING_ACC_IBD_Maker.Visible = true;
                        mnu_IMP_LC_DESCOUNTING_ACC_IBD_Maker.Disabled = false;
                    }
                    if (menuName == mnu_IMP_LC_DESCOUNTING_ACC_IBD_Checker.InnerHtml)
                    {
                        mnu_IMP_LC_DESCOUNTING_ACC_IBD_Checker.Visible = true;
                        mnu_IMP_LC_DESCOUNTING_ACC_IBD_Checker.Disabled = false;
                    }

                    if (menuName == mnu_BankReleaseOrder.InnerHtml)
                    {
                        mnu_BankReleaseOrder.Visible = true;
                        mnu_BankReleaseOrder.Disabled = false;
                    }

                    if (menuName == mnu_BankReleaseOrder_checker_view.InnerHtml)
                    {
                        mnu_BankReleaseOrder_checker_view.Visible = true;
                        mnu_BankReleaseOrder_checker_view.Disabled = false;
                    }

                    if (menuName == mnu_BROGO_Maker_View.InnerHtml)
                    {
                        mnu_BROGO_Maker_View.Visible = true;
                        mnu_BROGO_Maker_View.Disabled = false;
                    }

                    if (menuName == mnu_BROGO_Checker_View.InnerHtml)
                    {
                        mnu_BROGO_Checker_View.Visible = true;
                        mnu_BROGO_Checker_View.Disabled = false;
                    }

                    if (menuName == mnu_LC_DESCOUNTING_Settlement_Maker.InnerHtml)
                    {
                        mnu_LC_DESCOUNTING_Settlement_Maker.Visible = true;
                        mnu_LC_DESCOUNTING_Settlement_Maker.Disabled = false;
                    }
                    if (menuName == mnu_LC_DESCOUNTING_Settlement_Checker.InnerHtml)
                    {
                        mnu_LC_DESCOUNTING_Settlement_Checker.Visible = true;
                        mnu_LC_DESCOUNTING_Settlement_Checker.Disabled = false;
                    }
                    if (menuName == mnu_TF_IMP_InquiryOfLedgerFile_Maker.InnerHtml)
                    {
                        mnu_TF_IMP_InquiryOfLedgerFile_Maker.Visible = true;
                        mnu_TF_IMP_InquiryOfLedgerFile_Maker.Disabled = false;
                    }
                    if (menuName == mnu_TF_IMP_InquiryOfLedgerFile_Checker.InnerHtml)
                    {
                        mnu_TF_IMP_InquiryOfLedgerFile_Checker.Visible = true;
                        mnu_TF_IMP_InquiryOfLedgerFile_Checker.Disabled = false;
                    }

                    if (menuName == mnu_TF_IMP_Ledger_Modification_ICA_ICU_IBA_Maker.InnerHtml)
                    {
                        mnu_TF_IMP_Ledger_Modification_ICA_ICU_IBA_Maker.Visible = true;
                        mnu_TF_IMP_Ledger_Modification_ICA_ICU_IBA_Maker.Disabled = false;
                    }
                    if (menuName == mnu_TF_IMP_Ledger_Modification_ICA_ICU_IBA_Checker.InnerHtml)
                    {
                        mnu_TF_IMP_Ledger_Modification_ICA_ICU_IBA_Checker.Visible = true;
                        mnu_TF_IMP_Ledger_Modification_ICA_ICU_IBA_Checker.Disabled = false;
                    }

                    if (menuName == mnu_Shipping_Guarantee_Maker.InnerHtml)
                    {
                        mnu_Shipping_Guarantee_Maker.Visible = true;
                        mnu_Shipping_Guarantee_Maker.Disabled = false;
                    }
                    if (menuName == mnu_Shipping_Guarantee_Checker.InnerHtml)
                    {
                        mnu_Shipping_Guarantee_Checker.Visible = true;
                        mnu_Shipping_Guarantee_Checker.Disabled = false;
                    }

                    if (menuName == mnu_TF_IMP_ReversalOfLiability_Maker_View.InnerHtml)
                    {
                        mnu_TF_IMP_ReversalOfLiability_Maker_View.Visible = true;
                        mnu_TF_IMP_ReversalOfLiability_Maker_View.Disabled = false;
                    }
                    if (menuName == mnu_TF_IMP_ReversalOfLiability_Checker_View.InnerHtml)
                    {
                        mnu_TF_IMP_ReversalOfLiability_Checker_View.Visible = true;
                        mnu_TF_IMP_ReversalOfLiability_Checker_View.Disabled = false;
                    }

                    //==================== File Creation =================//
                    if (menuName == mnu_IMP_CustDumpdata.InnerHtml)
                    {
                        mnu_IMP_CustDumpdata.Visible = true;
                        mnu_IMP_CustDumpdata.Disabled = false;
                    }
                    if (menuName == mnu_imp_bankDumpData.InnerHtml)
                    {
                        mnu_imp_bankDumpData.Visible = true;
                        mnu_imp_bankDumpData.Disabled = false;
                    }
                    //==================== Excel Report =================//
                    if (menuName == mnu_lodgmentmaker.InnerHtml)
                    {
                        mnu_lodgmentmaker.Visible = true;
                        mnu_lodgmentmaker.Disabled = false;
                    }
                    if (menuName == mnu_AcceptanceApproveFile_XL.InnerHtml)
                    {
                        mnu_AcceptanceApproveFile_XL.Visible = true;
                        mnu_AcceptanceApproveFile_XL.Disabled = false;
                    }

                    if (menuName == mnu_SettlementFileCreation_XL.InnerHtml)
                    {
                        mnu_SettlementFileCreation_XL.Visible = true;
                        mnu_SettlementFileCreation_XL.Disabled = false;
                    }
                    //==================== Report =================//
                    //if (menuName == mnuMaker_Lodg_CheckList.InnerHtml)
                    //{
                    //    mnuMaker_Lodg_CheckList.Visible = true;
                    //    mnuMaker_Lodg_CheckList.Disabled = false;
                    //}
                    //if (menuName == mnuCheker_Lodg_CheckList.InnerHtml)
                    //{
                    //    mnuCheker_Lodg_CheckList.Visible = true;
                    //    mnuCheker_Lodg_CheckList.Disabled = false;
                    //}

                    if (menuName == mnu_imp_UnsettledDoc.InnerHtml)
                    {
                        mnu_imp_UnsettledDoc.Visible = true;
                        mnu_imp_UnsettledDoc.Disabled = false;
                    }
                    if (menuName == mun_imp_broreport.InnerHtml)
                    {
                        mun_imp_broreport.Visible = true;
                        mun_imp_broreport.Disabled = false;
                    }
                    if (menuName == mnu_imp_broexcel.InnerHtml)
                    {
                        mnu_imp_broexcel.Visible = true;
                        mnu_imp_broexcel.Disabled = false;
                    }

                    if (menuName == mnu_Trans_Summary.InnerHtml)
                    {
                        mnu_Trans_Summary.Visible = true;
                        mnu_Trans_Summary.Disabled = false;
                    }
                    if (menuName == mnu_IMP_VouchersBalance.InnerHtml)
                    {
                        mnu_IMP_VouchersBalance.Visible = true;
                        mnu_IMP_VouchersBalance.Disabled = false;
                    }
                    if (menuName == mnu_IMPControlSheetReport.InnerHtml)
                    {
                        mnu_IMPControlSheetReport.Visible = true;
                        mnu_IMPControlSheetReport.Disabled = false;
                    }
                    if (menuName == mnuAcceptance_Lodg_CheckList.InnerHtml)
                    {
                        mnuAcceptance_Lodg_CheckList.Visible = true;
                        mnuAcceptance_Lodg_CheckList.Disabled = false;
                    }

                    if (menuName == mnuLodgment_report_interface.InnerHtml)
                    {
                        mnuLodgment_report_interface.Visible = true;
                        mnuLodgment_report_interface.Disabled = false;
                    }

                    if (menuName == mnuLodgmentExcelrpt.InnerHtml)
                    {
                        mnuLodgmentExcelrpt.Disabled = false;
                        mnuLodgmentExcelrpt.Visible = true;
                    }
                    if (menuName == mnuAcceptanceExcelrpt.InnerHtml)
                    {
                        mnuAcceptanceExcelrpt.Disabled = false;
                        mnuAcceptanceExcelrpt.Visible = true;
                    }
                    if (menuName == mnuSG_Register.InnerHtml)
                    {
                        mnuSG_Register.Disabled = false;
                        mnuSG_Register.Visible = true;
                    }
                    if (menuName == mnuImpBill_Register.InnerHtml)
                    {
                        mnuImpBill_Register.Disabled = false;
                        mnuImpBill_Register.Visible = true;
                    }
                    if (menuName == mnu_IMPOutgoingMalisandCourier.InnerHtml)
                    {
                        mnu_IMPOutgoingMalisandCourier.Visible = true;
                        mnu_IMPOutgoingMalisandCourier.Disabled = false;
                    }
                    if (menuName == mnu_IMP_SwiftGenratedList.InnerHtml)
                    {
                        mnu_IMP_SwiftGenratedList.Disabled = false;
                        mnu_IMP_SwiftGenratedList.Visible = true;
                    }
                    //==================== File Upload =================//
                    if (menuName == mnu_HolidayFileUpload.InnerHtml)
                    {
                        mnu_HolidayFileUpload.Visible = true;
                        mnu_HolidayFileUpload.Disabled = false;
                    }
                    //=================File Format==================/////

                    if (menuName == mnu_HolidayFileFormat.InnerHtml)
                    {
                        mnu_HolidayFileFormat.Visible = true;
                        mnu_HolidayFileFormat.Disabled = false;
                    }

                    if (menuName == mnu_RMAFileFormat.InnerHtml)
                    {
                        mnu_RMAFileFormat.Visible = true;
                        mnu_RMAFileFormat.Disabled = false;
                    }
                    //=================Audit Trail==================/////
                    if (menuName == mnu_impMasterAuditTriail.InnerHtml)
                    {
                        mnu_impMasterAuditTriail.Visible = true;
                        mnu_impMasterAuditTriail.Disabled = false;
                    }
                    if (menuName == mnu_impUploadMasterAuditTriail.InnerHtml)
                    {
                        mnu_impUploadMasterAuditTriail.Visible = true;
                        mnu_impUploadMasterAuditTriail.Disabled = false;
                    }
                    if (menuName == mnu_impuploadTransactionAudittrial.InnerHtml)
                    {
                        mnu_impuploadTransactionAudittrial.Visible = true;
                        mnu_impuploadTransactionAudittrial.Disabled = false;
                    }

                }
                #endregion

                #region Export Module
                //----------------Export module--------------//
                if (hdnModuleID.Value == "EXP")
                {
                    //------------Transction-------------------//
                    if (menuName == mnu_EXPbillOfEntry.InnerHtml)
                    {
                        mnu_EXPbillOfEntry.Visible = true;
                        mnu_EXPbillOfEntry.Disabled = false;
                    }
                    if (menuName == mnu_EXPbillOfEntryChecker.InnerHtml)// Added by Bhupen 15-03-2023
                    {
                        mnu_EXPbillOfEntryChecker.Visible = true;
                        mnu_EXPbillOfEntryChecker.Disabled = false;
                    }

                    //---------------------Anand 31-07-2023---------------------------
                    if (menuName == mnu_EXPApprovedLodgemetForRODMaker.InnerHtml)
                    {
                        mnu_EXPApprovedLodgemetForRODMaker.Visible = true;
                        mnu_EXPApprovedLodgemetForRODMaker.Disabled = false;
                    }
                    if (menuName == mnu_EXPApprovedLodgemetForRODChecker.InnerHtml)
                    {
                        mnu_EXPApprovedLodgemetForRODChecker.Visible = true;
                        mnu_EXPApprovedLodgemetForRODChecker.Disabled = false;
                    }
                    //-------------------------End------------------------------------------

                    if (menuName == mnu_RealisationDataEntry.InnerHtml)
                    {
                        mnu_RealisationDataEntry.Visible = true;
                        mnu_RealisationDataEntry.Disabled = false;
                    }

                    if (menuName == mnu_RealisationDataEntryMaker.InnerHtml)
                    {
                        mnu_RealisationDataEntryMaker.Visible = true;
                        mnu_RealisationDataEntryMaker.Disabled = false;
                    }
                    if (menuName == mnu_RealisationDataEntrychecker.InnerHtml)//Added by Nilesh 20-02-2023
                    {
                        mnu_RealisationDataEntrychecker.Visible = true;
                        mnu_RealisationDataEntrychecker.Disabled = false;
                    }
                    //---------------------Shailesh 22-09-2023---------------------------
                    if (menuName == mnu_EXPApprovedRealisationForPRNMaker.InnerHtml)
                    {
                        mnu_EXPApprovedRealisationForPRNMaker.Visible = true;
                        mnu_EXPApprovedRealisationForPRNMaker.Disabled = false;
                    }
                    if (menuName == mnu_EXPApprovedRealisationForPRNChecker.InnerHtml)
                    {
                        mnu_EXPApprovedRealisationForPRNChecker.Visible = true;
                        mnu_EXPApprovedRealisationForPRNChecker.Disabled = false;
                    }
                    //-------------------------End------------------------------------------
                    if (menuName == mnu_EXPdueDate.InnerHtml)
                    {
                        mnu_EXPdueDate.Visible = true;
                        mnu_EXPdueDate.Disabled = false;
                    }
                    if (menuName == mnuInwDataEntry.InnerHtml)
                    {
                        mnuInwDataEntry.Visible = true;
                        mnuInwDataEntry.Disabled = false;
                    }
                    //Added by Nilesh 20-02-2023
                    if (menuName == mnuInwDataEntrychecker.InnerHtml)
                    {
                        mnuInwDataEntrychecker.Visible = true;
                        mnuInwDataEntrychecker.Disabled = false;
                    }
                    //Added by Nilesh 20-02-2023 END
                    if (menuName == mnuEXP_MerchantTrade.InnerHtml)
                    {
                        mnuEXP_MerchantTrade.Visible = true;
                        mnuEXP_MerchantTrade.Disabled = false;
                    }
                    if (menuName == mnu_EXP_swift.InnerHtml)
                    {
                        mnu_EXP_swift.Visible = true;
                        mnu_EXP_swift.Disabled = false;
                    }
                    if (menuName == mnu_EXP_swiftchecker.InnerHtml)
                    {
                        mnu_EXP_swiftchecker.Visible = true;
                        mnu_EXP_swiftchecker.Disabled = false;
                    }
                    


                    //-------------FILE CREATION-----------//

                    if (menuName == MnuprblmExpBill.InnerHtml)
                    {
                        MnuprblmExpBill.Visible = true;
                        MnuprblmExpBill.Disabled = false;
                    }

                    if (menuName == mnu_Export_Bill_Receipt_CSVFileCreation.InnerHtml)
                    {
                        mnu_Export_Bill_Receipt_CSVFileCreation.Visible = true;
                        mnu_Export_Bill_Receipt_CSVFileCreation.Disabled = false;
                    }
                    if (menuName == mnuRealisationCSV.InnerHtml)
                    {
                        mnuRealisationCSV.Visible = true;
                        mnuRealisationCSV.Disabled = false;
                    }
                    if (menuName == mnuManualGRCSV.InnerHtml)
                    {
                        mnuManualGRCSV.Visible = true;
                        mnuManualGRCSV.Disabled = false;
                    }
                    if (menuName == mnuRemittanceAdvance.InnerHtml)
                    {
                        mnuRemittanceAdvance.Visible = true;
                        mnuRemittanceAdvance.Disabled = false;
                    }
                    if (menuName == generateReturnData.InnerHtml)
                    {
                        generateReturnData.Visible = true;
                        generateReturnData.Disabled = false;
                    }
                    if (menuName == generateGBaseData.InnerHtml)
                    {
                        generateGBaseData.Visible = true;
                        generateGBaseData.Disabled = false;
                    }
                    //-------------FILE Upload-----------//
                    if (menuName == mnuEDPMS_INW_File_Upload.InnerHtml)
                    {
                        mnuEDPMS_INW_File_Upload.Visible = true;
                        mnuEDPMS_INW_File_Upload.Disabled = false;
                    }
                    if (menuName == mnuESPMD_OUTSTANDING_File_Upload.InnerHtml)
                    {
                        mnuESPMD_OUTSTANDING_File_Upload.Visible = true;
                        mnuESPMD_OUTSTANDING_File_Upload.Disabled = false;
                    }

                    //-------------File Format----------------//
                    if (menuName == mnu_InwFileFormat.InnerHtml)
                    {
                        mnu_InwFileFormat.Visible = true;
                        mnu_InwFileFormat.Disabled = false;
                    }
                    //-------------Reports-----------------//
                    if (menuName == mRep_InwRemReg.InnerHtml)
                    {
                        mRep_InwRemReg.Visible = true;
                        mRep_InwRemReg.Disabled = false;
                    }
                    if (menuName == mnu_rptIRM_Outstanding.InnerHtml)
                    {
                        mnu_rptIRM_Outstanding.Visible = true;
                        mnu_rptIRM_Outstanding.Disabled = false;
                    }
                    //----------------------Anand 23-08-2023------------
                    if (menuName == mnu_rptIRM_CheckList.InnerHtml)
                    {
                        mnu_rptIRM_CheckList.Visible = true;
                        mnu_rptIRM_CheckList.Disabled = false;
                    }
                    //----------------------Anand 18-09-2023------------
                    if (menuName == mnu_EXP_DiscountAdvice.InnerHtml)
                    {
                        mnu_EXP_DiscountAdvice.Visible = true;
                        mnu_EXP_DiscountAdvice.Disabled = false;
                    }

                    //----------------------Anand 26-10-2023------------
                    if (menuName == mnu_EXP_BillSettlementAdvice.InnerHtml)
                    {
                        mnu_EXP_BillSettlementAdvice.Visible = true;
                        mnu_EXP_BillSettlementAdvice.Disabled = false;
                    }
                    //------------------------END-----------------------
                    if (menuName == mnuEXPBillReg.InnerHtml)
                    {
                        mnuEXPBillReg.Visible = true;
                        mnuEXPBillReg.Disabled = false;
                    }
                    if (menuName == mnuEXPBillReg_Unaccepted.InnerHtml)
                    {
                        mnuEXPBillReg_Unaccepted.Visible = true;
                        mnuEXPBillReg_Unaccepted.Disabled = false;
                    }
                    if (menuName == mnuEXPBillIntimetion.InnerHtml)
                    {
                        mnuEXPBillIntimetion.Visible = true;
                        mnuEXPBillIntimetion.Disabled = false;
                    }
                    if (menuName == mnuEXPBillDocument.InnerHtml)
                    {
                        mnuEXPBillDocument.Visible = true;
                        mnuEXPBillDocument.Disabled = false;
                    }
                    if (menuName == mnuEXPBillRealisedDocument.InnerHtml)
                    {
                        mnuEXPBillRealisedDocument.Visible = true;
                        mnuEXPBillRealisedDocument.Disabled = false;
                    }
                    if (menuName == mnuExportOverDueStatement.InnerHtml)
                    {
                        mnuExportOverDueStatement.Visible = true;
                        mnuExportOverDueStatement.Disabled = false;
                    }
                    if (menuName == mnu_EXBillOut.InnerHtml)
                    {
                        mnu_EXBillOut.Visible = true;
                        mnu_EXBillOut.Disabled = false;
                    }
                    if (menuName == mnuEXPRealisationReport.InnerHtml)
                    {
                        mnuEXPRealisationReport.Visible = true;
                        mnuEXPRealisationReport.Disabled = false;
                    }
                    if (menuName == mnuListofExpDocRealised.InnerHtml)
                    {
                        mnuListofExpDocRealised.Visible = true;
                        mnuListofExpDocRealised.Disabled = false;
                    }
                    if (menuName == mnuListofExpDocDispatched.InnerHtml)
                    {
                        mnuListofExpDocDispatched.Visible = true;
                        mnuListofExpDocDispatched.Disabled = false;
                    }
                    if (menuName == mnuadvanceexp.InnerHtml)
                    {
                        mnuadvanceexp.Visible = true;
                        mnuadvanceexp.Disabled = false;
                    }
                    if (menuName == mnunonsubmissionsdoc.InnerHtml)
                    {
                        mnunonsubmissionsdoc.Visible = true;
                        mnunonsubmissionsdoc.Disabled = false;
                    }
                    if (menuName == mnuCautionAdviceEXPBill.InnerHtml)
                    {
                        mnuCautionAdviceEXPBill.Visible = true;
                        mnuCautionAdviceEXPBill.Disabled = false;
                    }
                    if (menuName == mnuADTransfer.InnerHtml)
                    {
                        mnuADTransfer.Visible = true;
                        mnuADTransfer.Disabled = false;
                    }
                    if (menuName == mnuBilldueDate.InnerHtml)
                    {
                        mnuBilldueDate.Visible = true;
                        mnuBilldueDate.Disabled = false;
                    }
                    if (menuName == mnuEXP_MerchantTradeReport.InnerHtml)
                    {
                        mnuEXP_MerchantTradeReport.Visible = true;
                        mnuEXP_MerchantTradeReport.Disabled = false;
                    }
                    if (menuName == mnuAdvanceRemittance.InnerHtml)
                    {
                        mnuAdvanceRemittance.Visible = true;
                        mnuAdvanceRemittance.Disabled = false;
                    }
                    if (menuName == mnuEXP_SDFstatement.InnerHtml)
                    {
                        mnuEXP_SDFstatement.Visible = true;
                        mnuEXP_SDFstatement.Disabled = false;
                    }
                    if (menuName == mnuexportBillsDue.InnerHtml)
                    {
                        mnuexportBillsDue.Visible = true;
                        mnuexportBillsDue.Disabled = false;
                    }
                    if (menuName == mnuexportDataStatus.InnerHtml)
                    {
                        mnuexportDataStatus.Visible = true;
                        mnuexportDataStatus.Disabled = false;
                    }
                    //----------------------Anand 12-12-2023------------
                    if (menuName == mnu_rptIRM_CreatedReport.InnerHtml)
                    {
                        mnu_rptIRM_CreatedReport.Visible = true;
                        mnu_rptIRM_CreatedReport.Disabled = false;
                    }
                    if (menuName == mnu_rptIRM_UtilizationReport.InnerHtml)
                    {
                        mnu_rptIRM_UtilizationReport.Visible = true;
                        mnu_rptIRM_UtilizationReport.Disabled = false;
                    }
                    if (menuName == mnu_rptIRM_OutSatandingReport.InnerHtml)
                    {
                        mnu_rptIRM_OutSatandingReport.Visible = true;
                        mnu_rptIRM_OutSatandingReport.Disabled = false;
                    }
                    if (menuName == mnu_rpt_PendingROD.InnerHtml)
                    {
                        mnu_rpt_PendingROD.Visible = true;
                        mnu_rpt_PendingROD.Disabled = false;
                    }
                    if (menuName == mnu_EXP_PendingPRN.InnerHtml)
                    {
                        mnu_EXP_PendingPRN.Visible = true;
                        mnu_EXP_PendingPRN.Disabled = false;
                    }
                    if (menuName == mnu_rptEXP_ExcelOS_Reports.InnerHtml)
                    {
                        mnu_rptEXP_ExcelOS_Reports.Visible = true;
                        mnu_rptEXP_ExcelOS_Reports.Disabled = false;
                    }
                    if (menuName == mnu_rptEXP_ExcelOS_Reports_270.InnerHtml)
                    {
                        mnu_rptEXP_ExcelOS_Reports_270.Visible = true;
                        mnu_rptEXP_ExcelOS_Reports_270.Disabled = false;
                    }
                    if (menuName == mnu_rptExportBillReport.InnerHtml)
                    {
                        mnu_rptExportBillReport.Visible = true;
                        mnu_rptExportBillReport.Disabled = false;
                    }
                    if (menuName == mnu_rptShippingBillsPendingforACK.InnerHtml)
                    {
                        mnu_rptShippingBillsPendingforACK.Visible = true;
                        mnu_rptShippingBillsPendingforACK.Disabled = false;
                    }
                    if (menuName == mnu_EXPRealisation_Excel.InnerHtml)
                    {
                        mnu_EXPRealisation_Excel.Visible = true;
                        mnu_EXPRealisation_Excel.Disabled = false;
                    }
                    if (menuName == mnu_EXP_DigiSettlement.InnerHtml)
                    {
                        mnu_EXP_DigiSettlement.Visible = true;
                        mnu_EXP_DigiSettlement.Disabled = false;
                    }
                    if (menuName == mnu_EXP_pndngatrztn.InnerHtml)
                    {
                        mnu_EXP_pndngatrztn.Visible = true;
                        mnu_EXP_pndngatrztn.Disabled = false;
                    }
                    //------------------------END-----------------------
                    //--------------Audit Trail--------------------------
                    //if (menuName == mnuEXPAuditTrail.InnerHtml)
                    //{
                    //    mnuEXPAuditTrail.Visible = true;
                    //    mnuEXPAuditTrail.Disabled = false;
                    //}

                }
                #endregion

                #region EBRC Module
                if (hdnModuleID.Value == "EBR")
                {
                    // Transaction //

                    if (menuName == mnu_EBRCancellationDataEntry.InnerHtml)
                    {
                        mnu_EBRCancellationDataEntry.Visible = true;
                        mnu_EBRCancellationDataEntry.Disabled = false;
                    }
                    if (menuName == mnu_EXPRealisation.InnerHtml)
                    {
                        mnu_EXPRealisation.Visible = true;
                        mnu_EXPRealisation.Disabled = false;
                    }
                    if (menuName == mnu_EBRCITTEUCCheker.InnerHtml)
                    {
                        mnu_EBRCITTEUCCheker.Visible = true;
                        mnu_EBRCITTEUCCheker.Disabled = false;
                    }
                    // File creation

                    if (menuName == mnu_Ebrc_Generate_TradeData.InnerHtml)
                    {
                        mnu_Ebrc_Generate_TradeData.Visible = true;
                        mnu_Ebrc_Generate_TradeData.Disabled = false;
                    }
                    if (menuName == mnu_Ebrc_Generate_XML.InnerHtml)
                    {
                        mnu_Ebrc_Generate_XML.Visible = true;
                        mnu_Ebrc_Generate_XML.Disabled = false;
                    }
                    // Reports //
                    if (menuName == mnu_DataCheckList.InnerHtml)
                    {
                        mnu_DataCheckList.Visible = true;
                        mnu_DataCheckList.Disabled = false;
                    }
                    if (menuName == mnu_DataValidation.InnerHtml)
                    {
                        mnu_DataValidation.Visible = true;
                        mnu_DataValidation.Disabled = false;
                    }
                    if (menuName == mnu_EBRCertGenerated.InnerHtml)
                    {
                        mnu_EBRCertGenerated.Visible = true;
                        mnu_EBRCertGenerated.Disabled = false;
                    }
                    if (menuName == mnu_EBRCert_TobeGenerate.InnerHtml)
                    {
                        mnu_EBRCert_TobeGenerate.Visible = true;
                        mnu_EBRCert_TobeGenerate.Disabled = false;
                    }
                    if (menuName == mnu_BRC_Cancelled.InnerHtml)
                    {
                        mnu_BRC_Cancelled.Visible = true;
                        mnu_BRC_Cancelled.Disabled = false;
                    }
                    if (menuName == mnu_WithoutGRDetails.InnerHtml)
                    {
                        mnu_WithoutGRDetails.Visible = true;
                        mnu_WithoutGRDetails.Disabled = false;
                    }
                    if (menuName == mnuebrcintimation.InnerHtml)
                    {
                        mnuebrcintimation.Visible = true;
                        mnuebrcintimation.Disabled = false;
                    }
                    if (menuName == mnu_ITTEUCFileUpload.InnerHtml)
                    {
                        mnu_ITTEUCFileUpload.Visible = true;
                        mnu_ITTEUCFileUpload.Disabled = false;
                    }
                    if (menuName == mnuapistatusIRMITT.InnerHtml)
                    {
                        mnuapistatusIRMITT.Visible = true;
                        mnuapistatusIRMITT.Disabled = false;
                    }
                }
                #endregion

                #region EDPMS Module
                if (hdnModuleID.Value == "EDPMS")
                {

                    //----Trasaction-----------------
                    if (menuName == mnu_payextn.InnerHtml)
                    {
                        mnu_payextn.Visible = true;
                        mnu_payextn.Disabled = false;
                    }

                    if (menuName == mnuEDPMSData.InnerHtml)
                    {
                        mnuEDPMSData.Visible = true;
                        mnuEDPMSData.Disabled = false;
                    }
                    if (menuName == mnuEDPMS_BillDetails.InnerHtml)
                    {
                        mnuEDPMS_BillDetails.Visible = true;
                        mnuEDPMS_BillDetails.Disabled = false;
                    }

                    if (menuName == mnu_EDPMSDataUpdation.InnerHtml)
                    {
                        mnu_EDPMSDataUpdation.Visible = true;
                        mnu_EDPMSDataUpdation.Disabled = false;
                    }

                    if (menuName == mnu_adj.InnerHtml)
                    {
                        mnu_adj.Visible = true;
                        mnu_adj.Disabled = false;
                    }

                    if (menuName == mnu_EDPMS_EFirc.InnerHtml)
                    {
                        mnu_EDPMS_EFirc.Visible = true;
                        mnu_E_Firc.Visible = true;
                    }
                    //----File Creation-----------------

                    if (menuName == mnu_inwxml.InnerHtml)
                    {
                        mnu_inwxml.Visible = true;
                        mnu_inwxml.Disabled = false;
                    }

                    if (menuName == mnu_closurexml.InnerHtml)
                    {
                        mnu_closurexml.Visible = true;
                        mnu_closurexml.Disabled = false;
                    }
                    if (menuName == mnu_EDPMS_XML_Receipt.InnerHtml)
                    {
                        mnu_EDPMS_XML_Receipt.Disabled = false;
                        mnu_EDPMS_XML_Receipt.Visible = true;
                    }
                    if (menuName == mnu_EDPMS_XML_Realization.InnerHtml)
                    {
                        mnu_EDPMS_XML_Realization.Disabled = false;
                        mnu_EDPMS_XML_Realization.Visible = true;
                    }
                    if (menuName == mnu_EDPMS_AD_Transfer.InnerHtml)
                    {
                        mnu_EDPMS_AD_Transfer.Visible = true;
                        mnu_EDPMS_AD_Transfer.Disabled = false;
                    }
                    if (menuName == mnu_pmtextfilecrn.InnerHtml)
                    {
                        mnu_pmtextfilecrn.Visible = true;
                        mnu_pmtextfilecrn.Disabled = false;
                    }
                    if (menuName == mnuE_Firc_Closure_Xml.InnerHtml)
                    {
                        mnuE_Firc_Closure_Xml.Visible = true;
                        mnuE_Firc_Closure_Xml.Disabled = false;
                    }

                    if (menuName == mnuDataTransfer.InnerHtml)
                    {
                        mnuDataTransfer.Visible = true;
                        mnuDataTransfer.Disabled = false;
                    }
                    if (menuName == mnu_roddataadtrans.InnerHtml)
                    {
                        mnu_roddataadtrans.Visible = true;
                        mnu_roddataadtrans.Disabled = false;
                    }

                    if (menuName == mnu_AckReal.InnerHtml)
                    {
                        mnu_AckReal.Visible = true;
                        mnu_AckReal.Disabled = false;
                    }
                    //---Reports------------------------------

                    if (menuName == mnu_InwRem_Utilization.InnerHtml)
                    {
                        mnu_InwRem_Utilization.Visible = true;
                        mnu_InwRem_Utilization.Disabled = false;
                    }
                    if (menuName == mnuExpbillPendingsAckEDPMS.InnerHtml)
                    {
                        mnuExpbillPendingsAckEDPMS.Visible = true;
                        mnuExpbillPendingsAckEDPMS.Disabled = false;
                    }
                    if (menuName == mnu_EDPMS_Datachecklist_DocReceipt.InnerHtml)
                    {
                        mnu_EDPMS_Datachecklist_DocReceipt.Visible = true;
                        mnu_EDPMS_Datachecklist_DocReceipt.Disabled = false;
                    }
                    if (menuName == mnu_EDPMS_Datachecklist_Realized.InnerHtml)
                    {
                        mnu_EDPMS_Datachecklist_Realized.Visible = true;
                        mnu_EDPMS_Datachecklist_Realized.Disabled = false;
                    }

                    if (menuName == mnu_EDPMS_XML_FilePending.InnerHtml)
                    {
                        mnu_EDPMS_XML_FilePending.Visible = true;
                        mnu_EDPMS_XML_FilePending.Disabled = false;
                    }
                    if (menuName == mnu_Receipt_AckReport.InnerHtml)
                    {
                        mnu_Receipt_AckReport.Visible = true;
                        mnu_Receipt_AckReport.Disabled = false;
                    }
                    if (menuName == mnu_Realization_AckReport.InnerHtml)
                    {
                        mnu_Realization_AckReport.Visible = true;
                        mnu_Realization_AckReport.Disabled = false;
                    }
                    if (menuName == Mnu_EDPMS_E_FIRC.InnerHtml)
                    {
                        Mnu_EDPMS_E_FIRC.Visible = true;
                        Mnu_EDPMS_E_FIRC.Disabled = false;
                    }
                    //-------File upload-------------------------

                    if (menuName == mFu_EDPMS_Receipt.InnerHtml)
                    {
                        mFu_EDPMS_Receipt.Visible = true;
                        mFu_EDPMS_Receipt.Disabled = false;
                    }
                    if (menuName == mFu_PaymentRealization.InnerHtml)
                    {
                        mFu_PaymentRealization.Visible = true;
                        mFu_PaymentRealization.Disabled = false;
                    }
                    if (menuName == mnu_adtransack.InnerHtml)
                    {
                        mnu_adtransack.Visible = true;
                        mnu_adtransack.Disabled = false;
                    }
                    if (menuName == mnu_Reciept_Ack.InnerHtml)
                    {
                        mnu_Reciept_Ack.Visible = true;
                        mnu_Reciept_Ack.Disabled = false;
                    }
                    if (menuName == mnu_Realization_Ack.InnerHtml)
                    {
                        mnu_Realization_Ack.Visible = true;
                        mnu_Realization_Ack.Disabled = false;
                    }
                    if (menuName == mnu_dump.InnerHtml)
                    {
                        mnu_dump.Visible = true;
                        mnu_dump.Disabled = false;
                    }
                    if (menuName == mnu_pmtextnack.InnerHtml)
                    {
                        mnu_pmtextnack.Visible = true;
                        mnu_pmtextnack.Disabled = false;
                    }

                    if (menuName == mnu_EFIRC_Closure_File_Upload.InnerHtml)
                    {
                        mnu_EFIRC_Closure_File_Upload.Visible = true;
                        mnu_EFIRC_Closure_File_Upload.Disabled = false;
                    }
                    if (menuName == mnu_EFIRC_Old_Closure_File_Upload.InnerHtml)
                    {
                        mnu_EFIRC_Old_Closure_File_Upload.Visible = true;
                        mnu_EFIRC_Old_Closure_File_Upload.Disabled = false;
                    }
                    if (menuName == mnuapistatusorm.InnerHtml)
                    {
                        mnuapistatusorm.Visible = true;
                        mnuapistatusorm.Disabled = false;
                    }
                    //------File Format----------
                    if (menuName == mnu_E_FIRC_Clr_Format.InnerHtml)
                    {
                        mnu_E_FIRC_Clr_Format.Visible = true;
                        mnu_E_FIRC_Clr_Format.Disabled = false;
                    }
                    //---Audit trail---------------------------
                    //if (menuName == mnuEDPMSAuditTrail.InnerHtml)
                    //{
                    //    mnuEDPMSAuditTrail.Visible = true;
                    //    mnuEDPMSAuditTrail.Disabled = false;
                    //}
                }
                #endregion

                #region IDPMS Module
                if (hdnModuleID.Value == "IDPMS")
                {
                    //------------Transactions------------------------

                    if (menuName == mnu_AddEditBOE.InnerHtml)
                    {
                        mnu_AddEditBOE.Visible = true;
                        mnu_AddEditBOE.Disabled = false;
                    }

                    if (menuName == mnuAddEditManualBoe.InnerHtml)
                    {
                        mnuAddEditManualBoe.Visible = true;
                        mnuAddEditManualBoe.Disabled = false;
                    }
                    if (menuName == mnu_OtherAdBOE.InnerHtml)
                    {
                        mnu_OtherAdBOE.Visible = true;
                        mnu_OtherAdBOE.Disabled = false;
                    }
                    if (menuName == mnu_AddEditPEX.InnerHtml)
                    {
                        mnu_AddEditPEX.Visible = true;
                        mnu_AddEditPEX.Disabled = false;
                    }
                    if (menuName == mnuORM_Closure.InnerHtml)
                    {
                        mnuORM_Closure.Visible = true;
                        mnuORM_Closure.Disabled = false;
                    }
                    if (menuName == mnu_BOEClosure.InnerHtml)
                    {
                        mnu_BOEClosure.Visible = true;
                        mnu_BOEClosure.Disabled = false;
                    }
                    if (menuName == mnu_BOE_Sett_View.InnerHtml)
                    {
                        mnu_BOE_Sett_View.Visible = true;
                        mnu_BOE_Sett_View.Disabled = false;
                    }
                    if (menuName == mnuBOECancel.InnerHtml)
                    {
                        mnuBOECancel.Visible = true;
                        mnuBOECancel.Disabled = false;

                    }
                    //-------------FILE CREATION-----------//

                    if (menuName == mnu_ORMXMLCreation.InnerHtml)
                    {
                        mnu_ORMXMLCreation.Visible = true;
                        mnu_ORMXMLCreation.Disabled = false;
                    }

                    if (menuName == mnu_ManFile.InnerHtml)
                    {
                        mnu_ManFile.Visible = true;
                        mnu_ManFile.Disabled = false;
                    }

                    if (menuName == mnu_otherADcsvcre.InnerHtml)
                    {
                        mnu_otherADcsvcre.Visible = true;
                        mnu_otherADcsvcre.Disabled = false;
                    }

                    if (menuName == mnu_PaySetCre.InnerHtml)
                    {
                        mnu_PaySetCre.Visible = true;
                        mnu_PaySetCre.Disabled = false;
                    }
                    if (menuName == mnu_PayExt.InnerHtml)
                    {
                        mnu_PayExt.Visible = true;
                        mnu_PayExt.Disabled = false;
                    }
                    if (menuName == mnuORM_XMl.InnerHtml)
                    {
                        mnuORM_XMl.Visible = true;
                        mnuORM_XMl.Disabled = false;
                    }
                    if (menuName == mnuBOEClosure.InnerHtml)
                    {
                        mnuBOEClosure.Visible = true;
                        mnuBOEClosure.Disabled = false;
                    }
                    if (menuName == mnu_CustomerMaster_CSV_file.InnerHtml)
                    {
                        mnu_CustomerMaster_CSV_file.Visible = true;
                        mnu_CustomerMaster_CSV_file.Disabled = false;
                    }

                    //-------------Reports-----------------//

                    if (menuName == mnu_ORMChecklist.InnerHtml)
                    {
                        mnu_ORMChecklist.Visible = true;
                        mnu_ORMChecklist.Disabled = false;
                    }
                    if (menuName == mnu_BOEDataCheck.InnerHtml)
                    {
                        mnu_BOEDataCheck.Visible = true;
                        mnu_BOEDataCheck.Disabled = false;
                    }
                    if (menuName == mnu_ORMAckReport.InnerHtml)
                    {
                        mnu_ORMAckReport.Visible = true;
                        mnu_ORMAckReport.Disabled = false;
                    }
                    if (menuName == mnuManualBOEAck.InnerHtml)
                    {
                        mnuManualBOEAck.Visible = true;
                        mnuManualBOEAck.Disabled = false;
                    }
                    if (menuName == mnuBOEOSRPT.InnerHtml)
                    {
                        mnuBOEOSRPT.Visible = true;
                        mnuBOEOSRPT.Disabled = false;
                    }
                    if (menuName == mnurptORMOS.InnerHtml)
                    {
                        mnurptORMOS.Visible = true;
                        mnurptORMOS.Disabled = false;
                    }
                    if (menuName == mnuBOEPenPayExt.InnerHtml)
                    {
                        mnuBOEPenPayExt.Visible = true;
                        mnuBOEPenPayExt.Disabled = false;
                    }
                    if (menuName == mnuBOEack.InnerHtml)
                    {
                        mnuBOEack.Visible = true;
                        mnuBOEack.Disabled = false;
                    }
                    if (menuName == mnuTracerLetter.InnerHtml)
                    {
                        mnuTracerLetter.Visible = true;
                        mnuTracerLetter.Disabled = false;
                    }
                    if (menuName == mnuPayExtDone.InnerHtml)
                    {
                        mnuPayExtDone.Visible = true;
                        mnuPayExtDone.Disabled = false;
                    }
                    if (menuName == mnuDmpCust.InnerHtml)
                    {
                        mnuDmpCust.Visible = true;
                        mnuDmpCust.Disabled = false;
                    }
                    if (menuName == mnuOrmClosure.InnerHtml)
                    {
                        mnuOrmClosure.Visible = true;
                        mnuOrmClosure.Disabled = false;
                    }
                    if (menuName == mnuBoeClosureRep.InnerHtml)
                    {
                        mnuBoeClosureRep.Visible = true;
                        mnuBoeClosureRep.Disabled = false;
                    }

                    if (menuName == mnuOtherAD.InnerHtml)
                    {
                        mnuOtherAD.Visible = true;
                        mnuOtherAD.Disabled = false;
                    }
                    if (menuName == mnu_rpt_ORM_Ack_Upload.InnerHtml)
                    {
                        mnu_rpt_ORM_Ack_Upload.Visible = true;
                        mnu_rpt_ORM_Ack_Upload.Disabled = false;
                    }
                    if (menuName == mnu_rpt_ORM_Ack_NotUpload.InnerHtml)
                    {
                        mnu_rpt_ORM_Ack_NotUpload.Visible = true;
                        mnu_rpt_ORM_Ack_NotUpload.Disabled = false;
                    }
                    //-------------FILE Upload-----------//

                    if (menuName == mnu_ORMFileupload.InnerHtml)
                    {
                        mnu_ORMFileupload.Visible = true;
                        mnu_ORMFileupload.Disabled = false;
                    }
                    if (menuName == mnu_idpms_dump.InnerHtml)
                    {
                        mnu_idpms_dump.Visible = true;
                        mnu_idpms_dump.Disabled = false;
                    }
                    if (menuName == mnu_BOE_settlement_Upl.InnerHtml)
                    {
                        mnu_BOE_settlement_Upl.Visible = true;
                        mnu_BOE_settlement_Upl.Disabled = false;
                    }
                    if (menuName == mnu_Other_AD_Upl.InnerHtml)
                    {
                        mnu_Other_AD_Upl.Visible = true;
                        mnu_Other_AD_Upl.Disabled = false;
                    }
                    if (menuName == mnu_Manual_BOE_Upl.InnerHtml)
                    {
                        mnu_Manual_BOE_Upl.Visible = true;
                        mnu_Manual_BOE_Upl.Disabled = false;
                    }
                    if (menuName == mnu_Pay_Ext_Upl.InnerHtml)
                    {
                        mnu_Pay_Ext_Upl.Visible = true;
                        mnu_Pay_Ext_Upl.Disabled = false;
                    }
                    if (menuName == mnu_ORMAck.InnerHtml)
                    {
                        mnu_ORMAck.Visible = true;
                        mnu_ORMAck.Disabled = false;
                    }
                    if (menuName == mnu_PaySet.InnerHtml)
                    {
                        mnu_PaySet.Visible = true;
                        mnu_PaySet.Disabled = false;
                    }
                    if (menuName == mnu_ORMClosureUpload.InnerHtml)
                    {
                        mnu_ORMClosureUpload.Visible = true;
                        mnu_ORMClosureUpload.Disabled = false;
                    }
                    if (menuName == mnu_BOEClosureUpload.InnerHtml)
                    {
                        mnu_BOEClosureUpload.Visible = true;
                        mnu_BOEClosureUpload.Disabled = false;
                    }
                    //--------File Format------------------------------------------

                    if (menuName == mnu_ORMFORMAT.InnerHtml)
                    {
                        mnu_ORMFORMAT.Visible = true;
                        mnu_ORMFORMAT.Disabled = false;
                    }
                    if (menuName == mnu_BOE_settlement_Upl_Format.InnerHtml)
                    {
                        mnu_BOE_settlement_Upl_Format.Visible = true;
                        mnu_BOE_settlement_Upl_Format.Disabled = false;
                    }
                    if (menuName == mnu_Other_AD_Upl_Format.InnerHtml)
                    {
                        mnu_Other_AD_Upl_Format.Visible = true;
                        mnu_Other_AD_Upl_Format.Disabled = false;
                    }
                    if (menuName == mnu_Manual_BOE_Upl_Format.InnerHtml)
                    {
                        mnu_Manual_BOE_Upl_Format.Visible = true;
                        mnu_Manual_BOE_Upl_Format.Disabled = false;
                    }
                    if (menuName == mnu_Pay_Ext_Upl_Format.InnerHtml)
                    {
                        mnu_Pay_Ext_Upl_Format.Visible = true;
                        mnu_Pay_Ext_Upl_Format.Disabled = false;
                    }
                    if (menuName == mnu_ORM_Clr_Format.InnerHtml)
                    {
                        mnu_ORM_Clr_Format.Visible = true;
                        mnu_ORM_Clr_Format.Disabled = false;
                    }
                    if (menuName == mnu_BOE_Clr_Format.InnerHtml)
                    {
                        mnu_BOE_Clr_Format.Visible = true;
                        mnu_BOE_Clr_Format.Disabled = false;
                    }
                    //---Audit trail---------------------------
                    //if (menuName == mnuIDPMSAuditTrail.InnerHtml)
                    //{
                    //    mnuIDPMSAuditTrail.Visible = true;
                    //    mnuIDPMSAuditTrail.Disabled = false;
                    //}
                }
                #endregion

                #region RReturn Module
                if (hdnModuleID.Value == "R-Return")
                {
                    //------------Transactions------------------------
                    if (menuName == mnu_RReturn.InnerHtml)
                    {
                        mnu_RReturn.Visible = true;
                        mnu_RReturn.Disabled = false;
                    }
                    if (menuName == mnu_Nostro.InnerHtml)
                    {
                        mnu_Nostro.Visible = true;
                        mnu_Nostro.Disabled = false;
                    }
                    if (menuName == mnu_Vostro.InnerHtml)
                    {
                        mnu_Vostro.Visible = true;
                        mnu_Vostro.Disabled = false;
                    }
                    //---------------- File Creation ----------------------
                    if (menuName == mnu_ret_txtFileCreation.InnerHtml)
                    {
                        mnu_ret_txtFileCreation.Visible = true;
                        mnu_ret_txtFileCreation.Disabled = false;
                    }
                    if (menuName == mnu_ret_DataCsvforCheck.InnerHtml)
                    {
                        mnu_ret_DataCsvforCheck.Visible = true;
                        mnu_ret_DataCsvforCheck.Disabled = false;
                    }
                    if (menuName == mnuRET_RBITextFileAtHeadOffice.InnerHtml)
                    {
                        if (Session["userADCode"].ToString() == "6770001")
                        {
                            mnuRET_RBITextFileAtHeadOffice.Visible = true;
                            mnuRET_RBITextFileAtHeadOffice.Disabled = false;
                        }
                        else
                        {
                            mnuRET_RBITextFileAtHeadOffice.Visible = false;
                            mnuRET_RBITextFileAtHeadOffice.Disabled = true;
                        }
                    }
                    if (menuName == mnu_Ret_CBTR_CSV_File_GENERATE.InnerHtml)
                    {
                        mnu_Ret_CBTR_CSV_File_GENERATE.Visible = true;
                        mnu_Ret_CBTR_CSV_File_GENERATE.Disabled = false;
                    }
                    //------------------REPORTS--------------------------------------
                    if (menuName == mnu_RReturnDataCheckList.InnerHtml)
                    {
                        mnu_RReturnDataCheckList.Visible = true;
                        mnu_RReturnDataCheckList.Disabled = false;
                    }
                    if (menuName == mnu_RReturnDataValidation.InnerHtml)
                    {
                        mnu_RReturnDataValidation.Visible = true;
                        mnu_RReturnDataValidation.Disabled = false;
                    }
                    if (menuName == mnu_RRETURN_DataStatistics.InnerHtml)
                    {
                        mnu_RRETURN_DataStatistics.Visible = true;
                        mnu_RRETURN_DataStatistics.Disabled = false;
                    }
                    if (menuName == mnu_RReturnCoverPage.InnerHtml)
                    {
                        mnu_RReturnCoverPage.Visible = true;
                        mnu_RReturnCoverPage.Disabled = false;
                    }
                    if (menuName == mnu_NostroReport.InnerHtml)
                    {
                        mnu_NostroReport.Visible = true;
                        mnu_NostroReport.Disabled = false;
                    }
                    if (menuName == mnu_RReturnVostroReport.InnerHtml)
                    {
                        mnu_RReturnVostroReport.Visible = true;
                        mnu_RReturnVostroReport.Disabled = false;
                    }
                    if (menuName == mnu_RReturnPurposeTotals.InnerHtml)
                    {
                        mnu_RReturnPurposeTotals.Visible = true;
                        mnu_RReturnPurposeTotals.Disabled = false;
                    }

                    if (menuName == mnu_ConsolRReturnNostroReport.InnerHtml)
                    {
                        if (Session["userADCode"].ToString() == "6770001")
                        {
                            mnu_ConsolRReturnNostroReport.Visible = true;
                            mnu_ConsolRReturnNostroReport.Disabled = false;
                        }
                        else
                        {
                            mnu_ConsolRReturnNostroReport.Visible = false;
                            mnu_ConsolRReturnNostroReport.Disabled = true;
                        }
                    }
                    if (menuName == mnu_ConsolRreturnVostroReport.InnerHtml)
                    {
                        if (Session["userADCode"].ToString() == "6770001")
                        {
                            mnu_ConsolRreturnVostroReport.Visible = true;
                            mnu_ConsolRreturnVostroReport.Disabled = false;
                        }
                        else
                        {
                            mnu_ConsolRreturnVostroReport.Visible = false;
                            mnu_ConsolRreturnVostroReport.Disabled = true;
                        }
                    }
                    //------------------File Upload--------------------------------
                    if (menuName == mFU_RET_CSV.InnerHtml)
                    {
                        mFU_RET_CSV.Visible = true;
                        mFU_RET_CSV.Disabled = false;
                    }

                    if (menuName == mnu_ConsolidateCSV.InnerHtml)
                    {
                        if (Session["userADCode"].ToString() == "6770001")
                        {
                            mnu_ConsolidateCSV.Visible = true;
                            mnu_ConsolidateCSV.Disabled = false;
                        }
                        else
                        {
                            mnu_ConsolidateCSV.Visible = false;
                            mnu_ConsolidateCSV.Disabled = true;
                        }
                    }
                    //--------------------File Format--------------------------------
                    if (menuName == mnu_uploadformt.InnerHtml)
                    {
                        mnu_uploadformt.Visible = true;
                        mnu_uploadformt.Disabled = false;
                    }

                }
                #endregion
            }
        }
    }

    protected void btnHome_Click(object sender, EventArgs e)
    {
        if (hdnModuleID.Value != null)
        {
            string Module = hdnModuleID.Value;

            if (Module == "INW")
            {
                Response.Redirect("~/INW/INW_Main.aspx", true);

            }
            if (Module == "PCFC")
            {
                Response.Redirect("~/PC/PC_Main.aspx", true);

            }
            if (Module == "EXPLC")
            {
                Response.Redirect("~/EXPLC/EXPLC_Main.aspx", true);

            }
            if (Module == "OUT")
            {
                Response.Redirect("~/OUT/OUT_Main.aspx", true);

            }
            if (Module == "IMP")
            {
                Response.Redirect("~/IMP/IMP_Main.aspx", true);

            }
            if (Module == "EXP")
            {
                Response.Redirect("~/EXP/EXP_Main.aspx", true);

            }
            if (Module == "GTE")
            {
                Response.Redirect("~/GTE/GTE_Main.aspx", true);

            }
            if (Module == "FWD")
            {
                Response.Redirect("~/FWD/FWD_Main.aspx", true);

            }
            if (Module == "EBR")
            {
                Response.Redirect("~/EBR/EBR_Main.aspx", true);

            }
        }
    }

    protected void btnHousekeeping_Click(object sender, EventArgs e)
    {
        if (Session["userRole"] == null || Session["userRole"].ToString().Trim() != "Admin")
            Response.Redirect("~/TF_HouseKeeping.aspx", true);
        else
            Response.Redirect("~/TF_ViewHouseKeeping.aspx", true);
    }

    protected string QuotedTimeOutUrl
    {
        get { return '"' + ResolveClientUrl("~/TF_Login.aspx?sessionout=yes&sessionid=" + hdnloginid.Value) + '"'; }
    }

}