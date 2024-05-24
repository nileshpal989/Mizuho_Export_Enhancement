using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;
public partial class Reports_RRETURNReports_ViewrptRRETURN_Purpose_Control_Totals : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["frm"] != null && Request.QueryString["to"] != null)
            {
                try
                {
                    string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
                    //ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                    ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                    IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
                    ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                    ServerReport serverReport = ReportViewer1.ServerReport;
                    serverReport.ReportServerUrl = new Uri(url);
                    switch (Request.QueryString["Report"])
                    {
                        case "Purpose Code Wise Control Totals":
                            serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptRRETURN_PURPOSE_CONTROLS_TOTAL";
                            break;
                        case "Data Statistics":
                            serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptRRETURN_Data_Statistics";
                            break;
                        case "Data Validation":
                            serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptRRETURN_Data_Validation";
                            break;
                        case "R RETURN Cover Page Total":
                            serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptRRETURN_CoverPageTotal";
                            break;
                    }
                    string frmdate = DateTime.ParseExact(Request.QueryString["frm"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                    string todate = DateTime.ParseExact(Request.QueryString["to"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                    Microsoft.Reporting.WebForms.ReportParameter startdate = new Microsoft.Reporting.WebForms.ReportParameter();
                    startdate.Name = "startdate";
                    startdate.Values.Add(frmdate);
                    Microsoft.Reporting.WebForms.ReportParameter enddate = new Microsoft.Reporting.WebForms.ReportParameter();
                    enddate.Name = "enddate";
                    enddate.Values.Add(todate);
                    Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
                    user.Name = "user";
                    user.Values.Add(Session["userName"].ToString());
                    Microsoft.Reporting.WebForms.ReportParameter branch = new Microsoft.Reporting.WebForms.ReportParameter();
                    branch.Name = "Branch";
                    branch.Values.Add(Request.QueryString["branch"]);
                    ReportViewer1.ServerReport.SetParameters( new ReportParameter[] { startdate, enddate, branch, user });
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}