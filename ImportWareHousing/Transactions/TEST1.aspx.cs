using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class ImportWareHousing_Transactions_TEST1 : System.Web.UI.Page
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
                txtAdCode.Text = Session["userADCode"].ToString();
                txtCustACNo.Focus();
                btnCustomerList.Attributes.Add("onclick", "return OpenCustomerCodeList('mouseClick');");

            }
        }
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
            //txtSupplierID.Focus();
        }
        else
        {
            txtCustACNo.Text = "";
            lblCustName.Text = "";
            txtCustACNo.Focus();
        }

        //if (txtSupplierID.Text.Trim() != "")
        //{
        //    txtSupplierID_TextChanged(null, null);
        //}
    }
}