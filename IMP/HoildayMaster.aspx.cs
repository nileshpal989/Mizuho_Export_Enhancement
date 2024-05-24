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

public partial class IMP_HoildayMaster : System.Web.UI.Page
{
    string _NewValue;
    string _OldValues;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("../TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            if (!IsPostBack)
            {
                ddlrecordperpage.SelectedValue = "10";
                fillBranch();
                txtCurrCode.Focus();
                btnCur.Attributes.Add("onclick", "return curhelp();");
                btnSave.Attributes.Add("onclick", "return validateSave();");
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
    //protected void fillCurrency()
    //{
    //    TF_DATA objData = new TF_DATA();
    //    SqlParameter p1 = new SqlParameter("@Curr", SqlDbType.VarChar);
    //    p1.Value = "";
    //    string _query = "Tf_GetCurrencyDetails";
    //    DataTable dt = objData.getData(_query, p1);
    //    ddlBranch.Items.Clear();
    //    ListItem li = new ListItem();
    //    li.Value = "0";
    //    if (dt.Rows.Count > 0)
    //    {
    //        li.Text = "---Select---";
    //        ddlCurrency.DataSource = dt.DefaultView;
    //        ddlCurrency.DataTextField = "C_CODE";
    //        //ddlBranch.DataValueField = "AuthorizedDealerCode";
    //        ddlCurrency.DataBind();
    //    }
    //    else
    //        li.Text = "No record(s) found";

    //    ddlCurrency.Items.Insert(0, li);
    //    //ddlBranch.SelectedIndex = 1;
    //    //ddlBranch_SelectedIndexChanged(null, null);
    //    //fillGrid();
    //}
    //protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlCurrency.SelectedValue == "INR")
    //    {
    //        ddlBranch.Enabled = true;
    //        fillGrid();
    //    }
    //    else
    //    {
    //        ddlBranch.Enabled = false;
    //        fillGrid();
    //    }
    //}
    protected void txtToolTip_TextChanged(object sender, EventArgs e)
    {
        GridViewSpecialDates.Visible = true;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string _year = "", _Mon = "", _Day = "", _txholydaytid = txtCurrCode.Text;
        TF_DATA dtobj = new TF_DATA();
        SqlParameter CurrCode = new SqlParameter("@CurrCode", txtCurrCode.Text);
        SqlParameter pYear = new SqlParameter("@year", SqlDbType.VarChar);
        _year = (txtHolidayDate.Text).Substring(txtHolidayDate.Text.IndexOf("/", txtHolidayDate.Text.IndexOf("/") + 1) + 1, 4);
        pYear.Value = _year;

        SqlParameter pMon = new SqlParameter("@mon", SqlDbType.VarChar);
        _Mon = (txtHolidayDate.Text).Substring(txtHolidayDate.Text.IndexOf("/") + 1, 2);
        pMon.Value = _Mon;

        SqlParameter pDay = new SqlParameter("@day", SqlDbType.VarChar);
        _Day = (txtHolidayDate.Text).Substring(0, 2);
        pDay.Value = _Day;

        SqlParameter BranchAdCode = new SqlParameter("@BranchAdCode", ddlBranch.SelectedValue.ToString());
        SqlParameter HoliDayDesc = new SqlParameter("@HoliDayDesc", txtToolTip.Text.ToString());
        SqlParameter pUser = new SqlParameter("@user", _userName);
        SqlParameter pUploadDate = new SqlParameter("@uploadDate", _uploadingDate);

        string _result = dtobj.SaveDeleteData("TF_IMP_AddEditHolidayData", CurrCode, BranchAdCode, pYear, pMon, pDay, HoliDayDesc, pUser, pUploadDate);


        SqlParameter A1 = new SqlParameter("@OldValues", SqlDbType.VarChar);
        SqlParameter A2 = new SqlParameter("@NewValues", SqlDbType.VarChar);
        SqlParameter A3 = new SqlParameter("@ModType", SqlDbType.VarChar);
        SqlParameter A4 = new SqlParameter("@ModifiedBy", Session["userName"].ToString().Trim());
        SqlParameter A5 = new SqlParameter("@ModifiedDate", "");
        SqlParameter A6 = new SqlParameter("@MenuName", "Holiday Dates Master");
        SqlParameter A7 = new SqlParameter("@Mode", SqlDbType.VarChar);
        SqlParameter A8 = new SqlParameter("@RefNo", SqlDbType.VarChar);

        if (_result == "added")
        {
            _NewValue = "Description : " + txtToolTip.Text.Trim() + " ; Holiday date : " + txtHolidayDate.Text.Trim();

            A1.Value = "";
            A2.Value = _NewValue;
            A3.Value = "IMP";
            A7.Value = "A";
            A8.Value = _txholydaytid;
            string p = dtobj.SaveDeleteData("TF_Mizuho_IMP_AuditTrail_master", A1, A2, A3, A4, A5, A6, A7, A8);


            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Record Added')", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "Alert('Record Added.');", true);
            fillGrid();
            clearData();
        }
        else
        {
            if (_result == "Updated")
            {
                _NewValue = "Description : " + txtToolTip.Text.Trim() + " ; Holiday date : " + txtHolidayDate.Text.Trim();
                _OldValues = "Description : " + hdnholidaydescription.Value + " ; Holiday date : " + hdnholidayidDate.Value;
                A1.Value = _OldValues;
                A2.Value = _NewValue;
                A3.Value = "IMP";
                A7.Value = "M";
                A8.Value = _txholydaytid;
                string p = dtobj.SaveDeleteData("TF_Mizuho_IMP_AuditTrail_master", A1, A2, A3, A4, A5, A6, A7, A8);


                //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Message", "alert('Record Added')", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "Alert('Record Updated.');", true);
                fillGrid();
                clearData();
                txtHolidayDate.ReadOnly = false;
                txtCurrCode.ReadOnly = false;
                ddlBranch.Enabled = true;
                btnSave.Text = "Save";
                btnCancel.Text = "Clear";
                btncalendar_DocDate.Enabled = true;
            }
            else
            {
                lblMessage.Text = _result;
            }
        }
    }
    protected void clearData()
    {
        lblMessage.Text = "";
        txtHolidayDate.Text = "";
        txtToolTip.Text = "";
        //txtCurrCode.Text = "";
        //lblCurrDesc.Text = "";
        //ddlCurrency.SelectedIndex = 0;
        //ddlBranch.SelectedIndex = 0;
        txtCurrCode.Focus();
    }
    protected void fillGrid()
    {
        GridViewSpecialDates.Visible = true;
        TF_DATA objData = new TF_DATA();
        string curr = txtCurrCode.Text;
        //string curr = ddlCurrency.SelectedValue;
        string adcode = ddlBranch.SelectedItem.Value;
        string _query = "TF_IMP_GetHoliDaysList";

        SqlParameter p1 = new SqlParameter("@adcode", SqlDbType.VarChar);
        p1.Value = adcode;
        SqlParameter p2 = new SqlParameter("@curr", SqlDbType.VarChar);
        p2.Value = curr;

        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewSpecialDates.PageSize = _pageSize;
            GridViewSpecialDates.DataSource = dt.DefaultView;
            GridViewSpecialDates.DataBind();
            GridViewSpecialDates.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewSpecialDates.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
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
            if (GridViewSpecialDates.PageCount != GridViewSpecialDates.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewSpecialDates.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewSpecialDates.PageIndex + 1) + " of " + GridViewSpecialDates.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewSpecialDates.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewSpecialDates.PageIndex != (GridViewSpecialDates.PageCount - 1))
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
        GridViewSpecialDates.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewSpecialDates.PageIndex > 0)
        {
            GridViewSpecialDates.PageIndex = GridViewSpecialDates.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewSpecialDates.PageIndex != GridViewSpecialDates.PageCount - 1)
        {
            GridViewSpecialDates.PageIndex = GridViewSpecialDates.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewSpecialDates.PageIndex = GridViewSpecialDates.PageCount - 1;
        fillGrid();
    }
    protected void GridViewSpecialDates_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string _Holidaydate = "";
        string _description = "";
        string _Curr = "";
        string _ADCode = "";
        string[] values_P;
        string AllID = e.CommandArgument.ToString();
        if (AllID != "")
        {
            char[] splitchar = { ';' };
            values_P = AllID.Split(splitchar);
            _Holidaydate = values_P[0].ToString();
            _description = values_P[1].ToString();
            _Curr = values_P[2].ToString();
            _ADCode = values_P[3].ToString();
        }
        hdnholidayidDate.Value = _Holidaydate;
        hdnholidaydescription.Value = _description;
        hdnADCode.Value = _ADCode;
        hdnCurrency.Value = _Curr;

        if (e.CommandName == "RemoveRecord")
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "confirm", "confirmDelete()", true);
        }
        if (e.CommandName == "EditRecord")
        {
            txtHolidayDate.Text = _Holidaydate;
            txtToolTip.Text = _description;
            ddlBranch.SelectedValue = _ADCode;
            txtHolidayDate.ReadOnly = true;
            txtCurrCode.ReadOnly = true;
            ddlBranch.Enabled = false;
            btnSave.Text = "Update";
            btnCancel.Text = "Clear";
            btncalendar_DocDate.Enabled = false;
        }

    }
    protected void GridViewSpecialDates_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSpecialDates = new Label();
            Label lblToolTip = new Label();

            Button btnDelete = new Button();
            Button btnEdit = new Button();
            lblSpecialDates = (Label)e.Row.FindControl("lblSpecialDates");
            lblToolTip = (Label)e.Row.FindControl("lblToolTip");

            btnDelete = (Button)e.Row.FindControl("btnDelete");

            btnDelete.Enabled = true;
            btnDelete.CssClass = "deleteButton";

            btnEdit = (Button)e.Row.FindControl("btnEdit");

            btnEdit.Enabled = true;
            btnEdit.CssClass = "deleteButton";
            //btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");

            string strToolTip = lblToolTip.Text;
            strToolTip = strToolTip.Replace("'", "\\'");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                //string pageurl = "window.location='TF_AddEditSpecialDates.aspx?mode=edit&sDate=" + lblSpecialDates.Text.Trim() + "&toolTip=" + strToolTip + "&ddlbranch=" + ddlBranch.SelectedItem.Text + "&adcode=" + ddlBranch.SelectedItem.Value + "'";
                if (i != 2)
                {


                }
                else
                {
                    cell.Style.Add("cursor", "default");
                }
                i++;
            }
        }
    }
    protected void GridViewSpecialDates_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtHolidayDate.Text = GridViewSpecialDates.SelectedRow.Cells[0].Text;
        txtToolTip.Text = GridViewSpecialDates.SelectedRow.Cells[1].Text;
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void txtHolidayDate_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@currCode", SqlDbType.VarChar);
        p1.Value = txtCurrCode.Text;

        SqlParameter p2 = new SqlParameter("@Adcode", SqlDbType.VarChar);
        if (txtCurrCode.Text == "INR")
        {
            p2.Value = ddlBranch.SelectedValue;
        }
        else
        {
            p2.Value = "";
        }

        SqlParameter p3 = new SqlParameter("@Date", SqlDbType.VarChar);
        p3.Value = txtHolidayDate.Text;

        DataTable dt = objData.getData("TF_IMP_GetDateDesc", p1, p2, p3);
        if (dt.Rows.Count > 0)
        {
            txtToolTip.Text = dt.Rows[0]["toolTip"].ToString();
            btnSave.Text = "Update";
        }
        else
        {
            btnSave.Text = "Save";
            txtToolTip.Text = "";
        }
    }
    protected void txtCurrCode_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@currCode", SqlDbType.VarChar);
        p1.Value = txtCurrCode.Text;

        DataTable dt = objData.getData("TF_IMP_GetCurrDesc", p1);
        if (dt.Rows.Count > 0)
        {
            txtCurrCode.Text = dt.Rows[0]["C_CODE"].ToString();
            lblCurrDesc.Text = dt.Rows[0]["C_DESCRIPTION"].ToString();
            if (txtCurrCode.Text == "INR")
            {
                ddlBranch.Enabled = true;
                lblMsg.Visible = true;
                fillGrid();
            }
            else
            {
                ddlBranch.SelectedIndex = 0;
                ddlBranch.Enabled = false;
                lblMsg.Visible = false;
                fillGrid();
            }
        }
        else
        {
            lblCurrDesc.Text = "Invalid Currency Code: " + txtCurrCode.Text;
            txtCurrCode.Text = "";
        }

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../IMP/IMP_Main.aspx", true);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("HoildayMaster.aspx", true);
    }
    protected void btnDeleteConfirm_Click(object sender, EventArgs e)
    {
        string result = "";
        string _hdnholidayid = hdnholidayidDate.Value;
        string adcode = hdnADCode.Value;
        string curr = hdnCurrency.Value;
        TF_DATA objData = new TF_DATA();
        string _query = "TF_IMP_deleteHolidays";

        SqlParameter pSDate = new SqlParameter("@sDate", SqlDbType.VarChar);
        pSDate.Value = _hdnholidayid;

        SqlParameter p1 = new SqlParameter("@adcode", SqlDbType.VarChar);
        p1.Value = adcode;

        SqlParameter p2 = new SqlParameter("@curr", SqlDbType.VarChar);
        p2.Value = curr;
        SqlParameter pusername = new SqlParameter("@userName", SqlDbType.VarChar);
        pusername.Value = Session["userName"].ToString();

        result = objData.SaveDeleteData(_query, pSDate, p1, p2, pusername);
        fillGrid();
        if (result == "deleted")
        {
            AuditTrailDeleted(hdnholidayidDate.Value, hdnholidaydescription.Value);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "deletemessage", "Alert('Record Deleted.');", true);
        }
        else
        {
            //lblMessage.Text = result;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "deletemessage", "Alert('" + result + "');", true);
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record can not be deleted as it is associated with another record.');", true);
        }

    }
    protected void AuditTrailDeleted(string _Holidaydate, string _description)
    {

        string _userName = Session["userName"].ToString();

        TF_DATA objdata = new TF_DATA();
        string _OldValues = "Description : " + _description + " ; Holiday date : " + _Holidaydate;

        SqlParameter A1 = new SqlParameter("@OldValues", SqlDbType.VarChar);
        SqlParameter A2 = new SqlParameter("@NewValues", SqlDbType.VarChar);
        SqlParameter A3 = new SqlParameter("@ModType", SqlDbType.VarChar);
        SqlParameter A4 = new SqlParameter("@ModifiedBy", Session["userName"].ToString().Trim());
        SqlParameter A5 = new SqlParameter("@ModifiedDate", "");
        SqlParameter A6 = new SqlParameter("@MenuName", "Holiday Dates Master");
        SqlParameter A7 = new SqlParameter("@Mode", SqlDbType.VarChar);
        SqlParameter A8 = new SqlParameter("@RefNo", SqlDbType.VarChar);
        A1.Value = _OldValues;
        A2.Value = "";
        A3.Value = "IMP";
        A7.Value = "D";
        A8.Value = _Holidaydate;
        string p = objdata.SaveDeleteData("TF_Mizuho_IMP_AuditTrail_master", A1, A2, A3, A4, A5, A6, A7, A8);

    }
}