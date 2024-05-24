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

public partial class TF_AddEditReasonForAdjustmentMaster : System.Web.UI.Page
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
                txtAdjustmentCode.Focus();
                
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("TF_View_ReasonForAdjustmentMaster.aspx", true);
                }

                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtAdjustmentCode.Text = Request.QueryString["Adjustment_Code"].Trim();
                        txtAdjustmentCode.Enabled = false;
                        fillDetails(Request.QueryString["Adjustment_Code"].Trim());
                    }
                    else
                    {
                        txtAdjustmentCode.Enabled = true;
                    }
                }
                
                btnSave.Attributes.Add("onclick", "return validateSave();");
            }
        }
              
    }

    protected void clearControls()
    {
        txtAdjustmentCode.Text = "";
        txtDescription.Text = "";
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_View_ReasonForAdjustmentMaster.aspx", true);
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _mode = Request.QueryString["mode"].Trim();
        string _Adjstcde = txtAdjustmentCode.Text.Trim();
        string _AdjDesc = txtDescription.Text.Trim();

        TF_DATA objSave = new TF_DATA();
        string _query = "TF_UpdateReasonForAdjstMent";

        SqlParameter p1 = new SqlParameter("@mode", SqlDbType.VarChar);
        p1.Value = _mode;
        SqlParameter p2 = new SqlParameter("@AdjstmntCde", SqlDbType.VarChar);
        p2.Value = _Adjstcde;
        SqlParameter p3 = new SqlParameter("@AdjstmntDesc", SqlDbType.VarChar);
        p3.Value = _AdjDesc;
       
        _result = objSave.SaveDeleteData(_query,p1,p2,p3);
        string _script = "";
        if (_result == "added")
        {
            _script = "window.location='TF_View_ReasonForAdjustmentMaster.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (_result == "updated")
            {
                _script = "window.location='TF_View_ReasonForAdjustmentMaster.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
                labelMessage.Text = _result;
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_View_ReasonForAdjustmentMaster.aspx", true);
    }

    protected void fillDetails(string _Adjstcde)
    {
        SqlParameter p1 = new SqlParameter("@AdjstmntCde", SqlDbType.VarChar);
        p1.Value = _Adjstcde;
        string _query = "TF_GetReasonForAdjstMent";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {

            txtDescription.Text = dt.Rows[0]["Adjustment_Description"].ToString().Trim();
        }
    }
}