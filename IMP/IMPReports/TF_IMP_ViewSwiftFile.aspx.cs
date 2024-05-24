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

public partial class IMP_IMPReports_TF_IMP_ViewSwiftFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["frm"] != null)
            {
                string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ServerReport serverReport = ReportViewer1.ServerReport;
                serverReport.ReportServerUrl = new Uri(url);
                // Set the report server URL and report path

                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_Swift_Generated";
                
                Microsoft.Reporting.WebForms.ReportParameter Fromdate = new Microsoft.Reporting.WebForms.ReportParameter();
                Fromdate.Name = "Fromdate";
                string Frmdate1 = Request.QueryString["frm"].ToString().Trim();
                Fromdate.Values.Add(Frmdate1);

                Microsoft.Reporting.WebForms.ReportParameter Todate = new Microsoft.Reporting.WebForms.ReportParameter();
                Todate.Name = "Todate";
                string todate1 = Request.QueryString["to"].ToString().Trim();
                Todate.Values.Add(todate1);

                Microsoft.Reporting.WebForms.ReportParameter Branch = new Microsoft.Reporting.WebForms.ReportParameter();
                Branch.Name = "Branch";
                string Branch1 = Request.QueryString["Branch"];
                Branch.Values.Add(Branch1);

                Microsoft.Reporting.WebForms.ReportParameter User = new Microsoft.Reporting.WebForms.ReportParameter();
                User.Name = "User";
                User.Values.Add(Session["userName"].ToString());
                //
                Microsoft.Reporting.WebForms.ReportParameter DocType = new Microsoft.Reporting.WebForms.ReportParameter();
                DocType.Name = "DocType";
                string menu = Request.QueryString["Menu"];
                DocType.Values.Add(menu);

                ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { Fromdate, Todate, User, Branch, DocType });
            }
        }
    }
}