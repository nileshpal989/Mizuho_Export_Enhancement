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

public partial class IMP_TF_ImpAuto_ViewDrawermaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("../TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {

            if (!IsPostBack)
            {
                hdnUserRole.Value = Session["userRole"].ToString().Trim();
                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                
                ddlrecordperpage.SelectedValue = "20";
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
            if (GridViewCustomerList.PageCount != GridViewCustomerList.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewCustomerList.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewCustomerList.PageIndex + 1) + " of " + GridViewCustomerList.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewCustomerList.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewCustomerList.PageIndex != (GridViewCustomerList.PageCount - 1))
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
        GridViewCustomerList.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewCustomerList.PageIndex > 0)
        {
            GridViewCustomerList.PageIndex = GridViewCustomerList.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewCustomerList.PageIndex != GridViewCustomerList.PageCount - 1)
        {
            GridViewCustomerList.PageIndex = GridViewCustomerList.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewCustomerList.PageIndex = GridViewCustomerList.PageCount - 1;
        fillGrid();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_IMPAuto_AddEditDrawermaster.aspx?mode=add&BranchName=" + ddlBranch.SelectedItem.Text, true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void fillGrid()
    {
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = txtSearch.Text.Trim();

        SqlParameter p2 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p2.Value = ddlBranch.SelectedItem.Text;

        string _query = "TF_GetdrawerMasterList";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1,p2);

        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewCustomerList.PageSize = _pageSize;
            GridViewCustomerList.DataSource = dt.DefaultView;
            GridViewCustomerList.DataBind();
            GridViewCustomerList.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewCustomerList.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
    protected void GridViewCustomerList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";

        string _str = e.CommandArgument.ToString();
        string[] values_p;
        if (_str != "")
        {
            char[] splitchar = { ';' };
            values_p = _str.Split(splitchar);
            hdnCustid.Value = values_p[0].ToString();
            hdnDrawerid.Value= values_p[1].ToString(); 
        }

        SqlParameter p1 = new SqlParameter("@custACNO", SqlDbType.VarChar);
        p1.Value = hdnCustid.Value;
        SqlParameter p2 = new SqlParameter("@drawerID", SqlDbType.VarChar);
        p2.Value = hdnDrawerid.Value;

        TF_DATA objData = new TF_DATA();

        string _query = "TF_DeleteDrawerMaster";
        result = objData.SaveDeleteData(_query, p1,p2);
        fillGrid();

        string _userName = Session["userName"].ToString();
        if (result == "deleted")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record can not be deleted as it is associated with another record.');", true);
        }
    }
    protected void GridViewCustomerList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblcustac = new Label();
            Label lblDrawer = new Label();
            Label lblLinked = new Label();
            Label lblBranchName = new Label();
            Button btnDelete = new Button();
            lblcustac = (Label)e.Row.FindControl("lblcustACNO");
            lblDrawer = (Label)e.Row.FindControl("lblcustomerID");
            lblLinked = (Label)e.Row.FindControl("lblLinked");
            lblBranchName = (Label)e.Row.FindControl("lblBranchName");
            btnDelete = (Button)e.Row.FindControl("btnDelete");
            btnDelete.Enabled = true;
            btnDelete.CssClass = "deleteButton";
            //if (hdnUserRole.Value == "OIC")
                btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");
            //else
            //    btnDelete.Attributes.Add("onclick", "alert('Only OIC can delete all records.');return false;");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.location='TF_IMPAuto_AddEditDrawermaster.aspx?mode=edit&BranchName=" + lblBranchName.Text.Trim() + "&CustAcNO=" + lblcustac.Text.Trim() + "&DrawerID=" + lblDrawer.Text.Trim() + "'";
                if (i != 9)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }

    protected void GridViewCustomerList_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
            fillGrid();
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
        li.Value = "All";
        if (dt.Rows.Count > 0)
        {
            li.Text = "All Branches";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
    }
}