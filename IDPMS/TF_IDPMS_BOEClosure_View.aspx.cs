using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class IDPMS_TF_IDPMS_BOEClosure_View : System.Web.UI.Page
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
                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;
                //txtYear.Text = System.DateTime.Now.ToString("yyyy");
                ddlrecordperpage.SelectedValue = "10";
                //fillBranch();
                //ddlbranch.SelectedValue = Session["userLBCode"].ToString();

                //ddlbranch.Enabled = false;
                hdnUserRole.Value = Session["userRole"].ToString().Trim();
                fillGrid();
                //if (Request.QueryString["result"] != null)
                //{
                //    if (Request.QueryString["result"].Trim() == "added")
                //    {
                //        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added.');", true);
                //    }
                //    else
                //        if (Request.QueryString["result"].Trim() == "updated")
                //        {
                //            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
                //        }
                //}
            }

            else
            {
            }
        }
    }
    //protected void fillBranch()
    //{
    //    TF_DATA objData = new TF_DATA();
    //    SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
    //    p1.Value = "";
    //    string _query = "TF_GetBranchDetails";
    //    DataTable dt = objData.getData(_query, p1);
    //    ddlbranch.Items.Clear();
    //    //ListItem li = new ListItem();
    //    //li.Value = "---Select---";
    //    if (dt.Rows.Count > 0)
    //    {
    //        //li.Text = "---Select---";

    //        ddlbranch.DataSource = dt.DefaultView;
    //        ddlbranch.DataTextField = "BranchName";
    //        ddlbranch.DataValueField = "AuthorizedDealerCode";
    //        ddlbranch.DataBind();

    //    }
    //}

    //    else
    //    //    li.Text = "No record(s) found";

    //    //ddlbranch.Items.Insert(0, li);
    //    //ddlbranch.Items.Insert(1, li01);
    //}
    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        //ListItem li = new ListItem();
        //li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            //li.Text = "---Select---";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else { }
        //    li.Text = "No record(s) found";

        //ddlBranch.Items.Insert(0, li);
    }
    protected void fillGrid()
    {
        //string Branch = ddlbranch.SelectedValue;
        string _query = "TF_IDPMS_BillClosure_GetDetails";
        SqlParameter p1 = new SqlParameter("@search", "");
        SqlParameter p2 = new SqlParameter("@branch", ddlBranch.SelectedValue.ToString());
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewInwData.PageSize = _pageSize;
            GridViewInwData.DataSource = dt.DefaultView;
            GridViewInwData.DataBind();
            GridViewInwData.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            lblMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewInwData.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            lblMessage.Text = "No record(s) found.";
            lblMessage.Visible = true;
        }
    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //fillGrid();

        string search = txtSearch.Text.Trim();

        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;
        SqlParameter p2 = new SqlParameter("@branch", ddlBranch.SelectedValue.ToString());
        string _query = "TF_IDPMS_BillClosure_GetDetails";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewInwData.PageSize = _pageSize;
            GridViewInwData.DataSource = dt.DefaultView;
            GridViewInwData.DataBind();
            GridViewInwData.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            lblMessage.Visible = false;
            //pagination(_records, _pageSize);
        }
        else
        {
            GridViewInwData.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            lblMessage.Text = "No record(s) found.";
            lblMessage.Visible = true;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("IDPMS_BOEClosure.aspx?mode=add", true);
    }
    protected void GridViewInwData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDocNo = new Label();
            Label lblBillNo = new Label();
            Label lblInvSrNo = new Label();
            Label lblInvNo = new Label();
            Button btnDelete = new Button();
            //lblDocNo = (Label)e.Row.FindControl("lblDocNo");
            lblBillNo = (Label)e.Row.FindControl("lblbillno");
            lblInvSrNo = (Label)e.Row.FindControl("lblInvoicSrNo"); ;
            lblInvNo = (Label)e.Row.FindControl("lblInvoiceNo");
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
                if (i != 9)
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void GridViewInwData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
        string result1 = "";
        string[] values_p;
        string _docNo = "", _billNo = "", _InvNo = "", _InvSrNo = "";
        string str = e.CommandArgument.ToString();
        if (str != "")
        {
            char[] splitchar = { ';' };
            values_p = str.Split(splitchar);
            //_docNo = values_p[0].ToString();
            _billNo = values_p[0].ToString();
            _InvNo = values_p[1].ToString();
            _InvSrNo = values_p[2].ToString();
        }


        TF_DATA objData = new TF_DATA();

        // SqlParameter p1 = new SqlParameter("@RefNo", _docNo);
        SqlParameter p2 = new SqlParameter("@BillNo", _billNo);
        SqlParameter p3 = new SqlParameter("@InvNo", _InvNo);
        SqlParameter p4 = new SqlParameter("@InvSrNo", _InvSrNo);

        string _query1 = "TF_IDPMS_Get_AccountNo";
        result1 = objData.SaveDeleteData(_query1, p2, p3, p4);

        string _query = "TF_IDPMS_BOE_Closure_Delete";
        result = objData.SaveDeleteData(_query, p2, p3, p4);
        fillGrid();
        if (result == "deleted")
        {

            string oldvalue = "Bill of Entry No:" + _billNo + ";Invoice No:" + _InvNo + ";Invoice Sr No:" + _InvSrNo;

            #region AUDIT TRAIL LOGIC
            SqlParameter Q1 = new SqlParameter("@Adcode", ddlBranch.SelectedValue.ToString());
            SqlParameter Q2 = new SqlParameter("@OldValues", oldvalue);
            SqlParameter Q3 = new SqlParameter("@NewValues", "");
            SqlParameter Q4 = new SqlParameter("@CustAcNo", result1);
            SqlParameter Q5 = new SqlParameter("@DocumentNo", "");
            SqlParameter Q6 = new SqlParameter("@DocumentDate", "");
            SqlParameter Q7 = new SqlParameter("@Mode", "D");
            SqlParameter Q8 = new SqlParameter("@ModifiedBy", Session["userName"].ToString());
            SqlParameter Q9 = new SqlParameter("@MenuName", "Bill Of Entry - Closure Data Entry");

            result = objData.SaveDeleteData("TF_IDPMS_Audit_Trail_Add", Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9);

            #endregion
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);

        }
    }


    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewInwData.PageIndex = 0;
        fillGrid();
    }

    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewInwData.PageIndex > 0)
        {
            GridViewInwData.PageIndex = GridViewInwData.PageIndex - 1;
        }
        fillGrid();
    }

    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewInwData.PageIndex != GridViewInwData.PageCount - 1)
        {
            GridViewInwData.PageIndex = GridViewInwData.PageIndex + 1;
        }
        fillGrid();
    }

    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewInwData.PageIndex = GridViewInwData.PageCount - 1;
        fillGrid();
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
            if (GridViewInwData.PageCount != GridViewInwData.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewInwData.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewInwData.PageIndex + 1) + " of " + GridViewInwData.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewInwData.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewInwData.PageIndex != (GridViewInwData.PageCount - 1))
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
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    //protected void btnShow_Click(object sender, EventArgs e)
    //{
    //    mp1.Show();
    //    btnClose.Focus();
    //}
    protected void btnCan_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_IDPMS_BOEClosure_Can.aspx?mode=add", true);
    }
}