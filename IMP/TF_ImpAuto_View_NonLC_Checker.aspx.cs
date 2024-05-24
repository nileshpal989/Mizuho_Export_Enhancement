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

public partial class IMP_TF_ImpAuto_View_NonLC_Checker : System.Web.UI.Page
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

                rdbForeign.Visible = true;
                rdbLocal.Visible = true;

                rdbusance_new.Visible = true;
                rdbsight_new.Visible = true;

                rdbForeign.Checked = true;
                rdbFLC.Visible = true;
                //  rdbFLC.Visible = false;

                // rdbC.Checked = true;
                //rdbC.Visible = true;
                //rdbDIR.Visible = true;
                //rdbADV.Visible = true;
                //rdbIFC.Visible = true;

                //rdbDIR.Visible = false;
                //rdbADV.Visible = false;
                //rdbIFC.Visible = false;        

                if (rdbFLC.Checked == true)
                    txtDocPrFx.Text = rdbFLC.Text;

                //if (rdbC.Checked == true)
                //{
                //    txtDocPrFx.Text = rdbC.Text;
                //    rdbC_CheckedChanged(null, null);
                //}
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
        if (rdbForeign.Checked == true)
        {
            rdbForeign_CheckedChanged(null, null);
        }
        if (rdbLocal.Checked == true)
        {
            rdbLocal_CheckedChanged(null, null);

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

    protected void GridViewImpEntryList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";


        string _docNo = e.CommandArgument.ToString();

        SqlParameter p1 = new SqlParameter("@docNo", SqlDbType.VarChar);
        p1.Value = _docNo;

        string _query = "TF_DeleteInwardEntryDetails";
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
            lblSubDocumentNo = (Label)e.Row.FindControl("lblSubDocumentNo");
            lblMode = (Label)e.Row.FindControl("lblMode");
            lblDocument_Sight_Usance = (Label)e.Row.FindControl("lblDocument_Sight_Usance");

            btnDelete = (Button)e.Row.FindControl("btnDelete");

            btnDelete.Enabled = true;
            btnDelete.CssClass = "deleteButton";
            btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.location='TF_ImpAuto_AddEdit_NonLC_Checker.aspx?mode=" + lblMode.Text + "&DocNo=" + lblDocumentNo.Text.Trim()
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string docNo = txtDocPrFx.Text.Trim() + "/" + txtDocumentNo.Text.Trim() + "/" + txtBranchCode.Text.Trim() + "/" + txtYear.Text.Trim();
        string _flcIlcType = "";
        //if (rdbFLC.Checked == true || rdbILC.Checked == true)
        //{
        //    if (rdbSight.Checked == true)
        //        _flcIlcType = "S";
        //    if (rdbUsuance.Checked == true)
        //        _flcIlcType = "U";
        //}
        Response.Redirect("TF_ImpAuto_AddEdit_NonLC_Checker.aspx?mode=add&DocNo=" + docNo
            + "&DocPrFx=" + txtDocPrFx.Text.Trim() + "&DocSrNo=" + txtDocumentNo.Text.Trim() + "&BranchCode=" + txtBranchCode.Text.Trim()
            + "&DocYear=" + txtYear.Text.Trim() + "&flcIlcType=" + _flcIlcType, true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    { fillGrid(); }


    protected void fillGrid()
    {
        getLastDocNo();
        string search = txtSearch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;
        SqlParameter p2 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p2.Value = ddlBranch.Text.Trim();
        SqlParameter p3 = new SqlParameter("@Year", SqlDbType.VarChar);
        p3.Value = txtYear.Text.Trim();
        SqlParameter p4 = new SqlParameter("@DocType", SqlDbType.VarChar);
        p4.Value = txtDocPrFx.Text.Trim();
        SqlParameter p5 = new SqlParameter("@flcIlcType", SqlDbType.VarChar);
        string _flcIlcType = "";

        //if (rdbSight.Checked == true)
        //    _flcIlcType = "S";
        //if (rdbUsuance.Checked == true)
        //    _flcIlcType = "U";
        p5.Value = _flcIlcType;

        SqlParameter p6 = new SqlParameter("@flcIlcMode", SqlDbType.VarChar);
        string _flcIlcMode = "";

        //if (rdbAdd.Checked == true)
        //    _flcIlcMode = "add";
        //if (rdbUpdate.Checked == true)
            _flcIlcMode = "edit";
        p6.Value = _flcIlcMode;

        string _query = "TF_ImpAuto_GetImportEntryList";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2, p3, p4, p5, p6);
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

    protected void rdbForeign_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbForeign.Checked == true)
        {
            txtForeignLocal.Text = "4";
            //rdbFLC.Visible = true;
            // rdbFLC.Visible = false;
            //rdbC.Visible = true;
            //rdbDIR.Visible = true;
            //rdbADV.Visible = true;
            //rdbIFC.Visible = true;

            //rdbDIR.Visible = false;
            //rdbADV.Visible = false;
            //rdbIFC.Visible = false;

            //rdbIBC.Visible = false;
            //rdbILC.Visible = false;

            if (rdbFLC.Checked == true)
            {
                if (rdbusance_new.Checked == true)
                {
                    txtDocPrFx.Text = "ACC";

                }
                if (rdbsight_new.Checked == true)
                {
                    txtDocPrFx.Text = "IBA";

                }

            }

            if (rdbC.Checked == true)
            {
                if (rdbusance_new.Checked == true)
                {
                    txtDocPrFx.Text = "ICU";

                }
                if (rdbsight_new.Checked == true)
                {
                    txtDocPrFx.Text = "ICA";

                }
            }

        }
        fillGrid();
        rdbForeign.Focus();

    }

    protected void rdbLocal_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbLocal.Checked == true)
        {
            txtForeignLocal.Text = "6";


            if (rdbFLC.Checked == true)
            {
                if (rdbusance_new.Checked == true)
                {
                    txtDocPrFx.Text = "ACC";

                }
                if (rdbsight_new.Checked == true)
                {
                    txtDocPrFx.Text = "IBA";

                }

            }

            if (rdbC.Checked == true)
            {
                if (rdbusance_new.Checked == true)
                {
                    txtDocPrFx.Text = "ICU";

                }
                if (rdbsight_new.Checked == true)
                {
                    txtDocPrFx.Text = "ICA";

                }
            }

        }
        fillGrid();
        rdbLocal.Focus();
    }

    protected void rdbFLC_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbFLC.Checked == true)
        {
            if (rdbusance_new.Checked == true)
            {
                txtDocPrFx.Text = "ACC";

            }
            if (rdbsight_new.Checked == true)
            {
                txtDocPrFx.Text = "IBA";

            }

        }
        fillGrid();
        rdbFLC.Focus();
    }
    protected void rdbC_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbC.Checked == true)
        {
            if (rdbusance_new.Checked == true)
            {
                txtDocPrFx.Text = "ICU";

            }
            if (rdbsight_new.Checked == true)
            {
                txtDocPrFx.Text = "ICA";

            }
        }
        fillGrid();
        rdbC.Focus();
    }
    
   

    protected void rdbAdd_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
        //rdbAdd.Focus();
    }
    protected void rdbUpdate_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
        //rdbUpdate.Focus();
    }
    protected void rdbSight_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
        //rdbSight.Focus();
    }
    protected void rdbUsuance_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
        //rdbUsuance.Focus();
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

    protected void txtForeignLocal_TextChanged(object sender, EventArgs e)
    {

    }
    protected void rdbsight_new_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void rdbusance_new_CheckedChanged(object sender, EventArgs e)
    {

    }
}