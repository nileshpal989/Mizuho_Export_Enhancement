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

public partial class Reports_ImportReport_Viewapprovechecker : System.Web.UI.Page
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

            serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/CheckerReoprt";

            string branch = Request.QueryString["branchname"].ToString();
            string DocumentType = Request.QueryString["Documentype"].ToString();
            string fromdate = Request.QueryString["from"].ToString();
            string todate = Request.QueryString["to"].ToString();
            string userName = Session["userName"].ToString();
            string Approval = Request.QueryString["Approval"].ToString();
            // string modu = Request.QueryString["ModuleType"].ToString();

            Microsoft.Reporting.WebForms.ReportParameter startdate = new Microsoft.Reporting.WebForms.ReportParameter();
            startdate.Name = "startdate";
            startdate.Values.Add(fromdate);

            Microsoft.Reporting.WebForms.ReportParameter enddate = new Microsoft.Reporting.WebForms.ReportParameter();
            enddate.Name = "enddate";
            enddate.Values.Add(todate);

            Microsoft.Reporting.WebForms.ReportParameter Branch = new Microsoft.Reporting.WebForms.ReportParameter();
            Branch.Name = "Branch";
            Branch.Values.Add(branch);

            Microsoft.Reporting.WebForms.ReportParameter Status = new Microsoft.Reporting.WebForms.ReportParameter();
            Status.Name = "Status";
            Status.Values.Add(Approval);

            Microsoft.Reporting.WebForms.ReportParameter Document_Type = new Microsoft.Reporting.WebForms.ReportParameter();
            Document_Type.Name = "Document_Type";
            Document_Type.Values.Add(DocumentType);

            Microsoft.Reporting.WebForms.ReportParameter UserName = new Microsoft.Reporting.WebForms.ReportParameter();
            UserName.Name = "UserName";
            UserName.Values.Add(userName);

            //Microsoft.Reporting.WebForms.ReportParameter module = new Microsoft.Reporting.WebForms.ReportParameter();
            //module.Name = "ModuleType";
            //module.Values.Add("IMPWH");

            ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { Document_Type, Status,UserName, startdate, enddate, Branch });
        }
    }
}
