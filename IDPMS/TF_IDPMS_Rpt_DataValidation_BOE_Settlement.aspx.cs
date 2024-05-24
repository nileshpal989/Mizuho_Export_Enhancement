using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class IDPMS_TF_IDPMS_Rpt_DataValidation_BOE_Settlement : System.Web.UI.Page
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
            serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IDPMS_DataValidation_BOE_Settlement";

            Microsoft.Reporting.WebForms.ReportParameter ADCODE = new Microsoft.Reporting.WebForms.ReportParameter();
            ADCODE.Name = "ADCODE";
            ADCODE.Values.Add(Session["userADCode"].ToString());

            Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
            user.Name = "user";
            user.Values.Add(Session["userName"].ToString());

            Microsoft.Reporting.WebForms.ReportParameter Addedby = new Microsoft.Reporting.WebForms.ReportParameter();
            Addedby.Name = "Addedby";
            Addedby.Values.Add(Session["userName"].ToString());

            ReportViewer1.ServerReport.SetParameters(
              new Microsoft.Reporting.WebForms.ReportParameter[] { ADCODE, user, Addedby });
        }
    }
}