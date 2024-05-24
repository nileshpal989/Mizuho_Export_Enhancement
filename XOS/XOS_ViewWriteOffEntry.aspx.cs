using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class XOS_XOS_ViewWriteOffEntry : System.Web.UI.Page
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
                txtYear.Text = DateTime.Now.Year.ToString().Substring(2, 2);
                ddlrecordperpage.SelectedValue = "20";                
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
                //btnAdd.Attributes.Add("onclick", "return validate();");
                btnSearch.Attributes.Add("onclick", "return validateSearch();");
                //txtYear.Attributes.Add("onblur", "return checkYear();");
                txtYear.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtYear.Attributes.Add("onkeydown", "return validate_Number(event);");
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
            txtBranchCode.Text = dt.Rows[0]["BranchCode"].ToString().Trim();
            getLastDocNo();
        }
        else
        {
            txtBranchCode.Text = "";
        }
        fillGrid();
        ddlBranch.Focus();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }

    protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }

    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewXOSWriteOff.PageIndex = 0;
        fillGrid();
    }

    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewXOSWriteOff.PageIndex > 0)
        {
            GridViewXOSWriteOff.PageIndex = GridViewXOSWriteOff.PageIndex - 1;
        }
        fillGrid();
    }

    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewXOSWriteOff.PageIndex != GridViewXOSWriteOff.PageCount - 1)
        {
            GridViewXOSWriteOff.PageIndex = GridViewXOSWriteOff.PageIndex + 1;
        }
        fillGrid();
    }

    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewXOSWriteOff.PageIndex = GridViewXOSWriteOff.PageCount - 1;
        fillGrid();
    }

    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
        fillGrid();
    }

    protected void fillGrid()
    {
        getLastDocNo();
        string type = "";
        string query = "TF_EXP_XOS_GetOutstandingList_WriteOff";
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
        //if (rbtIBD.Checked == true)
        //    type = "IBD";
        //if (rbtLBC.Checked == true)
        //    type = "LBC";
        //if (rbtBEB.Checked == true)
        //    type = "BEB";

        SqlParameter p1 = new SqlParameter("@DocType", SqlDbType.VarChar);
        p1.Value = type;

        SqlParameter p2 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p2.Value = ddlBranch.Text;

        SqlParameter p3 = new SqlParameter("@DocYear", SqlDbType.VarChar);
        p3.Value = txtYear.Text;

        SqlParameter p4 = new SqlParameter("@search", SqlDbType.VarChar);
        p4.Value = txtSearch.Text;

        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(query, p1, p2, p3, p4);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewXOSWriteOff.PageSize = _pageSize;
            GridViewXOSWriteOff.DataSource = dt.DefaultView;
            GridViewXOSWriteOff.DataBind();
            GridViewXOSWriteOff.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewXOSWriteOff.Visible = false;
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
            if (GridViewXOSWriteOff.PageCount != GridViewXOSWriteOff.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewXOSWriteOff.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewXOSWriteOff.PageIndex + 1) + " of " + GridViewXOSWriteOff.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewXOSWriteOff.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewXOSWriteOff.PageIndex != (GridViewXOSWriteOff.PageCount - 1))
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
        hdnBillDocumentType.Value = "BLA";
        fillGrid();
        rbtbla.Focus();
    }
    protected void rbtblu_CheckedChanged(object sender, EventArgs e)
    {
        hdnBillDocumentType.Value = "BLU";
        fillGrid();
        rbtblu.Focus();
    }
    protected void rbtbba_CheckedChanged(object sender, EventArgs e)
    {
        hdnBillDocumentType.Value = "BBA";
        fillGrid();
        rbtbba.Focus();
    }
    protected void rbtbbu_CheckedChanged(object sender, EventArgs e)
    {
        hdnBillDocumentType.Value = "BBU";
        fillGrid();
        rbtbbu.Focus();
    }

    protected void rbtbcs_CheckedChanged(object sender, EventArgs e)
    {
        hdnBillDocumentType.Value = "BCA";
        fillGrid();
        rbtbca.Focus();
    }
    protected void rbtbcu_CheckedChanged(object sender, EventArgs e)
    {
        hdnBillDocumentType.Value = "BCU";
        fillGrid();
        rbtbcu.Focus();
    }
    //protected void rbtIBD_CheckedChanged(object sender, EventArgs e)
    //{
    //    hdnBillDocumentType.Value = "IBD";
    //    fillGrid();
    //    rbtIBD.Focus();
    //}
    //protected void rbtLBC_CheckedChanged(object sender, EventArgs e)
    //{
    //    hdnBillDocumentType.Value = "LBC";
    //    fillGrid();
    //    rbtLBC.Focus();
    //}
    //protected void rbtBEB_CheckedChanged(object sender, EventArgs e)
    //{
    //    hdnBillDocumentType.Value = "BEB";
    //    fillGrid();
    //    rbtBEB.Focus();
    //}

    protected void GridViewXOSWriteOff_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string docno = "";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblBillNo = new Label();
            Label lbldocno = new Label();

            lblBillNo = (Label)e.Row.FindControl("lblBillNo");
            lbldocno = (Label)e.Row.FindControl("lblDocumentNo");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl;
                if (lbldocno.Text == "")
                {
                    docno = txtDocPrFx.Text + "/" + txtBranchCode.Text + "/" + txtYear.Text + txtDocumentNo.Text;
                }
                else
                    docno = lbldocno.Text;
                pageurl = "window.location='XOS_AddEditWriteOffBillEntry.aspx?DocNo=" + lblBillNo.Text.Trim()
                + "&DocumentNo=" + docno + "&BranchCode=" + txtBranchCode.Text + "&year=" + txtYear.Text + "'";
                if (i != 5)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;

            }
            //mode=edit&
        }
    }

    private void getLastDocNo()
    {

        TF_DATA obj = new TF_DATA();
        SqlParameter pYear = new SqlParameter("@year", SqlDbType.VarChar);
        pYear.Value = txtYear.Text.Trim();
        SqlParameter pDocType = new SqlParameter("@docType", SqlDbType.VarChar);
        pDocType.Value = txtDocPrFx.Text.Trim();

        string _query1 = "TF_GetLastDocNo_XOS";
        string _lastDocNo = obj.SaveDeleteData(_query1, pYear, pDocType);
        txtDocumentNo.Text = _lastDocNo;
    }

}