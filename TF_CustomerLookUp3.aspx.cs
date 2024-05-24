using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class TF_CustomerLookUp3 : System.Web.UI.Page
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
        string branchCode = Request.QueryString["branchCode"].ToString();
        SqlParameter p1 = new SqlParameter("@branchCode",branchCode);
        SqlParameter p2 = new SqlParameter("@search", txtSearch.Text.Trim());

        string _query = "TF_GetCustACDetails";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1,p2);
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
    protected void GridViewCustAcList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
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
                if (i != 6)
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
        string branchCode = Request.QueryString["branchCode"].ToString();
        SqlParameter p1 = new SqlParameter("@branchCode",branchCode);
        SqlParameter p2 = new SqlParameter("@search", txtSearch.Text.Trim());

        string _query = "TF_GetCustACDetails";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1,p2);
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
}
