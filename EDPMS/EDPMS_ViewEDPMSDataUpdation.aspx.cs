using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public partial class EDPMS_EDPMS_ViewEDPMSDataUpdation : System.Web.UI.Page
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

                fillBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                //txtMonth.Text = System.DateTime.Now.Month.ToString("00");
                //txtYear.Text = System.DateTime.Now.Year.ToString("0000");
                //txtMonth.Focus();
                //txtMonth.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txtYear.Attributes.Add("onkeydown", "return validate_Number(event);");
                //txtMonth.Attributes.Add("onblur", "return Valid_Month();");
                //txtYear.Attributes.Add("onblur", "return Valid_Year();");
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
    protected void fillGrid()
    {
        
        SqlParameter p1 = new SqlParameter("@BranchName", ddlBranch.SelectedValue.Trim());
       // SqlParameter p2 = new SqlParameter("@YearMonth", txtYear.Text.ToString() + txtMonth.Text.ToString());
        SqlParameter p2 = new SqlParameter("@FromDate", txtfromDate.Text.Trim());
        SqlParameter p3 = new SqlParameter("@ToDate", txtToDate.Text.Trim());
        SqlParameter p4 = new SqlParameter("@Search", txtSearch.Text.Trim());
        SqlParameter p5 = new SqlParameter("@AD_BillPrefix", ddlTransType.SelectedValue.Trim());
        string _query = "TF_EDPMS_Get_ProcessedData_forUpdate";

        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2, p3,p4,p5);
        if (dt.Rows.Count > 0)
        {
            int _records = dt.Rows.Count;
            int _pageSize = Convert.ToInt32(ddlrecordperpage.SelectedValue.Trim());
            GridViewEDPMSUpdation.PageSize = _pageSize;
            GridViewEDPMSUpdation.DataSource = dt.DefaultView;
            GridViewEDPMSUpdation.DataBind();
            GridViewEDPMSUpdation.Visible = true;
            rowGrid.Visible = true;
            rowPager.Visible = true;
            labelMessage.Visible = false;
            pagination(_records, _pageSize);
        }
        else
        {
            GridViewEDPMSUpdation.Visible = false;
            rowGrid.Visible = false;
            rowPager.Visible = false;
            labelMessage.Text = "No record(s) found.";
            labelMessage.Visible = true;
        }
    }
    protected void pagination(int _recordsCount, int _pageSize)
    {
        TF_PageControls pgcontrols = new TF_PageControls();
        if (_recordsCount > 0)
        {
            navigationVisibility(true);
            if (GridViewEDPMSUpdation.PageCount != GridViewEDPMSUpdation.PageIndex + 1)
            {
                lblrecordno.Text = "Record(s) : " + (GridViewEDPMSUpdation.PageIndex + 1) * _pageSize + " of " + _recordsCount;
            }
            else
            {
                lblrecordno.Text = "Record(s) : " + _recordsCount + " of " + _recordsCount;
            }
            lblpageno.Text = "Page : " + (GridViewEDPMSUpdation.PageIndex + 1) + " of " + GridViewEDPMSUpdation.PageCount;
        }
        else
        {
            navigationVisibility(false);
        }

        if (GridViewEDPMSUpdation.PageIndex != 0)
        {
            pgcontrols.enablefirstnav(btnnavpre, btnnavfirst);
        }
        else
        {
            pgcontrols.disablefirstnav(btnnavpre, btnnavfirst);
        }
        if (GridViewEDPMSUpdation.PageIndex != (GridViewEDPMSUpdation.PageCount - 1))
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
        GridViewEDPMSUpdation.PageIndex = 0;
        fillGrid();
    }
    protected void btnnavpre_Click(object sender, EventArgs e)
    {
        if (GridViewEDPMSUpdation.PageIndex > 0)
        {
            GridViewEDPMSUpdation.PageIndex = GridViewEDPMSUpdation.PageIndex - 1;
        }
        fillGrid();
    }
    protected void btnnavnext_Click(object sender, EventArgs e)
    {
        if (GridViewEDPMSUpdation.PageIndex != GridViewEDPMSUpdation.PageCount - 1)
        {
            GridViewEDPMSUpdation.PageIndex = GridViewEDPMSUpdation.PageIndex + 1;
        }
        fillGrid();
    }
    protected void btnnavlast_Click(object sender, EventArgs e)
    {
        GridViewEDPMSUpdation.PageIndex = GridViewEDPMSUpdation.PageCount - 1;
        fillGrid();
    }
    protected void GridViewEDPMSUpdation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Button lb = (Button)e.CommandSource;

        float _txt_FOBmt = (float)Convert.ToDouble(((TextBox)lb.Parent.FindControl("txt_FOBmt")).Text);
        float _txt_FOBICmt = (float)Convert.ToDouble(((TextBox)lb.Parent.FindControl("txt_FOBICmt")).Text);
        float _txt_FreigAmt = (float)Convert.ToDouble(((TextBox)lb.Parent.FindControl("txt_FreigAmt")).Text);
        float _txt_FreigICAmt = (float)Convert.ToDouble(((TextBox)lb.Parent.FindControl("txt_FreigICAmt")).Text);
        float _txt_InsuranceAmt = (float)Convert.ToDouble(((TextBox)lb.Parent.FindControl("txt_InsuranceAmt")).Text);
        float _txt_InsuranceICAmt = (float)Convert.ToDouble(((TextBox)lb.Parent.FindControl("txt_InsuranceICAmt")).Text);

        string result = "";
        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { '$' });
        string _ADBillNo = commandArgs[0];
        string _ShippingBillNo = commandArgs[1];
        string _InvoiceNo = commandArgs[2];
        string _IRMNo = commandArgs[3];
        string _docType = "Realized";
        SqlParameter p0 = new SqlParameter("@IRMNo", _IRMNo);
        SqlParameter p1 = new SqlParameter("@ADCode", ddlBranch.SelectedValue);
        SqlParameter p2 = new SqlParameter("@DocumentType", _docType);
        SqlParameter p3 = new SqlParameter("@ADBillNo", _ADBillNo);
        SqlParameter p4 = new SqlParameter("@ShippingBillNo", _ShippingBillNo);
        SqlParameter p5 = new SqlParameter("@InvoiceNo", _InvoiceNo);
        SqlParameter p7 = new SqlParameter("@FOB_Amount", _txt_FOBmt);
        SqlParameter p8 = new SqlParameter("@Freight_Amount", _txt_FreigAmt);
        SqlParameter p9 = new SqlParameter("@Insurance_Amount", _txt_InsuranceAmt);
        SqlParameter p10 = new SqlParameter("@FOB_IC_Amount", _txt_FOBICmt);
        SqlParameter p11 = new SqlParameter("@Freight_IC_Amount", _txt_FreigICAmt);
        SqlParameter p12 = new SqlParameter("@Insurance_IC_Amount", _txt_InsuranceICAmt);
        string _query = "TF_EDPMS_Update_processedData";
        TF_DATA objData = new TF_DATA();
        result = objData.SaveDeleteData(_query, p0, p1, p2, p3, p4, p5, p7, p8, p9, p10, p11, p12);

        if (result == "Updated")
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "deletemessage", "alert('Record Updated.');", true);
        else
            labelMessage.Text = result;
        fillGrid();
    }
    protected void GridViewEDPMSUpdation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Button btnUpdate = new Button();

            btnUpdate = (Button)e.Row.FindControl("btnUpdate");

            //Label lblShippingBillNo = new Label();
            //lblShippingBillNo = (Label)e.Row.FindControl("lblShippingBillNo");
            //Label lblInvoice = new Label();
            //lblInvoice = (Label)e.Row.FindControl("lblInvoice");

            btnUpdate.Enabled = true;
            btnUpdate.CssClass = "deleteButton";
            //if (hdnUserRole.Value == "OIC")
            btnUpdate.Attributes.Add("onclick", "return confirm('Are you sure you want to Update this record?');");
            //else
            //    btnUpdate.Attributes.Add("onclick", "alert('Only OIC can Update the records.');return false;");
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
        ddlBranch.Focus();
    }
    protected void ddlrecordperpage_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGrid();
    }
       protected void TransType_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void txtfromDate_TextChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        fillGrid();
    }
}