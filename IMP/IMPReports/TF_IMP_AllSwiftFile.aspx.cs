using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;

public partial class IMP_IMPReports_TF_IMP_AllSwiftFile : System.Web.UI.Page
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
                btnSave.Attributes.Add("onclick", "return Generate();");
                fillBranch();
                ddlBranch.Enabled = true;

                txtfromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                ddlBranch.Focus();
            }

        }
    }
    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        ListItem li = new ListItem();
        li.Text = "All Branch";
        li.Value = "All";
        if (dt.Rows.Count > 0)
        {
            //li.Text = "---Select---";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            //ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataValueField = "BranchCode";

            ddlBranch.DataBind();
        }
        //else
        //    li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
    }

}