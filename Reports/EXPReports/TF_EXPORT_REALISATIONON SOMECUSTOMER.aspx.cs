using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Drawing;
public partial class Reports_EXPReports_TF_EXPORT_REALISATIONON_SOMECUSTOMER : System.Web.UI.Page
{
    string cust;

    protected void Page_Load(object sender, EventArgs e)
    {
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
                    clearControls();
                    fillddlBranch();

                    ddlBranch.SelectedValue = Session["userADCode"].ToString();
                    ddlBranch.Enabled = false;
                    txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

                    //txtFromDate.Attributes.Add("onblur", "return LastDay();");
                    ddlBranch.Focus();
                }
                btnCreate.Attributes.Add("onclick", "return validateSave();");
                BtnCustList.Attributes.Add("onclick", "return Custhelp();");
                txtCustomer.Attributes.Add("onkeydown", "return CustId(event);");
            }

        }
    }

    protected void clearControls()
    {

        labelMessage.Text = "";

        ddlBranch.Focus();
        rdbAllCustomer.Focus();


    }

    //protected void fillBranch()
    //{
    //    TF_DATA objData = new TF_DATA();
    //    SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
    //    p1.Value = "";
    //    string _query = "TF_GetBranchDetails";
    //    DataTable dt = objData.getData(_query, p1);
    //    ddlBranch.Items.Clear();
    //    ListItem li = new ListItem();
    //    li.Value = "0";
    //    if (dt.Rows.Count > 0)
    //    {
    //        li.Text = "---Select---";
    //        ddlBranch.DataSource = dt.DefaultView;
    //        ddlBranch.DataTextField = "BranchName";
    //        ddlBranch.DataValueField = "BranchName";
    //        ddlBranch.DataBind();
    //    }
    //    else
    //        li.Text = "No record(s) found";
    //    ddlBranch.Items.Insert(0, "All Branches");
    //    ddlBranch.Items.Insert(0, li);
    //}

    public void fillddlBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "";
        ListItem li01 = new ListItem();
        li01.Value = "All Branches";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            li01.Text = "All Branches";

            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
        ddlBranch.Items.Insert(1, li01);
    }



    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";
        DateTime today = Convert.ToDateTime(txtFromDate.Text, dateInfo);

        today = today.AddMonths(1);

        DateTime lastday = today.AddDays(-(today.Day));

        txtToDate.Text = lastday.ToString("dd/MM/yyyy");
        txtToDate.Focus();
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Focus();
    }


    public void fillCustomerIdDescription()
    {
        lblCustomerName.Text = "";
        string custid = txtCustomer.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@custid", SqlDbType.VarChar);
        p1.Value = custid;
        string _query = "TF_rptGGGetCustomerMasterDetails1";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCustomerName.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
        }
        else
        {
            txtCustomer.Text = "";
            lblCustomerName.Text = "";
        }
    }


    protected void rdbAllCustomer_CheckedChanged(object sender, EventArgs e)
    {
        rdbAllCustomer.Checked = true;
        CustList.Visible = false;
        rdbAllCustomer.Focus();


    }
    protected void rdbSelectedCustomer_CheckedChanged(object sender, EventArgs e)
    {
        CustList.Visible = true;
        txtCustomer.Text = "";
        lblCustomerName.Text = "Customer Name";
        txtCustomer.Visible = true;
        BtnCustList.Visible = true;
        lblCustomerName.Visible = true;
        rdbSelectedCustomer.Focus();


    }

    protected void txtCustomer_TextChanged(object sender, EventArgs e)
    {
        fillCustomerIdDescription();
        txtCustomer.Focus();
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";

        if (rdbAllCustomer.Checked == true)
        {
            cust = "All";
        }
        if (rdbSelectedCustomer.Checked == true)
        {
            cust = txtCustomer.Text.Trim();
        }
        DateTime documentDate = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo);
        DateTime documentDate1 = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);


        string _directoryPath = ddlBranch.Text.ToString() + "-" + documentDate.ToString("ddMMyyyy") + "-" + documentDate1.ToString("ddMMyyyy") + "-" + txtCustomer.Text.ToString();
        _directoryPath = Server.MapPath("~/GeneratedFiles/EXPORT/Realisation/" + _directoryPath);



        //if (!Directory.Exists(_directoryPath))
        //{
        //    Directory.CreateDirectory(_directoryPath);
        //}
        //For csv file creation
        TF_DATA objData = new TF_DATA();


        SqlParameter p1 = new SqlParameter("@Branch", SqlDbType.VarChar);
        p1.Value = ddlBranch.Text.ToString().Trim();

        SqlParameter p2 = new SqlParameter("@startdate", SqlDbType.VarChar);
        p2.Value = documentDate.ToString("MM/dd/yyyy");

        SqlParameter p3 = new SqlParameter("@enddate", SqlDbType.VarChar);
        p3.Value = documentDate1.ToString("MM/dd/yyyy");


        SqlParameter p4 = new SqlParameter("@cust", SqlDbType.VarChar);
        p4.Value = cust;

        string _qry = "TF_EXP_REALISATION_SOMECUSTOMER";
        DataTable dt = objData.getData(_qry, p1, p2, p3, p4);
        if (dt.Rows.Count > 0)
        {
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _filePath = _directoryPath + "/" + ddlBranch.Text.ToString() + documentDate.ToString("dd-MM-yyyy") + "-" + documentDate1.ToString("dd-MM-yyyy") + ".csv";
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            sw.WriteLine("ACCOUNTNO,DOC_NO,DOC_DATE,OVERSEAS_PARTY_NAME,INVOICE_NO,CURR,BILL_AMOUNT,REALISED_AMOUNT,REALISED_DATE,Ex.Rate ,INR_EQUIVALENT,COMMISSION,COURIER_CH,OTHER CHGS,STAX,TOTAL_DEBIT");
            for (int j = 0; j < dt.Rows.Count; j++)
            {

                string _strBranchCode5 = "'"+dt.Rows[j]["CustAcNo"].ToString().Trim();
                string _strBranchCode6 = _strBranchCode5.Replace(",", " -");
                sw.Write(_strBranchCode6 + ",");


                /*1*/
                string _strBranchCode = dt.Rows[j]["Document_No"].ToString().Trim();
                string _strBranchCode1 = _strBranchCode.Replace(",", " -");
                sw.Write(_strBranchCode1 + ",");

                /*2*/
                string _strDocument_No = dt.Rows[j]["DOC_DATE"].ToString().Trim();
                string _strDocument_No1 = _strDocument_No.Replace(",", " -");
                sw.Write(_strDocument_No1 + ",");

             



                string _strDocument_Type4 = dt.Rows[j]["Party_Name"].ToString().Trim();
                string _strDocument_Type5 = _strDocument_Type4.Replace(",", " -");
                sw.Write(_strDocument_Type5 + ",");



                string _strDocument_Type6 = dt.Rows[j]["Invoice_No"].ToString().Trim();
                string _strDocument_Type7 = _strDocument_Type6.Replace(",", " -");
                sw.Write(_strDocument_Type7 + ",");



                string _strDocument_Type10 = dt.Rows[j]["Currency"].ToString().Trim();
                string _strDocument_Type11 = _strDocument_Type10.Replace(",", " -");
                sw.Write(_strDocument_Type10 + ",");


                string _strDocument_Type8 = dt.Rows[j]["Bill_Amount"].ToString().Trim();
                string _strDocument_Type9 = _strDocument_Type8.Replace(",", " -");
                sw.Write(_strDocument_Type9 + ",");


                /*7*/
                string _strAc_with_Institution_Id = dt.Rows[j]["Realised_Amount"].ToString().Trim();
                string _strAc_with_Institution_Id1 = _strAc_with_Institution_Id.Replace(",", " -");
                sw.Write(_strAc_with_Institution_Id1 + ",");


                /*8*/
                string _strCountry = dt.Rows[j]["Realised_Date"].ToString().Trim();
                string _strCountry1 = _strCountry.Replace(",", " -");
                sw.Write(_strCountry1 + ",");



                string _strCountry4 = dt.Rows[j]["Realised_Exchange_Rate"].ToString().Trim();
                string _strCountry5 = _strCountry4.Replace(",", " -");
                sw.Write(_strCountry5 + ",");



                /*9*/
                string _strCurrency = dt.Rows[j]["Realised_Amount_In_Rs"].ToString().Trim();
                string _strCurrency1 = _strCurrency.Replace(",", " -");
                sw.Write(_strCurrency1 + ",");

            

                /*12*/
                string _strEEFC_Exchange_Rate = dt.Rows[j]["Realised_Commission"].ToString().Trim();
                string _strEEFC_Exchange_Rate1 = _strEEFC_Exchange_Rate.Replace(",", " -");
                sw.Write(_strEEFC_Exchange_Rate1 + ",");


                /*10*/
                string _strAmount = dt.Rows[j]["Realised_Courier_Charges"].ToString().Trim();
                string _strAmount1 = _strAmount.Replace(",", " -");
                sw.Write(_strAmount1 + ",");

                /*11*/
                string _strEEFC_Currency = dt.Rows[j]["Realised_Other_Charges"].ToString().Trim();
                string _strEEFC_Currency1 = _strEEFC_Currency.Replace(",", " -");
                sw.Write(_strEEFC_Currency1 + ",");





                /*13*/
                string _strAmount_in_EEFC_AC = dt.Rows[j]["STaxAmt"].ToString().Trim();
                string _strAmount_in_EEFC_AC1 = _strAmount_in_EEFC_AC.Replace(",", " -");
                sw.Write(_strAmount_in_EEFC_AC1 + ",");


                /*14*/
                string _strBalanceAmt = dt.Rows[j]["TOTAL_AMOUNT"].ToString().Trim();
                string _strBalanceAmt1 = _strBalanceAmt.Replace(",", " -");
                sw.WriteLine(_strBalanceAmt1);


                ///*15*/
                //string _strExchange_Rate = dt.Rows[j]["Realised_Bank_Cert"].ToString().Trim();
                //string _strExchange_Rate1 = _strExchange_Rate.Replace(",", " -");
                //sw.Write(_strExchange_Rate1 + ",");


                ///*16*/
                //string _strAmount_In_INR = dt.Rows[j]["Realised_Negotiation_Fees"].ToString().Trim();
                //string _strAmount_In_INR1 = _strAmount_In_INR.Replace(",", " -");
                //sw.Write(_strAmount_In_INR1 + ",");

                ///*17*/
                //string _strDraft_Commission_Rate2 = dt.Rows[j]["Realised_Courier_Charges"].ToString().Trim();
                //string _strDraft_Commission_Rate3 = _strDraft_Commission_Rate2.Replace(",", " -");
                //sw.Write(_strDraft_Commission_Rate3 + ",");

                ///*18*/
                //string _strDraft_Commission_Rate = dt.Rows[j]["Realised_OBC"].ToString().Trim();
                //string _strDraft_Commission_Rate1 = _strDraft_Commission_Rate.Replace(",", " -");
                //sw.Write(_strDraft_Commission_Rate1 + ",");

                ///*19*/
                //string _strDraft_Commission = dt.Rows[j]["Realised_OBC_In_Rs"].ToString().Trim();
                //string _strDraft_Commission1 = _strDraft_Commission.Replace(",", " -");
                //sw.Write(_strDraft_Commission1 + ",");

                ///*20*/
                //string _strTelex_Charges = dt.Rows[j]["Realised_Interest_Rate_1"].ToString().Trim();
                //string _strTelex_Charges1 = _strTelex_Charges.Replace(",", " -");
                //sw.Write(_strTelex_Charges1 + ",");

                ///*21*/
                //string _strCommission_Rate = dt.Rows[j]["Realised_For_Days_1"].ToString().Trim();
                //string _strCommission_Rate1 = _strCommission_Rate.Replace(",", " -");
                //sw.Write(_strCommission_Rate1 + ",");

                ///*22*/
                //string _strCommission_Charges = dt.Rows[j]["Realised_Interest_Rate_2"].ToString().Trim();
                //string _strCommission_Charges1 = _strCommission_Charges.Replace(",", " -");
                //sw.Write(_strCommission_Charges1 + ",");

                ///*23*/
                //string _strEEFC_Commission = dt.Rows[j]["Realised_For_Days_2"].ToString().Trim();
                //string _strEEFC_Commission1 = _strEEFC_Commission.Replace(",", " -");
                //sw.Write(_strEEFC_Commission1 + ",");

                ///*24*/
                //string _strEEFC_Commission_Rate = dt.Rows[j]["Realised_Overdue_Interest"].ToString().Trim();
                //string _strEEFC_Commission_Rate1 = _strEEFC_Commission_Rate.Replace(",", " -");
                //sw.Write(_strEEFC_Commission_Rate1 + ",");

                ///*25*/
                //string _strOther_Charges = dt.Rows[j]["Realised_Refund_Interest"].ToString().Trim();
                //string _strOther_Charges1 = _strOther_Charges.Replace(",", " -");
                //sw.Write(_strOther_Charges1 + ",");

                ///*26*/
                //string _strTotal_Amount = dt.Rows[j]["Realised_Overdue_Interest_In_Rs"].ToString().Trim();
                //string _strTotal_Amount1 = _strTotal_Amount.Replace(",", " -");
                //sw.Write(_strTotal_Amount1 + ",");

                ///*27*/
                //string _strInvoice_No = dt.Rows[j]["Realised_Refund_Interest_In_Rs"].ToString().Trim();
                //string _strInvoice_No1 = _strInvoice_No.Replace(",", " -");
                //sw.Write(_strInvoice_No1 + ",");

                ///*28*/
                //string _strDetails_of_Payment = dt.Rows[j]["Realised_Commission"].ToString().Trim();
                //string _strDetails_of_Payment1 = _strDetails_of_Payment.Replace(",", " - ");
                //sw.Write(_strDetails_of_Payment1 + ",");

                ///*29*/
                //string _strDetails_Of_Charges = dt.Rows[j]["Realised_Commission_Rate"].ToString().Trim();
                //string _strDetails_Of_Charges1 = _strDetails_Of_Charges.Replace(",", " -");
                //sw.Write(_strDetails_Of_Charges1 + ",");

                ///*30*/
                //string _strDetails_Of_Charges_Indication = dt.Rows[j]["Realised_Current_Account_FC"].ToString().Trim();
                //string _strDetails_Of_Charges_Indication1 = _strDetails_Of_Charges_Indication.Replace(",", " -");
                //sw.Write(_strDetails_Of_Charges_Indication1 + ",");

                ///*31*/
                //string _strReceiver_Correspondent = dt.Rows[j]["Realised_Current_Account_FC_IN_INR"].ToString().Trim();
                //string _strReceiver_Correspondent1 = _strReceiver_Correspondent.Replace(",", " -");
                //sw.Write(_strReceiver_Correspondent1 + ",");

                ///*32*/
                //string _strSender_to_Receiver_Information = dt.Rows[j]["Realised_Current_Account_Amount_In_Rs"].ToString().Trim();
                //string _strSender_to_Receiver_Information1 = _strSender_to_Receiver_Information.Replace(",", " -");
                //sw.Write(_strSender_to_Receiver_Information1 + ",");

                ///*33*/
                //string _strRemarks = dt.Rows[j]["Realised_Current_Account_USD"].ToString().Trim();
                //string _strRemarks1 = _strRemarks.Replace(",", " -");
                //sw.Write(_strRemarks1 + ",");

                ///*34*/
                //string _strPurpose_Code = dt.Rows[j]["Realised_Current_Account_USD_IN_INR"].ToString().Trim();
                //string _strPurpose_Code1 = _strPurpose_Code.Replace(",", " -");
                //sw.Write(_strPurpose_Code1 + ",");

                ///*35*/
                //string _strDraft_No = dt.Rows[j]["Realised_Remarks"].ToString().Trim();
                //string _strDraft_No1 = _strDraft_No.Replace(",", " -");
                //sw.Write(_strDraft_No1 + ",");

                ///*36*/
                //string _strDraft_Date = dt.Rows[j]["Realised_NY_Ref_No"].ToString().Trim();
                //string _strDraft_Date1 = _strDraft_Date.Replace(",", " -");
                //sw.Write(_strDraft_Date1 + ",");

                ///*37*/
                //string _strValue_Date = dt.Rows[j]["Realised_Payment_Indication"].ToString().Trim();
                //string _strValue_Date1 = _strValue_Date.Replace(",", " -");
                //sw.Write(_strValue_Date1 + ",");

                ///*38*/
                //string _strMT_100_Indication = dt.Rows[j]["SR_NO"].ToString().Trim();
                //string _strMT_100_Indication1 = _strMT_100_Indication.Replace(",", " -");
                //sw.Write(_strMT_100_Indication1 + ",");

                ///*39*/
                //string _strMT_202_Indication = dt.Rows[j]["BalAmtRealised"].ToString().Trim();
                //string _strMT_202_Indication1 = _strMT_202_Indication.Replace(",", " -");
                //sw.Write(_strMT_202_Indication1 + ",");

                ///*40*/
                //string _strMT_110_Indication = dt.Rows[j]["BalAmtRealisedINR"].ToString().Trim();
                //string _strMT_110_Indication1 = _strMT_110_Indication.Replace(",", " -");
                //sw.Write(_strMT_110_Indication1 + ",");

                ///*41*/
                //string _strBeneficiaryAcInd = dt.Rows[j]["STax"].ToString().Trim();
                //string _strBeneficiaryAcInd1 = _strBeneficiaryAcInd.Replace(",", " -");
                //sw.Write(_strBeneficiaryAcInd1 + ",");

                ///*42*/
                //string _strdoc_No = dt.Rows[j]["STaxAmt"].ToString().Trim();
                //string _strdoc_No1 = _strdoc_No.Replace(",", " -");
                //sw.Write(_strdoc_No1 + ",");

                ///*43*/
                //string _strSender_Correspondent = dt.Rows[j]["TTREFNO"].ToString().Trim();
                //string _strSender_Correspondent1 = _strSender_Correspondent.Replace(",", " -");
                //sw.Write(_strSender_Correspondent1 + ",");

                ///*44*/
                //string _strIntermediaryAcInd = dt.Rows[j]["REALISED_FXDLS"].ToString().Trim();
                //string _strIntermediaryAcInd1 = _strIntermediaryAcInd.Replace(",", " -");Party_Name
                //sw.Write(_strIntermediaryAcInd1 + ",");

                ///*45*/
                //string _strInstituteAcInd = dt.Rows[j]["Loan"].ToString().Trim();
                //string _strInstituteAcInd1 = _strInstituteAcInd.Replace(",", " -");
                //sw.Write(_strInstituteAcInd1 + ",");

                ///*46*/
                //string _strBeneficiary = dt.Rows[j]["BankLine"].ToString().Trim();
                //string _strBeneficiary1 = _strBeneficiary.Replace(",", " -");
                //sw.Write(_strBeneficiary1 + ",");

                ///*47*/
                //string _strBeneficiaryBankId = dt.Rows[j]["Trans_Type"].ToString().Trim();
                //string _strBeneficiaryBankId1 = _strBeneficiaryBankId.Replace(",", " -");
                //sw.Write(_strBeneficiaryBankId1 + ",");

                ///*48*/
                //string _strAccountType = dt.Rows[j]["EEFC_Currency"].ToString().Trim();
                //string _strAccountType1 = _strAccountType.Replace(",", " -");
                //sw.Write(_strAccountType1 + ",");

                ///*49*/
                //string _strSTaxPer = dt.Rows[j]["EEFC_Exchange_Rate"].ToString().Trim();
                //string _strSTaxPer1 = _strSTaxPer.Replace(",", " -");
                //sw.Write(_strSTaxPer1 + ",");

                ///*50*/
                //string _strSTaxAmt = dt.Rows[j]["BalanceAmt"].ToString().Trim();
                //string _strSTaxAmt1 = _strSTaxAmt.Replace(",", " -");
                //sw.Write(_strSTaxAmt1 + ",");

                ///*51*/
                //string _strSTax_FXDLS = dt.Rows[j]["BalanceAmtinINR"].ToString().Trim();
                //string _strSTax_FXDLS1 = _strSTax_FXDLS.Replace(",", " -");
                //sw.Write(_strSTax_FXDLS1 + ",");

                ///*52*/
                //string _strTrans_Type = dt.Rows[j]["CrossAmt"].ToString().Trim();
                //string _strTrans_Type1 = _strTrans_Type.Replace(",", " -");
                //sw.Write(_strTrans_Type1 + ",");

                ///*53*/
                //string _strCrossAmt = dt.Rows[j]["ForwardContractNo"].ToString().Trim();
                //string _strCrossAmt1 = _strCrossAmt.Replace(",", " -");
                //sw.Write(_strCrossAmt1 + ",");

                ///*54*/
                //string _strSwiftCharges = dt.Rows[j]["AccountType"].ToString().Trim();
                //string _strSwiftCharges1 = _strSwiftCharges.Replace(",", " -");
                //sw.Write(_strSwiftCharges1 + ",");

                ///*55*/
                //string _strForwardContractNo = dt.Rows[j]["BalanceAmtforPaymentType"].ToString().Trim();
                //string _strForwardContractNo1 = _strForwardContractNo.Replace(",", " -");
                //sw.Write(_strForwardContractNo1 + ",");

                ///*56*/
                //string _straddedby = dt.Rows[j]["Addedby"].ToString().Trim();
                //string _straddedby1 = _straddedby.Replace(",", " -");
                //sw.Write(_straddedby1 + ",");

                ///*57*/
                //string _straddedDate = dt.Rows[j]["Addeddate"].ToString().Trim();
                //string _straddedDate1 = _straddedDate.Replace(",", " -");
                //sw.Write(_straddedDate1 + ",");

                ///*58*/
                //string _strUpdatedby = dt.Rows[j]["Updatedby"].ToString().Trim();
                //string _strUpdatedby1 = _strUpdatedby.Replace(",", " -");
                //sw.Write(_strUpdatedby1 + ",");

                ///*59*/
                //string _strUpdatedDate = dt.Rows[j]["UpdatedDate"].ToString().Trim();
                //string _strUpdatedDate1 = _strUpdatedDate.Replace(",", " -");
                //sw.WriteLine(_strUpdatedDate1);

            }
            sw.Flush();
            sw.Close();
            sw.Dispose();

            TF_DATA objServerName = new TF_DATA();
            string _serverName = objServerName.GetServerName();


            string path = "file://" + _serverName + "/GeneratedFiles/EXPORT/Realisation";
            string link = "/GeneratedFiles/EXPORT/Realisation";
            labelMessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";

            //string path = _serverName + "/GeneratedFiles/EXPORT/Realisation" + ddlBranch.Text.ToString() + "-" + documentDate.ToString("ddMMyyyy") + "-" + documentDate1.ToString("ddMMyyyy");
            //string link = "/GeneratedFiles/EXPORT/Realisation" + ddlBranch.Text.ToString() + "-" + documentDate.ToString("ddMMyyyy") + "-" + documentDate1.ToString("ddMMyyyy");
            //labelMessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
            //labelMessage.Text = "Files Created Successfully on Server : " + _serverName + " in " + _directoryPath;
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Files Created Successfully');", true);
            ddlBranch.Focus();
            //txtToDate.Text = "";
            //txtFromDate.Text = "";

        }
        else
        {
            //labelMessage.Text = "No Reocrds ";
            labelMessage.Text = "There is No Record Between This Dates " + documentDate.ToString("dd/MM/yyyy") + "-" + documentDate1.ToString("dd/MM/yyyy");
            //ddlBranch.Focus();
            txtCustomer.Text = "";
            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtFromDate.Focus();
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('There is No Record Between This Dates');", true);
        }

    }

    protected void btnCustClick_Click(object sender, EventArgs e)
    {
        fillCustomerIdDescription();
    }
}





