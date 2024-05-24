using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EXP_EXP_ViewRealisationEntry : System.Web.UI.Page
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

                //txtYear.Text = DateTime.Now.Year.ToString().Substring(2, 2);

                txtYear.Text = (DateTime.Now.Month >= 4 ? DateTime.Now.ToString("yy") : DateTime.Now.AddYears(-1).ToString("yy"));

                txtDocPrFx.Value = "BLA";
                ddlrecordperpage.SelectedValue = "20";
                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch_SelectedIndexChanged(null, null);
                ddlBranch.Enabled = false;
                fillGrid();
                /////////////////////////////////////////////BEFORE///////////////////////////////////////
                //if (Request.QueryString["result"] != null)
                //{
                //    if (Request.QueryString["result"].ToString() == "added")
                //    {
                //        string _docNo = Request.QueryString["result"].ToString();
                //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added.');", true);
                //    }
                //    else
                //        if (Request.QueryString["result"].Trim() == "updated")
                //        {
                //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
                //        }
                //}

                ///////////////////////////////////////////////////Modified By Nilesh///////////////////////////////////////////////////////////////////////////////////////////////////////////////
                if (Request.QueryString["result"] != null)
                {

                    string frm = Request.QueryString["frm"].ToString();
                    string rptdocno = Request.QueryString["rptFrdocno"].ToString();
                    ////////////////////Added by Shailesh
                    string Bnch = Request.QueryString["Branch"].ToString();
                    if (Request.QueryString["rptFrdocno"].ToString().Substring(0, 3) == "BCA" || Request.QueryString["rptFrdocno"].ToString().Substring(0, 3) == "BCU")
                    {
                        if (Bnch == "6770001")
                        {
                            txtYear.Text = Request.QueryString["rptFrdocno"].ToString().Substring(8, 2);
                        }
                    }
                    //////
                    if (Request.QueryString["result"].ToString().Substring(0, 5) == "added")
                    {
                        if (Request.QueryString["saveType"].ToString() == "save")
                        {
                            string _docNo = Request.QueryString["result"].ToString().Substring(5);
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added ');", true);
                        }
                        else
                        {
                            string _docNo = Request.QueryString["result"].ToString().Substring(5);
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added');", true);
                            string _script = "window.open('../Reports/EXPReports/ViewExportBillDocument.aspx?frm=" + frm.Trim() + "&rptType=Single&rptFrdocno=" + rptdocno.Trim() + "&rptTodocno=" + rptdocno.Trim() + "&rptCode=2&Branch=" + Session["userADCode"].ToString() + "&Report=Export Bill Document Realisation', 'popup_window', 'height=520,  width=800,status= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300,resizable=yes');";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                        }
                    }

                    else if (Request.QueryString["result"].Trim() == "updated")
                    {
                        if (Request.QueryString["saveType"].ToString() == "save")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
                            string _script = "window.open('../Reports/EXPReports/ViewExportBillDocument.aspx?frm=" + frm.Trim() + "&rptType=Single&rptFrdocno=" + rptdocno.Trim() + "&rptTodocno=" + rptdocno.Trim() + "&rptCode=2&Branch=" + Session["userADCode"].ToString() + "&Report=Export Bill Document Realisation', 'popup_window', 'height=520,  width=800,status= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300,resizable=yes');";
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "redirect", _script, true);
                        }

                    }
                }
                ///////////////////////////////////////////////////////END////////////////////////////////////////////////////////////////////////////////////////////////////////////


                btnAdd.Attributes.Add("onclick", "return validate();");
                btnSearch.Attributes.Add("onclick", "return validateSearch();");
                //// txtYear.Attributes.Add("onblur", "return checkYear();");
                txtYear.Attributes.Add("onkeydown", "return validate_Number(event);");
                //ddlBranch.Focus();
            }
        }
    }

    protected void GridViewEXPDocRealised_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
        string _docNo = "", Sr_No = "";

        string[] values_p;
        string str = e.CommandArgument.ToString();
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        if (str != "")
        {
            char[] splitchar = { ';' };
            values_p = str.Split(splitchar);
            _docNo = values_p[0].ToString();
            Sr_No = values_p[1].ToString();

        }

        SqlParameter p1 = new SqlParameter("@docNo", SqlDbType.VarChar);
        p1.Value = _docNo;

        SqlParameter p2 = new SqlParameter("@Sr_No", SqlDbType.VarChar);
        p2.Value = Sr_No;

        SqlParameter p3 = new SqlParameter("@user", SqlDbType.VarChar);
        p3.Value = _userName;
        SqlParameter p4 = new SqlParameter("@uploadeddate", SqlDbType.VarChar);
        p4.Value = _uploadingDate;

        string _query = "TF_EXP_DeleteRealizedEntryDetails";
        TF_DATA objData = new TF_DATA();
        result = objData.SaveDeleteData(_query, p1, p2, p3, p4);
        fillGrid();
        if (result == "deleted")
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);
        else
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record cannot be deleted as EBRC is already Generated.');", true);
    }

    protected void GridViewEXPDocRealised_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDocumentNo = new Label();
            Label lblSrNo = new Label();

            Button btnDelete = new Button();

            lblDocumentNo = (Label)e.Row.FindControl("lblDocumentNo");
            lblSrNo = (Label)e.Row.FindControl("lblSrNo");
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
                string pageurl = "window.location='EXP_AddEditFullRealisation.aspx?mode=edit&DocNo=" + lblDocumentNo.Text.Trim()
                    + "&DocPrFx=" + txtDocPrFx.Value + "&BranchCode=" + hdnBranchCode.Value.Trim() + "&BranchName=" + ddlBranch.Text + "&SrNo=" + lblSrNo.Text.Trim() + "'";
                if (i != 8)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
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
            hdnBranchCode.Value = dt.Rows[0]["BranchCode"].ToString().Trim();
        }
        else
        {
            hdnBranchCode.Value = "";
        }
        fillGrid();
        ddlBranch.Focus();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //string type = "";
        //if (rdbNegotiation.Checked == true)
        //    type = "N";
        //if (rdbPurchase.Checked == true)
        //    type = "P";
        //if (rdbDiscount.Checked == true)
        //    type = "D";
        //if (rdbEBR.Checked == true)
        //    type = "E";
        //if (rdbCollection.Checked == true)
        //    type = "C";
        if (rbtbla.Checked == true)
            txtDocPrFx.Value = "BLA";
        if (rbtblu.Checked == true)
            txtDocPrFx.Value = "BLU";
        if (rbtbba.Checked == true)
            txtDocPrFx.Value = "BBA";
        if (rbtbbu.Checked == true)
            txtDocPrFx.Value = "BBU";
        if (rbtbca.Checked == true)
            txtDocPrFx.Value = "BCA";
        if (rbtbcu.Checked == true)
            txtDocPrFx.Value = "BCU";
        if (rbtIBD.Checked == true)
            txtDocPrFx.Value = "IBD";
        //if (rbtLBC.Checked == true)
        //        txtDocPrFx.Value = "LBC";
        if (rbtBEB.Checked == true)
            txtDocPrFx.Value = "EB";
        string year = txtYear.Text;
        string ForeignOrLocal ="";
        if (rbtnForeign.Checked == true)
            ForeignOrLocal = "F";
        else if (rbtnLocal.Checked == true)
            ForeignOrLocal = "L";

        Response.Redirect("EXP_AddEditFullRealisation.aspx?mode=add&DocPrFx=" + txtDocPrFx.Value + "&BranchCode=" + hdnBranchCode.Value.Trim() + "&BranchName=" + ddlBranch.Text + "&year=" + year + "&ForeignOrLocal=" + ForeignOrLocal, true);
    }

    protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }

    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewEXPDocRealised.PageIndex = 0;
        fillGrid();
    }

    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewEXPDocRealised.PageIndex > 0)
        {
            GridViewEXPDocRealised.PageIndex = GridViewEXPDocRealised.PageIndex - 1;
        }
        fillGrid();
    }

    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewEXPDocRealised.PageIndex != GridViewEXPDocRealised.PageCount - 1)
        {
            GridViewEXPDocRealised.PageIndex = GridViewEXPDocRealised.PageIndex + 1;
        }
        fillGrid();
    }

    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewEXPDocRealised.PageIndex = GridViewEXPDocRealised.PageCount - 1;
        fillGrid();
    }

    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
        fillGrid();
    }

    protected void fillGrid()
    {
        string type = "", _foreignOrLocal = "";
        string query = "TF_EXPGetRealisationEntryListNew";


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
        ////if (rbtLBC.Checked == true)
        ////    type = "LBC";
        if (rbtBEB.Checked == true)
            type = "EB";

        if (rbtnForeign.Checked == true)
            _foreignOrLocal = "F";
        else if (rbtnLocal.Checked == true)
            _foreignOrLocal = "L";


        SqlParameter p1 = new SqlParameter("@type", SqlDbType.VarChar);
        p1.Value = type;

        SqlParameter p2 = new SqlParameter("@branch", SqlDbType.VarChar);
        p2.Value = ddlBranch.Text;

        SqlParameter p3 = new SqlParameter("@year", SqlDbType.VarChar);
        p3.Value = txtYear.Text;

        SqlParameter p4 = new SqlParameter("@search", SqlDbType.VarChar);
        p4.Value = txtSearch.Text;

        SqlParameter p5 = new SqlParameter("@Foreign_Local_Bill", _foreignOrLocal);

        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(query, p1, p2, p3, p4, p5);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewEXPDocRealised.PageSize = _pageSize;
            GridViewEXPDocRealised.DataSource = dt.DefaultView;
            GridViewEXPDocRealised.DataBind();
            GridViewEXPDocRealised.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewEXPDocRealised.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
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

    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewEXPDocRealised.PageCount != GridViewEXPDocRealised.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewEXPDocRealised.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewEXPDocRealised.PageIndex + 1) + " of " + GridViewEXPDocRealised.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewEXPDocRealised.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewEXPDocRealised.PageIndex != (GridViewEXPDocRealised.PageCount - 1))
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

    protected void rbtbla_CheckedChanged(object sender, EventArgs e)
    {
        txtDocPrFx.Value = "BLA";
        fillGrid();
        rbtbla.Focus();
    }

    protected void rbtblu_CheckedChanged(object sender, EventArgs e)
    {
        txtDocPrFx.Value = "BLU";
        fillGrid();
        rbtblu.Focus();
    }

    protected void rbtbba_CheckedChanged(object sender, EventArgs e)
    {
        txtDocPrFx.Value = "BBA";
        fillGrid();
        rbtbba.Focus();
    }

    protected void rbtbbu_CheckedChanged(object sender, EventArgs e)
    {
        txtDocPrFx.Value = "BBU";
        fillGrid();
        rbtbbu.Focus();
    }

    protected void rbtbcs_CheckedChanged(object sender, EventArgs e)
    {
        txtDocPrFx.Value = "BCA";
        fillGrid();
        rbtbca.Focus();
    }

    protected void rbtbcu_CheckedChanged(object sender, EventArgs e)
    {
        txtDocPrFx.Value = "BCU";
        fillGrid();
        rbtbcu.Focus();
    }

    protected void rbtIBD_CheckedChanged(object sender, EventArgs e)
    {
        txtDocPrFx.Value = "IBD";
        fillGrid();
        rbtIBD.Focus();
    }

    protected void rbtBEB_CheckedChanged(object sender, EventArgs e)
    {
        txtDocPrFx.Value = "EB";
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