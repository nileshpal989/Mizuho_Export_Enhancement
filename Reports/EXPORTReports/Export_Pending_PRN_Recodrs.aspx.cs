using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using ClosedXML.Excel;

public partial class Reports_EXPORTReports_Export_Pending_PRN_Recodrs : System.Web.UI.Page
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
                clearControls();
                rdbAllCustomer.Checked = true;
                fillddlBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = true;
                txtFromDate.Focus();
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                // btnSave.Attributes.Add("onclick", "return validateSave();");
                rdbDocWise.Visible = true;
                rdbAllCustomer.Visible = false;
                ////rdbSelectedCustomer.Visible = false;
                rdbAllCountry.Visible = false;
                //rdbSelectedCountry.Visible = false;
            }
            txtCustomerID.Attributes.Add("onkeydown", "return CustId(event)");
            btnCustList.Attributes.Add("onclick", "return custhelp()");
            txtCountry.Attributes.Add("onkeydown", "return CountryId(event)");
            btnCountryList.Attributes.Add("onclick", "return Countryhelp()");
        }
    }

    public void fillddlBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", SqlDbType.VarChar);
        p1.Value = "";
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "---Select---";
        ListItem li01 = new ListItem();
        li01.Value = "All Branches";
        if (dt.Rows.Count > 0)
        {
            //li.Text = "---Select---";

            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";
        ddlBranch.Items.Insert(0, li);

    }
    protected void clearControls()
    {
        txtFromDate.Text = "";

        txtFromDate.Focus();
        rdbDocWise.Checked = true;
    }
    public void fillCustomerIdDescription()
    {
        lblCustomerName.Text = "";
        string custid = txtCustomerID.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@custid", SqlDbType.VarChar);
        p1.Value = custid;
        string _query = "TF_rptGetCustomerMasterDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCustomerName.Text = dt.Rows[0]["CUST_NAME"].ToString().Trim();
        }
        else
        {
            txtCustomerID.Text = "";
            lblCustomerName.Text = "";
        }
    }

    public void fillCountryDescription()
    {
        lblCountyName.Text = "";

        string Countryid = txtCountry.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@C_Code", SqlDbType.VarChar);
        p1.Value = Countryid;

        string _query = "HelpCurMstr1";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCountyName.Text = dt.Rows[0]["C_Description"].ToString().Trim();
        }
        else
        {
            txtCountry.Text = "";
            lblCountyName.Text = "";
        }
    }

    protected void btnCustId_Click(object sender, EventArgs e)
    {
        if (hdnCustId.Value != "")
        {
            txtCustomerID.Text = hdnCustId.Value;
            fillCustomerIdDescription();
            txtCustomerID.Focus();
        }
    }
    protected void rdbDocWise_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbDocWise.Checked == true)
        {
            rdbAllCustomer.Visible = false;
            //rdbSelectedCustomer.Visible = false;
            //rdbSelectedCustomer.Checked = false;
            Custlist.Visible = false;
            rdbAllCountry.Visible = false;
            //rdbSelectedCountry.Visible = false;
            Custlist.Visible = false;
            CountryList.Visible = false;
        }
        rdbDocWise.Focus();
    }
    protected void rdbCustWise_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbCustWise.Checked == true)
        {
            Custlist.Visible = true;
            txtCustomerID.Visible = true;
            lblCustomerName.Visible = true;
            btnCustList.Visible = true;
            rdbAllCustomer.Visible = false;
            txtCustomerID.Text = "";
            lblCustomerName.Text = "Customer Name";
            //rdbSelectedCustomer.Checked = false;
            rdbAllCustomer.Checked = false;
            //rdbSelectedCustomer.Visible = true;
            //Custlist.Visible = false;
            rdbAllCountry.Visible = false;
            //rdbSelectedCountry.Visible = false;
            CountryList.Visible = false;
        }
        rdbCustWise.Focus();
    }
    protected void rdbAllCustomer_CheckedChanged(object sender, EventArgs e)
    {
        rdbAllCustomer.Checked = true;
        Custlist.Visible = false;
        rdbAllCustomer.Focus();
    }
    protected void rdbSelectedCustomer_CheckedChanged(object sender, EventArgs e)
    {
        Custlist.Visible = true;
        txtCustomerID.Text = "";
        lblCustomerName.Text = "Customer Name";
        //rdbSelectedCustomer.Focus();
        txtCustomerID.Visible = true;
        btnCustList.Visible = true;
        lblCustomerName.Visible = true;
        //rdbSelectedCustomer.Focus();
    }
    protected void txtCustomerID_TextChanged1(object sender, EventArgs e)
    {
        fillCustomerIdDescription();
        txtCustomerID.Focus();
    }
    protected void txtCountry_TextChanged(object sender, EventArgs e)
    {
        fillCountryDescription();
        txtCountry.Focus();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Focus();
        labelMessage.Text = "";
        txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
    }
    protected void rdbCountrywise_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbCountrywise.Checked == true)
        {
            CountryList.Visible = true;
            rdbAllCountry.Checked = false;
            txtCountry.Text = "";
            lblCountyName.Text = "Currency Name";
            txtCountry.Visible = true;
            btnCountryList.Visible = true;
            lblCountyName.Visible = true;
            //rdbSelectedCountry.Checked = false;
            rdbAllCountry.Visible = false;
            //rdbSelectedCountry.Visible = true;
            rdbAllCustomer.Visible = false;
            //rdbSelectedCustomer.Visible = false;
            Custlist.Visible = false;
        }
        else
        {
            rdbAllCountry.Visible = false;
            //rdbSelectedCountry.Visible = false;
        }
        rdbCountrywise.Focus();
    }
    protected void rdbAllCountry_CheckedChanged(object sender, EventArgs e)
    {
        rdbAllCountry.Checked = true;
        CountryList.Visible = false;
        rdbAllCountry.Focus();
    }
    protected void rdbSelectedCountry_CheckedChanged(object sender, EventArgs e)
    {
        CountryList.Visible = true;
        txtCountry.Text = "";
        lblCountyName.Text = "Currency Name";
        txtCountry.Visible = true;
        btnCountryList.Visible = true;
        lblCountyName.Visible = true;
        //rdbSelectedCountry.Focus();
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedItem.Text == "---Select---")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select branch.')", true);
        }
        else if (txtFromDate.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select from doc date.')", true);
        }
        else if (txtToDate.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select To doc date.')", true);
        }
        else if (rdbCustWise.Checked == true && txtCustomerID.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select customer A/c No.')", true);
        }
        else if (rdbCountrywise.Checked == true && txtCountry.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select currency.')", true);
        }
        else
        {
            System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            dateInfo.ShortDatePattern = "dd/MM/yyyy";
            DateTime From_Date = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo);
            DateTime To_Date = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);
            string type = "", custAcNo = "", Curr = "";

            TF_DATA objdata = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@branchnm", ddlBranch.SelectedItem.Text);
            SqlParameter p2 = new SqlParameter("@fromdate", txtFromDate.Text);
            SqlParameter p3 = new SqlParameter("@todate", txtToDate.Text);

            if (rdbDocWise.Checked == true)
            {
                type = "DocWise";
                custAcNo = "";
                Curr = "";
            }

            if (rdbCustWise.Checked == true)
            {
                type = "CustWise";
                custAcNo = txtCustomerID.Text;
                Curr = "";
            }

            if (rdbCountrywise.Checked == true)
            {
                type = "CurrWise";
                custAcNo = "";
                Curr = txtCountry.Text;
            }

            SqlParameter p4 = new SqlParameter("@type", type);
            SqlParameter p5 = new SqlParameter("@CustAcNo", custAcNo);
            SqlParameter p6 = new SqlParameter("@Curr", Curr);
            string script = "TF_Export_Report_Pending_PRN_Records";
            DataTable dt = objdata.getData(script, p1, p2, p3, p4, p5, p6);

            if (dt.Rows.Count > 0)
            {
                int[] columnsToCenter = { 2, 10, 11 };
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var sheet = wb.Worksheets.Add(dt, "Pending_PRN_Records");
                    foreach (var columnIndex in columnsToCenter)
                    {
                        foreach (var row in sheet.RowsUsed())
                        {
                            row.Cell(columnIndex + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        }
                    }
                    sheet.Table("Table1").ShowAutoFilter = false;
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=TF_Export_Pending_PRN_Records_" + System.DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }

            }

            else
            {
                txtFromDate.Text = "";
                txtFromDate.Focus();
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('No records found for this date range.')", true);
            }
        }

    }
}