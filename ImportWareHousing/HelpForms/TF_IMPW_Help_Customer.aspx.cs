using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class TF_IMPW_Help_Customer : System.Web.UI.Page
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
        SqlParameter p1 = new SqlParameter("@AdCode", Session["userADCode"].ToString());
        SqlParameter p2 = new SqlParameter("@Search", txtSearch.Text.Trim());

        string _query = "TF_IMPW_CustomerHelp";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewCustAcList.PageSize = _pageSize;
            GridViewCustAcList.DataSource = dt.DefaultView;
            GridViewCustAcList.DataBind();
            GridViewCustAcList.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            pagination(_records, _pageSize);
            labelMessage.Visible = false;

        }
        else
        {
            GridViewCustAcList.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Visible = true;
            labelMessage.Text = "No record(s) found.";
        }
        txtSearch.Focus();
    }
    protected void GridViewCustAcList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
    }
    protected void GridViewCustAcList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblCustACNo = new Label();
            lblCustACNo = (Label)e.Row.FindControl("lblCustACNo");

            string srno = Request.QueryString["srno"].ToString();

            string pageurl = "window.opener.selectCustomer('" + lblCustACNo.Text + "','" + srno + "');window.opener.EndRequest();window.close();";

            e.Row.Attributes.Add("onclick", pageurl);
            e.Row.Attributes.Add("onkeypress", "javascript:if (event.keyCode == 13 || event.keyCode == 32) {" + pageurl + " return false; }");
        }
    }

    protected void GridViewCustAcList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //onmouseover AND onmouseout of gridview row, Change the color of Gridview Row
            e.Row.Attributes["onmouseover"] = string.Format("javascript:OnMouseOver(this, {0});", e.Row.RowIndex);
            e.Row.Attributes["onmouseout"] = string.Format("javascript:OnMouseOut(this, {0});", e.Row.RowIndex);
            //onFocus AND onBlur of gridview row, Change the color of Gridview Row
            e.Row.Attributes["onfocus"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
            e.Row.Attributes["onblur"] = string.Format("javascript:DisSelectRow(this, {0});", e.Row.RowIndex);
            e.Row.Attributes["onselectstart"] = "javascript:return false;";
            e.Row.ToolTip = "Click to select row.";
        }

        //This Logic is used for assigning tabindex to every row in gridview
        //int LoopCounter;

        // Variable for starting index. Use this to make sure the tabindexes start at a higher
        // value than any other controls above the gridview. 
        // Header row indexes will be 110, 111, 112...
        // First data row will be 210, 211, 212... 
        // Second data row 310, 311, 312 .... and so on

        //int tabIndexStart = 2;

        //for (LoopCounter = 0; LoopCounter < e.Row.Cells.Count; LoopCounter++)
        //{
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    // Check to see if the cell contains any controls
        //    if (e.Row.Cells[LoopCounter].Controls.Count > 0)
        //    {
        //        // Set the TabIndex. Increment RowIndex by 2 because it starts at -1
        //        ((LinkButton)e.Row.Cells[LoopCounter].Controls[0]).TabIndex = short.Parse((e.Row.RowIndex + 2).ToString() + tabIndexStart++.ToString());
        //    }
        //}
        //else 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Set the TabIndex. Increment RowIndex by 2 because it starts at -1

            //e.Row.Cells[LoopCounter].TabIndex = short.Parse((e.Row.RowIndex + 2).ToString() + tabIndexStart++.ToString());

            //e.Row.TabIndex = short.Parse((e.Row.RowIndex + 1).ToString() + tabIndexStart++.ToString());
            //e.Row.TabIndex = short.Parse(tabIndexStart++.ToString());
            //e.Row.TabIndex = short.Parse((e.Row.RowIndex + 1).ToString());


            // Set the TabIndex. Increment RowIndex by 2 because it starts at -1
            e.Row.TabIndex = short.Parse((e.Row.RowIndex + 2).ToString());

        }
        //}

    }

    protected void btngo_Click(object sender, EventArgs e)
    {
        fillGrid();
    }

    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewCustAcList.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewCustAcList.PageIndex > 0)
        {
            GridViewCustAcList.PageIndex = GridViewCustAcList.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewCustAcList.PageIndex != GridViewCustAcList.PageCount - 1)
        {
            GridViewCustAcList.PageIndex = GridViewCustAcList.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewCustAcList.PageIndex = GridViewCustAcList.PageCount - 1;
        fillGrid();
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
            if (GridViewCustAcList.PageCount != GridViewCustAcList.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewCustAcList.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewCustAcList.PageIndex + 1) + " of " + GridViewCustAcList.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewCustAcList.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewCustAcList.PageIndex != (GridViewCustAcList.PageCount - 1))
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
