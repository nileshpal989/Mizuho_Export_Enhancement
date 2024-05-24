using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Reports_EDPMS_Reports_BOE_Balance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }

        if (!IsPostBack)
        {
            fillddlBranch();
            ddlBranch.SelectedValue = Session["userADCode"].ToString();
            ddlBranch.Enabled = false;
            txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            btnCustHelp.Attributes.Add("onclick", "return CustHelp();");
            Generate.Attributes.Add("onclick", "generateReport();");

        }
    }
    public void fillddlBranch()
    {


        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails1";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "---Select---";
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
    protected void rdballcust_CheckedChanged(object sender, EventArgs e)
    {
        rdbselected.Checked = false;
        rdballcust.Checked = true;
        selectedcust.Visible = false;
        txtCust.Text = "";
        lblcustname.Text = "";

    }
    protected void rdbselected_CheckedChanged(object sender, EventArgs e)
    {
        rdbselected.Checked = true;
        rdballcust.Checked = false;
        selectedcust.Visible = true;
    }
    protected void rdball_CheckedChanged(object sender, EventArgs e)
    {
        rdball.Checked = true;
        rdbos.Checked = false;
        rdbclosed.Checked = false;
    }
    protected void rdbos_CheckedChanged(object sender, EventArgs e)
    {
        rdbos.Checked = true;
        rdball.Checked = false;
        rdbclosed.Checked = false;
    }
    protected void rdbclosed_CheckedChanged(object sender, EventArgs e)
    {
        rdbclosed.Checked = true;
        rdball.Checked = false;
        rdbos.Checked = false;
    }
    protected void txtCust_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@accno", SqlDbType.VarChar);
        p1.Value = txtCust.Text;
        string _query = "Get_Customer_Name_BY_AccNo";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblcustname.Text = dt.Rows[0]["CUST_NAME"].ToString();
        }
        else
        {
            txtCust.Text = "";
            lblcustname.Text = "";
        }
    }
}