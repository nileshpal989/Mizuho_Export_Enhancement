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

public partial class IMP_HelpForms_TF_IMP_NostroBankHelp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillGrid();
        }
        txtSearch.Focus();
    }
    protected void fillGrid()
    {
        string search = txtSearch.Text;
        //BranchName
        string _query = "TF_IMP_NostroBankHelp";
        SqlParameter p1 = new SqlParameter("@search", txtSearch.Text);
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            GridViewNostroBankList.DataSource = dt.DefaultView;
            GridViewNostroBankList.DataBind();
            GridViewNostroBankList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;
        }
        else
        {
            GridViewNostroBankList.Visible = false;
            rowGrid.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
    protected void GridViewNostroBankList_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridViewNostroBankList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "";
                Label lblCustAbbr = new Label();
                Label lblCurrency = new Label();
                Label lblSwiftCode = new Label();

                lblCustAbbr = (Label)e.Row.FindControl("lblCustAbbr");
                lblCurrency = (Label)e.Row.FindControl("lblCurrency");
                lblSwiftCode = (Label)e.Row.FindControl("lblSwiftCode");
                if (Request.QueryString["Swift"].ToString() == "MT200")
                {
                    pageurl = "window.opener.select_NostroBank_MT200('" + lblCustAbbr.Text + "','" + lblCurrency.Text + "','" + lblSwiftCode.Text + "');window.opener.EndRequest();window.close();";
                }
                if (Request.QueryString["Swift"].ToString() == "MT103")
                {
                    pageurl = "window.opener.select_NostroBank_MT103('" + lblCustAbbr.Text + "','" + lblCurrency.Text + "','" + lblSwiftCode.Text + "');window.opener.EndRequest();window.close();";
                }
                if (i != 2)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void GridViewNostroBankList_RowCreated(object sender, GridViewRowEventArgs e)
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

