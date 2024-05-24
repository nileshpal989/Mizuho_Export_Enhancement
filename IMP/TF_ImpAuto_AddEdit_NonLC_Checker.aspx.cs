using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class IMP_TF_ImpAuto_AddEdit_NonLC_Checker : System.Web.UI.Page
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
                if (Request.QueryString["mode"] == null)
                {
                    //Response.Redirect("IMP_ViewImportBillEntry.aspx", true);
                }
                else
                {
                    if (Session["userRole"].ToString() == "Supervisor")
                    {
                        //btnSave.Enabled = false;
                        btnProcess.Enabled = false;
                        lblSupervisormsg.Visible = true;
                    }
                    else
                    {
                        //btnSave.Enabled = true;
                        btnProcess.Enabled = true;
                        lblSupervisormsg.Visible = false;
                    }
                    fillCurrency();
                    fillCountry();
                    //fillTaxRates();
                    txtDateRcvd.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    string _docType = Request.QueryString["DocPrFx"].ToString();
                    string _flcIlcType = Request.QueryString["flcIlcType"].ToString();

                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtDocNo.Text = Request.QueryString["DocNo"].Trim();
                        fillDetails();
                    }
                    else
                    {
                        lblCopyFrom.Visible = true;
                        txtCopyFromDocNo.Visible = true;
                        btnCopy.Visible = true;
                        btnDocNoListtoCopy.Visible = true;

                        txtDocNo.Text = Request.QueryString["DocNo"].Trim();
                        if (_docType == "FLC" || _docType == "ILC")
                        {
                            rdbSight.Visible = true;
                            rdbUsance.Visible = true;
                            if (_flcIlcType == "S")
                            {
                                rdbSight.Checked = true;
                                rdbUsance.Checked = false;
                            }
                            if (_flcIlcType == "U")
                            {
                                rdbUsance.Checked = true;
                                rdbSight.Checked = false;
                            }
                            rdbSight.Enabled = false;
                            rdbUsance.Enabled = false;
                            fillSubDocNo();
                            fillDetails_FLC_ILC();
                        }
                        if (_docType == "C")
                        {
                            rdbSight.Visible = true;
                            rdbUsance.Visible = true;
                        }
                    }

                }

                txtDateRcvd.Attributes.Add("onblur", "isValidDate(" + txtDateRcvd.ClientID + "," + "'Date Received'" + " );");
                txtDraftDocDate.Attributes.Add("onblur", "isValidDate(" + txtDraftDocDate.ClientID + "," + "'Draft Date'" + " );");
                txtInvoiceDate.Attributes.Add("onblur", "isValidDate(" + txtInvoiceDate.ClientID + "," + "'Invoice Date'" + " );");
                txtAWBDate.Attributes.Add("onblur", "isValidDate(" + txtAWBDate.ClientID + "," + "'AWB Date'" + " );");
                txtPackingListDate.Attributes.Add("onblur", "isValidDate(" + txtPackingListDate.ClientID + "," + "'Packing List Date'" + " );");
                txtInsPolicyDate.Attributes.Add("onblur", "isValidDate(" + txtInsPolicyDate.ClientID + "," + "'Ins Policy Date'" + " );");
                txtDoGauranteeDate.Attributes.Add("onblur", "isValidDate(" + txtDoGauranteeDate.ClientID + "," + "'D.O/Guarantee Date'" + " );");
                txtMaturityDate.Attributes.Add("onblur", "isValidDate(" + txtMaturityDate.ClientID + "," + "'Maturity Date'" + " );");
                txtImpLiceDate.Attributes.Add("onblur", "isValidDate(" + txtImpLiceDate.ClientID + "," + "'Import License Date'" + " );");

                btnImporterList.Attributes.Add("onclick", "return OpenCustomerCodeList('mouseClick');");
                btnOverseasPartyList.Attributes.Add("onclick", "return OpenOverseasPartyList('mouseClick');");
                btncurrList.Attributes.Add("onclick", "return OpenCurrencyList();");
                btnOverseasBankList.Attributes.Add("onclick", "return OpenOverseasBankList('mouseClick');");
                btnIntermediaryBankList.Attributes.Add("onclick", "return OpenIntermediaryBankList('mouseClick');");
                btnAcwithInstiList.Attributes.Add("onclick", "return OpenAcWithInstiList('mouseClick');");
                btnDocsRcvdBankList.Attributes.Add("onclick", "return OpenDocsRcvdBankList('mouseClick');");
                btnCoveringTo.Attributes.Add("onclick", "return OpenPortCodeList('mouseClick');");
                btnCountryOriginList.Attributes.Add("onclick", "return OpenCountryList('1');");
                btnCountryList.Attributes.Add("onclick", "return OpenCountryList('2');");
                //btnSave.Attributes.Add("onclick", "return ValidateSave();");

                btnDocNoListtoCopy.Attributes.Add("onclick", "return OpenCopyFromDocNoList('mouseClick');");
                txtEDDNo.Attributes.Add("onkeydown", "return ChkEDDNo(event);");
                txtEDDNo.Attributes.Add("onblur", "return ChkEDDNo1();");
            }
        }
    }
    protected void fillCurrency()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCurrencyList";
        DataTable dt = objData.getData(_query, p1);
        dropDownListCurrency.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            dropDownListCurrency.DataSource = dt.DefaultView;
            dropDownListCurrency.DataTextField = "C_Code";
            dropDownListCurrency.DataValueField = "C_Code";
            dropDownListCurrency.DataBind();

        }
        else
            li.Text = "No record(s) found";

        dropDownListCurrency.Items.Insert(0, li);

    }

    protected void fillCountry()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetCountryList";
        DataTable dt = objData.getData(_query, p1);
        ddlCountry.Items.Clear();

        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "";
            ddlCountry.DataSource = dt.DefaultView;
            ddlCountry.DataTextField = "CountryID";
            ddlCountry.DataValueField = "CountryID";
            ddlCountry.DataBind();

            ddlCountryOfOrigin.DataSource = dt.DefaultView;
            ddlCountryOfOrigin.DataTextField = "CountryID";
            ddlCountryOfOrigin.DataValueField = "CountryID";
            ddlCountryOfOrigin.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlCountry.Items.Insert(0, li);
        ddlCountryOfOrigin.Items.Insert(0, li);
    }

    protected void btnCurr_Click(object sender, EventArgs e)
    {
        if (hdnCurId.Value != "")
        {
            dropDownListCurrency.SelectedValue = hdnCurId.Value;
            dropDownListCurrency.Focus();
        }
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

    protected void btnOverseasBank_Click(object sender, EventArgs e)
    {
        if (hdnOverseasId.Value != "")
        {
            txtOverseasBankID.Text = hdnOverseasId.Value;
            fillOverseasBankDescription();
            txtOverseasBankID.Focus();
        }
    }
    protected void btnIntermediaryBank_Click(object sender, EventArgs e)
    {
        if (hdnIntermediaryBank.Value != "")
        {
            txtIntermediaryBankID.Text = hdnIntermediaryBank.Value;
            fillIntermediaryBankDescription();
            txtIntermediaryBankID.Focus();
        }
    }
    protected void btnAcwithInsti_Click(object sender, EventArgs e)
    {
        if (hdnAcwithInsti.Value != "")
        {
            txtAcwithInstitution.Text = hdnAcwithInsti.Value;
            fillAcWithInstiBankDescription();
            txtAcwithInstitution.Focus();
        }
    }
    protected void btnDocsRcvdBank_Click(object sender, EventArgs e)
    {
        if (hdnDocsRcvdBank.Value != "")
        {
            txtDocsRcvdBank.Text = hdnDocsRcvdBank.Value;
            fillDocsRcvdBankDescription();
            txtDocsRcvdBank.Focus();
        }
    }

    protected void btnCustomerCode_Click(object sender, EventArgs e)
    {
        if (hdnCustomerCode.Value != "")
        {
            txtImporterID.Text = hdnCustomerCode.Value;
            fillImporterCodeDescription();
            txtImporterID.Focus();
        }
    }

    protected void btnPortCode_Click(object sender, EventArgs e)
    {
        if (hdnPortCode.Value != "")
        {
            txtCoveringTo.Text = hdnPortCode.Value;

            txtCoveringTo.Focus();
        }
    }

    protected void btnCountry_Click(object sender, EventArgs e)
    {
        if (hdnCountry.Value != "")
        {
            if (hdnCountryHelpNo.Value == "1")
            {
                ddlCountryOfOrigin.SelectedValue = hdnCountry.Value;
                ddlCountryOfOrigin.Focus();
            }

            if (hdnCountryHelpNo.Value == "2")
            {
                ddlCountry.SelectedValue = hdnCountry.Value;
                ddlCountry.Focus();
            }
        }
    }

    private void fillImporterCodeDescription()
    {
        lblImporterDesc.Text = "";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@custid", SqlDbType.VarChar);
        p1.Value = txtImporterID.Text;
        string _query = "TF_rptGetCustomerMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblImporterDesc.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
            if (lblImporterDesc.Text.Length > 20)
            {
                lblImporterDesc.ToolTip = lblImporterDesc.Text;
                lblImporterDesc.Text = lblImporterDesc.Text.Substring(0, 20) + "...";
            }
        }
        else
        {
            txtImporterID.Text = "";
            lblImporterDesc.Text = "";
        }

    }

    private void fillOverseasPartyDescription()
    {
        lblOverseasPartyDesc.Text = "";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@over", SqlDbType.VarChar);
        p1.Value = txtOverseasPartyID.Text;
        string _query = "TF_GetOverseasPartyDetails";
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

    private void fillOverseasBankDescription()
    {
        lblOverseasBankDesc.Text = "";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@bankcode", SqlDbType.VarChar);
        p1.Value = txtOverseasBankID.Text;
        string _query = "TF_ImpAuto_GetOverseasBankMasterDetails_IMP";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblOverseasBankDesc.Text = dt.Rows[0]["BankName"].ToString().Trim();
            if (lblOverseasBankDesc.Text.Length > 20)
            {
                lblOverseasBankDesc.ToolTip = lblOverseasBankDesc.Text;
                lblOverseasBankDesc.Text = lblOverseasBankDesc.Text.Substring(0, 20) + "...";
            }
        }
        else
        {
            txtOverseasBankID.Text = "";
            lblOverseasBankDesc.Text = "";
        }

    }

    private void fillIntermediaryBankDescription()
    {
        lblIntermediaryBankDesc.Text = "";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@bankcode", SqlDbType.VarChar);
        p1.Value = txtIntermediaryBankID.Text;
        string _query = "TF_ImpAuto_GetOverseasBankMasterDetails_IMP";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblIntermediaryBankDesc.Text = dt.Rows[0]["BankName"].ToString().Trim();
            if (lblIntermediaryBankDesc.Text.Length > 20)
            {
                lblIntermediaryBankDesc.ToolTip = lblIntermediaryBankDesc.Text;
                lblIntermediaryBankDesc.Text = lblIntermediaryBankDesc.Text.Substring(0, 20) + "...";
            }
        }
        else
        {
            txtIntermediaryBankID.Text = "";
            lblIntermediaryBankDesc.Text = "";
        }

    }

    private void fillAcWithInstiBankDescription()
    {
        lblAcWithInstiBankDesc.Text = "";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@bankcode", SqlDbType.VarChar);
        p1.Value = txtAcwithInstitution.Text;
        string _query = "TF_ImpAuto_GetOverseasBankMasterDetails_IMP";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblAcWithInstiBankDesc.Text = dt.Rows[0]["BankName"].ToString().Trim();
            if (lblAcWithInstiBankDesc.Text.Length > 20)
            {
                lblAcWithInstiBankDesc.ToolTip = lblAcWithInstiBankDesc.Text;
                lblAcWithInstiBankDesc.Text = lblAcWithInstiBankDesc.Text.Substring(0, 20) + "...";
            }
        }
        else
        {
            txtAcwithInstitution.Text = "";
            lblAcWithInstiBankDesc.Text = "";
        }

    }

    private void fillDocsRcvdBankDescription()
    {
        lblDocsRcvdBankDesc.Text = "";
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@bankcode", SqlDbType.VarChar);
        p1.Value = txtDocsRcvdBank.Text;
        string _query = "TF_ImpAuto_GetOverseasBankMasterDetails_IMP";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblDocsRcvdBankDesc.Text = dt.Rows[0]["BankName"].ToString().Trim();
            if (lblDocsRcvdBankDesc.Text.Length > 20)
            {
                lblDocsRcvdBankDesc.ToolTip = lblDocsRcvdBankDesc.Text;
                lblDocsRcvdBankDesc.Text = lblDocsRcvdBankDesc.Text.Substring(0, 20) + "...";
            }
        }
        else
        {
            txtDocsRcvdBank.Text = "";
            lblDocsRcvdBankDesc.Text = "";
        }

    }

    protected void fillDetails()
    {
        SqlParameter p1 = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
        p1.Value = txtDocNo.Text.Trim();
        SqlParameter p3 = new SqlParameter("@DocType", SqlDbType.VarChar);
        p3.Value = Request.QueryString["DocPrFx"].ToString();
        SqlParameter p4 = new SqlParameter("@subDocNo", SqlDbType.VarChar);
        p4.Value = Request.QueryString["SubDocNo"].ToString();

        string _query = "TF_ImpAuto_GetImportEntryDetails";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p3, p4);

        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["Document_Type"].ToString().Trim() == "C")
            {
                rdbSight.Visible = true;
                rdbUsance.Visible = true;
                string _cType = dt.Rows[0]["Document_Sight_Usance"].ToString().Trim();
                if (_cType == "S")
                {
                    rdbSight.Checked = true;
                    rdbUsance.Checked = false;
                }
                if (_cType == "U")
                {
                    rdbUsance.Checked = true;
                    rdbSight.Checked = false;
                }

            }

            txtSubDocNo.Text = dt.Rows[0]["Sub_Document_No"].ToString().Trim();
            txtDateRcvd.Text = dt.Rows[0]["Date_Received"].ToString().Trim();
            txtImporterID.Text = dt.Rows[0]["CustAcNo"].ToString().Trim();
            fillImporterCodeDescription();

            txtOverseasPartyID.Text = dt.Rows[0]["Overseas_Party_Code"].ToString().Trim();
            fillOverseasPartyDescription();
            txtOverseasBankID.Text = dt.Rows[0]["Overseas_Bank_Code"].ToString().Trim();
            fillOverseasBankDescription();
            if (dt.Rows[0]["OverseasAcInd"].ToString().Trim() != "")
            {
                switch (dt.Rows[0]["OverseasAcInd"].ToString().Trim())
                {
                    case "AcNo":
                        rdbACno_OB.Checked = true;
                        break;
                    case "Sort":
                        rdbSortCode_OB.Checked = true;
                        break;
                    case "UID":
                        rdbChipUID_OB.Checked = true;
                        break;
                    case "Chip":
                        rdbChipsABAno_OB.Checked = true;
                        break;
                    case "ABA":
                        rdbABAno_OB.Checked = true;
                        break;
                }
            }

            txtIntermediaryBankID.Text = dt.Rows[0]["Intermediary_Bank"].ToString().Trim();
            fillIntermediaryBankDescription();
            if (dt.Rows[0]["IntermediaryAcInd"].ToString().Trim() != "")
            {
                switch (dt.Rows[0]["IntermediaryAcInd"].ToString().Trim())
                {
                    case "AcNo":
                        rdbACno_IB.Checked = true;
                        break;
                    case "Sort":
                        rdbSortCode_IB.Checked = true;
                        break;
                    case "UID":
                        rdbChipUID_IB.Checked = true;
                        break;
                    case "Chip":
                        rdbChipsABAno_IB.Checked = true;
                        break;
                    case "ABA":
                        rdbABAno_IB.Checked = true;
                        break;
                }
            }

            txtDraftNo.Text = dt.Rows[0]["Draft_Doc_No"].ToString().Trim();
            txtDraftDocDate.Text = dt.Rows[0]["Draft_Doc_Date"].ToString().Trim();
            txtDraftDoc.Text = dt.Rows[0]["Draft_Doc"].ToString().Trim();

            txtAcwithInstitution.Text = dt.Rows[0]["Institute_Bank"].ToString().Trim();
            fillAcWithInstiBankDescription();
            if (dt.Rows[0]["InstituteAcInd"].ToString().Trim() != "")
            {
                switch (dt.Rows[0]["InstituteAcInd"].ToString().Trim())
                {
                    case "AcNo":
                        rdbACno_AI.Checked = true;
                        break;
                    case "Sort":
                        rdbSortCode_AI.Checked = true;
                        break;
                    case "UID":
                        rdbChipUID_AI.Checked = true;
                        break;
                    case "Chip":
                        rdbChipsABAno_AI.Checked = true;
                        break;
                    case "ABA":
                        rdbABAno_AI.Checked = true;
                        break;
                }
            }

            txtInvoiceNo.Text = dt.Rows[0]["Invoice_No"].ToString().Trim();
            txtInvoiceDate.Text = dt.Rows[0]["Invoice_Date"].ToString().Trim();

            string _VariousInvoice = dt.Rows[0]["VariousInvoice"].ToString().Trim();
            if (_VariousInvoice == "True")
            {
                chkVariousInvoices.Checked = true;
            }
            txtInvoiceDoc.Text = dt.Rows[0]["Invoice_Doc"].ToString().Trim();

            txtAWBno.Text = dt.Rows[0]["AWB_No"].ToString().Trim();
            txtAWBDate.Text = dt.Rows[0]["AWB_Date"].ToString().Trim();
            txtAwbIssuedBy.Text = dt.Rows[0]["AWB_IssuedBy"].ToString().Trim();
            txtAWBDoc.Text = dt.Rows[0]["AWB_Doc"].ToString().Trim();

            txtPackingList.Text = dt.Rows[0]["Packing_List"].ToString().Trim();
            txtPackingListDate.Text = dt.Rows[0]["Packing_List_Date"].ToString().Trim();
            txtPackingDoc.Text = dt.Rows[0]["Packing_List_Doc"].ToString().Trim();

            txtCertOfOrigin.Text = dt.Rows[0]["Cert_of_Origin"].ToString().Trim();
            txtCertIssuedBy.Text = dt.Rows[0]["Cert_of_Origin_IssuedBy"].ToString().Trim();
            txtCertOfOriginDoc.Text = dt.Rows[0]["Cert_of_Origin_Doc"].ToString().Trim();

            txtInsPolicy.Text = dt.Rows[0]["Ins_Policy"].ToString().Trim();
            txtInsPolicyDate.Text = dt.Rows[0]["Ins_Policy_Date"].ToString().Trim();
            txtInsPolicyIssuedBy.Text = dt.Rows[0]["Ins_Policy_IssuedBy"].ToString().Trim();
            txtInsPolicyDoc.Text = dt.Rows[0]["Ins_Policy_Doc"].ToString().Trim();

            txtMiscellaneous.Text = dt.Rows[0]["Miscellaneous"].ToString().Trim();
            txtMiscDoc.Text = dt.Rows[0]["Miscellaneous_Doc"].ToString().Trim();

            if (dt.Rows[0]["Country_of_Origin"].ToString().Trim() != "")
            {
                ddlCountryOfOrigin.SelectedValue = dt.Rows[0]["Country_of_Origin"].ToString().Trim();
            }
            else
                ddlCountryOfOrigin.SelectedIndex = -1;

            if (dt.Rows[0]["Country_Code"].ToString().Trim() != "")
            {
                ddlCountry.SelectedValue = dt.Rows[0]["Country_Code"].ToString().Trim();
            }
            else
                ddlCountry.SelectedIndex = -1;

            txtCoveringFrom.Text = dt.Rows[0]["Covering_From"].ToString().Trim();
            txtCoveringTo.Text = dt.Rows[0]["Covering_To"].ToString().Trim();
            txtCommodity.Text = dt.Rows[0]["Commodity"].ToString().Trim();
            txtQuantity.Text = dt.Rows[0]["Quantity"].ToString().Trim();

            txtDoGauranteeNo.Text = dt.Rows[0]["DO_Guarantee_No"].ToString().Trim();
            txtDoGauranteeDate.Text = dt.Rows[0]["Do_Guarantee_Date"].ToString().Trim();

            string _shipment = dt.Rows[0]["Steamer"].ToString().Trim();
            if (_shipment == "A")
                rdbByAir.Checked = true;
            if (_shipment == "S")
                rdbBySea.Checked = true;
            if (_shipment == "R")
                rdbByRoad.Checked = true;

            txtImportLicenceNo.Text = dt.Rows[0]["Import_Licence_No"].ToString().Trim();
            txtImpLiceDate.Text = dt.Rows[0]["License_Date"].ToString().Trim();
            txtOGL.Text = dt.Rows[0]["OGL"].ToString().Trim();

            if (dt.Rows[0]["Currency"].ToString().Trim() != "")
            {
                dropDownListCurrency.SelectedValue = dt.Rows[0]["Currency"].ToString().Trim();
            }
            else
                dropDownListCurrency.SelectedIndex = -1;

            txtMaturityDate.Text = dt.Rows[0]["Maturity_Date"].ToString().Trim();

            txtBillAmount.Text = dt.Rows[0]["Bill_Amount"].ToString().Trim();
            txtCorrespChrgs.Text = dt.Rows[0]["Corres_Charges"].ToString().Trim();
            txtInterestAmt.Text = dt.Rows[0]["Interest_Amount"].ToString().Trim();

            txtOverseasBankRef.Text = dt.Rows[0]["Overseas_BankRef"].ToString().Trim();
            txtTerms.Text = dt.Rows[0]["Terms"].ToString().Trim();
            txtTenor.Text = dt.Rows[0]["Tenor"].ToString().Trim();

            txtDocsRcvdBank.Text = dt.Rows[0]["Beneficiary_Bank"].ToString().Trim();
            fillDocsRcvdBankDescription();
            if (dt.Rows[0]["BenAcInd"].ToString().Trim() != "")
            {
                switch (dt.Rows[0]["BenAcInd"].ToString().Trim())
                {
                    case "AcNo":
                        rdbACno_DB.Checked = true;
                        break;
                    case "Sort":
                        rdbSortCode_DB.Checked = true;
                        break;
                    case "UID":
                        rdbChipUID_DB.Checked = true;
                        break;
                    case "Chip":
                        rdbChipsABAno_DB.Checked = true;
                        break;
                    case "ABA":
                        rdbABAno_DB.Checked = true;
                        break;
                }
            }

            string _CB1 = dt.Rows[0]["Intimation_letter_CB1"].ToString().Trim();
            if (_CB1 == "True")
                chk1.Checked = true;
            else
                chk1.Checked = false;

            string _CB2 = dt.Rows[0]["Intimation_letter_CB2"].ToString().Trim();
            if (_CB2 == "True")
                chk1.Checked = true;
            else
                chk2.Checked = false;

            string _CB3 = dt.Rows[0]["Intimation_letter_CB3"].ToString().Trim();
            if (_CB3 == "True")
                chk1.Checked = true;
            else
                chk3.Checked = false;

            string _CB4 = dt.Rows[0]["Intimation_letter_CB4"].ToString().Trim();
            if (_CB4 == "True")
                chk4.Checked = true;
            else
                chk4.Checked = false;

            string _CB5 = dt.Rows[0]["Intimation_letter_CB5"].ToString().Trim();
            if (_CB5 == "True")
                chk5.Checked = true;
            else
                chk5.Checked = false;

            string _CB6 = dt.Rows[0]["Intimation_letter_CB6"].ToString().Trim();
            if (_CB6 == "True")
                chk6.Checked = true;
            else
                chk6.Checked = false;

            txtRemarkCovSchedule.Text = dt.Rows[0]["Intimation_letter_Remarks"].ToString().Trim();
            txtRegisterRemarks.Text = dt.Rows[0]["Document_Remarks"].ToString().Trim();
            txtDiscrepancy.Text = dt.Rows[0]["Discrepancy"].ToString().Trim();
            txtDiscrepanyfee.Text = dt.Rows[0]["Discrepancy_Fee"].ToString().Trim();
            txtDiscrepanydate.Text = dt.Rows[0]["Discrepancy_Date"].ToString().Trim();
            txtMT103Remarks.Text = dt.Rows[0]["MT103Remark"].ToString().Trim();
            txtMT202Remarks.Text = dt.Rows[0]["MT202Remark"].ToString().Trim();
            txtVoucherRemarks.Text = dt.Rows[0]["VoucherRemark"].ToString().Trim();

            string AcceptanceRec = dt.Rows[0]["Acceptance_Rec"].ToString().Trim();
            if (AcceptanceRec == "Y")
            {
                chkAcceptance.Checked = true;
                chkbox.Text = "Yes";
            }
            else
            {
                chkAcceptance.Checked = false;
                chkbox.Text = "No";
            }
            txtEDDNo.Text = dt.Rows[0]["EDDNo"].ToString();
            if (dt.Rows[0]["EDDChk"].ToString() == "Y")
            {
                chk_EDDChk.Checked = true;
                lblEDDChk.Text = "Y";
            }
            else
            {
                chk_EDDChk.Checked = false;
                lblEDDChk.Text = "N";
            }

        }
    }

    protected void fillDetails_FLC_ILC()
    {
        SqlParameter p1 = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
        p1.Value = txtDocNo.Text.Trim();

        string _query = "TF_ImpAuto_GetImportEntryDetails_FLC_ILC";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);

        if (dt.Rows.Count > 0)
        {
            txtImporterID.Text = dt.Rows[0]["CustAcNo"].ToString().Trim();
            fillImporterCodeDescription();

            txtOverseasPartyID.Text = dt.Rows[0]["Overseas_Party_Id"].ToString().Trim();
            fillOverseasPartyDescription();
            txtOverseasBankID.Text = dt.Rows[0]["Overseas_Bank_Id"].ToString().Trim();
            fillOverseasBankDescription();

            if (dt.Rows[0]["Country"].ToString().Trim() != "")
            {
                ddlCountry.SelectedValue = dt.Rows[0]["Country"].ToString().Trim();
            }
            else
                ddlCountry.SelectedIndex = -1;

            txtCoveringFrom.Text = dt.Rows[0]["Covering_From"].ToString().Trim();
            txtCoveringTo.Text = dt.Rows[0]["Covering_To"].ToString().Trim();
            txtCommodity.Text = dt.Rows[0]["Commodity"].ToString().Trim();
            txtQuantity.Text = dt.Rows[0]["Quantity"].ToString().Trim();

            if (dt.Rows[0]["Currency"].ToString().Trim() != "")
            {
                dropDownListCurrency.SelectedValue = dt.Rows[0]["Currency"].ToString().Trim();
            }
            else
                dropDownListCurrency.SelectedIndex = -1;

            //txtMaturityDate.Text = dt.Rows[0]["Maturity_Date"].ToString().Trim();

            txtTenor.Text = dt.Rows[0]["Tenor"].ToString().Trim();

        }
    }

    private void fillSubDocNo()
    {
        TF_DATA objData = new TF_DATA();

        SqlParameter pDocNo = new SqlParameter("@DocNo", SqlDbType.VarChar);
        pDocNo.Value = txtDocNo.Text;

        string _query = "TF_ImpAuto_IMP_GetLastSubDocNo";
        DataTable dt = objData.getData(_query, pDocNo);
        if (dt.Rows.Count > 0)
        {
            txtSubDocNo.Text = dt.Rows[0]["LastEntryNo"].ToString();
        }
    }

    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    string _userName = Session["userName"].ToString().Trim();
    //    string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
    //    string _mode = Request.QueryString["mode"].Trim();

    //    TF_DATA objSave = new TF_DATA();
    //    string _query = "TF_ImpAuto_UpdateImportEntryDetails";

    //    SqlParameter pMode = new SqlParameter("@mode", SqlDbType.VarChar);
    //    pMode.Value = _mode;

    //    SqlParameter pBranchCode = new SqlParameter("@bCode", SqlDbType.VarChar);
    //    pBranchCode.Value = Request.QueryString["BranchCode"].ToString();

    //    SqlParameter pDocNo = new SqlParameter("@docNo", SqlDbType.VarChar);
    //    pDocNo.Value = Request.QueryString["DocNo"].ToString();

    //    SqlParameter pDocPrFx = new SqlParameter("@docPrFx", SqlDbType.VarChar);
    //    pDocPrFx.Value = Request.QueryString["DocPrFx"].ToString();
    //    string _docType = Request.QueryString["DocPrFx"].ToString();

    //    SqlParameter pDocSrNo = new SqlParameter("@docSrNo", SqlDbType.VarChar);
    //    pDocSrNo.Value = Request.QueryString["DocSrNo"].ToString();

    //    SqlParameter pDocNoYear = new SqlParameter("@docNoYear", SqlDbType.VarChar);
    //    pDocNoYear.Value = Request.QueryString["DocYear"].ToString();

    //    SqlParameter pFlcIlcType = new SqlParameter("@flcIlcType", SqlDbType.VarChar);
    //    if (_docType == "C")
    //    {
    //        if (rdbSight.Checked == true)
    //            pFlcIlcType.Value = "S";
    //        if (rdbUsance.Checked == true)
    //            pFlcIlcType.Value = "U";

    //    }
    //    else
    //        pFlcIlcType.Value = Request.QueryString["flcIlcType"].ToString();

    //    SqlParameter pSubDocNo = new SqlParameter("@subDocNo", SqlDbType.VarChar);
    //    pSubDocNo.Value = txtSubDocNo.Text.Trim();

    //    SqlParameter pCustAcNo = new SqlParameter("@custAcNo", SqlDbType.VarChar);
    //    pCustAcNo.Value = txtImporterID.Text.Trim();

    //    SqlParameter pOverseasPartyID = new SqlParameter("@OverseasPartyID", SqlDbType.VarChar);
    //    pOverseasPartyID.Value = txtOverseasPartyID.Text.Trim();

    //    SqlParameter pOverseasBankID = new SqlParameter("@OverseasBankID", SqlDbType.VarChar);
    //    pOverseasBankID.Value = txtOverseasBankID.Text.Trim();

    //    SqlParameter pOverseasBankCodes = new SqlParameter("@OverseasBankCodes", SqlDbType.VarChar);
    //    string _OverseasBankCodes = "";
    //    if (rdbACno_OB.Checked == true)
    //        _OverseasBankCodes = "AcNo";
    //    if (rdbSortCode_OB.Checked == true)
    //        _OverseasBankCodes = "Sort";
    //    if (rdbChipUID_OB.Checked == true)
    //        _OverseasBankCodes = "UID";
    //    if (rdbChipsABAno_OB.Checked == true)
    //        _OverseasBankCodes = "Chip";
    //    if (rdbABAno_OB.Checked == true)
    //        _OverseasBankCodes = "ABA";
    //    pOverseasBankCodes.Value = _OverseasBankCodes;

    //    SqlParameter pIntermediaryBank = new SqlParameter("@IntermediaryBank", SqlDbType.VarChar);
    //    pIntermediaryBank.Value = txtIntermediaryBankID.Text.Trim();

    //    SqlParameter pIntermediaryBankCodes = new SqlParameter("@IntermediaryBankCodes", SqlDbType.VarChar);
    //    string _IntermediaryBankCodes = "";
    //    if (rdbACno_IB.Checked == true)
    //        _IntermediaryBankCodes = "AcNo";
    //    if (rdbSortCode_IB.Checked == true)
    //        _IntermediaryBankCodes = "Sort";
    //    if (rdbChipUID_IB.Checked == true)
    //        _IntermediaryBankCodes = "UID";
    //    if (rdbChipsABAno_IB.Checked == true)
    //        _IntermediaryBankCodes = "Chip";
    //    if (rdbABAno_IB.Checked == true)
    //        _IntermediaryBankCodes = "ABA";
    //    pIntermediaryBankCodes.Value = _IntermediaryBankCodes;

    //    SqlParameter pDateRcvd = new SqlParameter("@dateRcvd", SqlDbType.VarChar);
    //    pDateRcvd.Value = txtDateRcvd.Text.Trim();

    //    SqlParameter pDraftDocNo = new SqlParameter("@draftDocNo", SqlDbType.VarChar);
    //    pDraftDocNo.Value = txtDraftNo.Text.Trim();

    //    SqlParameter pDraftDocDate = new SqlParameter("@draftDocDate", SqlDbType.VarChar);
    //    pDraftDocDate.Value = txtDraftDocDate.Text.Trim();

    //    SqlParameter pDraftDoc = new SqlParameter("@draftDoc", SqlDbType.VarChar);
    //    pDraftDoc.Value = txtDraftDoc.Text.Trim();

    //    SqlParameter pInstituteBank = new SqlParameter("@instituteBank", SqlDbType.VarChar);
    //    pInstituteBank.Value = txtAcwithInstitution.Text.Trim();

    //    SqlParameter pAcWithInstiBankCodes = new SqlParameter("@AcWithInstiBankCodes", SqlDbType.VarChar);
    //    string _AcWithInstiBankCodes = "";
    //    if (rdbACno_AI.Checked == true)
    //        _AcWithInstiBankCodes = "AcNo";
    //    if (rdbSortCode_AI.Checked == true)
    //        _AcWithInstiBankCodes = "Sort";
    //    if (rdbChipUID_AI.Checked == true)
    //        _AcWithInstiBankCodes = "UID";
    //    if (rdbChipsABAno_AI.Checked == true)
    //        _AcWithInstiBankCodes = "Chip";
    //    if (rdbABAno_AI.Checked == true)
    //        _AcWithInstiBankCodes = "ABA";
    //    pAcWithInstiBankCodes.Value = _AcWithInstiBankCodes;

    //    SqlParameter pInvoiceNo = new SqlParameter("@invoiceNo", SqlDbType.VarChar);
    //    pInvoiceNo.Value = txtInvoiceNo.Text.Trim();

    //    SqlParameter pInvoiceDate = new SqlParameter("@invoiceDate", SqlDbType.VarChar);
    //    pInvoiceDate.Value = txtInvoiceDate.Text.Trim();

    //    SqlParameter pVariousInvoice = new SqlParameter("@variousInvoice", SqlDbType.VarChar);
    //    string bit_VariousInvoice = "0";
    //    if (chkVariousInvoices.Checked)
    //        bit_VariousInvoice = "1";
    //    pVariousInvoice.Value = bit_VariousInvoice;

    //    SqlParameter pInvoiceDoc = new SqlParameter("@InvoiceDoc", SqlDbType.VarChar);
    //    pInvoiceDoc.Value = txtInvoiceDoc.Text.Trim();

    //    SqlParameter pAWBno = new SqlParameter("@AWBno", SqlDbType.VarChar);
    //    pAWBno.Value = txtAWBno.Text.Trim();

    //    SqlParameter pAWBdate = new SqlParameter("@AWBdate", SqlDbType.VarChar);
    //    pAWBdate.Value = txtAWBDate.Text.Trim();

    //    SqlParameter pAWBissuedBy = new SqlParameter("@AWBissuedBy", SqlDbType.VarChar);
    //    pAWBissuedBy.Value = txtAwbIssuedBy.Text.Trim();

    //    SqlParameter pAWBdoc = new SqlParameter("@AWBdoc", SqlDbType.VarChar);
    //    pAWBdoc.Value = txtAWBDoc.Text.Trim();

    //    SqlParameter pPackingList = new SqlParameter("@PackingList", SqlDbType.VarChar);
    //    pPackingList.Value = txtPackingList.Text.Trim();

    //    SqlParameter pPackingListDate = new SqlParameter("@PackingListDate", SqlDbType.VarChar);
    //    pPackingListDate.Value = txtPackingListDate.Text.Trim();

    //    SqlParameter pPackingListDoc = new SqlParameter("@PackingListDoc", SqlDbType.VarChar);
    //    pPackingListDoc.Value = txtPackingDoc.Text.Trim();

    //    SqlParameter pCertOfOrigin = new SqlParameter("@certOfOrigin", SqlDbType.VarChar);
    //    pCertOfOrigin.Value = txtCertOfOrigin.Text.Trim();

    //    SqlParameter pCertOfOriginIssuedBy = new SqlParameter("@certOfOriginIssuedBy", SqlDbType.VarChar);
    //    pCertOfOriginIssuedBy.Value = txtCertIssuedBy.Text.Trim();

    //    SqlParameter pCertOfOriginDoc = new SqlParameter("@certOfOriginDoc", SqlDbType.VarChar);
    //    pCertOfOriginDoc.Value = txtCertOfOriginDoc.Text.Trim();

    //    SqlParameter pInsPolicy = new SqlParameter("@InsPolicy", SqlDbType.VarChar);
    //    pInsPolicy.Value = txtInsPolicy.Text.Trim();

    //    SqlParameter pInsPolicyDate = new SqlParameter("@InsPolicyDate", SqlDbType.VarChar);
    //    pInsPolicyDate.Value = txtInsPolicyDate.Text.Trim();

    //    SqlParameter pInsPolicyIssuedBy = new SqlParameter("@InsPolicyIssuedBy", SqlDbType.VarChar);
    //    pInsPolicyIssuedBy.Value = txtInsPolicyIssuedBy.Text.Trim();

    //    SqlParameter pInsPolicydoc = new SqlParameter("@InsPolicyDoc", SqlDbType.VarChar);
    //    pInsPolicydoc.Value = txtInsPolicyDoc.Text.Trim();

    //    SqlParameter pMisc = new SqlParameter("@Miscellaneous", SqlDbType.VarChar);
    //    pMisc.Value = txtMiscellaneous.Text.Trim();

    //    SqlParameter pMiscDoc = new SqlParameter("@MiscellaneousDoc", SqlDbType.VarChar);
    //    pMiscDoc.Value = txtMiscDoc.Text.Trim();

    //    SqlParameter pCountryOfOrigin = new SqlParameter("@CountryOfOrigin", SqlDbType.VarChar);
    //    if (ddlCountryOfOrigin.SelectedIndex > 0)
    //        pCountryOfOrigin.Value = ddlCountryOfOrigin.SelectedValue.Trim();
    //    else
    //        pCountryOfOrigin.Value = "";

    //    SqlParameter pCountryCode = new SqlParameter("@CountryCode", SqlDbType.VarChar);
    //    if (ddlCountry.SelectedIndex > 0)
    //        pCountryCode.Value = ddlCountry.SelectedValue.Trim();
    //    else
    //        pCountryCode.Value = "";

    //    SqlParameter pCoveringFrom = new SqlParameter("@CoveringFrom", SqlDbType.VarChar);
    //    pCoveringFrom.Value = txtCoveringFrom.Text.Trim();

    //    SqlParameter pCoveringTo = new SqlParameter("@CoveringTo", SqlDbType.VarChar);
    //    pCoveringTo.Value = txtCoveringTo.Text.Trim();

    //    SqlParameter pCommodity = new SqlParameter("@Commodity", SqlDbType.VarChar);
    //    pCommodity.Value = txtCommodity.Text.Trim();

    //    SqlParameter pQuantity = new SqlParameter("@Quantity", SqlDbType.VarChar);
    //    pQuantity.Value = txtQuantity.Text.Trim();

    //    SqlParameter pDOguaranteeNo = new SqlParameter("@DOguaranteeNo", SqlDbType.VarChar);
    //    pDOguaranteeNo.Value = txtDoGauranteeNo.Text.Trim();

    //    SqlParameter pDOguaranteeDate = new SqlParameter("@DOguaranteeDate", SqlDbType.VarChar);
    //    pDOguaranteeDate.Value = txtDoGauranteeDate.Text.Trim();

    //    SqlParameter pShipment = new SqlParameter("@Steamer", SqlDbType.VarChar);
    //    string _Shipment = "";
    //    if (rdbByAir.Checked)
    //        _Shipment = "A";
    //    else if (rdbBySea.Checked)
    //        _Shipment = "S";
    //    else if (rdbByRoad.Checked)
    //        _Shipment = "R";
    //    pShipment.Value = _Shipment;

    //    SqlParameter pImportLicenceNo = new SqlParameter("@ImportLicenceNo", SqlDbType.VarChar);
    //    pImportLicenceNo.Value = txtImportLicenceNo.Text.Trim();

    //    SqlParameter pImportLicenseDate = new SqlParameter("@ImportLicenseDate", SqlDbType.VarChar);
    //    pImportLicenseDate.Value = txtImpLiceDate.Text.ToString().Trim();

    //    SqlParameter pOGL = new SqlParameter("@OGL", SqlDbType.VarChar);
    //    pOGL.Value = txtOGL.Text.Trim();

    //    SqlParameter pCurr = new SqlParameter("@Curr", SqlDbType.VarChar);
    //    if (dropDownListCurrency.SelectedIndex > 0)
    //        pCurr.Value = dropDownListCurrency.SelectedValue.Trim();
    //    else
    //        pCurr.Value = "";

    //    SqlParameter pMaturityDate = new SqlParameter("@maturityDate", SqlDbType.VarChar);
    //    pMaturityDate.Value = txtMaturityDate.Text.Trim();

    //    SqlParameter pBillAmt = new SqlParameter("@billAmt", SqlDbType.VarChar);
    //    pBillAmt.Value = txtBillAmount.Text.Trim();

    //    SqlParameter pCorresChrgs = new SqlParameter("@corresChrgs", SqlDbType.VarChar);
    //    pCorresChrgs.Value = txtCorrespChrgs.Text.Trim();

    //    SqlParameter pIntAmt = new SqlParameter("@intAmt", SqlDbType.VarChar);
    //    pIntAmt.Value = txtInterestAmt.Text.Trim();


    //    SqlParameter pOverseasBankRef = new SqlParameter("@OverseasBankRef", SqlDbType.VarChar);
    //    pOverseasBankRef.Value = txtOverseasBankRef.Text.Trim();

    //    SqlParameter pTerms = new SqlParameter("@terms", SqlDbType.VarChar);
    //    pTerms.Value = txtTerms.Text.Trim();

    //    SqlParameter pTenor = new SqlParameter("@tenor", SqlDbType.VarChar);
    //    pTenor.Value = txtTenor.Text.Trim();

    //    SqlParameter pDocRcvd = new SqlParameter("@Beneficiary_Bank", SqlDbType.VarChar);
    //    pDocRcvd.Value = txtDocsRcvdBank.Text.Trim();

    //    SqlParameter pDocRcvdBankCodes = new SqlParameter("@DocRcvdBankCodes", SqlDbType.VarChar);
    //    string _DocRcvdBankCodes = "";
    //    if (rdbACno_DB.Checked == true)
    //        _DocRcvdBankCodes = "AcNo";
    //    if (rdbSortCode_DB.Checked == true)
    //        _DocRcvdBankCodes = "Sort";
    //    if (rdbChipUID_DB.Checked == true)
    //        _DocRcvdBankCodes = "UID";
    //    if (rdbChipsABAno_DB.Checked == true)
    //        _DocRcvdBankCodes = "Chip";
    //    if (rdbABAno_DB.Checked == true)
    //        _DocRcvdBankCodes = "ABA";
    //    pDocRcvdBankCodes.Value = _DocRcvdBankCodes;

    //    SqlParameter pcb1 = new SqlParameter("@cb1", SqlDbType.VarChar);
    //    string bit_chk1 = "0";
    //    if (chk1.Checked)
    //        bit_chk1 = "1";
    //    pcb1.Value = bit_chk1;

    //    SqlParameter pcb2 = new SqlParameter("@cb2", SqlDbType.VarChar);
    //    string bit_chk2 = "0";
    //    if (chk2.Checked)
    //        bit_chk2 = "1";
    //    pcb2.Value = bit_chk2;

    //    SqlParameter pcb3 = new SqlParameter("@cb3", SqlDbType.VarChar);
    //    string bit_chk3 = "0";
    //    if (chk3.Checked)
    //        bit_chk3 = "1";
    //    pcb3.Value = bit_chk3;

    //    SqlParameter pcb4 = new SqlParameter("@cb4", SqlDbType.VarChar);
    //    string bit_chk4 = "0";
    //    if (chk4.Checked)
    //        bit_chk4 = "1";
    //    pcb4.Value = bit_chk4;

    //    SqlParameter pcb5 = new SqlParameter("@cb5", SqlDbType.VarChar);
    //    string bit_chk5 = "0";
    //    if (chk5.Checked)
    //        bit_chk5 = "1";
    //    pcb5.Value = bit_chk5;

    //    SqlParameter pcb6 = new SqlParameter("@cb6", SqlDbType.VarChar);
    //    string bit_chk6 = "0";
    //    if (chk6.Checked)
    //        bit_chk6 = "1";
    //    pcb6.Value = bit_chk6;

    //    SqlParameter pCovRemarks = new SqlParameter("@IntimLtrRemarks", SqlDbType.VarChar);
    //    pCovRemarks.Value = txtRemarkCovSchedule.Text.Trim();

    //    SqlParameter pDiscrepancy = new SqlParameter("@Discrepancy", SqlDbType.VarChar);
    //    pDiscrepancy.Value = txtDiscrepancy.Text.Trim();

    //    SqlParameter pMT103 = new SqlParameter("@MT103Remark", SqlDbType.VarChar);
    //    pMT103.Value = txtMT103Remarks.Text.Trim();

    //    SqlParameter pMT202 = new SqlParameter("@MT202Remark", SqlDbType.VarChar);
    //    pMT202.Value = txtMT202Remarks.Text.Trim();

    //    SqlParameter pVoucherRemarks = new SqlParameter("@VoucherRemark", SqlDbType.VarChar);
    //    pVoucherRemarks.Value = txtVoucherRemarks.Text.Trim();


    //    SqlParameter pRegisterRemarks = new SqlParameter("@registerRemark", SqlDbType.VarChar);
    //    pRegisterRemarks.Value = txtRegisterRemarks.Text.Trim();

    //    SqlParameter pUser = new SqlParameter("@user", SqlDbType.VarChar);
    //    pUser.Value = _userName;

    //    SqlParameter pUploadDate = new SqlParameter("@uploadingdate", SqlDbType.VarChar);
    //    pUploadDate.Value = _uploadingDate;

    //    SqlParameter Discrepancy_Fee = new SqlParameter("@Discrepancy_Fee", SqlDbType.VarChar);
    //    Discrepancy_Fee.Value = txtDiscrepanyfee.Text.Trim();

    //    SqlParameter Discrepancy_Date = new SqlParameter("@Discrepancy_Date", SqlDbType.VarChar);
    //    Discrepancy_Date.Value = txtDiscrepanydate.Text.Trim();


    //    string Acceptance = "";
    //    if (chkAcceptance.Checked == true)
    //    {
    //        Acceptance = "Y";
    //    }
    //    SqlParameter AcceptanceRecived = new SqlParameter("@Acceptance", SqlDbType.VarChar);
    //    AcceptanceRecived.Value = Acceptance;

    //    SqlParameter EDDNo = new SqlParameter("@EDDNo", SqlDbType.VarChar);
    //    EDDNo.Value = txtEDDNo.Text;

    //    SqlParameter EDDChk = new SqlParameter("@EDDChk", SqlDbType.VarChar);
    //    if (chk_EDDChk.Checked == true)
    //    {
    //        EDDChk.Value = "Y";
    //    }
    //    else
    //    {
    //        EDDChk.Value = "N";
    //    }

    //    string _script = "";

    //    string _result = objSave.SaveDeleteData(_query, pMode, pBranchCode, pDocNo, pDocPrFx, pDocSrNo, pDocNoYear, pFlcIlcType, pSubDocNo,

    //        pCustAcNo, pOverseasPartyID, pOverseasBankID, pOverseasBankCodes, pIntermediaryBank, pIntermediaryBankCodes, pDateRcvd, pDraftDocNo, pDraftDocDate, pDraftDoc,

    //        pInstituteBank, pAcWithInstiBankCodes, pInvoiceNo, pInvoiceDate, pVariousInvoice, pInvoiceDoc, pAWBno, pAWBdate, pAWBissuedBy,

    //        pAWBdoc, pPackingList, pPackingListDate, pPackingListDoc, pCurr, pCertOfOrigin, pCertOfOriginIssuedBy,

    //        pCertOfOriginDoc, pInsPolicy, pInsPolicyDate, pInsPolicyIssuedBy, pInsPolicydoc, pMisc, pMiscDoc,

    //        pCountryOfOrigin, pCountryCode, pCoveringFrom, pCoveringTo, pCommodity, pQuantity, pDOguaranteeNo,

    //        pDOguaranteeDate, pShipment, pImportLicenceNo, pOGL, pMaturityDate, pBillAmt, pCorresChrgs,

    //        pIntAmt, pOverseasBankRef, pDocRcvdBankCodes, pTerms, pTenor,

    //        pDocRcvd, pcb1, pcb2, pcb3, pcb4, pcb5, pcb6, pCovRemarks, pDiscrepancy,

    //        pMT103, pMT202, pVoucherRemarks, pRegisterRemarks, pUser, pUploadDate, pImportLicenseDate

    //        //,Discrepancy_Fee, Discrepancy_Date, AcceptanceRecived, EDDNo, EDDChk
    //        );

    //    if (_result.Substring(0, 5) == "added")
    //    {
    //        _script = "window.location='TF_ImpAuto_View_NonLC.aspx?result=" + _result + "'";
    //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
    //    }
    //    else
    //    {
    //        if (_result == "updated")
    //        {
    //            _script = "window.location='TF_ImpAuto_View_NonLC.aspx?result=" + _result + "'";
    //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
    //        }
    //        else
    //            labelMessage.Text = _result;
    //    }

    //}

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ImpAuto_View_NonLC.aspx", true);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ImpAuto_View_NonLC.aspx", true);
    }
    protected void txtDocsRcvdBank_TextChanged(object sender, EventArgs e)
    {
        fillDocsRcvdBankDescription();
        rdbACno_DB.Focus();
    }
    protected void txtImporterID_TextChanged(object sender, EventArgs e)
    {
        fillImporterCodeDescription();
        txtDateRcvd.Focus();
    }
    protected void txtOverseasPartyID_TextChanged(object sender, EventArgs e)
    {
        fillOverseasPartyDescription();
        txtOverseasBankID.Focus();
    }
    protected void txtOverseasBankID_TextChanged(object sender, EventArgs e)
    {
        fillOverseasBankDescription();
        rdbACno_OB.Focus();
    }
    protected void txtIntermediaryBankID_TextChanged(object sender, EventArgs e)
    {
        fillIntermediaryBankDescription();
        rdbACno_IB.Focus();
    }
    protected void txtAcwithInstitution_TextChanged(object sender, EventArgs e)
    {
        fillAcWithInstiBankDescription();
        rdbACno_AI.Focus();
    }
    protected void btnCopy_Click(object sender, EventArgs e)
    {
        fillDetails_CopyFrom();
    }
    protected void fillDetails_CopyFrom()
    {
        SqlParameter p1 = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
        p1.Value = txtCopyFromDocNo.Text.Trim();
        SqlParameter p3 = new SqlParameter("@DocType", SqlDbType.VarChar);
        p3.Value = Request.QueryString["DocPrFx"].ToString();
        SqlParameter p4 = new SqlParameter("@subDocNo", SqlDbType.VarChar);
        p4.Value = "1";

        string _query = "TF_ImpAuto_GetImportEntryDetails";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p3, p4);

        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["Document_Type"].ToString().Trim() == "C")
            {
                rdbSight.Visible = true;
                rdbUsance.Visible = true;
                string _cType = dt.Rows[0]["Document_Sight_Usance"].ToString().Trim();
                if (_cType == "S")
                {
                    rdbSight.Checked = true;
                    rdbUsance.Checked = false;
                }
                if (_cType == "U")
                {
                    rdbUsance.Checked = true;
                    rdbSight.Checked = false;
                }

            }
            txtImporterID.Text = dt.Rows[0]["CustAcNo"].ToString().Trim();
            fillImporterCodeDescription();

            txtOverseasPartyID.Text = dt.Rows[0]["Overseas_Party_Code"].ToString().Trim();
            fillOverseasPartyDescription();
            txtOverseasBankID.Text = dt.Rows[0]["Overseas_Bank_Code"].ToString().Trim();
            fillOverseasBankDescription();
            if (dt.Rows[0]["OverseasAcInd"].ToString().Trim() != "")
            {
                switch (dt.Rows[0]["OverseasAcInd"].ToString().Trim())
                {
                    case "AcNo":
                        rdbACno_OB.Checked = true;
                        break;
                    case "Sort":
                        rdbSortCode_OB.Checked = true;
                        break;
                    case "UID":
                        rdbChipUID_OB.Checked = true;
                        break;
                    case "Chip":
                        rdbChipsABAno_OB.Checked = true;
                        break;
                    case "ABA":
                        rdbABAno_OB.Checked = true;
                        break;
                }
            }

            txtIntermediaryBankID.Text = dt.Rows[0]["Intermediary_Bank"].ToString().Trim();
            fillIntermediaryBankDescription();
            if (dt.Rows[0]["IntermediaryAcInd"].ToString().Trim() != "")
            {
                switch (dt.Rows[0]["IntermediaryAcInd"].ToString().Trim())
                {
                    case "AcNo":
                        rdbACno_IB.Checked = true;
                        break;
                    case "Sort":
                        rdbSortCode_IB.Checked = true;
                        break;
                    case "UID":
                        rdbChipUID_IB.Checked = true;
                        break;
                    case "Chip":
                        rdbChipsABAno_IB.Checked = true;
                        break;
                    case "ABA":
                        rdbABAno_IB.Checked = true;
                        break;
                }
            }

            txtDraftNo.Text = dt.Rows[0]["Draft_Doc_No"].ToString().Trim();
            txtDraftDocDate.Text = dt.Rows[0]["Draft_Doc_Date"].ToString().Trim();
            txtDraftDoc.Text = dt.Rows[0]["Draft_Doc"].ToString().Trim();

            txtAcwithInstitution.Text = dt.Rows[0]["Institute_Bank"].ToString().Trim();
            fillAcWithInstiBankDescription();
            if (dt.Rows[0]["InstituteAcInd"].ToString().Trim() != "")
            {
                switch (dt.Rows[0]["InstituteAcInd"].ToString().Trim())
                {
                    case "AcNo":
                        rdbACno_AI.Checked = true;
                        break;
                    case "Sort":
                        rdbSortCode_AI.Checked = true;
                        break;
                    case "UID":
                        rdbChipUID_AI.Checked = true;
                        break;
                    case "Chip":
                        rdbChipsABAno_AI.Checked = true;
                        break;
                    case "ABA":
                        rdbABAno_AI.Checked = true;
                        break;
                }
            }

            txtInvoiceNo.Text = dt.Rows[0]["Invoice_No"].ToString().Trim();
            txtInvoiceDate.Text = dt.Rows[0]["Invoice_Date"].ToString().Trim();

            string _VariousInvoice = dt.Rows[0]["VariousInvoice"].ToString().Trim();
            if (_VariousInvoice == "True")
            {
                chkVariousInvoices.Checked = true;
            }
            txtInvoiceDoc.Text = dt.Rows[0]["Invoice_Doc"].ToString().Trim();

            txtAWBno.Text = dt.Rows[0]["AWB_No"].ToString().Trim();
            txtAWBDate.Text = dt.Rows[0]["AWB_Date"].ToString().Trim();
            txtAwbIssuedBy.Text = dt.Rows[0]["AWB_IssuedBy"].ToString().Trim();
            txtAWBDoc.Text = dt.Rows[0]["AWB_Doc"].ToString().Trim();

            txtPackingList.Text = dt.Rows[0]["Packing_List"].ToString().Trim();
            txtPackingListDate.Text = dt.Rows[0]["Packing_List_Date"].ToString().Trim();
            txtPackingDoc.Text = dt.Rows[0]["Packing_List_Doc"].ToString().Trim();

            txtCertOfOrigin.Text = dt.Rows[0]["Cert_of_Origin"].ToString().Trim();
            txtCertIssuedBy.Text = dt.Rows[0]["Cert_of_Origin_IssuedBy"].ToString().Trim();
            txtCertOfOriginDoc.Text = dt.Rows[0]["Cert_of_Origin_Doc"].ToString().Trim();

            txtInsPolicy.Text = dt.Rows[0]["Ins_Policy"].ToString().Trim();
            txtInsPolicyDate.Text = dt.Rows[0]["Ins_Policy_Date"].ToString().Trim();
            txtInsPolicyIssuedBy.Text = dt.Rows[0]["Ins_Policy_IssuedBy"].ToString().Trim();
            txtInsPolicyDoc.Text = dt.Rows[0]["Ins_Policy_Doc"].ToString().Trim();

            txtMiscellaneous.Text = dt.Rows[0]["Miscellaneous"].ToString().Trim();
            txtMiscDoc.Text = dt.Rows[0]["Miscellaneous_Doc"].ToString().Trim();

            if (dt.Rows[0]["Country_of_Origin"].ToString().Trim() != "")
            {
                ddlCountryOfOrigin.SelectedValue = dt.Rows[0]["Country_of_Origin"].ToString().Trim();
            }
            else
                ddlCountryOfOrigin.SelectedIndex = -1;

            if (dt.Rows[0]["Country_Code"].ToString().Trim() != "")
            {
                ddlCountry.SelectedValue = dt.Rows[0]["Country_Code"].ToString().Trim();
            }
            else
                ddlCountry.SelectedIndex = -1;

            txtCoveringFrom.Text = dt.Rows[0]["Covering_From"].ToString().Trim();
            txtCoveringTo.Text = dt.Rows[0]["Covering_To"].ToString().Trim();
            txtCommodity.Text = dt.Rows[0]["Commodity"].ToString().Trim();
            txtQuantity.Text = dt.Rows[0]["Quantity"].ToString().Trim();

            txtDoGauranteeNo.Text = dt.Rows[0]["DO_Guarantee_No"].ToString().Trim();
            txtDoGauranteeDate.Text = dt.Rows[0]["Do_Guarantee_Date"].ToString().Trim();

            string _shipment = dt.Rows[0]["Steamer"].ToString().Trim();
            if (_shipment == "A")
                rdbByAir.Checked = true;
            if (_shipment == "S")
                rdbBySea.Checked = true;
            if (_shipment == "R")
                rdbByRoad.Checked = true;

            txtImportLicenceNo.Text = dt.Rows[0]["Import_Licence_No"].ToString().Trim();
            txtImpLiceDate.Text = dt.Rows[0]["License_Date"].ToString().Trim();
            txtOGL.Text = dt.Rows[0]["OGL"].ToString().Trim();

            if (dt.Rows[0]["Currency"].ToString().Trim() != "")
            {
                dropDownListCurrency.SelectedValue = dt.Rows[0]["Currency"].ToString().Trim();
            }
            else
                dropDownListCurrency.SelectedIndex = -1;

            txtMaturityDate.Text = dt.Rows[0]["Maturity_Date"].ToString().Trim();

            txtOverseasBankRef.Text = dt.Rows[0]["Overseas_BankRef"].ToString().Trim();
            txtTerms.Text = dt.Rows[0]["Terms"].ToString().Trim();
            txtTenor.Text = dt.Rows[0]["Tenor"].ToString().Trim();

            txtDocsRcvdBank.Text = dt.Rows[0]["Beneficiary_Bank"].ToString().Trim();
            fillDocsRcvdBankDescription();
            if (dt.Rows[0]["BenAcInd"].ToString().Trim() != "")
            {
                switch (dt.Rows[0]["BenAcInd"].ToString().Trim())
                {
                    case "AcNo":
                        rdbACno_DB.Checked = true;
                        break;
                    case "Sort":
                        rdbSortCode_DB.Checked = true;
                        break;
                    case "UID":
                        rdbChipUID_DB.Checked = true;
                        break;
                    case "Chip":
                        rdbChipsABAno_DB.Checked = true;
                        break;
                    case "ABA":
                        rdbABAno_DB.Checked = true;
                        break;
                }
            }

            txtRemarkCovSchedule.Text = dt.Rows[0]["Intimation_letter_Remarks"].ToString().Trim();
            txtRegisterRemarks.Text = dt.Rows[0]["Document_Remarks"].ToString().Trim();
            txtDiscrepancy.Text = dt.Rows[0]["Discrepancy"].ToString().Trim();
            txtMT103Remarks.Text = dt.Rows[0]["MT103Remark"].ToString().Trim();
            txtMT202Remarks.Text = dt.Rows[0]["MT202Remark"].ToString().Trim();
            txtVoucherRemarks.Text = dt.Rows[0]["VoucherRemark"].ToString().Trim();

        }
        else
        {
            if (txtCopyFromDocNo.Text.Trim() != "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('Invalid Document No.');", true);
                txtCopyFromDocNo.Focus();
            }
        }
    }
    protected void txtOverseasBankRef_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@OverseasBankRefNo", SqlDbType.VarChar);
        p1.Value = txtOverseasBankRef.Text;
        string _query = "TF_ImpAuto_IMP_CheckOverseasBankRefNo";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            //Document_No
            txtOverseasBankRef.Text = "";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "alert('Overseas Bank Reference No is already assigned to " + dt.Rows[0]["Document_No"].ToString() + " ')", true);
        }
    }

    protected void chk_EDDChk_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_EDDChk.Checked == true)
        {
            lblEDDChk.Text = "Y";
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Please Enter EDD No.')", true);
            txtEDDNo.Enabled = true;
            txtEDDNo.Focus();
        }
        else
        {
            lblEDDChk.Text = "N";
            txtEDDNo.Text = "";
            txtEDDNo.Enabled = false;
        }
    }

    protected void btnProcess_Click(object sender, EventArgs e)
    {
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _mode = Request.QueryString["mode"].Trim();

        TF_DATA objSave = new TF_DATA();
        string _query = "TF_ImpAuto_UpdateImportEntryDetails";

        SqlParameter pMode = new SqlParameter("@mode", SqlDbType.VarChar);
        pMode.Value = _mode;

        SqlParameter pBranchCode = new SqlParameter("@bCode", SqlDbType.VarChar);
        pBranchCode.Value = Request.QueryString["BranchCode"].ToString();

        SqlParameter pDocNo = new SqlParameter("@docNo", SqlDbType.VarChar);
        pDocNo.Value = Request.QueryString["DocNo"].ToString();

        SqlParameter pDocPrFx = new SqlParameter("@docPrFx", SqlDbType.VarChar);
        pDocPrFx.Value = Request.QueryString["DocPrFx"].ToString();
        string _docType = Request.QueryString["DocPrFx"].ToString();

        SqlParameter pDocSrNo = new SqlParameter("@docSrNo", SqlDbType.VarChar);
        pDocSrNo.Value = Request.QueryString["DocSrNo"].ToString();

        SqlParameter pDocNoYear = new SqlParameter("@docNoYear", SqlDbType.VarChar);
        pDocNoYear.Value = Request.QueryString["DocYear"].ToString();

        SqlParameter pFlcIlcType = new SqlParameter("@flcIlcType", SqlDbType.VarChar);
        if (_docType == "C")
        {
            if (rdbSight.Checked == true)
                pFlcIlcType.Value = "S";
            if (rdbUsance.Checked == true)
                pFlcIlcType.Value = "U";

        }
        else
            pFlcIlcType.Value = Request.QueryString["flcIlcType"].ToString();

        SqlParameter pSubDocNo = new SqlParameter("@subDocNo", SqlDbType.VarChar);
        pSubDocNo.Value = txtSubDocNo.Text.Trim();

        SqlParameter pCustAcNo = new SqlParameter("@custAcNo", SqlDbType.VarChar);
        pCustAcNo.Value = txtImporterID.Text.Trim();

        SqlParameter pOverseasPartyID = new SqlParameter("@OverseasPartyID", SqlDbType.VarChar);
        pOverseasPartyID.Value = txtOverseasPartyID.Text.Trim();

        SqlParameter pOverseasBankID = new SqlParameter("@OverseasBankID", SqlDbType.VarChar);
        pOverseasBankID.Value = txtOverseasBankID.Text.Trim();

        SqlParameter pOverseasBankCodes = new SqlParameter("@OverseasBankCodes", SqlDbType.VarChar);
        string _OverseasBankCodes = "";
        if (rdbACno_OB.Checked == true)
            _OverseasBankCodes = "AcNo";
        if (rdbSortCode_OB.Checked == true)
            _OverseasBankCodes = "Sort";
        if (rdbChipUID_OB.Checked == true)
            _OverseasBankCodes = "UID";
        if (rdbChipsABAno_OB.Checked == true)
            _OverseasBankCodes = "Chip";
        if (rdbABAno_OB.Checked == true)
            _OverseasBankCodes = "ABA";
        pOverseasBankCodes.Value = _OverseasBankCodes;

        SqlParameter pIntermediaryBank = new SqlParameter("@IntermediaryBank", SqlDbType.VarChar);
        pIntermediaryBank.Value = txtIntermediaryBankID.Text.Trim();

        SqlParameter pIntermediaryBankCodes = new SqlParameter("@IntermediaryBankCodes", SqlDbType.VarChar);
        string _IntermediaryBankCodes = "";
        if (rdbACno_IB.Checked == true)
            _IntermediaryBankCodes = "AcNo";
        if (rdbSortCode_IB.Checked == true)
            _IntermediaryBankCodes = "Sort";
        if (rdbChipUID_IB.Checked == true)
            _IntermediaryBankCodes = "UID";
        if (rdbChipsABAno_IB.Checked == true)
            _IntermediaryBankCodes = "Chip";
        if (rdbABAno_IB.Checked == true)
            _IntermediaryBankCodes = "ABA";
        pIntermediaryBankCodes.Value = _IntermediaryBankCodes;

        SqlParameter pDateRcvd = new SqlParameter("@dateRcvd", SqlDbType.VarChar);
        pDateRcvd.Value = txtDateRcvd.Text.Trim();

        SqlParameter pDraftDocNo = new SqlParameter("@draftDocNo", SqlDbType.VarChar);
        pDraftDocNo.Value = txtDraftNo.Text.Trim();

        SqlParameter pDraftDocDate = new SqlParameter("@draftDocDate", SqlDbType.VarChar);
        pDraftDocDate.Value = txtDraftDocDate.Text.Trim();

        SqlParameter pDraftDoc = new SqlParameter("@draftDoc", SqlDbType.VarChar);
        pDraftDoc.Value = txtDraftDoc.Text.Trim();

        SqlParameter pInstituteBank = new SqlParameter("@instituteBank", SqlDbType.VarChar);
        pInstituteBank.Value = txtAcwithInstitution.Text.Trim();

        SqlParameter pAcWithInstiBankCodes = new SqlParameter("@AcWithInstiBankCodes", SqlDbType.VarChar);
        string _AcWithInstiBankCodes = "";
        if (rdbACno_AI.Checked == true)
            _AcWithInstiBankCodes = "AcNo";
        if (rdbSortCode_AI.Checked == true)
            _AcWithInstiBankCodes = "Sort";
        if (rdbChipUID_AI.Checked == true)
            _AcWithInstiBankCodes = "UID";
        if (rdbChipsABAno_AI.Checked == true)
            _AcWithInstiBankCodes = "Chip";
        if (rdbABAno_AI.Checked == true)
            _AcWithInstiBankCodes = "ABA";
        pAcWithInstiBankCodes.Value = _AcWithInstiBankCodes;

        SqlParameter pInvoiceNo = new SqlParameter("@invoiceNo", SqlDbType.VarChar);
        pInvoiceNo.Value = txtInvoiceNo.Text.Trim();

        SqlParameter pInvoiceDate = new SqlParameter("@invoiceDate", SqlDbType.VarChar);
        pInvoiceDate.Value = txtInvoiceDate.Text.Trim();

        SqlParameter pVariousInvoice = new SqlParameter("@variousInvoice", SqlDbType.VarChar);
        string bit_VariousInvoice = "0";
        if (chkVariousInvoices.Checked)
            bit_VariousInvoice = "1";
        pVariousInvoice.Value = bit_VariousInvoice;

        SqlParameter pInvoiceDoc = new SqlParameter("@InvoiceDoc", SqlDbType.VarChar);
        pInvoiceDoc.Value = txtInvoiceDoc.Text.Trim();

        SqlParameter pAWBno = new SqlParameter("@AWBno", SqlDbType.VarChar);
        pAWBno.Value = txtAWBno.Text.Trim();

        SqlParameter pAWBdate = new SqlParameter("@AWBdate", SqlDbType.VarChar);
        pAWBdate.Value = txtAWBDate.Text.Trim();

        SqlParameter pAWBissuedBy = new SqlParameter("@AWBissuedBy", SqlDbType.VarChar);
        pAWBissuedBy.Value = txtAwbIssuedBy.Text.Trim();

        SqlParameter pAWBdoc = new SqlParameter("@AWBdoc", SqlDbType.VarChar);
        pAWBdoc.Value = txtAWBDoc.Text.Trim();

        SqlParameter pPackingList = new SqlParameter("@PackingList", SqlDbType.VarChar);
        pPackingList.Value = txtPackingList.Text.Trim();

        SqlParameter pPackingListDate = new SqlParameter("@PackingListDate", SqlDbType.VarChar);
        pPackingListDate.Value = txtPackingListDate.Text.Trim();

        SqlParameter pPackingListDoc = new SqlParameter("@PackingListDoc", SqlDbType.VarChar);
        pPackingListDoc.Value = txtPackingDoc.Text.Trim();

        SqlParameter pCertOfOrigin = new SqlParameter("@certOfOrigin", SqlDbType.VarChar);
        pCertOfOrigin.Value = txtCertOfOrigin.Text.Trim();

        SqlParameter pCertOfOriginIssuedBy = new SqlParameter("@certOfOriginIssuedBy", SqlDbType.VarChar);
        pCertOfOriginIssuedBy.Value = txtCertIssuedBy.Text.Trim();

        SqlParameter pCertOfOriginDoc = new SqlParameter("@certOfOriginDoc", SqlDbType.VarChar);
        pCertOfOriginDoc.Value = txtCertOfOriginDoc.Text.Trim();

        SqlParameter pInsPolicy = new SqlParameter("@InsPolicy", SqlDbType.VarChar);
        pInsPolicy.Value = txtInsPolicy.Text.Trim();

        SqlParameter pInsPolicyDate = new SqlParameter("@InsPolicyDate", SqlDbType.VarChar);
        pInsPolicyDate.Value = txtInsPolicyDate.Text.Trim();

        SqlParameter pInsPolicyIssuedBy = new SqlParameter("@InsPolicyIssuedBy", SqlDbType.VarChar);
        pInsPolicyIssuedBy.Value = txtInsPolicyIssuedBy.Text.Trim();

        SqlParameter pInsPolicydoc = new SqlParameter("@InsPolicyDoc", SqlDbType.VarChar);
        pInsPolicydoc.Value = txtInsPolicyDoc.Text.Trim();

        SqlParameter pMisc = new SqlParameter("@Miscellaneous", SqlDbType.VarChar);
        pMisc.Value = txtMiscellaneous.Text.Trim();

        SqlParameter pMiscDoc = new SqlParameter("@MiscellaneousDoc", SqlDbType.VarChar);
        pMiscDoc.Value = txtMiscDoc.Text.Trim();

        SqlParameter pCountryOfOrigin = new SqlParameter("@CountryOfOrigin", SqlDbType.VarChar);
        if (ddlCountryOfOrigin.SelectedIndex > 0)
            pCountryOfOrigin.Value = ddlCountryOfOrigin.SelectedValue.Trim();
        else
            pCountryOfOrigin.Value = "";

        SqlParameter pCountryCode = new SqlParameter("@CountryCode", SqlDbType.VarChar);
        if (ddlCountry.SelectedIndex > 0)
            pCountryCode.Value = ddlCountry.SelectedValue.Trim();
        else
            pCountryCode.Value = "";

        SqlParameter pCoveringFrom = new SqlParameter("@CoveringFrom", SqlDbType.VarChar);
        pCoveringFrom.Value = txtCoveringFrom.Text.Trim();

        SqlParameter pCoveringTo = new SqlParameter("@CoveringTo", SqlDbType.VarChar);
        pCoveringTo.Value = txtCoveringTo.Text.Trim();

        SqlParameter pCommodity = new SqlParameter("@Commodity", SqlDbType.VarChar);
        pCommodity.Value = txtCommodity.Text.Trim();

        SqlParameter pQuantity = new SqlParameter("@Quantity", SqlDbType.VarChar);
        pQuantity.Value = txtQuantity.Text.Trim();

        SqlParameter pDOguaranteeNo = new SqlParameter("@DOguaranteeNo", SqlDbType.VarChar);
        pDOguaranteeNo.Value = txtDoGauranteeNo.Text.Trim();

        SqlParameter pDOguaranteeDate = new SqlParameter("@DOguaranteeDate", SqlDbType.VarChar);
        pDOguaranteeDate.Value = txtDoGauranteeDate.Text.Trim();

        SqlParameter pShipment = new SqlParameter("@Steamer", SqlDbType.VarChar);
        string _Shipment = "";
        if (rdbByAir.Checked)
            _Shipment = "A";
        else if (rdbBySea.Checked)
            _Shipment = "S";
        else if (rdbByRoad.Checked)
            _Shipment = "R";
        pShipment.Value = _Shipment;

        SqlParameter pImportLicenceNo = new SqlParameter("@ImportLicenceNo", SqlDbType.VarChar);
        pImportLicenceNo.Value = txtImportLicenceNo.Text.Trim();

        SqlParameter pImportLicenseDate = new SqlParameter("@ImportLicenseDate", SqlDbType.VarChar);
        pImportLicenseDate.Value = txtImpLiceDate.Text.ToString().Trim();

        SqlParameter pOGL = new SqlParameter("@OGL", SqlDbType.VarChar);
        pOGL.Value = txtOGL.Text.Trim();

        SqlParameter pCurr = new SqlParameter("@Curr", SqlDbType.VarChar);
        if (dropDownListCurrency.SelectedIndex > 0)
            pCurr.Value = dropDownListCurrency.SelectedValue.Trim();
        else
            pCurr.Value = "";

        SqlParameter pMaturityDate = new SqlParameter("@maturityDate", SqlDbType.VarChar);
        pMaturityDate.Value = txtMaturityDate.Text.Trim();

        SqlParameter pBillAmt = new SqlParameter("@billAmt", SqlDbType.VarChar);
        pBillAmt.Value = txtBillAmount.Text.Trim();

        SqlParameter pCorresChrgs = new SqlParameter("@corresChrgs", SqlDbType.VarChar);
        pCorresChrgs.Value = txtCorrespChrgs.Text.Trim();

        SqlParameter pIntAmt = new SqlParameter("@intAmt", SqlDbType.VarChar);
        pIntAmt.Value = txtInterestAmt.Text.Trim();


        SqlParameter pOverseasBankRef = new SqlParameter("@OverseasBankRef", SqlDbType.VarChar);
        pOverseasBankRef.Value = txtOverseasBankRef.Text.Trim();

        SqlParameter pTerms = new SqlParameter("@terms", SqlDbType.VarChar);
        pTerms.Value = txtTerms.Text.Trim();

        SqlParameter pTenor = new SqlParameter("@tenor", SqlDbType.VarChar);
        pTenor.Value = txtTenor.Text.Trim();

        SqlParameter pDocRcvd = new SqlParameter("@Beneficiary_Bank", SqlDbType.VarChar);
        pDocRcvd.Value = txtDocsRcvdBank.Text.Trim();

        SqlParameter pDocRcvdBankCodes = new SqlParameter("@DocRcvdBankCodes", SqlDbType.VarChar);
        string _DocRcvdBankCodes = "";
        if (rdbACno_DB.Checked == true)
            _DocRcvdBankCodes = "AcNo";
        if (rdbSortCode_DB.Checked == true)
            _DocRcvdBankCodes = "Sort";
        if (rdbChipUID_DB.Checked == true)
            _DocRcvdBankCodes = "UID";
        if (rdbChipsABAno_DB.Checked == true)
            _DocRcvdBankCodes = "Chip";
        if (rdbABAno_DB.Checked == true)
            _DocRcvdBankCodes = "ABA";
        pDocRcvdBankCodes.Value = _DocRcvdBankCodes;

        SqlParameter pcb1 = new SqlParameter("@cb1", SqlDbType.VarChar);
        string bit_chk1 = "0";
        if (chk1.Checked)
            bit_chk1 = "1";
        pcb1.Value = bit_chk1;

        SqlParameter pcb2 = new SqlParameter("@cb2", SqlDbType.VarChar);
        string bit_chk2 = "0";
        if (chk2.Checked)
            bit_chk2 = "1";
        pcb2.Value = bit_chk2;

        SqlParameter pcb3 = new SqlParameter("@cb3", SqlDbType.VarChar);
        string bit_chk3 = "0";
        if (chk3.Checked)
            bit_chk3 = "1";
        pcb3.Value = bit_chk3;

        SqlParameter pcb4 = new SqlParameter("@cb4", SqlDbType.VarChar);
        string bit_chk4 = "0";
        if (chk4.Checked)
            bit_chk4 = "1";
        pcb4.Value = bit_chk4;

        SqlParameter pcb5 = new SqlParameter("@cb5", SqlDbType.VarChar);
        string bit_chk5 = "0";
        if (chk5.Checked)
            bit_chk5 = "1";
        pcb5.Value = bit_chk5;

        SqlParameter pcb6 = new SqlParameter("@cb6", SqlDbType.VarChar);
        string bit_chk6 = "0";
        if (chk6.Checked)
            bit_chk6 = "1";
        pcb6.Value = bit_chk6;

        SqlParameter pCovRemarks = new SqlParameter("@IntimLtrRemarks", SqlDbType.VarChar);
        pCovRemarks.Value = txtRemarkCovSchedule.Text.Trim();

        SqlParameter pDiscrepancy = new SqlParameter("@Discrepancy", SqlDbType.VarChar);
        pDiscrepancy.Value = txtDiscrepancy.Text.Trim();

        SqlParameter pMT103 = new SqlParameter("@MT103Remark", SqlDbType.VarChar);
        pMT103.Value = txtMT103Remarks.Text.Trim();

        SqlParameter pMT202 = new SqlParameter("@MT202Remark", SqlDbType.VarChar);
        pMT202.Value = txtMT202Remarks.Text.Trim();

        SqlParameter pVoucherRemarks = new SqlParameter("@VoucherRemark", SqlDbType.VarChar);
        pVoucherRemarks.Value = txtVoucherRemarks.Text.Trim();


        SqlParameter pRegisterRemarks = new SqlParameter("@registerRemark", SqlDbType.VarChar);
        pRegisterRemarks.Value = txtRegisterRemarks.Text.Trim();

        SqlParameter pUser = new SqlParameter("@user", SqlDbType.VarChar);
        pUser.Value = _userName;

        SqlParameter pUploadDate = new SqlParameter("@uploadingdate", SqlDbType.VarChar);
        pUploadDate.Value = _uploadingDate;

        SqlParameter Discrepancy_Fee = new SqlParameter("@Discrepancy_Fee", SqlDbType.VarChar);
        Discrepancy_Fee.Value = txtDiscrepanyfee.Text.Trim();

        SqlParameter Discrepancy_Date = new SqlParameter("@Discrepancy_Date", SqlDbType.VarChar);
        Discrepancy_Date.Value = txtDiscrepanydate.Text.Trim();

        // new addition 
        SqlParameter pStatus = new SqlParameter("@Status", SqlDbType.VarChar);
        pStatus.Value = "PROCESS";


        string Acceptance = "";
        if (chkAcceptance.Checked == true)
        {
            Acceptance = "Y";
        }
        SqlParameter AcceptanceRecived = new SqlParameter("@Acceptance", SqlDbType.VarChar);
        AcceptanceRecived.Value = Acceptance;

        SqlParameter EDDNo = new SqlParameter("@EDDNo", SqlDbType.VarChar);
        EDDNo.Value = txtEDDNo.Text;

        SqlParameter EDDChk = new SqlParameter("@EDDChk", SqlDbType.VarChar);
        if (chk_EDDChk.Checked == true)
        {
            EDDChk.Value = "Y";
        }
        else
        {
            EDDChk.Value = "N";
        }

        string _script = "";

        string _result = objSave.SaveDeleteData(_query, pMode, pBranchCode, pDocNo, pDocPrFx, pDocSrNo, pDocNoYear, pFlcIlcType, pSubDocNo,

            pCustAcNo, pOverseasPartyID, pOverseasBankID, pOverseasBankCodes, pIntermediaryBank, pIntermediaryBankCodes, pDateRcvd, pDraftDocNo, pDraftDocDate, pDraftDoc,

            pInstituteBank, pAcWithInstiBankCodes, pInvoiceNo, pInvoiceDate, pVariousInvoice, pInvoiceDoc, pAWBno, pAWBdate, pAWBissuedBy,

            pAWBdoc, pPackingList, pPackingListDate, pPackingListDoc, pCurr, pCertOfOrigin, pCertOfOriginIssuedBy,

            pCertOfOriginDoc, pInsPolicy, pInsPolicyDate, pInsPolicyIssuedBy, pInsPolicydoc, pMisc, pMiscDoc,

            pCountryOfOrigin, pCountryCode, pCoveringFrom, pCoveringTo, pCommodity, pQuantity, pDOguaranteeNo,

            pDOguaranteeDate, pShipment, pImportLicenceNo, pOGL, pMaturityDate, pBillAmt, pCorresChrgs,

            pIntAmt, pOverseasBankRef, pDocRcvdBankCodes, pTerms, pTenor,

            pDocRcvd, pcb1, pcb2, pcb3, pcb4, pcb5, pcb6, pCovRemarks, pDiscrepancy,

            pMT103, pMT202, pVoucherRemarks, pRegisterRemarks, pUser, pUploadDate, pImportLicenseDate, pStatus


            //,Discrepancy_Fee, Discrepancy_Date, AcceptanceRecived, EDDNo, EDDChk
            );

        if (_result.Substring(0, 5) == "added")
        {
            _script = "window.location='TF_ImpAuto_View_NonLC_Checker.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (_result == "updated")
            {
                TF_DATA objdat = new TF_DATA();
                string _query1 = "TF_ImpAuto_UpdateRemark";
                
                SqlParameter remark = new SqlParameter("@remark",SqlDbType.VarChar);
                remark.Value=txtRemarks.Text;

                string _result1 = objdat.SaveDeleteData(_query1, remark, pDocNo, pSubDocNo);

                if (_result1 == "update")
                {
                    _script = "window.location='TF_ImpAuto_View_NonLC_Checker.aspx?result=" + _result + "'";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                }
            }
            else
                labelMessage.Text = _result;
        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _mode = Request.QueryString["mode"].Trim();

        TF_DATA objSave = new TF_DATA();
        string _query = "TF_ImpAuto_UpdateImportEntryDetails";

        SqlParameter pMode = new SqlParameter("@mode", SqlDbType.VarChar);
        pMode.Value = _mode;

        SqlParameter pBranchCode = new SqlParameter("@bCode", SqlDbType.VarChar);
        pBranchCode.Value = Request.QueryString["BranchCode"].ToString();

        SqlParameter pDocNo = new SqlParameter("@docNo", SqlDbType.VarChar);
        pDocNo.Value = Request.QueryString["DocNo"].ToString();

        SqlParameter pDocPrFx = new SqlParameter("@docPrFx", SqlDbType.VarChar);
        pDocPrFx.Value = Request.QueryString["DocPrFx"].ToString();
        string _docType = Request.QueryString["DocPrFx"].ToString();

        SqlParameter pDocSrNo = new SqlParameter("@docSrNo", SqlDbType.VarChar);
        pDocSrNo.Value = Request.QueryString["DocSrNo"].ToString();

        SqlParameter pDocNoYear = new SqlParameter("@docNoYear", SqlDbType.VarChar);
        pDocNoYear.Value = Request.QueryString["DocYear"].ToString();

        SqlParameter pFlcIlcType = new SqlParameter("@flcIlcType", SqlDbType.VarChar);
        if (_docType == "C")
        {
            if (rdbSight.Checked == true)
                pFlcIlcType.Value = "S";
            if (rdbUsance.Checked == true)
                pFlcIlcType.Value = "U";

        }
        else
            pFlcIlcType.Value = Request.QueryString["flcIlcType"].ToString();

        SqlParameter pSubDocNo = new SqlParameter("@subDocNo", SqlDbType.VarChar);
        pSubDocNo.Value = txtSubDocNo.Text.Trim();

        SqlParameter pCustAcNo = new SqlParameter("@custAcNo", SqlDbType.VarChar);
        pCustAcNo.Value = txtImporterID.Text.Trim();

        SqlParameter pOverseasPartyID = new SqlParameter("@OverseasPartyID", SqlDbType.VarChar);
        pOverseasPartyID.Value = txtOverseasPartyID.Text.Trim();

        SqlParameter pOverseasBankID = new SqlParameter("@OverseasBankID", SqlDbType.VarChar);
        pOverseasBankID.Value = txtOverseasBankID.Text.Trim();

        SqlParameter pOverseasBankCodes = new SqlParameter("@OverseasBankCodes", SqlDbType.VarChar);
        string _OverseasBankCodes = "";
        if (rdbACno_OB.Checked == true)
            _OverseasBankCodes = "AcNo";
        if (rdbSortCode_OB.Checked == true)
            _OverseasBankCodes = "Sort";
        if (rdbChipUID_OB.Checked == true)
            _OverseasBankCodes = "UID";
        if (rdbChipsABAno_OB.Checked == true)
            _OverseasBankCodes = "Chip";
        if (rdbABAno_OB.Checked == true)
            _OverseasBankCodes = "ABA";
        pOverseasBankCodes.Value = _OverseasBankCodes;

        SqlParameter pIntermediaryBank = new SqlParameter("@IntermediaryBank", SqlDbType.VarChar);
        pIntermediaryBank.Value = txtIntermediaryBankID.Text.Trim();

        SqlParameter pIntermediaryBankCodes = new SqlParameter("@IntermediaryBankCodes", SqlDbType.VarChar);
        string _IntermediaryBankCodes = "";
        if (rdbACno_IB.Checked == true)
            _IntermediaryBankCodes = "AcNo";
        if (rdbSortCode_IB.Checked == true)
            _IntermediaryBankCodes = "Sort";
        if (rdbChipUID_IB.Checked == true)
            _IntermediaryBankCodes = "UID";
        if (rdbChipsABAno_IB.Checked == true)
            _IntermediaryBankCodes = "Chip";
        if (rdbABAno_IB.Checked == true)
            _IntermediaryBankCodes = "ABA";
        pIntermediaryBankCodes.Value = _IntermediaryBankCodes;

        SqlParameter pDateRcvd = new SqlParameter("@dateRcvd", SqlDbType.VarChar);
        pDateRcvd.Value = txtDateRcvd.Text.Trim();

        SqlParameter pDraftDocNo = new SqlParameter("@draftDocNo", SqlDbType.VarChar);
        pDraftDocNo.Value = txtDraftNo.Text.Trim();

        SqlParameter pDraftDocDate = new SqlParameter("@draftDocDate", SqlDbType.VarChar);
        pDraftDocDate.Value = txtDraftDocDate.Text.Trim();

        SqlParameter pDraftDoc = new SqlParameter("@draftDoc", SqlDbType.VarChar);
        pDraftDoc.Value = txtDraftDoc.Text.Trim();

        SqlParameter pInstituteBank = new SqlParameter("@instituteBank", SqlDbType.VarChar);
        pInstituteBank.Value = txtAcwithInstitution.Text.Trim();

        SqlParameter pAcWithInstiBankCodes = new SqlParameter("@AcWithInstiBankCodes", SqlDbType.VarChar);
        string _AcWithInstiBankCodes = "";
        if (rdbACno_AI.Checked == true)
            _AcWithInstiBankCodes = "AcNo";
        if (rdbSortCode_AI.Checked == true)
            _AcWithInstiBankCodes = "Sort";
        if (rdbChipUID_AI.Checked == true)
            _AcWithInstiBankCodes = "UID";
        if (rdbChipsABAno_AI.Checked == true)
            _AcWithInstiBankCodes = "Chip";
        if (rdbABAno_AI.Checked == true)
            _AcWithInstiBankCodes = "ABA";
        pAcWithInstiBankCodes.Value = _AcWithInstiBankCodes;

        SqlParameter pInvoiceNo = new SqlParameter("@invoiceNo", SqlDbType.VarChar);
        pInvoiceNo.Value = txtInvoiceNo.Text.Trim();

        SqlParameter pInvoiceDate = new SqlParameter("@invoiceDate", SqlDbType.VarChar);
        pInvoiceDate.Value = txtInvoiceDate.Text.Trim();

        SqlParameter pVariousInvoice = new SqlParameter("@variousInvoice", SqlDbType.VarChar);
        string bit_VariousInvoice = "0";
        if (chkVariousInvoices.Checked)
            bit_VariousInvoice = "1";
        pVariousInvoice.Value = bit_VariousInvoice;

        SqlParameter pInvoiceDoc = new SqlParameter("@InvoiceDoc", SqlDbType.VarChar);
        pInvoiceDoc.Value = txtInvoiceDoc.Text.Trim();

        SqlParameter pAWBno = new SqlParameter("@AWBno", SqlDbType.VarChar);
        pAWBno.Value = txtAWBno.Text.Trim();

        SqlParameter pAWBdate = new SqlParameter("@AWBdate", SqlDbType.VarChar);
        pAWBdate.Value = txtAWBDate.Text.Trim();

        SqlParameter pAWBissuedBy = new SqlParameter("@AWBissuedBy", SqlDbType.VarChar);
        pAWBissuedBy.Value = txtAwbIssuedBy.Text.Trim();

        SqlParameter pAWBdoc = new SqlParameter("@AWBdoc", SqlDbType.VarChar);
        pAWBdoc.Value = txtAWBDoc.Text.Trim();

        SqlParameter pPackingList = new SqlParameter("@PackingList", SqlDbType.VarChar);
        pPackingList.Value = txtPackingList.Text.Trim();

        SqlParameter pPackingListDate = new SqlParameter("@PackingListDate", SqlDbType.VarChar);
        pPackingListDate.Value = txtPackingListDate.Text.Trim();

        SqlParameter pPackingListDoc = new SqlParameter("@PackingListDoc", SqlDbType.VarChar);
        pPackingListDoc.Value = txtPackingDoc.Text.Trim();

        SqlParameter pCertOfOrigin = new SqlParameter("@certOfOrigin", SqlDbType.VarChar);
        pCertOfOrigin.Value = txtCertOfOrigin.Text.Trim();

        SqlParameter pCertOfOriginIssuedBy = new SqlParameter("@certOfOriginIssuedBy", SqlDbType.VarChar);
        pCertOfOriginIssuedBy.Value = txtCertIssuedBy.Text.Trim();

        SqlParameter pCertOfOriginDoc = new SqlParameter("@certOfOriginDoc", SqlDbType.VarChar);
        pCertOfOriginDoc.Value = txtCertOfOriginDoc.Text.Trim();

        SqlParameter pInsPolicy = new SqlParameter("@InsPolicy", SqlDbType.VarChar);
        pInsPolicy.Value = txtInsPolicy.Text.Trim();

        SqlParameter pInsPolicyDate = new SqlParameter("@InsPolicyDate", SqlDbType.VarChar);
        pInsPolicyDate.Value = txtInsPolicyDate.Text.Trim();

        SqlParameter pInsPolicyIssuedBy = new SqlParameter("@InsPolicyIssuedBy", SqlDbType.VarChar);
        pInsPolicyIssuedBy.Value = txtInsPolicyIssuedBy.Text.Trim();

        SqlParameter pInsPolicydoc = new SqlParameter("@InsPolicyDoc", SqlDbType.VarChar);
        pInsPolicydoc.Value = txtInsPolicyDoc.Text.Trim();

        SqlParameter pMisc = new SqlParameter("@Miscellaneous", SqlDbType.VarChar);
        pMisc.Value = txtMiscellaneous.Text.Trim();

        SqlParameter pMiscDoc = new SqlParameter("@MiscellaneousDoc", SqlDbType.VarChar);
        pMiscDoc.Value = txtMiscDoc.Text.Trim();

        SqlParameter pCountryOfOrigin = new SqlParameter("@CountryOfOrigin", SqlDbType.VarChar);
        if (ddlCountryOfOrigin.SelectedIndex > 0)
            pCountryOfOrigin.Value = ddlCountryOfOrigin.SelectedValue.Trim();
        else
            pCountryOfOrigin.Value = "";

        SqlParameter pCountryCode = new SqlParameter("@CountryCode", SqlDbType.VarChar);
        if (ddlCountry.SelectedIndex > 0)
            pCountryCode.Value = ddlCountry.SelectedValue.Trim();
        else
            pCountryCode.Value = "";

        SqlParameter pCoveringFrom = new SqlParameter("@CoveringFrom", SqlDbType.VarChar);
        pCoveringFrom.Value = txtCoveringFrom.Text.Trim();

        SqlParameter pCoveringTo = new SqlParameter("@CoveringTo", SqlDbType.VarChar);
        pCoveringTo.Value = txtCoveringTo.Text.Trim();

        SqlParameter pCommodity = new SqlParameter("@Commodity", SqlDbType.VarChar);
        pCommodity.Value = txtCommodity.Text.Trim();

        SqlParameter pQuantity = new SqlParameter("@Quantity", SqlDbType.VarChar);
        pQuantity.Value = txtQuantity.Text.Trim();

        SqlParameter pDOguaranteeNo = new SqlParameter("@DOguaranteeNo", SqlDbType.VarChar);
        pDOguaranteeNo.Value = txtDoGauranteeNo.Text.Trim();

        SqlParameter pDOguaranteeDate = new SqlParameter("@DOguaranteeDate", SqlDbType.VarChar);
        pDOguaranteeDate.Value = txtDoGauranteeDate.Text.Trim();

        SqlParameter pShipment = new SqlParameter("@Steamer", SqlDbType.VarChar);
        string _Shipment = "";
        if (rdbByAir.Checked)
            _Shipment = "A";
        else if (rdbBySea.Checked)
            _Shipment = "S";
        else if (rdbByRoad.Checked)
            _Shipment = "R";
        pShipment.Value = _Shipment;

        SqlParameter pImportLicenceNo = new SqlParameter("@ImportLicenceNo", SqlDbType.VarChar);
        pImportLicenceNo.Value = txtImportLicenceNo.Text.Trim();

        SqlParameter pImportLicenseDate = new SqlParameter("@ImportLicenseDate", SqlDbType.VarChar);
        pImportLicenseDate.Value = txtImpLiceDate.Text.ToString().Trim();

        SqlParameter pOGL = new SqlParameter("@OGL", SqlDbType.VarChar);
        pOGL.Value = txtOGL.Text.Trim();

        SqlParameter pCurr = new SqlParameter("@Curr", SqlDbType.VarChar);
        if (dropDownListCurrency.SelectedIndex > 0)
            pCurr.Value = dropDownListCurrency.SelectedValue.Trim();
        else
            pCurr.Value = "";

        SqlParameter pMaturityDate = new SqlParameter("@maturityDate", SqlDbType.VarChar);
        pMaturityDate.Value = txtMaturityDate.Text.Trim();

        SqlParameter pBillAmt = new SqlParameter("@billAmt", SqlDbType.VarChar);
        pBillAmt.Value = txtBillAmount.Text.Trim();

        SqlParameter pCorresChrgs = new SqlParameter("@corresChrgs", SqlDbType.VarChar);
        pCorresChrgs.Value = txtCorrespChrgs.Text.Trim();

        SqlParameter pIntAmt = new SqlParameter("@intAmt", SqlDbType.VarChar);
        pIntAmt.Value = txtInterestAmt.Text.Trim();


        SqlParameter pOverseasBankRef = new SqlParameter("@OverseasBankRef", SqlDbType.VarChar);
        pOverseasBankRef.Value = txtOverseasBankRef.Text.Trim();

        SqlParameter pTerms = new SqlParameter("@terms", SqlDbType.VarChar);
        pTerms.Value = txtTerms.Text.Trim();

        SqlParameter pTenor = new SqlParameter("@tenor", SqlDbType.VarChar);
        pTenor.Value = txtTenor.Text.Trim();

        SqlParameter pDocRcvd = new SqlParameter("@Beneficiary_Bank", SqlDbType.VarChar);
        pDocRcvd.Value = txtDocsRcvdBank.Text.Trim();

        SqlParameter pDocRcvdBankCodes = new SqlParameter("@DocRcvdBankCodes", SqlDbType.VarChar);
        string _DocRcvdBankCodes = "";
        if (rdbACno_DB.Checked == true)
            _DocRcvdBankCodes = "AcNo";
        if (rdbSortCode_DB.Checked == true)
            _DocRcvdBankCodes = "Sort";
        if (rdbChipUID_DB.Checked == true)
            _DocRcvdBankCodes = "UID";
        if (rdbChipsABAno_DB.Checked == true)
            _DocRcvdBankCodes = "Chip";
        if (rdbABAno_DB.Checked == true)
            _DocRcvdBankCodes = "ABA";
        pDocRcvdBankCodes.Value = _DocRcvdBankCodes;

        SqlParameter pcb1 = new SqlParameter("@cb1", SqlDbType.VarChar);
        string bit_chk1 = "0";
        if (chk1.Checked)
            bit_chk1 = "1";
        pcb1.Value = bit_chk1;

        SqlParameter pcb2 = new SqlParameter("@cb2", SqlDbType.VarChar);
        string bit_chk2 = "0";
        if (chk2.Checked)
            bit_chk2 = "1";
        pcb2.Value = bit_chk2;

        SqlParameter pcb3 = new SqlParameter("@cb3", SqlDbType.VarChar);
        string bit_chk3 = "0";
        if (chk3.Checked)
            bit_chk3 = "1";
        pcb3.Value = bit_chk3;

        SqlParameter pcb4 = new SqlParameter("@cb4", SqlDbType.VarChar);
        string bit_chk4 = "0";
        if (chk4.Checked)
            bit_chk4 = "1";
        pcb4.Value = bit_chk4;

        SqlParameter pcb5 = new SqlParameter("@cb5", SqlDbType.VarChar);
        string bit_chk5 = "0";
        if (chk5.Checked)
            bit_chk5 = "1";
        pcb5.Value = bit_chk5;

        SqlParameter pcb6 = new SqlParameter("@cb6", SqlDbType.VarChar);
        string bit_chk6 = "0";
        if (chk6.Checked)
            bit_chk6 = "1";
        pcb6.Value = bit_chk6;

        SqlParameter pCovRemarks = new SqlParameter("@IntimLtrRemarks", SqlDbType.VarChar);
        pCovRemarks.Value = txtRemarkCovSchedule.Text.Trim();

        SqlParameter pDiscrepancy = new SqlParameter("@Discrepancy", SqlDbType.VarChar);
        pDiscrepancy.Value = txtDiscrepancy.Text.Trim();

        SqlParameter pMT103 = new SqlParameter("@MT103Remark", SqlDbType.VarChar);
        pMT103.Value = txtMT103Remarks.Text.Trim();

        SqlParameter pMT202 = new SqlParameter("@MT202Remark", SqlDbType.VarChar);
        pMT202.Value = txtMT202Remarks.Text.Trim();

        SqlParameter pVoucherRemarks = new SqlParameter("@VoucherRemark", SqlDbType.VarChar);
        pVoucherRemarks.Value = txtVoucherRemarks.Text.Trim();


        SqlParameter pRegisterRemarks = new SqlParameter("@registerRemark", SqlDbType.VarChar);
        pRegisterRemarks.Value = txtRegisterRemarks.Text.Trim();

        SqlParameter pUser = new SqlParameter("@user", SqlDbType.VarChar);
        pUser.Value = _userName;

        SqlParameter pUploadDate = new SqlParameter("@uploadingdate", SqlDbType.VarChar);
        pUploadDate.Value = _uploadingDate;

        SqlParameter Discrepancy_Fee = new SqlParameter("@Discrepancy_Fee", SqlDbType.VarChar);
        Discrepancy_Fee.Value = txtDiscrepanyfee.Text.Trim();

        SqlParameter Discrepancy_Date = new SqlParameter("@Discrepancy_Date", SqlDbType.VarChar);
        Discrepancy_Date.Value = txtDiscrepanydate.Text.Trim();

        // new addition 
        SqlParameter pStatus = new SqlParameter("@Status", SqlDbType.VarChar);
        pStatus.Value = "REJECT";


        string Acceptance = "";
        if (chkAcceptance.Checked == true)
        {
            Acceptance = "Y";
        }
        SqlParameter AcceptanceRecived = new SqlParameter("@Acceptance", SqlDbType.VarChar);
        AcceptanceRecived.Value = Acceptance;

        SqlParameter EDDNo = new SqlParameter("@EDDNo", SqlDbType.VarChar);
        EDDNo.Value = txtEDDNo.Text;

        SqlParameter EDDChk = new SqlParameter("@EDDChk", SqlDbType.VarChar);
        if (chk_EDDChk.Checked == true)
        {
            EDDChk.Value = "Y";
        }
        else
        {
            EDDChk.Value = "N";
        }

        string _script = "";

        string _result = objSave.SaveDeleteData(_query, pMode, pBranchCode, pDocNo, pDocPrFx, pDocSrNo, pDocNoYear, pFlcIlcType, pSubDocNo,

            pCustAcNo, pOverseasPartyID, pOverseasBankID, pOverseasBankCodes, pIntermediaryBank, pIntermediaryBankCodes, pDateRcvd, pDraftDocNo, pDraftDocDate, pDraftDoc,

            pInstituteBank, pAcWithInstiBankCodes, pInvoiceNo, pInvoiceDate, pVariousInvoice, pInvoiceDoc, pAWBno, pAWBdate, pAWBissuedBy,

            pAWBdoc, pPackingList, pPackingListDate, pPackingListDoc, pCurr, pCertOfOrigin, pCertOfOriginIssuedBy,

            pCertOfOriginDoc, pInsPolicy, pInsPolicyDate, pInsPolicyIssuedBy, pInsPolicydoc, pMisc, pMiscDoc,

            pCountryOfOrigin, pCountryCode, pCoveringFrom, pCoveringTo, pCommodity, pQuantity, pDOguaranteeNo,

            pDOguaranteeDate, pShipment, pImportLicenceNo, pOGL, pMaturityDate, pBillAmt, pCorresChrgs,

            pIntAmt, pOverseasBankRef, pDocRcvdBankCodes, pTerms, pTenor,

            pDocRcvd, pcb1, pcb2, pcb3, pcb4, pcb5, pcb6, pCovRemarks, pDiscrepancy,

            pMT103, pMT202, pVoucherRemarks, pRegisterRemarks, pUser, pUploadDate, pImportLicenseDate, pStatus


            //,Discrepancy_Fee, Discrepancy_Date, AcceptanceRecived, EDDNo, EDDChk
            );

        if (_result.Substring(0, 5) == "added")
        {
            _script = "window.location='TF_ImpAuto_View_NonLC_Checker.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (_result == "updated")
            {
                TF_DATA objdat = new TF_DATA();
                string _query1 = "TF_ImpAuto_UpdateRemark";

                SqlParameter remark = new SqlParameter("@remark",SqlDbType.VarChar);
                remark.Value = txtRemarks.Text;

                string _result1 = objdat.SaveDeleteData(_query1,remark, pDocNo, pSubDocNo);

                if (_result1 == "update")
                {
                    _script = "window.location='TF_ImpAuto_View_NonLC_Checker.aspx?result=" + _result + "'";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                }
            }
            else
                labelMessage.Text = _result;
        }
    }
}