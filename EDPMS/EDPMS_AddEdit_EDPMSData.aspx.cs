﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EDPMS_EDPMS_AddEdit_EDPMSData : System.Web.UI.Page
{
    static string adCode = "";
    string _query = "";
    TF_DATA objData = new TF_DATA();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"].ToString() == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");
            Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["mode"].ToString() == "edit")
                {
                    txtBranch.Text = Request.QueryString["Branch"].ToString();
                    txtShippingBillNo.Text = Request.QueryString["ShipBillNo"].ToString();
                    adCode = Request.QueryString["ADCode"].ToString();
                    fillPortCode();
                    fillDetails();
                    txtBuyerCountry_TextChanged(null, null);
                }
            }

            txtShippingDate.Attributes.Add("onblur", "isValidDate(" + txtShippingDate.ClientID + "," + "' Shipping Date.'" + ");");
            txtNegotiationDate.Attributes.Add("onblur", "isValidDate(" + txtNegotiationDate.ClientID + "," + "' Negotiation Date.'" + ");");
            btnSave.Attributes.Add("onclick", "return validateSave();");
        }
    }
    protected void fillDetails()
    {
        SqlParameter p1 = new SqlParameter("@ADCode", adCode);
        SqlParameter p2 = new SqlParameter("@InvoiceNo", "");
        SqlParameter p3 = new SqlParameter("@DocumentType", "DocBill");
        SqlParameter p4 = new SqlParameter("@shippingBillNo", txtShippingBillNo.Text.Trim());

        _query = "TF_EDPMS_Get_Data";

        DataTable dt = objData.getData(_query, p1, p2, p3, p4);
        if (dt.Rows.Count > 0)
        {
            txtShippingDate.Text = dt.Rows[0]["Shipping_Bill_Date"].ToString().Trim();
            txtADBillNo.Text = dt.Rows[0]["ADBillNo"].ToString().Trim();
            txtNegotiationDate.Text = dt.Rows[0]["Negotiation_Date"].ToString().Trim();
            ddlExportType.SelectedValue = dt.Rows[0]["TypeofExport"].ToString().Trim();
            txtFormNo.Text = dt.Rows[0]["FormNo"].ToString().Trim();
            txtIECode.Text = dt.Rows[0]["IECode"].ToString().Trim();
            ddlExportAgency.SelectedValue = dt.Rows[0]["ExportAgency"].ToString().Trim();
            ddlDirectDispatch.SelectedValue = dt.Rows[0]["Direct_Dispatch_Indicator"].ToString().Trim();
            txtBuyerName.Text = dt.Rows[0]["Buyer_Name"].ToString().Trim();
            txtBuyerCountry.Text = dt.Rows[0]["Buyer_Country"].ToString().Trim();
            ddlPortCode.SelectedValue = dt.Rows[0]["PortCode"].ToString().Trim();
        }
    }
    protected void fillPortCode()
    {
        TF_DATA objData = new TF_DATA();
        string _query = "TF_GetPortCodeMasterList";
        SqlParameter p1 = new SqlParameter("@search", "");
        DataTable dt = objData.getData(_query, p1);
        ListItem li = new ListItem(); 

        ddlPortCode.Items.Clear();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "-Select-";
            ddlPortCode.DataSource = dt.DefaultView;
            ddlPortCode.DataTextField = "port_Code";
            ddlPortCode.DataValueField = "port_Code";
            ddlPortCode.DataBind();
        }
        else
            li.Text = "NoRecords";
        ddlPortCode.Items.Insert(0, li);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SqlParameter p1 = new SqlParameter("@ADCode", adCode);
        SqlParameter p2 = new SqlParameter("@InvoiceNo", "");
        SqlParameter p3 = new SqlParameter("@shippingBillNo", txtShippingBillNo.Text.Trim());
        SqlParameter p4 = new SqlParameter("@DocumentType", "DocBill");
        SqlParameter p5 = new SqlParameter("@shippingDate", txtShippingDate.Text.Trim());
        SqlParameter p6 = new SqlParameter("@ADBillNo", txtADBillNo.Text.Trim());
        SqlParameter p7 = new SqlParameter("@NegotiationDate", txtNegotiationDate.Text.Trim());
        SqlParameter p8 = new SqlParameter("@TypeOfExport", ddlExportType.SelectedValue.Trim());
        SqlParameter p9 = new SqlParameter("@FormNo", txtFormNo.Text.Trim());
        SqlParameter p10 = new SqlParameter("@IECode", txtIECode.Text.Trim());
        SqlParameter p11 = new SqlParameter("@exportAgency", ddlExportAgency.SelectedValue.Trim());
        SqlParameter p12 = new SqlParameter("@directDispatch", ddlDirectDispatch.SelectedValue.Trim());
        SqlParameter p13 = new SqlParameter("@buyerName", txtBuyerName.Text.Trim());
        SqlParameter p14 = new SqlParameter("@buyerCountry", txtBuyerCountry.Text.Trim());
        SqlParameter p15 = new SqlParameter("@portCode", ddlPortCode.SelectedValue);
        SqlParameter p16 = new SqlParameter("@ebrcNo", "");
        SqlParameter p17 = new SqlParameter("@invoiceSrNo", "");
        SqlParameter p18 = new SqlParameter("@invoiceDate", "");
        SqlParameter p19 = new SqlParameter("@realisedCurremcy", "");
        SqlParameter p20 = new SqlParameter("@realisedDate", "");
        SqlParameter p21 = new SqlParameter("@updatedBy",Session["userName"].ToString());
        SqlParameter p22 = new SqlParameter("@updatedDate", System.DateTime.Now);
        _query = "TF_EDPMS_Update_Data";
        string result = objData.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20,p21,p22);

        if (result == "DocUpdated")
        {
            string _script = "window.location='EDPMS_ViewEDPMSData.aspx?result=" + result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", _script, true);
        }
        else
            labelMessage.Text = result;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("EDPMS_ViewEDPMSData.aspx");
    }
    protected void txtBuyerCountry_TextChanged(object sender, EventArgs e)
    {
        SqlParameter p1 = new SqlParameter("@cid", txtBuyerCountry.Text.Trim());
        DataTable dt = objData.getData("TF_GetCountryDetails", p1);
        if (dt.Rows.Count > 0)
        {
            lblCountryDesc.Text = dt.Rows[0]["CountryName"].ToString();
        }
        else
        {
            lblCountryDesc.Text = "";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "alert('Invalid Country.')", true);
            txtBuyerCountry.Focus();
        }
    }
}