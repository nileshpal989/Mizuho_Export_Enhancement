using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EXP_EXP_AddEditAuthorisedSignatory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("EXP_ViewAutorisedSignatory.aspx", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtBranchname.Text = Request.QueryString["type"].ToString();
                        txtauthosign.Text = Request.QueryString["name"].ToString();
                        txtauthosign.Enabled = false;
                        fillDetails(txtBranchname.Text,txtauthosign.Text);
                    }
                    else
                    {
                        txtBranchname.Text = Request.QueryString["type"].ToString();
                    }
                }
                txtauthosign.Focus();
            }
        }
    }

    protected void fillDetails(string branchname,string name)
    {
        string query = "TF_GetAuthorisedSignatoryDetails";
        SqlParameter p1 = new SqlParameter("@branchname", SqlDbType.VarChar);
        p1.Value = branchname;
        SqlParameter p2 = new SqlParameter("@name", SqlDbType.VarChar);
        p2.Value = name;
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(query, p1,p2);
        if (dt.Rows.Count > 0)
        {
            //txtauthosign.Text = dt.Rows[0]["C_DESCRIPTION"].ToString();
            txtDesignation.Text = dt.Rows[0]["C_TEXT1"].ToString();
            txtEmailID.Text = dt.Rows[0]["C_TEXT2"].ToString();
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("EXP_ViewAutorisedSignatory.aspx", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _script = "";
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _mode = Request.QueryString["mode"].Trim();
        TF_DATA objSave = new TF_DATA();
        string _query = "TF_UpdateAuthorisedSignatoryMaster";
        SqlParameter pMode = new SqlParameter("@mode", SqlDbType.VarChar);
        pMode.Value = _mode;
        SqlParameter branch = new SqlParameter("@branchname", SqlDbType.VarChar);
        branch.Value = txtBranchname.Text;
        SqlParameter authosignatory = new SqlParameter("@authosignatory", SqlDbType.VarChar);
        authosignatory.Value = txtauthosign.Text;
        SqlParameter designation = new SqlParameter("@designation", SqlDbType.VarChar);
        designation.Value = txtDesignation.Text;
        SqlParameter emailid = new SqlParameter("@emailid", SqlDbType.VarChar);
        emailid.Value = txtEmailID.Text;
        _result = objSave.SaveDeleteData(_query, pMode, branch, authosignatory,designation,emailid);
        if (_result == "added")
        {
            _script = "window.location='EXP_ViewAutorisedSignatory.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (_result == "updated")
            {
                _script = "window.location='EXP_ViewAutorisedSignatory.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
                labelMessage.Text = _result;
        }
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("EXP_ViewAutorisedSignatory.aspx", true);
    }
}