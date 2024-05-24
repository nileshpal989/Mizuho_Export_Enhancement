using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;


public partial class IMP_Transactions_TF_IMP_SwiftFreeFormat_View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("../../TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                lbl_Header.Text = Request.QueryString["ID"].ToString();
                if (Request.QueryString["ID"].ToString() == "Swift Free Format Message - Checker")
                {
                    btnAdd.Visible = false;
                }
                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch_SelectedIndexChanged(null, null);

                fillGrid();

                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].ToString() == "Added")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added.');", true);
                    }
                    if (Request.QueryString["result"].ToString() == "Submit")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record submit to Checker.');", true);
                    }
                }
            }
        }
    }
    protected void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "";
        if (dt.Rows.Count > 0)
        {
            li.Text = "All Branches";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_IMP_SwiftFreeFormat.aspx?ID=" + Request.QueryString["ID"].ToString() + "&mode=add&BranchCode=" + ddlBranch.SelectedItem.Value.Trim() + "&BranchName=" + ddlBranch.SelectedItem.Text, true);
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void fillGrid()
    {
        string Maker_Checker = "";
        if (Request.QueryString["ID"].ToString() == "Swift Free Format Message - Checker")
        { Maker_Checker = "C"; }
        else
        { Maker_Checker = "M"; }

        SqlParameter p1 = new SqlParameter("@search", txtSearch.Text.Trim());
        SqlParameter p_BranchName = new SqlParameter("@BranchName", ddlBranch.SelectedItem.Text);
        SqlParameter p_Maker_Checker = new SqlParameter("@Maker_Checker", Maker_Checker);

        TF_DATA objdata = new TF_DATA();
        DataTable dt = objdata.getData("TF_IMP_Swift_FreeFormatList", p1, p_BranchName, p_Maker_Checker);

        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            FreeFormatSwiftGrid.PageSize = _pageSize;
            FreeFormatSwiftGrid.DataSource = dt.DefaultView;
            FreeFormatSwiftGrid.DataBind();
            FreeFormatSwiftGrid.Visible = true;
            rowGrid.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            FreeFormatSwiftGrid.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No Record(s) found.";
            labelMessage.Visible = true;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        FreeFormatSwiftGrid.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (FreeFormatSwiftGrid.PageIndex > 0)
        {
            FreeFormatSwiftGrid.PageIndex = FreeFormatSwiftGrid.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (FreeFormatSwiftGrid.PageIndex != FreeFormatSwiftGrid.PageCount - 1)
        {
            FreeFormatSwiftGrid.PageIndex = FreeFormatSwiftGrid.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        FreeFormatSwiftGrid.PageIndex = FreeFormatSwiftGrid.PageCount - 1;
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
            if (FreeFormatSwiftGrid.PageCount != FreeFormatSwiftGrid.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (FreeFormatSwiftGrid.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (FreeFormatSwiftGrid.PageIndex + 1) + " of " + FreeFormatSwiftGrid.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (FreeFormatSwiftGrid.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (FreeFormatSwiftGrid.PageIndex != (FreeFormatSwiftGrid.PageCount - 1))
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
    protected void FreeFormatSwiftGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string s = e.CommandArgument.ToString(), _DocNo, _Swift_Type;

        string[] subs = s.Split(',');
        _DocNo = subs[0].ToString();
        _Swift_Type = subs[1].ToString();
        SqlParameter p_Transrefno = new SqlParameter("@Document_No", _DocNo.ToString());
        SqlParameter p_SwiftType = new SqlParameter("@Swift_type", _Swift_Type.ToString());

        TF_DATA objdata = new TF_DATA();
        string result = objdata.SaveDeleteData("TF_IMP_Swift_FreeFormat_Delete", p_Transrefno, p_SwiftType);
        fillGrid();
        if (result == "deleted")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "deletemessage", "alert('Record Deleted.')'", true);
        }

    }
    protected void FreeFormatSwiftGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label _Document_No = new Label();
            _Document_No = (Label)e.Row.FindControl("lblDocument_No");
            Label _Swifttype = new Label();
            _Swifttype = (Label)e.Row.FindControl("lblSwift_type");

            Label Status = new Label();
            Status = (Label)e.Row.FindControl("lblStatus");
            Label Gbase_Status = new Label();
            Gbase_Status = (Label)e.Row.FindControl("lblGbase_Status");
            Label Swift_Status = new Label();
            Swift_Status = (Label)e.Row.FindControl("lblSwift_Status");

            Button btnDelete = new Button();
            btnDelete = (Button)e.Row.FindControl("btnDelete");


            if (Status.Text == "Reject By Checker" || Gbase_Status.Text == "Reject By Bot" || Swift_Status.Text == "Reject By Bot")
            {
                e.Row.BackColor = System.Drawing.Color.Tomato;
            }


            btnDelete.Enabled = true;
            btnDelete.CssClass = "deleteButton";
            btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string url = "window.location='TF_IMP_SwiftFreeFormat.aspx?ID=" + Request.QueryString["ID"].ToString() + "&mode=edit&Document_No=" + _Document_No.Text.Trim() + "&Swift_Type=" + _Swifttype.Text.Trim() + "'";
                if (i != 8)
                    cell.Attributes.Add("onclick", url);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
}