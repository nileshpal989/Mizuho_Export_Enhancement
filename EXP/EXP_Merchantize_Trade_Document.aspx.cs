using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EXP_EXP_Merchantize_Trade_Document : System.Web.UI.Page
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
                ddlBranch.SelectedValue = Session["userLBCode"].ToString();
                ddlBranch.Enabled = false;
                fillBranch();               
                ddlBranch.Focus();
                txtFromDate.Text = "01/" + System.DateTime.Now.ToString("MM/yyyy");
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                fillGrid();
            }
            txtFromDate.Attributes.Add("onblur", "return isValidDate(" + txtFromDate.ClientID + "," + "'From Date.'" + ");");
            txtToDate.Attributes.Add("onblur", "return isValidDate(" + txtToDate.ClientID + "," + "'To Date.'" + ");");          
        }
    }

    protected void fillGrid()
    {
        GridViewMerchTrade.Visible = false;
        rowGrid.Visible = false;
        rowPager.Visible = false;
        lblmessage.Text = "No record(s) found.";
        lblmessage.Visible = true;

        if (txtFromDate.Text != "")
        {

            TF_DATA objData = new TF_DATA();
            string _query = "Exp_MerchantingTrade_getList";
            SqlParameter p1 = new SqlParameter("@branchCode", ddlBranch.SelectedValue.Trim());
            SqlParameter p2 = new SqlParameter("@fromDate", txtFromDate.Text.Trim());
            SqlParameter p3 = new SqlParameter("@toDate", txtToDate.Text.Trim());
            SqlParameter p4 = new SqlParameter("@search", txtSearch.Text.Trim());
            DataTable dt = objData.getData(_query, p1, p2, p3, p4);
            if (dt.Rows.Count > 0)
            {
                int _records = dt.Rows.Count;
                int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
                GridViewMerchTrade.PageSize = _pageSize;
                GridViewMerchTrade.DataSource = dt.DefaultView;
                GridViewMerchTrade.DataBind();
                GridViewMerchTrade.Visible = true;
                rowGrid.Visible = true;
                rowPager.Visible = true;
                lblmessage.Visible = false;
                pagination(_records, _pageSize);
            }
            else
            {
                GridViewMerchTrade.Visible = false;
                rowGrid.Visible = false;
                rowPager.Visible = false;
                lblmessage.Text = "No record(s) found.";
                lblmessage.Visible = true;
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
            ddlBranch.DataValueField = "BranchCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";
        ddlBranch.Items.Insert(0, li);
    }
    protected void GridViewMerchTrade_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        Button lb = (Button)e.CommandSource;
        string textBoxValue = ((TextBox)lb.Parent.FindControl("txtImpRefNo")).Text;
        if (textBoxValue == "")
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Enter Import Ref No .');", true);
            
        else
        {
            string result = "";
            string LocID = ddlBranch.SelectedValue;
            string mode = "", ImpRefNo = "", Document_No = "";
            string str = e.CommandArgument.ToString();
            string[] values_p;
            if (str != "")
            {
                char[] splitchar = { ';' };
                values_p = str.Split(splitchar);
                mode = values_p[0].ToString();

                Document_No = values_p[1].ToString();
            }
            if (mode == "A")
                ImpRefNo = textBoxValue;
            else
                ImpRefNo = "";

            SqlParameter p1 = new SqlParameter("@branchCode", ddlBranch.SelectedValue.Trim());
            SqlParameter p2 = new SqlParameter("@mode", mode);
            SqlParameter p3 = new SqlParameter("@ImpRefNo", ImpRefNo);
            SqlParameter p4 = new SqlParameter("@DocNo", Document_No);
            string _query = "Exp_MerchantingTrade_Update";
            TF_DATA objData = new TF_DATA();
            result = objData.SaveDeleteData(_query, p1, p2, p3, p4);
            fillGrid();
            if (result == "updated")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record updated.');", true);
            }
            else
            {
                if (result == "deleted")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);
                }
                else
                    lblmessage.Text = result;
            }
        }
    }
    protected void GridViewMerchTrade_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtImRefNo = new TextBox();
            txtImRefNo = (TextBox)e.Row.FindControl("txtImpRefNo");

            Button btnAdd = new Button();
            btnAdd = (Button)e.Row.FindControl("btnAdd");

            Button btnDelete = new Button();
            btnDelete = (Button)e.Row.FindControl("btnDelete");

            if (txtImRefNo.Text != "")
            {
                e.Row.Attributes.Add("style", "background-color:#E7FCBE");
                btnAdd.Text = "Update";
                btnDelete.Visible = true;
            }
            btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");
        }
    }
    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewMerchTrade.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewMerchTrade.PageIndex > 0)
        {
            GridViewMerchTrade.PageIndex = GridViewMerchTrade.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewMerchTrade.PageIndex != GridViewMerchTrade.PageCount - 1)
        {
            GridViewMerchTrade.PageIndex = GridViewMerchTrade.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewMerchTrade.PageIndex = GridViewMerchTrade.PageCount - 1;
        fillGrid();
    }
    protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewMerchTrade.PageCount != GridViewMerchTrade.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewMerchTrade.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewMerchTrade.PageIndex + 1) + " of " + GridViewMerchTrade.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewMerchTrade.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewMerchTrade.PageIndex != (GridViewMerchTrade.PageCount - 1))
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
        fillGrid();
    }
}