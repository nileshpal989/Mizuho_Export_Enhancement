using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class Masters_TF_View_ImpAuto_CommissionMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("../TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlrecordperpage.SelectedValue = "20";
                fillGrid();
                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].Trim() == "added")
                    {
                        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added.');", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "Alert('Record Added.');", true);
                    }
                    else
                        if (Request.QueryString["result"].Trim() == "updated")
                        {
                            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "Alert('Record Updated.');", true);
                        }
                } txtSearch.Attributes.Add("onkeypress", "return submitForm(event);");
                btnSearch.Attributes.Add("onclick", "return validateSearch();");
            }
        }

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
            if (GridViewCommission.PageCount != GridViewCommission.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewCommission.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewCommission.PageIndex + 1) + " of " + GridViewCommission.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewCommission.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewCommission.PageIndex != (GridViewCommission.PageCount - 1))
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
        GridViewCommission.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewCommission.PageIndex > 0)
        {
            GridViewCommission.PageIndex = GridViewCommission.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewCommission.PageIndex != GridViewCommission.PageCount - 1)
        {
            GridViewCommission.PageIndex = GridViewCommission.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewCommission.PageIndex = GridViewCommission.PageCount - 1;
        fillGrid();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_ImpAuto_AddEditCommissionMaster.aspx?mode=add&BranchName=" + ddlBranch.SelectedItem.Text, true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    { fillGrid(); }
    protected void fillGrid()
    {
        //TF_DATA objData = new TF_DATA();
        string search = txtSearch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;
        SqlParameter p2 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p2.Value = ddlBranch.SelectedItem.Text;

        //  string _query = "TF_GetCommissionMasterList";
        string _query = "TF_Imp_GetCommissionMasterList";

        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewCommission.PageSize = _pageSize;
            GridViewCommission.DataSource = dt.DefaultView;
            GridViewCommission.DataBind();
            GridViewCommission.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewCommission.Visible = false;
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
        li.Value = "All";
        if (dt.Rows.Count > 0)
        {
            li.Text = "All Branches";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void GridViewCommission_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string str = e.CommandArgument.ToString();
        string[] values_p;
        if (str != "")
        {
            char[] splitchar = { ';' };
            values_p = str.Split(splitchar);
            hdnAc.Value = values_p[0].ToString();
            hdnid.Value = values_p[1].ToString();
            hdndescription.Value = values_p[2].ToString();
            hdnRate.Value = values_p[3].ToString();
            hdnMiniINR.Value = values_p[4].ToString();
            hdnMaxINR.Value = values_p[5].ToString();
            hdnFlatINR.Value = values_p[6].ToString();

        }
        if (e.CommandName == "RemoveRecord")
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "confirm", "confirmDelete()", true);
        }

    }
    protected void GridViewCommission_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblCommissionID = new Label();
            Label lblBranchName = new Label();
            Button btnDelete = new Button();
            lblCommissionID = (Label)e.Row.FindControl("lblID");
            Label lblcustacno = (Label)e.Row.FindControl("lblcustac");
            lblBranchName = (Label)e.Row.FindControl("lblBranchName");


            btnDelete = (Button)e.Row.FindControl("btnDelete");

            btnDelete.Enabled = true;
            btnDelete.CssClass = "deleteButton";
            //btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.location='TF_ImpAuto_AddEditCommissionMaster.aspx?mode=edit&BranchName=" + lblBranchName.Text.Trim() + "&CommissionID=" + lblCommissionID.Text.Trim() + "&custacno=" + lblcustacno.Text + "'";
                if (i != 9)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void btnDeleteConfirm_Click(object sender, EventArgs e)
    {
        string result = "";

        SqlParameter p1 = new SqlParameter("@CommissionID ", SqlDbType.VarChar);
        p1.Value = hdnid.Value;

        SqlParameter p2 = new SqlParameter("@CustomerAcno", SqlDbType.VarChar);
        p2.Value = hdnAc.Value;

        SqlParameter pusername = new SqlParameter("@userName", SqlDbType.VarChar);
        pusername.Value = Session["userName"].ToString();

        // string _query = "TF_DeleteCommissionMasterDetails";
        string _query = "TF_Imp_DeleteCommissionMasterDetails";


        TF_DATA objData = new TF_DATA();
        result = objData.SaveDeleteData(_query, p1, p2, pusername);
        fillGrid();

        if (result == "deleted")
        {
            AuditTrailDeleted(hdnAc.Value, hdnid.Value, hdndescription.Value, hdnRate.Value, hdnMiniINR.Value, hdnMaxINR.Value, hdnFlatINR.Value);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "deletemessage", "Alert('Record Deleted.');", true);

        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "deletemessage", "Alert('" + result + "');", true);
        }
    }
    protected void AuditTrailDeleted(string _ac, string _id, string _desc, string _rate, string _minimum, string _max, string _flat)
    {

        string _userName = Session["userName"].ToString();

        TF_DATA objdata = new TF_DATA();
        string _OldValues = "Customer AC NO : " + _ac + " ; Commision ID : " + _id +
        " ; Description : " + _desc + " ; Rate : " + _rate + " ; Minimum INR : " + _minimum + " ; Maximum INR : " + _max + " ; Flat INR : " + _flat;

        SqlParameter A1 = new SqlParameter("@OldValues", SqlDbType.VarChar);
        SqlParameter A2 = new SqlParameter("@NewValues", SqlDbType.VarChar);
        SqlParameter A3 = new SqlParameter("@ModType", SqlDbType.VarChar);
        SqlParameter A4 = new SqlParameter("@ModifiedBy", Session["userName"].ToString().Trim());
        SqlParameter A5 = new SqlParameter("@ModifiedDate", "");
        SqlParameter A6 = new SqlParameter("@MenuName", "Commission Master");
        SqlParameter A7 = new SqlParameter("@Mode", SqlDbType.VarChar);
        SqlParameter A8 = new SqlParameter("@RefNo", SqlDbType.VarChar);
        A1.Value = _OldValues;
        A2.Value = "";
        A3.Value = "IMP";
        A7.Value = "D";
        A8.Value = _id;
        string p = objdata.SaveDeleteData("TF_Mizuho_IMP_AuditTrail_master", A1, A2, A3, A4, A5, A6, A7, A8);

    }
}