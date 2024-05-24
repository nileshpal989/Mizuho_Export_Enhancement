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

public partial class IMP_HelpForms_TF_IMP_LCHelp : System.Web.UI.Page
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
        string _query = "TF_IMP_GetLCHelp";
        SqlParameter p1 = new SqlParameter("@search", txtSearch.Text);
        SqlParameter p2 = new SqlParameter("@CustAbbr", Request.QueryString["CustAbbr"].ToString());
        SqlParameter p3 = new SqlParameter("@RefNo", Request.QueryString["RefNo"].ToString());
        SqlParameter p4 = new SqlParameter("@LocalForeign", Request.QueryString["LocalForeign"].ToString());
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2,p3,p4);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            GridViewLCList.DataSource = dt.DefaultView;
            GridViewLCList.DataBind();
            GridViewLCList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;

        }
        else
        {
            GridViewLCList.Visible = false;
            rowGrid.Visible = false;

            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }

    }
    protected void GridViewLCList_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridViewLCList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblCustAbbr = new Label();
            Label lblLCNo = new Label();
            Label lblCurrency = new Label();
            Label lblCountryCode = new Label();
            Label lblCommodityCode = new Label();

            lblCustAbbr = (Label)e.Row.FindControl("lblCustAbbr");
            lblLCNo = (Label)e.Row.FindControl("lblLCNo");
            lblCurrency = (Label)e.Row.FindControl("lblCurrency");
            lblCountryCode = (Label)e.Row.FindControl("lblCountryCode");
            lblCommodityCode = (Label)e.Row.FindControl("lblCommodityCode");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.opener.selectLC('" + lblLCNo.Text + "','" + lblCustAbbr.Text + "','" + lblCurrency.Text + "','" + lblCountryCode.Text + "','" + lblCommodityCode.Text + "');window.opener.EndRequest();window.close();";
                if (i != 4)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void GridViewLCList_RowCreated(object sender, GridViewRowEventArgs e)
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

