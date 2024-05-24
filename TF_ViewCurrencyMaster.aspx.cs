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

public partial class RBI_ViewCurrencyMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
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

    protected void btnSearch_Click(object sender, EventArgs e)
    { fillGrid(); }
    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewCurrency.PageCount != GridViewCurrency.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewCurrency.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewCurrency.PageIndex + 1) + " of " + GridViewCurrency.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewCurrency.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewCurrency.PageIndex != (GridViewCurrency.PageCount - 1))
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
        GridViewCurrency.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewCurrency.PageIndex > 0)
        {
            GridViewCurrency.PageIndex = GridViewCurrency.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewCurrency.PageIndex != GridViewCurrency.PageCount - 1)
        {
            GridViewCurrency.PageIndex = GridViewCurrency.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewCurrency.PageIndex = GridViewCurrency.PageCount - 1;
        fillGrid();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_AddEditCurrencyMaster.aspx?mode=add", true);
    }
    protected void fillGrid()
    {
        string search = txtSearch.Text.Trim();

        TF_DATA objData = new TF_DATA();
        string _query = "TF_GetCurrencyList";

        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewCurrency.PageSize = _pageSize;
            GridViewCurrency.DataSource = dt.DefaultView;
            GridViewCurrency.DataBind();
            GridViewCurrency.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewCurrency.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
    protected void GridViewCurrency_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        string _currid = "";
        string _Description = "";
        string _GBCurrcny = "";
        string Status = "";
        string[] values_P;
        string AllID = e.CommandArgument.ToString();
        if (AllID != "")
        {
            char[] splitchar = { ';' };
            values_P = AllID.Split(splitchar);
            _currid = values_P[0].ToString();
            _Description = values_P[1].ToString();
            _GBCurrcny = values_P[2].ToString();
            Status = values_P[3].ToString();

        }
        hdncustid1.Value = _currid;
        hdnDescription.Value = _Description;
        hdnGBCurrcny.Value = _GBCurrcny;
        hdnStatus.Value = Status;

        if (e.CommandName == "RemoveRecord")
        {
            //hdncustid1.Value = _currid;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "confirm", "confirmDelete()", true);
        }
    }
    protected void GridViewCurrency_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblCurrencyID = new Label();
            Label lblLinked = new Label();
            Label lblStatus = new Label();
            Button btnDelete = new Button();
            lblCurrencyID = (Label)e.Row.FindControl("lblCurrencyID");
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

            lblStatus = (Label)e.Row.FindControl("lblStatus");
            if (lblStatus.Text == "In-Active")
            {
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.location='TF_AddEditCurrencyMaster.aspx?mode=edit&currencyid=" + lblCurrencyID.Text.Trim() + "'";
                if (i != 4)
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
        TF_DATA objSave = new TF_DATA();
        string _query = "TF_DeleteCurrencyMasterDetails";
        SqlParameter pusername = new SqlParameter("@userName", SqlDbType.VarChar);
        pusername.Value = Session["userName"].ToString();

        SqlParameter p1 = new SqlParameter("@currencyid", SqlDbType.VarChar);
        p1.Value = hdncustid1.Value;

        result = objSave.SaveDeleteData(_query, p1, pusername);

        fillGrid();
        if (result == "deleted")
        {
            AuditTrailDeleted(hdncustid1.Value, hdnDescription.Value, hdnGBCurrcny.Value, hdnStatus.Value);  // Audit Trial method call
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "deletemessage", "Alert('Record Deleted.');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "deletemessage", "Alert('Record can not be deleted as it is associated with another record.');", true);
        }

    }
    protected void AuditTrailDeleted(string _currid, string _Description, string _GBCurrcny, string Status)
    {

        string _userName = Session["userName"].ToString();

        TF_DATA objdata = new TF_DATA();
        string _OldValues = "Description : " + _Description + " ; GBase Curr Code : " + _GBCurrcny +
        " ; Currency ID : " + _currid + " ; Status : " + Status;

        SqlParameter A1 = new SqlParameter("@OldValues", SqlDbType.VarChar);
        SqlParameter A2 = new SqlParameter("@NewValues", SqlDbType.VarChar);
        SqlParameter A3 = new SqlParameter("@ModType", SqlDbType.VarChar);
        SqlParameter A4 = new SqlParameter("@ModifiedBy", Session["userName"].ToString().Trim());
        SqlParameter A5 = new SqlParameter("@ModifiedDate", "");
        SqlParameter A6 = new SqlParameter("@MenuName", "Currency Master");
        SqlParameter A7 = new SqlParameter("@Mode", SqlDbType.VarChar);
        SqlParameter A8 = new SqlParameter("@RefNo", SqlDbType.VarChar);
        A1.Value = _OldValues;
        A2.Value = "";
        A3.Value = "IMP";
        A7.Value = "D";
        A8.Value = _currid;
        string p = objdata.SaveDeleteData("TF_Mizuho_IMP_AuditTrail_master", A1, A2, A3, A4, A5, A6, A7, A8);

    }
}
