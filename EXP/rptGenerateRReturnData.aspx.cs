using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;

public partial class EXP_rptGenerateRReturnData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime nowDate = System.DateTime.Now;
            //if (Session["LoggedUserId"] != null)
            //{
            //    lblUserName.Text = "Welcome, " + Session["userName"].ToString().Trim();
            //    lblRole.Text = "| Role: " + Session["userRole"].ToString().Trim();
            //    lblTime.Text = nowDate.ToLongDateString();
            //}
            ddlBranch.SelectedValue = Session["userADCode"].ToString();
            ddlBranch.Enabled = false;
            btncalendar_FromDate.Focus();
            fillddlBranch();
        }
        btncalendar_FromDate.Focus();
        txtFromDate.Attributes.Add("onblur", "return ValidDates();");
        btnSave.Attributes.Add("onclick", "return ValidDates();");
    }
    public void fillddlBranch()
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
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";
        ddlBranch.Items.Insert(0, li);
    }

    protected void signout_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/TF_Log_Out.aspx", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //Session["FrRelDt"] = txtFromDate.Text;
        //Session["ToRelDt"] = txtToDate.Text;
        //Session["ModuleID"] = "RET";
        //Response.Redirect("~/RReturn/Ret_Main.aspx", true);
     
        string _ColumnHeader = "", _RowData = "";
        string a = Session["userADCode"].ToString();
        string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/EXPORT/" + a);
       
        if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
        }

        TF_DATA objData = new TF_DATA();
        //SqlParameter p1 = new SqlParameter("@Branch", ddlBranch.SelectedValue.ToString());

        string fromdate = txtFromDate.Text.Substring(6, 4) + "-" + txtFromDate.Text.Substring(3, 2) + "-" + txtFromDate.Text.Substring(0, 2);
        string todate = txtToDate.Text.Substring(6, 4) + "-" + txtToDate.Text.Substring(3, 2) + "-" + txtToDate.Text.Substring(0, 2);

        SqlParameter p1 = new SqlParameter("@startdate", fromdate);
        SqlParameter p2 = new SqlParameter("@enddate", todate);
        SqlParameter p3 = new SqlParameter("@Branch",ddlBranch.SelectedValue);
        //SqlParameter p1 = new SqlParameter("@fromDate", txtFromDate.Text.Trim());
        //SqlParameter p2 = new SqlParameter("@toDate", txtToDate.Text.Trim());
        //objData.getData("EXP_Generate_RRturn_Data_CSV", p1, p2);
      //  string _query = "EXP_Generate_RRturn_Data_CSV";

        string _query = "TF_RET_CreateData";

        DataTable dt = objData.getData(_query, p1, p2,p3);
        if (dt.Rows.Count > 0)
        {
            string _FromDate = txtFromDate.Text.Substring(0, 2) + " " + txtFromDate.Text.Substring(3, 2) + " " + txtFromDate.Text.Substring(6, 4);
            string _ToDate = txtToDate.Text.Substring(0, 2) + " " + txtToDate.Text.Substring(3, 2) + " " + txtToDate.Text.Substring(6, 4);

            string _filePath = _directoryPath + "/RReturn Data " + "-" + _FromDate + " to " + _ToDate + ".csv";
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