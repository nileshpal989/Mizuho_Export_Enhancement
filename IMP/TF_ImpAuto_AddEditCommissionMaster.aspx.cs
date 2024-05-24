using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Masters_TF_ImpAuto_AddEditCommissionMaster : System.Web.UI.Page
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
                clearall();
                fillBranch();
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("TF_View_ImpAuto_CommissionMaster.aspx", true);
                }
                else
                {
                    ddlBranch.SelectedValue = Request.QueryString["BranchName"].ToString();
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        ddlBranch.Enabled = false;
                        btncustacno.Enabled = false;
                        txtCommissionID.Text = Request.QueryString["CommissionID"].Trim();
                        txt_custacno.Text = Request.QueryString["custacno"].Trim();
                        txtCommissionID.Enabled = false;
                        txt_custacno.Enabled = false;
                        txtDescription.Focus();
                        fillDetails(Request.QueryString["CommissionID"].Trim(), Request.QueryString["custacno"].Trim());
                    }
                    else
                    {
                        txtCommissionID.Enabled = true;
                        txt_custacno.Enabled = true;
                        txt_custacno.Focus();
                    }
                }

                string adcode = Session["userADCode"].ToString();
                btnSave.Attributes.Add("onclick", "return validateSave();");
                txtCommissionID.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtRate.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtRate.Attributes.Add("onblur", "Rate();");
                txtMinRS.Attributes.Add("onkeydown", "return validate_Number(event);");
                btncustacno.Attributes.Add("onclick", "return OpenCustomerCodeList('mouseClick');");

            }
        }
    }
    void clearControls()
    {
        //txtCommissionID.Text = "";
        txtDescription.Text = "";
        txtRate.Text = "";
        txtMinRS.Text = "";
        txt_maxrs.Text = "";
        txt_flat.Text = "";
    }
    void fillDetails(string commissionid, string custacno)
    {
        SqlParameter p1 = new SqlParameter("@CommissionID", SqlDbType.VarChar);
        p1.Value = commissionid;
        SqlParameter p2 = new SqlParameter("@custacno", custacno);

        string _query = "TF_Imp_GetDetailsCommissionMaster";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2);

        if (dt.Rows.Count > 0)
        {
            txt_custacno.Text = dt.Rows[0]["CustAcNo"].ToString().Trim();
            lblCustomerDesc.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
            txtDescription.Text = dt.Rows[0]["Description"].ToString().Trim();
    
            //txtBillAmtFrom.Text = dt.Rows[0]["Slab1"].ToString();
            // txtBillAmtTO.Text = dt.Rows[0]["Slab2"].ToString();
            txtRate.Text = dt.Rows[0]["Rate"].ToString();

            /* ----------------Audit trial--------------------------------------------*/
            hdndescription.Value = dt.Rows[0]["Description"].ToString().Trim();
            hdnrate.Value = dt.Rows[0]["Rate"].ToString();
            /*------------------------------------------------------------------------*/

            if (dt.Rows[0]["Flat"].ToString() != "")
            {
                txt_flat.Text = Convert.ToDecimal(dt.Rows[0]["Flat"]).ToString("0.00");
                hdnflat.Value = Convert.ToDecimal(dt.Rows[0]["Flat"]).ToString("0.00"); //Audit trial
            }
            else
            {
                txt_flat.Text = dt.Rows[0]["Flat"].ToString();
                hdnflat.Value = dt.Rows[0]["Flat"].ToString();//Audit trial
            }
            if (txtRate.Text != "")
            {
                txtRate.Text = Convert.ToDecimal(txtRate.Text).ToString("0.00");
                //hdnrate.Value = Convert.ToDecimal(txtRate.Text).ToString("0.000000");//Audit trial
            }
            else
            {
                txtRate.Text = txtRate.Text;
                //hdnrate.Value = txtRate.Text;//Audit trial
            }
            txtMinRS.Text = dt.Rows[0]["MinAmt"].ToString();
            txt_maxrs.Text = dt.Rows[0]["MaxAmt"].ToString();

            /* ----------------Audit trial--------------------------------------------*/
            hdnminiINR.Value = dt.Rows[0]["MinAmt"].ToString(); 
            hdnmaxINR.Value = dt.Rows[0]["MaxAmt"].ToString();
            /*------------------------------------------------------------------------*/
            btnSave.Focus();
        }
        else
        {
            clearControls();
            txtDescription.Focus();
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_View_ImpAuto_CommissionMaster.aspx", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _mode = Request.QueryString["mode"].Trim();
        string _CommissionID = txtCommissionID.Text.Trim();
        string _CommissionDesc = txtDescription.Text.Trim();
        //string _BillAmtFrom = txtBillAmtFrom.Text.Trim();
        // string _BillAmtTo = txtBillAmtTO.Text.Trim();
        string _Rate = txtRate.Text.Trim();
        string _MinRs = txtMinRS.Text.Trim();

        TF_DATA objSave = new TF_DATA();
        string _query = "TF_Imp_UpdateCommissionMaster";

        SqlParameter p1 = new SqlParameter("@mode", SqlDbType.VarChar);
        p1.Value = _mode;

        SqlParameter p2 = new SqlParameter("@CommissionID", SqlDbType.VarChar);
        p2.Value = _CommissionID;

        SqlParameter p3 = new SqlParameter("@CommissionDesc", SqlDbType.VarChar);
        p3.Value = _CommissionDesc;

        //SqlParameter p4 = new SqlParameter("@BillAmtfrom", SqlDbType.VarChar);
        //p4.Value = _BillAmtFrom;

        //SqlParameter p5 = new SqlParameter("@BillAmtto", SqlDbType.VarChar);
        //p5.Value = _BillAmtTo;

        SqlParameter p6 = new SqlParameter("@Rate", SqlDbType.VarChar);
        p6.Value = _Rate;

        SqlParameter p7 = new SqlParameter("@MinRs", SqlDbType.VarChar);
        p7.Value = _MinRs;

        SqlParameter p8 = new SqlParameter("@user", SqlDbType.VarChar);
        p8.Value = _userName;

        SqlParameter p9 = new SqlParameter("@uploadingdate", SqlDbType.VarChar);
        p9.Value = _uploadingDate;

        SqlParameter p10 = new SqlParameter("@custacno", txt_custacno.Text);
        SqlParameter p11 = new SqlParameter("@maxrs", txt_maxrs.Text);
        SqlParameter p12 = new SqlParameter("@flat", txt_flat.Text);

        _result = objSave.SaveDeleteData(_query, p1, p2, p3, p6, p7, p8, p9, p10, p11, p12);

        ///////////Audit Trail////////////////////////////////
        SqlParameter A1 = new SqlParameter("@OldValues", SqlDbType.VarChar);
        SqlParameter A2 = new SqlParameter("@NewValues", SqlDbType.VarChar);
        SqlParameter A3 = new SqlParameter("@ModType", SqlDbType.VarChar);
        SqlParameter A4 = new SqlParameter("@ModifiedBy", Session["userName"].ToString().Trim());
        SqlParameter A5 = new SqlParameter("@ModifiedDate", "");
        SqlParameter A6 = new SqlParameter("@MenuName", "Commission Master");
        SqlParameter A7 = new SqlParameter("@Mode", SqlDbType.VarChar);
        SqlParameter A8 = new SqlParameter("@RefNo", SqlDbType.VarChar);

        string _script = "";

        if (_result == "added")
        {
            _NewValue = "Description : " + txtDescription.Text.Trim()+" ; Rate : "+txtRate.Text.Trim()+" ; Minimum INR : "+txtMinRS.Text.Trim()+" ; Maximum INR : "+txt_maxrs.Text.Trim()
                + " ; Flat INR : " + txt_flat.Text.Trim();

            A1.Value = "";
            A2.Value = _NewValue;
            A3.Value = "IMP";
            A7.Value = "A";
            A8.Value = _CommissionID;
            string p = objSave.SaveDeleteData("TF_Mizuho_IMP_AuditTrail_master", A1, A2, A3, A4, A5, A6, A7, A8);

            _script = "window.location='TF_View_ImpAuto_CommissionMaster.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            int isneedtolog = 0;
            if (_result == "updated")
            {
                _OldValues = "Description : " + hdndescription.Value + " ; Rate : " + hdnrate.Value + " ; Minimum INR : " +hdnminiINR.Value + " ; Maximum INR : " + hdnmaxINR.Value
                + " ; Flat INR : " + hdnflat.Value;
                if (hdndescription.Value != txtDescription.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = "Description : " + txtDescription.Text.Trim();
                    }
                    else
                    {
                        _NewValue = "Description : " + txtDescription.Text.Trim();
                    }
                }

                if (hdnrate.Value != txtRate.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; Rate : " + txtRate.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; Rate : " + txtRate.Text.Trim();
                    }
                }
                if (hdnminiINR.Value != txtMinRS.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; Minimum INR : " + txtMinRS.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; Minimum INR : " + txtMinRS.Text.Trim();
                    }
                }
                if (hdnmaxINR.Value != txt_maxrs.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; Maximum INR  : " + txt_maxrs.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; Maximum INR  : " + txt_maxrs.Text.Trim();
                    }
                }
                if (hdnflat.Value != txt_flat.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; Flat INR : " + txt_flat.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; Flat INR : " + txt_flat.Text.Trim();
                    }
                }

                A1.Value = _OldValues;
                A2.Value = _NewValue;
                A3.Value = "IMP";
                A7.Value = "M";
                A8.Value = _CommissionID;

                if (isneedtolog == 1)
                {
                    string p = objSave.SaveDeleteData("TF_Mizuho_IMP_AuditTrail_master", A1, A2, A3, A4, A5, A6, A7, A8);
                }


                _script = "window.location='TF_View_ImpAuto_CommissionMaster.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }

            else
                //labelMessage.Text = _result;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "VAlert('" + _result + "','#txt_custacno');", true);
        }


    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_View_ImpAuto_CommissionMaster.aspx", true);
    }
    protected void txtCommissionID_TextChanged(object sender, EventArgs e)
    {

        fillDetails(txtCommissionID.Text, txt_custacno.Text);
    }
    protected void txt_custacno_TextChanged(object sender, EventArgs e)
    {
        fillCustomerMasterDetails();
        //GetCommissionID(txt_custacno.Text.ToString());
    }
    private void fillCustomerMasterDetails()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@custid", SqlDbType.VarChar);
        p1.Value = txt_custacno.Text;
        SqlParameter p2 = new SqlParameter("@BranchName", ddlBranch.SelectedItem.Text.ToString());
        string _query = "TF_IMP_GetCustomerMasterDetails";
        DataTable dt = objData.getData(_query, p1,p2);
        if (dt.Rows.Count > 0)
        {
            lblCustomerDesc.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
            lblCustomerDesc.ToolTip = lblCustomerDesc.Text;
            if (lblCustomerDesc.Text.Length > 30)
            {
                lblCustomerDesc.Text = lblCustomerDesc.Text.Substring(0, 30) + "...";
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "messege", "alert('Select Customer From Selected Branch.')", true);
            txt_custacno.Text = "";
            lblCustomerDesc.Text = "";
        }

    }
    //private void GetCommissionID(string _CustACNO)
    //{
    //    SqlParameter p1 = new SqlParameter("@CustomerID", SqlDbType.VarChar);
    //    p1.Value = _CustACNO;

    //    string _query = "TF_IMP_Get_Max_CommissionID";
    //    TF_DATA objData = new TF_DATA();

    //    DataTable dt = objData.getData(_query, p1);
    //    if (dt.Rows.Count > 0 && _CustACNO != "")
    //    {
    //        txtCommissionID.Text = dt.Rows[0]["CommissionID"].ToString();
    //    }

    //}
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        clearall();
    }
    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        if (dt.Rows.Count > 0)
        {
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchName";
            ddlBranch.DataBind();
        }
    }
    private void clearall() {
        txt_custacno.Text = "";
        txtCommissionID.Text = "";
        txtDescription.Text = "";
        txtRate.Text = "";
        txtMinRS.Text = "";
        txt_maxrs.Text = "";
        txt_flat.Text = "";
    }
}