using DocumentFormat.OpenXml.Vml.Office;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Masters_EXP_ConsigneePartyMaster : System.Web.UI.Page
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
                clearall();
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("EXP_ConsigneePartyMasterView.aspx", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtPartyId.Text = Request.QueryString["PartyID"].Trim();
                        txtPartyId.Enabled = false;
                        fillDetails(Request.QueryString["PartyID"].Trim());
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
                txtEmailId.Attributes.Add("onblur", "return checkEmail(" + txtEmailId.ClientID + ");"); // Anand 20-11-2023
                txtCPEMailId.Attributes.Add("onblur", "return checkCPEmail(" + txtCPEMailId.ClientID + ");"); // Anand 20-11-2023
                txtTelephoneNo.Attributes.Add("onkeydown", "return validate_Number(event);"); //Anand 20-11-2023
                txtCPMobileNo.Attributes.Add("onkeydown", "return validate_Number(event);"); // Anand 20-11-2023
                txtpincode.Attributes.Add("onkeydown", "return validate_Number(event);"); // Anand 20-11-2023
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _userName = Session["userName"].ToString();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _mode = Request.QueryString["mode"].Trim();
        string _PartyID = txtPartyId.Text.Trim();
        string _PartyName = txtPartyName.Text.Trim();
        string _PartyAddress = txtaddress.Text.Trim();
        string _PartyCity = txtcity.Text.Trim();
        string _partyPincode = txtpincode.Text.Trim();
        string _partyCountry = txtcountry.Text.Trim();
        string _partyTelephoneNo = txtTelephoneNo.Text.Trim();
        string _Party_Fax_No = txtFaxNo.Text.Trim();
        string _partyEmailID = txtEmailId.Text.Trim();
        string _partyContactPerson = txtContactPerson.Text.Trim();
        string _Party_CP_Email_id = txtCPEMailId.Text.Trim();
        string _Party_CP_Mobile_No = txtCPMobileNo.Text.Trim();
        string _Party_AC_No = txtACNo.Text.Trim();
        string _Party_LEI_No = txtLEINo.Text.Trim();
        string _Party_LEIExpiry_Date = txtLEIExpiry.Text.Trim();

        SqlParameter p1 = new SqlParameter("@mode", SqlDbType.VarChar);
        p1.Value = _mode;
        SqlParameter p2 = new SqlParameter("@Party_Code", SqlDbType.VarChar);
        p2.Value = _PartyID;
        SqlParameter p3 = new SqlParameter("@Party_Name", SqlDbType.VarChar);
        p3.Value = _PartyName;
        SqlParameter p4 = new SqlParameter("@Party_Address", SqlDbType.VarChar);
        p4.Value = _PartyAddress;
        SqlParameter p5 = new SqlParameter("@Party_City", SqlDbType.VarChar);
        p5.Value = _PartyCity;
        SqlParameter p6 = new SqlParameter("@Party_Pincode ", SqlDbType.VarChar);
        p6.Value = _partyPincode;
        SqlParameter p7 = new SqlParameter("@Party_Country", SqlDbType.VarChar);
        p7.Value = _partyCountry;
        SqlParameter p8 = new SqlParameter("@Party_Telephone_No ", SqlDbType.VarChar);
        p8.Value = _partyTelephoneNo;
        SqlParameter p9 = new SqlParameter("@Party_Fax_No", SqlDbType.VarChar);
        p9.Value = _Party_Fax_No;
        SqlParameter p10 = new SqlParameter("@Party_Email_Id", SqlDbType.VarChar);
        p10.Value = _partyEmailID;
        SqlParameter p11 = new SqlParameter("@Party_Contact_Person", SqlDbType.VarChar);
        p11.Value = _partyContactPerson;
        SqlParameter p12 = new SqlParameter("@Party_CP_Email_id", SqlDbType.VarChar);
        p12.Value = _Party_CP_Email_id;
        SqlParameter p13 = new SqlParameter("@Party_CP_Mobile_No", SqlDbType.VarChar);
        p13.Value = _Party_CP_Mobile_No;
        SqlParameter p14 = new SqlParameter("@Party_AC_No", SqlDbType.VarChar);
        p14.Value = _Party_AC_No;

        SqlParameter p16 = new SqlParameter("@user", SqlDbType.VarChar);
        p16.Value = _userName;
        SqlParameter p17 = new SqlParameter("@uploadeddate", SqlDbType.VarChar);
        p17.Value = _uploadingDate;
        SqlParameter p18 = new SqlParameter("@Party_LEI_No", SqlDbType.VarChar);
        p18.Value = _Party_LEI_No;
        SqlParameter p19 = new SqlParameter("@Party_LEI_Expiry_Date", SqlDbType.VarChar);
        p19.Value = _Party_LEIExpiry_Date;

        string _query = "TF_UpdateConsigneePartyMaster";
        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p16, p17, p18, p19);
        //-----------------------Audit Trail BY NILESH-------------------------------
        SqlParameter A1 = new SqlParameter("@OldValues", SqlDbType.VarChar);
        SqlParameter A2 = new SqlParameter("@NewValues", SqlDbType.VarChar);
        SqlParameter A3 = new SqlParameter("@ModType", SqlDbType.VarChar);
        SqlParameter A4 = new SqlParameter("@ModifiedBy", Session["userName"].ToString().Trim());
        SqlParameter A5 = new SqlParameter("@ModifiedDate", "");
        SqlParameter A6 = new SqlParameter("@MenuName", "Bank Master");
        SqlParameter A7 = new SqlParameter("@Mode", SqlDbType.VarChar);
        SqlParameter A8 = new SqlParameter("@RefNo", SqlDbType.VarChar);
        string _script = "";
        if (_result == "added")
        {//-------------------------- BY NILESH-------------------------------
            _NewValue = "Party Code : " + txtPartyId.Text.Trim() + "; Party Name : " + txtPartyName.Text.Trim() + "; Party Address : " + txtaddress.Text.Trim()
                         + "; Party City : " + txtcity.Text.Trim() + "; Party Pincode : " + txtpincode.Text.Trim() + "; Party Country : " + txtcountry.Text.Trim()
                         + "; Telephone No : " + txtTelephoneNo.Text.Trim() + "; Fax No : " + txtFaxNo.Text.Trim()
                         + ";E-Mail Id : " + txtEmailId.Text.Trim() + "; Contact Person : " + txtContactPerson.Text.Trim() + ";Party CP E-Mail : " + txtCPEMailId.Text.Trim()
                         + "; Party CP Mobile No : " + txtCPMobileNo.Text.Trim() + "; Party AC No : " + txtACNo.Text.Trim()
                         + "; Party Type : " + "" + "; LEI No : " + txtLEINo.Text.Trim() + "; LEI Expiry Date : " + txtLEIExpiry.Text.Trim();
            A1.Value = "";
            A2.Value = _NewValue;
            A3.Value = "IMP";
            A7.Value = "A";
            A8.Value = txtPartyId.Text.Trim();
            string p = objSave.SaveDeleteData("TF_Mizuho_IMP_AuditTrail_master", A1, A2, A3, A4, A5, A6, A7, A8);
            //--------------------------END BY NILESH-------------------------------
            _script = "window.location='EXP_ConsigneePartyMasterView.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (_result == "updated")
            {
                //---------------------------------Nilesh------------------------------------------------------------------
                int isneedtolog = 0;
                _OldValues = "Party Code : " + hdnCode.Value + "; Party Name : " + hdnName.Value + "; Party Address : " + hdnAddress.Value
                         + "; Party City : " + hdnCity.Value + "; Party Pincode : " + hdnpincode.Value + "; Party Country : " + hdncountry.Value
                         + "; Telephone No : " + hdntelephone.Value + "; Fax No : " + hdnfaxno.Value
                         + ";E-Mail Id : " + hdnemail.Value + "; Contact Person : " + hdncontactperson.Value + ";Party CP E-Mail : " + hdnPartyCPEmail.Value
                         + "; Party CP Mobile No : " + hdnmobile.Value + "; Party AC No : " + hdnCPacntNo.Value
                         + "; Party Type : " + hdnPartyType.Value + "; LEI No : " + hdnPartyLEINo.Value + "; LEI Expiry Date : " + hdnPartyLEIExpiryDate.Value;

                if (hdnCode.Value != txtPartyId.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = "Party ID : " + txtPartyId.Text.Trim();
                    }
                    else
                    {
                        _NewValue = "Party ID : " + txtPartyId.Text.Trim();
                    }
                }
                if (hdnName.Value != txtPartyName.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; Party Name : " + txtPartyName.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; Party Name : " + txtPartyName.Text.Trim();
                    }
                }
                if (hdnAddress.Value != txtaddress.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; Party Address : " + txtaddress.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; Party Address : " + txtaddress.Text.Trim();
                    }
                }
                if (hdnCity.Value != txtcity.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; Party City : " + txtcity.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; Party City : " + txtcity.Text.Trim();
                    }
                }
                if (hdnpincode.Value != txtpincode.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; Party Pincode : " + txtpincode.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; Party Pincode : " + txtpincode.Text.Trim();
                    }
                }
                if (hdncountry.Value != txtcountry.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; Party Country : " + txtcountry.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; Party Country : " + txtcountry.Text.Trim();
                    }
                }
                if (hdntelephone.Value != txtTelephoneNo.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; Telephone No : " + txtTelephoneNo.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; Telephone No : " + txtTelephoneNo.Text.Trim();
                    }
                }
                if (hdnfaxno.Value != txtFaxNo.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; Fax No : " + txtFaxNo.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; Fax No : " + txtFaxNo.Text.Trim();
                    }
                }
                if (hdnemail.Value != txtEmailId.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; Email ID : " + txtEmailId.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; Email ID : " + txtEmailId.Text.Trim();
                    }
                }
                if (hdncontactperson.Value != txtContactPerson.Text.Trim())
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
                if (hdnPartyCPEmail.Value != txtCPEMailId.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; Party CP Email ID : " + txtCPEMailId.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; Party CP Email ID : " + txtCPEMailId.Text.Trim();
                    }
                }
                if (hdnmobile.Value != txtCPMobileNo.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; Party CP mobile No : " + txtCPMobileNo.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; Party CP mobile No : " + txtCPMobileNo.Text.Trim();
                    }
                }
                if (hdnCPacntNo.Value != txtACNo.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; Party AC No : " + txtACNo.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; Party AC No : " + txtACNo.Text.Trim();
                    }
                }
                if (hdnPartyLEINo.Value != txtLEINo.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; LEI No. : " + txtLEINo.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; LEI No. : " + txtLEINo.Text.Trim();
                    }
                }
                if (hdnPartyLEIExpiryDate.Value != txtLEIExpiry.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; LEI Expiry Date : " + txtLEIExpiry.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; LEI Expiry Date : " + txtLEIExpiry.Text.Trim();
                    }
                }
                A1.Value = _OldValues;
                A2.Value = _NewValue;
                A3.Value = "IMP";
                A7.Value = "M";
                A8.Value = txtPartyId.Text;

                if (isneedtolog == 1)
                {
                    string p = objSave.SaveDeleteData("TF_Mizuho_IMP_AuditTrail_master", A1, A2, A3, A4, A5, A6, A7, A8);
                }
                //---------------------------------END Nilesh------------------------------------------------------------------
                _script = "window.location='EXP_ConsigneePartyMasterView.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
                labelMessage.Text = _result;
        }
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("EXP_ConsigneePartyMasterView.aspx", true);
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
        txtLEINo.Text = "";
        txtLEIExpiry.Text = "";

    }
    protected void fillDetails(string _partyID)
    {
        SqlParameter p1 = new SqlParameter("@partyID", SqlDbType.VarChar);
        p1.Value = _partyID;



        string _query = "TF_GetConsigneePartyMasterDetails";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            txtPartyId.Text = dt.Rows[0]["Party_Code"].ToString().Trim();
            txtPartyName.Text = dt.Rows[0]["Party_Name"].ToString().Trim();
            txtaddress.Text = dt.Rows[0]["Party_Address"].ToString().Trim();
            txtcity.Text = dt.Rows[0]["Party_City"].ToString().Trim();
            txtpincode.Text = dt.Rows[0]["Party_Pincode"].ToString().Trim();
            txtcountry.Text = dt.Rows[0]["Party_Country"].ToString().Trim();
            fillCountryDescription();
            txtTelephoneNo.Text = dt.Rows[0]["Party_Telephone_No"].ToString().Trim();
            txtFaxNo.Text = dt.Rows[0]["Party_Fax_No"].ToString().Trim();
            txtEmailId.Text = dt.Rows[0]["Party_Email_Id"].ToString().Trim();
            txtContactPerson.Text = dt.Rows[0]["Party_Contact_Person"].ToString().Trim();
            txtCPEMailId.Text = dt.Rows[0]["Party_CP_Email_id"].ToString().Trim();
            txtCPMobileNo.Text = dt.Rows[0]["Party_CP_Mobile_No"].ToString().Trim();
            txtACNo.Text = dt.Rows[0]["Party_AC_No"].ToString().Trim();
            txtLEINo.Text = dt.Rows[0]["Party_LEI_No"].ToString().Trim();
            txtLEIExpiry.Text = dt.Rows[0]["Party_LEI_Expiry_Date"].ToString().Trim();
            //dropDownListType.SelectedIndex = dt.Rows[0]["Party_Type"].ToString().Trim();
            /*----------Audit trial-----------*/
            hdnCode.Value = dt.Rows[0]["Party_Code"].ToString().Trim();
            hdnName.Value = dt.Rows[0]["Party_Name"].ToString().Trim();
            hdnAddress.Value = dt.Rows[0]["Party_Address"].ToString().Trim();
            hdnCity.Value = dt.Rows[0]["Party_City"].ToString().Trim();
            hdnpincode.Value = dt.Rows[0]["Party_Pincode"].ToString().Trim();
            hdncountry.Value = dt.Rows[0]["Party_Country"].ToString().Trim();
            hdntelephone.Value = dt.Rows[0]["Party_Telephone_No"].ToString().Trim();
            hdnfaxno.Value = dt.Rows[0]["Party_Fax_No"].ToString().Trim();
            hdnemail.Value = dt.Rows[0]["Party_Email_Id"].ToString().Trim();
            hdncontactperson.Value = dt.Rows[0]["Party_Contact_Person"].ToString().Trim();
            hdnPartyCPEmail.Value = dt.Rows[0]["Party_CP_Email_id"].ToString().Trim();
            hdnmobile.Value = dt.Rows[0]["Party_CP_Mobile_No"].ToString().Trim();
            hdnCPacntNo.Value = dt.Rows[0]["Party_CP_Mobile_No"].ToString().Trim();
            hdnPartyLEINo.Value = dt.Rows[0]["Party_LEI_No"].ToString().Trim();
            hdnPartyLEIExpiryDate.Value = dt.Rows[0]["Party_LEI_Expiry_Date"].ToString().Trim();


        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EXP_ConsigneePartyMasterView.aspx", true);
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
    protected void txtcountry_TextChanged(object sender, EventArgs e)
    {
        fillCountryDescription();
        txtcountry.Focus();
    }
}