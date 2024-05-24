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

public partial class EXP_Expswift_DocNoHelp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Fill_FY();
            ddlYear.SelectedValue = System.DateTime.Now.ToString("yy");

            txtSearch.Text = Request.QueryString["DocNo"].ToString();
            fillGrid();
        }

    }
    protected void fillGrid()
    {
        SqlParameter Search = new SqlParameter("@search", SqlDbType.VarChar);
        Search.Value = txtSearch.Text.ToString();
        SqlParameter Document_Type = new SqlParameter("@Document_Type", SqlDbType.VarChar);
        Document_Type.Value = ddlDocTypes.SelectedValue;
        SqlParameter BranchCode = new SqlParameter("@Branch", SqlDbType.VarChar);
        BranchCode.Value = Request.QueryString["BranchCode"].ToString();
        SqlParameter FY = new SqlParameter("@FY", SqlDbType.Int);
        FY.Value = Convert.ToInt32(ddlYear.SelectedValue.Trim());

        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData("TF_EXP_SwiftDocHelpList", Search, Document_Type, BranchCode, FY);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewLogdeDocList.PageSize = _pageSize;
            GridViewLogdeDocList.DataSource = dt.DefaultView;
            GridViewLogdeDocList.DataBind();
            GridViewLogdeDocList.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewLogdeDocList.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }

    }
    protected void GridViewLogdeDocList_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridViewLogdeDocList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDocument_No = new Label();
            lblDocument_No = (Label)e.Row.FindControl("lblDocument_No");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "";
                if (Request.QueryString["hNo"].ToString() == "1")
                {
                    pageurl = "window.opener.selectLogded_DocNo('" + lblDocument_No.Text + "','" + Request.QueryString["Swift"].ToString() + "');window.opener.EndRequest();window.close();";
                }
                if (i != 10)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void GridViewLogdeDocList_RowCreated(object sender, GridViewRowEventArgs e)
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
        fillGrid();
    }
    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewLogdeDocList.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewLogdeDocList.PageIndex > 0)
        {
            GridViewLogdeDocList.PageIndex = GridViewLogdeDocList.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewLogdeDocList.PageIndex != GridViewLogdeDocList.PageCount - 1)
        {
            GridViewLogdeDocList.PageIndex = GridViewLogdeDocList.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewLogdeDocList.PageIndex = GridViewLogdeDocList.PageCount - 1;
        fillGrid();
    }
    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewLogdeDocList.PageCount != GridViewLogdeDocList.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewLogdeDocList.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewLogdeDocList.PageIndex + 1) + " of " + GridViewLogdeDocList.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewLogdeDocList.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewLogdeDocList.PageIndex != (GridViewLogdeDocList.PageCount - 1))
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
    protected void Fill_FY()
    {
        TF_DATA objdata = new TF_DATA();
        DataTable dt = objdata.getData("TF_EXP_Swift_FY");
        ddlYear.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "Select";
            ddlYear.DataSource = dt.DefaultView;
            ddlYear.DataTextField = "FY_YYYY";
            ddlYear.DataValueField = "FY_YY";
            ddlYear.DataBind();

        }
        else
        {
            li.Text = "No record(s) found";
        }
        ddlYear.Items.Insert(0, li);
    }
}