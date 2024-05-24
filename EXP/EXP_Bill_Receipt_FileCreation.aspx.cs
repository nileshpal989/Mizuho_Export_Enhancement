using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Globalization;
using System.Text;
public partial class EXP_EXP_Bill_Receipt_FileCreation : System.Web.UI.Page
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
            scriptManager.RegisterPostBackControl(this.btnDownload);

            if (!IsPostBack)
            {
                clearControls();
                btnDownload.Visible = false;
                rdbAllCustomer.Checked = true;
                fillddlBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch.Enabled = false;
                txtFromDate.Focus();
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                btnSave.Attributes.Add("onclick", "return validateSave();");
                btnDownload.Attributes.Add("onclick", "return validateSave();");
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
        //li.Value = "---Select---";
        ListItem li01 = new ListItem();
        li01.Value = "All Branches";
        if (dt.Rows.Count > 0)
        {
            //li.Text = "---Select---";
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
    protected void clearControls()
    {
        txtFromDate.Text = "";

        txtFromDate.Focus();
        rdbAllCustomer.Checked = true;
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
        rdbSelectedCustomer.Focus();
        txtCustomerID.Visible = true;
        btnCustList.Visible = true;
        lblCustomerName.Visible = true;
        rdbSelectedCustomer.Focus();
    }
    protected void txtCustomerID_TextChanged(object sender, EventArgs e)
    {
        fillCustomerIdDescription();
    }
    protected void txtCustomerID_TextChanged1(object sender, EventArgs e)
    {
        fillCustomerIdDescription();
        txtCustomerID.Focus();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _ColumnHeader = "", _RowData = "",cust="";
        string a = ddlBranch.Text;
        string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/EXPORT/"+a);

        if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
        }

        if (rdbAllCustomer.Checked == true)
        {
            cust = "All";
        }
        if (rdbSelectedCustomer.Checked == true)
        {
            cust = txtCustomerID.Text.Trim();
        }

        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@ADCode", ddlBranch.SelectedValue.ToString());
        SqlParameter p2 = new SqlParameter("@fromdate", txtFromDate.Text.ToString());
        SqlParameter p3 = new SqlParameter("@todate", txtToDate.Text.ToString());
        SqlParameter p4 = new SqlParameter("@cust", cust);

        string _query = "TF_Export_Bill_CSV_fileCreation";
        DataTable dt = objData.getData(_query, p1, p2, p3,p4);
        if (dt.Rows.Count > 0)
        {
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string _FromDate = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt16(txtFromDate.Text.ToString().Substring(3, 2)));
            string _ToDate = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt16(txtToDate.Text.ToString().Substring(3, 2)));

            _FromDate = txtFromDate.Text.Substring(0, 2) + " " + _FromDate.Substring(0, 3) + " " + txtFromDate.Text.Substring(6, 4);
            _ToDate = txtToDate.Text.Substring(0, 2) + " " + _ToDate.Substring(0, 3) + " " + txtToDate.Text.Substring(6, 4);

            string _filePath = _directoryPath + "/Export Bill-Receipt Details" + "-" + _FromDate + " to " + _ToDate + ".csv";
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
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        string  cust = "";
    
        if (rdbAllCustomer.Checked == true)
        {
            cust = "All";
        }
        if (rdbSelectedCustomer.Checked == true)
        {
            cust = txtCustomerID.Text.Trim();
        }

        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@ADCode", ddlBranch.SelectedValue.ToString());
        SqlParameter p2 = new SqlParameter("@fromdate", txtFromDate.Text.ToString());
        SqlParameter p3 = new SqlParameter("@todate", txtToDate.Text.ToString());
        SqlParameter p4 = new SqlParameter("@cust", cust);

        string _query = "TF_Export_Bill_CSV_fileCreation";
        DataTable dt = objData.getData(_query, p1, p2, p3, p4);
        if (dt.Rows.Count > 0)
        {
           
            string _FromDate = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt16(txtFromDate.Text.ToString().Substring(3, 2)));
            string _ToDate = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt16(txtToDate.Text.ToString().Substring(3, 2)));

            _FromDate = txtFromDate.Text.Substring(0, 2) + " " + _FromDate.Substring(0, 3) + " " + txtFromDate.Text.Substring(6, 4);
            _ToDate = txtToDate.Text.Substring(0, 2) + " " + _ToDate.Substring(0, 3) + " " + txtToDate.Text.Substring(6, 4);

            string _filePath = "Export Bill-Receipt Details" + "-" + _FromDate + " to " + _ToDate;
            
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + _filePath+".csv");
            Response.Charset = "";
            Response.ContentType = "application/text";

            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < dt.Columns.Count; k++)
            {
                //add separator
                sb.Append(dt.Columns[k].ColumnName + ',');
            }
            //append new line
            sb.Append("\r\n");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    //add separator
                    sb.Append(dt.Rows[i][k].ToString().Replace(",", ";") + ',');
                }
                //append new line
                sb.Append("\r\n");
            }
            Response.Output.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }
        else
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtFromDate.Focus();
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('There is No Record Between This Dates');", true);
        }
    }
    }