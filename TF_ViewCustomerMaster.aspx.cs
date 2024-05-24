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

public partial class TF_ViewCustomerMaster : System.Web.UI.Page
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
                hdnUserRole.Value = Session["userRole"].ToString().Trim();
                fillBranch();

                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                //ddlBranch.Enabled = false;

                ddlrecordperpage.SelectedValue = "20";
                fillGrid();
                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].Trim() == "added")
                    {
                        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Added.');", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "Alert('Record added.');", true);
                    }
                    else
                        if (Request.QueryString["result"].Trim() == "updated")
                        {
                            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "Alert('Record updated.');", true);
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
    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewCustomerList.PageCount != GridViewCustomerList.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewCustomerList.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewCustomerList.PageIndex + 1) + " of " + GridViewCustomerList.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewCustomerList.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewCustomerList.PageIndex != (GridViewCustomerList.PageCount - 1))
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
        GridViewCustomerList.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewCustomerList.PageIndex > 0)
        {
            GridViewCustomerList.PageIndex = GridViewCustomerList.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewCustomerList.PageIndex != GridViewCustomerList.PageCount - 1)
        {
            GridViewCustomerList.PageIndex = GridViewCustomerList.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewCustomerList.PageIndex = GridViewCustomerList.PageCount - 1;
        fillGrid();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_AddEditCustomerMaster.aspx?mode=add&ddlbranch=" + ddlBranch.SelectedItem.Text.Trim(), true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void fillGrid()
    {

        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = txtSearch.Text.Trim();

        SqlParameter p2 = new SqlParameter("@adcode", SqlDbType.VarChar);
        p2.Value = ddlBranch.SelectedItem.Value;


        string _query = "TF_GetCustomerMasterList";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2);

        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewCustomerList.PageSize = _pageSize;
            GridViewCustomerList.DataSource = dt.DefaultView;
            GridViewCustomerList.DataBind();
            GridViewCustomerList.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);

        }
        else
        {
            GridViewCustomerList.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
        RowCheckBox1_CheckedChanged(null, null);
        chkIMP_CheckedChanged(null, null);
    }
    protected void GridViewCustomerList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string CUST_ACCOUNT_NO = "";
        string Cust_Abbr = "";
        string CUST_NAME = "";
        string CUST_ADDRESS = "";
        string City = "";
        string CUST_COUNTRY = "";
        string CUST_IE_CODE = "";
        string S_Date1 = "";
        string S_Date2 = "";
        string S_Date3 = "";
        string S_Date4 = "";
        string[] values_P;
        string AllID = e.CommandArgument.ToString();
        if (AllID != "")
        {
            char[] splitchar = { ';' };
            values_P = AllID.Split(splitchar);
            CUST_ACCOUNT_NO = values_P[0].ToString();
            Cust_Abbr = values_P[1].ToString();
            CUST_NAME = values_P[2].ToString();
            CUST_ADDRESS = values_P[3].ToString();
            City = values_P[4].ToString();
            CUST_COUNTRY = values_P[5].ToString();
            CUST_IE_CODE = values_P[6].ToString();
            S_Date1 = values_P[7].ToString();
            S_Date2 = values_P[8].ToString();
            S_Date3 = values_P[9].ToString();
            S_Date4 = values_P[10].ToString();
        }


        string result = "";
        //string _custAC = e.CommandArgument.ToString();

        SqlParameter p3 = new SqlParameter("@ADCode", SqlDbType.VarChar);
        p3.Value = ddlBranch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@custAC", SqlDbType.VarChar);
        p1.Value = CUST_ACCOUNT_NO;
        TF_DATA objData = new TF_DATA();
        SqlParameter p2 = new SqlParameter("@customerACNo", SqlDbType.VarChar);
        p2.Value = CUST_ACCOUNT_NO;
        SqlParameter p5 = new SqlParameter("@user", Session["userName"].ToString().Trim());
        SqlParameter p6 = new SqlParameter("@uploadeddate", System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        string _query = "TF_DeleteCustomerMaster";
        result = objData.SaveDeleteData(_query, p1, p5, p6, p3);
        fillGrid();

        string _userName = Session["userName"].ToString();
        if (result == "deleted")
        {
            AuditTrailDeleted(CUST_ACCOUNT_NO, Cust_Abbr, CUST_NAME, CUST_ADDRESS, City, CUST_COUNTRY, CUST_IE_CODE, S_Date1, S_Date2, S_Date3, S_Date4);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record can not be deleted as it is associated with another record.');", true);
        }
    }
    protected void GridViewCustomerList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblCustomerACNO = new Label();
            Label lblLinked = new Label();
            Button btnDelete = new Button();
            lblCustomerACNO = (Label)e.Row.FindControl("lblcustomerACNo");
            lblLinked = (Label)e.Row.FindControl("lblLinked");
            btnDelete = (Button)e.Row.FindControl("btnDelete");
            btnDelete.Enabled = true;
            btnDelete.CssClass = "deleteButton";
            if (hdnUserRole.Value == "OIC")
                btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");
            else
                btnDelete.Attributes.Add("onclick", "alert('Only OIC can delete all records.');return false;");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.location='TF_AddEditCustomerMaster.aspx?mode=edit&customerACNo=" + lblCustomerACNO.Text.Trim() + "&ddlbranch=" + ddlBranch.SelectedItem.Text + "'";
                if (i != 10)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
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
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
        //ddlBranch.SelectedIndex = 1;
        //ddlBranch_SelectedIndexChanged(null, null);
        //fillGrid();
    }
    public void CheckIMPWH()
    {
        foreach (GridViewRow row in GridViewCustomerList.Rows)
        {
            Label custACNo = (Label)row.FindControl("lblcustomerACNo");
            TF_DATA objData = new TF_DATA();
            string _query = "TF_IMPWH_Select_Checkbox";
            SqlParameter PCustACN = new SqlParameter("@CustACNO", custACNo.Text.Trim());
            string dt = objData.SaveDeleteData(_query, PCustACN);
            if (dt == "Yes")
            {
                CheckBox chkcheck = (CheckBox)row.FindControl("RowCheckBox1");
                chkcheck.Checked = true;
                chkcheck.ToolTip = "Import Warehousing Customer";
            }
        }
    }
    protected void RowCheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        CheckIMPWH();
    }
    public void CheckIMP()
    {
        foreach (GridViewRow row in GridViewCustomerList.Rows)
        {
            Label custACNo = (Label)row.FindControl("lblcustomerACNo");
            TF_DATA objData = new TF_DATA();
            string _query = "TF_IMP_Select_Checkbox";
            SqlParameter PCustACN = new SqlParameter("@CustACNO", custACNo.Text.Trim());
            string dt = objData.SaveDeleteData(_query, PCustACN);
            if (dt == "Yes")
            {
                CheckBox chkcheck = (CheckBox)row.FindControl("chkIMP");
                chkcheck.Checked = true;
                chkcheck.ToolTip = "Import Automation Customer";
            }
        }
    }
    protected void chkIMP_CheckedChanged(object sender, EventArgs e)
    {
        CheckIMP();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void AuditTrailDeleted(string CUST_ACCOUNT_NO, string Cust_Abbr, string CUST_NAME, string CUST_ADDRESS, string City, string CUST_COUNTRY,
        string CUST_IE_CODE, string S_Date1, string S_Date2, string S_Date3, string S_Date4)
    {

        string _userName = Session["userName"].ToString();

        TF_DATA objdata = new TF_DATA();

        // string _OldValues = "Customer A/C No :" + CUST_ACCOUNT_NO.ToString() + "Customer Name :" + CUST_NAME.ToString() + ";IECode  : " + CUST_IE_CODE.ToString() + ";Address  : " + CUST_ADDRESS.ToString() +
        //";City : " + City.ToString() + ";Contry : " + CUST_COUNTRY.ToString()+";Date1 : " + S_Date1.ToString() + ";Date2 : " + S_Date2.ToString() + ";Date3 : " + S_Date3.ToString() + ";Date4 : " + S_Date4.ToString();
        string _OldValues = "Customer Name :" + CUST_NAME.ToString();
        SqlParameter A1 = new SqlParameter("@OldValues", SqlDbType.VarChar);
        SqlParameter A2 = new SqlParameter("@NewValues", SqlDbType.VarChar);
        SqlParameter A3 = new SqlParameter("@ModType", SqlDbType.VarChar);
        SqlParameter A4 = new SqlParameter("@ModifiedBy", Session["userName"].ToString().Trim());
        SqlParameter A5 = new SqlParameter("@ModifiedDate", "");
        SqlParameter A6 = new SqlParameter("@MenuName", "Customer Master");
        SqlParameter A7 = new SqlParameter("@Mode", SqlDbType.VarChar);
        SqlParameter A8 = new SqlParameter("@RefNo", SqlDbType.VarChar);
        A1.Value = _OldValues;
        A2.Value = "";
        A3.Value = "IMP";
        A7.Value = "D";
        A8.Value = CUST_ACCOUNT_NO;
        string p = objdata.SaveDeleteData("TF_Mizuho_IMP_AuditTrail_master", A1, A2, A3, A4, A5, A6, A7, A8);

    }
}
