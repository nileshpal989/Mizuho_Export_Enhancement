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

public partial class Reports_EXPReports_HelpExportOverdueStatement : System.Web.UI.Page
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

        System.Globalization.DateTimeFormatInfo dateInfo1 = new System.Globalization.DateTimeFormatInfo();
        dateInfo1.ShortDatePattern = "";

        string FromDate = DateTime.ParseExact(Request.QueryString["FromDate"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
        

        //DateTime documentDate1 = Convert.ToDateTime(FromDate, dateInfo1);

        
        string DocType = Request.QueryString["DocType"];
        string Branch = Request.QueryString["Branch"];
        
        string Bill_Amt = Request.QueryString["BillAmt"];
        string No_Of_Days = Request.QueryString["Days"];
        string Overseas = Request.QueryString["OverSeasBank"];
        string LC = Request.QueryString["rptLC"];
        string Loan = Request.QueryString["rptLoan"];  


        SqlParameter p1 = new SqlParameter("@DocType", SqlDbType.VarChar);
        p1.Value = DocType;
        
        SqlParameter p2 = new SqlParameter("@Branch", SqlDbType.VarChar);
        p2.Value = Branch;
        
        SqlParameter p3 = new SqlParameter("@Date", SqlDbType.VarChar);
        p3.Value = FromDate ; 

        SqlParameter p4 = new SqlParameter("@Bill_Amount", SqlDbType.VarChar);
        p4.Value = Bill_Amt ; 

        SqlParameter p5 = new SqlParameter("@No_Of_Days", SqlDbType.VarChar);
        p5.Value = No_Of_Days ; 

        SqlParameter p6 = new SqlParameter("@OverSeasBank", SqlDbType.VarChar);
        p6.Value = Overseas ;

        SqlParameter p7 = new SqlParameter("@Search", SqlDbType.VarChar);
        p7.Value = search ;

        SqlParameter p8 = new SqlParameter("@LC", SqlDbType.VarChar);
        p8.Value = LC;

        SqlParameter p9 = new SqlParameter("@Loan", SqlDbType.VarChar);
        p9.Value = Loan;


        string _query = "Help_Export_Report_OverDue_Statement";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2, p3, p4, p5, p6, p7 , p8 , p9);
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
            lblDocNo = (Label)e.Row.FindControl("lblCustName");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "";
                
                    pageurl = "window.opener.selectUser('" + lblDocNo.Text + "');window.opener.EndRequest();window.close();";
                    //string pageurl1 = "window.opener.selectUser1('" + lblDocNo.Text + "');window.opener.EndRequest();window.close();";
               
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
        
        string search = txtSearch.Text.Trim();

        string FromDate = DateTime.ParseExact(Request.QueryString["FromDate"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
        
        string DocType = Request.QueryString["DocType"];
        string Branch = Request.QueryString["Branch"];
        
        string Bill_Amt = Request.QueryString["BillAmt"];
        string No_Of_Days = Request.QueryString["Days"];
        string Overseas = Request.QueryString["OverSeasBank"];
        string LC = Request.QueryString["rptLC"];
        string Loan = Request.QueryString["rptLoan"];  


        SqlParameter p1 = new SqlParameter("@DocType", SqlDbType.VarChar);
        p1.Value = DocType;
        SqlParameter p2 = new SqlParameter("@Branch", SqlDbType.VarChar);
        p2.Value = Branch;
        
        SqlParameter p3 = new SqlParameter("@Date", SqlDbType.VarChar);
        p3.Value = FromDate ; 

        SqlParameter p4 = new SqlParameter("@Bill_Amount", SqlDbType.VarChar);
        p4.Value = Bill_Amt ; 

        SqlParameter p5 = new SqlParameter("@No_Of_Days", SqlDbType.VarChar);
        p5.Value = No_Of_Days ; 

        SqlParameter p6 = new SqlParameter("@OverSeasBank", SqlDbType.VarChar);
        p6.Value = Overseas ;

        SqlParameter p7 = new SqlParameter("@Search", SqlDbType.VarChar);
        p7.Value = search ;

        SqlParameter p8 = new SqlParameter("@LC", SqlDbType.VarChar);
        p8.Value = LC ;

        SqlParameter p9 = new SqlParameter("@Loan", SqlDbType.VarChar);
        p9.Value = Loan  ;


        string _query = "Help_Export_Report_OverDue_Statement";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2, p3, p4, p5, p6, p7 , p8 , p9);

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


