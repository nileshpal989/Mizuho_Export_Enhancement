using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class INW_AddEditOverseasBank : System.Web.UI.Page
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
                btnSave.Attributes.Add("onclick", "return validateSave();");
                clearControls();
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("INW_ViewCustomerMaster.aspx", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtBankID.Text = Request.QueryString["bankcode"].Trim();
                        txtBankID.Enabled = false;
                        txtBankName.Focus();
                        fillDetails(Request.QueryString["bankcode"].Trim());
                    }
                    else
                    {
                        GetMAX_BankCode();
                        txtBankID.Enabled = false;
                        txtBankID.Focus();
                    }
                }
                btnSave.Attributes.Add("onclick", "return validateSave();");
                txtCountry.Attributes.Add("onkeydown", "return CountryId(event)");
                btnCountryList.Attributes.Add("onclick", "return Countryhelp()");

            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("INW_ViewOverseasBank.aspx", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _IMP_Check = "";
        if (Chk_IMP_Auto.Checked)
        {
            _IMP_Check = "Y";
        }
        else
        {
            _IMP_Check = "N";
        }

        SqlParameter p_mode = new SqlParameter("@mode", SqlDbType.VarChar);
        p_mode.Value = Request.QueryString["mode"].Trim(); ;
        SqlParameter p_bankCode = new SqlParameter("@bankcode", SqlDbType.VarChar);
        p_bankCode.Value = txtBankID.Text.Trim(); ;
        SqlParameter p_bankName = new SqlParameter("@bankname", SqlDbType.VarChar);
        p_bankName.Value = txtBankName.Text.Trim(); ;
        SqlParameter p_address = new SqlParameter("@address", SqlDbType.VarChar);
        p_address.Value = txtAddress.Text.Trim();
        SqlParameter p_city = new SqlParameter("@city", SqlDbType.VarChar);
        p_city.Value = txtCity.Text.Trim();
        SqlParameter p_pincode = new SqlParameter("@pincode", SqlDbType.VarChar);
        p_pincode.Value = txtPincode.Text.Trim();
        SqlParameter p_country = new SqlParameter("@country", SqlDbType.VarChar);
        p_country.Value = txtCountry.Text.Trim();
        SqlParameter p_telephoneNo = new SqlParameter("@telephoneNo", SqlDbType.VarChar);
        p_telephoneNo.Value = txtTelephoneNo.Text.Trim();
        SqlParameter p_swiftCode = new SqlParameter("@swiftcode", SqlDbType.VarChar);
        p_swiftCode.Value = txtSwiftCode.Text.Trim();
        SqlParameter p_faxNo = new SqlParameter("@faxNo", SqlDbType.VarChar);
        p_faxNo.Value = txtFaxNo.Text.Trim();
        SqlParameter p_emailid = new SqlParameter("@emailid", SqlDbType.VarChar);
        p_emailid.Value = txtEmailId.Text.Trim();
        SqlParameter p_contactPerson = new SqlParameter("@contactPerson", SqlDbType.VarChar);
        p_contactPerson.Value = txtContactPerson.Text.Trim();
        SqlParameter p_vostroAcNo = new SqlParameter("@VostroBankAC_No", SqlDbType.VarChar);
        p_vostroAcNo.Value = txtVostroACNO.Text.Trim();
        SqlParameter pBMAcNo = new SqlParameter("@BMAC_No", SqlDbType.VarChar);
        pBMAcNo.Value = txtBMplusAcNo.Text.Trim();
        SqlParameter p_sortCode = new SqlParameter("@sortCd", SqlDbType.VarChar);
        p_sortCode.Value = txtSortCode.Text.Trim();
        SqlParameter p_chipUID = new SqlParameter("@chipUID", SqlDbType.VarChar);
        p_chipUID.Value = txtChipUID.Text.Trim();
        SqlParameter p_chipsABAno = new SqlParameter("@chipABANo", SqlDbType.VarChar);
        p_chipsABAno.Value = txtChipsABAno.Text.Trim();
        SqlParameter p_abaNo = new SqlParameter("@ABANo", SqlDbType.VarChar);
        p_abaNo.Value = txtABAno.Text.Trim();
        SqlParameter p_userName = new SqlParameter("@user", SqlDbType.VarChar);
        p_userName.Value = Session["userName"].ToString().Trim();
        SqlParameter p_uploadingDate = new SqlParameter("@uploadingdate", SqlDbType.VarChar);
        p_uploadingDate.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        SqlParameter p_IMP_Check = new SqlParameter("@IMP_Check", _IMP_Check);
        SqlParameter p_IFSC_Code = new SqlParameter("@IFSC_Code", txtIFSC_Code.Text.Trim());


        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData("TF_UpdateOverseasBankMaster", p_mode, p_bankCode, p_bankName, p_address, p_city, p_pincode, p_country, p_telephoneNo, p_swiftCode, p_faxNo,
            p_emailid, p_contactPerson, p_vostroAcNo, pBMAcNo, p_sortCode, p_chipUID, p_chipsABAno, p_abaNo, p_userName, p_uploadingDate, p_IMP_Check, p_IFSC_Code);

        string _script = "";
        if (_result == "added")
        {
            _script = "window.location='INW_ViewOverseasBank.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (_result == "updated")
            {
                _script = "window.location='INW_ViewOverseasBank.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
                labelMessage.Text = _result;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("INW_ViewOverseasBank.aspx", true);
    }
    protected void clearControls()
    {
        txtBankID.Text = "";
        txtBankName.Text = "";
        txtAddress.Text = "";
        txtCity.Text = "";
        txtPincode.Text = "";
        txtCountry.Text = "";
        txtTelephoneNo.Text = "";
        txtFaxNo.Text = "";
        txtSwiftCode.Text = "";
        txtEmailId.Text = "";
        txtContactPerson.Text = "";
        txtVostroACNO.Text = "";
        txtSortCode.Text = "";
        txtChipsABAno.Text = "";
        txtChipUID.Text = "";
        txtABAno.Text = "";

    }
    protected void fillDetails(string _bankCode)
    {
        SqlParameter p1 = new SqlParameter("@bankcode", SqlDbType.VarChar);
        p1.Value = _bankCode;
        string _query = "TF_GetOverseasBankMasterDetails";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            txtBankName.Text = dt.Rows[0]["BankName"].ToString().Trim();
            txtAddress.Text = dt.Rows[0]["BankAddress"].ToString().Trim();
            txtCity.Text = dt.Rows[0]["City"].ToString().Trim();
            txtPincode.Text = dt.Rows[0]["Pincode"].ToString().Trim();
            txtCountry.Text = dt.Rows[0]["Country"].ToString().Trim();
            txtTelephoneNo.Text = dt.Rows[0]["Telephone"].ToString().Trim();
            txtSwiftCode.Text = dt.Rows[0]["SwiftCode"].ToString().Trim();
            txtFaxNo.Text = dt.Rows[0]["FaxNo"].ToString().Trim();
            txtEmailId.Text = dt.Rows[0]["EmailID"].ToString().Trim();
            txtContactPerson.Text = dt.Rows[0]["ContactPerson"].ToString().Trim();
            txtVostroACNO.Text = dt.Rows[0]["VostroBankAC_No"].ToString().Trim();
            txtBMplusAcNo.Text = dt.Rows[0]["AC_No"].ToString().Trim();
            txtSortCode.Text = dt.Rows[0]["SortCd"].ToString().Trim();
            txtChipUID.Text = dt.Rows[0]["ChipUID"].ToString().Trim();
            txtChipsABAno.Text = dt.Rows[0]["ChipABANo"].ToString().Trim();
            txtABAno.Text = dt.Rows[0]["ABANo"].ToString().Trim();

            txtCountry_TextChanged(this, null);
            if (dt.Rows[0]["IMP_Check"].ToString().Trim() == "Y")
            {
                Chk_IMP_Auto.Checked = true;
            }
            else
            {
                Chk_IMP_Auto.Checked = false;
            }

            txtIFSC_Code.Text = dt.Rows[0]["IFSC_Code"].ToString().Trim();

        }
    }
    protected void txtCountry_TextChanged(object sender, EventArgs e)
    {
        fillCountryDescription();
    }
    public void fillCountryDescription()
    {
        //lblCountryName.Text = "";
        string Countryid = txtCountry.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@cid", SqlDbType.VarChar);
        p1.Value = Countryid;

        string _query = "TF_GetCountryDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCountryName.Text = dt.Rows[0]["CountryName"].ToString().Trim();
        }
        else
        {
            txtCountry.Text = "";
            lblCountryName.Text = "";
        }
    }
    private void GetMAX_BankCode()
    {
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData("TF_Get_Max_OverseasBankID");
        if (dt.Rows.Count > 0)
        {
            txtBankID.Text = dt.Rows[0]["OverseasBankID"].ToString();
        }

    }
}