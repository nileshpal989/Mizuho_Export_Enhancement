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


public partial class TF_AddEditSTaxFXDls : System.Web.UI.Page
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
                txtEffDate.Focus();
               // btnSave.Attributes.Add("onclick", "return validateSave();");
                clearControls();
                
                
            }
        }
    }
    //protected void btnBack_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("TF_ViewCurrencyMaster.aspx", true);
    //}
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _mode = "";
        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";

        DateTime effDate = Convert.ToDateTime(txtEffDate.Text.Trim(), dateInfo);
        string _effDate = effDate.ToString("MM/dd/yyyy");

        if (btnSave.Text == "Update")
            _mode = "edit";
        else
            _mode = "add";

        TF_DATA objSave = new TF_DATA();

        SqlParameter pMode = new SqlParameter("@mode", SqlDbType.VarChar);
        pMode.Value = _mode;

        SqlParameter pSrno = new SqlParameter("@srno", SqlDbType.VarChar);

        SqlParameter pslabFrom = new SqlParameter("@slabFrom", SqlDbType.VarChar);

        SqlParameter pslabTo = new SqlParameter("@slabTo", SqlDbType.VarChar);

        SqlParameter prate = new SqlParameter("@rate", SqlDbType.VarChar);

        SqlParameter pminAmt = new SqlParameter("@minAmt", SqlDbType.VarChar);

        SqlParameter pmaxAmt = new SqlParameter("@maxAmt", SqlDbType.VarChar);

        SqlParameter peffDate = new SqlParameter("@effDate", SqlDbType.VarChar);

        SqlParameter peduCess = new SqlParameter("@eduCess", SqlDbType.VarChar);

        SqlParameter psEduCess = new SqlParameter("@sEduCess", SqlDbType.VarChar);

        SqlParameter sbcess = new SqlParameter("@sbcess", txtsbcess.Text);
        SqlParameter KKCess = new SqlParameter("@KKCess", txtkkcess.Text);
        
        for (int i = 0; i <= 2; i++)
        {
            if (i == 0)
            {
                pSrno.Value = 1;
                pslabFrom.Value = txtSlabFrom1.Text.Trim();
                pslabTo.Value = txtSlabTo1.Text.Trim();
                prate.Value = txtRate1.Text.Trim();
            }
            if (i == 1)
            {
                pSrno.Value = 2;
                pslabFrom.Value = txtSlabFrom2.Text.Trim();
                pslabTo.Value = txtSlabTo2.Text.Trim();
                prate.Value = txtRate2.Text.Trim();
            }
            if (i == 2)
            {
                pSrno.Value = 3;
                pslabFrom.Value = txtSlabFrom3.Text.Trim();
                pslabTo.Value = txtSlabTo3.Text.Trim();
                prate.Value = txtRate3.Text.Trim();
            }
            pminAmt.Value = txtMinAmt.Text.Trim();
            pmaxAmt.Value = txtMaxAmt.Text.Trim();
            peffDate.Value = _effDate;
            peduCess.Value = txtEduCess.Text.Trim();
            psEduCess.Value = txtSeduCess.Text.Trim();

            string _query = "TF_UpdateStaxFXDLSDetails";

            _result = objSave.SaveDeleteData(_query, pMode, pSrno, pslabFrom, pslabTo, prate, pminAmt, pmaxAmt, peffDate, peduCess, psEduCess, sbcess, KKCess);
        
        }

        
        string _script = "";
        if (_result == "added")
        {
            _script = "alert('Record Added.')";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            clearControls();
        }
        else
        {
            if (_result == "updated")
            {
                _script = "alert('Record Updated.')";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                clearControls();
            }
            else
                labelMessage.Text = _result;
        }

    }
    
    protected void clearControls()
    {
        txtEffDate.Text = "";
        txtSlabFrom1.Text = "";
        txtSlabFrom2.Text = "";
        txtSlabFrom3.Text = "";
        txtSlabTo1.Text = "";
        txtSlabTo2.Text = "";
        txtSlabTo3.Text = "";
        txtRate1.Text = "";
        txtRate2.Text = "";
        txtRate3.Text = "";
        txtMinAmt.Text = "";
        txtMaxAmt.Text = "";
        txtEduCess.Text = "";
        txtSeduCess.Text = "";
        txtkkcess.Text = "";
        txtsbcess.Text = "";

        btnSave.Text = "Save";
    }
    
    protected void txtEffDate_TextChanged(object sender, EventArgs e)
    {

        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";

        DateTime effDate = Convert.ToDateTime(txtEffDate.Text.Trim(), dateInfo);
        string _result = "";
        
        string _effDate = effDate.ToString("MM/dd/yyyy");

        SqlParameter p1 = new SqlParameter("@effDate", SqlDbType.VarChar);
        p1.Value = _effDate;

        string _query = "TF_GetSTaxFXDLs";
        
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            txtSlabFrom1.Text=dt.Rows[0]["SLAB_FROM"].ToString();
            txtSlabTo1.Text = dt.Rows[0]["SLAB_TO"].ToString();
            txtRate1.Text = dt.Rows[0]["RATE"].ToString();
            txtMinAmt.Text = dt.Rows[0]["MIN_AMOUNT"].ToString();
           
            txtSlabFrom2.Text = dt.Rows[1]["SLAB_FROM"].ToString();
            txtSlabTo2.Text = dt.Rows[1]["SLAB_TO"].ToString();
            txtRate2.Text = dt.Rows[1]["RATE"].ToString();

            txtSlabFrom3.Text = dt.Rows[2]["SLAB_FROM"].ToString();
            txtSlabTo3.Text = dt.Rows[2]["SLAB_TO"].ToString();
            txtRate3.Text = dt.Rows[2]["RATE"].ToString();
            txtMaxAmt.Text = dt.Rows[2]["MAX_AMOUNT"].ToString();

            txtEduCess.Text = dt.Rows[0]["EDU_CESS"].ToString();
            txtSeduCess.Text = dt.Rows[2]["S_EDU_CESS"].ToString();

            txtsbcess.Text = dt.Rows[0]["SBCess"].ToString();
            txtkkcess.Text = dt.Rows[0]["KKCess"].ToString();

            btnSave.Text = "Update";
        }


    }
}
