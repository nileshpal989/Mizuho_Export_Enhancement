using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class IMP_TF_IMP_NostroMaster_View : System.Web.UI.Page
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
                fillGrid();
                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].Trim() == "added")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "Alert('Record Added.');", true);
                    }
                    else
                        if (Request.QueryString["result"].Trim() == "Updated")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "Alert('Record Updated.');", true);
                        }
                }
                txtSearch.Attributes.Add("onkeypress", "return submitForm(event);");
                btnSearch.Attributes.Add("onclick", "return validateSearch();");
            }
        }
    }
    public void fillGrid()
    {
        string search = txtSearch.Text.Trim();

        TF_DATA objData = new TF_DATA();
        string _query = "TF_IMP_GetNostroMaster_List";

        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewNostroMaster.PageSize = _pageSize;
            GridViewNostroMaster.DataSource = dt.DefaultView;
            GridViewNostroMaster.DataBind();
            GridViewNostroMaster.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewNostroMaster.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
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
            if (GridViewNostroMaster.PageCount != GridViewNostroMaster.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewNostroMaster.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewNostroMaster.PageIndex + 1) + " of " + GridViewNostroMaster.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewNostroMaster.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewNostroMaster.PageIndex != (GridViewNostroMaster.PageCount - 1))
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
        GridViewNostroMaster.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewNostroMaster.PageIndex > 0)
        {
            GridViewNostroMaster.PageIndex = GridViewNostroMaster.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewNostroMaster.PageIndex != GridViewNostroMaster.PageCount - 1)
        {
            GridViewNostroMaster.PageIndex = GridViewNostroMaster.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewNostroMaster.PageIndex = GridViewNostroMaster.PageCount - 1;
        fillGrid();
    }
    protected void GridViewNostroMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        string result = "";
        string CustABBR = e.CommandArgument.ToString();
        TF_DATA objSave = new TF_DATA();
        string _query = "TF_IMP_DeleteNostroMasterDetails";

        SqlParameter p1 = new SqlParameter("@CUST_ABBR", SqlDbType.VarChar);
        p1.Value = CustABBR;
        SqlParameter puserName = new SqlParameter("@userName", SqlDbType.VarChar);
        puserName.Value = Session["userName"].ToString();

        result = objSave.SaveDeleteData(_query, p1, puserName);
        fillGrid();
        if (result == "deleted")
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "Alert('Record Deleted.');", true);
        else
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "Alert('" + result + "');", true);

    }
    protected void GridViewNostroMaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCustABBR = new Label();
                Label lblLinked = new Label();
                Button btnDelete = new Button();
                lblCustABBR = (Label)e.Row.FindControl("lblCustABBR");
                lblLinked = (Label)e.Row.FindControl("lblLinked");
                btnDelete = (Button)e.Row.FindControl("btnDelete");
                if (lblLinked.Text.Trim() == "0")
                {
                    btnDelete.Enabled = true;
                    btnDelete.CssClass = "deleteButton";
                    btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");
                }
                else
                {
                    btnDelete.Enabled = false;
                    btnDelete.CssClass = "deleteButtonDisabled";
                }
                int i = 0;
                foreach (TableCell cell in e.Row.Cells)
                {
                    string pageurl = "window.location='TF_IMP_Nostro_Master.aspx?mode=edit&CustABBR=" + lblCustABBR.Text.Trim() + "'";
                    if (i != 8)
                        cell.Attributes.Add("onclick", pageurl);
                    else
                        cell.Style.Add("cursor", "default");
                    i++;
                }
            }
        }
        }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_IMP_Nostro_Master.aspx?mode=add", true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
}