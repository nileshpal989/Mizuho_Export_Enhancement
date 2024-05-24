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

public partial class IMP_HelpForms_TF_OverseasBankLookUp : System.Web.UI.Page
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
        string search = txtSearch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;
        txtSearch.Text = search;
        string _query = "TF_IMP_OverseasBankMasterList";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewBankList.PageSize = _pageSize;
            GridViewBankList.DataSource = dt.DefaultView;
            GridViewBankList.DataBind();
            GridViewBankList.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewBankList.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }

    }
    protected void GridViewBankList_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridViewBankList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSwiftCode = new Label();
            lblSwiftCode = (Label)e.Row.FindControl("lblSwiftCode");

            Label lblBankCode = new Label();
            lblBankCode = (Label)e.Row.FindControl("lblBankCode");

            Label lblBankName = new Label();
            lblBankName = (Label)e.Row.FindControl("lblBankName");

            Label lblBankAddress = new Label();
            lblBankAddress = (Label)e.Row.FindControl("lblBankAddress");


            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "";
                if (Request.QueryString["hNo"].ToString() == "1")
                {
                    pageurl = "window.opener.selectNego_Remit_Bank('" + lblSwiftCode.Text + "');window.opener.EndRequest();window.close();";
                }
                if (Request.QueryString["hNo"].ToString() == "IMP")
                {
                    pageurl = "window.opener.selectNego_Remit_Bank_IMP('" + lblSwiftCode.Text + "','" + lblBankCode.Text + "','" + lblBankName.Text + "','" + lblBankAddress.Text + "');window.opener.EndRequest();window.close();";
                }
                //if (Request.QueryString["hNo"].ToString() == "3")
                //{
                //    pageurl = "window.opener.selectAcWithInsti('" + lblBankCode.Text + "');window.opener.EndRequest();window.close();";
                //}
                //if (Request.QueryString["hNo"].ToString() == "4")
                //{
                //    pageurl = "window.opener.selectDocsRcvdBank('" + lblBankCode.Text + "');window.opener.EndRequest();window.close();";
                //}

                if (i != 10)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void GridViewBankList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TabIndex = -1;
            e.Row.Attributes["onmouseover"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
            e.Row.Attributes["onmouseout"] = string.Format("javascript:DisSelectRow(this, {0});", e.Row.RowIndex);

            e.Row.ToolTip = "Click to select row.";
        }

    }
    protected void btngo_Click(object sender, EventArgs e)
    {

        fillGrid();
    }
    protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewBankList.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewBankList.PageIndex > 0)
        {
            GridViewBankList.PageIndex = GridViewBankList.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewBankList.PageIndex != GridViewBankList.PageCount - 1)
        {
            GridViewBankList.PageIndex = GridViewBankList.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewBankList.PageIndex = GridViewBankList.PageCount - 1;
        fillGrid();
    }
    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewBankList.PageCount != GridViewBankList.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewBankList.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewBankList.PageIndex + 1) + " of " + GridViewBankList.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewBankList.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewBankList.PageIndex != (GridViewBankList.PageCount - 1))
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
}