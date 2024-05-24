using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class EXPORT_AddEditInterestRateMaster : System.Web.UI.Page
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
                txtSightMaxDays.Focus();
                fillDetails();
                //txtRemAmount.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtSightMaxDays.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtSightOutDays.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtSightDayRange1.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtSightDayRange2.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtSightInterestRate.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtSightOverdueInterest.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtUsanceMaxDays.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtUsanceOutDays.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtUsanceDayRange1.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtUsanceDayRange2.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtUsanceDayRange3.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtUsanceDayRange4.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtUsanceInterestRate1.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtUsanceInterestRate2.Attributes.Add("onkeydown", "return validate_Number(event);");
                TxtUsanceOverdueInterest1.Attributes.Add("onkeydown", "return validate_Number(event);");
                TxtUsanceOverdueInterest2.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtEBROutDays.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtLiborInterestRate.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtLiborOverdueInterest.Attributes.Add("onkeydown", "return validate_Number(event);");

            }
        }
    }

    protected void fillDetails()
    {
        string query = "TF_GetInterestRateDetails";
        TF_DATA objData = new TF_DATA();
        DataSet ds=objData.databind(query);
        DataTable dt=new DataTable();
        try
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                //FOR SIGHT BILLS ONLY.
                txtSightMaxDays.Text = ds.Tables[0].Rows[0]["C_RATE1"].ToString();
                txtSightOutDays.Text = ds.Tables[0].Rows[0]["C_RATE2"].ToString();
                txtSightDayRange1.Text = ds.Tables[0].Rows[0]["C_RATE3"].ToString();
                txtSightDayRange2.Text = ds.Tables[0].Rows[0]["C_RATE4"].ToString();
                txtSightInterestRate.Text = ds.Tables[0].Rows[0]["C_RATE5"].ToString();
                txtSightOverdueInterest.Text = ds.Tables[0].Rows[0]["C_RATE6"].ToString();

                //FOR USANCE BILLS ONLY.
                txtUsanceMaxDays.Text = ds.Tables[0].Rows[1]["C_RATE1"].ToString();
                txtUsanceOutDays.Text = ds.Tables[0].Rows[1]["C_RATE2"].ToString();
                txtUsanceDayRange1.Text = ds.Tables[0].Rows[1]["C_RATE3"].ToString();
                txtUsanceDayRange2.Text = ds.Tables[0].Rows[1]["C_RATE4"].ToString();
                txtUsanceInterestRate1.Text = ds.Tables[0].Rows[1]["C_RATE5"].ToString();
                TxtUsanceOverdueInterest1.Text = ds.Tables[0].Rows[1]["C_RATE6"].ToString();
                txtUsanceDayRange3.Text = ds.Tables[0].Rows[2]["C_RATE3"].ToString();
                txtUsanceDayRange4.Text = ds.Tables[0].Rows[2]["C_RATE4"].ToString();
                txtUsanceInterestRate2.Text = ds.Tables[0].Rows[2]["C_RATE5"].ToString();
                TxtUsanceOverdueInterest2.Text = ds.Tables[0].Rows[2]["C_RATE6"].ToString();

                //FOR EBR BILLS ONLY.

                txtEBROutDays.Text = ds.Tables[0].Rows[3]["C_RATE2"].ToString();
                txtLiborInterestRate.Text = ds.Tables[0].Rows[3]["C_RATE5"].ToString();
                txtLiborOverdueInterest.Text = ds.Tables[0].Rows[3]["C_RATE6"].ToString();
            }
        }
        catch (Exception)
        {

            //throw;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string query = "TF_UpdateInterestRateMaster";
        TF_DATA objSave = new TF_DATA();
        string user = Session["userName"].ToString();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        SqlParameter smaxdays = new SqlParameter("@smaxdays", SqlDbType.VarChar);
        smaxdays.Value = txtSightMaxDays.Text;

        SqlParameter soutdays = new SqlParameter("@soutdays", SqlDbType.VarChar);
        soutdays.Value = txtSightOutDays.Text;

        SqlParameter srange1 = new SqlParameter("@srange1", SqlDbType.VarChar);
        srange1.Value = txtSightDayRange1.Text;

        SqlParameter srange2 = new SqlParameter("@srange2", SqlDbType.VarChar);
        srange2.Value = txtSightDayRange2.Text;

        SqlParameter sintrate = new SqlParameter("@sintrate", SqlDbType.VarChar);
        sintrate.Value = txtSightInterestRate.Text;

        SqlParameter soverdue = new SqlParameter("@soverdue", SqlDbType.VarChar);
        soverdue.Value = txtSightOverdueInterest.Text;

        SqlParameter umaxdays = new SqlParameter("@umaxdays", SqlDbType.VarChar);
        umaxdays.Value = txtUsanceMaxDays.Text;

        SqlParameter uoutdays = new SqlParameter("@uoutdays", SqlDbType.VarChar);
        uoutdays.Value = txtUsanceOutDays.Text;

        SqlParameter urange1 = new SqlParameter("@urange1", SqlDbType.VarChar);
        urange1.Value = txtUsanceDayRange1.Text;

        SqlParameter urange2 = new SqlParameter("@urange2", SqlDbType.VarChar);
        urange2.Value = txtUsanceDayRange2.Text;

        SqlParameter uintrate1 = new SqlParameter("@uintrate1", SqlDbType.VarChar);
        uintrate1.Value = txtUsanceInterestRate1.Text;

        SqlParameter uoverdue1 = new SqlParameter("@uoverdue1", SqlDbType.VarChar);
        uoverdue1.Value = TxtUsanceOverdueInterest1.Text;

        SqlParameter urange3 = new SqlParameter("@urange3", SqlDbType.VarChar);
        urange3.Value = txtUsanceDayRange3.Text;

        SqlParameter urange4 = new SqlParameter("@urange4", SqlDbType.VarChar);
        urange4.Value = txtUsanceDayRange4.Text;

        SqlParameter uintrate2 = new SqlParameter("@uintrate2", SqlDbType.VarChar);
        uintrate2.Value = txtUsanceInterestRate2.Text;

        SqlParameter uoverdue2 = new SqlParameter("@uoverdue2", SqlDbType.VarChar);
        uoverdue2.Value = TxtUsanceOverdueInterest2.Text;

        SqlParameter eoutdays = new SqlParameter("@eoutdays", SqlDbType.VarChar);
        eoutdays.Value = txtEBROutDays.Text;

        SqlParameter lintrate = new SqlParameter("@lintrate", SqlDbType.VarChar);
        lintrate.Value = txtLiborInterestRate.Text;

        SqlParameter loverdue = new SqlParameter("@loverdue", SqlDbType.VarChar);
        loverdue.Value = txtLiborOverdueInterest.Text;

        SqlParameter username = new SqlParameter("@user", SqlDbType.VarChar);
        username.Value = user;

        SqlParameter updatedby = new SqlParameter("@updatedby", SqlDbType.VarChar);
        updatedby.Value = _uploadingDate;

        string _result = objSave.SaveDeleteData(query, smaxdays, soutdays, srange1, srange2, sintrate, soverdue, umaxdays, uoutdays, urange1, urange2, uintrate1, uoverdue1,
                                              urange3, urange4, uintrate2, uoverdue2, eoutdays, lintrate, loverdue, username, updatedby);
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

}