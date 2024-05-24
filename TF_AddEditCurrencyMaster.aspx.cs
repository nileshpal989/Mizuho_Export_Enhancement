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

public partial class RBI_AddEditCurrencyMaster : System.Web.UI.Page
{
    string _NewValue;
    string _OldValues;
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
                txtCurrencyID.Focus();
                btnSave.Attributes.Add("onclick", "return validateSave();");
                txtCurrencyID.Attributes.Add("onkeypress", "javascript:return onlyChars(event)");
                clearControls();
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("TF_ViewCurrencyMaster.aspx", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtCurrencyID.Text = Request.QueryString["currencyid"].Trim();
                        txtCurrencyID.Enabled = false;
                        fillDetails(Request.QueryString["currencyid"].Trim());
                    }
                    else
                    {
                        txtCurrencyID.Enabled = true;
                    }
                }
                btnSave.Attributes.Add("onclick", "return validateSave();");
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewCurrencyMaster.aspx", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _mode = Request.QueryString["mode"].Trim();
        //string _cReCID = txtCRecID.Text.Trim();
        string _currencyID = txtCurrencyID.Text.Trim();
        string _currencyDescription = txtDescription.Text.Trim();
        string _Status = "";
        if (rdbActive.Checked)
        {
            _Status = "Active";
        }
        if (rdbInActive.Checked)
        {
            _Status = "In-Active";
        }

        TF_DATA objSave = new TF_DATA();
        string _query = "TF_UpdateCurrencyMaster";

        SqlParameter p1 = new SqlParameter("@mode", SqlDbType.VarChar);
        p1.Value = _mode;
        // SqlParameter p2 = new SqlParameter("@crecid", SqlDbType.VarChar);
        // p2.Value = _cReCID;
        SqlParameter p3 = new SqlParameter("@currencyid", SqlDbType.VarChar);
        p3.Value = _currencyID;
        SqlParameter p4 = new SqlParameter("@description", SqlDbType.VarChar);
        p4.Value = _currencyDescription;
        SqlParameter p5 = new SqlParameter("@status", SqlDbType.VarChar);
        p5.Value = _Status;
        SqlParameter p6 = new SqlParameter("@GBaseCode", SqlDbType.VarChar);
        p6.Value = txtGBaseCode.Text.Trim();
        SqlParameter p7 = new SqlParameter("@user", SqlDbType.VarChar);
        p7.Value = Session["userName"].ToString().Trim();

        _result = objSave.SaveDeleteData(_query, p1, p3, p4, p5, p6, p7);
        string _script = "";

        ///////////Audit Trail////////////////////////////////
        SqlParameter A1 = new SqlParameter("@OldValues", SqlDbType.VarChar);
        SqlParameter A2 = new SqlParameter("@NewValues", SqlDbType.VarChar);
        SqlParameter A3 = new SqlParameter("@ModType", SqlDbType.VarChar);
        SqlParameter A4 = new SqlParameter("@ModifiedBy", Session["userName"].ToString().Trim());
        SqlParameter A5 = new SqlParameter("@ModifiedDate", "");
        SqlParameter A6 = new SqlParameter("@MenuName", "Currency Master");
        SqlParameter A7 = new SqlParameter("@Mode", SqlDbType.VarChar);
        SqlParameter A8 = new SqlParameter("@RefNo", SqlDbType.VarChar);
        string status = "";

        if (rdbActive.Checked == true)
        {
            status = "Active";

        }
        else if (rdbInActive.Checked == true)
        {
            status = "In-Active";
        }


        if (_result == "added")
        {

            _NewValue = "Description : " + txtDescription.Text.Trim()+ " ; GBase Curr Code : " +txtGBaseCode.Text.Trim() +
        " ; Currency ID : " + txtCurrencyID.Text.Trim() + " ; Status : "+status;

            A1.Value = "";
            A2.Value = _NewValue;
            A3.Value = "IMP";
            A7.Value = "A";
            A8.Value = _currencyID;
            string p = objSave.SaveDeleteData("TF_Mizuho_IMP_AuditTrail_master", A1, A2, A3, A4, A5, A6, A7, A8);

            _script = "window.location='TF_ViewCurrencyMaster.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            int isneedtolog = 0;
            if (_result == "updated")
            {
               
                _OldValues = "Description : " + hdncurrencydescription.Value.Trim()+" ; GBase Curr Code : " +hdnGBbase.Value.Trim()+" ; Status : " +hdnstatus.Value.Trim();
                if (hdncurrencydescription.Value != txtDescription.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = "Description : " + txtDescription.Text.Trim();
                    }
                    else
                    {
                        _NewValue = "Description : " + txtDescription.Text.Trim();
                    }
                }
                if (hdnGBbase.Value != txtGBaseCode.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; GBase Curr Code : " + txtGBaseCode.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; GBase Curr Code : " + txtGBaseCode.Text.Trim();
                    }
                }
                if (hdnstatus.Value != status.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; Status : " + status.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; Status : " + status.Trim();
                    }
                }

                A1.Value = _OldValues;
                A2.Value = _NewValue;
                A3.Value = "IMP";
                A7.Value = "M";
                A8.Value = _currencyID;

                if (isneedtolog == 1)
                {
                    string p = objSave.SaveDeleteData("TF_Mizuho_IMP_AuditTrail_master", A1, A2, A3, A4, A5, A6, A7, A8);
                }
                _script = "window.location='TF_ViewCurrencyMaster.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
                //labelMessage.Text = _result;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "VAlert('" + _result + "','#txtCurrencyID');", true);
             }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewCurrencyMaster.aspx", true);
    }
    protected void clearControls()
    {
        //  txtCRecID.Text = "";
        txtCurrencyID.Text = "";
        txtDescription.Text = "";
    }
    protected void fillDetails(string _currencyID)
    {
        SqlParameter p1 = new SqlParameter("@currencyid", SqlDbType.VarChar);
        p1.Value = _currencyID;
        string _query = "TF_GetCurrencyMasterDetails";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            //txtCRecID.Text = dt.Rows[0]["C_Rec_ID"].ToString().Trim();
            txtCurrencyID.Text = dt.Rows[0]["C_Code"].ToString().Trim();
            txtDescription.Text = dt.Rows[0]["C_Description"].ToString().Trim();
            txtGBaseCode.Text = dt.Rows[0]["GBase_Code"].ToString().Trim();
            if (dt.Rows[0]["C_Status"].ToString().Trim() == "In-Active")
            {
                rdbInActive.Checked = true;
            }
            if (dt.Rows[0]["C_Status"].ToString().Trim() == "Active")
            {
                rdbActive.Checked = true;
            }
            //-------------------------- audit trail  -------------------------------//
            hdncurrencydescription.Value = dt.Rows[0]["C_Description"].ToString();
            hdnGBbase.Value = dt.Rows[0]["GBase_Code"].ToString().Trim();
            hdnstatus.Value = dt.Rows[0]["C_Status"].ToString().Trim();
        }
    }
}