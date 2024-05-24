using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EXP_EXP_TTRefNo2_Maker : System.Web.UI.Page
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
        string _bcode = Request.QueryString["bcode"].ToString();
        string _custAcNo = Request.QueryString["CustAcNo"].ToString();
        SqlParameter p1 = new SqlParameter("@bCode", _bcode);
        SqlParameter p2 = new SqlParameter("@custAcNo", _custAcNo);
        SqlParameter p3 = new SqlParameter("@search", txtSearch.Text.Trim());

        string _query = "TF_EXP_GetTTnoList2_Maker";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2, p3);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewBankList.PageSize = _pageSize;
            GridViewBankList.DataSource = dt.DefaultView;
            GridViewBankList.DataBind();
            GridViewBankList.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);

        }
        else
        {
            GridViewBankList.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }

    }

    protected void GridViewBankList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDocNo = new Label();
            lblDocNo = (Label)e.Row.FindControl("lblDocNo");

            Label lblAmount = new Label();
            lblAmount = (Label)e.Row.FindControl("lblAmount");

            Label lbltotalamt = new Label();
            lbltotalamt = (Label)e.Row.FindControl("lblTAmount");

            Label lblCurr = new Label();
            lblCurr = (Label)e.Row.FindControl("lblCurr");

            string hNo = Request.QueryString["hNo"].ToString();
            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.opener.saveTTRefDetails('" + lblDocNo.Text + "','" + lblAmount.Text + "','" + hNo + "','" + lbltotalamt.Text + "','" + lblCurr.Text + "');window.opener.EndRequest();window.close();";
                if (i != 6)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }

    protected void GridViewBankList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TabIndex = -1;
            e.Row.Attributes["onmouseover"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
            e.Row.Attributes["onmouseout"] = string.Format("javascript:DisSelectRow(this, {0});", e.Row.RowIndex);

            // e.Row.ToolTip = "Click to select row.";
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
        GridViewBankList.PageIndex = 0;
        btngo_Click(sender, e);
    }

    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewBankList.PageIndex > 0)
        {
            GridViewBankList.PageIndex = GridViewBankList.PageIndex - 1;
        }
        btngo_Click(sender, e);
    }

    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewBankList.PageIndex != GridViewBankList.PageCount - 1)
        {
            GridViewBankList.PageIndex = GridViewBankList.PageIndex + 1;
        }
        btngo_Click(sender, e);
    }

    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewBankList.PageIndex = GridViewBankList.PageCount - 1;
        btngo_Click(sender, e);
    }

    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewBankList.PageCount != GridViewBankList.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewBankList.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewBankList.PageIndex + 1) + " of " + GridViewBankList.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewBankList.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewBankList.PageIndex != (GridViewBankList.PageCount - 1))
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