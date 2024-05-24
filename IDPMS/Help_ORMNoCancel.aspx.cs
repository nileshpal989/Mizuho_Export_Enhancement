using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class IDPMS_Help_ORMNoCancel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillGrid();
            txtSearch.Focus();
        }
    }
    protected void fillGrid()
    {

        //string branch = Request.QueryString["branch"].ToString();
        //string iecode = Request.QueryString["iecode"].ToString();


        SqlParameter p1 = new SqlParameter("@search", txtSearch.Text);
        //SqlParameter p2 = new SqlParameter("@branch", branch);
        //SqlParameter p3 = new SqlParameter("@iecode", iecode);

        string _query = "TF_GetOTTListCancel";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewUserList.PageSize = _pageSize;
            GridViewUserList.DataSource = dt.DefaultView;
            GridViewUserList.DataBind();
            GridViewUserList.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewUserList.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }

    }
    protected void GridViewUserList_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridViewUserList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblORMNo = new Label();
            Label lblOutRefNo = new Label();
            Label Currency = new Label();
            Label lbldate = new Label();
            lblORMNo = (Label)e.Row.FindControl("lblORMNo");
            lblOutRefNo = (Label)e.Row.FindControl("lblOutRefNo");
            Currency = (Label)e.Row.FindControl("Currency");
            lbldate = (Label)e.Row.FindControl("lbldate");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.opener.selectAccount1('" + lblORMNo.Text + "');window.opener.EndRequest();window.close();";
                if (i != 5)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void GridViewUserList_RowCreated(object sender, GridViewRowEventArgs e)
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
        fillGrid();
    }
    protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        btngo_Click(sender, e);
    }
    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewUserList.PageIndex = 0;
        btngo_Click(sender, e);
    }

    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewUserList.PageIndex > 0)
        {
            GridViewUserList.PageIndex = GridViewUserList.PageIndex - 1;
        }
        btngo_Click(sender, e);
    }

    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewUserList.PageIndex != GridViewUserList.PageCount - 1)
        {
            GridViewUserList.PageIndex = GridViewUserList.PageIndex + 1;
        }
        btngo_Click(sender, e);
    }

    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewUserList.PageIndex = GridViewUserList.PageCount - 1;
        btngo_Click(sender, e);
    }

    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewUserList.PageCount != GridViewUserList.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewUserList.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewUserList.PageIndex + 1) + " of " + GridViewUserList.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewUserList.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewUserList.PageIndex != (GridViewUserList.PageCount - 1))
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
}