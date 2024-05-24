using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class TD_AddReceivingBankMaster : System.Web.UI.Page
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
                btnSave.Attributes.Add("onclick", "return validateSave();");
                clearControls();
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("TF_VeiwReceivingBankMaster.aspx", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtSrNo.Text = Request.QueryString["SrNo"].Trim();
                        fillDetails(Request.QueryString["SrNo"].Trim());
                        btnCurrency.Visible = false;
                    }
                    else
                    {
                        btnCurrency.Visible = true;
                        clearControls();
                    }
                }
                btnSave.Attributes.Add("onclick", "return validateSave();");
                btnCurrency.Attributes.Add("onclick", "return curhelp();");
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewReceivingBankMaster.aspx", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _mode = Request.QueryString["mode"].Trim();

        TF_DATA objSave = new TF_DATA();
        string _query = "TF_UpdateReceivingBankMaster";

        SqlParameter p1 = new SqlParameter("@mode", SqlDbType.VarChar);
        p1.Value = _mode;
        SqlParameter p2 = new SqlParameter("@srno", SqlDbType.VarChar);
        p2.Value = txtSrNo.Text;
        SqlParameter p3 = new SqlParameter("@currencyid", SqlDbType.VarChar);
        p3.Value = txtCurrencyID.Text;
        SqlParameter p4 = new SqlParameter("@bankname", SqlDbType.VarChar);
        p4.Value = txtBankName.Text;
        SqlParameter p5 = new SqlParameter("@abbr", SqlDbType.VarChar);
        p5.Value = txtAbbr.Text;
        SqlParameter p6 = new SqlParameter("@addedby", SqlDbType.VarChar);
        p6.Value = Session["userName"].ToString();
        SqlParameter p7 = new SqlParameter("@addedtime", SqlDbType.VarChar);
        p7.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        SqlParameter p8 = new SqlParameter("@bankCountry", txtBankCountry.Text.Trim());

        _result = objSave.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6, p7, p8);
        string _script = "";
        if (_result == "added")
        {
            _script = "window.location='TF_ViewReceivingBankMaster.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (_result == "updated")
            {
                _script = "window.location='TF_ViewReceivingBankMaster.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
                labelMessage.Text = _result;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewReceivingBankMaster.aspx", true);
    }

    protected void clearControls()
    {
        //  txtCRecID.Text = "";
        txtSrNo.Text = getSrNo();
        txtCurrencyID.Text = "";
        txtBankName.Text = "";
        txtAbbr.Text = "";
    }

    protected void fillDetails(string srno)
    {
        SqlParameter p1 = new SqlParameter("@srno", SqlDbType.VarChar);
        p1.Value = srno;
        string _query = "TF_GetReceivingBankMasterDetails";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            txtCurrencyID.Text = dt.Rows[0]["CurrencyCode"].ToString().Trim();
            txtBankName.Text = dt.Rows[0]["BankName"].ToString().Trim();
            txtAbbr.Text = dt.Rows[0]["Abbreviation"].ToString().Trim();
            txtBankCountry.Text = dt.Rows[0]["BankCountry_Code"].ToString().Trim();
            if(txtBankCountry.Text!="")
            txtBankCountry_TextChanged(null,null);
        }
    }

    protected string getSrNo()
    {
        string srno = "";

        string query = "TF_GetLastSrNo";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(query);
        if (dt.Rows.Count > 0)
        {
            srno = dt.Rows[0]["SrNo"].ToString();
        }
        return srno;
    }

    protected void txtBankCountry_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@cid", txtBankCountry.Text.Trim());
        DataTable dt = objData.getData("TF_GetCountryDetails", p1);
        if (dt.Rows.Count > 0)
        {
            lblCountryDesc.Text = dt.Rows[0]["CountryName"].ToString();
        }
        else
        {
            if (lblCountryDesc.Text == "")
            {
                lblCountryDesc.Text = "";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "alert('Invalid Country.')", true);
                txtBankCountry.Focus();
            }
        }
    }
}
