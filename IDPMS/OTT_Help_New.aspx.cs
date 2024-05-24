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

public partial class IDPMS_OTT_Help_New : System.Web.UI.Page
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
        string iecode = Request.QueryString["iecode"].ToString();


        SqlParameter p1 = new SqlParameter("@search", txtSearch.Text);
        SqlParameter p2 = new SqlParameter("@branch", branch);
        SqlParameter p3 = new SqlParameter("@iecode", iecode);

        string _query = "TF_GetOTTListnew1";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2, p3);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            GridViewOTTList.DataSource = dt.DefaultView;
            GridViewOTTList.DataBind();
            GridViewOTTList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;

        }
        else
        {
            GridViewOTTList.Visible = false;
            rowGrid.Visible = false;

            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }

    }
    protected void GridViewOTTList_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridViewOTTList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblottno = new Label();
            Label lbldate = new Label();
            Label lblamt = new Label();
            Label lblCur = new Label();
            lblottno = (Label)e.Row.FindControl("lblottno");
            lbldate = (Label)e.Row.FindControl("lblottdate");
            lblamt = (Label)e.Row.FindControl("lblamount");
            lblCur = (Label)e.Row.FindControl("lblCur");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.opener.selectOtt('" + lblottno.Text + "','" + lbldate.Text + "','" + lblamt.Text + "','" + lblCur.Text + "');window.opener.EndRequest();window.close();";
                //if (i != 3)
                    cell.Attributes.Add("onclick", pageurl);
                //else
                //    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }

    protected void GridViewOTTList_RowCreated(object sender, GridViewRowEventArgs e)
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