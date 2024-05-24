using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class IMP_Transactions_TF_IMP_ReversalOfLiability_Maker_View : System.Web.UI.Page
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
                ddlrecordpage.SelectedValue = "20";
                fillBranch();
                fillGrid();
                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].ToString() == "Submit")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record submit to Checker.');", true);
                    }
                }
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
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
        li.Value = "";
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
    }
    protected void GridViewBRO_checker_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridViewBRO_checker_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDocNo = new Label();
            lblDocNo = (Label)e.Row.FindControl("lblDocNo");
            Label Status = new Label();
            Status = (Label)e.Row.FindControl("lblstatus");
            Label lblBranchName = new Label();
            lblBranchName = (Label)e.Row.FindControl("lblBranchName");
            HiddenField lblBranchCode = new HiddenField();
            lblBranchCode = (HiddenField)e.Row.FindControl("lblBranchCode");
            if (Status.Text == "Reject By Checker")
            {
                e.Row.BackColor = System.Drawing.Color.Tomato;
            }

            if (Status.Text == "Approved By Checker")
            {
                e.Row.BackColor = System.Drawing.Color.GreenYellow;
            }

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string url = "window.location='TF_IMP_ReversalOfLiability_Maker.aspx?BranchName=" + lblBranchName.Text + "&BranchCode=" + lblBranchCode.Value + "&DocNo=" + lblDocNo.Text + "&Status=" + Status.Text + "'";

                cell.Attributes.Add("onclick", url);
                i++;
            }
        }
    }
    protected void fillGrid()
    {
        SqlParameter p_search = new SqlParameter("@search", SqlDbType.VarChar);
        p_search.Value = txtSearch.Text.Trim();
        SqlParameter p_BranchName = new SqlParameter("@BranchName", ddlBranch.SelectedItem.Text);
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData("TF_IMP_GetRevOfLib_ForMaker", p_search, p_BranchName);

        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordpage.SelectedValue.Trim());
            GridViewBRO_checker.PageSize = _pageSize;
            GridViewBRO_checker.DataSource = dt.DefaultView;
            GridViewBRO_checker.DataBind();
            GridViewBRO_checker.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewBRO_checker.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewBRO_checker.PageCount != GridViewBRO_checker.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewBRO_checker.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewBRO_checker.PageIndex + 1) + " of " + GridViewBRO_checker.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewBRO_checker.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewBRO_checker.PageIndex != (GridViewBRO_checker.PageCount - 1))
        {
            pgcontrols.enablelastnav(btnnavnext, btnnavlast);
        }
        else
        {
            pgcontrols.disablelastnav(btnnavnext, btnnavlast);
        }
    }
    private void navigationVisibility(Boolean visibility)
    {
        lblpageno.Visible = visibility;
        lblrecordno.Visible = visibility;
        btnnavfirst.Visible = visibility;
        btnnavlast.Visible = visibility;
        btnnavnext.Visible = visibility;
        btnnavpre.Visible = visibility;
    }
    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewBRO_checker.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewBRO_checker.PageIndex > 0)
        {
            GridViewBRO_checker.PageIndex = GridViewBRO_checker.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewBRO_checker.PageIndex != GridViewBRO_checker.PageCount - 1)
        {
            GridViewBRO_checker.PageIndex = GridViewBRO_checker.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewBRO_checker.PageIndex = GridViewBRO_checker.PageCount - 1;
        fillGrid();
    }
    public void ddlrecordpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
}