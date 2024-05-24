using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EXP_AddEditGRCustoms : System.Web.UI.Page
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
                    btnSave.Enabled = false;
                    lblSupervisormsg.Visible = true;
                }
                else
                {
                    btnSave.Enabled = true;
                    lblSupervisormsg.Visible = false;
                }
                txtDocumentNo.Text = Request.QueryString["DocNo"].Trim();
                txtBillAmount.Text = Request.QueryString["Bill_Amt"].Trim();
                hdnBillType.Value = Request.QueryString["DocType"].Trim();
                lblCurrency.Text = Request.QueryString["Currency"].Trim();

                txtAmountGRPP.Attributes.Add("onkeydown", "return validate_Number(event)");
                txtAmountinINRGR.Attributes.Add("onkeydown", "return validate_Number(event)");
                txtBillAmount.Attributes.Add("onkeydown", "return validate_Number(event)");
                txtCommissionGRPP.Attributes.Add("onkeydown", "return validate_Number(event)");
                txtDiscount.Attributes.Add("onkeydown", "return validate_Number(event)");
                txtFreight.Attributes.Add("onkeydown", "return validate_Number(event)");
                txtInsurance.Attributes.Add("onkeydown", "return validate_Number(event)");
                txtInvoiceAmt.Attributes.Add("onkeydown", "return validate_Number(event)");
                txtOthDeduction.Attributes.Add("onkeydown", "return validate_Number(event)");
                txtPacking.Attributes.Add("onkeydown", "return validate_Number(event)");



                btnHelpCommodCode.Attributes.Add("onclick", "return Commodityelp();");

                btnHelpCountryCode.Attributes.Add("onclick", "return OpenCountryList('1');");

                //btnpo.Attributes.Add("onclick", "return PortHelp();");
                btnAddGRPPCustoms.Attributes.Add("onclick", "return chkShippingBillNo();");

                btnSave.Attributes.Add("onclick", "return ValidateSave();");

                ddlCurrencyGRPP.Attributes.Add("onblur", "return AlertGRcurrency();");
                ddlPortCode.Attributes.Add("onblur", "return FillFormType_SDF();");

                btnOverseasPartyList.Attributes.Add("onclick", "return OpenOverseasPartyList('mouseClick');");

                //btnSave.Attributes.Add("onclick", "return confirm('Are you sure you want to Save this record(s)?');");

                txtAsOnDate.Attributes.Add("onblur", "return isValidDate(" + txtAsOnDate.ClientID + "," + "'AWB Date'" + " );");
                txtShippingBillDate.Attributes.Add("onblur", "return isValidDate(" + txtShippingBillDate.ClientID + "," + "'Shipping Bill Date'" + " );");
                txtDueDate.Attributes.Add("onblur", "return isValidDate(" + txtDueDate.ClientID + "," + "'Due Date'" + " );");


                SetInitialRow();
                fillCurrency();
                fillCommodityDescription();
                fillPortCodes();
                fillCountryDescription();

            }
            txtAmountGRPP.Attributes.Add("onblur", "return Calculate();");
            //txtExchRateGR.Attributes.Add("onblur", "return Calculate();");
            txtAmountinINRGR.Attributes.Add("onblur", "return Calculate();");

        }
    }

    protected void fillCurrency()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        ddlCurrencyGRPP.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "-Select-";
            ddlCurrencyGRPP.DataSource = dt.DefaultView;
            ddlCurrencyGRPP.DataTextField = "C_Code";
            ddlCurrencyGRPP.DataValueField = "C_Code";
            ddlCurrencyGRPP.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlCurrencyGRPP.Items.Insert(0, li);

    }

    protected void LinkButtonClick(object sender, System.EventArgs e)
    {
        Button lb = (Button)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                if (gvRow.RowIndex <= dt.Rows.Count - 1)
                {
                    //Remove the Selected Row data
                    dt.Rows.Remove(dt.Rows[rowID]);

                    //if (gvRow.RowIndex <= dt.Rows.Count - 1)
                    //{
                    //    dt.Rows.Remove(dt.Rows[rowID]);
                    //}

                    //else
                    //{
                    //    dt.Rows.Add(dt.Rows[rowID]);
                    //}

                }
            }

            //Store the current data in ViewState for future reference
            ViewState["CurrentTable"] = dt;
            //Re bind the GridView for the updated data
            GridViewGRPPCustomsDetails.DataSource = dt;
            GridViewGRPPCustomsDetails.DataBind();
        }
        CalculateGR_Total();
    }

    /*private void AddNewRowToGrid()
    {
        GridViewGRPPCustomsDetails.Visible = true;
        int rowIndex = 0;
        string _GRcurr = "", _GRportID = "";
        string _GRFormType;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count >= 0)
            {
                for (int i = 0; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    if (GridViewGRPPCustomsDetails.Rows.Count > i)
                    {
                        Label lbl1 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[0].FindControl("lblSrNo");
                        Label lbl2 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[1].FindControl("lblFormType");
                        Label lbl4 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[2].FindControl("lblGR");
                        Label lbl5 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[3].FindControl("lblCurrency");
                        Label lbl6 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[4].FindControl("lblAmount");
                        Label lbl7 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[5].FindControl("lblExchRate");
                        Label lbl8 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[6].FindControl("lblAmountInInr");
                        Label lbl9 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[7].FindControl("lblShippingBillNo");
                        Label lbl10 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[8].FindControl("lblShippingBillDate");
                        Label lbl11 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[9].FindControl("lblCommission");
                        Label lbl12 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[10].FindControl("lblPortCode");

                    }
                    drCurrentRow = dtCurrentTable.NewRow();
                    Label lastSr = new Label();
                    if (GridViewGRPPCustomsDetails.Rows.Count > 0)
                    {
                        lastSr = (Label)GridViewGRPPCustomsDetails.Rows[dtCurrentTable.Rows.Count - 1].Cells[1].FindControl("lblSrNo");
                        if (lastSr.Text == "0")
                            dtCurrentTable.Rows.RemoveAt(0);
                    }
                    else
                        lastSr.Text = 0.ToString();

                    drCurrentRow["SrNo"] = Convert.ToInt32(lastSr.Text) + 1;
                    drCurrentRow["GR"] = txtGRPPCustomsNo.Text;
                    if (ddlCurrencyGRPP.SelectedIndex == 0)
                        _GRcurr = "";
                    else
                        _GRcurr = ddlCurrencyGRPP.SelectedValue;

                    drCurrentRow["GRCurrency"] = _GRcurr;

                    if (txtAmountGRPP.Text == "")
                        txtAmountGRPP.Text = "0";

                    drCurrentRow["Amount"] = txtAmountGRPP.Text;

                    drCurrentRow["Shipping_Bill_No"] = txtShippingBillNo.Text;

                    drCurrentRow["Shipping_Bill_Date"] = txtShippingBillDate.Text;
                    drCurrentRow["Commission"] = txtCommissionGRPP.Text;
                    drCurrentRow["ExchRate"] = txtGRppExchRate.Text;
                    drCurrentRow["AmtinINR"] = txtAmountGRPP_In_INR.Text;
                    //drCurrentRow["PortCode"] = txtPortCodeGrid.Text;

                    //if (ddlFormTypeGrid.SelectedIndex == 0)
                    //    _GRFormType = "";
                    //else
                        _GRFormType = ddlFormTypeGrid.Text;

                    drCurrentRow["FormType"] = _GRFormType;

                    if (ddlPortCodeGrid.SelectedIndex == 0)
                        _GRportID = "";
                    else
                        _GRportID = ddlPortCodeGrid.SelectedValue;

                    drCurrentRow["PortCode"] = _GRportID;

                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtCurrentTable;

                GridViewGRPPCustomsDetails.DataSource = dtCurrentTable;
                GridViewGRPPCustomsDetails.DataBind();
            }
            CalculateGR_Total();
        }
        else
        {
            Response.Write("ViewState is null");
        }
    }

    /*private void SetInitialRow()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter pc1 = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
        pc1.Value = txtDocumentNo.Text.Trim();
        string _query1 = "TF_EXP_GetGRPPCustomsList_XOS";

        DataTable dtPC = objData.getData(_query1, pc1);

        if (dtPC.Rows.Count > 0)
        {
            ViewState["CurrentTable"] = dtPC;
            GridViewGRPPCustomsDetails.DataSource = dtPC;
            GridViewGRPPCustomsDetails.DataBind();

            fillCommodityDescription();
            fillCountryDescription();

        }
        else
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("SrNo", typeof(string)));
            dt.Columns.Add(new DataColumn("GR", typeof(string)));
            dt.Columns.Add(new DataColumn("GRCurrency", typeof(string)));
            dt.Columns.Add(new DataColumn("ExchRate", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt.Columns.Add(new DataColumn("AmtinINR", typeof(string)));
            dt.Columns.Add(new DataColumn("Shipping_Bill_No", typeof(string)));
            dt.Columns.Add(new DataColumn("Shipping_Bill_Date", typeof(string)));
            dt.Columns.Add(new DataColumn("Commission", typeof(string)));
            dt.Columns.Add(new DataColumn("PortCode", typeof(string)));
            dt.Columns.Add(new DataColumn("FormType", typeof(string)));

            dr = dt.NewRow();

            dr["SrNo"] = 0;
            dr["GR"] = string.Empty;
            dr["GRCurrency"] = string.Empty;
            dr["ExchRate"] = string.Empty;
            dr["Amount"] = string.Empty;
            dr["AmtinINR"] = string.Empty;
            dr["Shipping_Bill_No"] = string.Empty;
            dr["Shipping_Bill_Date"] = string.Empty;
            dr["Commission"] = string.Empty;
            dr["PortCode"] = string.Empty;
            dr["FormType"] = string.Empty;

            dt.Rows.Add(dr);
            dr = dt.NewRow();

            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dt;

            GridViewGRPPCustomsDetails.DataSource = dt;
            GridViewGRPPCustomsDetails.DataBind();
            GridViewGRPPCustomsDetails.Visible = false;
        }

        TF_DATA objData1 = new TF_DATA();
        SqlParameter pc2 = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
        pc2.Value = txtDocumentNo.Text.Trim();
        string _query2 = "TF_EXP_GetGRPPCustomsListCommodity_XOS";

        DataTable dtPC1 = objData.getData(_query2, pc2);

        txtCommodityID.Text = dtPC1.Rows[0]["Commodity"].ToString();
        txtAsOnDate.Text = dtPC1.Rows[0]["AWB_Date"].ToString();
        txtDueDate.Text = dtPC1.Rows[0]["Due_Date"].ToString();
        txtCountryCode.Text = dtPC1.Rows[0]["Country_Code"].ToString();



        CalculateGR_Total();

    }*/

    private void SetInitialRow()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter pc1 = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
        pc1.Value = txtDocumentNo.Text.Trim();
        string _query1 = "TF_EXP_GetGRPPCustomsList";

        DataTable dtPC = objData.getData(_query1, pc1);

        if (dtPC.Rows.Count > 0)
        {
            ViewState["CurrentTable"] = dtPC;
            GridViewGRPPCustomsDetails.DataSource = dtPC;
            GridViewGRPPCustomsDetails.DataBind();
        }
        else
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("SrNo", typeof(string)));
            dt.Columns.Add(new DataColumn("FormType", typeof(string)));
            dt.Columns.Add(new DataColumn("ExportAgency", typeof(string)));
            dt.Columns.Add(new DataColumn("DispInd", typeof(string)));
            dt.Columns.Add(new DataColumn("GR", typeof(string)));
            dt.Columns.Add(new DataColumn("GRCurrency", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt.Columns.Add(new DataColumn("ExchRate", typeof(string)));
            dt.Columns.Add(new DataColumn("AmtinINR", typeof(string)));
            dt.Columns.Add(new DataColumn("Shipping_Bill_No", typeof(string)));
            dt.Columns.Add(new DataColumn("Shipping_Bill_Date", typeof(string)));
            dt.Columns.Add(new DataColumn("InvoiceNo", typeof(string)));
            dt.Columns.Add(new DataColumn("InvoiceDate", typeof(string)));
            dt.Columns.Add(new DataColumn("InvoiceAmt", typeof(string)));
            dt.Columns.Add(new DataColumn("FreightAmount", typeof(string)));
            dt.Columns.Add(new DataColumn("InsuranceAmount", typeof(string)));
            dt.Columns.Add(new DataColumn("DiscountAmt", typeof(string)));
            dt.Columns.Add(new DataColumn("Commission", typeof(string)));
            dt.Columns.Add(new DataColumn("OtherDeductionAmt", typeof(string)));
            dt.Columns.Add(new DataColumn("PackingCharges", typeof(string)));
            dt.Columns.Add(new DataColumn("PortCode", typeof(string)));
            ////dt.Columns.Add(new DataColumn("CommodityID", typeof(string)));

            dr = dt.NewRow();
            dr["SrNo"] = 0;
            dr["FormType"] = string.Empty;
            dr["ExportAgency"] = string.Empty;
            dr["DispInd"] = string.Empty;
            dr["GR"] = string.Empty;
            dr["GRCurrency"] = string.Empty;
            dr["Amount"] = string.Empty;
            dr["ExchRate"] = string.Empty;
            dr["AmtinINR"] = string.Empty;
            dr["Shipping_Bill_No"] = string.Empty;
            dr["Shipping_Bill_Date"] = string.Empty;
            dr["InvoiceNo"] = string.Empty;
            dr["InvoiceDate"] = string.Empty;
            dr["InvoiceAmt"] = string.Empty;
            dr["FreightAmount"] = string.Empty;
            dr["InsuranceAmount"] = string.Empty;
            dr["DiscountAmt"] = string.Empty;
            dr["Commission"] = string.Empty;
            dr["OtherDeductionAmt"] = string.Empty;
            dr["PackingCharges"] = string.Empty;
            dr["PortCode"] = string.Empty;
            ////dr["CommodityID"] = string.Empty;

            dt.Rows.Add(dr);
            dr = dt.NewRow();

            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dt;

            GridViewGRPPCustomsDetails.DataSource = dt;
            GridViewGRPPCustomsDetails.DataBind();
            GridViewGRPPCustomsDetails.Visible = false;
        }
        TF_DATA objData1 = new TF_DATA();
        SqlParameter pc2 = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
        pc2.Value = txtDocumentNo.Text.Trim();
        string _query2 = "TF_EXP_GetGRPPCustomsListCommodity_XOS";

        DataTable dtPC1 = objData.getData(_query2, pc2);

        txtCommodityID.Text = dtPC1.Rows[0]["Commodity"].ToString();
        txtAsOnDate.Text = dtPC1.Rows[0]["AWB_Date"].ToString();
        txtDueDate.Text = dtPC1.Rows[0]["Due_Date"].ToString();
        txtCountryCode.Text = dtPC1.Rows[0]["Country_Code"].ToString();
        txtOverseasPartyID.Text = dtPC1.Rows[0]["Overseas_Party_Code"].ToString();
        txtOverseasPartyID_TextChanged(this, null);
        CalculateGR_Total();
    }

    private void AddNewRowToGrid()
    {
        GridViewGRPPCustomsDetails.Visible = true;
        int rowIndex = 0;
        string _GRcurr = "", _GRportID = "";
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count >= 0)
            {
                for (int i = 0; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    if (GridViewGRPPCustomsDetails.Rows.Count > i)
                    {
                        Label lbl1 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[0].FindControl("lblSrNo");
                        Label lblFormType = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[1].FindControl("lblFormType");
                        Label lbl2 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[1].FindControl("lblGR");
                        Label lbl3 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[2].FindControl("lblCurrency");
                        Label lbl4 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[3].FindControl("lblAmount");
                        //Label lblExchRate = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[3].FindControl("lblExchRate");
                        Label lblAmountinINR = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[3].FindControl("lblAmountinINR");
                        Label lbl5 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[4].FindControl("lblShippingBillNo");
                        Label lbl6 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[5].FindControl("lblShippingBillDate");
                        Label lbl7 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[6].FindControl("lblCommission");
                        Label lbl8 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[7].FindControl("lblPortCode");
                        //Label lbl9 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[2].FindControl("lblExpAgency");
                    }
                    drCurrentRow = dtCurrentTable.NewRow();
                    Label lastSr = new Label();
                    if (GridViewGRPPCustomsDetails.Rows.Count > 0)
                    {
                        lastSr = (Label)GridViewGRPPCustomsDetails.Rows[dtCurrentTable.Rows.Count - 1].Cells[1].FindControl("lblSrNo");
                        if (lastSr.Text == "0")
                            dtCurrentTable.Rows.RemoveAt(0);
                    }
                    else
                        lastSr.Text = 0.ToString();

                    drCurrentRow["SrNo"] = Convert.ToInt32(lastSr.Text) + 1;
                    drCurrentRow["GR"] = txtGRPPCustomsNo.Text;
                    drCurrentRow["FormType"] = ddlFormType.SelectedValue;
                    drCurrentRow["ExportAgency"] = ddlExportAgency.SelectedValue;
                    drCurrentRow["DispInd"] = ddlDispachInd.SelectedValue;

                    if (ddlCurrencyGRPP.SelectedIndex == 0)
                        _GRcurr = "";
                    else
                        _GRcurr = ddlCurrencyGRPP.SelectedValue;

                    drCurrentRow["GRCurrency"] = _GRcurr;

                    if (txtAmountGRPP.Text == "")
                        txtAmountGRPP.Text = "0";
                    drCurrentRow["Amount"] = txtAmountGRPP.Text;

                    //if (txtExchRateGR.Text == "")
                    //    txtExchRateGR.Text = "0";
                    //drCurrentRow["ExchRate"] = txtExchRateGR.Text;

                    if (txtAmountinINRGR.Text == "")
                        txtAmountinINRGR.Text = "0";
                    drCurrentRow["AmtinINR"] = txtAmountinINRGR.Text;

                    if (txtFreight.Text == "")
                        txtFreight.Text = "0";

                    if (txtInsurance.Text == "")
                        txtInsurance.Text = "0";

                    if (txtDiscount.Text == "")
                        txtDiscount.Text = "0";

                    if (txtOthDeduction.Text == "")
                        txtOthDeduction.Text = "0";

                    if (txtPacking.Text == "")
                        txtPacking.Text = "0";



                    //hdnGRtotalAmount.Value = (Convert.ToDouble(hdnGRtotalAmount.Value) + Convert.ToDouble(txtAmountGRPP.Text)).ToString();
                    drCurrentRow["Shipping_Bill_No"] = txtShippingBillNo.Text;
                    drCurrentRow["Shipping_Bill_Date"] = txtShippingBillDate.Text;
                    drCurrentRow["InvoiceNo"] = txtInvoiceNum.Text;
                    drCurrentRow["InvoiceDate"] = txtInvoiceDt.Text;
                    drCurrentRow["InvoiceAmt"] = txtInvoiceAmt.Text;
                    drCurrentRow["FreightAmount"] = txtFreight.Text;
                    drCurrentRow["InsuranceAmount"] = txtInsurance.Text;
                    drCurrentRow["DiscountAmt"] = txtDiscount.Text;
                    drCurrentRow["Commission"] = txtCommissionGRPP.Text;
                    drCurrentRow["OtherDeductionAmt"] = txtOthDeduction.Text;
                    drCurrentRow["PackingCharges"] = txtPacking.Text;

                    if (ddlPortCode.SelectedIndex == 0)
                        _GRportID = "";
                    else
                        _GRportID = ddlPortCode.SelectedValue;
                    drCurrentRow["PortCode"] = _GRportID;
                    ////drCurrentRow["CommodityID"] = txtCommodityID.Text;
                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                DataView dv = dtCurrentTable.DefaultView;
                dv.Sort = "SrNo ASC";
                dtCurrentTable = dv.ToTable();
                //txtSrNo.Text = (Convert.ToInt32(dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["SrNo"].ToString()) + 1).ToString();
                ViewState["CurrentTable"] = dtCurrentTable;

                GridViewGRPPCustomsDetails.DataSource = dtCurrentTable;
                GridViewGRPPCustomsDetails.DataBind();
            }
            CalculateGR_Total();
        }
        else
        {
            Response.Write("ViewState is null");
        }
    }

    /*protected void btnGridValues_Click(object sender, EventArgs e)
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
                    Label lblSrNo = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[1].FindControl("lblSrNo");
                    int lcolumncount;
                    lcolumncount = GridViewGRPPCustomsDetails.Columns.Count;
                    if (srNo == lblSrNo.Text)
                    {
                        //Remove the Selected Row data
                        Label lbl1 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[0].FindControl("lblSrNo");
                        Label lbl2 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[1].FindControl("lblFormType");
                        Label lbl4 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[2].FindControl("lblGR");
                        Label lbl5 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[3].FindControl("lblCurrency");
                        Label lbl6 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[4].FindControl("lblAmount");
                        Label lbl7 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[5].FindControl("lblExchRate");
                        Label lbl8 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[6].FindControl("lblAmountInInr");
                        Label lbl9 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[7].FindControl("lblShippingBillNo");
                        Label lbl10 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[8].FindControl("lblShippingBillDate");
                        Label lbl11 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[9].FindControl("lblCommission");
                        Label lbl12 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[10].FindControl("lblPortCode");


                        dt.Rows.Remove(dt.Rows[rowIndex]);

                        if (lbl2.Text != "")
                            ddlFormTypeGrid.Text = lbl2.Text;

                        txtGRPPCustomsNo.Text = lbl4.Text;
                        //if (lbl5.Text != "")
                        //    ddlCurrencyGRPP.Text = lbl5.Text;

                        if (lbl5.Text != "")
                            ddlCurrencyGRPP.SelectedIndex = ddlCurrencyGRPP.Items.IndexOf(ddlCurrencyGRPP.Items.FindByText(lbl5.Text.ToString()));
                        else
                            ddlCurrencyGRPP.SelectedIndex = 0;

                        txtAmountGRPP.Text = lbl6.Text;
                        txtGRppExchRate.Text = lbl7.Text;
                        txtAmountGRPP_In_INR.Text = lbl8.Text;
                        txtShippingBillNo.Text = lbl9.Text;
                        txtShippingBillDate.Text = lbl10.Text;
                        txtCommissionGRPP.Text = lbl11.Text;





                        if (lbl12.Text != "")
                            ddlPortCodeGrid.SelectedIndex = ddlPortCodeGrid.Items.IndexOf(ddlPortCodeGrid.Items.FindByText(lbl12.Text.ToString()));
                        else
                            ddlPortCodeGrid.SelectedIndex = 0;

                    }
                    rowIndex++;
                }
            }

            //Store the current data in ViewState for future reference
            ViewState["CurrentTable"] = dt;
            //Re bind the GridView for the updated data
            GridViewGRPPCustomsDetails.DataSource = dt;
            GridViewGRPPCustomsDetails.DataBind();
        }
    }*/

    protected void btnGridValues_Click(object sender, EventArgs e)
    {
        //Button lb = (Button)sender;
        //GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowIndex = 0;
        string srNo = hdnGridValues.Value;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    Label lblSrNo = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[1].FindControl("lblSrNo");
                    int lcolumncount;
                    lcolumncount = GridViewGRPPCustomsDetails.Columns.Count;
                    if (srNo == lblSrNo.Text)
                    {
                        //Remove the Selected Row data
                        Label lblFormType = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[0].FindControl("lblFormType");
                        Label lbl1 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[1].FindControl("lblSrNo");
                        Label lbl2 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[2].FindControl("lblGR");
                        Label lbl3 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[3].FindControl("lblCurrency");
                        Label lbl4 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[4].FindControl("lblAmount");
                        //Label lblExchRate = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[5].FindControl("lblExchRate");
                        Label lblAmountinINR = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[6].FindControl("lblAmountinINR");
                        Label lbl5 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[7].FindControl("lblShippingBillNo");
                        Label lbl6 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[8].FindControl("lblShippingBillDate");
                        Label lbl7 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[9].FindControl("lblCommission");
                        Label lbl8 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[10].FindControl("lblPortCode");
                        Label lbl9 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[11].FindControl("lblExpAgency");
                        Label lbl10 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[12].FindControl("lblDispInd");
                        Label lbl11 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[13].FindControl("lblInvoiceNo");
                        Label lbl12 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[14].FindControl("lblInvoiceDate");
                        Label lbl13 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[15].FindControl("lblInvoiceAmt");
                        Label lbl14 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[16].FindControl("lblFreightAmt");
                        Label lbl15 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[17].FindControl("lblInsuranceAmt");
                        Label lbl16 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[18].FindControl("lblDiscountAmt");
                        Label lbl17 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[19].FindControl("lblOthDeduction");
                        Label lbl18 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[20].FindControl("lblPacking");
                        dt.Rows.Remove(dt.Rows[rowIndex]);


                        if (lblFormType.Text.Trim() != "")
                        {
                            ddlFormType.SelectedIndex = ddlFormType.Items.IndexOf(ddlFormType.Items.FindByText(lblFormType.Text));
                        }
                        else
                        {
                            ddlFormType.SelectedIndex = 0;
                        }


                        if (lbl9.Text != "")
                        {
                            ddlExportAgency.SelectedIndex = ddlExportAgency.Items.IndexOf(ddlExportAgency.Items.FindByText(lbl9.Text));
                        }
                        else
                        {
                            ddlExportAgency.SelectedIndex = 0;
                        }


                        if (lbl10.Text != "")
                        {
                            ddlDispachInd.SelectedIndex = ddlDispachInd.Items.IndexOf(ddlDispachInd.Items.FindByText(lbl10.Text));
                        }
                        else
                        {
                            ddlDispachInd.SelectedIndex = 0;
                        }

                        txtGRPPCustomsNo.Text = lbl2.Text;

                        if (lbl3.Text != "")
                        {
                            ddlCurrencyGRPP.SelectedIndex = ddlCurrencyGRPP.Items.IndexOf(ddlCurrencyGRPP.Items.FindByText(lbl3.Text));
                        }
                        else
                        {
                            ddlCurrencyGRPP.SelectedIndex = 0;
                        }

                        txtAmountGRPP.Text = lbl4.Text;
                        //txtExchRateGR.Text = lblExchRate.Text;
                        txtAmountinINRGR.Text = lblAmountinINR.Text;
                        txtShippingBillNo.Text = lbl5.Text;
                        txtShippingBillDate.Text = lbl6.Text;
                        txtInvoiceNum.Text = lbl11.Text;
                        txtInvoiceDt.Text = lbl12.Text;
                        txtInvoiceAmt.Text = lbl13.Text;
                        txtFreight.Text = lbl14.Text;
                        txtInsurance.Text = lbl15.Text;
                        txtDiscount.Text = lbl16.Text;
                        txtCommissionGRPP.Text = lbl7.Text;
                        txtOthDeduction.Text = lbl17.Text;
                        txtPacking.Text = lbl18.Text;


                        if (lbl8.Text.Trim() != "")
                        {
                            ddlPortCode.SelectedIndex = ddlPortCode.Items.IndexOf(ddlPortCode.Items.FindByText(lbl8.Text));
                            //ddlPortCode.Text = lbl8.Text;
                        }
                        else
                            ddlPortCode.SelectedIndex = 0;

                    }
                    rowIndex++;
                }
            }

            //Store the current data in ViewState for future reference
            ViewState["CurrentTable"] = dt;
            //Re bind the GridView for the updated data
            GridViewGRPPCustomsDetails.DataSource = dt;
            GridViewGRPPCustomsDetails.DataBind();
        }
    }

    protected void fillPortCodes()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetPortCodeMasterList";
        DataTable dt = objData.getData(_query, p1);
        ddlPortCode.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlPortCode.DataSource = dt.DefaultView;
            ddlPortCode.DataTextField = "port_Code";
            ddlPortCode.DataValueField = "port_Code";
            ddlPortCode.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlPortCode.Items.Insert(0, li);

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Exp_ViewGrDetails.aspx", true);
    }

    protected void GridViewGRPPCustomsDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerialNumber = new Label();
            Button btnDelete = new Button();
            lblSerialNumber = (Label)e.Row.FindControl("lblSrNo");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                if (i != 23)
                    cell.Attributes.Add("onclick", "return gridClicked(" + lblSerialNumber.Text + ");");
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }

    protected void btnAddGRPPCustoms_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();

        txtGRPPCustomsNo.Text = "";
        //txtCurrencyGRPP.Text = "";
        ddlCurrencyGRPP.SelectedIndex = 0;
        txtAmountGRPP.Text = "";
        txtShippingBillDate.Text = "";
        txtShippingBillNo.Text = "";
        txtCommissionGRPP.Text = "";
        ddlPortCode.SelectedIndex = 0;
        ddlFormType.SelectedIndex = 0;
        //txtExchRateGR.Text = "";
        txtAmountinINRGR.Text = "";
        txtInvoiceAmt.Text = "";
        txtInvoiceDt.Text = "";
        txtInvoiceNum.Text = "";
        txtFreight.Text = "";
        txtInsurance.Text = "";
        txtDiscount.Text = "";
        txtOthDeduction.Text = "";
        txtPacking.Text = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Boolean _proceed = true;
        string _script;
        string _query = "";
        string _result = "";

        TF_DATA objCheck = new TF_DATA();
        string _queryCheck = "TF_EXP_CheckShippingBillNo";
        SqlParameter cShippingBillNo = new SqlParameter("@shippingBillNo", SqlDbType.VarChar);
        SqlParameter cDocumentNo = new SqlParameter("@documentNo", SqlDbType.VarChar);
        SqlParameter cinvoiceno = new SqlParameter("@invoiceno", SqlDbType.VarChar);
        cDocumentNo.Value = txtDocumentNo.Text;

        if (GridViewGRPPCustomsDetails.Rows.Count >= 0)
        {
            for (int i = 0; i < GridViewGRPPCustomsDetails.Rows.Count; i++)
            {
                Label lbl5 = (Label)GridViewGRPPCustomsDetails.Rows[i].Cells[4].FindControl("lblShippingBillNo");
                cShippingBillNo.Value = lbl5.Text;
                Label lblinvoice = (Label)GridViewGRPPCustomsDetails.Rows[i].Cells[11].FindControl("lblInvoiceNo");
                cinvoiceno.Value = lblinvoice.Text;
                if (lbl5.Text != "")
                {
                    DataTable dtcheck = objCheck.getData(_queryCheck, cShippingBillNo, cDocumentNo, cinvoiceno);
                    if (dtcheck.Rows.Count > 0)
                    {
                        _proceed = false;
                        _script = "alert('Shipping Bill No: " + lbl5.Text + " is assigned to " + dtcheck.Rows[0]["Document_No"].ToString() + "')";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                        // txtRefNo.Focus();
                    }
                }
            }
        }

        if (_proceed == true)
        {
            int rowIndex = 0;

            TF_DATA objSaveGRCustomsDetails = new TF_DATA();

            SqlParameter pGRbCode = new SqlParameter("@bCode", SqlDbType.VarChar);
            SqlParameter pGRDocNo = new SqlParameter("@documentNo", SqlDbType.VarChar);
            SqlParameter pGRno = new SqlParameter("@GR", SqlDbType.VarChar);
            SqlParameter pGRCurrency = new SqlParameter("@currency", SqlDbType.VarChar);
            SqlParameter pGRAmount = new SqlParameter("@amount", SqlDbType.VarChar);
            SqlParameter pGRShippingBillNo = new SqlParameter("@shippingBillNo", SqlDbType.VarChar);
            SqlParameter pGRShippingBillDate = new SqlParameter("@shippingBillDate", SqlDbType.VarChar);
            SqlParameter pGRCommission = new SqlParameter("@commission", SqlDbType.VarChar);
            SqlParameter pGRPortCode = new SqlParameter("@portCode", SqlDbType.VarChar);
            SqlParameter pGRPFormType = new SqlParameter("@formtype", SqlDbType.VarChar);
            SqlParameter pGRPCommodity = new SqlParameter("@commodity", SqlDbType.VarChar);
            SqlParameter pGRPAmtInINR = new SqlParameter("@amount_in_inr", SqlDbType.VarChar);
            SqlParameter pGRPExchRate = new SqlParameter("@exchrate", SqlDbType.VarChar);
            SqlParameter pGRPAWB_Date = new SqlParameter("@AWB_Date", SqlDbType.VarChar);
            SqlParameter pGRPDUE_Date = new SqlParameter("@Due_Date", SqlDbType.VarChar);
            SqlParameter pGRPCountryCode = new SqlParameter("@CountryCode", SqlDbType.VarChar);
            SqlParameter pGRexpagency = new SqlParameter("@expAgency", SqlDbType.VarChar);
            SqlParameter pGRdispind = new SqlParameter("@dispind", SqlDbType.VarChar);
            SqlParameter pGRinvoiceno = new SqlParameter("@invoicenum", SqlDbType.VarChar);
            SqlParameter pGRinvoiceDate = new SqlParameter("@invoicedt", SqlDbType.VarChar);
            SqlParameter pGRinvoiceAmt = new SqlParameter("@invoiceamt", SqlDbType.VarChar);

            SqlParameter pGRFreightAmt = new SqlParameter("@freight", SqlDbType.VarChar);
            SqlParameter pGRInsuranceAmt = new SqlParameter("@insurance", SqlDbType.VarChar);
            SqlParameter pGRDiscountAmt = new SqlParameter("@discount", SqlDbType.VarChar);
            SqlParameter pGRDeductionAmt = new SqlParameter("@deduction", SqlDbType.VarChar);
            SqlParameter pGRPackingAmt = new SqlParameter("@packing", SqlDbType.VarChar);
            SqlParameter pPartyID = new SqlParameter("@party", SqlDbType.VarChar);
            pPartyID.Value = txtOverseasPartyID.Text;
            pGRPCommodity.Value = txtCommodityID.Text;
            pGRPAWB_Date.Value = txtAsOnDate.Text;
            pGRDocNo.Value = txtDocumentNo.Text;
            pGRPDUE_Date.Value = txtDueDate.Text;
            pGRPCountryCode.Value = txtCountryCode.Text;


            if (GridViewGRPPCustomsDetails.Visible == true)
            {
                _query = "TF_EXP_DeleteGRPPCustomsDetails_Xos";
                pGRDocNo.Value = txtDocumentNo.Text;
                TF_DATA objDel = new TF_DATA();
                objDel.SaveDeleteData(_query, pGRDocNo);

                _query = "TF_EXP_UpdateGRPPCustomsDetails_XOS";

                pGRbCode.Value = Request.QueryString["BranchName"];

                rowIndex = 0;

                labelMessage.Text = GridViewGRPPCustomsDetails.Rows.Count.ToString();

                for (int i = 1; i <= GridViewGRPPCustomsDetails.Rows.Count; i++)
                {
                    //extract the TextBox values
                    if (GridViewGRPPCustomsDetails.Rows.Count > 0)
                    {
                        Label lbl1 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[0].FindControl("lblSrNo");
                        Label lbl2 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[1].FindControl("lblFormType");
                        ////Label lbl3 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[2].FindControl("lblCommodity");
                        Label lbl4 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[3].FindControl("lblGR");
                        Label lbl5 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[4].FindControl("lblCurrency");
                        Label lbl6 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[5].FindControl("lblAmount");
                        //Label lbl7 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[6].FindControl("lblExchRate");
                        Label lbl8 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[7].FindControl("lblAmountInInr");
                        Label lbl9 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[8].FindControl("lblShippingBillNo");
                        Label lbl10 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[9].FindControl("lblShippingBillDate");
                        Label lbl11 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[10].FindControl("lblCommission");
                        Label lbl12 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[11].FindControl("lblPortCode");

                        Label lbl13 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[12].FindControl("lblExpAgency");
                        Label lbl14 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[13].FindControl("lblDispInd");
                        Label lbl15 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[14].FindControl("lblInvoiceNo");
                        Label lbl16 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[15].FindControl("lblInvoiceDate");
                        Label lbl17 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[16].FindControl("lblInvoiceAmt");

                        Label lbl18 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[16].FindControl("lblFreightAmt");
                        Label lbl19 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[17].FindControl("lblInsuranceAmt");
                        Label lbl20 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[18].FindControl("lblDiscountAmt");
                        Label lbl21 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[19].FindControl("lblOthDeduction");
                        Label lbl22 = (Label)GridViewGRPPCustomsDetails.Rows[rowIndex].Cells[20].FindControl("lblPacking");


                        pGRPFormType.Value = lbl2.Text;
                        ////pGRPCommodity.Value = lbl3.Text;
                        pGRno.Value = lbl4.Text;
                        pGRCurrency.Value = lbl5.Text;
                        pGRAmount.Value = lbl6.Text;
                        //pGRPExchRate.Value = lbl7.Text;
                        pGRPAmtInINR.Value = lbl8.Text;
                        pGRShippingBillNo.Value = lbl9.Text;
                        pGRShippingBillDate.Value = lbl10.Text;
                        pGRCommission.Value = lbl11.Text;
                        pGRPortCode.Value = lbl12.Text;
                        pGRexpagency.Value = lbl13.Text;
                        pGRdispind.Value = lbl14.Text;
                        pGRinvoiceno.Value = lbl15.Text;
                        pGRinvoiceDate.Value = lbl16.Text;
                        pGRinvoiceAmt.Value = lbl17.Text;
                        pGRFreightAmt.Value = lbl18.Text;
                        pGRInsuranceAmt.Value = lbl19.Text;
                        pGRDiscountAmt.Value = lbl20.Text;
                        pGRDeductionAmt.Value = lbl21.Text;
                        pGRPackingAmt.Value = lbl22.Text;


                    }

                    _result = objSaveGRCustomsDetails.SaveDeleteData(_query, pGRbCode, pGRDocNo, pGRno, pGRCurrency, pGRAmount,
                                    pGRShippingBillNo, pGRShippingBillDate, pGRCommission, pGRPortCode, pGRPFormType, pGRPCommodity, pGRPAmtInINR, 
                                    pGRexpagency, pGRdispind, pGRinvoiceno, pGRinvoiceDate, pGRinvoiceAmt,
                                    pGRFreightAmt, pGRInsuranceAmt, pGRDiscountAmt, pGRDeductionAmt, pGRPackingAmt,
                                    pGRPAWB_Date, pGRPDUE_Date, pGRPCountryCode);
                    rowIndex++;

                }
            }
        }

        if (GridViewGRPPCustomsDetails.Visible == false)
        {
            _query = "TF_EXP_Update_Commodity_GRPPCustomsDetails_XOS";

            TF_DATA objSaveGRCustomsDetails1 = new TF_DATA();

            SqlParameter pGRPCommodity1 = new SqlParameter("@commodity", SqlDbType.VarChar);
            SqlParameter pGRPAWB_Date1 = new SqlParameter("@AWB_Date", SqlDbType.VarChar);
            SqlParameter pGRPDUE_Date1 = new SqlParameter("@Due_Date", SqlDbType.VarChar);
            SqlParameter pGRPCountryCode1 = new SqlParameter("@CountryCode", SqlDbType.VarChar);
            SqlParameter pGRDocNo1 = new SqlParameter("@documentNo", SqlDbType.VarChar);

            pGRPCommodity1.Value = txtCommodityID.Text;
            pGRPAWB_Date1.Value = txtAsOnDate.Text;
            pGRDocNo1.Value = txtDocumentNo.Text;
            pGRPDUE_Date1.Value = txtDueDate.Text;
            pGRPCountryCode1.Value = txtCountryCode.Text;

            _result = objSaveGRCustomsDetails1.SaveDeleteData(_query, pGRDocNo1, pGRPCommodity1, pGRPAWB_Date1, pGRPDUE_Date1, pGRPCountryCode1);

        }

        if (GridViewGRPPCustomsDetails.Rows.Count == 0)
        {
            _script = "window.location='Exp_ViewGrDetails.aspx?result=updated'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (_result == "updated")
            {
                _script = "window.location='Exp_ViewGrDetails.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
                labelMessage.Text = _result;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Exp_ViewGrDetails.aspx", true);
    }

    protected void txtCommodityID_TextChanged(object sender, EventArgs e)
    {
        fillCommodityDescription();
    }

    private void CalculateGR_Total()
    {
        hdnGRtotalAmount.Value = "0";
        if (GridViewGRPPCustomsDetails.Rows.Count > 0)
        {
            Label lblSrNo = (Label)GridViewGRPPCustomsDetails.Rows[0].Cells[0].FindControl("lblSrNo");
            if (lblSrNo.Text != "0")
            {
                for (int i = 0; i < GridViewGRPPCustomsDetails.Rows.Count; i++)
                {
                    Label lbl4 = (Label)GridViewGRPPCustomsDetails.Rows[i].Cells[3].FindControl("lblAmount");
                    if (lbl4.Text == "")
                        lbl4.Text = "0";

                    hdnGRtotalAmount.Value = (Convert.ToDouble(hdnGRtotalAmount.Value) + Convert.ToDouble(lbl4.Text)).ToString();

                }
            }
        }
    }

    private void fillCommodityDescription()
    {
        lblCommodity.Text = "";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@CommodityID", SqlDbType.VarChar);
        p1.Value = txtCommodityID.Text;
        string _query = "TF_GetCommodityMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCommodity.Text = dt.Rows[0]["CommodityDescription"].ToString().Trim();
            if (lblCommodity.Text.Length > 60)
            {
                lblCommodity.ToolTip = lblCommodity.Text;
                lblCommodity.Text = lblCommodity.Text.Substring(0, 60) + "...";
            }
        }
        else
        {
            txtCommodityID.Text = "";
            lblCommodity.Text = "";
        }
    }


    private void fillCountryDescription()
    {
        lblCountryCode.Text = "";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@CountryId", SqlDbType.VarChar);
        p1.Value = txtCountryCode.Text;
        string _query = "TF_Country_Name";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCountryCode.Text = dt.Rows[0]["CountryName"].ToString().Trim();
            if (lblCountryCode.Text.Length > 60)
            {
                lblCountryCode.ToolTip = lblCountryCode.Text;
                lblCountryCode.Text = lblCountryCode.Text.Substring(0, 60) + "...";
            }
        }
        else
        {
            txtCountryCode.Text = "";
            lblCountryCode.Text = "";
        }
    }
    protected void txtCountryCode_TextChanged(object sender, EventArgs e)
    {
        fillCountryDescription();
    }

    protected void btnOverseasParty_Click(object sender, EventArgs e)
    {
        if (hdnOverseasPartyId.Value != "")
        {
            txtOverseasPartyID.Text = hdnOverseasPartyId.Value;
            fillOverseasPartyDescription();
            txtOverseasPartyID.Focus();
        }
    }

    private void fillOverseasPartyDescription()
    {
        lblOverseasPartyDesc.Text = "";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@partyID", SqlDbType.VarChar);
        p1.Value = txtOverseasPartyID.Text;
        //SqlParameter p2 = new SqlParameter("@type", SqlDbType.VarChar);
        //p2.Value = "EXPORT";
        string _query = "TF_GetOverseasPartyMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblOverseasPartyDesc.Text = dt.Rows[0]["Party_Name"].ToString().Trim();
            if (lblOverseasPartyDesc.Text.Length > 20)
            {
                lblOverseasPartyDesc.ToolTip = lblOverseasPartyDesc.Text;
                lblOverseasPartyDesc.Text = lblOverseasPartyDesc.Text.Substring(0, 20) + "...";
            }
        }
        else
        {
            txtOverseasPartyID.Text = "";
            lblOverseasPartyDesc.Text = "";
        }

    }

    protected void txtOverseasPartyID_TextChanged(object sender, EventArgs e)
    {
        fillOverseasPartyDescription();
    }

}