using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_EDPMS_Reports_View_EDPMS_XMLFile_PendingGenerated : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
            // Set the processing mode for the ReportViewer to Remote

            ReportViewer1.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
            ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            ServerReport serverReport = ReportViewer1.ServerReport;
            serverReport.ReportServerUrl =
              new Uri(url);

            serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_EDPMS_XMLPending_Created";

            string brname = Request.QueryString["branchname"].ToString();
            Microsoft.Reporting.WebForms.ReportParameter branchname = new Microsoft.Reporting.WebForms.ReportParameter();
            branchname.Name = "branchname";
            branchname.Values.Add(brname);

            string username = Session["userName"].ToString();
            Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
            user.Name = "user";
            user.Values.Add(username);

            Microsoft.Reporting.WebForms.ReportParameter date = new Microsoft.Reporting.WebForms.ReportParameter();
            date.Name = "date";
            date.Values.Add(Request.QueryString["date"]);

            Microsoft.Reporting.WebForms.ReportParameter doctype = new Microsoft.Reporting.WebForms.ReportParameter();
            doctype.Name = "doctype";
            doctype.Values.Add(Request.QueryString["doctype"]);

            Microsoft.Reporting.WebForms.ReportParameter status = new Microsoft.Reporting.WebForms.ReportParameter();
            status.Name = "status";
            status.Values.Add(Request.QueryString["status"]);

            ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { branchname, user, date, doctype, status });
        }
    }
}