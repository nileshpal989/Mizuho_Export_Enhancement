using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class IMP_TF_ImpAuto_View_NonLC : System.Web.UI.Page
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
                //   txtYear.Text = DateTime.Now.Year.ToString();
                TF_DATA objData = new TF_DATA();
                SqlParameter p1 = new SqlParameter("@ModType", SqlDbType.VarChar);
                p1.Value = "IMP";
                string _query = "TF_GetLastDocYear";
                DataTable dt = objData.getData(_query, p1);
                if (dt.Rows.Count > 0)
                    txtYear.Text = dt.Rows[0]["LastYear"].ToString();
                ddlrecordperpage.SelectedValue = "20";


                fillBranch();
                fillGrid();
                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].ToString().Substring(0, 5) == "added")
                    {
                        string _docNo = Request.QueryString["result"].ToString().Substring(5);
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added with Document No. : " + _docNo + "');", true);
                    }
                    else
                        if (Request.QueryString["result"].Trim() == "updated")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
                        }
                }
                //txtSearch.Attributes.Add("onkeypress", "return submitForm(event);");
                btnAdd.Attributes.Add("onclick", "return validate();");
                btnSearch.Attributes.Add("onclick", "return validateSearch();");
                txtYear.Attributes.Add("onblur", "return checkYear();");
                txtDocumentNo.Attributes.Add("onblur", "return checkDocNo();");
                txtDocumentNo.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtYear.Attributes.Add("onkeydown", "return validate_Number(event);");
            }
        }
        if (rdb_Foreign.Checked == true)
        {
            //rdb_Foreign_CheckedChanged(null, null);
        }
        if (rdb_Local.Checked == true)
        {
            //rdb_Local_CheckedChanged(null, null);
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
            ddlBranch.DataValueField = "BranchName";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
        //ddlBranch.SelectedIndex = 1;
        //ddlBranch_SelectedIndexChanged(null, null);
        //fillGrid();
    }
    protected void fillGrid()
    {
        getLastDocNo();
        SqlParameter p_search = new SqlParameter("@search", SqlDbType.VarChar);
        p_search.Value = txtSearch.Text.Trim();
        SqlParameter p_BranchName = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p_BranchName.Value = ddlBranch.Text.Trim();
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
        DataTable dt = objData.getData("TF_ImpAuto_GetImportEntryList", p_search, p_BranchName, p_Year, p_DocType, p_flcIlcType);
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

    protected void GridViewImpEntryList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
        string _docNo = e.CommandArgument.ToString();
        SqlParameter p1 = new SqlParameter("@docNo", SqlDbType.VarChar);
        p1.Value = _docNo;
        string _query = "TF_IMP_DeleteImportBillEntry";
        TF_DATA objData = new TF_DATA();
        result = objData.SaveDeleteData(_query, p1);
        fillGrid();
        if (result == "deleted")
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);
        else
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record can not be deleted as it is associated with another record.');", true);
    }
    protected void GridViewImpEntryList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDocumentNo = new Label();
            Label lblSubDocumentNo = new Label();
            Label lblMode = new Label();
            Label lblDocument_Sight_Usance = new Label();
            Button btnDelete = new Button();
            string docNo = txtDocPrFx.Text.Trim() + "/" + txtDocumentNo.Text.Trim() + "/" + txtBranchCode.Text.Trim() + "/" + txtYear.Text.Trim();

            lblDocumentNo = (Label)e.Row.FindControl("lblDocumentNo");
            //lblSubDocumentNo = (Label)e.Row.FindControl("lblSubDocumentNo");
            //lblMode = (Label)e.Row.FindControl("lblMode");
            //lblDocument_Sight_Usance = (Label)e.Row.FindControl("lblDocument_Sight_Usance");
            btnDelete = (Button)e.Row.FindControl("btnDelete");
            btnDelete.Enabled = true;
            btnDelete.CssClass = "deleteButton";
            btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");
            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.location='TF_IMP_BOE_Maker.aspx?mode=" + lblMode.Text + "&DocNo=" + lblDocumentNo.Text.Trim()
                    + "&DocPrFx=" + txtDocPrFx.Text.Trim() + "&DocSrNo=" + txtDocumentNo.Text.Trim() + "&BranchCode=" + txtBranchCode.Text.Trim()
                    + "&DocYear=" + txtYear.Text.Trim() + "&SubDocNo=" + lblSubDocumentNo.Text.Trim() + "&flcIlcType=" + lblDocument_Sight_Usance.Text.Trim() + "'";
                if (i != 5)
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string docNo = txtDocPrFx.Text.Trim() + "/" + txtBranchCode.Text.Trim() + "/" + txtYear.Text.Trim() + "/" + txtDocumentNo.Text.Trim(),
            flcIlcType = "";

        Response.Redirect("../IMP/Transactions/TF_IMP_BOE_Maker.aspx?mode=add&DocNo=" + docNo
            + "&DocPrFx=" + txtDocPrFx.Text.Trim() + "&DocSrNo=" + txtDocumentNo.Text.Trim() + "&BranchCode=" + txtBranchCode.Text.Trim()
            + "&DocYear=" + txtYear.Text.Trim() + "&flcIlcType=" + flcIlcType, true);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }

    protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
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
        if (dt.Rows.Count > 0)
        {
            txtBranchCode.Text = dt.Rows[0]["BranchCode"].ToString().Trim();
            getLastDocNo();
        }
        else
        {
            txtBranchCode.Text = "";
            txtDocumentNo.Text = "";
        }
        fillGrid();
        ddlBranch.Focus();
    }
    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
        fillGrid();
    }

    protected void rdb_Foreign_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void rdb_Local_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void rdb_LC_CheckedChanged(object sender, EventArgs e)
    {
        if (rdb_LC.Checked == true)
        {
            if (rdb_usance.Checked == true)
            {
                txtDocPrFx.Text = "ACC";

            }
            if (rdb_sight.Checked == true)
            {
                txtDocPrFx.Text = "IBA";

            }

        }
        fillGrid();
    }
    protected void rdb_C_CheckedChanged(object sender, EventArgs e)
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
        }
        fillGrid();
    }
    protected void rdb_sight_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void rdb_usance_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    private void getLastDocNo()
    {
        TF_DATA obj = new TF_DATA();
        SqlParameter pYear = new SqlParameter("@year", SqlDbType.VarChar);
        pYear.Value = txtYear.Text.Trim();
        SqlParameter pDocType = new SqlParameter("@docType", SqlDbType.VarChar);
        pDocType.Value = txtDocPrFx.Text.Trim();
        SqlParameter pFlcIlcType = new SqlParameter("@flcIlcType", SqlDbType.VarChar);
        string _flcIlcType = "";

        //if (rdbSight.Checked == true)
        //    _flcIlcType = "S";
        //if (rdbUsuance.Checked == true)
        //    _flcIlcType = "U";
        pFlcIlcType.Value = _flcIlcType;

        string _query1 = "TF_ImpAuto_GetLastDocNo_IMP";
        string _lastDocNo = obj.SaveDeleteData(_query1, pYear, pDocType, pFlcIlcType);
        txtDocumentNo.Text = _lastDocNo;
    }
}