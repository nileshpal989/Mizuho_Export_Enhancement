using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class IMP_HelpForms_TF_IMP_MiscellaneousDocNoHelp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillGrid();
        }
        txtSearch.Focus();
    }
    protected void fillGrid()
    {
        string search = txtSearch.Text;
        string Fromdate = Request.QueryString["Fromdate"].ToString();
        string ToDate = Request.QueryString["ToDate"].ToString();
        //BranchName
        string _query = "TF_IMP_MiscellaneousDocNoHelp";
        SqlParameter p1 = new SqlParameter("@search", txtSearch.Text);
        SqlParameter p2 = new SqlParameter("@Fromdate", Fromdate);
        SqlParameter p3 = new SqlParameter("@ToDate", ToDate);
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2, p3);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            GridViewMiscellaneousDocNO.DataSource = dt.DefaultView;
            GridViewMiscellaneousDocNO.DataBind();
            GridViewMiscellaneousDocNO.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;
        }
        else
        {
            GridViewMiscellaneousDocNO.Visible = false;
            rowGrid.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
    protected void GridViewMiscellaneousDocNO_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                Label lblDocNo = new Label();
                Label lblOwnLCDiscount_Type = new Label();

                lblDocNo = (Label)e.Row.FindControl("lblDocNo");
                lblOwnLCDiscount_Type = (Label)e.Row.FindControl("lblOwnLCDiscount_Type");

                string pageurl = "window.opener.selectDocNo('" + lblDocNo.Text + "','" + lblOwnLCDiscount_Type.Text + "');window.opener.EndRequest();window.close();";
                if (i != 2)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void GridViewMiscellaneousDocNO_RowCreated(object sender, GridViewRowEventArgs e)
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
}

