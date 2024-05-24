using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class IDPMS_IDPMS_ManualBOEAddEdit : System.Web.UI.Page
{
    Boolean _proceed = true;
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
                txtDocDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                if (Request.QueryString["mode"] != "Add")
                {
                    //txtDocumentNo.Text = Request.QueryString["DocNo"].Trim();
                    fillGrid();
                    fillDetails(Request.QueryString["DocNo"]);
                    // txtBranch.Text = Request.QueryString["Branch"];
                    // txtDocPrFx.Text = Request.QueryString["DocNo"].ToString().Substring(0, 3);
                    txtDocumentNo.Text = Request.QueryString["DocNo"].ToString();//.Substring(3, 6);
                    //  txtYear.Text = Request.QueryString["DocNo"].ToString().Substring(9, 4);

                }
                else
                {
                    txtDocumentNo.Text = Request.QueryString["DocPrfx"].ToString() + Request.QueryString["Docno"].ToString() + Request.QueryString["Year"].ToString();
                    hdnYear.Value = Request.QueryString["Year"];

                    //btnHelp_DocNo.Visible = true;
                    txtBranch.Text = Request.QueryString["Branch"];
                }

                hdnBranch.Value = Request.QueryString["Branch"];
                //txtDocPrFx.Text = Request.QueryString["DocPrfx"].ToString();
                //txtYear.Text = Request.QueryString["Year"].ToString();
                //txtDocumentNo.Text = Request.QueryString["Docno"].ToString().Substring(3, 6);
                txtbilldate.Attributes.Add("onblur", "return isValidDate(" + txtbilldate.ClientID + "," + "'Shipping Bill Date'" + " );");
                btnHelp_DocNo.Attributes.Add("onclick", "return HelpDocNo();");
                //Help_btnBillNo.Attributes.Add("onclick", "return HelpShippNo();");
                btnAddGRPPCustoms.Attributes.Add("onclick", "return ValidateAdd();");
                btnSave.Attributes.Add("onclick", "return ValidateSave();");
                btnportdschrg.Attributes.Add("onclick", "return PortHelp();");
                Button1.Attributes.Add("onclick", "return openCurrency();");
                txtinvcamt.Attributes.Add("onkeydown", "return validate_Number(event);");
                btnPartyID.Attributes.Add("onclick", "return Cust_Help();");
                //btnOverseasParty.Attributes.Add("onclick", "return OverseasPartyHelp();");

                SetInitialRow();
            }
            txtPartyID.Focus();
        }
    }




    private void fillDetails(string DocNo)
    {
        TF_DATA obj = new TF_DATA();
        string query = "TF_IDPMS_ManualBOE_FillDetails";
        SqlParameter p1 = new SqlParameter("@DocNo", SqlDbType.VarChar);
        p1.Value = DocNo;
        DataTable dt = obj.getData(query, p1);
        if (dt.Rows.Count > 0)
        {
            //txtDocumentNo.Text = dt.Rows[0]["DocNo"].ToString();
            txtPartyID.Text = dt.Rows[0]["IECODE"].ToString();
            ddImpAgency.SelectedValue = dt.Rows[0]["Import_Agency"].ToString();
            txtbillno.Text = dt.Rows[0]["Bill_No"].ToString();
            txtbilldate.Text = dt.Rows[0]["Bill_Date"].ToString();
            txtportchgs.Text = dt.Rows[0]["PortCode"].ToString();
            txtshipmentport.Text = dt.Rows[0]["Port_Of_Ship"].ToString();
            txtBranch.Text = dt.Rows[0]["AdCode"].ToString();
            //new additionss
            txtOverseasParty.Text = dt.Rows[0]["Party_Code"].ToString();
            txtadd.Text = dt.Rows[0]["Party_Address"].ToString();
            txtPartyCon.Text = dt.Rows[0]["Party_Country"].ToString();
            fillGrid();
        }
    }

    protected void fillGrid()
    {
        //getLastDocNo();
        //string search = txtSearch.Text.Trim();
        //SqlParameter p1 = new SqlParameter("@branchCode", ddlbranch.SelectedValue);
        //SqlParameter p2 = new SqlParameter("@IRMNumber", "");
        //SqlParameter p3 = new SqlParameter("@mode", "");
        SqlParameter p4 = new SqlParameter("@DocumentNo", txtDocumentNo.Text);
        SqlParameter p5 = new SqlParameter("@BillNo", txtbillno.Text);
        //SqlParameter p5 = new SqlParameter("@year", txtYear.Text);
        string _query = "TF_IDPMS_FillBOEGrid";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p4, p5);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            //int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            //GridViewEDPMS_Bill_Details.PageSize = _pageSize;
            GridViewEDPMS_Bill_Details.DataSource = dt.DefaultView;
            GridViewEDPMS_Bill_Details.DataBind();
            GridViewEDPMS_Bill_Details.Visible = true;
            //rowGrid.Visible = true;
            //rowPager.Visible = true;
            //lblMessage.Visible = false;
            //pagination(_records, _pageSize);
        }
        else
        {
            GridViewEDPMS_Bill_Details.Visible = false;
            //rowGrid.Visible = false;
            //rowPager.Visible = false;
            //lblMessage.Text = "No record(s) found.";
            //lblMessage.Visible = true;
        }
    }


    private void SetInitialRow()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter pc1 = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
        pc1.Value = txtDocumentNo.Text.Trim();
        SqlParameter pc2 = new SqlParameter("@BillNo", SqlDbType.VarChar);
        pc2.Value = txtbillno.Text.Trim();
        string _query1 = "TF_IDPMS_FillBOEGrid";

        DataTable dtPC = objData.getData(_query1, pc1, pc2);

        if (dtPC.Rows.Count > 0)
        {
            ViewState["CurrentTable"] = dtPC;
            GridViewEDPMS_Bill_Details.DataSource = dtPC;
            GridViewEDPMS_Bill_Details.DataBind();
            GridViewEDPMS_Bill_Details.Visible = true;
        }
        else
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            // dt.Columns.Add(new DataColumn("SrNo", typeof(string)));
            dt.Columns.Add(new DataColumn("InvoiceSerialNo", typeof(string)));
            dt.Columns.Add(new DataColumn("TermsofInvoice", typeof(string)));
            dt.Columns.Add(new DataColumn("InvoiceNo", typeof(string)));
            dt.Columns.Add(new DataColumn("remittanceCurrency", typeof(string)));
            dt.Columns.Add(new DataColumn("invoiceAmt", typeof(string)));


            dr = dt.NewRow();

            //  dr["SrNo"] = 0;
            dr["InvoiceSerialNo"] = string.Empty;
            dr["TermsofInvoice"] = string.Empty;
            dr["InvoiceNo"] = string.Empty;
            dr["remittanceCurrency"] = string.Empty;
            dr["invoiceAmt"] = string.Empty;



            dt.Rows.Add(dr);
            dr = dt.NewRow();

            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dt;

            GridViewEDPMS_Bill_Details.DataSource = dt;
            GridViewEDPMS_Bill_Details.DataBind();
            GridViewEDPMS_Bill_Details.Visible = false;
        }

        //CalculateGR_Total();

    }

    protected void btnGridValues_Click(object sender, EventArgs e)
    {
        int rowIndex = 0;
        string srNo = hdnGridValues.Value;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    Label lblSrNo = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[1].FindControl("lblinvoiesrno");
                    int lcolumncount;
                    lcolumncount = GridViewEDPMS_Bill_Details.Columns.Count;
                    if (srNo == lblSrNo.Text)
                    {
                        //Remove the Selected Row data
                        //Label lbl1 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[0].FindControl("lblSrNo");
                        Label lbl2 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[0].FindControl("lblinvoiesrno");
                        Label lbl4 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[1].FindControl("lblinvcterm");
                        Label lbl5 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[2].FindControl("lblinvcno");
                        Label lbl6 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[3].FindControl("lblinvcCur");
                        Label lbl7 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[4].FindControl("lblinvcamt");


                        dt.Rows.Remove(dt.Rows[rowIndex]);

                        txtinvocesrNo.Text = lbl2.Text;
                        txtinvoiceterm.Text = lbl4.Text;
                        txtinvcno.Text = lbl5.Text;
                        txtinvcCur.Text = lbl6.Text;
                        txtinvcamt.Text = lbl7.Text;


                    }
                    rowIndex++;
                }
            }

            //Store the current data in ViewState for future reference
            ViewState["CurrentTable"] = dt;
            //Re bind the GridView for the updated data
            GridViewEDPMS_Bill_Details.DataSource = dt;
            GridViewEDPMS_Bill_Details.DataBind();
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("IDPMS_ManualBOE_View.aspx", true);
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
            foreach (TableCell cell in e.Row.Cells)
            {
                if (i != 4)
                    cell.Attributes.Add("onclick", "return gridClicked(" + lblSerialNumber.Text + ");");
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
            //}
        }
    }


    protected void btnAddGRPPCustoms_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
        DataRow drCurrentRow = null;
        txtinvcamt.Text = "";
        txtinvcCur.Text = "";
        txtinvcno.Text = "";
        txtinvocesrNo.Text = "";
        txtinvoiceterm.Text = "";

        //for (int i = 0; i <= dtCurrentTable.Rows.Count; i++)
        //{
        //    drCurrentRow = dtCurrentTable.NewRow();
        //    if (GridViewEDPMS_Bill_Details.Rows.Count > 0)
        //    {
        //        txtinvocesrNo.Text = (Convert.ToInt32(dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["Sr_No"].ToString()) + 1).ToString();
        //    }
        //}
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        TF_DATA obj = new TF_DATA();

        string query = "TF_IDPMSAddEdit_BOE";
        string result = "";

        string NewValue = "Document No:" + txtDocumentNo.Text + ";Document Date:" + txtDocDate.Text +
            ";Customer ID:" + txtPartyID.Text;
        SqlParameter p1 = new SqlParameter("@branch", txtBranch.Text);
        SqlParameter p2 = new SqlParameter("@DocNo", txtDocumentNo.Text);
        SqlParameter p3 = new SqlParameter("@CustID", txtPartyID.Text);
        SqlParameter p4 = new SqlParameter("@ImportAgency", ddImpAgency.SelectedValue);
        SqlParameter p5 = new SqlParameter("@BillNo", txtbillno.Text);
        SqlParameter p6 = new SqlParameter("@BillDate", txtbilldate.Text);
        SqlParameter p7 = new SqlParameter("@port", txtportchgs.Text);
        SqlParameter p8 = new SqlParameter("@ShippmentPort", txtshipmentport.Text);
        SqlParameter p17 = new SqlParameter("@DocDate", txtDocDate.Text);

        //party details
        SqlParameter p18 = new SqlParameter("@Party_Code", txtOverseasParty.Text);
        SqlParameter p19 = new SqlParameter("@Party_Address", txtadd.Text);
        SqlParameter p20 = new SqlParameter("@Party_Country", txtPartyCon.Text);

        SqlParameter mode = new SqlParameter("@Mode", Request.QueryString["mode"]);

        if (GridViewEDPMS_Bill_Details.Visible == false)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Enter ADD To BOE Adjusted');", true);
            SetFocus(btnAddGRPPCustoms);
            return;
        }

        for (int i = 0; i < GridViewEDPMS_Bill_Details.Rows.Count; i++)
        {
            //Label lbl1 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[0].FindControl("lblSrNo");
            Label lbl1 = (Label)GridViewEDPMS_Bill_Details.Rows[i].FindControl("lblinvoiesrno");
            Label lbl2 = (Label)GridViewEDPMS_Bill_Details.Rows[i].FindControl("lblinvcterm");
            Label lbl3 = (Label)GridViewEDPMS_Bill_Details.Rows[i].FindControl("lblinvcno");
            Label lbl4 = (Label)GridViewEDPMS_Bill_Details.Rows[i].FindControl("lblinvcCur");
            Label lbl5 = (Label)GridViewEDPMS_Bill_Details.Rows[i].FindControl("lblinvcamt");

            SqlParameter p9 = new SqlParameter("@invoicesrno", lbl1.Text);
            SqlParameter p10 = new SqlParameter("@invoiceterm", lbl2.Text);
            SqlParameter p11 = new SqlParameter("@invoiceNo", lbl3.Text);
            SqlParameter p12 = new SqlParameter("@invoicecur", lbl4.Text);
            SqlParameter p13 = new SqlParameter("@invoiceamt", lbl5.Text);
            SqlParameter p14 = new SqlParameter("@addedby", Session["userName"].ToString());
            SqlParameter p15 = new SqlParameter("@addeddate", System.DateTime.Now.ToString());
            SqlParameter p16 = new SqlParameter("@Flag", "M");

            NewValue = NewValue + ";Bill Of Entry No:" + txtbillno.Text + ";Bill Of Entry Date:" + txtbilldate.Text +
                               ";Invoice Number:" + lbl3.Text + ";Invoice Amt:" + lbl5.Text;

            result = obj.SaveDeleteData(query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, mode, p18, p19, p20);
        }
        string _script = "";
        if (result == "Save")
        {

            #region AUDIT TRAIL LOGIC
            SqlParameter Q1 = new SqlParameter("@Adcode", txtBranch.Text);
            SqlParameter Q2 = new SqlParameter("@OldValues", "");
            SqlParameter Q3 = new SqlParameter("@NewValues", NewValue);
            SqlParameter Q4 = new SqlParameter("@CustAcNo", txtPartyID.Text);
            SqlParameter Q5 = new SqlParameter("@DocumentNo", txtDocumentNo.Text);
            SqlParameter Q6 = new SqlParameter("@DocumentDate", txtDocDate.Text);
            SqlParameter Q7 = new SqlParameter("@Mode", "A");
            SqlParameter Q8 = new SqlParameter("@ModifiedBy", Session["userName"].ToString());
            SqlParameter Q9 = new SqlParameter("@MenuName", "Bill Of Entry - Manual Port Data Entry");

            result = obj.SaveDeleteData("TF_IDPMS_Audit_Trail_Add", Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9);
            #endregion

            //string docno = result.Substring(5);
            _script = "window.location='IDPMS_ManualBOE_View.aspx?result=added'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }

        //if (result == "Update")
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record updated.');", true);
        //    _script = "window.location='IDPMS_ManualBOE_View.aspx?result=upddated'";
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);

        //    //_script = "window.location='AD_View_EXP_Voucher.aspx?result=upddated'";
        //    //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        //}
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }


    private void AddNewRowToGrid()
    {
        GridViewEDPMS_Bill_Details.Visible = true;
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count >= 0)
            {
                double Total_Amt = 0;
                double Total_Credit = 0;
                double net_pay = 0;
                for (int i = 0; i <= dtCurrentTable.Rows.Count; i++)
                {
                    drCurrentRow = dtCurrentTable.NewRow();
                    Label lastSr = new Label();
                    if (GridViewEDPMS_Bill_Details.Rows.Count > i)
                    {
                        Label lbl2 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[0].FindControl("lblinvoiesrno");
                        Label lbl4 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[1].FindControl("lblinvcterm");
                        Label lbl5 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[2].FindControl("lblinvcno");
                        Label lbl6 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[3].FindControl("lblinvcCur");
                        Label lbl7 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[4].FindControl("lblinvcamt");
                    }


                    if (GridViewEDPMS_Bill_Details.Rows.Count > 0)
                    {
                        lastSr = (Label)GridViewEDPMS_Bill_Details.Rows[dtCurrentTable.Rows.Count - 1].Cells[1].FindControl("lblinvoiesrno");
                        if (lastSr.Text == "")
                        {
                            dtCurrentTable.Rows.RemoveAt(0);
                        }
                    }

                    else
                    { }
                    drCurrentRow["InvoiceSerialNo"] = txtinvocesrNo.Text;
                    drCurrentRow["TermsofInvoice"] = txtinvoiceterm.Text;
                    drCurrentRow["InvoiceNo"] = txtinvcno.Text;
                    drCurrentRow["invoiceAmt"] = txtinvcamt.Text;
                    drCurrentRow["remittanceCurrency"] = txtinvcCur.Text;


                }
                dtCurrentTable.Rows.Add(drCurrentRow);

                ViewState["CurrentTable"] = dtCurrentTable;

                GridViewEDPMS_Bill_Details.DataSource = dtCurrentTable;
                GridViewEDPMS_Bill_Details.DataBind();


            }
        }

        else
        {
            Response.Write("ViewState is null");
        }
    }


    protected void txtPartyID_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@customerACNo", SqlDbType.VarChar);
        p1.Value = txtPartyID.Text.Trim();

        SqlParameter p2 = new SqlParameter("@branch", SqlDbType.VarChar);
        p2.Value = txtBranch.Text.Trim();
        string _query = "TF_GetCustomerMasterDetails1";
        string _query2 = "TF_GetCustMasterPanNo";

        DataTable dt = objData.getData(_query, p1, p2);

        DataTable dt2 = objData.getData(_query2, p1, p2);
        if (dt.Rows.Count > 0)
        {
            if (dt2.Rows[0]["CUST_PAN_NO"].ToString() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Update Pan No. in Customer Master.');", true);
            }
        }
        if (dt.Rows.Count > 0)
        {
            lblcustnm.Text = dt.Rows[0]["CUST_NAME"].ToString();
        }
        else
        {
            lblcustnm.Text = "";
            txtPartyID.Text = "";

        }

    }
}