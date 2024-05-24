using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EXP_EDPMS_INW_File_DataEntry_CheckerVIEW : System.Web.UI.Page
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
                txtDocDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                if (Request.QueryString["AR"] != null)
                {
                    
                    string _DocNo = Request.QueryString["DocNo"].ToString();
                    if (Request.QueryString["AR"].Trim() == "A")
                    {
                        TF_DATA objserverName = new TF_DATA();
                        string _serverName = objserverName.GetServerName();
                        string path = "file://" + _serverName + "/Mizuho_Export_Enhancement/TF_GeneratedFiles/Export/";
                        string link = "/Mizuho_Export_Enhancement/TF_GeneratedFiles/Export/";
                        lblLink.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
                        string script1 = "alert('Record Approved with Trans.Ref.No " + _DocNo + "and Files Created Successfully on " + path + "')";
                        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", script1, true);
                    }
                    else if (Request.QueryString["AR"].Trim() == "R")
                    {
                        string script1 = "alert('Record Rejected with Trans.Ref.No " + _DocNo + "')";
                        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", script1, true);
                    }
                }
                ddlrecordperpage.SelectedValue = "20";
                fillBranch();
                ddlbranch.SelectedValue = Session["userLBCode"].ToString();
                 fillGrid();
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
        ddlbranch.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlbranch.DataSource = dt.DefaultView;
            ddlbranch.DataTextField = "BranchName";
            ddlbranch.DataValueField = "BranchCode";
            ddlbranch.DataBind();
        }
        else
            li.Text = "No record(s) found";
        ddlbranch.Items.Insert(0, li);
    }
    protected void fillGrid()
    {
        string search = txtSearch.Text.Trim();
        SqlParameter p3 = new SqlParameter("@status", SqlDbType.VarChar);
        if (rdbAll.Checked == true)
        {
            p3.Value = "All";
        }
        else if (rdbApproved.Checked == true)
        {
            p3.Value = "A";
        }
        else if (rdbPending.Checked == true)
        {
            p3.Value = "C";
        }
        else
        {
            p3.Value = "R";
        }
        SqlParameter p1 = new SqlParameter("@branchCode", ddlbranch.SelectedValue);
        SqlParameter p2 = new SqlParameter("@date",txtDocDate.Text.Trim());
        SqlParameter p4 = new SqlParameter("@search", search);
        string _query = "TF_EDPMS_INW_File_GetDetails_checker";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1,p3, p4,p2);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewInwData.PageSize = _pageSize;
            GridViewInwData.DataSource = dt.DefaultView;
            GridViewInwData.DataBind();
            GridViewInwData.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            lblMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewInwData.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            lblMessage.Text = "No record(s) found.";
            lblMessage.Visible = true;
        }
    }

    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewInwData.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewInwData.PageIndex > 0)
        {
            GridViewInwData.PageIndex = GridViewInwData.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewInwData.PageIndex != GridViewInwData.PageCount - 1)
        {
            GridViewInwData.PageIndex = GridViewInwData.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewInwData.PageIndex = GridViewInwData.PageCount - 1;
        fillGrid();
    }
    protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }   
    protected void GridViewInwData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDocNo = new Label();
            Label lblDocDate = new Label();
            Label lblIRMStatus = new Label();
            Label Status = new Label();
            Label getstatus = new Label();
            Label pushstatus = new Label();
            lblIRMStatus = (Label)e.Row.FindControl("lblIRMStatus");
            pushstatus = (Label)e.Row.FindControl("lblAPIstatus");
            getstatus = (Label)e.Row.FindControl("lblDGFTstatus");
            Status = (Label)e.Row.FindControl("lblStatus");   
            
            if (Status.Text == "Approved By Checker")
            {
                e.Row.BackColor = System.Drawing.Color.GreenYellow;
            }
            if (Status.Text == "Reject By Checker")
            {
                e.Row.BackColor = System.Drawing.Color.Tomato;
            }
            if (Status.Text == "In Progress By Checker")
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
            }
            if (getstatus.Text == "Processed")
            {
                e.Row.BackColor = System.Drawing.Color.GreenYellow;
            }
            if ((getstatus.Text == "Failed") || (pushstatus.Text == "Failed"))
            {
                e.Row.BackColor = System.Drawing.Color.Tomato;
            }
            lblDocNo = (Label)e.Row.FindControl("lblDocNo");            
            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.location='EDPMS_INW_File_DataEntry_Checker.aspx?mode=edit&DocNo=" + lblDocNo.Text.Trim() + "&branchcode=" + ddlbranch.SelectedValue + "&Status=" + Status.Text + "&branchName=" + ddlbranch.SelectedItem + "&IRMStatus=" + lblIRMStatus.Text.Trim() + "'";
                if (i != 12)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void GridViewInwData_RowCommand(object sender, GridViewCommandEventArgs e)
    {        
        fillGrid();
    }
    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewInwData.PageCount != GridViewInwData.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewInwData.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewInwData.PageIndex + 1) + " of " + GridViewInwData.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewInwData.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewInwData.PageIndex != (GridViewInwData.PageCount - 1))
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
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }

    protected void txtDocDate_TextChanged(object sender, EventArgs e)
    {
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

    protected void rdbPending_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    
}