using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class EDPMS_EDPMS_ViewEDPMSData : System.Web.UI.Page
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
                hdnUserRole.Value = Session["userRole"].ToString().Trim();
             
                ddlrecordperpage.SelectedValue = "20";

                if (Request.QueryString["result"] != null)
                {
                    if (Request.QueryString["result"].Trim() == "DocUpdated")
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
                    }
                    //if (Request.QueryString["result"].Trim() == "Updated")
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Record Updated.');", true);
                    //}

                    ddlBranch.SelectedValue = Request.QueryString["Branch"];
                }
                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;
                fillGrid();

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
            if (GridViewEXPbillEntry.PageCount != GridViewEXPbillEntry.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewEXPbillEntry.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewEXPbillEntry.PageIndex + 1) + " of " + GridViewEXPbillEntry.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewEXPbillEntry.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewEXPbillEntry.PageIndex != (GridViewEXPbillEntry.PageCount - 1))
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
        GridViewEXPbillEntry.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewEXPbillEntry.PageIndex > 0)
        {
            GridViewEXPbillEntry.PageIndex = GridViewEXPbillEntry.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewEXPbillEntry.PageIndex != GridViewEXPbillEntry.PageCount - 1)
        {
            GridViewEXPbillEntry.PageIndex = GridViewEXPbillEntry.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewEXPbillEntry.PageIndex = GridViewEXPbillEntry.PageCount - 1;
        fillGrid();
    }

    protected void GridViewEXPbillEntry_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string result = "";
        string _userName = Session["userName"].ToString().Trim();
        string _uploadingDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string ShippingBillNo = e.CommandArgument.ToString();
        string _docType = "";
        if (rdbROD.Checked == true)
            _docType = "DocBill";
        else if (rdbPRN.Checked == true)
            _docType = "Realized";

        SqlParameter p1 = new SqlParameter("@ShippingBillNo", ShippingBillNo);
        SqlParameter p2 = new SqlParameter("@DocumentType", _docType);
        SqlParameter p3 = new SqlParameter("@user", _userName);
        SqlParameter p4 = new SqlParameter("@uploadeddate", _uploadingDate);

        string _query = "TF_EDPMS_DeleteEDPMS_processedData";
        TF_DATA objData = new TF_DATA();
        result = objData.SaveDeleteData(_query, p1, p2,p3,p4);
        fillGrid();
        if (result == "deleted")
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Deleted.');", true);
        else
            labelMessage.Text = result;
    }

    protected void GridViewEXPbillEntry_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Button btnDelete = new Button();

            btnDelete = (Button)e.Row.FindControl("btnDelete");

            Label lblShippingBillNo = new Label();
             lblShippingBillNo = (Label)e.Row.FindControl("lblShippingBillNo");
             Label lblInvoice = new Label();
             lblInvoice =(Label)e.Row.FindControl("lblInvoice");

            btnDelete.Enabled = true;
            btnDelete.CssClass = "deleteButton";
            if (hdnUserRole.Value == "OIC")
                btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this record?');");
            else
            btnDelete.Attributes.Add("onclick", "alert('Only OIC can delete all records.');return false;");

            int i = 0;
            foreach (TableCell cell in e.Row.Cells)
            {
                string pageurl = "";
                if(rdbROD.Checked==true)
                pageurl = "window.location='EDPMS_AddEdit_EDPMSData.aspx?mode=edit&Branch=" + ddlBranch.SelectedItem.Text+ "&ADCode=" + ddlBranch.SelectedValue.Trim() + "&ShipBillNo=" + lblShippingBillNo.Text.Trim() + "'";
                else
                    pageurl = "window.location='EDPMS_AddEdit_EDPMSData_Realisation.aspx?mode=edit&Branch=" + ddlBranch.SelectedItem.Text + "&ADCode=" + ddlBranch.SelectedValue.Trim() + "&ShipBillNo=" + lblShippingBillNo.Text.Trim() + "&InvoiceNo=" + lblInvoice.Text.Trim()+ "'";
                               
                
                if (i != 8)
                    cell.Attributes.Add("onclick", pageurl);
                else
                    cell.Style.Add("cursor", "default");
                i++;
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    { fillGrid(); }

    protected void fillGrid()
    {
        string _docType = "";
        if (rdbROD.Checked == true)
            _docType = "DocBill";
        else if (rdbPRN.Checked == true)
            _docType = "Realized";
        SqlParameter p1 = new SqlParameter("@ADCode", ddlBranch.SelectedValue);
        SqlParameter p2 = new SqlParameter("@UploadDate", "");
        SqlParameter p3 = new SqlParameter("@DocumentType", _docType);
        SqlParameter p4 = new SqlParameter("@Search", txtSearch.Text.Trim());
        string _query = "TF_EDPMS_GetEDPMS_ProcessedData";

        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2, p3, p4);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewEXPbillEntry.PageSize = _pageSize;
            GridViewEXPbillEntry.DataSource = dt.DefaultView;
            GridViewEXPbillEntry.DataBind();
            GridViewEXPbillEntry.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewEXPbillEntry.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
        ddlBranch.Focus();
    }
    protected void rdbROD_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void rdbPRN_CheckedChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void txtUploadDate_TextChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
}



