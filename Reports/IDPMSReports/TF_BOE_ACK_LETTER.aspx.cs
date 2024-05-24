using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Reports_IDPMSReports_TF_BOE_ACK_LETTER : System.Web.UI.Page
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
            btnDocHelp.Attributes.Add("onclick", "return Dump_Help();");
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
    protected void rdbAllDoc_CheckedChanged(object sender, EventArgs e)
    {
        rdbAllDoc.Checked = true;
        rdbSelectedDoc.Checked = false;
        SelectDoc.Visible = false;
        txtDoc.Text = "";
        lblDocName.Text = "";
    }
    protected void rdbSelectedDoc_CheckedChanged(object sender, EventArgs e)
    {
        rdbAllDoc.Checked = false;
        rdbSelectedDoc.Checked = true;
        SelectDoc.Visible = true;
    }
    protected void rdbAllCust_CheckedChanged(object sender, EventArgs e)
    {
        rdbSelectedCust.Checked = false;
        rdbAllCust.Checked = true;
        SelectCust.Visible = false;
        txtCust.Text = "";
        lblcustname.Text = "";
    }
    protected void rdbSelectedCust_CheckedChanged(object sender, EventArgs e)
    {
        rdbSelectedCust.Checked = true;
        rdbAllCust.Checked = false;
        SelectCust.Visible = true;
    }

}