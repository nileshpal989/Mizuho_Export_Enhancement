using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class TF_ImpAuto_AddEdit_DiscrepencyMaster : System.Web.UI.Page
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
                //clearControls();
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("TF_ImpAuto_View_DiscrepencyMaster.aspx", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                       
                        txtCurr.Enabled = false;
                        //txtdesc.Enabled = false;
                        //txtAmount.Focus();
                        btnCur.Enabled = false;
                        fillDetails(Request.QueryString["QCurrency"].ToString().Trim());
                        //string adcode=Session["userADCode"].ToString();
                    }

                    else
                    {
                        txtCurr.Enabled = true;
                        txtAmount.Enabled = true;
                        txtCurr.Focus();

                        //string adcode = Session["userADCode"].ToString();
                    }
                }
                btnCur.Attributes.Add("onclick", "return curhelp3();");
                // string adcode = Session["userADCode"].ToString();
                btnSave.Attributes.Add("onclick", "return validateSave();");
                txtAmount.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtAmount.Attributes.Add("onblur", "return decimal()");
                //  txtBillAmtTO.Attributes.Add("onkeydown", "return validate_Number(event);");
                //// txtRate.Attributes.Add("onkeydown", "return validate_Number(event);");
                // txtRate.Attributes.Add("onblur", "Rate();");
                // txtMinRS.Attributes.Add("onkeydown", "return validate_Number(event);");
                // btncustacno.Attributes.Add("onclick", "return custhelp(" + adcode + ");");

            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _mode = Request.QueryString["mode"].Trim();
        string query = "TF_IMP_UpdateDiscreptionMaster";
        TF_DATA objSave = new TF_DATA();
        string user = Session["userName"].ToString();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        SqlParameter pmode = new SqlParameter("@mode", SqlDbType.VarChar);
        pmode.Value = _mode;

        SqlParameter PCur = new SqlParameter("@CUR", SqlDbType.VarChar);
        PCur.Value = txtCurr.Text;

        SqlParameter pAmt = new SqlParameter("@Amount", SqlDbType.VarChar);
        pAmt.Value = txtAmount.Text;

        //SqlParameter pINR = new SqlParameter("@INR", SqlDbType.VarChar);
        //pINR.Value = txtINR.Text;

        SqlParameter updatedby = new SqlParameter("@user", SqlDbType.VarChar);
        updatedby.Value = user;

        SqlParameter updateddate = new SqlParameter("@updateDate", SqlDbType.VarChar);
        updateddate.Value = _uploadingDate;

        string _result = objSave.SaveDeleteData(query, pmode, PCur, pAmt, updatedby, updateddate);

        //if (_result == "updated")
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Record Updated');", true);
        //}
        //else
        //{
        //    labelMessage.Text = _result;
        //}
        ///////////Audit Trail////////////////////////////////
        SqlParameter A1 = new SqlParameter("@OldValues", SqlDbType.VarChar);
        SqlParameter A2 = new SqlParameter("@NewValues", SqlDbType.VarChar);
        SqlParameter A3 = new SqlParameter("@ModType", SqlDbType.VarChar);
        SqlParameter A4 = new SqlParameter("@ModifiedBy", Session["userName"].ToString().Trim());
        SqlParameter A5 = new SqlParameter("@ModifiedDate", "");
        SqlParameter A6 = new SqlParameter("@MenuName", "Discrepency Charges Master");
        SqlParameter A7 = new SqlParameter("@Mode", SqlDbType.VarChar);
        SqlParameter A8 = new SqlParameter("@RefNo", SqlDbType.VarChar);

        string _script = "";
        if (_result == "added")
        {
            _NewValue = "Amount : " + txtAmount.Text.Trim()+"; CUR : "+txtCurr.Text.Trim();

            A1.Value = "";
            A2.Value = _NewValue;
            A3.Value = "IMP";
            A7.Value = "A";
            A8.Value = txtCurr.Text;
            string p = objSave.SaveDeleteData("TF_Mizuho_IMP_AuditTrail_master", A1, A2, A3, A4, A5, A6, A7, A8);


            _script = "window.location='TF_ImpAuto_View_DiscrepencyMaster.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            int isneedtolog = 0;
            if (_result == "updated")
            {
                _OldValues = "Amount : " + hdnAmount.Value;
                if (hdnAmount.Value != txtAmount.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = "Amount : " + txtAmount.Text.Trim();
                    }
                    else
                    {
                        _NewValue = "Amount : " + txtAmount.Text.Trim();
                    }
                }


                A1.Value = _OldValues;
                A2.Value = _NewValue;
                A3.Value = "IMP";
                A7.Value = "M";
                A8.Value = txtCurr.Text;

                if (isneedtolog == 1)
                {
                    string p = objSave.SaveDeleteData("TF_Mizuho_IMP_AuditTrail_master", A1, A2, A3, A4, A5, A6, A7, A8);
                }

                _script = "window.location='TF_ImpAuto_View_DiscrepencyMaster.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }

            else
                //labelMessage.Text = _result;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "VAlert('" + _result + "','#txtCurr');", true);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ImpAuto_View_DiscrepencyMaster.aspx", true);
    }

    protected void fillDetails(string Currency)
    {
        SqlParameter p1 = new SqlParameter("@CUR", SqlDbType.VarChar);
        p1.Value = Currency;
        //SqlParameter p2 = new SqlParameter("@Amount", Amount);
        //p2.Value = Amount;
        string query = "TF_IMP_GetDiscrepencyDetails";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(query, p1);
        if (dt.Rows.Count > 0)
        {
            txtCurr.Text = dt.Rows[0]["CUR"].ToString().Trim();
            lblCurrDesc.Text = dt.Rows[0]["C_DESCRIPTION"].ToString().Trim();
            txtAmount.Text = dt.Rows[0]["Amount"].ToString().Trim();
            //txtINR.Text = dt.Rows[0]["INR"].ToString().Trim();

            /*----------Audit trial-----------*/
            hdnAmount.Value = dt.Rows[0]["Amount"].ToString().Trim();
            //hdnINR.Value = dt.Rows[0]["INR"].ToString().Trim();

        }

    }
    protected void txtCurr_TextChanged(object sender, EventArgs e)
    {
        if (txtCurr.Text != "")
        {
            

            TF_DATA objdata = new TF_DATA();
            string query,querylbldesc;
            query = "TF_IMP_getDiscrepencyCURDetails";
            querylbldesc = "TF_IMP_GetCurrDesc";
            SqlParameter pid = new SqlParameter("@currCode", SqlDbType.VarChar);
            pid.Value = txtCurr.Text;

            DataTable dtlbl = objdata.getData(querylbldesc, pid);
            if (dtlbl.Rows.Count > 0)
            {
                lblCurrDesc.Text = dtlbl.Rows[0]["C_DESCRIPTION"].ToString().Trim();
            }

            DataTable dt = objdata.getData(query, pid);
            if (dt.Rows.Count > 0)
            {
                txtCurr.Enabled = false;
                txtCurr.Text = dt.Rows[0]["CUR"].ToString().Trim();
                txtAmount.Text = dt.Rows[0]["Amount"].ToString().Trim();
                //txtINR.Text = dt.Rows[0]["INR"].ToString().Trim();
            }
            else
            {
                txtCurr.Enabled = true;
            }



        }
    }
}