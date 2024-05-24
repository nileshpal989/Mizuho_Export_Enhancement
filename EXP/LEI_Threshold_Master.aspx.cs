using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EXP_LEI_Threshold_Master : System.Web.UI.Page
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
                clearControls();

                btnSave.Attributes.Add("onclick", "return validateSave();");
                txtThresholdAmt.Attributes.Add("onkeypress", "javascript:return onlyNumber(event)");

                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("VIEW_LEI_Threshold_Master.aspx", true);
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
        Response.Redirect("VIEW_LEI_Threshold_Master.aspx", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string query = "TF_UpdateThresholdMaster";
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy");
        string _mode = Request.QueryString["mode"].Trim();
        //string _SrNo = txtSrNo.Text.Trim();
        string _eDate = txtEffectiveDate.Text.Trim();
        string _tAmt = txtThresholdAmt.Text.Trim();

        SqlParameter edate = new SqlParameter("@eDate", SqlDbType.VarChar);
        SqlParameter svtax = new SqlParameter("@tAmt", SqlDbType.VarChar);
        SqlParameter user = new SqlParameter("@user", SqlDbType.VarChar);
        SqlParameter date = new SqlParameter("@date", SqlDbType.VarChar);
        SqlParameter mode = new SqlParameter("@mode", SqlDbType.VarChar);
        decimal tAmt = Convert.ToDecimal(_tAmt);
        mode.Value = _mode;
        edate.Value = _eDate;
        svtax.Value = tAmt;
        user.Value = _userName;
        date.Value = _uploadingDate;

        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData(query, mode, edate, svtax, user, date);
        string _script = "";
        if (_result == "added")
        {
            _script = "window.location='VIEW_LEI_Threshold_Master.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", _script, true);
        }
        else if (_result == "updated")
        {
            _script = "window.location='VIEW_LEI_Threshold_Master.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", _script, true);
        }
        else
        {
            labelMessage.Text = _result;
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("VIEW_LEI_Threshold_Master.aspx", true);
    }
    protected void filldetails()
    {
        string _effectiveDate = txtEffectiveDate.Text.Trim();
        SqlParameter p1 = new SqlParameter("@EffectiveDate", SqlDbType.VarChar);
        p1.Value = _effectiveDate;
        string query = "TF_GetThresholdMasterDetails";
        TF_DATA objdata = new TF_DATA();
        DataTable dt = objdata.getData(query, p1);
        if (dt.Rows.Count > 0)
        {
            //txtEffectiveDate.Text = dt.Rows[0]["EFFECTIVE_DATE"].ToString();
            txtThresholdAmt.Text = dt.Rows[0]["Amount_Threshold"].ToString();
           
        }
    }
    protected void clearControls()
    {
        //txtSrNo.Text = "";
        txtEffectiveDate.Text = "";
        txtThresholdAmt.Text = "";
        txtEffectiveDate.Focus();
    }
}