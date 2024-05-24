using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class XOS_XOS_AddEditWriteOffBillEntry : System.Web.UI.Page
{
    string branchcode = "";
    string year = "";
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
                fillTaxRates();
                txtBillNo.Text = Request.QueryString["DocNo"].ToString();
                txtDocumentNo.Text = Request.QueryString["DocumentNo"].ToString();
                year = Request.QueryString["year"].ToString();
                fillBillDetails(txtBillNo.Text);
                getLastWriteOffNumber();

                txtDocDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtDocDate.Focus();
                btnDocNo.Attributes.Add("onclick", "return OpenExtensionNoList();");
                txtWriteOffNo.Attributes.Add("onblur", "return onBlurExtensionNo();");
                hdnMode.Value = "add";
            }
        }
        txtDocDate.Attributes.Add("onblur", "return checkSysDate();");
        txtWOAmt.Attributes.Add("onblur", "return chkWriteOffAmt();");
        txtWriteOffCharges.Attributes.Add("onblur", "return calTax();");
        txtCourier.Attributes.Add("onblur", "return calTax();");
        txtOtherCharges.Attributes.Add("onblur", "return calTax();");
        txtCommission.Attributes.Add("onblur", "return calTax();");
        txtExchRate.Attributes.Add("onblur", "return chkExrate();");
        txtWOAmt.Attributes.Add("onkeydown", "return validate_Number(event);");
        txtExchRate.Attributes.Add("onkeydown", "return validate_Number(event);");
        txtWriteOffCharges.Attributes.Add("onkeydown", "return validate_Number(event);");
        txtCourier.Attributes.Add("onkeydown", "return validate_Number(event);");
        txtOtherCharges.Attributes.Add("onkeydown", "return validate_Number(event);");
        txtCommission.Attributes.Add("onkeydown", "return validate_Number(event);");
        txtWriteOffNo.Attributes.Add("onkeydown", "return validate_Number(event);");
        txtaggregatepercent.Attributes.Add("onkeydown", "return validate_Number(event);");
        txtReasonWriteOff.Attributes.Add("onkeyup", "return CharCount(event," + txtReasonWriteOff.ClientID + "," + lblCharCountReason.ClientID + ",200 );");
        txtAllowedCriteria.Attributes.Add("onkeyup", "return CharCount(event," + txtAllowedCriteria.ClientID + "," + lblCharCountCriteria.ClientID + ",200 );");
        txtRemarks.Attributes.Add("onkeyup", "return CharCount(event," + txtRemarks.ClientID + "," + lblCharCountRemarks.ClientID + ",100 );");
    }

    protected void fillTaxRates()
    {
        TF_DATA objData = new TF_DATA();
        string _query = "TF_GetTaxRates_INW";

        ddlServicetax.Items.Clear();

        DataTable dt = objData.getData(_query);

        if (dt.Rows.Count > 0)
        {
            ddlServicetax.DataSource = dt.DefaultView;
            ddlServicetax.DataTextField = "TOTAL_SERVICE_TAX";
            ddlServicetax.DataValueField = "TOTAL_SERVICE_TAX";
            ddlServicetax.DataBind();
        }
    }

    protected void fillBillDetails(string billno)
    {
        string query = "TF_EXP_XOS_WriteOffDetials";
        SqlParameter p1 = new SqlParameter("@billno", SqlDbType.VarChar);
        p1.Value = billno;
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(query, p1);
        if (dt.Rows.Count > 0)
        {
            txtBillDate.Text = dt.Rows[0]["Date_Negotiated"].ToString();
            txtCustAcNo.Text = dt.Rows[0]["CustAcNo"].ToString();
            fillCustomerIdDescription();
            txtOverseasParty.Text = dt.Rows[0]["Overseas_Party_Code"].ToString();
            fillOverseasPartyDescription();
            txtBillCurr.Text = dt.Rows[0]["Currency"].ToString();
            if (dt.Rows[0]["ActBillAmt"].ToString() != "")
            {
                txtBillAmt.Text = Convert.ToDecimal(dt.Rows[0]["ActBillAmt"].ToString()).ToString("0.00");
            }
            else
                txtBillAmt.Text = dt.Rows[0]["ActBillAmt"].ToString();
            if (dt.Rows[0]["Balance"].ToString() != "")
            {
                txtOSAmt.Text = Convert.ToDecimal(dt.Rows[0]["Balance"].ToString()).ToString("0.00");
            }
            else
                txtOSAmt.Text = dt.Rows[0]["Balance"].ToString();
            txtDueDate.Text = dt.Rows[0]["Due_Date"].ToString();
            txtAWBDate.Text = dt.Rows[0]["AWB_Date"].ToString();
        }
    }

    protected void fillCustomerIdDescription()
    {
        lblCustName.Text = "";
        string custid = txtCustAcNo.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@custid", SqlDbType.VarChar);
        p1.Value = custid;
        string _query = "TF_rptGetCustomerMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCustName.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
            //if (lblCustName.Text.Length > 20)
            //{
            //    lblCustName.ToolTip = lblCustName.Text;
            //    lblCustName.Text = lblCustName.Text;
            //    lblCustName.Text = lblCustName.Text.Substring(0, 16) + "...";
            //}
        }
        else
        {
            txtCustAcNo.Text = "";
            lblCustName.Text = "INVALID ID";
        }
    }

    protected void fillOverseasPartyDescription()
    {
        lblOverseasPartyName.Text = "";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@partyID", SqlDbType.VarChar);
        p1.Value = txtOverseasParty.Text;
        SqlParameter p2 = new SqlParameter("@type", SqlDbType.VarChar);
        p2.Value = "EXPORT";
        string _query = "TF_GetOverseasPartyMasterDetails";
        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            lblOverseasPartyName.Text = dt.Rows[0]["Party_Name"].ToString().Trim();
            //if (lblOverseasPartyName.Text.Length > 20)
            //{
            //    lblOverseasPartyName.ToolTip = lblOverseasPartyName.Text;
            //    lblOverseasPartyName.Text = lblOverseasPartyName.Text.Substring(0, 16) + "...";
            //}
        }
        else
        {
            txtOverseasParty.Text = "";
            lblOverseasPartyName.Text = "";
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("XOS_ViewWriteOffEntry.aspx", true);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("XOS_ViewWriteOffEntry.aspx", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool flag = true;
        if (txtDocDate.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Doc Date cannot be blank');", true);
            txtDocDate.Focus();
            flag = false;
        }
        if (txtWOAmt.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('W/O amount cannot be blank');", true);
            txtWOAmt.Focus();
            flag = false;
        }
        if (txtWriteOffNo.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('W/O Number cannot be blank');", true);
            txtWriteOffNo.Focus();
            flag = false;
        }
        if (flag == true)
        {
            string doc = txtDocumentNo.Text;
            year = Request.QueryString["year"].ToString();
            //year = doc.Substring(doc.Length - 4);
            branchcode = Request.QueryString["BranchCode"].ToString();
            SqlParameter mode = new SqlParameter("@mode", SqlDbType.VarChar);
            mode.Value = hdnMode.Value;
            SqlParameter docyear = new SqlParameter("@year", SqlDbType.VarChar);
            docyear.Value = year;
            SqlParameter bcode = new SqlParameter("@branchcode", SqlDbType.VarChar);
            bcode.Value = branchcode;
            SqlParameter docno = new SqlParameter("@docno", SqlDbType.VarChar);
            docno.Value = txtDocumentNo.Text;
            SqlParameter billno = new SqlParameter("@billno", SqlDbType.VarChar);
            billno.Value = txtBillNo.Text;
            SqlParameter writeoffno = new SqlParameter("@writeoffno", SqlDbType.VarChar);
            writeoffno.Value = txtWriteOffNo.Text;
            SqlParameter writeoffdate = new SqlParameter("@writeoffdate", SqlDbType.VarChar);
            writeoffdate.Value = txtDocDate.Text;
            SqlParameter writeoffga = new SqlParameter("@writeoffga", SqlDbType.VarChar);
            writeoffga.Value = ddlGrantingAuthority.Text;
            SqlParameter writeoffamt = new SqlParameter("@writeoffamt", SqlDbType.VarChar);
            writeoffamt.Value = txtWOAmt.Text;
            SqlParameter reasonofwo = new SqlParameter("@reasonofwo", SqlDbType.VarChar);
            reasonofwo.Value = txtReasonWriteOff.Text;
            SqlParameter aggpercent = new SqlParameter("@aggpercent", SqlDbType.VarChar);
            aggpercent.Value = txtaggregatepercent.Text;
            SqlParameter allowedundercriteria = new SqlParameter("@allowedunderCriteria", SqlDbType.VarChar);
            allowedundercriteria.Value = txtAllowedCriteria.Text;
            string pos = "";
            if (chkProofSurrender.Checked == true)
                pos = "1";
            else
                pos = "0";
            SqlParameter proofofsur = new SqlParameter("@pos", SqlDbType.VarChar);
            proofofsur.Value = pos;
            SqlParameter remarks = new SqlParameter("@remarks", SqlDbType.VarChar);
            remarks.Value = txtRemarks.Text;
            SqlParameter exrate = new SqlParameter("@exrate", SqlDbType.VarChar);
            exrate.Value = txtExchRate.Text;
            SqlParameter wocharges = new SqlParameter("@wocharges", SqlDbType.VarChar);
            wocharges.Value = txtWriteOffCharges.Text;
            SqlParameter comm = new SqlParameter("@comm", SqlDbType.VarChar);
            comm.Value = txtCommission.Text;
            SqlParameter othercharges = new SqlParameter("@othercharges", SqlDbType.VarChar);
            othercharges.Value = txtOtherCharges.Text;
            SqlParameter couriercharges = new SqlParameter("@courier", SqlDbType.VarChar);
            couriercharges.Value = txtCourier.Text;
            SqlParameter stax = new SqlParameter("@stax", SqlDbType.VarChar);
            stax.Value = ddlServicetax.Text;
            SqlParameter staxamt = new SqlParameter("@staxamt", SqlDbType.VarChar);
            staxamt.Value = txtServiceTax.Text;
            SqlParameter totaldebit = new SqlParameter("@totaldebit", SqlDbType.VarChar);
            totaldebit.Value = txtTotalDebit.Text;
            string annex = "";
            if (chkAnnex.Checked == true)
                annex = "1";
            else
                annex = "0";
            SqlParameter annex4 = new SqlParameter("@annex", SqlDbType.VarChar);
            annex4.Value = annex;
            string cacert = "";
            if (chkCACert.Checked == true)
                cacert = "1";
            else
                cacert = "0";
            SqlParameter cacertwo = new SqlParameter("@cacert", SqlDbType.VarChar);
            cacertwo.Value = cacert;
            SqlParameter pRBIAprroval = new SqlParameter("@rbiapproval", SqlDbType.VarChar);
            pRBIAprroval.Value = txtRBIApproval.Text;

            SqlParameter pRBIApprovalDate = new SqlParameter("@rbiapprovaldate", SqlDbType.VarChar);
            pRBIApprovalDate.Value = txtRBIApprovalDate.Text;
            string user = Session["userName"].ToString();
            SqlParameter usernm = new SqlParameter("@usernm", SqlDbType.VarChar);
            usernm.Value = user;
            string time = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            SqlParameter entrytime = new SqlParameter("@entrytime", SqlDbType.VarChar);
            entrytime.Value = time;

            string _result = "";
            string _script = "";
            TF_DATA objSave = new TF_DATA();

            string query = "TF_EXP_XOS_UpdateWriteOffDetails";

            _result = objSave.SaveDeleteData(query, mode, docyear, bcode, docno, billno, writeoffno, writeoffdate, writeoffga, writeoffamt, reasonofwo, aggpercent, allowedundercriteria, proofofsur,
                                            remarks, exrate, wocharges, comm, othercharges, couriercharges, stax, staxamt, totaldebit, annex4, cacertwo, pRBIAprroval, pRBIApprovalDate, usernm, entrytime);

            if (_result.Substring(0, 5) == "added")
            {
                _script = "window.location='XOS_ViewWriteOffEntry.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
            {
                if (_result == "updated")
                {
                    _script = "window.location='XOS_ViewWriteOffEntry.aspx?result=" + _result + "'";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                }
                else
                    labelMessage.Text = _result;
            }
        }
    }

    protected void getLastWriteOffNumber()
    {
        string query = "TF_GetLastWriteOffNumber";
        SqlParameter p1 = new SqlParameter("@docno", SqlDbType.VarChar);
        p1.Value = txtDocumentNo.Text;
        TF_DATA objData=new TF_DATA();
        DataTable dt = objData.getData(query, p1);
        if (dt.Rows.Count > 0)
        {
            txtWriteOffNo.Text = dt.Rows[0]["LastWriteOff"].ToString();
            hdnPrevWriteOffNo.Value = dt.Rows[0]["LastWriteOff"].ToString();
        }
        
    }

    protected void btnExtensionNo_Click(object sender, EventArgs e)
    {
        SqlParameter p1 = new SqlParameter("@docno", SqlDbType.VarChar);
        p1.Value = txtDocumentNo.Text;
        SqlParameter p2 = new SqlParameter("@writeoffno", SqlDbType.VarChar);
        p2.Value = txtWriteOffNo.Text;
        string query = "TF_EXP_XOS_WriteooffEntryDetails";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            hdnMode.Value = "edit";
            txtDocDate.Text = dt.Rows[0]["WriteOffDate"].ToString();
            if (dt.Rows[0]["WrittenOffAmount"].ToString() != "")
            {
                txtWOAmt.Text = Convert.ToDecimal(dt.Rows[0]["WrittenOffAmount"].ToString()).ToString("0.00");
            }
            else
                txtWOAmt.Text = dt.Rows[0]["WrittenOffAmount"].ToString();
            if (dt.Rows[0]["WriteOffGrantingAuthority"].ToString() != "")
                ddlGrantingAuthority.Text = dt.Rows[0]["WriteOffGrantingAuthority"].ToString();
            if (dt.Rows[0]["ExchangeRate"].ToString() != "")
                txtExchRate.Text = Convert.ToDecimal(dt.Rows[0]["ExchangeRate"].ToString()).ToString("0.0000000000");
            else
                txtExchRate.Text = dt.Rows[0]["ExchangeRate"].ToString();
            txtaggregatepercent.Text = dt.Rows[0]["AggregatePercentage"].ToString();
            txtReasonWriteOff.Text = dt.Rows[0]["ReasonForWriteOff"].ToString();
            txtAllowedCriteria.Text = dt.Rows[0]["AllowedUnderCriteria"].ToString();
            txtRemarks.Text = dt.Rows[0]["Remarks"].ToString();
            if (dt.Rows[0]["Annex4received"].ToString() == "True")
                chkAnnex.Checked = true;
            else
                chkAnnex.Checked = false;
            if (dt.Rows[0]["ProofOfSurrender"].ToString() == "True")
                chkAnnex.Checked = true;
            else
                chkAnnex.Checked = false;
            if (dt.Rows[0]["CAcertForWriteOff"].ToString() == "True")
                chkAnnex.Checked = true;
            else
                chkAnnex.Checked = false;
            if (dt.Rows[0]["WriteOffCharges"].ToString() != "")
                txtWriteOffCharges.Text = Convert.ToDecimal(dt.Rows[0]["WriteOffCharges"].ToString()).ToString("0.00");
            else
                txtWriteOffCharges.Text = dt.Rows[0]["WriteOffCharges"].ToString();
            if (dt.Rows[0]["CourierCharges"].ToString() != "")
                txtCourier.Text = Convert.ToDecimal(dt.Rows[0]["CourierCharges"].ToString()).ToString("0.00");
            else
                txtCourier.Text = dt.Rows[0]["CourierCharges"].ToString();
            if (dt.Rows[0]["Commission"].ToString() != "")
                txtCommission.Text = Convert.ToDecimal(dt.Rows[0]["Commission"].ToString()).ToString("0.00");
            else
                txtCommission.Text = dt.Rows[0]["Commission"].ToString();
            if (dt.Rows[0]["OtherCharges"].ToString() != "")
                txtOtherCharges.Text = Convert.ToDecimal(dt.Rows[0]["OtherCharges"].ToString()).ToString("0.00");
            else
                txtOtherCharges.Text = dt.Rows[0]["OtherCharges"].ToString();
            if (dt.Rows[0]["STax"].ToString() != "")
                ddlServicetax.Text = dt.Rows[0]["STax"].ToString();
            if (dt.Rows[0]["STaxAmt"].ToString() != "")
                txtServiceTax.Text = Convert.ToDecimal(dt.Rows[0]["STaxAmt"].ToString()).ToString("0.00");
            else
                txtServiceTax.Text = dt.Rows[0]["STaxAmt"].ToString();
            if (dt.Rows[0]["TotalDebit"].ToString() != "")
                txtTotalDebit.Text = Convert.ToDecimal(dt.Rows[0]["TotalDebit"].ToString()).ToString("0.00");
            else
                txtTotalDebit.Text = dt.Rows[0]["TotalDebit"].ToString();
            txtRBIApproval.Text = dt.Rows[0]["RBIApprovalRefNo"].ToString().Trim();
            txtRBIApprovalDate.Text = dt.Rows[0]["RBIApprovalDate"].ToString().Trim();
        }
        else
            hdnMode.Value = "add";
    }

}