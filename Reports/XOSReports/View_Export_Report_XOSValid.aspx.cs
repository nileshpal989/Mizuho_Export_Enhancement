﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_EXPORTReports_View_Export_Report_XOSValid : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Request.QueryString["fromDate"] != null)
            {
                string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
                // Set the processing mode for the ReportViewer to Remote

                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ServerReport serverReport = ReportViewer1.ServerReport;
                serverReport.ReportServerUrl = new Uri(url);

                // Set the report server URL and report path

                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptXOSInValid";



                string frmdate = DateTime.ParseExact(Request.QueryString["fromDate"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                string branches = Request.QueryString["Branch"].ToString();

                Microsoft.Reporting.WebForms.ReportParameter DocDate = new Microsoft.Reporting.WebForms.ReportParameter();
                DocDate.Name = "startdate";
                DocDate.Values.Add(frmdate);

                Microsoft.Reporting.WebForms.ReportParameter branch = new Microsoft.Reporting.WebForms.ReportParameter();
                branch.Name = "Branch";
                branch.Values.Add(branches);

                Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
                user.Name = "user";
                user.Values.Add(Session["userName"].ToString());

                ReportViewer1.ServerReport.SetParameters(
                   new Microsoft.Reporting.WebForms.ReportParameter[] { DocDate, branch, user });
            }
        }
    }
}