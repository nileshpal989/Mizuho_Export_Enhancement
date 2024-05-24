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

public partial class EXP_TF_EXPORT_EBRC_File_Creation : System.Web.UI.Page
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
                txtFromDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                fillBranch();
                ddlBranch.Focus();
            }
        }
    }

    protected void fillBranch()
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
            ddlBranch.DataValueField = "BranchName";
            ddlBranch.DataBind();
        }
        else
            li.Text = "No record(s) found";

        ddlBranch.Items.Insert(0, li);
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
        dateInfo.ShortDatePattern = "dd/MM/yyyy";

        DateTime documentDate = Convert.ToDateTime(txtFromDate.Text.Trim(), dateInfo);
        DateTime documentDate1 = Convert.ToDateTime(txtToDate.Text.Trim(), dateInfo);

        string _directoryPath = documentDate.ToString("ddMMyyyy") + "-" + documentDate1.ToString("ddMMyyyy");
        _directoryPath = Server.MapPath("~/CSVFILE/EBRCDATA/" + _directoryPath);
        if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
        }
        //For text file creation
        TF_DATA objData = new TF_DATA();
        SqlParameter p1 = new SqlParameter("@Branch", SqlDbType.VarChar);
        p1.Value = ddlBranch.Text.ToString().Trim();


        SqlParameter p2 = new SqlParameter("@startdate", SqlDbType.VarChar);
        p2.Value = txtFromDate.Text.ToString();

        SqlParameter p3 = new SqlParameter("@enddate", SqlDbType.VarChar);
        p3.Value = txtToDate.Text.ToString();

        string _qry1 = "TF_EBRC";
        DataTable dt1 = objData.getData(_qry1, p1, p2, p3);

        string _qry = "TF_EBRC_CSV";
        DataTable dt = objData.getData(_qry, p1, p2, p3);
        if (dt.Rows.Count > 0)
        {
            string _filePath = _directoryPath + "/" + ddlBranch.Text.ToString() + "-EBRC-" + documentDate.ToString("ddMMyyyy") + "-" + documentDate1.ToString("ddMMyyyy") + ".csv";
            StreamWriter sw;
            sw = File.CreateText(_filePath);
            sw.WriteLine("BRANCHCODE,DOCNO,TRANSACTION_DT,PORT_CODE,SHIPP_BILL_NO,SHIPP_BILL_DT,CURR,AMOUNT,INR_AMOUNT,BEN,REM,REALISED_AMT,FULL_PART,EXRT,BILLNO");
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                string _strBRANCHCODE = dt.Rows[j]["BRANCHCODE"].ToString().Trim();
                sw.Write(_strBRANCHCODE + ",");
                string _strDOCNO = dt.Rows[j]["DOCNO"].ToString().Trim();
                sw.Write(_strDOCNO + ",");
                string _strTRANSACTION_DT = dt.Rows[j]["TRANSACTION_DT"].ToString().Trim();
                sw.Write(_strTRANSACTION_DT + ",");
                string _strPORT_CODE = dt.Rows[j]["PORT_CODE"].ToString().Trim();
                sw.Write(_strPORT_CODE + ",");
                string _strSHIPP_BILL_NO = dt.Rows[j]["SHIPP_BILL_NO"].ToString().Trim();
                sw.Write(_strSHIPP_BILL_NO + ",");
                string _strSHIPP_BILL_DT = dt.Rows[j]["SHIPP_BILL_DT"].ToString().Trim();
                sw.Write(_strSHIPP_BILL_DT + ",");
                string _strCURR = dt.Rows[j]["CURR"].ToString().Trim();
                sw.Write(_strCURR + ",");
                string _strAMOUNT = dt.Rows[j]["AMOUNT"].ToString().Trim();
                sw.Write(_strAMOUNT + ",");
                string _strINR_AMOUNT = dt.Rows[j]["INR_AMOUNT"].ToString().Trim();
                sw.Write(_strINR_AMOUNT + ",");
                string _strBEN = dt.Rows[j]["BEN"].ToString().Trim();
                sw.Write(_strBEN + ",");
                string _strRem = dt.Rows[j]["REM"].ToString().Trim();
                sw.Write(_strRem + ",");
                string _strREALISED_AMT = dt.Rows[j]["REALISED_AMT"].ToString().Trim();
                sw.Write(_strREALISED_AMT + ",");
                string _strFULL_PART = dt.Rows[j]["FULL_PART"].ToString().Trim();
                sw.Write(_strFULL_PART + ",");
                string _strEXRT = dt.Rows[j]["EXRT"].ToString().Trim();
                sw.Write(_strEXRT + ",");
                string _strBILLNO = dt.Rows[j]["BILLNO"].ToString().Trim();
                sw.WriteLine(_strBILLNO);

            }
            sw.Flush();
            sw.Close();
            sw.Dispose();

            TF_DATA objServerName = new TF_DATA();
            string _serverName = objServerName.GetServerName();

           // labelMessage.Text = "Files Created Successfully on Server : " + _serverName + " in " + _directoryPath;

            string path = "file://" + _serverName + "/CSVFILE/EBRCDATA";
            string link = "/CSVFILE/EBRCDATA/";
            labelMessage.Text = "Files Created Successfully on " + _serverName + " in " + "<a href=" + path + "> " + link + " </a>";
        }
        else
        {
            labelMessage.Text = "No Reocrds";
        }
    }
}