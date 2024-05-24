using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;
public partial class Reports_EXPReports_EXP_DocsReceived_ADTransfer_View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ServerReport serverReport = ReportViewer1.ServerReport;
                serverReport.ReportServerUrl = new Uri(url);

                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptEXPDocReceived_ADTransfer";

                Microsoft.Reporting.WebForms.ReportParameter branchid = new Microsoft.Reporting.WebForms.ReportParameter();
                branchid.Name = "branchid";
                branchid.Values.Add(Request.QueryString["branchid"].ToString());

                Microsoft.Reporting.WebForms.ReportParameter fromdate = new Microsoft.Reporting.WebForms.ReportParameter();
                fromdate.Name = "fromdate";
                fromdate.Values.Add(Request.QueryString["fromdate"].ToString());

                Microsoft.Reporting.WebForms.ReportParameter todate = new Microsoft.Reporting.WebForms.ReportParameter();
                todate.Name = "todate";
                todate.Values.Add(Request.QueryString["todate"].ToString());

                ReportParameter user = new ReportParameter();
                user.Name = "user";
                string u = Session["userName"].ToString();
                user.Values.Add(u);
                               
                ReportViewer1.ServerReport.SetParameters(new ReportParameter[] { branchid, fromdate, todate, user });
            }
            catch (Exception ex)
            {               
               
            }
        }
    }
}