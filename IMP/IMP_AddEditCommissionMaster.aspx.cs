using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class IMP_IMP_AddEditCommissionMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
    }


    private void fillDetails(string _year)
    {
        TF_DATA objData = new TF_DATA();
        string _query = "TF_IMP_GetCommissionDetails";

        SqlParameter pType = new SqlParameter("@type", SqlDbType.VarChar);
        pType.Value = txtType.Text.Trim();

        DataTable dt = objData.getData(_query, pType);
        if (dt.Rows.Count > 0)
        {
            txtDescription.Text = dt.Rows[0]["C_DESCRIPTION"].ToString();
            txtRate.Text = dt.Rows[0]["C_RATE1"].ToString();
            txtMinAmt.Text = dt.Rows[0]["C_RATE2"].ToString();

            txtDescription.Focus();
        }
        else
        {
            txtDescription.Focus();

        }
    }

    private void Clear()
    {
        txtType.Text = "";
        txtDescription.Text = "";
        txtRate.Text = "";
        txtMinAmt.Text = "";

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _mode = Request.QueryString["mode"].Trim();

        TF_DATA objSave = new TF_DATA();
        string _query = "TF_IMP_UpdateCommission_Master";

        SqlParameter pMode = new SqlParameter("@mode", SqlDbType.VarChar);
        pMode.Value = _mode;

        SqlParameter pType = new SqlParameter("@type", SqlDbType.VarChar);
        pType.Value = txtType.Text.Trim();

        SqlParameter pDesc = new SqlParameter("@desc", SqlDbType.VarChar);
        pDesc.Value = txtDescription.Text.Trim();

        SqlParameter pRate = new SqlParameter("@rate", SqlDbType.VarChar);
        pRate.Value = txtRate.Text.Trim();

        SqlParameter pminamt = new SqlParameter("@minamt", SqlDbType.VarChar);
        pminamt.Value = txtMinAmt.Text.Trim();

        SqlParameter pUser = new SqlParameter("@user", SqlDbType.VarChar);
        pUser.Value = _userName;

        SqlParameter pUploadDate = new SqlParameter("@uploadDate", SqlDbType.VarChar);
        pUploadDate.Value = _uploadingDate;

        string _result = objSave.SaveDeleteData(_query, pMode, pType, pDesc, pRate, pminamt, pUser, pUploadDate);
        //string _script = "";
        if (_result == "added")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Record Added')", true);
        }
        else
        {
            if (_result == "updated")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Record Added')", true);
            }
            else
                labelMessage.Text = _result;
        }
    }
}