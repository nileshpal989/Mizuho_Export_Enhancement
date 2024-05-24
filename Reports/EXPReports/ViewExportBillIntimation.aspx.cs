using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_EXPReports_ViewExportBillIntimation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Request.QueryString["frm"] != null)
            {
                string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;

                // Set the processing mode for the ReportViewer to Remote

                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ServerReport serverReport = ReportViewer1.ServerReport;
                serverReport.ReportServerUrl = new Uri(url);

                string frmdate = DateTime.ParseExact(Request.QueryString["frm"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");
                Microsoft.Reporting.WebForms.ReportParameter startdate = new Microsoft.Reporting.WebForms.ReportParameter();
                startdate.Name = "startdate";
                startdate.Values.Add(frmdate);

                Microsoft.Reporting.WebForms.ReportParameter Branch = new Microsoft.Reporting.WebForms.ReportParameter();
                Branch.Name = "Branch";
                string branch = Request.QueryString["Branch"].ToString();
                Branch.Values.Add(branch);

                Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
                user.Name = "user";
                user.Values.Add(Session["userName"].ToString());

                string frdocno = "";
                string todocno = "";
             
                Microsoft.Reporting.WebForms.ReportParameter pur = new Microsoft.Reporting.WebForms.ReportParameter();
                pur.Name = "pur";

                // Set the report server URL and report path
                if (Request.QueryString["rptCode"] == "1")
                {
                    frdocno=("All");
                    todocno=("All");
                    pur.Values.Add("0");
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Export_Report_Bill_Intimation";
                    
                }
                if (Request.QueryString["rptCode"] == "2")
                {
                    
                    frdocno = Request.QueryString["rptFrdocno"].ToString();
                    todocno = Request.QueryString["rptTodocno"].ToString();
                    pur.Values.Add("1");
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/Export_Report_Bill_Intimation";
                }
                Microsoft.Reporting.WebForms.ReportParameter Frdocno = new Microsoft.Reporting.WebForms.ReportParameter();
                Frdocno.Name = "Frdocno";
                Frdocno.Values.Add(frdocno);
                Microsoft.Reporting.WebForms.ReportParameter Todocno = new Microsoft.Reporting.WebForms.ReportParameter();
                Todocno.Name = "Todocno";
                Todocno.Values.Add(todocno);
               

                ReportViewer1.ServerReport.SetParameters(
                   new Microsoft.Reporting.WebForms.ReportParameter[] { startdate, pur, Frdocno, Todocno,  Branch });
            }
        }
    }
}