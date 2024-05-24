using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;


public partial class Reports_EXPORTReports_View_TF_Export_Nonsubmission_Export_Doc_followup : System.Web.UI.Page
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



            serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_Export_Nonsubmission_Export_Document_followup";
            Microsoft.Reporting.WebForms.ReportParameter branchCode = new Microsoft.Reporting.WebForms.ReportParameter();
            branchCode.Name = "branchCode";
            branchCode.Values.Add(Request.QueryString["Branch"].ToString());

            Microsoft.Reporting.WebForms.ReportParameter fromDate = new Microsoft.Reporting.WebForms.ReportParameter();
            fromDate.Name = "fromDate";
            fromDate.Values.Add(Request.QueryString["FromDate"].ToString());

            Microsoft.Reporting.WebForms.ReportParameter toDate = new Microsoft.Reporting.WebForms.ReportParameter();
            toDate.Name = "toDate";
            toDate.Values.Add(Request.QueryString["ToDate"].ToString());

            Microsoft.Reporting.WebForms.ReportParameter custacNo = new Microsoft.Reporting.WebForms.ReportParameter();
            custacNo.Name = "custacNo";
            custacNo.Values.Add(Request.QueryString["Cust"].ToString());

            //Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
            //user.Name = "user";
            //user.Values.Add(Session["userName"].ToString());


            ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { branchCode, fromDate, toDate, custacNo });
        }
    }
}