using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class TF_ACofficerHelpL1 : System.Web.UI.Page
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
        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";

        query = "HelpACofficerId";

        SqlParameter p1 = new SqlParameter("@id", SqlDbType.VarChar);
        p1.Value = txtsearch.Text.Trim().ToString();
        SqlParameter p2 = new SqlParameter("@bid", SqlDbType.VarChar);
        p2.Value = Request.QueryString["bid"].ToString();
        SqlParameter p3 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p3.Value = Request.QueryString["Branch"].ToString();

        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(query, p1, p2,p3);

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
        catch (Exception)
        {

            //throw;
        }


    }
    protected void txtsearch_TextChanged(object sender, EventArgs e)
    {
        if (txtsearch.Text != "")
        {
            query = "HelpACofficerId";
            System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            dateInfo.ShortDatePattern = "dd/MM/yyyy";

           

            SqlParameter p1 = new SqlParameter("@id", SqlDbType.VarChar);
            p1.Value = txtsearch.Text.Trim().ToString();
            SqlParameter p2 = new SqlParameter("@bid", SqlDbType.VarChar);
            p2.Value = Request.QueryString["bid"].ToString();
            SqlParameter p3 = new SqlParameter("@BranchName", SqlDbType.VarChar);
            p3.Value = Request.QueryString["Branch"].ToString();

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


                     //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('No Data Found');", true);
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
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please Enter Beneficiary Name');", true);
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