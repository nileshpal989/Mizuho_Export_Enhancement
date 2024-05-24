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


public partial class TF_AddEditUser : System.Web.UI.Page
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
                txtUserName.Focus();
                fillBranch();
                txtPassword.Attributes.Add("onkeyup", "return policyCheck(event," + txtPassword.ClientID + ");");
                txtReTypePassword.Attributes.Add("onkeyup", "return policyCheck(event," + txtReTypePassword.ClientID + ");");
                //txtReTypePassword.Attributes.Add("onkeyup", "return checkMatchPass();");
                txtReTypePassword.Attributes.Add("onblur", "return checkMatchPass();");
                btnSave.Attributes.Add("onclick", "return validateSave();");
                clearControls();
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("TF_ViewHouseKeeping.aspx", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {

                        btnChangePaswd.Enabled = true;
                        txtUserName.Text = Request.QueryString["username"].Trim();
                        txtUserName.Enabled = false;
                        txtPassword.Enabled = false;
                        txtReTypePassword.Enabled = false;
                        fillDetails(Request.QueryString["username"].Trim());
                    }
                    else
                    {
                       
                        btnChangePaswd.Enabled = false;
                        txtUserName.Enabled = true;
                    }
                }

                // txtPassword.Attributes.Add("onkeyup", "PasswordChanged(this);");
                btnSave.Attributes.Add("onclick", "return validateSave();");
                //if (hdntype.Value == "Pwsd")
                //{
                //    btnSave.Attributes.Add("onclick", "return validatePaswd();");
                //}
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewHouseKeeping.aspx", true);
    }
    protected void btnChangePaswd_Click(object sender, EventArgs e)
    {
        fillDetails(Request.QueryString["username"]);
        txtUserName.Enabled = false;
        ddlRole.Enabled = false;
        ddlStatus.Enabled = false;

        txtPassword.Enabled = true;
        txtReTypePassword.Enabled = true;

        txtPassword.Text = "";
        txtReTypePassword.Text = "";

        hdntype.Value = "Pwsd";
        btnChangePaswd.Enabled = false;
        txtPassword.Focus();
    }
    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _mode = Request.QueryString["mode"].Trim();
        string _userName = txtUserName.Text.Trim();
        string _role = ddlRole.SelectedValue.Trim();
        string _active = ddlStatus.SelectedValue.Trim();
        string _adcode = ddlBranch.SelectedValue.Trim();
		string _emailID ="";
		if(txtEmailID.Text.Trim()!="")
		{
           _emailID = txtEmailID.Text.Trim() + txtEmailID1.Text.Trim();
		}
       
        Encryption objEncryption = new Encryption();
        string _password = objEncryption.encryptplaintext(txtPassword.Text.Trim());


        TF_DATA objUpdateExDt = new TF_DATA();
        string s = "";

        TF_DATA objSave = new TF_DATA();
      

        if (hdntype.Value == "Pwsd")
        {
            _result = objSave.UpdateUserPassword(_userName, _password);
            labelMessage.Text = "Password " + _result;

            s = objUpdateExDt.UpdateUserExpiryDate(_userName);

            txtUserName.Enabled = true;
            ddlRole.Enabled = true;
            ddlStatus.Enabled = true;

            txtPassword.Enabled = false;
            txtReTypePassword.Enabled = false;
            hdntype.Value = "";
            btnChangePaswd.Enabled = true;
            fillDetails(Request.QueryString["username"]);
        }
        else
        {
            _result = objSave.addUpdateUserDetails(_mode, _userName, _password, _role, _active,_adcode,_emailID);
            s = objUpdateExDt.UpdateUserExpiryDate(_userName);
            string _script = "";
            if (_result == "added")
            {
                _script = "window.location='TF_ViewHouseKeeping.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
            {
                if (_result == "updated")
                {
                    _script = "window.location='TF_ViewHouseKeeping.aspx?result=" + _result + "'";
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                }
                else
                    labelMessage.Text = _result;
            }
        }


    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewHouseKeeping.aspx", true);
    }
    protected void clearControls()
    {
        txtUserName.Text = "";
        txtPassword.Text = "";
        ddlRole.SelectedIndex = -1;
        ddlStatus.SelectedIndex = -1;
        hdntype.Value = "";
        hdnpswd.Value = "";
        txtEmailID.Text = "";
    }
    protected void fillDetails(string _userName)
    {
        TF_DATA objData = new TF_DATA();
        Encryption objEncryption = new Encryption();
        string _password = "";
        DataTable dt = objData.getUserDetails(_userName);
        if (dt.Rows.Count > 0)
        {
            _password = objEncryption.decrypttext(dt.Rows[0][1].ToString().Trim());
            txtPassword.Text = _password;
            hdnpswd.Value = _password;
            txtReTypePassword.Text = _password;
            txtPassword.Attributes.Add("value", _password);
            txtReTypePassword.Attributes.Add("value", _password);
            ddlRole.SelectedValue = dt.Rows[0][2].ToString().Trim();
            ddlStatus.SelectedValue = dt.Rows[0][3].ToString().Trim();
            ddlBranch.SelectedValue = dt.Rows[0]["ADCode"].ToString().Trim();
            string input = dt.Rows[0]["EmailID"].ToString().Trim();
            int index = input.IndexOf("@");
            if (index > 0)
            {
                input = input.Substring(0, index);
            }
            txtEmailID.Text = input;
        }
    }
}
