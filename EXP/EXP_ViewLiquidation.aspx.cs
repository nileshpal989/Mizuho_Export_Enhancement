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

public partial class EXP_EXP_ViewLiquidation : System.Web.UI.Page
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

                ddlrecordperpage.SelectedValue = "20";
                fillGrid();
                fillBranch();
               // txtYear.Text = System.DateTime.Now.Year.ToString();
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
                txtSearch.Attributes.Add("onkeypress", "return submitForm(event);");
                btnSearch.Attributes.Add("onclick", "return validateSearch();");

                btnAdd.Attributes.Add("onclick", "return validateAdd();");
                btnDocNoList.Attributes.Add("onclick", "return OpenDocNoList('mouseClick');");
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
            if (GridViewLiquidationList.PageCount != GridViewLiquidationList.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewLiquidationList.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewLiquidationList.PageIndex + 1) + " of " + GridViewLiquidationList.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewLiquidationList.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewLiquidationList.PageIndex != (GridViewLiquidationList.PageCount - 1))
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
        GridViewLiquidationList.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewLiquidationList.PageIndex > 0)
        {
            GridViewLiquidationList.PageIndex = GridViewLiquidationList.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewLiquidationList.PageIndex != GridViewLiquidationList.PageCount - 1)
        {
            GridViewLiquidationList.PageIndex = GridViewLiquidationList.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewLiquidationList.PageIndex = GridViewLiquidationList.PageCount - 1;
        fillGrid();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //string docNo = txtDocPrFx.Text.Trim() + "/" + txtDocumentNo.Text.Trim() + "/" + txtBranchCode.Text.Trim() + "/" + txtYear.Text.Trim();
        Response.Redirect("EXP_AddEditLiquidation.aspx?mode=add&docNo=" + txtDocumentNo.Text.Trim() + "&Branch=" + ddlBranch.Text.Trim(), true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    { fillGrid(); }
    protected void fillGrid()
    {

        string search = txtSearch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;
        SqlParameter p2 = new SqlParameter("@branchName", SqlDbType.VarChar);
        p2.Value = ddlBranch.Text.Trim();
        SqlParameter p3 = new SqlParameter("@docNo", SqlDbType.VarChar);
        p3.Value = txtDocumentNo.Text.Trim();


        string _query = "TF_GetExportLiquidatedList";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2, p3);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewLiquidationList.PageSize = _pageSize;
            GridViewLiquidationList.DataSource = dt.DefaultView;
            GridViewLiquidationList.DataBind();
            GridViewLiquidationList.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewLiquidationList.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
    protected void GridViewLiquidationList_RowCommand(object sender, GridViewCommandEventArgs e)
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
    protected void GridViewLiquidationList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDocumentNo = new Label();
            Label lblbranchName = new Label();
            Label lblSrNo = new Label();
            Button btnDelete = new Button();
            //    string docNo = txtDocPrFx.Text.Trim() + "/" + txtDocumentNo.Text.Trim() + "/" + txtBranchCode.Text.Trim() + "/" + txtYear.Text.Trim();

            lblDocumentNo = (Label)e.Row.FindControl("lblDocumentNo");
            //   lblbranchName = (Label)e.Row.FindControl("lblbranchName");
            lblSrNo = (Label)e.Row.FindControl("lblSrNo");
            btnDelete = (Button)e.Row.FindControl("btnDelete");
            //if (lblLinked.Text.Trim() == "0")
            //{
            //btnDelete.Enabled = true;
            //btnDelete.CssClass = "deleteButton";
            //btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");
            //}
            //else
            //{
            //    btnDelete.Enabled = false;
            //    btnDelete.CssClass = "deleteButtonDisabled";
            //}
            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.location='EXP_AddEditLiquidation.aspx?mode=edit&Srno=" + lblSrNo.Text.Trim() + "&docNo=" + txtDocumentNo.Text.Trim() + "&Branch=" + ddlBranch.Text.Trim() + "'";
                if (i != 6)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }

    protected void txtDocumentNo_TextChanged(object sender, EventArgs e)
    {
        fillGrid();
        txtDocumentNo.Focus();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = ddlBranch.Text.Trim();
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);

        if (dt.Rows.Count > 0)
        {
            hdnBranchCode.Value = dt.Rows[0]["BranchCode"].ToString().Trim();

        }
        else
        {
            hdnBranchCode.Value = "";
            txtDocumentNo.Text = "";
        }
        fillGrid();
        ddlBranch.Focus();

    }
   
}
