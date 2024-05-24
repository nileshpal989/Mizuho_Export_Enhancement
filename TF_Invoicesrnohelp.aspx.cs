using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data;

public partial class TF_Invoicesrnohelp : System.Web.UI.Page
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
        string shipbillno=Request.QueryString["shipbillno"].ToString();
        string shipbilldate = Request.QueryString["shipbilldate"].ToString();
        string portcode=Request.QueryString["portcode"].ToString();
        string custid=Request.QueryString["custid"].ToString();


        SqlParameter p1 = new SqlParameter("@shipbillno", shipbillno);
        SqlParameter p2 = new SqlParameter("@shipbilldate", shipbilldate);
        SqlParameter p3 = new SqlParameter("@portcode", portcode);
        SqlParameter p4 = new SqlParameter("@custid", custid);
        SqlParameter p5 = new SqlParameter("@search", txtSearch.Text);

       // p1.Value = txtSearch.Text.Trim();

        string _query = "HelpInvSrNo";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1,p2,p3,p4,p5);
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
            lblSatetNames.Visible = false;
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
            Label lblinvsrno = new Label();
            lblinvsrno = (Label)e.Row.FindControl("lblinvsrno");
            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "";
                pageurl = "window.opener.selectinvsrno('" + lblinvsrno.Text + "');window.opener.EndRequest();window.close();";
                if (i != 3)
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

        fillGrid();

      
    }
}