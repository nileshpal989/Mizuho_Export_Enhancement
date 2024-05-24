using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class EXP_BankChargesMaster : System.Web.UI.Page
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
                txtbankCertificate.Focus();
                fillDetails();
                txtbankCertificate.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtNegotiation.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtCourier.Attributes.Add("onkeydown", "return validate_Number(event);");
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string query = "TF_UpdateBankChargesMaster";
        TF_DATA objSave = new TF_DATA();
        string user = Session["userName"].ToString();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        SqlParameter bankcert = new SqlParameter("@bankcert", SqlDbType.VarChar);
        bankcert.Value = txtbankCertificate.Text;

        SqlParameter negotiation = new SqlParameter("@negotiation", SqlDbType.VarChar);
        negotiation.Value = txtNegotiation.Text;

        SqlParameter postage = new SqlParameter("@postage", SqlDbType.VarChar);
        postage.Value = txtCourier.Text;

        SqlParameter updatedby = new SqlParameter("@user", SqlDbType.VarChar);
        updatedby.Value = user;

        SqlParameter updateddate = new SqlParameter("@updateDate", SqlDbType.VarChar);
        updateddate.Value = _uploadingDate;

        string _result = objSave.SaveDeleteData(query, bankcert, negotiation, postage, updatedby, updateddate);

        if (_result == "updated")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Record Updated');", true);
        }
        else
        {
            labelMessage.Text = _result;
        }
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/EXP/EXP_Main.aspx", true);
    }

    protected void fillDetails()
    {
        string query = "TF_GetBankChargesDetails";
        TF_DATA objData = new TF_DATA();
        DataSet ds = objData.databind(query);
        try
        {
            txtbankCertificate.Text = ds.Tables[0].Rows[0]["C_RATE1"].ToString();
            txtNegotiation.Text = ds.Tables[0].Rows[0]["C_RATE2"].ToString();
            txtCourier.Text = ds.Tables[0].Rows[0]["C_RATE3"].ToString();
        }
        catch(Exception ex)
        {
        }
    }

}