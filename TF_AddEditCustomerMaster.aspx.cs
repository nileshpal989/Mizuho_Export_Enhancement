using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class TF_AddEditCustomerMaster : System.Web.UI.Page
{
    public static string Cust_name;
    public static string IE_Code = "";
    public static string GL_CODE = "";
    public static string cUST_ADDRESS = "";

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
                    Response.Redirect("TF_ViewCustomerMaster.aspx", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtBranch.Text = Request.QueryString["ddlbranch"].ToString();
                        txtACNo.Enabled = false;
                        txtHF.Enabled = false;
                        txtCustName.Focus();
                        fillDetails(txtBranch.Text, Request.QueryString["customerACNo"].Trim());
                    }
                    else
                    {
                        txtBranch.Text = Request.QueryString["ddlbranch"].ToString();
                        fillBranchcode(txtBranch.Text);
                        txtHF.Focus();
                        txtACNo.Enabled = true;
                        rbtBCCEmail.Checked = true;
                    }
                }                
                btnSave.Attributes.Add("onclick", "return validateSave();");
                txtHF.Attributes.Add("onblur", "toUpper_Case();");

                txtACNo.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtcountry.Attributes.Add("onkeydown", "return CountryId(event)");
                btnCountryList.Attributes.Add("onclick", "return Countryhelp()");

                txtEmailID.Attributes.Add("onchange", "return checkEmail();");
                txtCPMobileNo.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtCustcode.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtIEcode.Attributes.Add("onblur", "return iecodecheck();");
                txtPanNo.Attributes.Add("onkeydown", "return PanCardNo(event);");
            }
        }
    }
    void fillBranchcode(string BranchName)
    {
        SqlParameter p1 = new SqlParameter("@branchname", SqlDbType.VarChar);
        p1.Value = BranchName;
        string _query = "TF_GetBranchCode";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            txtbranchcode.Text = dt.Rows[0][0].ToString();
        }
    }
    protected void fillDetails(string Branch, string _customerACNo)
    {
        SqlParameter p1 = new SqlParameter("@customerACNo", SqlDbType.VarChar);
        p1.Value = _customerACNo;
        SqlParameter p2 = new SqlParameter("@branch", SqlDbType.VarChar);
        p2.Value = Branch;

        string _query = "TF_GetCustomerMasterDetails";
        TF_DATA objData = new TF_DATA();

        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            txtHF.Text = dt.Rows[0]["HF"].ToString();
            txtbranchcode.Text = dt.Rows[0]["branch"].ToString();
            txtACNo.Text = dt.Rows[0]["acccno"].ToString();
            txtCustName.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
            txtGL_Code.Text = dt.Rows[0]["GL_Code"].ToString().Trim();
            txtCust_Abbr.Text = dt.Rows[0]["Cust_Abbr"].ToString().Trim();
            txtCustcode.Text = dt.Rows[0]["AC_Code"].ToString().Trim();
            txtIEcode.Text = dt.Rows[0]["CUST_IE_CODE"].ToString().Trim();
            txtaddress.Text = dt.Rows[0]["CUST_ADDRESS"].ToString().Trim();
            txtcity.Text = dt.Rows[0]["CUST_CITY"].ToString().Trim();
            txtpincode.Text = dt.Rows[0]["CUST_PINCODE"].ToString().Trim();
            txtcountry.Text = dt.Rows[0]["CUST_COUNTRY"].ToString().Trim();
            txtTelephoneNo.Text = dt.Rows[0]["CUST_TELEPHONE_NO"].ToString().Trim();
            txtFaxNo.Text = dt.Rows[0]["CUST_FAX_NO"].ToString().Trim();
            txtEmailID.Text = dt.Rows[0]["CUST_EMAIL_ID"].ToString().Trim();
            txtContactPerson.Text = dt.Rows[0]["CUST_CONTACT_PERSON"].ToString().Trim();
            txtCPMobileNo.Text = dt.Rows[0]["CP_MOBILE_NO"].ToString().Trim();
            txtPanNo.Text = dt.Rows[0]["CUST_PAN_NO"].ToString().Trim();
            txtTanNo.Text = dt.Rows[0]["CUST_TAN_NO"].ToString().Trim();
            txtEmailIdToCCBCC.Text = dt.Rows[0]["EmailIDToCCBCC"].ToString().Trim();
            txtEmailIdExportToCCBCC.Text = dt.Rows[0]["EmailID_IMP"].ToString().Trim();
            // txtExtRating.Text = dt.Rows[0]["EXTN_RATING"].ToString().Trim();

            //Start Added By Ashutosh on 09012019 For Import WareHousing Module
            if (dt.Rows[0]["Date_Check"].ToString().Trim() == "Y")
            {
                Chk_Imp_WH.Checked = true;
                chk();
            }
            else
            {
                Chk_Imp_WH.Checked = false;
            }
            if (dt.Rows[0]["IMP_Check"].ToString().Trim() == "Y")
            {
                Chk_IMP_Auto.Checked = true;
            }
            else
            {
                Chk_IMP_Auto.Checked = false;
            }
            if (dt.Rows[0]["S_Date1"].ToString().Trim() != "")
            {
                ddlDate1.SelectedValue = dt.Rows[0]["S_Date1"].ToString().Trim();
            }
            else
            {
                ddlDate1.SelectedIndex = -1;
            }
            if (dt.Rows[0]["S_Date2"].ToString().Trim() != "")
            {
                ddlDate2.SelectedValue = dt.Rows[0]["S_Date2"].ToString().Trim();
            }
            else
            {
                ddlDate2.SelectedIndex = -1;
            }
            if (dt.Rows[0]["S_Date3"].ToString().Trim() != "")
            {
                ddlDate3.SelectedValue = dt.Rows[0]["S_Date3"].ToString().Trim();
            }
            else
            {
                ddlDate3.SelectedIndex = -1;
            }
            if (dt.Rows[0]["S_Date4"].ToString().Trim() != "")
            {
                ddlDate4.SelectedValue = dt.Rows[0]["S_Date4"].ToString().Trim();
            }
            else
            {
                ddlDate4.SelectedIndex = -1;
            }
            if (dt.Rows[0]["S_Date5"].ToString().Trim() != "")
            {
                ddlDate5.SelectedValue = dt.Rows[0]["S_Date5"].ToString().Trim();
            }
            else
            {
                ddlDate5.SelectedIndex = -1;
            }
            //End Added By Ashutosh on 09012019 For Import WareHousing Module  
            fillCountryDescription();

            ////------------ rem by Vinay on 16/11/2013
            if (dt.Rows[0]["StatusHolder"].ToString().Trim() != "")
            {
                ddlStatusHolder.SelectedValue = dt.Rows[0]["StatusHolder"].ToString().Trim();
            }
            else
            {
                ddlStatusHolder.SelectedIndex = -1;
            }
            string _EmailType = dt.Rows[0]["EmailType"].ToString().Trim();
            if (_EmailType != "")
            {
                if (_EmailType == "BCC")
                {
                    rbtBCCEmail.Checked = true;
                }
                if (_EmailType == "To")
                {
                    rbtToEmail.Checked = true;
                }
                if (_EmailType == "CC")
                {
                    rbtCCEmail.Checked = true;
                }
            }
            else
            {
                rbtBCCEmail.Checked = true;
            }
        }
        ////---------------------------------------//
        //-------------------------- audit trail  -------------------------------//
        hdnCustname.Value = dt.Rows[0]["CUST_NAME"].ToString();
        hdnIE_Code.Value = dt.Rows[0]["CUST_IE_CODE"].ToString();
        hdnGLCode.Value = dt.Rows[0]["GL_Code"].ToString();
        hdnAddress.Value = dt.Rows[0]["CUST_ADDRESS"].ToString();
        hdnexperagency.Value = dt.Rows[0]["StatusHolder"].ToString();
        hdnAbbr.Value = dt.Rows[0]["Cust_Abbr"].ToString();
        hdnCity.Value = dt.Rows[0]["CUST_CITY"].ToString();
        hdnPincode.Value = dt.Rows[0]["CUST_PINCODE"].ToString();
        hdntelphoneno.Value = dt.Rows[0]["CUST_TELEPHONE_NO"].ToString();
        hdnemailid.Value = dt.Rows[0]["CUST_EMAIL_ID"].ToString();
        hdnCustCountry.Value = dt.Rows[0]["CUST_COUNTRY"].ToString().Trim();
        hdndate1.Value = dt.Rows[0]["S_Date1"].ToString();
        hdndate2.Value = dt.Rows[0]["S_Date2"].ToString();
        hdndate3.Value = dt.Rows[0]["S_Date3"].ToString();
        hdndate4.Value = dt.Rows[0]["S_Date4"].ToString();
        hdndate5.Value = dt.Rows[0]["S_Date5"].ToString();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _userName = Session["userName"].ToString();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _mode = Request.QueryString["mode"].Trim();
        string _BranchCode = txtbranchcode.Text.Trim();
        string _ACno = txtHF.Text + txtbranchcode.Text + txtACNo.Text.Trim();
        string _customerName = txtCustName.Text.Trim();
        string _iecode = txtIEcode.Text.Trim();
        string _address = txtaddress.Text.Trim();
        string _city = txtcity.Text.Trim();
        string _pincode = txtpincode.Text.Trim();
        string _country = txtcountry.Text.Trim();
        string _telephoneNo = txtTelephoneNo.Text.Trim();
        string _faxNo = txtFaxNo.Text.Trim();
        string _emailid = txtEmailID.Text.Trim();
        string _contactPerson = txtContactPerson.Text.Trim();
        string _cpEmailid = "";
        string _cpMobileNo = txtCPMobileNo.Text.Trim();
        string _panno = txtPanNo.Text.Trim();
        string _tanno = txtTanNo.Text.Trim();
        //string _extnrating = txtExtRating.Text.Trim();
        string _StatusHolder = ddlStatusHolder.Text;
        string _Date1 = ddlDate1.Text;
        string _Date2 = ddlDate2.Text;
        string _Date3 = ddlDate3.Text;
        string _Date4 = ddlDate4.Text;
        string _Date5 = ddlDate5.Text;

        string _check=" ";
        string _IMP_Check = "";
        if (Chk_Imp_WH.Checked)
        {
            _check = "Y";

        }
        else
        {
            _check = "N";
        }
        if (Chk_IMP_Auto.Checked)
        {
            _IMP_Check = "Y";

        }
        else
        {
            _IMP_Check = "N";
        }

        string _EmailType = "";
        if (rbtBCCEmail.Checked)
        {
            _EmailType = "BCC";
        }
        if (rbtCCEmail.Checked)
        {
            _EmailType = "CC";
        }
        if (rbtToEmail.Checked)
        {
            _EmailType = "To";
        }

        SqlParameter p1 = new SqlParameter("@mode", SqlDbType.VarChar);
        p1.Value = _mode;
        SqlParameter p2 = new SqlParameter("@BrachCode", SqlDbType.VarChar);
        p2.Value = _BranchCode;
        SqlParameter p3 = new SqlParameter("@CUST_ACNO", SqlDbType.VarChar);
        p3.Value = _ACno;
        SqlParameter p4 = new SqlParameter("@CUST_NAME", SqlDbType.VarChar);
        p4.Value = _customerName;
        SqlParameter p5 = new SqlParameter("@CUST_IECODE", SqlDbType.VarChar);
        p5.Value = _iecode;
        SqlParameter p6 = new SqlParameter("@CUST_ADDRESS", SqlDbType.VarChar);
        p6.Value = _address;
        SqlParameter p7 = new SqlParameter("@CUST_CITY", SqlDbType.VarChar);
        p7.Value = _city;
        SqlParameter p8 = new SqlParameter("@CUST_PINCODE", SqlDbType.VarChar);
        p8.Value = _pincode;
        SqlParameter p9 = new SqlParameter("@CUST_COUNTRY", SqlDbType.VarChar);
        p9.Value = _country;
        SqlParameter p10 = new SqlParameter("@CUST_TELEPHONENO", SqlDbType.VarChar);
        p10.Value = _telephoneNo;
        SqlParameter p11 = new SqlParameter("@CUST_FAXNO", SqlDbType.VarChar);
        p11.Value = _faxNo;
        SqlParameter p12 = new SqlParameter("@CUST_EMAIL_ID", SqlDbType.VarChar);
        p12.Value = _emailid;
        SqlParameter PEMailIDToCCBCC = new SqlParameter("@EMailIDToCCBCC", SqlDbType.VarChar);
        PEMailIDToCCBCC.Value = txtEmailIdToCCBCC.Text;
        SqlParameter PEMailIDExport = new SqlParameter("@EMailIDExport", SqlDbType.VarChar);
        PEMailIDExport.Value = txtEmailIdExportToCCBCC.Text;
        SqlParameter PEMailType = new SqlParameter("@EmailType", SqlDbType.VarChar);
        PEMailType.Value = _EmailType;
        SqlParameter p13 = new SqlParameter("@CUST_CONTACT_PERSON", SqlDbType.VarChar);
        p13.Value = _contactPerson;
        SqlParameter p14 = new SqlParameter("@CP_MOBILE_NO", SqlDbType.VarChar);
        p14.Value = _cpMobileNo;
        SqlParameter p15 = new SqlParameter("@CP_EMAIL_ID", SqlDbType.VarChar);
        p15.Value = _cpEmailid;
        SqlParameter p16 = new SqlParameter("@CUST_PANNO", SqlDbType.VarChar);
        p16.Value = _panno;
        SqlParameter p17 = new SqlParameter("@CUST_TANNO", SqlDbType.VarChar);
        p17.Value = _tanno;
        //SqlParameter p18 = new SqlParameter("@EXTN_RATING", SqlDbType.VarChar);
        //p18.Value = _extnrating;
        SqlParameter p19 = new SqlParameter("@StatusHolder", SqlDbType.VarChar);
        p19.Value = _StatusHolder;
        SqlParameter p20 = new SqlParameter("@USER", SqlDbType.VarChar);
        p20.Value = _userName;
        SqlParameter p21 = new SqlParameter("@UPLOADINGDATE", SqlDbType.VarChar);
        p21.Value = _uploadingDate;
        SqlParameter p22 = new SqlParameter("@GL_Code", txtGL_Code.Text.ToString());
        SqlParameter p23 = new SqlParameter("@Cust_Abbr", txtCust_Abbr.Text.ToString());
        SqlParameter p24 = new SqlParameter("@Ac_Code", txtCustcode.Text.ToString());
        SqlParameter p25 = new SqlParameter("@S_Date1", _Date1);
        SqlParameter p26 = new SqlParameter("@S_Date2", _Date2);
        SqlParameter p27 = new SqlParameter("@S_Date3", _Date3);
        SqlParameter p28 = new SqlParameter("@S_Date4", _Date4);
         SqlParameter p29 = new SqlParameter("@Check", _check);
         SqlParameter p30 = new SqlParameter("@IMP_Check", _IMP_Check);
         SqlParameter p31 = new SqlParameter("@S_Date5", _Date5);

        string _query = "TF_UpdateCustomerMaster";
        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p19, p20, p21, p22, p23, p24, p25, p26, p27, PEMailIDToCCBCC, PEMailIDExport, PEMailType, p28, p29, p30,p31);

        SqlParameter PMandad = new SqlParameter("@PMandad", SqlDbType.VarChar);
        if (Chk_Imp_WH.Checked)
        {
            PMandad.Value = "A";
            string _ResultMamdad = objSave.SaveDeleteData("TF_IMPWH_MandadAdd", p3, PMandad, p20);
        }
        else
        {
            PMandad.Value = "D";
            string _ResultMamdad = objSave.SaveDeleteData("TF_IMPWH_MandadAdd", p3, PMandad, p20);
        }

        string _script = "";
        string _OldValues = "";
        string _NewValues = "";

        if (_result == "added")
        {
            if (Chk_IMP_Auto.Checked == true)
            {
                AddModeAuditIMP(_query, _NewValues, _OldValues, _userName, _script, _result);
            }
            else
            {
                AddModeAuditIMPWH(_query, _NewValues, _OldValues, _userName, _script, _result);
            }
        }
        else
        {
            if (_result == "updated")
            {
                if (Chk_IMP_Auto.Checked == true)
                {
                    UpdateModeAuditIMP(_query, _NewValues, _OldValues, _userName, _script, _result);
                }
                else
                {
                    UpdateModeAuditIMPWH(_query, _NewValues, _OldValues, _userName, _script, _result);
                }
            }
            else
                labelMessage.Text = _result;
        }

        clearall();
    }
    public void clearall()
    {
        txtACNo.Text = "";

        txtHF.Text = "";
        txtbranchcode.Text = "";
        txtCustName.Text = "";
        txtGL_Code.Text = "";
        txtCust_Abbr.Text = "";
        txtCustcode.Text = "";
        txtIEcode.Text = "";
        txtaddress.Text = "";
        txtcity.Text = "";
        txtpincode.Text = "";
        txtcountry.Text = "";
        txtTelephoneNo.Text = "";
        txtFaxNo.Text = "";
        txtEmailID.Text = "";
        txtEmailIdToCCBCC.Text = "";
        txtEmailIdExportToCCBCC.Text = "";
        txtContactPerson.Text = "";
        txtCPMobileNo.Text = "";
        txtPanNo.Text = "";
        txtTanNo.Text = "";
        
       // txtExtRating.Text = "";
        txtcountry.Text = "IN";
        lblCountryName.Text = "INDIA";
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewCustomerMaster.aspx", true);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewCustomerMaster.aspx", true);
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
    protected void txtACNo_TextChanged(object sender, EventArgs e)
    {
        string _UniqueCode = txtACNo.Text.Trim();
        SqlParameter p1 = new SqlParameter("@code", SqlDbType.VarChar);
        p1.Value = _UniqueCode;
        string _query = "TF_GetUniqueCode";
        TF_DATA objData = new TF_DATA();

        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            string Code = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString();
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('This Code is Alreday assign.')", true);
            txtACNo.Text = "";
            txtACNo.Focus();
        }
        else
        {
            txtCustName.Focus();

        }
    }    
    protected void Chk_Imp_WH_CheckedChanged(object sender, EventArgs e)
    {
        if (Chk_Imp_WH.Checked)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "message", "datecheck('Checked');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "message", "datecheck('UnChecked');", true);
        }
    }
    protected void chk()
    {
        if (Chk_Imp_WH.Checked)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "message", "datecheck('Checked');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "message", "datecheck('UnChecked');", true);
        }
    }
    protected void txtCust_Abbr_TextChanged(object sender, EventArgs e)
    {

    }
    public void AddModeAuditIMPWH(string _query, string _NewValues, string _OldValues, string _userName, string _script,string _result)
    {
        TF_DATA objSave = new TF_DATA();
        _query = "TF_IMPWH_AuditTrail";
        _NewValues = "Customer Name: " + txtCustName.Text.Trim() + ";Cust Acc No: " + txtHF.Text + txtbranchcode.Text + txtACNo.Text.Trim() + ";IECode: " + txtIEcode.Text.Trim() + ";GL Code:" + txtGL_Code.Text.Trim() + ";Cust Abrrevation:" + txtCust_Abbr.Text.Trim() + ";Address:" + txtaddress.Text.Trim();

        SqlParameter Branch = new SqlParameter("@BranchCode", SqlDbType.VarChar);
        Branch.Value = txtBranch.Text.Trim();
        SqlParameter Mod = new SqlParameter("@ModType", SqlDbType.VarChar);
            Mod.Value = "IMPWH";
        SqlParameter oldvalues = new SqlParameter("@OldValues", SqlDbType.VarChar);
        oldvalues.Value = _OldValues;
        SqlParameter newvalues = new SqlParameter("@NewValues", SqlDbType.VarChar);
        newvalues.Value = _NewValues;
        SqlParameter Acno = new SqlParameter("@CustAcNo", SqlDbType.VarChar);
        Acno.Value = txtHF.Text + txtbranchcode.Text + txtACNo.Text.Trim();
        SqlParameter DocumentNo = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
        DocumentNo.Value = txtIEcode.Text.Trim();
        SqlParameter FWDContractNo = new SqlParameter("@FWD_Contract_No", SqlDbType.VarChar);
        FWDContractNo.Value = "";
        SqlParameter DocumnetDate = new SqlParameter("@DocumentDate", SqlDbType.VarChar);
        DocumnetDate.Value = "";
        SqlParameter Mode = new SqlParameter("@Mode", "A");
        SqlParameter user = new SqlParameter("@ModifiedBy", SqlDbType.VarChar);
        user.Value = _userName;
        string _moddate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        SqlParameter moddate = new SqlParameter("@ModifiedDate", SqlDbType.VarChar);
        moddate.Value = _moddate;
        //string _type = "A";
        //SqlParameter type = new SqlParameter("@type", SqlDbType.VarChar);
        //type.Value = _type;
        string _menu = "Customer Master";
        SqlParameter menu = new SqlParameter("@MenuName", SqlDbType.VarChar);
        menu.Value = _menu;
        string at = objSave.SaveDeleteData(_query, Branch, Mod, oldvalues, newvalues, Acno, DocumentNo, FWDContractNo, DocumnetDate, Mode, user, moddate, menu);

        _script = "window.location='TF_ViewCustomerMaster.aspx?result=" + _result + "'";
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
    }
    public void AddModeAuditIMP(string _query, string _NewValues, string _OldValues, string _userName, string _script, string _result)
    {
        TF_DATA objSave = new TF_DATA();
        _query = "TF_IMPWH_AuditTrail";
        _NewValues = "Customer Name: " + txtCustName.Text.Trim() + ";IECode: " + txtIEcode.Text.Trim() + ";GL Code:" + txtGL_Code.Text.Trim() + ";Country :" + txtcountry.Text.Trim() + ";EmailID :" +txtEmailID.Text.Trim();

        SqlParameter Branch = new SqlParameter("@BranchCode", SqlDbType.VarChar);
        Branch.Value = txtBranch.Text.Trim();
        SqlParameter Mod = new SqlParameter("@ModType", SqlDbType.VarChar);
        Mod.Value = "IMP";
        SqlParameter oldvalues = new SqlParameter("@OldValues", SqlDbType.VarChar);
        oldvalues.Value = _OldValues;
        SqlParameter newvalues = new SqlParameter("@NewValues", SqlDbType.VarChar);
        newvalues.Value = _NewValues;
        SqlParameter Acno = new SqlParameter("@CustAcNo", SqlDbType.VarChar);
        Acno.Value = txtHF.Text + txtbranchcode.Text + txtACNo.Text.Trim();
        SqlParameter DocumentNo = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
        DocumentNo.Value = txtHF.Text + txtbranchcode.Text + txtACNo.Text.Trim();
        SqlParameter FWDContractNo = new SqlParameter("@FWD_Contract_No", SqlDbType.VarChar);
        FWDContractNo.Value = "";
        SqlParameter DocumnetDate = new SqlParameter("@DocumentDate", SqlDbType.VarChar);
        DocumnetDate.Value = "";
        SqlParameter Mode = new SqlParameter("@Mode", "A");
        SqlParameter user = new SqlParameter("@ModifiedBy", SqlDbType.VarChar);
        user.Value = _userName;
        string _moddate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        SqlParameter moddate = new SqlParameter("@ModifiedDate", SqlDbType.VarChar);
        moddate.Value = _moddate;
        //string _type = "A";
        //SqlParameter type = new SqlParameter("@type", SqlDbType.VarChar);
        //type.Value = _type;
        string _menu = "Customer Master";
        SqlParameter menu = new SqlParameter("@MenuName", SqlDbType.VarChar);
        menu.Value = _menu;
        string at = objSave.SaveDeleteData(_query, Branch, Mod, oldvalues, newvalues, Acno, DocumentNo, FWDContractNo, DocumnetDate, Mode, user, moddate, menu);

        _script = "window.location='TF_ViewCustomerMaster.aspx?result=" + _result + "'";
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
    }
    public void UpdateModeAuditIMPWH(string _query, string _NewValues, string _OldValues, string _userName, string _script, string _result)
    {
        TF_DATA objSave = new TF_DATA();
        int isneedtolog = 0;
        string NewValues = "";
        //SqlParameter Old_Value = new SqlParameter("@OldValues", SqlDbType.VarChar);
        _OldValues = "Customer Name :" + hdnCustname.Value.ToString() + ";IECode  : " + hdnIE_Code.Value.ToString() + ";GLCode: " + hdnGLCode.Value.ToString() +
 ";Export Agency : " + hdnexperagency.Value.ToString() + ";Date1 : " + hdndate1.Value.ToString() + ";Date2 : " + hdndate2.Value.ToString() + ";Date3 : " + hdndate3.Value.ToString() + ";Date4 : " + hdndate4.Value.ToString() + "Date5 : " + hdndate5.Value.ToString() + ";City : " + hdnCity.Value.ToString() + ";Pincode : " + hdnPincode.Value.ToString() + ";Telphone No : " + hdntelphoneno.Value.ToString() + ";Email Id : " + hdnemailid.Value.ToString() + "; Address :" + hdnAddress.Value.ToString() + "";

        if (hdnCustname.Value != txtCustName.Text.Trim())
        {
            isneedtolog = 1;
            if (_NewValues == "")
            {
                _NewValues = _NewValues + "Customer Name : " + txtCustName.Text.Trim();
            }
            else
            {
                _NewValues = _NewValues + "; Customer Name : " + txtCustName.Text.Trim();
            }

        }


        if (hdnIE_Code.Value != txtIEcode.Text.Trim())
        {
            isneedtolog = 1;
            if (_NewValues == "")
            {
                _NewValues = _NewValues + "IECode : " + txtIEcode.Text.Trim();
            }
            else
            {
                _NewValues = _NewValues + "; IECode : " + txtIEcode.Text.Trim();
            }

        }

        if (hdnGLCode.Value != txtGL_Code.Text.Trim())
        {
            isneedtolog = 1;
            if (_NewValues == "")
            {
                _NewValues = _NewValues + "GLCode : " + txtGL_Code.Text.Trim();
            }
            else
            {
                _NewValues = _NewValues + "; GLCode : " + txtGL_Code.Text.Trim();
            }

        }

        if (hdnAddress.Value != txtaddress.Text.Trim())
        {
            isneedtolog = 1;
            if (_NewValues == "")
            {
                _NewValues = _NewValues + "Address : " + txtaddress.Text.Trim();
            }
            else
            {
                _NewValues = _NewValues + "; Address : " + txtaddress.Text.Trim();
            }

        }
        if (hdnexperagency.Value != ddlStatusHolder.SelectedItem.Text)
        {
            isneedtolog = 1;
            if (_NewValues == "")
            {
                _NewValues = _NewValues + "Port Code : " + ddlStatusHolder.SelectedItem.Text;
            }
            else
            {
                _NewValues = _NewValues + "; Port Code : " + ddlStatusHolder.SelectedItem.Text;
            }

        }
        if (hdnAbbr.Value != txtCust_Abbr.Text.Trim())
        {
            isneedtolog = 1;
            if (_NewValues == "")
            {
                _NewValues = _NewValues + "Abrrevation : " + txtCust_Abbr.Text.Trim();
            }
            else
            {
                _NewValues = _NewValues + "; Abrrevation : " + txtCust_Abbr.Text.Trim();
            }

        }
        if (hdnCity.Value != txtcity.Text.Trim())
        {
            isneedtolog = 1;
            if (_NewValues == "")
            {
                _NewValues = _NewValues + "City : " + txtcity.Text.Trim();
            }
            else
            {
                _NewValues = _NewValues + "; City : " + txtcity.Text.Trim();
            }

        }
        if (hdnPincode.Value != txtpincode.Text.Trim())
        {
            isneedtolog = 1;
            if (_NewValues == "")
            {
                _NewValues = _NewValues + "Pincode : " + txtpincode.Text.Trim();
            }
            else
            {
                _NewValues = _NewValues + "; Pincode : " + txtpincode.Text.Trim();
            }

        }
        if (hdntelphoneno.Value != txtTelephoneNo.Text.Trim())
        {
            isneedtolog = 1;
            if (_NewValues == "")
            {
                _NewValues = _NewValues + "Telephone No : " + txtTelephoneNo.Text.Trim();
            }
            else
            {
                _NewValues = _NewValues + "; Telephone No : " + txtTelephoneNo.Text.Trim();
            }

        }
        if (hdnemailid.Value != txtEmailID.Text.Trim())
        {
            isneedtolog = 1;
            if (_NewValues == "")
            {
                _NewValues = _NewValues + "Email Id : " + txtEmailID.Text.Trim();
            }
            else
            {
                _NewValues = _NewValues + "; Email Id : " + txtEmailID.Text.Trim();
            }

        }
        if (hdndate1.Value != ddlDate1.SelectedValue.ToString())
        {
            isneedtolog = 1;
            if (_NewValues == "")
            {
                _NewValues = _NewValues + "Date1 : " + ddlDate1.SelectedItem.Text;
            }
            else
            {
                _NewValues = _NewValues + "; Date1 : " + ddlDate1.SelectedItem.Text;
            }

        }
        if (hdndate2.Value != ddlDate2.SelectedValue.ToString())
        {
            isneedtolog = 1;
            if (_NewValues == "")
            {
                _NewValues = _NewValues + "Date2 : " + ddlDate2.SelectedItem.Text;
            }
            else
            {
                _NewValues = _NewValues + "; Date2 : " + ddlDate2.SelectedItem.Text;
            }

        }
        if (hdndate3.Value != ddlDate3.SelectedValue.ToString())
        {
            isneedtolog = 1;
            if (_NewValues == "")
            {
                _NewValues = _NewValues + "Date3 : " + ddlDate3.SelectedItem.Text;
            }
            else
            {
                _NewValues = _NewValues + "; Date3 : " + ddlDate3.SelectedItem.Text;
            }

        }
        if (hdndate4.Value != ddlDate4.SelectedValue.ToString())
        {
            isneedtolog = 1;
            if (_NewValues == "")
            {
                _NewValues = _NewValues + "Date4 : " + ddlDate4.SelectedItem.Text;
            }
            else
            {
                _NewValues = _NewValues + "; Date4 : " + ddlDate4.SelectedItem.Text;
            }

        }
        if (hdndate5.Value != ddlDate5.SelectedValue.ToString())
        {
            isneedtolog = 1;
            if (_NewValues == "")
            {
                _NewValues = _NewValues + "Date5 : " + ddlDate5.SelectedItem.Text;
            }
            else
            {
                _NewValues = _NewValues + "; Date5 : " + ddlDate5.SelectedItem.Text;
            }

        }


        _query = "TF_IMPWH_AuditTrail";
        SqlParameter Branch = new SqlParameter("@BranchCode", SqlDbType.VarChar);
        Branch.Value = txtBranch.Text.Trim();
        SqlParameter Mod = new SqlParameter("@ModType", SqlDbType.VarChar);
            Mod.Value = "IMPWH";
        SqlParameter oldvalues = new SqlParameter("@OldValues", SqlDbType.VarChar);
        oldvalues.Value = _OldValues;
        SqlParameter newvalues = new SqlParameter("@NewValues", SqlDbType.VarChar);
        newvalues.Value = _NewValues;
        SqlParameter Acno = new SqlParameter("@CustAcNo", SqlDbType.VarChar);
        Acno.Value = txtHF.Text + txtbranchcode.Text + txtACNo.Text.Trim();
        SqlParameter DocumentNo = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
        DocumentNo.Value = "";
        SqlParameter FWDContractNo = new SqlParameter("@FWD_Contract_No", SqlDbType.VarChar);
        FWDContractNo.Value = "";
        SqlParameter DocumnetDate = new SqlParameter("@DocumentDate", SqlDbType.VarChar);
        DocumnetDate.Value = "";
        SqlParameter Mode = new SqlParameter("@Mode", "M");
        SqlParameter user = new SqlParameter("@ModifiedBy", SqlDbType.VarChar);
        user.Value = _userName;
        string _moddate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        SqlParameter moddate = new SqlParameter("@ModifiedDate", SqlDbType.VarChar);
        moddate.Value = _moddate;
        //string _type = "A";
        //SqlParameter type = new SqlParameter("@type", SqlDbType.VarChar);
        //type.Value = _type;
        string _menu = "Customer Master";
        SqlParameter menu = new SqlParameter("@MenuName", SqlDbType.VarChar);
        menu.Value = _menu;
        if (isneedtolog == 1)
        {
            string at = objSave.SaveDeleteData(_query, Branch, Mod, oldvalues, newvalues, Acno, DocumentNo, FWDContractNo, DocumnetDate, Mode, user, moddate, menu);
        }

        _script = "window.location='TF_ViewCustomerMaster.aspx?result=" + _result + "'";
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
    
    }
    public void UpdateModeAuditIMP(string _query, string _NewValues, string _OldValues, string _userName, string _script, string _result)
    {
        TF_DATA objSave = new TF_DATA();
        int isneedtolog = 0;
        string NewValues = "";
        //SqlParameter Old_Value = new SqlParameter("@OldValues", SqlDbType.VarChar);
        _OldValues = "Customer Name :" + hdnCustname.Value.ToString() + " ; IECode  : " + hdnIE_Code.Value.ToString() + " ; GLCode: " + hdnGLCode.Value.ToString() + " ; Country :" + hdnCustCountry.Value.ToString() + " ; Email Id : " + hdnemailid.Value.ToString();

        if (hdnCustname.Value != txtCustName.Text.Trim())
        {
            isneedtolog = 1;
            if (_NewValues == "")
            {
                _NewValues = _NewValues + "Customer Name : " + txtCustName.Text.Trim();
            }
            else
            {
                _NewValues = _NewValues + "; Customer Name : " + txtCustName.Text.Trim();
            }

        }


        if (hdnIE_Code.Value != txtIEcode.Text.Trim())
        {
            isneedtolog = 1;
            if (_NewValues == "")
            {
                _NewValues = _NewValues + " ; IECode : " + txtIEcode.Text.Trim();
            }
            else
            {
                _NewValues = _NewValues + "; IECode : " + txtIEcode.Text.Trim();
            }

        }

        if (hdnGLCode.Value != txtGL_Code.Text.Trim())
        {
            isneedtolog = 1;
            if (_NewValues == "")
            {
                _NewValues = _NewValues + " ; GLCode : " + txtGL_Code.Text.Trim();
            }
            else
            {
                _NewValues = _NewValues + "; GLCode : " + txtGL_Code.Text.Trim();
            }

        }


        if (hdnCustCountry.Value != txtcountry.Text.Trim())
        {
            isneedtolog = 1;
            if (_NewValues == "")
            {
                _NewValues = _NewValues + " ; Country : " + txtcountry.Text.Trim();
            }
            else
            {
                _NewValues = _NewValues + " ; Country : " + txtcountry.Text.Trim();
            }

        }
       
       
        if (hdnemailid.Value != txtEmailID.Text.Trim())
        {
            isneedtolog = 1;
            if (_NewValues == "")
            {
                _NewValues = _NewValues + " ; Email Id : " + txtEmailID.Text.Trim();
            }
            else
            {
                _NewValues = _NewValues + "; Email Id : " + txtEmailID.Text.Trim();
            }

        }

        _query = "TF_IMPWH_AuditTrail";
        SqlParameter Branch = new SqlParameter("@BranchCode", SqlDbType.VarChar);
        Branch.Value = txtBranch.Text.Trim();
        SqlParameter Mod = new SqlParameter("@ModType", SqlDbType.VarChar);
        Mod.Value = "IMP";
        SqlParameter oldvalues = new SqlParameter("@OldValues", SqlDbType.VarChar);
        oldvalues.Value = _OldValues;
        SqlParameter newvalues = new SqlParameter("@NewValues", SqlDbType.VarChar);
        newvalues.Value = _NewValues;
        SqlParameter Acno = new SqlParameter("@CustAcNo", SqlDbType.VarChar);
        Acno.Value = txtHF.Text + txtbranchcode.Text + txtACNo.Text.Trim();
        SqlParameter DocumentNo = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
        DocumentNo.Value = txtHF.Text + txtbranchcode.Text + txtACNo.Text.Trim();
        SqlParameter FWDContractNo = new SqlParameter("@FWD_Contract_No", SqlDbType.VarChar);
        FWDContractNo.Value = "";
        SqlParameter DocumnetDate = new SqlParameter("@DocumentDate", SqlDbType.VarChar);
        DocumnetDate.Value = "";
        SqlParameter Mode = new SqlParameter("@Mode", "M");
        SqlParameter user = new SqlParameter("@ModifiedBy", SqlDbType.VarChar);
        user.Value = _userName;
        string _moddate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        SqlParameter moddate = new SqlParameter("@ModifiedDate", SqlDbType.VarChar);
        moddate.Value = _moddate;
        //string _type = "A";
        //SqlParameter type = new SqlParameter("@type", SqlDbType.VarChar);
        //type.Value = _type;
        string _menu = "Customer Master";
        SqlParameter menu = new SqlParameter("@MenuName", SqlDbType.VarChar);
        menu.Value = _menu;
        if (isneedtolog == 1)
        {
            string at = objSave.SaveDeleteData(_query, Branch, Mod, oldvalues, newvalues, Acno, DocumentNo, FWDContractNo, DocumnetDate, Mode, user, moddate, menu);
        }

        _script = "window.location='TF_ViewCustomerMaster.aspx?result=" + _result + "'";
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
    
    
    }
}