using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class IMP_TF_IMPAuto_AddEditDrawermaster : System.Web.UI.Page
{
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
                    Response.Redirect("TF_ImpAuto_ViewDrawermaster.aspx", true);
                }
                else
                {
                    ddlBranch.SelectedValue = Request.QueryString["BranchName"].ToString();
                    if (Request.QueryString["mode"].Trim() != "add")
                    {
                        ddlBranch.Enabled = false;
                        txtCustomer_ID.Enabled = false;
                        btnCustomerList.Enabled = false;
                        txtCustName.Focus();
                        fillDetails(Request.QueryString["CustAcNO"].Trim(), Request.QueryString["DrawerID"].Trim());
                    }
                    else
                    {
                        txtCustomer_ID.Focus();
                    }
                }

                btnSave.Attributes.Add("onclick", "return validateSave();");
                btnCustomerList.Attributes.Add("onclick", "return OpenCustomerCodeList('mouseClick');");
                txtCustid.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtcountry.Attributes.Add("onkeydown", "return CountryId(event)");
                btnCountryList.Attributes.Add("onclick", "return Countryhelp()");
            }
        }
    }
    protected void fillDetails(string _CustACNO, string _DrawerID)
    {

        SqlParameter p1 = new SqlParameter("@customerACNO", SqlDbType.VarChar);
        p1.Value = _CustACNO;
        SqlParameter p2 = new SqlParameter("@Drawer_ID", SqlDbType.VarChar);
        p2.Value = _DrawerID;

        string _query = "TF_GetdrawerMasterDetails";
        TF_DATA objData = new TF_DATA();

        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            txtCustomer_ID.Text = dt.Rows[0]["CUST_ACCOUNT_NO"].ToString();
            lblCustomerDesc.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
            txtCustid.Text = dt.Rows[0]["Drawer_ID"].ToString();
            txtCustName.Text = dt.Rows[0]["Drawer_NAME"].ToString().Trim();
            txt_LEINo.Text = dt.Rows[0]["Drawer_LEI_NO"].ToString();
            txt_LEIExpiryDate.Text = dt.Rows[0]["Drawer_LEIExpiryDate"].ToString();
            txtaddress.Text = dt.Rows[0]["Drawer_ADDRESS"].ToString().Trim();
            txtcountry.Text = dt.Rows[0]["Drawer_COUNTRY"].ToString().Trim();
            fillCountryDescription();


        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _result = "";
        string _userName = Session["userName"].ToString();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _mode = Request.QueryString["mode"].Trim();
        string _custacno = txtCustomer_ID.Text.Trim();
        string _drawerID = txtCustid.Text.Trim();
        string _drawerName = txtCustName.Text.Trim();
        string _address = txtaddress.Text.Trim();
        string _country = txtcountry.Text.Trim();
        string _leino = txt_LEINo.Text.Trim();
        string _leiexpirydate = txt_LEIExpiryDate.Text.Trim();

        SqlParameter p1 = new SqlParameter("@CUSTACNO", SqlDbType.VarChar);
        p1.Value = _custacno;

        SqlParameter p2 = new SqlParameter("@mode", SqlDbType.VarChar);
        p2.Value = _mode;

        SqlParameter p3 = new SqlParameter("@Drawer_ID", SqlDbType.VarChar);
        p3.Value = _drawerID;

        SqlParameter p4 = new SqlParameter("@Drawer_NAME ", SqlDbType.VarChar);
        p4.Value = _drawerName;

        SqlParameter p5 = new SqlParameter("@Drawer_ADDRESS", SqlDbType.VarChar);
        p5.Value = _address;

        SqlParameter p6 = new SqlParameter("@Drawer_COUNTRY", SqlDbType.VarChar);
        p6.Value = _country;

        SqlParameter p7 = new SqlParameter("@Drawer_LEINo", SqlDbType.VarChar);
        p7.Value = _leino;

        SqlParameter p8 = new SqlParameter("@Drawer_LEIExpiryDate", SqlDbType.VarChar);
        p8.Value = _leiexpirydate;

        string _query = "TF_UpdatedrawerMaster";
        TF_DATA objSave = new TF_DATA();
        _result = objSave.SaveDeleteData(_query, p1, p2, p3, p4, p5, p6,p7,p8);

        string _script = "";

        if (_result == "added")
        {

            _script = "window.location='TF_ImpAuto_ViewDrawermaster.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (_result == "updated")
            {

                _script = "window.location='TF_ImpAuto_ViewDrawermaster.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
                labelMessage.Text = _result;
        }

        clearall();
    }
    public void clearall()
    {
        txtCustomer_ID.Text = "";
        txtCustName.Text = "";
        txtCustid.Text = "";
        txtaddress.Text = "";
        txtcountry.Text = "";
        txt_LEIExpiryDate.Text = "";
        txt_LEINo.Text = "";
    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ImpAuto_ViewDrawermaster.aspx", true);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ImpAuto_ViewDrawermaster.aspx", true);
    }
    public void fillCountryDescription()
    {
        lblCountryName.Text = "";

        string Countryid = txtcountry.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@cid", SqlDbType.VarChar);
        p1.Value = Countryid;

        string _query = "TF_GetCountryDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCountryName.Text = dt.Rows[0]["CountryName"].ToString().Trim();
        }
        else
        {
            txtcountry.Text = "";
            lblCountryName.Text = "";
        }
    }
    protected void txtcountry_TextChanged(object sender, EventArgs e)
    {
        fillCountryDescription();
        txtcountry.Focus();
    }
    protected void txtCustomer_ID_TextChanged(object sender, EventArgs e)
    {
        fillCustomerMasterDetails();
        string id = txtCustomer_ID.Text.Trim();
        getmaxDrawerID(id);
        txtCustName.Focus();
    }
    protected void txtCustName_TextChanged(object sender, EventArgs e)
    {
        CheckDrawerName();
    }
    private void fillCustomerMasterDetails()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@custid", txtCustomer_ID.Text.ToString());
        SqlParameter p2 = new SqlParameter("@BranchName", ddlBranch.SelectedItem.Text.ToString());
        string _query = "TF_IMP_GetCustomerMasterDetails";
        DataTable dt = objData.getData(_query, p1, p2);
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
            txtCustomer_ID.Text = "";
            lblCustomerDesc.Text = "";
        }

    }
    private void getmaxDrawerID(string _CustACNO)
    {
        SqlParameter p1 = new SqlParameter("@CustomerID", SqlDbType.VarChar);
        p1.Value = _CustACNO;

        string _query = "GETMAXDrawerID";
        TF_DATA objData = new TF_DATA();

        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0 && _CustACNO != "")
        {
            txtCustid.Text = dt.Rows[0]["Drawer_ID"].ToString();
        }

    }
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
    protected void CheckDrawerName()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter PDrawerName = new SqlParameter("@DrawerName", txtCustName.Text.Trim());
        SqlParameter PCustID = new SqlParameter("@CustID", txtCustomer_ID.Text.Trim());
        SqlParameter PDrawerID = new SqlParameter("@DrawerID", txtCustid.Text.Trim());
        string result = obj.SaveDeleteData("TF_IMP_CheckDrawer", PDrawerName, PCustID, PDrawerID);
        if (result == "Yes")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, GetType(), "Message", "alert('Drawer name already exists.')", true);
            txtCustName.Text = "";
            txtCustName.Focus();
        }
        else
        {
            txtaddress.Focus();
        }
    }
}