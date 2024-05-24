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

public partial class IMP_FileCreation_TF_IMP_SettlementFileCreation : System.Web.UI.Page
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
                fillBranch();
                txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                ddlBranch.Focus();
            }
            txtFromDate.Attributes.Add("onblur", "return isValidDate(" + txtFromDate.ClientID + "," + "'From Date.'" + ");");
            txtToDate.Attributes.Add("onblur", "return isValidDate(" + txtToDate.ClientID + "," + "'To Date.'" + ");");
            btnCustomerList.Attributes.Add("onclick", "return OpenCustomerCodeList('mouseClick');");

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
        li.Value = "All";
        if (dt.Rows.Count > 0)
        {
            li.Text = "All";
            ddlBranch.DataSource = dt.DefaultView;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchCode";

            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";
        ddlBranch.Items.Insert(0, li);
    }
    protected void rdbAllCustomer_CheckedChanged(object sender, EventArgs e)
    {
        rdbSelectedCustomer.Checked = false;
        rdbAllCustomer.Checked = true;
        divUser.Visible = false;
        txtCustomer_ID.Text = "";

    }
    protected void rdbSelectedCustomer_CheckedChanged(object sender, EventArgs e)
    {
        rdbSelectedCustomer.Checked = true;
        rdbAllCustomer.Checked = false;
        divUser.Visible = true;
    }
    protected void btndownload_Click(object sender, EventArgs e)
    {
        string Customer = "";
        string Type = "";
        if (rdbAllCustomer.Checked == true)
        {
            Customer = "All";
        }
        else if (rdbSelectedCustomer.Checked == true)
        {
            Customer = txtCustomer_ID.Text.Trim();
        }
        if (rdbAll.Checked == true)
        {
            Type = "All";
        }
        else if (rdbIBA.Checked == true)
        {
            Type = "IBA";
        }
        else if (rdbICA.Checked == true)
        {
            Type = "ICA";
        }
        else if (rdbICU.Checked == true)
        {
            Type = "ICU";
        }
        else if (rdbACC.Checked == true)
        {
            Type = "ACC";
        }

        TF_DATA objData1 = new TF_DATA();
        string _fromdate = txtFromDate.Text.Substring(0, 2) + "" + txtFromDate.Text.Substring(3, 2) + "" + txtFromDate.Text.Substring(6, 4);
        string _todate = txtToDate.Text.Substring(0, 2) + "" + txtToDate.Text.Substring(3, 2) + "" + txtToDate.Text.Substring(6, 4);
        SqlParameter P1 = new SqlParameter("@FROMDATE", SqlDbType.VarChar);
        P1.Value = txtFromDate.Text.Trim();

        SqlParameter P2 = new SqlParameter("@TODATE", SqlDbType.VarChar);
        P2.Value = txtToDate.Text.Trim();

        SqlParameter P3 = new SqlParameter("@BRANCHCODE", SqlDbType.VarChar);
        P3.Value = ddlBranch.SelectedValue.ToString();

        SqlParameter P4 = new SqlParameter("@cust", SqlDbType.VarChar);
        P4.Value = Customer;

        SqlParameter P5 = new SqlParameter("@Type", SqlDbType.VarChar);
        P5.Value = Type;

        SqlParameter P6 = new SqlParameter("@status", SqlDbType.VarChar);
        P6.Value = ddlStatus.SelectedValue.ToString();
        DataTable dt = objData1.getData("TF_IMP_SettelmentFileCreation", P1, P2, P3, P4, P5, P6);
        string Todaytime = DateTime.Now.ToString("hhmmss");
        if (dt.Rows.Count > 0)
        {
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/Reports");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            string filename = "";
            if (ddlStatus.SelectedValue.ToString() == "A")
            {
                filename = "Settelment_Report_Approved_" + _fromdate + "_TO_" + _todate + "_AT_" + Todaytime + ".xlsx";
            }
            else if (ddlStatus.SelectedValue.ToString() == "R")
            {
                filename = "Settelment_Report_Rejected_" + _fromdate + "_TO_" + _todate + "_AT_" + Todaytime + ".xlsx";
            }
            else if (ddlStatus.SelectedValue.ToString() == "C")
            {
                filename = "Settelment_Report_Pending_" + _fromdate + "_TO_" + _todate + "_AT_" + Todaytime + ".xlsx";
            }
            string _filePath = _directoryPath + "/" + filename;
            if (dt.Rows.Count > 0)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var sheet1 = wb.Worksheets.Add(dt, "Worksheet");
                    sheet1.Table("Table1").ShowAutoFilter = false;
                    sheet1.Table("Table1").Theme = XLTableTheme.None;
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        FileStream file = new FileStream(_filePath, FileMode.Create, FileAccess.Write);
                        MyMemoryStream.WriteTo(file);
                        file.Close();
                        MyMemoryStream.Close();
                    }
                }
                TF_DATA objserverName = new TF_DATA();
                string _serverName = objserverName.GetServerName();// use to get pc name of the server

                string path = "file://" + _serverName + "/Mizuho_UAT/TF_GeneratedFiles/IMPORT/Reports/";
                string link = "/TF_GeneratedFiles/IMPORT/Reports/" + filename;
                labelMessage.Text = "Files Created in " + "<a href=" + path + "> " + link + " </a>";
                string filePath = "~/TF_GeneratedFiles/IMPORT/Reports/" + filename;
                //ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "Message", "VAlert('File Created Successfully on " + _serverName + " in " + path + "','#fgdfg')", true);
                exceldownload(filename, filePath);
            }
        }
        else
        {
            //ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "Message", "alert('There are No records for this period.')", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Message", "VAlert('There are No records for this period','#fromDate');", true);
            txtFromDate.Text = "";
            txtFromDate.Focus();
        }
    }
    public void exceldownload(string filename, string filePath)
    {
        Response.ContentType = ".xlsx";
        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
        Response.TransmitFile(Server.MapPath(filePath));
        Response.End();
    }
}