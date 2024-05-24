using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;

public partial class IDPMS_BOE_CSV_File_Creation : System.Web.UI.Page
{
    string cust;
    protected void Page_Load(object sender, EventArgs e)
    {
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
                    clearControls();
                    fillddlBranch();
                    ddlBranch.Focus();
                    ddlBranch.SelectedValue = Session["userADCode"].ToString();
                    ddlBranch.Enabled = false;
                }
                btnCreate.Attributes.Add("onclick", "return validateSave();");
            }

        }
    }

    protected void clearControls()
    {
        labelMessage.Text = "";

        ddlBranch.Focus();
    }
    public void fillddlBranch()
    {
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("BranchName", SqlDbType.VarChar);
        p1.Value = "";
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
            ddlBranch.DataValueField = "AuthorizedDealerCode";
            ddlBranch.DataBind();
        }
        else
        {
            li.Text = "No record(s) found";
        }
        ddlBranch.Items.Insert(0, li);

    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Focus();
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";


        DateTime documentDate = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo);
        DateTime documentDate1 = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);


        string _directoryPath = ddlBranch.Text.ToString() + "-" + documentDate.ToString("ddMMyyyy") + "-" + documentDate1.ToString("ddMMyyyy");
        _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IDPMS");

        //if (!Directory.Exists(_directoryPath))
        //{
        //    Directory.CreateDirectory(_directoryPath);
        //}

        //For csv file creation
        TF_DATA objData = new TF_DATA();

        SqlParameter p2 = new SqlParameter("@FromDate", SqlDbType.VarChar);
        p2.Value = txtFromDate.Text;

        SqlParameter p3 = new SqlParameter("@ToDate", SqlDbType.VarChar);
        p3.Value = txtToDate.Text;

        SqlParameter P4 = new SqlParameter("@BranchCode", SqlDbType.VarChar);
        P4.Value = ddlBranch.SelectedValue.ToString();

        string _qry = "Other_BOE_CSV_Creation";
        DataTable dt = objData.getData(_qry, p2, p3, P4);
        if (dt.Rows.Count > 0)
        {
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            string query = "FileName_Generation_BOE";
            DataTable dtfile = objData.getData(query);
            string filename1 = dtfile.Rows[0]["FileName"].ToString();
            string Adcode = ddlBranch.SelectedValue.ToString();

            string _filePath = _directoryPath + "/" + Adcode + "_" + filename1 + ".obb.csv";
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            sw.WriteLine("billOfEntryNumber|billOfEntryDate|portOfDischarge");
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                string _BOENo = dt.Rows[j]["BOENo"].ToString().Trim();
                string _BOENo1 = _BOENo.Replace(",", " -");
                sw.Write(_BOENo1 + "|");


                string _BOEDate = dt.Rows[j]["BOEDate"].ToString().Trim();
                string _BOEDate1 = _BOEDate.Replace(",", " -");
                sw.Write(_BOEDate1 + "|");


                string _BOEPortCode = dt.Rows[j]["BOEPortCode"].ToString().Trim();
                string _BOEPortCode1 = _BOEPortCode.Replace(",", " -");
                sw.WriteLine(_BOEPortCode1);


                string query2 = "Insert_created_Table";

                SqlParameter S1 = new SqlParameter("@BOENo", dt.Rows[j]["BOENo"].ToString());
                SqlParameter S2 = new SqlParameter("@BOEDate", dt.Rows[j]["BOEDate"].ToString());
                SqlParameter S3 = new SqlParameter("@BOEPortCode", dt.Rows[j]["BOEPortCode"].ToString());
                SqlParameter S4 = new SqlParameter("@CreatedBy", SqlDbType.VarChar);
                S4.Value = Session["userName"].ToString();
                SqlParameter S5 = new SqlParameter("@Createddate", SqlDbType.DateTime);
                S5.Value = System.DateTime.Now.ToString();
                SqlParameter S6 = new SqlParameter("@FileName", filename1);


                string result = objData.SaveDeleteData(query2, S1, S2, S3, S4, S5, S6);
                if (result == "Inserted")
                {

                }
            }
            sw.Flush();
            sw.Close();
            sw.Dispose();

            TF_DATA objServerName = new TF_DATA();
            string _serverName = objServerName.GetServerName();

            string path = "file://" + _serverName + "/TF_GeneratedFiles/IDPMS";
            string link = "TF_GeneratedFiles/IDPMS";
            labelMessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";

            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Files Created Successfully');", true);
            ddlBranch.Focus();

        }
        else
        {

            labelMessage.Text = "There is No Record Between This Dates " + documentDate.ToString("dd/MM/yyyy") + "-" + documentDate1.ToString("dd/MM/yyyy");

            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtFromDate.Focus();
            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('There is No Record Between This Dates');", true);
        }
    }
}