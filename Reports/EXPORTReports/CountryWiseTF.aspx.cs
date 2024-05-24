using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Reports_EXPORTReports_CountryWiseTF : System.Web.UI.Page
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

            txtCustomerID.Attributes.Add("onkeydown", "return CustId(event)");
            btnCustList.Attributes.Add("onclick", "return custhelp()");


            if (!IsPostBack)
            {
                clearControls();
                rdbAllCustomer.Checked = true;
                ddlBranch.Focus();
                fillProcessingDate();

                txtFromDate.Attributes.Add("onkeypress", "return false;");

                txtFromDate.Attributes.Add("oncut", "return false;");

                txtFromDate.Attributes.Add("oncopy", "return false;");


                txtFromDate.Attributes.Add("onpaste", "return false;");

                //txtToDate.Attributes.Add("onfocus", "blur();");

                txtFromDate.Attributes.Add("oncontextmenu", "return false;");


                btnSave.Attributes.Add("onclick", "validateSave();");

                txtFromDate.Attributes.Add("onblur", "toDate();");
                //txtFromDate.Attributes.Add("onchange", "changeDate();");
                //btnChangeDate.Attributes.Add("Style", "display:none");

                btnSave.Attributes.Add("onclick", "return validateSave();");
                btnCustList.Attributes.Add("onclick", "return OpenCustList('mouseClick');");
                fillddlBranch();
                Doccode.Visible = false;
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
            ddlBranch.DataValueField = "BranchName";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
        ddlBranch.Items.Insert(1, li01);
    }
    public void fillCustomerIdDescription()
    {
        lblCustomerName.Text = "";
        string custid = txtCustomerID.Text.Trim();
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@cid", SqlDbType.VarChar);
        p1.Value = custid;
        string _query = "TF_GetCountryDetails";
        DataTable dt = objData.getData(_query, p1);
        if (dt.Rows.Count > 0)
        {
            lblCustomerName.Text = dt.Rows[0]["CountryName"].ToString().Trim();
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

    protected void clearControls()
    {
        txtFromDate.Text = "";
        txtFromDate.Focus();
        rdbAllCustomer.Checked = true;
    }

    protected void btnChangeDate_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";
        DateTime newDate = new DateTime();

        string frmDate = "";
        string toDate = "";

        if (txtFromDate.Text != "")
        {
            newDate = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo);
            frmDate = newDate.ToString("dd/MM/yyyy");
            //hdnFromDate.Value = frmDate.Trim();
        }

        if (txtFromDate.Text == "")
        {

        }
        else
        {
            DateTime nextDate = new DateTime();

            nextDate = System.DateTime.Now;

            toDate = nextDate.ToString("dd/MM/yyyy");
            //hdnToDate.Value = toDate.Trim();

        }
    }


    protected void fillProcessingDate()
    {
        if (Session["startdate"] != null)
        {
            txtFromDate.Text = Session["startdate"].ToString();
            hdnFromDate.Value = Session["startdateMM"].ToString();
        }

        if (Session["enddate"] != null)
        {
            hdnToDate.Value = Session["enddateMM"].ToString();
        }
    }

    protected void rdbAllCustomer_CheckedChanged(object sender, EventArgs e)
    {
        rdbAllCustomer.Checked = true;
        rdbAllCustomer.Focus();
        rdbSelectedCustomer.Checked = false;
        txtCustomerID.Visible = false;
        btnCustList.Visible = false;
        lblCustomerName.Visible = false;
        Doccode.Visible = false;

        txtCustomerID.Text = "";
        lblCustomerName.Text = "";
    }
    protected void rdbSelectedCustomer_CheckedChanged(object sender, EventArgs e)
    {
        rdbSelectedCustomer.Checked = true;
        rdbSelectedCustomer.Focus();
        txtCustomerID.Visible = true;
        btnCustList.Visible = true;
        lblCustomerName.Visible = true;
        rdbAllCustomer.Checked = false;
        Doccode.Visible = true;
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Focus();
      //  txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

       // System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
       // dateInfo.ShortDatePattern = "dd/MM/yyyy";
       //DateTime dateNow = DateTime.Now; 
       // //create DateTime with current date 
       // string firstDayDate = dateNow.AddDays(-(dateNow.Day - 1)).ToString(); //first day 
       // dateNow = dateNow.AddMonths(1);
       // txtFromDate.Text= dateNow.ToString("dd/MM/yyyy");

    }

    protected void txtCustomerID_TextChanged(object sender, EventArgs e)
    {
        fillCustomerIdDescription();
        txtCustomerID.Focus();
    }
    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        txtFromDate.Focus();
        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";
        DateTime today = Convert.ToDateTime(txtFromDate.Text, dateInfo);

        today = today.AddMonths(1);

        DateTime lastday = today.AddDays(-(today.Day));

        txtToDate.Text = lastday.ToString("dd/MM/yyyy");
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        txtToDate.Focus();
    }
}