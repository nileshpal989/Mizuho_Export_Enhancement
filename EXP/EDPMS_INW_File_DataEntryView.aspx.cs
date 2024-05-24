using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EDPMS_EDPMS_INW_File_DataEntryView : System.Web.UI.Page
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
                ddlrecordperpage.SelectedValue = "20";
                fillBranch();
                ddlbranch.SelectedValue = Session["userLBCode"].ToString();
               
                fillGrid();
                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].Trim() == "added")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added.');", true);                        
                    }
                    else
                        if (Request.QueryString["result"].Trim() == "updated")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
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
        SqlParameter p1 = new SqlParameter("@branchCode", ddlbranch.SelectedValue);
        SqlParameter p2 = new SqlParameter("@docNo", "");
       // SqlParameter p3 = new SqlParameter("@mode", "");
        SqlParameter p3 = new SqlParameter("@search", search);
        string _query = "TF_EDPMS_INW_File_GetDetails_View";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query,p1,p2,p3);
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlbranch.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Select Branch.');", true);
            ddlbranch.Focus();
        }
        else
        {
            string branch = ddlbranch.SelectedValue.Trim();
            Response.Redirect("EDPMS_INW_File_DataEntry.aspx?mode=add&branchcode=" + branch + "&branchName=" + ddlbranch.SelectedItem, true);
        }
    }
    protected void GridViewInwData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDocNo = new Label();
            Label lblDocDate = new Label();
            Label lblIRMStatus = new Label();
            Label Status = new Label();
            Status = (Label)e.Row.FindControl("lblStatus");           
            if (Status.Text == "Reject By Checker")
            {
                e.Row.BackColor = System.Drawing.Color.Tomato;
            }
            Button btnDelete = new Button();
            lblDocNo = (Label)e.Row.FindControl("lblDocNo");
            lblIRMStatus = (Label)e.Row.FindControl("lblIRMStatus");
            btnDelete = (Button)e.Row.FindControl("btnDelete");
            btnDelete.Enabled = true;
            btnDelete.CssClass = "deleteButton";

            String userRole = Session["UserRole"].ToString();
            if (userRole == "OIC")
                btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");
            else
                btnDelete.Attributes.Add("onclick", "alert('Only OIC can delete all records.');return false;");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.location='EDPMS_INW_File_DataEntry.aspx?mode=edit&DocNo=" + lblDocNo.Text.Trim() + "&branchcode=" + ddlbranch.SelectedValue + "&branchName=" + ddlbranch.SelectedItem + "&IRMStatus=" + lblIRMStatus.Text.Trim() + "'";
                if (i != 13) 
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void GridViewInwData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "RemoveRecord") // Change "DeleteCommand" to your actual command name
        //{
        GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;

        if (row.RowType == DataControlRowType.DataRow)
        {
            Label lblDocDate = row.FindControl("lblDocDate") as Label;
            Label lblCustAcNo = row.FindControl("lblCustAcNo") as Label;
            Label lblIRMStatus = row.FindControl("lblIRMStatus") as Label;
            
            string DocumentDate = lblDocDate.Text;
            string CustomerAcNo = lblCustAcNo.Text;
            string IRMStatus = lblIRMStatus.Text;
            string result = "";
            String userRole = Session["UserRole"].ToString();
            string _docNo = e.CommandArgument.ToString();
            SqlParameter p1 = new SqlParameter("@branchCode", ddlbranch.SelectedValue.Trim());
            SqlParameter p2 = new SqlParameter("@docNo", _docNo);
            SqlParameter p3 = new SqlParameter("@IRMStatus", IRMStatus);

            string _query = "TF_EDPMS_INW_File_Delete";
            TF_DATA objData = new TF_DATA();
            result = objData.SaveDeleteData(_query, p1, p2,p3);
            fillGrid();
            // Your existing code for deleting the record...

            //-----------------Anand 23/01/2024------------------------
            string _query1 = "";
            string _NewValues = _docNo;
            string _OldValues = "";
            string _userName = Session["userName"].ToString();
            string _CustACNo = CustomerAcNo;
            string _DocumentNo = _docNo;
            string _DocumnetDate = DocumentDate;

            AuditChecker(_query1, _NewValues, _OldValues, _userName, _CustACNo, _DocumentNo, _DocumnetDate);
            //--------------------------End---------------------------------------------

            if (result == "deleted")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);
                fillGrid();
            }
        }
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
    public void AuditChecker(string _query1, string _NewValues, string _OldValues, string _userName, string _CustACNo, string _DocumentNo, string _DocumnetDate)
    {


        TF_DATA objSave = new TF_DATA();
        _query1 = "TF_Data_AuditTrailInward";
        _OldValues = "";
        _NewValues = "Document No :" + _NewValues;


        SqlParameter Branch = new SqlParameter("@BranchCode", SqlDbType.VarChar);
        Branch.Value = Session["userLBCode"].ToString();
        SqlParameter Mod = new SqlParameter("@Module", SqlDbType.VarChar);
        Mod.Value = "EXP";
        SqlParameter oldvalues = new SqlParameter("@OldValues", SqlDbType.VarChar);
        oldvalues.Value = _OldValues;
        SqlParameter newvalues = new SqlParameter("@NewValues", SqlDbType.VarChar);
        newvalues.Value = _NewValues;
        SqlParameter Acno = new SqlParameter("@CustAcNo", SqlDbType.VarChar);
        Acno.Value = _CustACNo;

        SqlParameter DocumentNo = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
        DocumentNo.Value = _DocumentNo;
        SqlParameter DocumnetDate = new SqlParameter("@DocumentDate", SqlDbType.VarChar);
        DocumnetDate.Value = _DocumnetDate;
        SqlParameter Mode = new SqlParameter("@Mode", SqlDbType.VarChar);
        Mode.Value = "Delete";
        SqlParameter user = new SqlParameter("@ModifiedBy", SqlDbType.VarChar);
        user.Value = _userName;
        string _moddate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        SqlParameter moddate = new SqlParameter("@ModifiedDate", SqlDbType.VarChar);
        moddate.Value = _moddate;
        string _menu = "Inward Remittance Data Entry";
        SqlParameter menu = new SqlParameter("@MenuName", SqlDbType.VarChar);
        menu.Value = _menu;
        string at = objSave.SaveDeleteData(_query1, Branch, Mod, oldvalues, newvalues, Acno, DocumentNo, DocumnetDate, Mode, user, moddate, menu);
    }
}