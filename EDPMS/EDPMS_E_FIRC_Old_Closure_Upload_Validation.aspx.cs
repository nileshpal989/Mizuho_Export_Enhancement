﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class EDPMS_EDPMS_E_FIRC_Old_Closure_Upload_Validation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
            // Set the processing mode for the ReportViewer to Remote

            ReportViewer1.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
            ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            ServerReport serverReport = ReportViewer1.ServerReport;
            serverReport.ReportServerUrl = new Uri(url);

            // Set the report server URL and report path
            serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_EDPMS_DataValidation_E_FIRC_Old_Closure";


            Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
            user.Name = "user";
            user.Values.Add(Session["userName"].ToString());

            Microsoft.Reporting.WebForms.ReportParameter adcode = new Microsoft.Reporting.WebForms.ReportParameter();
            adcode.Name = "adcode";
            adcode.Values.Add(Request.QueryString["adcode"].ToString());

            Microsoft.Reporting.WebForms.ReportParameter addedBy = new Microsoft.Reporting.WebForms.ReportParameter();
            addedBy.Name = "addedBy";
            addedBy.Values.Add(Session["userName"].ToString());

            ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { adcode, user, addedBy });
        }
    }
}