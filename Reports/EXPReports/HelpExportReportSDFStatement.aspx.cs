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

public partial class Reports_EXPReports_HelpExportReportSDFStatement : System.Web.UI.Page
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
        string search = txtSearch.Text.Trim();
        string fromDate = Request.QueryString["fromDate"].ToString();
        string Branch = Request.QueryString["Branch"].ToString();
        string toDate = Request.QueryString["toDate"].ToString();
       
        SqlParameter p1 = new SqlParameter("@fromDate", fromDate);
        SqlParameter p2 = new SqlParameter("@toDate", toDate);
        SqlParameter p3 = new SqlParameter("@Branch", Branch);
        SqlParameter p4 = new SqlParameter("@Search", search);
        string _query = "Help_Export_SDF_Statement";
        TF_DATA objData = new TF_DATA();

        DataTable dt = objData.getData(_query, p1, p2, p3, p4);
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

            Label lblCustName = new Label();
            lblCustName = (Label)e.Row.FindControl("lblCustName");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "";

                pageurl = "window.opener.selectUser('" + lblDocNo.Text + "','" + lblCustName.Text + "');window.opener.EndRequest();window.close();";
                    //string pageurl1 = "window.opener.selectUser1('" + lblDocNo.Text + "');window.opener.EndRequest();window.close();";
                         
                if (i != 5)
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
            e.Row.ToolTip = "Click to select row.";
        }
    }
    protected void btngo_Click(object sender, EventArgs e)
    {
        //string search = "";
        //string fromDate = Request.QueryString["fromDate"];
        //string Branch = Request.QueryString["Branch"];
        //string toDate = Request.QueryString["toDate"];

        //SqlParameter p1 = new SqlParameter("@fromDate", fromDate);
        //SqlParameter p2 = new SqlParameter("@toDate", toDate);
        //SqlParameter p3 = new SqlParameter("@Branch", Branch);
        //SqlParameter p4 = new SqlParameter("@Search", search);
        //string _query = "Help_Export_SDF_Statement";
        //TF_DATA objData = new TF_DATA();
        //DataTable dt = objData.getData(_query, p1, p2, p3, p4);
        //if (dt.Rows.Count > 0)
        //{
        //    int _records = dt.Rows.Count;
        //    GridViewUserList.DataSource = dt.DefaultView;
        //    GridViewUserList.DataBind();
        //    GridViewUserList.Visible = true;
        //    rowGrid.Visible = true;
        //    labelMessage.Visible = false;
        //}
        //else
        //{
        //    GridViewUserList.Visible = false;
        //    rowGrid.Visible = false;
        //    labelMessage.Text = "No record(s) found.";
        //    labelMessage.Visible = true;
        //}
        fillGrid();
    }
}


