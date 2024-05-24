using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EXP_EXP_ViewExportBillEntry : System.Web.UI.Page
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

                txtYear.Text = (DateTime.Now.Month >= 4 ? DateTime.Now.ToString("yy") : DateTime.Now.AddYears(-1).ToString("yy"));

                ddlrecordperpage.SelectedValue = "20";
                if (rbtbla.Checked == true)
                    txtDocPrFx.Text = "BLA";
                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch_SelectedIndexChanged(null, null);
                //ddlBranch.Enabled = false;
                fillGrid();
                if (Request.QueryString["result"] != null)
                {
                    string frm = Request.QueryString["frm"].ToString();
                    string rptdocno = Request.QueryString["rptFrdocno"].ToString();

                    if (Request.QueryString["result"].ToString().Substring(0, 5) == "added")
                    {
                        string DocType = Request.QueryString["result"].ToString().Substring(5, 2);
                        string _DocNo = "";
                        if (DocType == "EB")
                        {
                            _DocNo = Request.QueryString["result"].ToString().Substring(5, 13);
                        }
                        else
                        {
                            _DocNo = Request.QueryString["result"].ToString().Substring(5, 14);
                        }

                        if (Request.QueryString["saveType"].ToString() == "Submit")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record submit to Checker with Document No. : " + _DocNo + "');", true);
                        }
                        else if (Request.QueryString["saveType"].ToString() == "save")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added with Document No. : " + _DocNo + "');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added with Document No. : " + _DocNo + "');", true);
                            string _script = "window.open('../Reports/EXPReports/ViewExportBillDocument.aspx?frm=" + frm.Trim() + "&rptType=Single&rptFrdocno=" + rptdocno.Trim() + "&rptTodocno=" + rptdocno.Trim() + "&rptCode=2&Branch=" + Session["userADCode"].ToString() + "&Report=Export Bill lodgement', 'popup_window', 'height=520,  width=800,status= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300,resizable=yes');";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                        }
                    }

                    else if (Request.QueryString["result"].Trim() == "updated")
                    {
                        if (Request.QueryString["saveType"].ToString() == "Submit")
                        {
                            string _DocNo = Request.QueryString["rptTodocno"].ToString();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record submit to Checker with Document No. : " + _DocNo + "');", true);
                        }
                        else if (Request.QueryString["saveType"].ToString() == "save")
                        {
                            string _DocNo = Request.QueryString["rptTodocno"].ToString();
                            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added with Document No. : " + _docNo + "');", true);
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated with Document No. : " + _DocNo + "');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
                            string _script = "window.open('../Reports/EXPReports/ViewExportBillDocument.aspx?frm=" + frm.Trim() + "&rptType=Single&rptFrdocno=" + rptdocno.Trim() + "&rptTodocno=" + rptdocno.Trim() + "&rptCode=2&Branch=" + Session["userADCode"].ToString() + "&Report=Export Bill lodgement', 'popup_window', 'height=520,  width=800,status= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300,resizable=yes');";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                        }
                    }
                }

                btnAdd.Attributes.Add("onclick", "return validate();");
                btnSearch.Attributes.Add("onclick", "return validateSearch();");
                //txtYear.Attributes.Add("onblur", "return checkYear();");
                txtDocumentNo.Attributes.Add("onblur", "return checkDocNo();");
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

    protected void GridViewEXPbillEntry_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _docNo = e.CommandArgument.ToString();

        SqlParameter p1 = new SqlParameter("@documentNo", SqlDbType.VarChar);
        p1.Value = _docNo;
        SqlParameter p2 = new SqlParameter("@user", SqlDbType.VarChar);
        p2.Value = _userName;
        SqlParameter p3 = new SqlParameter("@uploadeddate", SqlDbType.VarChar);
        p3.Value = _uploadingDate;

        string _query = "TF_EXP_DeleteExportBillEntryDetails";
        TF_DATA objData = new TF_DATA();
        result = objData.SaveDeleteData(_query, p1, p2, p3);
        fillGrid();
        if (result == "deleted")
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);
        else
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record cannot be deleted as it is realised.');", true);
    }

    protected void GridViewEXPbillEntry_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDocumentNo = new Label();
            Label lblSubDocumentNo = new Label();
            Label lblDocument_Sight_Usance = new Label();
            Label lblDispByDefaltuValue = new Label();
            Label lblstatus = new Label();

            Button btnDelete = new Button();

            lblDocumentNo = (Label)e.Row.FindControl("lblDocumentNo");
            lblSubDocumentNo = (Label)e.Row.FindControl("lblSubDocumentNo");
            lblDispByDefaltuValue = (Label)e.Row.FindControl("lblDispByDefaltuValue");

            

            lblstatus = (Label)e.Row.FindControl("lblstatus");
            if (lblstatus.Text == "Reject By Checker")
            {
                e.Row.BackColor = System.Drawing.Color.Tomato;
            }

            btnDelete = (Button)e.Row.FindControl("btnDelete");

            btnDelete.Enabled = true;
            btnDelete.CssClass = "deleteButton";
            if (hdnUserRole.Value == "OIC")
                btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");
            else
                btnDelete.Attributes.Add("onclick", "alert('Only OIC can delete all records.');return false;");

            string _foreignOrLocal = "";
            if (rbtnForeign.Checked == true)
                _foreignOrLocal = "F";
            else if (rbtnLocal.Checked == true)
                _foreignOrLocal = "L";

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.location='EXP_AddEditExportBillEntry.aspx?mode=edit&DocNo=" + lblDocumentNo.Text.Trim()
                    + "&DocPrFx=" + txtDocPrFx.Text + "&DocSrNo=" + txtDocumentNo.Text + "&DocYear=" + txtYear.Text
                    + "&BranchCode=" + txtBranchCode.Text.Trim() + "&BranchName=" + ddlBranch.Text.Trim() + "&ForeignOrLocal=" + _foreignOrLocal + "'";
                if (i != 10)
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
        string type = "", _foreignOrLocal = "";
        string query = "TF_EXP_GetExportBillEntryList_Maker";

        if (rbtbla.Checked == true)
            type = "BLA";
        if (rbtblu.Checked == true)
            type = "BLU";
        if (rbtbba.Checked == true)
            type = "BBA";
        if (rbtbbu.Checked == true)
            type = "BBU";
        if (rbtbca.Checked == true)
            type = "BCA";
        if (rbtbcu.Checked == true)
            type = "BCU";
        if (rbtIBD.Checked == true)
            type = "IBD";
        if (rbtBEB.Checked == true)
            type = "EB";

        if (rbtnForeign.Checked == true)
            _foreignOrLocal = "F";
        else if (rbtnLocal.Checked == true)
            _foreignOrLocal = "L";

        SqlParameter p1 = new SqlParameter("@type", SqlDbType.VarChar);
        p1.Value = type;

        SqlParameter p2 = new SqlParameter("@branch", SqlDbType.VarChar);
        p2.Value = ddlBranch.Text.Trim();

        SqlParameter p3 = new SqlParameter("@year", SqlDbType.VarChar);
        p3.Value = txtYear.Text.Trim();

        SqlParameter p4 = new SqlParameter("@search", SqlDbType.VarChar);
        p4.Value = txtSearch.Text;

        SqlParameter p5 = new SqlParameter("@Foreign_Local_Bill", _foreignOrLocal);

        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(query, p1, p2, p3, p4, p5);

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

    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
        //fillGrid();
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
        string _lastDocNo = obj.SaveDeleteData(_query1, pYear, pDocType, pFL, pBranchCode);
        txtYear.Text = _lastDocNo.Substring(0, 2);
        txtDocumentNo.Text = _lastDocNo.Substring(2);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string _foreignOrLocal = "";
        if (rbtnForeign.Checked == true)
            _foreignOrLocal = "F";
        else if (rbtnLocal.Checked == true)
            _foreignOrLocal = "L";

        Response.Redirect("EXP_AddEditExportBillEntry.aspx?mode=add&DocPrFx=" + txtDocPrFx.Text + "&DocSrNo=" + txtDocumentNo.Text + "&DocYear=" + txtYear.Text
                    + "&BranchCode=" + txtBranchCode.Text.Trim() + "&BranchName=" + ddlBranch.Text.Trim() + "&ForeignOrLocal=" + _foreignOrLocal, true);
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
