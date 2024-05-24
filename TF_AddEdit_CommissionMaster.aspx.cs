using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class TF_AddEdit_CommissionMaster : System.Web.UI.Page
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
                    Response.Redirect("TF_View_Commission_Master.aspx", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        btncustacno.Enabled = false;
                        txtCommissionID.Text = Request.QueryString["CommissionID"].Trim();
                        txt_custacno.Text = Request.QueryString["custacno"].Trim();
                        txtCommissionID.Enabled = false;
                        txt_custacno.Enabled = false;
                        txtDescription.Focus();
                        fillDetails(Request.QueryString["CommissionID"].Trim(), Request.QueryString["custacno"].Trim());
                        //string adcode=Session["userADCode"].ToString();
                    }

                    else
                    {
                        txtCommissionID.Enabled = true;
                        txt_custacno.Enabled = true;
                        txt_custacno.Focus();

                        //string adcode = Session["userADCode"].ToString();
                    }
                }

                string adcode = Session["userADCode"].ToString();
                btnSave.Attributes.Add("onclick", "return validateSave();");
                //txtBillAmtFrom.Attributes.Add("onkeydown", "return validate_Number(event);");
              //  txtBillAmtTO.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtRate.Attributes.Add("onkeydown", "return validate_Number(event);");
               // txtRate.Attributes.Add("onblur", "Rate();");
                txtMinRS.Attributes.Add("onkeydown", "return validate_Number(event);");
                btncustacno.Attributes.Add("onclick", "return custhelp(" + adcode + ");");

            }
        }
    }
    void clearControls()
    {
        txtCommissionID.Text = "";
        txtDescription.Text = "";
        //txtBillAmtFrom.Text = "";
       // txtBillAmtTO.Text = "";
        txtRate.Text = "";
        txtMinRS.Text = "";
    }

    void fillDetails(string commissionid,string custacno)
    {
        SqlParameter p1 = new SqlParameter("@CommissionID", SqlDbType.VarChar);
        p1.Value = commissionid;
        SqlParameter p2 = new SqlParameter("@custacno", custacno);

        string _query = "TF_GetDetailsCommissionMaster";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1,p2);

        if (dt.Rows.Count > 0)
        {
            txt_custacno.Text = dt.Rows[0]["CustAcNo"].ToString().Trim();
            txtDescription.Text = dt.Rows[0]["Description"].ToString().Trim();
            //txtBillAmtFrom.Text = dt.Rows[0]["Slab1"].ToString();
           // txtBillAmtTO.Text = dt.Rows[0]["Slab2"].ToString();
            txtRate.Text = dt.Rows[0]["Rate"].ToString();

            if (dt.Rows[0]["Flat"].ToString()!="")
            {
                txt_flat.Text =Convert.ToDecimal(dt.Rows[0]["Flat"]).ToString("0.00");
            }

            else
            {
                txt_flat.Text = dt.Rows[0]["Flat"].ToString();
            }

            if(txtRate.Text !="")
            {
               txtRate.Text=Convert.ToDecimal(txtRate.Text).ToString("0.000000");
            }

            else
            {
                txtRate.Text=txtRate.Text;
            }

          
            txtMinRS.Text = dt.Rows[0]["MinAmt"].ToString();
            txt_maxrs.Text = dt.Rows[0]["MaxAmt"].ToString();
            btnSave.Focus();
        }

        else
        {
            txtDescription.Focus();
        }
    
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_View_Commission_Master.aspx", true);
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
        string _query = "TF_UpdateCommissionMaster";

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

        _result = objSave.SaveDeleteData(_query, p1, p2, p3,p6,p7,p8,p9,p10,p11,p12);

        string _script = "";
        if (_result == "added")
        {
            _script = "window.location='TF_View_Commission_Master.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (_result == "updated")
            {
                _script = "window.location='TF_View_Commission_Master.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }

            else
                labelMessage.Text = _result;
        }


    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_View_Commission_Master.aspx", true);
    }
    protected void txtCommissionID_TextChanged(object sender, EventArgs e)
    {
        
        fillDetails(txtCommissionID.Text, txt_custacno.Text);
    }
}