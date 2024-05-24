using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class IDPMS_TF_IDPMS_BOEClsrCan_Help : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //txtSearch.Text = Request.QueryString["txtValue"].ToString();
            fillGrid();
            txtSearch.Focus();
        }
    }

    protected void fillGrid()
    {

        string branch = Session["userADCode"].ToString();
        string IECode = Request.QueryString["IECode"].ToString();


        SqlParameter p1 = new SqlParameter("@search", txtSearch.Text);
        SqlParameter p2 = new SqlParameter("@branch", branch);
        SqlParameter p3 = new SqlParameter("@IEcode", IECode);

        string _query = "TF_IDPMS_BOEClsrCanHelp";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2 , p3);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            GridViewDumpList.DataSource = dt.DefaultView;
            GridViewDumpList.DataBind();
            GridViewDumpList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;

        }
        else
        {
            GridViewDumpList.Visible = false;
            rowGrid.Visible = false;

            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }

    }

    protected void GridViewDumpList_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridViewDumpList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblboeno = new Label();
            Label lblboedate = new Label();
            Label lblprtcd = new Label();
            //Label lblInvSr = new Label();
            Label lblInvNo = new Label();
            Label lblAdjAmt = new Label();
            Label lblAdjDate = new Label();
            Label lblAdjRef = new Label();

            lblboeno = (Label)e.Row.FindControl("lblboeno");
            lblboedate = (Label)e.Row.FindControl("lblboedate");
            lblprtcd = (Label)e.Row.FindControl("lblprtcode");
            //lblInvSr = (Label)e.Row.FindControl("lblInvSr");
            lblInvNo = (Label)e.Row.FindControl("lblInvNo");
            lblAdjAmt = (Label)e.Row.FindControl("lblAdjAmt");
            lblAdjDate = (Label)e.Row.FindControl("lblAdjDate");
            lblAdjRef = (Label)e.Row.FindControl("lblAdjRef");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.opener.selectDump('" + lblboeno.Text + "','" + lblboedate.Text + "','" + lblprtcd.Text + "','" + lblInvNo.Text + "','" + lblAdjAmt.Text + "','" + lblAdjDate.Text + "','" + lblAdjRef.Text + "');window.opener.EndRequest();window.close();";
                if (i != 7)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }

    protected void GridViewDumpList_RowCreated(object sender, GridViewRowEventArgs e)
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