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

public partial class IMP_TF_ImpAuto_ViewLocalMaster : System.Web.UI.Page
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
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "Alert('Record Added.');", true);
                }
                else
                    if (Request.QueryString["result"].Trim() == "updated")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "Alert('Record Updated.');", true);
                    }
            } txtSearch.Attributes.Add("onkeypress", "return submitForm(event);");
            btnSearch.Attributes.Add("onclick", "return validateSearch();");
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("TF_IMPAuto_AddEdit_LocalMaster.aspx?mode=add", true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    { fillGrid(); }
    protected void fillGrid()
    {

        string search = txtSearch.Text.Trim();
        SqlParameter p1 = new SqlParameter("@search", SqlDbType.VarChar);
        p1.Value = search;

        string _query = "TF_GetLocalBankMasterList";
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
    protected void GridViewBankCodeList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string str = e.CommandArgument.ToString();
        string[] values_p;
        if (str != "")
        {
            char[] splitchar = { ';' };
            values_p = str.Split(splitchar);
            hdnid.Value = values_p[0].ToString();
            hdnbankname.Value = values_p[1].ToString();
            hdnbankaddress.Value = values_p[2].ToString();
            hdncity.Value = values_p[3].ToString();
            hdnpincode.Value = values_p[4].ToString();
            hdncountry.Value = values_p[5].ToString();
            hdntelephone.Value = values_p[6].ToString();
            hdnfax.Value = values_p[7].ToString();
            hdnemail.Value = values_p[8].ToString();
            hdncontact.Value = values_p[9].ToString();

        }

        if (e.CommandName == "RemoveRecord")
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "confirm", "confirmDelete()", true);
        }
    }
    protected void GridViewBankCodeList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblbankCode = new Label();
            Label lblLinked = new Label();
            Button btnDelete = new Button();
            lblbankCode = (Label)e.Row.FindControl("lblbankCode");
            lblLinked = (Label)e.Row.FindControl("lblLinked");
            btnDelete = (Button)e.Row.FindControl("btnDelete");
              btnDelete.Enabled = true;
               btnDelete.CssClass = "deleteButton";
               string bankCode = lblbankCode.Text.Trim();
               bankCode = Server.UrlEncode(bankCode);
            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "window.location='TF_IMPAuto_AddEdit_LocalMaster.aspx?mode=edit&bankcode=" + bankCode + "'";
                if (i != 11)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }
    protected void btnDeleteConfirm_Click(object sender, EventArgs e)
    {
        string result = "";
        SqlParameter p1 = new SqlParameter("@bankCode ", SqlDbType.VarChar);
        p1.Value = hdnid.Value;

        string _query = "TF_DeleteLocalBankDetails";
        TF_DATA objData = new TF_DATA();
        result = objData.SaveDeleteData(_query, p1);
        fillGrid();
        if (result == "deleted")
        {
            AuditTrailDeleted(hdnid.Value.Trim(), hdnbankname.Value.Trim(), hdnbankaddress.Value.Trim(), hdncity.Value, hdnpincode.Value, hdncountry.Value, hdntelephone.Value
                , hdnfax.Value, hdnemail.Value, hdncontact.Value);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "deletemessage", "Alert('Record Deleted.');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "deletemessage", "Alert('Record can not be deleted as it is associated with another record.');", true);
        }
    }
    protected void AuditTrailDeleted(string BankID, string Name, string adress,string city,string pincode,string Country,string telephone,string fax,string email,string contact)
    {

        string _userName = Session["userName"].ToString();

        TF_DATA objdata = new TF_DATA();
        string _OldValues = "Bank ID : " + BankID + " ; Bank Name : " + Name + " ; Address : " + adress + " ; City : " + city
                + " ; Pincode : " + pincode + " ; Country : " + Country + " ; Telephone : " + telephone + " ; FaxNo : " + fax
                 + " ; EmailID : " + email + " ; Contact Person : " + contact;

        SqlParameter A1 = new SqlParameter("@OldValues", SqlDbType.VarChar);
        SqlParameter A2 = new SqlParameter("@NewValues", SqlDbType.VarChar);
        SqlParameter A3 = new SqlParameter("@ModType", SqlDbType.VarChar);
        SqlParameter A4 = new SqlParameter("@ModifiedBy", Session["userName"].ToString().Trim());
        SqlParameter A5 = new SqlParameter("@ModifiedDate", "");
        SqlParameter A6 = new SqlParameter("@MenuName", "Local Bank Master");
        SqlParameter A7 = new SqlParameter("@Mode", SqlDbType.VarChar);
        SqlParameter A8 = new SqlParameter("@RefNo", SqlDbType.VarChar);
        A1.Value = _OldValues;
        A2.Value = "";
        A3.Value = "IMP";
        A7.Value = "D";
        A8.Value = BankID;
        string p = objdata.SaveDeleteData("TF_Mizuho_IMP_AuditTrail_master", A1, A2, A3, A4, A5, A6, A7, A8);

    }
}
