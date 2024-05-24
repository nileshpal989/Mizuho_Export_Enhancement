using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EDPMS_EDPMS_IINW_Help : System.Web.UI.Page
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

        string search =txtSearch.Text;

        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@bCode1", SqlDbType.VarChar);
        p1.Value = Request.QueryString["branch"].Trim();


        SqlParameter p2 = new SqlParameter("@search", SqlDbType.VarChar);
        p2.Value = Request.QueryString["txtDocNo"].ToString();

        SqlParameter p3 = new SqlParameter("@Year",SqlDbType.VarChar);
        p3.Value = Request.QueryString["Year"].ToString();

        string _query = "TF_E_FIRC_GetTTnoList";
        DataTable dt = objData.getData(_query, p1, p2,p3);
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
        string result = "";
    }
    protected void GridViewDocNoList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDocNo = new Label();
            Label lblRecdAmt = new Label();

            lblDocNo = (Label)e.Row.FindControl("lblDocNo");
            lblRecdAmt = (Label)e.Row.FindControl("lblRecdAmt");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.opener.selectDocNo('" + lblDocNo.Text + "','" + lblRecdAmt.Text + "');window.opener.EndRequest();window.close();";
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
            e.Row.Attributes["onmouseover"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
            e.Row.ToolTip = "Click to select row.";
        }

    }
    protected void btngo_Click(object sender, EventArgs e)
    {

        string search = txtSearch.Text.Trim();
        TF_DATA objData = new TF_DATA();

        SqlParameter p1 = new SqlParameter("@bCode", SqlDbType.VarChar);
        p1.Value = Request.QueryString["branch"].Trim();

        SqlParameter p2 = new SqlParameter("@search", SqlDbType.VarChar);
        p2.Value = txtSearch.Text;

        SqlParameter p3 = new SqlParameter("@Year", SqlDbType.VarChar);
        p3.Value = Request.QueryString["Year"].ToString();

        string _query = "TF_E_FIRC_GetTTnoList";
        DataTable dt = objData.getData(_query, p1, p2,p3);
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
