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

public partial class IMP_HelpForms_TF_IMP_Settl_for_Bank_Help : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillGrid();
        }
    }
    protected void fillGrid()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@DocScrutiny", Request.QueryString["DocScrutiny"].ToString());
        SqlParameter p2 = new SqlParameter("@BranchName", Request.QueryString["BranchName"].ToString());
        DataTable dt = objData.getData("TF_IMP_Settl_For_Bank_Local_Help", p1, p2);

        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            GridView_Settl_For_Bank_List.DataSource = dt.DefaultView;
            GridView_Settl_For_Bank_List.DataBind();
            GridView_Settl_For_Bank_List.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;
        }
        else
        {
            GridView_Settl_For_Bank_List.Visible = false;
            rowGrid.Visible = false;

            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }

    }
    protected void GridView_Settl_For_Bank_List_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridView_Settl_For_Bank_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblAbbr = new Label();
            Label lblAccCode = new Label();
            Label lblAccNo = new Label();
            lblAccNo = (Label)e.Row.FindControl("lblAccNo");
            lblAbbr = (Label)e.Row.FindControl("lblAbbr");
            lblAccCode = (Label)e.Row.FindControl("lblAccCode");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.opener.selectSettl_For_Bank_Local('" + lblAbbr.Text + "','" + lblAccCode.Text + "','" + lblAccNo.Text + "');window.opener.EndRequest();window.close();";
                if (i != 3)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void GridView_Settl_For_Bank_List_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TabIndex = -1;
            e.Row.Attributes["onmouseover"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
            e.Row.Attributes["onmouseout"] = string.Format("javascript:DisSelectRow(this, {0});", e.Row.RowIndex);
            e.Row.ToolTip = "Click to select row.";
        }
    }

}