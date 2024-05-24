using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class ImportWareHousing_Transactions_TF_IMPWH_Settlement : System.Web.UI.Page
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
                if (Request.QueryString["mode"] != "Add")
                {
                    fillBranch();
                    ddlBranch.SelectedValue = Session["userADCode"].ToString();
                    ddlBranch.Enabled = false;
                    fillDetails(Request.QueryString["DocNo"]);
                    fillgrid();
                    //btnPartyID.Attributes.Add("onclick", "return Cust_Help();");
                    txtDocPrFx.Text = Request.QueryString["DocNo"].ToString().Substring(0, 3);
                    txtDocumentNo.Text = Request.QueryString["DocNo"].ToString().Substring(3, 6);
                    txtYear.Text = Request.QueryString["DocNo"].ToString().Substring(9, 4);

                }
                else
                {
                    //txtORMNo.Text = "";
                    hdnYear.Value = System.DateTime.Now.Year.ToString();
                    btnHelp_DocNo.Visible = true;
                    //txtinvocesrNo.Enabled = true;
                    //ddlBranch.Text = Request.QueryString["Branch"];
                    txtDocPrFx.Text = "BOE";
                    txtYear.Text = System.DateTime.Now.Year.ToString();
                    //txtDocumentNo.Text = Request.QueryString["Docno"].ToString();
                    fillBranch();
                    //txtPartyID_TextChanged(null, null);
                    //txtORMNo_TextChanged(null, null);
                    //btnPartyID.Attributes.Add("onclick", "return Cust_Help();");
                    //btnHelp_DocNo.Attributes.Add("onclick", "return OTT_Help();");
                    //fillgrid();
                }
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;
                generateDocumentNo();
                txtDocDate.Text = System.DateTime.Now.ToString("dd/MM/yyy");
                btnPartyID.Attributes.Add("onclick", "return Cust_Help('mouseClick');");
                txtPartyID.Attributes.Add("onkeydown", "return Cust_Help('this');");
                btnThirdPartyID.Attributes.Add("onclick", "return Cust_Help_New();");
                btnHelp_DocNo.Attributes.Add("onclick", "return OTT_Help()");
                btnBillEntryNo.Attributes.Add("onclick", "return Dump_Help()");
                btnSave.Attributes.Add("onclick", "return ValidateSave();");
                //Button1.Attributes.Add("onclick", "return openCurrency();");
                //btnAddGRPPCustoms.Attributes.Add("onclick", "return ValidateAdd();");
                //txtinvcamt.Attributes.Add("onblur", "return Amt();");
                //txtinvcamt.Attributes.Add("onkeydown", "return validate_Number(event);");
                //SetInitialRow();
                txtPartyID.Focus();
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
        //    li.Text = "No record(s) found";

        //ddlBranch.Items.Insert(0, li);
    }
    private void fillDetails(string DocNo)
    {
        TF_DATA obj = new TF_DATA();
        //string query = "TF_IDPMS_ManualBOE_FillDetails";
        string query = "getSettleDetails";
        SqlParameter p1 = new SqlParameter("@DocNo", SqlDbType.VarChar);
        p1.Value = DocNo;
        DataTable dt = obj.getData(query, p1);
        if (dt.Rows.Count > 0)
        {
            txtDocumentNo.Text = dt.Rows[0]["Doc_No"].ToString();
            txtPartyID.Text = dt.Rows[0]["CUST_AC_NO"].ToString();
            txtPartyID_TextChanged(null, null);
            //txtPartyID.TextChanged+=txtPartyID_TextChanged;
            txtORMNo.Text = dt.Rows[0]["outwardReferenceNumber"].ToString();
            txtORMNo_TextChanged(null, null);
            //txtORMNo.TextChanged += txtORMNo_TextChanged;
            txtbillno.Text = dt.Rows[0]["Bill_No"].ToString();
            txtbilldate.Text = dt.Rows[0]["Bill_Date"].ToString();
            txtprtcd.Text = dt.Rows[0]["PortCode"].ToString();


            //fillGrid();
            fillgrid();

        }
    }
    private void generateDocumentNo()
    {
        int docno;
        TF_DATA objData = new TF_DATA();
        string _query = "TF_GenerateDocNo";
        SqlParameter p1 = new SqlParameter("@year", SqlDbType.VarChar);
        p1.Value = txtYear.Text;
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {

            docno = Convert.ToInt32(dt.Rows[0]["DocNo"].ToString());

            docno = docno + 1;
            txtDocumentNo.Text = Convert.ToString(docno);


            int len = txtDocumentNo.Text.Length;
            if (txtDocumentNo.Text.Length < 6)
            {
                for (int i = 6; i > len; i--)
                {
                    txtDocumentNo.Text = "0" + txtDocumentNo.Text;
                }
            }

        }
        else
        {
            docno = 000001;
            txtDocumentNo.Text = Convert.ToString(docno);
            int len = txtDocumentNo.Text.Length;
            if (txtDocumentNo.Text.Length < 6)
            {
                for (int i = 6; i > len; i--)
                {
                    txtDocumentNo.Text = "0" + txtDocumentNo.Text;
                }
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("IDPMS_BOE_Setlement_view.aspx", true);
    }
    protected void txtPartyID_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@customerACNo", SqlDbType.VarChar);
        p1.Value = txtPartyID.Text.Trim();

        SqlParameter p2 = new SqlParameter("@branch", SqlDbType.VarChar);
        p2.Value = ddlBranch.SelectedValue.ToString();
        string _query = "TF_GetCustomerMasterDetails1";


        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            lblCustName.Text = dt.Rows[0]["CUST_NAME"].ToString();
        }
        else
        {
            lblCustName.Text = "";
            txtPartyID.Text = "";

        }


    }
    protected void txtbillno_TextChanged(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();
        string query = "TF_IMPWH_Selected_BOE_details";
        SqlParameter P1 = new SqlParameter("@BOENO", txtbillno.Text);
        SqlParameter P2 = new SqlParameter("@branch", ddlBranch.SelectedValue.ToString());
        SqlParameter P3;
        if (ThirdPartyCB.Checked == true)
        {
            P3 = new SqlParameter("@iecode", txtThirdPartyID.Text);
        }
        else
        {
            P3 = new SqlParameter("@iecode", txtPartyID.Text);
        }

        DataTable dt = obj.getData(query, P1, P2, P3);
        if (dt.Rows.Count > 0)
        {
            lblboecur.Text = dt.Rows[0]["Invoice_Currency"].ToString();
            lblboeamt.Text = dt.Rows[0]["BOEBalAmt"].ToString();
            txtbilldate.Text = dt.Rows[0]["Bill_Entry_Date"].ToString();
            txtprtcd.Text = dt.Rows[0]["PortCode"].ToString();
            int daydiff = Convert.ToInt32(dt.Rows[0]["DayDiff"].ToString());
            if (daydiff >= 180)
            {
                string CustACNo = "";
                if (ThirdPartyCB.Checked)
                {
                    query = "TF_IDPMS_GET_CUSTDETAILS_BY_IECODE";
                    SqlParameter IECODE = new SqlParameter("@CUST_IE_CODE", txtThirdPartyID.Text.Trim());
                    DataTable dt1 = obj.getData(query, IECODE);
                    CustACNo = dt1.Rows[0]["CUST_ACCOUNT_NO"].ToString();
                }
                else
                {
                    CustACNo = txtPartyID.Text.Trim();
                }
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('BOE NO : " + txtbillno.Text + " Requires Payment Extension !');", true);
            }
        }
        else
        {
            lblboecur.Text = "";
            lblboeamt.Text = "";
            txtbilldate.Text = "";
            txtprtcd.Text = "";
        }

    }
    protected void txtORMNo_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@OTTNo", SqlDbType.VarChar);
        p1.Value = txtORMNo.Text.Trim();
        SqlParameter p2 = new SqlParameter("@iecode", SqlDbType.VarChar);
        p2.Value = txtPartyID.Text.Trim();
        SqlParameter p3 = new SqlParameter("@branch", SqlDbType.VarChar);
        p3.Value = ddlBranch.Text.Trim();
        string _query = "TF_GetOTTDetails";
        DataTable dt = objData.getData(_query, p1, p2, p3);
        if (dt.Rows.Count > 0)
        {
            lblOrmAmount.Text = dt.Rows[0]["Amount"].ToString();
            lblcurren.Text = dt.Rows[0]["Currency"].ToString();
            if (lblcurren.Text != lblboecur.Text)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('BOE NO : " + txtbillno.Text + " Currency Is Diffrent Than ORM Currency !');", true);
                lblExchangeCurrency.Text = lblcurren.Text;
                txtExchangeRate.Focus();
            }
            else
            {
                lblExchangeCurrency.Text = "";
            }
        }
        else
        {
            lblOrmAmount.Text = "";
            lblExchangeCurrency.Text = "";
            txtORMNo.Text = "";
            lblcurren.Text = "";
            txtbillno.Text = "";
            //txtbillno_TextChanged(null, null);
        }
    }
    protected void btnAddGRPPCustoms_Click(object sender, EventArgs e)
    {
        if (lblcurren.Text != lblboecur.Text)
        {
            if (txtExchangeRate.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Exchange Rate For Cross Currency Can Not Be Balnk!');", true);
                lblExchangeCurrency.Text = lblcurren.Text;
                txtExchangeRate.Focus();
                return;
            }
        }
        else
        {
            txtExchangeRate.Text = "";
        }
        AddNewRowToGrid();

        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
        DataRow drCurrentRow = null;
    }
    private void AddNewRowToGrid()
    {
        GridViewEDPMS_Bill_Details.Visible = true;
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            for (int a = 0; a < dtCurrentTable.Rows.Count; a++)
            {
                if (txtbillno.Text == dtCurrentTable.Rows[a]["Bill_Entry_No"].ToString() && txtbilldate.Text == dtCurrentTable.Rows[a]["Bill_Entry_Date"].ToString() && txtprtcd.Text == dtCurrentTable.Rows[a]["PortCode"].ToString())
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('BON No. already Added Please Select Other BOE No.');", true);
                    return;
                }
            }
            for (int a = 0; a < dtCurrentTable.Rows.Count; a++)
            {
                if (lblboecur.Text != dtCurrentTable.Rows[a]["Invoice_Currency"].ToString())
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Selected BOE No. Currency is Diffrent !');", true);
                    return;
                }
            }
            double Exchrate = 1;
            if (txtExchangeRate.Text.Trim() == "")
            {
                Exchrate = 1;
            }
            else
            {
                Exchrate = Convert.ToDouble(txtExchangeRate.Text.Trim());
            }
            TF_DATA obj = new TF_DATA();
            string query = "TF_IMPWH_Get_BillDetails";
            SqlParameter P1 = new SqlParameter("@billno", txtbillno.Text);
            SqlParameter P2 = new SqlParameter("@billdate", txtbilldate.Text);
            SqlParameter P3 = new SqlParameter("@portcode", txtprtcd.Text);
            SqlParameter P4 = new SqlParameter("@ExchangeRate", Exchrate);
            SqlParameter P5 = new SqlParameter("@ExchangeCurrency", lblcurren.Text);

            DataTable dt = obj.getData(query, P1, P2, P3, P4, P5);
            if (dt.Rows.Count > 0)
            {
                for (int s = 0; s < dt.Rows.Count; s++)
                {
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["Bill_Entry_No"] = dt.Rows[s]["Bill_Entry_No"].ToString();
                    drCurrentRow["PortCode"] = dt.Rows[s]["PortCode"].ToString();
                    drCurrentRow["Bill_Entry_Date"] = dt.Rows[s]["Bill_Entry_Date"].ToString();
                    drCurrentRow["InvoiceSerialNo"] = dt.Rows[s]["InvoiceSerialNo"].ToString();
                    drCurrentRow["Terms_Invoice"] = dt.Rows[s]["Terms_Invoice"].ToString();
                    drCurrentRow["InvoiceNo"] = dt.Rows[s]["InvoiceNo"].ToString();
                    drCurrentRow["Invoice_Currency"] = dt.Rows[s]["Invoice_Currency"].ToString();
                    drCurrentRow["InvoiceAmt"] = dt.Rows[s]["InvoiceAmt"].ToString();
                    drCurrentRow["PaymentAmount"] = dt.Rows[s]["PaymentAmount"].ToString();
                    drCurrentRow["PaymentCurr"] = dt.Rows[s]["PaymentCurr"].ToString();
                    dtCurrentTable.Rows.Add(drCurrentRow);
                }

            }

            //dtCurrentTable.Rows.Add(drCurrentRow);
            // for showing sum of boes


            ViewState["CurrentTable"] = dtCurrentTable;

            GridViewEDPMS_Bill_Details.DataSource = dtCurrentTable;
            GridViewEDPMS_Bill_Details.DataBind();


        }

        else
        {
            //Response.Write("ViewState is null");
            fillgrid();

        }
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable2 = (DataTable)ViewState["CurrentTable"];
            double sumofboes = 0;
            double sumofPayBill = 0;
            for (int a = 0; a < dtCurrentTable2.Rows.Count; a++)
            {
                sumofboes = sumofboes + Convert.ToDouble(dtCurrentTable2.Rows[a]["InvoiceAmt"]);
            }
            for (int i = 0; i < GridViewEDPMS_Bill_Details.Rows.Count; i++)
            {
                TextBox txtPAmt = (TextBox)GridViewEDPMS_Bill_Details.Rows[i].FindControl("txtPayAmt");
                sumofPayBill = sumofPayBill + Convert.ToDouble(txtPAmt.Text);
            }
            //lblTot.Text = "Total :-" + sumofboes.ToString("F");
            lblTot.Text = "Selected BOE's Total : " + sumofboes.ToString("F");
            lblTotPayAmt.Text = sumofPayBill.ToString("F");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_IMPWH_Settlement.aspx?mode=Add", true);
    }
    protected void GridViewEDPMS_Bill_Details_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerialNumber = new Label();
            Button btnDelete = new Button();
            lblSerialNumber = (Label)e.Row.FindControl("lblinvoiesrno");

            int i = 0;

            //if (Request.QueryString["mode"] != "Add")
            //{
            //foreach (TableCell cell in e.Row.Cells)
            //{
            //    if (i != 4)
            //        //cell.Attributes.Add("onclick", "return gridClicked(" + lblSerialNumber.Text + ");");
            //    else
            //        cell.Style.Add("cursor", "default");
            //    i++;
            //}
            //}
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //e.Row.Cells.Clear();
            //// Create a new table cell
            //TableCell tableCell = new TableCell();
            //// Set the ColumnSpan 
            //tableCell.ColumnSpan = 6;
            //// Set the Text alignment
            //tableCell.HorizontalAlign = HorizontalAlign.Center;
            //// Set the text that you want to display in the footer
            //tableCell.Text = "Total = " + lblTot.Text.ToString();
            //// Finally add the cell to the footer row
            //e.Row.Cells.Add(tableCell);

            //int intNoOfMergeCol = e.Row.Cells.Count - 1; /*except last column */
            //for (int intCellCol = 1; intCellCol < intNoOfMergeCol; intCellCol++)
            //    e.Row.Cells.RemoveAt(1);
            //e.Row.Cells[0].ColumnSpan = intNoOfMergeCol;
            //e.Row.Cells[0].Text = "Running Total : " + lblTot.Text;
            //e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Right;

        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();

        string query = "TF_IMPWH_Add_BOE";
        string result = "";
        string DocNo = txtDocPrFx.Text + txtDocumentNo.Text + txtYear.Text;
        SqlParameter p2 = new SqlParameter("@DocNo", DocNo);
        SqlParameter p3 = new SqlParameter("@ottno", txtORMNo.Text);

        string NewValue = "Document No:" + DocNo + ";Document Date:" + txtDocDate.Text + ";Customer ID:" + txtPartyID.Text +
                            ";ORM No:" + txtORMNo.Text;


        SqlParameter p4 = new SqlParameter("@CustID", txtPartyID.Text);
        SqlParameter p1 = new SqlParameter("@branch", ddlBranch.Text);

        SqlParameter p17 = new SqlParameter("@DocDate", txtDocDate.Text);
        SqlParameter mode = new SqlParameter("@Mode", "Add");

        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
        if (dtCurrentTable == null)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('ADD BOE to be Adjusted');", true);
            SetFocus(btnAddGRPPCustoms);
        }
        else
        {
            int Cheqcount = 0;
            SqlParameter p20;
            if (ThirdPartyCB.Checked == true)
            {
                p20 = new SqlParameter("@PartyID", txtThirdPartyID.Text);
            }
            else
            {
                p20 = new SqlParameter("@PartyID", "");
            }


            for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
            {
                string lbl0 = dtCurrentTable.Rows[i]["Bill_Entry_No"].ToString();
                string lbl1 = dtCurrentTable.Rows[i]["InvoiceSerialNo"].ToString();
                string lbl2 = dtCurrentTable.Rows[i]["Terms_Invoice"].ToString();
                string lbl3 = dtCurrentTable.Rows[i]["InvoiceNo"].ToString();
                string lbl4 = dtCurrentTable.Rows[i]["Invoice_Currency"].ToString();
                string lbl6 = dtCurrentTable.Rows[i]["Bill_Entry_Date"].ToString();
                string lbl7 = dtCurrentTable.Rows[i]["PortCode"].ToString();


                TextBox lblinvcamt = (TextBox)GridViewEDPMS_Bill_Details.Rows[i].FindControl("lblinvcamt");
                TextBox txtPAmt = (TextBox)GridViewEDPMS_Bill_Details.Rows[i].FindControl("txtPayAmt");


                SqlParameter p5 = new SqlParameter("@BillNo", lbl0);
                SqlParameter p6 = new SqlParameter("@BillDate", lbl6);
                SqlParameter p7 = new SqlParameter("@port", lbl7);
                SqlParameter p8 = new SqlParameter("@ottAmt", txtPAmt.Text);

                SqlParameter p9 = new SqlParameter("@invoicesrno", lbl1);

                SqlParameter p11 = new SqlParameter("@invoiceNo", lbl3);
                SqlParameter p10 = new SqlParameter("@invoiceterm", lbl2);
                SqlParameter p13 = new SqlParameter("@invoiceamt", lblinvcamt.Text);
                SqlParameter p12 = new SqlParameter("@invoicecur", lbl4);
                SqlParameter p14 = new SqlParameter("@addedby", Session["userName"].ToString());
                SqlParameter p15 = new SqlParameter("@adddate", System.DateTime.Now.ToString());
                SqlParameter p16 = new SqlParameter("@Flag", "P");

                CheckBox chkrow = (CheckBox)GridViewEDPMS_Bill_Details.Rows[i].FindControl("RowChkAllow");
                if (chkrow.Checked == true)
                {
                    NewValue = NewValue + ";Bill Of Entry No:" + lbl0 + ";Bill Of Entry Date:" + lbl6 +
                               ";Invoice Number:" + lbl3 + ";Invoice Amt:" + lblinvcamt.Text + ";Payment Amt:" + txtPAmt.Text;
                    result = obj.SaveDeleteData(query, p2, p3, p5, p6, p4, p1, p7, p8, p9, p11, p10, p13, p12, p14, p15, p16, p17, mode, p20);
                    Cheqcount = Cheqcount + 1;
                }
            }

            if (Cheqcount < 1)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Select The BOE Before Saving');", true);
                return;
            }

            if (result == "Save")
            {
                #region AUDIT TRAIL LOGIC
                SqlParameter Q1 = new SqlParameter("@Adcode", ddlBranch.SelectedValue.ToString());
                SqlParameter Q2 = new SqlParameter("@OldValues", "");
                SqlParameter Q3 = new SqlParameter("@NewValues", NewValue);
                SqlParameter Q4 = new SqlParameter("@CustAcNo", txtPartyID.Text);
                SqlParameter Q5 = new SqlParameter("@DocumentNo", DocNo);
                SqlParameter Q6 = new SqlParameter("@DocumentDate", txtDocDate.Text);
                SqlParameter Q7 = new SqlParameter("@Mode", "A");
                SqlParameter Q8 = new SqlParameter("@ModifiedBy", Session["userName"].ToString());
                SqlParameter Q9 = new SqlParameter("@MenuName", "Payment Settlement ORM Reference");

                result = obj.SaveDeleteData("TF_IDPMS_Audit_Trail_Add", Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9);
                #endregion

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added.');", true);
                Clear();
            }

        }
    }
    protected void Clear()
    {
        ViewState["CurrentTable"] = null;
        GridViewEDPMS_Bill_Details.DataSource = null;
        GridViewEDPMS_Bill_Details.Visible = false;
        GridViewEDPMS_Bill_Details.DataBind();
        //lblsumofBOEs.Text = "";
        txtORMNo_TextChanged(null, null);
        generateDocumentNo();
        txtbillno.Text = "";
        txtbilldate.Text = "";
        txtprtcd.Text = "";
        lblTot.Text = "";
        lblTotPayAmt.Text = "";
        lblboeamt.Text = "";
        lblboecur.Text = "";
    }
    protected void fillgrid()
    {
        double Exchrate = 1;
        if (txtExchangeRate.Text.Trim() == "")
        {
            Exchrate = 1;
        }
        else
        {
            Exchrate = Convert.ToDouble(txtExchangeRate.Text.Trim());
        }
        TF_DATA obj = new TF_DATA();
        string query = "TF_IMPWH_Get_BillDetails";
        SqlParameter P1 = new SqlParameter("@billno", txtbillno.Text);
        SqlParameter P2 = new SqlParameter("@billdate", txtbilldate.Text);
        SqlParameter P3 = new SqlParameter("@portcode", txtprtcd.Text);
        SqlParameter P4 = new SqlParameter("@ExchangeRate", Exchrate);
        SqlParameter P5 = new SqlParameter("@ExchangeCurrency", lblcurren.Text);

        DataTable dt = obj.getData(query, P1, P2, P3, P4, P5);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            GridViewEDPMS_Bill_Details.DataSource = dt.DefaultView;
            ViewState["CurrentTable"] = dt;
            GridViewEDPMS_Bill_Details.DataBind();
            GridViewEDPMS_Bill_Details.Visible = true;

        }
        else
        {
            GridViewEDPMS_Bill_Details.Visible = false;
        }


    }
    protected void txtPayAmt_textchange(object sender, EventArgs e)
    {
        double sumofPayBill = 0;
        for (int i = 0; i < GridViewEDPMS_Bill_Details.Rows.Count; i++)
        {
            if (GridViewEDPMS_Bill_Details.Rows.Count > 0)
            {
                TextBox txtPamt = new TextBox();
                txtPamt = (TextBox)GridViewEDPMS_Bill_Details.Rows[i].Cells[6].FindControl("txtPayAmt");
                CheckBox chkrow = new CheckBox();
                chkrow = (CheckBox)GridViewEDPMS_Bill_Details.Rows[i].FindControl("RowChkAllow");
                if (chkrow.Checked == true)
                {
                    double txtPayAmt = Convert.ToDouble(txtPamt.Text);
                    sumofPayBill = sumofPayBill + Convert.ToDouble(txtPamt.Text);
                }
            }
        }
        lblTotPayAmt.Text = sumofPayBill.ToString("F");
    }
    protected void lblinvcamt_textchange(object sender, EventArgs e)
    {
        double sumofInvBill = 0;
        double sumofPayBill = 0;
        double exrate = 1;
        if (txtExchangeRate.Text.Trim() != "")
        {
            exrate = Convert.ToDouble(txtExchangeRate.Text.Trim());
        }
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int index = gvRow.RowIndex;
        TextBox PayAmtt = (TextBox)GridViewEDPMS_Bill_Details.Rows[index].Cells[7].FindControl("txtPayAmt");
        TextBox InvAmtt = (TextBox)GridViewEDPMS_Bill_Details.Rows[index].Cells[5].FindControl("lblinvcamt");
        PayAmtt.Text = (Convert.ToDouble(InvAmtt.Text) * Convert.ToDouble(exrate)).ToString();
        for (int i = 0; i < GridViewEDPMS_Bill_Details.Rows.Count; i++)
        {
            if (GridViewEDPMS_Bill_Details.Rows.Count > 0)
            {
                TextBox lblinvcamt = new TextBox();
                lblinvcamt = (TextBox)GridViewEDPMS_Bill_Details.Rows[i].Cells[5].FindControl("lblinvcamt");
                double txtInvAmt = Convert.ToDouble(lblinvcamt.Text);
                sumofInvBill = sumofInvBill + Convert.ToDouble(lblinvcamt.Text);

                TextBox lblPaycamt = new TextBox();
                lblPaycamt = (TextBox)GridViewEDPMS_Bill_Details.Rows[i].Cells[7].FindControl("txtPayAmt");
                double txtPaymentAmt = Convert.ToDouble(lblPaycamt.Text);
                sumofPayBill = sumofPayBill + Convert.ToDouble(lblPaycamt.Text);

            }
        }
        lblTot.Text = sumofInvBill.ToString("F");
        lblTotPayAmt.Text = sumofPayBill.ToString("F");
    }
    protected void HeaderChkAllow_CheckedChanged(object sender, EventArgs e)
    {

        CheckBox chk = (CheckBox)GridViewEDPMS_Bill_Details.HeaderRow.FindControl("HeaderChkAllow");
        if (chk.Checked)
        {
            for (int i = 0; i < GridViewEDPMS_Bill_Details.Rows.Count; i++)
            {
                CheckBox chkrow = (CheckBox)GridViewEDPMS_Bill_Details.Rows[i].FindControl("RowChkAllow");
                Label lblAccess = (Label)GridViewEDPMS_Bill_Details.Rows[i].FindControl("lblSel");
                chkrow.Checked = true;
                txtPayAmt_textchange(null, null);
                //lblinvcamt_textchange(null, null);
                Count();
                lblAccess.Text = "";
                lblAccess.ForeColor = System.Drawing.Color.Blue;

                double ORMamt = Convert.ToDouble(lblOrmAmount.Text);
                double TotPayamt = Convert.ToDouble(lblTotPayAmt.Text);
                if (TotPayamt > ORMamt)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Total Payment Amount is greater than Balance ORM Amount');", true);
                }
            }

        }
        else
        {
            for (int i = 0; i < GridViewEDPMS_Bill_Details.Rows.Count; i++)
            {

                CheckBox chkrow = (CheckBox)GridViewEDPMS_Bill_Details.Rows[i].FindControl("RowChkAllow");
                Label lblAccess = (Label)GridViewEDPMS_Bill_Details.Rows[i].FindControl("lblSel");
                chkrow.Checked = false;
                lblAccess.Text = "";
                lblAccess.ForeColor = System.Drawing.Color.Red;

                //double ORMamt = Convert.ToDouble(lblOrmAmount.Text);
                //double TotPayamt = Convert.ToDouble(lblTotPayAmt.Text);
                //if (TotPayamt > ORMamt)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Total Payment Amount is greater than Balance ORM Amount');", true);
                //}
            }
        }
        chk.Focus();

    }
    protected void RowChkAllow_CheckedChanged(object sender, EventArgs e)
    {
        txtPayAmt_textchange(null, null);
        //lblinvcamt_textchange(null, null);
        Count();
        CheckBox checkbox = (CheckBox)sender;
        GridViewRow row = (GridViewRow)checkbox.NamingContainer;

        Label lblAccess = (Label)row.FindControl("lblSel");
        TextBox txtPAmt = (TextBox)row.FindControl("txtPayAmt");

        TextBox lblInvAmt = (TextBox)row.FindControl("lblinvcamt");
        double invamt = Convert.ToDouble(lblInvAmt.Text);

        double ORMamt = Convert.ToDouble(lblOrmAmount.Text);
        double Payamt = Convert.ToDouble(txtPAmt.Text);
        //double TtlPayAmt = 0;
        //double sumofPayBill = 0;
        if (checkbox.Checked == true)
        {
            for (int i = 0; i < GridViewEDPMS_Bill_Details.Rows.Count; i++)
            {

                CheckBox chkrow = (CheckBox)GridViewEDPMS_Bill_Details.Rows[i].FindControl("RowChkAllow");
                if (chkrow.Checked == true)
                {
                    //if (Payamt > invamt)
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Payment Amount is greater than Invoice Amount');", true);
                    //    checkbox.Checked = true;
                    //    txtPAmt.Focus();
                    //}
                    if (ORMamt < Payamt)
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Bill amount is greater than Balance ORM amount');", true);
                        checkbox.Checked = false;
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

        CheckBox chk = (CheckBox)GridViewEDPMS_Bill_Details.HeaderRow.FindControl("HeaderChkAllow");
        int isAllChecked = 0;
        for (int i = 0; i < GridViewEDPMS_Bill_Details.Rows.Count; i++)
        {
            CheckBox chkrow = (CheckBox)GridViewEDPMS_Bill_Details.Rows[i].FindControl("RowChkAllow");
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
    protected void ThirdPartyCB_CheckedChanged(object sender, EventArgs e)
    {
        if (ThirdPartyCB.Checked == true)
        {
            ThirdPartyTR.Visible = true;
        }
        else
        {
            ThirdPartyTR.Visible = false;
            txtThirdPartyID.Text = "";
            lblThirdPartyName.Text = "";
        }
    }
    public void Count()
    {
        double sumofPayBill = 0;
        for (int i = 0; i < GridViewEDPMS_Bill_Details.Rows.Count; i++)
        {
            if (GridViewEDPMS_Bill_Details.Rows.Count > 0)
            {
                TextBox lblinvcamt = new TextBox();
                lblinvcamt = (TextBox)GridViewEDPMS_Bill_Details.Rows[i].Cells[5].FindControl("lblinvcamt");
                CheckBox chkrow = new CheckBox();
                chkrow = (CheckBox)GridViewEDPMS_Bill_Details.Rows[i].FindControl("RowChkAllow");
                if (chkrow.Checked == true)
                {
                    double txtInvAmt = Convert.ToDouble(lblinvcamt.Text);
                    sumofPayBill = sumofPayBill + Convert.ToDouble(lblinvcamt.Text);
                }
            }
        }
        lblTot.Text = sumofPayBill.ToString("F");
    }
}