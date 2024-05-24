﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class EXP_EXP_ViewApprovedLodgemetForROD_Checker : System.Web.UI.Page
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
                hdnUserRole.Value = Session["userRole"].ToString().Trim();
                if (Request.QueryString["AR"] != null)
                {
                    string _DocNo = Request.QueryString["DocNo"].ToString();
                    if (Request.QueryString["AR"].Trim() == "A")
                    {
                        string script1 = "alert('Record Approved with Trans.Ref.No " + _DocNo + "')";
                        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", script1, true);
                    }
                    else if (Request.QueryString["AR"].Trim() == "R")
                    {
                        string script1 = "alert('Record Rejected with Trans.Ref.No " + _DocNo + "')";
                        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", script1, true);
                    }
                }
                txtDocPrFx.Text = "All";
                txtYear.Text = (DateTime.Now.Month >= 4 ? DateTime.Now.ToString("yy") : DateTime.Now.AddYears(-1).ToString("yy"));
                txtDocDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                hdnUserRole.Value = Session["userRole"].ToString().Trim();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch_SelectedIndexChanged(null, null);
                ddlBranch.Enabled = true;
                ddlrecordperpage.SelectedValue = "20";
                fillBranch();
                fillGrid();
                btnSearch.Attributes.Add("onclick", "return validateSearch();");
                txtDocumentNo.Attributes.Add("onblur", "return checkDocNo();");
                txtDocumentNo.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtYear.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txtBranchCode.Attributes.Add("onkeydown", "return false;");
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
        //string result = "";
        //string _userName = Session["userName"].ToString().Trim();
        //string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        //string _docNo = e.CommandArgument.ToString();

        //SqlParameter p1 = new SqlParameter("@documentNo", SqlDbType.VarChar);
        //p1.Value = _docNo;
        //SqlParameter p2 = new SqlParameter("@user", SqlDbType.VarChar);
        //p2.Value = _userName;
        //SqlParameter p3 = new SqlParameter("@uploadeddate", SqlDbType.VarChar);
        //p3.Value = _uploadingDate;

        //string _query = "TF_EXP_DeleteExportBillEntryDetails";
        //TF_DATA objData = new TF_DATA();
        //result = objData.SaveDeleteData(_query, p1, p2, p3);
        //fillGrid();
        //if (result == "deleted")
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);
        //else
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record cannot be deleted as it is realised.');", true);
    }

    protected void GridViewEXPbillEntry_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDocumentNo = new Label();
            Label lblSubDocumentNo = new Label();
            Label lblDocument_Sight_Usance = new Label();
            Label lblDispByDefaltuValue = new Label();
            Label Status = new Label();

            Button btnDelete = new Button();

            lblDocumentNo = (Label)e.Row.FindControl("lblDocumentNo");
            lblSubDocumentNo = (Label)e.Row.FindControl("lblSubDocumentNo");
            lblDispByDefaltuValue = (Label)e.Row.FindControl("lblDispByDefaltuValue");



            Status = (Label)e.Row.FindControl("lblstatus");
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

            string _foreignOrLocal = "";
            if (rbtnForeign.Checked == true)
                _foreignOrLocal = "F";
            else if (rbtnLocal.Checked == true)
                _foreignOrLocal = "L";

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.location='EXP_AddEditApprovedLodgemetForROD_Checker.aspx?mode=edit&DocNo=" + lblDocumentNo.Text.Trim()
                    + "&DocPrFx=" + txtDocPrFx.Text + "&DocSrNo=" + txtDocumentNo.Text + "&DocYear=" + txtYear.Text
                    + "&BranchCode=" + txtBranchCode.Text.Trim() + "&Status=" + Status.Text + "&BranchName=" + ddlBranch.Text.Trim() + "&ForeignOrLocal=" + _foreignOrLocal + "'";
                if (i != 7)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }

    protected void fillGrid()
    {
        string type = "", _foreignOrLocal = "";
        string query = "TF_EXP_GetApproveRODList_Checker";


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
        if (rbAll.Checked == true)
            type = "All";


        if (rbtnForeign.Checked == true)
            _foreignOrLocal = "F";
        else if (rbtnLocal.Checked == true)
            _foreignOrLocal = "L";
        SqlParameter p1 = new SqlParameter("@type", SqlDbType.VarChar);
        p1.Value = type;

        SqlParameter p2 = new SqlParameter("@branch", SqlDbType.VarChar);
        p2.Value = ddlBranch.Text.Trim();

        SqlParameter p3 = new SqlParameter("@year", SqlDbType.VarChar);
        p3.Value = txtYear.Text;

        SqlParameter p4 = new SqlParameter("@search", SqlDbType.VarChar);
        p4.Value = txtSearch.Text;

        SqlParameter p5 = new SqlParameter("@Foreign_Local_Bill", _foreignOrLocal);

        SqlParameter p6 = new SqlParameter("@status", SqlDbType.VarChar);
        if (rdbAll.Checked == true)
        {
            p6.Value = "All";
        }
        else if (rdbPending.Checked == true)
        {
            p6.Value = "C";
        }
        else if (rdbApproved.Checked == true)
        {
            p6.Value = "A";
        }
        else
        {
            p6.Value = "R";
        }
        SqlParameter p7 = new SqlParameter("@date", SqlDbType.VarChar);
        p7.Value = txtDocDate.Text.Trim();

        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(query, p1, p2, p3, p4, p5, p6, p7);

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
            //------------------------------------Added Anand 27-07-2023--------------
            if (rdbAll.Checked == true)
            {
                lblcount.Text = "All Records:-" + _records;
            }
            else if (rdbPending.Checked == true)
            {
                lblcount.Text = "Pending Records:-" + _records;
            }
            else if (rdbApproved.Checked == true)
            {
                lblcount.Text = "Approved Records:-" + _records;
            }
            else
            {
                lblcount.Text = "Reject Records:-" + _records;
            }
            //-----------------------End----------------------------------
        }
        else
        {
            GridViewEXPbillEntry.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            lblcount.Text = "";// Added Anand 27-07-2023
            labelMessage.Visible = true;
        }
    }


    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //TF_DATA objData = new TF_DATA();
        //SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        //p1.Value = ddlBranch.Text.Trim();
        //string _query = "TF_GetBranchDetails_EXP_BillEntry";
        //DataTable dt = objData.getData(_query, p1);

        //if (dt.Rows.Count > 0)
        //{
        //    txtBranchCode.Text = dt.Rows[0]["BranchCode"].ToString().Trim();

        //    //getLastDocNo();
        //}
        //else
        //{
        //    hdnBranchCode.Value = "";
        //    txtDocumentNo.Text = "";
        //}
        fillGrid();
        ddlBranch.Focus();
    }

    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
        fillGrid();
    }

    protected void txtDocDate_TextChanged(object sender, EventArgs e)
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
        string _lastDocNo = obj.SaveDeleteData(_query1, pYear, pDocType, pFL, pBranchCode);
        txtDocumentNo.Text = _lastDocNo;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string _foreignOrLocal = "";
        if (rbtnForeign.Checked == true)
            _foreignOrLocal = "F";
        else if (rbtnLocal.Checked == true)
            _foreignOrLocal = "L";

        Response.Redirect("EXP_AddEditApprovedLodgemetForROD_Maker.aspx?mode=add&DocPrFx=" + txtDocPrFx.Text + "&DocSrNo=" + txtDocumentNo.Text + "&DocYear=" + txtYear.Text
                    + "&BranchCode=" + txtBranchCode.Text.Trim() + "&BranchName=" + ddlBranch.Text.Trim() + "&ForeignOrLocal=" + _foreignOrLocal, true);
    }

   
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

    //-------------------Anand06-07-2023-----------
    protected void rbAll_CheckedChanged(object sender, EventArgs e)
    {
        txtDocPrFx.Text = "All";
        fillGrid();
        rbtbcu.Focus();
    }
    //----------------End--------------
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