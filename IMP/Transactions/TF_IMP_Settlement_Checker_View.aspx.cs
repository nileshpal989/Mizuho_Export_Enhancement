using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;

public partial class IMP_Transactions_TF_IMP_Settlement_Checker_View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("../../TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["AR"] != null)
                {
                    string _DocNo = Request.QueryString["DocNo"].ToString();
                    if (Request.QueryString["AR"].Trim() == "A")
                    {
                        TF_DATA objserverName = new TF_DATA();
                        string _serverName = objserverName.GetServerName();
                        string path = "file://" + _serverName + "/Mizuho_UAT/TF_GeneratedFiles/IMPORT/";
                        string link = "/Mizuho_UAT/TF_GeneratedFiles/IMPORT/";
                        //lblLink.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
                        string script1 = "alert('Record Approved with Trans.Ref.No " + _DocNo + "')";
                        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", script1, true);
                    }
                    else if (Request.QueryString["AR"].Trim() == "R")
                    {
                        string script1 = "alert('Record Rejected with Trans.Ref.No " + _DocNo + "')";
                        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", script1, true);
                    }
                }

                fillBranch();
                txtYear.Text = (DateTime.Now.Month >= 4 ? DateTime.Now.ToString("yy") : DateTime.Now.AddYears(-1).ToString("yy"));
                ddlrecordperpage.SelectedValue = "20";
                txtLodgementDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
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
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);

    }
    protected void fillGrid()
    {
        string _status = "All";
        if (rdbApproved.Checked)
        {
            _status = "A";
        }
        else if (rdbRejected.Checked)
        {
            _status = "R";
        }
        else
        {
            _status = "All";
        }

        SqlParameter p_search = new SqlParameter("@search", txtSearch.Text.Trim());
        SqlParameter p_BranchName = new SqlParameter("@BranchName", ddlBranch.SelectedItem.Text.Trim());
        SqlParameter p_Year = new SqlParameter("@Year", SqlDbType.VarChar);
        p_Year.Value = txtYear.Text.Trim();
        SqlParameter p_LodgementDate = new SqlParameter("@LodgementDate", SqlDbType.VarChar);
        p_LodgementDate.Value = txtLodgementDate.Text.Trim();
        SqlParameter p_Status = new SqlParameter("@Status", SqlDbType.VarChar);
        p_Status.Value = _status;

        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData("TF_IMP_Settlement_GetList_Checker", p_search, p_BranchName, p_Year, p_LodgementDate, p_Status);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewImpEntryList.PageSize = _pageSize;
            GridViewImpEntryList.DataSource = dt.DefaultView;
            GridViewImpEntryList.DataBind();
            GridViewImpEntryList.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewImpEntryList.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void GridViewImpEntryList_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridViewImpEntryList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblBranchName = new Label();
            Label lblDocumentNo = new Label();
            Label Status = new Label();
            Label lblGBase_Status = new Label();
            Label lblSwift_Status = new Label();
            Label lblSFMS_Status = new Label();
            HiddenField lblDocType = new HiddenField();
            HiddenField lblForeignLocal = new HiddenField();
            HiddenField lblDocument_Scrutiny = new HiddenField();


            lblBranchName = (Label)e.Row.FindControl("lblBranchName");
            lblDocumentNo = (Label)e.Row.FindControl("lblDocumentNo");
            lblDocType = (HiddenField)e.Row.FindControl("lblDocType");
            lblForeignLocal = (HiddenField)e.Row.FindControl("lblForeignLocal");
            lblDocument_Scrutiny = (HiddenField)e.Row.FindControl("lblDocument_Scrutiny");
            Status = (Label)e.Row.FindControl("lblStatus");
            lblGBase_Status = (Label)e.Row.FindControl("lblGBase_Status");
            lblSwift_Status = (Label)e.Row.FindControl("lblSwift_Status");
            lblSFMS_Status = (Label)e.Row.FindControl("lblSFMS_Status");

            if (Status.Text == "Approved By Checker")
            {
                e.Row.BackColor = System.Drawing.Color.GreenYellow;
            }
            if (Status.Text == "Reject By Checker" || lblGBase_Status.Text == "Reject By Bot" || lblSwift_Status.Text == "Reject By Bot" || lblSFMS_Status.Text == "Reject By Bot")
            {
                e.Row.BackColor = System.Drawing.Color.Tomato;
            }

            string path = "TF_IMP_Settlement_Collection_Checker.aspx";
            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.location='" + path + "?mode=Edit&DocNo=" + lblDocumentNo.Text + "&DocType=" + lblDocType.Value +
            "&BranchName=" + lblBranchName.Text.Trim() + "&Doc_Scrutiny=" + lblDocument_Scrutiny.Value + "&FLC_ILC=" + lblForeignLocal.Value + "&Status=" + Status.Text.Trim() + "'";
                cell.Attributes.Add("onclick", pageurl);
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
            if (GridViewImpEntryList.PageCount != GridViewImpEntryList.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewImpEntryList.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewImpEntryList.PageIndex + 1) + " of " + GridViewImpEntryList.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewImpEntryList.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewImpEntryList.PageIndex != (GridViewImpEntryList.PageCount - 1))
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
        GridViewImpEntryList.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewImpEntryList.PageIndex > 0)
        {
            GridViewImpEntryList.PageIndex = GridViewImpEntryList.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewImpEntryList.PageIndex != GridViewImpEntryList.PageCount - 1)
        {
            GridViewImpEntryList.PageIndex = GridViewImpEntryList.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewImpEntryList.PageIndex = GridViewImpEntryList.PageCount - 1;
        fillGrid();
    }
    protected void rdbAll_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void rdbApproved_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void rdbRejected_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void txtLodgementDate_TextChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
}