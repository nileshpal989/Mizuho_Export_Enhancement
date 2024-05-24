using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Globalization;

public partial class IMP_IMPReports_TF_IMP_ViewExpoImpoDocControlSheetReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string Branchcode = Request.QueryString["Branchcode"].ToString();
            string txtFromDate = Request.QueryString["fromDate"].ToString();
            string txtToDate = Request.QueryString["toDate"].ToString();
            string userName = Session["userName"].ToString();
            string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;

            ReportViewer1.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
            ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            ServerReport serverReport = ReportViewer1.ServerReport;
            serverReport.ReportServerUrl = new Uri(url);


            serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_ExpoImpoDocControlSheetReport";


            Microsoft.Reporting.WebForms.ReportParameter Fromdate = new Microsoft.Reporting.WebForms.ReportParameter();
            Fromdate.Name = "FROMDATE";
            Fromdate.Values.Add(txtFromDate);

            Microsoft.Reporting.WebForms.ReportParameter Todate = new Microsoft.Reporting.WebForms.ReportParameter();
            Todate.Name = "TODATE";
            Todate.Values.Add(txtToDate);

            Microsoft.Reporting.WebForms.ReportParameter Branch = new Microsoft.Reporting.WebForms.ReportParameter();
            Branch.Name = "BRANCHCODE";
            Branch.Values.Add(Branchcode);


            Microsoft.Reporting.WebForms.ReportParameter UserName = new Microsoft.Reporting.WebForms.ReportParameter();
            UserName.Name = "user";
            UserName.Values.Add(userName);

            ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { UserName, Branch, Fromdate, Todate });
        }
    }
}
