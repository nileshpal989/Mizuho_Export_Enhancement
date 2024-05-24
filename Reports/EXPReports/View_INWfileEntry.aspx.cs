using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;

public partial class Reports_EXPReports_View_INWfileEntry : System.Web.UI.Page
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

            serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/View_INWfileEntry";

            Microsoft.Reporting.WebForms.ReportParameter userlog = new Microsoft.Reporting.WebForms.ReportParameter();
            userlog.Name = "user";
            userlog.Values.Add(Session["userName"].ToString());
            //
            Microsoft.Reporting.WebForms.ReportParameter Doc = new Microsoft.Reporting.WebForms.ReportParameter();
            Doc.Name = "DocNo";
            string DocNo = Request.QueryString["DocNo"];
            Doc.Values.Add(DocNo);

            ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { userlog, Doc });
        }
    }
}