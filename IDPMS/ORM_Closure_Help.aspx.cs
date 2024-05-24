using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

public partial class IDPMS_ORM_Closure_Help : System.Web.UI.Page
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
        string branch = Request.QueryString["Branch"].ToString();
        string IECODE = Request.QueryString["IEcode"].ToString();

        SqlParameter p1 = new SqlParameter("@search", txtSearch.Text);
        SqlParameter p2 = new SqlParameter("@branch", branch);
        SqlParameter p3 = new SqlParameter("@IECode", IECODE);

        string _query = "TF_ORM_Closure_Help";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2, p3);

        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            GridViewOTTList.DataSource = dt.DefaultView;
            GridViewOTTList.DataBind();
            GridViewOTTList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;

        }
        else
        {
            GridViewOTTList.Visible = false;
            rowGrid.Visible = false;

            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }

    }
    protected void GridViewOTTList_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridViewOTTList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblormno = new Label();
            Label lblamt = new Label();
            lblormno = (Label)e.Row.FindControl("lblormno");
            lblamt = (Label)e.Row.FindControl("lblamount");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.opener.selectOrm('" + lblormno.Text + "','" + lblamt.Text + "');window.opener.EndRequest();window.close();";
                if (i != 3)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }

    protected void GridViewOTTList_RowCreated(object sender, GridViewRowEventArgs e)
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