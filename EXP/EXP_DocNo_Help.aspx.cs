using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EXP_EXP_DocNo_Help : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void fillgrid()
    {
        string search = txtSearch.Text.Trim();
        string date=Request.QueryString["Document_No"].ToString();
        SqlParameter p1 = new SqlParameter("@search", search);
        SqlParameter p2 = new SqlParameter("@date", date);

        string query = "TF_EXP_DocNo_Help";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;

            GridViewDocList.DataSource = dt.DefaultView;
            GridViewDocList.DataBind();
            GridViewDocList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;

        }
        else {
            GridViewDocList.Visible = false;
            rowGrid.Visible = false;

            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        
        }
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
                string pageurl = "window.opener.selectDoc('" + lblDocNo.Text + "');window.opener.EndRequest();window.close();";
                if (i != 3)
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
        string search = txtSearch.Text.Trim();
        string date = Request.QueryString["Document_No"].ToString();
        SqlParameter p1 = new SqlParameter("@search", search);
        SqlParameter p2 = new SqlParameter("@date", date);

        string query = "TF_EXP_DocNo_Help";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;

            GridViewDocList.DataSource = dt.DefaultView;
            GridViewDocList.DataBind();
            GridViewDocList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;

        }
        else
        {
            GridViewDocList.Visible = false;
            rowGrid.Visible = false;

            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;

        }
    }
    protected void GridViewDocList_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridViewDocList_DataBound(object sender, EventArgs e)
    {

    }
}