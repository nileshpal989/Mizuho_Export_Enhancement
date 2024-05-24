using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class IMP_Transactions_TF_ViewLEIEmaildetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                hdnUserRole.Value = Session["userRole"].ToString().Trim();
                ddlrecordperpage.SelectedValue = "20";
                fillGrid();
                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].Trim() == "added")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "alert('Record added successfully.');", true);
                    }
                    else
                        if (Request.QueryString["result"].Trim() == "updated")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "alert('Record updated successfully.');", true);
                        }
                } txtSearch.Attributes.Add("onkeypress", "return submitForm(event);");
                btnSearch.Attributes.Add("onclick", "return validateSearch();");
            }
        }
    }
    protected void GridViewLeiEmail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
        string _reporttype = "";
        string _module = "";
        string[] values_P;
        string AllID = e.CommandArgument.ToString();
        if (AllID != "")
        {
            char[] splitchar = { ';' };
            values_P = AllID.Split(splitchar);
            _reporttype = values_P[0].ToString();
            _module = values_P[1].ToString();
        }
        SqlParameter p1 = new SqlParameter("@Report_Type", SqlDbType.VarChar);
        p1.Value = _reporttype;
        SqlParameter p2 = new SqlParameter("@Module", SqlDbType.VarChar);
        p2.Value = _module;

        string query = "TF_DeleteLEIEmaildetails";

        TF_DATA objdata = new TF_DATA();
        result = objdata.SaveDeleteData(query, p1, p2);
        fillGrid();
        if (result == "deleted")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "deletemessage", "alert('Record Deleted.')'", true);
        }
    }
    protected void GridViewLeiEmail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblReportType = new Label();
            Button btnDelete = new Button();
            Label lblModule = new Label();

            lblReportType = (Label)e.Row.FindControl("lblReportType");
            lblModule = (Label)e.Row.FindControl("lblModule");
            btnDelete = (Button)e.Row.FindControl("btnDelete");
            btnDelete.Enabled = true;
            btnDelete.CssClass = "deleteButton";
            btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string url = "window.location='TF_LEI_Mail_details.aspx?mode=edit&reporttype=" + lblReportType.Text.Trim() + "&module=" + lblModule.Text.Trim() + "'";
                if (i != 3)
                    cell.Attributes.Add("onclick", url);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_LEI_Mail_details.aspx?mode=add", true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewLeiEmail.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewLeiEmail.PageIndex > 0)
        {
            GridViewLeiEmail.PageIndex = GridViewLeiEmail.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewLeiEmail.PageIndex != GridViewLeiEmail.PageCount - 1)
        {
            GridViewLeiEmail.PageIndex = GridViewLeiEmail.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewLeiEmail.PageIndex = GridViewLeiEmail.PageCount - 1;
        fillGrid();
    }
    protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void fillGrid()
    {
        string _search = txtSearch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = _search;

        string query = "TF_Get_LEI_Emaildetails";
        TF_DATA objdata = new TF_DATA();
        DataTable dt = objdata.getData(query, p1);

        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewLeiEmail.PageSize = _pageSize;
            GridViewLeiEmail.DataSource = dt.DefaultView;
            GridViewLeiEmail.DataBind();
            GridViewLeiEmail.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewLeiEmail.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No Record(s) found.";
            labelMessage.Visible = true;
        }
    }
    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewLeiEmail.PageCount != GridViewLeiEmail.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewLeiEmail.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewLeiEmail.PageIndex + 1) + " of " + GridViewLeiEmail.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewLeiEmail.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewLeiEmail.PageIndex != (GridViewLeiEmail.PageCount - 1))
        {
            pgcontrols.enablelastnav(btnnavnext, btnnavlast);
        }
        else
        {
            pgcontrols.disablelastnav(btnnavnext, btnnavlast);
        }
    }
    private void navigationVisibility(Boolean visibility)
    {
        lblpageno.Visible = visibility;
        lblrecordno.Visible = visibility;
        btnnavfirst.Visible = visibility;
        btnnavlast.Visible = visibility;
        btnnavnext.Visible = visibility;
        btnnavpre.Visible = visibility;
    }
}