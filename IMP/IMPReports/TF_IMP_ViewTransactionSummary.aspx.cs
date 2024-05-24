using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class IMP_IMPReports_TF_IMP_ViewTransactionSummary : System.Web.UI.Page
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


            serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_Import_Transaction_Summary";

            string fromdate = Request.QueryString["fromdate"].ToString();
            string todate = Request.QueryString["todate"].ToString();
            string User = Session["userName"].ToString();

            Microsoft.Reporting.WebForms.ReportParameter Fromdate = new Microsoft.Reporting.WebForms.ReportParameter();
            Fromdate.Name = "fromdate";
            Fromdate.Values.Add(fromdate);

            Microsoft.Reporting.WebForms.ReportParameter Todate = new Microsoft.Reporting.WebForms.ReportParameter();
            Todate.Name = "todate";
            Todate.Values.Add(todate);

            Microsoft.Reporting.WebForms.ReportParameter User1 = new Microsoft.Reporting.WebForms.ReportParameter();
            User1.Name = "User";
            User1.Values.Add(User);

            ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { Fromdate, Todate, User1 });
        }
    }
}