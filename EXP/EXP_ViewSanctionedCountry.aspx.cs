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

public partial class EXP_EXP_ViewSanctionedCountry : System.Web.UI.Page
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
                ddlrecordpage.SelectedValue = "20";
                fillGrid();
                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].Trim() == "added")
                    {
                        //ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Record Added.');", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "Alert('Record Added.');", true);
                    }
                    else if (Request.QueryString["result"].Trim() == "updated")
                    {
                        //ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Record Updated.');", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "Alert('Record Updated.');", true);
                    }
                }
                txtSearch.Attributes.Add("onkeypress", "return submitForm(event);");
                btnSearch.Attributes.Add("onclick", "return validateSearch();");
            }
        }
    }
    protected void GridViewCountry_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string _cuntryid = "";
        string _CuntryName = "";
        string[] values_P;
        string AllID = e.CommandArgument.ToString();
        if (AllID != "")
        {
            char[] splitchar = { ';' };
            values_P = AllID.Split(splitchar);
            _cuntryid = values_P[0].ToString();
            _CuntryName = values_P[1].ToString();

        }
        hdnsancid.Value = _cuntryid;
        hdnsanname.Value = _CuntryName;
        //string _id = e.CommandArgument.ToString();
        if (e.CommandName == "RemoveRecord")
        {
            //hdnsancid.Value = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "confirm", "confirmDelete()", true);
        }
    }
    protected void GridViewCountry_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblCountryID = new Label();
            Button btnDelete = new Button();

            lblCountryID = (Label)e.Row.FindControl("lblCountryID");
            btnDelete = (Button)e.Row.FindControl("btnDelete");

            btnDelete.Enabled = true;
            btnDelete.CssClass = "deleteButton";
            //btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string url = "window.location='EXP_AddEditSanctionedCountry.aspx?mode=edit&countryid=" + lblCountryID.Text.Trim() + "'";
                if (i != 2)
                    cell.Attributes.Add("onclick", url);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("EXP_AddEditSanctionedCountry.aspx?mode=add", true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewCountry.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewCountry.PageIndex > 0)
        {
            GridViewCountry.PageIndex = GridViewCountry.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewCountry.PageIndex != GridViewCountry.PageCount - 1)
        {
            GridViewCountry.PageIndex = GridViewCountry.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewCountry.PageIndex = GridViewCountry.PageCount - 1;
        fillGrid();
    }
    protected void ddlrecordpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void fillGrid()
    {
        string search = txtSearch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        string query = "TF_GetCountryList_Sanctioned";
        TF_DATA objdata = new TF_DATA();
        DataTable dt = objdata.getData(query, p1);

        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordpage.SelectedValue.Trim());
            GridViewCountry.PageSize = _pageSize;
            GridViewCountry.DataSource = dt.DefaultView;
            GridViewCountry.DataBind();
            GridViewCountry.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewCountry.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No Record(s) found.";
            labelMessage.Visible = true;
        }
    }
    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewCountry.PageCount != GridViewCountry.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewCountry.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewCountry.PageIndex + 1) + " of " + GridViewCountry.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewCountry.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewCountry.PageIndex != (GridViewCountry.PageCount - 1))
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
    protected void btnDeleteConfirm_Click(object sender, EventArgs e)
    {
        string result = "";
        SqlParameter p1 = new SqlParameter("@countryID", SqlDbType.VarChar);
        p1.Value = hdnsancid.Value;
        SqlParameter pusername = new SqlParameter("@userName", SqlDbType.VarChar);
        pusername.Value = Session["userName"].ToString();

        string query = "TF_DeleteCountryMaster_Sanctioned";

        TF_DATA objdata = new TF_DATA();
        result = objdata.SaveDeleteData(query, p1, pusername);
        fillGrid();
        if (result == "deleted")
        {
            AuditTrailDeleted(hdnsancid.Value, hdnsanname.Value);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "deletemessage", "Alert('Record Deleted.');", true);
        }
    }
    protected void AuditTrailDeleted(string _cuntryid, string _CuntryName)
    {

        string _userName = Session["userName"].ToString();

        TF_DATA objdata = new TF_DATA();
        string _OldValues = "Description : " + _CuntryName + " ; Country ID : " + _cuntryid;

        SqlParameter A1 = new SqlParameter("@OldValues", SqlDbType.VarChar);
        SqlParameter A2 = new SqlParameter("@NewValues", SqlDbType.VarChar);
        SqlParameter A3 = new SqlParameter("@ModType", SqlDbType.VarChar);
        SqlParameter A4 = new SqlParameter("@ModifiedBy", Session["userName"].ToString().Trim());
        SqlParameter A5 = new SqlParameter("@ModifiedDate", "");
        SqlParameter A6 = new SqlParameter("@MenuName", "Sanctioned Country Master");
        SqlParameter A7 = new SqlParameter("@Mode", SqlDbType.VarChar);
        SqlParameter A8 = new SqlParameter("@RefNo", SqlDbType.VarChar);
        A1.Value = _OldValues;
        A2.Value = "";
        A3.Value = "IMP";
        A7.Value = "D";
        A8.Value = _cuntryid;
        string p = objdata.SaveDeleteData("TF_Mizuho_IMP_AuditTrail_master", A1, A2, A3, A4, A5, A6, A7, A8);

    }
}