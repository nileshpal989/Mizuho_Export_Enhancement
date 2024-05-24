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

public partial class TF_AddEditCommodityMaster : System.Web.UI.Page
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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewCommodityMaster.aspx", true);
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
        string _query = "TF_UpdateCommodityMaster";

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


        ///////////Audit Trail////////////////////////////////
        SqlParameter A1 = new SqlParameter("@OldValues", SqlDbType.VarChar);
        SqlParameter A2 = new SqlParameter("@NewValues", SqlDbType.VarChar);
        SqlParameter A3 = new SqlParameter("@ModType", SqlDbType.VarChar);
        SqlParameter A4 = new SqlParameter("@ModifiedBy", Session["userName"].ToString().Trim());
        SqlParameter A5 = new SqlParameter("@ModifiedDate", "");
        SqlParameter A6 = new SqlParameter("@MenuName", "Commodity Master");
        SqlParameter A7 = new SqlParameter("@Mode", SqlDbType.VarChar);
        SqlParameter A8 = new SqlParameter("@RefNo", SqlDbType.VarChar);
        if (_result == "added")
        {
            _NewValue = "Description : " + txtCommodityName.Text.Trim()+" ; Commodity ID : "+txtCommodityID.Text.Trim();
            A1.Value = "";
            A2.Value = _NewValue;
            A3.Value = "IMP";
            A7.Value = "A";
            A8.Value = _CommodityID;
            string p = objSave.SaveDeleteData("TF_Mizuho_IMP_AuditTrail_master", A1, A2, A3, A4, A5, A6, A7, A8);
               
            _script = "window.location='TF_ViewCommodityMaster.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
         else
            {
                int isneedtolog = 0;
             if (_result == "updated")
                 {

                     _OldValues = "Description : " + hdncommdity.Value.Trim();
                     if (hdncommdity.Value != txtCommodityName.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = "Description : " + txtCommodityName.Text.Trim();
                    }
                    else
                    {
                        _NewValue = "Description : " + txtCommodityName.Text.Trim();
                    }
                }

                A1.Value = _OldValues;
                A2.Value = _NewValue;
                A3.Value = "IMP";
                A7.Value = "M";
                A8.Value = _CommodityID;

                if (isneedtolog == 1)
                {
                    string pt = objSave.SaveDeleteData("TF_Mizuho_IMP_AuditTrail_master", A1, A2, A3, A4, A5, A6, A7, A8);
                }

                       _script = "window.location='TF_ViewCommodityMaster.aspx?result=" + _result + "'";
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                }
                   
                      else
                        //labelMessage.Text = _result;
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "VAlert('" + _result + "','#txtCommodityID');", true);
                }
        }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewCommodityMaster.aspx", true);
    }
    protected void clearControls()
    {
        txtCommodityID.Text = "";
        txtCommodityName.Text = "";
    }
    protected void fillDetails(string _CommodityID)
    {
        TF_DATA objData = new TF_DATA();
        string _query = "TF_GetCommodityMasterDetails";

        SqlParameter p1 = new SqlParameter("@CommodityID", SqlDbType.VarChar);
        p1.Value = _CommodityID;

        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            txtCommodityName.Text = dt.Rows[0]["CommodityDescription"].ToString().Trim();
            hdncommdity.Value = dt.Rows[0]["CommodityDescription"].ToString().Trim();
        }
    }
}
