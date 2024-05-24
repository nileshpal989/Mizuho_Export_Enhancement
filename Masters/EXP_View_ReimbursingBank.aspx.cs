﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Master_EXP_View_ReimbursingBank : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
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
    protected void fillGrid()
    {

        string search = txtSearch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        string _query = "TF_GetReimbursingBankMasterList";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewBankCodeList.PageSize = _pageSize;
            GridViewBankCodeList.DataSource = dt.DefaultView;
            GridViewBankCodeList.DataBind();
            GridViewBankCodeList.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewBankCodeList.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewBankCodeList.PageCount != GridViewBankCodeList.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewBankCodeList.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewBankCodeList.PageIndex + 1) + " of " + GridViewBankCodeList.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewBankCodeList.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewBankCodeList.PageIndex != (GridViewBankCodeList.PageCount - 1))
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("EXP_AddEditReimbursingBank.aspx?mode=add", true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewBankCodeList.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {

        if (GridViewBankCodeList.PageIndex > 0)
        {
            GridViewBankCodeList.PageIndex = GridViewBankCodeList.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewBankCodeList.PageIndex != GridViewBankCodeList.PageCount - 1)
        {
            GridViewBankCodeList.PageIndex = GridViewBankCodeList.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewBankCodeList.PageIndex = GridViewBankCodeList.PageCount - 1;
        fillGrid();
    }

    protected void GridViewBankCodeList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";

        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        string _bankCode = commandArgs[0];
        string _Curr = commandArgs[1];

        SqlParameter p1 = new SqlParameter("@bankCode", SqlDbType.VarChar);
        p1.Value = _bankCode;
        SqlParameter pusername = new SqlParameter("@userName", SqlDbType.VarChar);
        pusername.Value = Session["userName"].ToString();

        string _query = "TF_DeleteReimbursingBankDetails";
        TF_DATA objData = new TF_DATA();
        result = objData.SaveDeleteData(_query, p1, pusername);
        fillGrid();
        if (result == "deleted")
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);
        else
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record can not be deleted as it is associated with another record.');", true);
    }
    protected void GridViewBankCodeList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblbankCode = new Label();
            Label lblCurr = new Label();
            Button btnDelete = new Button();
            lblbankCode = (Label)e.Row.FindControl("lblbankCode");
            lblCurr = (Label)e.Row.FindControl("lblCurr");
            btnDelete = (Button)e.Row.FindControl("btnDelete");

            btnDelete.Enabled = true;
            btnDelete.CssClass = "deleteButton";
            btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");

            string bankCode = lblbankCode.Text.Trim();
            string Curr = lblCurr.Text.Trim();
            //bankCode = Server.UrlEncode(bankCode);
            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.location='EXP_AddEditReimbursingBank.aspx?mode=edit&bankcode=" + bankCode + "&Curr=" + Curr + "'";
                if (i != 4)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }

}