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
        }
    }

    protected void fillGrid()
    {

        string branch = Request.QueryString["branch"].ToString();
        string acno = Request.QueryString["acno"].ToString();


        SqlParameter p1 = new SqlParameter("@search", txtSearch.Text);
        SqlParameter p2 = new SqlParameter("@branch", branch);
        SqlParameter p3 = new SqlParameter("@iecode", acno);

        string _query = "TF_GetDumpListnew_1";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2, p3);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            GridViewDumpList.DataSource = dt.DefaultView;
            GridViewDumpList.DataBind();
            GridViewDumpList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;

        }
        else
        {
            GridViewDumpList.Visible = false;
            rowGrid.Visible = false;

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
            lblboeno = (Label)e.Row.FindControl("lblboeno");
            lblboedate = (Label)e.Row.FindControl("lblboedate");
            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.opener.selectDump('" + lblboeno.Text + "','" + lblboedate.Text + "');window.opener.EndRequest();window.close();";
                if (i != 3)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }

    protected void GridViewDumpList_RowCreated(object sender, GridViewRowEventArgs e)
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