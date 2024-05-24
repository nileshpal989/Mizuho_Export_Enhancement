using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Reports_EXPORTReports_DocumentLookUpForNonCustomer : System.Web.UI.Page
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
        string date = Request.QueryString["frdate"].ToString();
        //string DocNo = Request.QueryString["Doc"].ToString();
        string DocNo;
        string Type = Request.QueryString["rptType"].ToString();
        string Branch = Request.QueryString["branch"].ToString();
        //p2.Value = DocNo;
        if (Type == "Single")
        {
            DocNo = "";
        }
        else
        {
            DocNo = Request.QueryString["Doc"].ToString();
        }
        query = "HelpDocumentNOExportLCNonCust";

        System.Globalization.DateTimeFormatInfo dateInfo1 = new System.Globalization.DateTimeFormatInfo();
        dateInfo1.ShortDatePattern = "dd/MM/yyyy";
        DateTime documentDate1 = Convert.ToDateTime(date, dateInfo1);


        SqlParameter p1 = new SqlParameter("@DocDate", SqlDbType.VarChar);
        p1.Value = documentDate1.ToString("MM/dd/yyyy");

        SqlParameter p2 = new SqlParameter("@DocNo", SqlDbType.VarChar);
        p2.Value = DocNo;

        SqlParameter p3 = new SqlParameter("@Type", SqlDbType.VarChar);
        p3.Value = Type;

        SqlParameter p4 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p4.Value = Branch;

        TF_DATA objData = new TF_DATA();

        DataTable dt = objData.getData(query, p2, p1, p3, p4);

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
            query = "HelpDocumentNOExportLCSearchNonCust";
            System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            dateInfo.ShortDatePattern = "dd/MM/yyyy";
            string date = Request.QueryString["frdate"].ToString();
            DateTime documentDate = Convert.ToDateTime(date, dateInfo);
            SqlParameter p1 = new SqlParameter("@DocDate", SqlDbType.VarChar);
            p1.Value = documentDate.ToString("MM/dd/yyyy");
            SqlParameter p2 = new SqlParameter("@id", SqlDbType.VarChar);
            p2.Value = txtsearch.Text.Trim().ToString();
            TF_DATA objData = new TF_DATA();
            DataTable dt = objData.getData(query, p2, p1);

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