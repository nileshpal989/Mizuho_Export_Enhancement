using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;

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

    //protected void pagination(int _recordsCount, int _pageSize)
    //{
    //    TF_PageControls pgcontrols = new TF_PageControls();
    //    if (_recordsCount > 0)
    //    {
    //        navigationVisibility(true);
    //        if (GridViewCustomerList.PageCount != GridViewCustomerList.PageIndex + 1)
    //        {
    //            lblrecordno.Text = "Record(s) : " + (GridViewCustomerList.PageIndex + 1) * _pageSize + " of " + _recordsCount;
    //        }
    //        else
    //        {
    //            lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
    //        }
    //        lblpageno.Text = "Page : " + (GridViewCustomerList.PageIndex + 1) + " of " + GridViewCustomerList.PageCount;
    //    }
    //    else
    //    {
    //        navigationVisibility(false);
    //    }

    //    if (GridViewCustomerList.PageIndex != 0)
    //    {
    //        pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
    //    }
    //    else
    //    {
    //        pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
    //    }
    //    if (GridViewCustomerList.PageIndex != (GridViewCustomerList.PageCount - 1))
    //    {
    //        pgcontrols.enablelastnav(btnnavnext, btnnavlast);
    //    }
    //    else
    //    {
    //        pgcontrols.disablelastnav(btnnavnext, btnnavlast);
    //    }
    //}
    //private void navigationVisibility(Boolean visibility)
    //{
    //    lblpageno.Visible = visibility;
    //    lblrecordno.Visible = visibility;
    //    btnnavfirst.Visible = visibility;
    //    btnnavlast.Visible = visibility;
    //    btnnavnext.Visible = visibility;
    //    btnnavpre.Visible = visibility;
    //}
    //protected void btnnavfirst_Click(object sender, EventArgs e)
    //{
    //    GridViewCustomerList.PageIndex = 0;
    //    fillGrid();
    //}
    //protected void btnnavpre_Click(object sender, EventArgs e)
    //{
    //    if (GridViewCustomerList.PageIndex > 0)
    //    {
    //        GridViewCustomerList.PageIndex = GridViewCustomerList.PageIndex - 1;
    //    }
    //    fillGrid();
    //}
    //protected void btnnavnext_Click(object sender, EventArgs e)
    //{
    //    if (GridViewCustomerList.PageIndex != GridViewCustomerList.PageCount - 1)
    //    {
    //        GridViewCustomerList.PageIndex = GridViewCustomerList.PageIndex + 1;
    //    }
    //    fillGrid();
    //}
    //protected void btnnavlast_Click(object sender, EventArgs e)
    //{
    //    GridViewCustomerList.PageIndex = GridViewCustomerList.PageCount - 1;
    //    fillGrid();
    //}



    //protected void GridViewCustomerList_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        Label lblCustACNo = new Label();
    //        lblCustACNo = (Label)e.Row.FindControl("lblCustACNo");

    //        Label SupplierID = new Label();
    //        SupplierID = (Label)e.Row.FindControl("lblSupplierID");

    //        Button btnDelete = new Button();

    //        btnDelete = (Button)e.Row.FindControl("btnDelete");
    //        btnDelete.Enabled = true;
    //        btnDelete.CssClass = "deleteButton";
    //        if (hdnUserRole.Value == "OIC")
    //            btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");
    //        else
    //            btnDelete.Attributes.Add("onclick", "alert('Only OIC can delete all records.');return false;");

    //        int i = 0;
    //        string pageurl = "";
    //        foreach (TableCell cell in e.Row.Cells)
    //        {
    //            pageurl = "window.location='Impw_AddEditSupplierMaster.aspx?Mode=Edit&CustACNo=" + lblCustACNo.Text.Trim() + "&SupplierID=" + SupplierID.Text.Trim() + "&Branch=" + ddlBranch.SelectedItem.Text + "'";
    //            if (i != 7)
    //                cell.Attributes.Add("onclick", pageurl);
    //            else
    //                cell.Style.Add("cursor", "default");
    //            i++;
    //        }
    //    }
    //}

    protected void fillGrid()
    {
        DataTable dummy = new DataTable();
        dummy.Columns.Add("CUST_NAME");
        dummy.Columns.Add("Cust_ACNo");
        dummy.Columns.Add("Supplier_ID");
        dummy.Columns.Add("Supplier_Name");
        dummy.Columns.Add("Supplier_Address");
        dummy.Columns.Add("SupplierCountryName");
        dummy.Columns.Add("Bank_Name");
        dummy.Rows.Add();
        GridViewCustomerList.DataSource = dummy;
        GridViewCustomerList.DataBind();
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


    [WebMethod]
    public static string GetCustomers(string searchTerm, int pageIndex, int pageSize)
    {
        string query = "TF_IMPW_GetSupplierMasterList_Paging";
        SqlCommand cmd = new SqlCommand(query);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Adcode", "6770001");
        cmd.Parameters.AddWithValue("@Search", searchTerm);
        cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
        cmd.Parameters.AddWithValue("@PageSize", pageSize);
        cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        return GetData(cmd, pageIndex, pageSize).GetXml();
    }

    private static DataSet GetData(SqlCommand cmd, int pageIndex, int pageSize)
    {
        string strConnString = ConfigurationManager.ConnectionStrings["INWConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strConnString))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                cmd.Connection = con;
                sda.SelectCommand = cmd;
                using (DataSet ds = new DataSet())
                {
                    sda.Fill(ds, "Customers");
                    DataTable dt = new DataTable("Pager");
                    dt.Columns.Add("PageIndex");
                    dt.Columns.Add("PageSize");
                    dt.Columns.Add("RecordCount");
                    dt.Rows.Add();
                    dt.Rows[0]["PageIndex"] = pageIndex;
                    dt.Rows[0]["PageSize"] = pageSize;
                    dt.Rows[0]["RecordCount"] = cmd.Parameters["@RecordCount"].Value;
                    ds.Tables.Add(dt);
                    return ds;
                }
            }
        }
    }

    [WebMethod]
    public static string DeleteCustomer(string CustomerACNo, string SupplierID, string ADCode)
    {
        TF_DATA objData = new TF_DATA();
        string result = "";

        SqlParameter P1 = new SqlParameter("@Adcode", SqlDbType.VarChar);
        P1.Value = ADCode.Trim();

        SqlParameter P2 = new SqlParameter("@Cust_ACNo", SqlDbType.VarChar);
        P2.Value = CustomerACNo.Trim();

        SqlParameter P3 = new SqlParameter("@Supplier_ID", SqlDbType.VarChar);
        P3.Value = SupplierID.Trim();

        string Query = "TF_IMPW_DeleteSupplierMaster";
        result = objData.SaveDeleteData(Query, P1, P2, P3);

        return result;

    }
}

