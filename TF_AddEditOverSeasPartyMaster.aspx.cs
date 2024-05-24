using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class TF_AddEditOverSeasPartyMaster : System.Web.UI.Page
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
                    Response.Redirect("TF_ViewOverseasPartyMaster.aspx", true);
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
        SqlParameter p15 = new SqlParameter("@Party_LEI_No", SqlDbType.VarChar);
        p15.Value = _Party_LEI_No;
        SqlParameter p16 = new SqlParameter("@Party_LEI_Expiry_Date", SqlDbType.VarChar);
        p16.Value = _Party_LEIExpiry_Date;

        SqlParameter p18 = new SqlParameter("@user", SqlDbType.VarChar);
        p18.Value = _userName;
        SqlParameter p19 = new SqlParameter("@uploadeddate", SqlDbType.VarChar);
        p19.Value = _uploadingDate;

        string _query = "TF_UpdateOverseasPartyMaster";
        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData(_query, p1, p2, p3, p4, p5,p6,p7,p8,p9,p10,p11,p12,p13,p14,p15,p16,p18,p19);
        //------------------------------------------Anand 20-11-2023-------------------------------
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "Yes")
        {
            SqlParameter p21 = new SqlParameter("@mode", SqlDbType.VarChar);
            p21.Value = _mode;
            SqlParameter p22 = new SqlParameter("@Party_Code", SqlDbType.VarChar);
            p22.Value = _PartyID;
            SqlParameter p23 = new SqlParameter("@Party_Name", SqlDbType.VarChar);
            p23.Value = _PartyName;
            SqlParameter p24 = new SqlParameter("@Party_Address", SqlDbType.VarChar);
            p24.Value = _PartyAddress;
            SqlParameter p25 = new SqlParameter("@Party_City", SqlDbType.VarChar);
            p25.Value = _PartyCity;
            SqlParameter p26 = new SqlParameter("@Party_Pincode ", SqlDbType.VarChar);
            p26.Value = _partyPincode;
            SqlParameter p27 = new SqlParameter("@Party_Country", SqlDbType.VarChar);
            p27.Value = _partyCountry;
            SqlParameter p28 = new SqlParameter("@Party_Telephone_No ", SqlDbType.VarChar);
            p28.Value = _partyTelephoneNo;
            SqlParameter p29 = new SqlParameter("@Party_Fax_No", SqlDbType.VarChar);
            p29.Value = _Party_Fax_No;
            SqlParameter p30 = new SqlParameter("@Party_Email_Id", SqlDbType.VarChar);
            p30.Value = _partyEmailID;
            SqlParameter p31 = new SqlParameter("@Party_Contact_Person", SqlDbType.VarChar);
            p31.Value = _partyContactPerson;
            SqlParameter p32 = new SqlParameter("@Party_CP_Email_id", SqlDbType.VarChar);
            p32.Value = _Party_CP_Email_id;
            SqlParameter p33 = new SqlParameter("@Party_CP_Mobile_No", SqlDbType.VarChar);
            p33.Value = _Party_CP_Mobile_No;
            SqlParameter p34 = new SqlParameter("@Party_AC_No", SqlDbType.VarChar);
            p34.Value = _Party_AC_No;

            SqlParameter p36 = new SqlParameter("@user", SqlDbType.VarChar);
            p36.Value = _userName;
            SqlParameter p37 = new SqlParameter("@uploadeddate", SqlDbType.VarChar);
            p37.Value = _uploadingDate;
            SqlParameter p38 = new SqlParameter("@Party_LEI_No", SqlDbType.VarChar);
            p38.Value = _Party_LEI_No;
            SqlParameter p39 = new SqlParameter("@Party_LEI_Expiry_Date", SqlDbType.VarChar);
            p39.Value = _Party_LEIExpiry_Date;

            string _query1 = "TF_UpdateConsigneePartyMaster";
            TF_DATA objSave1 = new TF_DATA();
            _result = objSave1.SaveDeleteData(_query1, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p36, p37, p38, p39);
        }
        //-----------------------------------------------End-----------------------------------------------
        string _script = "";
        if (_result == "added")
        {
            _script = "window.location='TF_ViewOverseasPartyMaster.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (_result == "updated")
            {
                _script = "window.location='TF_ViewOverseasPartyMaster.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else 
              // labelMessage.Text = _result; // Anand 20-11-2023
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "err_msg", "alert('" + _result + "');", true); // Anand 20-11-2023
            txtPartyId.Text = ""; //Anand 20-11-2023
          
        }

        //clearall();


    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewOverseasPartyMaster.aspx", true);
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

      

        string _query = "TF_GetOverseasPartyMasterDetails";
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
            //dropDownListType.SelectedIndex = dt.Rows[0]["Party_Type"].ToString().Trim();
            txtLEINo.Text = dt.Rows[0]["Party_LEI_No"].ToString().Trim();
            txtLEIExpiry.Text = dt.Rows[0]["Party_LEI_Expiry_Date"].ToString().Trim();
            
            
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewOverseasPartyMaster.aspx", true);
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
//---------------------------------------------Anand 20-11-2023-----------------------------------
    protected void txtPartyName_TextChanged(object sender, EventArgs e)
    {
        string a = Request.QueryString["mode"].Trim();
        if ( a== "add")
        {
            SqlParameter p1 = new SqlParameter("@PartyName", SqlDbType.VarChar);
            p1.Value = txtPartyName.Text;
            string _query = "TF_OverPartName";
            TF_DATA objData = new TF_DATA();
            DataTable dt = objData.getData(_query, p1);
            if (dt.Rows.Count > 0)
            {
                string PartyName = "";
                PartyName = dt.Rows[0]["PartyName"].ToString().Trim();
                if (PartyName == "Party Name Already exits.")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Party Name already exits, do you wish to continue?')", true);
                }

            }
        }
    }
    //---------------------------------------------End------------------------------------------
}