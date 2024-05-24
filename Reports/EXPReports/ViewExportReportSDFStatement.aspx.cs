using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_EXPReports_ViewExportReportSDFStatement : System.Web.UI.Page
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


            serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_exp_SDFForm";
            Microsoft.Reporting.WebForms.ReportParameter Branch = new Microsoft.Reporting.WebForms.ReportParameter();
            Branch.Name = "Branch";
            Branch.Values.Add(Request.QueryString["Branch"].ToString());

            Microsoft.Reporting.WebForms.ReportParameter fromDate = new Microsoft.Reporting.WebForms.ReportParameter();
            fromDate.Name = "fromDate";
            fromDate.Values.Add(Request.QueryString["fromDate"].ToString());

            Microsoft.Reporting.WebForms.ReportParameter toDate = new Microsoft.Reporting.WebForms.ReportParameter();
            toDate.Name = "toDate";
            toDate.Values.Add(Request.QueryString["toDate"].ToString());

            Microsoft.Reporting.WebForms.ReportParameter documentNo = new Microsoft.Reporting.WebForms.ReportParameter();
            documentNo.Name = "documentNo";
            documentNo.Values.Add(Request.QueryString["documentNo"].ToString());

            //Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
            //user.Name = "user";
            //user.Values.Add(Session["userName"].ToString());


            ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { Branch, fromDate, toDate, documentNo });
        }
    }
}