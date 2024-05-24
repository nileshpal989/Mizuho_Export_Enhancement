using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using System.IO;
using OfficeOpenXml;
using ClosedXML.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using ExcelAutoFormat = Microsoft.Office.Interop.Excel.XlRangeAutoFormat;

public partial class IMP_Transactions_TF_IMP_Ledger_Modification_ICA_ICU_IBA_Checker : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("../../TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            hdnUserName.Value = Session["userName"].ToString();
            if (!IsPostBack)
            {
                if (Request.QueryString["DocNo"] == null)
                {
                    Response.Redirect("TF_IMP_Ledger_Modification_ICA_ICU_IBA_Checker_View.aspx", true);
                }
                else
                {
                    hdnUserName.Value = Session["userName"].ToString();
                    hdnBranchName.Value = Request.QueryString["BranchName"].Trim();
                    hdnDocType.Value = Request.QueryString["DocType"].Trim();
                    hdnDocScrutiny.Value = Request.QueryString["Document_Scrutiny"].Trim();
                    txtDocNo.Text = Request.QueryString["DocNo"].Trim();
                    Fill_Logd_Details();
                    Fill_LegerModification();
                    if (Request.QueryString["Status"].ToString() == "Approved By Checker")
                    {
                        ddlApproveReject.Enabled = false;
                    }

                }
            }
            ddlApproveReject.Attributes.Add("onchange", "return DialogAlert();");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        string AR = "";
        if (ddlApproveReject.SelectedIndex == 1)
        {
            AR = "A";
            GBaseFileCreationLedger();
            CreateSwiftSFMSFile();
        }
        if (ddlApproveReject.SelectedIndex == 2)
        {
            AR = "R";
        }
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
        SqlParameter P_Status = new SqlParameter("@Status", AR);
        SqlParameter P_RejectReason = new SqlParameter("@RejectReason", hdnRejectReason.Value.Trim());
        string _script = "", Result = "";
        Result = obj.SaveDeleteData("TF_IMP_ChekerApproveRejectLedgerModification", P_DocNo, P_Status, P_RejectReason);
        _script = "TF_IMP_Ledger_Modification_ICA_ICU_IBA_Checker_View.aspx?DocNo=" + Result.Substring(7) + "&AR=" + AR;
        Response.Redirect(_script, true);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_IMP_Ledger_Modification_ICA_ICU_IBA_Checker_View.aspx", true);
    }
    protected void Fill_Logd_Details()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
        DataTable dt = new DataTable();
        dt = obj.getData("TF_IMP_Get_BOE_Details_LegerModification_ICA_ICU_IBA", PDocNo);
        if (dt.Rows.Count > 0)
        {
            hdnDocNo.Value = dt.Rows[0]["Document_No"].ToString();
            hdnNegoRemiBankType.Value = dt.Rows[0]["Nego_Remit_Bank_Type"].ToString();
            txtLedgerRefNo.Text = dt.Rows[0]["Document_No"].ToString();
            ToggleDocType(dt.Rows[0]["Document_Type"].ToString(), dt.Rows[0]["Document_FLC_ILC"].ToString());
            hdn_FLC_ILC.Value = dt.Rows[0]["Document_FLC_ILC"].ToString();
            hdnDocScrutiny.Value = dt.Rows[0]["Document_Scrutiny"].ToString();
            lblDoc_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            lblBillAmt.Text = dt.Rows[0]["Bill_Amount"].ToString();
        }
    }
    protected void Fill_LegerModification()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
        DataTable dt = new DataTable();
        dt = obj.getData("TF_IMP_Get_Ledger_Modification_ICA_IBA_ICU", PDocNo);
        if (dt.Rows.Count > 0)
        {
            txtValueDate.Text = dt.Rows[0]["Acceptance_Value_Date"].ToString();
            //Fill_LegerModification
            txtLedgerRemark.Text = dt.Rows[0]["Ledger_Remark"].ToString();
            txtLedgerCustName.Text = dt.Rows[0]["Ledger_Cust_AccNo"].ToString();
            txtLedgerCURR.Text = dt.Rows[0]["Ledger_Curr"].ToString();
            txtLedgerAccode.Text = dt.Rows[0]["Ledger_AccCode"].ToString();
            txtLedgerRefNo.Text = dt.Rows[0]["Ledger_Ref_No"].ToString();
            txtLedger_Modify_amt.Text = dt.Rows[0]["Ledger_Amount"].ToString();
            txtLedgerOperationDate.Text = dt.Rows[0]["Ledger_Operation_Date"].ToString();
            txtLedgerBalanceAmt.Text = dt.Rows[0]["Ledger_Balance_Amt"].ToString();
            txtLedgerAcceptDate.Text = dt.Rows[0]["Ledger_Accept_Date"].ToString();
            txtLedgerMaturity.Text = dt.Rows[0]["Ledger_Maturity"].ToString();
            txtLedgerSettlememtDate.Text = dt.Rows[0]["Ledger_Settlement_Date"].ToString();
            txtLedgerSettlValue.Text = dt.Rows[0]["Ledger_Settlement_Value"].ToString();
            txtLedgerLastModDate.Text = dt.Rows[0]["Ledger_Last_Mod_Date"].ToString();
            txtLedgerREM_EUC.Text = dt.Rows[0]["Ledger_Rem_EUC"].ToString();
            txtLedgerLastOPEDate.Text = dt.Rows[0]["Ledger_Last_OPE_Date"].ToString();
            txtLedgerTransNo.Text = dt.Rows[0]["Ledger_Trans_No"].ToString();
            txtLedgerContraCountry.Text = dt.Rows[0]["Ledger_Contra_Country"].ToString();
            txtLedgerStatusCode.Text = dt.Rows[0]["Ledger_Status_Code"].ToString();
            txtLedgerCollectOfComm.Text = dt.Rows[0]["Ledger_Collect_Of_Cumm"].ToString();
            txtLedgerCommodity.Text = dt.Rows[0]["Ledger_Commodity"].ToString();
            txtLedgerhandlingCommRate.Text = dt.Rows[0]["Ledger_Handling_Comm_Rate"].ToString();
            txtLedgerhandlingCommCurr.Text = dt.Rows[0]["Ledger_Handling_Comm_Curr"].ToString();
            txtLedgerhandlingCommAmt.Text = dt.Rows[0]["Ledger_Handling_Comm_Amt"].ToString();
            txtLedgerPostageRate.Text = dt.Rows[0]["Ledger_Postage_Rate"].ToString();
            txtLedgerPostageCurr.Text = dt.Rows[0]["Ledger_Postage_Curr"].ToString();
            txtLedgerPostageAmt.Text = dt.Rows[0]["Ledger_Postage_Amt"].ToString();
            txtLedgerPostagePayer.Text = dt.Rows[0]["Ledger_Postage_Payer"].ToString();
            txtLedgerOthersRate.Text = dt.Rows[0]["Ledger_Others_Rate"].ToString();
            txtLedgerOthersCurr.Text = dt.Rows[0]["Ledger_Others_Curr"].ToString();
            txtLedgerOthersAmt.Text = dt.Rows[0]["Ledger_Others_Amt"].ToString();
            txtLedgerOthersPayer.Text = dt.Rows[0]["Ledger_Others_Payer"].ToString();
            txtLedgerTheirCommRate.Text = dt.Rows[0]["Ledger_Their_Comm_Rate"].ToString();
            txtLedgerTheirCommCurr.Text = dt.Rows[0]["Ledger_Their_Comm_Curr"].ToString();
            txtLedgerTheirCommAmt.Text = dt.Rows[0]["Ledger_Their_Comm_Amt"].ToString();
            txtLedgerTheirCommPayer.Text = dt.Rows[0]["Ledger_Their_Comm_Payer"].ToString();
            txtLedgerNegoBank.Text = dt.Rows[0]["Ledger_Collect_Nego_Bank"].ToString();
            txtLedgerReimbursingBank.Text = dt.Rows[0]["Ledger_Reimbursing_Bank"].ToString();
            txtLedgerDrawer.Text = dt.Rows[0]["Ledger_Drawer_Drawee"].ToString();
            txtLedgerTenor.Text = dt.Rows[0]["Ledger_Tenor"].ToString();
            txtLedgerAttn.Text = dt.Rows[0]["Ledger_Attn"].ToString();

            //// swift files
            if (dt.Rows[0]["None_Flag"].ToString() == "Y")
            {
                rdb_swift_None.Checked = true;
            }
            else
            {
                rdb_swift_None.Checked = false;
            }
            if (dt.Rows[0]["MT499_Flag"].ToString() == "Y")
            {
                rdb_swift_499.Checked = true;
            }
            else
            {
                rdb_swift_499.Checked = false;
            }

            if (dt.Rows[0]["MT799_Flag"].ToString() == "Y")
            {
                rdb_swift_799.Checked = true;
            }
            else
            {
                rdb_swift_799.Checked = false;
            }

            if (dt.Rows[0]["MT999_Flag"].ToString() == "Y")
            {
                rdb_swift_999.Checked = true;
            }
            else
            {
                rdb_swift_999.Checked = false;
            }

            if (dt.Rows[0]["MT499_Flag"].ToString() == "Y" || dt.Rows[0]["MT799_Flag"].ToString() == "Y" || dt.Rows[0]["MT999_Flag"].ToString() == "Y")
            {
                Narrative1.Text = dt.Rows[0]["Narrative1"].ToString();
                Narrative2.Text = dt.Rows[0]["Narrative2"].ToString();
                Narrative3.Text = dt.Rows[0]["Narrative3"].ToString();
                Narrative4.Text = dt.Rows[0]["Narrative4"].ToString();
                Narrative5.Text = dt.Rows[0]["Narrative5"].ToString();
                Narrative6.Text = dt.Rows[0]["Narrative6"].ToString();
                Narrative7.Text = dt.Rows[0]["Narrative7"].ToString();
                Narrative8.Text = dt.Rows[0]["Narrative8"].ToString();
                Narrative9.Text = dt.Rows[0]["Narrative9"].ToString();
                Narrative10.Text = dt.Rows[0]["Narrative10"].ToString();
                Narrative11.Text = dt.Rows[0]["Narrative11"].ToString();
                Narrative12.Text = dt.Rows[0]["Narrative12"].ToString();
                Narrative13.Text = dt.Rows[0]["Narrative13"].ToString();
                Narrative14.Text = dt.Rows[0]["Narrative14"].ToString();
                Narrative15.Text = dt.Rows[0]["Narrative15"].ToString();
                Narrative16.Text = dt.Rows[0]["Narrative16"].ToString();
                Narrative17.Text = dt.Rows[0]["Narrative17"].ToString();
                Narrative18.Text = dt.Rows[0]["Narrative18"].ToString();
                Narrative19.Text = dt.Rows[0]["Narrative19"].ToString();
                Narrative20.Text = dt.Rows[0]["Narrative20"].ToString();
                Narrative21.Text = dt.Rows[0]["Narrative21"].ToString();
                Narrative22.Text = dt.Rows[0]["Narrative22"].ToString();
                Narrative23.Text = dt.Rows[0]["Narrative23"].ToString();
                Narrative24.Text = dt.Rows[0]["Narrative24"].ToString();
                Narrative25.Text = dt.Rows[0]["Narrative25"].ToString();
                Narrative26.Text = dt.Rows[0]["Narrative26"].ToString();
                Narrative27.Text = dt.Rows[0]["Narrative27"].ToString();
                Narrative28.Text = dt.Rows[0]["Narrative28"].ToString();
                Narrative29.Text = dt.Rows[0]["Narrative29"].ToString();
                Narrative30.Text = dt.Rows[0]["Narrative30"].ToString();
                Narrative31.Text = dt.Rows[0]["Narrative31"].ToString();
                Narrative32.Text = dt.Rows[0]["Narrative32"].ToString();
                Narrative33.Text = dt.Rows[0]["Narrative33"].ToString();
                Narrative34.Text = dt.Rows[0]["Narrative34"].ToString();
                Narrative35.Text = dt.Rows[0]["Narrative35"].ToString();
            }
            hdnRejectReason.Value = dt.Rows[0]["Checker_Remark"].ToString();
        }
    }
    private void ToggleDocType(string DocType, string DocFLC_ILC_Type)
    {
        string Foreign_Local = "";
        switch (DocType)
        {
            case "ICA": //LodgmentUnderLC_Usance
                if (DocFLC_ILC_Type == "FLC")
                {
                    Foreign_Local = "Foreign";
                }
                else if (DocFLC_ILC_Type == "ILC")
                {
                    Foreign_Local = "Local";
                }
                lblForeign_Local.Text = Foreign_Local;
                lblCollection_Lodgment_UnderLC.Text = "Collection";
                lblSight_Usance.Text = "sight";
                break;
            case "IBA": //LodgmentUnderLC_Usance
                if (DocFLC_ILC_Type == "FLC")
                {
                    Foreign_Local = "Foreign";
                }
                else if (DocFLC_ILC_Type == "ILC")
                {
                    Foreign_Local = "Local";
                }
                lblForeign_Local.Text = Foreign_Local;
                lblCollection_Lodgment_UnderLC.Text = "Lodgement_Under_LC";
                lblSight_Usance.Text = "sight";
                break;
            case "ICU": //AcceptanceLC_Usance
                if (DocFLC_ILC_Type == "FLC")
                {
                    Foreign_Local = "Foreign";
                }
                else if (DocFLC_ILC_Type == "ILC")
                {
                    Foreign_Local = "Local";
                }
                lblForeign_Local.Text = Foreign_Local;
                lblCollection_Lodgment_UnderLC.Text = "Collection";
                lblSight_Usance.Text = "Usance";
                break;
        }
    }
    public void GBaseFileCreationLedger()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter PRefNo = new SqlParameter("@REFNo", SqlDbType.VarChar);
        PRefNo.Value = txtDocNo.Text.Trim();
        DataTable dt = objData1.getData("TF_IMP_GBaseFileCreation_ICU_ICA_IBA", PRefNo);
        string TodayDate = DateTime.Now.ToString("dd/MM/yyyy");
        string MTodayDate = "";
        if (TodayDate.Contains("/"))
        {
            MTodayDate = TodayDate.Replace("/", "");
        }
        if (TodayDate.Contains("-"))
        {
            MTodayDate = TodayDate.Replace("-", "");
        }
        if (dt.Rows.Count > 0)
        {
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/LedgerModification/LEDGER/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = _directoryPath + "/" + txtDocNo.Text.Trim() + "_Ledger" + ".xlsx";
            if (dt.Rows.Count > 0)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var sheet1 = wb.Worksheets.Add(dt, "Worksheet");
                    sheet1.Table("Table1").ShowAutoFilter = false;
                    sheet1.Table("Table1").Theme = XLTableTheme.None;
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        FileStream file = new FileStream(_filePath, FileMode.Create, FileAccess.Write);
                        MyMemoryStream.WriteTo(file);
                        file.Close();
                        MyMemoryStream.Close();
                    }
                }
                TF_DATA objserverName = new TF_DATA();
                string _serverName = objserverName.GetServerName();
                string path = "file://" + _serverName + "/Mizuho_UAT/TF_GeneratedFiles/IMPORT/";
            }
        }
        else
        {
        }
    }
    public void CreateSwiftSFMSFile()
    {
        string FileType = "";
        if (hdnNegoRemiBankType.Value == "FOREIGN")
        {
            FileType = "SWIFT";
            if (rdb_swift_499.Checked)
            {
                //CreateSwift499(FileType);
                CreateSwift499_XML("SWIFT");
                InsertDataForSwiftSTP("MT499LM");
            }
            if (rdb_swift_799.Checked)
            {
                //CreateSwift799("FileType");
                CreateSwift799_XML("SWIFT");
                InsertDataForSwiftSTP("MT799LM");
            }
            if (rdb_swift_999.Checked)
            {
                //CreateSwift999(FileType);
                CreateSwift999_XML("SWIFT");
                InsertDataForSwiftSTP("MT999LM");
            }
        }
        else
        {
            FileType = "SFMS";
            if (rdb_swift_499.Checked)
            {
                CreateSwift499(FileType);
            }
            if (rdb_swift_799.Checked)
            {
                CreateSwift799(FileType);
            }
            if (rdb_swift_999.Checked)
            {
                CreateSwift999(FileType);
            }
        }
    }
    public void CreateSwift799(string FileType)
    {
        string _directoryPath = ""; 
        string Query = "";
        string TodayDate = DateTime.Now.ToString("dd/MM/yyyy");
        string MTodayDate = "";
        if (TodayDate.Contains("/"))
        {
            MTodayDate = TodayDate.Replace("/", "");
        }
        if (TodayDate.Contains("-"))
        {
            MTodayDate = TodayDate.Replace("-", "");
        }
        if (FileType == "SWIFT")
        {
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/LedgerModification/SWIFT/MT799/" + MTodayDate + "/");
        }
        else
        {
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/LedgerModification/SFMS/SFMS799/" + MTodayDate + "/");
        }

        Query = "TF_IMP_Ledger_Mod_Swift_MT799_FileCreation";

        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData(Query, P_DocNo);

        if (dt.Rows.Count > 0)
        {
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string _filePath = "";
            if (FileType == "SWIFT")
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT799.xlsx";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN799.xlsx";
            }
            if (dt.Rows.Count > 0)
            {

                using (ExcelPackage pck = new ExcelPackage())
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Worksheet");
                    if (FileType == "SWIFT")
                    {
                        ws.Cells["B1"].Value = "TO:";
                    }
                    else
                    {
                        ws.Cells["B1"].Value = "Receiver Address";
                    }
                    ws.Cells["B2"].Value = "Transaction Reference Number";
                    ws.Cells["A2"].Value = "[20]";
                    ws.Cells["B3"].Value = "Related Reference";
                    ws.Cells["A3"].Value = "[21]";

                    ws.Cells["C1"].Value = dt.Rows[0]["To"].ToString();
                    ws.Cells["C2"].Value = dt.Rows[0][0].ToString();
                    ws.Cells["C3"].Value = dt.Rows[0][1].ToString();

                    ws.Cells["B4"].Value = "Narrative";
                    ws.Cells["A4"].Value = "[79]";
                    int _Ecol = 4;

                    int _Tcol = 1;
                    for (int i = 0; i < 36; i++)
                    {
                        if (dt.Rows[0]["Narrative" + _Tcol].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Narrative" + _Tcol].ToString();
                            _Ecol++;
                        }
                        _Tcol++;
                    }


                    ws.Cells[ws.Dimension.Address].AutoFitColumns();

                    using (MemoryStream MS = new MemoryStream())
                    {
                        pck.SaveAs(MS);
                        FileStream file = new FileStream(_filePath, FileMode.Create, FileAccess.Write);
                        MS.WriteTo(file);
                        file.Close();
                        MS.Close();
                    }
                }
            }
        }
        else
        {
        }
    }
    public void CreateSwift499(string FileType)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Ledger_Mod_Swift_MT499_FileCreation", P_DocNo);
        string TodayDate = DateTime.Now.ToString("dd/MM/yyyy");
        string MTodayDate = "";
        if (TodayDate.Contains("/"))
        {
            MTodayDate = TodayDate.Replace("/", "");
        }
        if (TodayDate.Contains("-"))
        {
            MTodayDate = TodayDate.Replace("-", "");
        }
        if (dt.Rows.Count > 0)
        {
            string _directoryPath = "";
            if (FileType == "SWIFT")
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/LedgerModification/SWIFT/MT499/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/LedgerModification/SFMS/SFMS499/" + MTodayDate + "/");
            }
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string _filePath = "";
            if (FileType == "SWIFT")
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT499.xlsx";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN499.xlsx";
            }
            if (dt.Rows.Count > 0)
            {

                using (ExcelPackage pck = new ExcelPackage())
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Worksheet");
                    if (FileType == "SWIFT")
                    {
                        ws.Cells["B1"].Value = "TO:";
                    }
                    else
                    {
                        ws.Cells["B1"].Value = "Receiver Address";
                    }
                    ws.Cells["B2"].Value = "Transaction Reference Number";
                    ws.Cells["A2"].Value = "[20]";
                    ws.Cells["B3"].Value = "Related Reference";
                    ws.Cells["A3"].Value = "[21]";

                    ws.Cells["C1"].Value = dt.Rows[0]["TO"].ToString();
                    ws.Cells["C2"].Value = dt.Rows[0][0].ToString();
                    ws.Cells["C3"].Value = dt.Rows[0][1].ToString();

                    ws.Cells["B4"].Value = "Narrative";
                    ws.Cells["A4"].Value = "[79]";

                    int _Tcol = 2;
                    int _Ecol = 4;

                    for (int i = 0; i < 35; i++)
                    {
                        if (dt.Rows[0][_Tcol].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0][_Tcol].ToString();
                            _Ecol++;
                        }
                        _Tcol++;
                    }
                    //ws.Cells["C" + _Ecol].Value =_Tcol;

                    ws.Cells[ws.Dimension.Address].AutoFitColumns();

                    using (MemoryStream MS = new MemoryStream())
                    {
                        pck.SaveAs(MS);
                        FileStream file = new FileStream(_filePath, FileMode.Create, FileAccess.Write);
                        MS.WriteTo(file);
                        file.Close();
                        MS.Close();
                    }
                }
            }
        }
        else
        {
        }
    }
    public void CreateSwift999(string FileType)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Ledger_Mod_Swift_MT999_FileCreation", P_DocNo);
        string TodayDate = DateTime.Now.ToString("dd/MM/yyyy");
        string MTodayDate = "";
        if (TodayDate.Contains("/"))
        {
            MTodayDate = TodayDate.Replace("/", "");
        }
        if (TodayDate.Contains("-"))
        {
            MTodayDate = TodayDate.Replace("-", "");
        }
        if (dt.Rows.Count > 0)
        {
            string _directoryPath = "";
            if (FileType == "SWIFT")
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/LedgerModification/SWIFT/MT999/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/LedgerModification/SFMS/SFMS999/" + MTodayDate + "/");
            }

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string _filePath = "";
            if (FileType == "SWIFT")
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT999.xlsx";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN999.xlsx";
            }
            if (dt.Rows.Count > 0)
            {

                using (ExcelPackage pck = new ExcelPackage())
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Worksheet");
                    if (FileType == "SWIFT")
                    {
                        ws.Cells["B1"].Value = "TO:";
                    }
                    else
                    {
                        ws.Cells["B1"].Value = "Receiver Address";
                    }
                    ws.Cells["B2"].Value = "Transaction Reference Number";
                    ws.Cells["A2"].Value = "[20]";
                    ws.Cells["B3"].Value = "Related Reference";
                    ws.Cells["A3"].Value = "[21]";

                    ws.Cells["C1"].Value = dt.Rows[0][37].ToString();
                    ws.Cells["C2"].Value = dt.Rows[0][0].ToString();
                    ws.Cells["C3"].Value = dt.Rows[0][1].ToString();

                    ws.Cells["B4"].Value = "Narrative";
                    ws.Cells["A4"].Value = "[79]";

                    int _Tcol = 2;
                    int _Ecol = 4;

                    for (int i = 0; i < 35; i++)
                    {
                        if (dt.Rows[0][_Tcol].ToString() != "")
                        {
                            ws.Cells["C" + _Ecol].Value = dt.Rows[0][_Tcol].ToString();
                            _Ecol++;
                        }
                        _Tcol++;
                    }
                    //ws.Cells["C" + _Ecol].Value = _Tcol;

                    ws.Cells[ws.Dimension.Address].AutoFitColumns();

                    using (MemoryStream MS = new MemoryStream())
                    {
                        pck.SaveAs(MS);
                        FileStream file = new FileStream(_filePath, FileMode.Create, FileAccess.Write);
                        MS.WriteTo(file);
                        file.Close();
                        MS.Close();
                    }
                }
            }
        }
        else
        {
        }
    }

    public void CreateSwift799_XML(string FileType)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Ledger_Mod_Swift_MT799_FileCreation_XML", P_DocNo);
        string TodayDate = DateTime.Now.ToString("dd/MM/yyyy");
        string TodayDate1 = DateTime.Now.ToString("ddMMyyyyhhmmss");
        string MTodayDate = "";
        if (TodayDate.Contains("/"))
        {
            MTodayDate = TodayDate.Replace("/", "");
        }
        if (TodayDate.Contains("-"))
        {
            MTodayDate = TodayDate.Replace("-", "");
        }
        if (dt.Rows.Count > 0)
        {
            string _directoryPath = "";
            if (FileType == "SWIFT")
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SWIFT_XML/LedgerModification/MT799/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SFMSXML/LedgerModification/SFMS799/" + MTodayDate + "/");
            }

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string _filePath = "";
            if (FileType == "SWIFT")
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT799_" + TodayDate1 + ".xml";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN799_" + TodayDate1 + ".xml";
            }
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            if (dt.Rows.Count > 0)
            {
                string Narrative = "";
                if (dt.Rows[0]["Narrative1"].ToString() != "")
                {
                    Narrative = Narrative + dt.Rows[0]["Narrative1"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative2"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative2"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative3"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative3"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative4"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative4"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative5"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative5"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative6"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative6"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative7"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative7"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative8"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative8"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative9"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative9"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative10"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative10"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative11"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative11"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative12"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative12"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative13"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative13"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative14"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative14"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative15"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative15"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative16"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative16"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative17"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative17"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative18"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative18"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative19"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative19"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative20"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative20"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative21"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative21"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative22"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative22"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative23"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative23"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative24"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative24"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative25"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative25"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative26"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative26"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative27"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative27"].ToString().Replace("/", "");
                }

                if (dt.Rows[0]["Narrative28"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative28"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative29"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative29"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative30"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative30"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative31"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative31"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative32"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative32"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative33"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative33"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative34"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative34"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative35"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative35"].ToString().Replace("/", "");
                }
                if (Narrative.Length != 0)
                {
                    string comma = Narrative.Trim().Substring(0, 1);
                    if (comma == ",") { Narrative = Narrative.Remove(0, 1); }
                    comma = "";
                }

                sw.WriteLine("<reqMT" + dt.Rows[0]["XMLStartTag"].ToString() + ">");
                sw.WriteLine("<Sender>" + dt.Rows[0]["Sender"].ToString() + "</Sender>");
                sw.WriteLine("<MessageType>" + dt.Rows[0]["MessageType"].ToString() + "</MessageType>");
                sw.WriteLine("<Receiver>" + dt.Rows[0]["Receiver"].ToString() + "</Receiver>");
                sw.WriteLine("<TransactionReferenceNumber>" + dt.Rows[0]["TransactionReferenceNumber"].ToString() + "</TransactionReferenceNumber>");
                sw.WriteLine("<RelatedReference>" + dt.Rows[0]["RelatedReference"].ToString() + "</RelatedReference>");
                sw.WriteLine("<Narrative>" + Narrative + "</Narrative>");
                sw.WriteLine("</reqMT>");

            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
    }
    public void CreateSwift499_XML(string FileType)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Ledger_Mod_Swift_MT499_FileCreation_XML", P_DocNo);
        string TodayDate = DateTime.Now.ToString("dd/MM/yyyy");
        string TodayDate1 = DateTime.Now.ToString("ddMMyyyyhhmmss");
        string MTodayDate = "";
        if (TodayDate.Contains("/"))
        {
            MTodayDate = TodayDate.Replace("/", "");
        }
        if (TodayDate.Contains("-"))
        {
            MTodayDate = TodayDate.Replace("-", "");
        }
        if (dt.Rows.Count > 0)
        {
            string _directoryPath = "";
            if (FileType == "SWIFT")
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SWIFT_XML/LedgerModification/MT499/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SFMSXML/LedgerModification/SFMS499/" + MTodayDate + "/");
            }

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string _filePath = "";
            if (FileType == "SWIFT")
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT499_" + TodayDate1 + ".xml";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN499_" + TodayDate1 + ".xml";
            }
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            if (dt.Rows.Count > 0)
            {

                string Narrative = "";
                if (dt.Rows[0]["Narrative1"].ToString() != "")
                {
                    Narrative = Narrative + dt.Rows[0]["Narrative1"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative2"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative2"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative3"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative3"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative4"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative4"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative5"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative5"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative6"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative6"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative7"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative7"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative8"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative8"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative9"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative9"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative10"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative10"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative11"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative11"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative12"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative12"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative13"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative13"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative14"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative14"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative15"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative15"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative16"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative16"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative17"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative17"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative18"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative18"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative19"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative19"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative20"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative20"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative21"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative21"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative22"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative22"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative23"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative23"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative24"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative24"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative25"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative25"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative26"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative26"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative27"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative27"].ToString().Replace("/", "");
                }

                if (dt.Rows[0]["Narrative28"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative28"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative29"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative29"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative30"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative30"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative31"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative31"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative32"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative32"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative33"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative33"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative34"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative34"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative35"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative35"].ToString().Replace("/", "");
                }
                if (Narrative.Length != 0)
                {
                    string comma = Narrative.Trim().Substring(0, 1);
                    if (comma == ",") { Narrative = Narrative.Remove(0, 1); }
                    comma = "";
                }
                sw.WriteLine("<reqMT" + dt.Rows[0]["XMLStartTag"].ToString() + ">");
                sw.WriteLine("<Sender>" + dt.Rows[0]["Sender"].ToString() + "</Sender>");
                sw.WriteLine("<MessageType>" + dt.Rows[0]["MessageType"].ToString() + "</MessageType>");
                sw.WriteLine("<Receiver>" + dt.Rows[0]["To"].ToString() + "</Receiver>");
                sw.WriteLine("<TransactionReferenceNumber>" + dt.Rows[0]["Transaction Reference Number"].ToString() + "</TransactionReferenceNumber>");
                sw.WriteLine("<RelatedReference>" + dt.Rows[0]["Related Reference"].ToString() + "</RelatedReference>");
                sw.WriteLine("<Narrative>" + Narrative + "</Narrative>");
                sw.WriteLine("</reqMT>");

            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
    }
    public void CreateSwift999_XML(string FileType)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Ledger_Mod_Swift_MT999_FileCreation_XML", P_DocNo);
        string TodayDate = DateTime.Now.ToString("dd/MM/yyyy");
        string TodayDate1 = DateTime.Now.ToString("ddMMyyyyhhmmss");
        string MTodayDate = "";
        if (TodayDate.Contains("/"))
        {
            MTodayDate = TodayDate.Replace("/", "");
        }
        if (TodayDate.Contains("-"))
        {
            MTodayDate = TodayDate.Replace("-", "");
        }
        if (dt.Rows.Count > 0)
        {
            string _directoryPath = "";
            if (FileType == "SWIFT")
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SWIFT_XML/LedgerModification/MT999/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SFMSXML/LedgerModification/SFMS999/" + MTodayDate + "/");
            }

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string _filePath = "";
            if (FileType == "SWIFT")
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT999_" + TodayDate1 + ".xml";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN999_" + TodayDate1 + ".xml";
            }
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            if (dt.Rows.Count > 0)
            {

                string Narrative = "";
                if (dt.Rows[0]["Narrative1"].ToString() != "")
                {
                    Narrative = Narrative + dt.Rows[0]["Narrative1"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative2"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative2"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative3"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative3"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative4"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative4"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative5"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative5"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative6"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative6"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative7"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative7"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative8"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative8"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative9"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative9"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative10"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative10"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative11"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative11"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative12"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative12"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative13"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative13"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative14"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative14"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative15"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative15"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative16"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative16"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative17"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative17"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative18"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative18"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative19"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative19"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative20"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative20"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative21"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative21"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative22"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative22"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative23"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative23"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative24"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative24"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative25"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative25"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative26"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative26"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative27"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative27"].ToString().Replace("/", "");
                }

                if (dt.Rows[0]["Narrative28"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative28"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative29"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative29"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative30"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative30"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative31"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative31"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative32"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative32"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative33"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative33"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative34"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative34"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Narrative35"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative35"].ToString().Replace("/", "");
                }
                if (Narrative.Length != 0)
                {
                    string comma = Narrative.Trim().Substring(0, 1);
                    if (comma == ",") { Narrative = Narrative.Remove(0, 1); }
                    comma = "";
                }

                sw.WriteLine("<reqMT" + dt.Rows[0]["XMLStartTag"].ToString() + ">");
                sw.WriteLine("<Sender>" + dt.Rows[0]["Sender"].ToString() + "</Sender>");
                sw.WriteLine("<MessageType>" + dt.Rows[0]["MessageType"].ToString() + "</MessageType>");
                sw.WriteLine("<Receiver>" + dt.Rows[0]["To"].ToString() + "</Receiver>");
                sw.WriteLine("<TransactionReferenceNumber>" + dt.Rows[0]["Transaction Reference Number"].ToString() + "</TransactionReferenceNumber>");
                sw.WriteLine("<RelatedReference>" + dt.Rows[0]["Related Reference"].ToString() + "</RelatedReference>");
                sw.WriteLine("<Narrative>" + Narrative + "</Narrative>");
                sw.WriteLine("</reqMT>");
            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
    }
    protected void InsertDataForSwiftSTP(string SwiftType)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P1 = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        SqlParameter P2 = new SqlParameter("@SwiftType", SwiftType);
        string result = obj.SaveDeleteData("TF_SWIFT_STP_InsertLedgerModificationData", P1, P2);
    }
}