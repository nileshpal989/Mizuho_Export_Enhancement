﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HelpForms_TF_EXP_ConsigneePARTY_Helpform_RPT : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            fillGrid();
        }

    }
    protected void fillGrid()
    {

        string search = "";
        txtSearch.Text = search;
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        string _query = "TF_GetConsigneePartyMasterList_Help";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;

            GridViewBankList.DataSource = dt.DefaultView;
            GridViewBankList.DataBind();
            GridViewBankList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;

        }
        else
        {
            GridViewBankList.Visible = false;
            rowGrid.Visible = false;

            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }

    }
    protected void GridViewBankList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
    }
    protected void GridViewBankList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblBankCode = new Label();
            lblBankCode = (Label)e.Row.FindControl("lblBankCode");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.opener.selectConsigneeParty('" + lblBankCode.Text + "');window.opener.EndRequest();window.close();";
                if (i != 6)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void GridViewBankList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow &&
   (e.Row.RowState == DataControlRowState.Normal ||
    e.Row.RowState == DataControlRowState.Alternate))
        {
            //e.Row.TabIndex = -1;
            //e.Row.Attributes["onmouseover"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
            //e.Row.Attributes["onmouseout"] = string.Format("javascript:DisSelectRow(this, {0});", e.Row.RowIndex);
            e.Row.TabIndex = -1;
            e.Row.Attributes["onclick"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
            e.Row.Attributes["onkeydown"] = "javascript:return SelectSibling(event);";
            e.Row.Attributes["onselectstart"] = "javascript:return false;";
            e.Row.ToolTip = "Click to select row and then navigate";

            // e.Row.ToolTip = "Click to select row.";
        }

    }
    protected void btngo_Click(object sender, EventArgs e)
    {

        string search = txtSearch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        string _query = "TF_GetConsigneePartyMasterList_Help";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;

            GridViewBankList.DataSource = dt.DefaultView;
            GridViewBankList.DataBind();
            GridViewBankList.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;

        }
        else
        {
            GridViewBankList.Visible = false;
            rowGrid.Visible = false;

            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
}