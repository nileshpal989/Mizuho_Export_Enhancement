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

public partial class IMP_IMPReports_View_TF_IMP_Cash_equivalent_VouchersBalance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string currency = Request.QueryString["Currency"].ToString();
            string fromDate = Request.QueryString["fromDate"].ToString();
            string ToDate = Request.QueryString["toDate"].ToString();
            //  string userName = Session["userName"].ToString();
            string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;

            ReportViewer1.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
            ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            ServerReport serverReport = ReportViewer1.ServerReport;
            serverReport.ReportServerUrl = new Uri(url);

            serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/TF_IMP_Cash_Equivalent_Vouchers";

            Microsoft.Reporting.WebForms.ReportParameter frmdate = new Microsoft.Reporting.WebForms.ReportParameter();
            frmdate.Name = "fromdate";
            frmdate.Values.Add(fromDate);

            Microsoft.Reporting.WebForms.ReportParameter Todate = new Microsoft.Reporting.WebForms.ReportParameter();
            Todate.Name = "todate";
            Todate.Values.Add(ToDate);

            Microsoft.Reporting.WebForms.ReportParameter curr = new Microsoft.Reporting.WebForms.ReportParameter();
            curr.Name = "Document_Curr";
            curr.Values.Add(currency);

            //Microsoft.Reporting.WebForms.ReportParameter UserName = new Microsoft.Reporting.WebForms.ReportParameter();
            //UserName.Name = "user";
            //UserName.Values.Add(userName);

            ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { frmdate, Todate, curr });
        }
    }
}