using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class TF_EXP_INWDOCLIST : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            fillGrid();
        }
    }
    protected void fillGrid()
    {
        SqlParameter P1 = new SqlParameter("@Type", Request.QueryString["DocPref"].ToString().Trim());
        SqlParameter P2 = new SqlParameter("@Branch", Request.QueryString["BranchCode"].ToString().Trim());
        SqlParameter P3 = new SqlParameter("@Year", Request.QueryString["Year"].ToString().Trim());
        SqlParameter P4 = new SqlParameter("@Search", Request.QueryString["DocNo"].ToString().Trim());

        string _query = "TF_EXP_GETDOCNOLISTFORINW";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, P1, P2, P3, P4);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewDocList.PageSize = _pageSize;
            GridViewDocList.DataSource = dt.DefaultView;
            GridViewDocList.DataBind();
            GridViewDocList.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);

        }
        else
        {
            GridViewDocList.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
    protected void GridViewDocList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
    }
    protected void GridViewDocList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDocNo = new Label();
            lblDocNo = (Label)e.Row.FindControl("lblDocNo");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.opener.selectDocNo('" + lblDocNo.Text + "');window.opener.EndRequest();window.close();";
                if (i != 6)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }

    protected void GridViewDocList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TabIndex = -1;
            e.Row.Attributes["onmouseover"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
            e.Row.Attributes["onmouseout"] = string.Format("javascript:DisSelectRow(this, {0});", e.Row.RowIndex);
            e.Row.ToolTip = "Click to select row.";
        }
    }

    protected void btngo_Click(object sender, EventArgs e)
    {
        SqlParameter P1 = new SqlParameter("@Type", Request.QueryString["DocPref"].ToString().Trim());
        SqlParameter P2 = new SqlParameter("@Branch", Request.QueryString["BranchCode"].ToString().Trim());
        SqlParameter P3 = new SqlParameter("@Year", Request.QueryString["Year"].ToString().Trim());
        SqlParameter P4 = new SqlParameter("@Search", txtSearch.Text.Trim());

        string _query = "TF_EXP_GETDOCNOLISTFORINW";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, P1, P2, P3, P4);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewDocList.PageSize = _pageSize;
            GridViewDocList.DataSource = dt.DefaultView;
            GridViewDocList.DataBind();
            GridViewDocList.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);

        }
        else
        {
            GridViewDocList.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
    protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewDocList.PageCount != GridViewDocList.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewDocList.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewDocList.PageIndex + 1) + " of " + GridViewDocList.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewDocList.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewDocList.PageIndex != (GridViewDocList.PageCount - 1))
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
        GridViewDocList.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewDocList.PageIndex > 0)
        {
            GridViewDocList.PageIndex = GridViewDocList.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewDocList.PageIndex != GridViewDocList.PageCount - 1)
        {
            GridViewDocList.PageIndex = GridViewDocList.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewDocList.PageIndex = GridViewDocList.PageCount - 1;
        fillGrid();
    }
}
