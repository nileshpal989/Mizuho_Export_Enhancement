using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class EXP_EXP_ViewExportBillEntry_Checker : System.Web.UI.Page
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
                if (rbDigitExport.Checked == true)
                {
                    fillGridDigiExport();
                    btnapprove.Visible = true;
                    btndelete.Visible = true;
                    //CheckBox chk = (CheckBox)e.Row.FindControl("chkselect");// Anand by 08-09-2023
                    //CheckBox chkall = (CheckBox)e.Row.FindControl("chkall");// Anand by 08-09-2023
                    //chk.Enable = true;
                    //chkall.Enable = true;
                    
                                        
                }
                else
                {
                    btnapprove.Visible = false;
                    btndelete.Visible = false;
                   // fillGrid();
                }
                fillBranch();
                fillGrid();
               
                btndelete.Attributes.Add("onclick", "return Confirm1();");//added by Anand 08-09-2023
                btnapprove.Attributes.Add("onclick", "return Confirm();");//added by Anand 08-09-2023
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
        if (rbDigitExport.Checked == true)
        {
            btnapprove.Visible = true;
            btndelete.Visible = true;
            fillGridDigiExport();
        }
        else
        {
            fillGrid();
        }
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
        if (rbDigitExport.Checked == true)
        {
            btnapprove.Visible = true;
            btndelete.Visible = true;
            fillGridDigiExport();
        }
        else
        {
            fillGrid();
        }
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewEXPbillEntry.PageIndex > 0)
        {
            GridViewEXPbillEntry.PageIndex = GridViewEXPbillEntry.PageIndex - 1;
        }
        if (rbDigitExport.Checked == true)
        {
            btnapprove.Visible = true;
            btndelete.Visible = true;
            fillGridDigiExport();
        }
        else
        {
            fillGrid();
        }
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewEXPbillEntry.PageIndex != GridViewEXPbillEntry.PageCount - 1)
        {
            GridViewEXPbillEntry.PageIndex = GridViewEXPbillEntry.PageIndex + 1;
        }
        if (rbDigitExport.Checked == true)
        {
            btnapprove.Visible = true;
            btndelete.Visible = true;
            fillGridDigiExport();
        }
        else
        {
            fillGrid();
        }
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewEXPbillEntry.PageIndex = GridViewEXPbillEntry.PageCount - 1;
        if (rbDigitExport.Checked == true)
        {
            btnapprove.Visible = true;
            btndelete.Visible = true;
            fillGridDigiExport();
        }
        else
        {
            fillGrid();
        }
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

            Label lblCheckedBy = new Label();

            Button btnDelete = new Button();

            lblDocumentNo = (Label)e.Row.FindControl("lblDocumentNo");
            lblSubDocumentNo = (Label)e.Row.FindControl("lblSubDocumentNo");
            lblDispByDefaltuValue = (Label)e.Row.FindControl("lblDispByDefaltuValue");

            lblCheckedBy = (Label)e.Row.FindControl("lblCheckby");
            string _userName = lblCheckedBy.Text;
            string docno = lblDocumentNo.Text;


            Status = (Label)e.Row.FindControl("lblstatus");
            CheckBox chk = (CheckBox)e.Row.FindControl("chkselect");// Anand by 08-09-2023
            //CheckBox chkall = (CheckBox)e.Row.FindControl("chkall");// Anand by 08-09-2023
            CheckBox HeaderChkAllow = (CheckBox)GridViewEXPbillEntry.HeaderRow.FindControl("chkall");
            if (rbDigitExport != null)
            {

                if (rbDigitExport.Checked == true)
                {
                    chk.Enabled = true;
                    HeaderChkAllow.Enabled = true;


                }
            }
            // chk.Enabled = false;   
            if (Status.Text == "Approved By Checker")
            {
                e.Row.BackColor = System.Drawing.Color.GreenYellow;
                chk.Enabled = false;// Anand by 08-09-2023 
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
                string pageurl = "window.location='EXP_AddEditExportBillEntry_Checker.aspx?mode=edit&DocNo=" + lblDocumentNo.Text.Trim()
                    + "&DocPrFx=" + txtDocPrFx.Text + "&DocSrNo=" + txtDocumentNo.Text + "&DocYear=" + txtYear.Text
                    + "&BranchCode=" + txtBranchCode.Text.Trim() + "&Status=" + Status.Text + "&BranchName=" + ddlBranch.Text.Trim() + "&ForeignOrLocal=" + _foreignOrLocal + "'";

                string alert = "alert('Checker is already checking this transaction.')";

                if (i != 10)
                {
                    cell.Attributes.Add("onclick", pageurl);
                }
                else
                {
                    cell.Style.Add("cursor", "default");
                   
                }
                i++;
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (rbDigitExport.Checked == true)
        {
            btnapprove.Visible = true;
            btndelete.Visible = true;
            fillGridDigiExport();
        }
        else
        {
            fillGrid();
        }
    }

    protected void fillGrid()
    {
        string type = "", _foreignOrLocal = "";
        string query = "TF_EXP_GetExportBillEntryList_Checker";


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
        //if(rbDigitExport.Checked==true)
        //    type="";

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
            p6.Value = "P";
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
        if (rbDigitExport.Checked == true)
        {
            btnapprove.Visible = true;
            btndelete.Visible = true;
            fillGridDigiExport();
        }
        else
        {
            fillGrid();
        }
        ddlBranch.Focus();
    }

    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
        if (rbDigitExport.Checked == true)
        {
            btnapprove.Visible = true;
            btndelete.Visible = true;
            fillGridDigiExport();
        }
        else
        {
            fillGrid();
        }
    }

    protected void txtDocDate_TextChanged(object sender, EventArgs e)
    {
        if (rbDigitExport.Checked == true)
        {
            btnapprove.Visible = true;
            btndelete.Visible = true;
            fillGridDigiExport();
        }
        else
        {
            fillGrid();
        }
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
        btnapprove.Visible = false;
        btndelete.Visible = false;
        txtDocPrFx.Text = "BLA";
        fillGrid();
        rbtbla.Focus();
    }
    protected void rbtblu_CheckedChanged(object sender, EventArgs e)
    {
        btnapprove.Visible = false;
        btndelete.Visible = false;
        txtDocPrFx.Text = "BLU";
        fillGrid();
        rbtblu.Focus();
    }
    protected void rbtbba_CheckedChanged(object sender, EventArgs e)
    {
        btnapprove.Visible = false;
        btndelete.Visible = false;
        txtDocPrFx.Text = "BBA";
        fillGrid();
        rbtbba.Focus();
    }
    protected void rbtbbu_CheckedChanged(object sender, EventArgs e)
    {
        btnapprove.Visible = false;
        btndelete.Visible = false;
        txtDocPrFx.Text = "BBU";
        fillGrid();
        rbtbbu.Focus();
    }

    protected void rbtbcs_CheckedChanged(object sender, EventArgs e)
    {
        btnapprove.Visible = false;
        btndelete.Visible = false;
        txtDocPrFx.Text = "BCA";
        fillGrid();
        rbtbca.Focus();
    }
    protected void rbtbcu_CheckedChanged(object sender, EventArgs e)
    {
        btnapprove.Visible = false;
        btndelete.Visible = false;
        txtDocPrFx.Text = "BCU";
        fillGrid();
        rbtbcu.Focus();
    }
    protected void rbtIBD_CheckedChanged(object sender, EventArgs e)
    {
        btnapprove.Visible = false;
        btndelete.Visible = false;
        txtDocPrFx.Text = "IBD";
        fillGrid();
        rbtIBD.Focus();
    }

    protected void rbtBEB_CheckedChanged(object sender, EventArgs e)
    {
        btnapprove.Visible = false;
        btndelete.Visible = false;
        txtDocPrFx.Text = "EB";
        fillGrid();
        rbtBEB.Focus();
    }

    //-------------------Anand06-07-2023-----------
    protected void rbAll_CheckedChanged(object sender, EventArgs e)
    {
        btnapprove.Visible = false;
        btndelete.Visible = false;
        txtDocPrFx.Text = "All";
        //fillGridDigiExport();
        fillGrid();
        rbtbcu.Focus();
    }
    protected void rbDigitExport_CheckedChanged(object sender, EventArgs e)
    {

        btnapprove.Visible = true;
        btndelete.Visible = true;
        fillGridDigiExport();
   
        rbtbcu.Focus();
    }
    //----------------End--------------
    protected void rbtnForeign_CheckedChanged(object sender, EventArgs e)
    {
        if (rbDigitExport.Checked == true)
        {
            btnapprove.Visible = true;
            btndelete.Visible = true;
            fillGridDigiExport();
        }
        else
        {
            fillGrid();
        }
        rbtnForeign.Focus();
    }
    protected void rbtnLocal_CheckedChanged(object sender, EventArgs e)
    {
        if (rbDigitExport.Checked == true)
        {
            btnapprove.Visible = true;
            btndelete.Visible = true;
            fillGridDigiExport();
        }
        else
        {
            fillGrid();
        }
       
        rbtnLocal.Focus();
    }

    protected void rdbAll_CheckedChanged(object sender, EventArgs e)
    {
        if (rbDigitExport.Checked == true)
        {
            btnapprove.Visible = true;
            btndelete.Visible = true;
            fillGridDigiExport();
        }
        else
        {
            fillGrid();
        }
    }
    protected void rdbApproved_CheckedChanged(object sender, EventArgs e)
    {
        if (rbDigitExport.Checked == true)
        {
            btnapprove.Visible = true;
            btndelete.Visible = true;
            fillGridDigiExport();
        }
        else
        {
            fillGrid();
        }
        
    }
    protected void rdbRejected_CheckedChanged(object sender, EventArgs e)
    {
        if (rbDigitExport.Checked == true)
        {
            btnapprove.Visible = true;
            btndelete.Visible = true;
            fillGridDigiExport();
        }
        else
        {
            fillGrid();
        }
    }
    protected void rdbPending_CheckedChanged(object sender, EventArgs e)
    {
        if (rbDigitExport.Checked == true)
        {
            btnapprove.Visible = true;
            btndelete.Visible = true;
            fillGridDigiExport();
        }

        else
        {
            fillGrid();
        }
    }

    protected void fillGridDigiExport()
    {
        string DDispIndc = "";
        string DispByDefaltuValue = "";
        // string type = "",
        string _foreignOrLocal = "";
        string query = "TF_EXP_GetDIGIExportBillEntryList_Checker";


        //if (rbtbla.Checked == true)
        //    type = "BLA";
        //if (rbtblu.Checked == true)
        //    type = "BLU";
        //if (rbtbba.Checked == true)
        //    type = "BBA";
        //if (rbtbbu.Checked == true)
        //    type = "BBU";
        //if (rbtbca.Checked == true)
        //    type = "BCA";
        //if (rbtbcu.Checked == true)
        //    type = "BCU";
        //if (rbtIBD.Checked == true)
        //    type = "IBD";
        //if (rbtBEB.Checked == true)
        //    type = "EB";
        //if (rbAll.Checked == true)
        //    type = "All";
        if (rbDigitExport.Checked == true)
        {
            DDispIndc = "Dispatched directly by exporter";
            DispByDefaltuValue = "Digi-Export";
        }


        if (rbtnForeign.Checked == true)
            _foreignOrLocal = "F";
        else if (rbtnLocal.Checked == true)
            _foreignOrLocal = "L";
        //SqlParameter p1 = new SqlParameter("@type", SqlDbType.VarChar);
        //p1.Value = type;



        SqlParameter p1 = new SqlParameter("@branch", SqlDbType.VarChar);
        p1.Value = ddlBranch.Text.Trim();

        SqlParameter p2 = new SqlParameter("@year", SqlDbType.VarChar);
        p2.Value = txtYear.Text;

        SqlParameter p3 = new SqlParameter("@search", SqlDbType.VarChar);
        p3.Value = txtSearch.Text;

        SqlParameter p4 = new SqlParameter("@Foreign_Local_Bill", _foreignOrLocal);

        SqlParameter p5 = new SqlParameter("@status", SqlDbType.VarChar);
        if (rdbAll.Checked == true)
        {
            p5.Value = "All";
        }
        else if (rdbPending.Checked == true)
        {
            p5.Value = "C";
           // p5.Value = "I";
        }
        else if (rdbApproved.Checked == true)
        {
            p5.Value = "A";
        }
        else
        {
            p5.Value = "R";
        }
        SqlParameter p6 = new SqlParameter("@date", SqlDbType.VarChar);
        p6.Value = txtDocDate.Text.Trim();
        SqlParameter p7 = new SqlParameter("@DispIndc", SqlDbType.VarChar);
        p7.Value = DDispIndc.Trim();
        SqlParameter p8 = new SqlParameter("@DispByDefaltuValue", SqlDbType.VarChar);
        p8.Value = DispByDefaltuValue.Trim();

        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(query, p1, p2, p3, p4, p5, p6, p7, p8);

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
    protected void chkall_CheckedChanged(object sender, EventArgs e)
    {
        Label lblDocumentNo = new Label();
        foreach (GridViewRow gvrow in GridViewEXPbillEntry.Rows)
        {
            if (gvrow.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk_ = (CheckBox)gvrow.FindControl("chkall");
                CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
                if (chk.Checked)
                {
                    //lblDocumentNo = (Label)e.Row.FindControl("lblDocumentNo");
                    lblDocumentNo = (gvrow.Cells[0].FindControl("lblDocumentNo") as Label);
                    string result;
                    SqlParameter p1 = new SqlParameter("@documnetno", lblDocumentNo.Text.Trim());
                    TF_DATA objData = new TF_DATA();
                    result = objData.SaveDeleteData("TF_Export_Digi_EnableDisableChkGrid", p1);
                    if (result == "Exists")
                    {
                        chk.Enabled = false;
                        chk.Checked = false;
                    }
                    else
                    {
                         btnapprove.Enabled = true;
                    }
                }
                else
                {
                       //CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
                    lblDocumentNo = (gvrow.Cells[0].FindControl("lblDocumentNo") as Label);
                    string result;
                    SqlParameter p1 = new SqlParameter("@documnetno", lblDocumentNo.Text.Trim());
                    TF_DATA objData = new TF_DATA();
                    result = objData.SaveDeleteData("TF_Export_Digi_EnableDisableChkGrid", p1);
                    if (result == "Exists")
                    {
                        chk.Enabled = false;
                        chk.Checked = false;
                    }
                    else
                    {
                        btnapprove.Enabled = true;
                    }
                }
            }
        }
    }

    protected void btnapprove_Click(object sender, EventArgs e)
    {
        int count = 0;
        string count_ = "";
        foreach (GridViewRow gvrow_ in GridViewEXPbillEntry.Rows)
        {
            if (gvrow_.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)gvrow_.FindControl("chkSelect");
                if (chk != null & chk.Checked)
                {
                    count++;
                }
                count_ = count.ToString();
            }
        }
        if (count != 0)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                string data = "";
                Label lblDocumentNo = new Label();
                string _result;
                string result_;
                
                TF_DATA objData = new TF_DATA();
                foreach (GridViewRow gvrow in GridViewEXPbillEntry.Rows)
                {
                    if (gvrow.RowType == DataControlRowType.DataRow)
                    {
                        lblDocumentNo = (gvrow.Cells[0].FindControl("lblDocumentNo") as Label);
                       // ormstatus = (gvrow.Cells[0].FindControl("lblormstatus") as Label);
                        CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");

                        if (chk != null & chk.Checked)
                        {
                            string storid = lblDocumentNo.Text;
                           // string storname = ormstatus.Text;
                            data = data + storid + " ,  "  + "<br>";
                            string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                            SqlParameter p1 = new SqlParameter("@Addedby", Session["userName"].ToString());
                            SqlParameter p2 = new SqlParameter("@Addeddate", _uploadingDate);
                            SqlParameter p3 = new SqlParameter("@status", "A");
                            SqlParameter p4 = new SqlParameter("@documnetno", lblDocumentNo.Text);
                           // SqlParameter p5 = new SqlParameter("@ormstatus", ormstatus.Text);
                            SqlParameter p5 = new SqlParameter("@bulkflag", "B");
                            _result = objData.SaveDeleteData("TF_Export_Digi_UpdateStatusGrid", p1, p2, p3, p4, p5);

                            if (_result == "Updated")
                            {
                                string script1 = "alert('Digi-Export" + count_ + " Record Approved.')";
                                ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", script1, true);
                                fillGridDigiExport();
                            }

                        }
                    }
                }
                //lbl.Text = data;
                //JsonCreation(uniquetxid, count_);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Row')", true);
        }
    }

    protected void btndelete_Click(object sender, EventArgs e)
    {
        int count = 0;
        string count_ = "";
        foreach (GridViewRow gvrow_ in GridViewEXPbillEntry.Rows)
        {
            if (gvrow_.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)gvrow_.FindControl("chkSelect");
                if (chk != null & chk.Checked)
                {
                    count++;
                }
                count_ = count.ToString();
            }
        }
        if (count != 0)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                string data = "";
                Label lblDocumentNo = new Label();
                string _result;
                string result_;
                TF_DATA objData = new TF_DATA();
                foreach (GridViewRow gvrow in GridViewEXPbillEntry.Rows)
                {
                    if (gvrow.RowType == DataControlRowType.DataRow)
                    {
                        lblDocumentNo = (gvrow.Cells[0].FindControl("lblDocumentNo") as Label);
                        // ormstatus = (gvrow.Cells[0].FindControl("lblormstatus") as Label);
                        CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
                        string _userName = Session["userName"].ToString().Trim();// Added by Anand 27-12-2023
                        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");// Added by Anand 27-12-2023
                        if (chk != null & chk.Checked)
                        {
                            string storid = lblDocumentNo.Text;
                            data = data + storid + " ,  " + "<br>";
                            SqlParameter p1 = new SqlParameter("@documnetno", lblDocumentNo.Text);
                            SqlParameter p2 = new SqlParameter("@user", SqlDbType.VarChar);// Added by Anand 27-12-2023
                            p2.Value = _userName;
                            SqlParameter p3 = new SqlParameter("@uploadeddate", SqlDbType.VarChar);// Added by Anand 27-12-2023
                            p3.Value = _uploadingDate;
                            _result = objData.SaveDeleteData("TF_Export_Digi_DeleteStatusGrid", p1, p2, p3);

                            if (_result == "Delete")
                            {
                                string script1 = "alert('Digi-Export " + count_ + " Record Delete.')";
                                ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", script1, true);
                                fillGridDigiExport();
                            }

                        }
                    }
                }
                //lbl.Text = data;
                //JsonCreation(uniquetxid, count_);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Row')", true);
        }
    }
}