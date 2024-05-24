using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EXP_EXP_ViewAutorisedSignatory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                ddlBranch.Focus();
                ddlrecordperpage.SelectedValue = "20";
                
                fillGrid();
                fillBranch();
                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].Trim() == "added")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added.');", true);
                    }
                    else if (Request.QueryString["result"].Trim() == "updated")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
                    }
                } 
                btnAdd.Attributes.Add("onclick", "return validate();");
                btnSearch.Attributes.Add("onclick", "return validateSearch();");
            }
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Focus();
        fillGrid();
    }

    protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }

    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewAuthorisedSignatoryList.PageIndex = 0;
        fillGrid();
    }

    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewAuthorisedSignatoryList.PageIndex > 0)
        {
            GridViewAuthorisedSignatoryList.PageIndex = GridViewAuthorisedSignatoryList.PageIndex - 1;
        }
        fillGrid();
    }

    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewAuthorisedSignatoryList.PageIndex != GridViewAuthorisedSignatoryList.PageCount - 1)
        {
            GridViewAuthorisedSignatoryList.PageIndex = GridViewAuthorisedSignatoryList.PageIndex + 1;
        }
        fillGrid();
    }

    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewAuthorisedSignatoryList.PageIndex = GridViewAuthorisedSignatoryList.PageCount - 1;
        fillGrid();
    }

    protected void fillGrid()
    {
        string query = "TF_GetAuthorisedSignatoryList";
        SqlParameter p1 = new SqlParameter("@branchname", SqlDbType.VarChar);
        p1.Value = ddlBranch.Text.Trim();
        SqlParameter p2 = new SqlParameter("@search", SqlDbType.VarChar);
        p2.Value = txtSearch.Text.Trim();
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewAuthorisedSignatoryList.PageSize = _pageSize;
            GridViewAuthorisedSignatoryList.DataSource = dt.DefaultView;
            GridViewAuthorisedSignatoryList.DataBind();
            GridViewAuthorisedSignatoryList.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewAuthorisedSignatoryList.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }

    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchName";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
        ddlBranch.SelectedIndex = 0;
        ddlBranch_SelectedIndexChanged(null, null);
        fillGrid();
    }

    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewAuthorisedSignatoryList.PageCount != GridViewAuthorisedSignatoryList.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewAuthorisedSignatoryList.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewAuthorisedSignatoryList.PageIndex + 1) + " of " + GridViewAuthorisedSignatoryList.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewAuthorisedSignatoryList.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewAuthorisedSignatoryList.PageIndex != (GridViewAuthorisedSignatoryList.PageCount - 1))
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

    protected void GridViewAuthorisedSignatoryList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";


        string _docNo = e.CommandArgument.ToString();

        SqlParameter p1 = new SqlParameter("@docNo", SqlDbType.VarChar);
        p1.Value = _docNo;

        string _query = "TF_DeleteInwardEntryDetails";
        TF_DATA objData = new TF_DATA();
        result = objData.SaveDeleteData(_query, p1);
        fillGrid();
        if (result == "deleted")
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);
        else
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record can not be deleted as it is associated with another record.');", true);
    }

    protected void GridViewAuthorisedSignatoryList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblbranch = new Label();
            Label lblname = new Label();
            Button btnDelete = new Button();
            lblbranch = (Label)e.Row.FindControl("lblBranch");
            lblname = (Label)e.Row.FindControl("lblName");

            btnDelete = (Button)e.Row.FindControl("btnDelete");

            btnDelete.Enabled = true;
            btnDelete.CssClass = "deleteButton";
            btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.location='EXP_AddEditAuthorisedSignatory.aspx?mode=edit&type=" + lblbranch.Text.Trim() +"&name="+ lblname.Text + "'";
                if (i != 4)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string branchname=ddlBranch.Text;
        Response.Redirect("EXP_AddEditAuthorisedSignatory.aspx?mode=add&type=" + branchname, true);
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
}