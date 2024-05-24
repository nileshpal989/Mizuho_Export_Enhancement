using ClosedXML.Excel;
using OfficeOpenXml;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;

public partial class IMP_Transactions_TF_IMP_Collection_Acceptance_Checker : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                TabContainerMain.ActiveTab = tbDocumentDetails;
                if (Request.QueryString["DocNo"] == null)
                {
                    Response.Redirect("TF_IMP_BOOKING_OF_IBD_AND_ACC_Maker_View.aspx", true);
                }
                else
                {
                    hdnUserName.Value = Session["userName"].ToString();
                    hdnBranchName.Value = Request.QueryString["BranchName"].Trim();
                    hdnDocType.Value = Request.QueryString["DocType"].Trim();
                    txtDocNo.Text = Request.QueryString["DocNo"].Trim();
                    makeReadOnly();
                    Fill_Logd_Details();
                    Fill_AcceptanceDetails();
                    Check_LEINO_ExchRateDetails();
                    Check_cust_Leiverify();
                    if (Request.QueryString["Status"].ToString() == "Approved By Checker")
                    {
                        ddlApproveReject.Enabled = false;
                        btn_Verify.Enabled = false;
                        Check_Lei_Verified();
                        Check_Lei_RecurringStatus();
                    }
                }
            }
            ddlApproveReject.Attributes.Add("onchange", "return DialogAlert();");
        }
    }
    protected void Fill_Logd_Details()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
        DataTable dt = new DataTable();
        dt = obj.getData("TF_IMP_Get_BOE_Details_For_Acceptance", PDocNo);
        if (dt.Rows.Count > 0)
        {
            ToggleDocType(dt.Rows[0]["Document_Type"].ToString(), dt.Rows[0]["Document_FLC_ILC"].ToString());
            hdnDocNo.Value = dt.Rows[0]["Document_No"].ToString();
            hdnBranchCode.Value = dt.Rows[0]["Branch_Code"].ToString();  //Added by bhupen on 14112022
            hdnNegoRemiBankType.Value = dt.Rows[0]["Nego_Remit_Bank_Type"].ToString();
            lblDoc_Curr.Text = dt.Rows[0]["Bill_Currency"].ToString();
            lblBillAmt.Text = dt.Rows[0]["Bill_Amount"].ToString();

            txtCustomer_ID.Text = dt.Rows[0]["CustAcNo"].ToString();
            fillCustomerMasterDetails();
            txt_Maturity_Date.Text = dt.Rows[0]["Maturity_Date"].ToString();

            // General Operation
            // //GO_Swift_SFMS
            txt_GO_Swift_SFMS_Ref_No.Text = dt.Rows[0]["Document_No"].ToString();
            txt_GO_Swift_SFMS_Debit_Cust_AcCode.Text = dt.Rows[0]["AC_Code"].ToString();
            txt_GO_Swift_SFMS_Debit_Cust.Text = dt.Rows[0]["Cust_Abbr"].ToString();
            txt_GO_Swift_SFMS_Debit_Cust_AccNo.Text = dt.Rows[0]["CustAcNo"].ToString();

            txt_GO_Swift_SFMS_Credit_Cust.Text = dt.Rows[0]["Cust_Abbr"].ToString();
            if (dt.Rows[0]["RMA_Status"].ToString() == "N")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('RMA has disable status for this Nego/Remit Bank SWIFT code.')", true);
            }
        }
    }
    protected void Fill_AcceptanceDetails()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", Request.QueryString["DocNo"].ToString());
        DataTable dt = new DataTable();
        dt = obj.getData("TF_IMP_Acceptance_Collection_Details", PDocNo);
        if (dt.Rows.Count > 0)
        {
            txtValueDate.Text = dt.Rows[0]["Acceptance_Value_Date"].ToString();
            txt_Accepted_Date.Text = dt.Rows[0]["Acceptance_Value_Date"].ToString();
            txtAttn.Text = dt.Rows[0]["Attn"].ToString();
            txt_Expected_InterestAmt.Text = dt.Rows[0]["Expected_Interest_Amount"].ToString();
            txt_Received_InterestAmt.Text = dt.Rows[0]["Received_Interest_Amount"].ToString();
            txt_Maturity_Date.Text = dt.Rows[0]["Interest_To_Date"].ToString();
            //// General Operation swift Sfms GO_SWIFT_SFMS_Flag

            if (dt.Rows[0]["GO_SWIFT_SFMS_Flag"].ToString() == "Y")
            {
                chkGO_Swift_SFMS.Checked = true;

                GO_Swift_SFMS_Toggel();

                txt_GO_Swift_SFMS_Comment.Text = dt.Rows[0]["GO_SWIFT_SFMS_Comment"].ToString();
                txt_GO_Swift_SFMS_Memo.Text = dt.Rows[0]["GO_SWIFT_SFMS_Memo"].ToString();

                txt_GO_Swift_SFMS_Debit_Amt.Text = dt.Rows[0]["GO_SWIFT_SFMS_Debit_Amt"].ToString();

                txt_GO_Swift_SFMS_Debit_Cust.Text = dt.Rows[0]["GO_SWIFT_SFMS_Debit_Cust"].ToString();
                txt_GO_Swift_SFMS_Debit_Cust_AcCode.Text = dt.Rows[0]["GO_SWIFT_SFMS_Debit_Cust_AcCode"].ToString();
                txt_GO_Swift_SFMS_Debit_Cust_AccNo.Text = dt.Rows[0]["GO_SWIFT_SFMS_Debit_Cust_AccNo"].ToString();

                txt_GO_Swift_SFMS_Debit_Exch_Curr.Text = dt.Rows[0]["GO_SWIFT_SFMS_Debit_ExchCCY"].ToString();
                txt_GO_Swift_SFMS_Debit_Exch_Rate.Text = dt.Rows[0]["GO_SWIFT_SFMS_Debit_ExchRate"].ToString();
                txt_GO_Swift_SFMS_Debit_Entity.Text = dt.Rows[0]["GO_SWIFT_SFMS_Debit_Entity"].ToString();
                txt_GO_Swift_SFMS_Debit_AdPrint.Text = dt.Rows[0]["GO_SWIFT_SFMS_Debit_Advice_Print"].ToString();

                txt_GO_Swift_SFMS_Credit_Amt.Text = dt.Rows[0]["GO_SWIFT_SFMS_Credit_Amt"].ToString();

                txt_GO_Swift_SFMS_Credit_Cust.Text = dt.Rows[0]["GO_SWIFT_SFMS_Credit_Cust"].ToString();
                txt_GO_Swift_SFMS_Credit_Cust_AccNo.Text = dt.Rows[0]["GO_SWIFT_SFMS_Credit_Cust_AccNo"].ToString();

                txt_GO_Swift_SFMS_Credit_Exch_Curr.Text = dt.Rows[0]["GO_SWIFT_SFMS_Credit_ExchCCY"].ToString();
                txt_GO_Swift_SFMS_Credit_Exch_Rate.Text = dt.Rows[0]["GO_SWIFT_SFMS_Credit_ExchRate"].ToString();
                txt_GO_Swift_SFMS_Credit_Entity.Text = dt.Rows[0]["GO_SWIFT_SFMS_Credit_Entity"].ToString();
                txt_GO_Swift_SFMS_Credit_AdPrint.Text = dt.Rows[0]["GO_SWIFT_SFMS_Credit_Advice_Print"].ToString();

                txt_GO_Swift_SFMS_Remarks.Text = dt.Rows[0]["GO_SWIFT_SFMS_Remark"].ToString();
                txt_GO_Swift_SFMS_Debit_Details.Text = dt.Rows[0]["GO_SWIFT_SFMS_Debit_Details"].ToString();
                txt_GO_Swift_SFMS_Credit_Details.Text = dt.Rows[0]["GO_SWIFT_SFMS_Credit_Details"].ToString();

                txt_GO_Swift_SFMS_Scheme_no.Text = dt.Rows[0]["GO_SWIFT_SFMS_SchemeNo"].ToString();
                txt_GO_Swift_SFMS_Debit_FUND.Text = dt.Rows[0]["GO_SWIFT_SFMS_Debit_Fund"].ToString();
                txt_GO_Swift_SFMS_Debit_Check_No.Text = dt.Rows[0]["GO_SWIFT_SFMS_Debit_CheckNo"].ToString();
                txt_GO_Swift_SFMS_Debit_Available.Text = dt.Rows[0]["GO_SWIFT_SFMS_Debit_Available"].ToString();
                txt_GO_Swift_SFMS_Debit_Division.Text = dt.Rows[0]["GO_SWIFT_SFMS_Debit_Division"].ToString();
                txt_GO_Swift_SFMS_Debit_Inter_Amount.Text = dt.Rows[0]["GO_SWIFT_SFMS_Debit_InterAmt"].ToString();
                txt_GO_Swift_SFMS_Debit_Inter_Rate.Text = dt.Rows[0]["GO_SWIFT_SFMS_Debit_InterRate"].ToString();
            }
            else
            {
                panalGO_Swift_SFMS.Visible = false;
            }
            // Swift Details
            if (dt.Rows[0]["MT412_Flag"].ToString() == "Y")
            {
                rdb_swift_412.Checked = true;
            }
            else
            {
                if (dt.Rows[0]["MT999C_Flag"].ToString() == "Y")
                {
                    rdb_swift_999.Checked = true;
                }
                else
                {
                    if (dt.Rows[0]["MT499_Flag"].ToString() == "Y")
                    {
                        rdb_swift_499.Checked = true;
                    }
                    else
                    {
                        rdb_swift_None.Checked = true;
                    }
                }
            }

            txt_Narrative_1.Text = dt.Rows[0]["Narrative412_1"].ToString();
            txt_Narrative_2.Text = dt.Rows[0]["Narrative412_2"].ToString();
            txt_Narrative_3.Text = dt.Rows[0]["Narrative412_3"].ToString();
            txt_Narrative_4.Text = dt.Rows[0]["Narrative412_4"].ToString();
            txt_Narrative_5.Text = dt.Rows[0]["Narrative412_5"].ToString();
            txt_Narrative_6.Text = dt.Rows[0]["Narrative412_6"].ToString();

            Narrative4991.Text = dt.Rows[0]["Narrative4991"].ToString();
            Narrative4992.Text = dt.Rows[0]["Narrative4992"].ToString();
            Narrative4993.Text = dt.Rows[0]["Narrative4993"].ToString();
            Narrative4994.Text = dt.Rows[0]["Narrative4994"].ToString();
            Narrative4995.Text = dt.Rows[0]["Narrative4995"].ToString();
            Narrative4996.Text = dt.Rows[0]["Narrative4996"].ToString();
            Narrative4997.Text = dt.Rows[0]["Narrative4997"].ToString();
            Narrative4998.Text = dt.Rows[0]["Narrative4998"].ToString();
            Narrative4999.Text = dt.Rows[0]["Narrative4999"].ToString();
            Narrative49910.Text = dt.Rows[0]["Narrative49910"].ToString();
            Narrative49911.Text = dt.Rows[0]["Narrative49911"].ToString();
            Narrative49912.Text = dt.Rows[0]["Narrative49912"].ToString();
            Narrative49913.Text = dt.Rows[0]["Narrative49913"].ToString();
            Narrative49914.Text = dt.Rows[0]["Narrative49914"].ToString();
            Narrative49915.Text = dt.Rows[0]["Narrative49915"].ToString();
            Narrative49916.Text = dt.Rows[0]["Narrative49916"].ToString();
            Narrative49917.Text = dt.Rows[0]["Narrative49917"].ToString();
            Narrative49918.Text = dt.Rows[0]["Narrative49918"].ToString();
            Narrative49919.Text = dt.Rows[0]["Narrative49919"].ToString();
            Narrative49920.Text = dt.Rows[0]["Narrative49920"].ToString();
            Narrative49921.Text = dt.Rows[0]["Narrative49921"].ToString();
            Narrative49922.Text = dt.Rows[0]["Narrative49922"].ToString();
            Narrative49923.Text = dt.Rows[0]["Narrative49923"].ToString();
            Narrative49924.Text = dt.Rows[0]["Narrative49924"].ToString();
            Narrative49925.Text = dt.Rows[0]["Narrative49925"].ToString();
            Narrative49926.Text = dt.Rows[0]["Narrative49926"].ToString();
            Narrative49927.Text = dt.Rows[0]["Narrative49927"].ToString();
            Narrative49928.Text = dt.Rows[0]["Narrative49928"].ToString();
            Narrative49929.Text = dt.Rows[0]["Narrative49929"].ToString();
            Narrative49930.Text = dt.Rows[0]["Narrative49930"].ToString();
            Narrative49931.Text = dt.Rows[0]["Narrative49931"].ToString();
            Narrative49932.Text = dt.Rows[0]["Narrative49932"].ToString();
            Narrative49933.Text = dt.Rows[0]["Narrative49933"].ToString();
            Narrative49934.Text = dt.Rows[0]["Narrative49934"].ToString();
            Narrative49935.Text = dt.Rows[0]["Narrative49935"].ToString();
        }
    }
    private void ToggleDocType(string DocType, string DocFLC_ILC_Type)
    {
        switch (DocType)
        {
            case "ICU": //Collection_Usance
                string Foreign_Local = "";
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
    private void fillCustomerMasterDetails()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@custid", SqlDbType.VarChar);
        p1.Value = txtCustomer_ID.Text;
        string _query = "TF_rptGetCustomerMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCustomerDesc.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
            lblCustomerDesc.ToolTip = lblCustomerDesc.Text;
            hdnCustAbbr.Value = dt.Rows[0]["Cust_Abbr"].ToString().Trim();  // Added by Bhupen for Lei changes 14112022
            if (lblCustomerDesc.Text.Length > 30)
            {
                lblCustomerDesc.Text = lblCustomerDesc.Text.Substring(0, 30) + "...";
            }
        }
        else
        {
            txtCustomer_ID.Text = "";
            lblCustomerDesc.Text = "";
        }

    }
    protected void chkGO_Swift_SFMS_OnCheckedChanged(object sender, EventArgs e)
    {
        GO_Swift_SFMS_Toggel();
    }
    protected void GO_Swift_SFMS_Toggel()
    {
        if (chkGO_Swift_SFMS.Checked == false)
        {
            panalGO_Swift_SFMS.Visible = false;
        }
        else if (chkGO_Swift_SFMS.Checked == true)
        {
            panalGO_Swift_SFMS.Visible = true;

            txt_GO_Swift_SFMS_SectionNo.Text = "05";
            txt_GO_Swift_SFMS_Remarks.Text = "SWIFT/SFMS CHARGES";

            txt_GO_Swift_SFMS_Debit_Code.Text = "D";
            txt_GO_Swift_SFMS_Debit_Curr.Text = "INR";

            txt_GO_Swift_SFMS_Debit_AdPrint.Text = "Y";
            txt_GO_Swift_SFMS_Debit_Details.Text = "SWIFT/SFMS CHARGES";

            txt_GO_Swift_SFMS_Credit_Code.Text = "C";
            txt_GO_Swift_SFMS_Credit_Curr.Text = "INR";

            txt_GO_Swift_SFMS_Credit_Cust_AcCode.Text = "67109";
            txt_GO_Swift_SFMS_Credit_Details.Text = "SWIFT/SFMS CHARGES";

        }

    }
    protected void makeReadOnly()
    {
        txtDocNo.Enabled = false;
        txtValueDate.Enabled = false;
        txt_Accepted_Date.Enabled = false;
        txt_Maturity_Date.Enabled = false;
        txtCustomer_ID.Enabled = false;
        txtAttn.Enabled = false;
        txt_Expected_InterestAmt.Enabled = false;
        txt_Received_InterestAmt.Enabled = false;
        //General Operation
        // //GO_Swift_SFMS
        chkGO_Swift_SFMS.Enabled = false;
        txt_GO_Swift_SFMS_Ref_No.Enabled = false;
        txt_GO_Swift_SFMS_Comment.Enabled = false;
        txt_GO_Swift_SFMS_SectionNo.Enabled = false;
        txt_GO_Swift_SFMS_Remarks.Enabled = false;
        txt_GO_Swift_SFMS_Memo.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Code.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Curr.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Amt.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Cust.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Cust_AcCode.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Cust_AccNo.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Exch_Curr.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Exch_Rate.Enabled = false;
        txt_GO_Swift_SFMS_Debit_AdPrint.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Details.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Entity.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Code.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Curr.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Amt.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Cust.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Cust_AcCode.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Cust_AccNo.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Exch_Curr.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Exch_Rate.Enabled = false;
        txt_GO_Swift_SFMS_Credit_AdPrint.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Details.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Entity.Enabled = false;

        txt_GO_Swift_SFMS_Scheme_no.Enabled = false;
        txt_GO_Swift_SFMS_Debit_FUND.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Check_No.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Available.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Division.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Inter_Amount.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Inter_Rate.Enabled = false;

        txt_GO_Swift_SFMS_Credit_FUND.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Check_No.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Available.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Division.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Inter_Amount.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Inter_Rate.Enabled = false;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_IMP_BOOKING_OF_IBD_AND_ACC_Checker_View.aspx", true);
    }
    protected void btnDocNext_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbDocumentGO;
    }
    protected void btnGO_Prev_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbDocumentDetails;
    }
    protected void btnGO_Next_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbSwiftDetails;
    }
    protected void btnSwift_Prev_Click(object sender, EventArgs e)
    {
        TabContainerMain.ActiveTab = tbDocumentGO;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", hdnDocNo.Value.Trim());
        string AR = "";
        if (ddlApproveReject.SelectedIndex == 1)
        {
            AR = "A";
            GBaseFileCreation();
            CreateSwiftSFMSFile();
            if (chkGO_Swift_SFMS.Checked)
            {
                GBaseFileCreationGOSwiftSFMS();
            }
        }
        if (ddlApproveReject.SelectedIndex == 2)
        {
            AR = "R";
        }
        Lei_Audit();
        SqlParameter P_Status = new SqlParameter("@Status", AR);
        SqlParameter P_RejectReason = new SqlParameter("@RejectReason", hdnRejectReason.Value.Trim());
        SqlParameter P_checkby = new SqlParameter("@checkby", hdnUserName.Value.Trim());
        string Result = obj.SaveDeleteData("TF_IMP_ChekerApproveRejectAcceptance", P_DocNo, P_Status, P_RejectReason, P_checkby);
        Response.Redirect("TF_IMP_BOOKING_OF_IBD_AND_ACC_Checker_View.aspx?DocNo=" + Result.Substring(7) + "&AR=" + AR, true);
    }
    public void GBaseFileCreation()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter PRefNo = new SqlParameter("@REFNo", SqlDbType.VarChar);
        PRefNo.Value = txtDocNo.Text.Trim();
        DataTable dt = objData1.getData("TF_IMP_GBaseFileCreation_Acceptance_Collection", PRefNo);
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
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Acceptance/GBASE/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = _directoryPath + "/" + txtDocNo.Text.Trim() + "_GBase" + ".xlsx";
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

            }
        }
        else
        {
        }
    }
    public void GBaseFileCreationGOSwiftSFMS()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter PRefNo = new SqlParameter("@REFNo", SqlDbType.VarChar);
        PRefNo.Value = txtDocNo.Text.Trim();
        DataTable dt = objData1.getData("TF_IMP_GBaseFileCreation_Acceptance_GOSF", PRefNo);
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
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Acceptance/GBASE/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = _directoryPath + "/" + txtDocNo.Text.Trim() + "_GBase_GOSwiftSFMS" + ".xlsx";
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
        string FileType;
        DeleteDataForSwiftSTP();
        if (hdnNegoRemiBankType.Value == "FOREIGN")
        {
            FileType = "SWIFT";

            if (rdb_swift_412.Checked)
            {
                //CreateSwift412(FileType);
                CreateSwift412_XML(FileType);
                InsertDataForSwiftSTP("MT412");
            }
            else if (rdb_swift_499.Checked)
            {
                //CreateSwift499(FileType);
                CreateSwift499_XML(FileType);
                InsertDataForSwiftSTP("MT499");
            }
            else if (rdb_swift_999.Checked)
            {
                //CreateSwift999(FileType);
                CreateSwift999_XML(FileType);
                InsertDataForSwiftSTP("MT999C");
            }
        }
        else if (hdnNegoRemiBankType.Value == "LOCAL")
        {
            FileType = "SFMS";

            if (rdb_swift_412.Checked)
            {
                CreateSwift412(FileType);
            }
            else if (rdb_swift_499.Checked)
            {
                CreateSwift499(FileType);
            }
            else if (rdb_swift_999.Checked)
            {
                CreateSwift999(FileType);
            }
        }
    }
    public void CreateSwift412(string FileType)
    {
        string _directoryPath = ""; string Query = "";
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
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Acceptance/SWIFT/MT412/" + MTodayDate + "/");
            Query = "TF_IMP_Swift_MT412_FileCreation";
        }
        else
        {
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Acceptance/SFMS/SFMS412/" + MTodayDate + "/");
            Query = "TF_IMP_Swift_MT412_FileCreation";
        }
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
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT412.xlsx";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN412.xlsx";
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
                        ws.Cells["B1"].Value = "Reciver Address";
                    }
                    ws.Cells["C1"].Value = dt.Rows[0]["To"].ToString();
                    ws.Cells["B2"].Value = "Transation Reference Number";
                    ws.Cells["A2"].Value = "[20]";
                    ws.Cells["C2"].Value = dt.Rows[0]["TransactionReferenceNumber"].ToString();
                    ws.Cells["B3"].Value = "Related Reference";
                    ws.Cells["A3"].Value = "[21]";
                    ws.Cells["C3"].Value = dt.Rows[0]["RelatedReference"].ToString();
                    ws.Cells["B4"].Value = "Maturity Date";
                    ws.Cells["A4"].Value = "[32A]";
                    ws.Cells["C4"].Value = dt.Rows[0]["MaturityDate"].ToString();
                    ws.Cells["D4"].Value = dt.Rows[0]["CurrencyCode"].ToString();
                    ws.Cells["E4"].Value = dt.Rows[0]["AmountAccepted"].ToString();

                    ws.Cells["B5"].Value = "Sender to Receiver Information";
                    ws.Cells["A5"].Value = "[72]";
                    ws.Cells["C5"].Value = dt.Rows[0]["Narrative1"].ToString();
                    int _Ecol = 6;
                    if (dt.Rows[0]["Narrative2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Narrative2"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Narrative3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Narrative3"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Narrative4"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Narrative4"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Narrative5"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Narrative5"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Narrative6"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Narrative6"].ToString();
                        _Ecol++;
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
        string _directoryPath = ""; string Query = "";
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
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Acceptance/SWIFT/MT499/" + MTodayDate + "/");
            Query = "TF_IMP_Swift_MT499C_FileCreation";
        }
        else
        {
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Acceptance/SFMS/SFMS499/" + MTodayDate + "/");
            Query = "TF_IMP_Swift_MT499C_FileCreation";
        }
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
                        ws.Cells["B1"].Value = "Reciver Address";
                    }
                    ws.Cells["B2"].Value = "Transation Reference Number";
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
    public void CreateSwift999(string FileType)
    {
        string _directoryPath = ""; string Query = "";
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
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Acceptance/SWIFT/MT999/" + MTodayDate + "/");
            Query = "TF_IMP_Swift_MT412_FileCreation";
        }
        else
        {
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Acceptance/SFMS/SFMS999/" + MTodayDate + "/");
            Query = "TF_IMP_Swift_MT412_FileCreation";
        }
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
                        ws.Cells["B1"].Value = "Reciver Address";
                    }
                    ws.Cells["C1"].Value = dt.Rows[0]["To"].ToString();
                    ws.Cells["B2"].Value = "Transation Reference Number";
                    ws.Cells["A2"].Value = "[20]";
                    ws.Cells["C2"].Value = dt.Rows[0]["TransactionReferenceNumber"].ToString();
                    ws.Cells["B3"].Value = "Related Reference";
                    ws.Cells["A3"].Value = "[21]";
                    ws.Cells["C3"].Value = dt.Rows[0]["RelatedReference"].ToString();
                    ws.Cells["B4"].Value = "Narrative";
                    ws.Cells["A4"].Value = "[79]";
                    ws.Cells["C4"].Value = "KIND ATTN: TRADE FINANCE DEPT.";
                    ws.Cells["C5"].Value = "KINDLY TREAT THIS MESSAGE AS MT 412, AS WE DON'T";
                    ws.Cells["C6"].Value = "HAVE DIRECT RMA WITH YOUR GOOD BANK.";
                    ws.Cells["C7"].Value = "DETAILS OF MESSAGE GIVEN BELOW.";


                    ws.Cells["C8"].Value = "20: " + dt.Rows[0]["TransactionReferenceNumber"].ToString();
                    ws.Cells["C9"].Value = "21: " + dt.Rows[0]["RelatedReference"].ToString();
                    ws.Cells["C10"].Value = "32A: " + dt.Rows[0]["MaturityDate"].ToString() + " " + dt.Rows[0]["CurrencyCode"].ToString() + " " + dt.Rows[0]["AmountAccepted"].ToString();
                    ws.Cells["C11"].Value = dt.Rows[0]["Narrative1"].ToString();
                    int _Ecol = 12;
                    if (dt.Rows[0]["Narrative2"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Narrative2"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Narrative3"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Narrative3"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Narrative4"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Narrative4"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Narrative5"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Narrative5"].ToString();
                        _Ecol++;
                    }
                    if (dt.Rows[0]["Narrative6"].ToString() != "")
                    {
                        ws.Cells["C" + _Ecol].Value = dt.Rows[0]["Narrative6"].ToString();
                        _Ecol++;
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
    //------------------------27/01/2020---------------------------------------------------
    public void CreateSwift412_XML(string FileType)
    {
        TF_DATA objDate1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objDate1.getData("TF_IMP_Swift_MT412_FileCreation_XML", P_DocNo);
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
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SWIFT_XML/Acceptance/MT412/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SFMSXML/Acceptance/SFMS412/" + MTodayDate + "/");
            }

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string _filePath = "";
            if (FileType == "SWIFT")
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT412_" + TodayDate1 + ".xml";
            }
            else
            {
                _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN412_" + TodayDate1 + ".xml";
            }
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            if (dt.Rows.Count > 0)
            {
                string sendertrn = dt.Rows[0]["TransactionReferenceNumber"].ToString();
                string RelatedReferenc = dt.Rows[0]["RelatedReference"].ToString();
                string MaturityDateCurrencyCodeAmountAccepted = dt.Rows[0]["MaturityDate"].ToString() +
                                                                  dt.Rows[0]["CurrencyCode"].ToString();
                #region AmountAccepted
                if (dt.Rows[0]["AmountAccepted"].ToString() != "")
                {
                    if (dt.Rows[0]["AmountAccepted"].ToString().Contains("."))
                    {
                        if (dt.Rows[0]["AmountAccepted"].ToString().Contains(".00"))
                        {
                            MaturityDateCurrencyCodeAmountAccepted = MaturityDateCurrencyCodeAmountAccepted + dt.Rows[0]["AmountAccepted"].ToString().Replace(".00", ",");
                        }
                        else
                        {
                            MaturityDateCurrencyCodeAmountAccepted = MaturityDateCurrencyCodeAmountAccepted + dt.Rows[0]["AmountAccepted"].ToString().Replace(".", ",");
                        }
                    }
                    else
                    {
                        MaturityDateCurrencyCodeAmountAccepted = MaturityDateCurrencyCodeAmountAccepted + dt.Rows[0]["AmountAccepted"].ToString() + ",";
                    }
                }
                #endregion
                #region Sender To Receiver
                string sendertoreceiver = "";
                if (dt.Rows[0]["Narrative1"].ToString() != "")
                {
                    sendertoreceiver = dt.Rows[0]["Narrative1"].ToString().Replace("-", "");
                }
                if (dt.Rows[0]["Narrative2"].ToString() != "")
                {
                    sendertoreceiver = sendertoreceiver + "," + dt.Rows[0]["Narrative2"].ToString().Replace("-", "");
                }
                if (dt.Rows[0]["Narrative3"].ToString() != "")
                {
                    sendertoreceiver = sendertoreceiver + ',' + dt.Rows[0]["Narrative3"].ToString().Replace("-", "");
                }
                if (dt.Rows[0]["Narrative4"].ToString() != "")
                {
                    sendertoreceiver = sendertoreceiver + "," + dt.Rows[0]["Narrative4"].ToString().Replace("-", "");
                }
                if (dt.Rows[0]["Narrative5"].ToString() != "")
                {
                    sendertoreceiver = sendertoreceiver + "," + dt.Rows[0]["Narrative5"].ToString().Replace("-", "");
                }
                if (dt.Rows[0]["Narrative6"].ToString() != "")
                {
                    sendertoreceiver = sendertoreceiver + "," + dt.Rows[0]["Narrative6"].ToString().Replace("-", "");
                }
                if (sendertoreceiver.Length != 0)
                {
                    string comma = sendertoreceiver.Trim().Substring(0, 1);
                    if (comma == ",") { sendertoreceiver = sendertoreceiver.Remove(0, 1); }
                    comma = "";
                }
                #endregion

                sw.WriteLine("<reqMT" + dt.Rows[0]["XMLStartTag"].ToString() + ">");
                sw.WriteLine("<Sender>" + dt.Rows[0]["Sender"].ToString() + "</Sender>");
                sw.WriteLine("<MessageType>" + dt.Rows[0]["MessageType"].ToString() + "</MessageType>");
                sw.WriteLine("<Receiver>" + dt.Rows[0]["To"].ToString() + "</Receiver>");
                sw.WriteLine("<SendingBanksTRN>" + sendertrn + "</SendingBanksTRN>");
                sw.WriteLine("<RelatedReference>" + RelatedReferenc + "</RelatedReference>");
                sw.WriteLine("<MaturityDate>" + MaturityDateCurrencyCodeAmountAccepted + "</MaturityDate>");
                sw.WriteLine("<SendertoReceiverInformation>" + sendertoreceiver + "</SendertoReceiverInformation>");
                sw.WriteLine("</reqMT>");
            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
    } // Bhupendra swift changes
    public void CreateSwift499_XML(string FileType)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Swift_MT499C_FileCreation_XML", P_DocNo);
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
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SWIFT_XML/Acceptance/MT499/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SFMSXML/Acceptance/SFMS499/" + MTodayDate + "/");
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
                #region Narrative
                if (dt.Rows[0]["Narrative1"].ToString() != "")
                {
                    Narrative = dt.Rows[0]["Narrative1"].ToString().Replace("/", "");
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
                #endregion
                sw.WriteLine("<reqMT" + dt.Rows[0]["XMLStartTag"].ToString() + ">");
                sw.WriteLine("<Sender>" + dt.Rows[0]["Sender"].ToString() + "</Sender>");
                sw.WriteLine("<MessageType>" + dt.Rows[0]["MessageType"].ToString() + "</MessageType>");
                sw.WriteLine("<Receiver>" + dt.Rows[0]["To"].ToString() + "</Receiver>");
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
    public void CreateSwift999_XML(string FileType)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        DataTable dt = objData1.getData("TF_IMP_Swift_MT999_ICU_FileCreation_XML", P_DocNo);
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
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SWIFT_XML/Acceptance/MT999/" + MTodayDate + "/");
            }
            else
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/SFMSXML/Acceptance/SFMS999/" + MTodayDate + "/");
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
                Narrative = Narrative + "KIND ATTN: TRADE FINANCE DEPT.";
                Narrative = Narrative + " KINDLY TREAT THIS MESSAGE AS MT 412, AS WE DO NOT";
                Narrative = Narrative + " HAVE DIRECT RMA WITH YOUR GOOD BANK.";
                Narrative = Narrative + " DETAILS OF MESSAGE GIVEN BELOW.";
                Narrative = Narrative + " 20: " + dt.Rows[0]["TransactionReferenceNumber"].ToString();
                Narrative = Narrative + " 21: " + dt.Rows[0]["RelatedReference"].ToString();
                Narrative = Narrative + " 32A: " + dt.Rows[0]["MaturityDate"].ToString() + " " + dt.Rows[0]["CurrencyCode"].ToString() + " " + dt.Rows[0]["AmountAccepted"].ToString();
                #region Narrative
                if (dt.Rows[0]["Narrative1"].ToString() != "")
                {
                    Narrative = Narrative + " " + dt.Rows[0]["Narrative1"].ToString();
                }
                if (dt.Rows[0]["Narrative2"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative2"].ToString();
                }
                if (dt.Rows[0]["Narrative3"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative3"].ToString();
                }
                if (dt.Rows[0]["Narrative4"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative4"].ToString();
                }
                if (dt.Rows[0]["Narrative5"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative5"].ToString();
                }
                if (dt.Rows[0]["Narrative6"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative6"].ToString();
                }
                if (Narrative.Length != 0)
                {
                    string comma = Narrative.Trim().Substring(0, 1);
                    if (comma == ",") { Narrative = Narrative.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                sw.WriteLine("<reqMT" + dt.Rows[0]["XMLStartTag"].ToString() + ">");
                sw.WriteLine("<Sender>" + dt.Rows[0]["Sender"].ToString() + "</Sender>");
                sw.WriteLine("<MessageType>" + dt.Rows[0]["MessageType"].ToString() + "</MessageType>");
                sw.WriteLine("<Receiver>" + dt.Rows[0]["To"].ToString() + "</Receiver>");
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
    //--------------------------------------------------------------------------------------
    protected void InsertDataForSwiftSTP(string SwiftType)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P1 = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        SqlParameter P2 = new SqlParameter("@SwiftType", SwiftType);
        string result = obj.SaveDeleteData("TF_SWIFT_STP_InsertAcceptanceData", P1, P2);
    }
    protected void DeleteDataForSwiftSTP()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P1 = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        string result = obj.SaveDeleteData("TF_SWIFT_STP_DeleteAcceptanceData", P1);
    }

    //----------------Added by bhupen on 14112022 for LEI-----------------------------------//
    protected void GetDrawee_Details_LEI()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
        DataTable dt = new DataTable();
        dt = obj.getData("TF_IMP_GetLodgementLEIdetails_acc", PDocNo);
        if (dt.Rows.Count > 0)
        {
            hdnDrawerno.Value = dt.Rows[0]["Drawer"].ToString();
            if (hdnDrawerno.Value != "")
            {
                SqlParameter CustAcno = new SqlParameter("@CustAcno", txtCustomer_ID.Text);
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
            SqlParameter p2 = new SqlParameter("@DueDate", txt_Maturity_Date.Text.ToString().Trim());
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
                    //ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('LEI for this customer is Expired. Kindly Check.')", true);
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
                            ddlApproveReject.Enabled = false;
                            btn_Verify.Visible = true;
                        }
                        else
                        {
                            label1.Text = "Transaction Bill Amt is less than LEI Thresold Limit.";
                            label1.ForeColor = System.Drawing.Color.Green;
                            label1.Font.Size = 10;
                            hdnLeiFlag.Value = "N";
                            ddlApproveReject.Enabled = true;
                            btn_Verify.Visible = false;
                        }
                        Check_cust_Leiverify();
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
    private void Check_DraweeLEINODetails()
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@CustAcno", txtCustomer_ID.Text.Trim());
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
            SqlParameter p1 = new SqlParameter("@CustAcno", txtCustomer_ID.Text.Trim());
            SqlParameter p2 = new SqlParameter("@Draweename", hdnDrawer.Value.Trim());
            SqlParameter p3 = new SqlParameter("@DueDate", txt_Maturity_Date.Text.ToString().Trim());
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
                SqlParameter P_CustAcno = new SqlParameter("@CustAcNo", txtCustomer_ID.Text.Trim());
                SqlParameter P_Cust_Abbr = new SqlParameter("@Cust_Abbr", hdnCustAbbr.Value.Trim());
                SqlParameter P_Cust_Name = new SqlParameter("@Cust_Name", lblCustomerDesc.Text.Trim());
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
                SqlParameter P_DueDate = new SqlParameter("@DueDate", txt_Maturity_Date.Text.Trim());
                SqlParameter P_LEI_Flag = new SqlParameter("@LEI_Flag", hdnLeiFlag.Value.Trim());
                SqlParameter P_CustLEI_Flag = new SqlParameter("@Cust_LEIFlag", hdncustleiflag.Value.Trim());
                SqlParameter P_Status = new SqlParameter("@status", ddlApproveReject.SelectedItem.Text.Trim());
                SqlParameter P_Module = new SqlParameter("@Module", "A");
                SqlParameter P_AddedBy = new SqlParameter("@user", hdnUserName.Value.Trim());
                SqlParameter P_AddedDate = new SqlParameter("@uploadingdate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

                string _queryLEI = "TF_IMP_AddUpdate_LEITransaction";

                string _result = obj.SaveDeleteData(_queryLEI, P_BranchCode, P_DocNo, P_DocType, P_Sight_Usance, P_CustAcno, P_Cust_Abbr, P_Cust_Name, P_Cust_LEI,
                   P_Cust_LEI_Expiry, P_Cust_LEI_Expired, P_Drawee_Name, P_Drawee_LEI, P_Drawee_LEI_Expiry, P_Drawee_LEI_Expired, P_BillAmount, P_txtCurr, P_Exchrate,
                   P_LodgementDate, P_DueDate, P_LEI_Flag,P_CustLEI_Flag, P_Status, P_Module, P_AddedBy, P_AddedDate);
            }
            catch (Exception ex)
            {
                //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
            }
        }
    }
    private void Check_cust_Leiverify()
    {
        if (lblForeign_Local.Text == "FOREIGN" || lblForeign_Local.Text == "Foreign")
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@Cust_Abbr", hdnCustAbbr.Value.Trim());
            SqlParameter p2 = new SqlParameter("@BranchCode", hdnBranchCode.Value.ToString());
            //SqlParameter p3 = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
            string _query = "TF_IMP_CheckLEIflaggedCust_Count_Checker";
            DataTable dt = objData.getData(_query, p1, p2);
            if (dt.Rows.Count > 0)
            {
                //hdncustleiflag.Value = dt.Rows[0]["Cust_Lei_Flag"].ToString();
                //if (hdncustleiflag.Value == "R")
                //{
                    ddlApproveReject.Enabled = false;
                    btn_Verify.Visible = true;
                    ReccuringLEI.Visible = true;
                    ReccuringLEI.Text = "This is Recurring LEI Customer.";
                    ReccuringLEI.ForeColor = System.Drawing.Color.Red;
                    ReccuringLEI.Font.Size = 10;
                //}
            }
        }
    }
    private void Check_Lei_Verified()
    {
        if (lblForeign_Local.Text == "FOREIGN" || lblForeign_Local.Text == "Foreign")
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
            SqlParameter p2 = new SqlParameter("@Module", "A");
            string _query = "TF_IMP_Check_LeiVerified";
            DataTable dt = objData.getData(_query, p1, p2);
            if (dt.Rows.Count > 0)
            {
                LeiVerified.Visible = true;
                if (label1.Text == "Transaction Bill Amt is Greater than LEI Thresold Limit.Please Verify LEI details.")
                {
                    label1.Text = "Transaction Bill Amt is Greater than LEI Thresold Limit.";
                }
                LeiVerified.Text = "LEI Already Verified.";
                LeiVerified.ForeColor = System.Drawing.Color.Red;
                LeiVerified.Font.Size = 10;
            }
            else
            {
                LeiVerified.Visible = false;
            }
        }
    }
    private void Check_Lei_RecurringStatus()
    {
        if (lblForeign_Local.Text == "FOREIGN" || lblForeign_Local.Text == "Foreign")
        {
            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
            SqlParameter p2 = new SqlParameter("@Module", "A");
            string _query = "TF_IMP_Check_LeiRecurring";
            DataTable dt = objData.getData(_query, p1, p2);
            if (dt.Rows.Count > 0)
            {
                string cust_lei_flag = dt.Rows[0]["Cust_Lei_Flag"].ToString();
                string Tstatus = dt.Rows[0]["TStatus"].ToString();
                if (cust_lei_flag.ToString().Trim() == "" && Tstatus.ToString().Trim() == "A")
                {
                    ReccuringLEI.Visible = false;
                }
            }
        }
    }
    protected void btn_Verify_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCustomer_ID.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Please Select Customer for Verifying LEI details.')", true);
                txtCustomer_ID.Focus();
            }
            else if (lblBillAmt.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Kindly Fill Amount for Verifying LEI details.')", true);
                lblBillAmt.Focus();
            }
            else if (txt_Maturity_Date.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Kindly Fill Due date for Verifying LEI details.')", true);
                txt_Maturity_Date.Focus();
            }
            else
            {
                GetDrawee_Details_LEI();
                Check_LEINODetails();
                Check_LEINO_ExpirydateDetails();
                Check_DraweeLEINODetails();
                Check_DraweeLEINO_ExpirydateDetails();
                ddlApproveReject.Enabled = true;

                String CustLEINo = hdnleino.Value + " " + lblLEI_Remark.Text + "\\n";
                String CustLEIExpiry = hdnleiexpiry.Value + " " + lblLEIExpiry_Remark.Text + "\\n";
                String DrawerLEINo = hdnDraweeleino.Value + " " + lblLEI_Remark_Drawee.Text + "\\n";
                String DrawerLEIExpiry = hdnDraweeleiexpiry.Value + " " + lblLEIExpiry_Remark_Drawee.Text + "\\n";

                String LEIMSG = @"Customer LEI No : " + CustLEINo + "Customer LEI Expiry : " + CustLEIExpiry + "Drawer LEI : " + DrawerLEINo + "Drawer LEI Expiry : " + DrawerLEIExpiry;
                // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "messege", "MyConfirm('" + LEIMSG + "')", true);
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('" + LEIMSG + "')", true);
            }
        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }
    //------------------------------------End------------------------------------------//
}

