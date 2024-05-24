using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EXP_EXP_ViewUpdatingAcceptedDueDate : System.Web.UI.Page
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
                txtYear.Text = DateTime.Now.Year.ToString().Substring(2, 2);
                ddlrecordperpage.SelectedValue = "20";

                if (rbtbla.Checked == true)
                    txtDocPrFx.Text = "BLA";

                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch_SelectedIndexChanged(null, null);
                ddlBranch.Enabled = false;
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
                // btnAdd.Attributes.Add("onclick", "return validate();");
                btnSearch.Attributes.Add("onclick", "return validateSearch();");
                ////txtYear.Attributes.Add("onblur", "return checkYear();");
                ////txtDocumentNo.Attributes.Add("onblur", "return checkDocNo();");
                txtDocumentNo.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtYear.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtBranchCode.Attributes.Add("onkeydown", "return false;");
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
            if (GridViewEXPbillEntry.PageCount != GridViewEXPbillEntry.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewEXPbillEntry.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewEXPbillEntry.PageIndex + 1) + " of " + GridViewEXPbillEntry.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewEXPbillEntry.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewEXPbillEntry.PageIndex != (GridViewEXPbillEntry.PageCount - 1))
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
        GridViewEXPbillEntry.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewEXPbillEntry.PageIndex > 0)
        {
            GridViewEXPbillEntry.PageIndex = GridViewEXPbillEntry.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewEXPbillEntry.PageIndex != GridViewEXPbillEntry.PageCount - 1)
        {
            GridViewEXPbillEntry.PageIndex = GridViewEXPbillEntry.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewEXPbillEntry.PageIndex = GridViewEXPbillEntry.PageCount - 1;
        fillGrid();
    }

    protected void GridViewEXPbillEntry_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDocumentNo = new Label();
            Label lblSubDocumentNo = new Label();
            Label lblDocument_Sight_Usance = new Label();

            lblDocumentNo = (Label)e.Row.FindControl("lblDocumentNo");
            lblSubDocumentNo = (Label)e.Row.FindControl("lblSubDocumentNo");
            lblDocument_Sight_Usance = (Label)e.Row.FindControl("lblDocument_Sight_Usance");


            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.location='EXP_AddEditAcceptedDate.aspx?mode=edit&DocNo=" + lblDocumentNo.Text.Trim() + "&DocPrFx=" + txtDocPrFx.Text + "'";
                if (i != 5)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    { fillGrid(); }

    protected void fillGrid()
    {
        getLastDocNo();
        string search = txtSearch.Text.Trim(), _foreignOrLocal = "";
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        SqlParameter p2 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p2.Value = ddlBranch.Text.Trim();
        SqlParameter p3 = new SqlParameter("@DocType", SqlDbType.VarChar);
        p3.Value = txtDocPrFx.Text.Trim();
        SqlParameter p4 = new SqlParameter("@year", SqlDbType.VarChar);
        p4.Value = txtYear.Text.Trim();

        if (rbtnForeign.Checked == true)
            _foreignOrLocal = "F";
        else if (rbtnLocal.Checked == true)
            _foreignOrLocal = "L";

        SqlParameter p5 = new SqlParameter("@Foreign_Local_Bill", _foreignOrLocal);

        string _query = "TF_EXP_GetExportBillEntryList";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2, p3, p4,p5);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewEXPbillEntry.PageSize = _pageSize;
            GridViewEXPbillEntry.DataSource = dt.DefaultView;
            GridViewEXPbillEntry.DataBind();
            GridViewEXPbillEntry.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewEXPbillEntry.Visible = false;
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
        string _query = "TF_GetBranchDetails_EXP_BillEntry";
        DataTable dt = objData.getData(_query, p1);

        if (dt.Rows.Count > 0)
        {
            txtBranchCode.Text = dt.Rows[0]["BranchCode"].ToString().Trim();

            getLastDocNo();
        }
        else
        {
            hdnBranchCode.Value = "";
            txtDocumentNo.Text = "";
        }
        fillGrid();
        ddlBranch.Focus();
    }
    protected void txtDocumentNo_TextChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
        fillGrid();
    }

    private void getLastDocNo()
    {
        string _foreignOrLocal = "";
        TF_DATA obj = new TF_DATA();
        SqlParameter pYear = new SqlParameter("@year", SqlDbType.VarChar);
        pYear.Value = txtYear.Text.Trim();
        SqlParameter pDocType = new SqlParameter("@docType", SqlDbType.VarChar);
        pDocType.Value = txtDocPrFx.Text.Trim();
        SqlParameter pBranchCode = new SqlParameter("@branchCode", SqlDbType.VarChar);
        pBranchCode.Value = txtBranchCode.Text.Trim();

        if (rbtnForeign.Checked == true)
            _foreignOrLocal = "F";
        else if (rbtnLocal.Checked == true)
            _foreignOrLocal = "L";

        SqlParameter pFL = new SqlParameter("@foreignOrLocal", _foreignOrLocal);

        string _query1 = "TF_GetLastDocNo_EXP";
        string _lastDocNo = obj.SaveDeleteData(_query1, pYear, pDocType, pFL,pBranchCode);
        txtDocumentNo.Text = _lastDocNo;
    }
    //protected void rdbNego_CheckedChanged(object sender, EventArgs e)
    //{
    //    txtDocPrFx.Text = "N";
    //    fillGrid();
    //    rdbNego.Focus();
    //}
    //protected void rdbPur_CheckedChanged(object sender, EventArgs e)
    //{
    //    txtDocPrFx.Text = "P";
    //    fillGrid();
    //    rdbPur.Focus();
    //}
    //protected void rdbDis_CheckedChanged(object sender, EventArgs e)
    //{
    //    txtDocPrFx.Text = "D";
    //    fillGrid();
    //    rdbDis.Focus();
    //}
    //protected void rdbEBR_CheckedChanged(object sender, EventArgs e)
    //{
    //    txtDocPrFx.Text = "E";
    //    fillGrid();
    //    rdbEBR.Focus();
    //}
    //protected void rdbColl_CheckedChanged(object sender, EventArgs e)
    //{
    //    txtDocPrFx.Text = "C";
    //    fillGrid();
    //    rdbColl.Focus();
    //}
    //protected void rdbMAdv_CheckedChanged(object sender, EventArgs e)
    //{
    //    txtDocPrFx.Text = "M";
    //    fillGrid();
    //    rdbMAdv.Focus();
    //}

    //protected void rdbLBDbuyers_CheckedChanged(object sender, EventArgs e)
    //{
    //    txtDocPrFx.Text = "B";
    //    fillGrid();
    //    rdbLBDbuyers.Focus();
    //}
    //protected void rdbLBDsellers_CheckedChanged(object sender, EventArgs e)
    //{
    //    txtDocPrFx.Text = "S";
    //    fillGrid();
    //    rdbLBDsellers.Focus();
    //}

    protected void rbtbla_CheckedChanged(object sender, EventArgs e)
    {
        txtDocPrFx.Text = "BLA";
        fillGrid();
        rbtbla.Focus();
    }
    protected void rbtblu_CheckedChanged(object sender, EventArgs e)
    {
        txtDocPrFx.Text = "BLU";
        fillGrid();
        rbtblu.Focus();
    }
    protected void rbtbba_CheckedChanged(object sender, EventArgs e)
    {
        txtDocPrFx.Text = "BBA";
        fillGrid();
        rbtbba.Focus();
    }
    protected void rbtbbu_CheckedChanged(object sender, EventArgs e)
    {
        txtDocPrFx.Text = "BBU";
        fillGrid();
        rbtbbu.Focus();
    }

    protected void rbtbcs_CheckedChanged(object sender, EventArgs e)
    {
        txtDocPrFx.Text = "BCA";
        fillGrid();
        rbtbca.Focus();
    }
    protected void rbtbcu_CheckedChanged(object sender, EventArgs e)
    {
        txtDocPrFx.Text = "BCU";
        fillGrid();
        rbtbcu.Focus();
    }
    protected void rbtIBD_CheckedChanged(object sender, EventArgs e)
    {
        txtDocPrFx.Text = "IBD";
        fillGrid();
        rbtIBD.Focus();
    }
    //protected void rbtLBC_CheckedChanged(object sender, EventArgs e)
    //{
    //    txtDocPrFx.Text = "LBC";
    //    fillGrid();
    //    rbtLBC.Focus();
    //}
    protected void rbtBEB_CheckedChanged(object sender, EventArgs e)
    {
        txtDocPrFx.Text = "EB";
        fillGrid();
        rbtBEB.Focus();
    }

    protected void rbtnForeign_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
        rbtnForeign.Focus();
    }
    protected void rbtnLocal_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
        rbtnLocal.Focus();
    }
}
