using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class EXP_AddEditCommissionMaster : System.Web.UI.Page
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
                txtRange1.Focus();
                fillDetails();
                txtRange1.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtRange2.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtRate1.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtMinAmt1.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtRange3.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtRange4.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtRate2.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtMinAmt2.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtRange5.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtRange6.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtRate3.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtMinAmt3.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtRange7.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtRange8.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtRate4.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtMinAmt4.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtRange9.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtRange10.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtRate5.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtMinAmt5.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtRange11.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtRange12.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtRate6.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtMinAmt6.Attributes.Add("onkeydown", "return validate_Number(event);");
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string query = "TF_UpdateExportCommissionDetails";
        TF_DATA objSave = new TF_DATA();
        string user = Session["userName"].ToString();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        SqlParameter range1 = new SqlParameter("@range1", SqlDbType.VarChar);
        range1.Value = txtRange1.Text;

        SqlParameter range2 = new SqlParameter("@range2", SqlDbType.VarChar);
        range2.Value = txtRange2.Text;

        SqlParameter intrate1 = new SqlParameter("@intrate1", SqlDbType.VarChar);
        intrate1.Value = txtRate1.Text;

        SqlParameter minamt1 = new SqlParameter("@minamt1", SqlDbType.VarChar);
        minamt1.Value = txtMinAmt1.Text;

        SqlParameter range3 = new SqlParameter("@range3", SqlDbType.VarChar);
        range3.Value = txtRange3.Text;

        SqlParameter range4 = new SqlParameter("@range4", SqlDbType.VarChar);
        range4.Value = txtRange4.Text;

        SqlParameter intrate2 = new SqlParameter("@intrate2", SqlDbType.VarChar);
        intrate2.Value = txtRate2.Text;

        SqlParameter minamt2 = new SqlParameter("@minamt2", SqlDbType.VarChar);
        minamt2.Value = txtMinAmt2.Text;

        SqlParameter range5 = new SqlParameter("@range5", SqlDbType.VarChar);
        range5.Value = txtRange5.Text;

        SqlParameter range6 = new SqlParameter("@range6", SqlDbType.VarChar);
        range6.Value = txtRange6.Text;

        SqlParameter intrate3 = new SqlParameter("@intrate3", SqlDbType.VarChar);
        intrate3.Value = txtRate3.Text;

        SqlParameter minamt3 = new SqlParameter("@minamt3", SqlDbType.VarChar);
        minamt3.Value = txtMinAmt3.Text;

        SqlParameter range7 = new SqlParameter("@range7", SqlDbType.VarChar);
        range7.Value = txtRange7.Text;

        SqlParameter range8 = new SqlParameter("@range8", SqlDbType.VarChar);
        range8.Value = txtRange8.Text;

        SqlParameter intrate4 = new SqlParameter("@intrate4", SqlDbType.VarChar);
        intrate4.Value = txtRate4.Text;

        SqlParameter minamt4 = new SqlParameter("@minamt4", SqlDbType.VarChar);
        minamt4.Value = txtMinAmt4.Text;

        SqlParameter range9 = new SqlParameter("@range9", SqlDbType.VarChar);
        range9.Value = txtRange9.Text;

        SqlParameter range10 = new SqlParameter("@range10", SqlDbType.VarChar);
        range10.Value = txtRange10.Text;

        SqlParameter intrate5 = new SqlParameter("@intrate5", SqlDbType.VarChar);
        intrate5.Value = txtRate5.Text;

        SqlParameter minamt5 = new SqlParameter("@minamt5", SqlDbType.VarChar);
        minamt5.Value = txtMinAmt5.Text;

        SqlParameter range11 = new SqlParameter("@range11", SqlDbType.VarChar);
        range11.Value = txtRange11.Text;

        SqlParameter range12 = new SqlParameter("@range12", SqlDbType.VarChar);
        range12.Value = txtRange12.Text;

        SqlParameter intrate6 = new SqlParameter("@intrate6", SqlDbType.VarChar);
        intrate6.Value = txtRate6.Text;

        SqlParameter minamt6 = new SqlParameter("@minamt6", SqlDbType.VarChar);
        minamt6.Value = txtMinAmt6.Text;

        SqlParameter commrate = new SqlParameter("@commrate", SqlDbType.VarChar);
        commrate.Value = txtCommissionRate.Text;

        SqlParameter commamt = new SqlParameter("@commamt", SqlDbType.VarChar);
        commamt.Value = txtCommissionMinAmt.Text;

        SqlParameter updatedby = new SqlParameter("@user", SqlDbType.VarChar);
        updatedby.Value = user;

        SqlParameter updatedate = new SqlParameter("@updateDate", SqlDbType.VarChar);
        updatedate.Value = _uploadingDate;

        string _result = objSave.SaveDeleteData(query, range1, range2, intrate1, minamt1, range3, range4, intrate2, minamt2, range5, range6, intrate3, minamt3,
                                                range7, range8, intrate4, minamt4, range9, range10, intrate5, minamt5, range11, range12, intrate6, minamt6,commrate,commamt, updatedby, updatedate);

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
        string query = "TF_GetCommissionDetails";
        TF_DATA objData = new TF_DATA();
        DataSet ds = objData.databind(query);
        try
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtRange1.Text = ds.Tables[0].Rows[0]["C_RATE1"].ToString();
                txtRange2.Text = ds.Tables[0].Rows[0]["C_RATE2"].ToString();
                txtRate1.Text = ds.Tables[0].Rows[0]["C_RATE3"].ToString();
                txtMinAmt1.Text = ds.Tables[0].Rows[0]["C_RATE4"].ToString();

                txtRange3.Text = ds.Tables[0].Rows[1]["C_RATE1"].ToString();
                txtRange4.Text = ds.Tables[0].Rows[1]["C_RATE2"].ToString();
                txtRate2.Text = ds.Tables[0].Rows[1]["C_RATE3"].ToString();
                txtMinAmt2.Text = ds.Tables[0].Rows[1]["C_RATE4"].ToString();

                txtRange5.Text = ds.Tables[0].Rows[2]["C_RATE1"].ToString();
                txtRange6.Text = ds.Tables[0].Rows[2]["C_RATE2"].ToString();
                txtRate3.Text = ds.Tables[0].Rows[2]["C_RATE3"].ToString();
                txtMinAmt3.Text = ds.Tables[0].Rows[2]["C_RATE4"].ToString();

                txtRange7.Text = ds.Tables[0].Rows[3]["C_RATE1"].ToString();
                txtRange8.Text = ds.Tables[0].Rows[3]["C_RATE2"].ToString();
                txtRate4.Text = ds.Tables[0].Rows[3]["C_RATE3"].ToString();
                txtMinAmt4.Text = ds.Tables[0].Rows[3]["C_RATE4"].ToString();

                txtRange9.Text = ds.Tables[0].Rows[4]["C_RATE1"].ToString();
                txtRange10.Text = ds.Tables[0].Rows[4]["C_RATE2"].ToString();
                txtRate5.Text = ds.Tables[0].Rows[4]["C_RATE3"].ToString();
                txtMinAmt5.Text = ds.Tables[0].Rows[4]["C_RATE4"].ToString();

                txtRange11.Text = ds.Tables[0].Rows[5]["C_RATE1"].ToString();
                txtRange12.Text = ds.Tables[0].Rows[5]["C_RATE2"].ToString();
                txtRate6.Text = ds.Tables[0].Rows[5]["C_RATE3"].ToString();
                txtMinAmt6.Text = ds.Tables[0].Rows[5]["C_RATE4"].ToString();

                txtCommissionRate.Text = ds.Tables[0].Rows[5]["C_RATE5"].ToString();
                txtCommissionMinAmt.Text = ds.Tables[0].Rows[5]["C_RATE6"].ToString();
            }
        }
        catch (Exception ex)
        {
        }
    }

}