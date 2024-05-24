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

public partial class IMP_IMPReports_TF_IMP_View_RecofOutgoingMalisandCourier : System.Web.UI.Page
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


            serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_RecOfOutgoingRegiMailsandCourier";
            //serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Test_Outgoing_services";


            string txtdate = Request.QueryString["txtdate"].ToString();
            string Username = Session["userName"].ToString();

            Microsoft.Reporting.WebForms.ReportParameter Date = new Microsoft.Reporting.WebForms.ReportParameter();
            Date.Name = "Date";
            Date.Values.Add(txtdate);

            Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
            user.Name = "user";
            user.Values.Add(Username);

            ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { Date, user });
        }
    }
}