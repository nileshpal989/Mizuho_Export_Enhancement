using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Reports_EXPReports_rptExportBillRealisationReport1 : System.Web.UI.Page
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

            txtCustomer.Attributes.Add("onkeydown", "return CustId(event)");
            BtnCustList.Attributes.Add("onclick", "return Custhelp()");
            txtDocNo.Attributes.Add("onkeydown", "return DocId(event)");
            btnDocLIst.Attributes.Add("onclick", "return dochelp()");

            if (!IsPostBack)
            {
                clearControls();
                rdbAllCustomer.Checked = true;
                ddlBranch.Focus();
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
               
                txtFromDate.Attributes.Add("onblur", "toDate();");
                btnSave.Attributes.Add("onclick", "return validateSave();");

                fillddlBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;
                PageHeader.Text = Request.QueryString["PageHeader"].ToString();

                rdbAllCustomer.Visible = true;
                rdbSelectedCustomer.Visible = true;
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
        li01.Value = "1";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            li01.Text = "All Branches";

            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
        ddlBranch.Items.Insert(1, li01);
    }

    protected void clearControls()
    {
        txtFromDate.Text = "";
        txtToDate.Text = "";
        txtFromDate.Focus();
    }

    protected void btnCustId_Click(object sender, EventArgs e)
    {
        if (hdnCustId.Value != "")
        {
            txtCustomer.Text = hdnCustId.Value;
            fillCustomerIdDescription();
            txtCustomer.Focus();
        }
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
    }


    protected void rdbAllCustomer_CheckedChanged(object sender, EventArgs e)
    {
        rdbAllCustomer.Checked = true;
        CustList.Visible = false;
        rdbAllCustomer.Focus();
        rdbAllDoc.Visible = false;
        rdbSelectedDoc.Visible = false;
        txtDocNo.Visible = false;
        lblDocumentNo.Visible = false;
        lblDocNo.Visible = false;
        btnDocLIst.Visible = false;
    }
    protected void rdbSelectedCustomer_CheckedChanged(object sender, EventArgs e)
    {
        CustList.Visible = true;
        txtCustomer.Text = "";
        lblCustomerName.Text = "Customer Name";
        txtCustomer.Visible = true;
        BtnCustList.Visible = true;
        lblCustomerName.Visible = true;
        rdbSelectedDoc.Checked = false;
        rdbAllDoc.Checked = true;
        rdbAllDoc.Visible = true;
        rdbSelectedDoc.Visible = true;
       
        rdbSelectedCustomer.Focus();
    }

    protected void txtCustomer_TextChanged(object sender, EventArgs e)
    {
        fillCustomerIdDescription();
        txtCustomer.Focus();
    }


    protected void rdbAllDoc_CheckedChanged(object sender, EventArgs e)
    {
        rdbAllDoc.Checked = true;    
        txtDocNo.Visible = false;
        txtDocNo.Text = "";
        lblDocumentNo.Visible = false;
        lblDocNo.Visible = false;
        btnDocLIst.Visible = false;
        
    }
    protected void rdbSelectedDoc_CheckedChanged(object sender, EventArgs e)
    {
        txtDocNo.Visible = true;
        lblDocumentNo.Visible = true;
        lblDocNo.Visible = true;
        btnDocLIst.Visible = true;
    }
    protected void txtDocNo_TextChanged(object sender, EventArgs e)
    {

    }
}
