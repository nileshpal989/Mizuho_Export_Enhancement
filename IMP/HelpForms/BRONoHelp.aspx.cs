using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class IMP_HelpForms_BRONoHelp : System.Web.UI.Page
{
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
        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";
        string date = Request.QueryString["date"].ToString();
        string Branch = Request.QueryString["Branch"].ToString();
        query = "TF_HelpBROReportDetails";
        SqlParameter p1 = new SqlParameter("@Branch", SqlDbType.VarChar);
        p1.Value = Branch;

        SqlParameter p2 = new SqlParameter("@date", SqlDbType.VarChar);
        p2.Value = date;

        SqlParameter p3 = new SqlParameter("@search", SqlDbType.VarChar);
        p3.Value = txtsearch.Text.Trim().ToString();

        TF_DATA objData = new TF_DATA();

        DataTable dt = objData.getData(query, p1, p2, p3);
        try
        {
            if (dt.Rows.Count > 0)
            {
                grdsearch.DataSource = dt;
                grdsearch.DataBind();
            }
            else
            {
                lblCustName.Visible = false;
                txtsearch.Visible = false;
                ImageButton1.Visible = false;
                NoRecords.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void txtsearch_TextChanged(object sender, EventArgs e)
    {
        if (txtsearch.Text != "")
        {
            grdDataBind();
        }
        else if (txtsearch.Text == "")
        {
            // MessageBox.Show("Please Enter Customer Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please Enter Document No.');", true);
            grdDataBind();
            txtsearch.Focus();
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