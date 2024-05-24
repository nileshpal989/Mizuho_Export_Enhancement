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

public partial class IMP_HelpForms_TF_IMP_MintHelp : System.Web.UI.Page
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
        string _query = "TF_IMP_MintHelp";
        SqlParameter p1 = new SqlParameter("@search", txtSearch.Text);
        string BranchCode=Request.QueryString["DocNo"].ToString().Substring(4,3);
        SqlParameter p2 = new SqlParameter("@BranchCode", BranchCode);
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1,p2);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            GridViewMintList.DataSource = dt.DefaultView;
            GridViewMintList.DataBind();
            GridViewMintList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;

        }
        else
        {
            GridViewMintList.Visible = false;
            rowGrid.Visible = false;

            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }

    }
    protected void GridViewMintList_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridViewMintList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblGbaseRefNo = new Label();
            Label lblNumberOfDays = new Label();
            Label lblCustAbbr = new Label();
            Label lblPrincipalAmount = new Label();
            Label lblFinalDueDate = new Label();
            Label lblInterestRate = new Label();
            Label lblTransSpread = new Label();
            Label lblInterestAmount = new Label();
            Label lblInternalRate = new Label();

            lblGbaseRefNo = (Label)e.Row.FindControl("lblGbaseRefNo");
            lblNumberOfDays = (Label)e.Row.FindControl("lblNumberOfDays");
            lblCustAbbr = (Label)e.Row.FindControl("lblCustAbbr");
            lblPrincipalAmount = (Label)e.Row.FindControl("lblPrincipalAmount");
            lblFinalDueDate = (Label)e.Row.FindControl("lblFinalDueDate");
            lblInterestRate = (Label)e.Row.FindControl("lblInterestRate");
            lblTransSpread = (Label)e.Row.FindControl("lblTransSpread");
            lblInterestAmount = (Label)e.Row.FindControl("lblInterestAmount");
            lblInternalRate = (Label)e.Row.FindControl("lblInternalRate");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.opener.selectMint('" + lblGbaseRefNo.Text + "','" + lblNumberOfDays.Text + "','" + lblCustAbbr.Text + "','" + lblPrincipalAmount.Text + "','" + lblFinalDueDate.Text + "','" + lblInterestRate.Text + "','" + lblTransSpread.Text + "','" + lblInterestAmount.Text + "','" + lblInternalRate.Text + "');window.opener.EndRequest();window.close();";
                if (i != 7)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void GridViewMintList_RowCreated(object sender, GridViewRowEventArgs e)
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

