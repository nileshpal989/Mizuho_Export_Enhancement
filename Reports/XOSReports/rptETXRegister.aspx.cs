using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Reports_XOSReports_rptETXRegister : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {



            if (!IsPostBack)
            {
                clearControls();
                rdbAllCustomer.Checked = true;
                //ddlBranch.Focus();
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

                txtToDate.Attributes.Add("onblur", "toDate();");
                fillddlBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                //ddlBranch_SelectedIndexChanged(null, null);
                ddlBranch.Enabled = false;
                txtFromDate.Focus();
 
                rdbAllCustomer.Visible = true;
                rdbSelectedCustomer.Visible = true;
                btnSave.Attributes.Add("onclick", "return validateSave();");
                txtFromDate.Attributes.Add("onblur", "return fn_DateCompare(" + txtFromDate.ClientID + "," + "'FromDate Cant be Greater than ToDate '" + " );");
                BtnCustList.Attributes.Add("onclick", "return helpCustId();");
                BtnCustList.Attributes.Add("onclick", "Custhelp();");
                txtCustomer.Attributes.Add("onkeydown", "CustId(event);");
            }

        }

    }
    public void fillddlBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
    }

    protected void clearControls()
    {
        txtFromDate.Text = "";
        txtFromDate.Focus();

    }


    public void fillCustomerIdDescription()
    {
        lblCustomerName.Text = "";
        string custid = txtCustomer.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@custid", SqlDbType.VarChar);
        p1.Value = custid;
        string _query = "TF_rptGGGetCustomerMasterDetails1";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCustomerName.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
        }
        else
        {
            txtCustomer.Text = "";
            lblCustomerName.Text = "";
        }
    }


    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Focus();
    }


    protected void rdbAllCustomer_CheckedChanged(object sender, EventArgs e)
    {
        rdbAllCustomer.Checked = true;
        CustList.Visible = false;
        rdbAllCustomer.Focus();
    }
    protected void rdbSelectedCustomer_CheckedChanged(object sender, EventArgs e)
    {
        CustList.Visible = true;
        txtCustomer.Text = "";
        lblCustomerName.Text = "Customer Name";
        txtCustomer.Visible = true;
        BtnCustList.Visible = true;
        lblCustomerName.Visible = true;
        rdbSelectedCustomer.Focus();
    }

    protected void txtCustomer_TextChanged(object sender, EventArgs e)
    {
        fillCustomerIdDescription();
        txtCustomer.Focus();
    }

    protected void btnCustClick_Click(object sender, EventArgs e)
    {
        fillCustomerIdDescription();
    }


    
}

