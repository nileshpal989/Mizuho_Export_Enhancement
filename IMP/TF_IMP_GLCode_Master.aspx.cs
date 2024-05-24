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

public partial class IMP_TF_IMP_GLCode_Master : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");
            Response.Redirect("~/TF_Login.aspx?Sessionout=yes&Sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("TF_IMP_GLCode_Master_View.aspx", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtGLCode.Text = Request.QueryString["GlCode"].Trim();
                        txtGLCode.Enabled = false;
                        fillDetails(Request.QueryString["GlCode"].Trim());
                    }
                    else
                    {
                        txtGLCode.Enabled = true;
                        txtGLCode.Focus();
                        txtGLCurr.Text = "INR";
                    }
                }
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string result = "";
        TF_DATA objSave = new TF_DATA();
        string _query = "TF_IMP_AddUpdateGLCodeMaster";

        SqlParameter pmode = new SqlParameter("@Mode", SqlDbType.VarChar);
        pmode.Value = Request.QueryString["mode"].Trim();

        SqlParameter pGLCode = new SqlParameter("@GL_CODE", SqlDbType.VarChar);
        pGLCode.Value = txtGLCode.Text.Trim();

        SqlParameter pGLCodeDiscreption = new SqlParameter("@GL_CODE_Description", SqlDbType.VarChar);
        pGLCodeDiscreption.Value = txtGLCodeDiscreption.Text.Trim();

        SqlParameter pGLCurr = new SqlParameter("@GL_Curr", SqlDbType.VarChar);
        pGLCurr.Value = txtGLCurr.Text.Trim();

        SqlParameter puserName = new SqlParameter("@User", SqlDbType.VarChar);
        puserName.Value = Session["userName"].ToString().Trim();

        SqlParameter pAddedDate = new SqlParameter("@AddedDate", SqlDbType.VarChar);
        pAddedDate.Value = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        result = objSave.SaveDeleteData(_query, pmode, pGLCode, pGLCodeDiscreption, pGLCurr, puserName, pAddedDate);
        string _script = "";
        if (result == "added")
        {
            _script = "window.location='TF_IMP_GLCode_Master_View.aspx?result=" + result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (result == "updated")
            {
                _script = "window.location='TF_IMP_GLCode_Master_View.aspx?result=" + result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
                labelMessage.Text = result;
        }
    }
    protected void fillDetails(string GL_CODE)
    {
        TF_DATA objData = new TF_DATA();
        string _query = "TF_IMP_GLCodeMaster_Details";

        SqlParameter pGL_CODE = new SqlParameter("@GL_CODE", SqlDbType.VarChar);
        pGL_CODE.Value = GL_CODE;

        DataTable dt = objData.getData(_query, pGL_CODE);
        if (dt.Rows.Count > 0)
        {
            txtGLCodeDiscreption.Text = dt.Rows[0]["GL_CODE_Description"].ToString().Trim();
            txtGLCurr.Text = dt.Rows[0]["GL_Curr"].ToString().Trim();
            btnSave.Focus();
        }
        else
        {
            txtGLCodeDiscreption.Focus();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_IMP_GLCode_Master_View.aspx", true);
    }
    protected void txtGLCode_TextChanged(object sender, EventArgs e)
    {
        fillDetails(txtGLCode.Text.Trim());
    }
}