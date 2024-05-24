using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_EXPReports_ViewExportReportBRCRegister : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Microsoft.Reporting.WebForms.ReportParameter from = new Microsoft.Reporting.WebForms.ReportParameter();
            from.Name = "FDate";

            Microsoft.Reporting.WebForms.ReportParameter To = new Microsoft.Reporting.WebForms.ReportParameter();
            To.Name = "TDate";

            Microsoft.Reporting.WebForms.ReportParameter DocType = new Microsoft.Reporting.WebForms.ReportParameter();
            DocType.Name = "DocType";

            Microsoft.Reporting.WebForms.ReportParameter FCust = new Microsoft.Reporting.WebForms.ReportParameter();
            FCust.Name = "FCust";

            Microsoft.Reporting.WebForms.ReportParameter TCust = new Microsoft.Reporting.WebForms.ReportParameter();
            TCust.Name = "TCust";

            Microsoft.Reporting.WebForms.ReportParameter CustType = new Microsoft.Reporting.WebForms.ReportParameter();
            CustType.Name = "CustType";

            Microsoft.Reporting.WebForms.ReportParameter User = new Microsoft.Reporting.WebForms.ReportParameter();
            User.Name = "User";

            Microsoft.Reporting.WebForms.ReportParameter Branch = new Microsoft.Reporting.WebForms.ReportParameter();
            Branch.Name = "Branch";

            if (Request.QueryString["from"] != null)
            {
                string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
                // Set the processing mode for the ReportViewer to Remote

                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ServerReport serverReport = ReportViewer1.ServerReport;
                serverReport.ReportServerUrl = new Uri(url);

                serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptExpBRCRegister";

                    string frmdate = DateTime.ParseExact(Request.QueryString["from"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                    from.Values.Add(frmdate);

                    string toDate = DateTime.ParseExact(Request.QueryString["To"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                    To.Values.Add(toDate);

                    string Branch1 = Request.QueryString["Branch"];
                    Branch.Values.Add(Branch1);

                    string DocType1 = Request.QueryString["DocType"];
                    DocType.Values.Add(DocType1);

                    string CustType1 = Request.QueryString["CustType"];
                    CustType.Values.Add(CustType1);

                    string FDoc1 = Request.QueryString["FCust"];
                    FCust.Values.Add(FDoc1);

                    string TDoc1 = Request.QueryString["TCust"];
                    TCust.Values.Add(TDoc1);

                    User.Values.Add(Session["userName"].ToString());

                    ReportViewer1.ServerReport.SetParameters(
                       new Microsoft.Reporting.WebForms.ReportParameter[] { from , To ,  DocType, Branch, FCust, TCust, CustType  , User });

               
                
            }
        }
    }
}