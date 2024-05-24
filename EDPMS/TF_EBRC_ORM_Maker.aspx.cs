using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EBR_TF_EBRC_ORM_Maker : System.Web.UI.Page
{
    TF_DATA objData = new TF_DATA();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"].ToString() == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");
            Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["mode"].ToString() != "add")
                {
                    txtbktxid.Text = Request.QueryString["Ormno"].ToString();
                    ddlOrmstatus.Text = Request.QueryString["Ormstatus"].ToString();
                    getdetails();
                    //txtBranchCode.Enabled = false;
                    //txtORMNo.Enabled = false;
                    //txtPaymentDate.Enabled = false;
                    //txtPurposeCode.Enabled = false;
                    //txtBeneficiaryName.Enabled = false;
                    //txtBeneficiaryCountry.Enabled = false;
                    //txt_ieccode.Enabled = false;
                    //txadcode.Enabled = false;
                    //txt_ORNfc.Enabled = false;
                    //txORNFCCAmount.Enabled = false;
                    //txt_exchagerate.Enabled = false;
                    //txtbktxid.Enabled = false;
                    //txtormissueDate.Enabled = false;
                    //txt_inrpayableamt.Enabled = false;
                    //txtifsccode.Enabled = false;
                    //txt_panno.Enabled = false;
                    //txtmodeofpayment.Enabled = false;
                    //txtrefIRM.Enabled = false;
                    //ddlOrmstatus.Enabled = false;
                    txORNFCCAmount.Attributes.Add("onblur", "return validateAmt();");
                    txt_inrpayableamt.Attributes.Add("onblur", "return validateAmt();");
                    txt_exchagerate.Attributes.Add("onblur", "return validateAmt();");
                }
                else
                {
                    GenerateTxId();
                }

            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            string mode = Request.QueryString["mode"].ToString();
            string _result = "", _script = "";
            string txid = txtbktxid.Text;
            SqlParameter p1 = new SqlParameter("@branchCode", txtBranchCode.Text.Trim());
            SqlParameter p2 = new SqlParameter("@bkuniquetxid", txtbktxid.Text.Trim());
            SqlParameter p3 = new SqlParameter("@ormissuedate", txtormissueDate.Text.Trim());
            SqlParameter p4 = new SqlParameter("@ormno", txtORMNo.Text.Trim());
            SqlParameter p5 = new SqlParameter("@ormstaus", ddlOrmstatus.SelectedItem.Text.ToString().Trim());
            SqlParameter p6 = new SqlParameter("@ifsccode", txtifsccode.Text.Trim());
            SqlParameter p7 = new SqlParameter("@adcode", txadcode.Text.Trim());
            SqlParameter p8 = new SqlParameter("@paymentdate", txtPaymentDate.Text.Trim());
            SqlParameter p9 = new SqlParameter("@ornFC", txt_ORNfc.Text.Trim());
            SqlParameter p10 = new SqlParameter("@ornFCAMT", txORNFCCAmount.Text.Trim());
            SqlParameter p11 = new SqlParameter("@inrpayableamt", txt_inrpayableamt.Text.Trim());
            SqlParameter p12 = new SqlParameter("@exchangerate", txt_exchagerate.Text.Trim());
            SqlParameter p13 = new SqlParameter("@ieccode", txt_ieccode.Text.Trim());
            SqlParameter p14 = new SqlParameter("@panno", txt_panno.Text.Trim());
            SqlParameter p15 = new SqlParameter("@benefname", txtBeneficiaryName.Text.Trim());
            SqlParameter p16 = new SqlParameter("@benefcountry", txtBeneficiaryCountry.Text.Trim());
            SqlParameter p17 = new SqlParameter("@purposecode", txtPurposeCode.Text.Trim());
            SqlParameter p18 = new SqlParameter("@modeofpayment", txtmodeofpayment.Text.Trim());
            SqlParameter p19 = new SqlParameter("@refirm", txtrefIRM.Text.Trim());
            SqlParameter p20 = new SqlParameter("@Addedby", Session["userName"].ToString());
            SqlParameter p21 = new SqlParameter("@Addeddate", System.DateTime.Now.ToString("dd/MM/yyyy"));
            SqlParameter p22 = new SqlParameter("@status", "");
            SqlParameter p23 = new SqlParameter("@mode", mode);
            SqlParameter p24 = new SqlParameter("@remark", "");
            _result = objData.SaveDeleteData("TF_EBRC_ORM_Grid_UpdateDetails", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24);
            if (_result == "exists")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", "alert('Bank Transaction ID already exists')", true);
                //txtdocSrNo.Focus();
            }
            if (_result == "added")
            {
                _script = "window.location='TF_EBRC_ORM_MakerView.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", _script, true);
            }
            if (_result == "Updated")
            {
                _script = "window.location='TF_EBRC_ORM_MakerView.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "redirect", _script, true);
            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message;
        }
    }

    public void getdetails()
    {
        string irmno = Request.QueryString["Ormno"].ToString();
        string irmstatus_ = Request.QueryString["Ormstatus"].ToString();
        SqlParameter p1 = new SqlParameter("@ormno", irmno);
        SqlParameter p2 = new SqlParameter("@ormstatus", irmstatus_);
        DataTable dt = objData.getData("TF_EBRC_ORM_Grid_GetDetailes", p1, p2);
        if (dt.Rows.Count > 0)
        {
            txtBranchCode.Text = dt.Rows[0]["BranchCode"].ToString();
            txtORMNo.Text = dt.Rows[0]["ORMNo"].ToString();
            txtPaymentDate.Text = dt.Rows[0]["PaymentDate"].ToString();
            txtPurposeCode.Text = dt.Rows[0]["PurposeCode"].ToString();
            txtBeneficiaryName.Text = dt.Rows[0]["BenefName"].ToString();
            txtBeneficiaryCountry.Text = dt.Rows[0]["BenefCountry"].ToString();
            txt_ieccode.Text = dt.Rows[0]["IECCode"].ToString();
            txadcode.Text = dt.Rows[0]["ADCode"].ToString();
            txt_ORNfc.Text = dt.Rows[0]["ORNFCC"].ToString();
            txORNFCCAmount.Text = dt.Rows[0]["ORNFCCAmt"].ToString();
            txt_exchagerate.Text = dt.Rows[0]["ExchangeRate"].ToString();
            txtbktxid.Text = dt.Rows[0]["BkUniqueTxId"].ToString();
            txtormissueDate.Text = dt.Rows[0]["ORMIssueDate"].ToString();
            txt_inrpayableamt.Text = dt.Rows[0]["INRPayableAmt"].ToString();
            txtifsccode.Text = dt.Rows[0]["IFSCCode"].ToString();
            txt_panno.Text = dt.Rows[0]["PanNo"].ToString();
            txtmodeofpayment.Text = dt.Rows[0]["ModeOfPayment"].ToString();
            txtrefIRM.Text = dt.Rows[0]["RefIRM"].ToString();
            string irmtatus = dt.Rows[0]["ORMStatus"].ToString();
            lblremark.Text= dt.Rows[0]["Remark"].ToString();
            if (irmtatus == "F" || irmtatus == "C" || irmtatus == "A")
            {
                ddlOrmstatus.SelectedIndex = ddlOrmstatus.Items.IndexOf(ddlOrmstatus.Items.FindByText(dt.Rows[0]["ORMStatus"].ToString().Trim()));
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/EDPMS/TF_EBRC_ORM_MakerView.aspx", true);
    }
    protected void txtbktxid_TextChanged(object sender, EventArgs e)
    {
        getdetails();
    }

    public void GenerateTxId()
    {
        SqlParameter p1 = new SqlParameter("@branchcode", txtBranchCode.Text);
        string _qry = "TF_EBRC_ORM_GenerateTxId";
        DataTable dt = objData.getData(_qry, p1);
        txtbktxid.Text = dt.Rows[0]["bktxid"].ToString().Trim();
    }
}