using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EDPMS_AddEditErrorCodeMaster : System.Web.UI.Page
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
                txtError_Code.Text = Request.QueryString["Error_Code"].ToString();
                txtError_Code.Enabled=false;
                txtFieldName.Text = Request.QueryString["FieldName"].ToString();
                txtFieldName.Enabled=false;
                txtErrorDescription.Focus();
                fillDetails(Request.QueryString["Error_Code"].ToString(), Request.QueryString["FieldName"].ToString());
                btnSave.Attributes.Add("onclick", "return validateSave();");

            }
        }

    }

    void fillDetails(string errorcode,string fieldname)
    {
        TF_DATA objData = new TF_DATA();
        string _query = "EDPMS_GetErrorCodeDetails";

        SqlParameter p1 = new SqlParameter("@ErrorCode", SqlDbType.VarChar);
        p1.Value = errorcode;

        SqlParameter p2 = new SqlParameter("@FieldName", SqlDbType.VarChar);
        p2.Value = fieldname;
            
        DataTable dt = objData.getData(_query, p1,p2);
        if (dt.Rows.Count > 0)
        {
            txtErrorDescription.Text = dt.Rows[0]["ErrorDesc"].ToString().Trim();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _Error_Code = txtError_Code.Text.Trim();
        string _XML_Field = txtFieldName.Text.Trim();
        string _ErrorDesc = txtErrorDescription.Text.Trim();

        TF_DATA objSave = new TF_DATA();
        string _query = "EDPMS_UpdateError_Code";

        SqlParameter p1 = new SqlParameter("@Error_Code", SqlDbType.VarChar);
        p1.Value = _Error_Code;
        SqlParameter p2 = new SqlParameter("@XML_Field", SqlDbType.VarChar);
        p2.Value = _XML_Field;
        SqlParameter p3 = new SqlParameter("@Error_Desc", SqlDbType.VarChar);
        p3.Value = _ErrorDesc;
        SqlParameter p4 = new SqlParameter("@UpdatedBy", SqlDbType.VarChar);
        p4.Value = _userName;
        SqlParameter p5 = new SqlParameter("@UpdatedTime", SqlDbType.VarChar);
        p5.Value = _uploadingDate;
        _result = objSave.SaveDeleteData(_query, p1, p2, p3, p4,p5);
        string _script = "";
        if (_result == "Updated")
          {
              _script = "window.location='EDPMS_View_Error_Code_Master.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
           }
            else
                labelMessage.Text = _result;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("EDPMS_View_Error_Code_Master.aspx", true);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EDPMS_View_Error_Code_Master.aspx", true);
    }


}