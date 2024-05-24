using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class TF_AddEditBusinessSourceMaster : System.Web.UI.Page
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
               string ACId  = Session["BranchI"].ToString();
               //txtID.Text = ACId.Substring(0, 1);
               txtBusinessSourceID.Text = ACId.Substring(0,1);

                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("TF_ViewBusinessSourceMaster.aspx", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtBusinessSourceID.Text = Request.QueryString["businessSourceID"].Trim();
                        txtBusinessSourceID.Enabled = false;
                        fillDetails(Request.QueryString["businessSourceID"].Trim());
                    }
                    else
                    {
                        txtBusinessSourceID.Enabled = true;
                    }
                }
                if (Session["BranchS"].ToString() != null)
                {

                    Button2.Text = Session["BranchS"].ToString();
                }
                btnSave.Attributes.Add("onclick", "return validateSave();");
                txtBusinessSourceID.Attributes.Add("onblur", "return Myfunction2();");
               // txtBusinessSourceID.Attributes.Add("onblur", "return Myfunction();");
              

            }
        }

    }
    public void clearall()
    {
        txtBusinessSourceID.Text = "";
        txtName.Text = "";
        txtDesignation.Text = "";
    }
    protected void txtName_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _userName = Session["userName"].ToString();
        string _mode = Request.QueryString["mode"].Trim();
        string _BranchCode = Request.QueryString["BranchCode"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        
        string _BusinessSourceID =   txtBusinessSourceID.Text.Trim();
        string _Name = txtName.Text.Trim();
        string _Designation = txtDesignation.Text.Trim();


        SqlParameter p1 = new SqlParameter("@mode", SqlDbType.VarChar);
        p1.Value = _mode;
        SqlParameter p2 = new SqlParameter("@branchcode", SqlDbType.VarChar);
        p2.Value = _BranchCode;
        SqlParameter p3 = new SqlParameter("@businessSourceID", SqlDbType.VarChar);
        p3.Value = _BusinessSourceID;
        SqlParameter p4 = new SqlParameter("@name", SqlDbType.VarChar);
        p4.Value = _Name;
        SqlParameter p5 = new SqlParameter("@designation", SqlDbType.VarChar);
        p5.Value = _Designation;
        SqlParameter p6 = new SqlParameter("@user", SqlDbType.VarChar);
        p6.Value = _userName;
        SqlParameter p7 = new SqlParameter("@uploadeddate", SqlDbType.VarChar);
        p7.Value = _uploadingDate;

        string _query = "TF_UpdateBusinessSourceMaster";
        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData(_query, p1, p2 ,p3, p4, p5,p6,p7);



        string _script = "";
        if (_result == "added")
        {
            _script = "window.location='TF_ViewBusinessSourceMaster.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (_result == "updated")
            {
                _script = "window.location='TF_ViewBusinessSourceMaster.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            //else
            //    //labelMessage.Text = _result;
        }

        clearall();
    }
   

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewBusinessSourceMaster.aspx", true);
    }
    protected void fillDetails(string _businessSourceID)
    {
        SqlParameter p1 = new SqlParameter("@businessSourceID", SqlDbType.VarChar);
        p1.Value = _businessSourceID;
        string _query = "TF_GetBusinessSourceMasterDetails";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            //txtID.Text = dt.Rows[0]["BUSINESS_SOURCE_ID"].ToString().Trim().Substring(0,1);
            //txtBusinessSourceID.Text = dt.Rows[0]["BUSINESS_SOURCE_ID"].ToString().Trim().Substring(1);
            txtBusinessSourceID.Text = dt.Rows[0]["BUSINESS_SOURCE_ID"].ToString().Trim();
            txtName.Text = dt.Rows[0]["NAME"].ToString().Trim();
            txtDesignation.Text = dt.Rows[0]["DESIGNATION"].ToString().Trim();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewBusinessSourceMaster.aspx", true);
    }

    protected void txtBusinessSourceID_TextChanged(object sender, EventArgs e)
    {

    }
}
