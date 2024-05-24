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
public partial class Reports_EDPMS_Reports_E_FIRC_Cust_Help : System.Web.UI.Page
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

        query = "help_FIRC_Cust";
        // da=new SqlDataAdapter(query,
        DataSet ds = erp.databind(query);
        try
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                grdsearch.DataSource = ds.Tables[0];
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
        if (txtsearch.Text != "")
        {
            query = "fillgrid_help_firc";
            DataSet ds = erp.databind1para(query, txtsearch.Text.Trim());
            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdsearch.DataSource = ds.Tables[0];
                    grdsearch.DataBind();
                    txtsearch.Focus();
                }
            }
            catch (Exception)
            {

                //throw;
            }

        }
        else if (txtsearch.Text == "")
        {
            // MessageBox.Show("Please Enter Customer Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please Enter Customer  Name');", true);
            grdDataBind();
            txtsearch.Focus();
        }
    }
    protected void  ImageButton1_Click(object sender, ImageClickEventArgs e)
{
        txtsearch_TextChanged(this,null);
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