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

public partial class IMP_HelpForms_TF_IMP_Acceptance_ACCHelp : System.Web.UI.Page
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
        string search = txtSearch.Text.Trim();
        string branch = Request.QueryString["BranchCode"].Trim();
        string doctype = Request.QueryString["DocType"].Trim();
        SqlParameter p_search = new SqlParameter("@search", search);
        SqlParameter p_branch = new SqlParameter("@search", branch);
        SqlParameter p_doctype = new SqlParameter("@search", doctype);
        txtSearch.Text = search;
        string _query = "TF_IMP_Approved_Logd_List";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p_search, p_branch, p_doctype);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridView_Approved_Logd_List.PageSize = _pageSize;
            GridView_Approved_Logd_List.DataSource = dt.DefaultView;
            GridView_Approved_Logd_List.DataBind();
            GridView_Approved_Logd_List.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridView_Approved_Logd_List.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }

    }
    protected void GridView_Approved_Logd_List_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridView_Approved_Logd_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblBankCode = new Label();
            lblBankCode = (Label)e.Row.FindControl("lblBankCode");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "";
                if (Request.QueryString["hNo"].ToString() == "1")
                {
                    pageurl = "window.opener.selectNego_Remit_Bank('" + lblBankCode.Text + "');window.opener.EndRequest();window.close();";
                }
                //if (Request.QueryString["hNo"].ToString() == "2")
                //{
                //    pageurl = "window.opener.selectIntermediaryBank('" + lblBankCode.Text + "');window.opener.EndRequest();window.close();";
                //}
                //if (Request.QueryString["hNo"].ToString() == "3")
                //{
                //    pageurl = "window.opener.selectAcWithInsti('" + lblBankCode.Text + "');window.opener.EndRequest();window.close();";
                //}
                //if (Request.QueryString["hNo"].ToString() == "4")
                //{
                //    pageurl = "window.opener.selectDocsRcvdBank('" + lblBankCode.Text + "');window.opener.EndRequest();window.close();";
                //}

                if (i != 10)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void GridView_Approved_Logd_List_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridView_Approved_Logd_List.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridView_Approved_Logd_List.PageIndex > 0)
        {
            GridView_Approved_Logd_List.PageIndex = GridView_Approved_Logd_List.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridView_Approved_Logd_List.PageIndex != GridView_Approved_Logd_List.PageCount - 1)
        {
            GridView_Approved_Logd_List.PageIndex = GridView_Approved_Logd_List.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridView_Approved_Logd_List.PageIndex = GridView_Approved_Logd_List.PageCount - 1;
        fillGrid();
    }
    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridView_Approved_Logd_List.PageCount != GridView_Approved_Logd_List.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridView_Approved_Logd_List.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridView_Approved_Logd_List.PageIndex + 1) + " of " + GridView_Approved_Logd_List.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridView_Approved_Logd_List.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridView_Approved_Logd_List.PageIndex != (GridView_Approved_Logd_List.PageCount - 1))
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