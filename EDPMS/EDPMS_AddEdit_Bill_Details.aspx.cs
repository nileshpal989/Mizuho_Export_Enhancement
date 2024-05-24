using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EDPMS_EDPMS_AddEdit_Bill_Details : System.Web.UI.Page
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
                if (Request.QueryString["mode"] != "Add")
                {
                    txtDocumentNo.Text = Request.QueryString["DocNo"].Trim();
                    txtBillAmount.Text = Request.QueryString["Bill_Amt"].Trim();
                    lblCurrency.Text = Request.QueryString["Currency"].Trim();



                    SetInitialRow();
                    fillPortCodes();
                }
                else
                {
                    txtDocumentNo.Text = "";
                    txtBillAmount.Text = "";
                    lblCurrency.Text = "";


                    hdnYear.Value = Request.QueryString["Year"];
                    Help_btnBillNo.Visible = true;
                    btnHelp_DocNo.Visible = true;

                }
                FillPartyID();
                hdnBranch.Value = Request.QueryString["Branch"];
                txtShippingBillDate.Attributes.Add("onblur", "return isValidDate(" + txtShippingBillDate.ClientID + "," + "'Shipping Bill Date'" + " );");
                btnHelp_DocNo.Attributes.Add("onclick", "return HelpDocNo();");
                Help_btnBillNo.Attributes.Add("onclick", "return HelpShippNo();");
                btnAddGRPPCustoms.Attributes.Add("onclick", "return ValidateAdd();");
                btnSave.Attributes.Add("onclick", "return ValidateSave();");
                btnPortCode.Attributes.Add("onclick", "return PortHelp();");


                txtInvoiceAmt.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtFreight.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtInsuranceAmt.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtDiscount.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtPackingCharges.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtDeductionCharges.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtcommission.Attributes.Add("onkeydown", "return validate_Number(event);");

                btnPartyID.Attributes.Add("onclick", "return Party_Help();");

            }
        }
    }


    private void FillPartyID() {
        TF_DATA objData = new TF_DATA();
        string _query = "TF_EDPMS_Get_Party_ID";

        SqlParameter p1 = new SqlParameter("@DocumentNo", txtDocumentNo.Text);

        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            txtPartyID.Text = dt.Rows[0][0].ToString();
        }
        else {
            txtPartyID.Text = "";
        }


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
            GridViewEDPMS_Bill_Details.DataSource = dt;
            GridViewEDPMS_Bill_Details.DataBind();
        }
    }

    private void AddNewRowToGrid()
    {
        GridViewEDPMS_Bill_Details.Visible = true;
        int rowIndex = 0;
        string _GRcurr = "", _GRportID = "";
        string _GRFormType;

        hdnInvoiceAmt.Value = "0";

        TF_DATA objCheck = new TF_DATA();
        string _queryCheck = "TF_EDPMS_Check_Invoice_No";

        SqlParameter p1 = new SqlParameter("@DocumentNo", txtDocumentNo.Text);
        SqlParameter p2 = new SqlParameter("@ShippingBillNo", txtShippingBillNo.Text);
        SqlParameter p3 = new SqlParameter("@InvoiceNo", txtInvoiceNo.Text);

        DataTable dt = objCheck.getData(_queryCheck, p1, p2, p3);

        if (dt.Rows.Count > 0 && Request.QueryString["mode"] == "Add")
        {
            _proceed = false;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "alert('Invoice Number already exists.')", true);
        }
        else
        {
            _proceed = true;
        }

        //TF_DATA objCheck = new TF_DATA();
        _queryCheck = "TF_EDPMS_Check_Multiple_Invoice_No";

        SqlParameter pDocumentNo = new SqlParameter("@DocumentNo", txtDocumentNo.Text);
        SqlParameter pShippingBillNo = new SqlParameter("@ShippingBillNo", txtShippingBillNo.Text);
        SqlParameter pInvoiceNo = new SqlParameter("@InvoiceNo", txtInvoiceNo.Text);

        DataTable dt1 = objCheck.getData(_queryCheck, pDocumentNo, pShippingBillNo, pInvoiceNo);

        if (dt1.Rows.Count > 0 && Request.QueryString["mode"] != "Add")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "alert('This information is updated for all invoices of Bill Number." + txtShippingBillNo.Text + "');", true);
        }
        
        if (_proceed == true)
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count >= 0)
                {
                    for (int i = 0; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        if (GridViewEDPMS_Bill_Details.Rows.Count > i)
                        {
                            Label lbl1 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[0].FindControl("lblSrNo");
                            Label lbl2 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[1].FindControl("lblShippingBillNo");
                            Label lbl4 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[2].FindControl("lblShipping_Bill_Date");
                            Label lbl5 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[3].FindControl("lblShipBillAmt");
                            Label lbl6 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[4].FindControl("lblTypeofExport");
                            Label lbl7 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[5].FindControl("lblExportAgency");
                            Label lbl8 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[6].FindControl("lblDispatchInd");
                            Label lbl9 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[7].FindControl("lblInvoiceSrNo");
                            Label lbl10 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[8].FindControl("lblInvoiceNo");
                            Label lbl11 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[9].FindControl("lblInvoice_Date");
                            Label lbl12 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[10].FindControl("lblInvoiceAmt");
                            Label lbl13 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[11].FindControl("lblFreightAmt");
                            Label lbl14 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[12].FindControl("lblInsuranceAmt");
                            Label lbl15 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[13].FindControl("lblPortCode");
                            Label lbl16 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[14].FindControl("lblDiscountAmt");
                            Label lbl17 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[15].FindControl("lblPackingCharges");
                            Label lbl18 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[16].FindControl("lblDeductionCharges");
                            Label lbl19 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[17].FindControl("lblCommission");
                            

                            //if (lbl2.Text == txtShippingBillNo.Text)
                            //{
                            //    hdnInvoiceAmt.Value = Convert.ToString(Convert.ToDecimal(hdnInvoiceAmt.Value) + Convert.ToDecimal(lbl12.Text) + Convert.ToDecimal(txtInvoiceAmt.Text));

                            //    if (Convert.ToDecimal(hdnInvoiceAmt.Value) > Convert.ToDecimal(lbl5.Text))
                            //    {
                            //        _proceed = false;
                            //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "alert('Please Check Invoice Amt.')", true);
                            //        txtInvoiceAmt.Focus();
                            //    }
                            //    else
                            //    {
                            //        _proceed = true;
                            //    }
                            //}
                        }

                        if (txtInvoiceAmt.Text != "")
                        {
                            if (Convert.ToDecimal(txtInvoiceAmt.Text) > Convert.ToDecimal(txtShipBillAmt.Text))
                            {
                                _proceed = false;
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "alert('Please Check Invoice Amt.')", true);
                                txtInvoiceAmt.Focus();
                            }
                            else
                            {
                                _proceed = true;
                            }
                        }

                        drCurrentRow = dtCurrentTable.NewRow();

                        Label lastSr = new Label();
                        if (GridViewEDPMS_Bill_Details.Rows.Count > 0)
                        {
                            lastSr = (Label)GridViewEDPMS_Bill_Details.Rows[dtCurrentTable.Rows.Count - 1].Cells[1].FindControl("lblSrNo");
                            if (lastSr.Text == "0")
                                dtCurrentTable.Rows.RemoveAt(0);
                        }
                        else
                            lastSr.Text = 0.ToString();

                        drCurrentRow["SrNo"] = Convert.ToInt32(lastSr.Text) + 1;

                        drCurrentRow["ShippingBillNo"] = txtShippingBillNo.Text;
                        drCurrentRow["Shipping_Bill_Date"] = txtShippingBillDate.Text;
                        drCurrentRow["ShipBillAmt"] = txtShipBillAmt.Text;
                        drCurrentRow["TypeofExport"] = ddlTypeOfExport.SelectedItem.Text;
                        drCurrentRow["ExportAgency"] = ddlExportAgency.SelectedItem.Text;
                        drCurrentRow["DispatchInd"] = ddlDispachInd.SelectedItem.Text;
                        drCurrentRow["InvoiceSrNo"] = txtInvoiceSrNo.Text;
                        drCurrentRow["InvoiceNo"] = txtInvoiceNo.Text;
                        drCurrentRow["Invoice_Date"] = txtInvoiceDate.Text;
                        drCurrentRow["PortCode"] = ddlPortCodeGrid.SelectedItem.Text;
                        drCurrentRow["PartyID"] = txtPartyID.Text;

                        if (txtInvoiceAmt.Text != "")
                        {
                            drCurrentRow["Invoice_Amt"] = txtInvoiceAmt.Text;
                        }
                        else
                        {
                            drCurrentRow["Invoice_Amt"] = "0";
                        }

                        if (txtFreight.Text != "")
                        {
                            drCurrentRow["FreightAmt"] = txtFreight.Text;
                        }
                        else
                        {
                            drCurrentRow["FreightAmt"] = "0";
                        }
                        if (txtInsuranceAmt.Text != "")
                        {
                            drCurrentRow["InsuranceAmt"] = txtInsuranceAmt.Text;
                        }
                        else
                        {
                            drCurrentRow["InsuranceAmt"] = "0";
                        }

                        if (txtDiscount.Text != "")
                        {
                            drCurrentRow["DiscountAmt"] = txtDiscount.Text;
                        }
                        else
                        {
                            drCurrentRow["DiscountAmt"] = "0";
                        }

                        if (txtPackingCharges.Text != "")
                        {
                            drCurrentRow["PackingCharges"] = txtPackingCharges.Text;
                        }
                        else
                        {
                            drCurrentRow["PackingCharges"] = "0";
                        }

                        if (txtDeductionCharges.Text != "")
                        {
                            drCurrentRow["DeductionCharges"] = txtDeductionCharges.Text;
                        }
                        else
                        {
                            drCurrentRow["DeductionCharges"] = "0";
                        }

                        if (txtcommission.Text != "")
                        {
                            drCurrentRow["Commission"] = txtcommission.Text;
                        }
                        else
                        {
                            drCurrentRow["Commission"] = "0";
                        }



                        rowIndex++;



                    }
                }
                if (_proceed == true)
                {
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
    }

    private void SetInitialRow()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter pc1 = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
        pc1.Value = txtDocumentNo.Text.Trim();
        string _query1 = "TF_EDPMS_GetShippingBillDetails";

        DataTable dtPC = objData.getData(_query1, pc1);

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
            dt.Columns.Add(new DataColumn("SrNo", typeof(string)));
            dt.Columns.Add(new DataColumn("ShippingBillNo", typeof(string)));
            dt.Columns.Add(new DataColumn("Shipping_Bill_Date", typeof(string)));
            dt.Columns.Add(new DataColumn("ShipBillAmt", typeof(string)));
            dt.Columns.Add(new DataColumn("TypeofExport", typeof(string)));
            dt.Columns.Add(new DataColumn("ExportAgency", typeof(string)));
            dt.Columns.Add(new DataColumn("DispatchInd", typeof(string)));
            dt.Columns.Add(new DataColumn("InvoiceSrNo", typeof(string)));
            dt.Columns.Add(new DataColumn("InvoiceNo", typeof(string)));
            dt.Columns.Add(new DataColumn("Invoice_Date", typeof(string)));
            dt.Columns.Add(new DataColumn("Invoice_Amt", typeof(string)));
            dt.Columns.Add(new DataColumn("FreightAmt", typeof(string)));
            dt.Columns.Add(new DataColumn("InsuranceAmt", typeof(string)));
            dt.Columns.Add(new DataColumn("PortCode", typeof(string)));

            dt.Columns.Add(new DataColumn("DiscountAmt", typeof(string)));
            dt.Columns.Add(new DataColumn("PackingCharges", typeof(string)));
            dt.Columns.Add(new DataColumn("DeductionCharges", typeof(string)));
            dt.Columns.Add(new DataColumn("Commission", typeof(string)));
            dt.Columns.Add(new DataColumn("PartyID", typeof(string)));

            dr = dt.NewRow();

            dr["SrNo"] = 0;
            dr["ShippingBillNo"] = string.Empty;
            dr["Shipping_Bill_Date"] = string.Empty;
            dr["ShipBillAmt"] = string.Empty;
            dr["TypeofExport"] = string.Empty;
            dr["ExportAgency"] = string.Empty;
            dr["DispatchInd"] = string.Empty;
            dr["InvoiceSrNo"] = string.Empty;
            dr["InvoiceNo"] = string.Empty;
            dr["Invoice_Date"] = string.Empty;
            dr["Invoice_Amt"] = string.Empty;
            dr["FreightAmt"] = string.Empty;
            dr["InsuranceAmt"] = string.Empty;
            dr["PortCode"] = string.Empty;
            dr["DiscountAmt"] = string.Empty;
            dr["PackingCharges"] = string.Empty;
            dr["DeductionCharges"] = string.Empty;
            dr["Commission"] = string.Empty;
            dr["PartyID"] = string.Empty;


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
                    Label lblSrNo = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[1].FindControl("lblSrNo");
                    int lcolumncount;
                    lcolumncount = GridViewEDPMS_Bill_Details.Columns.Count;
                    if (srNo == lblSrNo.Text)
                    {
                        //Remove the Selected Row data
                        Label lbl1 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[0].FindControl("lblSrNo");
                        Label lbl2 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[1].FindControl("lblShippingBillNo");
                        Label lbl4 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[2].FindControl("lblShipping_Bill_Date");
                        Label lbl5 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[3].FindControl("lblShipBillAmt");
                        Label lbl6 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[4].FindControl("lblTypeofExport");
                        Label lbl7 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[5].FindControl("lblExportAgency");
                        Label lbl8 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[6].FindControl("lblDispatchInd");
                        Label lbl9 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[7].FindControl("lblInvoiceSrNo");
                        Label lbl10 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[8].FindControl("lblInvoiceNo");
                        Label lbl11 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[9].FindControl("lblInvoice_Date");
                        Label lbl12 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[10].FindControl("lblInvoiceAmt");
                        Label lbl13 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[11].FindControl("lblFreightAmt");
                        Label lbl14 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[12].FindControl("lblInsuranceAmt");
                        Label lbl15 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[13].FindControl("lblPortCode");
                        Label lbl16 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[14].FindControl("lblDiscountAmt");
                        Label lbl17 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[15].FindControl("lblPackingCharges");
                        Label lbl18 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[16].FindControl("lblDeductionCharges");
                        Label lbl19 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[17].FindControl("lblCommission");
                        

                        dt.Rows.Remove(dt.Rows[rowIndex]);

                        txtShippingBillNo.Text = lbl2.Text;
                        txtShippingBillDate.Text = lbl4.Text;
                        txtShipBillAmt.Text = lbl5.Text;


                        if (lbl6.Text != "")
                        {
                            ddlTypeOfExport.SelectedIndex = ddlTypeOfExport.Items.IndexOf(ddlTypeOfExport.Items.FindByText(lbl6.Text.ToString()));
                        }
                        else
                        {
                            ddlTypeOfExport.SelectedIndex = 0;
                        }

                        if (lbl7.Text != "")
                        {
                            ddlExportAgency.SelectedIndex = ddlExportAgency.Items.IndexOf(ddlExportAgency.Items.FindByText(lbl7.Text.ToString()));
                        }
                        else
                        {
                            ddlExportAgency.SelectedIndex = 0;
                        }

                        if (lbl8.Text != "")
                        {
                            ddlDispachInd.SelectedIndex = ddlDispachInd.Items.IndexOf(ddlDispachInd.Items.FindByText(lbl8.Text.ToString()));
                        }
                        else
                        {
                            ddlDispachInd.SelectedIndex = 0;
                        }

                        txtInvoiceSrNo.Text = lbl9.Text;
                        txtInvoiceNo.Text = lbl10.Text;
                        txtInvoiceDate.Text = lbl11.Text;


                        if (lbl12.Text != "")
                            txtInvoiceAmt.Text = lbl12.Text;

                        if (lbl13.Text != "")
                            txtFreight.Text = lbl13.Text;

                        if (lbl14.Text != "")
                            txtInsuranceAmt.Text = lbl14.Text;

                        if (lbl16.Text != "")
                            txtDiscount.Text = lbl16.Text;

                        if (lbl17.Text != "")
                            txtPackingCharges.Text = lbl17.Text;

                        if (lbl18.Text != "")
                            txtDeductionCharges.Text = lbl18.Text;

                        if (lbl19.Text != "")
                            txtcommission.Text = lbl19.Text;

                        

                        if (lbl15.Text != "")
                        {
                            ddlPortCodeGrid.SelectedIndex = ddlPortCodeGrid.Items.IndexOf(ddlPortCodeGrid.Items.FindByText(lbl15.Text.ToString()));
                        }
                        else
                        {
                            ddlPortCodeGrid.SelectedIndex = 0;
                        }
                        

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
        Response.Redirect("EDPMS_View_Bill_Details.aspx", true);
    }

    protected void GridViewEDPMS_Bill_Details_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSerialNumber = new Label();
            Button btnDelete = new Button();
            lblSerialNumber = (Label)e.Row.FindControl("lblSrNo");

            int i = 0;

            if (Request.QueryString["mode"] != "Add")
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    if (i != 13)
                        cell.Attributes.Add("onclick", "return gridClicked(" + lblSerialNumber.Text + ");");
                    else
                        cell.Style.Add("cursor", "default");
                    i++;
                }
            }
        }
    }

    protected void btnAddGRPPCustoms_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
        if (_proceed == true)
        {
            txtShippingBillNo.Text = "";
            txtShippingBillDate.Text = "";
            txtShipBillAmt.Text = "";
            ddlTypeOfExport.SelectedIndex = 0;
            ddlExportAgency.SelectedIndex = 0;
            ddlDispachInd.SelectedIndex = 0;
            txtInvoiceSrNo.Text = "";
            txtInvoiceNo.Text = "";
            txtFreight.Text = "";
            txtInsuranceAmt.Text = "";
            txtInvoiceAmt.Text = "";
            txtInvoiceDate.Text = "";

            txtDiscount.Text = "";
            txtPackingCharges.Text = "";
            txtDeductionCharges.Text = "";
            txtcommission.Text = "";
            //txtPartyID.Text = "";
            ddlPortCodeGrid.SelectedIndex = 0;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Boolean _proceed = true;
        string _script;
        string _query = "";
        string _result = "";

        if (Request.QueryString["mode"] == "Add")
        {
            if (_proceed == true)
            {
                int rowIndex = 0;

                TF_DATA objSaveGRCustomsDetails = new TF_DATA();

                if (GridViewEDPMS_Bill_Details.Visible == true)
                {
                    _query = "TF_EDPMS_Update_Bill_Details";

                    rowIndex = 0;

                    //labelMessage.Text = GridViewEDPMS_Bill_Details.Rows.Count.ToString();

                    for (int i = 1; i <= GridViewEDPMS_Bill_Details.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        if (GridViewEDPMS_Bill_Details.Rows.Count > 0)
                        {
                            Label lbl1 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[0].FindControl("lblSrNo");
                            Label lbl2 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[1].FindControl("lblShippingBillNo");
                            Label lbl4 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[2].FindControl("lblShipping_Bill_Date");
                            Label lbl5 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[3].FindControl("lblShipBillAmt");
                            Label lbl6 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[4].FindControl("lblTypeofExport");
                            Label lbl7 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[5].FindControl("lblExportAgency");
                            Label lbl8 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[6].FindControl("lblDispatchInd");
                            Label lbl9 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[7].FindControl("lblInvoiceSrNo");
                            Label lbl10 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[8].FindControl("lblInvoiceNo");
                            Label lbl11 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[9].FindControl("lblInvoice_Date");
                            Label lbl12 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[10].FindControl("lblInvoiceAmt");
                            Label lbl13 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[11].FindControl("lblFreightAmt");
                            Label lbl14 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[12].FindControl("lblInsuranceAmt");
                            Label lbl15 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[13].FindControl("lblPortCode");
                            Label lbl16 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[14].FindControl("lblDiscountAmt");
                            Label lbl17 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[15].FindControl("lblPackingCharges");
                            Label lbl18 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[16].FindControl("lblDeductionCharges");
                            Label lbl19 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[17].FindControl("lblCommission");



                            SqlParameter pDocumentNo = new SqlParameter("@DocumentNo", txtDocumentNo.Text);
                            SqlParameter pShippingBillNo = new SqlParameter("@ShippingBillNo", lbl2.Text);
                            SqlParameter pTypeofExport = new SqlParameter("@TypeofExport", lbl6.Text);
                            SqlParameter pExportAgency = new SqlParameter("@ExportAgency", lbl7.Text);
                            SqlParameter pDispatchInd = new SqlParameter("@DispatchInd", lbl8.Text);
                            SqlParameter pInvoiceSrNo = new SqlParameter("@InvoiceSrNo", lbl9.Text);
                            SqlParameter pInvoiceNo = new SqlParameter("@InvoiceNo", lbl10.Text);
                            SqlParameter pInvoice_Date = new SqlParameter("@Invoice_Date", lbl11.Text);
                            SqlParameter pInvoice_Amt = new SqlParameter("@Invoice_Amt", lbl12.Text);
                            SqlParameter pFreightAmt = new SqlParameter("@FreightAmt", lbl13.Text);
                            SqlParameter pInsuranceAmt = new SqlParameter("@InsuranceAmt", lbl14.Text);
                            SqlParameter pPortCode = new SqlParameter("@Port_Code", lbl15.Text);
                            SqlParameter pMode = new SqlParameter("@Mode", Request.QueryString["mode"]);

                            SqlParameter pDiscountAmt = new SqlParameter("@DiscountAmt", lbl16.Text);
                            SqlParameter pPackingCharges = new SqlParameter("@PackingCharges", lbl17.Text);
                            SqlParameter pDeductionCharges = new SqlParameter("@DeductionCharges", lbl18.Text);
                            SqlParameter pCommission = new SqlParameter("@Commission", lbl19.Text);
                            SqlParameter pPartyID = new SqlParameter("@PartyID", txtPartyID.Text);


                            _result = objSaveGRCustomsDetails.SaveDeleteData(_query,
                                pDocumentNo, pShippingBillNo, pTypeofExport, pExportAgency
                                , pDispatchInd, pInvoiceSrNo, pInvoiceNo, pInvoice_Date,
                                pInvoice_Amt, pFreightAmt, pInsuranceAmt, pMode, pPortCode, pDiscountAmt, pPackingCharges
                                , pDeductionCharges, pCommission,pPartyID);
                            rowIndex++;
                        }
                        _script = "window.location='EDPMS_View_Bill_Details.aspx?result=" + _result + "&Branch=" + hdnBranch.Value + "'";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                    }
                }
            }
        }
        else
        {
            if (GridViewEDPMS_Bill_Details.Visible == true)
            {
                int rowIndex = 0;

                TF_DATA objSaveGRCustomsDetails = new TF_DATA();

                _query = "TF_EDPMS_Update_Bill_Details";

                rowIndex = 0;

                //labelMessage.Text = GridViewEDPMS_Bill_Details.Rows.Count.ToString();

                for (int i = 1; i <= GridViewEDPMS_Bill_Details.Rows.Count; i++)
                {
                    //extract the TextBox values
                    if (GridViewEDPMS_Bill_Details.Rows.Count > 0)
                    {
                        Label lbl1 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[0].FindControl("lblSrNo");
                        Label lbl2 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[1].FindControl("lblShippingBillNo");
                        Label lbl4 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[2].FindControl("lblShipping_Bill_Date");
                        Label lbl5 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[3].FindControl("lblShipBillAmt");
                        Label lbl6 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[4].FindControl("lblTypeofExport");
                        Label lbl7 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[5].FindControl("lblExportAgency");
                        Label lbl8 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[6].FindControl("lblDispatchInd");
                        Label lbl9 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[7].FindControl("lblInvoiceSrNo");
                        Label lbl10 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[8].FindControl("lblInvoiceNo");
                        Label lbl11 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[9].FindControl("lblInvoice_Date");
                        Label lbl12 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[10].FindControl("lblInvoiceAmt");
                        Label lbl13 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[11].FindControl("lblFreightAmt");
                        Label lbl14 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[12].FindControl("lblInsuranceAmt");
                        Label lbl15 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[13].FindControl("lblPortCode");
                        Label lbl16 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[14].FindControl("lblDiscountAmt");
                        Label lbl17 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[15].FindControl("lblPackingCharges");
                        Label lbl18 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[16].FindControl("lblDeductionCharges");
                        Label lbl19 = (Label)GridViewEDPMS_Bill_Details.Rows[rowIndex].Cells[17].FindControl("lblCommission");


                        SqlParameter pDocumentNo = new SqlParameter("@DocumentNo", txtDocumentNo.Text);
                        SqlParameter pShippingBillNo = new SqlParameter("@ShippingBillNo", lbl2.Text);
                        SqlParameter pTypeofExport = new SqlParameter("@TypeofExport", lbl6.Text);
                        SqlParameter pExportAgency = new SqlParameter("@ExportAgency", lbl7.Text);
                        SqlParameter pDispatchInd = new SqlParameter("@DispatchInd", lbl8.Text);
                        SqlParameter pInvoiceSrNo = new SqlParameter("@InvoiceSrNo", lbl9.Text);
                        SqlParameter pInvoiceNo = new SqlParameter("@InvoiceNo", lbl10.Text);
                        SqlParameter pInvoice_Date = new SqlParameter("@Invoice_Date", lbl11.Text);
                        SqlParameter pInvoice_Amt = new SqlParameter("@Invoice_Amt", lbl12.Text);
                        SqlParameter pFreightAmt = new SqlParameter("@FreightAmt", lbl13.Text);
                        SqlParameter pInsuranceAmt = new SqlParameter("@InsuranceAmt", lbl14.Text);
                        SqlParameter pPortCode = new SqlParameter("@Port_Code", lbl15.Text);
                        SqlParameter pMode = new SqlParameter("@Mode", Request.QueryString["mode"]);
                        SqlParameter pDiscountAmt = new SqlParameter("@DiscountAmt", lbl16.Text);
                        SqlParameter pPackingCharges = new SqlParameter("@PackingCharges", lbl17.Text);
                        SqlParameter pDeductionCharges = new SqlParameter("@DeductionCharges", lbl18.Text);
                        SqlParameter pCommission = new SqlParameter("@Commission", lbl19.Text);
                        SqlParameter pPartyID = new SqlParameter("@PartyID", txtPartyID.Text);


                        _result = objSaveGRCustomsDetails.SaveDeleteData(_query,
                            pDocumentNo, pShippingBillNo, pTypeofExport, pExportAgency
                            , pDispatchInd, pInvoiceSrNo, pInvoiceNo, pInvoice_Date,
                            pInvoice_Amt, pFreightAmt, pInsuranceAmt, pPortCode, pMode,pDiscountAmt, pPackingCharges
                                , pDeductionCharges, pCommission,pPartyID
                            );
                        rowIndex++;
                        _script = "window.location='EDPMS_View_Bill_Details.aspx?result=" + _result + "&Branch=" + hdnBranch.Value + "'";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                    }
                }

                //if (GridViewEDPMS_Bill_Details.Rows.Count == 0)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "alert(' Please ensure click Add Button before Save Record. ')", true);
                //}

            }
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Response.Redirect("EDPMS_View_Bill_Details.aspx", true);
        SetInitialRow();
        txtShippingBillNo.Text = "";
        txtShippingBillDate.Text = "";
        txtShipBillAmt.Text = "";
        ddlTypeOfExport.SelectedIndex = 0;
        ddlExportAgency.SelectedIndex = 0;
        ddlDispachInd.SelectedIndex = 0;
        ddlPortCodeGrid.SelectedIndex = 0;
        txtInvoiceSrNo.Text = "";
        txtInvoiceNo.Text = "";
        txtFreight.Text = "";
        txtInsuranceAmt.Text = "";
        txtInvoiceAmt.Text = "";
        txtInvoiceDate.Text = "";
        txtcommission.Text = "";
        txtPartyID.Text = "";
        txtDiscount.Text = "";
        txtDeductionCharges.Text = "";
        txtPackingCharges.Text = "";
    }

    private void CalculateGR_Total()
    {
        hdnGRtotalAmount.Value = "0";
        if (GridViewEDPMS_Bill_Details.Rows.Count > 0)
        {
            Label lblSrNo = (Label)GridViewEDPMS_Bill_Details.Rows[0].Cells[0].FindControl("lblSrNo");
            if (lblSrNo.Text != "0")
            {
                for (int i = 0; i < GridViewEDPMS_Bill_Details.Rows.Count; i++)
                {
                    Label lbl4 = (Label)GridViewEDPMS_Bill_Details.Rows[i].Cells[3].FindControl("lblAmount");
                    if (lbl4.Text == "")
                        lbl4.Text = "0";

                    hdnGRtotalAmount.Value = (Convert.ToDouble(hdnGRtotalAmount.Value) + Convert.ToDouble(lbl4.Text)).ToString();

                }
            }
        }
    }

    protected void txtDocumentNo_TextChanged(object sender, EventArgs e)
    {
        SetInitialRow();
        fillPortCodes();
        FillPartyID();
    }

    protected void fillPortCodes()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetPortCodeMasterList";
        DataTable dt = objData.getData(_query, p1);
        ddlPortCodeGrid.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlPortCodeGrid.DataSource = dt.DefaultView;
            ddlPortCodeGrid.DataTextField = "port_Code";
            ddlPortCodeGrid.DataValueField = "port_Code";
            ddlPortCodeGrid.DataBind();

        }
        else
            li.Text = "No record(s) found";

        ddlPortCodeGrid.Items.Insert(0, li);

    }
}