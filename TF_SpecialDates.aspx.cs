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

public partial class TF_SpecialDates : System.Web.UI.Page
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
                ddlrecordperpage.SelectedValue = "20";
                fillBranch();
                
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;
                fillGrid();
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
                } txtSearch.Attributes.Add("onkeypress", "return submitForm(event);");
                btnSearch.Attributes.Add("onclick", "return validateSearch();");
                btnAdd.Attributes.Add("onclick", "return validateSave();");
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
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
        //ddlBranch.SelectedIndex = 1;
        //ddlBranch_SelectedIndexChanged(null, null);
        //fillGrid();
    }

    protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
        ddlBranch.Focus();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    { fillGrid(); }
    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewSpecialDates.PageCount != GridViewSpecialDates.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewSpecialDates.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewSpecialDates.PageIndex + 1) + " of " + GridViewSpecialDates.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewSpecialDates.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewSpecialDates.PageIndex != (GridViewSpecialDates.PageCount - 1))
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
        GridViewSpecialDates.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewSpecialDates.PageIndex > 0)
        {
            GridViewSpecialDates.PageIndex = GridViewSpecialDates.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewSpecialDates.PageIndex != GridViewSpecialDates.PageCount - 1)
        {
            GridViewSpecialDates.PageIndex = GridViewSpecialDates.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewSpecialDates.PageIndex = GridViewSpecialDates.PageCount - 1;
        fillGrid();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        Response.Redirect("TF_AddEditSpecialDates.aspx?mode=add&ddlbranch="+ddlBranch.SelectedItem.Text.Trim() + "&adcode="+ddlBranch.SelectedItem.Value, true);
    }
    protected void fillGrid()
    {
        TF_DATA objData = new TF_DATA();
        string search = txtSearch.Text.Trim();
        string adcode = ddlBranch.SelectedItem.Value;
        string _query = "TF_GetSpecialDatesList";

        SqlParameter p1 = new SqlParameter("@adcode", SqlDbType.VarChar);
        p1.Value = adcode;
        SqlParameter p2 = new SqlParameter("@search", SqlDbType.VarChar);
        p2.Value = search;

        DataTable dt = objData.getData(_query,p1,p2);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewSpecialDates.PageSize = _pageSize;
            GridViewSpecialDates.DataSource = dt.DefaultView;
            GridViewSpecialDates.DataBind();
            GridViewSpecialDates.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewSpecialDates.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
    protected void GridViewSpecialDates_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
        string _SpecialDate = e.CommandArgument.ToString();
        string adcode = ddlBranch.SelectedItem.Value;
        TF_DATA objData = new TF_DATA();
        string _query = "TF_deleteSpecialDates";


        SqlParameter pSDate = new SqlParameter("@sDate", SqlDbType.VarChar);
        pSDate.Value = _SpecialDate;

        SqlParameter p1 = new SqlParameter("@adcode",SqlDbType.VarChar);
        p1.Value = adcode;

        result = objData.SaveDeleteData(_query, pSDate,p1);
        fillGrid();
        if (result == "deleted")
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);
        else
            labelMessage.Text = result;
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record can not be deleted as it is associated with another record.');", true);

    }
    protected void GridViewSpecialDates_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSpecialDates = new Label();
            Label lblToolTip = new Label();

            Button btnDelete = new Button();
            lblSpecialDates = (Label)e.Row.FindControl("lblSpecialDates");
            lblToolTip = (Label)e.Row.FindControl("lblToolTip");

            btnDelete = (Button)e.Row.FindControl("btnDelete");
            
                btnDelete.Enabled = true;
                btnDelete.CssClass = "deleteButton";
                btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");

                string strToolTip = lblToolTip.Text;
                strToolTip = strToolTip.Replace("'", "\\'"); 

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.location='TF_AddEditSpecialDates.aspx?mode=edit&sDate=" + lblSpecialDates.Text.Trim() + "&toolTip=" + strToolTip + "&ddlbranch=" + ddlBranch.SelectedItem.Text + "&adcode=" + ddlBranch.SelectedItem.Value + "'";
                if (i != 2)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
}
