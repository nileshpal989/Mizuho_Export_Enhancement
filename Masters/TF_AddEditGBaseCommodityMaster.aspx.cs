using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class TF_AddEditGBaseCommodityMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("../TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                btnSave.Attributes.Add("onclick", "return validateSave();");
                clear();
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("TF_GBaseCommodityMaster.aspx", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtCommodityID.Text = Request.QueryString["CommodityID"].Trim();
                        txtCommodityID.Enabled = false;
                        fillDetails(Request.QueryString["CommodityID"].Trim());
                    }
                    else
                    {
                        txtCommodityID.Enabled = true;
                        txtCommodityID.Focus();
                    }
                }
                btnSave.Attributes.Add("onclick", "return validateSave();");

            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _mode = Request.QueryString["mode"].Trim();
        string _CommodityID = txtCommodityID.Text.Trim();
        string _CommodityDesc = txtCommodityName.Text.Trim();

        TF_DATA objSave = new TF_DATA();
        string _query = "TF_UpdateGBASECommodityMaster";

        SqlParameter p1 = new SqlParameter("@mode", SqlDbType.VarChar);
        p1.Value = _mode;

        SqlParameter p2 = new SqlParameter("@CommodityID", SqlDbType.VarChar);
        p2.Value = _CommodityID;

        SqlParameter p3 = new SqlParameter("@CommodityDesc", SqlDbType.VarChar);
        p3.Value = _CommodityDesc;

        SqlParameter p4 = new SqlParameter("@user", SqlDbType.VarChar);
        p4.Value = _userName;

        SqlParameter p5 = new SqlParameter("@uploadingdate", SqlDbType.VarChar);
        p5.Value = _uploadingDate;

        _result = objSave.SaveDeleteData(_query, p1, p2, p3, p4, p5);
        string _script = "";
        if (_result == "added")
        {
            _script = "window.location='TF_GBaseCommodityMaster.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (_result == "updated")
            {
                _script = "window.location='TF_GBaseCommodityMaster.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
                labelMessage.Text = _result;
        }
    }
    protected void fillDetails(string _CommodityID)
    {
        TF_DATA objData = new TF_DATA();
        string _query = "TF_GetGBASECommodityMasterDetails";

        SqlParameter p1 = new SqlParameter("@CommodityID", SqlDbType.VarChar);
        p1.Value = _CommodityID;

        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            txtCommodityName.Text = dt.Rows[0]["GBase_Commodity_Description"].ToString().Trim();
            txtCommodityName.Focus();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_GBaseCommodityMaster.aspx", true);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_GBaseCommodityMaster.aspx", true);
    }
    protected void txtCommodityID_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        string _query = "TF_GBASECommodity_ID_Textchange";

        SqlParameter p1 = new SqlParameter("@CommodityID", SqlDbType.VarChar);
        p1.Value = txtCommodityID.Text.ToString();

        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows[0]["result"].ToString() == "exists")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record already exists.');", true);
            fillDetails(txtCommodityID.Text.ToString());
        }
        else
        {
            txtCommodityName.Focus();
        }
    }
    protected void clear()
    {
        txtCommodityID.Text = "";
        txtCommodityName.Text = "";
    }
}