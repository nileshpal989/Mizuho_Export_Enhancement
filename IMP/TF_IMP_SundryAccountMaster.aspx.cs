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

public partial class IMP_TF_IMP_SundryAccountMaster : System.Web.UI.Page
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
                sundryAc_Rdbtn.Checked = true;
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
                }
                txtSearch.Attributes.Add("onkeypress", "return submitForm(event);");
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
            if (GridViewSundryAccount.PageCount != GridViewSundryAccount.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewSundryAccount.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewSundryAccount.PageIndex + 1) + " of " + GridViewSundryAccount.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewSundryAccount.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewSundryAccount.PageIndex != (GridViewSundryAccount.PageCount - 1))
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
        GridViewSundryAccount.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewSundryAccount.PageIndex > 0)
        {
            GridViewSundryAccount.PageIndex = GridViewSundryAccount.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewSundryAccount.PageIndex != GridViewSundryAccount.PageCount - 1)
        {
            GridViewSundryAccount.PageIndex = GridViewSundryAccount.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewSundryAccount.PageIndex = GridViewSundryAccount.PageCount - 1;
        fillGrid();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_IMP_AddEditSundryAccountMaster.aspx?mode=add", true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    { fillGrid(); }
    protected void fillGrid()
    {
        string search = txtSearch.Text.Trim();
        string Sundryacctype = string.Empty;
        string interoffacctype = string.Empty;
        if(sundryAc_Rdbtn.Checked)
        {
            Sundryacctype = "1";
        }
        else
        {
            Sundryacctype = "0";
        }

        if (InteroffAc_Rdbtn.Checked)
        {
            interoffacctype = "1";
        }
        else
        {
            interoffacctype = "0";
        }

        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        SqlParameter p2 = new SqlParameter("@sundry_AC", SqlDbType.VarChar);
        p2.Value = Sundryacctype;

        SqlParameter p3 = new SqlParameter("@interoffice_ac", SqlDbType.VarChar);
        p3.Value = interoffacctype;

        TF_DATA objData = new TF_DATA();
        string _query = "TF_GetSundryAccountMaster";

        DataTable dt = objData.getData(_query, p1,p2,p3);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewSundryAccount.PageSize = _pageSize;
            GridViewSundryAccount.DataSource = dt.DefaultView;
            GridViewSundryAccount.DataBind();
            GridViewSundryAccount.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewSundryAccount.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
        RowCheckBox1_CheckedChanged(null, null);
        chkIMP_CheckedChanged(null, null);
    }
    protected void GridViewSundryAccount_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
        if (sundryAc_Rdbtn.Checked)
        {
            string _ACCODE = e.CommandArgument.ToString();
            TF_DATA objSave = new TF_DATA();
            string _query = "TF_IMP_DeleteSundryAccountDetails";

            SqlParameter p1 = new SqlParameter("@SRNO", SqlDbType.VarChar);
            p1.Value = _ACCODE;

            result = objSave.SaveDeleteData(_query, p1);
            fillGrid();
            if (result == "deleted")
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record can not be deleted as it is associated with another record.');", true);
        }
        else {
            string _ACCODE = e.CommandArgument.ToString();
            TF_DATA objSave = new TF_DATA();
            string _query = "TF_IMP_DeleteinterofficeAccountDetails";

            SqlParameter p1 = new SqlParameter("@SRNO", SqlDbType.VarChar);
            p1.Value = _ACCODE;

            result = objSave.SaveDeleteData(_query, p1);
            fillGrid();
            if (result == "deleted")
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);
            else
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record can not be deleted as it is associated with another record.');", true);
       
        
        }
    }
    
    protected void GridViewSundryAccount_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSrNO = new Label();
            Label lblAccode = new Label();
            Button btnDelete = new Button();
            lblSrNO = (Label)e.Row.FindControl("lblSrNo");
            lblAccode = (Label)e.Row.FindControl("lblAccode");
            btnDelete = (Button)e.Row.FindControl("btnDelete");

                btnDelete.Enabled = true;
                btnDelete.CssClass = "deleteButton";
                btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.location='TF_IMP_AddEditSundryAccountMaster.aspx?mode=edit&AcCode=" + lblAccode.Text.Trim() + "&SrNo=" + lblSrNO.Text.Trim() + "'";
               
                if (i != 9)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void RowCheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        CheckIMPWH();
    }
    public void CheckIMPWH()//CHECKED CHECKBOX SUNDRY ACCOUNT IN GRIDVIEW
    {
        foreach (GridViewRow row in GridViewSundryAccount.Rows)
        {
            Label custACNo = (Label)row.FindControl("lblAccode");
            TF_DATA objData = new TF_DATA();
            string _query = "TF_SundryAC_Select_Checkbox";
            SqlParameter PCustACN = new SqlParameter("@CustACNO", custACNo.Text.Trim());
            string dt = objData.SaveDeleteData(_query, PCustACN);
            if (dt == "Yes")
            {
                CheckBox chkcheck = (CheckBox)row.FindControl("RowCheckBox1");
                chkcheck.Checked = true;
                chkcheck.ToolTip = "SUNDRY ACCOUNT";
            }
        }
    }

    protected void chkIMP_CheckedChanged(object sender, EventArgs e)
    {
        CheckIMP();
    }
    public void CheckIMP()//CHECKED CHECKBOX INTEROFFICE IN GRIDVIEW
    {
        foreach (GridViewRow row in GridViewSundryAccount.Rows)
        {
            Label custACNo = (Label)row.FindControl("lblAccode");
            TF_DATA objData = new TF_DATA();
            string _query = "[TF_INTEROFFAC_Select_Checkbox]";
            SqlParameter PCustACN = new SqlParameter("@CustACNO", custACNo.Text.Trim());
            string dt = objData.SaveDeleteData(_query, PCustACN);
            if (dt == "Yes")
            {
                CheckBox chkcheck = (CheckBox)row.FindControl("chkIMP");
                chkcheck.Checked = true;
                chkcheck.ToolTip = "INTEROFFICE ACCOUNT";
            }
        }
    }
    protected void sundryAc_Rdbtn_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();

    }
    protected void InteroffAc_Rdbtn_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
}