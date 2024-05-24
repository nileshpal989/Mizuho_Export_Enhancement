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


public partial class TF_ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["userName"] == null)
        //{
        //    //    System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)TF_Menu1.FindControl("hdnloginid");

        //    //    Response.Redirect("TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        //}
        //else
        //{
            if (!IsPostBack)
            {
                txtNewPassword.Focus();
                btnSave.Attributes.Add("onclick", "return validateSave();");
                txtNewPassword.Attributes.Add("onkeyup", "return policyCheck(event," + txtNewPassword.ClientID + ");");
                txtReTypePassword.Attributes.Add("onkeyup", "return policyCheck(event," + txtReTypePassword.ClientID + ");");
                txtReTypePassword.Attributes.Add("onblur", "return checkMatchPass();");
                clearControls();
                Encryption objEncryption = new Encryption();
                string _UserName = Request.QueryString["userName"].ToString().Trim();
                txtUserName.Text = _UserName;
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
                fillDetails(_UserName);
            }
        //}
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _mode = "edit";
        string _userName = txtUserName.Text.Trim();

        Encryption objEncryption = new Encryption();
        string _password = objEncryption.encryptplaintext(txtNewPassword.Text.Trim());

        TF_DATA objSave = new TF_DATA();
        _result = objSave.UpdateUserPassword(_userName, _password);
        if (_result == "updated")
        {
            _result = objSave.UpdateUserExpiryDate(_userName);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Message", "alert('Password succesfully updated.');window.location.href='TF_Login.aspx?UserName=" + _userName + "';", true);          
        }
        else
        {
            Response.Redirect("TF_Login.aspx", true);
        }
    }
    protected void clearControls()
    {
        txtUserName.Text = "";
        txtPassword.Text = "";
        txtNewPassword.Text = "";
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
            txtReTypePassword.Text = "";
            // txtReTypePassword.Text = _password;          
            txtPassword.Attributes.Add("value", _password);
            txtReTypePassword.Attributes.Add("value", "");
        }
    }
}
