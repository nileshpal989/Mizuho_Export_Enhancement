using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_EBRReports_View_rptEBR_Reports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["frm"] != null && Request.QueryString["to"] != null)
            {
                string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;

                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ServerReport serverReport = ReportViewer1.ServerReport;
                serverReport.ReportServerUrl = new Uri(url);

                // Set the report server URL and report path

                switch (Request.QueryString["Report"])
                {
                    case "Data Check List":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptDataCheckListEBRC";
                        break;
                    //case "Data Validation":
                    //    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptEBWRegister";
                    //    break;
                    case "List of E-BRC Certificate Generated":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptListofCertificateGenerated";
                        break;
                    case "E-BRC Certificate to be Generated":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptCertificateToBeGenerated";
                        break;
                    case "List of BRC's Cancelled":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptListofBRCCancelled";
                        break;
                    case "Bills Fully Realised - But No GR Details":
                        serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptBillsFullyRealisedWithoutGRDetails";
                        break;
                }

                string frmdate = DateTime.ParseExact(Request.QueryString["frm"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                string todate = DateTime.ParseExact(Request.QueryString["to"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");

                Microsoft.Reporting.WebForms.ReportParameter startdate = new Microsoft.Reporting.WebForms.ReportParameter();
                startdate.Name = "startdate";
                startdate.Values.Add(frmdate);

                Microsoft.Reporting.WebForms.ReportParameter enddate = new Microsoft.Reporting.WebForms.ReportParameter();
                enddate.Name = "enddate";
                enddate.Values.Add(todate);

                Microsoft.Reporting.WebForms.ReportParameter Branch = new Microsoft.Reporting.WebForms.ReportParameter();
                Branch.Name = "Branch";
                string Branch1 = Request.QueryString["Branch"];
                Branch.Values.Add(Branch1);

                Microsoft.Reporting.WebForms.ReportParameter Customer = new Microsoft.Reporting.WebForms.ReportParameter();
                Customer.Name = "CustAcNo";
                string Customer1= Request.QueryString["Customer"];
                Customer.Values.Add(Customer1);

                Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
                user.Name = "user";
                user.Values.Add(Session["userName"].ToString());

                ReportViewer1.ServerReport.SetParameters(new Microsoft.Reporting.WebForms.ReportParameter[] { startdate,enddate,Branch,Customer,user});

            
            }
        }
    }
}