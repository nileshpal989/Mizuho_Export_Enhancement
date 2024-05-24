using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_EXPReports_ViewrptExportDataStatusReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
            ReportViewer1.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
            ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            ServerReport serverReport = ReportViewer1.ServerReport;
            serverReport.ReportServerUrl = new Uri(url);


            serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptDataStatusDetails";
            Microsoft.Reporting.WebForms.ReportParameter Branch = new Microsoft.Reporting.WebForms.ReportParameter();
            Branch.Name = "branchcode";
            Branch.Values.Add(Request.QueryString["Branch"].ToString());

            Microsoft.Reporting.WebForms.ReportParameter fromDate = new Microsoft.Reporting.WebForms.ReportParameter();
            fromDate.Name = "fromdate";
            fromDate.Values.Add(Request.QueryString["fromdate"].ToString());

            Microsoft.Reporting.WebForms.ReportParameter toDate = new Microsoft.Reporting.WebForms.ReportParameter();
            toDate.Name = "todate";
            toDate.Values.Add(Request.QueryString["todate"].ToString());

            Microsoft.Reporting.WebForms.ReportParameter username = new Microsoft.Reporting.WebForms.ReportParameter();
            username.Name = "username";
            username.Values.Add(Session["userName"].ToString());

            Microsoft.Reporting.WebForms.ReportParameter ADCode = new Microsoft.Reporting.WebForms.ReportParameter();
            ADCode.Name = "ADCode";
            ADCode.Values.Add(Session["userADCode"].ToString());

            ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { Branch, fromDate, toDate, username, ADCode });
        }
    }
}