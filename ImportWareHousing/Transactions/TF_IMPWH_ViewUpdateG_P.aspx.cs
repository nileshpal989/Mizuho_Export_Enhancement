using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Net;
using System.Collections;

public partial class ImportWareHousing_Transactions_TF_IMPWH_ViewUpdateG_P : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        String sMacAddress = string.Empty;
        foreach (NetworkInterface adapter in nics)
        {
            if (sMacAddress == String.Empty)// only return MAC Address from first card  
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                sMacAddress = adapter.GetPhysicalAddress().ToString();
            }
        }
        // To Get IP Address
        if (!IsPostBack)
        {
            ddlrecordperpage.SelectedValue = "10";
            if (Session["userName"] == null)
            {
                System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

                Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
            }
            fillBranch();
            ddlBranch.SelectedValue = Session["userADCode"].ToString();
            ddlBranch.Enabled = false;
            btnCustHelp.Attributes.Add("Onclick", "return CustHelp();");
            btnSearch.Attributes.Add("Onclick", "return Validate();");
            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            rowGrid.Visible = false;
            rowPager.Visible = false;
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
    }
    protected void fillgrid()
    {
        TF_DATA objget = new TF_DATA();
        string search = txtSearch.Text.Trim();
        string CustAcNo = txtCustACNo.Text.Trim();
        string FromDate = txtFromDate.Text;
        SqlParameter p1 = new SqlParameter("@ADCode", SqlDbType.VarChar);
        p1.Value = ddlBranch.SelectedValue.ToString();
        SqlParameter p2 = new SqlParameter("@search", SqlDbType.VarChar);
        p2.Value = search;
        SqlParameter p3 = new SqlParameter("@CustAcNo", SqlDbType.VarChar);
        p3.Value = CustAcNo;
        SqlParameter p4 = new SqlParameter("@AsOnDate", SqlDbType.VarChar);
        p4.Value = FromDate;
        DataTable dt = objget.getData("TF_IMPWH_FillGridG_D", p1, p2, p3, p4);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _PageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewInvoice.PageSize = _PageSize;
            GridViewInvoice.DataSource = dt.DefaultView;
            GridViewInvoice.DataBind();
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _PageSize);
            GridViewInvoice.Enabled = true;
            GridViewInvoice.Visible = true;
        }
        else
        {
            GridViewInvoice.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No Record(s) Found...";
            labelMessage.Visible = true;
        }
    }
    protected void txtCustACNo_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@accno", SqlDbType.VarChar);
        p1.Value = txtCustACNo.Text;
        string _query = "Get_Customer_Name_BY_AccNo";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblcustname.Text = dt.Rows[0]["CUST_NAME"].ToString();
            fillgrid();
        }
        else
        {
            txtCustACNo.Text = "";
            lblcustname.Text = "";
            txtCustACNo.Focus();
        }
    }
    protected void btnApproval_Click(object sender, EventArgs e)
    {
        Int32 gridrows = GridViewInvoice.Rows.Count;
        string xyz = gridrows.ToString();
        for (Int32 i = 0; i < gridrows; i++)
        {
            string aprovestatus = "";
            string username = Session["userName"].ToString();
            CheckBox chk = (CheckBox)GridViewInvoice.Rows[i].FindControl("RowChkAllow");
            Label ORDERID = (Label)GridViewInvoice.Rows[i].FindControl("lblOrderID");
            SqlParameter p1 = new SqlParameter("@approve", SqlDbType.VarChar);
            if (chk.Checked)
            {
                p1.Value = "A";
                aprovestatus = "Processed";
            }
            TF_DATA objData = new TF_DATA();
            SqlParameter p2 = new SqlParameter("@orderIDNo", SqlDbType.VarChar);
            p2.Value = ORDERID.Text.Trim();
            SqlParameter p3 = new SqlParameter("@approveBY", SqlDbType.VarChar);
            p3.Value = username;
            SqlParameter p4 = new SqlParameter("@ApproveDate", SqlDbType.DateTime);
            p4.Value = System.DateTime.Now;
            string result = objData.SaveDeleteData("UpdateChkmaker", p1, p2, p3, p4);
            if (result == "updated")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Processed Successfully');", true);
            }
        }
        fillgrid();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ADII_Doc_Maker.aspx", true);
    }
    protected void GridViewInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = e.Row.DataItem as DataRowView;
            if (drv["Status"].ToString().Equals("D"))
            {
                e.Row.BackColor = System.Drawing.Color.Red;
                e.Row.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                e.Row.BackColor = System.Drawing.Color.LightGreen;
                e.Row.ForeColor = System.Drawing.Color.Navy;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow && GridViewInvoice.EditIndex == e.Row.RowIndex)
        {
            DropDownList ddlCities = (DropDownList)e.Row.FindControl("DDLStatus");
            Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            string selectedStatus = lblStatus.Text.ToString();
            if (selectedStatus.Trim() != "")
            {
                ddlCities.Items.FindByValue(selectedStatus).Selected = true;
            }
            else
            {
                selectedStatus = "G";
                ddlCities.Items.FindByValue(selectedStatus).Selected = true;
            }            
        }
    }
    protected void btnnavfirst_Click(object sender, EventArgs e)
    {
        GridViewInvoice.PageIndex = 0;
        fillgrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewInvoice.PageIndex > 0)
        {
            GridViewInvoice.PageIndex = GridViewInvoice.PageIndex - 1;
        }
        fillgrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewInvoice.PageIndex != GridViewInvoice.PageCount - 1)
        {
            GridViewInvoice.PageIndex = GridViewInvoice.PageIndex + 1;
        }
        fillgrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewInvoice.PageIndex = GridViewInvoice.PageCount - 1;
        fillgrid();
    }
    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();

        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewInvoice.PageCount != GridViewInvoice.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewInvoice.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) :" + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewInvoice.PageIndex + 1) + " of " + GridViewInvoice.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewInvoice.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }

        if (GridViewInvoice.PageIndex != (GridViewInvoice.PageCount - 1))
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
    protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void UpdateCommision(object sender, GridViewUpdateEventArgs e)
    {
        string PD_Status = ((DropDownList)(GridViewInvoice.Rows[e.RowIndex].Cells[1].FindControl("DDLStatus"))).SelectedValue;
        string IECode = GridViewInvoice.DataKeys[e.RowIndex].Values[0].ToString();
        string BOENo = GridViewInvoice.DataKeys[e.RowIndex].Values[1].ToString();
        string BOEDate = GridViewInvoice.DataKeys[e.RowIndex].Values[2].ToString();
        string InvoiceNo = GridViewInvoice.DataKeys[e.RowIndex].Values[3].ToString();
        string PortCode = GridViewInvoice.DataKeys[e.RowIndex].Values[4].ToString();
        string OldStatus = GridViewInvoice.DataKeys[e.RowIndex].Values[5].ToString();
        string SupplierName = GridViewInvoice.DataKeys[e.RowIndex].Values[6].ToString();

        TF_DATA Dataobj = new TF_DATA();
        String Query = "TF_IMPWH_UpdatePD_StatusDump";

        SqlParameter S1 = new SqlParameter("@IECode", IECode);
        SqlParameter S2 = new SqlParameter("@BOENo", BOENo);
        SqlParameter S3 = new SqlParameter("@BOEDate", BOEDate);
        SqlParameter S4 = new SqlParameter("@InvoiceNo", InvoiceNo);
        SqlParameter S5 = new SqlParameter("@PortCode", PortCode);
        SqlParameter S6 = new SqlParameter("@PD_Status", PD_Status);

        String Result = Dataobj.SaveDeleteData(Query, S1, S2, S3, S4, S5, S6);
        if (PD_Status != OldStatus)
        {
            AuditTrail(IECode, BOENo, BOEDate, InvoiceNo, PortCode, OldStatus, PD_Status, SupplierName);
        }
        GridViewInvoice.EditIndex = -1;
        fillgrid();
    }
    protected void CancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridViewInvoice.EditIndex = -1;
        fillgrid();
    }
    protected void EditCommision(object sender, GridViewEditEventArgs e)
    {
        GridViewInvoice.EditIndex = e.NewEditIndex;
        fillgrid();
    }
    public void AuditTrail(string IECode, string BOENo, string BOEDate, string InvoiceNo, string PortCode, string OldStatus, string NewStatus, string SupplierName)
    {
        TF_DATA obj = new TF_DATA();
        string _query = "TF_IMPWH_AuditTrail";
        string _OldValues = "Supplier: " + SupplierName + ";BOE No: " + BOENo + ";BOE Date:" + BOEDate + ";Invoice No:" + InvoiceNo;
        SqlParameter Branch = new SqlParameter("@BranchCode", SqlDbType.VarChar);
        Branch.Value = ddlBranch.SelectedItem.ToString();
        SqlParameter Mod = new SqlParameter("@ModType", SqlDbType.VarChar);
        Mod.Value = "IMPWH";
        SqlParameter oldvalues = new SqlParameter("@OldValues", SqlDbType.VarChar);
        oldvalues.Value = _OldValues;
        SqlParameter newvalues = new SqlParameter("@NewValues", SqlDbType.VarChar);
        newvalues.Value = NewStatus;
        SqlParameter Acno = new SqlParameter("@CustAcNo", SqlDbType.VarChar);
        Acno.Value = txtCustACNo.Text;
        SqlParameter DocumentNo = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
        DocumentNo.Value = "";
        SqlParameter FWDContractNo = new SqlParameter("@FWD_Contract_No", SqlDbType.VarChar);
        FWDContractNo.Value = "";
        SqlParameter DocumnetDate = new SqlParameter("@DocumentDate", SqlDbType.VarChar);
        DocumnetDate.Value = "";
        SqlParameter Mode = new SqlParameter("@Mode", "U");
        SqlParameter user = new SqlParameter("@ModifiedBy", SqlDbType.VarChar);
        user.Value = Session["UserName"].ToString();
        string _moddate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        SqlParameter moddate = new SqlParameter("@ModifiedDate", SqlDbType.VarChar);
        moddate.Value = _moddate;
        //string _type = "A";
        //SqlParameter type = new SqlParameter("@type", SqlDbType.VarChar);
        //type.Value = _type;
        string _menu = "Invoice Status Updation";
        SqlParameter menu = new SqlParameter("@MenuName", SqlDbType.VarChar);
        menu.Value = _menu;
        string at = obj.SaveDeleteData(_query, Branch, Mod, oldvalues, newvalues, Acno, DocumentNo, FWDContractNo, DocumnetDate, Mode, user, moddate, menu);
    }
}
