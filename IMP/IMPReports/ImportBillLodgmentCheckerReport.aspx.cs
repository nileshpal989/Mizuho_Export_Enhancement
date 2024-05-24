using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;



public partial class Reports_ImportReport_ImportBillLodgmentCheckerReport : System.Web.UI.Page
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
                fillddlBranch();
                txtfromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            }
            Generate.Attributes.Add("onclick", "generateReport();");
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
        ListItem li01 = new ListItem();
        li01.Value = "All";
        if (dt.Rows.Count > 0)
        {
            li.Text = "All";

            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
    }
    protected void rdbApproval_CheckedChanged(object sender, EventArgs e)
    {
        rdbRejected.Checked = false;
        rdbApproval.Checked = true;
       
    }
    protected void rdbRejected_CheckedChanged(object sender, EventArgs e)
    {
        rdbRejected.Checked = true;
        rdbApproval.Checked = false;
    }

    protected void rbdCollection_CheckedChanged(object sender, EventArgs e)
    {
        rbdLodgment.Checked = false;
        rbdCollection.Checked = true;
        rdbcollctionslight.Checked = false;
        rbdloanusane.Checked = false;
        rbdall.Checked = false;
        
    }
    protected void rbdLodgment_CheckedChanged(object sender, EventArgs e)
    {
        rbdLodgment.Checked = true;
        rbdCollection.Checked = false;
        rdbcollctionslight.Checked = false;
        rbdloanusane.Checked = false;
        
    }
    protected void rdbcollctionslight_CheckedChanged(object sender, EventArgs e)
    {
        rbdLodgment.Checked = false;
        rbdCollection.Checked = false;
        rdbcollctionslight.Checked = true;
        rbdloanusane.Checked = false;
        //rbdall.Checked = false;
    }
    protected void rbdloanusane_CheckedChanged(object sender, EventArgs e)
    {
        rbdLodgment.Checked = false;
        rbdCollection.Checked = false;
        rdbcollctionslight.Checked = false;
        rbdloanusane.Checked = true;
        //rbdall.Checked = false;
    }
    protected void rbdall_CheckedChanged(object sender, EventArgs e)
    {
        rbdall.Checked = true;
        rbdLodgment.Checked = false;
        rbdCollection.Checked = false; ;
        rdbcollctionslight.Checked = false;
        rbdloanusane.Checked = false;
    }
}