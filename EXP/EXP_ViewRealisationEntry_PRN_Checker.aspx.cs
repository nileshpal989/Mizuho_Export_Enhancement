using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EXP_EXP_ViewRealisationEntry_PRN_Checker : System.Web.UI.Page
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
                        //---------------------Create by Anand 02-09-2023-----------------------
                        //TF_DATA objserverName = new TF_DATA();
                        //string _serverName = objserverName.GetServerName();
                        //string path = "file://" + _serverName + "/Mizuho_Export_Enhancement/TF_GeneratedFiles/Export/";
                        //string link = "/Mizuho_Export_Enhancement/TF_GeneratedFiles/Export/";
                        //lblLink.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
                        string script1 = "alert('Record Approved with Trans.Ref.No " + _DocNo + "')"; //"and Files Created Successfully on " + path + "')";
                        //-------------------End----------------------------------------------
                        //string script1 = "alert('Record Approved with Trans.Ref.No " + _DocNo + "')";
                        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", script1, true);
                    }
                    else if (Request.QueryString["AR"].Trim() == "R")
                    {
                        string script1 = "alert('Record Rejected with Trans.Ref.No " + _DocNo + "')";
                        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", script1, true);
                    }
                }
                txtYear.Text = (DateTime.Now.Month >= 4 ? DateTime.Now.ToString("yy") : DateTime.Now.AddYears(-1).ToString("yy"));

                txtDocPrFx.Value = "BLA";
                ddlrecordperpage.SelectedValue = "20";
                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch_SelectedIndexChanged(null, null);
                ddlBranch.Enabled = true;
                fillGrid();
                btnAdd.Attributes.Add("onclick", "return validate();");
                btnSearch.Attributes.Add("onclick", "return validateSearch();");
                txtYear.Attributes.Add("onkeydown", "return validate_Number(event);");                
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
    }
    protected void GridViewEXPDocRealised_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDocumentNo = new Label();
            Label lblSrNo = new Label();            
            Label Status = new Label();
            Status = (Label)e.Row.FindControl("lblStatus");
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

            lblDocumentNo = (Label)e.Row.FindControl("lblDocumentNo");
            lblSrNo = (Label)e.Row.FindControl("lblSrNo");           
            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.location='EXP_AddEditFullRealisationPRN_Checker.aspx?mode=edit&DocNo=" + lblDocumentNo.Text.Trim()
                    + "&DocPrFx=" + txtDocPrFx.Value + "&BranchCode=" + hdnBranchCode.Value.Trim() + "&Status=" + Status.Text + "&BranchName=" + ddlBranch.Text + "&SrNo=" + lblSrNo.Text.Trim() + "'";
                if (i != 9)
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
        Response.Redirect("EXP_AddEditFullRealisation_Checker.aspx?mode=add&DocPrFx=" + txtDocPrFx.Value + "&BranchCode=" + hdnBranchCode.Value.Trim() + "&BranchName=" + ddlBranch.Text + "&year=" + year, true);
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
        string query = "TF_EXPGetRealisationEntryListNew_PendingPRN";


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
        if (rbAll.Checked == true)
            type = "All";

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

        //SqlParameter p6 = new SqlParameter("@page", SqlDbType.VarChar);
        //p6.Value = "Checker";   // comment by Anand 28-06-2023

        SqlParameter p7 = new SqlParameter("@status", SqlDbType.VarChar);
        if (rdbAll.Checked == true)
        {
            p7.Value = "All";
        }
        else if (rdbApproved.Checked == true)
        {
            p7.Value = "A";
        }
        else if (rdbRejected.Checked == true)
        {
            p7.Value = "R";
        }
        else if (rdbPending.Checked == true)
        {
            p7.Value = "C";
        }
        else
        {
            p7.Value = "R";
        }
        SqlParameter p8 = new SqlParameter("@Docdate", SqlDbType.VarChar);
        p8.Value = txtDocDate.Text.Trim();

        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(query, p1, p2, p3, p4, p5,p7,p8);
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

    protected void rbAll_CheckedChanged(object sender, EventArgs e)
    {
        txtDocPrFx.Value = "All";
        fillGrid();
        rbAll.Focus();
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
    //----------------- create by Anand 28-06-2023------------------
    protected void rdbPending_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
    }

    //--------------End----------------------
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
    protected void txtLodgementDate_TextChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void txtDocDate_TextChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
}