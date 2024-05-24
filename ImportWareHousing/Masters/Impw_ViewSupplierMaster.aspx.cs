using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class ImportWareHousing_Masters_Supplier_Master : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("../../TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            txtSearch.Attributes.Add("onkeyup", "javascript:setTimeout('__doPostBack(\'txtSearch\',\'\')', 0)");
            if (!IsPostBack)
            {
                hdnUserRole.Value = Session["userRole"].ToString().Trim();
                fillBranch();

                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;

                ddlrecordperpage.SelectedValue = "20";
                fillGrid();
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
            }
            txtSearch.Focus();
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void fillGrid()
    {
        SqlParameter p1 = new SqlParameter("@Search", SqlDbType.VarChar);
        p1.Value = txtSearch.Text.Trim();

        SqlParameter p2 = new SqlParameter("@Adcode", SqlDbType.VarChar);
        p2.Value = ddlBranch.SelectedItem.Value;


        string _query = "TF_IMPW_GetSupplierMasterList";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2);

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
        TF_DATA objData = new TF_DATA();
        string result = "";

        string[] values;
        string CustAcNo = "", SupplierID = "";
        string str = e.CommandArgument.ToString();
        if (str != "")
        {
            char[] splitchar = { ';' };
            values = str.Split(splitchar);
            CustAcNo = values[0].ToString().Trim();
            SupplierID = values[1].ToString().Trim();
        }

        SqlParameter P1 = new SqlParameter("@Adcode", SqlDbType.VarChar);
        P1.Value = Session["userADCode"].ToString().Trim();

        SqlParameter P2 = new SqlParameter("@Cust_ACNo", SqlDbType.VarChar);
        P2.Value = CustAcNo.Trim();

        SqlParameter P3 = new SqlParameter("@Supplier_ID", SqlDbType.VarChar);
        P3.Value = SupplierID.Trim();

        string Query = "TF_IMPW_DeleteSupplierMaster";
        result = objData.SaveDeleteData(Query, P1, P2, P3);
        fillGrid();

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
            Label lblCustACNo = new Label();
            lblCustACNo = (Label)e.Row.FindControl("lblCustACNo");

            Label SupplierID = new Label();
            SupplierID = (Label)e.Row.FindControl("lblSupplierID");

            Button btnDelete = new Button();

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
                pageurl = "window.location='Impw_AddEditSupplierMaster.aspx?Mode=Edit&CustACNo=" + lblCustACNo.Text.Trim() + "&SupplierID=" + SupplierID.Text.Trim() + "&Branch=" + ddlBranch.SelectedItem.Text + "'";
                if (i != 7)
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
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
}

