using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_EXPReports_ViewEXP_TransStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
                //ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ServerReport serverReport = ReportViewer1.ServerReport;
                serverReport.ReportServerUrl = new Uri(url);

                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptAllTransStatus";

                string frmdate = DateTime.ParseExact(Request.QueryString["frm"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                ReportParameter frmDate = new ReportParameter();
                frmDate.Name = "frmDate";
                frmDate.Values.Add(frmdate);

                string todate = DateTime.ParseExact(Request.QueryString["to"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                ReportParameter toDate = new ReportParameter();
                toDate.Name = "toDate";
                toDate.Values.Add(todate);

                ReportParameter user = new ReportParameter();
                user.Name = "user";
                string u = Session["userName"].ToString();
                user.Values.Add(u);

                ReportViewer1.ServerReport.SetParameters(
                    new ReportParameter[] { frmDate, toDate, user });

            }
            catch (Exception ex)
            {

            }
        }
    }
}