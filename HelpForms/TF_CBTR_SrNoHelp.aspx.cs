using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class TF_CBTR_SrNoHelp : System.Web.UI.Page
{
    TF_DATA erp = new TF_DATA();
    string query;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack != true)
        {
            //txtsearch.Text = Request.QueryString["docno"].ToString();
            grdDataBind();
            txtsearch.Focus();
        }
        txtsearch.Focus();
    }

    public void grdDataBind()
    {
        string i = txtsearch.Text.Trim();

        string MonthYear = Request.QueryString["MonthYear"].ToString();
       
        query = "TF_HelpSrNo_CBTR";
        SqlParameter p1 = new SqlParameter("@id", SqlDbType.VarChar);
        p1.Value = i;

        SqlParameter p2 = new SqlParameter("@MonthYear", SqlDbType.VarChar);
        p2.Value = MonthYear;
      
        // da=new SqlDataAdapter(query,
        DataTable dt = erp.getData(query, p1, p2);
        try
        {
            if (dt.Rows.Count > 0)
            {
                grdsearch.DataSource = dt.DefaultView;
                grdsearch.DataBind();
            }
        }
        catch (Exception)
        {

            //throw;
        }


    }
    protected void txtsearch_TextChanged(object sender, EventArgs e)
    {
        grdDataBind();
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
