using System.Collections.Generic;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

public partial class EXP_Exp_SwiftChecker : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("Expswift_CheckerView.aspx", true);
                }
                else
                {
                    fillBranch();
                    fillCurrency();
                    if (Request.QueryString["mode"].Trim() != "Add")
                    {
                        string no = Request.QueryString["Trans_RefNo"].Trim();
                        if (no != "")
                        {
                            fillDetails();
                            Readonly();
                        }
                        if (Request.QueryString["Status"].ToString() == "Approved By Checker")
                        {
                            ddlApproveReject.Enabled = false;
                        }
                        if (Request.QueryString["Status"].ToString() == "Reject By Checker")
                        {
                            ddlApproveReject.Enabled = false;
                        }
                    }
                }
            }
            ddlApproveReject.Attributes.Add("onchange", "return DialogAlert();");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        string UserName = Session["userName"].ToString();
        string AR = "";
        if (ddlApproveReject.SelectedIndex == 1)
        {
            AR = "A";
            if (ddlSwiftTypes.SelectedItem.Text == "MT 742")
            {
                CreateSwift742_XML();
                InsertDataForSwiftSTP("MT742", txt_742_ClaimBankRef.Text.Trim());
                audittrail("A", txt_742_ClaimBankRef.Text.Trim());
            }
            if (ddlSwiftTypes.SelectedItem.Text == "MT 754")
            {
                CreateSwift754_XML();
                InsertDataForSwiftSTP("MT754", txt_754_SenRef.Text.Trim());
                audittrail("A", txt_754_SenRef.Text.Trim());
            }
            if (ddlSwiftTypes.SelectedItem.Text == "MT 799")
            {
                CreateSwift799_XML();
                InsertDataForSwiftSTP("MT799", txt_799_transRefNo.Text.Trim());
                audittrail("A", txt_799_transRefNo.Text.Trim());
            }
            if (ddlSwiftTypes.SelectedItem.Text == "MT 499")
            {
                CreateSwift499_XML();
                InsertDataForSwiftSTP("MT499", txt_499_transRefNo.Text.Trim());
                audittrail("A", txt_499_transRefNo.Text.Trim());
            }
            if (ddlSwiftTypes.SelectedItem.Text == "MT 999")
            {
                CreateSwift999_XML();
                InsertDataForSwiftSTP("MT999", txt_999_transRefNo.Text.Trim());
                audittrail("A", txt_999_transRefNo.Text.Trim());
            }
            if (ddlSwiftTypes.SelectedItem.Text == "MT 299")
            {
                CreateSwift299_XML();
                InsertDataForSwiftSTP("MT299", txt_299_transRefNo.Text.Trim());
                audittrail("A", txt_299_transRefNo.Text.Trim());
            }
            if (ddlSwiftTypes.SelectedItem.Text == "MT 199")
            {
                CreateSwift199_XML();
                InsertDataForSwiftSTP("MT199", txt_199_transRefNo.Text.Trim());
                audittrail("A", txt_199_transRefNo.Text.Trim());
            }
            if (ddlSwiftTypes.SelectedItem.Text == "MT 420")
            {
                CreateSwift420_XML();
                InsertDataForSwiftSTP("MT420", txt_420_SendingBankTRN.Text.Trim());
                audittrail("A", txt_420_SendingBankTRN.Text.Trim());
            }

        }
        if (ddlApproveReject.SelectedIndex == 2)
        {
            AR = "R";
            if (ddlSwiftTypes.SelectedItem.Text == "MT 742")
            {
                audittrail("R", txt_742_ClaimBankRef.Text.Trim());
            }
            if (ddlSwiftTypes.SelectedItem.Text == "MT 754")
            {
                audittrail("R", txt_754_SenRef.Text.Trim());
            }
            if (ddlSwiftTypes.SelectedItem.Text == "MT 799")
            {
                audittrail("R", txt_799_transRefNo.Text.Trim());
            }
            if (ddlSwiftTypes.SelectedItem.Text == "MT 499")
            {
                audittrail("R", txt_499_transRefNo.Text.Trim());
            }
            if (ddlSwiftTypes.SelectedItem.Text == "MT 199")
            {
                audittrail("R", txt_199_transRefNo.Text.Trim());
            }
            if (ddlSwiftTypes.SelectedItem.Text == "MT 299")
            {
                audittrail("R", txt_299_transRefNo.Text.Trim());
            }
            if (ddlSwiftTypes.SelectedItem.Text == "MT 999")
            {
                audittrail("R", txt_999_transRefNo.Text.Trim());
            }
            if (ddlSwiftTypes.SelectedItem.Text == "MT 420")
            {
                audittrail("R", txt_420_SendingBankTRN.Text.Trim());
            }
        }
        //if (AR == "A")
        //{
        //    SqlParameter DocNo = new SqlParameter("@Trans_RefNo", Request.QueryString["Trans_RefNo"].ToString().Trim());
        //    SqlParameter P_Status = new SqlParameter("@Status", AR);
        //    SqlParameter P_RejectReason = new SqlParameter("@RejectReason", hdnRejectReason.Value.Trim());
        //    SqlParameter p3 = new SqlParameter("@ADate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        //    SqlParameter PUserName = new SqlParameter("@CheckerID", UserName);
        //    string Result = obj.SaveDeleteData("TF_EXP_ChekerApproveSwift", DocNo, P_Status, P_RejectReason, p3, PUserName);

        //    Response.Redirect("Expswift_CheckerView.aspx?DocNo=" + Result.Substring(7) + "&AR=" + AR, true);
        //}
        //else
        //{
        //    SqlParameter DocNo = new SqlParameter("@Trans_RefNo", Request.QueryString["Trans_RefNo"].ToString().Trim());
        //    SqlParameter P_Status = new SqlParameter("@Status", AR);
        //    SqlParameter P_RejectReason = new SqlParameter("@RejectReason", hdnRejectReason.Value.Trim());
        //    SqlParameter Approvedate = new SqlParameter("@Approvedate", Request.QueryString["ApproveDate"].ToString());
        //    SqlParameter PUserName = new SqlParameter("@CheckerID", UserName);
        //    string Result = obj.SaveDeleteData("TF_EXP_ChekerRejectSwift", DocNo, P_Status, P_RejectReason, Approvedate, PUserName);

        //    Response.Redirect("Expswift_CheckerView.aspx?DocNo=" + Result.Substring(7) + "&AR=" + AR, true);
        // Old code
        //}
        //Changed on 18102022 for swift type by bhupendra
        if (AR == "A")
        {
            SqlParameter DocNo = new SqlParameter("@Trans_RefNo", Request.QueryString["Trans_RefNo"].ToString().Trim());
            SqlParameter P_Swift = new SqlParameter("@Swift_Type", ddlSwiftTypes.SelectedItem.Text);
            SqlParameter P_Status = new SqlParameter("@Status", AR);
            SqlParameter P_RejectReason = new SqlParameter("@RejectReason", hdnRejectReason.Value.Trim());
            SqlParameter p3 = new SqlParameter("@ADate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            SqlParameter PUserName = new SqlParameter("@CheckerID", UserName);
            string Result = obj.SaveDeleteData("TF_EXP_ChekerApproveSwift", DocNo,P_Swift, P_Status, P_RejectReason, p3, PUserName);

            Response.Redirect("Expswift_CheckerView.aspx?DocNo=" + Result.Substring(7) + "&AR=" + AR, true);
        }
        else
        {
            SqlParameter DocNo = new SqlParameter("@Trans_RefNo", Request.QueryString["Trans_RefNo"].ToString().Trim());
            SqlParameter P_Swift = new SqlParameter("@Swift_Type", ddlSwiftTypes.SelectedItem.Text);
            SqlParameter P_Status = new SqlParameter("@Status", AR);
            SqlParameter P_RejectReason = new SqlParameter("@RejectReason", hdnRejectReason.Value.Trim());
            SqlParameter Approvedate = new SqlParameter("@Approvedate", Request.QueryString["ApproveDate"].ToString());
            SqlParameter PUserName = new SqlParameter("@CheckerID", UserName);
            string Result = obj.SaveDeleteData("TF_EXP_ChekerRejectSwift", DocNo,P_Swift, P_Status, P_RejectReason, Approvedate, PUserName);

            Response.Redirect("Expswift_CheckerView.aspx?DocNo=" + Result.Substring(7) + "&AR=" + AR, true);

        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Expswift_CheckerView.aspx", true);
    }

    public void fillDetails()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDocNo = new SqlParameter("@Trans_RefNo", Request.QueryString["Trans_RefNo"].ToString());
        SqlParameter PSwifttype = new SqlParameter("@Swift_Type", Request.QueryString["Swift_Type"].ToString());

        DataTable dt = new DataTable();
        dt = obj.getData("TF_EXP_SwiftViewDetails", PDocNo, PSwifttype);

        if (dt.Rows.Count > 0)
        {
            TabContainerMain.Visible = true;
            TabSwifts.Visible = true;
            ddlSwiftTypes.SelectedItem.Text = dt.Rows[0]["Swift_Type"].ToString();
            ddlBranch.SelectedItem.Text = dt.Rows[0]["Branch"].ToString();

            //=========================742 ===================//
            if (dt.Rows[0]["Swift_Type"].ToString() == "MT 742")
            {
                Panel_742.Visible = true;
                txtReceiver742.Text = dt.Rows[0]["Receiver"].ToString();
                txt_742_ClaimBankRef.Text = dt.Rows[0]["Trans_RefNo"].ToString();
                txt_742_DocumCreditNo.Text = dt.Rows[0]["Related_Ref"].ToString();
                txt_742_Dateofissue.Text = dt.Rows[0]["Dateofissue"].ToString();

                ddl_Issuingbank_742.SelectedValue = dt.Rows[0]["ddlIssuingBank"].ToString();
                txtIssuingBankAccountnumber_742.Text = dt.Rows[0]["IssuingBank_PartyIdent"].ToString();
                txtIssuingBankAccountnumber1_742.Text = dt.Rows[0]["IssuingBank_PartyIdent1"].ToString();
                txtIssuingBankIdentifiercode_742.Text = dt.Rows[0]["IssuingBank_Identcode"].ToString();
                txtIssuingBankName_742.Text = dt.Rows[0]["IssuingBank_Name"].ToString();
                txtIssuingBankAddress1_742.Text = dt.Rows[0]["IssuingBank_Add1"].ToString();
                txtIssuingBankAddress2_742.Text = dt.Rows[0]["IssuingBank_Add2"].ToString();
                txtIssuingBankAddress3_742.Text = dt.Rows[0]["IssuingBank_Add3"].ToString();
                ddl_Issuingbank_742_TextChanged(null, null);

                ddl_742_PrinAmtClmd_Ccy.ClearSelection();
                ddl_742_PrinAmtClmd_Ccy.SelectedValue = dt.Rows[0]["PrincipalAmtClaimed_Curr"].ToString();
                txt_742_PrinAmtClmd_Amt.Text = dt.Rows[0]["PrincipalAmtClaimed_Amt"].ToString();

                ddl_742_AddAmtClamd_Ccy.ClearSelection();
                ddl_742_AddAmtClamd_Ccy.SelectedValue = dt.Rows[0]["AddnlAmt_Curr"].ToString();

                txt_742_AddAmtClamd_Amt.Text = dt.Rows[0]["AddnlAmt_Amt"].ToString();

                txt_742_Charges1.Text = dt.Rows[0]["Charges1"].ToString();
                txt_742_Charges2.Text = dt.Rows[0]["Charges2"].ToString();
                txt_742_Charges3.Text = dt.Rows[0]["Charges3"].ToString();
                txt_742_Charges4.Text = dt.Rows[0]["Charges4"].ToString();
                txt_742_Charges5.Text = dt.Rows[0]["Charges5"].ToString();
                txt_742_Charges6.Text = dt.Rows[0]["Charges6"].ToString();

                ddlTotalAmtclamd_742.ClearSelection();
                ddlTotalAmtclamd_742.SelectedValue = dt.Rows[0]["ddlTotalAmtClaimed"].ToString();

                txt_742_TotalAmtClmd_Date.Text = dt.Rows[0]["TotalAmtClaimed_Date"].ToString();

                ddl_742_TotalAmtClmd_Ccy.ClearSelection();
                ddl_742_TotalAmtClmd_Ccy.Text = dt.Rows[0]["TotalAmtClaimed_Curr"].ToString();
                txt_742_TotalAmtClmd_Amt.Text = dt.Rows[0]["TotalAmtClaimed_Amt"].ToString();
                ddlTotalAmtclamd_742_TextChanged(null, null);

                ddlAccountwithbank_742.ClearSelection();
                ddlAccountwithbank_742.SelectedValue = dt.Rows[0]["ddlAccWithBank"].ToString();
                txtAccountwithBankAccountnumber_742.Text = dt.Rows[0]["AccWithBank_PartyIdent"].ToString();
                txtAccountwithBankAccountnumber1_742.Text = dt.Rows[0]["AccWithBank_PartyIdent1"].ToString();
                txtAccountwithBankIdentifiercode_742.Text = dt.Rows[0]["AccWithBank_Identcode"].ToString();
                txtAccountwithBankLocation_742.Text = dt.Rows[0]["AccWithBank_Location"].ToString();
                txtAccountwithBankName_742.Text = dt.Rows[0]["AccWithBank_Name"].ToString();
                txtAccountwithBankAddress1_742.Text = dt.Rows[0]["AccWithBank_Add1"].ToString();
                txtAccountwithBankAddress2_742.Text = dt.Rows[0]["AccWithBank_Add2"].ToString();
                txtAccountwithBankAddress3_742.Text = dt.Rows[0]["AccWithBank_Add3"].ToString();
                ddlAccountwithbank_742_TextChanged(null, null);

                ddlBeneficiarybank_742.ClearSelection();
                ddlBeneficiarybank_742.SelectedValue = dt.Rows[0]["ddlBeneficiaryBank"].ToString();
                txtBeneficiaryBankAccountnumber_742.Text = dt.Rows[0]["BeneficiaryBank_PartyIdent"].ToString();
                txtBeneficiaryBankAccountnumber1_742.Text = dt.Rows[0]["BeneficiaryBank_PartyIdent1"].ToString();
                txtBeneficiaryBankIdentifiercode_742.Text = dt.Rows[0]["BeneficiaryBank_Identcode"].ToString();
                txtBeneficiaryBankName_742.Text = dt.Rows[0]["BeneficiaryBank_Name"].ToString();
                txtBeneficiaryBankAddress1_742.Text = dt.Rows[0]["BeneficiaryBank_Add1"].ToString();
                txtBeneficiaryBankAddress2_742.Text = dt.Rows[0]["BeneficiaryBank_Add2"].ToString();
                txtBeneficiaryBankAddress3_742.Text = dt.Rows[0]["BeneficiaryBank_Add3"].ToString();
                ddlBeneficiarybank_742_TextChanged(null, null);

                txt_742_SenRecInfo1.Text = dt.Rows[0]["SenToRecinfo1"].ToString();
                txt_742_SenRecInfo2.Text = dt.Rows[0]["SenToRecinfo2"].ToString();
                txt_742_SenRecInfo3.Text = dt.Rows[0]["SenToRecinfo3"].ToString();
                txt_742_SenRecInfo4.Text = dt.Rows[0]["SenToRecinfo4"].ToString();
                txt_742_SenRecInfo5.Text = dt.Rows[0]["SenToRecinfo5"].ToString();
                txt_742_SenRecInfo6.Text = dt.Rows[0]["SenToRecinfo6"].ToString();
            }
            ////=========================742 end===================//

            ////=========================754 ===================//
            if (ddlSwiftTypes.SelectedItem.Text == "MT 754")
            {
                Panel_754.Visible = true;
                txtReceiver754.Text = dt.Rows[0]["Receiver"].ToString();
                txt_754_SenRef.Text = dt.Rows[0]["Trans_RefNo"].ToString();
                txt_754_RelRef.Text = dt.Rows[0]["Related_Ref"].ToString();

                ddlPrinAmtPaidAccNego_754.ClearSelection();
                ddlPrinAmtPaidAccNego_754.SelectedValue = dt.Rows[0]["ddlPrincipalAmtPaidAccptdNego"].ToString();
                txtPrinAmtPaidAccNegoDate_754.Text = dt.Rows[0]["PrincipalAmtPaidAccptdNego_Date"].ToString();

                ddl_PrinAmtPaidAccNegoCurr_754.ClearSelection();
                ddl_PrinAmtPaidAccNegoCurr_754.Text = dt.Rows[0]["PrincipalAmtPaidAccptdNego_Curr"].ToString();
                txtPrinAmtPaidAccNegoAmt_754.Text = dt.Rows[0]["PrincipalAmtPaidAccptdNego_Amt"].ToString();
                ddlPrinAmtPaidAccNego_754_TextChanged(null, null);

                ddl_754_AddAmtClamd_Ccy.ClearSelection();
                ddl_754_AddAmtClamd_Ccy.Text = dt.Rows[0]["AddnlAmt_Curr"].ToString();
                txt_754_AddAmtClamd_Amt.Text = dt.Rows[0]["AddnlAmt_Amt"].ToString();

                txt_754_ChargesDeduct1.Text = dt.Rows[0]["Charges1"].ToString();
                txt_754_ChargesDeduct2.Text = dt.Rows[0]["Charges2"].ToString();
                txt_754_ChargesDeduct3.Text = dt.Rows[0]["Charges3"].ToString();
                txt_754_ChargesDeduct4.Text = dt.Rows[0]["Charges4"].ToString();
                txt_754_ChargesDeduct5.Text = dt.Rows[0]["Charges5"].ToString();
                txt_754_ChargesDeduct6.Text = dt.Rows[0]["Charges6"].ToString();

                txt_754_ChargesAdded1.Text = dt.Rows[0]["ChargesAdded1"].ToString();
                txt_754_ChargesAdded2.Text = dt.Rows[0]["ChargesAdded2"].ToString();
                txt_754_ChargesAdded3.Text = dt.Rows[0]["ChargesAdded3"].ToString();
                txt_754_ChargesAdded4.Text = dt.Rows[0]["ChargesAdded4"].ToString();
                txt_754_ChargesAdded5.Text = dt.Rows[0]["ChargesAdded5"].ToString();
                txt_754_ChargesAdded6.Text = dt.Rows[0]["ChargesAdded6"].ToString();

                ddlTotalAmtclamd_754.ClearSelection();
                ddlTotalAmtclamd_754.SelectedValue = dt.Rows[0]["ddlTotalAmtClaimed"].ToString(); ;
                txt_754_TotalAmtClmd_Date.Text = dt.Rows[0]["TotalAmtClaimed_Date"].ToString();

                ddl_754_TotalAmtClmd_Ccy.ClearSelection();
                ddl_754_TotalAmtClmd_Ccy.Text = dt.Rows[0]["TotalAmtClaimed_Curr"].ToString();
                txt_754_TotalAmtClmd_Amt.Text = dt.Rows[0]["TotalAmtClaimed_Amt"].ToString();
                ddlTotalAmtclamd_754_TextChanged(null, null);

                ddlReimbursingbank_754.ClearSelection();
                ddlReimbursingbank_754.SelectedValue = dt.Rows[0]["ddlReimbursingBank"].ToString();
                txtReimbursingBankAccountnumber_754.Text = dt.Rows[0]["ReimbursingBank_PartyIdent"].ToString();
                txtReimbursingBankAccountnumber1_754.Text = dt.Rows[0]["ReimbursingBank_PartyIdent1"].ToString();
                txtReimbursingBankIdentifiercode_754.Text = dt.Rows[0]["ReimbursingBank_Identcode"].ToString();
                txtReimbursingBankLocation_754.Text = dt.Rows[0]["ReimbursingBank_Location"].ToString();
                txtReimbursingBankName_754.Text = dt.Rows[0]["ReimbursingBank_Name"].ToString();
                txtReimbursingBankAddress1_754.Text = dt.Rows[0]["ReimbursingBank_Add1"].ToString();
                txtReimbursingBankAddress2_754.Text = dt.Rows[0]["ReimbursingBank_Add2"].ToString();
                txtReimbursingBankAddress3_754.Text = dt.Rows[0]["ReimbursingBank_Add3"].ToString();
                ddlReimbursingbank_754_TextChanged(null, null);

                ddlAccountwithbank_754.ClearSelection();
                ddlAccountwithbank_754.SelectedValue = dt.Rows[0]["ddlAccWithBank"].ToString();
                txtAccountwithBankAccountnumber_754.Text = dt.Rows[0]["AccWithBank_PartyIdent"].ToString();
                txtAccountwithBankAccountnumber1_754.Text = dt.Rows[0]["AccWithBank_PartyIdent1"].ToString();
                txtAccountwithBankIdentifiercode_754.Text = dt.Rows[0]["AccWithBank_Identcode"].ToString();
                txtAccountwithBankLocation_754.Text = dt.Rows[0]["AccWithBank_Location"].ToString();
                txtAccountwithBankName_754.Text = dt.Rows[0]["AccWithBank_Name"].ToString();
                txtAccountwithBankAddress1_754.Text = dt.Rows[0]["AccWithBank_Add1"].ToString();
                txtAccountwithBankAddress2_754.Text = dt.Rows[0]["AccWithBank_Add2"].ToString();
                txtAccountwithBankAddress3_754.Text = dt.Rows[0]["AccWithBank_Add3"].ToString();
                ddlAccountwithbank_754_TextChanged(null, null);

                ddlBeneficiarybank_754.ClearSelection();
                ddlBeneficiarybank_754.SelectedValue = dt.Rows[0]["ddlBeneficiaryBank"].ToString();
                txtBeneficiaryBankAccountnumber_754.Text = dt.Rows[0]["BeneficiaryBank_PartyIdent"].ToString();
                txtBeneficiaryBankAccountnumber1_754.Text = dt.Rows[0]["BeneficiaryBank_PartyIdent1"].ToString();
                txtBeneficiaryBankIdentifiercode_754.Text = dt.Rows[0]["BeneficiaryBank_Identcode"].ToString();
                txtBeneficiaryBankName_754.Text = dt.Rows[0]["BeneficiaryBank_Name"].ToString();
                txtBeneficiaryBankAddress1_754.Text = dt.Rows[0]["BeneficiaryBank_Add1"].ToString();
                txtBeneficiaryBankAddress2_754.Text = dt.Rows[0]["BeneficiaryBank_Add2"].ToString();
                txtBeneficiaryBankAddress3_754.Text = dt.Rows[0]["BeneficiaryBank_Add3"].ToString();
                ddlBeneficiarybank_754_TextChanged(null, null);

                txt_754_SenRecInfo1.Text = dt.Rows[0]["SenToRecinfo1"].ToString();
                txt_754_SenRecInfo2.Text = dt.Rows[0]["SenToRecinfo2"].ToString();
                txt_754_SenRecInfo3.Text = dt.Rows[0]["SenToRecinfo3"].ToString();
                txt_754_SenRecInfo4.Text = dt.Rows[0]["SenToRecinfo4"].ToString();
                txt_754_SenRecInfo5.Text = dt.Rows[0]["SenToRecinfo5"].ToString();
                txt_754_SenRecInfo6.Text = dt.Rows[0]["SenToRecinfo6"].ToString();

                txt_754_Narr1.Text = dt.Rows[0]["Narrative1"].ToString();
                txt_754_Narr2.Text = dt.Rows[0]["Narrative2"].ToString();
                txt_754_Narr3.Text = dt.Rows[0]["Narrative3"].ToString();
                txt_754_Narr4.Text = dt.Rows[0]["Narrative4"].ToString();
                txt_754_Narr5.Text = dt.Rows[0]["Narrative5"].ToString();
                txt_754_Narr6.Text = dt.Rows[0]["Narrative6"].ToString();
                txt_754_Narr7.Text = dt.Rows[0]["Narrative7"].ToString();
                txt_754_Narr8.Text = dt.Rows[0]["Narrative8"].ToString();
                txt_754_Narr9.Text = dt.Rows[0]["Narrative9"].ToString();
                txt_754_Narr10.Text = dt.Rows[0]["Narrative10"].ToString();
                txt_754_Narr11.Text = dt.Rows[0]["Narrative11"].ToString();
                txt_754_Narr12.Text = dt.Rows[0]["Narrative12"].ToString();
                txt_754_Narr13.Text = dt.Rows[0]["Narrative13"].ToString();
                txt_754_Narr14.Text = dt.Rows[0]["Narrative14"].ToString();
                txt_754_Narr15.Text = dt.Rows[0]["Narrative15"].ToString();
                txt_754_Narr16.Text = dt.Rows[0]["Narrative16"].ToString();
                txt_754_Narr17.Text = dt.Rows[0]["Narrative17"].ToString();
                txt_754_Narr18.Text = dt.Rows[0]["Narrative18"].ToString();
                txt_754_Narr19.Text = dt.Rows[0]["Narrative19"].ToString();
                txt_754_Narr20.Text = dt.Rows[0]["Narrative20"].ToString();

            }
            ////=========================754 end===================//

            if (ddlSwiftTypes.SelectedItem.Text == "MT 420")
            {
                Panel_420.Visible = true;
                txtReceiver420.Text = dt.Rows[0]["Receiver"].ToString();
                txt_420_SendingBankTRN.Text = dt.Rows[0]["Trans_RefNo"].ToString();
                txt_420_RelRef.Text = dt.Rows[0]["Related_Ref"].ToString();

                ddlAmountTraced_420.ClearSelection();
                ddlAmountTraced_420.SelectedValue = dt.Rows[0]["ddlAmtTraced"].ToString();
                txtAmountTracedAmount_420.Text = dt.Rows[0]["TracedAmt"].ToString();
                ddlAmountTracedCode_420.SelectedValue = dt.Rows[0]["Tracedcode"].ToString();
                ddl_AmountTracedCurrency_420.ClearSelection();
                ddl_AmountTracedCurrency_420.SelectedValue = dt.Rows[0]["Tracedcurr"].ToString();
                txtAmountTracedDate_420.Text = dt.Rows[0]["TracedDate"].ToString();
                ddlAmountTracedDayMonth_420.SelectedValue = dt.Rows[0]["TracedDayMonth"].ToString();
                txtAmountTracedNoofDaysMonth_420.Text = dt.Rows[0]["TracedNoofDayMonth"].ToString();
                ddlAmountTraced_420_TextChanged(null, null);

                txt_420_DateofCollnInstruction.Text = dt.Rows[0]["DateofCollnInstruction"].ToString();

                txt_420_DraweeAccount.Text = dt.Rows[0]["DraweeAccount"].ToString();
                txt_420_DraweeName.Text = dt.Rows[0]["DraweeName"].ToString();
                txt_420_DraweeAdd1.Text = dt.Rows[0]["DraweeAdd1"].ToString();
                txt_420_DraweeAdd2.Text = dt.Rows[0]["DraweeAdd2"].ToString();
                txt_420_DraweeAdd3.Text = dt.Rows[0]["DraweeAdd3"].ToString();

                txt_420_SenToRecinfo1.Text = dt.Rows[0]["SenToRecinfo1"].ToString();
                txt_420_SenToRecinfo2.Text = dt.Rows[0]["SenToRecinfo2"].ToString();
                txt_420_SenToRecinfo3.Text = dt.Rows[0]["SenToRecinfo3"].ToString();
                txt_420_SenToRecinfo4.Text = dt.Rows[0]["SenToRecinfo4"].ToString();
                txt_420_SenToRecinfo5.Text = dt.Rows[0]["SenToRecinfo5"].ToString();
                txt_420_SenToRecinfo6.Text = dt.Rows[0]["SenToRecinfo6"].ToString();
            }
            //================================420 end===========================================//

            if (ddlSwiftTypes.SelectedItem.Text == "MT 499")
            {
                Panel_499.Visible = true;
                txtReceiver499.Text = dt.Rows[0]["Receiver"].ToString();
                txt_499_transRefNo.Text = dt.Rows[0]["Trans_RefNo"].ToString();
                txt_499_RelRef.Text = dt.Rows[0]["Related_Ref"].ToString();
                txt_499_Narr1.Text = dt.Rows[0]["Narrative1"].ToString();
                txt_499_Narr2.Text = dt.Rows[0]["Narrative2"].ToString();
                txt_499_Narr3.Text = dt.Rows[0]["Narrative3"].ToString();
                txt_499_Narr4.Text = dt.Rows[0]["Narrative4"].ToString();
                txt_499_Narr5.Text = dt.Rows[0]["Narrative5"].ToString();
                txt_499_Narr6.Text = dt.Rows[0]["Narrative6"].ToString();
                txt_499_Narr7.Text = dt.Rows[0]["Narrative7"].ToString();
                txt_499_Narr8.Text = dt.Rows[0]["Narrative8"].ToString();
                txt_499_Narr9.Text = dt.Rows[0]["Narrative9"].ToString();
                txt_499_Narr10.Text = dt.Rows[0]["Narrative10"].ToString();
                txt_499_Narr11.Text = dt.Rows[0]["Narrative11"].ToString();
                txt_499_Narr12.Text = dt.Rows[0]["Narrative12"].ToString();
                txt_499_Narr13.Text = dt.Rows[0]["Narrative13"].ToString();
                txt_499_Narr14.Text = dt.Rows[0]["Narrative14"].ToString();
                txt_499_Narr15.Text = dt.Rows[0]["Narrative15"].ToString();
                txt_499_Narr16.Text = dt.Rows[0]["Narrative16"].ToString();
                txt_499_Narr17.Text = dt.Rows[0]["Narrative17"].ToString();
                txt_499_Narr18.Text = dt.Rows[0]["Narrative18"].ToString();
                txt_499_Narr19.Text = dt.Rows[0]["Narrative19"].ToString();
                txt_499_Narr20.Text = dt.Rows[0]["Narrative20"].ToString();
                txt_499_Narr21.Text = dt.Rows[0]["Narrative21"].ToString();
                txt_499_Narr22.Text = dt.Rows[0]["Narrative22"].ToString();
                txt_499_Narr23.Text = dt.Rows[0]["Narrative23"].ToString();
                txt_499_Narr24.Text = dt.Rows[0]["Narrative24"].ToString();
                txt_499_Narr25.Text = dt.Rows[0]["Narrative25"].ToString();
                txt_499_Narr26.Text = dt.Rows[0]["Narrative26"].ToString();
                txt_499_Narr27.Text = dt.Rows[0]["Narrative27"].ToString();
                txt_499_Narr28.Text = dt.Rows[0]["Narrative28"].ToString();
                txt_499_Narr29.Text = dt.Rows[0]["Narrative29"].ToString();
                txt_499_Narr30.Text = dt.Rows[0]["Narrative30"].ToString();
                txt_499_Narr31.Text = dt.Rows[0]["Narrative31"].ToString();
                txt_499_Narr32.Text = dt.Rows[0]["Narrative32"].ToString();
                txt_499_Narr33.Text = dt.Rows[0]["Narrative33"].ToString();
                txt_499_Narr34.Text = dt.Rows[0]["Narrative34"].ToString();
                txt_499_Narr35.Text = dt.Rows[0]["Narrative35"].ToString();
            }
            //============================499 end==========================//


            ////================799================//
            if (ddlSwiftTypes.SelectedItem.Text == "MT 799")
            {
                Panel_799.Visible = true;
                txtReceiver799.Text = dt.Rows[0]["Receiver"].ToString();
                txt_799_transRefNo.Text = dt.Rows[0]["Trans_RefNo"].ToString();
                txt_799_RelRef.Text = dt.Rows[0]["Related_Ref"].ToString();
                txt_799_Narr1.Text = dt.Rows[0]["Narrative1"].ToString();
                txt_799_Narr2.Text = dt.Rows[0]["Narrative2"].ToString();
                txt_799_Narr3.Text = dt.Rows[0]["Narrative3"].ToString();
                txt_799_Narr4.Text = dt.Rows[0]["Narrative4"].ToString();
                txt_799_Narr5.Text = dt.Rows[0]["Narrative5"].ToString();
                txt_799_Narr6.Text = dt.Rows[0]["Narrative6"].ToString();
                txt_799_Narr7.Text = dt.Rows[0]["Narrative7"].ToString();
                txt_799_Narr8.Text = dt.Rows[0]["Narrative8"].ToString();
                txt_799_Narr9.Text = dt.Rows[0]["Narrative9"].ToString();
                txt_799_Narr10.Text = dt.Rows[0]["Narrative10"].ToString();
                txt_799_Narr11.Text = dt.Rows[0]["Narrative11"].ToString();
                txt_799_Narr12.Text = dt.Rows[0]["Narrative12"].ToString();
                txt_799_Narr13.Text = dt.Rows[0]["Narrative13"].ToString();
                txt_799_Narr14.Text = dt.Rows[0]["Narrative14"].ToString();
                txt_799_Narr15.Text = dt.Rows[0]["Narrative15"].ToString();
                txt_799_Narr16.Text = dt.Rows[0]["Narrative16"].ToString();
                txt_799_Narr17.Text = dt.Rows[0]["Narrative17"].ToString();
                txt_799_Narr18.Text = dt.Rows[0]["Narrative18"].ToString();
                txt_799_Narr19.Text = dt.Rows[0]["Narrative19"].ToString();
                txt_799_Narr20.Text = dt.Rows[0]["Narrative20"].ToString();
                txt_799_Narr21.Text = dt.Rows[0]["Narrative21"].ToString();
                txt_799_Narr22.Text = dt.Rows[0]["Narrative22"].ToString();
                txt_799_Narr23.Text = dt.Rows[0]["Narrative23"].ToString();
                txt_799_Narr24.Text = dt.Rows[0]["Narrative24"].ToString();
                txt_799_Narr25.Text = dt.Rows[0]["Narrative25"].ToString();
                txt_799_Narr26.Text = dt.Rows[0]["Narrative26"].ToString();
                txt_799_Narr27.Text = dt.Rows[0]["Narrative27"].ToString();
                txt_799_Narr28.Text = dt.Rows[0]["Narrative28"].ToString();
                txt_799_Narr29.Text = dt.Rows[0]["Narrative29"].ToString();
                txt_799_Narr30.Text = dt.Rows[0]["Narrative30"].ToString();
                txt_799_Narr31.Text = dt.Rows[0]["Narrative31"].ToString();
                txt_799_Narr32.Text = dt.Rows[0]["Narrative32"].ToString();
                txt_799_Narr33.Text = dt.Rows[0]["Narrative33"].ToString();
                txt_799_Narr34.Text = dt.Rows[0]["Narrative34"].ToString();
                txt_799_Narr35.Text = dt.Rows[0]["Narrative35"].ToString();

                //==================799 end===============//
            }
            if (ddlSwiftTypes.SelectedItem.Text == "MT 199")
            {
                Panel_199.Visible = true;
                txtReceiver199.Text = dt.Rows[0]["Receiver"].ToString();
                txt_199_transRefNo.Text = dt.Rows[0]["Trans_RefNo"].ToString();
                txt_199_RelRef.Text = dt.Rows[0]["Related_Ref"].ToString();
                txt_199_Narr1.Text = dt.Rows[0]["Narrative1"].ToString();
                txt_199_Narr2.Text = dt.Rows[0]["Narrative2"].ToString();
                txt_199_Narr3.Text = dt.Rows[0]["Narrative3"].ToString();
                txt_199_Narr4.Text = dt.Rows[0]["Narrative4"].ToString();
                txt_199_Narr5.Text = dt.Rows[0]["Narrative5"].ToString();
                txt_199_Narr6.Text = dt.Rows[0]["Narrative6"].ToString();
                txt_199_Narr7.Text = dt.Rows[0]["Narrative7"].ToString();
                txt_199_Narr8.Text = dt.Rows[0]["Narrative8"].ToString();
                txt_199_Narr9.Text = dt.Rows[0]["Narrative9"].ToString();
                txt_199_Narr10.Text = dt.Rows[0]["Narrative10"].ToString();
                txt_199_Narr11.Text = dt.Rows[0]["Narrative11"].ToString();
                txt_199_Narr12.Text = dt.Rows[0]["Narrative12"].ToString();
                txt_199_Narr13.Text = dt.Rows[0]["Narrative13"].ToString();
                txt_199_Narr14.Text = dt.Rows[0]["Narrative14"].ToString();
                txt_199_Narr15.Text = dt.Rows[0]["Narrative15"].ToString();
                txt_199_Narr16.Text = dt.Rows[0]["Narrative16"].ToString();
                txt_199_Narr17.Text = dt.Rows[0]["Narrative17"].ToString();
                txt_199_Narr18.Text = dt.Rows[0]["Narrative18"].ToString();
                txt_199_Narr19.Text = dt.Rows[0]["Narrative19"].ToString();
                txt_199_Narr20.Text = dt.Rows[0]["Narrative20"].ToString();
                txt_199_Narr21.Text = dt.Rows[0]["Narrative21"].ToString();
                txt_199_Narr22.Text = dt.Rows[0]["Narrative22"].ToString();
                txt_199_Narr23.Text = dt.Rows[0]["Narrative23"].ToString();
                txt_199_Narr24.Text = dt.Rows[0]["Narrative24"].ToString();
                txt_199_Narr25.Text = dt.Rows[0]["Narrative25"].ToString();
                txt_199_Narr26.Text = dt.Rows[0]["Narrative26"].ToString();
                txt_199_Narr27.Text = dt.Rows[0]["Narrative27"].ToString();
                txt_199_Narr28.Text = dt.Rows[0]["Narrative28"].ToString();
                txt_199_Narr29.Text = dt.Rows[0]["Narrative29"].ToString();
                txt_199_Narr30.Text = dt.Rows[0]["Narrative30"].ToString();
                txt_199_Narr31.Text = dt.Rows[0]["Narrative31"].ToString();
                txt_199_Narr32.Text = dt.Rows[0]["Narrative32"].ToString();
                txt_199_Narr33.Text = dt.Rows[0]["Narrative33"].ToString();
                txt_199_Narr34.Text = dt.Rows[0]["Narrative34"].ToString();
                txt_199_Narr35.Text = dt.Rows[0]["Narrative35"].ToString();
            }
            //============================199 end==========================//

            if (ddlSwiftTypes.SelectedItem.Text == "MT 299")
            {
                Panel_299.Visible = true;
                txtReceiver299.Text = dt.Rows[0]["Receiver"].ToString();
                txt_299_transRefNo.Text = dt.Rows[0]["Trans_RefNo"].ToString();
                txt_299_RelRef.Text = dt.Rows[0]["Related_Ref"].ToString();
                txt_299_Narr1.Text = dt.Rows[0]["Narrative1"].ToString();
                txt_299_Narr2.Text = dt.Rows[0]["Narrative2"].ToString();
                txt_299_Narr3.Text = dt.Rows[0]["Narrative3"].ToString();
                txt_299_Narr4.Text = dt.Rows[0]["Narrative4"].ToString();
                txt_299_Narr5.Text = dt.Rows[0]["Narrative5"].ToString();
                txt_299_Narr6.Text = dt.Rows[0]["Narrative6"].ToString();
                txt_299_Narr7.Text = dt.Rows[0]["Narrative7"].ToString();
                txt_299_Narr8.Text = dt.Rows[0]["Narrative8"].ToString();
                txt_299_Narr9.Text = dt.Rows[0]["Narrative9"].ToString();
                txt_299_Narr10.Text = dt.Rows[0]["Narrative10"].ToString();
                txt_299_Narr11.Text = dt.Rows[0]["Narrative11"].ToString();
                txt_299_Narr12.Text = dt.Rows[0]["Narrative12"].ToString();
                txt_299_Narr13.Text = dt.Rows[0]["Narrative13"].ToString();
                txt_299_Narr14.Text = dt.Rows[0]["Narrative14"].ToString();
                txt_299_Narr15.Text = dt.Rows[0]["Narrative15"].ToString();
                txt_299_Narr16.Text = dt.Rows[0]["Narrative16"].ToString();
                txt_299_Narr17.Text = dt.Rows[0]["Narrative17"].ToString();
                txt_299_Narr18.Text = dt.Rows[0]["Narrative18"].ToString();
                txt_299_Narr19.Text = dt.Rows[0]["Narrative19"].ToString();
                txt_299_Narr20.Text = dt.Rows[0]["Narrative20"].ToString();
                txt_299_Narr21.Text = dt.Rows[0]["Narrative21"].ToString();
                txt_299_Narr22.Text = dt.Rows[0]["Narrative22"].ToString();
                txt_299_Narr23.Text = dt.Rows[0]["Narrative23"].ToString();
                txt_299_Narr24.Text = dt.Rows[0]["Narrative24"].ToString();
                txt_299_Narr25.Text = dt.Rows[0]["Narrative25"].ToString();
                txt_299_Narr26.Text = dt.Rows[0]["Narrative26"].ToString();
                txt_299_Narr27.Text = dt.Rows[0]["Narrative27"].ToString();
                txt_299_Narr28.Text = dt.Rows[0]["Narrative28"].ToString();
                txt_299_Narr29.Text = dt.Rows[0]["Narrative29"].ToString();
                txt_299_Narr30.Text = dt.Rows[0]["Narrative30"].ToString();
                txt_299_Narr31.Text = dt.Rows[0]["Narrative31"].ToString();
                txt_299_Narr32.Text = dt.Rows[0]["Narrative32"].ToString();
                txt_299_Narr33.Text = dt.Rows[0]["Narrative33"].ToString();
                txt_299_Narr34.Text = dt.Rows[0]["Narrative34"].ToString();
                txt_299_Narr35.Text = dt.Rows[0]["Narrative35"].ToString();
            }
            //============================299 end==========================//

            if (ddlSwiftTypes.SelectedItem.Text == "MT 999")
            {
                Panel_999.Visible = true;
                txtReceiver999.Text = dt.Rows[0]["Receiver"].ToString();
                txt_999_transRefNo.Text = dt.Rows[0]["Trans_RefNo"].ToString();
                txt_999_RelRef.Text = dt.Rows[0]["Related_Ref"].ToString();
                txt_999_Narr1.Text = dt.Rows[0]["Narrative1"].ToString();
                txt_999_Narr2.Text = dt.Rows[0]["Narrative2"].ToString();
                txt_999_Narr3.Text = dt.Rows[0]["Narrative3"].ToString();
                txt_999_Narr4.Text = dt.Rows[0]["Narrative4"].ToString();
                txt_999_Narr5.Text = dt.Rows[0]["Narrative5"].ToString();
                txt_999_Narr6.Text = dt.Rows[0]["Narrative6"].ToString();
                txt_999_Narr7.Text = dt.Rows[0]["Narrative7"].ToString();
                txt_999_Narr8.Text = dt.Rows[0]["Narrative8"].ToString();
                txt_999_Narr9.Text = dt.Rows[0]["Narrative9"].ToString();
                txt_999_Narr10.Text = dt.Rows[0]["Narrative10"].ToString();
                txt_999_Narr11.Text = dt.Rows[0]["Narrative11"].ToString();
                txt_999_Narr12.Text = dt.Rows[0]["Narrative12"].ToString();
                txt_999_Narr13.Text = dt.Rows[0]["Narrative13"].ToString();
                txt_999_Narr14.Text = dt.Rows[0]["Narrative14"].ToString();
                txt_999_Narr15.Text = dt.Rows[0]["Narrative15"].ToString();
                txt_999_Narr16.Text = dt.Rows[0]["Narrative16"].ToString();
                txt_999_Narr17.Text = dt.Rows[0]["Narrative17"].ToString();
                txt_999_Narr18.Text = dt.Rows[0]["Narrative18"].ToString();
                txt_999_Narr19.Text = dt.Rows[0]["Narrative19"].ToString();
                txt_999_Narr20.Text = dt.Rows[0]["Narrative20"].ToString();
                txt_999_Narr21.Text = dt.Rows[0]["Narrative21"].ToString();
                txt_999_Narr22.Text = dt.Rows[0]["Narrative22"].ToString();
                txt_999_Narr23.Text = dt.Rows[0]["Narrative23"].ToString();
                txt_999_Narr24.Text = dt.Rows[0]["Narrative24"].ToString();
                txt_999_Narr25.Text = dt.Rows[0]["Narrative25"].ToString();
                txt_999_Narr26.Text = dt.Rows[0]["Narrative26"].ToString();
                txt_999_Narr27.Text = dt.Rows[0]["Narrative27"].ToString();
                txt_999_Narr28.Text = dt.Rows[0]["Narrative28"].ToString();
                txt_999_Narr29.Text = dt.Rows[0]["Narrative29"].ToString();
                txt_999_Narr30.Text = dt.Rows[0]["Narrative30"].ToString();
                txt_999_Narr31.Text = dt.Rows[0]["Narrative31"].ToString();
                txt_999_Narr32.Text = dt.Rows[0]["Narrative32"].ToString();
                txt_999_Narr33.Text = dt.Rows[0]["Narrative33"].ToString();
                txt_999_Narr34.Text = dt.Rows[0]["Narrative34"].ToString();
                txt_999_Narr35.Text = dt.Rows[0]["Narrative35"].ToString();
            }
            //============================999 end==========================//

        }
    }

    public void Readonly()
    {
        ddlSwiftTypes.Enabled = false;
        ddlBranch.Enabled = false;
        txtReceiver742.Enabled = false;
        txtReceiver754.Enabled = false;
        txtReceiver799.Enabled = false;
        txtReceiver499.Enabled = false;
        txtReceiver199.Enabled = false;
        txtReceiver299.Enabled = false;
        txtReceiver999.Enabled = false;
        txtReceiver420.Enabled = false;
        txt_420_SendingBankTRN.Enabled = false;
        txt_420_RelRef.Enabled = false;
        ddlAmountTraced_420.Enabled = false;
        txtAmountTracedAmount_420.Enabled = false;
        ddlAmountTracedCode_420.Enabled = false;
        ddl_AmountTracedCurrency_420.Enabled = false;
        txtAmountTracedDate_420.Enabled = false;
        ddlAmountTracedDayMonth_420.Enabled = false;
        txtAmountTracedNoofDaysMonth_420.Enabled = false;
        txt_420_DateofCollnInstruction.Enabled = false;
        txt_420_DraweeAccount.Enabled = false;
        txt_420_DraweeName.Enabled = false;
        txt_420_DraweeAdd1.Enabled = false;
        txt_420_DraweeAdd2.Enabled = false;
        txt_420_DraweeAdd3.Enabled = false;
        txt_420_SenToRecinfo1.Enabled = false;
        txt_420_SenToRecinfo2.Enabled = false;
        txt_420_SenToRecinfo3.Enabled = false;
        txt_420_SenToRecinfo4.Enabled = false;
        txt_420_SenToRecinfo5.Enabled = false;
        txt_420_SenToRecinfo6.Enabled = false;
        //=========================420 end===================//
        //================499================//
        txt_499_transRefNo.Enabled = false;
        txt_499_RelRef.Enabled = false;
        txt_499_Narr1.Enabled = false;
        txt_499_Narr2.Enabled = false;
        txt_499_Narr3.Enabled = false;
        txt_499_Narr4.Enabled = false;
        txt_499_Narr5.Enabled = false;
        txt_499_Narr6.Enabled = false;
        txt_499_Narr7.Enabled = false;
        txt_499_Narr8.Enabled = false;
        txt_499_Narr9.Enabled = false;
        txt_499_Narr10.Enabled = false;
        txt_499_Narr11.Enabled = false;
        txt_499_Narr12.Enabled = false;
        txt_499_Narr13.Enabled = false;
        txt_499_Narr14.Enabled = false;
        txt_499_Narr15.Enabled = false;
        txt_499_Narr16.Enabled = false;
        txt_499_Narr17.Enabled = false;
        txt_499_Narr18.Enabled = false;
        txt_499_Narr19.Enabled = false;
        txt_499_Narr20.Enabled = false;
        txt_499_Narr21.Enabled = false;
        txt_499_Narr22.Enabled = false;
        txt_499_Narr23.Enabled = false;
        txt_499_Narr24.Enabled = false;
        txt_499_Narr25.Enabled = false;
        txt_499_Narr26.Enabled = false;
        txt_499_Narr27.Enabled = false;
        txt_499_Narr28.Enabled = false;
        txt_499_Narr29.Enabled = false;
        txt_499_Narr30.Enabled = false;
        txt_499_Narr31.Enabled = false;
        txt_499_Narr32.Enabled = false;
        txt_499_Narr33.Enabled = false;
        txt_499_Narr34.Enabled = false;
        txt_499_Narr35.Enabled = false;
        //==================499 end===============//
        //=========================742 ===================//
        txt_742_ClaimBankRef.Enabled = false;
        txt_742_DocumCreditNo.Enabled = false;
        txt_742_Dateofissue.Enabled = false;
        ddl_742_AddAmtClamd_Ccy.Enabled = false;
        ddl_742_PrinAmtClmd_Ccy.Enabled = false;
        ddl_742_TotalAmtClmd_Ccy.Enabled = false;
        ddlTotalAmtclamd_742.Enabled = false;
        TotalAmtClmd742_Date.Enabled = false;
        ddl_Issuingbank_742.Enabled = false;
        Dateofissue742.Enabled = false;
        txtIssuingBankAccountnumber_742.Enabled = false;
        txtIssuingBankAccountnumber1_742.Enabled = false;
        txtIssuingBankIdentifiercode_742.Enabled = false;
        txtIssuingBankName_742.Enabled = false;
        txtIssuingBankAddress1_742.Enabled = false;
        txtIssuingBankAddress2_742.Enabled = false;
        txtIssuingBankAddress3_742.Enabled = false;
        ddl_742_PrinAmtClmd_Ccy.Enabled = false;
        txt_742_PrinAmtClmd_Amt.Enabled = false;
        ddl_742_AddAmtClamd_Ccy.Enabled = false;
        txt_742_AddAmtClamd_Amt.Enabled = false;
        txt_742_Charges1.Enabled = false;
        txt_742_Charges2.Enabled = false;
        txt_742_Charges3.Enabled = false;
        txt_742_Charges4.Enabled = false;
        txt_742_Charges5.Enabled = false;
        txt_742_Charges6.Enabled = false;
        txt_742_TotalAmtClmd_Date.Enabled = false;
        ddl_742_TotalAmtClmd_Ccy.Enabled = false;
        txt_742_TotalAmtClmd_Amt.Enabled = false;
        ddlAccountwithbank_742.Enabled = false;
        txtAccountwithBankAccountnumber_742.Enabled = false;
        txtAccountwithBankAccountnumber1_742.Enabled = false;
        txtAccountwithBankIdentifiercode_742.Enabled = false;
        txtAccountwithBankLocation_742.Enabled = false;
        txtAccountwithBankName_742.Enabled = false;
        txtAccountwithBankAddress1_742.Enabled = false;
        txtAccountwithBankAddress2_742.Enabled = false;
        txtAccountwithBankAddress3_742.Enabled = false;
        ddlBeneficiarybank_742.Enabled = false;
        txtBeneficiaryBankAccountnumber_742.Enabled = false;
        txtBeneficiaryBankAccountnumber1_742.Enabled = false;
        txtBeneficiaryBankIdentifiercode_742.Enabled = false;
        txtBeneficiaryBankName_742.Enabled = false;
        txtBeneficiaryBankAddress1_742.Enabled = false;
        txtBeneficiaryBankAddress2_742.Enabled = false;
        txtBeneficiaryBankAddress3_742.Enabled = false;
        txt_742_SenRecInfo1.Enabled = false;
        txt_742_SenRecInfo2.Enabled = false;
        txt_742_SenRecInfo3.Enabled = false;
        txt_742_SenRecInfo4.Enabled = false;
        txt_742_SenRecInfo5.Enabled = false;
        txt_742_SenRecInfo6.Enabled = false;
        //=========================742 end===================//

        //=========================754 ===================//
        txt_754_SenRef.Enabled = false;
        txt_754_RelRef.Enabled = false;
        ddlAccountwithbank_754.Enabled = false;
        ddlBeneficiarybank_754.Enabled = false;
        txtPrinAmtPaidAccNegoDate_754.Enabled = false;
        ddl_PrinAmtPaidAccNegoCurr_754.Enabled = false;
        txtPrinAmtPaidAccNegoAmt_754.Enabled = false;
        ddl_754_AddAmtClamd_Ccy.Enabled = false;
        txtPrinAmtPaidAccNegoAmt_754.Enabled = false;
        ddlTotalAmtclamd_754.Enabled = false;
        ddlReimbursingbank_754.Enabled = false;
        ddlPrinAmtPaidAccNego_754.Enabled = false;
        txtPrinAmtPaidAccNegoAmt_754.Enabled = false;
        txt_754_AddAmtClamd_Amt.Enabled = false;
        BtnPrinAmtPaidAccNegoDate_754.Enabled = false;
        TotalAmtClmd754_Date.Enabled = false;
        txt_754_ChargesDeduct1.Enabled = false;
        txt_754_ChargesDeduct2.Enabled = false;
        txt_754_ChargesDeduct3.Enabled = false;
        txt_754_ChargesDeduct4.Enabled = false;
        txt_754_ChargesDeduct5.Enabled = false;
        txt_754_ChargesDeduct6.Enabled = false;
        txt_754_ChargesAdded1.Enabled = false;
        txt_754_ChargesAdded2.Enabled = false;
        txt_754_ChargesAdded3.Enabled = false;
        txt_754_ChargesAdded4.Enabled = false;
        txt_754_ChargesAdded5.Enabled = false;
        txt_754_ChargesAdded6.Enabled = false;
        txt_754_TotalAmtClmd_Date.Enabled = false;
        ddl_754_TotalAmtClmd_Ccy.Enabled = false;
        txt_754_TotalAmtClmd_Amt.Enabled = false;
        txtReimbursingBankAccountnumber_754.Enabled = false;
        txtReimbursingBankAccountnumber1_754.Enabled = false;
        txtReimbursingBankIdentifiercode_754.Enabled = false;
        txtReimbursingBankLocation_754.Enabled = false;
        txtReimbursingBankName_754.Enabled = false;
        txtReimbursingBankAddress1_754.Enabled = false;
        txtReimbursingBankAddress2_754.Enabled = false;
        txtReimbursingBankAddress3_754.Enabled = false;
        txtAccountwithBankAccountnumber_754.Enabled = false;
        txtAccountwithBankAccountnumber1_754.Enabled = false;
        txtAccountwithBankIdentifiercode_754.Enabled = false;
        txtAccountwithBankLocation_754.Enabled = false;
        txtAccountwithBankName_754.Enabled = false;
        txtAccountwithBankAddress1_754.Enabled = false;
        txtAccountwithBankAddress2_754.Enabled = false;
        txtAccountwithBankAddress3_754.Enabled = false;
        txtBeneficiaryBankAccountnumber_754.Enabled = false;
        txtBeneficiaryBankAccountnumber1_754.Enabled = false;
        txtBeneficiaryBankIdentifiercode_754.Enabled = false;
        txtBeneficiaryBankName_754.Enabled = false;
        txtBeneficiaryBankAddress1_754.Enabled = false;
        txtBeneficiaryBankAddress2_754.Enabled = false;
        txtBeneficiaryBankAddress3_754.Enabled = false;
        txt_754_SenRecInfo1.Enabled = false;
        txt_754_SenRecInfo2.Enabled = false;
        txt_754_SenRecInfo3.Enabled = false;
        txt_754_SenRecInfo4.Enabled = false;
        txt_754_SenRecInfo5.Enabled = false;
        txt_754_SenRecInfo6.Enabled = false;
        txt_754_Narr1.Enabled = false;
        txt_754_Narr2.Enabled = false;
        txt_754_Narr3.Enabled = false;
        txt_754_Narr4.Enabled = false;
        txt_754_Narr5.Enabled = false;
        txt_754_Narr6.Enabled = false;
        txt_754_Narr7.Enabled = false;
        txt_754_Narr8.Enabled = false;
        txt_754_Narr9.Enabled = false;
        txt_754_Narr10.Enabled = false;
        txt_754_Narr11.Enabled = false;
        txt_754_Narr12.Enabled = false;
        txt_754_Narr13.Enabled = false;
        txt_754_Narr14.Enabled = false;
        txt_754_Narr15.Enabled = false;
        txt_754_Narr16.Enabled = false;
        txt_754_Narr17.Enabled = false;
        txt_754_Narr18.Enabled = false;
        txt_754_Narr19.Enabled = false;
        txt_754_Narr20.Enabled = false;
        //=========================754 end===================//

        //================799================//
        txt_799_transRefNo.Enabled = false;
        txt_799_RelRef.Enabled = false;
        txt_799_Narr1.Enabled = false;
        txt_799_Narr2.Enabled = false;
        txt_799_Narr3.Enabled = false;
        txt_799_Narr4.Enabled = false;
        txt_799_Narr5.Enabled = false;
        txt_799_Narr6.Enabled = false;
        txt_799_Narr7.Enabled = false;
        txt_799_Narr8.Enabled = false;
        txt_799_Narr9.Enabled = false;
        txt_799_Narr10.Enabled = false;
        txt_799_Narr11.Enabled = false;
        txt_799_Narr12.Enabled = false;
        txt_799_Narr13.Enabled = false;
        txt_799_Narr14.Enabled = false;
        txt_799_Narr15.Enabled = false;
        txt_799_Narr16.Enabled = false;
        txt_799_Narr17.Enabled = false;
        txt_799_Narr18.Enabled = false;
        txt_799_Narr19.Enabled = false;
        txt_799_Narr20.Enabled = false;
        txt_799_Narr21.Enabled = false;
        txt_799_Narr22.Enabled = false;
        txt_799_Narr23.Enabled = false;
        txt_799_Narr24.Enabled = false;
        txt_799_Narr25.Enabled = false;
        txt_799_Narr26.Enabled = false;
        txt_799_Narr27.Enabled = false;
        txt_799_Narr28.Enabled = false;
        txt_799_Narr29.Enabled = false;
        txt_799_Narr30.Enabled = false;
        txt_799_Narr31.Enabled = false;
        txt_799_Narr32.Enabled = false;
        txt_799_Narr33.Enabled = false;
        txt_799_Narr34.Enabled = false;
        txt_799_Narr35.Enabled = false;
        //==================799 end===============//
        //================199================//
        txt_199_transRefNo.Enabled = false;
        txt_199_RelRef.Enabled = false;
        txt_199_Narr1.Enabled = false;
        txt_199_Narr2.Enabled = false;
        txt_199_Narr3.Enabled = false;
        txt_199_Narr4.Enabled = false;
        txt_199_Narr5.Enabled = false;
        txt_199_Narr6.Enabled = false;
        txt_199_Narr7.Enabled = false;
        txt_199_Narr8.Enabled = false;
        txt_199_Narr9.Enabled = false;
        txt_199_Narr10.Enabled = false;
        txt_199_Narr11.Enabled = false;
        txt_199_Narr12.Enabled = false;
        txt_199_Narr13.Enabled = false;
        txt_199_Narr14.Enabled = false;
        txt_199_Narr15.Enabled = false;
        txt_199_Narr16.Enabled = false;
        txt_199_Narr17.Enabled = false;
        txt_199_Narr18.Enabled = false;
        txt_199_Narr19.Enabled = false;
        txt_199_Narr20.Enabled = false;
        txt_199_Narr21.Enabled = false;
        txt_199_Narr22.Enabled = false;
        txt_199_Narr23.Enabled = false;
        txt_199_Narr24.Enabled = false;
        txt_199_Narr25.Enabled = false;
        txt_199_Narr26.Enabled = false;
        txt_199_Narr27.Enabled = false;
        txt_199_Narr28.Enabled = false;
        txt_199_Narr29.Enabled = false;
        txt_199_Narr30.Enabled = false;
        txt_199_Narr31.Enabled = false;
        txt_199_Narr32.Enabled = false;
        txt_199_Narr33.Enabled = false;
        txt_199_Narr34.Enabled = false;
        txt_199_Narr35.Enabled = false;

        //================299================//
        txt_299_transRefNo.Enabled = false;
        txt_299_RelRef.Enabled = false;
        txt_299_Narr1.Enabled = false;
        txt_299_Narr2.Enabled = false;
        txt_299_Narr3.Enabled = false;
        txt_299_Narr4.Enabled = false;
        txt_299_Narr5.Enabled = false;
        txt_299_Narr6.Enabled = false;
        txt_299_Narr7.Enabled = false;
        txt_299_Narr8.Enabled = false;
        txt_299_Narr9.Enabled = false;
        txt_299_Narr10.Enabled = false;
        txt_299_Narr11.Enabled = false;
        txt_299_Narr12.Enabled = false;
        txt_299_Narr13.Enabled = false;
        txt_299_Narr14.Enabled = false;
        txt_299_Narr15.Enabled = false;
        txt_299_Narr16.Enabled = false;
        txt_299_Narr17.Enabled = false;
        txt_299_Narr18.Enabled = false;
        txt_299_Narr19.Enabled = false;
        txt_299_Narr20.Enabled = false;
        txt_299_Narr21.Enabled = false;
        txt_299_Narr22.Enabled = false;
        txt_299_Narr23.Enabled = false;
        txt_299_Narr24.Enabled = false;
        txt_299_Narr25.Enabled = false;
        txt_299_Narr26.Enabled = false;
        txt_299_Narr27.Enabled = false;
        txt_299_Narr28.Enabled = false;
        txt_299_Narr29.Enabled = false;
        txt_299_Narr30.Enabled = false;
        txt_299_Narr31.Enabled = false;
        txt_299_Narr32.Enabled = false;
        txt_299_Narr33.Enabled = false;
        txt_299_Narr34.Enabled = false;
        txt_299_Narr35.Enabled = false;

        //================999================//
        txt_999_transRefNo.Enabled = false;
        txt_999_RelRef.Enabled = false;
        txt_999_Narr1.Enabled = false;
        txt_999_Narr2.Enabled = false;
        txt_999_Narr3.Enabled = false;
        txt_999_Narr4.Enabled = false;
        txt_999_Narr5.Enabled = false;
        txt_999_Narr6.Enabled = false;
        txt_999_Narr7.Enabled = false;
        txt_999_Narr8.Enabled = false;
        txt_999_Narr9.Enabled = false;
        txt_999_Narr10.Enabled = false;
        txt_999_Narr11.Enabled = false;
        txt_999_Narr12.Enabled = false;
        txt_999_Narr13.Enabled = false;
        txt_999_Narr14.Enabled = false;
        txt_999_Narr15.Enabled = false;
        txt_999_Narr16.Enabled = false;
        txt_999_Narr17.Enabled = false;
        txt_999_Narr18.Enabled = false;
        txt_999_Narr19.Enabled = false;
        txt_999_Narr20.Enabled = false;
        txt_999_Narr21.Enabled = false;
        txt_999_Narr22.Enabled = false;
        txt_999_Narr23.Enabled = false;
        txt_999_Narr24.Enabled = false;
        txt_999_Narr25.Enabled = false;
        txt_999_Narr26.Enabled = false;
        txt_999_Narr27.Enabled = false;
        txt_999_Narr28.Enabled = false;
        txt_999_Narr29.Enabled = false;
        txt_999_Narr30.Enabled = false;
        txt_999_Narr31.Enabled = false;
        txt_999_Narr32.Enabled = false;
        txt_999_Narr33.Enabled = false;
        txt_999_Narr34.Enabled = false;
        txt_999_Narr35.Enabled = false;
    }

    public void audittrail(string status, string DocNo)
    {
        SqlParameter p_Branch = new SqlParameter("@Branch", SqlDbType.VarChar);
        p_Branch.Value = ddlBranch.SelectedItem.Text;
        SqlParameter p_SwiftType = new SqlParameter("@Swift_type", SqlDbType.VarChar);
        p_SwiftType.Value = ddlSwiftTypes.SelectedItem.Text;
        SqlParameter p_Transrefno = new SqlParameter("@Trans_Ref_No", DocNo);
        string hdnUserName = Session["userName"].ToString();
        SqlParameter P_Status = new SqlParameter("@Status", status);
        SqlParameter P_Checkerid = new SqlParameter("@Checkerid", hdnUserName);
        SqlParameter P_Checkerdate = new SqlParameter("@CheckerDate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

        TF_DATA objSave = new TF_DATA();
        string _result = objSave.SaveDeleteData("TF_Exp_SaveAuditTrail_Checker", p_Branch, p_SwiftType, p_Transrefno, P_Status, P_Checkerid, P_Checkerdate);

        if (_result == "added")
        {
        }
    }

    protected void btn_View_Swift_Click(object sender, EventArgs e)
    {
        string SwiftType = ddlSwiftTypes.SelectedItem.Text;

        if (ddlSwiftTypes.SelectedItem.Text == "MT 742")
        {
            string Docno = txt_742_ClaimBankRef.Text.Trim();
            string url = "TF_EXP_SwiftFileView.aspx?SwiftType=" + SwiftType + "&Docno=" + Docno;
            string script = "window.open('" + url + "','_blank','height=600,width=1100,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
        }
        if (ddlSwiftTypes.SelectedItem.Text == "MT 799")
        {
            string Docno = txt_799_transRefNo.Text.Trim();
            string url = "TF_EXP_SwiftFileView.aspx?SwiftType=" + SwiftType + "&Docno=" + Docno;
            string script = "window.open('" + url + "','_blank','height=600,width=1100,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
        }
        if (ddlSwiftTypes.SelectedItem.Text == "MT 499")
        {
            string Docno = txt_499_transRefNo.Text.Trim();
            string url = "TF_EXP_SwiftFileView.aspx?SwiftType=" + SwiftType + "&Docno=" + Docno;
            string script = "window.open('" + url + "','_blank','height=600,width=1100,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
        }
        if (ddlSwiftTypes.SelectedItem.Text == "MT 199")
        {
            string Docno = txt_199_transRefNo.Text.Trim();
            string url = "TF_EXP_SwiftFileView.aspx?SwiftType=" + SwiftType + "&Docno=" + Docno;
            string script = "window.open('" + url + "','_blank','height=600,width=1100,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
        }
        if (ddlSwiftTypes.SelectedItem.Text == "MT 299")
        {
            string Docno = txt_299_transRefNo.Text.Trim();
            string url = "TF_EXP_SwiftFileView.aspx?SwiftType=" + SwiftType + "&Docno=" + Docno;
            string script = "window.open('" + url + "','_blank','height=600,width=1100,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
        }
        if (ddlSwiftTypes.SelectedItem.Text == "MT 999")
        {
            string Docno = txt_999_transRefNo.Text.Trim();
            string url = "TF_EXP_SwiftFileView.aspx?SwiftType=" + SwiftType + "&Docno=" + Docno;
            string script = "window.open('" + url + "','_blank','height=600,width=1100,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
        }

        if (ddlSwiftTypes.SelectedItem.Text == "MT 754")
        {
            string Docno = txt_754_SenRef.Text.Trim();
            string url = "TF_EXP_SwiftFileView.aspx?SwiftType=" + SwiftType + "&Docno=" + Docno;
            string script = "window.open('" + url + "','_blank','height=600,width=1100,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
        }
        if (ddlSwiftTypes.SelectedItem.Text == "MT 420")
        {
            string Docno = txt_420_SendingBankTRN.Text.Trim();
            string url = "TF_EXP_SwiftFileView.aspx?SwiftType=" + SwiftType + "&Docno=" + Docno;
            string script = "window.open('" + url + "','_blank','height=600,width=1100,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=20, left=100')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "popup", script, true);
        }
    }

    public void CreateSwift799_XML()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txt_799_transRefNo.Text.Trim());
        DataTable dt = objData1.getData("TF_Exp_Swift_MT799_FileCreation_XML", P_DocNo);
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
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/EXPORT/SWIFT_XML/799/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string transRefNo = txt_799_transRefNo.Text;
            if (txt_799_transRefNo.Text.Contains("/"))
            {
                transRefNo = txt_799_transRefNo.Text.Replace("/", "");
            }
            if (txt_799_transRefNo.Text.Contains("-"))
            {
                transRefNo = txt_799_transRefNo.Text.Replace("-", "");
            }

            string _filePath = "";
            _filePath = _directoryPath + "/" + transRefNo + "_MT799_" + TodayDate1 + ".xml";
            //if (FileType == "SWIFT")
            //{
            //    _filePath = _directoryPath + "/" + txtDocNo.Text + "_MT499_" + TodayDate1 + ".xml";
            //}
            //else
            //{
            //    _filePath = _directoryPath + "/" + txtDocNo.Text + "_FIN499_" + TodayDate1 + ".xml";
            //}
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            if (dt.Rows.Count > 0)
            {
                string Narrative = "";
                if (dt.Rows[0]["Narrative1"].ToString() != "")
                {
                    Narrative = Narrative + dt.Rows[0]["Narrative1"].ToString();
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
                if (dt.Rows[0]["Narrative7"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative7"].ToString();
                }
                if (dt.Rows[0]["Narrative8"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative8"].ToString();
                }
                if (dt.Rows[0]["Narrative9"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative9"].ToString();
                }
                if (dt.Rows[0]["Narrative10"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative10"].ToString();
                }
                if (dt.Rows[0]["Narrative11"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative11"].ToString();
                }
                if (dt.Rows[0]["Narrative12"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative12"].ToString();
                }
                if (dt.Rows[0]["Narrative13"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative13"].ToString();
                }
                if (dt.Rows[0]["Narrative14"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative14"].ToString();
                }
                if (dt.Rows[0]["Narrative15"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative15"].ToString();
                }
                if (dt.Rows[0]["Narrative16"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative16"].ToString();
                }
                if (dt.Rows[0]["Narrative17"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative17"].ToString();
                }
                if (dt.Rows[0]["Narrative18"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative18"].ToString();
                }
                if (dt.Rows[0]["Narrative19"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative19"].ToString();
                }
                if (dt.Rows[0]["Narrative20"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative20"].ToString();
                }
                if (dt.Rows[0]["Narrative21"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative21"].ToString();
                }
                if (dt.Rows[0]["Narrative22"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative22"].ToString();
                }
                if (dt.Rows[0]["Narrative23"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative23"].ToString();
                }
                if (dt.Rows[0]["Narrative24"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative24"].ToString();
                }
                if (dt.Rows[0]["Narrative25"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative25"].ToString();
                }
                if (dt.Rows[0]["Narrative26"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative26"].ToString();
                }
                if (dt.Rows[0]["Narrative27"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative27"].ToString();
                }
                if (dt.Rows[0]["Narrative28"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative28"].ToString();
                }
                if (dt.Rows[0]["Narrative29"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative29"].ToString();
                }
                if (dt.Rows[0]["Narrative30"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative30"].ToString();
                }
                if (dt.Rows[0]["Narrative31"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative31"].ToString();
                }
                if (dt.Rows[0]["Narrative32"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative32"].ToString();
                }
                if (dt.Rows[0]["Narrative33"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative33"].ToString();
                }
                if (dt.Rows[0]["Narrative34"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative34"].ToString();
                }
                if (dt.Rows[0]["Narrative35"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative35"].ToString();
                }

                sw.WriteLine("<reqMT" + dt.Rows[0]["XMLStartTag"].ToString() + ">");
                sw.WriteLine("<Sender>" + dt.Rows[0]["Sender"].ToString() + "</Sender>");
                sw.WriteLine("<MessageType>" + dt.Rows[0]["MessageType"].ToString() + "</MessageType>");
                sw.WriteLine("<Receiver>" + dt.Rows[0]["Receiver"].ToString() + "</Receiver>");
                sw.WriteLine("<TransactionReferenceNumber>" + dt.Rows[0]["Transaction Reference Number"].ToString().Replace("-", "") + "</TransactionReferenceNumber>");
                sw.WriteLine("<RelatedReference>" + dt.Rows[0]["Related Reference"].ToString() + "</RelatedReference>");
                sw.WriteLine("<Narrative>" + Narrative + "</Narrative>");
                sw.WriteLine("</reqMT>");

            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
            //Clearall();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('XML file generated successfully.');", true);
        }
    }

    public void CreateSwift499_XML()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txt_499_transRefNo.Text.Trim());
        DataTable dt = objData1.getData("TF_Exp_Swift_MT499_FileCreation_XML", P_DocNo);
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
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/EXPORT/SWIFT_XML/499/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string transRefNo = txt_499_transRefNo.Text;
            if (txt_499_transRefNo.Text.Contains("/"))
            {
                transRefNo = txt_499_transRefNo.Text.Replace("/", "");
            }
            if (txt_499_transRefNo.Text.Contains("-"))
            {
                transRefNo = txt_499_transRefNo.Text.Replace("-", "");
            }

            string _filePath = "";
            _filePath = _directoryPath + "/" + transRefNo + "_MT499_" + TodayDate1 + ".xml";

            StreamWriter sw;
            sw = File.CreateText(_filePath);
            if (dt.Rows.Count > 0)
            {
                string Narrative = "";
                if (dt.Rows[0]["Narrative1"].ToString() != "")
                {
                    Narrative = Narrative + dt.Rows[0]["Narrative1"].ToString();
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
                if (dt.Rows[0]["Narrative7"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative7"].ToString();
                }
                if (dt.Rows[0]["Narrative8"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative8"].ToString();
                }
                if (dt.Rows[0]["Narrative9"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative9"].ToString();
                }
                if (dt.Rows[0]["Narrative10"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative10"].ToString();
                }
                if (dt.Rows[0]["Narrative11"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative11"].ToString();
                }
                if (dt.Rows[0]["Narrative12"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative12"].ToString();
                }
                if (dt.Rows[0]["Narrative13"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative13"].ToString();
                }
                if (dt.Rows[0]["Narrative14"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative14"].ToString();
                }
                if (dt.Rows[0]["Narrative15"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative15"].ToString();
                }
                if (dt.Rows[0]["Narrative16"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative16"].ToString();
                }
                if (dt.Rows[0]["Narrative17"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative17"].ToString();
                }
                if (dt.Rows[0]["Narrative18"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative18"].ToString();
                }
                if (dt.Rows[0]["Narrative19"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative19"].ToString();
                }
                if (dt.Rows[0]["Narrative20"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative20"].ToString();
                }
                if (dt.Rows[0]["Narrative21"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative21"].ToString();
                }
                if (dt.Rows[0]["Narrative22"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative22"].ToString();
                }
                if (dt.Rows[0]["Narrative23"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative23"].ToString();
                }
                if (dt.Rows[0]["Narrative24"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative24"].ToString();
                }
                if (dt.Rows[0]["Narrative25"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative25"].ToString();
                }
                if (dt.Rows[0]["Narrative26"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative26"].ToString();
                }
                if (dt.Rows[0]["Narrative27"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative27"].ToString();
                }
                if (dt.Rows[0]["Narrative28"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative28"].ToString();
                }
                if (dt.Rows[0]["Narrative29"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative29"].ToString();
                }
                if (dt.Rows[0]["Narrative30"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative30"].ToString();
                }
                if (dt.Rows[0]["Narrative31"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative31"].ToString();
                }
                if (dt.Rows[0]["Narrative32"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative32"].ToString();
                }
                if (dt.Rows[0]["Narrative33"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative33"].ToString();
                }
                if (dt.Rows[0]["Narrative34"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative34"].ToString();
                }
                if (dt.Rows[0]["Narrative35"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative35"].ToString();
                }

                sw.WriteLine("<reqMT" + dt.Rows[0]["XMLStartTag"].ToString() + ">");
                sw.WriteLine("<Sender>" + dt.Rows[0]["Sender"].ToString() + "</Sender>");
                sw.WriteLine("<MessageType>" + dt.Rows[0]["MessageType"].ToString() + "</MessageType>");
                sw.WriteLine("<Receiver>" + dt.Rows[0]["Receiver"].ToString() + "</Receiver>");
                sw.WriteLine("<TransactionReferenceNumber>" + dt.Rows[0]["Transaction Reference Number"].ToString().Replace("-", "") + "</TransactionReferenceNumber>");
                sw.WriteLine("<RelatedReference>" + dt.Rows[0]["Related Reference"].ToString() + "</RelatedReference>");
                sw.WriteLine("<Narrative>" + Narrative + "</Narrative>");
                sw.WriteLine("</reqMT>");

            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
            //Clearall();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('XML file generated successfully.');", true);
        }
    }

    public void CreateSwift299_XML()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txt_299_transRefNo.Text.Trim());
        DataTable dt = objData1.getData("TF_Exp_Swift_MT299_FileCreation_XML", P_DocNo);
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
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/EXPORT/SWIFT_XML/299/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string transRefNo = txt_299_transRefNo.Text;
            if (txt_299_transRefNo.Text.Contains("/"))
            {
                transRefNo = txt_299_transRefNo.Text.Replace("/", "");
            }
            if (txt_299_transRefNo.Text.Contains("-"))
            {
                transRefNo = txt_299_transRefNo.Text.Replace("-", "");
            }

            string _filePath = "";
            _filePath = _directoryPath + "/" + transRefNo + "_MT299_" + TodayDate1 + ".xml";

            StreamWriter sw;
            sw = File.CreateText(_filePath);
            if (dt.Rows.Count > 0)
            {
                string Narrative = "";
                if (dt.Rows[0]["Narrative1"].ToString() != "")
                {
                    Narrative = Narrative + dt.Rows[0]["Narrative1"].ToString();
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
                if (dt.Rows[0]["Narrative7"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative7"].ToString();
                }
                if (dt.Rows[0]["Narrative8"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative8"].ToString();
                }
                if (dt.Rows[0]["Narrative9"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative9"].ToString();
                }
                if (dt.Rows[0]["Narrative10"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative10"].ToString();
                }
                if (dt.Rows[0]["Narrative11"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative11"].ToString();
                }
                if (dt.Rows[0]["Narrative12"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative12"].ToString();
                }
                if (dt.Rows[0]["Narrative13"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative13"].ToString();
                }
                if (dt.Rows[0]["Narrative14"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative14"].ToString();
                }
                if (dt.Rows[0]["Narrative15"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative15"].ToString();
                }
                if (dt.Rows[0]["Narrative16"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative16"].ToString();
                }
                if (dt.Rows[0]["Narrative17"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative17"].ToString();
                }
                if (dt.Rows[0]["Narrative18"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative18"].ToString();
                }
                if (dt.Rows[0]["Narrative19"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative19"].ToString();
                }
                if (dt.Rows[0]["Narrative20"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative20"].ToString();
                }
                if (dt.Rows[0]["Narrative21"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative21"].ToString();
                }
                if (dt.Rows[0]["Narrative22"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative22"].ToString();
                }
                if (dt.Rows[0]["Narrative23"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative23"].ToString();
                }
                if (dt.Rows[0]["Narrative24"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative24"].ToString();
                }
                if (dt.Rows[0]["Narrative25"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative25"].ToString();
                }
                if (dt.Rows[0]["Narrative26"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative26"].ToString();
                }
                if (dt.Rows[0]["Narrative27"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative27"].ToString();
                }
                if (dt.Rows[0]["Narrative28"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative28"].ToString();
                }
                if (dt.Rows[0]["Narrative29"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative29"].ToString();
                }
                if (dt.Rows[0]["Narrative30"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative30"].ToString();
                }
                if (dt.Rows[0]["Narrative31"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative31"].ToString();
                }
                if (dt.Rows[0]["Narrative32"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative32"].ToString();
                }
                if (dt.Rows[0]["Narrative33"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative33"].ToString();
                }
                if (dt.Rows[0]["Narrative34"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative34"].ToString();
                }
                if (dt.Rows[0]["Narrative35"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative35"].ToString();
                }

                sw.WriteLine("<reqMT" + dt.Rows[0]["XMLStartTag"].ToString() + ">");
                sw.WriteLine("<Sender>" + dt.Rows[0]["Sender"].ToString() + "</Sender>");
                sw.WriteLine("<MessageType>" + dt.Rows[0]["MessageType"].ToString() + "</MessageType>");
                sw.WriteLine("<Receiver>" + dt.Rows[0]["Receiver"].ToString() + "</Receiver>");
                sw.WriteLine("<TransactionReferenceNumber>" + dt.Rows[0]["Transaction Reference Number"].ToString().Replace("-", "") + "</TransactionReferenceNumber>");
                sw.WriteLine("<RelatedReference>" + dt.Rows[0]["Related Reference"].ToString() + "</RelatedReference>");
                sw.WriteLine("<Narrative>" + Narrative + "</Narrative>");
                sw.WriteLine("</reqMT>");

            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
            //Clearall();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('XML file generated successfully.');", true);
        }
    }

    public void CreateSwift199_XML()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txt_199_transRefNo.Text.Trim());
        DataTable dt = objData1.getData("TF_Exp_Swift_MT199_FileCreation_XML", P_DocNo);
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
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/EXPORT/SWIFT_XML/199/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string transRefNo = txt_199_transRefNo.Text;
            if (txt_199_transRefNo.Text.Contains("/"))
            {
                transRefNo = txt_199_transRefNo.Text.Replace("/", "");
            }
            if (txt_199_transRefNo.Text.Contains("-"))
            {
                transRefNo = txt_199_transRefNo.Text.Replace("-", "");
            }

            string _filePath = "";
            _filePath = _directoryPath + "/" + transRefNo + "_MT199_" + TodayDate1 + ".xml";

            StreamWriter sw;
            sw = File.CreateText(_filePath);
            if (dt.Rows.Count > 0)
            {
                string Narrative = "";
                if (dt.Rows[0]["Narrative1"].ToString() != "")
                {
                    Narrative = Narrative + dt.Rows[0]["Narrative1"].ToString();
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
                if (dt.Rows[0]["Narrative7"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative7"].ToString();
                }
                if (dt.Rows[0]["Narrative8"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative8"].ToString();
                }
                if (dt.Rows[0]["Narrative9"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative9"].ToString();
                }
                if (dt.Rows[0]["Narrative10"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative10"].ToString();
                }
                if (dt.Rows[0]["Narrative11"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative11"].ToString();
                }
                if (dt.Rows[0]["Narrative12"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative12"].ToString();
                }
                if (dt.Rows[0]["Narrative13"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative13"].ToString();
                }
                if (dt.Rows[0]["Narrative14"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative14"].ToString();
                }
                if (dt.Rows[0]["Narrative15"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative15"].ToString();
                }
                if (dt.Rows[0]["Narrative16"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative16"].ToString();
                }
                if (dt.Rows[0]["Narrative17"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative17"].ToString();
                }
                if (dt.Rows[0]["Narrative18"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative18"].ToString();
                }
                if (dt.Rows[0]["Narrative19"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative19"].ToString();
                }
                if (dt.Rows[0]["Narrative20"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative20"].ToString();
                }
                if (dt.Rows[0]["Narrative21"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative21"].ToString();
                }
                if (dt.Rows[0]["Narrative22"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative22"].ToString();
                }
                if (dt.Rows[0]["Narrative23"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative23"].ToString();
                }
                if (dt.Rows[0]["Narrative24"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative24"].ToString();
                }
                if (dt.Rows[0]["Narrative25"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative25"].ToString();
                }
                if (dt.Rows[0]["Narrative26"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative26"].ToString();
                }
                if (dt.Rows[0]["Narrative27"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative27"].ToString();
                }
                if (dt.Rows[0]["Narrative28"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative28"].ToString();
                }
                if (dt.Rows[0]["Narrative29"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative29"].ToString();
                }
                if (dt.Rows[0]["Narrative30"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative30"].ToString();
                }
                if (dt.Rows[0]["Narrative31"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative31"].ToString();
                }
                if (dt.Rows[0]["Narrative32"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative32"].ToString();
                }
                if (dt.Rows[0]["Narrative33"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative33"].ToString();
                }
                if (dt.Rows[0]["Narrative34"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative34"].ToString();
                }
                if (dt.Rows[0]["Narrative35"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative35"].ToString();
                }

                sw.WriteLine("<reqMT" + dt.Rows[0]["XMLStartTag"].ToString() + ">");
                sw.WriteLine("<Sender>" + dt.Rows[0]["Sender"].ToString() + "</Sender>");
                sw.WriteLine("<MessageType>" + dt.Rows[0]["MessageType"].ToString() + "</MessageType>");
                sw.WriteLine("<Receiver>" + dt.Rows[0]["Receiver"].ToString() + "</Receiver>");
                sw.WriteLine("<TransactionReferenceNumber>" + dt.Rows[0]["Transaction Reference Number"].ToString().Replace("-", "") + "</TransactionReferenceNumber>");
                sw.WriteLine("<RelatedReference>" + dt.Rows[0]["Related Reference"].ToString() + "</RelatedReference>");
                sw.WriteLine("<Narrative>" + Narrative + "</Narrative>");
                sw.WriteLine("</reqMT>");

            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
            //Clearall();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('XML file generated successfully.');", true);
        }
    }

    public void CreateSwift999_XML()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txt_999_transRefNo.Text.Trim());
        DataTable dt = objData1.getData("TF_Exp_Swift_MT999_FileCreation_XML", P_DocNo);
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
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/EXPORT/SWIFT_XML/999/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string transRefNo = txt_999_transRefNo.Text;
            if (txt_999_transRefNo.Text.Contains("/"))
            {
                transRefNo = txt_999_transRefNo.Text.Replace("/", "");
            }
            if (txt_999_transRefNo.Text.Contains("-"))
            {
                transRefNo = txt_999_transRefNo.Text.Replace("-", "");
            }

            string _filePath = "";
            _filePath = _directoryPath + "/" + transRefNo + "_MT999_" + TodayDate1 + ".xml";

            StreamWriter sw;
            sw = File.CreateText(_filePath);
            if (dt.Rows.Count > 0)
            {
                string Narrative = "";
                if (dt.Rows[0]["Narrative1"].ToString() != "")
                {
                    Narrative = Narrative + dt.Rows[0]["Narrative1"].ToString();
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
                if (dt.Rows[0]["Narrative7"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative7"].ToString();
                }
                if (dt.Rows[0]["Narrative8"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative8"].ToString();
                }
                if (dt.Rows[0]["Narrative9"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative9"].ToString();
                }
                if (dt.Rows[0]["Narrative10"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative10"].ToString();
                }
                if (dt.Rows[0]["Narrative11"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative11"].ToString();
                }
                if (dt.Rows[0]["Narrative12"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative12"].ToString();
                }
                if (dt.Rows[0]["Narrative13"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative13"].ToString();
                }
                if (dt.Rows[0]["Narrative14"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative14"].ToString();
                }
                if (dt.Rows[0]["Narrative15"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative15"].ToString();
                }
                if (dt.Rows[0]["Narrative16"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative16"].ToString();
                }
                if (dt.Rows[0]["Narrative17"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative17"].ToString();
                }
                if (dt.Rows[0]["Narrative18"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative18"].ToString();
                }
                if (dt.Rows[0]["Narrative19"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative19"].ToString();
                }
                if (dt.Rows[0]["Narrative20"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative20"].ToString();
                }
                if (dt.Rows[0]["Narrative21"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative21"].ToString();
                }
                if (dt.Rows[0]["Narrative22"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative22"].ToString();
                }
                if (dt.Rows[0]["Narrative23"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative23"].ToString();
                }
                if (dt.Rows[0]["Narrative24"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative24"].ToString();
                }
                if (dt.Rows[0]["Narrative25"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative25"].ToString();
                }
                if (dt.Rows[0]["Narrative26"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative26"].ToString();
                }
                if (dt.Rows[0]["Narrative27"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative27"].ToString();
                }
                if (dt.Rows[0]["Narrative28"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative28"].ToString();
                }
                if (dt.Rows[0]["Narrative29"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative29"].ToString();
                }
                if (dt.Rows[0]["Narrative30"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative30"].ToString();
                }
                if (dt.Rows[0]["Narrative31"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative31"].ToString();
                }
                if (dt.Rows[0]["Narrative32"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative32"].ToString();
                }
                if (dt.Rows[0]["Narrative33"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative33"].ToString();
                }
                if (dt.Rows[0]["Narrative34"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative34"].ToString();
                }
                if (dt.Rows[0]["Narrative35"].ToString() != "")
                {
                    Narrative = Narrative + "," + dt.Rows[0]["Narrative35"].ToString();
                }

                sw.WriteLine("<reqMT" + dt.Rows[0]["XMLStartTag"].ToString() + ">");
                sw.WriteLine("<Sender>" + dt.Rows[0]["Sender"].ToString() + "</Sender>");
                sw.WriteLine("<MessageType>" + dt.Rows[0]["MessageType"].ToString() + "</MessageType>");
                sw.WriteLine("<Receiver>" + dt.Rows[0]["Receiver"].ToString() + "</Receiver>");
                sw.WriteLine("<TransactionReferenceNumber>" + dt.Rows[0]["Transaction Reference Number"].ToString().Replace("-", "") + "</TransactionReferenceNumber>");
                sw.WriteLine("<RelatedReference>" + dt.Rows[0]["Related Reference"].ToString() + "</RelatedReference>");
                sw.WriteLine("<Narrative>" + Narrative + "</Narrative>");
                sw.WriteLine("</reqMT>");
            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
            //Clearall();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('XML file generated successfully.');", true);
        }
    }

    public void CreateSwift420_XML()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txt_420_SendingBankTRN.Text.Trim());
        DataTable dt = objData1.getData("TF_Exp_Swift_MT420_FileCreation_XML", P_DocNo);
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
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/EXPORT/SWIFT_XML/420/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string transRefNo = txt_420_SendingBankTRN.Text;
            if (txt_420_SendingBankTRN.Text.Contains("/"))
            {
                transRefNo = txt_420_SendingBankTRN.Text.Replace("/", "");
            }
            if (txt_420_SendingBankTRN.Text.Contains("-"))
            {
                transRefNo = txt_420_SendingBankTRN.Text.Replace("-", "");
            }

            string _filePath = "";
            _filePath = _directoryPath + "/" + transRefNo + "_MT420_" + TodayDate1 + ".xml";
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            if (dt.Rows.Count > 0)
            {
                //--------------------------------------------------AmountTraced-----------------------------------------------------------------------//
                string AmountTraced = "";
                if (dt.Rows[0]["ddlAmtTraced"].ToString() == "A")
                {
                    AmountTraced = dt.Rows[0]["TracedDate"].ToString() + dt.Rows[0]["Tracedcurr"].ToString();
                    if (dt.Rows[0]["TracedAmt"].ToString().Contains("."))
                    {
                        AmountTraced = AmountTraced + dt.Rows[0]["TracedAmt"].ToString().Replace(".", ",");
                    }
                    else
                    {
                        AmountTraced = AmountTraced + dt.Rows[0]["TracedAmt"].ToString() + ",";
                    }
                }
                if (dt.Rows[0]["ddlAmtTraced"].ToString() == "B")
                {
                    AmountTraced = dt.Rows[0]["Tracedcurr"].ToString();
                    if (dt.Rows[0]["TracedAmt"].ToString().Contains("."))
                    {
                        AmountTraced = AmountTraced + dt.Rows[0]["TracedAmt"].ToString().Replace(".", ",");
                    }
                    else
                    {
                        AmountTraced = AmountTraced + dt.Rows[0]["TracedAmt"].ToString() + ",";
                    }
                }
                if (dt.Rows[0]["ddlAmtTraced"].ToString() == "K")
                {
                    AmountTraced = dt.Rows[0]["TracedDayMonth"].ToString() + dt.Rows[0]["TracedNoofDayMonth"].ToString()
                        + dt.Rows[0]["Tracedcode"].ToString() + dt.Rows[0]["Tracedcurr"].ToString();
                    if (dt.Rows[0]["TracedAmt"].ToString().Contains("."))
                    {
                        AmountTraced = AmountTraced + dt.Rows[0]["TracedAmt"].ToString().Replace(".", ",");
                    }
                    else
                    {
                        AmountTraced = AmountTraced + dt.Rows[0]["TracedAmt"].ToString() + ",";
                    }

                }
                //-------------------------------------------End---AmountTraced-----------------------------------------------------------------------//
                //====================================== DateofCollnInstruction ===================================================//
                string DateofCollnInstruction = "";
                if (dt.Rows[0]["DateofCollnInstruction"].ToString() != "")
                {
                    DateofCollnInstruction = dt.Rows[0]["DateofCollnInstruction"].ToString().Replace("/", "");
                }
                //=================================== End DateofCollnInstruction =================================================//
                //--------------------------------------------------Drawee-----------------------------------------------------------------------//
                string Drawee = "";
                if (dt.Rows[0]["DraweeAccount"].ToString() != "")
                {
                    Drawee = dt.Rows[0]["DraweeAccount"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["DraweeName"].ToString() != "")
                {
                    Drawee = Drawee + "," + dt.Rows[0]["DraweeName"].ToString().ToString();
                }
                if (dt.Rows[0]["DraweeAdd1"].ToString() != "")
                {
                    Drawee = Drawee + "," + dt.Rows[0]["DraweeAdd1"].ToString().ToString();
                }
                if (dt.Rows[0]["DraweeAdd2"].ToString() != "")
                {
                    Drawee = Drawee + "," + dt.Rows[0]["DraweeAdd2"].ToString().ToString();
                }
                if (dt.Rows[0]["DraweeAdd3"].ToString() != "")
                {
                    Drawee = Drawee + "," + dt.Rows[0]["DraweeAdd3"].ToString().ToString();
                }
                //-----------------------------------------------End Drawee----------------------------------------------------------------------//
                //---------------------------------------------SendertoReceiverInformation-------------------------------------------------------//
                string SendertoReceiverInformation = "";
                if (dt.Rows[0]["SenToRecinfo1"].ToString() != "")
                {
                    SendertoReceiverInformation = dt.Rows[0]["SenToRecinfo1"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SenToRecinfo2"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SenToRecinfo2"].ToString().ToString();
                }
                if (dt.Rows[0]["SenToRecinfo3"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SenToRecinfo3"].ToString().ToString();
                }
                if (dt.Rows[0]["SenToRecinfo4"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SenToRecinfo4"].ToString().ToString();
                }
                if (dt.Rows[0]["SenToRecinfo5"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SenToRecinfo5"].ToString().ToString();
                }
                if (dt.Rows[0]["SenToRecinfo6"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SenToRecinfo6"].ToString().ToString();
                }
                //------------------------------------------------------End SendertoReceiverInformation----------------------------------------------------------//

                sw.WriteLine("<reqMT" + dt.Rows[0]["XMLStartTag"].ToString() + ">");
                sw.WriteLine("<Sender>" + dt.Rows[0]["Sender"].ToString() + "</Sender>");
                sw.WriteLine("<MessageType>" + dt.Rows[0]["MessageType"].ToString() + "</MessageType>");
                sw.WriteLine("<Receiver>" + dt.Rows[0]["Receiver"].ToString() + "</Receiver>");
                sw.WriteLine("<SendingBanksTRN>" + dt.Rows[0]["Transaction Reference Number"].ToString().Replace("-", "") + "</SendingBanksTRN>");
                sw.WriteLine("<RelatedReference>" + dt.Rows[0]["Related Reference"].ToString() + "</RelatedReference>");
                if (dt.Rows[0]["ddlAmtTraced"].ToString() == "A")
                {
                    sw.WriteLine("<AmountTracedA>" + AmountTraced + "</AmountTracedA>");
                }
                if (dt.Rows[0]["ddlAmtTraced"].ToString() == "B")
                {
                    sw.WriteLine("<AmountTracedB>" + AmountTraced + "</AmountTracedB>");
                }
                if (dt.Rows[0]["ddlAmtTraced"].ToString() == "K")
                {
                    sw.WriteLine("<AmountTracedK>" + AmountTraced + "</AmountTracedK>");
                }
                sw.WriteLine("<DateofCollection>" + DateofCollnInstruction + "</DateofCollection>");
                sw.WriteLine("<Drawee>" + Drawee + "</Drawee>");
                sw.WriteLine("<SendertoReceiverInformation>" + SendertoReceiverInformation + "</SendertoReceiverInformation>");
                sw.WriteLine("</reqMT>");

            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
            //Clearall();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('XML file generated successfully.');", true);
        }
    }

    public void CreateSwift754_XML()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txt_754_SenRef.Text.Trim());
        DataTable dt = objData1.getData("TF_Exp_Swift_MT754_FileCreation_XML", P_DocNo);
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
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/EXPORT/SWIFT_XML/754/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string transRefNo = txt_754_SenRef.Text;
            if (txt_754_SenRef.Text.Contains("/"))
            {
                transRefNo = txt_754_SenRef.Text.Replace("/", "");
            }
            if (txt_754_SenRef.Text.Contains("-"))
            {
                transRefNo = txt_754_SenRef.Text.Replace("-", "");
            }
            string _filePath = "";
            _filePath = _directoryPath + "/" + transRefNo + "_MT754_" + TodayDate1 + ".xml";
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            if (dt.Rows.Count > 0)
            {
                //--------------------------------------------------Principal Amount Paid/Accepted/Negotiated-----------------------------------------------------------------------//
                string ddlPrincipalAmtPaidAccptdNego = "";
                if (dt.Rows[0]["ddlPrincipalAmtPaidAccptdNego"].ToString() == "A")
                {
                    ddlPrincipalAmtPaidAccptdNego = dt.Rows[0]["PrincipalAmtPaidAccptdNego_Date"].ToString().Replace("/", "") + dt.Rows[0]["PrincipalAmtPaidAccptdNego_Curr"].ToString().Replace("/", "");
                    if (dt.Rows[0]["PrincipalAmtPaidAccptdNego_Amt"].ToString().Contains("."))
                    {
                        if (dt.Rows[0]["PrincipalAmtPaidAccptdNego_Amt"].ToString().Contains(".00"))
                        {
                            ddlPrincipalAmtPaidAccptdNego = ddlPrincipalAmtPaidAccptdNego + dt.Rows[0]["PrincipalAmtPaidAccptdNego_Amt"].ToString().Replace(".00", ",");
                        }
                        else
                        {
                            ddlPrincipalAmtPaidAccptdNego = ddlPrincipalAmtPaidAccptdNego + dt.Rows[0]["PrincipalAmtPaidAccptdNego_Amt"].ToString().Replace(".", ",");
                        }
                    }
                    else
                    {
                        ddlPrincipalAmtPaidAccptdNego = ddlPrincipalAmtPaidAccptdNego + dt.Rows[0]["PrincipalAmtPaidAccptdNego_Amt"].ToString().Replace("/", "") + ",";
                    }
                }
                if (dt.Rows[0]["ddlPrincipalAmtPaidAccptdNego"].ToString() == "B")
                {
                    ddlPrincipalAmtPaidAccptdNego = dt.Rows[0]["PrincipalAmtPaidAccptdNego_Curr"].ToString().Replace("/", "");
                    if (dt.Rows[0]["PrincipalAmtPaidAccptdNego_Amt"].ToString().Contains("."))
                    {
                        if (dt.Rows[0]["PrincipalAmtPaidAccptdNego_Amt"].ToString().Contains(".00"))
                        {
                            ddlPrincipalAmtPaidAccptdNego = ddlPrincipalAmtPaidAccptdNego + dt.Rows[0]["PrincipalAmtPaidAccptdNego_Amt"].ToString().Replace(".00", ",");
                        }
                        else
                        {
                            ddlPrincipalAmtPaidAccptdNego = ddlPrincipalAmtPaidAccptdNego + dt.Rows[0]["PrincipalAmtPaidAccptdNego_Amt"].ToString().Replace(".", ",");
                        }
                    }
                    else
                    {
                        ddlPrincipalAmtPaidAccptdNego = ddlPrincipalAmtPaidAccptdNego + dt.Rows[0]["PrincipalAmtPaidAccptdNego_Amt"].ToString().Replace("/", "") + ",";
                    }
                }

                //-------------------------------------------End---Principal Amount Paid/Accepted/Negotiated-----------------------------------------------------------------------//
                //-------------------------------------------------Additional Amounts---------------------------------------------------------------------//
                string AdditionalAmounts = "";
                if (dt.Rows[0]["AddnlAmt_Curr"].ToString() != "")
                {
                    AdditionalAmounts = dt.Rows[0]["AddnlAmt_Curr"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["AddnlAmt_Amt"].ToString() != "")
                {
                    if (dt.Rows[0]["AddnlAmt_Amt"].ToString().Contains("."))
                    {
                        if (dt.Rows[0]["AddnlAmt_Amt"].ToString().Contains(".00"))
                        {
                            AdditionalAmounts = AdditionalAmounts + dt.Rows[0]["AddnlAmt_Amt"].ToString().Replace(".00", ",");
                        }
                        else
                        {
                            AdditionalAmounts = AdditionalAmounts + dt.Rows[0]["AddnlAmt_Amt"].ToString().Replace(".", ",");
                        }
                    }
                    else
                    {
                        AdditionalAmounts = AdditionalAmounts + dt.Rows[0]["AddnlAmt_Amt"].ToString().Replace("/", "") + ",";
                    }
                }

                //-------------------------------------------------End Additional Amounts---------------------------------------------------------------------//
                //--------------------------------------------------Charges Deducted-----------------------------------------------------------------------//
                string ChargesDeducted = "";
                if (dt.Rows[0]["Charges1"].ToString() != "")
                {
                    ChargesDeducted = dt.Rows[0]["Charges1"].ToString();
                }
                if (dt.Rows[0]["Charges2"].ToString() != "")
                {
                    ChargesDeducted = ChargesDeducted + "," + dt.Rows[0]["Charges2"].ToString();
                }
                if (dt.Rows[0]["Charges3"].ToString() != "")
                {
                    ChargesDeducted = ChargesDeducted + "," + dt.Rows[0]["Charges3"].ToString();
                }
                if (dt.Rows[0]["Charges4"].ToString() != "")
                {
                    ChargesDeducted = ChargesDeducted + "," + dt.Rows[0]["Charges4"].ToString();
                }
                if (dt.Rows[0]["Charges5"].ToString() != "")
                {
                    ChargesDeducted = ChargesDeducted + "," + dt.Rows[0]["Charges5"].ToString();
                }
                if (dt.Rows[0]["Charges6"].ToString() != "")
                {
                    ChargesDeducted = ChargesDeducted + "," + dt.Rows[0]["Charges6"].ToString();
                }
                //-----------------------------------------------End Charges Deducted----------------------------------------------------------------------//
                //--------------------------------------------------Charges Added-----------------------------------------------------------------------//
                string ChargesAdded = "";
                if (dt.Rows[0]["ChargesAdded1"].ToString() != "")
                {
                    ChargesAdded = dt.Rows[0]["ChargesAdded1"].ToString();
                }
                if (dt.Rows[0]["ChargesAdded2"].ToString() != "")
                {
                    ChargesAdded = ChargesAdded + "," + dt.Rows[0]["ChargesAdded2"].ToString();
                }
                if (dt.Rows[0]["ChargesAdded3"].ToString() != "")
                {
                    ChargesAdded = ChargesAdded + "," + dt.Rows[0]["ChargesAdded3"].ToString();
                }
                if (dt.Rows[0]["ChargesAdded4"].ToString() != "")
                {
                    ChargesAdded = ChargesAdded + "," + dt.Rows[0]["ChargesAdded4"].ToString();
                }
                if (dt.Rows[0]["ChargesAdded5"].ToString() != "")
                {
                    ChargesAdded = ChargesAdded + "," + dt.Rows[0]["ChargesAdded5"].ToString();
                }
                if (dt.Rows[0]["ChargesAdded6"].ToString() != "")
                {
                    ChargesAdded = ChargesAdded + "," + dt.Rows[0]["ChargesAdded6"].ToString();
                }
                //-----------------------------------------------End Charges Added----------------------------------------------------------------------//
                //----------------------------------------------Total Amt clmd---------------------------------------------------------------------//
                string TotalAmountClaimed = "";
                if (dt.Rows[0]["ddlTotalAmtClaimed"].ToString() == "A")
                {
                    if (dt.Rows[0]["TotalAmtClaimed_Date"].ToString() != "")
                    {
                        TotalAmountClaimed = dt.Rows[0]["TotalAmtClaimed_Date"].ToString();
                    }
                    if (dt.Rows[0]["TotalAmtClaimed_Curr"].ToString() != "")
                    {
                        TotalAmountClaimed = TotalAmountClaimed + dt.Rows[0]["TotalAmtClaimed_Curr"].ToString();
                    }
                    if (dt.Rows[0]["TotalAmtClaimed_Amt"].ToString() != "")
                    {
                        if (dt.Rows[0]["TotalAmtClaimed_Amt"].ToString().Contains("."))
                        {
                            if (dt.Rows[0]["TotalAmtClaimed_Amt"].ToString().Contains(".00"))
                            {
                                TotalAmountClaimed = TotalAmountClaimed + dt.Rows[0]["TotalAmtClaimed_Amt"].ToString().Replace(".00", ",");
                            }
                            else
                            {
                                TotalAmountClaimed = TotalAmountClaimed + dt.Rows[0]["TotalAmtClaimed_Amt"].ToString().Replace(".", ",");
                            }
                        }
                        else
                        {
                            TotalAmountClaimed = TotalAmountClaimed + dt.Rows[0]["TotalAmtClaimed_Amt"].ToString().Replace("/", "") + ",";
                        }
                    }
                }
                if (dt.Rows[0]["ddlTotalAmtClaimed"].ToString() == "B")
                {
                    if (dt.Rows[0]["TotalAmtClaimed_Curr"].ToString() != "")
                    {
                        TotalAmountClaimed = TotalAmountClaimed + dt.Rows[0]["TotalAmtClaimed_Curr"].ToString();
                    }
                    if (dt.Rows[0]["TotalAmtClaimed_Amt"].ToString() != "")
                    {
                        if (dt.Rows[0]["TotalAmtClaimed_Amt"].ToString().Contains("."))
                        {
                            if (dt.Rows[0]["TotalAmtClaimed_Amt"].ToString().Contains(".00"))
                            {
                                TotalAmountClaimed = TotalAmountClaimed + dt.Rows[0]["TotalAmtClaimed_Amt"].ToString().Replace(".00", ",");
                            }
                            else
                            {
                                TotalAmountClaimed = TotalAmountClaimed + dt.Rows[0]["TotalAmtClaimed_Amt"].ToString().Replace(".", ",");
                            }
                        }
                        else
                        {
                            TotalAmountClaimed = TotalAmountClaimed + dt.Rows[0]["TotalAmtClaimed_Amt"].ToString().Replace("/", "") + ",";
                        }
                    }
                }

                //----------------------------------------------End Total Amt clmd---------------------------------------------------------------------//
                //----------------------------------------------Reimbursing Bank-----------------------------------------------------------------------//
                string ReimbursingBank = "";
                if (dt.Rows[0]["ddlReimbursingBank"].ToString() == "A")
                {
                    if (dt.Rows[0]["ReimbursingBank_PartyIdent"].ToString() != "" || dt.Rows[0]["ReimbursingBank_PartyIdent1"].ToString() != "")
                    {
                        ReimbursingBank = dt.Rows[0]["ReimbursingBank_PartyIdent"].ToString() + dt.Rows[0]["ReimbursingBank_PartyIdent1"].ToString();
                    }
                    if (ReimbursingBank != "" && dt.Rows[0]["ReimbursingBank_Identcode"].ToString() != "")
                    {
                        ReimbursingBank = ReimbursingBank + "#" + dt.Rows[0]["ReimbursingBank_Identcode"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["ReimbursingBank_Identcode"].ToString() != "")
                        {
                            ReimbursingBank = dt.Rows[0]["ReimbursingBank_Identcode"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["ddlReimbursingBank"].ToString() == "B")
                {
                    if (dt.Rows[0]["ReimbursingBank_PartyIdent"].ToString() != "" || dt.Rows[0]["ReimbursingBank_PartyIdent1"].ToString() != "")
                    {
                        ReimbursingBank = dt.Rows[0]["ReimbursingBank_PartyIdent"].ToString() + dt.Rows[0]["ReimbursingBank_PartyIdent1"].ToString();
                    }
                    if (ReimbursingBank != "" && dt.Rows[0]["ReimbursingBank_Location"].ToString() != "")
                    {
                        ReimbursingBank = ReimbursingBank + "#" + dt.Rows[0]["ReimbursingBank_Location"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["ReimbursingBank_Location"].ToString() != "")
                        {
                            ReimbursingBank = dt.Rows[0]["ReimbursingBank_Location"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["ddlReimbursingBank"].ToString() == "D")
                {
                    ReimbursingBank = dt.Rows[0]["ReimbursingBank_PartyIdent"].ToString() + dt.Rows[0]["ReimbursingBank_PartyIdent1"].ToString();
                    if (dt.Rows[0]["ReimbursingBank_Name"].ToString() != "")
                    {
                        ReimbursingBank = ReimbursingBank + '#' + dt.Rows[0]["ReimbursingBank_Name"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["ReimbursingBank_Add1"].ToString() != "")
                    {
                        ReimbursingBank = ReimbursingBank + ',' + dt.Rows[0]["ReimbursingBank_Add1"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["ReimbursingBank_Add2"].ToString() != "")
                    {
                        ReimbursingBank = ReimbursingBank + ',' + dt.Rows[0]["ReimbursingBank_Add2"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["ReimbursingBank_Add3"].ToString() != "")
                    {
                        ReimbursingBank = ReimbursingBank + ',' + dt.Rows[0]["ReimbursingBank_Add3"].ToString().Replace("/", "");
                    }
                }
                //----------------------------------------------End Reimbursing Bank-----------------------------------------------------------------------//
                //----------------------------------------------Account With Bank----------------------------------------------------------------------//
                string AccountWithBank = "";
                if (dt.Rows[0]["ddlAccWithBank"].ToString() == "A")
                {
                    if (dt.Rows[0]["AccWithBank_PartyIdent"].ToString() != "" || dt.Rows[0]["AccWithBank_PartyIdent1"].ToString() != "")
                    {
                        AccountWithBank = dt.Rows[0]["AccWithBank_PartyIdent"].ToString() + dt.Rows[0]["AccWithBank_PartyIdent1"].ToString();
                    }
                    if (AccountWithBank != "" && dt.Rows[0]["AccWithBank_Identcode"].ToString() != "")
                    {
                        AccountWithBank = AccountWithBank + "#" + dt.Rows[0]["AccWithBank_Identcode"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["AccWithBank_Identcode"].ToString() != "")
                        {
                            AccountWithBank = dt.Rows[0]["AccWithBank_Identcode"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["ddlAccWithBank"].ToString() == "B")
                {
                    if (dt.Rows[0]["AccWithBank_PartyIdent"].ToString() != "" || dt.Rows[0]["AccWithBank_PartyIdent1"].ToString() != "")
                    {
                        AccountWithBank = dt.Rows[0]["AccWithBank_PartyIdent"].ToString() + dt.Rows[0]["AccWithBank_PartyIdent1"].ToString();
                    }
                    if (AccountWithBank != "" && dt.Rows[0]["AccWithBank_Location"].ToString() != "")
                    {
                        AccountWithBank = AccountWithBank + "#" + dt.Rows[0]["AccWithBank_Location"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["AccWithBank_Location"].ToString() != "")
                        {
                            AccountWithBank = dt.Rows[0]["AccWithBank_Location"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["ddlAccWithBank"].ToString() == "D")
                {
                    AccountWithBank = dt.Rows[0]["AccWithBank_PartyIdent"].ToString() + dt.Rows[0]["AccWithBank_PartyIdent1"].ToString();
                    if (dt.Rows[0]["AccWithBank_Name"].ToString() != "")
                    {
                        AccountWithBank = AccountWithBank + '#' + dt.Rows[0]["AccWithBank_Name"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["AccWithBank_Add1"].ToString() != "")
                    {
                        AccountWithBank = AccountWithBank + ',' + dt.Rows[0]["AccWithBank_Add1"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["AccWithBank_Add2"].ToString() != "")
                    {
                        AccountWithBank = AccountWithBank + ',' + dt.Rows[0]["AccWithBank_Add2"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["AccWithBank_Add3"].ToString() != "")
                    {
                        AccountWithBank = AccountWithBank + ',' + dt.Rows[0]["AccWithBank_Add3"].ToString().Replace("/", "");
                    }
                }

                //----------------------------------------------End Account With Bank-----------------------------------------------------------------------//
                //----------------------------------------------Beneficiary Bank----------------------------------------------------------------------//
                string BeneficiaryBank = "";
                if (dt.Rows[0]["ddlBeneficiaryBank"].ToString() == "A")
                {
                    if (dt.Rows[0]["BeneficiaryBank_PartyIdent"].ToString() != "" || dt.Rows[0]["BeneficiaryBank_PartyIdent1"].ToString() != "")
                    {
                        BeneficiaryBank = dt.Rows[0]["BeneficiaryBank_PartyIdent"].ToString() + dt.Rows[0]["BeneficiaryBank_PartyIdent1"].ToString();
                    }
                    if (BeneficiaryBank != "" && dt.Rows[0]["BeneficiaryBank_Identcode"].ToString() != "")
                    {
                        BeneficiaryBank = BeneficiaryBank + "#" + dt.Rows[0]["BeneficiaryBank_Identcode"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["BeneficiaryBank_Identcode"].ToString() != "")
                        {
                            BeneficiaryBank = dt.Rows[0]["BeneficiaryBank_Identcode"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["ddlBeneficiaryBank"].ToString() == "D")
                {
                    BeneficiaryBank = dt.Rows[0]["BeneficiaryBank_PartyIdent"].ToString() + dt.Rows[0]["BeneficiaryBank_PartyIdent1"].ToString();
                    if (dt.Rows[0]["BeneficiaryBank_Name"].ToString() != "")
                    {
                        BeneficiaryBank = BeneficiaryBank + '#' + dt.Rows[0]["BeneficiaryBank_Name"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["BeneficiaryBank_Add1"].ToString() != "")
                    {
                        BeneficiaryBank = BeneficiaryBank + ',' + dt.Rows[0]["BeneficiaryBank_Add1"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["BeneficiaryBank_Add2"].ToString() != "")
                    {
                        BeneficiaryBank = BeneficiaryBank + ',' + dt.Rows[0]["BeneficiaryBank_Add2"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["BeneficiaryBank_Add3"].ToString() != "")
                    {
                        BeneficiaryBank = BeneficiaryBank + ',' + dt.Rows[0]["BeneficiaryBank_Add3"].ToString().Replace("/", "");
                    }
                }

                //----------------------------------------------End Beneficiary Bank-----------------------------------------------------------------------//
                //---------------------------------------------SendertoReceiverInformation-------------------------------------------------------//
                string SendertoReceiverInformation = "";
                if (dt.Rows[0]["SenToRecinfo1"].ToString() != "")
                {
                    SendertoReceiverInformation = dt.Rows[0]["SenToRecinfo1"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SenToRecinfo2"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SenToRecinfo2"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SenToRecinfo3"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SenToRecinfo3"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SenToRecinfo4"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SenToRecinfo4"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SenToRecinfo5"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SenToRecinfo5"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SenToRecinfo6"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SenToRecinfo6"].ToString().Replace("/", "");
                }

                //------------------------------------------------------End SendertoReceiverInformation----------------------------------------------------------//
                //==========================================================Narrative================================================================================//
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
                //=================================================Narrative end================================================//

                sw.WriteLine("<reqMT" + dt.Rows[0]["XMLStartTag"].ToString() + ">");
                sw.WriteLine("<Sender>" + dt.Rows[0]["Sender"].ToString() + "</Sender>");
                sw.WriteLine("<MessageType>" + dt.Rows[0]["MessageType"].ToString() + "</MessageType>");
                sw.WriteLine("<Receiver>" + dt.Rows[0]["Receiver"].ToString() + "</Receiver>");
                sw.WriteLine("<SendersReference>" + dt.Rows[0]["Senders Reference Number"].ToString() + "</SendersReference>");
                sw.WriteLine("<RelatedReference>" + dt.Rows[0]["Related Reference"].ToString() + "</RelatedReference>");
                if (dt.Rows[0]["ddlPrincipalAmtPaidAccptdNego"].ToString() == "A")
                {
                    sw.WriteLine("<PrincipalAmountA>" + ddlPrincipalAmtPaidAccptdNego + "</PrincipalAmountA>");
                }
                if (dt.Rows[0]["ddlPrincipalAmtPaidAccptdNego"].ToString() == "B")
                {
                    sw.WriteLine("<PrincipalAmountB>" + ddlPrincipalAmtPaidAccptdNego + "</PrincipalAmountB>");
                }

                sw.WriteLine("<AdditionalAmounts>" + AdditionalAmounts + "</AdditionalAmounts>");
                sw.WriteLine("<ChargesDeducted>" + ChargesDeducted + "</ChargesDeducted>");
                sw.WriteLine("<ChargesAdded>" + ChargesAdded + "</ChargesAdded>");
                if (dt.Rows[0]["ddlTotalAmtClaimed"].ToString() == "A")
                {
                    sw.WriteLine("<TotalAmountClaimedA>" + TotalAmountClaimed + "</TotalAmountClaimedA>");
                }
                if (dt.Rows[0]["ddlTotalAmtClaimed"].ToString() == "B")
                {
                    sw.WriteLine("<TotalAmountClaimedB>" + TotalAmountClaimed + "</TotalAmountClaimedB>");
                }

                if (dt.Rows[0]["ddlReimbursingBank"].ToString() == "A")
                {
                    sw.WriteLine("<ReimbursingBankA>" + ReimbursingBank + "</ReimbursingBankA>");
                }
                if (dt.Rows[0]["ddlReimbursingBank"].ToString() == "B")
                {
                    sw.WriteLine("<ReimbursingBankB>" + ReimbursingBank + "</ReimbursingBankB>");
                }
                if (dt.Rows[0]["ddlReimbursingBank"].ToString() == "D")
                {
                    sw.WriteLine("<ReimbursingBankD>" + ReimbursingBank + "</ReimbursingBankD>");
                }

                if (dt.Rows[0]["ddlAccWithBank"].ToString() == "A")
                {
                    sw.WriteLine("<AccountWithBankA>" + AccountWithBank + "</AccountWithBankA>");
                }
                if (dt.Rows[0]["ddlAccWithBank"].ToString() == "B")
                {
                    sw.WriteLine("<AccountWithBankB>" + AccountWithBank + "</AccountWithBankB>");
                }
                if (dt.Rows[0]["ddlAccWithBank"].ToString() == "D")
                {
                    sw.WriteLine("<AccountWithBankD>" + AccountWithBank + "</AccountWithBankD>");
                }

                if (dt.Rows[0]["ddlBeneficiaryBank"].ToString() == "A")
                {
                    sw.WriteLine("<BeneficiaryBankA>" + BeneficiaryBank + "</BeneficiaryBankA>");
                }
                if (dt.Rows[0]["ddlBeneficiaryBank"].ToString() == "D")
                {
                    sw.WriteLine("<BeneficiaryBankD>" + BeneficiaryBank + "</BeneficiaryBankD>");
                }
                sw.WriteLine("<SendertoReceiverInformation>" + SendertoReceiverInformation + "</SendertoReceiverInformation>");
                sw.WriteLine("<Narrative>" + Narrative + "</Narrative>");
                sw.WriteLine("</reqMT>");

            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
            //Clearall();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('XML file generated successfully.');", true);
        }
    }

    public void CreateSwift742_XML()
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter P_DocNo = new SqlParameter("@DocNo", txt_742_ClaimBankRef.Text.Trim());
        DataTable dt = objData1.getData("TF_Exp_Swift_MT742_FileCreation_XML", P_DocNo);
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
            _directoryPath = Server.MapPath("~/TF_GeneratedFiles/EXPORT/SWIFT_XML/742/" + MTodayDate + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string transRefNo = txt_742_ClaimBankRef.Text;
            if (txt_742_ClaimBankRef.Text.Contains("/"))
            {
                transRefNo = txt_742_ClaimBankRef.Text.Replace("/", "");
            }
            if (txt_742_ClaimBankRef.Text.Contains("-"))
            {
                transRefNo = txt_742_ClaimBankRef.Text.Replace("-", "");
            }
            string _filePath = "";
            _filePath = _directoryPath + "/" + transRefNo + "_MT742_" + TodayDate1 + ".xml";
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            if (dt.Rows.Count > 0)
            {
                string comma = "";
                #region Date Of Issue
                string DateofIssue = dt.Rows[0]["Dateofissue"].ToString().Replace("/", "");
                #endregion
                #region Issuing Bank
                string IssuingBank = "";
                if (dt.Rows[0]["ddlIssuingBank"].ToString() == "A")
                {
                    if (dt.Rows[0]["IssuingBank_PartyIdent"].ToString() != "" || dt.Rows[0]["IssuingBank_PartyIdent1"].ToString() != "")
                    {
                        IssuingBank = dt.Rows[0]["IssuingBank_PartyIdent"].ToString() + dt.Rows[0]["IssuingBank_PartyIdent1"].ToString();
                    }
                    if (IssuingBank != "" && dt.Rows[0]["IssuingBank_Identcode"].ToString() != "")
                    {
                        IssuingBank = IssuingBank + "#" + dt.Rows[0]["IssuingBank_Identcode"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["IssuingBank_Identcode"].ToString() != "")
                        {
                            IssuingBank = dt.Rows[0]["IssuingBank_Identcode"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["ddlIssuingBank"].ToString() == "D")
                {
                    IssuingBank = dt.Rows[0]["IssuingBank_PartyIdent"].ToString() + dt.Rows[0]["IssuingBank_PartyIdent1"].ToString();
                    if (dt.Rows[0]["IssuingBank_Name"].ToString() != "")
                    {
                        IssuingBank = IssuingBank + "#" + dt.Rows[0]["IssuingBank_Name"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["IssuingBank_Add1"].ToString() != "")
                    {
                        IssuingBank = IssuingBank + "," + dt.Rows[0]["IssuingBank_Add1"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["IssuingBank_Add2"].ToString() != "")
                    {
                        IssuingBank = IssuingBank + "," + dt.Rows[0]["IssuingBank_Add2"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["IssuingBank_Add3"].ToString() != "")
                    {
                        IssuingBank = IssuingBank + "," + dt.Rows[0]["IssuingBank_Add3"].ToString().Replace("/", "");
                    }
                }
                if (IssuingBank.Length != 0)
                {
                    comma = IssuingBank.Trim().Substring(0, 1);
                    if (comma == ",") { IssuingBank = IssuingBank.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region Principal Amount Claimed
                string PrincipalAmountClaimed = "";
                if (dt.Rows[0]["PrincipalAmtClaimed_Curr"].ToString() != "")
                {
                    PrincipalAmountClaimed = dt.Rows[0]["PrincipalAmtClaimed_Curr"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["PrincipalAmtClaimed_Amt"].ToString() != "")
                {
                    if (dt.Rows[0]["PrincipalAmtClaimed_Amt"].ToString().Contains("."))
                    {
                        if (dt.Rows[0]["PrincipalAmtClaimed_Amt"].ToString().Contains(".00"))
                        {
                            PrincipalAmountClaimed = PrincipalAmountClaimed + dt.Rows[0]["PrincipalAmtClaimed_Amt"].ToString().Replace(".00", ",");
                        }
                        else
                        {
                            PrincipalAmountClaimed = PrincipalAmountClaimed + dt.Rows[0]["PrincipalAmtClaimed_Amt"].ToString().Replace(".", ",");
                        }
                    }
                    else
                    {
                        PrincipalAmountClaimed = PrincipalAmountClaimed + dt.Rows[0]["PrincipalAmtClaimed_Amt"].ToString().Replace("/", "") + ",";
                    }
                }
                #endregion
                #region Additional Amount Claimed as Allowed for in Excess of Principal Amounts
                string AdditionalAmounts = "";
                if (dt.Rows[0]["AddnlAmt_Curr"].ToString() != "")
                {
                    AdditionalAmounts = dt.Rows[0]["AddnlAmt_Curr"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["AddnlAmt_Amt"].ToString() != "")
                {
                    if (dt.Rows[0]["AddnlAmt_Amt"].ToString().Contains("."))
                    {
                        AdditionalAmounts = AdditionalAmounts + dt.Rows[0]["AddnlAmt_Amt"].ToString().Replace(".", ",");
                    }
                    else
                    {
                        AdditionalAmounts = AdditionalAmounts + dt.Rows[0]["AddnlAmt_Amt"].ToString().Replace("/", "") + ",";
                    }
                }
                #endregion
                #region Charges
                string Charges = "";
                if (dt.Rows[0]["Charges1"].ToString() != "")
                {
                    Charges = dt.Rows[0]["Charges1"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Charges2"].ToString() != "")
                {
                    Charges = Charges + "," + dt.Rows[0]["Charges2"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Charges3"].ToString() != "")
                {
                    Charges = Charges + "," + dt.Rows[0]["Charges3"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Charges4"].ToString() != "")
                {
                    Charges = Charges + "," + dt.Rows[0]["Charges4"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Charges5"].ToString() != "")
                {
                    Charges = Charges + "," + dt.Rows[0]["Charges5"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["Charges6"].ToString() != "")
                {
                    Charges = Charges + "," + dt.Rows[0]["Charges6"].ToString().Replace("/", "");
                }
                if (Charges.Length != 0)
                {
                    comma = Charges.Trim().Substring(0, 1);
                    if (comma == ",") { Charges = Charges.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region Total Amount Claimed
                string TotalAmountClaimed = "";
                if (dt.Rows[0]["ddlTotalAmtClaimed"].ToString() == "A")
                {
                    if (dt.Rows[0]["TotalAmtClaimed_Date"].ToString() != "")
                    {
                        TotalAmountClaimed = dt.Rows[0]["TotalAmtClaimed_Date"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["TotalAmtClaimed_Curr"].ToString() != "")
                    {
                        TotalAmountClaimed = TotalAmountClaimed + dt.Rows[0]["TotalAmtClaimed_Curr"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["TotalAmtClaimed_Amt"].ToString() != "")
                    {
                        if (dt.Rows[0]["TotalAmtClaimed_Amt"].ToString().Contains("."))
                        {
                            if (dt.Rows[0]["TotalAmtClaimed_Amt"].ToString().Contains(".00"))
                            {
                                TotalAmountClaimed = TotalAmountClaimed + dt.Rows[0]["TotalAmtClaimed_Amt"].ToString().Replace(".00", ",");
                            }
                            else
                            {
                                TotalAmountClaimed = TotalAmountClaimed + dt.Rows[0]["TotalAmtClaimed_Amt"].ToString().Replace(".", ",");
                            }
                        }
                        else
                        {
                            TotalAmountClaimed = TotalAmountClaimed + dt.Rows[0]["TotalAmtClaimed_Amt"].ToString().Replace("/", "") + ",";
                        }
                    }
                }
                if (dt.Rows[0]["ddlTotalAmtClaimed"].ToString() == "B")
                {
                    if (dt.Rows[0]["TotalAmtClaimed_Curr"].ToString() != "")
                    {
                        TotalAmountClaimed = TotalAmountClaimed + dt.Rows[0]["TotalAmtClaimed_Curr"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["TotalAmtClaimed_Amt"].ToString() != "")
                    {
                        if (dt.Rows[0]["TotalAmtClaimed_Amt"].ToString().Contains("."))
                        {
                            if (dt.Rows[0]["TotalAmtClaimed_Amt"].ToString().Contains(".00"))
                            {
                                TotalAmountClaimed = TotalAmountClaimed + dt.Rows[0]["TotalAmtClaimed_Amt"].ToString().Replace(".00", ",");
                            }
                            else
                            {
                                TotalAmountClaimed = TotalAmountClaimed + dt.Rows[0]["TotalAmtClaimed_Amt"].ToString().Replace(".", ",");
                            }
                        }
                        else
                        {
                            TotalAmountClaimed = TotalAmountClaimed + dt.Rows[0]["TotalAmtClaimed_Amt"].ToString().Replace("/", "") + ",";
                        }
                    }
                }
                #endregion
                #region Account With Bank
                string AccountWithBank = "";
                if (dt.Rows[0]["ddlAccWithBank"].ToString() == "A")
                {
                    if (dt.Rows[0]["AccWithBank_PartyIdent"].ToString() != "" || dt.Rows[0]["AccWithBank_PartyIdent1"].ToString() != "")
                    {
                        AccountWithBank = dt.Rows[0]["AccWithBank_PartyIdent"].ToString() + dt.Rows[0]["AccWithBank_PartyIdent1"].ToString();
                    }
                    if (AccountWithBank != "" && dt.Rows[0]["AccWithBank_Identcode"].ToString() != "")
                    {
                        AccountWithBank = AccountWithBank + "#" + dt.Rows[0]["AccWithBank_Identcode"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["AccWithBank_Identcode"].ToString() != "")
                        {
                            AccountWithBank = dt.Rows[0]["AccWithBank_Identcode"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["ddlAccWithBank"].ToString() == "B")
                {
                    if (dt.Rows[0]["AccWithBank_PartyIdent"].ToString() != "" || dt.Rows[0]["AccWithBank_PartyIdent1"].ToString() != "")
                    {
                        AccountWithBank = dt.Rows[0]["AccWithBank_PartyIdent"].ToString() + dt.Rows[0]["AccWithBank_PartyIdent1"].ToString();
                    }
                    if (AccountWithBank != "" && dt.Rows[0]["AccWithBank_Location"].ToString() != "")
                    {
                        AccountWithBank = AccountWithBank + "#" + dt.Rows[0]["AccWithBank_Location"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["AccWithBank_Location"].ToString() != "")
                        {
                            AccountWithBank = dt.Rows[0]["AccWithBank_Location"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["ddlAccWithBank"].ToString() == "D")
                {
                    AccountWithBank = dt.Rows[0]["AccWithBank_PartyIdent"].ToString() + dt.Rows[0]["AccWithBank_PartyIdent1"].ToString();
                    if (dt.Rows[0]["AccWithBank_Name"].ToString() != "")
                    {
                        AccountWithBank = AccountWithBank + '#' + dt.Rows[0]["AccWithBank_Name"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["AccWithBank_Add1"].ToString() != "")
                    {
                        AccountWithBank = AccountWithBank + ',' + dt.Rows[0]["AccWithBank_Add1"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["AccWithBank_Add2"].ToString() != "")
                    {
                        AccountWithBank = AccountWithBank + ',' + dt.Rows[0]["AccWithBank_Add2"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["AccWithBank_Add3"].ToString() != "")
                    {
                        AccountWithBank = AccountWithBank + ',' + dt.Rows[0]["AccWithBank_Add3"].ToString().Replace("/", "");
                    }
                }
                if (AccountWithBank.Length != 0)
                {
                    comma = AccountWithBank.Trim().Substring(0, 1);
                    if (comma == ",") { AccountWithBank = AccountWithBank.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region Beneficiary Bank
                string BeneficiaryBank = "";
                if (dt.Rows[0]["ddlBeneficiaryBank"].ToString() == "A")
                {
                    if (dt.Rows[0]["BeneficiaryBank_PartyIdent"].ToString() != "" || dt.Rows[0]["BeneficiaryBank_PartyIdent1"].ToString() != "")
                    {
                        BeneficiaryBank = dt.Rows[0]["BeneficiaryBank_PartyIdent"].ToString() + dt.Rows[0]["BeneficiaryBank_PartyIdent1"].ToString();
                    }
                    if (BeneficiaryBank != "" && dt.Rows[0]["BeneficiaryBank_Identcode"].ToString() != "")
                    {
                        BeneficiaryBank = BeneficiaryBank + "#" + dt.Rows[0]["BeneficiaryBank_Identcode"].ToString();
                    }
                    else
                    {
                        if (dt.Rows[0]["BeneficiaryBank_Identcode"].ToString() != "")
                        {
                            BeneficiaryBank = dt.Rows[0]["BeneficiaryBank_Identcode"].ToString();
                        }
                    }
                }
                if (dt.Rows[0]["ddlBeneficiaryBank"].ToString() == "D")
                {
                    BeneficiaryBank = dt.Rows[0]["BeneficiaryBank_PartyIdent"].ToString() + dt.Rows[0]["BeneficiaryBank_PartyIdent1"].ToString();
                    if (dt.Rows[0]["BeneficiaryBank_Name"].ToString() != "")
                    {
                        BeneficiaryBank = BeneficiaryBank + '#' + dt.Rows[0]["BeneficiaryBank_Name"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["BeneficiaryBank_Add1"].ToString() != "")
                    {
                        BeneficiaryBank = BeneficiaryBank + ',' + dt.Rows[0]["BeneficiaryBank_Add1"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["BeneficiaryBank_Add2"].ToString() != "")
                    {
                        BeneficiaryBank = BeneficiaryBank + ',' + dt.Rows[0]["BeneficiaryBank_Add2"].ToString().Replace("/", "");
                    }
                    if (dt.Rows[0]["BeneficiaryBank_Add3"].ToString() != "")
                    {
                        BeneficiaryBank = BeneficiaryBank + ',' + dt.Rows[0]["BeneficiaryBank_Add3"].ToString().Replace("/", "");
                    }
                }
                if (BeneficiaryBank.Length != 0)
                {
                    comma = BeneficiaryBank.Trim().Substring(0, 1);
                    if (comma == ",") { BeneficiaryBank = BeneficiaryBank.Remove(0, 1); }
                    comma = "";
                }
                #endregion
                #region Sender To Receiver Information
                string SendertoReceiverInformation = "";
                if (dt.Rows[0]["SenToRecinfo1"].ToString() != "")
                {
                    SendertoReceiverInformation = dt.Rows[0]["SenToRecinfo1"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SenToRecinfo2"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SenToRecinfo2"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SenToRecinfo3"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SenToRecinfo3"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SenToRecinfo4"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SenToRecinfo4"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SenToRecinfo5"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SenToRecinfo5"].ToString().Replace("/", "");
                }
                if (dt.Rows[0]["SenToRecinfo6"].ToString() != "")
                {
                    SendertoReceiverInformation = SendertoReceiverInformation + "," + dt.Rows[0]["SenToRecinfo6"].ToString().Replace("/", "");
                }
                if (SendertoReceiverInformation.Length != 0)
                {
                    comma = SendertoReceiverInformation.Trim().Substring(0, 1);
                    if (comma == ",") { SendertoReceiverInformation = SendertoReceiverInformation.Remove(0, 1); }
                    comma = "";
                }
                #endregion

                sw.WriteLine("<reqMT" + dt.Rows[0]["XMLStartTag"].ToString() + ">");
                sw.WriteLine("<Sender>" + dt.Rows[0]["Sender"].ToString() + "</Sender>");
                sw.WriteLine("<MessageType>" + dt.Rows[0]["MessageType"].ToString() + "</MessageType>");
                sw.WriteLine("<Receiver>" + dt.Rows[0]["Receiver"].ToString() + "</Receiver>");
                sw.WriteLine("<ClaimingBanksReference>" + dt.Rows[0]["Claiming Banks Reference"].ToString() + "</ClaimingBanksReference>");
                sw.WriteLine("<DocumentaryCreditNumber>" + dt.Rows[0]["Documentary Credit Number"].ToString() + "</DocumentaryCreditNumber>");
                sw.WriteLine("<DateofIssue>" + DateofIssue + "</DateofIssue>");
                if (dt.Rows[0]["ddlIssuingBank"].ToString() == "A")
                {
                    sw.WriteLine("<IssuingBankA>" + IssuingBank + "</IssuingBankA>");
                }
                if (dt.Rows[0]["ddlIssuingBank"].ToString() == "D")
                {
                    sw.WriteLine("<IssuingBankD>" + IssuingBank + "</IssuingBankD>");
                }
                sw.WriteLine("<PrincipalAmountClaimed>" + PrincipalAmountClaimed + "</PrincipalAmountClaimed>");
                sw.WriteLine("<AdditionalAmtClaimed>" + AdditionalAmounts + "</AdditionalAmtClaimed>");
                sw.WriteLine("<Charges>" + Charges + "</Charges>");
                if (dt.Rows[0]["ddlTotalAmtClaimed"].ToString() == "A")
                {
                    sw.WriteLine("<TotalAmountClaimedA>" + TotalAmountClaimed + "</TotalAmountClaimedA>");
                }
                if (dt.Rows[0]["ddlTotalAmtClaimed"].ToString() == "B")
                {
                    sw.WriteLine("<TotalAmountClaimedB>" + TotalAmountClaimed + "</TotalAmountClaimedB>");
                }
                if (dt.Rows[0]["ddlAccWithBank"].ToString() == "A")
                {
                    sw.WriteLine("<AccountWithBankA>" + AccountWithBank + "</AccountWithBankA>");
                }
                if (dt.Rows[0]["ddlAccWithBank"].ToString() == "B")
                {
                    sw.WriteLine("<AccountWithBankB>" + AccountWithBank + "</AccountWithBankB>");
                }
                if (dt.Rows[0]["ddlAccWithBank"].ToString() == "D")
                {
                    sw.WriteLine("<AccountWithBankD>" + AccountWithBank + "</AccountWithBankD>");
                }
                if (dt.Rows[0]["ddlBeneficiaryBank"].ToString() == "A")
                {
                    sw.WriteLine("<BeneficiaryBankA>" + BeneficiaryBank + "</BeneficiaryBankA>");
                }
                if (dt.Rows[0]["ddlBeneficiaryBank"].ToString() == "D")
                {
                    sw.WriteLine("<BeneficiaryBankD>" + BeneficiaryBank + "</BeneficiaryBankD>");
                }
                sw.WriteLine("<SendertoReceiverInformation>" + SendertoReceiverInformation + "</SendertoReceiverInformation>");
                sw.WriteLine("</reqMT>");

            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
            //Clearall();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('XML file generated successfully.');", true);
        }
    }

    protected void fillCurrency()
    {
        try
        {
            TF_DATA objData = new TF_DATA();
            DataTable dt_PrinAmt = objData.getData("TF_IMP_Currency_List");
            ddl_742_PrinAmtClmd_Ccy.Items.Clear();
            ListItem li_PrinAmt = new ListItem();
            li_PrinAmt.Value = "";
            if (dt_PrinAmt.Rows.Count > 0)
            {
                li_PrinAmt.Text = "";
                ddl_742_PrinAmtClmd_Ccy.DataSource = dt_PrinAmt.DefaultView;
                ddl_742_PrinAmtClmd_Ccy.DataTextField = "C_Code";
                ddl_742_PrinAmtClmd_Ccy.DataValueField = "C_Code";
                ddl_742_PrinAmtClmd_Ccy.DataBind();
            }
            else
            {
                li_PrinAmt.Text = "No record(s) found";
            }
            ddl_742_PrinAmtClmd_Ccy.Items.Insert(0, li_PrinAmt);

            //////////////////////////

            DataTable dt_AddAmtClamd = objData.getData("TF_IMP_Currency_List");
            ddl_742_AddAmtClamd_Ccy.Items.Clear();
            ListItem li_AddAmtClamd = new ListItem();
            li_AddAmtClamd.Value = "";
            if (dt_AddAmtClamd.Rows.Count > 0)
            {
                li_AddAmtClamd.Text = "";
                ddl_742_AddAmtClamd_Ccy.DataSource = dt_AddAmtClamd.DefaultView;
                ddl_742_AddAmtClamd_Ccy.DataTextField = "C_Code";
                ddl_742_AddAmtClamd_Ccy.DataValueField = "C_Code";
                ddl_742_AddAmtClamd_Ccy.DataBind();
            }
            else
            {
                li_AddAmtClamd.Text = "No record(s) found";
            }
            ddl_742_AddAmtClamd_Ccy.Items.Insert(0, li_AddAmtClamd);

            ///////////////////////////

            DataTable dt_TotalAmtClmd = objData.getData("TF_IMP_Currency_List");
            ddl_742_TotalAmtClmd_Ccy.Items.Clear();
            ListItem li_TotalAmtClmd = new ListItem();
            li_TotalAmtClmd.Value = "";
            if (dt_TotalAmtClmd.Rows.Count > 0)
            {
                li_TotalAmtClmd.Text = "";
                ddl_742_TotalAmtClmd_Ccy.DataSource = dt_TotalAmtClmd.DefaultView;
                ddl_742_TotalAmtClmd_Ccy.DataTextField = "C_Code";
                ddl_742_TotalAmtClmd_Ccy.DataValueField = "C_Code";
                ddl_742_TotalAmtClmd_Ccy.DataBind();
            }
            else
            {
                li_TotalAmtClmd.Text = "No record(s) found";
            }
            ddl_742_TotalAmtClmd_Ccy.Items.Insert(0, li_TotalAmtClmd);

            ////////////////////////
            DataTable dt_AddAmt = objData.getData("TF_IMP_Currency_List");
            ddl_754_AddAmtClamd_Ccy.Items.Clear();
            ListItem li_AddAmt = new ListItem();
            li_AddAmt.Value = "";
            if (dt_AddAmt.Rows.Count > 0)
            {
                li_AddAmt.Text = "";
                ddl_754_AddAmtClamd_Ccy.DataSource = dt_AddAmt.DefaultView;
                ddl_754_AddAmtClamd_Ccy.DataTextField = "C_Code";
                ddl_754_AddAmtClamd_Ccy.DataValueField = "C_Code";
                ddl_754_AddAmtClamd_Ccy.DataBind();
            }
            else
            {
                li_AddAmt.Text = "No record(s) found";
            }
            ddl_754_AddAmtClamd_Ccy.Items.Insert(0, li_AddAmt);

            ///////////////////
            DataTable dt_PrinAmtPaidAccNego = objData.getData("TF_IMP_Currency_List");
            ddl_PrinAmtPaidAccNegoCurr_754.Items.Clear();
            ListItem li_PrinAmtPaidAccNego = new ListItem();
            li_PrinAmtPaidAccNego.Value = "";
            if (dt_PrinAmtPaidAccNego.Rows.Count > 0)
            {
                li_PrinAmtPaidAccNego.Value = "";
                ddl_PrinAmtPaidAccNegoCurr_754.DataSource = dt_PrinAmtPaidAccNego.DefaultView;
                ddl_PrinAmtPaidAccNegoCurr_754.DataTextField = "C_Code";
                ddl_PrinAmtPaidAccNegoCurr_754.DataValueField = "C_Code";
                ddl_PrinAmtPaidAccNegoCurr_754.DataBind();
            }
            else
            {
                li_PrinAmtPaidAccNego.Text = "No record(s) found";
            }
            ddl_PrinAmtPaidAccNegoCurr_754.Items.Insert(0, li_PrinAmtPaidAccNego);

            ////////////////

            DataTable dt_TotalAmt = objData.getData("TF_IMP_Currency_List");
            ddl_754_TotalAmtClmd_Ccy.Items.Clear();
            ListItem li_TotalAmt = new ListItem();
            li_TotalAmt.Value = "";
            if (dt_TotalAmt.Rows.Count > 0)
            {
                li_TotalAmt.Text = "";
                ddl_754_TotalAmtClmd_Ccy.DataSource = dt_TotalAmt.DefaultView;
                ddl_754_TotalAmtClmd_Ccy.DataTextField = "C_Code";
                ddl_754_TotalAmtClmd_Ccy.DataValueField = "C_Code";
                ddl_754_TotalAmtClmd_Ccy.DataBind();
            }
            else
            {
                li_TotalAmt.Text = "No record(s) found";
            }
            ddl_754_TotalAmtClmd_Ccy.Items.Insert(0, li_TotalAmt);

            //////////////// 

            DataTable dt_AmountTraced = objData.getData("TF_IMP_Currency_List");
            ddl_AmountTracedCurrency_420.Items.Clear();
            ListItem li_AmountTraced = new ListItem();
            li_AmountTraced.Value = "";
            if (dt_AmountTraced.Rows.Count > 0)
            {
                ddl_AmountTracedCurrency_420.DataSource = dt_AmountTraced.DefaultView;
                ddl_AmountTracedCurrency_420.DataTextField = "C_Code";
                ddl_AmountTracedCurrency_420.DataValueField = "C_Code";
                ddl_AmountTracedCurrency_420.DataBind();
            }
            else
            {
                li_AmountTraced.Text = "No record(s) found";
            }
            ddl_AmountTracedCurrency_420.Items.Insert(0, li_AmountTraced);


        }
        catch (Exception ex)
        {
            //objIMP.CreateUserLog(hdnUserName.Value, ex.Message);
        }
    }

    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "All Branches";
        if (dt.Rows.Count > 0)
        {
            li.Text = "All Branches";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
    }

    protected void ddl_742_PrinAmtClmd_Ccy_SelectedIndexChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@CCode", SqlDbType.VarChar);
        p1.Value = ddl_742_PrinAmtClmd_Ccy.SelectedItem.Text.Trim();
        string _query = "TF_Exp_Currency_List";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            ddl_742_PrinAmtClmd_Ccy.Text = dt.Rows[0]["C_Code"].ToString().Trim();
        }
    }

    protected void ddl_742_AddAmtClamd_Ccy_SelectedIndexChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@CCode", SqlDbType.VarChar);
        p1.Value = ddl_742_AddAmtClamd_Ccy.SelectedItem.Text.Trim();
        string _query = "TF_Exp_Currency_List";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            ddl_742_AddAmtClamd_Ccy.Text = dt.Rows[0]["C_Code"].ToString().Trim();
        }
    }

    protected void ddl_742_TotalAmtClmd_Ccy_SelectedIndexChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@CCode", SqlDbType.VarChar);
        p1.Value = ddl_742_TotalAmtClmd_Ccy.SelectedItem.Text.Trim();
        string _query = "TF_Exp_Currency_List";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            ddl_742_TotalAmtClmd_Ccy.Text = dt.Rows[0]["C_Code"].ToString().Trim();
        }
    }

    protected void ddl_754_AddAmtClamd_Ccy_SelectedIndexChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@CCode", SqlDbType.VarChar);
        p1.Value = ddl_754_AddAmtClamd_Ccy.SelectedItem.Text.Trim();
        string _query = "TF_Exp_Currency_List";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            ddl_754_AddAmtClamd_Ccy.Text = dt.Rows[0]["C_Code"].ToString().Trim();
        }
    }

    protected void ddl_PrinAmtPaidAccNegoCurr_754_SelectedIndexChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@CCode", SqlDbType.VarChar);
        p1.Value = ddl_PrinAmtPaidAccNegoCurr_754.SelectedItem.Text.Trim();
        string _query = "TF_Exp_Currency_List";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            ddl_PrinAmtPaidAccNegoCurr_754.Text = dt.Rows[0]["C_Code"].ToString().Trim();
        }
    }

    protected void ddl_754_TotalAmtClmd_Ccy_SelectedIndexChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@CCode", SqlDbType.VarChar);
        p1.Value = ddl_754_TotalAmtClmd_Ccy.SelectedItem.Text.Trim();
        string _query = "TF_Exp_Currency_List";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            ddl_754_TotalAmtClmd_Ccy.Text = dt.Rows[0]["C_Code"].ToString().Trim();
        }
    }

    protected void ddl_AmountTracedCurrency_420_SelectedIndexChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@CCode", SqlDbType.VarChar);
        p1.Value = ddl_AmountTracedCurrency_420.SelectedItem.Text.Trim();
        string _query = "TF_Exp_Currency_List";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            ddl_AmountTracedCurrency_420.Text = dt.Rows[0]["C_Code"].ToString().Trim();
        }
    }

    protected void ddlAmountTraced_420_TextChanged(object sender, EventArgs e)
    {
        if (ddlAmountTraced_420.SelectedValue == "A")
        {
            lblAmountTracedDayMonth_420.Visible = false;
            ddlAmountTracedDayMonth_420.Visible = false;
            //ddlAmountTracedDayMonth_420.Text = "";
            lblAmountTracedNoofDaysMonth_420.Visible = false;
            txtAmountTracedNoofDaysMonth_420.Visible = false;
            txtAmountTracedNoofDaysMonth_420.Text = "";
            lblAmountTracedDate_420.Visible = true;
            txtAmountTracedDate_420.Visible = true;
            lblAmountTracedCode_420.Visible = false;
            ddlAmountTracedCode_420.Visible = false;
            //ddlAmountTracedCode_420.Text = "";
            ddl_AmountTracedCurrency_420.Visible = true;
            txtAmountTracedAmount_420.Visible = true;
            btnCal420_Date.Visible = true;
        }
        else
        {
            if (ddlAmountTraced_420.SelectedValue == "B")
            {
                lblAmountTracedDayMonth_420.Visible = false;
                ddlAmountTracedDayMonth_420.Visible = false;
                //ddlAmountTracedDayMonth_420.Text = "";
                lblAmountTracedNoofDaysMonth_420.Visible = false;
                txtAmountTracedNoofDaysMonth_420.Visible = false;
                txtAmountTracedNoofDaysMonth_420.Text = "";
                lblAmountTracedDate_420.Visible = false;
                txtAmountTracedDate_420.Visible = false;
                txtAmountTracedDate_420.Text = "";
                lblAmountTracedCode_420.Visible = false;
                ddlAmountTracedCode_420.Visible = false;
                //ddlAmountTracedCode_420.Text = "";
                ddl_AmountTracedCurrency_420.Visible = true;
                txtAmountTracedAmount_420.Visible = true;
                btnCal420_Date.Visible = false;
            }
            else
            {
                lblAmountTracedDayMonth_420.Visible = true;
                ddlAmountTracedDayMonth_420.Visible = true;
                lblAmountTracedNoofDaysMonth_420.Visible = true;
                txtAmountTracedNoofDaysMonth_420.Visible = true;
                lblAmountTracedDate_420.Visible = false;
                txtAmountTracedDate_420.Visible = false;
                txtAmountTracedDate_420.Text = "";
                lblAmountTracedCode_420.Visible = true;
                ddlAmountTracedCode_420.Visible = true;
                ddl_AmountTracedCurrency_420.Visible = true;
                txtAmountTracedAmount_420.Visible = true;
                btnCal420_Date.Visible = false;
            }
        }
    }

    //protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlBranch.SelectedItem.Value == "4")
    //    {
    //        ddlBranch.SelectedItem.Text = "Bangalore";
    //    }
    //    else if (ddlBranch.SelectedItem.Value == "2")
    //    {
    //        ddlBranch.SelectedItem.Text = "New Delhi";
    //    }
    //    else if (ddlBranch.SelectedItem.Value == "3")
    //    {
    //        ddlBranch.SelectedItem.Text = "Chennai";
    //    }
    //    else if (ddlBranch.SelectedItem.Value == "1")
    //    {
    //        ddlBranch.SelectedItem.Text = "Mumbai";
    //    }
    //    else
    //    {
    //        ddlBranch.Text = "";
    //    }
    //}

    protected void ddl_Issuingbank_742_TextChanged(object sender, EventArgs e)
    {
        if (ddl_Issuingbank_742.SelectedValue == "A")
        {
            lblIssuingBankIdentifier_742.Text = "Identifier Code: ";
            txtIssuingBankIdentifiercode_742.Visible = true;
            txtIssuingBankName_742.Visible = false;
            txtIssuingBankName_742.Text = "";
            lblIssuingBankAddress1_742.Visible = false;
            lblIssuingBankAddress2_742.Visible = false;
            lblIssuingBankAddress3_742.Visible = false;
            txtIssuingBankAddress1_742.Visible = false;
            txtIssuingBankAddress1_742.Text = "";
            txtIssuingBankAddress2_742.Visible = false;
            txtIssuingBankAddress2_742.Text = "";
            txtIssuingBankAddress3_742.Visible = false;
            txtIssuingBankAddress3_742.Text = "";
        }
        else
        {
            lblIssuingBankIdentifier_742.Text = "Name: ";
            txtIssuingBankIdentifiercode_742.Visible = false;
            txtIssuingBankIdentifiercode_742.Text = "";
            txtIssuingBankName_742.Visible = true;
            lblIssuingBankAddress1_742.Visible = true;
            lblIssuingBankAddress2_742.Visible = true;
            lblIssuingBankAddress3_742.Visible = true;
            txtIssuingBankAddress1_742.Visible = true;
            txtIssuingBankAddress2_742.Visible = true;
            txtIssuingBankAddress3_742.Visible = true;
        }
    }

    protected void ddlTotalAmtclamd_742_TextChanged(object sender, EventArgs e)
    {
        if (ddlTotalAmtclamd_742.SelectedValue == "A")
        {
            lbl_742_TotalAmtClmd_Date.Visible = true;
            txt_742_TotalAmtClmd_Date.Visible = true;
            TotalAmtClmd742_Date.Visible = true;
        }
        else
        {
            lbl_742_TotalAmtClmd_Date.Visible = false;
            txt_742_TotalAmtClmd_Date.Visible = false;
            txt_742_TotalAmtClmd_Date.Text = "";
            TotalAmtClmd742_Date.Visible = false;
        }
    }

    protected void ddlBeneficiarybank_742_TextChanged(object sender, EventArgs e)
    {
        if (ddlBeneficiarybank_742.SelectedValue == "A")
        {
            lblBeneficiaryBankIdentifier_742.Text = "Identifier Code: ";
            txtBeneficiaryBankIdentifiercode_742.Visible = true;
            txtBeneficiaryBankName_742.Visible = false;
            txtBeneficiaryBankName_742.Text = "";
            lblBeneficiaryBankAddress1_742.Visible = false;
            lblBeneficiaryBankAddress2_742.Visible = false;
            lblBeneficiaryBankAddress3_742.Visible = false;
            txtBeneficiaryBankAddress1_742.Visible = false;
            txtBeneficiaryBankAddress1_742.Text = "";
            txtBeneficiaryBankAddress2_742.Visible = false;
            txtBeneficiaryBankAddress2_742.Text = "";
            txtBeneficiaryBankAddress3_742.Visible = false;
            txtBeneficiaryBankAddress3_742.Text = "";
        }
        else
        {
            lblBeneficiaryBankIdentifier_742.Text = "Name: ";
            txtBeneficiaryBankIdentifiercode_742.Visible = false;
            txtBeneficiaryBankIdentifiercode_742.Text = "";
            txtBeneficiaryBankName_742.Visible = true;
            lblBeneficiaryBankAddress1_742.Visible = true;
            lblBeneficiaryBankAddress2_742.Visible = true;
            lblBeneficiaryBankAddress3_742.Visible = true;
            txtBeneficiaryBankAddress1_742.Visible = true;
            txtBeneficiaryBankAddress2_742.Visible = true;
            txtBeneficiaryBankAddress3_742.Visible = true;
        }
    }

    protected void ddlAccountwithbank_742_TextChanged(object sender, EventArgs e)
    {
        if (ddlAccountwithbank_742.SelectedValue == "A")
        {
            lblAccountwithBankIdentifier_742.Text = "Identifier Code: ";
            txtAccountwithBankIdentifiercode_742.Visible = true;
            txtAccountwithBankLocation_742.Visible = false;
            txtAccountwithBankLocation_742.Text = "";
            txtAccountwithBankName_742.Visible = false;
            txtAccountwithBankName_742.Text = "";
            lblAccountwithBankAddress1_742.Visible = false;
            lblAccountwithBankAddress2_742.Visible = false;
            lblAccountwithBankAddress3_742.Visible = false;
            txtAccountwithBankAddress1_742.Visible = false;
            txtAccountwithBankAddress1_742.Text = "";
            txtAccountwithBankAddress2_742.Visible = false;
            txtAccountwithBankAddress2_742.Text = "";
            txtAccountwithBankAddress3_742.Visible = false;
            txtAccountwithBankAddress3_742.Text = "";
        }
        else
        {
            if (ddlAccountwithbank_742.SelectedValue == "B")
            {
                lblAccountwithBankIdentifier_742.Text = "Location :";
                txtAccountwithBankIdentifiercode_742.Visible = false;
                txtAccountwithBankIdentifiercode_742.Text = "";
                txtAccountwithBankLocation_742.Visible = true;
                txtAccountwithBankName_742.Visible = false;
                txtAccountwithBankName_742.Text = "";
                lblAccountwithBankAddress1_742.Visible = false;
                lblAccountwithBankAddress2_742.Visible = false;
                lblAccountwithBankAddress3_742.Visible = false;
                txtAccountwithBankAddress1_742.Visible = false;
                txtAccountwithBankAddress1_742.Text = "";
                txtAccountwithBankAddress2_742.Visible = false;
                txtAccountwithBankAddress2_742.Text = "";
                txtAccountwithBankAddress3_742.Visible = false;
                txtAccountwithBankAddress3_742.Text = "";
            }
            else
            {
                lblAccountwithBankIdentifier_742.Text = "Name : ";
                txtAccountwithBankIdentifiercode_742.Visible = false;
                txtAccountwithBankIdentifiercode_742.Text = "";
                txtAccountwithBankLocation_742.Visible = false;
                txtAccountwithBankLocation_742.Text = "";
                txtAccountwithBankName_742.Visible = true;
                lblAccountwithBankAddress1_742.Visible = true;
                lblAccountwithBankAddress2_742.Visible = true;
                lblAccountwithBankAddress3_742.Visible = true;
                txtAccountwithBankAddress1_742.Visible = true;
                txtAccountwithBankAddress2_742.Visible = true;
                txtAccountwithBankAddress3_742.Visible = true;
            }
        }
    }

    protected void ddlTotalAmtclamd_754_TextChanged(object sender, EventArgs e)
    {
        if (ddlTotalAmtclamd_754.SelectedValue == "A")
        {
            lbl_754_TotalAmtClmd_Date.Visible = true;
            txt_754_TotalAmtClmd_Date.Visible = true;
            TotalAmtClmd754_Date.Visible = true;
            txt_754_TotalAmtClmd_Date.Focus();
        }
        else
        {
            lbl_754_TotalAmtClmd_Date.Visible = false;
            txt_754_TotalAmtClmd_Date.Visible = false;
            txt_754_TotalAmtClmd_Date.Text = "";
            TotalAmtClmd754_Date.Visible = false;
            ddl_754_TotalAmtClmd_Ccy.Focus();
        }
    }

    protected void ddlBeneficiarybank_754_TextChanged(object sender, EventArgs e)
    {
        if (ddlBeneficiarybank_754.SelectedValue == "A")
        {
            lblBeneficiaryBankIdentifier_754.Text = "Identifier Code: ";
            txtBeneficiaryBankIdentifiercode_754.Visible = true;
            txtBeneficiaryBankName_754.Visible = false;
            txtBeneficiaryBankName_754.Text = "";
            lblBeneficiaryBankAddress1_754.Visible = false;
            lblBeneficiaryBankAddress2_754.Visible = false;
            lblBeneficiaryBankAddress3_754.Visible = false;
            txtBeneficiaryBankAddress1_754.Visible = false;
            txtBeneficiaryBankAddress1_754.Text = "";
            txtBeneficiaryBankAddress2_754.Visible = false;
            txtBeneficiaryBankAddress2_754.Text = "";
            txtBeneficiaryBankAddress3_754.Visible = false;
            txtBeneficiaryBankAddress3_754.Text = "";
        }
        else
        {
            lblBeneficiaryBankIdentifier_754.Text = "Name: ";
            txtBeneficiaryBankIdentifiercode_754.Visible = false;
            txtBeneficiaryBankIdentifiercode_754.Text = "";
            txtBeneficiaryBankName_754.Visible = true;
            lblBeneficiaryBankAddress1_754.Visible = true;
            lblBeneficiaryBankAddress2_754.Visible = true;
            lblBeneficiaryBankAddress3_754.Visible = true;
            txtBeneficiaryBankAddress1_754.Visible = true;
            txtBeneficiaryBankAddress2_754.Visible = true;
            txtBeneficiaryBankAddress3_754.Visible = true;
        }
    }

    protected void ddlAccountwithbank_754_TextChanged(object sender, EventArgs e)
    {
        if (ddlAccountwithbank_754.SelectedValue == "A")
        {
            lblAccountwithBankIdentifier_754.Text = "Identifier Code: ";
            txtAccountwithBankIdentifiercode_754.Visible = true;
            txtAccountwithBankLocation_754.Visible = false;
            txtAccountwithBankLocation_754.Text = "";
            txtAccountwithBankName_754.Visible = false;
            txtAccountwithBankName_754.Text = "";

            lblAccountwithBankAddress1_754.Visible = false;
            lblAccountwithBankAddress2_754.Visible = false;
            lblAccountwithBankAddress3_754.Visible = false;
            txtAccountwithBankAddress1_754.Visible = false;
            txtAccountwithBankAddress1_754.Text = "";
            txtAccountwithBankAddress2_754.Visible = false;
            txtAccountwithBankAddress2_754.Text = "";
            txtAccountwithBankAddress3_754.Visible = false;
            txtAccountwithBankAddress3_754.Text = "";
        }
        else
        {
            if (ddlAccountwithbank_754.SelectedValue == "B")
            {
                lblAccountwithBankIdentifier_754.Text = "Location :";
                txtAccountwithBankIdentifiercode_754.Visible = false;
                txtAccountwithBankIdentifiercode_754.Text = "";
                txtAccountwithBankLocation_754.Visible = true;
                txtAccountwithBankName_754.Visible = false;
                txtAccountwithBankName_754.Text = "";
                lblAccountwithBankAddress1_754.Visible = false;
                lblAccountwithBankAddress2_754.Visible = false;
                lblAccountwithBankAddress3_754.Visible = false;
                txtAccountwithBankAddress1_754.Visible = false;
                txtAccountwithBankAddress1_754.Text = "";
                txtAccountwithBankAddress2_754.Visible = false;
                txtAccountwithBankAddress2_754.Text = "";
                txtAccountwithBankAddress3_754.Visible = false;
                txtAccountwithBankAddress3_754.Text = "";
            }
            else
            {
                lblAccountwithBankIdentifier_754.Text = "Name : ";
                txtAccountwithBankIdentifiercode_754.Visible = false;
                txtAccountwithBankIdentifiercode_754.Text = "";
                txtAccountwithBankLocation_754.Visible = false;
                txtAccountwithBankLocation_754.Text = "";
                txtAccountwithBankName_754.Visible = true;

                lblAccountwithBankAddress1_754.Visible = true;
                lblAccountwithBankAddress2_754.Visible = true;
                lblAccountwithBankAddress3_754.Visible = true;
                txtAccountwithBankAddress1_754.Visible = true;
                txtAccountwithBankAddress2_754.Visible = true;
                txtAccountwithBankAddress3_754.Visible = true;
            }
        }
    }

    protected void ddlPrinAmtPaidAccNego_754_TextChanged(object sender, EventArgs e)
    {
        if (ddlPrinAmtPaidAccNego_754.SelectedValue == "A")
        {
            lblPrinAmtPaidAccNegoDate_754.Visible = true;
            txtPrinAmtPaidAccNegoDate_754.Visible = true;
            BtnPrinAmtPaidAccNegoDate_754.Visible = true;
        }
        else
        {
            lblPrinAmtPaidAccNegoDate_754.Visible = false;
            txtPrinAmtPaidAccNegoDate_754.Visible = false;
            txtPrinAmtPaidAccNegoDate_754.Text = "";
            BtnPrinAmtPaidAccNegoDate_754.Visible = false;
        }
    }

    protected void ddlReimbursingbank_754_TextChanged(object sender, EventArgs e)
    {
        if (ddlReimbursingbank_754.SelectedValue == "A")
        {
            lblReimbursingBankIdentifier_754.Text = "Identifier Code: ";
            txtReimbursingBankIdentifiercode_754.Visible = true;
            txtReimbursingBankLocation_754.Visible = false;
            txtReimbursingBankLocation_754.Text = "";
            txtReimbursingBankName_754.Visible = false;
            txtReimbursingBankName_754.Text = "";

            lblReimbursingBankAddress1_754.Visible = false;
            lblReimbursingBankAddress2_754.Visible = false;
            lblReimbursingBankAddress3_754.Visible = false;
            txtReimbursingBankAddress1_754.Visible = false;
            txtReimbursingBankAddress1_754.Text = "";
            txtReimbursingBankAddress2_754.Visible = false;
            txtReimbursingBankAddress2_754.Text = "";
            txtReimbursingBankAddress3_754.Visible = false;
            txtReimbursingBankAddress3_754.Text = "";
        }
        else
        {
            if (ddlReimbursingbank_754.SelectedValue == "B")
            {
                lblReimbursingBankIdentifier_754.Text = "Location :";
                txtReimbursingBankIdentifiercode_754.Visible = false;
                txtReimbursingBankIdentifiercode_754.Text = "";
                txtReimbursingBankLocation_754.Visible = true;
                txtReimbursingBankName_754.Visible = false;
                txtReimbursingBankName_754.Text = "";
                lblReimbursingBankAddress1_754.Visible = false;
                lblReimbursingBankAddress2_754.Visible = false;
                lblReimbursingBankAddress3_754.Visible = false;
                txtReimbursingBankAddress1_754.Visible = false;
                txtReimbursingBankAddress1_754.Text = "";
                txtReimbursingBankAddress2_754.Visible = false;
                txtReimbursingBankAddress2_754.Text = "";
                txtReimbursingBankAddress3_754.Visible = false;
                txtReimbursingBankAddress3_754.Text = "";
            }
            else
            {
                lblReimbursingBankIdentifier_754.Text = "Name : ";
                txtReimbursingBankIdentifiercode_754.Visible = false;
                txtReimbursingBankIdentifiercode_754.Text = "";
                txtReimbursingBankLocation_754.Visible = false;
                txtReimbursingBankLocation_754.Text = "";
                txtReimbursingBankName_754.Visible = true;

                lblReimbursingBankAddress1_754.Visible = true;
                lblReimbursingBankAddress2_754.Visible = true;
                lblReimbursingBankAddress3_754.Visible = true;
                txtReimbursingBankAddress1_754.Visible = true;
                txtReimbursingBankAddress2_754.Visible = true;
                txtReimbursingBankAddress3_754.Visible = true;
            }
        }
    }
    protected void InsertDataForSwiftSTP(string SwiftType, string DocNo)
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter P1 = new SqlParameter("@DocNo", DocNo);
        SqlParameter P2 = new SqlParameter("@SwiftType", SwiftType);
        string result = obj.SaveDeleteData("TF_SWIFT_STP_InsertExportData", P1, P2);
    }

}