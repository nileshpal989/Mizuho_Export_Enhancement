using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Data;

public partial class EXP_EXP_EDPMSAckwAndRealized_CSV : System.Web.UI.Page
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
                ddlBranch.SelectedValue = Session["userLBCode"].ToString();
                ddlBranch.Enabled = false;
                fillBranch();
                txtFromDate.Text = "01/" + System.DateTime.Now.ToString("MM/yyyy");
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                ddlBranch.Focus();
                
            }
            txtFromDate.Attributes.Add("onblur", "return isValidDate(" + txtFromDate.ClientID + "," + "'From Date.'" + ");");
            txtToDate.Attributes.Add("onblur", "return isValidDate(" + txtToDate.ClientID + "," + "'To Date.'" + ");");
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
        string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/EDPMS/" + a);
              
         if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
        }

        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@Branch", ddlBranch.SelectedValue.ToString());
        SqlParameter p2 = new SqlParameter("@fromDate", txtFromDate.Text.ToString());
        SqlParameter p3 = new SqlParameter("@toDate", txtToDate.Text.ToString());
        
        string _query = "TF_ExportBillCSV_EDPMSAcknwRealized";
        DataTable dt = objData.getData(_query, p1, p2, p3);
        if (dt.Rows.Count > 0)
        {
            string _FromDate = txtFromDate.Text.Substring(0, 2) + " " + txtFromDate.Text.Substring(3, 2) + " " + txtFromDate.Text.Substring(6, 4);
            string _ToDate = txtToDate.Text.Substring(0, 2) + " " + txtToDate.Text.Substring(3, 2) + " " + txtToDate.Text.Substring(6, 4);

            string _filePath = _directoryPath + "/EDPMS Acknowledged and Realized" + "-" + _FromDate + " to " + _ToDate + ".csv";
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
            string path = "file://" + _serverName + "/TF_GeneratedFiles/EDPMS";
            string link = "/TF_GeneratedFiles/EDPMS";
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
}
