using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EXP_EXP_TTRefNo : System.Web.UI.Page
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
        string _custAcNo = Request.QueryString["CustAcNo"].ToString();
        string bcode = Request.QueryString["bcode"].ToString();

        SqlParameter p1 = new SqlParameter("@bcode", bcode);
        SqlParameter p2 = new SqlParameter("@custAcNo", _custAcNo);
        SqlParameter p3 = new SqlParameter("@search", txtSearch.Text.Trim());

        string _query = "TF_EXP_GetTTnoList";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1,p2, p3);
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
            Label lblDocNo = new Label();
            lblDocNo = (Label)e.Row.FindControl("lblDocNo");

            Label lblAmount = new Label();
            lblAmount = (Label)e.Row.FindControl("lblbalance");

            Label lbltotalamt = new Label();
            lbltotalamt = (Label)e.Row.FindControl("lblAmount");

            Label lblCurr = new Label();
            lblCurr = (Label)e.Row.FindControl("lblCurr");

            //Label lblRname = new Label();
            //lblRname = (Label)e.Row.FindControl("lblRname");

            string hNo = Request.QueryString["hNo"].ToString();
            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.opener.saveTTRefDetails('" + lblDocNo.Text + "','" + lblAmount.Text + "','" + hNo + "','" + lbltotalamt.Text + "','" + lblCurr.Text + "');window.opener.EndRequest();window.close();";
                if (i != 9)
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
