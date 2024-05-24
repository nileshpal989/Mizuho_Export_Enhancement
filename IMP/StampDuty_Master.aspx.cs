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
public partial class IMP_StampDuty_Master : System.Web.UI.Page
{
    string _NewValue;
    string _OldValues;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("../TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {

                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("TF_ViewStampCharges.aspx", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        txtStampID.Text = Request.QueryString["ID"].Trim();
                        txtEffectiveDate.Text = Request.QueryString["effectDate"].Trim();
                        fillDetails(Request.QueryString["effectDate"].Trim(), Request.QueryString["ID"].Trim());
                        btncalendar_DocDate.Enabled = false;
                        txtEffectiveDate.Enabled = false;
                    }
                    else
                    {
                        txtEffectiveDate.Focus();
                        LastDate();
                        LastStampDuty_ID();

                    }

                }
                txtTenor.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtUptoTenor.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtStampID.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtRates.Attributes.Add("onkeydown", "return validate_Number(event);");
            }
            btnSave.Attributes.Add("onclick", "return validateSave();");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _script;
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _mode = Request.QueryString["mode"].Trim();

        TF_DATA objdata = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@EffectiveDate", SqlDbType.VarChar);
        p1.Value = txtEffectiveDate.Text;

        SqlParameter p2 = new SqlParameter("@StampID", SqlDbType.VarChar);
        p2.Value = txtStampID.Text;

        SqlParameter p3 = new SqlParameter("@StampDesc", SqlDbType.VarChar);
        p3.Value = txtDesc.Text;

        SqlParameter p4 = new SqlParameter("@tenor", SqlDbType.VarChar);
        p4.Value = txtTenor.Text;

        SqlParameter pUptotenor = new SqlParameter("@uptotenor", SqlDbType.VarChar);
        pUptotenor.Value = txtUptoTenor.Text;

        SqlParameter p5 = new SqlParameter("@rates", SqlDbType.VarChar);
        p5.Value = Convert.ToDecimal(txtRates.Text);

        SqlParameter p6 = new SqlParameter("@mode", SqlDbType.VarChar);
        p6.Value = _mode;

        SqlParameter pUser = new SqlParameter("@user", SqlDbType.VarChar);
        pUser.Value = _userName;

        SqlParameter pUploadDate = new SqlParameter("@uploadDate", SqlDbType.VarChar);
        pUploadDate.Value = _uploadingDate;

        string _result = objdata.SaveDeleteData("TF_IMP_AddEditStampCharges", p1, p2, p3, p4, p5, p6,pUptotenor, pUser, pUploadDate);


        ///////////Audit Trail////////////////////////////////
        SqlParameter A1 = new SqlParameter("@OldValues", SqlDbType.VarChar);
        SqlParameter A2 = new SqlParameter("@NewValues", SqlDbType.VarChar);
        SqlParameter A3 = new SqlParameter("@ModType", SqlDbType.VarChar);
        SqlParameter A4 = new SqlParameter("@ModifiedBy", Session["userName"].ToString().Trim());
        SqlParameter A5 = new SqlParameter("@ModifiedDate", "");
        SqlParameter A6 = new SqlParameter("@MenuName", "Stamp Duty master");
        SqlParameter A7 = new SqlParameter("@Mode", SqlDbType.VarChar);
        SqlParameter A8 = new SqlParameter("@RefNo", SqlDbType.VarChar);

        if (_result == "added")
        {
            _NewValue = "Description : " + txtDesc.Text.Trim()+" ; Tenor : "+txtTenor.Text.Trim()+ " ; Rates : "+txtRates.Text.Trim()+" ; ID : "+txtStampID.Text.Trim()+" ; Effective Date : "+txtEffectiveDate.Text.Trim();

            A1.Value = "";
            A2.Value = _NewValue;
            A3.Value = "IMP";
            A7.Value = "A";
            A8.Value = txtStampID.Text;
            string p = objdata.SaveDeleteData("TF_Mizuho_IMP_AuditTrail_master", A1, A2, A3, A4, A5, A6, A7, A8);



            _script = "window.location='TF_ViewStampCharges.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", _script, true);
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Record Added')", true);
            clearData();
        }
        else
        {
            int isneedtolog = 0;
            if (_result == "updated")
            {
                _OldValues = "Description : " + hdnstampdutydescription.Value.Trim() + " ; Tenor :" + hdntenior.Value.Trim() + " ; Rates :" +hdnrate.Value.Trim();
                if (hdnstampdutydescription.Value != txtDesc.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = "Description : " + txtDesc.Text.Trim();
                    }
                    else
                    {
                        _NewValue = "Description : " + txtDesc.Text.Trim();
                    }
                }

                if (hdntenior.Value != txtTenor.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; Tenor : " + txtTenor.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; Tenor : " + txtTenor.Text.Trim();
                    }
                }
                if (hdnrate.Value != txtRates.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; Rates : " + txtRates.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; Rates : " + txtRates.Text.Trim();
                    }
                }

                A1.Value = _OldValues;
                A2.Value = _NewValue;
                A3.Value = "IMP";
                A7.Value = "M";
                A8.Value = txtStampID.Text;

                if (isneedtolog == 1)
                {
                    string p = objdata.SaveDeleteData("TF_Mizuho_IMP_AuditTrail_master", A1, A2, A3, A4, A5, A6, A7, A8);
                }

                _script = "window.location='TF_ViewStampCharges.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", _script, true);
            }
            else
                //labelMessage.Text = _result;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "VAlert('" + _result + "','#txtStampID');", true);
        }

    }
    protected void clearData()
    {
        txtStampID.Text = "";
        txtDesc.Text = "";
        txtTenor.Text = "";
        txtUptoTenor.Text = "";
        txtRates.Text = "";
    }
    protected void fillDetails(string _EffectiveDate, string StampID)
    {
        SqlParameter p1 = new SqlParameter("@EffectiveDate", SqlDbType.VarChar);
        p1.Value = _EffectiveDate;
        SqlParameter p2 = new SqlParameter("@ID", SqlDbType.VarChar);
        p2.Value = StampID;
        string _query = "TF_IMP_GetStampDutyChargesDetails";
        TF_DATA objdata = new TF_DATA();
        DataTable dt = objdata.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            txtStampID.Text = dt.Rows[0]["ID"].ToString();
            txtStampID.Enabled = false;
            txtEffectiveDate.Text = dt.Rows[0]["Effective_Date"].ToString();
            txtDesc.Text = dt.Rows[0]["Description"].ToString();
            txtTenor.Text = dt.Rows[0]["TenorDays"].ToString();
            txtUptoTenor.Text = dt.Rows[0]["UptoTenorDays"].ToString();
            txtRates.Text = dt.Rows[0]["Rates"].ToString();

            /*----------------Audit trial ---------------------- */
            hdnstampdutydescription.Value = dt.Rows[0]["Description"].ToString();
            hdntenior.Value = dt.Rows[0]["TenorDays"].ToString();
            hdnrate.Value = dt.Rows[0]["Rates"].ToString();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ViewStampCharges.aspx", true);
    }
    public void LastDate()
    {
         TF_DATA objdata = new TF_DATA();
         DataTable dt = objdata.getData("TF_IMP_GetStampDutyLastEffective_Date");
        if (dt.Rows.Count > 0)
        {
            txtEffectiveDate.Text = dt.Rows[0]["Effective_Date"].ToString();
        }
    }
    public void LastStampDuty_ID()
    {
        SqlParameter p1 = new SqlParameter("@Effective_Date", SqlDbType.VarChar);
        p1.Value = txtEffectiveDate.Text;
        TF_DATA objdata = new TF_DATA();
        DataTable dt = objdata.getData("TF_IMP_GetMaxStampDutyID_For_Effective_Date", p1);
        if (dt.Rows.Count > 0)
        {
            txtStampID.Text = dt.Rows[0]["ID"].ToString();
        }
    }
    protected void txtEffectiveDate_TextChanged(object sender, EventArgs e)
    {
        LastStampDuty_ID();
    }
}