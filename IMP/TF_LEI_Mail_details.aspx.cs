using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class IMP_Transactions_TF_LEI_Mail_details : System.Web.UI.Page
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
                btnSave.Attributes.Add("onclick", "return validateSave();");
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("TF_ViewLEIEmaildetails.aspx", true);
                }
                else
                {

                    if (Request.QueryString["mode"].Trim() == "edit")
                    {
                        ddlReporttype.Text = Request.QueryString["reporttype"].Trim();
                        ddlModule.Enabled = false;
                        ddlModule.Text = Request.QueryString["module"].Trim();
                        ddlReporttype.Enabled = false;
                        fillDetails();
                    }
                }
            }
        }
    }
    public void clearall()
    {
        ddlModule.Text = "";
        txtCC.Text = "";
        txtBCC.Text = "";
        txtTo.Text = "";
        txtsubject.Text = "";
        txtservicetime1.Text = "";
        txtservicetime2.Text = "";
    }
    public void fillDetails()
    {
        SqlParameter p1 = new SqlParameter("@Module", SqlDbType.VarChar);
        p1.Value = ddlModule.SelectedItem.Text.ToString();
        SqlParameter p2 = new SqlParameter("@ReportType", SqlDbType.VarChar);
        p2.Value = ddlReporttype.SelectedItem.Text.ToString();
        string _query = "TF_Fill_Leiemaildetails";
        TF_DATA objData = new TF_DATA();

        DataTable dt = objData.getData(_query, p1,p2);
        if (dt.Rows.Count > 0)
        {
            txtCC.Text = dt.Rows[0]["Email_CC"].ToString();
            txtBCC.Text = dt.Rows[0]["Email_BCC"].ToString();
            txtTo.Text = dt.Rows[0]["Email_To"].ToString();
            txtsubject.Text = dt.Rows[0]["Email_Subject"].ToString();
            txtservicetime1.Text = dt.Rows[0]["Service_time1"].ToString();
            txtservicetime2.Text = dt.Rows[0]["Service_time2"].ToString();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _userName = Session["userName"].ToString();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _mode = Request.QueryString["mode"].Trim();
        string _Reporttype = ddlReporttype.SelectedItem.Text.ToString().Trim();
        string _Module = ddlModule.SelectedItem.Text.ToString().Trim();
        string _To = txtTo.Text.Trim();
        string _CC = txtCC.Text.Trim();
        string _BCC = txtBCC.Text.Trim();
        string _Subject = txtsubject.Text.Trim();
        string _Servicetime = txtservicetime1.Text.Trim()+":"+ txtservicetime2.Text.Trim();

        SqlParameter p1 = new SqlParameter("@Module", SqlDbType.VarChar);
        p1.Value = _Module;
        SqlParameter p10 = new SqlParameter("@ReportType", SqlDbType.VarChar);
        p10.Value = _Reporttype;
        SqlParameter p2 = new SqlParameter("@To", SqlDbType.VarChar);
        p2.Value = _To;
        SqlParameter p3 = new SqlParameter("@CC", SqlDbType.VarChar);
        p3.Value = _CC;
        SqlParameter p4 = new SqlParameter("@BCC", SqlDbType.VarChar);
        p4.Value = _BCC;
        SqlParameter p5 = new SqlParameter("@Subject", SqlDbType.VarChar);
        p5.Value = _Subject;
        SqlParameter p6 = new SqlParameter("@USER", SqlDbType.VarChar);
        p6.Value = _userName;
        SqlParameter p7 = new SqlParameter("@UPLOADINGDATE", SqlDbType.VarChar);
        p7.Value = _uploadingDate;
        SqlParameter p8 = new SqlParameter("@Servicetime", SqlDbType.VarChar);
        p8.Value = _Servicetime;
        SqlParameter p9 = new SqlParameter("@mode", SqlDbType.VarChar);
        p9.Value = _mode;
        
        string _query = "TF_UpdateLEIemaildetails";
        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
        string _script = "";
        if (_result == "added")
        { 
            _script = "window.location='TF_ViewLEIEmaildetails.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", _script, true);
        }
        else if (_result == "updated")
        {
            _script = "window.location='TF_ViewLEIEmaildetails.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", _script, true);
        }
        else
        {
            labelMessage.Text = _result;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewLEIEmaildetails.aspx", true);
    }
}