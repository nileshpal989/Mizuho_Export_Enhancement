using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Reports_EDPMS_Reports_EDPMS_Rpt_E_Firc_Register : System.Web.UI.Page
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
            btnCustList.Attributes.Add("onclick", "return custhelp()");
            txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            btnGenerate.Attributes.Add("onclick", "validateSave();");

         }


    }

    private void fillddlBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
      ListItem li = new ListItem();
        //li.Value = "---Select---";
        //ListItem li01 = new ListItem();
        //li01.Value = "All Branches";
        if (dt.Rows.Count > 0)
        {
            //li.Text = "---Select---";
            //li.Text = "All Branches";

            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            //li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
      
    }
    protected void rdbCust_CheckedChanged(object sender, EventArgs e)
    {
        Cust.Visible = true;
        rdball1.Visible = true;
        rdball1.Checked = true;
        Rdbselectedcust.Checked = false;
        RdbDocument.Checked = false;
        Rdbselectedcust.Visible = true;
    }
    protected void rdball1_CheckedChanged(object sender, EventArgs e)
    {
        Rdbselectedcust.Checked = false;
        CustID.Visible = false;
        RdbDocument.Checked = false;
        
    }
    protected void Rdbselectedcust_CheckedChanged(object sender, EventArgs e)
    {
        rdball1.Checked = false;
        CustID.Visible = true;
        rdball1.Checked = false;
        btnCustList.Visible = true;
        lblCustomerName.Visible = true;
        txtcustID.Text = "";
        lblCustomerName.Text = "";
    }
    protected void RdbDocument_CheckedChanged(object sender, EventArgs e)
    {
        CustID.Visible = false;
        rdball1.Visible = false;
        Rdbselectedcust.Visible = false;
        rdbCust.Checked = false;

    }
    public void fillCustomerIdDescription()
    {
        lblCustomerName.Text = "";
        string custid = txtcustID.Text.ToString().Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@IECODE", SqlDbType.VarChar);
        p1.Value = custid;
        string _query = "Get_Customer_Name";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCustomerName.Text = dt.Rows[0]["IEName"].ToString().Trim();
        }
        else
        {
            txtcustID.Text = "";
            lblCustomerName.Text = "";
        }
    }

    protected void txtcustID_TextChanged(object sender, EventArgs e)
    {
        fillCustomerIdDescription();
    }
}