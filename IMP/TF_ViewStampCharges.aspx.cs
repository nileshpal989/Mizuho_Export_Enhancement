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

public partial class IMP_TF_ViewStampCharges : System.Web.UI.Page
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
            if (GridViewStampDutyChargesList.PageCount != GridViewStampDutyChargesList.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewStampDutyChargesList.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewStampDutyChargesList.PageIndex + 1) + " of " + GridViewStampDutyChargesList.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewStampDutyChargesList.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewStampDutyChargesList.PageIndex != (GridViewStampDutyChargesList.PageCount - 1))
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
        GridViewStampDutyChargesList.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewStampDutyChargesList.PageIndex > 0)
        {
            GridViewStampDutyChargesList.PageIndex = GridViewStampDutyChargesList.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewStampDutyChargesList.PageIndex != GridViewStampDutyChargesList.PageCount - 1)
        {
            GridViewStampDutyChargesList.PageIndex = GridViewStampDutyChargesList.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewStampDutyChargesList.PageIndex = GridViewStampDutyChargesList.PageCount - 1;
        fillGrid();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("StampDuty_Master.aspx?mode=add", true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    { fillGrid(); }
    protected void fillGrid()
    {

        string search = txtSearch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        string _query = "TF_IMP_GetStampDutyChargesList";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewStampDutyChargesList.PageSize = _pageSize;
            GridViewStampDutyChargesList.DataSource = dt.DefaultView;
            GridViewStampDutyChargesList.DataBind();
            GridViewStampDutyChargesList.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewStampDutyChargesList.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
    protected void GridViewStampDutyChargesList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string _str = e.CommandArgument.ToString();
        string[] values_p;
        if (_str != "")
        {
            char[] splitchar = { ';' };
            values_p = _str.Split(splitchar);
            hdid.Value = values_p[0].ToString();
            hdndate.Value = values_p[1].ToString();
            hdndescription.Value = values_p[2].ToString();
            hdntenior.Value = values_p[3].ToString();
            hdnrate.Value = values_p[4].ToString();
        }


        if (e.CommandName == "RemoveRecord")
        {

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "confirm", "confirmDelete()", true);
        }

    }
    protected void GridViewStampDutyChargesList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblID = new Label();
            Label lblEffectiveDate = new Label();
            Label lblLinked = new Label();
            Button btnDelete = new Button();
            lblID = (Label)e.Row.FindControl("lblID");
            lblEffectiveDate = (Label)e.Row.FindControl("lblEffectiveDate");
            lblLinked = (Label)e.Row.FindControl("lblLinked");
            btnDelete = (Button)e.Row.FindControl("btnDelete");
            //if (lblLinked.Text.Trim() == "0")
            //{
            btnDelete.Enabled = true;
            btnDelete.CssClass = "deleteButton";
            //    btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");
            //}
            //else
            //{
            //    btnDelete.Enabled = false;
            //    btnDelete.CssClass = "deleteButtonDisabled";
            //}
            string ID = lblID.Text.Trim();
            ID = Server.UrlEncode(ID);
            string EffectiveDate = lblEffectiveDate.Text.Trim();
            EffectiveDate = Server.UrlEncode(EffectiveDate);
            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.location='StampDuty_Master.aspx?mode=edit&ID=" + ID + "&effectDate=" + EffectiveDate + "'";
                if (i != 6)
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

        SqlParameter p1 = new SqlParameter("@lblID", SqlDbType.VarChar);
        p1.Value = hdid.Value;

        SqlParameter p2 = new SqlParameter("@lblEffectiveDate", SqlDbType.VarChar);
        p2.Value = hdndate.Value;
        SqlParameter puserName = new SqlParameter("@userName", SqlDbType.VarChar);
        puserName.Value = Session["userName"].ToString();

        string _query = "TF_IMP_DeleteStampDutyChargeDetails";
        TF_DATA objData = new TF_DATA();
        result = objData.SaveDeleteData(_query, p1, p2, puserName);
        fillGrid();
        if (result == "deleted")
        {
            AuditTrailDeleted(hdid.Value, hdndate.Value, hdndescription.Value, hdntenior.Value, hdnrate.Value);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "deletemessage", "Alert('Record Deleted.');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "deletemessage", "Alert('" + result + "');", true);
        }
    }
    protected void AuditTrailDeleted(string _id, string _date, string _desc, string _tenior, string _rate)
    {

        string _userName = Session["userName"].ToString();

        TF_DATA objdata = new TF_DATA();
        string _OldValues = "ID : " + _id + " ; Effective Date : " + _date + " ; Description : " + _desc + " ; Tenor : " + _tenior + " ; Rate : " + _rate;
        SqlParameter A1 = new SqlParameter("@OldValues", SqlDbType.VarChar);
        SqlParameter A2 = new SqlParameter("@NewValues", SqlDbType.VarChar);
        SqlParameter A3 = new SqlParameter("@ModType", SqlDbType.VarChar);
        SqlParameter A4 = new SqlParameter("@ModifiedBy", Session["userName"].ToString().Trim());
        SqlParameter A5 = new SqlParameter("@ModifiedDate", "");
        SqlParameter A6 = new SqlParameter("@MenuName", "Stamp Duty master");
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