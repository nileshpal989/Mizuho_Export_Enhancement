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

public partial class EXP_rptLEI_EOD_reports : System.Web.UI.Page
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
                hdnModuleIDLEI.Value = Session["ModuleID"].ToString();
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
        string qureyLEI = "";
        if (hdnModuleIDLEI.Value == "EXP")
        {
            qureyLEI = "TF_EXP_LEI_EOD_Excel_GENERATION";
        }
        else if (hdnModuleIDLEI.Value == "IMP")
        {
            qureyLEI = "TF_IMP_LEI_EOD_Excel_GENERATION";
        }

        DataTable dt = objData1.getData(qureyLEI, P1, P2, P3);
        
        if (dt.Rows.Count > 0)
        {
            string _directoryPath = "";
            if (hdnModuleIDLEI.Value == "EXP")
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/LEI/EXPORT/" + ddlBranch.SelectedItem.Text.Trim() + "/");
                hdnfilepath.Value = _directoryPath;
            }
            else if (hdnModuleIDLEI.Value == "IMP")
            {
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/LEI/IMPORT/" + ddlBranch.SelectedItem.Text.Trim() + "/");
                hdnfilepath.Value = _directoryPath;
            }

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
            //string _filePath = _directoryPath + "/" + "GBaseDataFile" + _fromdate + "TO" + _todate + ".xlsx";
            string filename = "";
            filename = brpref + _fromdate + "TO" + _todate + ".xlsx";
            hdnpathname.Value = brpref + _fromdate + "TO" + _todate + ".xlsx";


            if (dt.Rows.Count > 0)
            {

                using (XLWorkbook wb = new XLWorkbook())
                {
                    //dt.Columns.Remove("Addeddate");

                    var sheet1 = wb.Worksheets.Add(dt, "Sheet1");
                    sheet1.Table("Table1").ShowAutoFilter = false;
                    sheet1.Table("Table1").Theme = XLTableTheme.None;

                    //wb.Worksheets.Add(dt, "Sheet1");

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
                string path = "file://" + _serverName + "/TF_GeneratedFiles/";
                string link = "";
                if (hdnModuleIDLEI.Value == "EXP")
                {
                    link = "/TF_GeneratedFiles/LEI/EXPORT/";
                }
                else if (hdnModuleIDLEI.Value == "IMP")
                {
                    link = "/TF_GeneratedFiles/LEI/IMPORT/";
                }
                 
                labelMessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myconfirm1", "confirmmessage();", true);

                //ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('File Created Successfully on " + _serverName + " in " + path + "')", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "Message", "alert('There is No records Between this Dates.')", true);
            txtFromDate.Text = "";

            txtFromDate.Focus();
        }
    }

    protected void btndownload_Click(object sender, EventArgs e)
    {
        string _filePath = hdnfilepath.Value + hdnpathname.Value;
       
        string FileName = hdnpathname.Value;

        Response.ContentType = ".xlsx";
        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + FileName + "\"");
        Response.TransmitFile(_filePath);
        Response.End();
    }
}