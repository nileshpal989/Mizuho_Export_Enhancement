using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

public partial class TF_OtherBankCharges : System.Web.UI.Page
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
                btnSave.Attributes.Add("onclick", "return validateSave();");
                //clearControls();
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("TF_View_ImpAuto_CommissionMaster.aspx", true);
                }
                else
                {
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        // btncustacno.Enabled = false;
                        txtid.Text = Request.QueryString["ID"].Trim();
                        txtdesc.Text = Request.QueryString["custacno"].Trim();
                        txtid.Enabled = false;
                        //txtdesc.Enabled = false;
                        txtdesc.Focus();
                        fillDetails(Request.QueryString["ID"].Trim());
                        //string adcode=Session["userADCode"].ToString();
                    }   
                    else
                    {
                        txtid.Enabled = true;
                        txtdesc.Enabled = true;
                        txtid.Focus();
                        //string adcode = Session["userADCode"].ToString();
                    }
                }

                // string adcode = Session["userADCode"].ToString();
                btnSave.Attributes.Add("onclick", "return validateSave();");
                txtAmount.Attributes.Add("onkeydown", "return validate_Number(event);");
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
        string query_getdata = "TF_IMP_Get_Otherbankcharges";        
        string _mode = Request.QueryString["mode"].Trim();
        string query = "TF_IMP_AddEdit_OtherBankCharges";
        TF_DATA objSave = new TF_DATA();
        string user = Session["userName"].ToString();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        SqlParameter pID = new SqlParameter("@ID", SqlDbType.VarChar);
        pID.Value = txtid.Text;

        SqlParameter pmode = new SqlParameter("@mode", SqlDbType.VarChar);

        DataTable dt = objSave.getData(query_getdata, pID);

        if (dt.Rows.Count > 0)
        {
            pmode.Value = "edit";
        }
        else
        {
            pmode.Value = "add";
        }

        SqlParameter pDesc = new SqlParameter("@Description", SqlDbType.VarChar);
        pDesc.Value = txtdesc.Text;

        SqlParameter pAmount = new SqlParameter("@Amount", SqlDbType.VarChar);
        pAmount.Value = txtAmount.Text;

        SqlParameter updatedby = new SqlParameter("@user", SqlDbType.VarChar);
        updatedby.Value = user;

        SqlParameter updateddate = new SqlParameter("@updateDate", SqlDbType.VarChar);
        updateddate.Value = _uploadingDate;

        string _result = objSave.SaveDeleteData(query, pmode, pID, pDesc, pAmount, updatedby, updateddate);

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
        SqlParameter A6 = new SqlParameter("@MenuName", "Other Bank Charges Master");
        SqlParameter A7 = new SqlParameter("@Mode", SqlDbType.VarChar);
        SqlParameter A8 = new SqlParameter("@RefNo", SqlDbType.VarChar);

        string _script = "";
        if (_result == "added")
        {
            _NewValue = "Description : " + txtdesc.Text.Trim()+" ; Amount : "+txtAmount.Text.Trim()+" ; ID : "+txtid.Text.Trim();
            A1.Value = "";
            A2.Value = _NewValue;
            A3.Value = "IMP";
            A7.Value = "A";
            A8.Value = txtid.Text;
            string p = objSave.SaveDeleteData("TF_Mizuho_IMP_AuditTrail_master", A1, A2, A3, A4, A5, A6, A7, A8);

            _script = "window.location='TF_ImpAuto_View_OtherBankCharges.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            int isneedtolog = 0;
            if (_result == "updated")
            {
                _OldValues = "Description : " + hdnotherbankdescription.Value.Trim() + " ; Amount : " + hdnAmount.Value.Trim();
                if (hdnotherbankdescription.Value != txtdesc.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = "Description : " + txtdesc.Text.Trim();
                    }
                    else
                    {
                        _NewValue = "Description : " + txtdesc.Text.Trim();
                    }
                }

                if (hdnAmount.Value != txtAmount.Text.Trim())
                {
                    isneedtolog = 1;
                    if (_NewValue == "")
                    {
                        _NewValue = _NewValue + " ; Amount : " + txtAmount.Text.Trim();
                    }
                    else
                    {
                        _NewValue = _NewValue + " ; Amount : " + txtAmount.Text.Trim();
                    }
                }

                A1.Value = _OldValues;
                A2.Value = _NewValue;
                A3.Value = "IMP";
                A7.Value = "M";
                A8.Value = txtid.Text;

                if (isneedtolog == 1)
                {
                    string p = objSave.SaveDeleteData("TF_Mizuho_IMP_AuditTrail_master", A1, A2, A3, A4, A5, A6, A7, A8);
                }

                _script = "window.location='TF_ImpAuto_View_OtherBankCharges.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }

            else
                //labelMessage.Text = _result;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "VAlert('" + _result + "','#txtid');", true);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ImpAuto_View_OtherBankCharges.aspx", true);
    }

    protected void fillDetails(string ID)
    {
        SqlParameter p1 = new SqlParameter("@ID", SqlDbType.VarChar);
        p1.Value = ID;
        string query = "TF_IMP_Get_Otherbankcharges";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(query, p1);
        if (dt.Rows.Count > 0)
        {
            txtid.Text = dt.Rows[0]["ID"].ToString().Trim();
            txtdesc.Text = dt.Rows[0]["Description"].ToString().Trim();
            txtAmount.Text = dt.Rows[0]["Amount"].ToString().Trim();

            /*------------Audit trial-----------------------------*/
            hdnotherbankdescription.Value = dt.Rows[0]["Description"].ToString().Trim();
            hdnAmount.Value = dt.Rows[0]["Amount"].ToString().Trim();
        }

    }
    protected void txtid_TextChanged(object sender, EventArgs e)
    {
        if (txtid.Text != "")
        {
            TF_DATA objdata = new TF_DATA();
            string query;
            query = "TF_IMP_Get_Otherbankcharges";
            SqlParameter pid = new SqlParameter("@ID", SqlDbType.VarChar);
            pid.Value = txtid.Text;
            DataTable dt = objdata.getData(query, pid);
            if (dt.Rows.Count > 0)
            {
                txtid.Enabled = false;
                txtid.Text = dt.Rows[0]["ID"].ToString().Trim();
                txtdesc.Text = dt.Rows[0]["Description"].ToString().Trim();
                txtAmount.Text = dt.Rows[0]["Amount"].ToString().Trim();
            }
            else
            {
                txtid.Enabled = true;
            }
        }
    }

    [WebMethod]
    public static void AutoSave(string firstname, string middlename, string lastname)
    {
        //int id = 1;
        string ID = firstname;
        SqlParameter pid = new SqlParameter("@ID", SqlDbType.VarChar);
        pid.Value = ID;

        SqlParameter pname = new SqlParameter("@Desc", SqlDbType.VarChar);
        pname.Value = middlename;

        SqlParameter pamount = new SqlParameter("@Amount", SqlDbType.VarChar);
        pamount.Value = lastname;

        string query_getdata = "TF_IMP_Get_Otherbankcharges";
        string query_Update = "TF_IMP_AutoUpdate_OtherBankCharges";
        string query_Insert = "TF_IMP_AutoInsert_OtherBankCharges";
        TF_DATA objdata = new TF_DATA();

        if (firstname == "")
        {

        }
        else
        {
            DataTable dt = objdata.getData(query_getdata, pid);
            if (dt.Rows.Count > 0)
            {
                string result = objdata.SaveDeleteData(query_Update, pid, pname, pamount);
                // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Updated.');", true);
            }
            else
            {
                string resultInsert = objdata.SaveDeleteData(query_Insert, pid, pname, pamount);
                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Added.');", true);
            }
        }
        //string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString;
        //SqlConnection con = new SqlConnection(ConnectionString);
        //{
        //    string str = "Update TF_ImpAuto_OtherbankCharges set ID='" + firstname + "',Description= '" + middlename + "',Amount= '" + lastname + "' where Id=" + ID + "";
        //    SqlCommand cmd = new SqlCommand(str, con);
        //    {
        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //        return "True";
        //    }
        //}
    }
}