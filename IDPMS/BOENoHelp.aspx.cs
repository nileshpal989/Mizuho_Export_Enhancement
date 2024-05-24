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


public partial class IDPMS_BOENoHelp : System.Web.UI.Page
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
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;
        SqlParameter p2 = new SqlParameter("@boeno", Request.QueryString["boeno"].ToString());
        SqlParameter p3 = new SqlParameter("@custid", Request.QueryString["custid"].ToString());
        txtSearch.Text = search;
        string _query = "BOEHelp";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2,p3);

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

        protected void GridViewBankList_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

    protected void GridViewBankList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblbillno = new Label();
            lblbillno = (Label)e.Row.FindControl("lblbillno");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "";
                //hNo = Request.QueryString["hNo"].ToString();
                pageurl = "window.opener.saveTTRefDetails('" + lblbillno.Text + "');window.opener.EndRequest();window.close();";

                if (i != 2)
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

            e.Row.ToolTip = "Click to select row.";
        }

    }

    protected void btngo_Click(object sender, EventArgs e)
    {

        fillGrid();
    }
}