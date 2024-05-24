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

public partial class IMP_Transactions_TF_IMP_BOOKING_OF_IBD_AND_ACC_Maker_View : System.Web.UI.Page
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
                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch_SelectedIndexChanged(null, null);
                txtYear.Text = (DateTime.Now.Month >= 4 ? DateTime.Now.ToString("yy") : DateTime.Now.AddYears(-1).ToString("yy"));
                ddlrecordperpage.SelectedValue = "20";
                fillGrid();
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
        fillGrid();
    }
    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
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

        string Document_Type = "";
        if (rbd_ACC.Checked == true)
        {
            Document_Type = "ACC";
        }

        else if (rdb_ICU.Checked == true)
        {
            Document_Type = "ICU";
        }

        SqlParameter p_search = new SqlParameter("@search", txtSearch.Text.Trim());
        SqlParameter p_BranchName = new SqlParameter("@BranchName", ddlBranch.SelectedItem.Text.Trim());
        SqlParameter p_Year = new SqlParameter("@Year", txtYear.Text.Trim());
        SqlParameter p_Document_Type = new SqlParameter("@Document_Type", Document_Type);

        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData("TF_IMP_BOOKING_OF_IBD_ACC_GetList_Maker", p_search, p_BranchName, p_Year, p_Document_Type);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewBooking_IBD_ACC_List.PageSize = _pageSize;
            GridViewBooking_IBD_ACC_List.DataSource = dt.DefaultView;
            GridViewBooking_IBD_ACC_List.DataBind();
            GridViewBooking_IBD_ACC_List.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewBooking_IBD_ACC_List.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }

    protected void GridViewBooking_IBD_ACC_List_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridViewBooking_IBD_ACC_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDocumentNo = new Label();
            Label Status = new Label();
            Label lblGBase_Status = new Label();
            Label lblSwift_Status = new Label();
            Label lblSFMS_Status = new Label();
            HiddenField lblDocType = new HiddenField();
            HiddenField lblDocument_Scrutiny = new HiddenField();

            lblDocumentNo = (Label)e.Row.FindControl("lblDocumentNo");
            Status = (Label)e.Row.FindControl("lblStatus");
            lblGBase_Status = (Label)e.Row.FindControl("lblGBase_Status");
            lblSwift_Status = (Label)e.Row.FindControl("lblSwift_Status");
            lblSFMS_Status = (Label)e.Row.FindControl("lblSFMS_Status");
            lblDocType = (HiddenField)e.Row.FindControl("lblDocType");
            lblDocument_Scrutiny = (HiddenField)e.Row.FindControl("lblDocument_Scrutiny");

            if (Status.Text == "Reject By Checker" || lblGBase_Status.Text == "Reject By Bot" || lblSwift_Status.Text == "Reject By Bot" || lblSFMS_Status.Text == "Reject By Bot")
            {
                e.Row.BackColor = System.Drawing.Color.Tomato;
            }

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string path = "";
                if (rbd_ACC.Checked == true)
                {
                    path = "TF_IMP_BOOKING_OF_IBD_AND_ACC_Maker.aspx";
                }
                else if (rdb_ICU.Checked == true)
                {
                    path = "TF_IMP_Collection_Acceptance_Maker.aspx";
                }
                else if (rdb_ICU.Checked == true)
                { }

                string pageurl = "window.location='" + path + "?DocNo=" + lblDocumentNo.Text + "&DocType=" + lblDocType.Value.Trim() + "&Document_Scrutiny=" + lblDocument_Scrutiny.Value.Trim() +
            "&BranchName=" + ddlBranch.SelectedItem.Text.Trim() + "'";
                if (i != 7)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewBooking_IBD_ACC_List.PageCount != GridViewBooking_IBD_ACC_List.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewBooking_IBD_ACC_List.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewBooking_IBD_ACC_List.PageIndex + 1) + " of " + GridViewBooking_IBD_ACC_List.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewBooking_IBD_ACC_List.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewBooking_IBD_ACC_List.PageIndex != (GridViewBooking_IBD_ACC_List.PageCount - 1))
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
        GridViewBooking_IBD_ACC_List.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewBooking_IBD_ACC_List.PageIndex > 0)
        {
            GridViewBooking_IBD_ACC_List.PageIndex = GridViewBooking_IBD_ACC_List.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewBooking_IBD_ACC_List.PageIndex != GridViewBooking_IBD_ACC_List.PageCount - 1)
        {
            GridViewBooking_IBD_ACC_List.PageIndex = GridViewBooking_IBD_ACC_List.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewBooking_IBD_ACC_List.PageIndex = GridViewBooking_IBD_ACC_List.PageCount - 1;
        fillGrid();
    }
    protected void rbd_ACC_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void rbd_IBD_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void rbd_ICU_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
    }

}