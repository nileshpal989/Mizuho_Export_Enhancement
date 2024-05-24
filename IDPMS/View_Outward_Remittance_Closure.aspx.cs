using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class IDPMS_View_Outward_Remittance_Closure : System.Web.UI.Page
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
                txtYear.Text = DateTime.Now.Year.ToString();
                ddlrecordperpage.SelectedValue = "20";

                fillBranch();
                fillGrid();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;


                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].ToString() == "inserted")
                    {
                        string _docNo = Request.QueryString["result"].ToString().Substring(5);
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added.');", true);
                    }
                    else if (Request.QueryString["result"].Trim() == "updated")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
                    }
                }

            }
        }
    }

    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        //ListItem li = new ListItem();
        //li.Value = "All Branches";

        if (dt.Rows.Count > 0)
        {
            //li.Text = "All Branches";

            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();

        }
        //else
        //    li.Text = "No record(s) found";

        //ddlBranch.Items.Insert(0, li);

    }
    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
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
            if (GridViewinwardclosure.PageCount != GridViewinwardclosure.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewinwardclosure.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewinwardclosure.PageIndex + 1) + " of " + GridViewinwardclosure.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewinwardclosure.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewinwardclosure.PageIndex != (GridViewinwardclosure.PageCount - 1))
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
        GridViewinwardclosure.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewinwardclosure.PageIndex > 0)
        {
            GridViewinwardclosure.PageIndex = GridViewinwardclosure.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewinwardclosure.PageIndex != GridViewinwardclosure.PageCount - 1)
        {
            GridViewinwardclosure.PageIndex = GridViewinwardclosure.PageIndex + 1;
        }

        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewinwardclosure.PageIndex = GridViewinwardclosure.PageCount - 1;
        fillGrid();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();

    }

    protected void fillGrid()
    {
        //getLastDocNo();
        string search = txtSearch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        SqlParameter p2 = new SqlParameter("@branch", SqlDbType.VarChar);
        p2.Value = ddlBranch.SelectedValue.ToString();

        SqlParameter p3 = new SqlParameter("@year", SqlDbType.VarChar);
        p3.Value = txtYear.Text.ToString();


        string _query = "Fill_ORM_CLOSURE";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2, p3);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewinwardclosure.PageSize = _pageSize;
            GridViewinwardclosure.DataSource = dt.DefaultView;
            GridViewinwardclosure.DataBind();
            GridViewinwardclosure.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewinwardclosure.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }

    protected void GridViewinwardclosure_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblirmno = new Label();
            Button btnDelete = new Button();
            //Label lbldocno = new Label();
            //Label lblshipbillno = new Label();

            lblirmno = (Label)e.Row.FindControl("lblORMno");

            btnDelete = (Button)e.Row.FindControl("btnDelete");

            btnDelete.Enabled = true;
            btnDelete.CssClass = "deleteButton";
            if (hdnUserRole.Value == "OIC")
                btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");
            else
                btnDelete.Attributes.Add("onclick", "alert('Only OIC can delete all records.');return false;");


            //lbldocno = (Label)e.Row.FindControl("lbldocno");
            //lblshipbillno = (Label)e.Row.FindControl("lblshipbillno");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                //string pageurl = "window.location='AddEdit_Outward_Remittance_Closure.aspx?mode=edit&ORMNo=" + lblirmno.Text.Trim() + "'";
                //if (i != 6)
                //    cell.Attributes.Add("onclick", pageurl);
                //else
                cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }

    protected void GridViewinwardclosure_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";

        string[] values_p;
        string _irmno = "", _docNo = "", _docdate = "", _iecode = "";
        string str = e.CommandArgument.ToString();
        if (str != "")
        {
            char[] splitchar = { ';' };
            values_p = str.Split(splitchar);
            _irmno = values_p[0].ToString().Trim();
            _docNo = values_p[1].ToString().Trim();
            _docdate = values_p[2].ToString().Trim();
            _iecode = values_p[3].ToString().Trim();
        }

        //string _userName = Session["userName"].ToString().Trim();
        //string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string irmno = e.CommandArgument.ToString();

        SqlParameter p1 = new SqlParameter("@ORMNo", SqlDbType.VarChar);
        p1.Value = _irmno;


        string _query = "Delete_ORM";
        TF_DATA objData = new TF_DATA();
        result = objData.SaveDeleteData(_query, p1);
        fillGrid();
        if (result == "deleted")
        {

            #region AUDIT TRAIL
            string oldvalue = "Document No:" + _docNo + ";ORM No:" + _irmno + ";Adjustment Date:" + _docdate;

            _query = "TF_IDPMS_AddEdit_AuditTrail";

            SqlParameter q1 = new SqlParameter("@ADCode", Session["userADCode"].ToString());
            SqlParameter q2 = new SqlParameter("@IECode", _iecode);
            SqlParameter q3 = new SqlParameter("@OldValues", oldvalue);
            SqlParameter q4 = new SqlParameter("@NewValues", "");
            SqlParameter q5 = new SqlParameter("@DocumentNo", _docNo);
            SqlParameter q6 = new SqlParameter("@Mode", "D");
            SqlParameter q7 = new SqlParameter("@ModifiedBy", Session["userName"].ToString());
            SqlParameter q8 = new SqlParameter("@ModifiedDate", "");
            SqlParameter q9 = new SqlParameter("@MenuName", "Outward Remittance Closure");

            string S = objData.SaveDeleteData(_query, q1, q2, q3, q4, q5, q6, q7, q8, q9);


            #endregion

            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);
        }
        else
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record cannot be deleted.');", true);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddEdit_Outward_Remittance_Closure.aspx", true);
    }
}