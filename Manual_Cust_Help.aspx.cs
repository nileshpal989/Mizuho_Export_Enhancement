using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Manual_Cust_Help : System.Web.UI.Page
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
        int _pageSize = 0;
        string search = Request.QueryString["CustID"].ToString();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;
        
        txtSearch.Text = search;

        SqlParameter P2 = new SqlParameter("@adcode", SqlDbType.VarChar);
        P2.Value = Request.QueryString["Branch"];
        string _query = "getCustDetails";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1,P2);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewCustAcList.PageSize = _pageSize;
            GridViewCustAcList.DataSource = dt.DefaultView;
            GridViewCustAcList.DataBind();
            GridViewCustAcList.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewCustAcList.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }

    }
    protected void GridViewCustAcList_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridViewCustAcList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblAcNo = new Label();
            lblAcNo = (Label)e.Row.FindControl("lblAcNo");


            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.opener.selectCustomer('" + lblAcNo.Text + "');window.opener.EndRequest();window.close();";
                if (i != 3)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }

    protected void GridViewCustAcList_RowCreated(object sender, GridViewRowEventArgs e)
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

        string search = txtSearch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;
        SqlParameter P2 = new SqlParameter("@adcode", SqlDbType.VarChar);
        P2.Value = Request.QueryString["Branch"];

        string _query = "getCustDetails";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1,P2);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;

            GridViewCustAcList.DataSource = dt.DefaultView;
            GridViewCustAcList.DataBind();
            GridViewCustAcList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;

        }
        else
        {
            GridViewCustAcList.Visible = false;
            rowGrid.Visible = false;

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
            if (GridViewCustAcList.PageCount != GridViewCustAcList.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewCustAcList.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewCustAcList.PageIndex + 1) + " of " + GridViewCustAcList.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewCustAcList.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewCustAcList.PageIndex != (GridViewCustAcList.PageCount - 1))
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
        GridViewCustAcList.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewCustAcList.PageIndex > 0)
        {
            GridViewCustAcList.PageIndex = GridViewCustAcList.PageIndex - 1;
        }

        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewCustAcList.PageIndex != GridViewCustAcList.PageCount - 1)
        {
            GridViewCustAcList.PageIndex = GridViewCustAcList.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewCustAcList.PageIndex = GridViewCustAcList.PageCount - 1;
        fillGrid();
    }
}