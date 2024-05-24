using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class TF_AddEditSpecialDates : System.Web.UI.Page
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
                
                btnSave.Attributes.Add("onclick", "return validateSave();");
                clearControls();
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("TF_SpecialDates.aspx", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtHolidayDate.Text = Request.QueryString["sDate"].Trim();
                        txtHolidayDate.Enabled = false;
                        btncalendar_DocDate.Enabled = false;
                        txtToolTip.Text = Request.QueryString["toolTip"].Trim();
                        txtbranch.Text = Request.QueryString["ddlbranch"].Trim();
                        hdnadCode.Value = Request.QueryString["adcode"].Trim();
                    }
                    else
                    {
                        txtbranch.Text = Request.QueryString["ddlbranch"].Trim();
                        hdnadCode.Value = Request.QueryString["adcode"].Trim();
                        txtHolidayDate.Enabled = true;
                        txtHolidayDate.Focus();
                    }
                }
                btnSave.Attributes.Add("onclick", "return validateSave();");
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        TF_DATA obj = new TF_DATA();

        string _query = "TF_UpdateSpecialDates";

        SqlParameter pMode = new SqlParameter("@mode", SqlDbType.VarChar);
        pMode.Value = Request.QueryString["mode"].Trim();

        SqlParameter pBrach = new SqlParameter("@adcode", SqlDbType.VarChar);
        pBrach.Value = Request.QueryString["adcode"].Trim();

        SqlParameter pYear = new SqlParameter("@year", SqlDbType.VarChar);

        string _year = "";
        _year = (txtHolidayDate.Text).Substring(txtHolidayDate.Text.IndexOf("/", txtHolidayDate.Text.IndexOf("/") + 1) + 1, 4);

        pYear.Value = _year;

        SqlParameter pMon = new SqlParameter("@mon", SqlDbType.VarChar);
        string _Mon = "";

        _Mon = (txtHolidayDate.Text).Substring(txtHolidayDate.Text.IndexOf("/") + 1, 2);
        pMon.Value = _Mon;

        SqlParameter pDay = new SqlParameter("@day", SqlDbType.VarChar);
        string _Day = "";

        _Day = (txtHolidayDate.Text).Substring(0, 2);

        pDay.Value = _Day;

        SqlParameter pToolTip = new SqlParameter("@toolTip", SqlDbType.VarChar);
        pToolTip.Value = txtToolTip.Text;

        SqlParameter pUser = new SqlParameter("@user", SqlDbType.VarChar);
        pUser.Value = _userName;

        SqlParameter pUploadDate = new SqlParameter("@uploadDate", SqlDbType.VarChar);
        pUploadDate.Value = _uploadingDate;
        
        string _script = "";
        string _result = obj.SaveDeleteData(_query,pMode,pBrach ,pYear, pMon, pDay, pToolTip,pUser,pUploadDate);
        if (_result == "added")
        {
            _script = "window.location='TF_SpecialDates.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (_result == "updated")
            {
                _script = "window.location='TF_SpecialDates.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
                labelMessage.Text = _result;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_SpecialDates.aspx",true);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_SpecialDates.aspx", true);
    }

    protected void clearControls()
    {
        txtHolidayDate.Text = "";
        txtToolTip.Text = "";
    }
}