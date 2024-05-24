using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using System.Data;

public partial class Reports_EXPORTReports_EXPORT_ViewCoveringScheduleLetterExportLC_Amtd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Microsoft.Reporting.WebForms.ReportParameter Frdocno = new Microsoft.Reporting.WebForms.ReportParameter();
            Frdocno.Name = "Frdocno";
            Microsoft.Reporting.WebForms.ReportParameter Todocno = new Microsoft.Reporting.WebForms.ReportParameter();
            Todocno.Name = "Todocno";
            string frdocno = "";
            string todocno = "";

            if (Request.QueryString["frm"] != null)
            {
                string url = WebConfigurationManager.ConnectionStrings["urlrpt"].ConnectionString;
                // Set the processing mode for the ReportViewer to Remote

                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.ConnectionStrings["user"].ConnectionString, WebConfigurationManager.ConnectionStrings["password"].ConnectionString, WebConfigurationManager.ConnectionStrings["domain"].ConnectionString);
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ServerReport serverReport = ReportViewer1.ServerReport;
                serverReport.ReportServerUrl = new Uri(url);
                Microsoft.Reporting.WebForms.ReportParameter pur = new Microsoft.Reporting.WebForms.ReportParameter();
                pur.Name = "pur";
                Microsoft.Reporting.WebForms.ReportParameter Print1 = new Microsoft.Reporting.WebForms.ReportParameter();
                Print1.Name = "Print";
                Microsoft.Reporting.WebForms.ReportParameter Print2 = new Microsoft.Reporting.WebForms.ReportParameter();
                Print2.Name = "Print2";
                Microsoft.Reporting.WebForms.ReportParameter Copy = new Microsoft.Reporting.WebForms.ReportParameter();
                Copy.Name = "Copy";

                // Set the report server URL and report path
                if (Request.QueryString["rptCode"] == "1")
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptExportCoveringScheduleLetter_Amtd";
                    frdocno = "";
                    todocno = "";
                    pur.Values.Add("0");

                }
                if (Request.QueryString["rptCode"] == "2")
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptExportCoveringScheduleLetter_Amtd";


                    frdocno = Request.QueryString["rptFrdocno"].ToString();
                    todocno = Request.QueryString["rptTodocno"].ToString();
                    pur.Values.Add("1");

                }


                string frmdate = DateTime.ParseExact(Request.QueryString["frm"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");

                Microsoft.Reporting.WebForms.ReportParameter startdate = new Microsoft.Reporting.WebForms.ReportParameter();
                startdate.Name = "startdate";
                startdate.Values.Add(frmdate);


                Microsoft.Reporting.WebForms.ReportParameter sessiontrue = new Microsoft.Reporting.WebForms.ReportParameter();
                sessiontrue.Name = "sessiontrue";
                sessiontrue.Values.Add("1");

                Microsoft.Reporting.WebForms.ReportParameter Branch = new Microsoft.Reporting.WebForms.ReportParameter();
                Branch.Name = "Branch";
                string branch = Request.QueryString["branch"];
                Branch.Values.Add(branch);
                Print1.Values.Add(Request.QueryString["Print1"]);
                Print2.Values.Add(Request.QueryString["Print2"]);
                Copy.Values.Add(Request.QueryString["Copy"]);

                Frdocno.Values.Add(frdocno);
                Todocno.Values.Add(todocno);

                ReportViewer1.ServerReport.SetParameters(
                   new Microsoft.Reporting.WebForms.ReportParameter[] { startdate, pur, Frdocno, Todocno, sessiontrue, Branch, Print1, Print2, Copy });
            }
        }
    }
}