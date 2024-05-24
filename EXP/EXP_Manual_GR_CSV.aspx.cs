using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Data;

public partial class EXP_EXP_Manual_GR_CSV : System.Web.UI.Page
{
    string s = "";
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
            //    fillBranch();
            //    ddlBranch.SelectedValue = Session["userADCode"].ToString();
            //    ddlBranch.Enabled = false;


                ddlBranch.SelectedValue = Session["userLBCode"].ToString();
                ddlBranch.Enabled = false;
                fillBranch();

                txtFromDate.Text = "01/" + System.DateTime.Now.ToString("MM/yyyy");
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                ddlBranch.Focus();
                rdbAllCustomer.Checked = true;
            }
            txtFromDate.Attributes.Add("onblur", "return isValidDate(" + txtFromDate.ClientID + "," + "'From Date.'" + ");");
            txtToDate.Attributes.Add("onblur", "return isValidDate(" + txtToDate.ClientID + "," + "'To Date.'" + ");");
            btnCreate.Attributes.Add("onclick", "return validateSave();");
        }
    }

    public void fillBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@BranchName", "");
        string _query = "TF_GetBranchDetails";
        DataTable dt = objData.getData(_query, p1);
        ddlBranch.Items.Clear();
        ListItem li = new ListItem();
        li.Value = "";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            //ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataValueField = "BranchCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";
        ddlBranch.Items.Insert(0, li);
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        string _ColumnHeader = "", _RowData = "";
        string a = Session["userADCode"].ToString();
        string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/EXPORT/" + a);

        string cust = "";
        if (rdbAllCustomer.Checked == true)
            cust = "ALL";
        else
            cust = txtCustomer.Text;

        if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
        }

        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@Branch", ddlBranch.SelectedValue.ToString());
        SqlParameter p2 = new SqlParameter("@fromDate", txtFromDate.Text.ToString());
        SqlParameter p3 = new SqlParameter("@toDate", txtToDate.Text.ToString());
        SqlParameter p4 = new SqlParameter("@CustAcNo", cust);

        string _query = "TF_EXP_Manual_GR_CSV";
        DataTable dt = objData.getData(_query, p1, p2, p3, p4);
        if (dt.Rows.Count > 0)
        {
            string _FromDate = txtFromDate.Text.Substring(0, 2) + " " + txtToDate.Text.Substring(3, 2) + " " + txtFromDate.Text.Substring(6, 4);
            string _ToDate = txtToDate.Text.Substring(0, 2) + " " + txtToDate.Text.Substring(3, 2) + " " + txtToDate.Text.Substring(6, 4);

            string _filePath = _directoryPath + "/Export Manual GRs" + "-" + _FromDate + " to " + _ToDate + ".csv";
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            for (int c = 0; c < dt.Columns.Count; c++)
            {
                _ColumnHeader = _ColumnHeader + dt.Columns[c].ColumnName.ToString() + ",";
            }

            sw.WriteLine(_ColumnHeader);
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int r = 0; r < dt.Columns.Count; r++)
                {
                    _RowData = _RowData + dt.Rows[j][r].ToString().Replace(",", "").ToString() + ",";
                }
                sw.WriteLine(_RowData.ToString());
                _RowData = "";
            }
            sw.Flush();
            sw.Close();
            sw.Dispose();

            TF_DATA objServerName = new TF_DATA();
            string _serverName = objServerName.GetServerName();
            string path = "file://" + _serverName + "/TF_GeneratedFiles/EXPORT";
            string link = "/TF_GeneratedFiles/EXPORT";
            labelMessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Files Created Successfully');", true);
            ddlBranch.Focus();
        }
        else
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtFromDate.Focus();
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('There is No Record Between This Dates');", true);
        }
    }
    protected void rdbSelectedCustomer_CheckedChanged(object sender, EventArgs e)
    {
        rdbAllCustomer.Checked = false;

        if (rdbSelectedCustomer.Checked == true)
        {
            divSelectedCustomer.Attributes.Add("style", "display:block");
        }
    }
    protected void rdbAllCustomer_CheckedChanged(object sender, EventArgs e)
    {
        rdbSelectedCustomer.Checked = false;

        if (rdbAllCustomer.Checked == true)
        {
            txtCustomer.Text = "";
            lblCustomerName.Text = "";
            divSelectedCustomer.Attributes.Add("style", "display:none");
        }
    }
    protected void txtCustomer_TextChanged(object sender, EventArgs e)
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@customerACNo", txtCustomer.Text);
        SqlParameter p2 = new SqlParameter("@branch", ddlBranch.SelectedValue);
        DataTable dt = objData.getData("TF_INW_GetCustDetails", p1, p2);
        if (dt.Rows.Count > 0)
        {
            lblCustomerName.Text = dt.Rows[0]["CUST_NAME"].ToString();
            if (lblCustomerName.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "alert('Invalid Cust A/C No.')", true);
                txtCustomer.Text = "";
                txtCustomer.Focus();
            }
        }
    }
}