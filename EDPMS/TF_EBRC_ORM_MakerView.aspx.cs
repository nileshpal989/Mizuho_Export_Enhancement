using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EDPMS_TF_EBRC_ORM_MakerView : System.Web.UI.Page
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
                //txtYear.Text = System.DateTime.Now.ToString("yyyy");
                ddlrecordperpage.SelectedValue = "20";
                fillBranch();
                ddlbranch.SelectedValue = Session["userLBCode"].ToString();
                txtfromDate.Focus();
                txtfromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                ddlOrmstatus.SelectedValue = "1";
                
                fillGrid();
                btnAdd.Visible = false;
                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].Trim() == "added")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added.');", true);
                    }
                    else
                        if (Request.QueryString["result"].Trim() == "updated")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
                        }
                }
            }
        }

        //txtYear.Attributes.Add("onblur", "return checkYear();");
        //txtDocumentNo.Attributes.Add("onblur", "return checkDocNo();");
    }
    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlbranch.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlbranch.DataSource = dt.DefaultView;
            ddlbranch.DataTextField = "BranchName";
            ddlbranch.DataValueField = "BranchCode";
            ddlbranch.DataBind();
        }
        else
            li.Text = "No record(s) found";
        ddlbranch.Items.Insert(0, li);
    }
    protected void fillGrid()
    {
        //getLastDocNo();
        string search = txtSearch.Text.Trim();
        string irmstatus;
        SqlParameter p1 = new SqlParameter("@branchCode", ddlbranch.SelectedValue);
        SqlParameter p2 = new SqlParameter("@Ormno", "");
        SqlParameter p3 = new SqlParameter("@mode", "");
        SqlParameter p4 = new SqlParameter("@search", search);
        SqlParameter p5 = new SqlParameter("@status", "AllANDREJ");
        if (ddlOrmstatus.SelectedValue == "1")
        {
            irmstatus = "F";
        }
        else if (ddlOrmstatus.SelectedValue == "2")
        {
            irmstatus = "A";
        }
        else if (ddlOrmstatus.SelectedValue == "3")
        {
            irmstatus = "C";
        }
        else
        {
            irmstatus = "";
        }

        if (irmstatus == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select orm status')", true);
            ddlOrmstatus.Focus();
        }
        else
        {
            SqlParameter p6 = new SqlParameter("@fromdate", txtfromDate.Text);
            SqlParameter p7 = new SqlParameter("@todate", txtToDate.Text);
            SqlParameter p8 = new SqlParameter("@Ormstatus", irmstatus);
            string _query = "TF_EBRC_ORM_Grid";
            TF_DATA objData = new TF_DATA();
            DataTable dt = objData.getData(_query, p1, p2, p3, p4, p5, p6, p7, p8);
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
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void btnsearchrecord_Click(object sender, EventArgs e)
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
    protected void ddlrecordperpage_SelectedIndexChanged1(object sender, EventArgs e)
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
    protected void ddlOrmstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOrmstatus.SelectedValue == "0")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Orm status')", true);
            ddlOrmstatus.Focus();
        }
        if (ddlOrmstatus.SelectedValue == "1")
        {
            fillGrid();
        }
        else if (ddlOrmstatus.SelectedValue == "2")
        {
            fillGrid();
        }
        else if (ddlOrmstatus.SelectedValue == "3")
        {
            fillGrid();
        }
    }
    protected void GridViewInwData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //string result = "";
        //string _Ormno = e.CommandArgument.ToString();
        //SqlParameter p1 = new SqlParameter("@branchCode", ddlbranch.SelectedValue.Trim());
        //SqlParameter p2 = new SqlParameter("@Ormno", _Ormno);

        //string _query = "TF_EBRC_ORM_Grid_Delete";
        //TF_DATA objData = new TF_DATA();
        //result = objData.SaveDeleteData(_query, p1, p2);
        //fillGrid();
        //if (result == "deleted")
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);
        //}
    }
    protected void GridViewInwData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblormno = new Label();
            Label lblDocDate = new Label();
            Button btnDelete = new Button();
            Label Status = new Label();
            Label ormstatus = new Label();
            Label APIStatus = new Label();
            Label DGTFStatus = new Label();
            Status = (Label)e.Row.FindControl("lblstatus");
            APIStatus = (Label)e.Row.FindControl("lblAPIstatus");
            DGTFStatus = (Label)e.Row.FindControl("lblDGFTstatus");
            if (Status.Text == "Rejected")
            {
                e.Row.BackColor = System.Drawing.Color.Tomato;
            }
            else if ((APIStatus.Text == "Failed" || DGTFStatus.Text == "Failed") && (Status.Text== "Rejected" || Status.Text=="Approved"))
            {
                e.Row.BackColor = System.Drawing.Color.Tomato;
            }
            
            lblormno = (Label)e.Row.FindControl("lblORMNo");
            ormstatus = (Label)e.Row.FindControl("lblormstatus");
            //btnDelete = (Button)e.Row.FindControl("btnDelete");
            //btnDelete.Enabled = true;
            //btnDelete.CssClass = "deleteButton";
            //btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");
            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.location='TF_EBRC_ORM_Maker.aspx?mode=edit&Ormno=" + lblormno.Text.Trim() + "&Ormstatus=" + ormstatus.Text + "'";
                if (i != 11)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlbranch.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Select Branch.');", true);
            ddlbranch.Focus();
        }
        else
        {
            string branch = ddlbranch.SelectedValue.Trim();
            Response.Redirect("TF_EBRC_ORM_Maker.aspx?mode=add", true);
        }
    }
}