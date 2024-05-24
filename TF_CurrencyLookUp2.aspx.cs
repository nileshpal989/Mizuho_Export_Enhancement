using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class TF_CurrencyLookUp2 : System.Web.UI.Page
{

    TF_DATA erp = new TF_DATA();
    string query;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack != true)
        {
            grdDataBind();
            txtsearch.Focus();
        }
        txtsearch.Focus();
    }

    public void grdDataBind()
    {
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "HelpCurMstr";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            grdsearch.DataSource = dt.DefaultView;
            grdsearch.DataBind();
            grdsearch.Visible = true;
        }
        else
        {
            grdsearch.Visible = false;
        }

    }
    protected void txtsearch_TextChanged(object sender, EventArgs e)
    {
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = txtsearch.Text.Trim();
        string _query = "HelpCurMstr";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            grdsearch.DataSource = dt.DefaultView;
            grdsearch.DataBind();
            grdsearch.Visible = true;
        }
        else
        {
            grdsearch.Visible = false;
        }
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        txtsearch_TextChanged(this, null);
    }
    protected void grdsearch_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow &&
   (e.Row.RowState == DataControlRowState.Normal ||
    e.Row.RowState == DataControlRowState.Alternate))
        {
            e.Row.TabIndex = -1;
            e.Row.Attributes["onclick"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
            e.Row.Attributes["onkeydown"] = "javascript:return SelectSibling(event);";
            e.Row.Attributes["onselectstart"] = "javascript:return false;";
            e.Row.ToolTip = "Click to select row and then navigate";
        }
    }
}