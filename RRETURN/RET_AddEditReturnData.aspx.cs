using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
public partial class RRETURN_RET_AddEditReturnData : System.Web.UI.Page
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
                txtDocumentNumber.Focus();
                rdbtradefinance.Checked = true;
                btnSave.Attributes.Add("onclick", "return validateSave();");
                clearControls();
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("RBI_ViewReturnData.aspx", true);
                }
                else
                {
                    //fillCountry();
                    //fillCountryAC();
                    //fillCurrency();
                    //fillPortCode();
                    //fillPurposeCode();
                    fillScheduleCodes();
                    //fillBankCode();
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        lblAdcode.Text = Request.QueryString["Adcode"].ToString();
                        txtBranchName.Text = Request.QueryString["BranchName"].ToString();
                        txtFromDate.Text = Request.QueryString["fromdate"].ToString();
                        txtToDate.Text = Request.QueryString["todate"].ToString();
                        txtSerialNumber.Text = Request.QueryString["srno"].ToString();
                        fillDetails();
                        fillBank();
                    }
                    else
                    {
                        lblAdcode.Text = Request.QueryString["Adcode"].ToString();
                        txtBranchName.Text = Request.QueryString["BranchName"].ToString();
                        txtFromDate.Text = Request.QueryString["fromdate"].ToString();
                        txtToDate.Text = Request.QueryString["todate"].ToString();
                        fillProcessingDate();
                        fillBank();
                        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "toogleDisplayHelp", "toogleDisplayHelp();", true);
                    }
                    txtFromDate.Attributes.Add("onkeypress", "return false;");
                    txtToDate.Attributes.Add("onkeypress", "return false;");
                    txtDocumentDate.Attributes.Add("onkeypress", "return false;");
                    txtShippingBillDate.Attributes.Add("onkeypress", "return false;");
                    txtINRAmount.Attributes.Add("onkeypress", "return false;");
                    txtFromDate.Attributes.Add("oncut", "return false;");
                    txtToDate.Attributes.Add("oncut", "return false;");
                    txtDocumentDate.Attributes.Add("oncut", "return false;");
                    txtShippingBillDate.Attributes.Add("oncut", "return false;");
                    txtINRAmount.Attributes.Add("oncut", "return false;");
                    txtFromDate.Attributes.Add("oncopy", "return false;");
                    txtToDate.Attributes.Add("oncopy", "return false;");
                    txtDocumentDate.Attributes.Add("oncopy", "return false;");
                    txtShippingBillDate.Attributes.Add("oncopy", "return false;");
                    txtINRAmount.Attributes.Add("oncopy", "return false;");
                    txtFromDate.Attributes.Add("onpaste", "return false;");
                    txtToDate.Attributes.Add("onpaste", "return false;");
                    txtToDate.Attributes.Add("onfocus", "blur();");
                    txtDocumentDate.Attributes.Add("onpaste", "return false;");
                    txtShippingBillDate.Attributes.Add("onpaste", "return false;");
                    txtINRAmount.Attributes.Add("onpaste", "return false;");
                    txtFromDate.Attributes.Add("oncontextmenu", "return false;");
                    txtToDate.Attributes.Add("oncontextmenu", "return false;");
                    txtDocumentDate.Attributes.Add("oncontextmenu", "return false;");
                    txtShippingBillDate.Attributes.Add("oncontextmenu", "return false;");
                    txtINRAmount.Attributes.Add("oncontextmenu", "return false;");
                    btnSave.Attributes.Add("onclick", "return validateSave();");
                    //txtINRAmount.Attributes.Add("onfocus", "blur();");
                    rbtnExport.Attributes.Add("onclick", "toggleSelection();");
                    rbtnImport.Attributes.Add("onclick", "toggleSelection();");
                    rbtnInward.Attributes.Add("onclick", "toggleSelection();");
                    rbtnOutward.Attributes.Add("onclick", "toggleSelection();");
                    rbtnOthers.Attributes.Add("onclick", "toggleSelection();");
                    txtExchangeRate.Attributes.Add("onblur", "calculateINR();");
                    txtINRAmount.Attributes.Add("onchange", "return CalculateExchangeRateFromINR();");
                    txtFCAmount.Attributes.Add("onchange", "return calculateINRForFC();");
                    txtBillNumber.Attributes.Add("onchange", "return formatBillNumber();");
                    txtFormNumber.Attributes.Add("onchange", "return formatFormNumber();");
                    // txtINRAmount.Attributes.Add("onblur", "AddCoomaToTextboxes();");
                    txtFCAmount.Attributes.Add("onchange", "return AddCoomaToTextboxes();");
                    // txtRealisedAmount.Attributes.Add("onblur", "realisedAmt();");
                    txtINRAmount.Attributes.Add("onkeydown", "return validate_Number(event);");
                    txtFCAmount.Attributes.Add("onkeydown", "return validate_Number(event);");
                    txtRealisedAmount.Attributes.Add("onkeydown", "return validate_Number(event);");
                    txtExchangeRate.Attributes.Add("onkeydown", "return validate_Number(event);");
                    txtIECode.Attributes.Add("onkeydown", "return validate_date(event);");
                    txtCountryBeneficiary1.Attributes.Add("onchange", "setACFocus();");
                    txtCurrency.Attributes.Add("onblur", "exchRate();");
                    txtCountryACHolder1.Attributes.Add("onchange", "setShipDateFocus();");
                    txtBankCode.Attributes.Add("onchange", "setShipDateFocus();");
                    txtShippingBillDate.Attributes.Add("onblur", "setSaveFocus();");
                    ddlSchcode.Attributes.Add("onblur", "schCode();");
                    txtDocumentDate.Attributes.Add("onchange", "return validate_dateRange();");
                    checkOthers();
                    //help function calls on events
                    btnCountryBeneficiary.Attributes.Add("onclick", "return OpenCountryList('1');");
                    btnImpExpCountry.Attributes.Add("onclick", "return OpenCountryList('2');");
                    btnCountryACHolder.Attributes.Add("onclick", "return OpenCountryListAc('1');");
                    btnPortCodeList.Attributes.Add("onclick", "return PortHelp();");
                    //txtCountryBeneficiary1.Attributes.Add("onchange", "return changeCountryDesc();");
                    btncurrList.Attributes.Add("onclick", "return OpenCurrencyList('1');");
                    // txtCurrency.Attributes.Add("onchange", "return changeCurrDesc();");
                    //  txtPort.Attributes.Add("onchange", "return changePortCodeDesc();");
                    BtnBankCode.Attributes.Add("onclick", "return OpenBankCodeList('1');");
                    // txtBankCode.Attributes.Add("onchange", "return changeBankCodeDesc()");
                    // end of help function
                    // NostroVostro();
                }
                btnPCList.Attributes.Add("onclick", "return OpenPCList();");
            }
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "toogleDisplayHelp", "toogleDisplayHelp();", true);
        }
    }
    //help functions
    //protected void fillBankCode()
    //{
    //    TF_DATA objData = new TF_DATA();
    //    SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
    //    p1.Value = "";
    //    SqlParameter p2 = new SqlParameter("@countryofac", SqlDbType.VarChar);
    //    p2.Value = txtCountryACHolder1.Text.ToString();
    //    string _query = "TF_RET_HelpGetBankCodeList";
    //    DataTable dt = objData.getData(_query, p1, p2);
    //    txtBankCode.Text.Clear();
    //    ListItem li = new ListItem();
    //    li.Value = "0";
    //    if (dt.Rows.Count > 0)
    //    {
    //        li.Text = "";
    //        ddlBankCode.DataSource = dt.DefaultView;
    //        ddlBankCode.DataTextField = "BANKCODE";
    //        ddlBankCode.DataValueField = "BANKNAME";
    //        ddlBankCode.DataBind();
    //    }
    //    else
    //    {
    //        li.Text = "No record(s)";
    //    }
    //    ddlBankCode.Items.Insert(0, li);
    //}
    protected void btnBankCode_Click(object sender, EventArgs e)
    {
        if (hdnBankCode.Value != "")
        {
            txtBankCode.Text = hdnBankCode.Value;
            lblBankCode.Text = hdnBankName.Value;
            txtBankCode.Focus();
        }
    }
    //protected void fillCurrency()
    //{
    //    TF_DATA objData = new TF_DATA();
    //    SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
    //    p1.Value = "";
    //    string _query = "TF_GetCurrencyList";
    //    DataTable dt = objData.getData(_query, p1);
    //    dropDownListCurrency.Items.Clear();
    //    ListItem li = new ListItem();
    //    li.Value = "0";
    //    if (dt.Rows.Count > 0)
    //    {
    //        li.Text = "Select";
    //        dropDownListCurrency.DataSource = dt.DefaultView;
    //        dropDownListCurrency.DataTextField = "C_Code";
    //        dropDownListCurrency.DataValueField = "C_DESCRIPTION";
    //        dropDownListCurrency.DataBind();
    //    }
    //    else
    //        li.Text = "No record(s) found";
    //    dropDownListCurrency.Items.Insert(0, li);
    //}
    protected void btnCurr_Click(object sender, EventArgs e)
    {
        if (hdnCurId.Value != "")
        {
            txtCurrency.Text = hdnCurId.Value;
            txtCurrencyDescription.Text = hdnCurName.Value;
            txtCurrency.Focus();
        }
    }
    //protected void fillPortCode()
    //{
    //    TF_DATA objData = new TF_DATA();
    //    SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
    //    p1.Value = "";
    //    string _query = "TF_GetPortCodeMasterList";
    //    DataTable dt = objData.getData(_query, p1);
    //    dropDownListPortCode.Items.Clear();
    //    ListItem li = new ListItem();
    //    li.Value = "0";
    //    if (dt.Rows.Count > 0)
    //    {
    //        li.Text = "---Select---";
    //        dropDownListPortCode.DataSource = dt.DefaultView;
    //        dropDownListPortCode.DataTextField = "port_Code";
    //        dropDownListPortCode.DataValueField = "portName";
    //        dropDownListPortCode.DataBind();
    //    }
    //    else
    //        li.Text = "No record(s) found";
    //    dropDownListPortCode.Items.Insert(0, li);
    //}
    protected void btnPortCode_Click(object sender, EventArgs e)
    {
        if (hdnPortCode.Value != "")
        {
            txtPort.Text = hdnPortCode.Value;
            txtPortCode.Text = hdnPortDesc.Value;
            txtPort.Focus();
        }
    }
    //protected void fillCountry()
    //{
    //    TF_DATA objData = new TF_DATA();
    //    SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
    //    p1.Value = "";
    //    string _query = "TF_GetCountryList";
    //    DataTable dt = objData.getData(_query, p1);
    //    dropDownListCountryBeneficiary.Items.Clear();
    //    ListItem li = new ListItem();
    //    li.Value = "0";
    //    if (dt.Rows.Count > 0)
    //    {
    //        li.Text = "";
    //        dropDownListCountryBeneficiary.DataSource = dt.DefaultView;
    //        dropDownListCountryBeneficiary.DataTextField = "CountryID";
    //        dropDownListCountryBeneficiary.DataValueField = "CountryName";
    //        dropDownListCountryBeneficiary.DataBind();
    //    }
    //    else
    //    {
    //        li.Text = "No record(s) found";
    //    }
    //    dropDownListCountryBeneficiary.Items.Insert(0, li);
    //
    //protected void fillCountryAC()
    //{
    //    TF_DATA objData = new TF_DATA();
    //    SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
    //    p1.Value = "";
    //    string _query = "TF_RET_HelpGetCountryACList";
    //    DataTable dt = objData.getData(_query, p1);
    //    txtCountryACHolder1.Text.Clear();
    //    ListItem li = new ListItem();
    //    li.Value = "0";
    //    if (dt.Rows.Count > 0)
    //    {
    //        li.Text = "";
    //        dropDownListCountryACHolder.DataSource = dt.DefaultView;
    //        dropDownListCountryACHolder.DataTextField = "CountryID";
    //        dropDownListCountryACHolder.DataValueField = "CountryName";
    //        dropDownListCountryACHolder.DataBind();
    //    }
    //    else
    //    {
    //        li.Text = "No record(s) found";
    //    }
    protected void btnCountry_Click(object sender, EventArgs e)
    {
        if (hdnCountry.Value != "")
        {
            txtCountryBeneficiary1.Text = hdnCountry.Value;
            txtCountryBeneficiary.Text = hdnCountryName.Value;
            txtCountryBeneficiary1.Focus();
        }
    }
    protected void btnCountryAc_Click(object sender, EventArgs e)
    {
        if (hdnCountryAc.Value != "")
        {
            txtCountryACHolder1.Text = hdnCountryAc.Value;
            txtCountryACHolder.Text = hdnCountryAcName.Value;
            txtCountryACHolder1.Focus();
        }
        //  fillBankCode();
    }
    //protected void fillPurposeCode()
    //{
    //    txtPurposeCodeDesc.Text = "";
    //    string _modType = "";
    //    if (rbtnExport.Checked)
    //        _modType = "EXP";
    //    if (rbtnImport.Checked)
    //        _modType = "IMP";
    //    if (rbtnInward.Checked)
    //        _modType = "INW";
    //    if (rbtnOutward.Checked)
    //        _modType = "OTW";
    //    if (rbtnOthers.Checked)
    //        _modType = "OTH";
    //    SqlParameter p1 = new SqlParameter("@type", SqlDbType.VarChar);
    //    p1.Value = _modType;
    //    string _query = "TF_RET_GetPurposeCodelist";
    //    TF_DATA objData = new TF_DATA();
    //    DataTable dt = objData.getData(_query, p1);
    //    dropDownListPurposeCode.Items.Clear();
    //    ListItem li = new ListItem();
    //    li.Value = "0";
    //    if (dt.Rows.Count > 0)
    //    {
    //        li.Text = "---Select---";
    //        dropDownListPurposeCode.DataSource = dt.DefaultView;
    //        dropDownListPurposeCode.DataTextField = "purposeID";
    //        dropDownListPurposeCode.DataValueField = "purposeID";
    //        dropDownListPurposeCode.DataBind();
    //    }
    //    else
    //        li.Text = "No record(s) found";
    //    dropDownListPurposeCode.Items.Insert(0, li);
    //}
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("RET_ViewReturnData.aspx", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _RemiCountry = txtImpExpCountry.Text.ToString();
        //string confirmValue = Request.Form["confirm_value"];
       

            //if (txtImpExpCountry.Text.Trim() != "")
            //{
            //    _RemiCountry = txtImpExpCountry.Text.Trim();
            //}
            //else
            //{

            //    _RemiCountry = txtCountryBeneficiary1.Text;
                
            //}
            string _userName = Session["userName"].ToString().Trim();
            string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            string _mode = Request.QueryString["mode"].Trim();
            string _adCode = lblAdcode.Text.ToString().Trim();
            string _fromDate = txtFromDate.Text.ToString();
            string _toDate = txtToDate.Text.ToString();
            string _serialNumber = txtSerialNumber.Text.Trim();
            string _documentNumber = txtDocumentNumber.Text.Trim();
            string _documentDate = txtDocumentDate.Text.ToString();
            string _modType = "";
            if (rbtnExport.Checked)
                _modType = "EXP";
            if (rbtnImport.Checked)
                _modType = "IMP";
            if (rbtnInward.Checked)
                _modType = "INW";
            if (rbtnOutward.Checked)
                _modType = "OTW";
            if (rbtnOthers.Checked)
                _modType = "OTH";
            string _purposeCode = txtPurposecode.Text.Trim();
            string _currency = txtCurrency.Text.ToString();
            string _countryACHolder = txtCountryACHolder1.Text.ToString();
            //fillBankCode();
            string _BankCode;
            if (txtBankCode.Text.ToString().Trim() == "No record(s)")
                _BankCode = "";
            else
                _BankCode = txtBankCode.Text.ToString();
            string _fcAmount = txtFCAmount.Text.Trim();
            string _inrAmount = txtINRAmount.Text.Trim();
            string _realisedAmount = txtRealisedAmount.Text.Trim();
            string _nostroVostro = txtNostroVostro.Text.Trim();
            string _beneficiaryName = txtBeneficiaryName.Text.Trim();
            string _remitterName = txtRemitterName.Text.Trim();
            string _beneficiaryAdd = txtBeneficiaryAdd.Text.Trim();
            string _remitterAdd = txtRemitterAdd.Text.Trim();
            string _countryBeneficiary = txtCountryBeneficiary1.Text.ToString();
            string _portCode = txtPort.Text.ToString();
            string _ieCode = txtIECode.Text.Trim();
            string _shippingBillNumber = txtShippingBillNumber.Text.Trim();
            string _shippingBillDate = "";
            if (txtShippingBillDate.Text != "")
            {
                _shippingBillDate = txtShippingBillDate.Text.ToString();
            }
            string _customSerialNumber;
            if (rbtnExport.Checked)
                _customSerialNumber = txtCustomSerialNumber.Text.ToString(); //_customSerialNumber = System.DateTime.Now.ToString("yy").ToString() + _serialNumber;
            else
                _customSerialNumber = "";
            string _lcIndication = chkLCIndication.Checked == true ? "1" : "0";
            string _scheduleNumber = txtScheduleNumber.Text.Trim();
            string _formNumber = txtFormNumber.Text.Trim();
            string _exchangeRate = txtExchangeRate.Text.Trim();
            string _billNumber = txtBillNumber.Text.Trim();
            string _excelUpload = "";
            if (rdbscotiamoctia.Checked == true)
            {
                _excelUpload = "C";
            }
            else if (rdbtradefinance.Checked == true)
            {
                _excelUpload = "T";
            }
            else if (rdbtrearusury.Checked == true)
            {
                _excelUpload = "Y";
            }
            else if (rdbcustomerservice.Checked == true)
            {
                _excelUpload = "S";
            }
            else if (rdbadmin.Checked == true)
            {
                _excelUpload = "A";
            }
            //
            SqlParameter mode = new SqlParameter("@mode", SqlDbType.VarChar);
            mode.Value = _mode;
            SqlParameter adcode = new SqlParameter("@adcode", SqlDbType.VarChar);
            adcode.Value = _adCode;
            SqlParameter serialnumber = new SqlParameter("@serialnumber", SqlDbType.VarChar);
            serialnumber.Value = _serialNumber;
            SqlParameter fromdate = new SqlParameter("@fromdate", SqlDbType.VarChar);
            fromdate.Value = _fromDate;
            SqlParameter todate = new SqlParameter("@todate", SqlDbType.VarChar);
            todate.Value = _toDate;
            SqlParameter transactiondate = new SqlParameter("@transactiondate", SqlDbType.VarChar);
            transactiondate.Value = _documentDate;
            SqlParameter documentnumber = new SqlParameter("@documentnumber", SqlDbType.VarChar);
            documentnumber.Value = _documentNumber;
            SqlParameter iecode = new SqlParameter("@iecode", SqlDbType.VarChar);
            iecode.Value = _ieCode;
            SqlParameter formserialnumber = new SqlParameter("@formserialnumber", SqlDbType.VarChar);
            formserialnumber.Value = _formNumber;
            SqlParameter purposeid = new SqlParameter("@purposeid", SqlDbType.VarChar);
            purposeid.Value = _purposeCode;
            SqlParameter portcode = new SqlParameter("@portcode", SqlDbType.VarChar);
            portcode.Value = _portCode;
            //--------------------------------------
            SqlParameter shippingbillnumber = new SqlParameter("@shippingbillnumber", SqlDbType.VarChar);
            shippingbillnumber.Value = _shippingBillNumber;
            SqlParameter shippingbilldate = new SqlParameter("@shippingbilldate", SqlDbType.VarChar);
            shippingbilldate.Value = _shippingBillDate;
            SqlParameter currencyid = new SqlParameter("@currencyid", SqlDbType.VarChar);
            currencyid.Value = _currency;
            SqlParameter amount = new SqlParameter("@amount", SqlDbType.VarChar); amount.Value = _fcAmount;
            SqlParameter amountinr = new SqlParameter("@amountinr", SqlDbType.VarChar);
            amountinr.Value = _inrAmount;
            SqlParameter accountrycode = new SqlParameter("@accountrycode", SqlDbType.VarChar);
            accountrycode.Value = _countryACHolder;
            SqlParameter bncountrycode = new SqlParameter("@bncountrycode", SqlDbType.VarChar);
            bncountrycode.Value = _countryBeneficiary;
            SqlParameter customserialnumber = new SqlParameter("@customserialnumber", SqlDbType.VarChar);
            customserialnumber.Value = _customSerialNumber;
            SqlParameter lcindication = new SqlParameter("@lcindication", SqlDbType.VarChar);
            lcindication.Value = _lcIndication;
            SqlParameter beneficiaryname = new SqlParameter("@beneficiaryname", SqlDbType.VarChar);
            beneficiaryname.Value = _beneficiaryName;
            SqlParameter remittername = new SqlParameter("@remittername", SqlDbType.VarChar);
            remittername.Value = _remitterName;
            SqlParameter remitterCountry = new SqlParameter("@RemiCountry", SqlDbType.VarChar);
            remitterCountry.Value = _RemiCountry;
            SqlParameter beneficiaryAdd = new SqlParameter("@beneficiaryAdd", SqlDbType.VarChar);
            beneficiaryAdd.Value = _beneficiaryAdd;
            SqlParameter remitterAdd = new SqlParameter("@remitterAdd", SqlDbType.VarChar);
            remitterAdd.Value = _remitterAdd;
            SqlParameter vastroac = new SqlParameter("@vastroac ", SqlDbType.VarChar);
            vastroac.Value = _nostroVostro;
            SqlParameter modtype = new SqlParameter("@modtype", SqlDbType.VarChar);
            modtype.Value = _modType;
            SqlParameter schedulenumber = new SqlParameter("@schedulenumber", SqlDbType.VarChar);
            schedulenumber.Value = _scheduleNumber;
            SqlParameter realisedamount = new SqlParameter("@realisedamount", SqlDbType.VarChar);
            realisedamount.Value = _realisedAmount;
            SqlParameter exchangerate = new SqlParameter("@exchangerate", SqlDbType.VarChar);
            exchangerate.Value = _exchangeRate;
            SqlParameter billnumber = new SqlParameter("@billnumber", SqlDbType.VarChar);
            billnumber.Value = _billNumber;
            SqlParameter excelUpload = new SqlParameter("@excelUpload", SqlDbType.VarChar);
            excelUpload.Value = _excelUpload;
            SqlParameter adduser = new SqlParameter("@adduser", SqlDbType.VarChar);
            adduser.Value = _userName;
            SqlParameter adddate = new SqlParameter("@adddate", SqlDbType.VarChar);
            adddate.Value = _uploadingDate;
            SqlParameter vostrobankcode = new SqlParameter("@vostrobankcode", SqlDbType.VarChar);
            vostrobankcode.Value = _BankCode;
            string _query = "TF_RET_UpdateRReturnData";
            TF_DATA objSave = new TF_DATA();
            _result = objSave.SaveDeleteData(_query, mode, adcode, serialnumber, fromdate, todate, transactiondate, documentnumber, iecode,
            formserialnumber, purposeid, portcode, shippingbillnumber, shippingbilldate, currencyid,
            amount, amountinr, accountrycode, bncountrycode, vostrobankcode, customserialnumber, lcindication,
            beneficiaryname, remittername, beneficiaryAdd, remitterAdd, vastroac, modtype, schedulenumber, realisedamount, adduser, adddate,
            exchangerate, billnumber, excelUpload, remitterCountry);
            string _script = "";
            string _OldValues = "";
            string _NewValues = "";
            if (_result.Substring(0, 5) == "added")
            {
                _query = "TF_RET_AuditTrail";
                _NewValues = "Sr No: " + txtSerialNumber.Text.Trim() + ";Document No: " + txtDocumentNumber.Text.Trim() + ";Document Date:" + txtDocumentDate.Text.Trim() + ";Purpose Code:" + txtPurposecode.Text.Trim() + ";Currency:" + txtCurrency.Text.ToString().Trim() + ";Amount:" + txtFCAmount.Text.Trim();
                SqlParameter Branch = new SqlParameter("@BranchCode", SqlDbType.VarChar);
                Branch.Value = txtBranchName.Text.Trim();
                SqlParameter Mod = new SqlParameter("@ModType", SqlDbType.VarChar);
                Mod.Value = "RET";
                SqlParameter oldvalues = new SqlParameter("@OldValues", SqlDbType.VarChar);
                oldvalues.Value = _OldValues;
                SqlParameter newvalues = new SqlParameter("@NewValues", SqlDbType.VarChar);
                newvalues.Value = _NewValues;
                SqlParameter Acno = new SqlParameter("@CustAcNo", SqlDbType.VarChar);
                Acno.Value = "";
                SqlParameter DocumentNo = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
                DocumentNo.Value = txtDocumentNumber.Text.Trim();
                SqlParameter FWDContractNo = new SqlParameter("@FWD_Contract_No", SqlDbType.VarChar);
                FWDContractNo.Value = "";
                SqlParameter DocumnetDate = new SqlParameter("@DocumentDate", SqlDbType.VarChar);
                DocumnetDate.Value = txtDocumentDate.Text.Trim();
                SqlParameter Mode = new SqlParameter("@Mode", "A");
                SqlParameter user = new SqlParameter("@ModifiedBy", SqlDbType.VarChar);
                user.Value = _userName;
                string _moddate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                SqlParameter moddate = new SqlParameter("@ModifiedDate", SqlDbType.VarChar);
                moddate.Value = _moddate;
                //string _type = "A";
                //SqlParameter type = new SqlParameter("@type", SqlDbType.VarChar);
                //type.Value = _type;
                string _menu = "R Return Data Entry";
                SqlParameter menu = new SqlParameter("@MenuName", SqlDbType.VarChar);
                menu.Value = _menu;
                string at = objSave.SaveDeleteData(_query, Branch, Mod, oldvalues, newvalues, Acno, DocumentNo, FWDContractNo, DocumnetDate, Mode, user, moddate, menu);
                _script = "window.location='RET_ViewReturnData.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
            {
                if (_result == "updated")
                {
                    int isneedtolog = 0;
                    _OldValues = "Sr No: " + hdnSerialno.Value.ToString() + ";Document No: " + hdnDocumentNo.Value.ToString() + ";Document Date:" + hdnDocumentDate.Value.ToString() + ";Purpose Code:" + hdnPurposeCode.Value.ToString() + ";Currency:" + hdnCur.Value.ToString() + ";Amount:" + hdnFCAmt.Value.ToString();
                    if (hdnSerialno.Value != txtSerialNumber.Text)
                    {
                        isneedtolog = 1;
                        _NewValues = _NewValues + "Sr no : " + txtSerialNumber.Text.Trim();
                    }
                    if (hdnDocumentNo.Value != txtDocumentNumber.Text)
                    {
                        isneedtolog = 1;
                        if (_NewValues == "")
                        {
                            _NewValues = _NewValues + "Document No : " + txtDocumentNumber.Text.Trim();
                        }
                        else
                        {
                            _NewValues = _NewValues + "; Document No : " + txtDocumentNumber.Text.Trim();
                        }
                    }
                    if (hdnDocumentDate.Value != txtDocumentDate.Text)
                    {
                        isneedtolog = 1;
                        if (_NewValues == "")
                        {
                            _NewValues = _NewValues + "Document Date : " + txtDocumentDate.Text.Trim();
                        }
                        else
                        {
                            _NewValues = _NewValues + "; Document Date : " + txtDocumentDate.Text.Trim();
                        }
                    }
                    if (hdnPurposeCode.Value != txtPurposecode.Text)
                    {
                        isneedtolog = 1;
                        if (_NewValues == "")
                        {
                            _NewValues = _NewValues + "Purpose Code : " + txtPurposecode.Text.Trim();
                        }
                        else
                        {
                            _NewValues = _NewValues + ";Purpose Code : " + txtPurposecode.Text.Trim();
                        }
                    }
                    if (hdnCur.Value != txtCurrency.Text.ToString())
                    {
                        isneedtolog = 1;
                        if (_NewValues == "")
                        {
                            _NewValues = _NewValues + "Currency: " + txtCurrency.Text.Trim();
                        }
                        else
                        {
                            _NewValues = _NewValues + ";Currency: " + txtCurrency.Text.Trim();
                        }
                    }
                    if (hdnFCAmt.Value != txtFCAmount.Text.Trim())
                    {
                        isneedtolog = 1;
                        if (_NewValues == "")
                        {
                            _NewValues = _NewValues + "Amount FC: " + txtFCAmount.Text.Trim();
                        }
                        else
                        {
                            _NewValues = _NewValues + ";Amount FC: " + txtFCAmount.Text.Trim();
                        }
                    }
                    _query = "TF_RET_AuditTrail";
                    SqlParameter Branch = new SqlParameter("@BranchCode", SqlDbType.VarChar);
                    Branch.Value = txtBranchName.Text.Trim();
                    SqlParameter Mod = new SqlParameter("@ModType", SqlDbType.VarChar);
                    Mod.Value = "RET";
                    SqlParameter oldvalues = new SqlParameter("@OldValues", SqlDbType.VarChar);
                    oldvalues.Value = _OldValues;
                    SqlParameter newvalues = new SqlParameter("@NewValues", SqlDbType.VarChar);
                    newvalues.Value = _NewValues;
                    SqlParameter Acno = new SqlParameter("@CustAcNo", SqlDbType.VarChar);
                    Acno.Value = "";
                    SqlParameter DocumentNo = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
                    DocumentNo.Value = txtDocumentNumber.Text.Trim();
                    SqlParameter FWDContractNo = new SqlParameter("@FWD_Contract_No", SqlDbType.VarChar);
                    FWDContractNo.Value = "";
                    SqlParameter DocumnetDate = new SqlParameter("@DocumentDate", SqlDbType.VarChar);
                    DocumnetDate.Value = txtDocumentDate.Text.Trim();
                    SqlParameter Mode = new SqlParameter("@Mode", "M");
                    SqlParameter user = new SqlParameter("@ModifiedBy", SqlDbType.VarChar);
                    user.Value = _userName;
                    string _moddate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    SqlParameter moddate = new SqlParameter("@ModifiedDate", SqlDbType.VarChar);
                    moddate.Value = _moddate;
                    //string _type = "A";
                    //SqlParameter type = new SqlParameter("@type", SqlDbType.VarChar);
                    //type.Value = _type;
                    string _menu = "R Return Data Entry";
                    SqlParameter menu = new SqlParameter("@MenuName", SqlDbType.VarChar);
                    menu.Value = _menu;
                    if (isneedtolog == 1)
                    {
                        string at = objSave.SaveDeleteData(_query, Branch, Mod, oldvalues, newvalues, Acno, DocumentNo, FWDContractNo, DocumnetDate, Mode, user, moddate, menu);
                    }
                    _script = "window.location='RET_ViewReturnData.aspx?result=" + _result + "'";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                }
                else
                    labelMessage.Text = _result;
            }
        }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("RET_ViewReturnData.aspx", true);
    }
    protected void clearControls()
    {
        txtFromDate.Text = "";
        txtToDate.Text = "";
        txtSerialNumber.Text = "";
        txtDocumentNumber.Text = "";
        txtDocumentDate.Text = "";
        rbtnExport.Checked = false;
        rbtnImport.Checked = false;
        rbtnInward.Checked = false;
        rbtnOutward.Checked = false;
        rbtnOthers.Checked = true;
        txtPurposecode.Text = "";
        txtCurrency.Text = "";
        txtFCAmount.Text = "";
        txtExchangeRate.Text = "";
        txtINRAmount.Text = "";
        txtRealisedAmount.Text = "";
        //txtNostroVostro.Text = "N";
        txtBeneficiaryName.Text = "";
        txtRemitterName.Text = "";
        txtBeneficiaryAdd.Text = "";
        txtRemitterAdd.Text = "";
        txtCountryACHolder1.Text = "";
        txtCountryBeneficiary1.Text = "";
        txtPort.Text = "";
        txtBankCode.Text = "";
        txtIECode.Text = "";
        txtBillNumber.Text = "";
        txtShippingBillDate.Text = "";
        txtCustomSerialNumber.Text = "";
        chkLCIndication.Checked = false;
        txtScheduleNumber.Text = "";
        ddlSchcode.SelectedIndex = 0;
        txtFormNumber.Text = "";
        hdnType.Value = "";
        hdnPurposeCode.Value = "";
        hdnCurrency.Value = "";
        hdnFCAmt.Value = "";
        hdnINRAmt.Value = "";
        hdnExtRate.Value = "";
        hdnRealisedAmt.Value = "";
        hdnDstCode.Value = "";
    }
    protected void fillDetails()
    {
        SqlParameter p1 = new SqlParameter("@adcode", SqlDbType.VarChar);
        p1.Value = lblAdcode.Text.ToString();
        SqlParameter p2 = new SqlParameter("@fromdate", SqlDbType.VarChar);
        p2.Value = txtFromDate.Text.ToString();
        SqlParameter p3 = new SqlParameter("@todate", SqlDbType.VarChar);
        p3.Value = txtToDate.Text.ToString();
        SqlParameter p4 = new SqlParameter("@srno", SqlDbType.VarChar);
        p4.Value = txtSerialNumber.Text.ToString();
        hdnSerialno.Value = txtSerialNumber.Text.ToString();
        string _query = "TF_RET_GetRReturnEntryDetails";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2, p3, p4);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["RemiCountry"].ToString().Trim() != "")
            {
                txtImpExpCountry.Text = dt.Rows[0]["RemiCountry"].ToString().Trim();
                fillCountryIEdesc();
            }
            else
            {
                txtImpExpCountry.Text = "";
            }
            txtDocumentNumber.Text = dt.Rows[0]["DOCNO"].ToString().Trim();
            hdnDocumentNo.Value = dt.Rows[0]["DOCNO"].ToString().Trim();
            txtDocumentDate.Text = dt.Rows[0]["TRANSACTION_DT"].ToString().Trim();
            hdnDocumentDate.Value = dt.Rows[0]["TRANSACTION_DT"].ToString().Trim();
            string _modType = dt.Rows[0]["MOD_TYPE"].ToString().Trim();
            hdnType.Value = _modType;
            switch (_modType)
            {
                case "EXP":
                    rbtnExport.Checked = true;
                    rbtnOthers.Checked = false;
                    break;
                case "IMP":
                    rbtnImport.Checked = true;
                    rbtnOthers.Checked = false;
                    break;
                case "INW":
                    rbtnInward.Checked = true;
                    rbtnOthers.Checked = false;
                    break;
                case "OTW":
                    rbtnOutward.Checked = true;
                    rbtnOthers.Checked = false;
                    break;
                case "OTH":
                    rbtnOthers.Checked = true;
                    break;
            }
            // fillPurposeCode();
            checkOthers();
            if (dt.Rows[0]["PURPOSEID"].ToString().Trim() != "")
            {
                //dropDownListPurposeCode.SelectedIndex = dropDownListPurposeCode.Items.IndexOf(dropDownListPurposeCode.Items.FindByText(dt.Rows[0]["PURPOSEID"].ToString().Trim()));
                txtPurposecode.Text = dt.Rows[0]["PURPOSEID"].ToString().Trim();
                hdnPurposeCode.Value = dt.Rows[0]["PURPOSEID"].ToString().Trim();
                fillPCDescription();
            }
            else
            {
                txtPurposecode.Text = "";
            }
            hdnPurposeCode.Value = dt.Rows[0]["PURPOSEID"].ToString();
            if (dt.Rows[0]["CURR"].ToString().Trim() != "")
            {
                //    dropDownListCurrency.SelectedIndex = dropDownListCurrency.Items.IndexOf(dropDownListCurrency.Items.FindByText(dt.Rows[0]["CURR"].ToString().Trim()));
                txtCurrency.Text = dt.Rows[0]["CURR"].ToString().Trim();
                fillCurrencyDes();
            }
            else
            {
                txtCurrency.Text = "";
            }
            hdnCur.Value = dt.Rows[0]["CURR"].ToString();
            txtFCAmount.Text = dt.Rows[0]["AMOUNT"].ToString().Trim();
            hdnFCAmt.Value = dt.Rows[0]["AMOUNT"].ToString().Trim();
            if (dt.Rows[0]["INR_AMOUNT"].ToString() != "")
            {
                txtINRAmount.Text = (decimal.Parse(dt.Rows[0]["INR_AMOUNT"].ToString().Trim()).ToString("0.00")).ToString();
                hdnINRAmt.Value = (decimal.Parse(dt.Rows[0]["INR_AMOUNT"].ToString().Trim()).ToString("0.00")).ToString();
            }
            else
            {
                txtINRAmount.Text = dt.Rows[0]["INR_AMOUNT"].ToString().Trim();
                hdnINRAmt.Value = dt.Rows[0]["INR_AMOUNT"].ToString().Trim();
            }
            if (dt.Rows[0]["REALISED_AMT"].ToString() == "0")
            {
                txtRealisedAmount.Text = "";
            }
            else
            {
                txtRealisedAmount.Text = dt.Rows[0]["REALISED_AMT"].ToString().Trim();
            }
            hdnRealisedAmt.Value = txtRealisedAmount.Text;
            txtNostroVostro.Text = dt.Rows[0]["VASTRO_AC"].ToString().Trim();
            txtBeneficiaryName.Text = dt.Rows[0]["BENEFICIARYNAME"].ToString().Trim();
            txtRemitterName.Text = dt.Rows[0]["REMMITERNAME"].ToString().Trim();
            txtBeneficiaryAdd.Text = dt.Rows[0]["BENEFICIARY_ADDRESS"].ToString().Trim();
            txtRemitterAdd.Text = dt.Rows[0]["REMITTER_ADDRESS"].ToString().Trim();
            if (dt.Rows[0]["AC_COUNTRY_CODE"].ToString().Trim() != "")
            {
                txtCountryACHolder1.Text = dt.Rows[0]["AC_COUNTRY_CODE"].ToString().Trim();
                fillCountryACHolderdesc();
            }
            else
            {
                txtCountryACHolder1.Text = "";
            }

            if (dt.Rows[0]["BN_COUNTRY_CODE"].ToString().Trim() != "")
            {
                txtCountryBeneficiary1.Text = dt.Rows[0]["BN_COUNTRY_CODE"].ToString().Trim();
                fillCountryBeneficiarydesc();
            }
            else
            {
                txtCountryBeneficiary1.Text = "";
            }

            //fillBankCode();
            if (dt.Rows[0]["VOSTRO_BANK_CODE"].ToString().Trim() != "")
            {
                txtBankCode.Text = dt.Rows[0]["VOSTRO_BANK_CODE"].ToString().Trim();
                fillBankDesc();
            }
            else
            {
                txtBankCode.Text = "";
            }
            if (dt.Rows[0]["PORT_CODE"].ToString().Trim() != "")
            {
                txtPort.Text = dt.Rows[0]["PORT_CODE"].ToString().Trim();
                fillportCode();
            }
            else
            {
                txtPort.Text = "";
            }
            string Department = dt.Rows[0]["ExcelFlag"].ToString().Trim();
            if (Department == "T")
            {
                rdbtradefinance.Checked = true;
            }
            else if (Department == "C")
            {
                rdbscotiamoctia.Checked = true;
                rdbtradefinance.Checked = false;
            }
            else if (Department == "Y")
            {
                rdbtrearusury.Checked = true;
                rdbtradefinance.Checked = false;
            }
            else if (Department == "S")
            {
                rdbcustomerservice.Checked = true;
                rdbtradefinance.Checked = false;
            }
            else if (Department == "A")
            {
                rdbadmin.Checked = true;
                rdbtradefinance.Checked = false;
            }
            txtIECode.Text = dt.Rows[0]["IECODE"].ToString().Trim();
            txtBillNumber.Text = dt.Rows[0]["BILLNO"].ToString().Trim();
            if (dt.Rows[0]["SHIPPING_BILL_DT"] != null && dt.Rows[0]["SHIPPING_BILL_DT"].ToString() != "")
            {
                txtShippingBillDate.Text = dt.Rows[0]["SHIPPING_BILL_DT"].ToString().Trim();
            }
            else
                txtShippingBillDate.Text = "";
            txtCustomSerialNumber.Text = dt.Rows[0]["CUSTOMSRNO"].ToString().Trim();
            string _lcIndication = dt.Rows[0]["LCINDICATION"].ToString().Trim();
            if (_lcIndication == "1")
            {
                chkLCIndication.Checked = true;
                chkLCIndication.Text = "Yes";
            }
            else
            {
                chkLCIndication.Checked = false;
                chkLCIndication.Text = "NO";
            }
            ddlSchcode.SelectedValue = dt.Rows[0]["SCHEDULENO"].ToString().Trim();
            txtScheduleNumber.Text = dt.Rows[0]["SCHEDULENO"].ToString().Trim();
            txtFormNumber.Text = dt.Rows[0]["FORMSRNO"].ToString().Trim();
            float exChangeRate = 0;
            if (dt.Rows[0]["INR_AMOUNT"].ToString() != "" && dt.Rows[0]["AMOUNT"].ToString() != "")
            {
                float inrAmount = float.Parse(dt.Rows[0]["INR_AMOUNT"].ToString().Trim());
                float FCAmount = float.Parse(dt.Rows[0]["AMOUNT"].ToString().Trim());
                if (FCAmount > 0)
                {
                    exChangeRate = inrAmount / FCAmount;

                }
            }
            else
            {
                exChangeRate = 0;
            }
            txtExchangeRate.Text = exChangeRate.ToString();
            hdnExtRate.Value = exChangeRate.ToString();
            txtShippingBillNumber.Text = dt.Rows[0]["SHIPPING_BILL_NO"].ToString().Trim();
            txtDocumentNumber.Focus();
        }
    }
    public void AddLeadingZero(TextBox currentField)
    {
        //Check if the value length hasn't reach its max length yet
        if (currentField.Text.Length != currentField.MaxLength)
        {
            //Add leading zero(s) in front of the value
            int numToAdd = currentField.MaxLength - currentField.Text.Length;
            string value = "";
            for (int i = 0; i < numToAdd; i++)
            {
                value += "0";
            }
            currentField.Text = value + currentField.Text;
        }
    }
    protected void checkOthers()
    {
        if (rbtnExport.Checked == true || rbtnImport.Checked == true)
        {
            txtShippingBillNumber.Enabled = true;
            txtShippingBillDate.Enabled = true;
            calendarShippingBillDate.Enabled = true;
            btncalendar_ShippingBillDate.Enabled = true;
            txtCustomSerialNumber.Enabled = true;
            chkLCIndication.Enabled = true;
            ddlSchcode.Enabled = true;
            txtScheduleNumber.Enabled = true;
            txtFormNumber.Enabled = true;
            txtPort.Enabled = true;
            txtBillNumber.Enabled = true;
            txtIECode.Enabled = true;
            btnPortCodeList.Enabled = true;
        }
        else
        {
            txtShippingBillNumber.Enabled = false;
            txtShippingBillDate.Enabled = false;
            calendarShippingBillDate.Enabled = false;
            btncalendar_ShippingBillDate.Enabled = false;
            txtCustomSerialNumber.Enabled = false;
            chkLCIndication.Enabled = false;
            ddlSchcode.Enabled = false;
            txtScheduleNumber.Enabled = false;
            txtFormNumber.Enabled = false;
            txtPort.Enabled = false;
            txtBillNumber.Enabled = false;
            txtIECode.Enabled = false;
            btnPortCodeList.Enabled = false;
        }
    }
    protected void fillProcessingDate()
    {
        // Session["FrRelDt"] = "01/02/2013";
        //Session["ToRelDt"] = "15/02/2013";
        //if (Session["FrRelDt"] != null)
        //{ txtFromDate.Text = Session["FrRelDt"].ToString(); }
        //if (Session["ToRelDt"] != null)
        //{ txtToDate.Text = Session["ToRelDt"].ToString(); }
        if ((Session["FrRelDt"] != null && Session["ToRelDt"] != null) && (Session["FrRelDt"] != "" && Session["ToRelDt"] != ""))
        {
            TF_DATA objData = new TF_DATA();
            String _query = "TF_RET_GetSerialNumbers";
            SqlParameter p1 = new SqlParameter("@adcode", SqlDbType.VarChar);
            p1.Value = lblAdcode.Text.ToString();
            SqlParameter p2 = new SqlParameter("@startdate", SqlDbType.VarChar);
            p2.Value = txtFromDate.Text.ToString();
            SqlParameter p3 = new SqlParameter("@enddate", SqlDbType.VarChar);
            p3.Value = txtToDate.Text.ToString();
            DataTable dt = objData.getData(_query, p1, p2, p3);
            if (dt.Rows.Count > 0)
            {
                int newsrno = Convert.ToInt16(dt.Rows[0]["SrNo"].ToString().Trim()) + 1;
                txtSerialNumber.Text = newsrno.ToString();
                // AddLeadingZero(txtSerialNumber);
            }
            else
            {
                txtSerialNumber.Text = "1";
            }
        }
    }
    public void fillPCDescription()
    {
        if (txtPurposecode.Text != "")
        {
            txtPurposeCodeDesc.Text = "";
            string _query = "TF_RET_GetPurposeCodeMasterDetails";
            SqlParameter p1 = new SqlParameter("@purposecode", SqlDbType.VarChar);
            p1.Value = txtPurposecode.Text.Trim();
            TF_DATA objData = new TF_DATA();
            DataTable dt = objData.getData(_query, p1);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][1].ToString().Trim().Length > 30)
                {
                    txtPurposeCodeDesc.Text = dt.Rows[0][1].ToString().Trim().Substring(0, 30) + "...";
                    txtPurposeCodeDesc.ToolTip = dt.Rows[0][1].ToString().Trim();
                }
                else
                {
                    txtPurposeCodeDesc.Text = dt.Rows[0][1].ToString().Trim();
                }
            }
            else
            {
                txtPurposecode.Text = "";
                txtPurposeCodeDesc.Text = "<font style=color:red>" + "Invalid Purpose Code" + "</font>";
                txtPurposecode.Focus();
            }
        }
        else
        {
            txtPurposeCodeDesc.Text = "";
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "key", "toggleSelection();document.getElementById('txtPurposecode').focus();", true);
    }
    //protected void dropDownListCountryACHolder_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillBankCode();
    //    dropDownListCountryACHolder.Focus();
    //}
    protected void rbtnExport_CheckedChanged(object sender, EventArgs e)
    {
        checkOthers();
        //fillPurposeCode();
        rbtnExport.Focus();
        txtCountryBeneficiary1.Text = "";
        txtPurposecode.Text = "";
        txtPurposeCodeDesc.Text = "";
    }
    protected void rbtnImport_CheckedChanged(object sender, EventArgs e)
    {
        checkOthers();
        //fillPurposeCode();
        rbtnImport.Focus();
        txtCountryBeneficiary1.Text = "";
        txtPurposecode.Text = "";
        txtPurposeCodeDesc.Text = "";
    }
    protected void rbtnInward_CheckedChanged(object sender, EventArgs e)
    {
        checkOthers();
        //fillPurposeCode();
        //dropDownListCountryBeneficiary.SelectedIndex = dropDownListCountryBeneficiary.Items.IndexOf(dropDownListCountryBeneficiary.Items.FindByText("IN"));
        if (txtCountryBeneficiary1.Text == "IN")
        {
            rbtnInward.Focus();
        }
        txtPurposecode.Text = "";
        txtPurposeCodeDesc.Text = "";
    }
    protected void rbtnOutward_CheckedChanged(object sender, EventArgs e)
    {
        checkOthers();
        //fillPurposeCode();
        rbtnOutward.Focus();
        txtCountryBeneficiary1.Text = "";
        txtPurposecode.Text = "";
        txtPurposeCodeDesc.Text = "";
    }
    protected void rbtnOthers_CheckedChanged(object sender, EventArgs e)
    {
        checkOthers();
        //fillPurposeCode();
        rbtnOthers.Focus();
        txtCountryBeneficiary1.Text = "";
        txtPurposecode.Text = "";
        txtPurposeCodeDesc.Text = "";
    }
    protected void btnPc_Click(object sender, EventArgs e)
    {
        if (hdnpcId.Value != "")
        {
            txtPurposecode.Text = hdnpcId.Value;
            fillPCDescription();
            txtPurposecode.Focus();
        }
    }
    public void fillScheduleCodes()
    {
        ddlSchcode.Items.Clear();
        ListItem lio = new ListItem();
        lio.Value = "0";
        lio.Text = "--Select--";
        ddlSchcode.Items.Add(lio);
        ListItem li1 = new ListItem();
        li1.Value = "3";
        li1.Text = "3 Full Realisation";
        ddlSchcode.Items.Add(li1);
        ListItem li2 = new ListItem();
        li2.Value = "4";
        li2.Text = "4 Part Realisation";
        ddlSchcode.Items.Add(li2);
        ListItem li3 = new ListItem();
        li3.Value = "5";
        li3.Text = "5 Full Advance";
        ddlSchcode.Items.Add(li3);
        ListItem li4 = new ListItem();
        li4.Value = "6";
        li4.Text = "6 Part Advance";
        ddlSchcode.Items.Add(li4);
    }
    protected void ddlSchcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSchcode.SelectedValue != "0")
        {
            txtScheduleNumber.Text = ddlSchcode.SelectedValue;
        }
        else
            txtScheduleNumber.Text = "";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "key", "toggleSelection();", true);
        ddlSchcode.Focus();
    }
    protected void chkLCIndication_CheckedChanged(object sender, EventArgs e)
    {
        if (chkLCIndication.Checked == true)
        {
            chkLCIndication.Text = "Yes";
        }
        else
        {
            chkLCIndication.Text = "No";
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "key", "toggleSelection();", true);
        chkLCIndication.Focus();
    }
    protected void txtPurposecode_TextChanged(object sender, EventArgs e)
    {
        txtPurposecode.Text = txtPurposecode.Text.ToUpper();
        fillPCDescription();
        txtPurposecode.Focus();
    }
    protected void txtCountryACHolder1_TextChanged(object sender, EventArgs e)
    {
        txtCountryACHolder1.Text = txtCountryACHolder1.Text.ToUpper();
        fillCountryACHolderdesc();
        fillBankDesc();
        txtCountryACHolder1.Focus();
        txtBankCode.Text = "";
        lblBankCode.Text = "";
    }
    public void NostroVostro()
    {
        if (txtNostroVostro.Text == "N")
        {
            lblnostrovostro.Text = "Nostro";
        }
        else if (txtNostroVostro.Text == "V")
        {
            lblnostrovostro.Text = "Vostro";
        }
    }
    protected void txtNostroVostro_TextChanged1(object sender, EventArgs e)
    {
        NostroVostro();
    }
    protected void txtCurrency_TextChanged(object sender, EventArgs e)
    {
        txtCurrency.Text = txtCurrency.Text.ToUpper();
        fillCurrencyDes();
        txtCurrency.Focus();
    }
    protected void txtCountryBeneficiary1_TextChanged(object sender, EventArgs e)
    {
        txtCountryBeneficiary1.Text = txtCountryBeneficiary1.Text.ToUpper();
        fillCountryBeneficiarydesc();
        txtCountryBeneficiary1.Focus();
    }
    protected void txtBankCode_TextChanged(object sender, EventArgs e)
    {
        fillBankDesc();
        txtBankCode.Focus();
    }
    protected void txtImpExpCountry_TextChange(object sender, EventArgs e)
    {
        txtImpExpCountry.Text = txtImpExpCountry.Text.ToUpper();
        fillCountryIEdesc();
        txtImpExpCountry.Focus();
    }
    public void fillCountryIEdesc()
    {
        if (txtImpExpCountry.Text != "")
        {
            lblImpExpCountry.Text = "";
            string _query = "TF_GetCountryDesc";
            SqlParameter p1 = new SqlParameter("@countryID", SqlDbType.VarChar);
            p1.Value = txtImpExpCountry.Text.Trim();
            TF_DATA objData = new TF_DATA();
            DataTable dt = objData.getData(_query, p1);
            if (dt.Rows.Count > 0)
            {
                lblImpExpCountry.Text = dt.Rows[0][0].ToString().Trim();
            }
            else
            {
                lblImpExpCountry.Text = "<font style=color:red>" + "Invalid Country of Importer/Exporter" + "</font>";
                txtImpExpCountry.Text = "";
                txtImpExpCountry.Focus();
            }
        }
        else
        {
            lblImpExpCountry.Text = "";
        }
    }
    public void fillCurrencyDes()
    {
        if (txtCurrency.Text != "")
        {
            txtCurrencyDescription.Text = "";
            SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
            string _query = "TF_ValidateCurrencyList";
            p1.Value = txtCurrency.Text.Trim();
            TF_DATA objData = new TF_DATA();
            DataTable dt = objData.getData(_query, p1);
            if (dt.Rows.Count > 0)
            {
                txtCurrencyDescription.Text = dt.Rows[0][2].ToString().Trim();
            }
            else
            {
                txtCurrency.Text = "";
                txtCurrencyDescription.Text = "<font style=color:red>" + "Invalid Currency" + "</font>";
                txtCurrency.Focus();
            }
        }
        else
        {
            txtCurrencyDescription.Text = "";
        }
    }
    public void fillCountryACHolderdesc()
    {
        if (txtCountryACHolder1.Text != "")
        {
            txtCountryACHolder.Text = "";
            string _query = "TF_RET_HelpGetCountryACList";
            SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
            p1.Value = txtCountryACHolder1.Text.Trim();
            TF_DATA objData = new TF_DATA();
            DataTable dt = objData.getData(_query, p1);
            if (dt.Rows.Count > 0)
            {
                txtCountryACHolder.Text = dt.Rows[0][1].ToString().Trim();
            }
            else
            {
                txtCountryACHolder1.Text = "";
                txtCountryACHolder.Text = "";
                txtCountryACHolder1.Focus();
            }
        }
        else
        {
            txtCountryACHolder.Text = "";
        }
    }
    public void fillCountryBeneficiarydesc()
    {
        if (txtCountryBeneficiary1.Text != "")
        {
            txtCountryBeneficiary.Text = "";
            string _query = "TF_GetCountryDesc";
            SqlParameter p1 = new SqlParameter("@countryID", SqlDbType.VarChar);
            p1.Value = txtCountryBeneficiary1.Text.Trim();
            TF_DATA objData = new TF_DATA();
            DataTable dt = objData.getData(_query, p1);
            if (dt.Rows.Count > 0)
            {
                txtCountryBeneficiary.Text = dt.Rows[0][0].ToString().Trim();
            }
            else
            {
                txtCountryBeneficiary.Text = "Invalid Country Code";
                txtCountryBeneficiary1.Focus();
            }
        }
        else
        {
            txtCountryBeneficiary.Text = "";
        }
    }
    public void fillBankDesc()
    {
        if (txtBankCode.Text != "")
        {
            lblBankCode.Text = "";
            string _query = "TF_RET_HelpGetBankCodeList";
            SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
            p1.Value = txtBankCode.Text.Trim();
            SqlParameter p2 = new SqlParameter("@countryofac", SqlDbType.VarChar);
            p2.Value = txtCountryACHolder1.Text.Trim();
            TF_DATA objData = new TF_DATA();
            DataTable dt = objData.getData(_query, p1, p2);
            if (dt.Rows.Count > 0)
            {
                lblBankCode.Text = dt.Rows[0][1].ToString().Trim();
            }
            else
            {
                txtBankCode.Text = "";
                txtBankCode.Focus();
            }
        }
        else
        {
            lblBankCode.Text = "";
        }
    }
    public void fillportCode()
    {
        if (txtPort.Text != "")
        {
            txtPortCode.Text = "";
            string _query = "TF_RET_HelpPortCode";
            SqlParameter p1 = new SqlParameter("@Search", SqlDbType.VarChar);
            p1.Value = txtPort.Text.Trim();
            TF_DATA objData = new TF_DATA();
            DataTable dt = objData.getData(_query, p1);
            if (dt.Rows.Count > 0)
            {
                txtPortCode.Text = dt.Rows[0][1].ToString().Trim();
            }
            else
            {
                txtPort.Text = "";
                txtPortCode.Text = "Invalid Port Code";
                txtPort.Focus();
            }

        }
        else
        {
            txtPortCode.Text = "";
        }
    }
    protected void txtPort_TextChanged(object sender, EventArgs e)
    {
        fillportCode();
        txtPort.Focus();
    }
    protected void fillBank()
    {
        TF_DATA objData = new TF_DATA();
        string _query = "TF_RET_GetBankName";
        DataTable dt = objData.getData(_query);
        if (dt.Rows.Count > 0)
        {
            lblBankname.Text = dt.Rows[0]["BankName"].ToString();
        }
    }}
