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


public partial class View_UserDetails_Admin : System.Web.UI.Page
{
    int employeeId;
    string status = "0";
    string resultupdate;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoggedUserId"] == null)
        {
            Response.Redirect("~/TF_Log_out.aspx?sessionout=yes&sessionid=" + "", true);
        }
        //string s = Session["userRole"].ToString().Trim();

        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");
            Response.Redirect("TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        if (Session["LoggedUserId"] == null)
        {
            Response.Redirect("~/TF_Logout.aspx?sessionout=yes&sessionid=" + "", true);
        }
        if (Session["userRole"].ToString().Trim() != "Admin" && Session["userRole"].ToString().Trim() != "Supervisor")
        {
            Response.Redirect("~/TF_Log_Out.aspx?sessionout=yes&sessionid=" + "", true);
        }


        else
        {

            if (!IsPostBack)
            {
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                ddlrecordperpage.SelectedValue = "20";
                //fillGrid();
                GridViewUsers.Visible = false;
                rowGrid.Visible = false;
                rowPager.Visible = false;
                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].Trim() == "added")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added.');", true);
                    }
                    else
                        if (Request.QueryString["result"].Trim() == "updated")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
                        }
                }

            }
        }
    }

    protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewUsers.PageCount != GridViewUsers.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewUsers.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewUsers.PageIndex + 1) + " of " + GridViewUsers.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }
        if (GridViewUsers.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewUsers.PageIndex != (GridViewUsers.PageCount - 1))
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
    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewUsers.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewUsers.PageIndex > 0)
        {
            GridViewUsers.PageIndex = GridViewUsers.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewUsers.PageIndex != GridViewUsers.PageCount - 1)
        {
            GridViewUsers.PageIndex = GridViewUsers.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewUsers.PageIndex = GridViewUsers.PageCount - 1;
        fillGrid();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_AddEditUser.aspx?mode=add&SecurityHeader=House Keeping", true);
    }
    protected void fillGrid()
    {
        SqlParameter p3 = new SqlParameter("@fromdate", SqlDbType.VarChar);
        p3.Value = txtFromDate.Text.ToString().Trim();
        SqlParameter p4 = new SqlParameter("@todate", SqlDbType.VarChar);
        p4.Value = txtToDate.Text.ToString().Trim();

        string query = "TF_GetUserActivityDetails";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(query, p3, p4);

        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewUsers.PageSize = _pageSize;
            GridViewUsers.DataSource = dt.DefaultView;
            GridViewUsers.DataBind();
            GridViewUsers.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewUsers.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
    protected void GridViewUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        employeeId = Convert.ToInt32(e.CommandArgument);
        UpdateEmployee();

        fillGrid();
        if (resultupdate == "1")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "window.alert('Status Updated Successfully.');", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "window.alert('Status Updation Falied.');", true);
        }
    }
    public void UpdateEmployee()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p3 = new SqlParameter("@Id", SqlDbType.Int);
        p3.Value = employeeId.ToString().Trim();
        SqlParameter p4 = new SqlParameter("@UserUpdateStatus", SqlDbType.Int);
        p3.Value = status.ToString().Trim();
        resultupdate = objData.UserStatusUpdate(employeeId, status);
        fillGrid();
    }
    protected void GridViewUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStatus = new Label();
            lblStatus = (Label)e.Row.FindControl("lblActiveStatus");

            if (lblStatus.Text == "Active")
            {
                e.Row.Cells[5].BackColor = System.Drawing.Color.LightGreen;
            }
            if (lblStatus.Text == "Inactive")
            {
                e.Row.Cells[5].BackColor = System.Drawing.Color.Tomato;
            }
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != GridViewUsers.EditIndex)
            {
                (e.Row.Cells[7].Controls[1] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to change the session status for the selected user? All unsaved data will be lost?');";
            }
        }
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void btnfillgrid_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
}
