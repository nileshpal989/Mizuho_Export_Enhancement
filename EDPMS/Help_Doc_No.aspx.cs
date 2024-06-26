﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class EDPMS_Help_Doc_No : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillGrid();
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
            if (GridViewUserList.PageCount != GridViewUserList.PageIndex + 1)
            {
                //lblrecordno.Text = "Record(s) : " + (GridViewUserList.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                //lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            //lblpageno.Text = "Page : " + (GridViewUserList.PageIndex + 1) + " of " + GridViewUserList.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewUserList.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewUserList.PageIndex != (GridViewUserList.PageCount - 1))
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
        //lblpageno.Visible = visibility;
        //lblrecordno.Visible = visibility;
        btnnavfirst.Visible = visibility;
        btnnavlast.Visible = visibility;
        btnnavnext.Visible = visibility;
        btnnavpre.Visible = visibility;
    }
    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewUserList.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewUserList.PageIndex > 0)
        {
            GridViewUserList.PageIndex = GridViewUserList.PageIndex - 1;
        }
        fillGrid();
    }

    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewUserList.PageIndex != GridViewUserList.PageCount - 1)
        {
            GridViewUserList.PageIndex = GridViewUserList.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewUserList.PageIndex = GridViewUserList.PageCount - 1;
        fillGrid();
    }



    protected void fillGrid()
    {
        string search = "";

        string Branch = Request.QueryString["Branch"];
        string Year = Request.QueryString["Year"];

        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        SqlParameter p2 = new SqlParameter("@Branch", SqlDbType.VarChar);
        p2.Value = Branch;

        SqlParameter p3 = new SqlParameter("@Year", SqlDbType.VarChar);
        p3.Value = Year;

        string _query = "Help_EDPMS_Doc_No";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2, p3);


        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());

            GridViewUserList.PageSize = _pageSize;
            GridViewUserList.DataSource = dt.DefaultView;
            GridViewUserList.DataBind();
            GridViewUserList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;

            rowGrid.Visible = true;
            rowPager.Visible = true;
            pagination(_records, _pageSize);


        }
        else
        {
            GridViewUserList.Visible = false;
            rowGrid.Visible = false;

            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }

    }
    protected void GridViewUserList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
    }
    protected void GridViewUserList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDocNo = new Label();
            lblDocNo = (Label)e.Row.FindControl("lblDocNo");
            Label lblBillAmount = new Label();
            lblBillAmount = (Label)e.Row.FindControl("lblBillAmount");
            Label lblCurrency = new Label();
            lblCurrency = (Label)e.Row.FindControl("lblCurrency");


            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "";

                pageurl = "window.opener.selectDocNo('" + lblDocNo.Text + "','" + lblBillAmount.Text +  "','" + lblCurrency.Text + "');window.opener.EndRequest();window.close();";

                if (i != 3)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }

    protected void GridViewUserList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TabIndex = -1;
            //e.Row.Attributes["onmouseover"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
            //e.Row.Attributes["onmouseout"] = string.Format("javascript:DisSelectRow(this, {0});", e.Row.RowIndex);

            e.Row.ToolTip = "Click to select row.";
        }

    }

    protected void btngo_Click(object sender, EventArgs e)
    {

        string search = txtSearch.Text.Trim();

        string Branch = Request.QueryString["Branch"];
        string Year = Request.QueryString["Year"];

        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        SqlParameter p2 = new SqlParameter("@Branch", SqlDbType.VarChar);
        p2.Value = Branch;

        SqlParameter p3 = new SqlParameter("@Year", SqlDbType.VarChar);
        p3.Value = Year;

        string _query = "Help_EDPMS_Doc_No";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1,p2,p3);

        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;

            GridViewUserList.DataSource = dt.DefaultView;
            GridViewUserList.DataBind();
            GridViewUserList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;
        }
        else
        {
            GridViewUserList.Visible = false;
            rowGrid.Visible = false;

            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
}


