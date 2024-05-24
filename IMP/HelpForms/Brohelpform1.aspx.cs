using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class IMP_HelpForms_Brohelpform1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillGrid();
        }
        txtSearch.Focus();
    }
    public void fillGrid()
    {
        string query = "";
        string date = Request.QueryString["date"].ToString();
        string Branch = Request.QueryString["Branch"].ToString();
        query = "TF_IMP_HelpBROReportDetails";
        SqlParameter p1 = new SqlParameter("@Branch", SqlDbType.VarChar);
        p1.Value = Branch;

        SqlParameter p2 = new SqlParameter("@date", SqlDbType.VarChar);
        p2.Value = date;

        SqlParameter p3 = new SqlParameter("@search", SqlDbType.VarChar);
        p3.Value = txtSearch.Text.Trim().ToString();

        TF_DATA objData = new TF_DATA();

        DataTable dt = objData.getData(query, p1, p2, p3);
        try
        {
            if (dt.Rows.Count > 0)
            {
                int _records = dt.Rows.Count;
                GridViewBROList.DataSource = dt.DefaultView;
                GridViewBROList.DataBind();
                GridViewBROList.Visible = true;
                rowGrid.Visible = true;
            }
            else
            {
                GridViewBROList.Visible = false;
                rowGrid.Visible = false;
                labelMessage.Text = "No record(s) found.";
                labelMessage.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void GridViewBROList_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridViewBROList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblBrono = new Label();
            Label lblApplicantName = new Label();
            Label lblCurrency = new Label();
            Label lblBillAmt = new Label();
            Label lblBroDate = new Label();
            lblBrono = (Label)e.Row.FindControl("lblBrono");
            lblApplicantName = (Label)e.Row.FindControl("lblApplicantName");
            lblCurrency = (Label)e.Row.FindControl("lblCurrency");
            lblBillAmt = (Label)e.Row.FindControl("lblBillAmt");
            lblBroDate = (Label)e.Row.FindControl("lblBroDate");
            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.opener.selectBroNo('" + lblBrono.Text + "','" + lblBroDate.Text + "','" + lblApplicantName.Text + "','" + lblCurrency.Text + "','" + lblBillAmt.Text + "');window.opener.EndRequest();window.close();";
                if (i != 6)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void GridViewBROList_RowCreated(object sender, GridViewRowEventArgs e)
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