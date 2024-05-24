using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class TF_BeneficiaryLookUp2 : System.Web.UI.Page
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
        //System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        //dateInfo.ShortDatePattern = "dd/MM/yyyy";
        //string date = Request.QueryString["frdate"].ToString();
        //string date1 = Request.QueryString["todate"].ToString();
        query = "HelpBenfMstrPayNotRec";
        //DateTime documentDate = Convert.ToDateTime(date, dateInfo);
        //SqlParameter p1 = new SqlParameter("@startdate", SqlDbType.VarChar);
        //p1.Value = documentDate.ToString("MM/dd/yyyy");
        //DateTime documentDate1 = Convert.ToDateTime(date1, dateInfo);
        //SqlParameter p2 = new SqlParameter("@enddate", SqlDbType.VarChar);
        //p2.Value = documentDate1.ToString("MM/dd/yyyy");


        //SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        //p1.Value = Request.QueryString["Branch"].ToString();
        TF_DATA objData = new TF_DATA();
        //DataTable dt = objData.getData(query, p1);
        DataTable dt = objData.getData(query);

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
                // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('No Data Found');", true);
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
            query = "HelpBenfPayNotRecSaerch";
            //System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            //dateInfo.ShortDatePattern = "dd/MM/yyyy";
            //string date = Request.QueryString["frdate"].ToString();
            //DateTime documentDate = Convert.ToDateTime(date, dateInfo);
            //SqlParameter p1 = new SqlParameter("@startdate", SqlDbType.VarChar);
            //p1.Value = documentDate.ToString("MM/dd/yyyy");

            //string date1 = Request.QueryString["todate"].ToString();
            //DateTime documentDate1 = Convert.ToDateTime(date1, dateInfo);
            //SqlParameter p2 = new SqlParameter("@enddate", SqlDbType.VarChar);
            //p2.Value = documentDate1.ToString("MM/dd/yyyy");

            SqlParameter p1 = new SqlParameter("@id", SqlDbType.VarChar);
            p1.Value = txtsearch.Text.Trim().ToString();

            TF_DATA objData = new TF_DATA();
            DataTable dt = objData.getData(query, p1);

            try
            {
                if (dt.Rows.Count > 0)
                {
                    grdsearch.DataSource = dt;
                    grdsearch.DataBind();
                }
                else
                {


                    // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('No Data Found');", true);
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