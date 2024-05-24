using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class IDPMS_EXP_ViewPaymentExtension : System.Web.UI.Page
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
               // ddlBranch.SelectedValue = "Mumbai";
                //hdnUserRole.Value = Session["userRole"].ToString().Trim();
                txtYear.Text = DateTime.Now.Year.ToString();
                ddlrecordperpage.SelectedValue = "20";

                fillBranch();
                fillGrid();
                
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;
                //string a = ddlBranch.DataTextField;

                

                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].ToString() == "inserted")
                    {
                        string _docNo = Request.QueryString["result"].ToString().Substring(5);
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added.');", true);
                        //ddlBranch.SelectedValue = "Mumbai";
                    }
                    else
                        if (Request.QueryString["result"].Trim() == "updated")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
                            //ddlBranch.SelectedValue = "Mumbai";
                        }
                }
                //txtSearch.Attributes.Add("onkeypress", "return submitForm(event);");
                btnAdd.Attributes.Add("onclick", "return validate();");
                btnSearch.Attributes.Add("onclick", "return validateSearch();");
                txtYear.Attributes.Add("onblur", "return checkYear();");
                //txtDocumentNo.Attributes.Add("onblur", "return checkDocNo();");
                txtYear.Attributes.Add("onkeydown", "return validate_Number(event);");
               // txtYear.Attributes.Add("onkeydown", "return validate_Number(event);");

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
            if (GridViewpaymentextn.PageCount != GridViewpaymentextn.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewpaymentextn.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewpaymentextn.PageIndex + 1) + " of " + GridViewpaymentextn.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewpaymentextn.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewpaymentextn.PageIndex != (GridViewpaymentextn.PageCount - 1))
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
        GridViewpaymentextn.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewpaymentextn.PageIndex > 0)
        {
            GridViewpaymentextn.PageIndex = GridViewpaymentextn.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewpaymentextn.PageIndex != GridViewpaymentextn.PageCount - 1)
        {
            GridViewpaymentextn.PageIndex = GridViewpaymentextn.PageIndex + 1;
        }

        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewpaymentextn.PageIndex = GridViewpaymentextn.PageCount - 1;
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

        SqlParameter p2 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p2.Value = ddlBranch.SelectedItem.Text;

        //SqlParameter p3 = new SqlParameter("@DocType", SqlDbType.VarChar);
        //p3.Value = txtDocPrFx.Text.Trim();

        SqlParameter p4 = new SqlParameter("@year", SqlDbType.VarChar);
        p4.Value = txtYear.Text.Trim();

        string _query = "TF_EXP_Getpaymentextndetails1";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2, p4);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewpaymentextn.PageSize = _pageSize;
            GridViewpaymentextn.DataSource = dt.DefaultView;
            GridViewpaymentextn.DataBind();
            GridViewpaymentextn.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewpaymentextn.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //TF_DATA objData = new TF_DATA();
        //SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        //p1.Value = ddlBranch.Text.Trim();
        //string _query = "TF_GetBranchDetails";
        //DataTable dt = objData.getData(_query, p1);

        //if (dt.Rows.Count > 0)
        //{
        //    txtBranchCode.Text = dt.Rows[0]["BranchCode"].ToString().Trim();

        //    getLastDocNo();
        //}
        //else
        //{
        //    hdnBranchCode.Value = "";
        //    txtDocumentNo.Text = "";
        //}

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
    protected void GridViewpaymentextn_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblcustacno = new Label();
            Label lblprtcd = new Label();
            Label lblbillno = new Label();

            lblcustacno = (Label)e.Row.FindControl("lblcustacno");
            lblprtcd = (Label)e.Row.FindControl("lblprtcd");
            lblbillno = (Label)e.Row.FindControl("lblbillno");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.location='TF_AddEditPaymentExtn.aspx?mode=edit&custacno=" + lblcustacno.Text.Trim()
                    + "&DocYear=" + txtYear.Text + "&BranchName=" + ddlBranch.SelectedItem.Text + "&prtcd=" + lblprtcd.Text + "&billno=" + lblbillno.Text + "'";
                if (i != 7)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_AddEditPaymentExtn.aspx?mode=add&Year=" + txtYear.Text + "&BranchName=" + ddlBranch.Text.Trim(), true);
    }
}
