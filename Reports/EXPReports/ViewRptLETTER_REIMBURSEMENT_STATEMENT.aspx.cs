using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reports_EXPReports_ViewRptLETTER_REIMBURSEMENT_STATEMENT : System.Web.UI.Page
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
                Microsoft.Reporting.WebForms.ReportParameter docno = new Microsoft.Reporting.WebForms.ReportParameter();
                docno.Name = "docno";               
                Microsoft.Reporting.WebForms.ReportParameter Remark2 = new Microsoft.Reporting.WebForms.ReportParameter();
                Remark2.Name = "Remark2";
                
                string remark2 = "";

                // Set the report server URL and report path
                if (Request.QueryString["rptCode"] == "1")
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptExp_LETTER_REIMBURSEMENT_STATEMENT";
                    frdocno = "";
                    todocno = "";
                    docno.Values.Add("All");
                }
                if (Request.QueryString["rptCode"] == "2")
                {
                    serverReport.ReportPath = "/Mizuho_TF_UAT_Reports/rptExp_LETTER_REIMBURSEMENT_STATEMENT";


                    frdocno = Request.QueryString["rptFrdocno"].ToString();
                    todocno = Request.QueryString["rptTodocno"].ToString();
                    docno.Values.Add("1");

                }
               
                remark2 = Request.QueryString["Remark2"].ToString();

                string frmdate = DateTime.ParseExact(Request.QueryString["frm"].ToString(), "dd/MM/yyyy", null).ToString("yyyy/MM/dd");

                Microsoft.Reporting.WebForms.ReportParameter DocDate = new Microsoft.Reporting.WebForms.ReportParameter();
                DocDate.Name = "DocDate";
                DocDate.Values.Add(frmdate);

                Microsoft.Reporting.WebForms.ReportParameter Branch = new Microsoft.Reporting.WebForms.ReportParameter();
                Branch.Name = "Branch";
                string branch = Request.QueryString["Branch"];
                Branch.Values.Add(branch);

                Microsoft.Reporting.WebForms.ReportParameter user = new Microsoft.Reporting.WebForms.ReportParameter();
                user.Name = "user";
                user.Values.Add(Session["userName"].ToString());


                Frdocno.Values.Add(frdocno);
                Todocno.Values.Add(todocno);                
                Remark2.Values.Add(remark2);

                ReportViewer1.ServerReport.SetParameters(
                   new Microsoft.Reporting.WebForms.ReportParameter[] { DocDate, docno, Frdocno, Todocno, Remark2, user, Branch });
            }
        }
    }
}