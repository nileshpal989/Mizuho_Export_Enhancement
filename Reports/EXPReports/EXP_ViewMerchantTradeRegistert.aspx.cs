using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_EXPReports_EXP_ViewMerchantTradeRegistert : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["frm"] != null)
            {
                string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
                // Set the processing mode for the ReportViewer to Remote

                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ServerReport serverReport = ReportViewer1.ServerReport;
                serverReport.ReportServerUrl =
                  new Uri(url);
                // Set the report server URL and report path

                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rpt_Exp_MerchantizeTradeDoc";

                Microsoft.Reporting.WebForms.ReportParameter branch = new Microsoft.Reporting.WebForms.ReportParameter();
                branch.Name = "branchCode";
                branch.Values.Add(Request.QueryString["branch"].ToString());

                Microsoft.Reporting.WebForms.ReportParameter fromDate = new Microsoft.Reporting.WebForms.ReportParameter();
                fromDate.Name = "fromDate";
                fromDate.Values.Add(Request.QueryString["frm"].ToString());

                Microsoft.Reporting.WebForms.ReportParameter toDate = new Microsoft.Reporting.WebForms.ReportParameter();
                toDate.Name = "toDate";
                toDate.Values.Add(Request.QueryString["to"].ToString());

                Microsoft.Reporting.WebForms.ReportParameter Status = new Microsoft.Reporting.WebForms.ReportParameter();
                Status.Name = "status";
                Status.Values.Add(Request.QueryString["Status"].ToString());

                Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
                user.Name = "user";
                user.Values.Add(Session["userName"].ToString());

                ReportViewer1.ServerReport.SetParameters(
                   new Microsoft.Reporting.WebForms.ReportParameter[] { fromDate, toDate, branch, Status, user });

            }
        }
    }
}