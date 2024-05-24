using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class TF_ViewReceivingBankMaster : System.Web.UI.Page
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
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added.');", true);
                    }
                    else
                        if (Request.QueryString["result"].Trim() == "updated")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
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
            if (GridViewCurrencyReceivingBank.PageCount != GridViewCurrencyReceivingBank.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewCurrencyReceivingBank.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewCurrencyReceivingBank.PageIndex + 1) + " of " + GridViewCurrencyReceivingBank.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewCurrencyReceivingBank.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewCurrencyReceivingBank.PageIndex != (GridViewCurrencyReceivingBank.PageCount - 1))
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
        GridViewCurrencyReceivingBank.PageIndex = 0;
        fillGrid();
    }

    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewCurrencyReceivingBank.PageIndex > 0)
        {
            GridViewCurrencyReceivingBank.PageIndex = GridViewCurrencyReceivingBank.PageIndex - 1;
        }
        fillGrid();
    }

    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewCurrencyReceivingBank.PageIndex != GridViewCurrencyReceivingBank.PageCount - 1)
        {
            GridViewCurrencyReceivingBank.PageIndex = GridViewCurrencyReceivingBank.PageIndex + 1;
        }
        fillGrid();
    }

    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewCurrencyReceivingBank.PageIndex = GridViewCurrencyReceivingBank.PageCount - 1;
        fillGrid();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("TD_AddReceivingBankMaster.aspx?mode=add", true);
    }

    protected void fillGrid()
    {
        string search = txtSearch.Text.Trim();

        TF_DATA objData = new TF_DATA();
        string _query = "TF_GetReceivingBankList";

        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewCurrencyReceivingBank.PageSize = _pageSize;
            GridViewCurrencyReceivingBank.DataSource = dt.DefaultView;
            GridViewCurrencyReceivingBank.DataBind();
            GridViewCurrencyReceivingBank.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewCurrencyReceivingBank.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }

    protected void GridViewCurrencyReceivingBank_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
        string srno = e.CommandArgument.ToString();
        TF_DATA objSave = new TF_DATA();
        string _query = "TF_DeleteReceivingBankDetails";

        SqlParameter p1 = new SqlParameter("@srno", SqlDbType.VarChar);
        p1.Value = srno;

        result = objSave.SaveDeleteData(_query, p1);
        fillGrid();
        if (result == "deleted")
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);
        else
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record can not be deleted as it is associated with another record.');", true);
    }

    protected void GridViewCurrencyReceivingBank_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSrNo = new Label();
            Label lblLinked = new Label();
            Button btnDelete = new Button();
            lblSrNo = (Label)e.Row.FindControl("lblSrNo");
            btnDelete = (Button)e.Row.FindControl("btnDelete");
            
                btnDelete.Enabled = true;
                btnDelete.CssClass = "deleteButton";
                btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");
            
            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.location='TD_AddReceivingBankMaster.aspx?mode=edit&SrNo=" + lblSrNo.Text.Trim() + "'";
                if (i != 4)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
}


