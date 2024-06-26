﻿using System;
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

public partial class IDPMS_TF_IDPMS_ORMACkUpldRpt_View : System.Web.UI.Page
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

            serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IDPMS_ORMACKFileUpldRpt";

            string branch = Request.QueryString["branchname"].ToString();
            string fromdate = Request.QueryString["from"].ToString();
            string todate = Request.QueryString["to"].ToString();
            string userName = Session["userName"].ToString();
            string Cust1 = Request.QueryString["Cust"].ToString();

            Microsoft.Reporting.WebForms.ReportParameter FROMDT = new Microsoft.Reporting.WebForms.ReportParameter();
            FROMDT.Name = "FromDate";
            FROMDT.Values.Add(fromdate);

            Microsoft.Reporting.WebForms.ReportParameter TODT = new Microsoft.Reporting.WebForms.ReportParameter();
            TODT.Name = "ToDate";
            TODT.Values.Add(todate);

            Microsoft.Reporting.WebForms.ReportParameter Cust = new Microsoft.Reporting.WebForms.ReportParameter();
            Cust.Name = "CustIECode";
            Cust.Values.Add(Cust1);

            Microsoft.Reporting.WebForms.ReportParameter branch1 = new Microsoft.Reporting.WebForms.ReportParameter();
            branch1.Name = "branch";
            branch1.Values.Add(branch);

            Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
            user.Name = "user";
            user.Values.Add(userName);

            ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { FROMDT, TODT, branch1, Cust, user });
        }
    }
}