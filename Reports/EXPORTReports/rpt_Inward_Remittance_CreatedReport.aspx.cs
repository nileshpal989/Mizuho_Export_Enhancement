using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ClosedXML.Excel;
using System.IO;

public partial class Reports_EXPORTReports_rpt_Inward_Remittance_CreatedReport : System.Web.UI.Page
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

                fillddlBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                // ddlBranch.Enabled = false;
                txtFromDate.Focus();
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                //rdbDocumnetwise.Checked = true;
                ddlBranch.Focus();
                btnSave.Attributes.Add("onclick", "return validateSave();");

            }
            // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "message", "toogleDisplay();", true);
            btnSave.Attributes.Add("onclick", "return Custhelp1();");
            txtCustomer.Attributes.Add("onkeydown", "return CustId(event)");
            BtnCustList.Attributes.Add("onclick", "return Custhelp()");
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
        if (dt.Rows.Count > 0)
        {
            li.Value = "---All Branch---";
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
    }

    protected void rdbDocumnetwise_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbDocumnetwise.Checked == true)
        {
            CustList.Visible = false;
        }
        rdbDocumnetwise.Focus();
    }

    public void rdbCustomerwise_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbCustomerwise.Checked == true)
        {
            CustList.Visible = true;
            txtCustomer.Text = "";
            lblCustomerName.Text = "Customer Name";
            txtCustomer.Visible = true;
            BtnCustList.Visible = true;
            lblCustomerName.Visible = true;
        }
    }

    protected void txtCustomer_TextChanged(object sender, EventArgs e)
    {
        fillCustomerIdDescription();
        txtCustomer.Focus();
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string CurrentDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
      
        if (ddlBranch.SelectedValue == "---Select---")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Please Select Branch.');", true);
        }
        else if (txtFromDate.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Please select From Date.');", true);
        }

        else if (rdbCustomerwise.Checked == true && txtCustomer.Text.ToString()=="")
        {
                ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Enter Customer A/c No.');", true);
        }
        else
        {
            string Type = "";
            if (rdbDocumnetwise.Checked == true)
            {
                Type = "IRM";
            }
            else
            {
                Type = txtCustomer.Text;
            }
            TF_DATA objdata = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@Branch", ddlBranch.SelectedItem.Text);
            SqlParameter p2 = new SqlParameter("@startdate", txtFromDate.Text);
            SqlParameter p3 = new SqlParameter("@enddate", txtToDate.Text);
            SqlParameter p4 = new SqlParameter("@Type", Type);
            string script = "TF_IRMCreatedReport";
            DataTable dt = objdata.getData(script, p1, p2, p3, p4);
            if (dt.Rows.Count > 0)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "Data_CheckLsit");
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=TF_IRMCreatedReport" + Convert.ToDateTime(CurrentDate) + ".xlsx");
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
                //Response.Write("<script>alert('No Records')</script>");
                ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('No Records.');", true);

            }
        }
        }
    }
