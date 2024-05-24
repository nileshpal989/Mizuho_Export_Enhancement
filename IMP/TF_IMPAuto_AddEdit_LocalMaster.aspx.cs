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

public partial class IMP_TF_IMPAuto_AddEdit_LocalMaster : System.Web.UI.Page
{
    string _NewValue;
    string _OldValues;
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
                    Response.Redirect("TF_ImpAuto_ViewLocalMaster.aspx", true);
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
                        txtBankID.Enabled = true;
                        txtBankID.Focus();
                    }
                }
                btnSave.Attributes.Add("onclick", "return validateSave();");
                //txtCountry.Attributes.Add("onkeydown", "return CountryId(event)");
                //btnCountryList.Attributes.Add("onclick", "return Countryhelp()");

            }
            txtCountry.Text = "IN";
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ImpAuto_ViewLocalMaster.aspx", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _mode = Request.QueryString["mode"].Trim();
        string _bankCode = txtBankID.Text.Trim();
        string _bankName = txtBankName.Text.Trim();
        string _address = txtAddress.Text.Trim();
        string _city = txtCity.Text.Trim();
        string _pincode = txtPincode.Text.Trim();
        string _state = txtState.Text.Trim();
        string _country = txtCountry.Text.Trim();
        string _telephoneNo = txtTelephoneNo.Text.Trim();
        string _faxNo = txtFaxNo.Text.Trim();
        string _swiftCode = txtSwiftCode.Text.Trim();
        string _emailid = txtEmailId.Text.Trim();
        string _contactPerson = txtContactPerson.Text.Trim();

        SqlParameter p1 = new SqlParameter("@mode", SqlDbType.VarChar);
        p1.Value = _mode;
        SqlParameter p2 = new SqlParameter("@bankcode", SqlDbType.VarChar);
        p2.Value = _bankCode;
        SqlParameter p3 = new SqlParameter("@bankname", SqlDbType.VarChar);
        p3.Value = _bankName;
        SqlParameter p4 = new SqlParameter("@address", SqlDbType.VarChar);
        p4.Value = _address;
        SqlParameter p5 = new SqlParameter("@city", SqlDbType.VarChar);
        p5.Value = _city;
        SqlParameter p6 = new SqlParameter("@pincode", SqlDbType.VarChar);
        p6.Value = _pincode;
        SqlParameter pState = new SqlParameter("@State", SqlDbType.VarChar);
        pState.Value = _state;
        SqlParameter p7 = new SqlParameter("@country", SqlDbType.VarChar);
        p7.Value = _country;
        SqlParameter p8 = new SqlParameter("@telephoneNo", SqlDbType.VarChar);
        p8.Value = _telephoneNo;
        SqlParameter p9 = new SqlParameter("@swiftcode", SqlDbType.VarChar);
        p9.Value = _swiftCode;
        SqlParameter p10 = new SqlParameter("@faxNo", SqlDbType.VarChar);
        p10.Value = _faxNo;
        SqlParameter p11 = new SqlParameter("@emailid", SqlDbType.VarChar);
        p11.Value = _emailid;
        SqlParameter p12 = new SqlParameter("@contactPerson", SqlDbType.VarChar);
        p12.Value = _contactPerson;
        SqlParameter p18 = new SqlParameter("@user", SqlDbType.VarChar);
        p18.Value = _userName;
        SqlParameter p19 = new SqlParameter("@uploadingdate", SqlDbType.VarChar);
        p19.Value = _uploadingDate;

        string _query = "TF_UpdateLocalBankMaster";
        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p18, p19,pState);

        ///////////Audit Trail////////////////////////////////
        SqlParameter A1 = new SqlParameter("@OldValues", SqlDbType.VarChar);
        SqlParameter A2 = new SqlParameter("@NewValues", SqlDbType.VarChar);
        SqlParameter A3 = new SqlParameter("@ModType", SqlDbType.VarChar);
        SqlParameter A4 = new SqlParameter("@ModifiedBy", Session["userName"].ToString().Trim());
        SqlParameter A5 = new SqlParameter("@ModifiedDate", "");
        SqlParameter A6 = new SqlParameter("@MenuName", "Local Bank Master");
        SqlParameter A7 = new SqlParameter("@Mode", SqlDbType.VarChar);
        SqlParameter A8 = new SqlParameter("@RefNo", SqlDbType.VarChar);

        string _script = "";
        if (_result == "added")
        {
            _NewValue = "Bank ID : " + txtBankID.Text.Trim() + " ; Bank Name : " + txtBankName.Text.Trim() + " ; Address : " + txtAddress.Text.Trim() + " ; City : " + txtCity.Text.Trim()
                + " ; Pincode : " + txtPincode.Text.Trim() + " ; Country : " + txtCountry.Text.Trim() + " ; Telephone : " + txtTelephoneNo.Text.Trim() + " ; FaxNo : " + txtFaxNo.Text.Trim()
                 + " ; EmailID : " + txtEmailId.Text.Trim() + " ; Contact Person : " + txtContactPerson.Text.Trim();

            A1.Value = "";
            A2.Value = _NewValue;
            A3.Value = "IMP";
            A7.Value = "A";
            A8.Value = txtBankID.Text;
            string p = objSave.SaveDeleteData("TF_Mizuho_IMP_AuditTrail_master", A1, A2, A3, A4, A5, A6, A7, A8);

            _script = "window.location='TF_ImpAuto_ViewLocalMaster.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            int isneedtolog = 0;
            if (_result == "updated")
            {
                _OldValues = " Bank Name : " + hdnbankname.Value.Trim() + " ; Address : " + hdnaddress.Value.Trim() + " ; City : " + hdncity.Value.Trim()
                + " ; Pincode : " + hdnpincode.Value.Trim() + " ; Country : " + hdncountry.Value.Trim() + " ; Telephone : " + hdntelino.Value.Trim() + " ; FaxNo : " + hdnfaxno.Value.Trim()
                 + " ; EmailID : " + hdnemail.Value.Trim() + " ; Contact Person : " + hdncontactp.Value.Trim();
                if (hdnbankname.Value != txtBankName.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = "Bank Name : " + txtBankName.Text.Trim();
                    }
                    else
                    {
                        _NewValue = "Bank Name : " + txtBankName.Text.Trim();
                    }
                }
                if (hdnaddress.Value != txtAddress.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; Address : " + txtAddress.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; Address : " + txtAddress.Text.Trim();
                    }
                }
                if (hdncity.Value != txtCity.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ;  City : " + txtCity.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ;  City  : " + txtCity.Text.Trim();
                    }
                }
                if (hdnpincode.Value != txtPincode.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; Pincode : " + txtPincode.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; Pincode : " + txtPincode.Text.Trim();
                    }
                }
                if (hdncountry.Value != txtCountry.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; Country : " + txtCountry.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; Country : " + txtCountry.Text.Trim();
                    }
                }
                if (hdntelino.Value != txtTelephoneNo.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; Telephone : " + txtTelephoneNo.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; Telephone : " + txtTelephoneNo.Text.Trim();
                    }
                }
                if (hdnfaxno.Value != txtFaxNo.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; FaxNo : " + txtFaxNo.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; FaxNo : " + txtFaxNo.Text.Trim();
                    }
                }
                if (hdnemail.Value != txtEmailId.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; EmailID : " + txtEmailId.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; EmailID : " + txtEmailId.Text.Trim();
                    }
                }
                if (hdncontactp.Value != txtContactPerson.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; Contact Person : " + txtContactPerson.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; Contact Person : " + txtContactPerson.Text.Trim();
                    }
                }

                A1.Value = _OldValues;
                A2.Value = _NewValue;
                A3.Value = "IMP";
                A7.Value = "M";
                A8.Value = txtBankID.Text;

                if (isneedtolog == 1)
                {
                    string p = objSave.SaveDeleteData("TF_Mizuho_IMP_AuditTrail_master", A1, A2, A3, A4, A5, A6, A7, A8);
                }
                _script = "window.location='TF_ImpAuto_ViewLocalMaster.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
                labelMessage.Text = _result;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ImpAuto_ViewLocalMaster.aspx", true);
    }
    protected void clearControls()
    {
        txtBankID.Text = "";
        txtBankName.Text = "";
        txtAddress.Text = "";
        txtCity.Text = "";
        txtPincode.Text = "";
        txtState.Text = "";
        txtTelephoneNo.Text = "";
        txtFaxNo.Text = "";
        txtSwiftCode.Text = "";
        txtEmailId.Text = "";
        txtContactPerson.Text = "";
    }
    protected void fillDetails(string _bankCode)
    {
        SqlParameter p1 = new SqlParameter("@bankcode", SqlDbType.VarChar);
        p1.Value = _bankCode;
        string _query = "TF_LocalBankMasterDetails";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            txtBankName.Text = dt.Rows[0]["BankName"].ToString().Trim();
            txtAddress.Text = dt.Rows[0]["BankAddress"].ToString().Trim();
            txtCity.Text = dt.Rows[0]["City"].ToString().Trim();
            txtPincode.Text = dt.Rows[0]["Pincode"].ToString().Trim();
            txtTelephoneNo.Text = dt.Rows[0]["Telephone"].ToString().Trim();
            txtSwiftCode.Text = dt.Rows[0]["SwiftCode"].ToString().Trim();
            txtFaxNo.Text = dt.Rows[0]["FaxNo"].ToString().Trim();
            txtEmailId.Text = dt.Rows[0]["EmailID"].ToString().Trim();
            txtContactPerson.Text = dt.Rows[0]["ContactPerson"].ToString().Trim();
            //------------------------Audit trial--------------------------
            hdnbankname.Value = dt.Rows[0]["BankName"].ToString().Trim();
            hdnaddress.Value = dt.Rows[0]["BankAddress"].ToString().Trim();
            hdncity.Value = dt.Rows[0]["City"].ToString().Trim();
            hdnpincode.Value = txtPincode.Text = dt.Rows[0]["Pincode"].ToString().Trim();
            hdnState.Value = txtState.Text = dt.Rows[0]["State"].ToString().Trim();
            hdncountry.Value = dt.Rows[0]["Country"].ToString().Trim();
            hdntelino.Value = dt.Rows[0]["Telephone"].ToString().Trim();
            hdnfaxno.Value = dt.Rows[0]["FaxNo"].ToString().Trim();
            hdnemail.Value = dt.Rows[0]["EmailID"].ToString().Trim();
            hdncontactp.Value = dt.Rows[0]["ContactPerson"].ToString().Trim();
        }
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
    }
}