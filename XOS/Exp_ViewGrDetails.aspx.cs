using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EXP_Exp_ViewGrDetails : System.Web.UI.Page
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
                if (rbtbla.Checked == true)
                    hdnBillDocumentType.Value = "BLA";
                
                fillGrid();
                

                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].Trim() == "Saved")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Saved.');", true);
                    }
                    else
                        if (Request.QueryString["result"].Trim() == "updated")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
                        }
                }
                //txtSearch.Attributes.Add("onkeypress", "return submitForm(event);");
                //btnAdd.Attributes.Add("onclick", "return validate();");
                btnSearch.Attributes.Add("onclick", "return validateSearch();");
                //txtYear.Attributes.Add("onblur", "return checkYear();");
                //txtDocumentNo.Attributes.Add("onblur", "return checkDocNo();");
                //txtDocumentNo.Attributes.Add("onkeydown", "return validate_Number(event);");
                txtYear.Attributes.Add("onkeydown", "return validate_Number(event);");

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

        ////string _query = "TF_EXP_DeleteExportBillEntryDetails";
        //TF_DATA objData = new TF_DATA();
        //result = objData.SaveDeleteData(_query, p1,p2,p3);
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

            Label lblBillAmount = new Label();
            Label lblCurrency = new Label();



            Button btnDelete = new Button();

            lblDocumentNo = (Label)e.Row.FindControl("lblDocumentNo");
            lblSubDocumentNo = (Label)e.Row.FindControl("lblSubDocumentNo");
            lblDocument_Sight_Usance = (Label)e.Row.FindControl("lblDocument_Sight_Usance");


            lblCurrency = (Label)e.Row.FindControl("lblCurrency");


            lblBillAmount = (Label)e.Row.FindControl("lblBillAmount");

            //btnDelete = (Button)e.Row.FindControl("btnDelete");

            //btnDelete.Enabled = true;
            //btnDelete.CssClass = "deleteButton";
            //if (hdnUserRole.Value == "Supervisor")
            //    btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");
            //else
            //    btnDelete.Attributes.Add("onclick", "alert('Only Supervisor can delete all records.');return false;");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                //string pageurl = "window.location='EXP_AddEditExportBillEntry.aspx?mode=edit&DocNo=" + lblDocumentNo.Text.Trim()
                //    + "&DocPrFx=" + txtDocPrFx.Text + "&DocSrNo=" + txtDocumentNo.Text + "&DocYear=" + txtYear.Text
                //    + "&BranchCode=" + txtBranchCode.Text.Trim() + "&BranchName=" + ddlBranch.Text.Trim() + "'";

                var DocNo = lblDocumentNo.Text.Trim();



                string pageurl = "window.location='AddEditGRCustoms.aspx?DocNo=" + lblDocumentNo.Text.Trim() + "&DocType=" + hdnBillDocumentType.Value + "&Bill_Amt=" + lblBillAmount.Text.Trim() + "&BranchName=" + ddlBranch.Text.Trim() + "&Currency=" + lblCurrency.Text.Trim() + "'";
                
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
        //getLastDocNo();
        string search = txtSearch.Text.Trim(), _foreignOrLocal = "";
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        SqlParameter p2 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p2.Value = ddlBranch.Text.Trim();
        SqlParameter p3 = new SqlParameter("@DocType", SqlDbType.VarChar);
        p3.Value = hdnBillDocumentType.Value;
        SqlParameter p4 = new SqlParameter("@year", SqlDbType.VarChar);
        p4.Value = txtYear.Text.Trim();

        SqlParameter p5 = new SqlParameter("@Foreign_Local_Bill", SqlDbType.VarChar);

        if (rbtnForeign.Checked == true)
            _foreignOrLocal = "F";
        else if (rbtnLocal.Checked == true)
            _foreignOrLocal = "L";

        p5.Value = _foreignOrLocal;

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
            //txtBranchCode.Text = dt.Rows[0]["BranchCode"].ToString().Trim();

            //getLastDocNo();
        }
        else
        {
            hdnBranchCode.Value = "";
            //txtDocumentNo.Text = "";
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

    //private void getLastDocNo()
    //{

    //    TF_DATA obj = new TF_DATA();
    //    SqlParameter pYear = new SqlParameter("@year", SqlDbType.VarChar);
    //    pYear.Value = txtYear.Text.Trim();
    //    SqlParameter pDocType = new SqlParameter("@docType", SqlDbType.VarChar);
    //    pDocType.Value = txtDocPrFx.Text.Trim();

    //    string _query1 = "TF_GetLastDocNo_EXP";
    //    string _lastDocNo = obj.SaveDeleteData(_query1, pYear, pDocType);
    //    txtDocumentNo.Text = _lastDocNo;
    //}


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
}
