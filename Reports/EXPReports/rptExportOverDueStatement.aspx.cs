using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Drawing;

public partial class Reports_EXPReports_rptExportOverDueStatement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnFDocHelp.Attributes.Add("onclick", "return OpenDocList();");
            txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            btnSave.Attributes.Add("onclick", "return validateSave();");
            txtBill.Attributes.Add("onkeydown", "return validate_Number(event);");
            txtDays.Attributes.Add("onkeydown", "return validate_Number(event);");            
            fillddlBranch();
            ddlBranch.SelectedValue = Session["userADCode"].ToString();
            ddlBranch.Enabled = false;
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
        li.Value = "All";
        if (dt.Rows.Count > 0)
        {
            li.Text = "All";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";
        ddlBranch.Items.Insert(0, li);
    }

    protected void rdbAllDocumentNo_CheckedChanged(object sender, EventArgs e)
    {
        tblDocNo.Visible = false;
    }
    protected void rdbSelectedDocumentNo_CheckedChanged(object sender, EventArgs e)
    {
        tblDocNo.Visible = true;
        txtFDocNo.Text = "";       
    }
    protected void btnCSV_Click(object sender, EventArgs e)
    {
        string Bill_Type = "";
        string Over = "";
        string Loan_Advanced = "";
        string LC = "";
        string Customer = "";
        bool flag = true;

        if (txtFromDate.Text == "")
        {
            txtFromDate.Focus();
            flag = false;
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Please Enter As On Date');", true);

        }
        if (flag == true)
        {
            if (rdbAll.Checked == true)
            {
                Bill_Type = "All";
            }
            if (rbtbla.Checked == true)
                Bill_Type = "BLA";
            if (rbtblu.Checked == true)
                Bill_Type = "BLU";
            if (rbtbba.Checked == true)
                Bill_Type = "BBA";
            if (rbtbbu.Checked == true)
                Bill_Type = "BBU";
            if (rbtbca.Checked == true)
                Bill_Type = "BCA";
            if (rbtbcu.Checked == true)
                Bill_Type = "BCU";
            if (rbtIBD.Checked == true)
                Bill_Type = "IBD";
            //if (rbtLBC.Checked == true)
            //    Bill_Type = "LBC";
            if (rbtBEB.Checked == true)
                Bill_Type = "EB";

            if (rdbOverseasBank.Checked == true)
            {
                Over = "Over";
            }
            if (rdbCustomer.Checked == true)
            {
                Over = "Cust";
            }
            if (rdbBoth.Checked == true)
            {
                Over = "All";
            }

            if (rdbLoanAdv.Checked == true)
            {
                Loan_Advanced = "Y";
            }
            if (rdbLoanNotAdv.Checked == true)
            {
                Loan_Advanced = "N";
            }
            if (rdbLoanAll.Checked == true)
            {
                Loan_Advanced = "All";
            }
            if (rdbLCWise.Checked == true)
            {
                LC = "LC";
            }
            if (rdbNonLCWise.Checked == true)
            {
                LC = "NLC";
            }
            if (rdbLCAll.Checked == true)
            {
                LC = "All";
            }
            if (rdbSelectedDocumentNo.Checked == true)
            {
                Customer = txtFDocNo.Text;
            }
            if (rdbAllDocumentNo.Checked == true)
            {
                Customer = "All";
            }


            System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            dateInfo.ShortDatePattern = "dd/MM/yyyy";
            string _qry = "";

            DateTime documentDate = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo);

            string a = Session["userADCode"].ToString();
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/EXPORT/" + a);

            TF_DATA objData = new TF_DATA();
            SqlParameter p1 = new SqlParameter("@DocType", SqlDbType.VarChar);
            p1.Value = Bill_Type;

            SqlParameter p2 = new SqlParameter("@Branch", SqlDbType.VarChar);
            p2.Value = ddlBranch.Text.ToString().Trim();

            SqlParameter p3 = new SqlParameter("@FCust", SqlDbType.VarChar);
            p3.Value = Customer;

            SqlParameter p4 = new SqlParameter("@Date", SqlDbType.VarChar);
            p4.Value = documentDate.ToString("MM/dd/yyyy");

            SqlParameter p5 = new SqlParameter("@Bill_Amount", SqlDbType.VarChar);
            p5.Value = txtBill.Text;

            SqlParameter p6 = new SqlParameter("@No_Of_Days", SqlDbType.VarChar);
            p6.Value = txtDays.Text;

            SqlParameter p7 = new SqlParameter("@OverSeasBank", SqlDbType.VarChar);
            p7.Value = Over;

            SqlParameter p8 = new SqlParameter("@Loan", SqlDbType.VarChar);
            p8.Value = Loan_Advanced;

            SqlParameter p9 = new SqlParameter("@LC", SqlDbType.VarChar);
            p9.Value = LC;

            _qry = "TF_Export_Report_OverDue_Statement1_Excel";
            DataTable dt = objData.getData(_qry, p1, p2, p3, p4, p5, p6, p7, p8, p9);

            if (dt.Rows.Count > 0)
            {
                string _ColumnHeader = "", _RowData = "";
                string _FromDate = txtFromDate.Text.Substring(0, 2) + " " + txtFromDate.Text.Substring(3, 2) + " " + txtFromDate.Text.Substring(6, 4);
                string _filePath = _directoryPath + "/Export Bills Overdue" + "-" + _FromDate + ".csv";
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
                string path = "file://" + _serverName + "/GeneratedFiles/EXPORT";
                string link = "/TF_GeneratedFiles/EXPORT";
                labelMessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
                ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Files Created Successfully');", true);
            }
            else
            {
                txtFromDate.Text = "";
                txtFromDate.Focus();
                ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('There is No Record Between This Dates');", true);
            }
        }
    }
}