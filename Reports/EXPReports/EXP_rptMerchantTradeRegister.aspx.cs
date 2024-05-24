using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Reports_EXPReports_EXP_rptMerchantTradeRegister : System.Web.UI.Page
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
                ddlBranch.SelectedValue = Session["userLBCode"].ToString();

                txtFromDate.Attributes.Add("onblur", "toDate();");
                btnGenerate.Attributes.Add("onclick", "return validateSave();");

                txtFromDate.Text = "01/" + System.DateTime.Now.Month.ToString() + "/" + System.DateTime.Now.Year.ToString();
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtFromDate.Attributes.Add("onblur", "return isValidDate(" + txtFromDate.ClientID + "," + "'From Date.'" + ");");
                txtToDate.Attributes.Add("onblur", "return isValidDate(" + txtToDate.ClientID + "," + "'To Date.'" + ");");
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
        li.Value = "0";
        ListItem lAll = new ListItem();
        lAll.Text = "All Branches";
        lAll.Value = "ALL";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
        ddlBranch.Items.Insert(1, lAll);
    }
}