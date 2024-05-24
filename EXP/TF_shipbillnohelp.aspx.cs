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


public partial class EXP_TF_shipbillnohelp : System.Web.UI.Page
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

        string search = Request.QueryString["shipbillno"].ToString();
        string custacno = Request.QueryString["custacno"].ToString();

        if (search != "")
        {
            txtSearch.Text = search;

        }
        else
        {
            search = txtSearch.Text;
            
        }

        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;
        SqlParameter p2 = new SqlParameter("@custacno", custacno);

        string _query = "Shipbillnohelp";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1,p2);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;

            GridViewBankList.DataSource = dt.DefaultView;
            GridViewBankList.DataBind();
            GridViewBankList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;

        }
        else
        {
            GridViewBankList.Visible = false;
            rowGrid.Visible = false;

            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }

    }

    protected void GridViewBankList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblshipbillno = new Label();
            Label lblshipbilldate = new Label();
            Label lblportcode = new Label();

            lblshipbillno = (Label)e.Row.FindControl("lblshipbillno");
            lblshipbilldate = (Label)e.Row.FindControl("lblshipbilldate");
            lblportcode = (Label)e.Row.FindControl("lblportcode");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.opener.selectshipbillno('" + lblshipbillno.Text + "','" + lblshipbilldate.Text + "','" + lblportcode.Text + "');window.opener.EndRequest();window.close();";
                if (i != 6)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }

    protected void GridViewBankList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TabIndex = -1;
            e.Row.Attributes["onmouseover"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
            e.Row.Attributes["onmouseout"] = string.Format("javascript:DisSelectRow(this, {0});", e.Row.RowIndex);

            // e.Row.ToolTip = "Click to select row.";
        }

    }

    protected void btngo_Click(object sender, EventArgs e)
    {

        fillGrid();
    }
}