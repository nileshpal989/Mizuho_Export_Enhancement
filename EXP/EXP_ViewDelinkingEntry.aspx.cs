using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EXP_EXP_ViewDelinkingEntry : System.Web.UI.Page
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
                txtYear.Text = DateTime.Now.Year.ToString();
                ddlrecordpage.SelectedValue = "20";
                fillBranch();
                fillGrid();
                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].Trim() == "added")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Record Added.');", true);
                    }

                    else if (Request.QueryString["result"].Trim() == "updated")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Loan Advanced removed');", true);
                    }
                }
                txtSearch.Attributes.Add("onkeypress", "return submitForm(event);");
                btnSearch.Attributes.Add("onclick", "return validateSearch();");
                txtYear.Attributes.Add("onblur", "return checkYear();");
                txtYear.Attributes.Add("onkeydown", "return validate_Number(event);");
            }
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
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewDelinking.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewDelinking.PageIndex > 0)
        {
            GridViewDelinking.PageIndex = GridViewDelinking.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewDelinking.PageIndex != GridViewDelinking.PageCount - 1)
        {
            GridViewDelinking.PageIndex = GridViewDelinking.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewDelinking.PageIndex = GridViewDelinking.PageCount - 1;
        fillGrid();
    }
    protected void ddlrecordpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void fillGrid()
    {
        string search = txtSearch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;
        SqlParameter p2 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p2.Value = ddlBranch.Text.Trim();
        SqlParameter p3 = new SqlParameter("@Year", SqlDbType.VarChar);
        p3.Value = txtYear.Text.Trim();
        SqlParameter p4 = new SqlParameter("@DocType", SqlDbType.VarChar);
        string _strDocType = "";
        if (rdbNego.Checked == true)
        {
            _strDocType = "N";
        }
        else if (rdbPur.Checked == true)
        {
            _strDocType = "P";
        }
        else if (rdbDis.Checked == true)
        {
            _strDocType = "D";
        }
        else if (rdbEBR.Checked == true)
        {
            _strDocType = "E";
        }
        else if (rdbColl.Checked == true)
        {
            _strDocType = "C";
        }
        p4.Value = _strDocType.ToString();

        string _query = "TF_GetExportDelinkingEntry";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2, p3, p4);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordpage.SelectedValue.Trim());
            GridViewDelinking.PageSize = _pageSize;
            GridViewDelinking.DataSource = dt.DefaultView;
            GridViewDelinking.DataBind();
            GridViewDelinking.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewDelinking.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewDelinking.PageCount != GridViewDelinking.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewDelinking.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewDelinking.PageIndex + 1) + " of " + GridViewDelinking.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewDelinking.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewDelinking.PageIndex != (GridViewDelinking.PageCount - 1))
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
    protected void rdbNego_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void rdbDis_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void rdbEBR_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void GridViewDelinking_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDocumentNo = new Label();

            lblDocumentNo = (Label)e.Row.FindControl("blDocNo");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.location='EXP_AddEditExportDelinkingEntry.aspx?DocNo=" + lblDocumentNo.Text.Trim() + "'";
                if (i != 8)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void rdbPur_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void rdbColl_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
}