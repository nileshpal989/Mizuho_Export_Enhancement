using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EXP_EXP_AddEditExportDelinkingEntry : System.Web.UI.Page
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
                txtDocNo.Text = Request.QueryString["DocNo"].ToString();
                fillDetails(txtDocNo.Text);
                txtDateDelinked.Focus();
            }
            txtDelinkedExRt.Attributes.Add("onkeydown", "return validate_Number(event);");
            txtDelinkedExRt.Attributes.Add("onblur", "return Exrate();");
        }

    }
    protected void txtDocNo_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EXP_ViewDelinkingEntry.aspx", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool flag = true;

        if (txtDelinkedExRt.Text.Trim() == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Please Enter Delinked Exchange Rate')", true);
            txtDelinkedExRt.Focus();
            flag = false;
        }

        if (txtDateDelinked.Text.Trim() == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Please Enter Delinked Date')", true);
            txtDateDelinked.Focus();
            flag = false;
        }

        if (flag == true)
        {
            string query = "";
            string _result = "";
            string _script = "";
            SqlParameter p1 = new SqlParameter("@docno", SqlDbType.VarChar);
            p1.Value = txtDocNo.Text;

            SqlParameter p2 = new SqlParameter("@delinkedDt", SqlDbType.VarChar);
            p2.Value = txtDateDelinked.Text;

            SqlParameter p3 = new SqlParameter("@delinkedExRt", SqlDbType.VarChar);
            p3.Value = txtDelinkedExRt.Text;

            SqlParameter p4 = new SqlParameter("@delinkedOvrIntRate", SqlDbType.VarChar);
            p4.Value = txtIntRate.Text;

            SqlParameter p5 = new SqlParameter("@delinkedOvrDays", SqlDbType.VarChar);
            p5.Value = txtDays.Text;

            SqlParameter p6 = new SqlParameter("@delinkedOvrInt", SqlDbType.VarChar);
            p6.Value = txtODInt.Text;

            SqlParameter p7 = new SqlParameter("@delinkedOvrIntRs", SqlDbType.VarChar);
            p7.Value = txtODIntRs.Text;

            SqlParameter p8 = new SqlParameter("@delinkedBalInt", SqlDbType.VarChar);
            p8.Value = txtBalInt.Text;

            SqlParameter p9 = new SqlParameter("@delinkedAmt", SqlDbType.VarChar);
            p9.Value = txtNegoAmtInt.Text;

            SqlParameter p10 = new SqlParameter("@delinkedAmtRs", SqlDbType.VarChar);
            p10.Value = txtNegoAmtRsInt.Text;

            query = "TF_UpdateExportDelinking";

            TF_DATA objSave = new TF_DATA();

            _result = objSave.SaveDeleteData(query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);

            if (_result == "updated")
            {
                _script = "window.location='EXP_ViewDelinkingEntry.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
                labelMessage.Text = _result;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("EXP_ViewDelinkingEntry.aspx", true);
    }

    protected void fillDetails(string docno)
    {
        SqlParameter p1 = new SqlParameter("@docno", SqlDbType.VarChar);
        p1.Value = docno;
        string query = "TF_ExportDelinkingDetails";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(query, p1);
        if (dt.Rows.Count > 0)
        {
            string _strOptSu = "";
            _strOptSu = dt.Rows[0]["Bill_Type"].ToString();
            if (_strOptSu == "U")
            {
                rdbUsance.Checked = true;
                rdbSight.Checked = false;
            }
            else
            {
                rdbUsance.Checked = false;
                rdbSight.Checked = true;
            }

            txtCustAcNo.Text = dt.Rows[0]["CustAcNo"].ToString();
            txtCustAcNo_TextChanged(this, null);

            txtDateRecieved.Text = dt.Rows[0]["Date_received"].ToString();

            txtDateDelinked.Text = dt.Rows[0]["Delinked_Date"].ToString();

            txtOverseasParty.Text = dt.Rows[0]["Overseas_Party_Code"].ToString();
            txtOverseasParty_TextChanged(this, null);

            txtNegoDt.Text = dt.Rows[0]["Date_Negotiated"].ToString();

            txtOverseasBank.Text = dt.Rows[0]["Overseas_Bank_Code"].ToString();
            txtOverseasBank_TextChanged(this, null);

            txtDueDt.Text = dt.Rows[0]["Due_Date"].ToString();
            txtCur.Text = dt.Rows[0]["Currency"].ToString();
            lblCur.Text = dt.Rows[0]["Currency"].ToString();
            txtotherCur.Text = dt.Rows[0]["Other_Currency_For_INR"].ToString();

            if (dt.Rows[0]["Exchange_Rate"].ToString() != "")
            {
                txtExRt.Text = Convert.ToDecimal(dt.Rows[0]["Exchange_Rate"].ToString()).ToString("0.000000");
            }
            else
                txtExRt.Text = dt.Rows[0]["Exchange_Rate"].ToString();

            if (dt.Rows[0]["Bill_Amount"].ToString() != "")
            {
                txtNegoAmt.Text = Convert.ToDecimal(dt.Rows[0]["Bill_Amount"].ToString()).ToString("0.00");
            }
            else
                txtNegoAmt.Text = dt.Rows[0]["Bill_Amount"].ToString();

            if (dt.Rows[0]["Bill_Amount_In_Rs"].ToString() != "")
            {
                txtNegoAmtRs.Text = Convert.ToDecimal(dt.Rows[0]["Bill_Amount_In_Rs"].ToString()).ToString("0.00");
            }
            else
                txtNegoAmtRs.Text = dt.Rows[0]["Bill_Amount_In_Rs"].ToString();

            if (dt.Rows[0]["ActBillAmt"].ToString() != "")
            {
                txtBillAmt.Text = Convert.ToDecimal(dt.Rows[0]["ActBillAmt"].ToString()).ToString("0.00");
            }
            else
                txtBillAmt.Text = dt.Rows[0]["ActBillAmt"].ToString();

            if (dt.Rows[0]["ActBillAmt_InRs"].ToString() != "")
            {
                txtBillAmtRs.Text = Convert.ToDecimal(dt.Rows[0]["ActBillAmt_InRs"].ToString()).ToString("0.00");
            }
            else
                txtBillAmtRs.Text = dt.Rows[0]["ActBillAmt_InRs"].ToString();


            txtDelinkedExRt.Text = dt.Rows[0]["Delinked_Date"].ToString();
            if (dt.Rows[0]["Delinked_Exchange_Rate"].ToString() != "")
            {
                txtDelinkedExRt.Text = Convert.ToDecimal(dt.Rows[0]["Delinked_Exchange_Rate"].ToString()).ToString("0.000000");
            }
            else
                txtDelinkedExRt.Text = dt.Rows[0]["Delinked_Exchange_Rate"].ToString();

            if (dt.Rows[0]["Delinked_Overdue_Interest_Rate"].ToString() != "")
            {
                txtIntRate.Text = Convert.ToDecimal(dt.Rows[0]["Delinked_Overdue_Interest_Rate"].ToString()).ToString("0.000000");
            }
            else
                txtIntRate.Text = dt.Rows[0]["Delinked_Overdue_Interest_Rate"].ToString();

            txtDays.Text = dt.Rows[0]["Delinked_Overdue_Days"].ToString();

            if (dt.Rows[0]["Bill_Amount"].ToString() != "")
            {
                txtNegoAmtInt.Text = Convert.ToDecimal(dt.Rows[0]["Bill_Amount"].ToString()).ToString("0.00");
            }
            else
                txtNegoAmtInt.Text = dt.Rows[0]["Bill_Amount"].ToString();

            if (dt.Rows[0]["Bill_Amount_In_Rs"].ToString() != "")
            {
                txtNegoAmtRsInt.Text = Convert.ToDecimal(dt.Rows[0]["Bill_Amount_In_Rs"].ToString()).ToString("0.00");
            }
            else
                txtNegoAmtRsInt.Text = dt.Rows[0]["Bill_Amount_In_Rs"].ToString();

            if (dt.Rows[0]["Delinked_Overdue_Interest"].ToString() != "")
            {
                txtODInt.Text = Convert.ToDecimal(dt.Rows[0]["Delinked_Overdue_Interest"].ToString()).ToString("0.00");
            }
            else
                txtODInt.Text = dt.Rows[0]["Delinked_Overdue_Interest"].ToString();

            if (dt.Rows[0]["Delinked_Overdue_Interest_In_Rs"].ToString() != "")
            {
                txtODIntRs.Text = Convert.ToDecimal(dt.Rows[0]["Delinked_Overdue_Interest_In_Rs"].ToString()).ToString("0.00");
            }
            else
                txtODIntRs.Text = dt.Rows[0]["Delinked_Overdue_Interest_In_Rs"].ToString();

            if (dt.Rows[0]["Interest"].ToString() != "")
            {
                txtIAC.Text = Convert.ToDecimal(dt.Rows[0]["Interest"].ToString()).ToString("0.000000");
            }
            else
                txtIAC.Text = dt.Rows[0]["Interest"].ToString();

            if (dt.Rows[0]["Interest_In_Rs"].ToString() != "")
            {
                txtIACRs.Text = Convert.ToDecimal(dt.Rows[0]["Interest_In_Rs"].ToString()).ToString("0.000000");
            }
            else
                txtIACRs.Text = dt.Rows[0]["Interest_In_Rs"].ToString();


            if (dt.Rows[0]["Delinked_Balance_Interest_In_Rs"].ToString() != "")
            {
                txtBalInt.Text = Convert.ToDecimal(dt.Rows[0]["Delinked_Balance_Interest_In_Rs"].ToString()).ToString("0.00");
            }
            else
                txtBalInt.Text = dt.Rows[0]["Delinked_Balance_Interest_In_Rs"].ToString();
        }
    }
    protected void txtCustAcNo_TextChanged(object sender, EventArgs e)
    {
        fillCustomerIdDescription();
    }

    public void fillCustomerIdDescription()
    {
        lblCustname.Text = "";
        string custid = txtCustAcNo.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@custid", SqlDbType.VarChar);
        p1.Value = custid;
        string _query = "TF_rptGetCustomerMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCustname.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
            if (lblCustname.Text.Length > 20)
            {
                lblCustname.ToolTip = lblCustname.Text;
                lblCustname.Text = lblCustname.Text;
                lblCustname.Text = lblCustname.Text.Substring(0, 20) + "...";

            }
        }
        else
        {
            txtCustAcNo.Text = "";
            lblCustname.Text = "";
        }
    }

    protected void txtOverseasParty_TextChanged(object sender, EventArgs e)
    {
        fillOverseasPartyDescription();
    }
    protected void fillOverseasPartyDescription()
    {
        lblOverseasParty.Text = "";
        string over = txtOverseasParty.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@over", SqlDbType.VarChar);
        p1.Value = over;
        string _query = "TF_GetOverseasPartyDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblOverseasParty.Text = dt.Rows[0]["Party_Name"].ToString();
            if (lblOverseasParty.Text.Length > 20)
            {
                lblOverseasParty.ToolTip = lblOverseasParty.Text;
                lblOverseasParty.Text = lblOverseasParty.Text;
                lblOverseasParty.Text = lblOverseasParty.Text.Substring(0, 20) + "...";
            }
        }
        else
        {
            txtOverseasParty.Text = "";
            lblOverseasParty.Text = "";
        }
    }
    protected void txtOverseasBank_TextChanged(object sender, EventArgs e)
    {
        fillOverseasBankDescription();
    }

    protected void fillOverseasBankDescription()
    {
        lblOverseasBank.Text = "";
        string over = txtOverseasBank.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@bankcode", SqlDbType.VarChar);
        p1.Value = over;
        string _query = "TF_GetOverseasBankMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblOverseasBank.Text = dt.Rows[0]["BankName"].ToString();
            if (lblOverseasBank.Text.Length > 20)
            {
                lblOverseasBank.ToolTip = lblOverseasBank.Text;
                lblOverseasBank.Text = lblOverseasBank.Text;
                lblOverseasBank.Text = lblOverseasBank.Text.Substring(0, 20) + "...";
            }
        }
        else
        {
            txtOverseasBank.Text = "";
            lblOverseasBank.Text = "";
        }
    }
    protected void txtDelinkedExRt_TextChanged(object sender, EventArgs e)
    {
        //DateTime dt1;
        //DateTime dt2;

        //dt1 = Convert.ToDateTime(txtDueDt.Text.ToString());
        //dt2 = Convert.ToDateTime(txtDateDelinked.Text.ToString());
        //txtDays.Text = dt2.Subtract(dt1).Days.ToString();

        string d1 = txtDueDt.Text;
        string d2 = txtDateDelinked.Text;

        SqlParameter p3 = new SqlParameter("@dt1", SqlDbType.VarChar);
        p3.Value = d2;

        SqlParameter p4 = new SqlParameter("@dt2", SqlDbType.VarChar);
        p4.Value = d1;


        string query1 = "getdatediff";
        TF_DATA objData1 = new TF_DATA();
        DataTable dt1 = objData1.getData(query1, p3, p4);
        if (dt1.Rows.Count > 0)
        {
            txtDays.Text = dt1.Rows[0]["Diff"].ToString();
        }
        SqlParameter p1 = new SqlParameter("@docno", SqlDbType.VarChar);
        p1.Value = txtDocNo.Text;


        SqlParameter p2 = new SqlParameter("@ovrDays", SqlDbType.VarChar);
        p2.Value = txtDays.Text;

        SqlParameter p5 = new SqlParameter("@ExRt", SqlDbType.VarChar);
        p5.Value = txtDelinkedExRt.Text;

        string query = "TF_Export_Delinked_IntCal";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(query, p1, p2, p5);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["OvrInt"].ToString() != "")
            {
                txtODInt.Text = Convert.ToDecimal(dt.Rows[0]["OvrInt"].ToString()).ToString("0.00");
            }
            else
                txtODInt.Text = dt.Rows[0]["OvrInt"].ToString();

            if (dt.Rows[0]["OvrIntRs"].ToString() != "")
            {
                txtODIntRs.Text = Convert.ToDecimal(dt.Rows[0]["OvrIntRs"].ToString()).ToString("0.00");
            }
            else
                txtODIntRs.Text = dt.Rows[0]["OvrIntRs"].ToString();


            if (dt.Rows[0]["OvrDueIntRt"].ToString() != "")
            {
                txtIntRate.Text = Convert.ToDecimal(dt.Rows[0]["OvrDueIntRt"].ToString()).ToString("0.000000");
            }
            else
                txtIntRate.Text = dt.Rows[0]["OvrDueIntRt"].ToString();


            if (dt.Rows[0]["IntActCharged"].ToString() != "")
            {
                txtIAC.Text = Convert.ToDecimal(dt.Rows[0]["IntActCharged"].ToString()).ToString("0.00");
            }
            else
                txtIAC.Text = dt.Rows[0]["IntActCharged"].ToString();

            if (dt.Rows[0]["IntActChargedRs"].ToString() != "")
            {
                txtIACRs.Text = Convert.ToDecimal(dt.Rows[0]["IntActChargedRs"].ToString()).ToString("0.00");
            }
            else
                txtIACRs.Text = dt.Rows[0]["IntActChargedRs"].ToString();

            if (dt.Rows[0]["BalAmt"].ToString() != "")
            {
                txtBalInt.Text = Convert.ToDecimal(dt.Rows[0]["BalAmt"].ToString()).ToString("0.00");
            }
            else
                txtBalInt.Text = dt.Rows[0]["BalAmt"].ToString();
        }
    }

    protected void txtDateDelinked_TextChanged(object sender, EventArgs e)
    {
        if (txtDelinkedExRt.Text != "")
        {
            //DateTime dt1;
            //DateTime dt2;

            //dt1 = Convert.ToDateTime(txtDueDt.Text.ToString());
            //dt2 = Convert.ToDateTime(txtDateDelinked.Text.ToString());
            //txtDays.Text = dt2.Subtract(dt1).Days.ToString();
            string d1 = txtDueDt.Text ;
            string d2 = txtDateDelinked.Text;

            SqlParameter p3 = new SqlParameter("@dt1", SqlDbType.VarChar);
            p3.Value = d2;

            SqlParameter p4 = new SqlParameter("@dt2", SqlDbType.VarChar);
            p4.Value = d1;


            string query1 = "getdatediff";
            TF_DATA objData1 = new TF_DATA();
            DataTable dt1 = objData1.getData(query1, p3, p4);
            if (dt1.Rows.Count > 0)
            {
                txtDays.Text = dt1.Rows[0]["Diff"].ToString();
            }

            SqlParameter p1 = new SqlParameter("@docno", SqlDbType.VarChar);
            p1.Value = txtDocNo.Text;


            SqlParameter p2 = new SqlParameter("@ovrDays", SqlDbType.VarChar);
            p2.Value = txtDays.Text;


            SqlParameter p5 = new SqlParameter("@ExRt", SqlDbType.VarChar);
            p5.Value = txtDelinkedExRt.Text;
            
            string query = "TF_Export_Delinked_IntCal";
            TF_DATA objData = new TF_DATA();
            DataTable dt = objData.getData(query, p1, p2,p5);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["OvrInt"].ToString() != "")
                {
                    txtODInt.Text = Convert.ToDecimal(dt.Rows[0]["OvrInt"].ToString()).ToString("0.00");
                }
                else
                    txtODInt.Text = dt.Rows[0]["OvrInt"].ToString();

                if (dt.Rows[0]["OvrIntRs"].ToString() != "")
                {
                    txtODIntRs.Text = Convert.ToDecimal(dt.Rows[0]["OvrIntRs"].ToString()).ToString("0.00");
                }
                else
                    txtODIntRs.Text = dt.Rows[0]["OvrIntRs"].ToString();


                if (dt.Rows[0]["OvrDueIntRt"].ToString() != "")
                {
                    txtIntRate.Text = Convert.ToDecimal(dt.Rows[0]["OvrDueIntRt"].ToString()).ToString("0.000000");
                }
                else
                    txtIntRate.Text = dt.Rows[0]["OvrDueIntRt"].ToString();


                if (dt.Rows[0]["IntActCharged"].ToString() != "")
                {
                    txtIAC.Text = Convert.ToDecimal(dt.Rows[0]["IntActCharged"].ToString()).ToString("0.00");
                }
                else
                    txtIAC.Text = dt.Rows[0]["IntActCharged"].ToString();

                if (dt.Rows[0]["IntActChargedRs"].ToString() != "")
                {
                    txtIACRs.Text = Convert.ToDecimal(dt.Rows[0]["IntActChargedRs"].ToString()).ToString("0.00");
                }
                else
                    txtIACRs.Text = dt.Rows[0]["IntActChargedRs"].ToString();

                if (dt.Rows[0]["BalAmt"].ToString() != "")
                {
                    txtBalInt.Text = Convert.ToDecimal(dt.Rows[0]["BalAmt"].ToString()).ToString("0.00");
                }
                else
                    txtBalInt.Text = dt.Rows[0]["BalAmt"].ToString();
            }
        }
    }
}