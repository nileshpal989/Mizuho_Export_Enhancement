using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;

public partial class IMP_Transactions_TF_IMP_Shipping_Guarantee_Maker_View : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                txtDocPrFx.Text = "GRT";
                txtYear.Text = (DateTime.Now.Month >= 4 ? DateTime.Now.ToString("yy") : DateTime.Now.AddYears(-1).ToString("yy"));
                fillBranch();
                ddlBranch_SelectedIndexChanged(null, null);
                ddlrecordperpage.SelectedValue = "20";

                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].ToString() == "Added")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added.');", true);
                    }
                    if (Request.QueryString["result"].ToString() == "Submit")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record submit to Checker.');", true);
                    }
                }
            }
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = ddlBranch.SelectedItem.Text.Trim();
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            txtBranchCode.Text = dt.Rows[0]["BranchCode"].ToString().Trim();
            getLastDocNo();
            fillGrid();
        }
        else
        {
            txtBranchCode.Text = "";
            txtDocumentNo.Text = "";
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

    }
    protected void fillGrid()
    {
        SqlParameter p_search = new SqlParameter("@search", SqlDbType.VarChar);
        p_search.Value = txtSearch.Text.Trim();
        SqlParameter p_BranchName = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p_BranchName.Value = ddlBranch.SelectedItem.Text.Trim();

        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData("TF_IMP_Shipping_Guarantee_GetList_maker", p_search, p_BranchName);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewShipGuaranteeList.PageSize = _pageSize;
            GridViewShipGuaranteeList.DataSource = dt.DefaultView;
            GridViewShipGuaranteeList.DataBind();
            GridViewShipGuaranteeList.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewShipGuaranteeList.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
    private void getLastDocNo()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter pBranch_Code = new SqlParameter("@Branch_Code", SqlDbType.VarChar);
        pBranch_Code.Value = txtBranchCode.Text.Trim();
        SqlParameter pYear = new SqlParameter("@year", SqlDbType.VarChar);
        pYear.Value = txtYear.Text.Trim();

        string _lastDocNo = obj.SaveDeleteData("TF_IMP_Shipping_Guarantee_GetMaxDocNo", pYear,  pBranch_Code);
        txtDocumentNo.Text = _lastDocNo;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void GridViewShipGuaranteeList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
        string _docNo = e.CommandArgument.ToString();
        SqlParameter p1 = new SqlParameter("@docNo", SqlDbType.VarChar);
        p1.Value = _docNo;
        TF_DATA objData = new TF_DATA();
        result = objData.SaveDeleteData("TF_IMP_Shipping_Guarantee_Delete", p1);
        fillGrid();

        if (result == "deleted")
        {
            getLastDocNo();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);
        }
        else if (result == "Checking")
        {
            getLastDocNo();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Send to Checker can not delete the record.');", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('" + result + "');", true);
        }
    }
    protected void GridViewShipGuaranteeList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDocumentNo = new Label();
            Label Status = new Label();
            Label lblGBase_Status = new Label();
            Button btnDelete = new Button();

            lblGBase_Status = (Label)e.Row.FindControl("lblGBase_Status");
            Status = (Label)e.Row.FindControl("lblStatus");
            if (Status.Text == "Reject By Checker" || lblGBase_Status.Text == "Reject By Bot")
            {
                e.Row.BackColor = System.Drawing.Color.Tomato;
            }

            lblDocumentNo = (Label)e.Row.FindControl("lblDocumentNo");
            btnDelete = (Button)e.Row.FindControl("btnDelete");
            btnDelete.Enabled = true;
            btnDelete.CssClass = "deleteButton";
            btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");
            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.location='TF_IMP_Shipping_Guarantee_Maker.aspx?DocNo=" + lblDocumentNo.Text + "&BranchCode=" + txtBranchCode.Text.Trim() + "&BranchName=" + ddlBranch.SelectedItem.Text + "&DocYear=" + txtYear.Text.Trim() + "'";
                if (i != 9)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string DocNo = txtDocPrFx.Text.Trim() + "-" + txtBranchCode.Text.Trim() + "-" + txtYear.Text.Trim() + txtDocumentNo.Text.Trim();

        Response.Redirect("TF_IMP_Shipping_Guarantee_Maker.aspx?DocNo=" + DocNo + "&BranchCode=" + txtBranchCode.Text.Trim() + "&BranchName=" + ddlBranch.SelectedItem.Text +
            "&DocYear=" + txtYear.Text.Trim(), true);
    }
    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewShipGuaranteeList.PageCount != GridViewShipGuaranteeList.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewShipGuaranteeList.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewShipGuaranteeList.PageIndex + 1) + " of " + GridViewShipGuaranteeList.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewShipGuaranteeList.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewShipGuaranteeList.PageIndex != (GridViewShipGuaranteeList.PageCount - 1))
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
    protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewShipGuaranteeList.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewShipGuaranteeList.PageIndex > 0)
        {
            GridViewShipGuaranteeList.PageIndex = GridViewShipGuaranteeList.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewShipGuaranteeList.PageIndex != GridViewShipGuaranteeList.PageCount - 1)
        {
            GridViewShipGuaranteeList.PageIndex = GridViewShipGuaranteeList.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewShipGuaranteeList.PageIndex = GridViewShipGuaranteeList.PageCount - 1;
        fillGrid();
    }

}