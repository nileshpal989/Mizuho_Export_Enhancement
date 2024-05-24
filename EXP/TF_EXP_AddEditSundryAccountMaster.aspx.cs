using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EXP_TF_EXP_AddEditSundryAccountMaster : System.Web.UI.Page
{
    string _NewValue;
    string _OldValues;

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
                sundryAc_Rdbtn.Checked = true;


                clearControls();
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("TF_EXP_SundryAccountMaster.aspx", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        // txtcode.Text = Request.QueryString["AcCode"].Trim();
                        txtSrno.Text = Request.QueryString["SrNo"].Trim();
                        txtcode.Text = Request.QueryString["AcCode"].Trim();
                        txtSrno.Enabled = false;
                        txtcode.Enabled = false;
                        fillDetails(Request.QueryString["AcCode"].Trim(), Request.QueryString["SrNo"].Trim());
                    }
                    else
                    {
                        if (sundryAc_Rdbtn.Checked)
                        {
                            Sundrycountsrno();
                        }

                        if (InteroffAc_Rdbtn.Checked)
                        {
                            InterOffcountsrno();
                        }
                        txtcode.Enabled = true;
                    }
                }
                btnSave.Attributes.Add("onclick", "return validateSave();");

            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_EXP_SundryAccountMaster.aspx", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        string _result = "";
        string sundryaccounttype = string.Empty;
        string introaccounttype = string.Empty;
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _mode = Request.QueryString["mode"].Trim();

        TF_DATA objSave = new TF_DATA();

        if (sundryAc_Rdbtn.Checked)
        {
            sundryaccounttype = "1";
        }
        else
        {
            sundryaccounttype = "0";
        }
        if (InteroffAc_Rdbtn.Checked)
        {
            introaccounttype = "1";
        }
        else
        {
            introaccounttype = "0";
        }

        string _query = "TF_EXP_UpdateSundryAccount";

        SqlParameter p0 = new SqlParameter("@Srno", SqlDbType.VarChar);
        p0.Value = txtSrno.Text.Trim();

        SqlParameter p1 = new SqlParameter("@mode", SqlDbType.VarChar);
        p1.Value = _mode;

        SqlParameter p2 = new SqlParameter("@AcCode", SqlDbType.VarChar);
        p2.Value = txtcode.Text.Trim();

        SqlParameter p3 = new SqlParameter("@Branch", SqlDbType.VarChar);
        p3.Value = txtBranch.Text.Trim().ToUpper();

        SqlParameter p4 = new SqlParameter("@Ccy", SqlDbType.VarChar);
        p4.Value = txtCcy.Text.Trim().ToUpper();

        SqlParameter p5 = new SqlParameter("@CustAbb", SqlDbType.VarChar);
        p5.Value = txtCustabb.Text.Trim().ToUpper();

        SqlParameter p6 = new SqlParameter("@CustAcNo", SqlDbType.VarChar);
        p6.Value = txtCustAcNo.Text.Trim().ToUpper();

        SqlParameter p7 = new SqlParameter("@Description", SqlDbType.VarChar);
        p7.Value = txtdescription.Text.Trim().ToUpper();

        SqlParameter p8 = new SqlParameter("@sundry_flag", SqlDbType.VarChar);
        p8.Value = sundryaccounttype;

        SqlParameter p9 = new SqlParameter("@INterOffice_flag", SqlDbType.VarChar);
        p9.Value = introaccounttype;

        _result = objSave.SaveDeleteData(_query, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9);

        //-----------------------Audit Trail BY NILESH-------------------------------
        SqlParameter A1 = new SqlParameter("@OldValues", SqlDbType.VarChar);
        SqlParameter A2 = new SqlParameter("@NewValues", SqlDbType.VarChar);
        SqlParameter A3 = new SqlParameter("@ModType", SqlDbType.VarChar);
        SqlParameter A4 = new SqlParameter("@ModifiedBy", Session["userName"].ToString().Trim());
        SqlParameter A5 = new SqlParameter("@ModifiedDate", "");
        SqlParameter A6 = new SqlParameter("@MenuName", "Sundry Account Master");
        SqlParameter A7 = new SqlParameter("@Mode", SqlDbType.VarChar);
        SqlParameter A8 = new SqlParameter("@RefNo", SqlDbType.VarChar);
        //--------------------------END BY NILESH-------------------------------


        string _script = "";
        if (_result == "added")
        { //-------------------------- BY NILESH-------------------------------
            _NewValue = "SR No : " + txtSrno.Text.Trim() + "; Code : " + txtcode.Text.Trim() + "; Description : " + txtdescription.Text.Trim()
                         + "; Branch : " + txtBranch.Text.Trim() + "; CCY : " + txtCcy.Text.Trim() + "; Cust Abb : " + txtCustabb.Text.Trim()
                         + "; Account No : " + txtCustAcNo.Text.Trim();
            A1.Value = "";
            A2.Value = _NewValue;
            A3.Value = "IMP";
            A7.Value = "A";
            A8.Value = txtCustAcNo.Text.Trim();
            string p = objSave.SaveDeleteData("TF_Mizuho_IMP_AuditTrail_master", A1, A2, A3, A4, A5, A6, A7, A8);
            //--------------------------END BY NILESH-------------------------------
            _script = "window.location='TF_EXP_SundryAccountMaster.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (_result == "updated")
            {
                //---------------------------------Nilesh------------------------------------------------------------------
                int isneedtolog = 0;
                _OldValues = "SR No : " + txtSrno.Text.Trim() + "; Code : " + txtcode.Text.Trim() + "; Description : " + hdndesc.Value
                         + "; Branch : " + hdnbranch.Value + "; CCY : " + hdnccy.Value + "; Cust Abb : " + hdncstabbr.Value
                         + "; Account No : " + hdnacntno.Value;
                if (hdndesc.Value != txtdescription.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = "Description : " + txtdescription.Text.Trim();
                    }
                    else
                    {
                        _NewValue = "Description : " + txtdescription.Text.Trim();
                    }
                }
                if (hdnbranch.Value != txtBranch.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; Branch : " + txtBranch.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; Branch : " + txtBranch.Text.Trim();
                    }
                }
                if (hdnccy.Value != txtCcy.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; CCY : " + txtCcy.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; CCY : " + txtCcy.Text.Trim();
                    }
                }
                if (hdncstabbr.Value != txtCustabb.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; Cust Abb : " + txtCustabb.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; Cust Abb : " + txtCustabb.Text.Trim();
                    }
                }
                if (hdnacntno.Value != txtCustAcNo.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; Account No : " + txtCustAcNo.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; Account No : " + txtCustAcNo.Text.Trim();
                    }
                }
                A1.Value = _OldValues;
                A2.Value = _NewValue;
                A3.Value = "IMP";
                A7.Value = "M";
                A8.Value = txtCustAcNo.Text;

                if (isneedtolog == 1)
                {
                    string p = objSave.SaveDeleteData("TF_Mizuho_IMP_AuditTrail_master", A1, A2, A3, A4, A5, A6, A7, A8);
                }
                //---------------------------------END Nilesh------------------------------------------------------------------
                _script = "window.location='TF_EXP_SundryAccountMaster.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
                labelMessage.Text = _result;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_EXP_SundryAccountMaster.aspx", true);
    }
    protected void clearControls()
    {
        txtBranch.Text = "";
        txtCcy.Text = "";
        txtcode.Text = "";
        txtCustabb.Text = "";
        txtdescription.Text = "";
        txtCustAcNo.Text = "";
    }
    protected void fillDetails(string _AcCode, string _SRNO)
    {
        string sundry = string.Empty;
        string interoff = string.Empty;
        TF_DATA objData = new TF_DATA();
        string _query = "TF_EXP_GETSUNDRY_Details";

        SqlParameter p1 = new SqlParameter("@ACCODE", SqlDbType.VarChar);
        p1.Value = _AcCode;
        SqlParameter p2 = new SqlParameter("@SRNO", SqlDbType.VarChar);
        p2.Value = _SRNO;

        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            txtSrno.Text = dt.Rows[0]["SrNo"].ToString().Trim();
            txtBranch.Text = dt.Rows[0]["BRANCH"].ToString().Trim();
            txtcode.Text = dt.Rows[0]["ACCODE"].ToString().Trim();
            txtdescription.Text = dt.Rows[0]["DESCRIPTION"].ToString().Trim();
            txtCcy.Text = dt.Rows[0]["CCY"].ToString().Trim();
            txtCustabb.Text = dt.Rows[0]["CUSTABB"].ToString().Trim();
            txtCustAcNo.Text = dt.Rows[0]["ACCOUNTNO"].ToString().Trim();
            sundry = dt.Rows[0]["SundryAcc"].ToString().Trim();
            interoff = dt.Rows[0]["InterOficeAcc"].ToString().Trim();
            if (sundry == "1")
            {
                sundryAc_Rdbtn.Checked = true;
                InteroffAc_Rdbtn.Visible = false;
            }
            else
            {
                sundryAc_Rdbtn.Checked = false;
            }
            if (interoff == "1")
            {
                InteroffAc_Rdbtn.Checked = true;
                sundryAc_Rdbtn.Visible = false;
            }
            else
            {
                InteroffAc_Rdbtn.Checked = false;
            }
            /*----------Audit trial-----------*/
            hdndesc.Value = dt.Rows[0]["DESCRIPTION"].ToString().Trim();
            hdnbranch.Value = dt.Rows[0]["BRANCH"].ToString().Trim();
            hdnccy.Value = dt.Rows[0]["CCY"].ToString().Trim();
            hdncstabbr.Value = dt.Rows[0]["CUSTABB"].ToString().Trim();
            hdnacntno.Value = dt.Rows[0]["ACCOUNTNO"].ToString().Trim();
        }
    }
    public void Sundrycountsrno()
    {

        TF_DATA objData = new TF_DATA();
        string _query = "EXP_CountSrno";
        DataTable dt = objData.getData(_query);
        if (dt.Rows.Count > 0)
        {
            txtSrno.Text = dt.Rows[0]["Countnum"].ToString().Trim();
            txtSrno.Enabled = false;
        }
        else
        {
            txtSrno.Text = dt.Rows[0]["Countnum"].ToString().Trim();
            txtSrno.Enabled = false;
        }
    }
    public void InterOffcountsrno()
    {

        TF_DATA objData = new TF_DATA();
        string _query = "EXP_CountSrno_interOffice";
        DataTable dt = objData.getData(_query);
        if (dt.Rows.Count > 0)
        {
            txtSrno.Text = dt.Rows[0]["Countnum"].ToString().Trim();
            txtSrno.Enabled = false;
        }
        else
        {
            txtSrno.Text = dt.Rows[0]["Countnum"].ToString().Trim();
            txtSrno.Enabled = false;
        }
    }
    protected void sundryAc_Rdbtn_CheckedChanged(object sender, EventArgs e)
    {
        Sundrycountsrno();
    }
    protected void InteroffAc_Rdbtn_CheckedChanged(object sender, EventArgs e)
    {
        InterOffcountsrno();
    }
}