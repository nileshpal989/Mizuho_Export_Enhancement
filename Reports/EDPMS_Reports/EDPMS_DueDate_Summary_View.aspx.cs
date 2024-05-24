using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_EDPMS_Reports_EDPMS_DueDate_Summary_View : System.Web.UI.Page
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

                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_EDPMS_BillDueDate_Holiday";

                Microsoft.Reporting.WebForms.ReportParameter branch = new Microsoft.Reporting.WebForms.ReportParameter();
                branch.Name = "branch";
                branch.Values.Add(Request.QueryString["branch"].ToString());

                Microsoft.Reporting.WebForms.ReportParameter AsOnDate = new Microsoft.Reporting.WebForms.ReportParameter();
                AsOnDate.Name = "AsOnDate";
                AsOnDate.Values.Add(Request.QueryString["AsOnDate"].ToString());
                            
                ReportParameter user = new ReportParameter();
                user.Name = "user";
                string u = Session["userName"].ToString();
                user.Values.Add(u);

                ReportViewer1.ServerReport.SetParameters(new ReportParameter[] { branch,AsOnDate, user });
            }
            catch (Exception ex)
            {

            }
        }
    }
}