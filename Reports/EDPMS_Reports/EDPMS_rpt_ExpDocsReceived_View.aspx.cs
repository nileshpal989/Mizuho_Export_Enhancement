using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class EDPMS_EDPMS_rpt_ExpDocsReceived_View : System.Web.UI.Page
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

            // Set the report server URL and report path

            if (Request.QueryString["Header"].ToString() == "EDPMS Realization")
            {
                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rpt_EDPMS_Realization";
            }
            if (Request.QueryString["Header"].ToString() == "Export Bill CSV File")
            {
                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rpt_EDPMS_EXP_Docs_Received";
            }
            Microsoft.Reporting.WebForms.ReportParameter branchid = new Microsoft.Reporting.WebForms.ReportParameter();
            branchid.Name = "branchid";
            branchid.Values.Add(Request.QueryString["branchid"].ToString());

            Microsoft.Reporting.WebForms.ReportParameter fromdate = new Microsoft.Reporting.WebForms.ReportParameter();
            fromdate.Name = "fromdate";
            fromdate.Values.Add(Request.QueryString["fromdate"].ToString());

            Microsoft.Reporting.WebForms.ReportParameter todate = new Microsoft.Reporting.WebForms.ReportParameter();
            todate.Name = "todate";
            todate.Values.Add(Request.QueryString["todate"].ToString());

            Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
            user.Name = "user";
            user.Values.Add(Session["userName"].ToString());

            ReportViewer1.ServerReport.SetParameters(
               new Microsoft.Reporting.WebForms.ReportParameter[] { branchid, fromdate, todate, user });
        }
    }
}