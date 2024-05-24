using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

public partial class ImportWareHousing_Masters_Impw_AddEditSupplierMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("../../TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["mode"] == null)
                {
                    Response.Redirect("Impw_ViewSupplierMaster.aspx", true);
                }
                else
                {
                    txtAdCode.Text = Session["userADCode"].ToString();
                    if (Request.QueryString["mode"].Trim() == "Add")
                    {
                        txtSupplierID.Enabled = true;
                        txtCustACNo.Focus();
                        btnSave.Text = "Save";
                    }
                    else
                    {
                        btnSave.Text = "Update";
                        txtCustACNo.Enabled = false;
                        btnCustomerList.Enabled = false;
                        txtSupplierID.Enabled = false;

                        string CustACNo = Request.QueryString["CustACNo"].ToString().Trim();
                        string SupplierID = Request.QueryString["SupplierID"].ToString().Trim();

                        FillDetails(Session["userADCode"].ToString(), CustACNo, SupplierID);
                        txtSupplierName.Focus();
                    }
                }
                btnSave.Attributes.Add("onclick", "return validateSave();");
                btnCustomerList.Attributes.Add("onclick", "return OpenCustomerCodeList('mouseClick');");
                btnSupplierCountryList.Attributes.Add("onclick", "return Countryhelp('mouseClick','1')");
                btnBankCountryList.Attributes.Add("onclick", "return Countryhelp('mouseClick','2')");
            }
        }
    }
    protected void FillDetails(string AdCode, string CustACNo, string SupplierID)
    {
        SqlParameter P1 = new SqlParameter("@Adcode", SqlDbType.VarChar);
        P1.Value = AdCode.Trim();
        SqlParameter P2 = new SqlParameter("@Cust_ACNo", SqlDbType.VarChar);
        P2.Value = CustACNo.Trim();
        SqlParameter P3 = new SqlParameter("@Supplier_ID", SqlDbType.VarChar);
        P3.Value = SupplierID.Trim();

        string _query = "TF_IMPW_GetSupplierMasterDetails";
        TF_DATA objData = new TF_DATA();

        DataTable dt = objData.getData(_query, P1, P2, P3);
        if (dt.Rows.Count > 0)
        {
            txtCustACNo.Text = CustACNo.Trim();
            txtCustACNo_TextChanged(null, null);
            txtSupplierID.Text = SupplierID.Trim();
            txtSupplierName.Text = dt.Rows[0]["Supplier_Name"].ToString();
            txtSupplierAddress.Text = dt.Rows[0]["Supplier_Address"].ToString();
            txtSupplierCountry.Text = dt.Rows[0]["Supplier_Country"].ToString();
            txtSupplierCountry_TextChanged(null, null);
            txtBankSwiftCode.Text = dt.Rows[0]["Bank_SwiftCode"].ToString();
            txtBankName.Text = dt.Rows[0]["Bank_Name"].ToString();
            txtBankAddress.Text = dt.Rows[0]["Bank_Address"].ToString();
            txtBankCountry.Text = dt.Rows[0]["Bank_Country"].ToString();
            txtBankCountry_TextChanged(null, null);
            txtSupplierContactNo.Text = dt.Rows[0]["Supplier_ContactNo"].ToString();
            txtSupplierEmailID1.Text = dt.Rows[0]["Supplier_EmailID1"].ToString();
            txtSupplierEmailID2.Text = dt.Rows[0]["Supplier_EmailID2"].ToString();
            txtSupplierEmailID3.Text = dt.Rows[0]["Supplier_EmailID3"].ToString();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Impw_ViewSupplierMaster.aspx", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SqlParameter P0 = new SqlParameter("@Mode", SqlDbType.VarChar);
        P0.Value = Request.QueryString["Mode"].ToString().Trim();

        SqlParameter P1 = new SqlParameter("@Adcode", SqlDbType.VarChar);
        P1.Value = txtAdCode.Text.Trim();

        SqlParameter P2 = new SqlParameter("@Cust_ACNo", SqlDbType.VarChar);
        P2.Value = txtCustACNo.Text.Trim();

        SqlParameter P3 = new SqlParameter("@Supplier_ID", SqlDbType.VarChar);
        P3.Value = txtSupplierID.Text.Trim();

        SqlParameter P4 = new SqlParameter("@Supplier_Name", SqlDbType.VarChar);
        P4.Value = txtSupplierName.Text.Trim();

        SqlParameter P5 = new SqlParameter("@Supplier_Address", SqlDbType.VarChar);
        P5.Value = txtSupplierAddress.Text.Trim();

        SqlParameter P6 = new SqlParameter("@Supplier_Country", SqlDbType.VarChar);
        P6.Value = txtSupplierCountry.Text.Trim();

        SqlParameter P7 = new SqlParameter("@Bank_SwiftCode", SqlDbType.VarChar);
        P7.Value = txtBankSwiftCode.Text.Trim();

        SqlParameter P8 = new SqlParameter("@Bank_Name", SqlDbType.VarChar);
        P8.Value = txtBankName.Text.Trim();

        SqlParameter P9 = new SqlParameter("@Bank_Address", SqlDbType.VarChar);
        P9.Value = txtBankAddress.Text.Trim();

        SqlParameter P10 = new SqlParameter("@Bank_Country", SqlDbType.VarChar);
        P10.Value = txtBankCountry.Text.Trim();

        SqlParameter P11 = new SqlParameter("@Supplier_ContactNo", SqlDbType.VarChar);
        P11.Value = txtSupplierContactNo.Text.Trim();

        SqlParameter P12 = new SqlParameter("@Supplier_EmailID1", SqlDbType.VarChar);
        P12.Value = txtSupplierEmailID1.Text.Trim();

        SqlParameter P13 = new SqlParameter("@Supplier_EmailID2", SqlDbType.VarChar);
        P13.Value = txtSupplierEmailID2.Text.Trim();

        SqlParameter P14 = new SqlParameter("@Supplier_EmailID3", SqlDbType.VarChar);
        P14.Value = txtSupplierEmailID3.Text.Trim();

        SqlParameter P15 = new SqlParameter("@AddedBy", SqlDbType.VarChar);
        P15.Value = Session["userName"].ToString().Trim();

        string query = "TF_IMPW_UpdateSupplierMaster";
        TF_DATA objSave = new TF_DATA();

        string _result, _script = "";

        _result = objSave.SaveDeleteData(query, P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15);

        if (_result == "Added")
        {

            _script = "window.location='Impw_ViewSupplierMaster.aspx?result=" + _result + "'";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
        }
        else
        {
            if (_result == "Updated")
            {

                _script = "window.location='Impw_ViewSupplierMaster.aspx?result=" + _result + "'";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
            }
            else
            {
                labelMessage.Text = _result;
            }
        }

        ClearAll();

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    protected void ClearAll()
    {
        if (Request.QueryString["Mode"].ToString().Trim() == "Add")
        {
            txtSupplierID.Text = "";
            txtSupplierName.Text = "";
            txtSupplierAddress.Text = "";
            txtSupplierCountry.Text = "";
            lblSupplierCountryName.Text = "";
            txtBankSwiftCode.Text = "";
            txtBankName.Text = "";
            txtBankAddress.Text = "";
            txtBankCountry.Text = "";
            lblBankCountryName.Text = "";
            txtSupplierContactNo.Text = "";
            txtSupplierEmailID1.Text = "";
            txtSupplierEmailID2.Text = "";
            txtSupplierEmailID3.Text = "";
        }
    }
    protected void txtSupplierID_TextChanged(object sender, EventArgs e)
    {
        SqlParameter P1 = new SqlParameter("@Adcode", SqlDbType.VarChar);
        P1.Value = txtAdCode.Text.Trim();
        SqlParameter P2 = new SqlParameter("@Cust_ACNo", SqlDbType.VarChar);
        P2.Value = txtCustACNo.Text.Trim();
        SqlParameter P3 = new SqlParameter("@Supplier_ID", SqlDbType.VarChar);
        P3.Value = txtSupplierID.Text.Trim();

        string _query = "TF_IMPW_GetSupplierMasterDetails";
        TF_DATA objData = new TF_DATA();

        DataTable dt = objData.getData(_query, P1, P2, P3);
        if (dt.Rows.Count > 0)
        {
            txtSupplierID.Text = "";
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Supplier ID already exists against this Customer')", true);
            txtSupplierID.Focus();
        }
        else
        {
            txtSupplierName.Focus();
        }
    }
    protected void txtSupplierCountry_TextChanged(object sender, EventArgs e)
    {
        lblSupplierCountryName.Text = fillCountryDescription(txtSupplierCountry.Text.Trim());
        if (lblSupplierCountryName.Text == "")
        {
            txtSupplierCountry.Text = "";
            lblSupplierCountryName.Text = "";
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Enter Valid Country Code!')", true);
            txtSupplierCountry.Focus();
        }
        else
        {
            txtSupplierCountry.Text = txtSupplierCountry.Text.Trim().ToUpper();
            txtBankSwiftCode.Focus();
        }

    }
    protected void txtBankCountry_TextChanged(object sender, EventArgs e)
    {
        lblBankCountryName.Text = fillCountryDescription(txtBankCountry.Text.Trim());
        if (lblBankCountryName.Text == "")
        {
            txtBankCountry.Text = "";
            lblBankCountryName.Text = "";
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Enter Valid Country Code!')", true);
            txtBankCountry.Focus();
        }
        else
        {
            txtBankCountry.Text = txtBankCountry.Text.Trim().ToUpper();
            txtSupplierContactNo.Focus();
        }
    }
    public string fillCountryDescription(string CountryID)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@cid", SqlDbType.VarChar);
        p1.Value = CountryID.Trim();

        string CountryName = "";

        string _query = "TF_GetCountryDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            CountryName = dt.Rows[0]["CountryName"].ToString().Trim();
        }
        else
        {
            CountryName = "";
        }
        return CountryName;
    }
    protected void txtCustACNo_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@customerACNo", SqlDbType.VarChar);
        p1.Value = txtCustACNo.Text.Trim();

        SqlParameter p2 = new SqlParameter("@branch", SqlDbType.VarChar);
        p2.Value = txtAdCode.Text.Trim();
        string _query = "TF_GetCustomerMasterDetails1";


        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            lblCustName.Text = dt.Rows[0]["CUST_NAME"].ToString();
            txtSupplierID.Focus();
        }
        else
        {
            txtCustACNo.Text = "";
            lblCustName.Text = "";
            txtCustACNo.Focus();
        }

        if (txtSupplierID.Text.Trim() != "")
        {
            txtSupplierID_TextChanged(null, null);
        }
    }
    [WebMethod]
    public static string InsertUpdateSuplier(string Mode,string AdCode,string CustAcNo,string SupID,string SupplierName,string SupplierAddress,string SupplierCountry,string BankSwiftCode,string BankName,string BankAddress,string BankCountry,string SupConNo,string SupEID1, string SupEID2, string SupEID3,string UserName)
    {
        SqlParameter P0 = new SqlParameter("@Mode", SqlDbType.VarChar);
        P0.Value = Mode;

        SqlParameter P1 = new SqlParameter("@Adcode", SqlDbType.VarChar);
        P1.Value = AdCode;

        SqlParameter P2 = new SqlParameter("@Cust_ACNo", SqlDbType.VarChar);
        P2.Value = CustAcNo;

        SqlParameter P3 = new SqlParameter("@Supplier_ID", SqlDbType.VarChar);
        P3.Value = SupID;

        SqlParameter P4 = new SqlParameter("@Supplier_Name", SqlDbType.VarChar);
        P4.Value = SupplierName;

        SqlParameter P5 = new SqlParameter("@Supplier_Address", SqlDbType.VarChar);
        P5.Value = SupplierAddress;

        SqlParameter P6 = new SqlParameter("@Supplier_Country", SqlDbType.VarChar);
        P6.Value = SupplierCountry;

        SqlParameter P7 = new SqlParameter("@Bank_SwiftCode", SqlDbType.VarChar);
        P7.Value = BankSwiftCode;

        SqlParameter P8 = new SqlParameter("@Bank_Name", SqlDbType.VarChar);
        P8.Value = BankName;

        SqlParameter P9 = new SqlParameter("@Bank_Address", SqlDbType.VarChar);
        P9.Value = BankAddress;

        SqlParameter P10 = new SqlParameter("@Bank_Country", SqlDbType.VarChar);
        P10.Value = BankCountry;

        SqlParameter P11 = new SqlParameter("@Supplier_ContactNo", SqlDbType.VarChar);
        P11.Value = SupConNo;

        SqlParameter P12 = new SqlParameter("@Supplier_EmailID1", SqlDbType.VarChar);
        P12.Value = SupEID1;

        SqlParameter P13 = new SqlParameter("@Supplier_EmailID2", SqlDbType.VarChar);
        P13.Value = SupEID2;

        SqlParameter P14 = new SqlParameter("@Supplier_EmailID3", SqlDbType.VarChar);
        P14.Value = SupEID3;

        SqlParameter P15 = new SqlParameter("@AddedBy", SqlDbType.VarChar);
        P15.Value = UserName;

        string query = "TF_IMPW_UpdateSupplierMaster";
        TF_DATA objSave = new TF_DATA();

        string _result, _script = "";

        _result = objSave.SaveDeleteData(query, P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11, P12, P13, P14, P15);
        
        return _result;
    }
    [WebMethod]
    public static string CheckSuppID(string AdCode,string CustAcNo,string SupID)
    {
        string Result="";
        SqlParameter P1 = new SqlParameter("@Adcode", SqlDbType.VarChar);
        P1.Value = AdCode;
        SqlParameter P2 = new SqlParameter("@Cust_ACNo", SqlDbType.VarChar);
        P2.Value = CustAcNo;
        SqlParameter P3 = new SqlParameter("@Supplier_ID", SqlDbType.VarChar);
        P3.Value = SupID;

        string _query = "TF_IMPW_GetSupplierMasterDetails";
        TF_DATA objData = new TF_DATA();

        DataTable dt = objData.getData(_query, P1, P2, P3);
        if (dt.Rows.Count > 0)
        {
            Result = "Supplier ID already exists against this Customer";
        }
        else
        {
            Result = "Allow";
        }
        return Result;
    }
    [WebMethod]
    public static string CheckSuppCountry(string CountryID)
    {
        string Result = "";
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@cid", SqlDbType.VarChar);
        p1.Value = CountryID;
        string _query = "TF_GetCountryDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            Result = dt.Rows[0]["CountryName"].ToString().Trim();
        }
        else
        {
            Result = "false";
        }
        return Result;
    }
    [WebMethod]
    public static string CheckBankCountry(string CountryID)
    {
        string Result = "";
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@cid", SqlDbType.VarChar);
        p1.Value = CountryID;
        string _query = "TF_GetCountryDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            Result = dt.Rows[0]["CountryName"].ToString().Trim();
        }
        else
        {
            Result = "false";
        }
        return Result;
    }
    [WebMethod]
    public static string CheckCustAcNo(string AdCode,string CustAcNo)
    {
        string Result = "";
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@customerACNo", SqlDbType.VarChar);
        p1.Value = CustAcNo;
        SqlParameter p2 = new SqlParameter("@branch", SqlDbType.VarChar);
        p2.Value = AdCode;
        string _query = "TF_GetCustomerMasterDetails1";
        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            Result = dt.Rows[0]["CUST_NAME"].ToString();
        }
        else
        {
            Result = "false";
        }
        return Result;
    }
}