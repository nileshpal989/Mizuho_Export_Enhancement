using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Master_EXP_AddEditReimbursingBank : System.Web.UI.Page
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
                    Response.Redirect("EXP_View_ReimbursingBank.aspx", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtBankID.Text = Request.QueryString["bankcode"].Trim();
                        txtCurr.Text = Request.QueryString["Curr"].Trim();
                        txtCurr.Enabled = false;
                        txtBankID.Enabled = false;
                        txtBankName.Focus();
                        fillDetails(Request.QueryString["bankcode"].Trim());
                    }
                    else
                    {
                        txtCurr.Focus();
                        txtBankID.Enabled = true;
                    }
                }
                btnSave.Attributes.Add("onclick", "return validateSave();");
                txtCountry.Attributes.Add("onkeydown", "return CountryId(event)");
                btnCountryList.Attributes.Add("onclick", "return Countryhelp()");

            }
        }
    }

    protected void fillDetails(string _bankCode)
    {
        SqlParameter p1 = new SqlParameter("@bankcode", _bankCode);
        string _query = "TF_GetReimburseBankMasterDetails";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            txtCurr.Text = dt.Rows[0]["Curr"].ToString().Trim();
            txtCurr_TextChanged(this, null);

            txtBankName.Text = dt.Rows[0]["BankName"].ToString().Trim();
            txtBranchName.Text = dt.Rows[0]["BranchName"].ToString().Trim();
            txtAddress.Text = dt.Rows[0]["BankAddress"].ToString().Trim();
            txtCity.Text = dt.Rows[0]["City"].ToString().Trim();
            txtPincode.Text = dt.Rows[0]["Pincode"].ToString().Trim();
            txtCountry.Text = dt.Rows[0]["Country"].ToString().Trim();
            txtCountry_TextChanged(this, null);

            txtSpecialInstructions1.Text = dt.Rows[0]["Special_Instructions1"].ToString().Trim();
            txtSpecialInstructions2.Text = dt.Rows[0]["Special_Instructions2"].ToString().Trim();
            txtSpecialInstructions3.Text = dt.Rows[0]["Special_Instructions3"].ToString().Trim();
            txtSpecialInstructions4.Text = dt.Rows[0]["Special_Instructions4"].ToString().Trim();
            txtSpecialInstructions5.Text = dt.Rows[0]["Special_Instructions5"].ToString().Trim();
            txtSpecialInstructions6.Text = dt.Rows[0]["Special_Instructions6"].ToString().Trim();
            txtSpecialInstructions7.Text = dt.Rows[0]["Special_Instructions7"].ToString().Trim();
            txtSpecialInstructions8.Text = dt.Rows[0]["Special_Instructions8"].ToString().Trim();
            txtSpecialInstructions9.Text = dt.Rows[0]["Special_Instructions9"].ToString().Trim();
            txtSpecialInstructions10.Text = dt.Rows[0]["Special_Instructions10"].ToString().Trim();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";

        SqlParameter p1 = new SqlParameter("@Curr", txtCurr.Text.Trim());
        SqlParameter p2 = new SqlParameter("@BankCode", txtBankID.Text.Trim());
        SqlParameter p3 = new SqlParameter("@Bankname", txtBankName.Text.Trim());
        SqlParameter p4 = new SqlParameter("@Branchname", txtBranchName.Text.Trim());
        SqlParameter p5 = new SqlParameter("@BankAddress", txtAddress.Text.Trim());
        SqlParameter p6 = new SqlParameter("@City", txtCity.Text.Trim());
        SqlParameter p7 = new SqlParameter("@Pincode", txtPincode.Text.Trim());
        SqlParameter p8 = new SqlParameter("@Country", txtCountry.Text.Trim());

        SqlParameter p9 = new SqlParameter("@Special_Instructions1", txtSpecialInstructions1.Text.Trim());
        SqlParameter p10 = new SqlParameter("@Special_Instructions2", txtSpecialInstructions2.Text.Trim());
        SqlParameter p11 = new SqlParameter("@Special_Instructions3", txtSpecialInstructions3.Text.Trim());
        SqlParameter p12 = new SqlParameter("@Special_Instructions4", txtSpecialInstructions4.Text.Trim());
        SqlParameter p13 = new SqlParameter("@Special_Instructions5", txtSpecialInstructions5.Text.Trim());
        SqlParameter p14 = new SqlParameter("@Special_Instructions6", txtSpecialInstructions6.Text.Trim());
        SqlParameter p15 = new SqlParameter("@Special_Instructions7", txtSpecialInstructions7.Text.Trim());
        SqlParameter p16 = new SqlParameter("@Special_Instructions8", txtSpecialInstructions8.Text.Trim());
        SqlParameter p17 = new SqlParameter("@Special_Instructions9", txtSpecialInstructions9.Text.Trim());
        SqlParameter p18 = new SqlParameter("@Special_Instructions10", txtSpecialInstructions10.Text.Trim());

        SqlParameter p19 = new SqlParameter("@Added_by", Session["userName"].ToString());
        SqlParameter p20 = new SqlParameter("@Added_date", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));

        string _query = "TF_Update_ReimbursingBankMaster";
        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20);
        string _script = "";
        if (_result == "added")
        {
            _script = "window.location='EXP_View_ReimbursingBank.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (_result == "updated")
            {
                _script = "window.location='EXP_View_ReimbursingBank.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
            {
                labelMessage.Text = _result;
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("EXP_View_ReimbursingBank.aspx", true);
    }
    protected void txtCountry_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@cid", txtCountry.Text.ToString());
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

    protected void clearControls()
    {
        txtBankID.Text = "";
        txtBankName.Text = "";
        txtAddress.Text = "";
        txtCity.Text = "";
        txtPincode.Text = "";
        txtCountry.Text = "";

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EXP_View_ReimbursingBank.aspx", true);
    }
    protected void txtCurr_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@currencyid", txtCurr.Text.ToString());
        string _query = "TF_GetCurrencyMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCurrDesc.Text = dt.Rows[0]["C_Description"].ToString().Trim();
            txtCurr.Text.ToUpper();
            txtBankID.Focus();
        }
        else
        {
            txtCurr.Text = "";
            lblCurrDesc.Text = "";
            txtCurr.Focus();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('Currency Code does not exist in Currency Master');", true);
        }
    }
    protected void txtBankID_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter BankCode = new SqlParameter("@BankCode", txtBankID.Text.ToString());
        string _query = "TF_GetReimbBankMasterDetails";
        DataTable dt = objData.getData(_query, BankCode);
        if (dt.Rows.Count > 0)
        {
            txtCurr.Text = dt.Rows[0]["Curr"].ToString().Trim();
            txtBankName.Text = dt.Rows[0]["BankName"].ToString().Trim();
            txtBranchName.Text = dt.Rows[0]["BranchName"].ToString().Trim();
            txtAddress.Text = dt.Rows[0]["BankAddress"].ToString().Trim();
            txtCity.Text = dt.Rows[0]["City"].ToString().Trim();
            txtPincode.Text = dt.Rows[0]["Pincode"].ToString().Trim();
            txtCountry.Text = dt.Rows[0]["Country"].ToString().Trim();
            txtSpecialInstructions1.Text = dt.Rows[0]["Special_Instructions1"].ToString().Trim();
            txtSpecialInstructions2.Text = dt.Rows[0]["Special_Instructions2"].ToString().Trim();
            txtSpecialInstructions3.Text = dt.Rows[0]["Special_Instructions3"].ToString().Trim();
            txtSpecialInstructions4.Text = dt.Rows[0]["Special_Instructions4"].ToString().Trim();
            txtSpecialInstructions5.Text = dt.Rows[0]["Special_Instructions5"].ToString().Trim();
            txtSpecialInstructions6.Text = dt.Rows[0]["Special_Instructions6"].ToString().Trim();
            txtSpecialInstructions7.Text = dt.Rows[0]["Special_Instructions7"].ToString().Trim();
            txtSpecialInstructions8.Text = dt.Rows[0]["Special_Instructions8"].ToString().Trim();
            txtSpecialInstructions9.Text = dt.Rows[0]["Special_Instructions9"].ToString().Trim();
            txtSpecialInstructions10.Text = dt.Rows[0]["Special_Instructions10"].ToString().Trim();
            txtBankID.Focus();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('Bank ID Alredy Exists');", true);
        }
        else
        {
            txtBankName.Text = "";
            txtBranchName.Text = "";
            txtAddress.Text = "";
            txtCity.Text = "";
            txtPincode.Text = "";
            txtCountry.Text = "";
            txtSpecialInstructions1.Text = "";
            txtSpecialInstructions2.Text = "";
            txtSpecialInstructions3.Text = "";
            txtSpecialInstructions4.Text = "";
            txtSpecialInstructions5.Text = "";
            txtSpecialInstructions6.Text = "";
            txtSpecialInstructions7.Text = "";
            txtSpecialInstructions8.Text = "";
            txtSpecialInstructions9.Text = "";
            txtSpecialInstructions10.Text = "";
            if (txtBankID.Text.Trim() != "")
            {
                txtBankName.Focus();
            }
            else
            {
                txtBankID.Focus();
            }
        }
    }
}