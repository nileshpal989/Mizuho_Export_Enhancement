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

public partial class IDPMS_Dump_Help : System.Web.UI.Page
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
        string branch = Request.QueryString["branch"].ToString();
        string iecode = Request.QueryString["iecode"].ToString();


        SqlParameter p1 = new SqlParameter("@search", txtSearch.Text);
        SqlParameter p2 = new SqlParameter("@branch", branch);
        SqlParameter p3 = new SqlParameter("@iecode", iecode);

        string _query = "TF_GetDumpListnew";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2, p3);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewDumpList.PageSize = _pageSize;
            GridViewDumpList.DataSource = dt.DefaultView;
            GridViewDumpList.DataBind();
            GridViewDumpList.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);

        }
        else
        {
            GridViewDumpList.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }

    protected void GridViewDumpList_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridViewDumpList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblboeno = new Label();
            Label lblboedate = new Label();
            Label lblprtcd = new Label();
            Label lblcur = new Label();
            Label lblamt = new Label();
            lblboeno = (Label)e.Row.FindControl("lblboeno");
            lblboedate = (Label)e.Row.FindControl("lblboedate");
            lblprtcd = (Label)e.Row.FindControl("lblprtcode");
            lblcur = (Label)e.Row.FindControl("lblCur");
            lblamt = (Label)e.Row.FindControl("lblboebalamt");

            string pageurl = "window.opener.selectDump('" + lblboeno.Text + "','" + lblboedate.Text + "','" + lblprtcd.Text + "','" + lblcur.Text + "','" + lblamt.Text + "');window.opener.EndRequest();window.close();";
            e.Row.Attributes.Add("onclick", pageurl);
            e.Row.Attributes.Add("onkeypress", "javascript:if (event.keyCode == 13 || event.keyCode == 32) {" + pageurl + " return false; }");
            //int i = 0;
            //foreach (TableCell cell in e.Row.Cells)
            //{
            //    string pageurl = "window.opener.selectDump('" + lblboeno.Text + "','" + lblboedate.Text + "','" + lblprtcd.Text + "','" + lblcur.Text + "','" + lblamt.Text + "');window.opener.EndRequest();window.close();";
            //    //if (i != 3)
            //    cell.Attributes.Add("onclick", pageurl);
            //    //else
            //    //cell.Style.Add("cursor", "default");
            //    i++;
            //}
        }
    }

    protected void GridViewDumpList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //onmouseover AND onmouseout of gridview row, Change the color of Gridview Row
            e.Row.Attributes["onmouseover"] = string.Format("javascript:OnMouseOver(this, {0});", e.Row.RowIndex);
            e.Row.Attributes["onmouseout"] = string.Format("javascript:OnMouseOut(this, {0});", e.Row.RowIndex);
            //onFocus AND onBlur of gridview row, Change the color of Gridview Row
            e.Row.Attributes["onfocus"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
            e.Row.Attributes["onblur"] = string.Format("javascript:DisSelectRow(this, {0});", e.Row.RowIndex);
            e.Row.Attributes["onselectstart"] = "javascript:return false;";
            e.Row.ToolTip = "Click to select row.";
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TabIndex = short.Parse((e.Row.RowIndex + 2).ToString());

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
        GridViewDumpList.PageIndex = 0;
        btngo_Click(sender, e);
    }

    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewDumpList.PageIndex > 0)
        {
            GridViewDumpList.PageIndex = GridViewDumpList.PageIndex - 1;
        }
        btngo_Click(sender, e);
    }

    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewDumpList.PageIndex != GridViewDumpList.PageCount - 1)
        {
            GridViewDumpList.PageIndex = GridViewDumpList.PageIndex + 1;
        }
        btngo_Click(sender, e);
    }

    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewDumpList.PageIndex = GridViewDumpList.PageCount - 1;
        btngo_Click(sender, e);
    }

    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewDumpList.PageCount != GridViewDumpList.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewDumpList.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewDumpList.PageIndex + 1) + " of " + GridViewDumpList.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewDumpList.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewDumpList.PageIndex != (GridViewDumpList.PageCount - 1))
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