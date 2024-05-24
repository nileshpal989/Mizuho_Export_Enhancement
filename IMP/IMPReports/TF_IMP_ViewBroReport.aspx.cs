using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Globalization;

public partial class IMP_IMPReports_TF_IMP_ViewBroReport : System.Web.UI.Page
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


            serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/BROPRINTING";
           
            string Branchcode = Request.QueryString["Branchcode"].ToString();
            string Type = Request.QueryString["Type"].ToString();
            string Date = Request.QueryString["Date"].ToString();
           // string userName = Session["userName"].ToString();

            Microsoft.Reporting.WebForms.ReportParameter Asondate = new Microsoft.Reporting.WebForms.ReportParameter();
            Asondate.Name = "Date";
            Asondate.Values.Add(Date);

            Microsoft.Reporting.WebForms.ReportParameter Branch = new Microsoft.Reporting.WebForms.ReportParameter();
            Branch.Name = "branch";
            Branch.Values.Add(Branchcode);

            Microsoft.Reporting.WebForms.ReportParameter Type1 = new Microsoft.Reporting.WebForms.ReportParameter();
            Type1.Name = "BRO";
            Type1.Values.Add(Type);

            ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { Asondate, Branch, Type1 });
        }
    }
}