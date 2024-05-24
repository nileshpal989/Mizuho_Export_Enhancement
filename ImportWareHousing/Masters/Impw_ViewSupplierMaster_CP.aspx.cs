using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class ImportWareHousing_Masters_Supplier_Master : System.Web.UI.Page
{
    public static int countglobal = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("../../TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {

            if (!IsPostBack)
            {
                hdnUserRole.Value = Session["userRole"].ToString().Trim();
                fillBranch();

                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;

                //fillGrid();
                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].Trim() == "Added")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added.');", true);
                    }
                    else
                        if (Request.QueryString["result"].Trim() == "Updated")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
                        }
                }
                //txtSearch.Attributes.Add("onkeypress", "return submitForm(event);");
                //btnSearch.Attributes.Add("onclick", "return validateSearch();");

                GridViewCustomerList.PageIndex = 1;
                GetCustomersPageWise(1);
            }
        }
    }

    protected void pagination(int _recordsCount, int _pageSize, int pagecount)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (pagecount != GridViewCustomerList.PageIndex)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewCustomerList.PageIndex) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewCustomerList.PageIndex) + " of " + pagecount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewCustomerList.PageIndex != 1)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewCustomerList.PageIndex != (pagecount))
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //fillGrid();
    }
    //protected void fillGrid()
    //{
    //    SqlParameter p1 = new SqlParameter("@Search", SqlDbType.VarChar);
    //    p1.Value = txtSearch.Text.Trim();

    //    SqlParameter p2 = new SqlParameter("@Adcode", SqlDbType.VarChar);
    //    p2.Value = ddlBranch.SelectedItem.Value;


    //    string _query = "TF_IMPW_GetSupplierMasterList";
    //    TF_DATA objData = new TF_DATA();
    //    DataTable dt = objData.getData(_query, p1, p2);

    //    if (dt.Rows.Count > 0)
    //    {
    //        int _records = dt.Rows.Count;
    //        int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
    //        GridViewCustomerList.PageSize = _pageSize;
    //        GridViewCustomerList.DataSource = dt.DefaultView;
    //        GridViewCustomerList.DataBind();
    //        GridViewCustomerList.Visible = true;
    //        rowGrid.Visible = true;
    //        rowPager.Visible = true;
    //        labelMessage.Visible = false;
    //        pagination(_records, _pageSize);
    //    }
    //    else
    //    {
    //        GridViewCustomerList.Visible = false;
    //        rowGrid.Visible = false;
    //        rowPager.Visible = false;
    //        labelMessage.Text = "No record(s) found.";
    //        labelMessage.Visible = true;
    //    }
    //}
    protected void GridViewCustomerList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        string result = "";
        string SupplierID = e.CommandArgument.ToString();

        SqlParameter P1 = new SqlParameter("@Adcode", SqlDbType.VarChar);
        P1.Value = Session["userADCode"].ToString().Trim();

        SqlParameter P2 = new SqlParameter("@Supplier_ID", SqlDbType.VarChar);
        P2.Value = SupplierID.Trim();

        string Query = "TF_IMPW_DeleteSupplierMaster";
        result = objData.SaveDeleteData(Query, P1, P2);
        //fillGrid();

        string _userName = Session["userName"].ToString();
        if (result == "Deleted")
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
            Label SupplierID = new Label();

            Button btnDelete = new Button();
            SupplierID = (Label)e.Row.FindControl("lblSupplierID");

            btnDelete = (Button)e.Row.FindControl("btnDelete");
            btnDelete.Enabled = true;
            btnDelete.CssClass = "deleteButton";
            if (hdnUserRole.Value == "OIC")
                btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");
            else
                btnDelete.Attributes.Add("onclick", "alert('Only OIC can delete all records.');return false;");

            int i = 0;
            string pageurl = "";
            foreach (TableCell cell in e.Row.Cells)
            {
                pageurl = "window.location='Impw_AddEditSupplierMaster.aspx?Mode=Edit&SupplierID=" + SupplierID.Text.Trim() + "&Branch=" + ddlBranch.SelectedItem.Text + "'";
                if (i != 5)
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Impw_AddEditSupplierMaster.aspx?Mode=Add&Branch=" + ddlBranch.SelectedItem.Text.Trim(), true);
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        Response.Redirect("Impw_UploadSupplierMaster.aspx?Branch=" + ddlBranch.SelectedItem.Text.Trim(), true);
    }

    //Paging in Gridview

    private void GetCustomersPageWise(int pageIndex)
    {
        string constring = ConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constring))
        {
            using (SqlCommand cmd = new SqlCommand("TF_IMPW_GetSupplierMasterList_Paging", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Adcode", ddlBranch.SelectedItem.Value.ToString());
                cmd.Parameters.AddWithValue("@Search", txtSearch.Text.Trim());
                cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                cmd.Parameters.AddWithValue("@PageSize", int.Parse(ddlPageSize.SelectedValue));
                cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                con.Open();
                IDataReader idr = cmd.ExecuteReader();
                GridViewCustomerList.DataSource = idr;
                GridViewCustomerList.DataBind();
                idr.Close();
                con.Close();

                int recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                this.PopulatePager(recordCount, pageIndex);
                double dblPageCount = (double)((decimal)recordCount / decimal.Parse(ddlPageSize.SelectedValue));
                int pageCount = (int)Math.Ceiling(dblPageCount);
                pagination(recordCount, int.Parse(ddlPageSize.SelectedValue), pageCount);
                countglobal = recordCount;
            }
        }
    }
    private void PopulatePager(int recordCount, int currentPage)
    {
        double dblPageCount = (double)((decimal)recordCount / decimal.Parse(ddlPageSize.SelectedValue));
        int pageCount = (int)Math.Ceiling(dblPageCount);
        List<ListItem> pages = new List<ListItem>();
        if (pageCount > 0)
        {
            //pages.Add(new ListItem("First", "1", currentPage > 1));
            for (int i = 1; i <= pageCount; i++)
            {
                pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
            }
            //pages.Add(new ListItem("Last", pageCount.ToString(), currentPage < pageCount));
        }
        rptPager.DataSource = pages;
        rptPager.DataBind();
    }
    protected void PageSize_Changed(object sender, EventArgs e)
    {
        GridViewCustomerList.PageIndex = 1;
        this.GetCustomersPageWise(1);
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        GridViewCustomerList.PageIndex = pageIndex;

        this.GetCustomersPageWise(pageIndex);
    }


    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewCustomerList.PageIndex = 1;
        this.GetCustomersPageWise(1);
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewCustomerList.PageIndex > 1)
        {
            GridViewCustomerList.PageIndex = GridViewCustomerList.PageIndex - 1;
            int pageIndex = GridViewCustomerList.PageIndex;
            this.GetCustomersPageWise(pageIndex);
        }
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        double dblPageCount = (double)((decimal)countglobal / decimal.Parse(ddlPageSize.SelectedValue));
        int pageCount = (int)Math.Ceiling(dblPageCount);

        if (GridViewCustomerList.PageIndex != pageCount)
        {
            GridViewCustomerList.PageIndex = GridViewCustomerList.PageIndex + 1;
            int pageIndex = GridViewCustomerList.PageIndex;
            this.GetCustomersPageWise(pageIndex);
        }
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        double dblPageCount = (double)((decimal)countglobal / decimal.Parse(ddlPageSize.SelectedValue));
        int pageCount = (int)Math.Ceiling(dblPageCount);

        GridViewCustomerList.PageIndex = pageCount;
        this.GetCustomersPageWise(pageCount);
    }

}

