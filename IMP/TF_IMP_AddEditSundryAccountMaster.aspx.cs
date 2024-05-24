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

public partial class IMP_TF_IMP_AddEditSundryAccountMaster : System.Web.UI.Page
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
                sundryAc_Rdbtn.Checked = true;

               
                clearControls();
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("TF_IMP_SundryAccountMaster.aspx", true);
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
        Response.Redirect("TF_IMP_SundryAccountMaster.aspx", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //if(sundryAc_Rdbtn.Checked ==false || InteroffAc_Rdbtn.Checked == false)
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", "Select Account type Radio Button", true);
        //}
       
        //else
        //{
       
        string _result = "";
        string sundryaccounttype = string.Empty;
         string introaccounttype = string.Empty;
        //string _userName = Session["userName"].ToString().Trim();
        //string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
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

        string _query = "TF_IMP_UpdateSundryAccount";

        SqlParameter p0 = new SqlParameter("@Srno", SqlDbType.VarChar);
        p0.Value = txtSrno.Text.Trim() ;

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

        _result = objSave.SaveDeleteData(_query,p0, p1, p2, p3, p4, p5,p6,p7,p8,p9);
        string _script = "";
        if (_result == "added")
        {
            _script = "window.location='TF_IMP_SundryAccountMaster.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (_result == "updated")
            {
                _script = "window.location='TF_IMP_SundryAccountMaster.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
                labelMessage.Text = _result;
        }
       // }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_IMP_SundryAccountMaster.aspx", true);
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
    protected void fillDetails(string _AcCode,string _SRNO)
    {
        string sundry = string.Empty;
        string interoff = string.Empty;
        TF_DATA objData = new TF_DATA();
        string _query = "TF_IMP_GETSUNDRY_Details";

        SqlParameter p1 = new SqlParameter("@ACCODE", SqlDbType.VarChar);
        p1.Value = _AcCode;
        SqlParameter p2 = new SqlParameter("@SRNO", SqlDbType.VarChar);
        p2.Value = _SRNO;

        DataTable dt = objData.getData(_query, p1,p2);
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
           if (sundry=="1")
           {
               sundryAc_Rdbtn.Checked = true;
           }
           else
           {
               sundryAc_Rdbtn.Checked = false;
           }
           if (interoff =="1")
           {
               InteroffAc_Rdbtn.Checked = true;
           }
           else
           {
               InteroffAc_Rdbtn.Checked = false;
           }
        }
    }
    public void Sundrycountsrno()
    {

        TF_DATA objData = new TF_DATA();
        string _query = "countSrno";
        DataTable dt = objData.getData(_query);
        if (dt.Rows.Count > 0)
        {
            txtSrno.Text = dt.Rows[0]["Countnum"].ToString().Trim();
            txtSrno.Enabled = false;
            //int srno = Convert.ToInt32(txtSrno.Text) + 1;
            //string srn = txtSrno.Text;
            //txtSrno.Text = srno.ToString();
        }
        else
        {
            //Countnum
            txtSrno.Text = dt.Rows[0]["Countnum"].ToString().Trim();
            txtSrno.Enabled = false;
            //int srno = Convert.ToInt32(txtSrno.Text)+1;
            //string srn = txtSrno.Text;
            //txtSrno.Text = srno.ToString();
        }
    }
    public void InterOffcountsrno()
    {

        TF_DATA objData = new TF_DATA();
        string _query = "countSrno1";
        DataTable dt = objData.getData(_query);
        if (dt.Rows.Count > 0)
        {
            txtSrno.Text = dt.Rows[0]["Countnum"].ToString().Trim();
            txtSrno.Enabled = false;
            //int srno = Convert.ToInt32(txtSrno.Text) ;
            //string srn = txtSrno.Text;
            //txtSrno.Text = srno.ToString();
        }
        else
        {
            //Countnum
            txtSrno.Text = dt.Rows[0]["Countnum"].ToString().Trim();
            txtSrno.Enabled = false;
            //int srno = Convert.ToInt32(txtSrno.Text) + 1;
            //string srn = txtSrno.Text;
            //txtSrno.Text = srno.ToString();
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