using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class TF_Ref_No_Help : System.Web.UI.Page
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
        string search = "";
        string adCode = Request.QueryString["adcode"];
        string cust = Request.QueryString["cust"];
        string fromdate = Request.QueryString["fromdate"];
        string todate = Request.QueryString["todate"];

        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        SqlParameter p2 = new SqlParameter("@adcode", SqlDbType.VarChar);
        p2.Value = adCode;
        SqlParameter p3 = new SqlParameter("@cust", SqlDbType.VarChar);
        p3.Value = cust;
        SqlParameter p4 = new SqlParameter("@fromdate", SqlDbType.VarChar);
        p4.Value = fromdate;
        SqlParameter p5 = new SqlParameter("@todate", SqlDbType.VarChar);
        p5.Value = todate;

        string _query = "IDPMS_Get_ORM_RefNo_Details";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2, p3, p4, p5);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;

            GridViewUserList.DataSource = dt.DefaultView;
            GridViewUserList.DataBind();
            GridViewUserList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;

        }
        else
        {
            GridViewUserList.Visible = false;
            rowGrid.Visible = false;

            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }

    }
    protected void GridViewUserList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
    }
    protected void GridViewUserList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDocNo = new Label();
            lblDocNo = (Label)e.Row.FindControl("lblDocNo");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.opener.selectDocument('" + lblDocNo.Text + "');window.opener.EndRequest();window.close();";
                if (i != 2)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }

    protected void GridViewUserList_RowCreated(object sender, GridViewRowEventArgs e)
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
        string search = "";
        string adCode = Request.QueryString["adcode"];
        string cust = Request.QueryString["cust"];
        string fromdate = Request.QueryString["fromdate"];
        string todate = Request.QueryString["todate"];

        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = txtSearch.Text;
        SqlParameter p2 = new SqlParameter("@adcode", SqlDbType.VarChar);
        p2.Value = adCode;
        SqlParameter p3 = new SqlParameter("@cust", SqlDbType.VarChar);
        p3.Value = cust;
        SqlParameter p4 = new SqlParameter("@fromdate", SqlDbType.VarChar);
        p4.Value = fromdate;
        SqlParameter p5 = new SqlParameter("@todate", SqlDbType.VarChar);
        p5.Value = todate;

        string _query = "IDPMS_Get_ORM_RefNo_Details";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2, p3, p4, p5);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;

            GridViewUserList.DataSource = dt.DefaultView;
            GridViewUserList.DataBind();
            GridViewUserList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;

        }
        else
        {
            GridViewUserList.Visible = false;
            rowGrid.Visible = false;

            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
}
