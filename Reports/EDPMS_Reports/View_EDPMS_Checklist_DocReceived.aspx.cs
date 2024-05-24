﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_EDPMS_Reports_View_EDPMS_Checklist_DocReceived : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["branchname"] != null)
            {
                string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;

                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ServerReport serverReport = ReportViewer1.ServerReport;
                serverReport.ReportServerUrl = new Uri(url);

                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_EDPMS_Checklist_DocReceived";

                string brname = Request.QueryString["branchname"].ToString();
                Microsoft.Reporting.WebForms.ReportParameter branchname = new Microsoft.Reporting.WebForms.ReportParameter();
                branchname.Name = "branch";
                branchname.Values.Add(brname);

                //string brname = Request.QueryString["branchname"].ToString();
                Microsoft.Reporting.WebForms.ReportParameter fromdate = new Microsoft.Reporting.WebForms.ReportParameter();
                fromdate.Name = "fromdate";
                fromdate.Values.Add(Request.QueryString["fromdate"]);

                Microsoft.Reporting.WebForms.ReportParameter todate = new Microsoft.Reporting.WebForms.ReportParameter();
                todate.Name = "todate";
                todate.Values.Add(Request.QueryString["todate"]);

                string username = Session["userName"].ToString();
                Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
                user.Name = "user";
                user.Values.Add(username);

                ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { branchname, fromdate, todate,user });

            }
        }
    }
}