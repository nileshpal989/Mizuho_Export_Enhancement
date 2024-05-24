using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class TF_AddEditServiceTaxMaster : System.Web.UI.Page
{
    decimal totaltax;
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
                clearControls();
                //txtSrNo.Focus();
                //txtCountryID.Text = Request.QueryString["countryid"].ToString();

                btnSave.Attributes.Add("onclick", "return validateSave();");
                //txtSrNo.Attributes.Add("onkeypress", "javascript:return onlyNumber(event)");
                txtServiceTax.Attributes.Add("onkeypress", "javascript:return onlyNumber(event)");
                txtEduCessTax.Attributes.Add("onkeypress", "javascript:return onlyNumber(event)");
                txtSecEduCessTax.Attributes.Add("onkeypress", "javascript:return onlyNumber(event)");

                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("TF_ViewServiceTaxMaster.aspx", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtEffectiveDate.Text = Request.QueryString["date"].Trim();
                        txtEffectiveDate.Enabled = false;
                    }
                }
                filldetails();
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewServiceTaxMaster.aspx", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string query = "TF_UpdateServiceTaxMaster";
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy");
        string _mode = Request.QueryString["mode"].Trim();
        //string _SrNo = txtSrNo.Text.Trim();
        string _eDate = txtEffectiveDate.Text.Trim();
        string _stax = txtServiceTax.Text.Trim();
        string _edutax = txtEduCessTax.Text.Trim();
        string _secedutax = txtSecEduCessTax.Text.Trim();
        decimal stax = Convert.ToDecimal(_stax);
        decimal edutax = Convert.ToDecimal(_edutax);
        decimal secedutax = Convert.ToDecimal(_secedutax);

        //decimal tax = stax + ((stax * edutax) / 100);

        //totaltax = tax + ((tax * secedutax) / 100);

        string _totaltax = txtTotalTax.Text;

        //SqlParameter srno = new SqlParameter("@SrNo", SqlDbType.VarChar);

        SqlParameter edate = new SqlParameter("@eDate", SqlDbType.VarChar);
        SqlParameter svtax = new SqlParameter("@Stax", SqlDbType.VarChar);
        SqlParameter sbcess = new SqlParameter("@sbcess", SqlDbType.VarChar);
        SqlParameter kkcess = new SqlParameter("@kkcess", SqlDbType.VarChar);
        SqlParameter etax = new SqlParameter("@eduTax", SqlDbType.VarChar);
        SqlParameter secetax = new SqlParameter("@SecEduTax", SqlDbType.VarChar);
        SqlParameter user = new SqlParameter("@user", SqlDbType.VarChar);
        SqlParameter date = new SqlParameter("@date", SqlDbType.VarChar);
        SqlParameter mode = new SqlParameter("@mode", SqlDbType.VarChar);
        SqlParameter ttax = new SqlParameter("@ttax", SqlDbType.VarChar);

        //srno.Value = _SrNo;
        mode.Value = _mode;
        edate.Value = _eDate;
        svtax.Value = stax;
        sbcess.Value = txt_sbcess.Text;
        kkcess.Value = txt_kkcess.Text;
        etax.Value = edutax;
        secetax.Value = secedutax;
        ttax.Value = Convert.ToDecimal(_totaltax);
        user.Value = _userName;
        date.Value = _uploadingDate;

        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData(query,mode,edate,svtax,sbcess,kkcess,etax,secetax,ttax, user, date);
        string _script = "";
        if (_result == "added")
        {
            _script = "window.location='TF_ViewServiceTaxMaster.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", _script, true);
        }
        else if (_result == "updated")
        {
            _script = "window.location='TF_ViewServiceTaxMaster.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", _script, true);
        }
        else
        {
            labelMessage.Text = _result;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewServiceTaxMaster.aspx", true);
    }
    protected void filldetails()
    {
        //string _srno = txtSrNo.Text.Trim();
        string _effectiveDate = txtEffectiveDate.Text.Trim();
        SqlParameter p1 = new SqlParameter("@EffectiveDate", SqlDbType.VarChar);
       p1.Value = _effectiveDate;
        string query = "TF_GetServiceTaxDetails";
        TF_DATA objdata = new TF_DATA();
        DataTable dt = objdata.getData(query, p1);
        if (dt.Rows.Count > 0)
        {
            //txtEffectiveDate.Text = dt.Rows[0]["EFFECTIVE_DATE"].ToString();
            txtServiceTax.Text = dt.Rows[0]["SERVICE_TAX"].ToString();
            txt_sbcess.Text = dt.Rows[0]["SBCess"].ToString();
            txt_kkcess.Text = dt.Rows[0]["KKCess"].ToString();
            txtEduCessTax.Text = dt.Rows[0]["EDU_CESS"].ToString();
            txtSecEduCessTax.Text = dt.Rows[0]["SEC_EDU_CESS"].ToString();
            txtTotalTax.Text = dt.Rows[0]["TOTAL_SERVICE_TAX"].ToString();
        }
    }
    protected void clearControls()
    {
        //txtSrNo.Text = "";
        txtEffectiveDate.Text = "";
        txtServiceTax.Text = "";
        txtEduCessTax.Text = "";
        txtSecEduCessTax.Text = "";
        txtTotalTax.Text = "";
        txtEffectiveDate.Focus();
    }
    protected void txtSecEduCessTax_TextChanged(object sender, EventArgs e)
    {
        string _eDate = txtEffectiveDate.Text.Trim();
        string _stax = txtServiceTax.Text.Trim();
        string _edutax = txtEduCessTax.Text.Trim();
        string _secedutax = txtSecEduCessTax.Text.Trim();
        string _sbcess = txt_sbcess.Text.Trim();
        string _kkcess = txt_kkcess.Text.Trim();

        decimal stax = Convert.ToDecimal(_stax);
        decimal edutax = Convert.ToDecimal(_edutax);
        decimal secedutax = Convert.ToDecimal(_secedutax);
        decimal sbcess = Convert.ToDecimal(_sbcess);
        decimal kkcess = Convert.ToDecimal(_kkcess);

        decimal tax =  sbcess +kkcess + stax + ((stax * edutax) / 100);

        totaltax = tax + ((tax * secedutax) / 100);
        txtTotalTax.Text = Convert.ToString(totaltax);
        btnSave.Focus();
    }
}