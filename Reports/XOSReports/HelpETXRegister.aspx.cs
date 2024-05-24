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
using System.Text.RegularExpressions;

public partial class Reports_XOSReports_HelpETXRegister : System.Web.UI.Page
{
    private string SearchString = "";
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
        string Branch = Request.QueryString["Branch"];

        SqlParameter p1 = new SqlParameter("@FromDate", SqlDbType.VarChar);
        p1.Value = FromDate;

        SqlParameter p2 = new SqlParameter("@ToDate", SqlDbType.VarChar);
        p2.Value = ToDate;

        SqlParameter p3 = new SqlParameter("@Branch", SqlDbType.VarChar);
        p3.Value = Branch;

        SqlParameter p4 = new SqlParameter("@Search", SqlDbType.VarChar);
        p4.Value = search;

        txtSearch.Text = search;
        string _query = "HelpCustETXRegister";
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
            lblCustNames.Visible = false;
            txtSearch.Visible = false;
            btngo.Visible = false;
            btnCloseMe.Visible = false;
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
            e.Row.Attributes["onselectstart"] = "javascript:return false;";
            e.Row.ToolTip = "Click to select row.";
        }

    }

    protected void btngo_Click(object sender, EventArgs e)
    {
        string FromDate = Request.QueryString["fromDate"];
        string ToDate = Request.QueryString["toDate"];
        string search = txtSearch.Text;
        System.Globalization.DateTimeFormatInfo dateInfo1 = new System.Globalization.DateTimeFormatInfo();
        dateInfo1.ShortDatePattern = "dd/MM/yyyy";
        DateTime documentDate1 = Convert.ToDateTime(FromDate, dateInfo1);
        string Branch = Request.QueryString["Branch"];


        SqlParameter p1 = new SqlParameter("@FromDate", SqlDbType.VarChar);
        p1.Value = Request.QueryString["fromDate"].ToString();
        SqlParameter p2 = new SqlParameter("@ToDate", SqlDbType.VarChar);
        p2.Value = Request.QueryString["toDate"].ToString();
        SqlParameter p3 = new SqlParameter("@Branch", SqlDbType.VarChar);
        p3.Value = Branch;
        SqlParameter p4 = new SqlParameter("@Search", SqlDbType.VarChar);
        p4.Value = search;
        string _query = "HelpCustETXRegister";
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


    /// <summary>
    ///  This For GridView Search Button To HighLight Value In The Gridview -This Made By  Manish Maurya
    /// </summary>
    /// <param name="InputTxt"></param>
    /// <returns></returns>
    public string HighlightText(string InputTxt)
    {
        string Search_Str = txtSearch.Text;
        // Setup the regular expression and add the Or operator.
        Regex RegExp = new Regex(Search_Str.Replace(" ", "|").Trim(), RegexOptions.IgnoreCase);
        // Highlight keywords by calling the
        //delegate each time a keyword is found.
        return RegExp.Replace(InputTxt, new MatchEvaluator(ReplaceKeyWords));
    }

    public string ReplaceKeyWords(Match m)
    {
        return ("<span class=highlight>" + m.Value + "</span>");
    }

    protected void txtSearch_TextChanged1(object sender, EventArgs e)
    {

    }
    //protected void TimerTick(object sender, EventArgs e)
    //{
    //    this.fillGrid();
    //    Timer1.Enabled = false;
       
    //}
}


    

    


    

