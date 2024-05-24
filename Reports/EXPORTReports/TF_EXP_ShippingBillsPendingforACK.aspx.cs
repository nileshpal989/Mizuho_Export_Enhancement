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

public partial class Reports_EXPORTReports_TF_EXP_ShippingBillsPendingforACK : System.Web.UI.Page
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
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            if (!IsPostBack)
            {
                fillddlBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                btnSave.Attributes.Add("onclick", "return validateSave();");
            }
            txtCustomerID.Attributes.Add("onkeydown", "return CustId(event)");
            btnCustList.Attributes.Add("onclick", "return custhelp()");
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
        ListItem li01 = new ListItem();
        li01.Value = "All Branches";
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
   
    protected void btnCustId_Click(object sender, EventArgs e)
    {
        if (hdnCustId.Value != "")
        {
            txtCustomerID.Text = hdnCustId.Value;
            fillCustomerIdDescription();
            txtCustomerID.Focus();
        }
    }
    protected void rdbCustomerWise_CheckedChanged(object sender, EventArgs e)
    {
        rdbAllCustomerWise.Visible = true;
        rdbSelectedCustomer.Visible = true;
        Custlist.Visible = false; 
        rdbAllCustomerWise.Checked = true;
    }
    protected void rdbAllCustomerWise_CheckedChanged(object sender, EventArgs e)
    {
        rdbCustomerWise.Checked = true;
        Custlist.Visible = false;
    }
    protected void rdbSelectedCustomer_CheckedChanged(object sender, EventArgs e)
    {
        rdbCustomerWise.Checked = true;
        Custlist.Visible = true;
        txtCustomerID.Text = "";
        lblCustomerName.Text = "Customer Name";
        rdbSelectedCustomer.Focus();
        txtCustomerID.Visible = true;
        btnCustList.Visible = true;
        lblCustomerName.Visible = true;
        rdbSelectedCustomer.Focus();
    }


    protected void txtCustomerID_TextChanged1(object sender, EventArgs e)
    {
        fillCustomerIdDescription();
        txtCustomerID.Focus();
    }
   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string CurrentDate = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        if (ddlBranch.SelectedValue == "---Select---")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Please Select Branch.');", true);
        }
        else if (txtToDate.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Please select As on Date.');", true);
        }
        else if (rdbSelectedCustomer.Checked == true && txtCustomerID.Text.ToString() == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Enter Customer A/c No.');", true);
        }
        else
        {
            string Wise = "";

            if (rdbAllCustomerWise.Checked == true)
            {
                Wise = "All";
            }
            if (rdbSelectedCustomer.Checked == true)
            {
                Wise = txtCustomerID.Text.Trim();
            }
            TF_DATA objdata = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@Branch", ddlBranch.SelectedItem.Text);
            SqlParameter p2 = new SqlParameter("@startdate", txtToDate.Text.ToString());
            SqlParameter p3 = new SqlParameter("@Wise", Wise);
            string script = "TF_ShippingBillsPendingforAck";
            DataTable dt = objdata.getData(script, p1, p2, p3);
            if (dt.Rows.Count > 0)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt, "Data_CheckLsit");
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=TF_ShippingBillsPendingforAckReport" + Convert.ToDateTime(CurrentDate) + ".xlsx");
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
                ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('No Records.');", true);
            }
        }
    }     
}