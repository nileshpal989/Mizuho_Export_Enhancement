using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using ClosedXML.Excel;
using System.IO;

public partial class Reports_EXPORTReports_rpt_Pending_ROD : System.Web.UI.Page
{
    TF_DATA objdata = new TF_DATA();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
        {
            System.Web.UI.HtmlControls.HtmlInputHidden lbl = (System.Web.UI.HtmlControls.HtmlInputHidden)Menu1.FindControl("hdnloginid");

            Response.Redirect("~/TF_Login.aspx?sessionout=yes&sessionid=" + lbl.Value, true);
        }
        else
        {
            txtCustomer.Attributes.Add("onkeydown", "return CustId(event)");
            BtnCustList.Attributes.Add("onclick", "return Custhelp()");
           
            txtCountry.Attributes.Add("onkeydown", "return CountryId(event)");
            btnCountryList.Attributes.Add("onclick", "return Countryhelp()");
            if (!IsPostBack)
            {
                clearControls();
                rdbDocumnetwise.Checked = true;
                ddlBranch.Focus();
                fillddlBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = true;
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                rdbDocumnetwise.Visible = true;
                txtFromDate.Focus();
                //btnGenerate.Attributes.Add("onclick", "return validateSave();");
            }
            
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
        li.Value = "0";
        ListItem li01 = new ListItem();
        li.Value = "1";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
          //  li01.Text = "All Branches";

            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
       // ddlBranch.Items.Insert(1, li01);
    }

    protected void clearControls()
    {
        txtFromDate.Text = "";
        txtFromDate.Focus();
        rdbDocumnetwise.Checked = true;
    }
    public void fillCustomerIdDescription()
    {
        lblCustomerName.Text = "";
        string custid = txtCustomer.Text.Trim();
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
            txtCustomer.Text = "";
            lblCustomerName.Text = "";
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Focus();
        labelMessage.Text = "";
        txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
    }

    protected void rdbDocumnetwise_CheckedChanged(object sender, EventArgs e)
    {
        {
            if (rdbDocumnetwise.Checked == true)
            {
                CountryList.Visible = false;
                txtCountry.Text = "";
                CustList.Visible = false;
                txtCustomer.Text = "";
            }
            rdbDocumnetwise.Focus();
        }
    }
 
    protected void rdbCustomerwise_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbCustomerwise.Checked == true)
        {
            CountryList.Visible = false;
            txtCountry.Text = "";
            CustList.Visible = true;
            txtCustomer.Text = "";
            lblCustomerName.Text = "Customer Name";
            txtCustomer.Visible = true;
            BtnCustList.Visible = true;
            lblCustomerName.Visible = true;
            CountryList.Visible = false;
        }
        else
        {
            CountryList.Visible = false;
            txtCountry.Text = "";
        }
        rdbCustomerwise.Focus();
    }
    protected void txtCustomer_TextChanged(object sender, EventArgs e)
    {
        fillCustomerIdDescription();
        txtCustomer.Focus();
    } 
    protected void rdbCountrywise_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbCountrywise.Checked == true)
        {
            CustList.Visible = false;
            txtCustomer.Text = "";
            CountryList.Visible = true;
            txtCountry.Text = "";
            lblCountyName.Text = "Currency Name";
            txtCountry.Visible = true;
            btnCountryList.Visible = true;
            lblCountyName.Visible = true;
        }
        else
        {
            CustList.Visible = false;
            txtCustomer.Text = "";
        }
        rdbCountrywise.Focus();
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
    protected void txtCountry_TextChanged(object sender, EventArgs e)
    {
        fillCountryDescription();
        txtCountry.Focus();
    }
    protected void btnGenerate_Click(object sender, System.EventArgs e)
    {
        if (ddlBranch.SelectedItem.Text == "---Select---")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select branch.')", true);
        }
        else if(txtFromDate.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select from doc date.')", true);
        }
        else if (txtToDate.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select To doc date.')", true);
        }
        else if (rdbCustomerwise.Checked == true && txtCustomer.Text=="")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select customer A/c No.')", true);
        }
        else if (rdbCountrywise.Checked == true && txtCountry.Text=="")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "alert('Please select currency.')", true);
        }
        else
        {
            System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            dateInfo.ShortDatePattern = "dd/MM/yyyy";
            DateTime From_Date = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo);
            DateTime To_Date = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);

            string typeR="", Custtype="", Currtype = "";
            SqlParameter p1 = new SqlParameter("@Branch", ddlBranch.SelectedItem.Text);
            SqlParameter p2 = new SqlParameter("@startdate", txtFromDate.Text);
            SqlParameter p3 = new SqlParameter("@enddate", txtToDate.Text);

            //====DOC Wise====//
            if (rdbDocumnetwise.Checked == true)
            {
                typeR = "DocW";
                Custtype = "ALL";
                Currtype = "ALL";
            }
            //====Customer Wise====//
            if (rdbCustomerwise.Checked == true)
            {
                typeR = "Cust";
                Custtype = txtCustomer.Text;
                Currtype = "ALL";
            }
            //====Currency Wise====//
            if (rdbCountrywise.Checked == true)
            {
                typeR = "Curr";
                Custtype = "ALL";
                Currtype = txtCountry.Text;
            }


            SqlParameter p4 = new SqlParameter("@Type", typeR);
            SqlParameter p5 = new SqlParameter("@Cust", Custtype);
            SqlParameter p6 = new SqlParameter("@Curr", Currtype);


            string script = "TF_rpt_EXPPending_ROD";
            DataTable dt = objdata.getData(script, p1, p2, p3, p4, p5, p6);
            if (dt.Rows.Count > 0)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "Pending_ROD");

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=TF_Export_Pending_ROD_Records" + System.DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xlsx");
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