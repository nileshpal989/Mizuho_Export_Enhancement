using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class TF_ViewrptAccessControl : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Microsoft.Reporting.WebForms.ReportParameter User_Name = new Microsoft.Reporting.WebForms.ReportParameter();
            User_Name.Name = "User_Name";
            string User_Name1 = "";
            //Microsoft.Reporting.WebForms.ReportParameter User = new Microsoft.Reporting.WebForms.ReportParameter();
            //User.Name = "User";

            string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
            // Set the processing mode for the ReportViewer to Remote

            ReportViewer1.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
            ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            ServerReport serverReport = ReportViewer1.ServerReport;
            serverReport.ReportServerUrl =
              new Uri(url);

            // Set the report server URL and report path
            if (Request.QueryString["rptCode"].ToString() == "1")
            {
                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptAccessControl";
                User_Name.Values.Add(" ");
            }

            if (Request.QueryString["rptCode"].ToString() == "2")
            {
                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptAccessControl";
                User_Name1 = Request.QueryString["rptType"].ToString();
                User_Name.Values.Add(User_Name1);
            }
            Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
            user.Name = "User";
            user.Values.Add(Session["userName"].ToString());

            ReportViewer1.ServerReport.SetParameters(
               new Microsoft.Reporting.WebForms.ReportParameter[] { User_Name, user });
        }
    }
}
