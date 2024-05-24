using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class TF_AddEditCourierChargesMaster : System.Web.UI.Page
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
                    Response.Redirect("TF_ViewCommodityMaster.aspx", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtCourierchrgID.Text = Request.QueryString["CourierChargesID"].Trim();
                        txtCourierchrgID.Enabled = false;
                        txtDescription.Focus();
                        fillDetails(Request.QueryString["CourierChargesID"].Trim());
                    }
                    else
                    {
                        txtCourierchrgID.Enabled = true;
                        txtCourierchrgID.Focus();
                    }
                }
                btnSave.Attributes.Add("onclick", "return validateSave();");
                txtAmount.Attributes.Add("onkeydown", "return validate_Number(event);");
            }
        }
    }
    void clearControls()
    {
        txtCourierchrgID.Text = "";
        txtDescription.Text = "";
        txtAmount.Text = "";
    }
    void fillDetails(string CourierChargesID)
    {
        SqlParameter p1 = new SqlParameter("@CourierChargesID", SqlDbType.VarChar);
        p1.Value = CourierChargesID;

        string _query = "TF_GetDetailsCourierChargesMaster";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            txtDescription.Text = dt.Rows[0]["Description"].ToString().Trim();
            txtAmount.Text = dt.Rows[0]["Amount"].ToString();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_View_Courier_Charges_Master.aspx", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _mode = Request.QueryString["mode"].Trim();
        TF_DATA objSave = new TF_DATA();
        string _query = "TF_UpdateCourierChargesMaster";

        SqlParameter p1 = new SqlParameter("@mode", SqlDbType.VarChar);
        p1.Value = _mode;

        SqlParameter p2 = new SqlParameter("@CourierChargesID", SqlDbType.VarChar);
        p2.Value = txtCourierchrgID.Text.Trim();

        SqlParameter p3 = new SqlParameter("@CourierDesc", SqlDbType.VarChar);
        p3.Value = txtDescription.Text.Trim();

        SqlParameter p4 = new SqlParameter("@Amt", SqlDbType.VarChar);
        p4.Value = txtAmount.Text.Trim();

        SqlParameter p5 = new SqlParameter("@user", SqlDbType.VarChar);
        p5.Value = _userName;

        SqlParameter p6 = new SqlParameter("@uploadingdate", SqlDbType.VarChar);
        p6.Value = _uploadingDate;

        _result = objSave.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6);
        string _script = "";
        if (_result == "added")
        {
            _script = "window.location='TF_View_Courier_Charges_Master.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (_result == "updated")
            {
                _script = "window.location='TF_View_Courier_Charges_Master.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
                labelMessage.Text = _result;
        }


    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_View_Courier_Charges_Master.aspx", true);
    }
}