using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class IMP_TF_IMP_Nostro_Master : System.Web.UI.Page
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
                fillddlCurrency();
                txtACtype.Enabled = false;
                if (Request.QueryString["mode"].Trim() != "add")
                {
                    txtCustABBR.Text = Request.QueryString["CustABBR"].Trim();
                    txtCustABBR.Enabled = false;
                    fillDetails(Request.QueryString["CustABBR"].Trim());
                }
                else
                {
                    txtACtype.Text = "NOSTRO";
                }
                txtGLcode.Attributes.Add("onkeydown", "return validate_Number(event);");
                btnSave.Attributes.Add("onclick", "return validation();");
            }
        }
    }
    public void fillddlCurrency()
    {
        TF_DATA objData = new TF_DATA();
        string _query = "TF_IMP_Currency_List";
        DataTable dt = objData.getData(_query);
        ddlCurrency.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "Select";
        if (dt.Rows.Count > 0)
        {
            li.Text = "Select";

            ddlCurrency.DataSource = dt.DefaultView;
            ddlCurrency.DataTextField = "C_Code";
            ddlCurrency.DataValueField = "C_Code";
            ddlCurrency.DataBind();
        }
    }
    //public void fillddlCUSTABBR()
    //{
    //    TF_DATA objData = new TF_DATA();
    //    string _query = "TF_IMP_get_CUSTOMERmaster_List";
    //    DataTable dt = objData.getData(_query);
    //    ddlCustABBR.Items.Clear();
    //    ListItem li = new ListItem();
    //    li.Value = "--Select--";
    //    if (dt.Rows.Count > 0)
    //    {
    //        li.Text = "--Select--";

    //        ddlCustABBR.DataSource = dt.DefaultView;
    //        ddlCustABBR.DataTextField = "Cust_Abbr";
    //        ddlCustABBR.DataValueField = "Cust_Abbr";
    //        ddlCustABBR.DataBind();
    //    }
    //}
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        TF_DATA objSave = new TF_DATA();
        string _query = "TF_IMP_AddEdit_NostroMaster";

        SqlParameter p1 = new SqlParameter("@CUST_ABBR", SqlDbType.VarChar);
        p1.Value = txtCustABBR.Text.Trim();
        SqlParameter p2 = new SqlParameter("@CURR", SqlDbType.VarChar);
        p2.Value = ddlCurrency.SelectedItem.Text.Trim();
        SqlParameter p3 = new SqlParameter("@GL_CODE", SqlDbType.VarChar);
        p3.Value = txtGLcode.Text.Trim();
        SqlParameter p4 = new SqlParameter("@AC_No", SqlDbType.VarChar);
        p4.Value = txtACNo.Text.Trim();
        SqlParameter p5 = new SqlParameter("@SWIFT_CODE", SqlDbType.VarChar);
        p5.Value = txtSwiftCode.Text.Trim();
        SqlParameter p6 = new SqlParameter("@AC_Type", SqlDbType.VarChar);
        p6.Value = txtACtype.Text.Trim();
        SqlParameter p7 = new SqlParameter("@Nostro_AC_No", SqlDbType.VarChar);
        p7.Value = txtNostroACno.Text.Trim();
        SqlParameter p8 = new SqlParameter("@Bank_Name", SqlDbType.VarChar);
        p8.Value = txtBankName.Text.Trim();
        SqlParameter p9 = new SqlParameter("@user", SqlDbType.VarChar);
        p9.Value = _userName;
        SqlParameter p10 = new SqlParameter("@uploadDate", SqlDbType.DateTime);
        p10.Value = _uploadingDate;

        _result = objSave.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);

        string _script = "";
        if (_result == "added")
        {
            _script = "window.location='TF_IMP_NostroMaster_View.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (_result == "Updated")
            {
                _script = "window.location='TF_IMP_NostroMaster_View.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
        }
        }
    protected void txtCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_IMP_NostroMaster_View.aspx", true);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_IMP_NostroMaster_View.aspx", true);
    }
    protected void fillDetails(string CustABBR)
    { 
     SqlParameter p1 = new SqlParameter("@CUST_ABBR", SqlDbType.VarChar);
        p1.Value = CustABBR;
        string _query = "TF_IMP_GetNostroMasterDetails";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            txtCustABBR.Text = dt.Rows[0]["CUST_ABBR"].ToString().Trim();
            ddlCurrency.SelectedItem.Text = dt.Rows[0]["CURR"].ToString().Trim();
            txtGLcode.Text = dt.Rows[0]["GL_CODE"].ToString().Trim();
            txtACNo.Text = dt.Rows[0]["AC_No"].ToString().Trim();
            txtSwiftCode.Text = dt.Rows[0]["SWIFT_CODE"].ToString().Trim();
            txtACtype.Text = dt.Rows[0]["AC_Type"].ToString().Trim();
            txtNostroACno.Text = dt.Rows[0]["Nostro_AC_No"].ToString().Trim();
            txtBankName.Text = dt.Rows[0]["Bank_Name"].ToString().Trim();
        }
    }
}