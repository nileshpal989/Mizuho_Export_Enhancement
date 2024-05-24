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

public partial class IMP_HelpForms_TF_IMP_FXHelp : System.Web.UI.Page
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
        string _query = "TF_IMP_FXHelp";
        SqlParameter p1 = new SqlParameter("@search", txtSearch.Text);
        SqlParameter p2 = new SqlParameter("@CustAbbr", Request.QueryString["CustAbbr"].ToString());
        SqlParameter p3 = new SqlParameter("@BranchName", Request.QueryString["BranchName"].ToString());
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2,p3);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            GridViewFXList.DataSource = dt.DefaultView;
            GridViewFXList.DataBind();
            GridViewFXList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;

        }
        else
        {
            GridViewFXList.Visible = false;
            rowGrid.Visible = false;

            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }

    }
    protected void GridViewFXList_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridViewFXList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblGbaseRefNo = new Label();
            Label lblContractDate = new Label();
            Label lblInternalRate = new Label();
            Label lblExchangeRate = new Label();
            Label lblContractAmount = new Label();
            Label lblContractCurrency = new Label();
            Label lblEquivalentAmount = new Label();
            Label lblEquivalentCurrency = new Label();

            lblGbaseRefNo = (Label)e.Row.FindControl("lblGbaseRefNo");
            lblContractDate = (Label)e.Row.FindControl("lblContractDate");
            lblInternalRate = (Label)e.Row.FindControl("lblInternalRate");
            lblExchangeRate = (Label)e.Row.FindControl("lblExchangeRate");
            lblContractAmount = (Label)e.Row.FindControl("lblContractAmount");
            lblContractCurrency = (Label)e.Row.FindControl("lblContractCurrency");
            lblEquivalentAmount = (Label)e.Row.FindControl("lblEquivalentAmount");
            lblEquivalentCurrency = (Label)e.Row.FindControl("lblEquivalentCurrency");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string IMPFCRefNo = Request.QueryString["IMPFCRefNo"].Trim();
                string pageurl = "window.opener.selectFX('" + IMPFCRefNo + "','" + lblGbaseRefNo.Text + "','" + lblContractDate.Text + "','" + lblInternalRate.Text + "','" + lblExchangeRate.Text + "','" + lblContractAmount.Text + "','" + lblContractCurrency.Text + "','" + lblEquivalentAmount.Text + "','" + lblEquivalentCurrency.Text + "');window.opener.EndRequest();window.close();";
                if (i != 6)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void GridViewFXList_RowCreated(object sender, GridViewRowEventArgs e)
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

