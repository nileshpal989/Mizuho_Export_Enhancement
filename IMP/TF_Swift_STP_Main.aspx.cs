using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;

public partial class TF_Swift_STP_Main : System.Web.UI.Page
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
                txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                fillGrid();
            }
        }
    }
    protected void fillGrid()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter _TransType = new SqlParameter("@TransType", ddlTrans.SelectedValue.Trim());
        SqlParameter _txtFromDate = new SqlParameter("@FromDate", txtFromDate.Text.Trim());
        SqlParameter _Module = new SqlParameter("@Module", ddlModule.SelectedValue.Trim());
        SqlParameter _Search = new SqlParameter("@Search", txtSearch.Text.Trim());
        DataTable dt = objData.getData("TF_SWIFT_STP_FillSwift", _TransType, _txtFromDate,_Module, _Search);
        if (dt.Rows.Count > 0)
        {
            
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewSwiftDash.PageSize = _pageSize;
            GridViewSwiftDash.DataSource = dt.DefaultView;
            GridViewSwiftDash.DataBind();
            GridViewSwiftDash.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewSwiftDash.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        SpanTranstype.Visible = true;
        ddlTrans.Items.Clear();
        ddlTrans.ClearSelection();
        ListItem _select = new ListItem("-select-", "");
        ddlTrans.Items.Insert(0, _select);

        if (ddlModule.SelectedValue == "IMP")
        {
            ListItem _Lodgement = new ListItem("Lodgement", "Lodgement");
            ListItem _Acceptance = new ListItem("Acceptance", "Acceptance");
            ListItem _Settlement = new ListItem("Settlement", "Settlement");

            ddlTrans.Items.Insert(1, _Lodgement);
            ddlTrans.Items.Insert(2, _Acceptance);
            ddlTrans.Items.Insert(3, _Settlement);
        }
        else if (ddlModule.SelectedValue == "EXP")
        {
            ListItem _Export = new ListItem("Exp Swift", "Export");
            ddlTrans.Items.Insert(1, _Export);
        }
        else 
        {
            SpanTranstype.Visible = false;
            ddlTrans.Items.Clear();
            ddlTrans.ClearSelection();
        }
        fillGrid();
    }
    protected void ddlSwift_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void ddlTrans_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void GridViewSwiftDash_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblGbaseStatus = new Label();
            Label lblXMLStatus = new Label();
            Label lblInfraStatus = new Label();
            Label lblSwiftStatus = new Label();
            lblGbaseStatus = (Label)e.Row.FindControl("lblGbaseStatus");
            lblXMLStatus = (Label)e.Row.FindControl("lblXMLStatus");
            lblInfraStatus = (Label)e.Row.FindControl("lblInfraStatus");

            if (lblGbaseStatus.Text == "Approved")
            {
                lblGbaseStatus.ForeColor = System.Drawing.Color.Green;
            }
            if (lblGbaseStatus.Text == "Rejected")
            {
                lblGbaseStatus.ForeColor = System.Drawing.Color.Tomato;
            }
            if (lblXMLStatus.Text == "Yes")
            {
                lblXMLStatus.ForeColor = System.Drawing.Color.Green;
            }
            if (lblXMLStatus.Text == "No")
            {
                lblXMLStatus.ForeColor = System.Drawing.Color.Tomato;
            }
            if (lblInfraStatus.Text == "ACK")
            {
                lblInfraStatus.ForeColor = System.Drawing.Color.Green;
            }
            if (lblInfraStatus.Text == "NAK")
            {
                lblInfraStatus.ForeColor = System.Drawing.Color.Tomato;
            }
        }
    }
    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewSwiftDash.PageCount != GridViewSwiftDash.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewSwiftDash.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewSwiftDash.PageIndex + 1) + " of " + GridViewSwiftDash.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewSwiftDash.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewSwiftDash.PageIndex != (GridViewSwiftDash.PageCount - 1))
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
        GridViewSwiftDash.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewSwiftDash.PageIndex > 0)
        {
            GridViewSwiftDash.PageIndex = GridViewSwiftDash.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewSwiftDash.PageIndex != GridViewSwiftDash.PageCount - 1)
        {
            GridViewSwiftDash.PageIndex = GridViewSwiftDash.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewSwiftDash.PageIndex = GridViewSwiftDash.PageCount - 1;
        fillGrid();
    }

    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
}