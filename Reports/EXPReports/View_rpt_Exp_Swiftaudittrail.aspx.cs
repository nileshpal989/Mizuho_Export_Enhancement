using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;

public partial class Reports_EXPReports_View_rpt_Exp_Swiftaudittrail : System.Web.UI.Page
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

            serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptEXP_Export_SwiftAuditTrail";
            Microsoft.Reporting.WebForms.ReportParameter startdate = new Microsoft.Reporting.WebForms.ReportParameter();
            startdate.Name = "Fromdate";
            string Frmdate1 = Request.QueryString["frm"].ToString().Trim();
            startdate.Values.Add(Frmdate1);

            Microsoft.Reporting.WebForms.ReportParameter enddate = new Microsoft.Reporting.WebForms.ReportParameter();
            enddate.Name = "Todate";
            string todate1 = Request.QueryString["to"].ToString().Trim();
            enddate.Values.Add(todate1);

            Microsoft.Reporting.WebForms.ReportParameter Branch = new Microsoft.Reporting.WebForms.ReportParameter();
            Branch.Name = "Branch";
            string Branch1 = Request.QueryString["Branch"];
            Branch.Values.Add(Branch1);

            Microsoft.Reporting.WebForms.ReportParameter userlog = new Microsoft.Reporting.WebForms.ReportParameter();
            userlog.Name = "user";
            userlog.Values.Add(Session["userName"].ToString());
            //
            Microsoft.Reporting.WebForms.ReportParameter menuname = new Microsoft.Reporting.WebForms.ReportParameter();
            menuname.Name = "DocType";
            string menu = Request.QueryString["Menu"];
            menuname.Values.Add(menu);

            ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { startdate, enddate, userlog, Branch, menuname });
        }
    }
}