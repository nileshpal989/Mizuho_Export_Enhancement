using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class IMP_IMPReports_TF_IMP_UnsettledDoc : System.Web.UI.Page
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
                fillBranch();
                txtdate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            }
           // btnCustomerList.Attributes.Add("onclick", "return OpenCustomerCodeList('mouseClick');");
            Generate.Attributes.Add("onclick", "generateReport();");
        }
    }

    public void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", "");
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "All";
        if (dt.Rows.Count > 0)
        {
            li.Text = "All";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchCode";

            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";
        ddlBranch.Items.Insert(0, li);
    }

    //protected void rdbAllCustomer_CheckedChanged(object sender, EventArgs e)
    //{
    //    rdbSelectedCustomer.Checked = false;
    //    rdbAllCustomer.Checked = true;
    //    divUser.Visible = false;
    //    txtCustomer_ID.Text = "";
    //}
    //protected void rdbSelectedCustomer_CheckedChanged(object sender, EventArgs e)
    //{
    //    rdbSelectedCustomer.Checked = true;
    //    rdbAllCustomer.Checked = false;
    //    divUser.Visible = true;
    //}
}