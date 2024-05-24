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
using Excel = Microsoft.Office.Interop.Excel;
using ExcelAutoFormat = Microsoft.Office.Interop.Excel.XlRangeAutoFormat;

public partial class IMP_Transactions_TF_IMP_Ledger_Modification_ICA_ICU_IBA_Maker : System.Web.UI.Page
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
                    Response.Redirect("TF_IMP_Ledger_Modification_ICA_ICU_IBA_Maker_View.aspx", true);
                }
                else
                {
                    hdnUserName.Value = Session["userName"].ToString();
                    hdnBranchName.Value = Request.QueryString["BranchName"].Trim();
                    hdnDocType.Value = Request.QueryString["DocType"].Trim();
                    hdnDocScrutiny.Value = Request.QueryString["Document_Scrutiny"].Trim();
                    txtDocNo.Text = Request.QueryString["DocNo"].Trim();
                    TF_DATA obj = new TF_DATA();
                    SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
                    string result = "";
                    result = obj.SaveDeleteData("TF_IMP_Check_Ref_In_Ledger_Modification", PDocNo);
                    if (result == "exists")
                    {
                        hdnMode.Value = "Edit";
                    }
                    else
                    {
                        hdnMode.Value = "Add";
                    }
                    txtValueDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

                    if (hdnMode.Value == "Edit")
                    {
                        Fill_Logd_Details();
                        Fill_LegerModification();
                        Check_LEINO_ExchRateDetails();
                    }
                    else
                    {
                        Fill_Logd_Details();
                        Check_LEINO_ExchRateDetails();
                    }
                }
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_IMP_Ledger_Modification_ICA_ICU_IBA_Maker_View.aspx", true);
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
            txtLedgerRefNo.Text = dt.Rows[0]["Document_No"].ToString();
            ToggleDocType(dt.Rows[0]["Document_Type"].ToString(), dt.Rows[0]["Document_FLC_ILC"].ToString());
            hdn_FLC_ILC.Value = dt.Rows[0]["Document_FLC_ILC"].ToString();
            hdnDocScrutiny.Value = dt.Rows[0]["Document_Scrutiny"].ToString();
            lblDoc_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            lblBillAmt.Text = dt.Rows[0]["Bill_Amount"].ToString();
            hdnBranchCode.Value = dt.Rows[0]["Branch_Code"].ToString();
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
           // txtValueDate.Text = dt.Rows[0]["Acceptance_Value_Date"].ToString();
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
            if (dt.Rows[0]["Checker_Remark"].ToString() != "")
            {
                lblChecker_Remark.Text = "Checker Remark :- " + dt.Rows[0]["Checker_Remark"].ToString();
            }
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
                    Foreign_Local = "FOREIGN";
                }
                else if (DocFLC_ILC_Type == "ILC")
                {
                    Foreign_Local = "LOCAL";
                }
                lblForeign_Local.Text = Foreign_Local;
                lblCollection_Lodgment_UnderLC.Text = "Collection";
                lblSight_Usance.Text = "Usance";
                break;
        }
    }
    [WebMethod]
    public static string AddUpdateLedgerModification(string hdnUserName, string _BranchName, string _txtDocNo, string _txtValueDate,
        ////LEDGER MODIFICATION
        string _txtLedgerRemark,
        string _txtLedgerCustName, string _txtLedgerAccode, string _txtLedgerCURR,
        string _txtLedgerRefNo, string _txtLedger_Modify_amt, string _txtLedgerOperationDate, string _txtLedgerBalanceAmt,
        string _txtLedgerAcceptDate, string _txtLedgerMaturity, string _txtLedgerSettlememtDate, string _txtLedgerSettlValue,
        string _txtLedgerLastModDate, string _txtLedgerREM_EUC, string _txtLedgerLastOPEDate, string _txtLedgerTransNo,
        string _txtLedgerContraCountry, string _txtLedgerStatusCode, string _txtLedgerCollectOfComm, string _txtLedgerCommodity,
        string _txtLedgerhandlingCommRate, string _txtLedgerhandlingCommCurr, string _txtLedgerhandlingCommAmt, string _txtLedgerhandlingCommPayer,
        string _txtLedgerPostageRate, string _txtLedgerPostageCurr, string _txtLedgerPostageAmt, string _txtLedgerPostagePayer,
        string _txtLedgerOthersRate, string _txtLedgerOthersCurr, string _txtLedgerOthersAmt, string _txtLedgerOthersPayer,
        string _txtLedgerTheirCommRate, string _txtLedgerTheirCommCurr, string _txtLedgerTheirCommAmt, string _txtLedgerTheirCommPayer,
        string _txtLedgerNegoBank,
        string _txtLedgerReimbursingBank,
        string _txtLedgerDrawer, string _txtLedgerTenor, string _txtLedgerAttn,
        //Swift Files 
        string None_Flag,string MT499_Flag,string MT799_Flag,string MT999_Flag,
        string _txt_Narrative1,string _txt_Narrative2,string _txt_Narrative3,string _txt_Narrative4,string _txt_Narrative5,string _txt_Narrative6,
        string _txt_Narrative7,string _txt_Narrative8,string _txt_Narrative9,string _txt_Narrative10,string _txt_Narrative11,string _txt_Narrative12,
        string _txt_Narrative13,string _txt_Narrative14,string _txt_Narrative15,string _txt_Narrative16,string _txt_Narrative17,string _txt_Narrative18,
        string _txt_Narrative19,string _txt_Narrative20,string _txt_Narrative21,string _txt_Narrative22,string _txt_Narrative23,string _txt_Narrative24,
        string _txt_Narrative25,string _txt_Narrative26,string _txt_Narrative27,string _txt_Narrative28,string _txt_Narrative29,string _txt_Narrative30,
        string _txt_Narrative31,string _txt_Narrative32,string _txt_Narrative33,string _txt_Narrative34,string _txt_Narrative35
        )
    {
        TF_DATA obj = new TF_DATA();

        SqlParameter P_BranchName = new SqlParameter("@BranchName", _BranchName.ToUpper());
        SqlParameter P_txtDocNo = new SqlParameter("@Document_No", _txtDocNo.ToUpper());
        SqlParameter P_txtValueDate = new SqlParameter("@Acceptance_Value_Date", _txtValueDate.ToUpper());
       //Ledger Modification
        SqlParameter P_Ledger_Remark = new SqlParameter("@Ledger_Remark", _txtLedgerRemark.ToUpper());
        SqlParameter P_Ledger_Cust_AccNo = new SqlParameter("@Ledger_Cust_AccNo", _txtLedgerCustName.ToUpper());
        SqlParameter P_Ledger_Curr = new SqlParameter("@Ledger_Curr", _txtLedgerCURR.ToUpper());
        SqlParameter P_Ledger_AccCode = new SqlParameter("@Ledger_AccCode", _txtLedgerAccode.ToUpper());
        SqlParameter P_Ledger_Ref_No = new SqlParameter("@Ledger_Ref_No", _txtLedgerRefNo.ToUpper());
        SqlParameter P_Ledger_Amount = new SqlParameter("@Ledger_Amount", _txtLedger_Modify_amt.ToUpper());
        SqlParameter P_Ledger_Operation_Date = new SqlParameter("@Ledger_Operation_Date", _txtLedgerOperationDate.ToUpper());
        SqlParameter p_Ledger_Balance_Amt = new SqlParameter("@Ledger_Balance_Amt", _txtLedgerBalanceAmt.ToUpper());
        SqlParameter P_Ledger_Accept_Date = new SqlParameter("@Ledger_Accept_Date", _txtLedgerAcceptDate.ToUpper());
        SqlParameter P_Ledger_Maturity = new SqlParameter("@Ledger_Maturity", _txtLedgerMaturity.ToUpper());
        SqlParameter P_Ledger_Settlement_Date = new SqlParameter("@Ledger_Settlement_Date", _txtLedgerSettlememtDate.ToUpper());
        SqlParameter P_Ledger_Settlement_Value = new SqlParameter("@Ledger_Settlement_Value", _txtLedgerSettlValue.ToUpper());
        SqlParameter P_Ledger_Last_Mod_Date = new SqlParameter("@Ledger_Last_Mod_Date", _txtLedgerLastModDate.ToUpper());
        SqlParameter P_Ledger_Rem_EUC = new SqlParameter("@Ledger_Rem_EUC", _txtLedgerREM_EUC.ToUpper());
        SqlParameter P_Ledger_Last_OPE_Date = new SqlParameter("@Ledger_Last_OPE_Date", _txtLedgerLastOPEDate.ToUpper());
        SqlParameter P_Ledger_Trans_No = new SqlParameter("@Ledger_Trans_No", _txtLedgerTransNo.ToUpper());
        SqlParameter P_Ledger_Contra_Country = new SqlParameter("@Ledger_Contra_Country", _txtLedgerContraCountry.ToUpper());
        SqlParameter P_Ledger_Status_Code = new SqlParameter("@Ledger_Status_Code", _txtLedgerStatusCode.ToUpper());
        SqlParameter P_Ledger_Collect_Of_Cumm = new SqlParameter("@Ledger_Collect_Of_Cumm", _txtLedgerCollectOfComm.ToUpper());
        SqlParameter P_Ledger_Commodity = new SqlParameter("@Ledger_Commodity", _txtLedgerCommodity.ToUpper());
        SqlParameter P_Ledger_Handling_Comm_Rate = new SqlParameter("@Ledger_Handling_Comm_Rate", _txtLedgerhandlingCommRate.ToUpper());
        SqlParameter P_Ledger_Handling_Comm_Curr = new SqlParameter("@Ledger_Handling_Comm_Curr", _txtLedgerhandlingCommCurr.ToUpper());
        SqlParameter P_Ledger_Handling_Comm_Amt = new SqlParameter("@Ledger_Handling_Comm_Amt", _txtLedgerhandlingCommAmt.ToUpper());
        SqlParameter P_Ledger_Handling_Comm_Payer = new SqlParameter("@Ledger_Handling_Comm_Payer", _txtLedgerhandlingCommPayer.ToUpper());
        SqlParameter P_Ledger_Postage_Rate = new SqlParameter("@Ledger_Postage_Rate", _txtLedgerPostageRate.ToUpper());
        SqlParameter P_Ledger_Postage_Curr = new SqlParameter("@Ledger_Postage_Curr", _txtLedgerPostageCurr.ToUpper());
        SqlParameter P_Ledger_Postage_Amt = new SqlParameter("@Ledger_Postage_Amt", _txtLedgerPostageAmt.ToUpper());
        SqlParameter P_Ledger_Postage_Payer = new SqlParameter("@Ledger_Postage_Payer", _txtLedgerPostagePayer.ToUpper());
        SqlParameter P_Ledger_Others_Rate = new SqlParameter("@Ledger_Others_Rate", _txtLedgerOthersRate.ToUpper());
        SqlParameter P_Ledger_Others_Curr = new SqlParameter("@Ledger_Others_Curr", _txtLedgerOthersCurr.ToUpper());
        SqlParameter P_Ledger_Others_Amt = new SqlParameter("@Ledger_Others_Amt", _txtLedgerOthersAmt.ToUpper());
        SqlParameter P_Ledger_Others_Payer = new SqlParameter("@Ledger_Others_Payer", _txtLedgerOthersPayer.ToUpper());
        SqlParameter P_Ledger_Their_Comm_Rate = new SqlParameter("@Ledger_Their_Comm_Rate", _txtLedgerTheirCommRate.ToUpper());
        SqlParameter P_Ledger_Their_Comm_Curr = new SqlParameter("@Ledger_Their_Comm_Curr", _txtLedgerTheirCommCurr.ToUpper());
        SqlParameter P_Ledger_Their_Comm_Amt = new SqlParameter("@Ledger_Their_Comm_Amt", _txtLedgerTheirCommAmt.ToUpper());
        SqlParameter P_Ledger_Their_Comm_Payer = new SqlParameter("@Ledger_Their_Comm_Payer", _txtLedgerTheirCommPayer.ToUpper());
        SqlParameter P_Ledger_Collect_Nego_Bank = new SqlParameter("@Ledger_Collect_Nego_Bank", _txtLedgerNegoBank.ToUpper());
        SqlParameter P_Ledger_Reimbursing_Bank = new SqlParameter("@Ledger_Reimbursing_Bank", _txtLedgerReimbursingBank.ToUpper());
        SqlParameter P_Ledger_Drawer_Drawee = new SqlParameter("@Ledger_Drawer_Drawee", _txtLedgerDrawer.ToUpper());
        SqlParameter P_Ledger_Tenor = new SqlParameter("@Ledger_Tenor", _txtLedgerTenor.ToUpper());
        SqlParameter P_Ledger_Attn = new SqlParameter("@Ledger_Attn", _txtLedgerAttn.ToUpper());

        //Swift Details
        SqlParameter P_None_Flag = new SqlParameter("@None_Flag", None_Flag.ToUpper());
        SqlParameter P_MT499_Flag = new SqlParameter("@MT499_Flag", MT499_Flag.ToUpper());
        SqlParameter P_MT799_Flag = new SqlParameter("@MT799_Flag", MT799_Flag.ToUpper());
        SqlParameter P_MT999_Flag = new SqlParameter("@MT999_Flag", MT999_Flag.ToUpper());

        SqlParameter P_txt_Narrative1 = new SqlParameter("@_txt_Narrative1", _txt_Narrative1.ToUpper());
        SqlParameter P_txt_Narrative2 = new SqlParameter("@_txt_Narrative2", _txt_Narrative2.ToUpper());
        SqlParameter P_txt_Narrative3 = new SqlParameter("@_txt_Narrative3", _txt_Narrative3.ToUpper());
        SqlParameter P_txt_Narrative4 = new SqlParameter("@_txt_Narrative4", _txt_Narrative4.ToUpper());
        SqlParameter P_txt_Narrative5 = new SqlParameter("@_txt_Narrative5", _txt_Narrative5.ToUpper());
        SqlParameter P_txt_Narrative6 = new SqlParameter("@_txt_Narrative6", _txt_Narrative6.ToUpper());
        SqlParameter P_txt_Narrative7 = new SqlParameter("@_txt_Narrative7", _txt_Narrative7.ToUpper());
        SqlParameter P_txt_Narrative8 = new SqlParameter("@_txt_Narrative8", _txt_Narrative8.ToUpper());
        SqlParameter P_txt_Narrative9 = new SqlParameter("@_txt_Narrative9", _txt_Narrative9.ToUpper());
        SqlParameter P_txt_Narrative10 = new SqlParameter("@_txt_Narrative10", _txt_Narrative10.ToUpper());
        SqlParameter P_txt_Narrative11 = new SqlParameter("@_txt_Narrative11", _txt_Narrative11.ToUpper());
        SqlParameter P_txt_Narrative12 = new SqlParameter("@_txt_Narrative12", _txt_Narrative12.ToUpper());
        SqlParameter P_txt_Narrative13 = new SqlParameter("@_txt_Narrative13", _txt_Narrative13.ToUpper());
        SqlParameter P_txt_Narrative14 = new SqlParameter("@_txt_Narrative14", _txt_Narrative14.ToUpper());
        SqlParameter P_txt_Narrative15 = new SqlParameter("@_txt_Narrative15", _txt_Narrative15.ToUpper());
        SqlParameter P_txt_Narrative16 = new SqlParameter("@_txt_Narrative16", _txt_Narrative16.ToUpper());
        SqlParameter P_txt_Narrative17 = new SqlParameter("@_txt_Narrative17", _txt_Narrative17.ToUpper());
        SqlParameter P_txt_Narrative18 = new SqlParameter("@_txt_Narrative18", _txt_Narrative18.ToUpper());
        SqlParameter P_txt_Narrative19 = new SqlParameter("@_txt_Narrative19", _txt_Narrative19.ToUpper());
        SqlParameter P_txt_Narrative20 = new SqlParameter("@_txt_Narrative20", _txt_Narrative20.ToUpper());
        SqlParameter P_txt_Narrative21 = new SqlParameter("@_txt_Narrative21", _txt_Narrative21.ToUpper());
        SqlParameter P_txt_Narrative22 = new SqlParameter("@_txt_Narrative22", _txt_Narrative22.ToUpper());
        SqlParameter P_txt_Narrative23 = new SqlParameter("@_txt_Narrative23", _txt_Narrative23.ToUpper());
        SqlParameter P_txt_Narrative24 = new SqlParameter("@_txt_Narrative24", _txt_Narrative24.ToUpper());
        SqlParameter P_txt_Narrative25 = new SqlParameter("@_txt_Narrative25", _txt_Narrative25.ToUpper());
        SqlParameter P_txt_Narrative26 = new SqlParameter("@_txt_Narrative26", _txt_Narrative26.ToUpper());
        SqlParameter P_txt_Narrative27 = new SqlParameter("@_txt_Narrative27", _txt_Narrative27.ToUpper());
        SqlParameter P_txt_Narrative28 = new SqlParameter("@_txt_Narrative28", _txt_Narrative28.ToUpper());
        SqlParameter P_txt_Narrative29 = new SqlParameter("@_txt_Narrative29", _txt_Narrative29.ToUpper());
        SqlParameter P_txt_Narrative30 = new SqlParameter("@_txt_Narrative30", _txt_Narrative30.ToUpper());
        SqlParameter P_txt_Narrative31 = new SqlParameter("@_txt_Narrative31", _txt_Narrative31.ToUpper());
        SqlParameter P_txt_Narrative32 = new SqlParameter("@_txt_Narrative32", _txt_Narrative32.ToUpper());
        SqlParameter P_txt_Narrative33 = new SqlParameter("@_txt_Narrative33", _txt_Narrative33.ToUpper());
        SqlParameter P_txt_Narrative34 = new SqlParameter("@_txt_Narrative34", _txt_Narrative34.ToUpper());
        SqlParameter P_txt_Narrative35 = new SqlParameter("@_txt_Narrative35", _txt_Narrative35.ToUpper());

        SqlParameter P_AddedBy = new SqlParameter("@AddedBy", hdnUserName);
        SqlParameter P_AddedDate = new SqlParameter("@AddedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        SqlParameter P_UpdatedBy = new SqlParameter("@UpdatedBy", hdnUserName);
        SqlParameter P_UpdatedDate = new SqlParameter("@UpdatedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

        string _Result = obj.SaveDeleteData("TF_IMP_AddUpdate_Ledger_Modification_ICA_ICU_IBA", P_BranchName, P_txtDocNo, P_txtValueDate,
            ////Ledger_File
            P_Ledger_Remark, P_Ledger_Cust_AccNo, P_Ledger_Curr, P_Ledger_AccCode, P_Ledger_Ref_No, P_Ledger_Amount, p_Ledger_Balance_Amt,
            P_Ledger_Operation_Date, P_Ledger_Maturity, P_Ledger_Accept_Date, P_Ledger_Settlement_Date, P_Ledger_Settlement_Value,
            P_Ledger_Last_Mod_Date, P_Ledger_Rem_EUC, P_Ledger_Last_OPE_Date, P_Ledger_Trans_No, P_Ledger_Contra_Country, P_Ledger_Status_Code,
            P_Ledger_Collect_Of_Cumm, P_Ledger_Commodity,
            P_Ledger_Handling_Comm_Rate, P_Ledger_Handling_Comm_Curr, P_Ledger_Handling_Comm_Amt, P_Ledger_Handling_Comm_Payer,
            P_Ledger_Postage_Rate, P_Ledger_Postage_Curr, P_Ledger_Postage_Amt, P_Ledger_Postage_Payer,
            P_Ledger_Others_Rate, P_Ledger_Others_Curr, P_Ledger_Others_Amt, P_Ledger_Others_Payer,
            P_Ledger_Their_Comm_Rate, P_Ledger_Their_Comm_Curr, P_Ledger_Their_Comm_Amt, P_Ledger_Their_Comm_Payer,
            P_Ledger_Collect_Nego_Bank, P_Ledger_Reimbursing_Bank, P_Ledger_Drawer_Drawee, P_Ledger_Tenor, P_Ledger_Attn,
            //Swift File
            P_None_Flag,P_MT499_Flag,P_MT799_Flag,P_MT999_Flag,
            P_txt_Narrative1, P_txt_Narrative2, P_txt_Narrative3, P_txt_Narrative4, P_txt_Narrative5, P_txt_Narrative6, P_txt_Narrative7, P_txt_Narrative8, P_txt_Narrative9, P_txt_Narrative10,
            P_txt_Narrative11, P_txt_Narrative12, P_txt_Narrative13, P_txt_Narrative14, P_txt_Narrative15, P_txt_Narrative16, P_txt_Narrative17, P_txt_Narrative18, P_txt_Narrative19, P_txt_Narrative20,
            P_txt_Narrative21, P_txt_Narrative22, P_txt_Narrative23, P_txt_Narrative24, P_txt_Narrative25, P_txt_Narrative26, P_txt_Narrative27, P_txt_Narrative28, P_txt_Narrative29, P_txt_Narrative30,
            P_txt_Narrative31, P_txt_Narrative32, P_txt_Narrative33, P_txt_Narrative34, P_txt_Narrative35,
               P_AddedBy, P_AddedDate, P_UpdatedBy, P_UpdatedDate
            );
        return _Result;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        string Result = obj.SaveDeleteData("TF_IMP_SubmitONLedgerModificationForChecker", P_DocNo);
        if (Result == "Submit")
        {
            string _script = "window.location='TF_IMP_Ledger_Modification_ICA_ICU_IBA_Maker_View.aspx?result=Submit'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
    }
    //----------------Added by bhupen on 15042023-----------------------------------//
    protected void GetDrawee_Details_LEI()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
        DataTable dt = new DataTable();
        dt = obj.getData("TF_IMP_GetLodgementLEIdetails_acc", PDocNo);
        if (dt.Rows.Count > 0)
        {
            hdnDrawerno.Value = dt.Rows[0]["Drawer"].ToString();
            hdnCustAcNo.Value = dt.Rows[0]["CustAcNo"].ToString();
            hdnCustAbbr.Value = dt.Rows[0]["Cust_Abbr"].ToString();
            hdnCustName.Value = dt.Rows[0]["CUST_NAME"].ToString();
            if (hdnDrawerno.Value != "")
            {
                SqlParameter CustAcno = new SqlParameter("@CustAcno", hdnCustAcNo.Value);
                SqlParameter Drawerno = new SqlParameter("@Drawee", hdnDrawerno.Value);
                DataTable dt1 = new DataTable();
                dt1 = obj.getData("TF_IMP_GetDraweedetails_acceptance", CustAcno, Drawerno);
                if (dt1.Rows.Count > 0)
                {
                    hdnDrawer.Value = dt1.Rows[0]["Drawer_NAME"].ToString();
                }
            }
        }
    }
    private void Check_LEINO_ExchRateDetails()
    {
        if (lblForeign_Local.Text == "FOREIGN" || lblForeign_Local.Text == "Foreign")
        {
            try
            {
                TF_DATA objData = new TF_DATA();
                string result2 = "";
                SqlParameter p1 = new SqlParameter("@CurrCode", SqlDbType.VarChar);
                p1.Value = lblDoc_Curr.Text.Trim();
                SqlParameter p2 = new SqlParameter("@Date", SqlDbType.VarChar);
                p2.Value = txtValueDate.Text.ToString();
                string _query = "TF_IMP_GetLEI_RateCardDetails";
                DataTable dt = objData.getData(_query, p1, p2);
                if (dt.Rows.Count > 0)
                {
                    string Exch_rate = dt.Rows[0]["CARD_EXCHANGE_RATE"].ToString().Trim();
                    lbl_Exch_rate.Text = dt.Rows[0]["CARD_EXCHANGE_RATE"].ToString().Trim();
                    string Amount = lblBillAmt.Text.Trim().Replace(",", "");
                    if (Amount != "" && Exch_rate != "")
                    {
                        SqlParameter billamt = new SqlParameter("@billamt", Amount);
                        SqlParameter exch_Rate = new SqlParameter("@exch_Rate", Exch_rate);
                        string _queryC = "TF_IMP_GetThresholdLimitCheck";
                        result2 = objData.SaveDeleteData(_queryC, billamt, exch_Rate);

                        if (result2 == "Grater")
                        {
                            label1.Text = "Transaction Bill Amt is Greater than LEI Thresold Limit.Please Verify LEI details.";
                            label1.ForeColor = System.Drawing.Color.Red;
                            label1.Font.Size = 10;
                            //label1.Font.Bold = true;
                            hdnLeiFlag.Value = "Y";
                            btnSubmit.Enabled = false;
                            btn_Verify.Visible = true;
                        }
                        else
                        {
                            label1.Text = "Transaction Bill Amt is less than LEI Thresold Limit.";
                            label1.ForeColor = System.Drawing.Color.Green;
                            label1.Font.Size = 10;
                            hdnLeiFlag.Value = "N";
                            btnSubmit.Enabled = true;
                            btn_Verify.Visible = false;
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('No Exch Rate is available for currency.')", true);
                }
            }
            catch (Exception ex)
            {
                //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
            }
        }
    }
    private void Check_LEINODetails()
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@CustAbbr", hdnCustAbbr.Value.ToString().Trim());
            SqlParameter p2 = new SqlParameter("@BranchCode", hdnBranchCode.Value.ToString().Trim());
            string _query = "TF_IMP_GetLEIDetails_Customer";
            DataTable dt = objData.getData(_query, p1, p2);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["LEI_No1"].ToString().Trim() == "")
                {
                    lblLEI_Remark.Text = "...Not Verified.";
                    lblLEI_Remark.ForeColor = System.Drawing.Color.Red;
                    hdnleino.Value = "";
                }
                else
                {
                    lblLEI_Remark.Text = "...Verified.";
                    hdnleino.Value = dt.Rows[0]["LEI_No"].ToString();
                }
            }
            else
            {
                lblLEI_Remark.Text = "...Not Verified.";
                lblLEI_Remark.ForeColor = System.Drawing.Color.Red;
                hdnleino.Value = "";
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    private void Check_LEINO_ExpirydateDetails()
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@CustAbbr", hdnCustAbbr.Value.ToString().Trim());
            SqlParameter p2 = new SqlParameter("@DueDate", txtLedgerMaturity.Text.ToString().Trim());
            SqlParameter p3 = new SqlParameter("@BranchCode", hdnBranchCode.Value.ToString().Trim());

            string _query = "TF_IMP_GetLEIDetails_ExpiryDate";
            DataTable dt = objData.getData(_query, p1, p2, p3);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Statuschk"].ToString() == "Grater")
                {
                    lblLEIExpiry_Remark.Text = "...Verified.";
                    lblLEIExpiry_Remark.ForeColor = System.Drawing.Color.Green;
                    hdnleiexpiry.Value = dt.Rows[0]["LEI_Expiry_Date"].ToString();
                    hdnleiexpiry1.Value = "N";
                }
                else if (dt.Rows[0]["Statuschk"].ToString() == "Less")
                {
                    lblLEIExpiry_Remark.Text = "...Not Verified.";
                    lblLEIExpiry_Remark.ForeColor = System.Drawing.Color.Green;
                    hdnleiexpiry.Value = dt.Rows[0]["LEI_Expiry_Date"].ToString();
                    hdnleiexpiry1.Value = "Y";
                }
            }
            else
            {
                lblLEIExpiry_Remark.Text = "...Not Verified.";
                lblLEIExpiry_Remark.ForeColor = System.Drawing.Color.Red;
                hdnleiexpiry.Value = "";
                hdnleiexpiry1.Value = "";
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    private void Check_DraweeLEINODetails()
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@CustAcno", hdnCustAcNo.Value.Trim());
            SqlParameter p2 = new SqlParameter("@Draweename", hdnDrawer.Value.Trim());
            SqlParameter p3 = new SqlParameter("@BranchCode", hdnBranchCode.Value.ToString());

            string _query = "TF_IMP_GetLEIDetails_Drawee";
            DataTable dt = objData.getData(_query, p1, p2, p3);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Drawer_LEI_NO"].ToString() == "")
                {
                    lblLEI_Remark_Drawee.Text = "...Not Verified.";
                    lblLEI_Remark_Drawee.ForeColor = System.Drawing.Color.Red;
                    hdnDraweeleino.Value = "";
                }
                else
                {
                    lblLEI_Remark_Drawee.Text = "...Verified.";
                    hdnDraweeleino.Value = dt.Rows[0]["Drawer_LEI_NO"].ToString();
                }
            }
            else
            {
                lblLEI_Remark_Drawee.Text = "...Not Verified.";
                lblLEI_Remark_Drawee.ForeColor = System.Drawing.Color.Red;
                hdnDraweeleino.Value = "";
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    private void Check_DraweeLEINO_ExpirydateDetails()
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@CustAcno", hdnCustAcNo.Value.Trim());
            SqlParameter p2 = new SqlParameter("@Draweename", hdnDrawer.Value.Trim());
            SqlParameter p3 = new SqlParameter("@DueDate", txtLedgerMaturity.Text.ToString().Trim());
            SqlParameter p4 = new SqlParameter("@BranchCode", hdnBranchCode.Value.ToString());

            string _query = "TF_IMP_GetLEIDetails_ExpiryDate_Drawee";
            DataTable dt = objData.getData(_query, p1, p2, p3, p4);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Statuschk"].ToString() == "Grater")
                {
                    lblLEIExpiry_Remark_Drawee.Text = "...Verified.";
                    lblLEIExpiry_Remark_Drawee.ForeColor = System.Drawing.Color.Green;
                    hdnDraweeleiexpiry.Value = dt.Rows[0]["Drawer_LEIExpiryDate"].ToString();
                    hdnDraweeleiexpiry1.Value = "N";
                }
                else if (dt.Rows[0]["Statuschk"].ToString() == "Less")
                {
                    lblLEIExpiry_Remark_Drawee.Text = "...Not Verified.";
                    lblLEIExpiry_Remark_Drawee.ForeColor = System.Drawing.Color.Green;
                    hdnDraweeleiexpiry.Value = dt.Rows[0]["Drawer_LEIExpiryDate"].ToString();
                    hdnDraweeleiexpiry1.Value = "Y";
                }
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('LEI for this customer is Expired. Kindly Check.')", true);
                lblLEIExpiry_Remark_Drawee.Text = "...Not Verified.";
                lblLEIExpiry_Remark_Drawee.ForeColor = System.Drawing.Color.Red;
                hdnDraweeleiexpiry.Value = "";
                hdnDraweeleiexpiry1.Value = "";
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void Lei_Audit()
    {
        if (lblForeign_Local.Text == "FOREIGN" || lblForeign_Local.Text == "Foreign")
        {
            try
            {
                TF_DATA obj = new TF_DATA();
                SqlParameter P_BranchCode = new SqlParameter("@BranchCode", hdnBranchCode.Value.Trim());
                SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
                SqlParameter P_DocType = new SqlParameter("@DocType", "ICU");
                SqlParameter P_Sight_Usance = new SqlParameter("@billType", lblSight_Usance.Text.Trim());
                SqlParameter P_CustAcno = new SqlParameter("@CustAcNo", hdnCustAcNo.Value.Trim());
                SqlParameter P_Cust_Abbr = new SqlParameter("@Cust_Abbr", hdnCustAbbr.Value.Trim());
                SqlParameter P_Cust_Name = new SqlParameter("@Cust_Name", hdnCustName.Value.Trim());
                SqlParameter P_Cust_LEI = new SqlParameter("@Cust_LEI", hdnleino.Value.Trim());
                SqlParameter P_Cust_LEI_Expiry = new SqlParameter("@Cust_LEI_Expiry", hdnleiexpiry.Value.Trim());
                SqlParameter P_Cust_LEI_Expired = new SqlParameter("@Cust_LEI_Expired", hdnleiexpiry1.Value.Trim());
                SqlParameter P_Drawee_Name = new SqlParameter("@Drawee_Name", hdnDrawer.Value.Trim());
                SqlParameter P_Drawee_LEI = new SqlParameter("@Drawee_LEI", hdnDraweeleino.Value.Trim());
                SqlParameter P_Drawee_LEI_Expiry = new SqlParameter("@Drawee_LEI_Expiry", hdnDraweeleiexpiry.Value.Trim());
                SqlParameter P_Drawee_LEI_Expired = new SqlParameter("@Drawee_LEI_Expired", hdnDraweeleiexpiry1.Value.Trim());
                SqlParameter P_BillAmount = new SqlParameter("@BillAmt", lblBillAmt.Text.Trim().Replace(",", ""));
                SqlParameter P_txtCurr = new SqlParameter("@Curr", lblDoc_Curr.Text.Trim());
                SqlParameter P_Exchrate = new SqlParameter("@ExchRt", lbl_Exch_rate.Text.Trim());
                SqlParameter P_LodgementDate = new SqlParameter("@LodgDate", txtValueDate.Text.Trim());
                SqlParameter P_DueDate = new SqlParameter("@DueDate", txtLedgerMaturity.Text.Trim());
                SqlParameter P_LEI_Flag = new SqlParameter("@LEI_Flag", hdnLeiFlag.Value.Trim());
                //SqlParameter P_CustLEI_Flag = new SqlParameter("@Cust_LEIFlag", hdncustleiflag.Value.Trim());
                SqlParameter P_Status = new SqlParameter("@status", "C");
                SqlParameter P_Module = new SqlParameter("@Module", "LM");
                SqlParameter P_AddedBy = new SqlParameter("@user", hdnUserName.Value.Trim());
                SqlParameter P_AddedDate = new SqlParameter("@uploadingdate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

                string _queryLEI = "TF_IMP_AddUpdate_LEITransaction";

                string _result = obj.SaveDeleteData(_queryLEI, P_BranchCode, P_DocNo, P_DocType, P_Sight_Usance, P_CustAcno, P_Cust_Abbr, P_Cust_Name, P_Cust_LEI,
                   P_Cust_LEI_Expiry, P_Cust_LEI_Expired, P_Drawee_Name, P_Drawee_LEI, P_Drawee_LEI_Expiry, P_Drawee_LEI_Expired, P_BillAmount, P_txtCurr, P_Exchrate,
                   P_LodgementDate, P_DueDate, P_LEI_Flag, P_Status, P_Module, P_AddedBy, P_AddedDate);
            }
            catch (Exception ex)
            {
                //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
            }
        }
    }
    protected void btn_Verify_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    if (txtCustomer_ID.Text.Trim() == "")
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Please Select Customer for Verifying LEI details.')", true);
        //        txtCustomer_ID.Focus();
        //    }
        //    else if (lblBillAmt.Text.Trim() == "")
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Kindly Fill Amount for Verifying LEI details.')", true);
        //        lblBillAmt.Focus();
        //    }
        //    else if (txt_Maturity_Date.Text.Trim() == "")
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Kindly Fill Due date for Verifying LEI details.')", true);
        //        txt_Maturity_Date.Focus();
        //    }
        //    else
        //    {
        //        GetDrawee_Details_LEI();
        //        Check_LEINODetails();
        //        Check_LEINO_ExpirydateDetails();
        //        Check_DraweeLEINODetails();
        //        Check_DraweeLEINO_ExpirydateDetails();
        //        btnSubmit.Enabled = true;

        //        String CustLEINo = hdnleino.Value + " " + lblLEI_Remark.Text + "\\n";
        //        String CustLEIExpiry = hdnleiexpiry.Value + " " + lblLEIExpiry_Remark.Text + "\\n";
        //        String DrawerLEINo = hdnDraweeleino.Value + " " + lblLEI_Remark_Drawee.Text + "\\n";
        //        String DrawerLEIExpiry = hdnDraweeleiexpiry.Value + " " + lblLEIExpiry_Remark_Drawee.Text + "\\n";

        //        String LEIMSG = @"Customer LEI No : " + CustLEINo + "Customer LEI Expiry : " + CustLEIExpiry + "Drawer LEI : " + DrawerLEINo + "Drawer LEI Expiry : " + DrawerLEIExpiry;
        //        // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "messege", "MyConfirm('" + LEIMSG + "')", true);
        //        ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('" + LEIMSG + "')", true);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        //}
    }

    [WebMethod]
    public static Fields SubmitToChecker(string UserName, string DocNo)
    {
        Fields fields = new Fields();
        TF_DATA obj = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", DocNo);
        fields.CheckerStatus = obj.SaveDeleteData("TF_IMP_SubmitONLedgerModificationForChecker", P_DocNo);
        ////IMPClass.CreateUserLogA(UserName, "Sent document(" + DocNo + ") to checker.");
        return fields;
    }
    public class Fields
    {
        // Submit to Checker
        public string CheckerStatus { get; set; }
    }
}