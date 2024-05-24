using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class IMP_Transactions_TF_IMP_ReversalofGO_Maker_View : System.Web.UI.Page
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
                fillBranch();
                fillGrid();
                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].ToString() == "Submit")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record submit to Checker.');", true);
                    }
                }
                ddlrecordpage.SelectedValue = "20";
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewBRO.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewBRO.PageIndex > 0)
        {
            GridViewBRO.PageIndex = GridViewBRO.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewBRO.PageIndex != GridViewBRO.PageCount - 1)
        {
            GridViewBRO.PageIndex = GridViewBRO.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewBRO.PageIndex = GridViewBRO.PageCount - 1;
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

        string query = "TF_IMP_GetBROWITHGenOprtn";
        TF_DATA objdata = new TF_DATA();
        DataTable dt = objdata.getData(query, p1, p_BranchName);

        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordpage.SelectedValue.Trim());
            GridViewBRO.PageSize = _pageSize;
            GridViewBRO.DataSource = dt.DefaultView;
            GridViewBRO.DataBind();
            GridViewBRO.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewBRO.Visible = false;
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
            if (GridViewBRO.PageCount != GridViewBRO.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewBRO.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewBRO.PageIndex + 1) + " of " + GridViewBRO.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewBRO.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewBRO.PageIndex != (GridViewBRO.PageCount - 1))
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
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void GridViewBRO_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
        string _Deliveryorder = e.CommandArgument.ToString();
        SqlParameter p1 = new SqlParameter("@Deliveryorder", SqlDbType.VarChar);
        p1.Value = _Deliveryorder;
        string query = "TF_IMP_DeleteBRO";

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
    protected void GridViewBRO_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblBrono = new Label();
            lblBrono = (Label)e.Row.FindControl("lblBrono");
            Label lblApplicantName = new Label();
            lblApplicantName = (Label)e.Row.FindControl("lblApplicantName");
            Label lblBillamt = new Label();
            lblBillamt = (Label)e.Row.FindControl("lblBillamt");
            Label lblBroDate = new Label();
            lblBroDate = (Label)e.Row.FindControl("lblBroDate");
            Label lblCurrency = new Label();
            lblCurrency = (Label)e.Row.FindControl("lblCurrency");
            Label Status = new Label();
            Status = (Label)e.Row.FindControl("lblStatus");
            if (Status.Text == "Reject By Checker")
            {
                e.Row.BackColor = System.Drawing.Color.Tomato;
            }

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string url = "window.location='TF_IMP_ReversalofGO_Maker.aspx?mode=edit&Delivery_Order_No=" + lblBrono.Text.Trim() + "&Applicant_Name=" + lblApplicantName.Text.Trim() +
            "&Bill_Amt=" + lblBillamt.Text.Trim() + "&BRO_Date=" + lblBroDate.Text.Trim() + "&Currency=" + lblCurrency.Text.Trim() + "'";
                if (i != 8)
                    cell.Attributes.Add("onclick", url);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
}