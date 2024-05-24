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

public partial class IMP_FileCreation_TF_IMP_GBaseFileCreation : System.Web.UI.Page
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
                //ddlBranch.SelectedValue = Session["userLBCode"].ToString();
                //ddlBranch.Enabled = false;
                fillBranch();
                txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                ddlBranch.Focus();
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
            ddlBranch.DataValueField = "BranchCode";

            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";
        ddlBranch.Items.Insert(0, li);
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        TF_DATA objData1 = new TF_DATA();
        if (txtFromDate.Text == "" || txtToDate.Text == "")
        {
            return;
        }
        string _fromdate = txtFromDate.Text.Substring(0, 2) + "" + txtFromDate.Text.Substring(3, 2) + "" + txtFromDate.Text.Substring(6, 4);
        string _todate = txtToDate.Text.Substring(0, 2) + "" + txtToDate.Text.Substring(3, 2) + "" + txtToDate.Text.Substring(6, 4);
        SqlParameter P1 = new SqlParameter("@FROMDATE", SqlDbType.VarChar);
        P1.Value = txtFromDate.Text.Trim();
        SqlParameter P2 = new SqlParameter("@TODATE", SqlDbType.VarChar);
        P2.Value = txtToDate.Text.Trim();
        SqlParameter P3 = new SqlParameter("@BRANCHCODE", SqlDbType.VarChar);
        P3.Value = ddlBranch.SelectedValue.ToString();
        DataTable dt = objData1.getData("TF_IMP_GBaseFileCreation", P1, P2, P3);
        if (dt.Rows.Count > 0)
        {
            string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IMPORT/GBASE/" + ddlBranch.SelectedItem.Text.Trim() + "/");
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string branchname = ddlBranch.SelectedItem.ToString().Trim();
            string brpref = "";
            if (branchname == "Mumbai")
            {
                brpref = "MB";
            }
            else if (branchname == "New Delhi")
            {
                brpref = "ND";
            }
            else if (branchname == "Chennai")
            {
                brpref = "CH";
            }
            else if (branchname == "Bangalore")
            {
                brpref = "BG";
            }
            string _filePath = _directoryPath + "/" + brpref + _fromdate + "TO" + _todate + ".xlsx";
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
                string _serverName = objserverName.GetServerName();
                string path = "file://" + _serverName + "/Mizuho_UAT/TF_GeneratedFiles/IMPORT/GBASE/";
                string link = "/TF_GeneratedFiles/IMPORT/GBASE/";
                labelMessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
                ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('File Created Successfully on " + _serverName + " in " + path + "')", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "Message", "alert('There is No records Between this Dates.')", true);
            txtFromDate.Text = "";
            txtFromDate.Focus();
        }
    }
}