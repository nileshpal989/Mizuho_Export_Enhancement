using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class IDPMS_IDPMS_DUMP_AsCustomsData : System.Web.UI.Page
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
            fillbranch();
            ddlBranch.SelectedValue = Session["userADCode"].ToString();
            ddlBranch.Enabled = false;
            txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            btnCustHelp.Attributes.Add("onclick", "return CustHelp();");
            Generate.Attributes.Add("onclick", "generateReport();");

        }
    }


    public void fillbranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        ListItem li = new ListItem();
        //li.Value = "---Select---";
        ListItem li01 = new ListItem();
        li01.Value = "All Branches";
        if (dt.Rows.Count > 0)
        {
            //li.Text = "---Select---";
            li.Text = "All Branches";

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
}
