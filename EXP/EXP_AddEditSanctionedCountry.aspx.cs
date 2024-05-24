using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EXP_EXP_AddEditSanctionedCountry : System.Web.UI.Page
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
               
                txtCountryID.Focus();
               
                btnSave.Attributes.Add("onclick", "return validateSave();");
                btnCountryList.Attributes.Add("onclick", "return OpenCountryCodeList('mouseClick');");

                txtCountryID.Attributes.Add("onkeypress", "javascript:return onlyChars(event)");
                txtCountryName.Attributes.Add("onkeypress", "javascript:return onlyChars(event)");
               
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("EXP_ViewSanctionedCountry.aspx", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtCountryID.Text = Request.QueryString["countryid"].Trim();
                        txtCountryID.Enabled = false;
                        btnCountryList.Enabled = false;
                        filldetails("child");
                    }
                }
               
            }
        }
    }

    protected void clearControls()
    {
        txtCountryID.Text = "";
        txtCountryName.Text = "";
    }

    protected void btnCountry_Click(object sender, EventArgs e)
    {
        if (hdnCountry.Value != "")
        {
                txtCountryID.Text = hdnCountry.Value;
                filldetails("parent");
                btnSave.Focus();             
        }
    }
    protected void filldetails(string _table)
    {
        string _countryID = txtCountryID.Text.Trim().ToUpper();
        txtCountryID.Text = txtCountryID.Text.Trim().ToUpper();
        string query = "";
        SqlParameter p1 = new SqlParameter("@cid", SqlDbType.VarChar);
        p1.Value = _countryID;
            
            if (_table=="parent")
                query = "TF_GetCountryDetails";
            else
                query = "TF_GetCountryDetails_Sanctioned";

        TF_DATA objdata = new TF_DATA();
        DataTable dt = objdata.getData(query, p1);
        if (dt.Rows.Count > 0)
        {
            txtCountryName.Text = dt.Rows[0]["CountryName"].ToString();

            //-------------------------- audit trail  -------------------------------//

            hdnsancountryname.Value = dt.Rows[0]["CountryName"].ToString();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EXP_ViewSanctionedCountry.aspx", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string query = "TF_UpdateCountryMaster_Sanctioned";
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy");
        string _mode = Request.QueryString["mode"].Trim();
        string _cid = txtCountryID.Text.Trim().ToUpper();
        string _cname = txtCountryName.Text.Trim().ToUpper();

        SqlParameter cid = new SqlParameter("@cid", SqlDbType.VarChar);
        SqlParameter cname = new SqlParameter("@cname", SqlDbType.VarChar);
        SqlParameter mode = new SqlParameter("@cmode", SqlDbType.VarChar);
        SqlParameter user = new SqlParameter("@user", SqlDbType.VarChar);
        SqlParameter date = new SqlParameter("@date", SqlDbType.VarChar);

        cid.Value = _cid;
        cname.Value = _cname;
        mode.Value = _mode;
        user.Value = _userName;
        date.Value = _uploadingDate;

        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData(query, cid, cname, mode, user, date);
        string _script = "";

        ///////////Audit Trail////////////////////////////////
        SqlParameter A1 = new SqlParameter("@OldValues", SqlDbType.VarChar);
        SqlParameter A2 = new SqlParameter("@NewValues", SqlDbType.VarChar);
        SqlParameter A3 = new SqlParameter("@ModType", SqlDbType.VarChar);
        SqlParameter A4 = new SqlParameter("@ModifiedBy", Session["userName"].ToString().Trim());
        SqlParameter A5 = new SqlParameter("@ModifiedDate", "");
        SqlParameter A6 = new SqlParameter("@MenuName", "Sanctioned Country Master");
        SqlParameter A7 = new SqlParameter("@Mode", SqlDbType.VarChar);
        SqlParameter A8 = new SqlParameter("@RefNo", SqlDbType.VarChar);


        if (_result == "added")
        {
            _NewValue = "Description : " + txtCountryName.Text.Trim()+" ; Country ID : "+txtCountryID.Text.Trim();
            A1.Value = "";
            A2.Value = _NewValue;
            A3.Value = "IMP";
            A7.Value = "A";
            A8.Value = _cid;
            string p = objSave.SaveDeleteData("TF_Mizuho_IMP_AuditTrail_master", A1, A2, A3, A4, A5, A6, A7, A8);
            _script = "window.location='EXP_ViewSanctionedCountry.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", _script, true);
        }

        else if (_result == "updated")
        {

             int isneedtolog = 0;
             if (_result == "updated")
             {

                 _OldValues = "Description : " + hdnsancountryname.Value.Trim();
                 if (hdnsancountryname.Value != txtCountryName.Text.Trim())
                 {
                     isneedtolog = 1;
                     if (_NewValue == "")
                     {
                         _NewValue = "Description : " + txtCountryName.Text.Trim();
                     }
                     else
                     {
                         _NewValue = "Description : " + txtCountryName.Text.Trim();
                     }
                 }

                 A1.Value = _OldValues;
                 A2.Value = _NewValue;
                 A3.Value = "IMP";
                 A7.Value = "M";
                 A8.Value = _cid;

                 if (isneedtolog == 1)
                 {
                     string p = objSave.SaveDeleteData("TF_Mizuho_IMP_AuditTrail_master", A1, A2, A3, A4, A5, A6, A7, A8);
                 }

                 _script = "window.location='EXP_ViewSanctionedCountry.aspx?result=" + _result + "'";
                 ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", _script, true);
             }
             else
             {
                 //labelMessage.Text = _result;
                 ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "VAlert('" + _result + "','#txtCountryID');", true);
             }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("EXP_ViewSanctionedCountry.aspx", true);
    }
    protected void txtCountryID_TextChanged(object sender, EventArgs e)
    {
        filldetails("parent");
    }
}