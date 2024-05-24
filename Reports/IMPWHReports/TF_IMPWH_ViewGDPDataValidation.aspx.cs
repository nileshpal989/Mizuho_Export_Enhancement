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

public partial class Reports_IMPWHReports_TF_IMPWH_ViewGDPDataValidation : System.Web.UI.Page
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

            if (Request.QueryString["Type"].ToString() == "GTP")
            {
                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMPWH_GoodToPayValidationReport";
            }
            if (Request.QueryString["Type"].ToString() == "Payment")
            {
                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMPWH_PaymentValidationReport";
            }
            string _userName = Session["userName"].ToString();
            string _IECode = Request.QueryString["IECode"].ToString();            

            Microsoft.Reporting.WebForms.ReportParameter User = new Microsoft.Reporting.WebForms.ReportParameter();
            User.Name = "User";
            User.Values.Add(_userName);

            Microsoft.Reporting.WebForms.ReportParameter IECode = new Microsoft.Reporting.WebForms.ReportParameter();
            IECode.Name = "IECode";
            IECode.Values.Add(_IECode);

            ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { User, IECode });
        }
    }
}