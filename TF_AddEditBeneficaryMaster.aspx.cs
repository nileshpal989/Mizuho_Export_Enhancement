using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class TF_AddEditBeneficaryMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                clearall();
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("TF_ViewBeneficaryMaster.aspx", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtPartyId.Text = Request.QueryString["BenfID"].Trim();
                        txtPartyId.Enabled = false;
                        fillDetails(Request.QueryString["BenfID"].Trim(),Request.QueryString["Type"].Trim());
                    }
                    else
                    {
                        txtPartyId.Enabled = true;
                    }
                }
                btnSave.Attributes.Add("onclick", "return validateSave();");

                txtcountry.Attributes.Add("onkeydown", "return CountryId(event)");
                btnCountryList.Attributes.Add("onclick", "return Countryhelp()");
                txtEmailId.Attributes.Add("onblur", "toLower_Case();");
                txtCPEMailId.Attributes.Add("onblur", "toLower_Case();");
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _userName = Session["userName"].ToString();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _mode = Request.QueryString["mode"].Trim();
        string _BenfID = txtPartyId.Text.Trim();
        string _BenfName = txtPartyName.Text.Trim();
        string _BenfAddress = txtaddress.Text.Trim();
        string _BenfCity = txtcity.Text.Trim();
        string _BenfPincode = txtpincode.Text.Trim();
        string _BenfCountry = txtcountry.Text.Trim();
        string _BenfTelephoneNo = txtTelephoneNo.Text.Trim();
        string _Benf_Fax_No = txtFaxNo.Text.Trim();
        string _BenfEmailID = txtEmailId.Text.Trim();
        string _BenfContactPerson = txtContactPerson.Text.Trim();
        string _Benf_CP_Email_id = txtCPEMailId.Text.Trim();
        string _Benf_CP_Mobile_No = txtCPMobileNo.Text.Trim();
        string _Benf_AC_No = txtACNo.Text.Trim();
        string _Benf_Type;
        if (dropDownListType.SelectedIndex > -1)
        {
            _Benf_Type = dropDownListType.SelectedValue.Trim();
        }
        else
            _Benf_Type = "";


        SqlParameter p1 = new SqlParameter("@mode", SqlDbType.VarChar);
        p1.Value = _mode;
        SqlParameter p2 = new SqlParameter("@BenfID", SqlDbType.VarChar);
        p2.Value = _BenfID;
        SqlParameter p3 = new SqlParameter("@BenfName", SqlDbType.VarChar);
        p3.Value = _BenfName;
        SqlParameter p4 = new SqlParameter("@BenfAdd", SqlDbType.VarChar);
        p4.Value = _BenfAddress;
        SqlParameter p5 = new SqlParameter("@BenfCity", SqlDbType.VarChar);
        p5.Value = _BenfCity;
        SqlParameter p6 = new SqlParameter("@BenfPincode ", SqlDbType.VarChar);
        p6.Value = _BenfPincode;
        SqlParameter p7 = new SqlParameter("@BenfCountry", SqlDbType.VarChar);
        p7.Value = _BenfCountry;
        SqlParameter p8 = new SqlParameter("@BenfTelNo", SqlDbType.VarChar);
        p8.Value = _BenfTelephoneNo;
        SqlParameter p9 = new SqlParameter("@BenfFaxNo", SqlDbType.VarChar);
        p9.Value = _Benf_Fax_No;
        SqlParameter p10 = new SqlParameter("@BenfEmailId", SqlDbType.VarChar);
        p10.Value = _BenfEmailID;
        SqlParameter p11 = new SqlParameter("@Benf_Contact_person", SqlDbType.VarChar);
        p11.Value = _BenfContactPerson;
        SqlParameter p12 = new SqlParameter("@BenfCPEmailId", SqlDbType.VarChar);
        p12.Value = _Benf_CP_Email_id;
        SqlParameter p13 = new SqlParameter("@BenfCPMobNo", SqlDbType.VarChar);
        p13.Value = _Benf_CP_Mobile_No;
        SqlParameter p14 = new SqlParameter("@BenfACNo", SqlDbType.VarChar);
        p14.Value = _Benf_AC_No;
        SqlParameter p15 = new SqlParameter("@BenfType", SqlDbType.VarChar);
        p15.Value = _Benf_Type;
        SqlParameter p16 = new SqlParameter("@user", SqlDbType.VarChar);
        p16.Value = _userName;
        SqlParameter p17 = new SqlParameter("@UploadingDate", SqlDbType.VarChar);
        p17.Value = _uploadingDate;

        string _query = "TF_UpdateBeneficaryMaster";
        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17);

        string _script = "";
        if (_result == "added")
        {
            _script = "window.location='TF_ViewBeneficaryMaster.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (_result == "updated")
            {
                _script = "window.location='TF_ViewBeneficaryMaster.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
                labelMessage.Text = _result;
        }

     //   clearall();


    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewBeneficaryMaster.aspx", true);
    }


    public void clearall()
    {
        txtPartyId.Text = "";
        txtPartyName.Text = "";
        txtaddress.Text = "";
        txtcity.Text = "";
        txtpincode.Text = "";
        txtcountry.Text = "";
        txtTelephoneNo.Text = "";
        txtFaxNo.Text = "";
        txtEmailId.Text = "";
        txtContactPerson.Text = "";
        txtCPEMailId.Text = "";
        txtCPMobileNo.Text = "";
        txtACNo.Text = "";
        dropDownListType.SelectedIndex = -1;

    }

    protected void fillDetails(string _BenfID, string _Type)
    {
        SqlParameter p1 = new SqlParameter("@BenfID", SqlDbType.VarChar);
        p1.Value = _BenfID;
        SqlParameter p2 = new SqlParameter("@type", SqlDbType.VarChar);
        p2.Value = _Type;

        string _query = "TF_GetBeneficiaryMasterDetails_new";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1,p2);
        if (dt.Rows.Count > 0)
        {
            txtPartyId.Text = dt.Rows[0]["Benf_Id"].ToString().Trim();
            txtPartyName.Text = dt.Rows[0]["Benf_Name"].ToString().Trim();
            txtaddress.Text = dt.Rows[0]["Benf_Address"].ToString().Trim();
            txtcity.Text = dt.Rows[0]["Benf_City"].ToString().Trim();
            txtpincode.Text = dt.Rows[0]["Benf_pincode"].ToString().Trim();
            txtcountry.Text = dt.Rows[0]["Benf_Country"].ToString().Trim();
            fillCountryDescription();
            txtTelephoneNo.Text = dt.Rows[0]["Benf_Telephone_No"].ToString().Trim();
            txtFaxNo.Text = dt.Rows[0]["Benf_Fax_no"].ToString().Trim();
            txtEmailId.Text = dt.Rows[0]["Benf_Email_id"].ToString().Trim();
            txtContactPerson.Text = dt.Rows[0]["Benf_Contact_Person"].ToString().Trim();
            txtCPEMailId.Text = dt.Rows[0]["Benf_CP_Email_ID"].ToString().Trim();
            txtCPMobileNo.Text = dt.Rows[0]["Benf_CP_Mobile_No"].ToString().Trim();
            txtACNo.Text = dt.Rows[0]["Benf_Ac_No"].ToString().Trim();
            if (dt.Rows[0]["Benf_type"].ToString().Trim() != "")
            {
                dropDownListType.SelectedValue = dt.Rows[0]["Benf_type"].ToString().Trim();
            }

        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewBeneficaryMaster.aspx", true);
    }
    public void fillCountryDescription()
    {
        lblCountryName.Text = "";

        string Countryid = txtcountry.Text.Trim();
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
            txtcountry.Text = "";
            lblCountryName.Text = "";
        }
    }

    protected void dropDownListType_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropDownListType.Focus();
    }
    protected void txtcountry_TextChanged(object sender, EventArgs e)
    {
        fillCountryDescription();
        txtcountry.Focus();
    }
}