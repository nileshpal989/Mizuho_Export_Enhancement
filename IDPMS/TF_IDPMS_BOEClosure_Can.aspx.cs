using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class IDPMS_TF_IDPMS_BOEClosure_Can : System.Web.UI.Page
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

                if (Session["userRole"].ToString() == "Supervisor")
                {

                    lblSupervisormsg.Visible = true;

                }

                else
                {
                    btnSave.Enabled = true;
                    lblSupervisormsg.Visible = false;
                }

                fillBranch();
                btnboehelp.Attributes.Add("onclick", "return BOECan_Help();");
                btncusthelp.Attributes.Add("onclick", "return Cust_Help();");
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;
            }

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

        if (dt.Rows.Count > 0)
        {

            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else { }
        //else
        //    li.Text = "No record(s) found";

        //ddlBranch.Items.Insert(0, li);

    }
    protected void filldetails(string irmno, string adjrefno)
    {
        //string _script = "";
        string query = "fillBOEclosure";
        TF_DATA objdata = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@BOENO", irmno);
        SqlParameter p2 = new SqlParameter("@adjref", adjrefno);
        DataTable dt = objdata.getData(query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            txt_boeno.Text = dt.Rows[0]["billOfEntryNumber"].ToString();
            // txt_adcode.Text = dt.Rows[0]["ADCode"].ToString();
            txt_iecode.Text = dt.Rows[0]["IECode"].ToString();
            txtprtcd.Text = dt.Rows[0]["portOfDischarge"].ToString();
            txtbilldate.Text = dt.Rows[0]["billOfEntryDate"].ToString();

            txt_adjdate.Text = dt.Rows[0]["adjustmentDate"].ToString();
            ddlreasadj.Text = dt.Rows[0]["adjustmentIndicator"].ToString();
            txt_letterNo.Text = dt.Rows[0]["letterNumber"].ToString();
            txt_letterDate.Text = dt.Rows[0]["letterDate"].ToString();
            ddlapproved.Text = dt.Rows[0]["approvedBy"].ToString();
            txt_docno.Text = dt.Rows[0]["documentNumber"].ToString();
            txt_docdate.Text = dt.Rows[0]["documentDate"].ToString();
            txtdocprt.Text = dt.Rows[0]["documentPort"].ToString();
            //txt_closseqno.Text = dt.Rows[0]["ClosureSeqNo"].ToString();
            ddlboeclose.Text = dt.Rows[0]["closeofBillIndicator"].ToString();
            txt_remarks.Text = dt.Rows[0]["Remark"].ToString();
            txtadjrefno.Text = dt.Rows[0]["adjustmentReferenceNumber"].ToString();
            fillgrid1();
            //GenerateAdjRefNo();
        }


    }

    protected void fillgrid1()
    {
        int _pageSize = 0;
        string search = txt_boeno.Text.Trim();
        SqlParameter p1 = new SqlParameter("@BOENO", SqlDbType.VarChar);
        p1.Value = search;

        SqlParameter p2 = new SqlParameter("@prtcd", SqlDbType.VarChar);
        p2.Value = txtprtcd.Text.Trim();

        //SqlParameter p3 = new SqlParameter("@DocType", SqlDbType.VarChar);
        //p3.Value = txtDocPrFx.Text.Trim();

        SqlParameter p4 = new SqlParameter("@billdate", SqlDbType.VarChar);
        p4.Value = txtbilldate.Text.Trim();

        SqlParameter p5 = new SqlParameter("@adjref", SqlDbType.VarChar);
        p5.Value = txtadjrefno.Text.Trim();

        string query = "FillInvoiceDetail1";
        TF_DATA objData = new TF_DATA();

        DataTable dt = objData.getData(query, p1, p2, p4, p5);

        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            //_pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            // GridViewInvoice.PageSize = _pageSize;
            GridViewInvoice.DataSource = dt.DefaultView;
            GridViewInvoice.DataBind();
            GridViewInvoice.Visible = true;
            //rowPager.Visible = true;
            //lblmessage.Visible = false;
            //pagination(_records, _pageSize);
            CalcTotAmt();
        }

        else
        {
            GridViewInvoice.Visible = false;
            GridViewInvoice.Visible = false;
            //rowPager.Visible = false;
            //lblmessage.Text = "No record(s) found.";
            //labelMessage.Visible = true;
        }



    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        string result = "";
        string query = "Save_BOEClosureCancellation";
        TF_DATA objdata = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@AdjRefNo", SqlDbType.VarChar);
        p1.Value = txtadjrefno.Text;
        SqlParameter p2 = new SqlParameter("@AddedBy", SqlDbType.VarChar);
        p2.Value = Session["userName"].ToString().Trim();
        result = objdata.SaveDeleteData(query, p1, p2);

        string NewValue = "ADCode:" + ddlBranch.Text + ";BOENo:" + txt_boeno.Text + ";IECode:" + txt_iecode.Text + ";PortCode:" + txtprtcd.Text + ";BillDate:" + txtbilldate.Text + ";ClosureDate:" + txt_adjdate.Text +
                           ";ReasonForClosure:" + ddlreasadj.SelectedItem.Text + ";LetterNo:" + txt_letterNo.Text + ";LetterDate:" + txt_letterDate.Text + ";ApprovedBy:" + ddlapproved.SelectedItem.Text + ";ShippingNo:" + txt_docno.Text +
                           ";ShippingDtae:" + txt_docdate.Text + ";ShippingPort:" + txtdocprt.Text + ";ClosureBillIndicator:" + ddlboeclose.SelectedItem.Text + ";Remark:" + txt_remarks.Text + ";ClosureRefNo:" + txtadjrefno.Text;

        if (result == "inserted")
        {
            #region AUDIT TRAIL LOGIC
            query = "TF_IDPMS_AddEdit_AuditTrail_ByIECode";
            SqlParameter Q1 = new SqlParameter("@ADCode", ddlBranch.SelectedValue.ToString());
            SqlParameter Q2 = new SqlParameter("@IECode", txt_iecode.Text.Trim());
            SqlParameter Q3 = new SqlParameter("@OldValues", "");
            SqlParameter Q4 = new SqlParameter("@NewValues", NewValue);
            SqlParameter Q5 = new SqlParameter("@DocumentNo", txtadjrefno.Text);
            SqlParameter Q6 = new SqlParameter("@DocumentDate", txt_adjdate.Text);
            SqlParameter Q7 = new SqlParameter("@Mode", "A");
            SqlParameter Q8 = new SqlParameter("@ModifiedBy", Session["userName"].ToString());
            SqlParameter Q9 = new SqlParameter("@MenuName", "Bill Of Entry - Closure Cancel Data Entry");

            result = objdata.SaveDeleteData(query, Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9);

            #endregion

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Bill Of Entry Closure Cancellation Done Successfully.');", true);
            clear();

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("IDPMS_BOEClosure.aspx");
    }

    protected void txt_iecode_TextChanged(object sender, EventArgs e)
    {
        string query = "Get_Cust";
        TF_DATA objdata = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@custiecd", txt_iecode.Text);
        DataTable dt = objdata.getData(query, p1);
        if (dt.Rows.Count > 0)
        {
            txt_iecode.Text = dt.Rows[0]["CUST_IE_CODE"].ToString();
            lblcust.Text = dt.Rows[0]["CUST_NAME"].ToString().ToLower();
        }

    }
    protected void clear()
    {
        txt_iecode.Text = "";
        lblcust.Text = "";
        txt_boeno.Text = "";
        txtprtcd.Text = "";
        txtbilldate.Text = "";
        txt_adjdate.Text = "";
        txt_letterNo.Text = "";
        txt_letterDate.Text = "";
        txt_docno.Text = "";
        txt_docdate.Text = "";
        txtdocprt.Text = "";
        txtadjrefno.Text = "";
        txt_remarks.Text = "";
        lblBalAmt.Text = "";
        lblBOECur.Text = "";
        lblTot.Text = "";
        ddlapproved.SelectedValue = "1";
        ddlreasadj.SelectedValue = "1";
        ddlboeclose.SelectedValue = "2";
        GridViewInvoice.Visible = false;
    }

    protected void CalcTotAmt()
    {
        double sumofPayBill = 0;


        for (int i = 0; i < GridViewInvoice.Rows.Count; i++)
        {
            if (GridViewInvoice.Rows.Count > 0)
            {
                Label txtPamt = new Label();
                txtPamt = (Label)GridViewInvoice.Rows[i].Cells[2].FindControl("lblBalInvAmt");

                double txtPayAmt = Convert.ToDouble(txtPamt.Text);

                sumofPayBill = sumofPayBill + Convert.ToDouble(txtPamt.Text);

            }
        }

        lblTot.Text = sumofPayBill.ToString("F");
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_IDPMS_BOEClosure_View.aspx", true);
    }
    protected void txtadjrefno_TextChanged(object sender, EventArgs e)
    {
        filldetails(txt_boeno.Text, txtadjrefno.Text);
    }
}