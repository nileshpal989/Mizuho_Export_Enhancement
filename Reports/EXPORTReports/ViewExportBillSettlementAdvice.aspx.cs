using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Data.SqlClient;

public partial class Reports_EXPORTReports_ViewExportBillSettlementAdvice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
        {

            Microsoft.Reporting.WebForms.ReportParameter DocumentNo = new Microsoft.Reporting.WebForms.ReportParameter();
            DocumentNo.Name = "DocumentNo";
            string frdocno = "";
            string todocno = "";
            string SrNo1 = "";

            string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
            ReportViewer1.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
            ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            ServerReport serverReport = ReportViewer1.ServerReport;
            serverReport.ReportServerUrl = new Uri(url);
            todocno = Request.QueryString["DocSrNo"].ToString().Replace("-", "/").Replace("-", "/");
            SrNo1 = Request.QueryString["SrNo"].ToString();
            string abc = todocno.Substring(0, 3);
            if (abc == "BCA" || abc == "BCU")
            {
                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/ExportBillSettlementAdviceBCUBCA";
            }
            else
            {
                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/ExportBillSettlementAdvice";
            }
            
            Microsoft.Reporting.WebForms.ReportParameter BranchCode = new Microsoft.Reporting.WebForms.ReportParameter();
            BranchCode.Name = "BranchCode";
            string branch = Request.QueryString["BranchName"].ToString();
            BranchCode.Values.Add(branch);

            Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
            user.Name = "user";
            DocumentNo.Values.Add(todocno);

            Microsoft.Reporting.WebForms.ReportParameter SrNo = new Microsoft.Reporting.WebForms.ReportParameter();
            SrNo.Name = "SrNo";
            SrNo.Values.Add(SrNo1);

            FillDatails();

            ReportViewer1.ServerReport.DisplayName = CustAbbr + "_" + DocumentNo1 + "_" + DocumentDate;// Anand 26-10-2023
            ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { DocumentNo, BranchCode,SrNo });

        }
    }
    //----------------------Anand 26-10-2023---------------------------------
    string CustAbbr = "";
    string DocumentDate = "";
    String DocumentNo1 = "";
    public void FillDatails()
    {
        string todocno = Request.QueryString["DocSrNo"].ToString().Replace("-", "/").Replace("-", "/");
        string branchcode = Request.QueryString["BranchName"].ToString();
        SqlParameter p1 = new SqlParameter("@DocumentNo", SqlDbType.VarChar);
        p1.Value = todocno;
        SqlParameter p2 = new SqlParameter("@BranchCode", SqlDbType.VarChar);
        p2.Value = branchcode;
        string _query = "FillSettlementDocumentNo";
        TF_DATA objData = new TF_DATA();
        DataTable dt = objData.getData(_query, p1, p2);
        if (dt.Rows.Count > 0)
        {
            CustAbbr = dt.Rows[0]["Cust_Abbr"].ToString();
            DocumentNo1 = dt.Rows[0]["Document_No"].ToString().Replace("/", "").Replace("_", "");
            DocumentDate = dt.Rows[0]["Realised_Date"].ToString().Replace("/", "").Replace("_", "");
        }
    }
    //---------------------------End-----------------------------------------------
}