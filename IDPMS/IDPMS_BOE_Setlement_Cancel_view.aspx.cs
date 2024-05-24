using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class IDPMS_IDPMS_BOE_Setlement_Cancel_view : System.Web.UI.Page
{
    string hdnUserRole = "";
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
                //txtYear.Text = System.DateTime.Now.ToString("yyyy");
                ddlrecordperpage.SelectedValue = "20";
                fillBranch();
                ddlbranch.SelectedValue = Session["userADCode"].ToString();

                ddlbranch.Enabled = false;

                fillGrid();
                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].Trim() == "added")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Payment Settlement Cancellation Done.');", true);
                    }
                    
                }
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
        ddlbranch.Items.Clear();
        //ListItem li = new ListItem();
        //li.Value = "---Select---";
        if (dt.Rows.Count > 0)
        {
            //li.Text = "---Select---";

            ddlbranch.DataSource = dt.DefaultView;
            ddlbranch.DataTextField = "BranchName";
            ddlbranch.DataValueField = "AuthorizedDealerCode";
            ddlbranch.DataBind();

        }
    }

    //else
    //    li.Text = "No record(s) found";

    //ddlbranch.Items.Insert(0, li);
    //ddlbranch.Items.Insert(1, li01);
    //}

    protected void fillGrid()
    {
        //string Branch = ddlbranch.SelectedValue;
        string _query = "TF_IDPMS_ImportlBillCreated_GetDetails";
        SqlParameter p1 = new SqlParameter("@search", "");
        SqlParameter p2 = new SqlParameter("@Branch", Session["userADCode"].ToString());
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
        string _query = "TF_IDPMS_ImportlBillCreated_GetDetails";
        SqlParameter p2 = new SqlParameter("@Branch", Session["userADCode"].ToString());
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
    protected void GridViewInwData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblPayRefNo = new Label();
            Label lblDocDate = new Label();
            Button btnDelete = new Button();
            lblPayRefNo = (Label)e.Row.FindControl("lblPayRefNo");
            //btnDelete = (Button)e.Row.FindControl("btnDelete");
            //btnDelete.Enabled = true;
            //btnDelete.CssClass = "deleteButton";

            //hdnUserRole = Session["userRole"].ToString().Trim();

            //if (hdnUserRole == "OIC")
            //    btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");
            //else
            //    btnDelete.Attributes.Add("onclick", "alert('Only OIC can delete all records.');return false;");

            //btnDelete.Attributes.Add("onclick", "callModalpopup()");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.location='IDPMS_BOE_Setlement_Cancel.aspx?mode=edit&PayRefNo=" + lblPayRefNo.Text.Trim() + "'";
                if (i != 9)
                    cell.Attributes.Add("onclick", pageurl);
                else
                cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void GridViewInwData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if(btnhdndelete.Value == "Yes")
        //{
        //string result = "";
        //string result1 = "";
        //string[] values_p;
        //string _docNo = "", _billNo = "", _InvSrNo = "";
        //string str = e.CommandArgument.ToString();
        //if (str != "")
        //{
        //    char[] splitchar = { ';' };
        //    values_p = str.Split(splitchar);
        //    _docNo = values_p[0].ToString();
        //    _billNo = values_p[1].ToString();
        //    _InvSrNo = values_p[2].ToString();
        //}
        //SqlParameter a1 = new SqlParameter("@docNo", _docNo);
        //SqlParameter a2 = new SqlParameter("@billNo", _billNo);
        //SqlParameter a3 = new SqlParameter("@invSrNo", _InvSrNo);
        //string _query1 = "TF_IDPMS_SETTLEMENT_GetIecde";
        //TF_DATA objData1 = new TF_DATA();
        //result1 = objData1.SaveDeleteData(_query1, a1, a2, a3);


        //SqlParameter p1 = new SqlParameter("@AdCode", Session["userADCode"].ToString());
        //SqlParameter p2 = new SqlParameter("@docNo", _docNo);
        //SqlParameter p3 = new SqlParameter("@billNo", _billNo);
        //SqlParameter p4 = new SqlParameter("@invSrNo", _InvSrNo);

        //string _query = "IDPMS_SETTLEMENT_DELETE";
        //TF_DATA objData = new TF_DATA();
        //result = objData.SaveDeleteData(_query, p1, p2, p3, p4);
        //fillGrid();
        //if (result == "deleted")
        //{
        //    //#region AUDIT TRAIL
        //    string oldvalue = "Document No:" + _docNo + ";Bill of Entry No:" + _billNo + ";Invoice Sr No:" + _InvSrNo;
        //    _query = "TF_IDPMS_AddEdit_AuditTrail";
        //    #region AUDIT TRAIL
        //    SqlParameter q1 = new SqlParameter("@ADCode", Session["userADCode"].ToString());
        //    SqlParameter q2 = new SqlParameter("@IECode", result1);
        //    SqlParameter q3 = new SqlParameter("@OldValues", oldvalue);
        //    SqlParameter q4 = new SqlParameter("@NewValues", "");
        //    SqlParameter q5 = new SqlParameter("@DocumentNo", _docNo);
        //    SqlParameter q6 = new SqlParameter("@Mode", "D");
        //    SqlParameter q7 = new SqlParameter("@ModifiedBy", Session["userName"].ToString());
        //    SqlParameter q8 = new SqlParameter("@ModifiedDate", "");
        //    SqlParameter q9 = new SqlParameter("@MenuName", "Bill Of Entry - Settlement Deletion Entry");
        //    string S = objData.SaveDeleteData(_query, q1, q2, q3, q4, q5, q6, q7, q8, q9);


        //    #endregion
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);

        //}
        //}
    }
    protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
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
    //protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillGrid();
    //}
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
}