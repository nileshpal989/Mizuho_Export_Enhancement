using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

public partial class TF_CustomerMaster_CSV_file_Genaration : System.Web.UI.Page
{
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
                    fillddlBranch();
                    ddlBranch.Focus();
                    ddlBranch.Enabled = true;
                    labelMessage.Text = "";
                }
                btnCreate.Attributes.Add("onclick", "return validateSave();");
            }

        }
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
            li.Text = "All Branches";
            li.Value = "All";
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
    protected void btnCreate_Click(object sender, EventArgs e)
    {



        string _directoryPath = Server.MapPath("~/TF_GeneratedFiles/IDPMS");

        //if (!Directory.Exists(_directoryPath))
        //{
        //    Directory.CreateDirectory(_directoryPath);
        //}

        //For csv file creation
        TF_DATA objData = new TF_DATA();



        SqlParameter P1 = new SqlParameter("@Adcode", SqlDbType.VarChar);
        P1.Value = ddlBranch.SelectedValue.ToString();

        string _qry = "TF_IDPMS_Customer_Master_CSV_Creation";
        DataTable dt = objData.getData(_qry, P1);
        if (dt.Rows.Count > 0)
        {
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }
            //string query = "FileName_Generation_BOE";
            //DataTable dtfile = objData.getData(query);
            string filename1 = "Customer_" + ddlBranch.SelectedValue.ToString() +".csv";

            string _filePath = _directoryPath + "/" + filename1 ;
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            sw.WriteLine("ieCode|ieName|ieAddress1|iePAN|ieEmail|remarks");
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                // CUST_IE_CODE,CUST_NAME,CUST_ADDRESS,CUST_PAN_NO,CUST_EMAIL_ID
                string _CUST_IE_CODE = dt.Rows[j]["CUST_IE_CODE"].ToString().Trim();
                string _CUST_IE_CODE1 = _CUST_IE_CODE.Replace(",", "").Trim();
                sw.Write(_CUST_IE_CODE1 + "|");


                string _CUST_NAME = dt.Rows[j]["CUST_NAME"].ToString().Trim();
                string _CUST_NAME1 = Regex.Replace(_CUST_NAME, @"\s+", " ").Trim();
                sw.Write(_CUST_NAME1 + "|");


                string _ieAddress1 = dt.Rows[j]["CUST_ADDRESS"].ToString().Trim();
                _ieAddress1 = Regex.Replace(_ieAddress1, @"\s+", " ");
                sw.Write(_ieAddress1);

                string _CUST_PAN_NO = dt.Rows[j]["CUST_PAN_NO"].ToString().Trim();
                string _CUST_PAN_NO1 = _CUST_PAN_NO.Replace(",", "").Trim();
                sw.Write(_CUST_PAN_NO1 + "|");

                string _CUST_EMAIL_ID = dt.Rows[j]["CUST_EMAIL_ID"].ToString().Trim();
                string _CUST_EMAIL_ID1 = _CUST_EMAIL_ID.Replace(",", "").Trim();
                sw.Write(_CUST_EMAIL_ID1 + "|");

                string _remark = "";
                sw.WriteLine(_remark);


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

            labelMessage.Text = "There is No Record ";


            ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('There is No Record Between This Dates');", true);
        }
    }
}