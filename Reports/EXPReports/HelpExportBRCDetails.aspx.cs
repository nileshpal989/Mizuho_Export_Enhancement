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

public partial class Reports_EXPReports_HelpExportBRCDetails : System.Web.UI.Page
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

        string DocType = Request.QueryString["DocType"];

        string FromDate = Request.QueryString["FromDate"];

        string ToDate = Request.QueryString["ToDate"];

        string search = "";

        System.Globalization.DateTimeFormatInfo dateInfo1 = new System.Globalization.DateTimeFormatInfo();
        dateInfo1.ShortDatePattern = "dd/MM/yyyy";
        DateTime documentDate1 = Convert.ToDateTime(FromDate, dateInfo1);

        System.Globalization.DateTimeFormatInfo dateInfo2 = new System.Globalization.DateTimeFormatInfo();
        dateInfo2.ShortDatePattern = "dd/MM/yyyy";
        DateTime documentDate2 = Convert.ToDateTime(ToDate, dateInfo2);


        string Branch = Request.QueryString["Branch"];

        string FDoc = Request.QueryString["DocNo"];


        SqlParameter p1 = new SqlParameter("@Branch", SqlDbType.VarChar);
        p1.Value = Branch;

        SqlParameter p2 = new SqlParameter("@DocType", SqlDbType.VarChar);
        p2.Value = DocType;

        SqlParameter p3 = new SqlParameter("@FDate", SqlDbType.VarChar);
        p3.Value = documentDate1.ToString("MM/dd/yyyy");

        SqlParameter p4 = new SqlParameter("@TDate", SqlDbType.VarChar);
        p4.Value = documentDate2.ToString("MM/dd/yyyy");

        SqlParameter p5 = new SqlParameter("@FCust", SqlDbType.VarChar);
        p5.Value = FDoc;

        SqlParameter p6 = new SqlParameter("@search", SqlDbType.VarChar);
        p6.Value = search;



        string _query = "Help_Export_Report_BRCRegister";
        TF_DATA objData = new TF_DATA();

        DataTable dt = objData.getData(_query, p1, p2, p3, p4, p5 , p6);
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
                string pageurl = "";
                if (Request.QueryString["pc"].ToString() == "1")
                {
                    pageurl = "window.opener.selectUser('" + lblDocNo.Text + "');window.opener.EndRequest();window.close();";
                    //string pageurl1 = "window.opener.selectUser1('" + lblDocNo.Text + "');window.opener.EndRequest();window.close();";
                }
                if (Request.QueryString["pc"].ToString() == "2")
                {
                    pageurl = "window.opener.selectUser1('" + lblDocNo.Text + "');window.opener.EndRequest();window.close();";
                }

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
            //e.Row.Attributes["onmouseover"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
            //e.Row.Attributes["onmouseout"] = string.Format("javascript:DisSelectRow(this, {0});", e.Row.RowIndex);

            e.Row.ToolTip = "Click to select row.";
        }

    }

    protected void btngo_Click(object sender, EventArgs e)
    {
        

        string search = txtSearch.Text;

        string DocType = Request.QueryString["DocType"];

        string FromDate = Request.QueryString["FromDate"];

        string ToDate = Request.QueryString["ToDate"];

        System.Globalization.DateTimeFormatInfo dateInfo1 = new System.Globalization.DateTimeFormatInfo();
        dateInfo1.ShortDatePattern = "dd/MM/yyyy";
        DateTime documentDate1 = Convert.ToDateTime(FromDate, dateInfo1);

        System.Globalization.DateTimeFormatInfo dateInfo2 = new System.Globalization.DateTimeFormatInfo();
        dateInfo2.ShortDatePattern = "dd/MM/yyyy";
        DateTime documentDate2 = Convert.ToDateTime(ToDate, dateInfo2);

        string Branch = Request.QueryString["Branch"];

        string FDoc = Request.QueryString["DocNo"];


        SqlParameter p1 = new SqlParameter("@Branch", SqlDbType.VarChar);
        p1.Value = Branch;

        SqlParameter p2 = new SqlParameter("@DocType", SqlDbType.VarChar);
        p2.Value = DocType;

        SqlParameter p3 = new SqlParameter("@FDate", SqlDbType.VarChar);
        p3.Value = documentDate1.ToString("MM/dd/yyyy");

        SqlParameter p4 = new SqlParameter("@TDate", SqlDbType.VarChar);
        p4.Value = documentDate2.ToString("MM/dd/yyyy");

        SqlParameter p5 = new SqlParameter("@FCust", SqlDbType.VarChar);
        p5.Value = FDoc;

        SqlParameter p6 = new SqlParameter("@search", SqlDbType.VarChar);
        p6.Value = search;



        string _query = "Help_Export_Report_BRCRegister";
        TF_DATA objData = new TF_DATA();

        DataTable dt = objData.getData(_query, p1, p2, p3, p4, p5, p6);
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


