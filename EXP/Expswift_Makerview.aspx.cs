using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EXP_Expswift_Makerview : System.Web.UI.Page
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
                ddlrecordpage.SelectedValue = "20";
                txtLodgementDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                fillBranch();
                fillGrid();
                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].Trim() == "added")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Record drafted successfully.');", true);
                    }
                    if (Request.QueryString["result"].ToString() == "Submit")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record submit to Checker.');", true);
                    }
                }

                txtSearch.Attributes.Add("onkeypress", "return submitForm(event);");
                btnSearch.Attributes.Add("onclick", "return validateSearch();");

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
        li.Value = "All Branches";
        if (dt.Rows.Count > 0)
        {
            li.Text = "All Branches";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("SwiftMessage.aspx?mode=add", true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        ExportViewMaker.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (ExportViewMaker.PageIndex > 0)
        {
            ExportViewMaker.PageIndex = ExportViewMaker.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (ExportViewMaker.PageIndex != ExportViewMaker.PageCount - 1)
        {
            ExportViewMaker.PageIndex = ExportViewMaker.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        ExportViewMaker.PageIndex = ExportViewMaker.PageCount - 1;
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
        SqlParameter p_BranchName = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p_BranchName.Value = ddlBranch.SelectedItem.Text;
        SqlParameter p_LodgementDate = new SqlParameter("@Date", SqlDbType.VarChar);
        p_LodgementDate.Value = txtLodgementDate.Text.Trim();

        string query = "TF_EXP_GetSwiftlist";
        TF_DATA objdata = new TF_DATA();
        DataTable dt = objdata.getData(query, p1, p_BranchName, p_LodgementDate);

        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordpage.SelectedValue.Trim());
            ExportViewMaker.PageSize = _pageSize;
            ExportViewMaker.DataSource = dt.DefaultView;
            ExportViewMaker.DataBind();
            ExportViewMaker.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            ExportViewMaker.Visible = false;
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
            if (ExportViewMaker.PageCount != ExportViewMaker.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (ExportViewMaker.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (ExportViewMaker.PageIndex + 1) + " of " + ExportViewMaker.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (ExportViewMaker.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (ExportViewMaker.PageIndex != (ExportViewMaker.PageCount - 1))
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
    protected void ExportViewMaker_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
        string _TransRefno = e.CommandArgument.ToString();
        SqlParameter p1 = new SqlParameter("@Trans_RefNo", SqlDbType.VarChar);
        p1.Value = _TransRefno;
        string query = "TF_EXP_DeleteSwift";

        TF_DATA objdata = new TF_DATA();
        result = objdata.SaveDeleteData(query, p1);
        fillGrid();
        if (result == "deleted")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "deletemessage", "alert('Record Deleted.')'", true);
        }
        else if (result == "Checking")
        {
            // getLastDocNo();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Send to Checker can not delete the record.');", true);
        }
    }
    protected void ExportViewMaker_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblTransrefno = new Label();
            Button btnDelete = new Button();
            Label Status = new Label();
            Label lblSwifttype = new Label();
            Label Swift_Status = new Label();
            lblSwifttype = (Label)e.Row.FindControl("lblSwift_type");
            Status = (Label)e.Row.FindControl("lblStatus");
            Swift_Status = (Label)e.Row.FindControl("lblSwift_Status");

            if (Status.Text == "Reject By Checker")
            {
                e.Row.BackColor = System.Drawing.Color.Tomato;
            }
            if (Swift_Status.Text == "Reject By Bot")
            {
                e.Row.BackColor = System.Drawing.Color.Tomato;
            }
            lblTransrefno = (Label)e.Row.FindControl("lblTransrefno");
            btnDelete = (Button)e.Row.FindControl("btnDelete");

            btnDelete.Enabled = true;
            btnDelete.CssClass = "deleteButton";
            btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string url = "window.location='SwiftMessage.aspx?mode=edit&Trans_RefNo=" + lblTransrefno.Text.Trim() + "&Swift_Type=" + lblSwifttype.Text.Trim() + "'";
                if (i != 3)
                    cell.Attributes.Add("onclick", url);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
}