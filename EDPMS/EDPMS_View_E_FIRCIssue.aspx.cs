using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EDPMS_EDPMS_View_E_FIRCIssue : System.Web.UI.Page
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
                hdnUserRole.Value = Session["userRole"].ToString().Trim();
                txtYear.Text = DateTime.Now.Year.ToString();
                ddlrecordperpage.SelectedValue = "20";



                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;
                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].Trim() == "Added")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Saved.');", true);
                    }
                    if (Request.QueryString["result"].Trim() == "Updated")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
                    }

                    ddlBranch.SelectedValue = Request.QueryString["Branch"];
                }

                fillGrid();
                btnAdd.Attributes.Add("onclick", "return validate();");
                btnSearch.Attributes.Add("onclick", "return validateSearch();");
                txtYear.Attributes.Add("onblur", "return checkYear();");
                txtYear.Attributes.Add("onkeydown", "return validate_Number(event);");

            }
        }
    }

    private void fillGrid()
    {
        string search = txtSearch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        SqlParameter p2 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p2.Value = ddlBranch.Text.Trim();

        SqlParameter p3 = new SqlParameter("@year", SqlDbType.VarChar);
        p3.Value = txtYear.Text.Trim();

        string _query = "Fillgrid_EFIRC";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2,p3);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewEDPMSbillEntry.PageSize = _pageSize;
            GridViewEDPMSbillEntry.DataSource = dt.DefaultView;
            GridViewEDPMSbillEntry.DataBind();
            GridViewEDPMSbillEntry.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewEDPMSbillEntry.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
    private void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        //ListItem li = new ListItem();
        //li.Value = "0";
        if (dt.Rows.Count > 0)
        {
          //  li.Text = "---Select---";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        //else
        //    li.Text = "No record(s) found";

        //ddlBranch.Items.Insert(0, li);
        //ddlBranch.SelectedIndex = 1;
        //ddlBranch_SelectedIndexChanged(null, null);
        //fillGrid();
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
            if (GridViewEDPMSbillEntry.PageCount != GridViewEDPMSbillEntry.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewEDPMSbillEntry.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewEDPMSbillEntry.PageIndex + 1) + " of " + GridViewEDPMSbillEntry.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewEDPMSbillEntry.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewEDPMSbillEntry.PageIndex != (GridViewEDPMSbillEntry.PageCount - 1))
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
        GridViewEDPMSbillEntry.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewEDPMSbillEntry.PageIndex > 0)
        {
            GridViewEDPMSbillEntry.PageIndex = GridViewEDPMSbillEntry.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewEDPMSbillEntry.PageIndex != GridViewEDPMSbillEntry.PageCount - 1)
        {
            GridViewEDPMSbillEntry.PageIndex = GridViewEDPMSbillEntry.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewEDPMSbillEntry.PageIndex = GridViewEDPMSbillEntry.PageCount - 1;
        fillGrid();
    }
    protected void GridViewEDPMSbillEntry_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDocumentNo = new Label();
            Label lblfircno = new Label();
            Button btnDelete = new Button();


            lblDocumentNo = (Label)e.Row.FindControl("lblDocumentNo");
            lblfircno = (Label)e.Row.FindControl("lblfircno");
            btnDelete = (Button)e.Row.FindControl("btnDelete");

            btnDelete.Enabled = true;
            btnDelete.CssClass = "deleteButton";
            if (hdnUserRole.Value == "OIC")
                btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");
            else
                btnDelete.Attributes.Add("onclick", "alert('Only OIC can delete all records.');return false;");


            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                var DocNo = lblDocumentNo.Text.Trim();

                string pageurl = "window.location='EDPMS_E_FIRC_Issuse.aspx?mode=Edit&DocNo=" + lblDocumentNo.Text.Trim() + "&FircNo=" + lblfircno.Text.Trim() + "&Branch=" + ddlBranch.SelectedItem.Value +  "'";

                if (i != 5)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("EDPMS_E_FIRC_Issuse.aspx?mode=add&Branch=" + ddlBranch.SelectedItem.Value + "&Year=" + txtYear.Text.Trim(), true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = ddlBranch.Text.Trim();
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);

        fillGrid();
        ddlBranch.Focus();
    }

    protected void GridViewEDPMSbillEntry_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _Firc_No = e.CommandArgument.ToString();

        SqlParameter p1 = new SqlParameter("@EFRIC_No", SqlDbType.VarChar);
        p1.Value = _Firc_No;


        string _query = "TF_EDPMS_EFIRC_Delete";
        TF_DATA objData = new TF_DATA();
        result = objData.SaveDeleteData(_query, p1);
        fillGrid();
        if (result == "Deleted")
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);
        //else
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record cannot be deleted as it is realised.');", true);

    }
}