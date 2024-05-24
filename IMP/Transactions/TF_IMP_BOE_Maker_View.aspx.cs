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

public partial class IMP_Transactions_TF_IMP_BOE_Maker_View : System.Web.UI.Page
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
                getLastDocNo();
                ddlrecordperpage.SelectedValue = "20";
                if (rdb_C.Checked == true)
                {
                    rdb_C_CheckedChanged(null, null);
                }
                else if (rdb_LC.Checked == true)
                {
                    rdb_LC_CheckedChanged(null, null);
                }
                if (rdb_sight.Checked == true)
                {
                    rdb_sight_CheckedChanged(null, null);
                }
                else if (rdb_usance.Checked == true)
                {
                    rdb_usance_CheckedChanged(null, null);
                }
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
                btnAdd.Attributes.Add("onclick", "return validate();");
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
        SqlParameter p_Year = new SqlParameter("@Year", SqlDbType.VarChar);
        p_Year.Value = txtYear.Text.Trim();
        SqlParameter p_DocType = new SqlParameter("@DocType", SqlDbType.VarChar);
        p_DocType.Value = txtDocPrFx.Text.Trim();
        SqlParameter p_flcIlcType = new SqlParameter("@flcIlcType", SqlDbType.VarChar);
        string _flcIlcType = "";

        if (rdb_Foreign.Checked == true)
            _flcIlcType = "FLC";
        else
            _flcIlcType = "ILC";

        p_flcIlcType.Value = _flcIlcType;

        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData("TF_IMP_BOE_GetList", p_search, p_BranchName, p_Year, p_DocType, p_flcIlcType);
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
    private void getLastDocNo()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter pBranch_Code = new SqlParameter("@Branch_Code", SqlDbType.VarChar);
        pBranch_Code.Value = txtBranchCode.Text.Trim();
        SqlParameter pYear = new SqlParameter("@year", SqlDbType.VarChar);
        pYear.Value = txtYear.Text.Trim();
        SqlParameter pDocType = new SqlParameter("@docType", SqlDbType.VarChar);
        pDocType.Value = txtDocPrFx.Text.Trim();

        string _lastDocNo = obj.SaveDeleteData("TF_IPM_BOE_GetMaxDocNo", pYear, pDocType, pBranch_Code);
        txtDocumentNo.Text = _lastDocNo;
    }
    private void getDocType()
    {
        if (rdb_C.Checked == true)
        {
            if (rdb_usance.Checked == true)
            {
                txtDocPrFx.Text = "ICU";
            }
            if (rdb_sight.Checked == true)
            {
                txtDocPrFx.Text = "ICA";
            }
            //ddl_Doc_Scrutiny.SelectedValue = "1";
            //ddl_Doc_Scrutiny.Enabled = false;
            Panal_Scrutiny.Visible = false;
        }
        else if (rdb_LC.Checked == true)
        {
            if (rdb_usance.Checked == true)
            {
                txtDocPrFx.Text = "ACC";
            }
            if (rdb_sight.Checked == true)
            {
                txtDocPrFx.Text = "IBA";
            }
            Panal_Scrutiny.Visible = true;
            ddl_Doc_Scrutiny.SelectedValue = "0";
            ddl_Doc_Scrutiny.Enabled = true;
        }
        fillGrid();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        getLastDocNo();

        string docNo = txtDocPrFx.Text.Trim() + "-" + txtBranchCode.Text.Trim() + "-" + txtYear.Text.Trim() + txtDocumentNo.Text.Trim();
        string flcIlcType = "", Collection_Lodgment = "", Sight_Usance = "";
        if (rdb_Foreign.Checked == true)
        {
            flcIlcType = "FOREIGN";
        }
        else if (rdb_Local.Checked == true)
        {
            flcIlcType = "LOCAL";
        }
        if (rdb_C.Checked == true)
        {
            Collection_Lodgment = "Collection";
        }
        else if (rdb_LC.Checked == true)
        {
            Collection_Lodgment = "Lodgment_Under_LC";
        }
        if (rdb_sight.Checked == true)
        {
            Sight_Usance = "Sight";
        }
        else if (rdb_usance.Checked == true)
        {
            Sight_Usance = "Usance";
        }

        Response.Redirect("TF_IMP_BOE_Maker.aspx?mode=add&DocNo=" + docNo + "&DocType=" + txtDocPrFx.Text.Trim() +
            "&BranchCode=" + txtBranchCode.Text.Trim() + "&BranchName=" + ddlBranch.SelectedItem.Text +
            "&DocYear=" + txtYear.Text.Trim() +
            "&flcIlcType=" + flcIlcType + "&Collection_Lodgment=" + Collection_Lodgment + "&Sight_Usance=" + Sight_Usance +
            "&Doc_Scrutiny=" + ddl_Doc_Scrutiny.SelectedValue.ToString(), true);
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
    protected void rdb_Foreign_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void rdb_Local_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void rdb_C_CheckedChanged(object sender, EventArgs e)
    {
        getDocType();
        getLastDocNo();
        fillGrid();
    }
    protected void rdb_LC_CheckedChanged(object sender, EventArgs e)
    {
        getDocType();
        getLastDocNo();
        fillGrid();
    }
    protected void rdb_sight_CheckedChanged(object sender, EventArgs e)
    {
        getDocType();
        getLastDocNo();
        fillGrid();
    }
    protected void rdb_usance_CheckedChanged(object sender, EventArgs e)
    {
        getDocType();
        getLastDocNo();
        fillGrid();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
        getLastDocNo();
        fillGrid();
    }
    protected void GridViewImpEntryList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
        string _docNo = e.CommandArgument.ToString();
        SqlParameter p1 = new SqlParameter("@docNo", SqlDbType.VarChar);
        p1.Value = _docNo;
        string _query = "TF_IMP_BOE_Delete";
        TF_DATA objData = new TF_DATA();
        result = objData.SaveDeleteData(_query, p1);
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
    protected void GridViewImpEntryList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDocumentNo = new Label();
            Label Status = new Label();
            Label lblGBase_Status = new Label();
            Label lblSwift_Status = new Label();
            Label lblSFMS_Status = new Label();
            Button btnDelete = new Button();
            string flcIlcType = "", Collection_Lodgment = "", Sight_Usance = "";
            if (rdb_Foreign.Checked == true)
            {
                flcIlcType = "FOREIGN";
            }
            else if (rdb_Local.Checked == true)
            {
                flcIlcType = "LOCAL";
            }
            if (rdb_C.Checked == true)
            {
                Collection_Lodgment = "Collection";
            }
            else if (rdb_LC.Checked == true)
            {
                Collection_Lodgment = "Lodgment_Under_LC";
            }
            if (rdb_sight.Checked == true)
            {
                Sight_Usance = "Sight";
            }
            else if (rdb_usance.Checked == true)
            {
                Sight_Usance = "Usance";
            }

            lblGBase_Status = (Label)e.Row.FindControl("lblGBase_Status");
            lblSwift_Status = (Label)e.Row.FindControl("lblSwift_Status");
            lblSFMS_Status = (Label)e.Row.FindControl("lblSFMS_Status");
            Status = (Label)e.Row.FindControl("lblStatus");
            if (Status.Text == "Reject By Checker" || lblGBase_Status.Text == "Reject By Bot" || lblSwift_Status.Text == "Reject By Bot" || lblSFMS_Status.Text == "Reject By Bot")
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
                string pageurl = "window.location='TF_IMP_BOE_Maker.aspx?mode=Edit&DocNo=" + lblDocumentNo.Text + "&DocType=" + txtDocPrFx.Text.Trim() +
            "&BranchCode=" + txtBranchCode.Text.Trim() + "&BranchName=" + ddlBranch.SelectedItem.Text +
            "&DocYear=" + txtYear.Text.Trim() +
            "&flcIlcType=" + flcIlcType + "&Collection_Lodgment=" + Collection_Lodgment + "&Sight_Usance=" + Sight_Usance + "'";
                if (i != 11)
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
}