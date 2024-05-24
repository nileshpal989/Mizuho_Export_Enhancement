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

public partial class IMP_HelpForms_TF_imp_IBD_SundryaccountHelp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            sundryAc_Rdbtn.Checked = true;
            fillGrid();
        }
        txtSearch.Focus();
    }
    protected void fillGrid()
    {
        string search = txtSearch.Text.Trim();
        string AccType = "";
        if (sundryAc_Rdbtn.Checked == true)
        {
            AccType = "Sundry";
        }

        else if (InteroffAc_Rdbtn.Checked == true)
        {
            AccType = "InterOffice";
        }
        else if (rdb_NostroAc.Checked == true)
        {
            AccType = "Nostro";
        }

        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        SqlParameter p2 = new SqlParameter("@AccType", SqlDbType.VarChar);
        p2.Value = AccType;


        //BranchName
        string _query = "TF_IMP_GetSundryHelp";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            GridviewSundryAccount.DataSource = dt.DefaultView;
            GridviewSundryAccount.DataBind();
            GridviewSundryAccount.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;

        }
        else
        {
            GridviewSundryAccount.Visible = false;
            rowGrid.Visible = false;

            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }

    }
    protected void GridviewSundryAccount_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridviewSundryAccount_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblAccode = new Label();
            Label lblCustAbb = new Label();
            Label lblAcno = new Label();
            lblAccode = (Label)e.Row.FindControl("lblAccode");
            lblCustAbb = (Label)e.Row.FindControl("lblCustAbb");
            lblAcno = (Label)e.Row.FindControl("lblAcno");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.opener.select_CR_Code('" + lblAccode.Text + "','" + lblCustAbb.Text + "','" + lblAcno.Text + "');window.opener.EndRequest();window.close();";
                if (i != 7)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void GridviewSundryAccount_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void sundryAc_Rdbtn_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
        txtSearch.Text = "";
    }

}