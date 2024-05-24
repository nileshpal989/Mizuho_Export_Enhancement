using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class TF_AddEditSectorMaster : System.Web.UI.Page
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
                clearall();
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("TF_ViewSectorMaster.aspx", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtSectorID.Text = Request.QueryString["sectorID"].Trim();
                        txtSectorID.Enabled = false;
                        fillDetails(Request.QueryString["sectorID"].Trim());
                    }
                    else
                    {
                        txtSectorID.Enabled = true;
                    }
                }
                btnSave.Attributes.Add("onclick", "return validateSave();");
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _userName = Session["userName"].ToString();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _mode = Request.QueryString["mode"].Trim();
        string _SectorId = txtSectorID.Text.Trim();
        string _SectorName = txtSectorName.Text.Trim();

        SqlParameter p1 = new SqlParameter("@mode", SqlDbType.VarChar);
        p1.Value = _mode;
        SqlParameter p2 = new SqlParameter("@sectorid", SqlDbType.VarChar);
        p2.Value = _SectorId;
        SqlParameter p3 = new SqlParameter("@sectorname", SqlDbType.VarChar);
        p3.Value = _SectorName;
        SqlParameter p4 = new SqlParameter("@user", SqlDbType.VarChar);
        p4.Value = _userName;
        SqlParameter p5 = new SqlParameter("@uploadeddate", SqlDbType.VarChar);
        p5.Value = _uploadingDate;

        string _query = "TF_UpdateSectorMaster";
        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData(_query ,p1,p2,p3,p4,p5);

            string _script = "";
            if (_result == "added")
            {
                _script = "window.location='TF_ViewSectorMaster.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
            {
                if (_result == "updated")
                {
                    _script = "window.location='TF_ViewSectorMaster.aspx?result=" + _result + "'";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                }
                //else
                //    //labelMessage.Text = _result;
            }
      
        clearall();


    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewSectorMaster.aspx", true);
    }
    protected void txtSectorID_TextChanged(object sender, EventArgs e)
    {

    }
    
    public void clearall()
    {
        txtSectorID.Text = "";
        txtSectorName.Text = "";
        
    }
    protected void txtSectorName_TextChanged(object sender, EventArgs e)
    {

    }
    protected void fillDetails(string _sectorID)
    {
        SqlParameter p1 = new SqlParameter("@sectorid", SqlDbType.VarChar);
        p1.Value = _sectorID;
        string _query = "TF_GetSectorMasterDetails";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query,p1);
        if (dt.Rows.Count > 0)
        {
            txtSectorID.Text = dt.Rows[0][0].ToString().Trim();
            txtSectorName.Text = dt.Rows[0][1].ToString().Trim();
            
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewSectorMaster.aspx", true);
    }
}