using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class IDPMS_IDPMS_BOEClosure : System.Web.UI.Page
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
                    // btnSave.Enabled = false;
                    lblSupervisormsg.Visible = true;

                    //txt_boeno.Focus();
                }

                else
                {
                    btnSave.Enabled = true;
                    lblSupervisormsg.Visible = false;
                }



                //if (Request.QueryString["mode"].ToString() == "add")
                //{
                //    //fillCurrency();
                //    hdnyr.Value = Request.QueryString["year"].ToString();
                //    //txtadrefno.Text = txt_boeno.Text + txtprtcd.Text + txtbilldate.Text;

                //}

                //else
                //{
                //    //fillCurrency();
                //    string irmno = Request.QueryString["irmnono"].ToString();
                //    string adjrefno = Request.QueryString["adjref"].ToString();
                //    filldetails(irmno, adjrefno);
                //    txt_boeno.Enabled = false;
                //    btnboehelp.Enabled = false;
                //}

                fillBranch();
                btnboehelp.Attributes.Add("onclick", "return Dump_Help();");
                btncusthelp.Attributes.Add("onclick", "return Cust_Help();");
                //btn_reasonhelp.Attributes.Add("onclick", "return adj();");
                btnSave.Attributes.Add("onclick", "return validate();");
                //txtadjrefno.Attributes.Add("onblur", "return amt();");
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                txt_adjdate.Attributes.Add("onblur", "return isValidDate(" + txt_adjdate.ClientID + "," + "'Closure Date'" + ");");
                txt_letterDate.Attributes.Add("onblur", "return isValidDate(" + txt_letterDate.ClientID + "," + "'Letter Date'" + ");");
                txt_docdate.Attributes.Add("onblur", "return isValidDate(" + txt_docdate.ClientID + "," + "'Shipping Bill Date'" + " );");
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
        //ListItem li = new ListItem();
        //li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            //li.Text = "---Select---";
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
    protected void fillgrid()
    {
        int _pageSize = 0;
        double sumofboes = 0;

        SqlParameter p1 = new SqlParameter("@billno", SqlDbType.VarChar);
        p1.Value = txt_boeno.Text;

        SqlParameter p2 = new SqlParameter("@portcode", SqlDbType.VarChar);
        p2.Value = txtprtcd.Text.Trim();

        SqlParameter p3 = new SqlParameter("@IECode", SqlDbType.VarChar);
        p3.Value = txt_iecode.Text.Trim();

        SqlParameter p4 = new SqlParameter("@billdate", SqlDbType.VarChar);
        p4.Value = txtbilldate.Text.Trim();

        string query = "FillInvoiceDetail";
        TF_DATA objData = new TF_DATA();

        DataTable dt = objData.getData(query, p1, p2, p3, p4);

        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            //_pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            // GridViewInvoice.PageSize = _pageSize;
            GridViewInvoice.DataSource = dt.DefaultView;
            GridViewInvoice.DataBind();
            GridViewInvoice.Visible = true;
            for (int i = 0; i < GridViewInvoice.Rows.Count; i++)
            {
                TextBox txtPayAmt = (TextBox)GridViewInvoice.Rows[i].FindControl("txt_adrefno");
                sumofboes = sumofboes + Convert.ToDouble(txtPayAmt.Text);
            }
            lblTot.Text = "Total :-" + sumofboes.ToString("F");
            lblBalTot.Text = "Total :-" + sumofboes.ToString("F");
            //rowPager.Visible = true;
            //lblmessage.Visible = false;
            //pagination(_records, _pageSize);
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
        string _query = "";
        string query1 = "FillInvoiceDetail";
        TF_DATA object1 = new TF_DATA();
        //string search = txt_boeno.Text.Trim();
        //SqlParameter a1 = new SqlParameter("@BOENO", SqlDbType.VarChar);
        //a1.Value = search;

        //SqlParameter a2 = new SqlParameter("@prtcd", SqlDbType.VarChar);
        //a2.Value = txtprtcd.Text.Trim();

        ////SqlParameter p3 = new SqlParameter("@DocType", SqlDbType.VarChar);
        ////p3.Value = txtDocPrFx.Text.Trim();

        //SqlParameter a4 = new SqlParameter("@billdate", SqlDbType.VarChar);
        //a4.Value = txtbilldate.Text.Trim();

        //DataTable dt = object1.getData(query1, a1, a2, a4);
        //if (dt.Rows.Count > 0)
        //{

        string result = "";
        string _script = "";
        int Cheqcount = 0;
        string NewValue = "ADCode:" + ddlBranch.Text + ";BOENo:" + txt_boeno.Text + ";IECode:" + txt_iecode.Text + ";PortCode:" + txtprtcd.Text + ";BillDate:" + txtbilldate.Text + ";ClosureDate:" + txt_adjdate.Text +
                           ";ReasonForClosure:" + ddlreasadj.SelectedItem.Text + ";LetterNo:" + txt_letterNo.Text + ";LetterDate:" + txt_letterDate.Text + ";ApprovedBy:" + ddlapproved.SelectedItem.Text + ";ShippingNo:" + txt_docno.Text +
                           ";ShippingDtae:" + txt_docdate.Text + ";ShippingPort:" + txtdocprt.Text + ";ClosureBillIndicator:" + ddlboeclose.SelectedItem.Text + ";Remark:" + txt_remarks.Text + ";ClosureRefNo:" + txtadjrefno.Text;

        Label lblinsrno = new Label();
        Label lblinvoiceno = new Label();
        TextBox adjrefno = new TextBox();

        SqlParameter p1 = new SqlParameter("@BOENO", txt_boeno.Text);
        SqlParameter p2 = new SqlParameter("@AdCode", ddlBranch.Text);
        SqlParameter p3 = new SqlParameter("@IECode", txt_iecode.Text);
        SqlParameter p4 = new SqlParameter("@prtcd", txtprtcd.Text);
        SqlParameter p5 = new SqlParameter("@billdate", txtbilldate.Text);
        //SqlParameter p6 = new SqlParameter("@adjustedValue", SqlDbType.Float);
        //p6.Value = txt_adjamt.Text;
        SqlParameter p7 = new SqlParameter("@adjustmentDate", txt_adjdate.Text);
        SqlParameter p8 = new SqlParameter("@Reason", ddlreasadj.Text);
        SqlParameter p9 = new SqlParameter("@LetterNo", txt_letterNo.Text);
        SqlParameter p10 = new SqlParameter("@LetterDate", txt_letterDate.Text);
        SqlParameter p11 = new SqlParameter("@ApprovedBy", ddlapproved.Text);
        SqlParameter p12 = new SqlParameter("@DocumentNo", txt_docno.Text);
        SqlParameter p13 = new SqlParameter("@DocDate", txt_docdate.Text);
        SqlParameter p14 = new SqlParameter("@DocPort", txtdocprt.Text);
        SqlParameter p15 = new SqlParameter("@closeofBillIndicator", ddlboeclose.Text);
        SqlParameter p16 = new SqlParameter("@Remark", txt_remarks.Text);

        string insr; string invno; string adref;
        for (int i = 0; i < GridViewInvoice.Rows.Count; i++)
        {
            lblinsrno = (Label)GridViewInvoice.Rows[i].Cells[0].FindControl("lblinsrno");
            insr = lblinsrno.Text;
            lblinvoiceno = (Label)GridViewInvoice.Rows[i].Cells[1].FindControl("lblinvoiceno");
            invno = lblinvoiceno.Text;
            adjrefno = (TextBox)GridViewInvoice.Rows[i].Cells[3].FindControl("txt_adrefno");
            adref = adjrefno.Text;



            SqlParameter p17 = new SqlParameter("@insr", insr);
            SqlParameter p18 = new SqlParameter("@invno", invno);
            SqlParameter p19 = new SqlParameter("@adref", adref);
            SqlParameter p20 = new SqlParameter("@adjref", txtadjrefno.Text);
            SqlParameter p21 = new SqlParameter("@AddedBy", Session["userName"].ToString().Trim());

            NewValue = NewValue + ";InvoiceSrNo:" + insr + ";InvoiceNo:" + invno
                + ";Closure Amount:" + adref;

            string query = "Save_BOEClosure";
            TF_DATA objdata = new TF_DATA();

            CheckBox chkrow = (CheckBox)GridViewInvoice.Rows[i].FindControl("RowChkAllow");
            if (chkrow.Checked == true)
            {
                result = objdata.SaveDeleteData(query, p1, p2, p3, p4, p5, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21);
                Cheqcount = Cheqcount + 1;
            }
        }
        if (Cheqcount < 1)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Select The BOE Before Saving');", true);
            return;
        }
        if (result == "inserted")
        {
            #region AUDIT TRAIL LOGIC
            string query = "TF_IDPMS_AddEdit_AuditTrail_ByIECode";
            SqlParameter Q1 = new SqlParameter("@ADCode", ddlBranch.SelectedValue.ToString());
            SqlParameter Q2 = new SqlParameter("@IECode", txt_iecode.Text.Trim());
            SqlParameter Q3 = new SqlParameter("@OldValues", "");
            SqlParameter Q4 = new SqlParameter("@NewValues", NewValue);
            SqlParameter Q5 = new SqlParameter("@DocumentNo", txtadjrefno.Text);
            SqlParameter Q6 = new SqlParameter("@DocumentDate", txt_adjdate.Text);
            SqlParameter Q7 = new SqlParameter("@Mode", "A");
            SqlParameter Q8 = new SqlParameter("@ModifiedBy", Session["userName"].ToString());
            SqlParameter Q9 = new SqlParameter("@MenuName", "Bill Of Entry - Closure Data Entry");

            result = object1.SaveDeleteData(query, Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9);

            #endregion

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Bill Of Entry Closed.');", true);
            clear();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("IDPMS_BOEClosure.aspx");
    }
    protected void txt_boeno_TextChanged(object sender, EventArgs e)
    {
        string query = "Filladie1";
        TF_DATA objdata = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BOENo", txt_boeno.Text);
        SqlParameter p2 = new SqlParameter("@IECode", txt_iecode.Text);
        //filldetails(txt_boeno.Text, txtadjrefno.Text);
        DataTable dt = objdata.getData(query, p1, p2);
        //if (dt.Rows.Count > 0)
        //{
        //    txtprtcd.Text = dt.Rows[0]["PortCode"].ToString();
        //    txtbilldate.Text = dt.Rows[0]["Bill_Entry_Date"].ToString();
        //    lblBalAmt.Text = Convert.ToDouble(dt.Rows[0]["BalAmt"].ToString()).ToString();
        //    lblBOECur.Text = dt.Rows[0]["Invoice_Currency"].ToString();
        //    string AdjBalAmt = dt.Rows[0]["BalAmt"].ToString();
        //    txt_docno.Text = txt_boeno.Text;
        //    txt_docdate.Text = dt.Rows[0]["Bill_Entry_Date"].ToString();
        //    txtdocprt.Text = dt.Rows[0]["PortCode"].ToString();
        //    //ddlcurr.Focus();
        //    fillgrid();
        //    GenerateAdjRefNo();
        //}
        if (dt.Rows.Count > 0)
        {
            txtprtcd.Text = dt.Rows[0]["PortCode"].ToString();
            txtbilldate.Text = dt.Rows[0]["Bill_Entry_Date"].ToString();
            lblBalAmt.Text = Convert.ToDouble(dt.Rows[0]["BalAmt"].ToString()).ToString();
            lblBOECur.Text = dt.Rows[0]["Invoice_Currency"].ToString();
            string AdjBalAmt = dt.Rows[0]["BalAmt"].ToString();
            txt_docno.Text = txt_boeno.Text;
            txt_docdate.Text = dt.Rows[0]["Bill_Entry_Date"].ToString();
            txtdocprt.Text = dt.Rows[0]["PortCode"].ToString();
            //ddlcurr.Focus();
            fillgrid();
            GenerateAdjRefNo();
            int daydiff = Convert.ToInt32(dt.Rows[0]["DayDiff"].ToString());
            if (daydiff >= 180)
            {
                string CustACNo = "";
                query = "TF_IDPMS_GET_CUSTDETAILS_BY_IECODE";
                SqlParameter IECODE = new SqlParameter("@CUST_IE_CODE", txt_iecode.Text.Trim());
                DataTable dt1 = objdata.getData(query, IECODE);
                if (dt1.Rows.Count > 0)
                {
                    CustACNo = dt1.Rows[0]["CUST_ACCOUNT_NO"].ToString();
                }
                lblLink.InnerText = "[ Click here to Go To Bill Of Entry Extension Screen..... ] ";
                lblLink.Attributes.Add("href", "TF_AddEditPaymentExtn.aspx?CustACNo=" + CustACNo + "&BOENo=" + txt_boeno.Text.Trim() + "");
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('BOE NO : " + txt_boeno.Text + " Requires Payment Extension !');", true);
            }

        }

        else
        {
            //txt_adcode.Text = "";
            txt_iecode.Text = "";
            lblcust.Text = "";
            txt_boeno.Text = "";
            lblBOECur.Text = "";
            lblBalAmt.Text = "";
            txt_boeno.Focus();
        }
    }
    protected void GenerateAdjRefNo()
    {
        string adj;
        TF_DATA objData = new TF_DATA();
        string query = "GenerateADJ";
        SqlParameter p1 = new SqlParameter("@BOENO", txt_boeno.Text);
        SqlParameter p2 = new SqlParameter("@prtcd", txtprtcd.Text);
        SqlParameter p3 = new SqlParameter("@billdate", SqlDbType.VarChar);
        p3.Value = txtbilldate.Text;
        DataTable dt = objData.getData(query, p1, p2, p3);

        if (dt.Rows.Count > 0)
        {
            adj = dt.Rows[0]["adjustmentReferenceNumber"].ToString();

            txtadjrefno.Text = Convert.ToString(adj);
        }


    }
    //protected void txtadjrefno_TextChanged(object sender, EventArgs e)
    //{
    //    GenerateAdjRefNo();
    //}
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
        lblBalTot.Text = "";
        GridViewInvoice.Visible = false;
    }

    protected void txt_adrefno_textchange(object sender, EventArgs e)
    {
        double sumofPayBill = 0;


        for (int i = 0; i < GridViewInvoice.Rows.Count; i++)
        {
            if (GridViewInvoice.Rows.Count > 0)
            {
                TextBox txtPamt = new TextBox();
                txtPamt = (TextBox)GridViewInvoice.Rows[i].Cells[3].FindControl("txt_adrefno");
                CheckBox chkrow = new CheckBox();
                chkrow = (CheckBox)GridViewInvoice.Rows[i].FindControl("RowChkAllow");
                if (chkrow.Checked == true)
                {
                    double txtPayAmt = Convert.ToDouble(txtPamt.Text);
                    //txtPamt.Enabled = false;
                    //txtPamt.Text = "";
                    //TextBox txthead = new TextBox();
                    //txthead = (TextBox)GridViewInvoice.Rows[i].Cells[2].FindControl("txthead");
                    //txthead.Enabled = true;
                    //txthead.Text = "";
                    sumofPayBill = sumofPayBill + Convert.ToDouble(txtPamt.Text);
                }
            }
        }
        lblTot.Text = "Total :-" + sumofPayBill.ToString("F");
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_IDPMS_BOEClosure_View.aspx", true);
    }
    protected void HeaderChkAllow_CheckedChanged(object sender, EventArgs e)
    {

        CheckBox chk = (CheckBox)GridViewInvoice.HeaderRow.FindControl("HeaderChkAllow");
        if (chk.Checked)
        {
            for (int i = 0; i < GridViewInvoice.Rows.Count; i++)
            {
                CheckBox chkrow = (CheckBox)GridViewInvoice.Rows[i].FindControl("RowChkAllow");
                Label lblAccess = (Label)GridViewInvoice.Rows[i].FindControl("lblSel");
                chkrow.Checked = true;
                lblAccess.Text = "";
                lblAccess.ForeColor = System.Drawing.Color.Blue;
            }

        }
        else
        {
            for (int i = 0; i < GridViewInvoice.Rows.Count; i++)
            {
                CheckBox chkrow = (CheckBox)GridViewInvoice.Rows[i].FindControl("RowChkAllow");
                Label lblAccess = (Label)GridViewInvoice.Rows[i].FindControl("lblSel");
                chkrow.Checked = false;
                lblAccess.Text = "";
                lblAccess.ForeColor = System.Drawing.Color.Red;
            }
        }
        txt_adrefno_textchange(null, null);
        chk.Focus();

    }
    protected void RowChkAllow_CheckedChanged(object sender, EventArgs e)
    {
        txt_adrefno_textchange(null, null);
        CheckBox checkbox = (CheckBox)sender;
        GridViewRow row = (GridViewRow)checkbox.NamingContainer;

        Label lblAccess = (Label)row.FindControl("lblSel");

        TextBox txtPAmt = (TextBox)row.FindControl("txt_adrefno");
        Label lblBalInvAmt = (Label)row.FindControl("lblBalInvAmt");
        double invamt = Convert.ToDouble(lblBalInvAmt.Text);


        double Payamt = Convert.ToDouble(txtPAmt.Text);
        if (checkbox.Checked == true)
        {
            for (int i = 0; i < GridViewInvoice.Rows.Count; i++)
            {

                CheckBox chkrow = (CheckBox)GridViewInvoice.Rows[i].FindControl("RowChkAllow");
                if (chkrow.Checked == true)
                {
                    if (Payamt > invamt)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Closure Amount is greater than Invoice Amount');", true);
                        checkbox.Checked = true;
                        txtPAmt.Text = lblBalInvAmt.Text;
                        txtPAmt.Focus();
                    }
                }

            }

        }
        else
        {
            lblAccess.Text = "";
            lblAccess.ForeColor = System.Drawing.Color.Blue;
            checkbox.Focus();

        }

        CheckBox chk = (CheckBox)GridViewInvoice.HeaderRow.FindControl("HeaderChkAllow");
        int isAllChecked = 0;
        for (int i = 0; i < GridViewInvoice.Rows.Count; i++)
        {
            CheckBox chkrow = (CheckBox)GridViewInvoice.Rows[i].FindControl("RowChkAllow");
            if (chkrow.Checked == true)
                isAllChecked = 1;
            else
            {
                isAllChecked = 0;
                break;
            }
        }
        if (isAllChecked == 1)
            chk.Checked = true;
        else
            chk.Checked = false;
    }
}