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


public partial class Reports_EXPReports_Export_Report_XOS_Nil_Statement : System.Web.UI.Page
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
                clearControls();
                fillddlBranch();
                ddlBranch.SelectedValue = Session["userADCode"].ToString();
                ddlBranch_SelectedIndexChanged(null, null);
                ddlBranch.Enabled = false;
                txtFromDate.Focus();
            }
            txtFromDate.Attributes.Add("onblur", "return isValidDate(" + txtFromDate.ClientID + "," + "'As On Date'" + " );");
            btnCreate.Attributes.Add("onclick", "return validateSave();");
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
        li.Value = "0";
        if (dt.Rows.Count > 0)
        {
            li.Text = "---Select---";
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
    }


    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Focus();
        labelMessage.Text = "";
        //txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
    }


    protected void btnCreate_Click(object sender, EventArgs e)
    {
        TF_DATA objData1 = new TF_DATA();
        SqlParameter p = new SqlParameter("@startdate", SqlDbType.VarChar);
        p.Value = txtFromDate.Text.ToString().Trim();
        
            System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            dateInfo.ShortDatePattern = "dd/MM/yyyy";
            bool flag = true;
            if (ddlBranch.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Enter Branch Name.');", true);
                flag = false;
            }
            if (txtFromDate.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Enter As on Date');", true);
                flag = false;
            }
            if (flag == true)
            {
                DateTime documentDate = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo);
                string _directoryPath = "XOS/" + ddlBranch.Text.ToString();
                _directoryPath = Server.MapPath("~/TF_GeneratedFiles/" + _directoryPath);


                TF_DATA objData = new TF_DATA();
                SqlParameter p1 = new SqlParameter("@startdate", SqlDbType.VarChar);
                p1.Value = documentDate.ToString("MM/dd/yyyy");
                SqlParameter p2 = new SqlParameter("@Branch", SqlDbType.VarChar);
                p2.Value = ddlBranch.Text.ToString().Trim();
                SqlParameter p3 = new SqlParameter("@Remarks", SqlDbType.VarChar);
                p3.Value = txtRemarks.Text.ToString().Trim();
                int cnt = 0;
                string _qry = "TF_Export_Report_XOS_NilStatement";
                DataTable dt = objData.getData(_qry, p1, p2,p3);
                if (dt.Rows.Count > 0)
                {
                    if (!Directory.Exists(_directoryPath))
                    {
                        Directory.CreateDirectory(_directoryPath);
                    }
                    string _filePath = _directoryPath + "/" + ddlBranch.Text.ToString() + documentDate.ToString("dd-MM-yyyy") + ".csv";
                    StreamWriter sw;
                    sw = File.CreateText(_filePath);
                    string s = "Bank_Code,AD_CODE,XOS_PERIOD,REMARK";
                    sw.WriteLine(s);


                    for (int j = 0; j < dt.Rows.Count; j++)
                    {

                        string _strBranchName = dt.Rows[j]["BranchName"].ToString().Trim();
                        string _strBranchName1 = _strBranchName.Replace(",", "-");

                        string _strBank_Code = dt.Rows[j]["Bank_Code"].ToString().Trim();
                        string _strBank_Code1 = _strBank_Code.Replace(",", "-");
                        
                        string _strAD_CODE = dt.Rows[j]["ADcode"].ToString().Trim();
                        string _strAD_CODE1 = _strAD_CODE.Replace(",", "-");
                       
                        string _strXOS_PERIOD = dt.Rows[j]["XOS_PERIOD"].ToString().Trim();
                        string _strXOS_PERIOD1 = _strXOS_PERIOD.Replace(",", "-");
                        
                        string _strREMARK = dt.Rows[j]["REMARK"].ToString().Trim();
                        string _strREMARK1 = _strREMARK.Replace(",", "-");


                        sw.Write(_strBank_Code1 + ",");
                        sw.Write(_strAD_CODE1 + ",");
                        sw.Write(_strXOS_PERIOD1 + ",");
                        sw.WriteLine(_strREMARK1 + ",");


                        cnt++;

                    }
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();

                    TF_DATA objServerName = new TF_DATA();
                    string _serverName = objServerName.GetServerName();

                    // labelMessage.Text = "Files Created Successfully on Server : " + _serverName + " in " + _directoryPath;
                    // ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('Files Created Successfully');", true);

                    if (cnt == 0)
                    {
                        labelMessage.Text = "There is No Record for This Dates " + documentDate.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        string path = "file://" + _serverName + "/TF_GeneratedFiles";
                        string link = _directoryPath;
                        labelMessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
                    }


                    ddlBranch.Focus();

                }
                else
                {
                    //labelMessage.Text = "No Reocrds ";
                    labelMessage.Text = "There is No Record for This Dates " + documentDate.ToString("dd/MM/yyyy");
                    //ddlBranch.Focus();

                    txtFromDate.Focus();
                    ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "message", "alert('There is No Record for This Dates');", true);
                }
            }

       
    }
}