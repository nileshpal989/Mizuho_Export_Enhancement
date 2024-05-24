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

public partial class IMP_Transactions_TF_IMP_Collection_Acceptance_Maker : System.Web.UI.Page
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
                TabContainerMain.ActiveTab = tbDocumentDetails;
                if (Request.QueryString["DocNo"] == null)
                {
                    Response.Redirect("TF_IMP_BOOKING_OF_IBD_AND_ACC_Maker_View.aspx", true);
                }
                else
                {
                    hdnBranchName.Value = Request.QueryString["BranchName"].Trim();
                    hdnDocType.Value = Request.QueryString["DocType"].Trim();
                    txtDocNo.Text = Request.QueryString["DocNo"].Trim();
                    makeReadOnly();
                    TF_DATA obj = new TF_DATA();
                    SqlParameter PDocNo = new SqlParameter("@DocNo", txtDocNo.Text.ToString());
                    string result = "";
                    result = obj.SaveDeleteData("TF_IMP_Check_Ref_In_Acceptance", PDocNo);
                    if (result == "exists")
                    {
                        hdnMode.Value = "Edit";
                    }
                    else
                    {
                        hdnMode.Value = "Add";
                    }

                    txtValueDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    txt_Accepted_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    if (hdnMode.Value == "Edit")
                    {
                        Fill_Logd_Details();
                        Fill_AcceptanceDetails();
                        Check_LEINO_ExchRateDetails();
                        Check_cust_Leiverify();
                    }
                    else
                    {
                        rdb_swift_None.Checked = true;
                        //txtValueDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                        //txt_Accepted_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                        Fill_Logd_Details();
                        Check_LEINO_ExchRateDetails();
                        Check_cust_Leiverify();
                    }
                }
            }
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
            hdnNegoRemiBankType.Value = dt.Rows[0]["Nego_Remit_Bank_Type"].ToString();
            hdnBranchCode.Value = dt.Rows[0]["Branch_Code"].ToString();  //Added by bhupen on 09112022
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
            //txtValueDate.Text = dt.Rows[0]["Acceptance_Value_Date"].ToString();
            //txt_Accepted_Date.Text = dt.Rows[0]["Acceptance_Value_Date"].ToString();
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

                txt_GO_Swift_SFMS_Credit_FUND.Text = dt.Rows[0]["GO_SWIFT_SFMS_Credit_Fund"].ToString();
                txt_GO_Swift_SFMS_Credit_Check_No.Text = dt.Rows[0]["GO_SWIFT_SFMS_Credit_CheckNo"].ToString();
                txt_GO_Swift_SFMS_Credit_Available.Text = dt.Rows[0]["GO_SWIFT_SFMS_Credit_Available"].ToString();
                txt_GO_Swift_SFMS_Credit_Division.Text = dt.Rows[0]["GO_SWIFT_SFMS_Credit_Division"].ToString();
                txt_GO_Swift_SFMS_Credit_Inter_Amount.Text = dt.Rows[0]["GO_SWIFT_SFMS_Credit_InterAmt"].ToString();
                txt_GO_Swift_SFMS_Credit_Inter_Rate.Text = dt.Rows[0]["GO_SWIFT_SFMS_Credit_InterRate"].ToString();
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

            if (dt.Rows[0]["Checker_Remark"].ToString() != "")
            {
                lblChecker_Remark.Text = "Checker Remark :- " + dt.Rows[0]["Checker_Remark"].ToString();
            }

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
            hdnCustAbbr.Value = dt.Rows[0]["Cust_Abbr"].ToString().Trim();  // Added by Bhupen for Lei changes 10112022
            lblCustomerDesc.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
            lblCustomerDesc.ToolTip = lblCustomerDesc.Text;
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
        //txt_Maturity_Date.Enabled = false;
        txtCustomer_ID.Enabled = false;
        btncal_Accepted_Date.Enabled = false;
        //btncal_Maturity_Date.Enabled = false;

        //General Operation
        // //GO_Swift_SFMS
        txt_GO_Swift_SFMS_Ref_No.Enabled = false;
        //txt_GO_Swift_SFMS_Debit_Cust_AcCode.Enabled = false;
        //txt_GO_Swift_SFMS_Debit_Cust.Enabled = false;
        //txt_GO_Swift_SFMS_Credit_Cust.Enabled = false;
        txt_GO_Swift_SFMS_SectionNo.Enabled = false;
        //txt_GO_Swift_SFMS_Remarks.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Code.Enabled = false;
        txt_GO_Swift_SFMS_Debit_Curr.Enabled = false;
        //txt_GO_Swift_SFMS_Debit_AdPrint.Enabled = false;
        //txt_GO_Swift_SFMS_Debit_Details.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Code.Enabled = false;
        txt_GO_Swift_SFMS_Credit_Curr.Enabled = false;
        //txt_GO_Swift_SFMS_Credit_Cust_AcCode.Enabled = false;
        //txt_GO_Swift_SFMS_Credit_Details.Enabled = false;
    }
    //----------------Added by bhupen on 09112022-----------------------------------//
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
                        Check_cust_Leiverify();
                    }
                }
                else
                {
                    lbl_Exch_rate.Text = "";
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
                SqlParameter P_Status = new SqlParameter("@status", "C");
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
                    hdncustleiflag.Value = "R";
                    btnSubmit.Enabled = false;
                    btn_Verify.Visible = true;
                    ReccuringLEI.Visible = true;
                    ReccuringLEI.Text = "This is Recurring LEI Customer.";
                    ReccuringLEI.ForeColor = System.Drawing.Color.Red;
                    ReccuringLEI.Font.Size = 10;
                //}
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
                btnSubmit.Enabled = true;

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
    protected void txt_Maturity_Date_TextChanged(object sender, EventArgs e)
    {
        if (lblForeign_Local.Text == "FOREIGN" || lblForeign_Local.Text == "Foreign")
        {
            Check_LEINO_ExchRateDetails();
        }
    }
    //------------------------------------End------------------------------------------//

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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        string Result = obj.SaveDeleteData("TF_IMP_SubmitAcceptaneForChecker", P_DocNo);
        if (Result == "Submit")
        {
            Lei_Audit();
            string _script = "";
            _script = "window.location='TF_IMP_BOOKING_OF_IBD_AND_ACC_Maker_View.aspx?result=Submit'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txtDocNo.Text.Trim());
        /*
        obj.SaveDeleteData("TF_IMP_ACCEPTANCE_IBD_ACC_LOGDetails", P_DocNo);
        obj.SaveDeleteData("TF_IMP_ACCEPTANCE_IBD_ACC_AuditRecords", P_DocNo);
        */
        Response.Redirect("TF_IMP_BOOKING_OF_IBD_AND_ACC_Maker_View.aspx", true);
    }
    [WebMethod]
    public static string AddUpdateAceptance(string hdnUserName, string _BranchName, string _txtDocNo, string _Document_Curr, string _txtValueDate,
        string _txt_Accepted_Date, string _txt_Maturity_Date, string _txtAttn, string _txt_Expected_InterestAmt, string _txt_Received_InterestAmt,
        ////  General_Operations Swift_SFMS_CHRG
        string _chkGO_Swift_SFMS, string _txt_GO_Swift_SFMS_Comment, string _txt_GO_Swift_SFMS_Ref_No,
        string _txt_GO_Swift_SFMS_SectionNo, string _txt_GO_Swift_SFMS_Remarks, string _txt_GO_Swift_SFMS_Memo,

        string _txt_GO_Swift_SFMS_Debit_Code, string _txt_GO_Swift_SFMS_Debit_Curr, string _txt_GO_Swift_SFMS_Debit_Amt,
        string _txt_GO_Swift_SFMS_Debit_Cust, string _txt_GO_Swift_SFMS_Debit_Cust_AcCode, string _txt_GO_Swift_SFMS_Debit_Cust_AccNo,
        string _txt_GO_Swift_SFMS_Debit_Exch_Curr, string _txt_GO_Swift_SFMS_Debit_Exch_Rate,
        string _txt_GO_Swift_SFMS_Debit_AdPrint, string _txt_GO_Swift_SFMS_Debit_Details, string _txt_GO_Swift_SFMS_Debit_Entity,

        string _txt_GO_Swift_SFMS_Credit_Code, string _txt_GO_Swift_SFMS_Credit_Curr, string _txt_GO_Swift_SFMS_Credit_Amt,
        string _txt_GO_Swift_SFMS_Credit_Cust, string _txt_GO_Swift_SFMS_Credit_Cust_AcCode, string _txt_GO_Swift_SFMS_Credit_Cust_AccNo,
        string _txt_GO_Swift_SFMS_Credit_Exch_Curr, string _txt_GO_Swift_SFMS_Credit_Exch_Rate,
        string _txt_GO_Swift_SFMS_Credit_AdPrint, string _txt_GO_Swift_SFMS_Credit_Details, string _txt_GO_Swift_SFMS_Credit_Entity,

        string _txt_GO_Swift_SFMS_Scheme_no,
        string _txt_GO_Swift_SFMS_Debit_FUND, string _txt_GO_Swift_SFMS_Debit_Check_No, string _txt_GO_Swift_SFMS_Debit_Available,
        string _txt_GO_Swift_SFMS_Debit_Division, string _txt_GO_Swift_SFMS_Debit_Inter_Amount, string _txt_GO_Swift_SFMS_Debit_Inter_Rate,
        string _txt_GO_Swift_SFMS_Credit_FUND, string _txt_GO_Swift_SFMS_Credit_Check_No, string _txt_GO_Swift_SFMS_Credit_Available,
        string _txt_GO_Swift_SFMS_Credit_Division, string _txt_GO_Swift_SFMS_Credit_Inter_Amount, string _txt_GO_Swift_SFMS_Credit_Inter_Rate,

        // Swift Details
        string Swift_412, string Swift_999C, string Swift_499,
        string _txt_Narrative1, string _txt_Narrative2, string _txt_Narrative3, string _txt_Narrative4, string _txt_Narrative5,
        string _txt_Narrative6,
        string _txt_Narrative499_1, string _txt_Narrative499_2, string _txt_Narrative499_3, string _txt_Narrative499_4, string _txt_Narrative499_5,
        string _txt_Narrative499_6, string _txt_Narrative499_7, string _txt_Narrative499_8, string _txt_Narrative499_9, string _txt_Narrative499_10,
        string _txt_Narrative499_11, string _txt_Narrative499_12, string _txt_Narrative499_13, string _txt_Narrative499_14, string _txt_Narrative499_15,
        string _txt_Narrative499_16, string _txt_Narrative499_17, string _txt_Narrative499_18, string _txt_Narrative499_19, string _txt_Narrative499_20,
        string _txt_Narrative499_21, string _txt_Narrative499_22, string _txt_Narrative499_23, string _txt_Narrative499_24, string _txt_Narrative499_25,
        string _txt_Narrative499_26, string _txt_Narrative499_27, string _txt_Narrative499_28, string _txt_Narrative499_29, string _txt_Narrative499_30,
        string _txt_Narrative499_31, string _txt_Narrative499_32, string _txt_Narrative499_33, string _txt_Narrative499_34, string _txt_Narrative499_35
        )
    {
        TF_DATA obj = new TF_DATA();

        SqlParameter P_BranchName = new SqlParameter("@BranchName", _BranchName);
        SqlParameter P_txtDocNo = new SqlParameter("@Document_No", _txtDocNo);
        //SqlParameter P_Document_Curr = new SqlParameter("@Document_Curr", _Document_Curr);

        SqlParameter P_txt_Maturity_Date = new SqlParameter("@Maturity_Date", _txt_Maturity_Date);
        SqlParameter P_txtValueDate = new SqlParameter("@Acceptance_Date", _txt_Accepted_Date);
        SqlParameter P_txtAttn = new SqlParameter("@Attn", _txtAttn);
        SqlParameter P_txt_Expected_InterestAmt = new SqlParameter("@Expected_Interest_Amount", _txt_Expected_InterestAmt);
        SqlParameter P_txt_Received_InterestAmt = new SqlParameter("@Received_Interest_Amount", _txt_Received_InterestAmt);
        //// General Operation SWIFT_SFMS
        SqlParameter P_GO_SWIFT_SFMS_Flag = new SqlParameter("@GO_SWIFT_SFMS_Flag", _chkGO_Swift_SFMS);
        SqlParameter P_GO_SWIFT_SFMS_Ref_No = new SqlParameter("@GO_SWIFT_SFMS_Ref_No", _txt_GO_Swift_SFMS_Ref_No);
        SqlParameter P_GO_SWIFT_SFMS_Remark = new SqlParameter("@GO_SWIFT_SFMS_Remark", _txt_GO_Swift_SFMS_Remarks);
        SqlParameter P_GO_SWIFT_SFMS_Section = new SqlParameter("@GO_SWIFT_SFMS_Section", _txt_GO_Swift_SFMS_SectionNo);
        SqlParameter P_GO_SWIFT_SFMS_Comment = new SqlParameter("@GO_SWIFT_SFMS_Comment", _txt_GO_Swift_SFMS_Comment);
        SqlParameter P_GO_SWIFT_SFMS_Memo = new SqlParameter("@GO_SWIFT_SFMS_Memo", _txt_GO_Swift_SFMS_Memo);

        SqlParameter P_GO_SWIFT_SFMS_Debit = new SqlParameter("@GO_SWIFT_SFMS_Debit", _txt_GO_Swift_SFMS_Debit_Code);
        SqlParameter P_GO_SWIFT_SFMS_Debit_Amt = new SqlParameter("@GO_SWIFT_SFMS_Debit_Amt", _txt_GO_Swift_SFMS_Debit_Amt);
        SqlParameter P_GO_SWIFT_SFMS_Debit_CCY = new SqlParameter("@GO_SWIFT_SFMS_Debit_CCY", _txt_GO_Swift_SFMS_Debit_Curr);

        SqlParameter P_GO_SWIFT_SFMS_Debit_Cust = new SqlParameter("@GO_SWIFT_SFMS_Debit_Cust", _txt_GO_Swift_SFMS_Debit_Cust);
        SqlParameter P_GO_SWIFT_SFMS_Debit_Cust_AcCode = new SqlParameter("@GO_SWIFT_SFMS_Debit_Cust_AcCode", _txt_GO_Swift_SFMS_Debit_Cust_AcCode);
        SqlParameter P_GO_SWIFT_SFMS_Debit_Cust_AccNo = new SqlParameter("@GO_SWIFT_SFMS_Debit_Cust_AccNo", _txt_GO_Swift_SFMS_Debit_Cust_AccNo);

        SqlParameter P_GO_SWIFT_SFMS_Debit_ExchRate = new SqlParameter("@GO_SWIFT_SFMS_Debit_ExchRate", _txt_GO_Swift_SFMS_Debit_Exch_Rate);
        SqlParameter P_GO_SWIFT_SFMS_Debit_ExchCCY = new SqlParameter("@GO_SWIFT_SFMS_Debit_ExchCCY", _txt_GO_Swift_SFMS_Debit_Exch_Curr);

        SqlParameter P_GO_SWIFT_SFMS_Debit_Advice_Print = new SqlParameter("@GO_SWIFT_SFMS_Debit_Advice_Print", _txt_GO_Swift_SFMS_Debit_AdPrint);
        SqlParameter P_GO_SWIFT_SFMS_Debit_Details = new SqlParameter("@GO_SWIFT_SFMS_Debit_Details", _txt_GO_Swift_SFMS_Debit_Details);
        SqlParameter P_GO_SWIFT_SFMS_Debit_Entity = new SqlParameter("@GO_SWIFT_SFMS_Debit_Entity", _txt_GO_Swift_SFMS_Debit_Entity);

        SqlParameter P_GO_SWIFT_SFMS_Credit = new SqlParameter("@GO_SWIFT_SFMS_Credit", _txt_GO_Swift_SFMS_Credit_Code);
        SqlParameter P_GO_SWIFT_SFMS_Credit_Amt = new SqlParameter("@GO_SWIFT_SFMS_Credit_Amt", _txt_GO_Swift_SFMS_Credit_Amt);
        SqlParameter P_GO_SWIFT_SFMS_Credit_CCY = new SqlParameter("@GO_SWIFT_SFMS_Credit_CCY", _txt_GO_Swift_SFMS_Credit_Curr);

        SqlParameter P_GO_SWIFT_SFMS_Credit_Cust = new SqlParameter("@GO_SWIFT_SFMS_Credit_Cust", _txt_GO_Swift_SFMS_Credit_Cust);
        SqlParameter P_GO_SWIFT_SFMS_Credit_Cust_AccNo = new SqlParameter("@GO_SWIFT_SFMS_Credit_Cust_AccNo", _txt_GO_Swift_SFMS_Credit_Cust_AccNo);
        SqlParameter P_GO_SWIFT_SFMS_Credit_Cust_AcCode = new SqlParameter("@GO_SWIFT_SFMS_Credit_Cust_AcCode", _txt_GO_Swift_SFMS_Credit_Cust_AcCode);

        SqlParameter P_GO_SWIFT_SFMS_Credit_ExchCCY = new SqlParameter("@GO_SWIFT_SFMS_Credit_ExchCCY", _txt_GO_Swift_SFMS_Credit_Exch_Curr);
        SqlParameter P_GO_SWIFT_SFMS_Credit_ExchRate = new SqlParameter("@GO_SWIFT_SFMS_Credit_ExchRate", _txt_GO_Swift_SFMS_Credit_Exch_Rate);
        SqlParameter P_GO_SWIFT_SFMS_Credit_Advice_Print = new SqlParameter("@GO_SWIFT_SFMS_Credit_Advice_Print", _txt_GO_Swift_SFMS_Credit_AdPrint);
        SqlParameter P_GO_SWIFT_SFMS_Credit_Details = new SqlParameter("@GO_SWIFT_SFMS_Credit_Details", _txt_GO_Swift_SFMS_Credit_Details);
        SqlParameter P_GO_SWIFT_SFMS_Credit_Entity = new SqlParameter("@GO_SWIFT_SFMS_Credit_Entity", _txt_GO_Swift_SFMS_Credit_Entity);

        SqlParameter P_txt_GO_Swift_SFMS_Scheme_no = new SqlParameter("@GO_SWIFT_SFMS_SchemeNo", _txt_GO_Swift_SFMS_Scheme_no.ToUpper());
        SqlParameter P_txt_GO_Swift_SFMS_Debit_FUND = new SqlParameter("@GO_SWIFT_SFMS_Debit_Fund", _txt_GO_Swift_SFMS_Debit_FUND.ToUpper());
        SqlParameter P_txt_GO_Swift_SFMS_Debit_Check_No = new SqlParameter("@GO_SWIFT_SFMS_Debit_CheckNo", _txt_GO_Swift_SFMS_Debit_Check_No.ToUpper());
        SqlParameter P_txt_GO_Swift_SFMS_Debit_Available = new SqlParameter("@GO_SWIFT_SFMS_Debit_Available", _txt_GO_Swift_SFMS_Debit_Available.ToUpper());
        SqlParameter P_txt_GO_Swift_SFMS_Debit_Division = new SqlParameter("@GO_SWIFT_SFMS_Debit_Division", _txt_GO_Swift_SFMS_Debit_Division.ToUpper());
        SqlParameter P_txt_GO_Swift_SFMS_Debit_Inter_Amount = new SqlParameter("@GO_SWIFT_SFMS_Debit_InterAmt", _txt_GO_Swift_SFMS_Debit_Inter_Amount.ToUpper());
        SqlParameter P_txt_GO_Swift_SFMS_Debit_Inter_Rate = new SqlParameter("@GO_SWIFT_SFMS_Debit_InterRate", _txt_GO_Swift_SFMS_Debit_Inter_Rate.ToUpper());
        SqlParameter P_txt_GO_Swift_SFMS_Credit_FUND = new SqlParameter("@GO_SWIFT_SFMS_Credit_Fund", _txt_GO_Swift_SFMS_Credit_FUND.ToUpper());
        SqlParameter P_txt_GO_Swift_SFMS_Credit_Check_No = new SqlParameter("@GO_SWIFT_SFMS_Credit_CheckNo", _txt_GO_Swift_SFMS_Credit_Check_No.ToUpper());
        SqlParameter P_txt_GO_Swift_SFMS_Credit_Available = new SqlParameter("@GO_SWIFT_SFMS_Credit_Available", _txt_GO_Swift_SFMS_Credit_Available.ToUpper());
        SqlParameter P_txt_GO_Swift_SFMS_Credit_Division = new SqlParameter("@GO_SWIFT_SFMS_Credit_Division", _txt_GO_Swift_SFMS_Credit_Division.ToUpper());
        SqlParameter P_txt_GO_Swift_SFMS_Credit_Inter_Amount = new SqlParameter("@GO_SWIFT_SFMS_Credit_InterAmt", _txt_GO_Swift_SFMS_Credit_Inter_Amount.ToUpper());
        SqlParameter P_txt_GO_Swift_SFMS_Credit_Inter_Rate = new SqlParameter("@GO_SWIFT_SFMS_Credit_InterRate", _txt_GO_Swift_SFMS_Credit_Inter_Rate.ToUpper());

        //Swift Details
        SqlParameter P_Swift_412 = new SqlParameter("@Swift_412", Swift_412.ToUpper());
        SqlParameter P_Swift_999C = new SqlParameter("@Swift_999C", Swift_999C.ToUpper());
        SqlParameter P_Swift_499 = new SqlParameter("@Swift_499", Swift_499.ToUpper());
        SqlParameter P_txt_Narrative1 = new SqlParameter("@412_Narrative1", _txt_Narrative1.ToUpper());
        SqlParameter P_txt_Narrative2 = new SqlParameter("@412_Narrative2", _txt_Narrative2.ToUpper());
        SqlParameter P_txt_Narrative3 = new SqlParameter("@412_Narrative3", _txt_Narrative3.ToUpper());
        SqlParameter P_txt_Narrative4 = new SqlParameter("@412_Narrative4", _txt_Narrative4.ToUpper());
        SqlParameter P_txt_Narrative5 = new SqlParameter("@412_Narrative5", _txt_Narrative5.ToUpper());
        SqlParameter P_txt_Narrative6 = new SqlParameter("@412_Narrative6", _txt_Narrative6.ToUpper());

        SqlParameter P_txt_Narrative499_1 = new SqlParameter("@_txt_Narrative499_1", _txt_Narrative499_1.ToUpper());
        SqlParameter P_txt_Narrative499_2 = new SqlParameter("@_txt_Narrative499_2", _txt_Narrative499_2.ToUpper());
        SqlParameter P_txt_Narrative499_3 = new SqlParameter("@_txt_Narrative499_3", _txt_Narrative499_3.ToUpper());
        SqlParameter P_txt_Narrative499_4 = new SqlParameter("@_txt_Narrative499_4", _txt_Narrative499_4.ToUpper());
        SqlParameter P_txt_Narrative499_5 = new SqlParameter("@_txt_Narrative499_5", _txt_Narrative499_5.ToUpper());
        SqlParameter P_txt_Narrative499_6 = new SqlParameter("@_txt_Narrative499_6", _txt_Narrative499_6.ToUpper());
        SqlParameter P_txt_Narrative499_7 = new SqlParameter("@_txt_Narrative499_7", _txt_Narrative499_7.ToUpper());
        SqlParameter P_txt_Narrative499_8 = new SqlParameter("@_txt_Narrative499_8", _txt_Narrative499_8.ToUpper());
        SqlParameter P_txt_Narrative499_9 = new SqlParameter("@_txt_Narrative499_9", _txt_Narrative499_9.ToUpper());
        SqlParameter P_txt_Narrative499_10 = new SqlParameter("@_txt_Narrative499_10", _txt_Narrative499_10.ToUpper());
        SqlParameter P_txt_Narrative499_11 = new SqlParameter("@_txt_Narrative499_11", _txt_Narrative499_11.ToUpper());
        SqlParameter P_txt_Narrative499_12 = new SqlParameter("@_txt_Narrative499_12", _txt_Narrative499_12.ToUpper());
        SqlParameter P_txt_Narrative499_13 = new SqlParameter("@_txt_Narrative499_13", _txt_Narrative499_13.ToUpper());
        SqlParameter P_txt_Narrative499_14 = new SqlParameter("@_txt_Narrative499_14", _txt_Narrative499_14.ToUpper());
        SqlParameter P_txt_Narrative499_15 = new SqlParameter("@_txt_Narrative499_15", _txt_Narrative499_15.ToUpper());
        SqlParameter P_txt_Narrative499_16 = new SqlParameter("@_txt_Narrative499_16", _txt_Narrative499_16.ToUpper());
        SqlParameter P_txt_Narrative499_17 = new SqlParameter("@_txt_Narrative499_17", _txt_Narrative499_17.ToUpper());
        SqlParameter P_txt_Narrative499_18 = new SqlParameter("@_txt_Narrative499_18", _txt_Narrative499_18.ToUpper());
        SqlParameter P_txt_Narrative499_19 = new SqlParameter("@_txt_Narrative499_19", _txt_Narrative499_19.ToUpper());
        SqlParameter P_txt_Narrative499_20 = new SqlParameter("@_txt_Narrative499_20", _txt_Narrative499_20.ToUpper());
        SqlParameter P_txt_Narrative499_21 = new SqlParameter("@_txt_Narrative499_21", _txt_Narrative499_21.ToUpper());
        SqlParameter P_txt_Narrative499_22 = new SqlParameter("@_txt_Narrative499_22", _txt_Narrative499_22.ToUpper());
        SqlParameter P_txt_Narrative499_23 = new SqlParameter("@_txt_Narrative499_23", _txt_Narrative499_23.ToUpper());
        SqlParameter P_txt_Narrative499_24 = new SqlParameter("@_txt_Narrative499_24", _txt_Narrative499_24.ToUpper());
        SqlParameter P_txt_Narrative499_25 = new SqlParameter("@_txt_Narrative499_25", _txt_Narrative499_25.ToUpper());
        SqlParameter P_txt_Narrative499_26 = new SqlParameter("@_txt_Narrative499_26", _txt_Narrative499_26.ToUpper());
        SqlParameter P_txt_Narrative499_27 = new SqlParameter("@_txt_Narrative499_27", _txt_Narrative499_27.ToUpper());
        SqlParameter P_txt_Narrative499_28 = new SqlParameter("@_txt_Narrative499_28", _txt_Narrative499_28.ToUpper());
        SqlParameter P_txt_Narrative499_29 = new SqlParameter("@_txt_Narrative499_29", _txt_Narrative499_29.ToUpper());
        SqlParameter P_txt_Narrative499_30 = new SqlParameter("@_txt_Narrative499_30", _txt_Narrative499_30.ToUpper());
        SqlParameter P_txt_Narrative499_31 = new SqlParameter("@_txt_Narrative499_31", _txt_Narrative499_31.ToUpper());
        SqlParameter P_txt_Narrative499_32 = new SqlParameter("@_txt_Narrative499_32", _txt_Narrative499_32.ToUpper());
        SqlParameter P_txt_Narrative499_33 = new SqlParameter("@_txt_Narrative499_33", _txt_Narrative499_33.ToUpper());
        SqlParameter P_txt_Narrative499_34 = new SqlParameter("@_txt_Narrative499_34", _txt_Narrative499_34.ToUpper());
        SqlParameter P_txt_Narrative499_35 = new SqlParameter("@_txt_Narrative499_35", _txt_Narrative499_35.ToUpper());

        SqlParameter P_AddedBy = new SqlParameter("@AddedBy", hdnUserName);
        SqlParameter P_AddedDate = new SqlParameter("@AddedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        SqlParameter P_UpdatedBy = new SqlParameter("@UpdatedBy", hdnUserName);
        SqlParameter P_UpdatedDate = new SqlParameter("@UpdatedDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

        string _Result = obj.SaveDeleteData("TF_IMP_AddUpdate_Acceptance_Collection", P_BranchName, P_txtDocNo, P_txtValueDate,P_txt_Maturity_Date,
            P_txtAttn, P_txt_Expected_InterestAmt, P_txt_Received_InterestAmt,
            //// General Operation SWIFT_SFMS
            P_GO_SWIFT_SFMS_Flag, P_GO_SWIFT_SFMS_Ref_No, P_GO_SWIFT_SFMS_Remark,
            P_GO_SWIFT_SFMS_Section, P_GO_SWIFT_SFMS_Comment, P_GO_SWIFT_SFMS_Memo,

            P_GO_SWIFT_SFMS_Debit, P_GO_SWIFT_SFMS_Debit_Amt, P_GO_SWIFT_SFMS_Debit_CCY,
            P_GO_SWIFT_SFMS_Debit_Cust, P_GO_SWIFT_SFMS_Debit_Cust_AcCode, P_GO_SWIFT_SFMS_Debit_Cust_AccNo,
            P_GO_SWIFT_SFMS_Debit_ExchCCY, P_GO_SWIFT_SFMS_Debit_ExchRate,
            P_GO_SWIFT_SFMS_Debit_Advice_Print, P_GO_SWIFT_SFMS_Debit_Details, P_GO_SWIFT_SFMS_Debit_Entity,

            P_GO_SWIFT_SFMS_Credit, P_GO_SWIFT_SFMS_Credit_Amt, P_GO_SWIFT_SFMS_Credit_CCY,
            P_GO_SWIFT_SFMS_Credit_Cust, P_GO_SWIFT_SFMS_Credit_Cust_AccNo, P_GO_SWIFT_SFMS_Credit_Cust_AcCode,
            P_GO_SWIFT_SFMS_Credit_ExchCCY, P_GO_SWIFT_SFMS_Credit_ExchRate,
            P_GO_SWIFT_SFMS_Credit_Advice_Print, P_GO_SWIFT_SFMS_Credit_Details, P_GO_SWIFT_SFMS_Credit_Entity,

            P_txt_GO_Swift_SFMS_Scheme_no,
            P_txt_GO_Swift_SFMS_Debit_FUND, P_txt_GO_Swift_SFMS_Debit_Check_No, P_txt_GO_Swift_SFMS_Debit_Available,
            P_txt_GO_Swift_SFMS_Debit_Division, P_txt_GO_Swift_SFMS_Debit_Inter_Amount, P_txt_GO_Swift_SFMS_Debit_Inter_Rate,
            P_txt_GO_Swift_SFMS_Credit_FUND, P_txt_GO_Swift_SFMS_Credit_Check_No, P_txt_GO_Swift_SFMS_Credit_Available,
            P_txt_GO_Swift_SFMS_Credit_Division, P_txt_GO_Swift_SFMS_Credit_Inter_Amount, P_txt_GO_Swift_SFMS_Credit_Inter_Rate,

            P_Swift_412, P_Swift_999C, P_Swift_499,
            P_txt_Narrative1, P_txt_Narrative2, P_txt_Narrative3, P_txt_Narrative4, P_txt_Narrative5, P_txt_Narrative6,
            P_txt_Narrative499_1, P_txt_Narrative499_2, P_txt_Narrative499_3, P_txt_Narrative499_4, P_txt_Narrative499_5,
            P_txt_Narrative499_6, P_txt_Narrative499_7, P_txt_Narrative499_8, P_txt_Narrative499_9, P_txt_Narrative499_10,
            P_txt_Narrative499_11, P_txt_Narrative499_12, P_txt_Narrative499_13, P_txt_Narrative499_14, P_txt_Narrative499_15,
            P_txt_Narrative499_16, P_txt_Narrative499_17, P_txt_Narrative499_18, P_txt_Narrative499_19, P_txt_Narrative499_20,
            P_txt_Narrative499_21, P_txt_Narrative499_22, P_txt_Narrative499_23, P_txt_Narrative499_24, P_txt_Narrative499_25,
            P_txt_Narrative499_26, P_txt_Narrative499_27, P_txt_Narrative499_28, P_txt_Narrative499_29, P_txt_Narrative499_30,
            P_txt_Narrative499_31, P_txt_Narrative499_32, P_txt_Narrative499_33, P_txt_Narrative499_34, P_txt_Narrative499_35,
            P_AddedBy, P_AddedDate, P_UpdatedBy, P_UpdatedDate
            );
        return _Result;
    }
}