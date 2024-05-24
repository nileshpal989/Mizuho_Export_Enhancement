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
public partial class Reports_GuaranteeReports_CustomerHelp : System.Web.UI.Page
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



        string FromDate = Request.QueryString["fromDate"];
        string ToDate = Request.QueryString["toDate"];
        string search = Request.QueryString["cust"];

        //System.Globalization.DateTimeFormatInfo dateInfo1 = new System.Globalization.DateTimeFormatInfo();
        //dateInfo1.ShortDatePattern = "dd/MM/yyyy";
        //DateTime documentDate1 = Convert.ToDateTime(FromDate, dateInfo1);



        //System.Globalization.DateTimeFormatInfo dateInfo2 = new System.Globalization.DateTimeFormatInfo();
        //dateInfo2.ShortDatePattern = "dd/MM/yyyy";
        //DateTime documentDate2 = Convert.ToDateTime(ToDate, dateInfo2);



        string Branch = Request.QueryString["Branch"];


        SqlParameter p1 = new SqlParameter("@FromDate", SqlDbType.VarChar);
        p1.Value = FromDate;

          SqlParameter p2 = new SqlParameter("@ToDate", SqlDbType.VarChar);
          p2.Value = ToDate;

        SqlParameter p3 = new SqlParameter("@Branch", SqlDbType.VarChar);
        p3.Value = Branch;

        SqlParameter p4 = new SqlParameter("@search", SqlDbType.VarChar);
        p4.Value = search;


        string _query = "HelpCustMaster_ExportRealisation";
        TF_DATA objData = new TF_DATA();

        DataTable dt = objData.getData(_query, p1, p2, p3,p4);
        
        
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
            Label lblAccNo = new Label();
            lblAccNo = (Label)e.Row.FindControl("lblAccNo");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "";

                pageurl = "window.opener.selectUser('" + lblAccNo.Text + "');window.opener.EndRequest();window.close();";
                    //string pageurl1 = "window.opener.selectUser1('" + lblDocNo.Text + "');window.opener.EndRequest();window.close();";
                

                if (i != 2)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;

                //return false;
            }
        }
    }

    protected void GridViewUserList_RowCreated(object sender, GridViewRowEventArgs e)
    {

        //if (e.Row.RowType == DataControlRowType.DataRow &&
        //(e.Row.RowState == DataControlRowState.Normal ||
        //e.Row.RowState == DataControlRowState.Alternate))
        //{
        //    e.Row.TabIndex = -1;
        //    e.Row.Attributes["onclick"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
        //    e.Row.Attributes["onkeydown"] = "javascript:return SelectSibling(event);";
        //    e.Row.Attributes["onselectstart"] = "javascript:return false;";
        //    e.Row.ToolTip = "Click to select row and then navigate";
        //}



        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TabIndex = -1;
            e.Row.Attributes["onmouseover"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
            e.Row.Attributes["onmouseout"] = string.Format("javascript:DisSelectRow(this, {0});", e.Row.RowIndex);
            e.Row.Attributes["onselectstart"] = "javascript:return false;";
            e.Row.ToolTip = "Click to select row.";
        }

    }

    protected void btngo_Click(object sender, EventArgs e)
    {
        string FromDate = Request.QueryString["fromDate"];
        string ToDate = Request.QueryString["toDate"];

        string search = txtSearch.Text ;

        System.Globalization.DateTimeFormatInfo dateInfo1 = new System.Globalization.DateTimeFormatInfo();
        dateInfo1.ShortDatePattern = "dd/MM/yyyy";
        DateTime documentDate1 = Convert.ToDateTime(FromDate, dateInfo1);



        string Branch = Request.QueryString["Branch"];


        SqlParameter p1 = new SqlParameter("@FromDate", SqlDbType.VarChar);
        p1.Value =  Request.QueryString["fromDate"].ToString();
      

        SqlParameter p2 = new SqlParameter("@ToDate", SqlDbType.VarChar);
        p2.Value = Request.QueryString["toDate"].ToString();


        SqlParameter p3 = new SqlParameter("@Branch", SqlDbType.VarChar);
        p3.Value = Branch;

        SqlParameter p4 = new SqlParameter("@search", SqlDbType.VarChar);
        p4.Value = search;


        string _query = "HelpCustMaster_ExportRealisation";
        TF_DATA objData = new TF_DATA();

        DataTable dt = objData.getData(_query, p1, p2, p3,p4);
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


