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

public partial class EXP_EXP_DocumentNoLookUpforCopy : System.Web.UI.Page
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

        string search = txtSearch.Text;

        TF_DATA objData = new TF_DATA();

        SqlParameter p2 = new SqlParameter("@custAcNo", SqlDbType.VarChar);
        p2.Value = Request.QueryString["custAcNo"].Trim();

        SqlParameter p4 = new SqlParameter("@bcode", SqlDbType.VarChar);
        p4.Value = Request.QueryString["bcode"].Trim();

        SqlParameter p3 = new SqlParameter("@search", SqlDbType.VarChar);
        p3.Value = search;

        string _query = "TF_EXP_GetDocNoListForCopy";
        DataTable dt = objData.getData(_query, p2, p4, p3);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;

            GridViewDocNoList.DataSource = dt.DefaultView;
            GridViewDocNoList.DataBind();
            GridViewDocNoList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;

        }
        else
        {
            GridViewDocNoList.Visible = false;
            rowGrid.Visible = false;

            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }

    }
    protected void GridViewDocNoList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    protected void GridViewDocNoList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDocNo = new Label();


            lblDocNo = (Label)e.Row.FindControl("lblDocNo");


            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.opener.selectDocNoCopy('" + lblDocNo.Text + "');window.opener.EndRequest();window.close();";
                if (i != 6)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }

    protected void GridViewDocNoList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TabIndex = -1;
            //e.Row.Attributes["onmouseover"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
            e.Row.ToolTip = "Click to select row.";
        }

    }
    protected void btngo_Click(object sender, EventArgs e)
    {

        string search = txtSearch.Text.Trim();
        TF_DATA objData = new TF_DATA();

        SqlParameter p2 = new SqlParameter("@custAcNo", SqlDbType.VarChar);
        p2.Value = Request.QueryString["custAcNo"].Trim();

        SqlParameter p3 = new SqlParameter("@search", SqlDbType.VarChar);
        p3.Value = search;

        string _query = "TF_EXP_GetDocNoListForCopy";
        DataTable dt = objData.getData(_query, p2, p3);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;

            GridViewDocNoList.DataSource = dt.DefaultView;
            GridViewDocNoList.DataBind();
            GridViewDocNoList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;

        }
        else
        {
            GridViewDocNoList.Visible = false;
            rowGrid.Visible = false;

            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
}
